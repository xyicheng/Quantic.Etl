using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Quantic.Common;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl
{
    public static class Transformations
    {
        private static readonly ILog _log = Log.Get();
        private static readonly HashSet<ITransformation> _transformationCache = new HashSet<ITransformation>();

        /// <summary>
        ///     Reloads all transformations.
        /// </summary>
        public static void ReloadTransformations()
        {
            _transformationCache.Clear();

            _log.Debug("Reloading transformation cache..");

            // Iterate all assemblies in the application domain so users of the lib can define their own types, and we'd still pick them up.
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in asm.GetTypes())
                {
                    // We don't look for attributes on derived types here because we'll end up checking them
                    // later down the line anyway.
                    var attrib =
                        (TransformationForAttribute)
                            type.GetCustomAttributes(typeof (TransformationForAttribute), false).FirstOrDefault();

                    if (attrib == null)
                        continue;

                    var transform = (ITransformation) Activator.CreateInstance(type);

                    if (transform == null)
                        throw new TypeInitializationException(type.FullName,
                            new Exception($"Could not instantiate type of {type.FullName} for transformation reload!"));

                    transform.SourceType = attrib.SourceType;
                    transform.DestinationType = attrib.DestinationType;

                    _transformationCache.Add(transform);
                }
            }

            _log.Debug($"Reload of transformations complete, {_transformationCache.Count} transformation(s) loaded.");
        }

        /// <summary>
        ///     Attempts to find a transformation between the two specified types. This method throws an
        ///     <see cref="AmbiguousMatchException" /> if more than one transformation is found, and returns a
        ///     <see cref="Maybe{T}" /> instance containing no value if no suitable transformation could be found.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDest">The type of the dest.</typeparam>
        /// <returns></returns>
        /// <exception cref="System.Reflection.AmbiguousMatchException">
        ///     Multiple transformations found for transforming from
        ///     {sourceType.Name} to {destType.Name} - only one transformation can exist per source/destination combination!
        /// </exception>
        public static Maybe<ITransformation> TryFind<TSource, TDest>()
        {
            if (!_transformationCache.Any())
                return Maybe<ITransformation>.NoValue;

            var sourceType = typeof (TSource);
            var destType = typeof (TDest);

            var transformations =
                _transformationCache.Where(t => t.SourceType == sourceType && t.DestinationType == destType).ToList();

            // We'll just throw straight away. If we have more than one transformation for a combination of types, we have no way
            // of knowing which one to apply first, and after the first has been applied, there is no way to reliably apply any subsequent.
            // This is a user error that must be fixed.
            if (transformations.Count > 1)
                throw new AmbiguousMatchException(
                    $"Multiple transformations found for transforming from {sourceType.Name} to {destType.Name} - only one transformation can exist per source/destination combination!");

            var trans = transformations.FirstOrDefault();

            return trans != null
                ? new Maybe<ITransformation>(transformations.FirstOrDefault())
                : Maybe<ITransformation>.NoValue;
        }
    }
}