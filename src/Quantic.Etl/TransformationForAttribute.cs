using System;
using Quantic.Common;

namespace Quantic.Etl
{
    /// <summary>
    ///     Determines the source and destination type for a transformation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TransformationForAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TransformationForAttribute" /> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="destinationType">Type of the destination.</param>
        public TransformationForAttribute(Type sourceType, Type destinationType)
        {
            Requires.NotNull(sourceType, nameof(sourceType));
            Requires.NotNull(destinationType, nameof(destinationType));

            SourceType = sourceType;
            DestinationType = destinationType;
        }

        /// <summary>
        ///     Gets or sets the type of the source.
        /// </summary>
        /// <value>
        ///     The type of the source.
        /// </value>
        public Type SourceType { get; set; }

        /// <summary>
        ///     Gets or sets the type of the destination.
        /// </summary>
        /// <value>
        ///     The type of the destination.
        /// </value>
        public Type DestinationType { get; set; }
    }
}