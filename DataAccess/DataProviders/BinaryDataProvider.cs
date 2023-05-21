using System;
using System.Collections.Generic;
using System.IO;
using DataAccess.Entities;

namespace DataAccess.DataSources
{
    public class BinaryDataProvider
    {
        public static List<GpsData> GetData(string filepath)
        {
            var allBytes = File.ReadAllBytes(filepath);
            // data is in this order: latitude, longitude, gpsTime, speed, angle, altitude, satellites
            // each object is 23 bytes long
            // 4 bytes(int32) for latitude,
            // 4 bytes(int32) for longitude,
            // 8(int64) bytes for gpsTime,
            // 2 bytes for speed,
            // 2 bytes for angle,
            // 2 bytes for altitude,
            // 1 byte for satellites
            var gpsDataLog = new List<GpsData>();
            for (var i = 0; i < allBytes.Length; i += 23)
            {
                var gpsData = new GpsData();
                gpsData.Latitude = BitConverter.ToInt32(allBytes, i) / 100000000f;
                gpsData.Longitude = BitConverter.ToInt32(allBytes, i + 4) / 100000000f;
                gpsData.GpsTime = BinToDate(BitConverter.ToInt64(allBytes, i + 8));
                gpsData.Speed = BitConverter.ToInt16(allBytes, i + 16);
                gpsData.Angle = BitConverter.ToInt16(allBytes, i + 18);
                gpsData.Altitude = BitConverter.ToInt16(allBytes, i + 20);
                gpsData.Satellites = allBytes[i + 22];
                gpsDataLog.Add(gpsData);
            }
            return gpsDataLog;
        }
        
        private static DateTime BinToDate(long binDate)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds(binDate);
        }
    }
}