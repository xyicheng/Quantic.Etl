using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Quantic.Common;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl
{
	/// <summary>
	///     Represents a row in a data table.
	/// </summary>
	public class Row : IRow
	{
		// Key contains the name of the column, value contains the value.
		private readonly Dictionary<string, object> _columns =
			new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		/// <summary>
		///     Initializes a new instance of the <see cref="Row" /> class using the specified collection of key value pairs as its
		///     column/value source.
		/// </summary>
		/// <param name="values">The values.</param>
		public Row(IEnumerable<KeyValuePair<string, object>> values)
		{
			foreach (var v in values)
				_columns.Add(v.Key, v.Value);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="Row" /> class using the specified dictionary as its column/value
		///     source.
		/// </summary>
		/// <param name="values">The values.</param>
		public Row(IDictionary<string, object> values) : this(values.ToList())
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="Row" /> class.
		/// </summary>
		public Row()
		{
		}

		/// <summary>
		///     Gets or sets the <see cref="System.Object" /> at the specified column.
		/// </summary>
		/// <value>
		///     The <see cref="System.Object" />.
		/// </value>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		public object this[string column]
		{
			get { return Get(column); }
			set { Set(column, value); }
		}

		/// <summary>
		///     Gets the columns for this <see cref="Row" />
		/// </summary>
		/// <value>
		///     The columns.
		/// </value>
		public IEnumerable<string> Columns => new List<string>(_columns.Keys);

		#region Implementation of IEquatable<IRow>

		public bool Equals(IRow other)
		{
			if (other == null)
				return false;

			// Do all column names match?
			if (!_columns.Keys.SequenceEqual(other.Columns, EqualityComparer<string>.Default))
				return false;

			// This should definitely not be using != for equality comparison: implement EqualityComparer<T> here somehow.
			// Second, check for equality in all the column values.
			if (_columns.Any(c => !other.Get(c.Key).Equals(c.Value)))
				return false;

			return true;
		}

		#endregion

		/// <summary>
		///     Adds a column with the specified name, and assigns it the specified value.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Column can not be null.</exception>
		public IRow AddColumn(string column, object value)
		{
			Requires.NotNull(column, nameof(column));

			_columns.Add(column, value);

			return this;
		}

		/// <summary>
		///     Removes the specified column and its value from this row.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Column can not be null.</exception>
		public IRow RemoveColumn(string column)
		{
			Requires.NotNull(column, nameof(column));

			_columns.Remove(column);

			return this;
		}

		/// <summary>
		///     Renames the specified column to the specified new name, retaining its value.
		/// </summary>
		/// <param name="oldName">The old name.</param>
		/// <param name="newName">The new name.</param>
		/// <returns></returns>
		public IRow RenameColumn(string oldName, string newName)
		{
			Requires.NotNullOrEmpty(oldName, nameof(oldName));
			Requires.NotNullOrEmpty(newName, nameof(newName));

			var oldVal = this[oldName];

			// Note: This implementation will _always_ add the new column at the end of the column collection.
			// Reason we do this is we can't simply rename a key while maintaining the guarantee that the row name
			// will be unique. This way, we do, but sacrifice ordering of the columns instead.

			RemoveColumn(oldName);
			AddColumn(newName, oldVal);

			return this;
		}

		/// <summary>
		///     Sets the value of the column with the specified name to the specified value.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Row doesn't contain the specified column.</exception>
		public IRow Set<T>(string column, T value)
		{
			Requires.NotNull(column, nameof(column));

			if (!_columns.ContainsKey(column))
				throw new ArgumentException(
					$"Can not set value for column {column} - row doesn't contain a column by this name!");

			_columns[column] = value;

			return this;
		}

		/// <summary>
		///     Applies the specified transformation to the current row.
		/// </summary>
		/// <param name="transformFunc">The transform function.</param>
		/// <returns></returns>
		public IRow Transform(Func<IRow, IRow> transformFunc)
		{
			Requires.NotNull(transformFunc, nameof(transformFunc));

			return transformFunc(this);
		}

		/// <summary>
		///     Gets the value of the specified column, if the column exists.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		public object Get(string column)
		{
			Requires.NotNullOrEmpty(column, nameof(column));

			if (!_columns.ContainsKey(column))
				throw new KeyNotFoundException("Column " + column + " does not exist in this row!");

			return _columns[column];
		}

		private static Row FromObjectInternal(object obj, BindingFlags bindingFlags)
		{
			var row = new Row();
			var properties = obj.GetType().GetProperties();
			var fields = obj.GetType().GetFields();

			foreach (var p in properties)
			{
				if (row._columns.ContainsKey(p.Name))
					throw new InvalidOperationException(
						"Row objects can't be initialized more than once - are you sure this Row wasn't initialized previously?");

				row._columns.Add(p.Name, p.GetValue(obj));
			}

			foreach (var f in fields)
			{
				if (row._columns.ContainsKey(f.Name))
					throw new InvalidOperationException(
						"Row objects can't be initialized more than once - are you sure this Row wasn't initialized previously?");

				row._columns.Add(f.Name, f.GetValue(obj));
			}

			return row;
		}

		/// <summary>
		///     Initializes this <see cref="Row" /> from the specified object, obtaining its columns and values from its properties
		///     and fields.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="bindingFlags">The binding flags.</param>
		public static Row FromObject(object obj, BindingFlags bindingFlags = BindingFlags.Default)
		{
			Requires.NotNull(obj, nameof(obj));

			return FromObjectInternal(obj, bindingFlags);
		}

		/// <summary>
		///     Applies the current row to a new object of the specified type.
		///     The out parameter will be set to true if the entire object had all its properties and fields properly set,
		///     otherwise false.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="success">if set to <c>true</c> [success].</param>
		/// <returns>
		///     True if all columns and their values could be mapped to the specified object, otherwise false.
		/// </returns>
		/// <exception cref="MissingMethodException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch the
		///     base class exception, <see cref="T:System.MissingMemberException" />, instead.The type that is specified for
		///     <paramref name="T" /> does not have a parameterless constructor.
		/// </exception>
		/// <exception cref="TargetException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch
		///     <see cref="T:System.Exception" /> instead.The type of <paramref name="obj" /> does not match the target type, or a
		///     property is an instance property but <paramref name="obj" /> is null.
		/// </exception>
		/// <exception cref="MethodAccessException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch the
		///     base class exception, <see cref="T:System.MemberAccessException" />, instead. There was an illegal attempt to
		///     access a private or protected method inside a class.
		/// </exception>
		/// <exception cref="TargetInvocationException">
		///     An error occurred while setting the property value. The
		///     <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.
		/// </exception>
		/// <exception cref="FieldAccessException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch the base
		///     class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to
		///     access this field.
		/// </exception>
		public T ToObject<T>(out bool success)
		{
			var obj = Activator.CreateInstance<T>();
			var s = true;

			foreach (var p in obj.GetType().GetProperties())
			{
				if (!_columns.ContainsKey(p.Name) || !p.CanWrite)
				{
					s = false;
					continue;
				}

				p.SetValue(obj, _columns[p.Name]);
			}

			foreach (var f in obj.GetType().GetFields())
			{
				if (!_columns.ContainsKey(f.Name))
				{
					s = false;
					continue;
				}

				f.SetValue(obj, _columns[f.Name]);
			}

			success = s;
			return obj;
		}

		/// <summary>
		///     To the object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="MissingMethodException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch the
		///     base class exception, <see cref="T:System.MissingMemberException" />, instead.The type that is specified for
		///     <paramref name="T" /> does not have a parameterless constructor.
		/// </exception>
		/// <exception cref="TargetException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch
		///     <see cref="T:System.Exception" /> instead.The type of <paramref name="obj" /> does not match the target type, or a
		///     property is an instance property but <paramref name="obj" /> is null.
		/// </exception>
		/// <exception cref="MethodAccessException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch the
		///     base class exception, <see cref="T:System.MemberAccessException" />, instead. There was an illegal attempt to
		///     access a private or protected method inside a class.
		/// </exception>
		/// <exception cref="TargetInvocationException">
		///     An error occurred while setting the property value. The
		///     <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.
		/// </exception>
		/// <exception cref="FieldAccessException">
		///     In the .NET for Windows Store apps or the Portable Class Library, catch the base
		///     class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to
		///     access this field.
		/// </exception>
		public T ToObject<T>()
		{
			bool b;
			return ToObject<T>(out b);
		}
	}
}