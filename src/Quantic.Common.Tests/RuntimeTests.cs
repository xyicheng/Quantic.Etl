using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantic.Common.Tests
{
	[TestClass]
	public class RuntimeTests
	{
		[TestMethod]
		public void Runtime_ApplicationPath_Valid()
		{
			Assert.IsNotNull(Runtime.ApplicationPath);
		}

		[TestMethod]
		public void Runtime_ApplicationPath_Exists()
		{
			Debug.WriteLine("ApplicationPath: " + Runtime.ApplicationPath);

			Assert.IsTrue(Directory.Exists(Runtime.ApplicationPath));
		}

		[TestMethod]
		public void Runtime_Version_Valid()
		{
			Assert.IsTrue(Runtime.Version.Major > 0);
		}
	}
}
