using Lite.Databases;
using Lite.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace CoverageCS.作りたい仕様確認.Set
{
    [TestClass]
    public class Test_Transaction_Add
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
            using (var e0 = e.Transaction())
            {
                var table1 = e0.table1;
                table1.Add(new table1(1));
                table1.Add(new table1(2));
                var table2 = e0.table2;
                table2.Add(new table2(1, 1));
                table2.Add(new table2(2, 1));
                table2.Add(new table2(3, 2));
                table2.Add(new table2(4, 2));
                e0.Commit();
            }
            using (var e0 = e.Transaction())
            {
                var table1 = e0.table1;
                table1.Add(new table1(1));
                table1.Add(new table1(2));
                var table2 = e0.table2;
                table2.Add(new table2(1, 1));
                table2.Add(new table2(2, 1));
                table2.Add(new table2(3, 2));
                table2.Add(new table2(4, 2));
                e0.Commit();
            }
        }
        [TestMethod, ExpectedException(typeof(RelationshipException))]
        public void table1table2参照制約RelationshipException0()
        {
            using (var e = this.e.Transaction())
            {
                e.table1.Add(new table1(1));
                e.table1.Add(new table1(2));
                e.table2.Add(new table2(1, 3));
                e.Commit();
            }
        }
        [TestMethod, ExpectedException(typeof(RelationshipException))]
        public void table1table2参照制約RelationshipException1()
        {
            using (var e = this.e.Transaction())
            {
                e.table1.Add(new table1(1));
                e.table1.Add(new table1(2));
                e.table2.Add(new table2(1, 3));
                e.Commit();
            }
        }
        [TestMethod]
        public void table1table2参照制約RelationshipException2()
        {
            const Int32 Count = 10;
            using (var e = this.e.Transaction())
            {
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
        [TestMethod]
        public void 多対多参照制約1()
        {
            //  0,  0
            //  0,  1
            //  0,  9
            //  1,  0
            //  1,  1
            //  1   8
            //  8,  0
            //  8,  1
            //  9,  0
            const Int32 A = 10;
            const Int32 B = A - 1;
            var e = this.e;
            using (var e0 = e.Transaction())
            {
                var 多1 = e0.多1;
                for (var a = 0; a < A; a++)
                {
                    多1.Add(new 多1(a, a, a));
                }
                var 多2 = e0.多2;
                for (var a = 0; a < A; a++)
                {
                    多2.Add(new 多2(a, a, a));
                }
                e0.Commit();
            }
            using (var e0 = e.Transaction())
            {
                var 多1多2 = e0.多1多2;
                for (var a = 0; a < A; a++)
                {
                    for (var b = 0; b < A - a; b++)
                    {
                        var 多1 = new 多1(a, a, a);
                        var 多2 = new 多2(b, b, b);
                        var v = new 多1多2(多1, 多2);
                        多1多2.Add(v);
                    }
                }
                foreach (var 多1 in e0.多1)
                {
                    var a = 多1.ID1;
                    Assert.AreEqual(A - a, 多1.多2End1ナビゲーションプロパティ.LongCount());
                }
                foreach (var 多2 in e0.多2)
                {
                    var a = 多2.ID1;
                    Assert.AreEqual(A - a, 多2.多1End2ナビゲーションプロパティ.LongCount());
                }
                e0.Commit();
            }
            foreach (var 多1 in e.多1)
            {
                var a = 多1.ID1;
                Assert.AreEqual(A - a, 多1.多2End1ナビゲーションプロパティ.LongCount());
            }
            foreach (var 多2 in e.多2)
            {
                var a = 多2.ID1;
                Assert.AreEqual(A - a, 多2.多1End2ナビゲーションプロパティ.LongCount());
            }
            using (var e0 = e.Transaction())
            {
                var 多1多2 = e0.多1多2;
                多1多2.Clear();
                for (var a = 0; a < B; a++)
                {
                    for (var b = 0; b < B - a; b++)
                    {
                        多1多2.Add(new 多1多2(new 多1(a, a, a), new 多2(b, b, b)));
                    }
                }
                foreach (var 多1 in e0.多1)
                {
                    var a = 多1.ID1;
                    Assert.AreEqual(B - a, 多1.多2End1ナビゲーションプロパティ.LongCount());
                }
                foreach (var 多2 in e0.多2)
                {
                    var a = 多2.ID1;
                    Assert.AreEqual(B - a, 多2.多1End2ナビゲーションプロパティ.LongCount());
                }
            }
            foreach (var 多1 in e.多1)
            {
                var a = 多1.ID1;
                Assert.AreEqual(A - a, 多1.多2End1ナビゲーションプロパティ.LongCount());
            }
            foreach (var 多2 in e.多2)
            {
                var a = 多2.ID1;
                Assert.AreEqual(A - a, 多2.多1End2ナビゲーションプロパティ.LongCount());
            }
        }
    }
}
