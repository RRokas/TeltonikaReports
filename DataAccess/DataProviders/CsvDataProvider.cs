﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DataAccess.Entities;

namespace DataAccess.DataProviders
{
    public static class CsvDataProvider
    {
        public static List<GpsData> GetData(string filepath)
        {
            var textData = File.ReadLines(filepath);
            var gpsData = new List<GpsData>();
            foreach (var fileLine in textData)
            {
                gpsData.Add(ParseLine(fileLine));
            }
            return gpsData;
        }
        
        public static GpsData ParseLine(string line)
        {
            var gpsData = new GpsData();
            var lineData = line.Split(',');
            gpsData.Latitude = double.Parse(lineData[0], CultureInfo.InvariantCulture);
            gpsData.Longitude = double.Parse(lineData[1], CultureInfo.InvariantCulture);
            gpsData.GpsTime = DateTime.Parse(lineData[2], CultureInfo.InvariantCulture);
            gpsData.Speed = short.Parse(lineData[3], CultureInfo.InvariantCulture);
            gpsData.Angle = short.Parse(lineData[4], CultureInfo.InvariantCulture);
            gpsData.Altitude = short.Parse(lineData[5], CultureInfo.InvariantCulture);
            gpsData.Satellites = byte.Parse(lineData[6], CultureInfo.InvariantCulture);
            return gpsData;
        }
    }
}