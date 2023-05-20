using System;
using Xunit;

namespace DataAccess.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void GpsDataRepository_throws_argumentExceptionWhenUnsupportedFormatProvided()
        {
            Assert.Throws<ArgumentException>(() => new GpsDataRepository(@"C:\TestData\GpsData.jpg"));
        }

        [Fact]
        public void GpsDataRepository_returns_CorrectCountOfGpsDataFromJsonFile()
        {
            var repository = new GpsDataRepository(TestData.JsonTestDataFilepath);
            var dataFromJson = repository.GetAllData();
            Assert.Equal(23986, dataFromJson.Count);
        }
        
        [Fact]
        public void GpsDataRepository_returns_CorrectCountOfGpsDataFromCsvFile()
        {
            var repository = new GpsDataRepository(TestData.CsvTestDataFilepath);
            var dataFromCsv = repository.GetAllData();
            Assert.Equal(27475, dataFromCsv.Count);
        }
    }
}