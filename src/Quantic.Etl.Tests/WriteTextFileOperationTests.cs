using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantic.Etl.Operations;

namespace Quantic.Etl.Tests
{
    [TestClass]
    public class WriteTextFileOperationTests
    {
        private static string _fileLocation = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "QuanticTestWrite.txt");
    }
}
