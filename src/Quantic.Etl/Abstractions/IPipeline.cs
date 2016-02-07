using System.Threading.Tasks;

namespace Quantic.Etl.Abstractions
{
    /// <summary>
    ///     Interface all types representing a pipeline should implement.
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        ///     Gets a value indicating whether this pipeline is running, i.e. processing.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        bool IsRunning { get; }

        /// <summary>
        ///     Processes this pipeline.
        /// </summary>
        /// <returns></returns>
        Task Process();

        /// <summary>
        ///     Starts this pipeline instance.
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops this pipeline instance from processing.
        /// </summary>
        /// <returns></returns>
        Task Stop();
    }
}