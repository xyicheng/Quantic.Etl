using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Quantic.Common;

namespace Quantic.Etl
{
    /// <summary>
    ///     Represents a row in a data table.
    /// </summary>
    public class Row : IEquatable<Row>
    {
        // Key contains the name of the column, value contains the value.
        private readonly Dictionary<string, object> _columns = new Dictionary<string, object>();

        /// <summary>
        ///     Gets the columns for this <see cref="Row" />
        /// </summary>
        /// <value>
        ///     The columns.
        /// </value>
        public IEnumerable<string> Columns => new List<string>(_columns.Keys);

        #region Implementation of IEquatable<Row>

        public bool Equals(Row other)
        {
            if (other == null)
                return false;

            // Do all column names match?
            if (!_columns.Keys.SequenceEqual(other._columns.Keys, EqualityComparer<string>.Default))
                return false;

            // This should definitely not be using != for equality comparison: implement EqualityComparer<T> here somehow.
            // Second, check for equality in all the column values.
            if (_columns.Any(c => other._columns[c.Key] != c.Value))
                return false;

            return true;
        }

        #endregion

        private void FromObjectInternal(object obj, BindingFlags bindingFlags)
        {
            var properties = obj.GetType().GetProperties(bindingFlags);
            var fields = obj.GetType().GetFields(bindingFlags);

            foreach (var p in properties)
            {
                if (_columns.ContainsKey(p.Name))
                    throw new InvalidOperationException(
                        "Row objects can't be initialized more than once - are you sure this Row wasn't initialized previously?");

                _columns.Add(p.Name, p.GetValue(obj));
            }

            foreach (var f in fields)
            {
                if (_columns.ContainsKey(f.Name))
                    throw new InvalidOperationException(
                        "Row objects can't be initialized more than once - are you sure this Row wasn't initialized previously?");

                _columns.Add(f.Name, f.GetValue(obj));
            }
        }

        /// <summary>
        ///     Transforms the specified column's value using the specified transformation.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="transformation">The transformation.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Neither parameters may be null (or empty).</exception>
        public Row TransformColumn(string column, Action<object> transformation)
        {
            Requires.NotNullOrEmpty(column, nameof(column));
            Requires.NotNull(transformation, nameof(transformation));

            transformation(_columns[column]);

            return this;
        }

        /// <summary>
        ///     Adds a column with the specified name, and assigns it the specified value.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Column can not be null.</exception>
        public Row AddColumn(string column, object value)
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
        public Row RemoveColumn(string column)
        {
            Requires.NotNull(column, nameof(column));

            _columns.Remove(column);

            return this;
        }

        /// <summary>
        ///     Sets the value of the column with the specified name to the specified value.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Row doesn't contain the specified column.</exception>
        public Row Set(string column, object value)
        {
            Requires.NotNull(column, nameof(column));

            if (!_columns.ContainsKey(column))
                throw new ArgumentException(
                    $"Can not set value for column {column} - row doesn't contain a column by this name!");

            _columns[column] = value;

            return this;
        }

        /// <summary>
        ///     Initializes this <see cref="Row" /> from the specified object, obtaining its columns and values from its properties
        ///     and fields.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        public void FromObject(object obj, BindingFlags bindingFlags = BindingFlags.Default)
        {
            Requires.NotNull(obj, nameof(obj));

            FromObjectInternal(obj, bindingFlags);
        }

        /// <summary>
        ///     Applies the current row to a new object of the specified type. This method will return true if all columns and their
        ///     values could be successfully mapped to an object of the specified type, and false if it couldn't.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>True if all columns and their values could be mapped to the specified object, otherwise false.</returns>
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
        public bool ToObject<T>(out T obj)
        {
            obj = Activator.CreateInstance<T>();
            var completeSuccess = true;

            foreach (var p in obj.GetType().GetProperties())
            {
                if (!_columns.ContainsKey(p.Name) || !p.CanWrite)
                {
                    completeSuccess = false;
                    continue;
                }

                p.SetValue(obj, _columns[p.Name]);
            }

            foreach (var f in obj.GetType().GetFields())
            {
                if (!_columns.ContainsKey(f.Name))
                {
                    completeSuccess = false;
                    continue;
                }

                f.SetValue(obj, _columns[f.Name]);
            }

            return completeSuccess;
        }
    }
}