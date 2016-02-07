using System;
using System.Threading;
using System.Threading.Tasks;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl
{
    /// <summary>
    ///     Provides a base implementation for pipeline types.
    /// </summary>
    internal abstract class PipelineBase : IPipeline, IDisposable
    {
        private CancellationTokenSource _cts;
        private Task _runningTask;

        #region Implementation of IDisposable

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_cts == null)
                return;

            try
            {
                _cts.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            _cts.Dispose();
            _cts = null;

            IsRunning = false;
        }

        #endregion

        /// <summary>
        ///     Processes this pipeline.
        /// </summary>
        /// <returns></returns>
        public abstract Task Process();

        /// <summary>
        ///     Starts this pipeline instance.
        /// </summary>
        public void Start()
        {
            _cts = new CancellationTokenSource();

            _runningTask = Task.Run(Run);

            IsRunning = true;
        }

        /// <summary>
        ///     Stops this pipeline instance from processing.
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            _cts.Cancel();

            try
            {
                await _runningTask;
            }
            catch (OperationCanceledException)
            {
            }

            _cts.Dispose();
            _cts = null;

            IsRunning = false;
        }

        /// <summary>
        ///     Gets a value indicating whether this pipeline is running, i.e. processing.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning { get; private set; }

        private async Task Run()
        {
            while (!_cts.IsCancellationRequested)
                await Process();
        }
    }
}