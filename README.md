![Build Status](https://ci.appveyor.com/api/projects/status/github/aevitas/Quantic.Etl?branch=master&svg=true)

# Summary
Quantic ETL is a library providing powerful API to support extracting data from a source database, transforming it, and loading it into a different database - often a data mart or data warehouse.

The design is focused around the use of pipelines to transmit data between the three stages, and provides support for defining data structures in POCO classes every step along the way. Quantic ETL strongly focuses on definition over configuration, which allows for instance transformations to be defined in plain C#, which can then be loaded from any assembly using the library.

The library is strongly focused on being data source agnostic. This means it will work on MySQL, Microsoft SQL, and virtually any database provider that ADO.NET support through `IDbConnection`.

This library makes use of `async`/`await` and the TAP wherever possible to allow for scalability.

# Examples

This section will be updated shortly. In the mean time, please refer to the unit tests included in the project.

# Requirements
To use Quantic ETL, you'll need:

* Visual Studio 2015, or 2013 with at least update 4
* .NET 4.5 or higher

# License
Quantic ETL is licensed under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0). Dependencies may be licensed independently. Please respect all licenses.
