using System;
using System.Collections.Generic;
using System.Linq;
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

            if (reportNumber == "1" || reportNumber == "2")
            {
                Console.WriteLine("Enter path to file:");
                var filepath = Console.ReadLine();
                var gpsDataLog = new GpsDataRepository(filepath).GetAllData();
                if (reportNumber == "1")
                {
                    Console.Clear();
                    ChartRenderer.HorizontalChart(ReportDataGenerators.OrderedSatelliteCountHistogramGraphData(gpsDataLog));
                } else if (reportNumber == "2")
                {
                    Console.Clear();
                    ChartRenderer.HorizontalChart(ReportDataGenerators.OrderedSpeedHistogram(gpsDataLog));
                }
            } else if (reportNumber == "3")
            {
                Console.Write("Directory path to source files (folder should contain only supported files):");
                var directoryPath = Console.ReadLine();
                var gpsDataLog = new List<GpsData>();
                foreach (var file in System.IO.Directory.GetFiles(directoryPath))
                {
                    gpsDataLog.AddRange(new GpsDataRepository(file).GetAllData());
                }
                Console.Clear();
                Console.WriteLine("Loading...");
                var reportData = ReportDataGenerators.FindFastestSectionGraphData(gpsDataLog);
                Console.Clear();
                Console.Write(reportData);
            }
        }
    }
}