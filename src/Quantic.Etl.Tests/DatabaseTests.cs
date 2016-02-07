using System;
using System.Data.SqlClient;
using System.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantic.Etl.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Database_GetConnection_ThrowsIfEmptyConnectionString()
        {
            Database.GetConnection(string.Empty);
        }

        [TestMethod]
        public void Database_GetConnection_ReturnsConnection()
        {
            // Won't be able to connect, but w/e.
            var con = Database.GetConnection("Server=someserver;Database=db;User Id=user;Password =pwd;");

            Assert.IsNotNull(con);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Database_GetConnection_NullCredentialsThrows()
        {
            Database.GetConnection("", null);
        }

        [TestMethod]
        public void Database_GetConnection_ReturnsConnectionWithCredentials()
        {
            SecureString password = new SecureString();
            password.AppendChar('c'); // Pretty secure
            password.MakeReadOnly();

            var creds = new SqlCredential("username", password);

            var con = Database.GetConnection("Server=someserver;Database=db", creds);

            Assert.IsNotNull(con);
        }
    }
}
