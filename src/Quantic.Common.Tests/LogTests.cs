using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantic.Common.Tests
{
	[TestClass]
	public class LogTests
	{
		[TestMethod]
		public void Log_Get_ReturnsValidLogger()
		{
			ILog log = Log.Get();

			Assert.IsNotNull(log);
		}

		[TestMethod]
		public void Log_Get_NamedReturnsNamedLogger()
		{
			ILog log = Log.Get("Named");

			Assert.IsNotNull(log);
			Assert.AreEqual("Named", log.Logger.Name);
		}

		[TestMethod]
		public void Log_Get_TypedReturnsTypedLogged()
		{
			ILog log = Log.Get(GetType());

			Assert.IsNotNull(log);
			Assert.AreEqual(GetType().FullName, log.Logger.Name);
		}
	}
}
