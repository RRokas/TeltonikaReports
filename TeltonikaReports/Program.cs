using System;
using System.Collections.Generic;
using DataAccess;
using DataAccess.Entities;

namespace TeltonikaReports
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose report:");
            Console.WriteLine("1. Satellite count histogram");
            Console.WriteLine("2. Speed histogram");
            Console.WriteLine("3. Fastest 100km section from multiple files");

            var reportNumber = Console.ReadLine();

            switch (reportNumber)
            {
                case "1":
                    SatelliteCountHistogramReport(AskForPath("Enter path to file:"));
                    break;
                case "2":
                    SpeedHistogramReport(AskForPath("Enter path to file:"));
                    break;
                case "3":
                    FastestSectionReport(AskForPath("Enter path to directory:"));
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

            Console.Read();
        }

        private static void SpeedHistogramReport(string filepath)
        {
            var gpsDataLog = new GpsDataRepository(filepath).GetAllData();
            Console.Clear();
            ChartRenderer.HorizontalChart(ReportDataGenerators.OrderedSpeedHistogram(gpsDataLog));
        }
        
        private static void SatelliteCountHistogramReport(string filepath)
        {
            Console.Clear();
            var gpsDataLog = new GpsDataRepository(filepath).GetAllData();
            ChartRenderer.VerticalChart(ReportDataGenerators.OrderedSatelliteCountHistogramGraphData(gpsDataLog));
        }
        
        private static void FastestSectionReport(string directoryPath)
        {
            Console.Clear();
            var gpsDataLog = new List<GpsData>();
            foreach (var file in System.IO.Directory.GetFiles(directoryPath))
            {
                gpsDataLog.AddRange(new GpsDataRepository(file).GetAllData());
            }
            
            Console.WriteLine("Loading...");
            var reportData = ReportDataGenerators.FindFastestSectionGraphData(gpsDataLog);
            Console.Clear();
            Console.Write(reportData);
        }
        
        private static string AskForPath(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}