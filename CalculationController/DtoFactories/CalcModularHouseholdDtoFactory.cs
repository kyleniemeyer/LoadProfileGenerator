﻿using System;
using System.Collections.Generic;
using System.Linq;
using Automation;
using Automation.ResultFiles;
using CalculationController.CalcFactories;
using CalculationEngine.HouseholdElements;
using Common;
using Common.CalcDto;
using Common.JSON;
using Common.SQLResultLogging.Loggers;
using Database;
using Database.Helpers;
using Database.Tables.BasicElements;
using Database.Tables.ModularHouseholds;
using Database.Tables.Transportation;
using JetBrains.Annotations;

namespace CalculationController.DtoFactories
{
    public class CalcModularHouseholdDtoFactory
    {
        [JetBrains.Annotations.NotNull]
        private readonly CalcLoadTypeDtoDictionary _ltDict;
        [JetBrains.Annotations.NotNull]
        private readonly Random _random;
        [JetBrains.Annotations.NotNull]
        private readonly CalcPersonDtoFactory _calcPersonDtoFactory;
        [JetBrains.Annotations.NotNull]
        private readonly CalcDeviceDtoFactory _calcDeviceDtoFactory;
        [JetBrains.Annotations.NotNull]
        private readonly CalcLocationDtoFactory _calcLocationDtoFactory;
        [JetBrains.Annotations.NotNull]
        private readonly CalcVariableDtoFactory _calcVariableRepositoryDtoFactory;
        [JetBrains.Annotations.NotNull]
        private readonly CalcAffordanceDtoFactory _calcAffordanceDtoFactory;
        [JetBrains.Annotations.NotNull]
        private readonly CalcTransportationDtoFactory _transportationDtoFactory;

        private readonly CalcRepo _calcRepo;


