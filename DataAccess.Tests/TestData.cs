using System.IO;

namespace DataAccess.Tests
{
    public class TestData
    {
        private static readonly string TestDataRootPath = Path.Join("..", "..", "..", "TestData"); // go up in the directories to reach project root
        public static readonly string JsonTestDataFilepath = Path.Join(TestDataRootPath, "2019-07.json");
        public static readonly string CsvTestDataFilepath = Path.Join(TestDataRootPath, "2019-08.csv");
        public static readonly string BinaryTestDataFilepath = Path.Join(TestDataRootPath, "2019-09.bin");
    }
}