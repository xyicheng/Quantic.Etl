using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Quantic.Common
{
    /// <summary>
    ///     Common runtime checks that aid in precondition checking.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        ///     Requires the specified parameter to be non-null, and throws an <see cref="ArgumentNullException" /> upon failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        [DebuggerStepThrough]
        public static void NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        ///     Requires the specified parameter to be neither null nor empty, and throws an <see cref="ArgumentNullException" />
        ///     upon failure.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        ///     Requires the specified parameter to be neither null nor whitespace, and throws a
        ///     <see cref="ArgumentNullException" /> upon failure.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        [DebuggerStepThrough]
        public static void NotNullOrWhitespace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        ///     Requires the specified condition to evaluate to true, and throws an <see cref="ArgumentException" /> if it fails.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        [DebuggerStepThrough]
        public static void Condition(bool assertion, string parameterName)
        {
            if (!assertion)
                throw new ArgumentException(parameterName);
        }

        /// <summary>
        ///     Requires neither the specified collection, nor any of its elements to be null, and throws an
        ///     <see cref="ArgumentNullException" /> if it fails.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        [DebuggerStepThrough]
        public static void NotNullOrNullElements<T>(IEnumerable<T> collection, string parameterName) where T : class
        {
            NotNull(collection, parameterName);

            if (collection.Any(o => o == null))
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        ///     Requires neither the specified collection, nor any of its key/value pairs to be null, and throws an
        ///     <see cref="ArgumentNullException" /> if it fails.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        [DebuggerStepThrough]
        public static void NotNullOrNullElements<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> collection,
            string parameterName) where TKey : class
            where TValue : class
        {
            NotNull(collection, parameterName);

            if (collection.Any(e => e.Key == null || e.Value == null))
                throw new ArgumentNullException(parameterName);
        }
    }
}