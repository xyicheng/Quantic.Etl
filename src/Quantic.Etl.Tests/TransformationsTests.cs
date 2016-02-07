using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }

    [TransformationFor(typeof(string), typeof(int))]
    public class TestTransformation : ITransformation
    {
        public Type SourceType { get; set; }
        public Type DestinationType { get; set; }
        public Task<TDest> Transform<TSource, TDest>(TSource source)
        {
            throw new NotImplementedException();
        }
    }
}
