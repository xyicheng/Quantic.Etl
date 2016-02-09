using System;
using System.Collections.Generic;

namespace Quantic.Etl.Abstractions
{
	/// <summary>
	///     Interface that all types representing a Row should implement.
	/// </summary>
	/// <seealso cref="System.IEquatable{Quantic.Etl.Abstractions.IRow}" />
	public interface IRow : IEquatable<IRow>
	{
		/// <summary>
		///     Gets all columns in this row.
		/// </summary>
		/// <value>
		///     The columns.
		/// </value>
		IEnumerable<string> Columns { get; }

		/// <summary>
		///     Gets or sets the <see cref="System.Object" /> at the specified column.
		/// </summary>
		/// <value>
		///     The <see cref="System.Object" />.
		/// </value>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		object this[string column] { get; set; }

		/// <summary>
		///     Adds a column with the specified name, and optionally the specified value.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		IRow AddColumn(string column, object value = null);

		/// <summary>
		///     Removes the column with the specified name from this row.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		IRow RemoveColumn(string column);

		/// <summary>
		///     Renames the specified column to the specified new name, retaining its value.
		/// </summary>
		/// <param name="oldName">The old name.</param>
		/// <param name="newName">The new name.</param>
		/// <returns></returns>
		IRow RenameColumn(string oldName, string newName);

		/// <summary>
		///     Sets the specified column to the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="column">The column.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		IRow Set<T>(string column, T value);

		/// <summary>
		///     Applies the specified transformation to the current row.
		/// </summary>
		/// <param name="transformFunc">The transform function.</param>
		/// <returns></returns>
		IRow Transform(Func<IRow, IRow> transformFunc);

		/// <summary>
		///     Gets the value of the specified column.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		object Get(string column);
	}
}