        public CalcModularHouseholdDtoFactory([JetBrains.Annotations.NotNull] CalcLoadTypeDtoDictionary ltDict, [JetBrains.Annotations.NotNull] Random random,
            [JetBrains.Annotations.NotNull] CalcPersonDtoFactory calcPersonDtoFactory,
            [JetBrains.Annotations.NotNull] CalcDeviceDtoFactory calcDeviceDtoFactory,
            [JetBrains.Annotations.NotNull] CalcLocationDtoFactory calcLocationDtoFactory,
            [JetBrains.Annotations.NotNull] CalcVariableDtoFactory calcVariableRepositoryDtoFactory,
            [JetBrains.Annotations.NotNull] CalcAffordanceDtoFactory calcAffordanceDtoFactory,
                                              [JetBrains.Annotations.NotNull] CalcTransportationDtoFactory transportationDtoFactory,
                                              CalcRepo calcRepo)
        {
            _ltDict = ltDict;
            _random = random;
            _calcPersonDtoFactory = calcPersonDtoFactory;
            _calcDeviceDtoFactory = calcDeviceDtoFactory;
            _calcLocationDtoFactory = calcLocationDtoFactory;
            _calcVariableRepositoryDtoFactory = calcVariableRepositoryDtoFactory;
            _calcAffordanceDtoFactory = calcAffordanceDtoFactory;
            _transportationDtoFactory = transportationDtoFactory;
            _calcRepo = calcRepo;
        }
        [JetBrains.Annotations.NotNull]
        public CalcHouseholdDto MakeCalcModularHouseholdDto([JetBrains.Annotations.NotNull] Simulator sim, [JetBrains.Annotations.NotNull] ModularHousehold mhh,
            [JetBrains.Annotations.NotNull] TemperatureProfile temperatureProfile, [JetBrains.Annotations.NotNull] HouseholdKey householdKey, [JetBrains.Annotations.NotNull] GeographicLocation geographicLocation,
            [JetBrains.Annotations.NotNull] out LocationDtoDict locationDict,
            [CanBeNull] TransportationDeviceSet transportationDeviceSet,
            [CanBeNull] TravelRouteSet travelRouteSet, EnergyIntensityType energyIntensity,
                                                            [CanBeNull] ChargingStationSet chargingStationSet)
        {
            //  _lf.RegisterKey(householdKey, mhh.PrettyName);
            var name = CalcAffordanceFactory.FixAffordanceName(mhh.Name, sim.MyGeneralConfig.CSVCharacter);
            if (geographicLocation == null)
            {
                throw new DataIntegrityException("no geographic Location was set");
            }
            var et = energyIntensity;
            if (et == EnergyIntensityType.AsOriginal)
            {
                et = mhh.EnergyIntensityType;
            }
            name = name + " " + householdKey.Key;
                var locations = mhh.CollectLocations();
                //var deviceLocationDict = new Dictionary<CalcLocation, List<IAssignableDevice>>();
                var deviceLocationDtoDict = new Dictionary<CalcLocationDto, List<IAssignableDevice>>();
                locationDict = new LocationDtoDict();
                List<DeviceCategoryDto> deviceCategoryDtos = new List<DeviceCategoryDto>();
                foreach (var deviceCategory in sim.DeviceCategories.Items) {
                    deviceCategoryDtos.Add(new DeviceCategoryDto(deviceCategory.FullPath,Guid.NewGuid().ToStrGuid()));
                }
                var locationDtos = _calcLocationDtoFactory.MakeCalcLocations(locations,
                    householdKey,
                    et, deviceLocationDtoDict, sim.DeviceActions.Items, locationDict,deviceCategoryDtos);
                // persons

                if (mhh.Vacation == null)
                {
                    throw new LPGException("Vacation was null");
                }

                var personDtos = _calcPersonDtoFactory.MakePersonDtos(mhh.Persons.ToList(), householdKey,
                    mhh.Vacation.VacationTimeframes(), mhh.CollectTraitDesires(), mhh.Name);
                if (_calcRepo.CalcParameters.Options.Contains(CalcOption.HouseholdContents)) {
                    _calcRepo.InputDataLogger.SaveList<CalcPersonDto>(personDtos.ConvertAll(x => (IHouseholdKey)x));
                }

                //mhh.Persons.ToList(),mhh.Vacation.VacationTimeframes(),  sim.MyGeneralConfig.RepetitionCount,householdKey, locs[0],name);
            //CalcPersonFactory.AddTraitDesires(mhh.CollectTraitDesires(), calcpersons,sim.MyGeneralConfig.TimeStepsPerHour, chh.Name, new Dictionary<Desire, SharedDesireValue>());
            //check if unhungry and unhungry join only have been added both
            //can't check it in the integrity checker, because that would mean having to duplicate the entire
            // desire collection logic

            /*  foreach (CalcPerson person in calcpersons) {
                  var desires =
                      person.PersonDesires.Desires.Values.Where(x => x.Name.ToLower().Contains("unhungry") || x.Name.ToLower().Contains("un-hungry")).ToList();
                  if (desires.Count > 1) {
                      throw new DataIntegrityException("More than one unhungry desire for the person " + person.Name, mhh);
                  }
              }*/

            // devices

            var deviceLocations = new List<DeviceLocationTuple>();

                foreach (var modularHouseholdTrait in mhh.Traits)
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (modularHouseholdTrait.HouseholdTrait != null)
                    {
                        CollectDevicesFromTrait(modularHouseholdTrait.HouseholdTrait, deviceLocations);
                    }
                }

                var deviceDtos = _calcDeviceDtoFactory.MakeCalcDevices(locationDtos,
                    deviceLocations, et, householdKey, deviceLocationDtoDict, sim.DeviceActions.Items,_ltDict, deviceCategoryDtos);
                if(_calcRepo.CalcParameters.Options.Contains(CalcOption.HouseholdContents)) {
                    _calcRepo.InputDataLogger.SaveList<CalcDeviceDto>(deviceDtos.ConvertAll(x => (IHouseholdKey)x));
                }

