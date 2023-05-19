using System;
using System.IO;
using DataAccess.DataSources;
using Xunit;

namespace DataAccess.Tests
{
    public class DataSourceTests
    {
        private static readonly string TestDataRootPath = Path.Join("..", "..", "..", "TestData"); // go up in the directories to reach project root
        private static readonly string JsonTestDataFilepath = Path.Join(TestDataRootPath, "2019-07.json");
        private static readonly string CsvTestDataFilepath = Path.Join(TestDataRootPath, "2019-08.csv");
        
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