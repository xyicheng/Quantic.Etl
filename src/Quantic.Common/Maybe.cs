using System;
using System.Collections.Generic;

namespace Quantic.Common
{
    /// <summary>
    ///     Provides a wrapper class for reference types, where a value may or may not be held.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Maybe<T> : IEquatable<Maybe<T>> where T : class
    {
        /// <summary>
        ///     Gets an instance of Maybe
        ///     <typeparam name="T">T</typeparam>
        ///     which doesn't hold a value.
        /// </summary>
        public static Maybe<T> NoValue => default(Maybe<T>);

        /// <summary>
        ///     Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue { get; }

        /// <summary>
        ///     Gets the value, if a value is held; otherwise, null.
        /// </summary>
        public T Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Maybe{T}" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Maybe(T value)
        {
            HasValue = true;
            Value = value;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="Maybe{T}" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Maybe<T> other)
        {
            if (HasValue)
            {
                if (other.HasValue)
                    return EqualityComparer<T>.Default.Equals(Value, other.Value);

                return false;
            }

            if (other.HasValue)
                return false;

            return true;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Maybe<T> && Equals((Maybe<T>)obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (HasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }
    }
}
