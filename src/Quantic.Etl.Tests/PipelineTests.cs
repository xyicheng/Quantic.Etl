using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantic.Etl.Tests
{
    [TestClass]
    public class PipelineTests
    {
        [TestMethod]
        public void Pipeline_Start_IsRunning()
        {
            var pipeline = new TestPipeline();

            pipeline.Start();

            Assert.IsTrue(pipeline.IsRunning);
        }

        [TestMethod]
        public async Task Pipeline_Start_ProcessRuns()
        {
            // This is a horrible test, please kill me for it.
            var pipeline = new TestPipeline();

            pipeline.Start();

            await Task.Delay(500);

            Assert.AreEqual(10, pipeline.Number);
        }

        [TestMethod]
        public async Task Pipeline_Stop_StopsRunning()
        {
            var pipeline = new TestPipeline();

            pipeline.Start();

            await pipeline.Stop();

            Assert.IsFalse(pipeline.IsRunning);
        }

        [TestMethod]
        public async Task Pipeline_Dispose_StopsProcessing()
        {
            var pipeline = new TestPipeline();

            pipeline.Start();
            pipeline.Dispose();

            Assert.IsFalse(pipeline.IsRunning);
        }
    }

    internal class TestPipeline : PipelineBase
    {
        internal int Number { get; set; }
        public override async Task Process()
        {
            Number = 10;
        }
    }
}
