using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantic.Common;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl.Tests
{
    [TestClass]
    public class TransformationsTests
    {
        [TestInitialize]
        public void Setup()
        {
            Transformations.ReloadTransformations();
        }

        [TestMethod]
        public void Transformations_Find_ContainsTestTransformation()
        {
            var transform = Transformations.TryFind<string, int>();

            Assert.IsTrue(transform.HasValue);
        }

        [TestMethod]
        public async Task Transformations_Transform_StringToInt()
        {
            var transform = Transformations.TryFind<string, int>();

            Assert.IsTrue(transform.HasValue);

            var s = "10";
            int i = (int) await transform.Value.Transform(s);

            Assert.AreEqual(10, i);
        }
    }

    [TransformationFor(typeof(string), typeof(int))]
    public class TestTransformation : ITransformation
    {
        public Type SourceType { get; set; }

        public Type DestinationType { get; set; }

        public async Task<object> Transform(object source)
        {
            Requires.NotNull(source, nameof(source));

            int ret;
            string s = (string) source;

            if (!int.TryParse(s, out ret))
                throw new ArgumentException(
                    $"Could not apply conversion from {SourceType.Name} to {DestinationType.Name} on the specified object!");

            return ret;
        }
    }
}
