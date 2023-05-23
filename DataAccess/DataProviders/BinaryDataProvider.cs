using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                var gpsData = new GpsData
                {
                    Latitude = BitConverter.ToInt32(allBytes.Skip(i).Take(4).Reverse().ToArray(), 0) / 10000000d,
                    Longitude = BitConverter.ToInt32(allBytes.Skip(i+4).Take(4).Reverse().ToArray(), 0) / 10000000d,
                    GpsTime = BinToDate(BitConverter.ToInt64(allBytes.Skip(i+8).Take(8).Reverse().ToArray(), 0)),
                    Speed = BitConverter.ToInt16(allBytes.Skip(i+16).Take(2).Reverse().ToArray(), 0),
                    Angle = BitConverter.ToInt16(allBytes.Skip(i+18).Take(2).Reverse().ToArray(), 0),
                    Altitude = BitConverter.ToInt16(allBytes.Skip(i+20).Take(2).Reverse().ToArray(), 0),
                    Satellites = allBytes[i + 22]
                };
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