                //autodevs
            var autonomousDevices = mhh.CollectAutonomousDevices();
                if (mhh.Vacation == null)
                {
                    throw new LPGException("Vacation was null");
                }

                var autoDevDtos = _calcDeviceDtoFactory.MakeCalcAutoDevDtos(autonomousDevices,
                    energyIntensity, householdKey, mhh.Vacation.VacationTimeframes(),
                    mhh.Name + "###" + householdKey,
                    sim.DeviceActions.Items, locationDict,
                    temperatureProfile, geographicLocation,deviceCategoryDtos);
                if (_calcRepo.CalcParameters.Options.Contains(CalcOption.HouseholdContents)) {
                    _calcRepo.InputDataLogger.SaveList<CalcAutoDevDto>(autoDevDtos.ConvertAll(x => (IHouseholdKey)x));
                }

                //affordances
                var affordancesAtLoc =
                    new Dictionary<CalcLocationDto, List<AffordanceWithTimeLimit>>();
                foreach (var location in locations)
                {
                    affordancesAtLoc.Add(locationDict.GetDtoForLocation(location), mhh.GetAllAffordancesForLocation(location));
                }
                if (mhh.Vacation == null)
                {
                    throw new LPGException("Vacation was null");
                }

                List<CalcAffordanceDto> allAffordances = _calcAffordanceDtoFactory.SetCalcAffordances(locationDtos, temperatureProfile,
                    _ltDict,
                    geographicLocation, _random, sim.MyGeneralConfig.TimeStepsPerHour,
                    sim.MyGeneralConfig.InternalStepSize, mhh.Vacation.VacationTimeframes(),
                    mhh.Name + "###" + householdKey, sim.DeviceActions.Items, affordancesAtLoc, locationDict,
                    out List<DateTime> bridgeDays, householdKey, deviceDtos,deviceCategoryDtos);
                if (_calcRepo.CalcParameters.Options.Contains(CalcOption.AffordanceDefinitions)) {
                    _calcRepo.InputDataLogger.SaveList<CalcAffordanceDto>(allAffordances.ConvertAll(x => (IHouseholdKey)x));
                }

                if (_calcRepo.CalcParameters.Options.Contains(CalcOption.HouseholdContents))
                    {
                    _calcRepo.InputDataLogger.SaveList<CalcVariableDto>(_calcVariableRepositoryDtoFactory.GetAllVariableDtos()
                        .ConvertAll(x => (IHouseholdKey)x));
                }

                //                SaveVariableDefinitionsDtos(_calcVariableRepositoryDtoFactory.GetAllVariableDtos());
            //CalcVariableRepository variableRepository = _calcVariableRepositoryDtoFactory.GetRepository(householdKey);
            List<CalcSiteDto> sites = null;
            List<CalcTransportationDeviceDto> transportationDevices = null;
            List<CalcTravelRouteDto> routes = null;
            if (_calcRepo.CalcParameters.TransportationEnabled) {
                _transportationDtoFactory.MakeTransportationDtos(sim, mhh, transportationDeviceSet,
                    travelRouteSet,chargingStationSet,
                    out  sites, out  transportationDevices,
                    out routes, locationDtos, householdKey);
                if (_calcRepo.CalcParameters.IsSet(CalcOption.TransportationStatistics)) {
                    _calcRepo.InputDataLogger.SaveList<CalcSiteDto>(sites.ConvertAll(x => (IHouseholdKey)x));
                    _calcRepo.InputDataLogger.SaveList<CalcTransportationDeviceDto>(transportationDevices.ConvertAll(x => (IHouseholdKey)x));
                    _calcRepo.InputDataLogger.SaveList<CalcTravelRouteDto>(routes.ConvertAll(x => (IHouseholdKey)x));
                }
            }
            var chh = new CalcHouseholdDto(name, mhh.IntID, temperatureProfile.Name,householdKey,  Guid.NewGuid().ToStrGuid(),
                    geographicLocation.Name,
                    bridgeDays,autoDevDtos,locationDtos,personDtos,deviceDtos,
                    allAffordances, mhh.Vacation.VacationTimeframes(),
                    sites,routes,transportationDevices,
                    mhh.Description);
            if (_calcRepo.CalcParameters.Options.Contains(CalcOption.HouseholdContents)) {
                _calcRepo.InputDataLogger.Save(householdKey, chh);
            }

