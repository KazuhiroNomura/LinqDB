using Lite.Databases;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認.Set
{
    [TestClass]
    public class Test_Relationship_Add
    {
        private SetEntities e;
        [TestInitialize]
        public void MyTestInitialize()
        {
            this.e = new SetEntities();
        }
        [TestCleanup]
        public void MyTestCleanup()
        {
            this.e.Dispose();
        }
        [TestMethod]
        public void 親子参照制約()
        {
            var table1 = this.e.table1;
            table1.Add(new table1(1));
            table1.Add(new table1(2));
            var table2 = this.e.table2;
            table2.Add(new table2(1, 1));
            table2.Add(new table2(2, 1));
            table2.Add(new table2(3, 2));
            table2.Add(new table2(4, 2));
        }
        [TestMethod]
        [ExpectedException(typeof(RelationshipException))]
        public void table1table2参照制約RelationshipException0()
        {
            this.e.table1.Add(new table1(1));
            this.e.table1.Add(new table1(3));
            this.e.table2.Add(new table2(1, 2));
        }
        [TestMethod, ExpectedException(typeof(RelationshipException))]
        public void table1table2参照制約RelationshipException1()
        {
            this.e.table1.Add(new table1(1));
            this.e.table1.Add(new table1(2));
            this.e.table2.Add(new table2(1, 3));
        }
        [TestMethod]
        public void table1table2参照制約RelationshipException2()
        {
            const Int32 Count = 10;
            var e = this.e;
            var table1 = e.table1;
            var table2 = e.table2;
            for (var b = 0; b < Count; b++)
            {
                for (var a = 0; a < b; a++)
                {
                    table1.Add(new table1(a));
                }
                for (var a = 0; a < b; a++)
                {
                    table2.Add(new table2(1, a));
                }
                try
                {
                    table2.Add(new table2(1, b));
                    Assert.Fail("RelationshipExceptionが発生するべき");
                }
                catch (RelationshipException) { }
            }
        }
    }
}
