using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantic.Common.Tests
{
    [TestClass]
    public class MaybeTests
    {
        [TestMethod]
        public void Maybe_Default_HasNoValue()
        {
            var maybe = Maybe<string>.NoValue;

            Assert.IsFalse(maybe.HasValue);
        }

        [TestMethod]
        public void Maybe_Default_ValueIsNull()
        {
            var maybe = Maybe<string>.NoValue;

            Assert.IsNull(maybe.Value);
        }

        [TestMethod]
        public void Maybe_String_HasValue()
        {
            var maybe = new Maybe<string>("Value");

            Assert.IsTrue(maybe.HasValue);
        }

        [TestMethod]
        public void Maybe_String_ValueIsCorrect()
        {
            var val = "Value";
            var maybe = new Maybe<string>(val);

            Assert.AreEqual(val, maybe.Value);
        }

        [TestMethod]
        public void Maybe_Equals_ComparisonByValue()
        {
            var val = "Value";
            var a = new Maybe<string>(val);
            var b = new Maybe<string>(val);

            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void Maybe_Equals_SameValueAreEqual()
        {
            var val = "Value";
            var a = new Maybe<string>(val);
            object b = new Maybe<string>(val);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Maybe_Equals_DifferentValuesAreNotEqual()
        {
            var a = new Maybe<string>("a");
            var b = new Maybe<string>("b");

            Assert.AreNotEqual(a, b);
        }

        [TestMethod]
        public void Maybe_Equals_BothHaveSameValueIsEqual()
        {
            var a = new Maybe<string>("a");
            var b = new Maybe<string>("a");

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Maybe_Equals_OnlyLeftHasValueNotEqual()
        {
            var a = new Maybe<string>("a");
            var b = Maybe<string>.NoValue;

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Maybe_Equals_OnlyRightHasValueNotEqual()
        {
            var a = Maybe<string>.NoValue;
            var b = new Maybe<string>("b");

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Maybe_Equals_NoValueEqualityIsTrue()
        {
            var a = Maybe<string>.NoValue;
            var b = Maybe<string>.NoValue;

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Maybe_Equals_NullIsNotEqual()
        {
            object a = null;
            var b = new Maybe<string>("b");

            Assert.IsFalse(b.Equals(a));
        }

        [TestMethod]
        public void Maybe_GetHashCode_SameHashForSameValues()
        {
            var a = new Maybe<string>("a");
            var b = new Maybe<string>("a");

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Maybe_GetHashCode_DifferentHashForDifferentValues()
        {
            var a = new Maybe<string>("a");
            var b = new Maybe<string>("b");

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
            ;
        }
    }
}
