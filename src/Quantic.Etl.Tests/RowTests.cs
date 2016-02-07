using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quantic.Etl.Tests
{
    [TestClass]
    public class RowTests
    {
        private static TestRow TestRow => new TestRow() { Name = "Test Row", Number = 10, Double = 20.4d };

        [TestMethod]
        public void Row_FromFile_ValidRow()
        {
            var row = Row.FromObject(TestRow);

            Assert.IsNotNull(row);
            Assert.AreEqual("Test Row", row.Get("Name"));
            Assert.AreEqual(10, row.Get("Number"));
            Assert.AreEqual(20.4d, row.Get("Double"));
        }

        [TestMethod]
        public void Row_ToObject_ValidObject()
        {
            bool complete = false;
            var row = Row.FromObject(TestRow).ToObject<TestRow>(out complete);

            Assert.IsNotNull(row);
            Assert.IsTrue(complete);
            Assert.AreEqual(row.Name, TestRow.Name);
            Assert.AreEqual(row.Number, TestRow.Number);
            Assert.AreEqual(row.Double, TestRow.Double);

            var row2 = Row.FromObject(TestRow).ToObject<TestRow>();

            Assert.IsNotNull(row2);
            Assert.AreEqual(row2.Name, TestRow.Name);
            Assert.AreEqual(row2.Number, TestRow.Number);
            Assert.AreEqual(row2.Double, TestRow.Double);
        }

        [TestMethod]
        public void Row_Equals_NullRowNotEqual()
        {
            Row r = null;

            Assert.IsFalse(TestRow.Equals(r));
        }

        [TestMethod]
        public void Row_Equals_EmptyRowsAreEqual()
        {
            var a = new Row();
            var b = new Row();

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Row_Equals_SimilarRowsAreEqual()
        {
            var a = new Row(new Dictionary<string, object>() {{"Key", 10f}});
            var b = new Row(new Dictionary<string, object>() {{"Key", 10f}});

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Row_AddColumn_NewColumnExists()
        {
            var row = new Row();

            row.AddColumn("Foo", 10);

            Assert.IsTrue(row.Columns.Any(c => c == "Foo"));
            Assert.AreEqual(10, row.Get("Foo"));
        }

        [TestMethod]
        public void Row_RemoveColumn_ColumnIsRemoved()
        {
            var row = new Row(new Dictionary<string, object>() {{"Foo", 10}});

            row.RemoveColumn("Foo");

            Assert.IsFalse(row.Columns.Any(c => c == "Foo"));
        }

        [TestMethod]
        public void Row_TransformColumnValueType_DataChanged()
        {
            var row = new Row(new Dictionary<string, object>() { { "Foo", 10 } });

            row.TransformColumn<int>("Foo", o => o += 10);

            Assert.AreEqual(20, row.Get("Foo"));
        }

        [TestMethod]
        public void Row_TransformColumnReferenceType_DataChanged()
        {
            var row = new Row(new Dictionary<string, object>() { { "Foo", "Bar" } });

            row.TransformColumn<string>("Foo", o => o = "Lol");

            Assert.AreEqual("Lol", row.Get("Foo"));
        }

        [TestMethod]
        public void Row_Set_ValueIsSet()
        {
            var row = new Row(new Dictionary<string, object>() { { "Foo", "Bar" } });

            row.Set("Foo", "Bar");

            Assert.AreEqual("Bar", row.Get("Foo"));
        }
    }

    public class TestRow
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public double Double { get; set; }
    }
}
