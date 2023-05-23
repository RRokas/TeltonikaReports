using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DataAccess.Entities;

namespace DataAccess.DataProviders
{
    public static class BinaryDataProvider
    {
        public static List<GpsData> GetData(string filepath)
        {
            var gpsDataLog = new List<GpsData>();
            using var reader = new BinaryReader(File.Open(filepath, FileMode.Open));
            while (reader.BaseStream.Length > reader.BaseStream.Position)
            {
                gpsDataLog.Add(new GpsData
                {
                    // IPAddress.NetworkToHostOrder is used to change endianness
                    Latitude = IPAddress.NetworkToHostOrder(reader.ReadInt32()) / 10000000d,
                    Longitude = IPAddress.NetworkToHostOrder(reader.ReadInt32()) / 10000000d,
                    GpsTime = BinToDate(IPAddress.NetworkToHostOrder(reader.ReadInt64())),
                    Speed = IPAddress.NetworkToHostOrder(reader.ReadInt16()),
                    Angle = IPAddress.NetworkToHostOrder(reader.ReadInt16()),
                    Altitude = IPAddress.NetworkToHostOrder(reader.ReadInt16()),
                    Satellites = reader.ReadByte()
                });
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