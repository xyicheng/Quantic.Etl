using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Quantic.Common;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl.Operations
{
    /// <summary>
    ///     Represents an SQL query operation that yields a collection of results of the specified type.
    ///     This type should be used as a one-off SQL command - after Execute has been called, the connection object this
    ///     operation is constructed with, will be closed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlQueryOperation<T> : IOperation<IEnumerable<T>>
    {
        private readonly IDbConnection _connection;
        private readonly string _query;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlQueryOperation{T}" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="query">The query.</param>
        /// <exception cref="ArgumentNullException">Connection and query may not be null.</exception>
        public SqlQueryOperation(IDbConnection connection, string query)
        {
            Requires.NotNull(connection, nameof(connection));
            Requires.NotNullOrEmpty(query, nameof(query));

            _connection = connection;
            _query = query;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlQueryOperation{T}" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="query">The query.</param>
        /// <exception cref="ArgumentNullException">ConnectionString and Query may not be null or empty.</exception>
        public SqlQueryOperation(string connectionString, string query)
        {
            Requires.NotNullOrEmpty(connectionString, nameof(connectionString));
            Requires.NotNullOrEmpty(query, nameof(query));

            _connection = Database.GetConnection(connectionString);
            _query = query;
        }

        /// <summary>
        ///     Executes this instance, and returns the result of the operation.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Execute()
        {
            using (var con = _connection)
            {
                con.Open();

                return await con.QueryAsync<T>(_query);
            }
        }
    }
}