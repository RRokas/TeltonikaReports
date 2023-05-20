using System;
using System.IO;
using DataAccess.DataSources;
using Xunit;

namespace DataAccess.Tests
{
    public class DataProviderTests
    {

        
        [Fact]
        public void JsonDataProvider_returns_correctCountOfGpsData()
        {
            var dataFromJson = JsonDataProvider.GetData(JsonTestDataFilepath);
            Assert.Equal(23986, dataFromJson.Count);
        }
        
        [Fact]
        public void CsvDataProvider_returns_correctCountOfGpsData()
        {
            var dataFromJson = CsvDataProvider.GetData(CsvTestDataFilepath);
            Assert.Equal(27475, dataFromJson.Count);
        }
    }
}