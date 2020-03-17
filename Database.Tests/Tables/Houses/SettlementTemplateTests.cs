﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation;
using Automation.ResultFiles;
using CalculationController.Integrity;
using Common;
using Common.Tests;
using Database.Tables.Houses;
using Database.Tables.ModularHouseholds;
using NUnit.Framework;

namespace Database.Tests.Tables.Houses {
    [TestFixture]
    public class SettlementTemplateTests : UnitTestBaseClass
    {
        [Test]
        [Category(UnitTestCategories.BasicTest)]
        public void SettlementTemplatePreviewTest() {
            var db = new DatabaseSetup(Utili.GetCurrentMethodAndClass(), DatabaseSetup.TestPackage.DatabaseIo);

            var sim = new Simulator(db.ConnectionString);
            var st = sim.SettlementTemplates.CreateNewItem(sim.ConnectionString);
            st.AddHouseSize(1, 2, 0.50);
            st.AddHouseSize(1, 2, 0.50);
            st.AddHouseType(sim.HouseTypes[0]);
            st.TemperatureProfile = sim.TemperatureProfiles[0];
            st.GeographicLocation = sim.GeographicLocations[0];
            st.DesiredHHCount = 100;
            var limittrait = sim.HouseholdTraits.FindByName("Showering with electric Air Heater");
            if (limittrait == null) {
                throw new LPGException("Trait not found");
            }
            st.AddTraitLimit(limittrait, 1);
            st.AddHouseholdDistribution(1, 1, 0.95, EnergyIntensityType.EnergyIntensive);
            st.AddHouseholdDistribution(1, 2, 0.05, EnergyIntensityType.EnergySaving);
            st.AddHouseType(sim.HouseTypes[0]);
            foreach (var template in sim.HouseholdTemplates.It) {
                st.AddHouseholdTemplate(template);
            }

            st.GenerateSettlementPreview( sim);
            var i = 1;
            var traitCounts = new Dictionary<string, int>();
            foreach (var housePreviewEntry in st.HousePreviewEntries) {
                Logger.Info("-------------------");
                Logger.Info("HousePreviewEntry " + i++);
                Logger.Info("Households:" + housePreviewEntry.Households.Count + " Housetype:" +
                            housePreviewEntry.HouseType?.Name + " First Housesize:" + housePreviewEntry.HouseSize);
                var j = 1;
                foreach (var calcObject in housePreviewEntry.Households) {
                    Logger.Info("#" + j++ + " Persons: " + calcObject.CalculatePersonCount() + ", " +
                                calcObject.Name + ", " + calcObject.EnergyIntensityType);
                    if (calcObject is ModularHousehold chh)
                    {
                        foreach (var trait in chh.Traits)
                        {
                            var name = trait.HouseholdTrait.Name;
                            if (!traitCounts.ContainsKey(name))
                            {
                                traitCounts.Add(name, 1);
                            }
                            else
                            {
                                traitCounts[name] += 1;
                            }
                        }
                    }
                }
            }
            st.CreateSettlementFromPreview(sim);
            SimIntegrityChecker.Run(sim);
            db.Cleanup();
            foreach (var pair in traitCounts) {
                Logger.Info(pair.Key + ": " + pair.Value);
            }
        }

        [Test]
        [Category(UnitTestCategories.BasicTest)]
        public void SettlementTemplateTest() {
            var db = new DatabaseSetup(Utili.GetCurrentMethodAndClass(), DatabaseSetup.TestPackage.DatabaseIo);

            var sts = new ObservableCollection<SettlementTemplate>();

            var templates = db.LoadHouseholdTemplates(out var realDevices,
                out var deviceCategories, out var timeBasedProfiles, out var timeLimits, out var loadTypes, out var deviceActions,
                out var deviceActionGroups, out var traits);
            var energyStorages = db.LoadEnergyStorages(loadTypes);
            var transformationDevices = db.LoadTransformationDevices(loadTypes,
                energyStorages);
            var dateBasedProfiles = db.LoadDateBasedProfiles();
            var generators = db.LoadGenerators(loadTypes, dateBasedProfiles);
            var allLocations = db.LoadLocations(realDevices, deviceCategories, loadTypes);
            var variables = db.LoadVariables();
            var houseTypes = db.LoadHouseTypes(realDevices, deviceCategories,
                timeBasedProfiles, timeLimits, loadTypes, transformationDevices, energyStorages, generators,
                allLocations, deviceActions, deviceActionGroups, variables);
            var tempprofiles = db.LoadTemperatureProfiles();
            var geolocs = db.LoadGeographicLocations(out _, timeLimits);
            var householdTags = db.LoadHouseholdTags();
            db.ClearTable(SettlementTemplate.TableName);
            db.ClearTable(STHouseholdDistribution.TableName);
            db.ClearTable(STHouseholdTemplate.TableName);
            db.ClearTable(STHouseSize.TableName);
            db.ClearTable(STHouseType.TableName);
            db.ClearTable(STTraitLimit.TableName);
            var st = new SettlementTemplate("bla", null, "desc", db.ConnectionString, 100, "TestName",
                null, null, Guid.NewGuid().ToString());
            st.SaveToDB();
            Assert.AreNotEqual(-1, st.IntID);
            st.AddHouseholdDistribution(10, 100, 0.2, EnergyIntensityType.AsOriginal);
            st.AddHouseholdTemplate(templates[0]);
            st.AddHouseSize(10, 100, 0.2);
            st.AddHouseType(houseTypes[0]);
            st.AddTraitLimit(traits[0], 10);
            SettlementTemplate.LoadFromDatabase(sts, db.ConnectionString, templates, houseTypes, false, tempprofiles,
                geolocs, householdTags, traits);
            Assert.AreEqual(1, sts.Count);
            Assert.AreEqual(1, sts[0].HouseholdDistributions.Count);
            Assert.AreEqual(1, sts[0].HouseholdTemplates.Count);

            Assert.AreEqual(1, sts[0].HouseTypes.Count);
            Assert.AreEqual(1, sts[0].HouseSizes.Count);
            st = sts[0];
            st.DeleteHouseholdDistribution(st.HouseholdDistributions[0]);
            st.DeleteHouseholdTemplate(st.HouseholdTemplates[0]);
            st.DeleteHouseType(st.HouseTypes[0]);
            st.DeleteHouseSize(st.HouseSizes[0]);
            st.DeleteTraitLimit(st.TraitLimits[0]);
            sts.Clear();
            SettlementTemplate.LoadFromDatabase(sts, db.ConnectionString, templates, houseTypes, false, tempprofiles,
                geolocs, householdTags, traits);
            Assert.AreEqual(1, sts.Count);
            Assert.AreEqual(0, sts[0].HouseholdDistributions.Count);
            Assert.AreEqual(0, sts[0].HouseholdTemplates.Count);

            Assert.AreEqual(0, sts[0].HouseTypes.Count);
            Assert.AreEqual(0, sts[0].HouseSizes.Count);
            db.Cleanup();
        }
    }
}