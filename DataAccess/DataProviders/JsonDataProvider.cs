using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataAccess.Entities;

namespace DataAccess.DataProviders
{
    public static class JsonDataProvider
    {
        public static List<GpsData> GetData(string filepath)
        {
            var fileContent = File.ReadAllText(filepath);
            var gpsDataLog = JsonSerializer.Deserialize<List<GpsData>>(fileContent);
            return gpsDataLog;
        } 
    }
}