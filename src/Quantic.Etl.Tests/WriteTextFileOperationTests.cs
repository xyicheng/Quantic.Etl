using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantic.Etl.Operations;

namespace Quantic.Etl.Tests
{
    [TestClass]
    public class WriteTextFileOperationTests
    {
        private static string _fileLocation = @"C:\TestText.txt";

        [TestMethod]
        public async Task WriteTextFileOperation_Execute_WritesToFile()
        {
            string text = "Foo";

            var op = new WriteTextFileOperation(_fileLocation, text);

            await op.Execute();

            Assert.IsTrue(File.Exists(_fileLocation));
        }
    }
}
