﻿using System;
using System.Collections;
using System.IO;
using Automation;
using Automation.ResultFiles;
using CalculationController.DtoFactories;
using CalculationEngine.HouseholdElements;
using CalculationEngine.OnlineLogging;
using Common;
using Common.CalcDto;
using Common.JSON;
using Common.SQLResultLogging.InputLoggers;
using Common.Tests;
using Moq;
using NUnit.Framework;

namespace Calculation.Tests.OnlineLogging {
    [TestFixture]
    public class FileFactoryAndTrackerTests : UnitTestBaseClass
    {
        [Test]
        [Category(UnitTestCategories.BasicTest)]
        public void FileFactoryAndTrackerTest()
        {
            Config.IsInUnitTesting = true;
            var wd = new WorkingDir(Utili.GetCurrentMethodAndClass());
            wd.InputDataLogger.AddSaver(new HouseholdKeyLogger(wd.SqlResultLoggingService));
            wd.InputDataLogger.AddSaver(new ResultFileEntryLogger(wd.SqlResultLoggingService));
            CalcParameters calcParameters = CalcParametersFactory.MakeGoodDefaults();
            var clt = new CalcLoadType("calcloadtype",  "kwh", "kW", 0.001, true, Guid.NewGuid().ToString());
            BitArray isSick = new BitArray(calcParameters.InternalTimesteps);
            BitArray isOnVacation = new BitArray(calcParameters.InternalTimesteps);
            var personDto = CalcPersonDto.MakeExamplePerson();
            Random r = new Random();
            Mock<ILogFile> lf = new Mock<ILogFile>();
            CalcLocation cloc = new CalcLocation("blub",Guid.NewGuid().ToString());
            var cp = new CalcPerson(personDto, r, lf.Object, cloc, calcParameters, isSick, isOnVacation);
                /*"personname", 1, 1, null, 1, PermittedGender.Female, lf: null,
                householdKey: "hh1", startingLocation: null, traitTag: "traittag",
                householdName: "hhname0",calcParameters:calcParameters,isSick:isSick, guid:Guid.NewGuid().ToString());
                */
            var fft = new FileFactoryAndTracker(wd.WorkingDirectory, "testhh",wd.InputDataLogger);
            fft.RegisterHousehold(new HouseholdKey( "hh1"),"test key",HouseholdKeyType.Household,"desc",null,null);
            fft.RegisterHousehold(Constants.GeneralHouseholdKey, "general key", HouseholdKeyType.General,"desc",null,null);
            fft.MakeFile<StreamWriter>("file1", "desc", true, ResultFileID.Actions, new HouseholdKey("hh1"), TargetDirectory.Charts, TimeSpan.FromMinutes(1), clt.ConvertToLoadTypeInformation(),
                cp.MakePersonInformation());
            //fft.ResultFileList.WriteResultEntries(wd.WorkingDirectory);
            ResultFileEntryLogger rfel = new ResultFileEntryLogger(wd.SqlResultLoggingService);
            var rfes = rfel.Load();
            Assert.That(rfes.Count,Is.GreaterThan(0));
            //ResultFileList.ReadResultEntries(wd.WorkingDirectory);
            fft.GetResultFileEntry(ResultFileID.Actions, clt.Name, new HouseholdKey( "hh1"), cp.MakePersonInformation(), null);
            fft.Close();
            wd.CleanUp();
        }
    }
}