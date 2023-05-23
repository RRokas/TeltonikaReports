using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccess.DataProviders;
using DataAccess.Entities;

namespace DataAccess
{
    public interface IGpsDataRepository
    {
        public List<GpsData> GetAllData();
    }
    
    public class GpsDataRepository : IGpsDataRepository
    {
        private string DataSourceFilepath { get; set; }
        private string DataSourceFileExtension { get; set; }
        private static readonly string[] SupportedFileExtensions = new[] { ".CSV", ".JSON", ".BIN" };

        public GpsDataRepository(string dataSourceFilepath)
        {
            var extension = Path.GetExtension(dataSourceFilepath).ToUpper();
            
            if (!SupportedFileExtensions.Contains(extension))
                throw new ArgumentException($"Unsupported file format. Supported formats: {string.Join(", ", SupportedFileExtensions)}");
                    
            DataSourceFilepath = dataSourceFilepath;
            DataSourceFileExtension = extension;
        }

        public List<GpsData> GetAllData()
        {
            switch (DataSourceFileExtension)
            {
                case ".JSON":
                    return JsonDataProvider.GetData(DataSourceFilepath);
                case ".CSV":
                    return CsvDataProvider.GetData(DataSourceFilepath);
                case ".BIN":
                    return BinaryDataProvider.GetData(DataSourceFilepath);
                default:
                    throw new NotImplementedException("Missing implementation for file format!");
            }
        }
    }
}