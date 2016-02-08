using System;
using System.Threading.Tasks;

namespace Quantic.Etl.Abstractions
{
    /// <summary>
    ///     Interface that all types representing a transformation between two types should implement.
    /// </summary>
    public interface ITransformation
    {
        /// <summary>
        ///     Gets the type of the source.
        /// </summary>
        /// <value>
        ///     The type of the source.
        /// </value>
        Type SourceType { get; set; }

        /// <summary>
        ///     Gets or the type of the destination.
        /// </summary>
        /// <value>
        ///     The type of the destination.
        /// </value>
        Type DestinationType { get; set; }

        /// <summary>
        ///     Applies the transformation from source type to the destination type.
        /// </summary>
        /// <returns></returns>
        Task<object> Transform(object source);
    }
}