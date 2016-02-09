using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Quantic.Etl.Extensions;
using Quantic.Etl.Operations;

namespace Quantic.Etl.Tests
{
    [TestClass]
    public class ReadTextFileOperationTests
    {
	    private static string _fileLocation = Path.Combine(
		    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestTable.txt");

        [TestInitialize]
        public void Setup()
        {
            WriteTestTableFile();
        }

        [TestMethod]
        public async Task ReadTextFileOperation_Execute_LoadsText()
        {
            var op = new ReadTextFileOperation(_fileLocation);

            var ret = await op.Execute();

            Debug.Write(ret);

            Assert.IsTrue(ret.Length > 0);
        }

        [TestCleanup]
        public void Cleanup()
        {
            RemoveTestTableFile();
        }

        private void WriteTestTableFile()
        {
            var table = TestRecords.GetPeopleTable();

            var rows = table.Select(row => row.ToDictionary()).ToList();

            var text = JsonConvert.SerializeObject(rows, Formatting.Indented);

            using (var sw = new StreamWriter(_fileLocation, false))
                sw.Write(text);
        }

        private void RemoveTestTableFile()
        {
            if (!File.Exists(_fileLocation))
                throw new FileNotFoundException("Where did the test file go? :(");

            File.Delete(_fileLocation);
        }
    }
}
