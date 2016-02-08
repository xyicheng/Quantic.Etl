using System;
using System.Collections.Generic;

namespace Quantic.Etl.Abstractions
{
    public interface IRow : IEquatable<IRow>
    {
        IEnumerable<string> Columns { get; }

        IRow AddColumn(string column, object value = null);

        IRow RemoveColumn(string column);

        IRow Set<T>(string column, T value);

        IRow Transform<T>(string column, Func<T, T> transformFunc);

        object Get(string column);
    }
}
