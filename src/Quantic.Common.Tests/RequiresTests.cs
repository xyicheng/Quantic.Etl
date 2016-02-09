using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantic.Common.Tests
{
    [TestClass]
    public class RequiresTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNull_ThrowsIfNull()
        {
            object o = null;

            Requires.NotNull(o, nameof(o));
        }

        [TestMethod]
        public void Requires_NotNull_PassesIfValid()
        {
            var o = new object();

            Requires.NotNull(o, nameof(o));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrEmpty_ThrowsIfEmptyString()
        {
            var s = string.Empty;

            Requires.NotNullOrEmpty(s, nameof(s));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrEmpty_ThrowsIfNull()
        {
            string s = null;

            Requires.NotNullOrEmpty(s, nameof(s));
        }

        [TestMethod]
        public void Requires_NotNullOrEmpty_PassesIfValid()
        {
            var s = "s";

            Requires.NotNullOrEmpty(s, nameof(s));
        }

        [TestMethod]
        public void Requires_NotNullOrWhiteSpace_PassesIfValid()
        {
            string s = "s";

            Requires.NotNullOrWhitespace(s, nameof(s));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrWhitteSpace_ThrowsIfNull()
        {
            string s = null;

            Requires.NotNullOrWhitespace(s, nameof(s));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires_Condition_ThrowsIfFalse()
        {
            Requires.Condition(false, nameof(Requires_Condition_ThrowsIfFalse));
        }

        [TestMethod]
        public void Requires_Condition_PassesIfTrue()
        {
            Requires.Condition(true, nameof(Requires_Condition_ThrowsIfFalse));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrNullElements_ThrowsIfNullCollection()
        {
            List<object> list = null;

            Requires.NotNullOrNullElements(list, nameof(list));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrNullElements_ThrowsIfNullElement()
        {
            var list = new List<object>
            {
                new object(),
                null,
                new object()
            };

            Requires.NotNullOrNullElements(list, nameof(list));
        }

        [TestMethod]
        public void Requires_NotNullOrNullElements_PassesIfValid()
        {
            var list = new List<object>();

            Requires.NotNullOrNullElements(list, nameof(list));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrNullElements_ThrowsIfNullKey()
        {
            var dict = new Dictionary<object, object>
            {
                {new object(), new object()},
                {null, new object()}
            };

            Requires.NotNullOrNullElements(dict, nameof(dict));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrNullElements_ThrowsIfNullValue()
        {
            var dict = new Dictionary<object, object>
            {
                {new object(), new object()},
                {null, new object()}
            };

            Requires.NotNullOrNullElements(dict, nameof(dict));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrNullElements_ThrowsIfNullDictionary()
        {
            Dictionary<object, object> dict = null;

            Requires.NotNullOrNullElements(dict, nameof(dict));
        }
    }
}