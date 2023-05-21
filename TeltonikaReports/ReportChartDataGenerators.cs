using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using Geolocation;

namespace TeltonikaReports
{
    public static class ReportChartDataGenerators
    {
        public static List<(string, int)> OrderedSatelliteCountHistogram(List<GpsData> sourceData)
        {
            return sourceData.GroupBy(gpsData => gpsData.Satellites)
                .Select(group => (group.Key.ToString(), group.Count()))
                .OrderBy(x=>Convert.ToInt32(x.Item1))
                .ToList();
        }

        public static List<(string, int)> OrderedSpeedHistogram(List<GpsData> sourceData, int groupSize = 10)
        {
            return sourceData.GroupBy(x => Math.Floor(x.Speed / groupSize) * groupSize)
                .Select(group 
                    => ($"{group.Min(gpsData => gpsData.Speed)}-{group.Max(gpsData => gpsData.Speed)}",
                        group.Count()))
                .ToList();
        }

        public static string FindFastestSection(List<GpsData> sourceData, int sectionLength = 100)
        {
            var resultString = "";
            var orderedData = sourceData.OrderBy(x => x.GpsTime).ToList();
            var fastestSectionTime = TimeSpan.Zero;
            var currentSection = new List<GpsData>();

            for (int i = 1; i < orderedData.Count; i++)
            {
                var innerLoopIndex = i;
                var sectionFound = false;
                var distance = 0.0;
                currentSection.Clear();
                currentSection.Add(orderedData[i - 1]);
                // iterate till we find a point that is 100km away from the current one
                while (!sectionFound && i < orderedData.Count)
                {
                    if (innerLoopIndex == orderedData.Count)
                        break;

                    var currentDistance = GeoCalculator.GetDistance(
                        orderedData[innerLoopIndex - 1].Latitude,
                        orderedData[innerLoopIndex - 1].Longitude,
                        orderedData[innerLoopIndex].Latitude,
                        orderedData[innerLoopIndex].Longitude,
                        4,
                        DistanceUnit.Kilometers);
                    distance += currentDistance;
                    if (distance >= sectionLength)
                    {
                        sectionFound = true;
                    }
                    else
                    {
                        currentSection.Add(orderedData[innerLoopIndex]);
                        innerLoopIndex++;
                    }
                }

                if (!sectionFound) continue;
                
                var sectionTime = currentSection.Last().GpsTime - currentSection.First().GpsTime;
                
                if (sectionTime >= fastestSectionTime && fastestSectionTime != TimeSpan.Zero) continue;
                
                resultString = $"Time taken to cover section: {sectionTime}\n" +
                               $"Exact distance covered: {distance}km\n" +
                               $"Average speed calculated from distance and time: {distance / sectionTime.TotalHours}km/h\n" +
                               $"Start position: {currentSection.First().Longitude}, {currentSection.First().Latitude}\n" +
                               $"Start time: {currentSection.First().GpsTime}\n" +
                               $"End position: {currentSection.Last().Longitude}, {currentSection.Last().Latitude}\n" +
                               $"End time: {currentSection.Last().GpsTime}\n";

                fastestSectionTime = sectionTime;
            }
            return resultString;
        }
    }
}