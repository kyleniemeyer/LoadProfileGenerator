﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Automation;
using Automation.ResultFiles;
using Common;
using JetBrains.Annotations;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace ChartCreator2.OxyCharts {
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    internal class VariableLogFileChart : ChartBaseFileStep
    {
        public VariableLogFileChart([JetBrains.Annotations.NotNull] ChartCreationParameters parameters,
                                    [JetBrains.Annotations.NotNull] FileFactoryAndTracker fft,
                                    [JetBrains.Annotations.NotNull] ICalculationProfiler calculationProfiler) : base(parameters, fft,
            calculationProfiler, new List<ResultFileID>() { ResultFileID.VariableLogfile
            },
            "Variable Logfile", FileProcessingResult.ShouldCreateFiles
        )
        {
        }

        [JetBrains.Annotations.NotNull]
        private PlotModel MakeChart([JetBrains.Annotations.NotNull] string plotName, [JetBrains.Annotations.NotNull] string yaxisLabel, TimeSpan timestep, [ItemNotNull] [JetBrains.Annotations.NotNull] List<string> headers,
            [ItemNotNull] [JetBrains.Annotations.NotNull] List<double[]> values, [JetBrains.Annotations.NotNull] List<DateTime> dates, int maxTimestep)
        {
            var plotModel1 = new PlotModel();
            var l = new Legend();
            plotModel1.Legends.Add(l);
                // general
            l.LegendBorderThickness = 0;
            l.LegendOrientation = LegendOrientation.Horizontal;
            l.LegendPlacement = LegendPlacement.Outside;
            l.LegendPosition = LegendPosition.BottomCenter;
            if (Parameters.ShowTitle) {
                plotModel1.Title = plotName;
            }
            var dateTimeAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "dd.MM. HH:mm"
            };
            plotModel1.Axes.Add(dateTimeAxis);
            var linearAxis2 = new LinearAxis
            {
                AbsoluteMinimum = 0,
                MaximumPadding = 0.06,
                MinimumPadding = 0,
                Title = yaxisLabel
            };
            plotModel1.Axes.Add(linearAxis2);
            // data
            var p = OxyPalettes.HueDistinct(headers.Count);
            var currentTime = new TimeSpan(0);
            for (var i = 2; i < headers.Count; i++) {
                // main columns
                var columnSeries2 = new LineSeries
                {
                    Title = headers[i]
                };
                for (var j = 0; j < values.Count && j < maxTimestep; j++) {
                    currentTime = currentTime.Add(timestep);
                    var dt = dates[j];
                    var dp = new DataPoint(DateTimeAxis.ToDouble(dt), values[j][i]);
                    columnSeries2.Points.Add(dp);
                }

                columnSeries2.Color = p.Colors[i];
                plotModel1.Series.Add(columnSeries2);
            }

            return plotModel1;
        }

        protected override FileProcessingResult MakeOnePlot(ResultFileEntry srcEntry)
        {
            string plotName = "Variables " + srcEntry.HouseholdNumberString;
            Profiler.StartPart(Utili.GetCurrentMethodAndClass());
            const string yaxisLabel = "Value [-]";
            const double conversionfactor = 1;

            var headers = new List<string>();
            var values = new List<double[]>();
            var dates = new List<DateTime>();
            if (srcEntry.FullFileName == null)
            {
                throw new LPGException("filename was null");
            }
            using (var sr = new StreamReader(srcEntry.FullFileName)) {
                var topLine = sr.ReadLine();
                if (topLine == null) {
                    throw new LPGException("Readline Failed");
                }
                var header1 = topLine.Split(Parameters.CSVCharacterArr, StringSplitOptions.None);
                headers.AddRange(header1);
                while (!sr.EndOfStream) {
                    var s = sr.ReadLine();
                    if (s == null) {
                        throw new LPGException("Readline failed");
                    }
                    var cols = s.Split(Parameters. CSVCharacterArr, StringSplitOptions.None);
                    var result = new double[headers.Count];
                    var success1 = DateTime.TryParse(cols[1], CultureInfo.CurrentCulture, DateTimeStyles.None, out var dt);
                    if (!success1) {
                        Logger.Error("Parsing failed in MakePlot.");
                    }
                    dates.Add(dt);
                    for (var index = 0; index < cols.Length; index++) {
                        var col = cols[index];
                        var success = double.TryParse(col, out double d);
                        if (success) {
                            result[index] = d / conversionfactor;
                        }
                    }
                    values.Add(result);
                }
            }
            var fi = new FileInfo(srcEntry.FullFileName);
            var plotModel1 = MakeChart(plotName, yaxisLabel, srcEntry.TimeResolution, headers, values, dates, 25000);
            var dstfilename = fi.Name.Insert(fi.Name.Length - 4, ".Short");
            Save(plotModel1, plotName, srcEntry.FullFileName, Parameters.BaseDirectory, CalcOption.VariableLogFile,dstfilename );

            plotModel1 = MakeChart(plotName, yaxisLabel, srcEntry.TimeResolution, headers, values, dates, int.MaxValue);
            dstfilename = fi.Name.Insert(fi.Name.Length - 4, ".Full");
            Save(plotModel1, plotName, srcEntry.FullFileName, Parameters.BaseDirectory, CalcOption.VariableLogFile, dstfilename);
            Profiler.StopPart(Utili.GetCurrentMethodAndClass());
            return FileProcessingResult.ShouldCreateFiles;
        }
    }
}