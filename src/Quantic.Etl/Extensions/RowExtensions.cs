﻿using System.Collections.Generic;
using System.Linq;

namespace Quantic.Etl.Extensions
{
    /// <summary>
    /// Provides various extensions to the <see cref="Row"/> class.
    /// </summary>
    public static class RowExtensions
    {
        /// <summary>
        /// Returns the specified row as a dictionary of column/value pairs.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this Row row)
        {
            return row.Columns.ToDictionary(c => c, row.Get);
        }
    }
}
