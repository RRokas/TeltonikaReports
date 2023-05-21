using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;

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

        public static List<(string, int)> OrderedSpeedHistogram(List<GpsData> sourceData, int groupSize)
        {
            return sourceData.GroupBy(x => Math.Floor(x.Speed / groupSize) * groupSize)
                .Select(group 
                    => ($"{group.Min(gpsData => gpsData.Speed)}-{group.Max(gpsData => gpsData.Speed)}",
                        group.Count()))
                .ToList();
        }
    }
}