using System.Threading.Tasks;

namespace Quantic.Etl.Abstractions
{
    /// <summary>
    /// Interface all types representing an operation should implement.
    /// </summary>
    public interface IOperation<T>
    {
        /// <summary>
        /// Executes this operation.
        /// </summary>
        /// <returns></returns>
        Task<T> Execute();
    }
}
