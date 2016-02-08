using System.Collections.Generic;
using System.Linq;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl.Extensions
{
    /// <summary>
    /// Provides various extensions to the <see cref="IRow"/> interface.
    /// </summary>
    public static class RowExtensions
    {
        /// <summary>
        /// Returns the specified row as a dictionary of column/value pairs.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this IRow row)
        {
            return row.Columns.ToDictionary(c => c, row.Get);
        }
    }
}
