using Lite.Databases;
using Lite.Sets;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認.Set
{
    [TestClass]
    public class Test_Relationship_Assign
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
            var e = this.e;
            var table1 = new Set<table1>();
            table1.Add(new table1(1));
            table1.Add(new table1(2));
            var table2 = new Set<table2>();
            table2.Add(new table2(1, 1));
            table2.Add(new table2(2, 1));
            table2.Add(new table2(3, 2));
            table2.Add(new table2(4, 2));
            e.table1.Assign(table1);
            e.table2.Assign(table2);
            e.Commit();
        }
        [TestMethod]
        [ExpectedException(typeof(RelationshipException))]
        public void table1table2参照制約RelationshipException0()
        {
            var e0 = this.e;
            var e1=e0.Transaction();
            var table1 = new Set<table1>();
            table1.Add(new table1(1));
            table1.Add(new table1(3));
            var table2 = new Set<table2>();
            table2.Add(new table2(1, 2));
            e1.table1.Assign(table1);
            e1.table2.Assign(table2);
            e1.Commit();
        }
        [TestMethod, ExpectedException(typeof(RelationshipException))]
        public void table1table2参照制約RelationshipException1()
        {
            var e = this.e;
            var table1 = new Set<table1>();
            table1.Add(new table1(1));
            table1.Add(new table1(2));
            var table2 = new Set<table2>();
            table2.Add(new table2(1, 3));
            e.table1.Assign(table1);
            e.table2.Assign(table2);
            e.Commit();
        }
        [TestMethod]
        public void table1table2参照制約RelationshipException2()
        {
            const Int32 Count = 10;
            var e = this.e;
            var table1 = new Set<table1>();
            var table2 = new Set<table2>();
            for (var b = 0; b < Count; b++)
            {
                for (var a = 0; a < b; a++)
                {
                    table1.Add(new table1(a));
                }
                for (var a = 0; a <= b; a++)
                {
                    table2.Add(new table2(1, a));
                }
            }
            e.table1.Assign(table1);
            e.table2.Assign(table2);
            try
            {
                e.Commit();
                Assert.Fail("RelationshipExceptionが発生するべき");
            }
            catch (RelationshipException) { }
        }
    }
}