            if (_calcRepo.CalcParameters.Options.Contains(CalcOption.HouseholdContents)) {
                BridgeDayEntries bdes = new BridgeDayEntries(householdKey, chh.BridgeDays);
                _calcRepo.InputDataLogger.Save(householdKey, bdes);
            }

            return chh;
        }
        /*
        private void SaveDeviceDts([JetBrains.Annotations.NotNull][ItemNotNull] List<CalcDeviceDto> deviceDtos)
        {
            List<HouseholdDefinition> deviceDefs = new List<HouseholdDefinition>();
            foreach (var deviceDto in deviceDtos)
            {
                HouseholdDefinition pd = new HouseholdDefinition();
                pd.Name = deviceDto.Name;
                pd.HouseholdKey = deviceDto.HouseholdKey;
                pd.Json = JsonConvert.SerializeObject(deviceDto, Formatting.Indented);
                deviceDefs.Add(pd);
            }
            _srls.SaveToDatabase(deviceDefs);
        }*/

        private static void CollectDevicesFromTrait([JetBrains.Annotations.NotNull] HouseholdTrait hht,
            [JetBrains.Annotations.NotNull][ItemNotNull] List<DeviceLocationTuple> deviceList)
        {
            var allDevices = hht.CollectDevicesFromTrait();
            foreach (var dev in allDevices)
            {
                if (!deviceList.Any(x => x.Device == dev.Item2 && x.Location == dev.Item1))
                {
                    deviceList.Add(new DeviceLocationTuple(dev.Item1, dev.Item2));
                }
            }
        }

        /*
        private void SaveAutoDeviceDtos([JetBrains.Annotations.NotNull][ItemNotNull] List<CalcAutoDevDto> deviceDtos)
        {
            List<AutoDeviceDefinition> deviceDefs = new List<AutoDeviceDefinition>();
            foreach (var deviceDto in deviceDtos)
            {
                AutoDeviceDefinition pd = new AutoDeviceDefinition();
                pd.Name = deviceDto.Name;
                pd.HouseholdKey = deviceDto.HouseholdKey;
                pd.Json = JsonConvert.SerializeObject(deviceDto, Formatting.Indented);
                deviceDefs.Add(pd);
            }
            _srls.SaveToDatabase(deviceDefs);
        }*/
        /*
        public void SavePersonDtos([JetBrains.Annotations.NotNull][ItemNotNull] List<CalcPersonDto> persons)
        {
            List<PersonDefinition> persondefs = new List<PersonDefinition>();
            foreach (var personDto in persons)
            {
                PersonDefinition pd = new PersonDefinition(personDto.ID, personDto.Name,
                 personDto.HouseholdKey,
                    JsonConvert.SerializeObject(personDto, Formatting.Indented));
                persondefs.Add(pd);
            }
            _srls.SaveToDatabase(persondefs);
        }
        */
        /*
        private void SaveVariableDefinitionsDtos([JetBrains.Annotations.NotNull][ItemNotNull] List<CalcVariableDto> variableDtos)
        {
            List<VariableDefinition> variableDefinitions = new List<VariableDefinition>();
            foreach (var variableDto in variableDtos)
            {
                VariableDefinition pd = new VariableDefinition();
                pd.Name = variableDto.Name;
                pd.HouseholdKey = variableDto.HouseholdKey;
                pd.Json = JsonConvert.SerializeObject(variableDto, Formatting.Indented);
                variableDefinitions.Add(pd);
            }
            _srls.SaveToDatabase(variableDefinitions);
        }*/
    }
}