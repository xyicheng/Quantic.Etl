using System;
using System.Data;
using System.Data.SqlClient;
using Quantic.Common;

namespace Quantic.Etl
{
    /// <summary>
    ///     Provides various properties and methods for accessing ADO.NET databases.
    /// </summary>
    internal static class Database
    {
        /// <summary>
        ///     Gets a connection to the database using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Connection string may not be null or empty.</exception>
        public static IDbConnection GetConnection(string connectionString)
        {
            Requires.NotNullOrEmpty(connectionString, nameof(connectionString));

            return new SqlConnection(connectionString);
        }

        /// <summary>
        ///     Gets a connection to the database using the specified connection string and credentials.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Connection string and credentials may not be null.</exception>
        public static IDbConnection GetConnection(string connectionString, SqlCredential credentials)
        {
            Requires.NotNullOrEmpty(connectionString, nameof(connectionString));
            Requires.NotNull(credentials, nameof(credentials));

            return new SqlConnection(connectionString, credentials);
        }
    }
}