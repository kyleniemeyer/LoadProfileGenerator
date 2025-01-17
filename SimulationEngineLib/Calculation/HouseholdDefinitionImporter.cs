﻿using System;
using Automation;
using CalculationController.Integrity;
using Common;
using Database;
using JetBrains.Annotations;
using PowerArgs;
using SimulationEngineLib.SettlementCalculation;

namespace SimulationEngineLib.Calculation {
    public static class HouseholdDefinitionImporter {
        public static void ImportHouseholdDefinition([NotNull] HouseholdDefinitionImporterOptions o, [NotNull] string connectionString) {
            var sim = new Simulator(connectionString);
            try {
                var sett = ModularHouseholdSerializer.ImportFromCSV(o.File, sim);
                SimIntegrityChecker.Run(sim, CheckingOptions.Default());
                var bo = new BatchOptions
                {
                    OutputFileDefault = OutputFileDefault.ReasonableWithCharts,
                    EnergyIntensity = EnergyIntensityType.EnergySavingPreferMeasured
                };
                var count = 1;
                foreach (var settlement in sett) {
                    bo.Suffix = "CSV" + count++;
                    bo.SettlementName = settlement.Name;
                    BatchfileFromSettlement.MakeBatchfileFromSettlement(sim, bo);
                }
            }
            catch (Exception ex) {
                Logger.Exception(ex);
                if (!Config.CatchErrors) {
                    throw;
                }
            }
        }

        [UsedImplicitly]
        public class HouseholdDefinitionImporterOptions {
            [CanBeNull]
            [ArgDescription("Sets the file to import")]
            [ArgShortcut(null)]
            [UsedImplicitly]
            public string File { get; set; }
        }
    }
}