using System.Collections.Generic;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl.Tests
{
    internal static class TestRecords
    {
        internal static IEnumerable<IRow> GetPeopleTable()
        {
            return new List<IRow>()
            {
                new Row(new Dictionary<string, object>() { { "Id", 10 }, { "Name", "Pete"}, {"IsDead", true} } ),
                new Row(new Dictionary<string, object>() { { "Id", 11 }, { "Name", "Joe"}, {"IsDead", false} } ),
                new Row(new Dictionary<string, object>() { { "Id", 12 }, { "Name", "D.J. Trump"}, {"IsDead", false} } ),
                new Row(new Dictionary<string, object>() { { "Id", 13 }, { "Name", "Barry"}, {"IsDead", true} } ),
                new Row(new Dictionary<string, object>() { { "Id", 10 }, { "Name", "Susan"}, {"IsDead", false} } ),
            };
        }
    }
}
