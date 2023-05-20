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
            var dataFromJson = JsonDataProvider.GetData(TestData.JsonTestDataFilepath);
            Assert.Equal(23986, dataFromJson.Count);
        }
        
        [Fact]
        public void CsvDataProvider_returns_correctCountOfGpsData()
        {
            var dataFromCsv = CsvDataProvider.GetData(TestData.CsvTestDataFilepath);
            Assert.Equal(27475, dataFromCsv.Count);
        }
    }
}