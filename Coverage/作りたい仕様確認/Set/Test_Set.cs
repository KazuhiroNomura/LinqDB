using System;
using System.Linq;
using Lite.Optimizers;
using Lite.Sets;
using Lite.Sets.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ReplaceWithSingleCallToSingle
// ReSharper disable ReplaceWithSingleCallToAny
namespace CoverageCS.作りたい仕様確認.Set
{
    [TestClass]
    public class Test_Set
    {
        private static SetEntities T;
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            T = new SetEntities();
            {
                T.table1.Add(new table1(2));
                T.table1.Add(new table1(1));
                T.table1.Add(new table1(3));
            }
            {
                T.table2.Add(new table2(1, 1));
                T.table2.Add(new table2(2, 1));
                T.table2.Add(new table2(3, 2));
                T.table2.Add(new table2(4, 2));
                T.table2.Add(new table2(5, 3));
            }
            {
                T.StringTable.Add(new StringTable1(1, Property1: "StringTable1"));
                T.StringTable.Add(new StringTable2(2, Property2: "StringTable2"));
            }
        }
        private readonly Optimizer 変数Cache = new Optimizer();

        [TestMethod]
        public void ToArray()
        {
            T.table1.ToArray();
        }
        [TestMethod]
        public void Select()
        {
            T.table1.Select(p => p);
        }
        [TestMethod]
        public void SelectMany()
        {
            T.table1.SelectMany(p => T.table1.Select(q => new { q }), (p, q) => new { p, q });
        }
        [TestMethod]
        public void Where()
        {
            T.table1.Where(p => true);
        }
        [TestMethod]
        public void Join()
        {
            T.table1.Join(T.table2, o => o.PrimaryKey, i => i.table1table2, (o, i) => new { o, i });
            T.table2.Join(T.table1, o => o.table1table2, i => i.PrimaryKey, (o, i) => new { o, i });
        }
        [TestMethod]
        public void GroupJoin()
        {
            T.table1.GroupJoin(T.table2, o => o.PrimaryKey, i => i.table1table2, (o, i) => new { o, i });
            T.table2.GroupJoin(T.table1, o => o.table1table2, i => i.PrimaryKey, (o, i) => new { o, i });
        }
        [TestMethod]
        public void GroupJoin2()
        {
            var table1 = new[] { 0, 1 }.ToSet();
            var table2 = new[] { 0, 1 }.ToSet();
            {
                var result = table1.GroupJoin(table2, o => o, i => i, (o, i) => new { o, Single = i.Single() });
                Assert.AreEqual(2, result.LongCount());
            }
            {
                var result = table1.GroupJoin(table2, o => o, i => i, (o, i) => new { o = o / 2, Single = i.Single() });
                Assert.AreEqual(2, result.LongCount());
            }
            {
                var result = table1.GroupJoin(table2, o => o, i => i, (o, i) => new { o = o / 2, Single = i.Single() / 2 });
                Assert.AreEqual(1, result.LongCount());
            }
        }
        [TestMethod]
        public void GroupBy()
        {
            T.table1.GroupBy(p => p.PrimaryKey).Select(p => new { p.Key, p });
            T.table1.GroupBy(p => p.PrimaryKey, p => new { p });
            T.table1.GroupBy(p => p.PrimaryKey).Select(p => p.ToString());
        }
        [TestMethod]
        public void OfType()
        {
            {
                var r = T.StringTable.OfType<StringTable1>();
                Assert.AreEqual(1, r.LongCount());
            }
            {
                var r = T.StringTable.OfType<StringTable2>();
                Assert.AreEqual(1, r.LongCount());
            }
        }
        [TestMethod]
        public void Aggregate()
        {
            //10+1→11
            //11+2→13
            //13+3→16
            //16+20→36
            var v = T.table1.Select(p => p.ID).Aggregate(10m, (a, b) => a + b, p => p + 20);
            Assert.AreEqual(36, v);
        }
        [TestMethod]
        public void All()
        {
            Assert.IsFalse(T.table1.All(p => p.ID == 1));
            Assert.IsTrue(T.table1.All(p => p.ID != 10));
        }
        [TestMethod]
        public void Any()
        {
            Assert.IsTrue(T.table1.Any());
            Assert.IsFalse(T.table1.Where(p => false).Any());
        }
        [TestMethod]
        public void Average()
        {
            Assert.AreEqual(2, T.table1.Average(p => (Double)p.ID));
            Assert.AreEqual(2, T.table1.Average(p => (Decimal)p.ID));
        }
        [TestMethod]
        public void Count()
        {
            Assert.AreEqual(3, T.table1.LongCount());
        }
        [TestMethod]
        public void Max()
        {
            Assert.AreEqual(3, T.table1.Max(p => p.ID));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MaxInvalidOperationException()
        {
            Assert.AreEqual(3, T.table1.Where(p => false).Max(p => p.ID));
        }
        [TestMethod]
        public void Min()
        {
            Assert.AreEqual(1, T.table1.Min(p => p.ID));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MinInvalidOperationException()
        {
            Assert.AreEqual(1, T.table1.Where(p => false).Min(p => p.ID));
        }
        [TestMethod]
        public void Sum()
        {
            Assert.AreEqual(6, T.table1.Sum(p => p.ID));
            Assert.AreEqual(6, T.table1.Sum(p => (Int64)p.ID));
            Assert.AreEqual(6, T.table1.Sum(p => (Single)p.ID));
            Assert.AreEqual(6, T.table1.Sum(p => (Double)p.ID));
            Assert.AreEqual(6, T.table1.Sum(p => (Decimal)p.ID));
        }
        //[TestMethod]
        //public void Single() {
        //    Assert.IsNotNull(T.table1.WhereSingle(p => p.ID==1));
        //}
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void SingleInvalidOperationException1() {
        //    T.table1.WhereSingle(p => true);
        //}
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void SingleInvalidOperationException2() {
        //    T.table1.WhereSingle(p => false);
        //}

        [TestMethod]
        public void Single()
        {
            Assert.IsNotNull(T.table1.Where(p => p.ID == 1).Single());
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SingleInvalidOperationException4()
        {
            T.table1.Where(p => true).Single();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SingleInvalidOperationException5()
        {
            T.table1.Where(p => false).Single();
        }
        [TestMethod]
        public void SingleOrDefault()
        {
            var defaultValue = new table1(1);
            var v = T.table1.Where(p => p.ID == 1).SingleOrDefault();
            Assert.AreEqual(defaultValue, v);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SingleOrDefaultInvalidOperationException1()
        {
            Assert.IsNull(T.table1.SingleOrDefault());
        }
        [TestMethod]
        public void SingleOrDefault1()
        {
            Assert.AreEqual(default, T.table1.Where(p => false).SingleOrDefault());
        }
        [TestMethod]
        public void SingleOrDefault2()
        {
            var defaultValue = new table1(5);
            var v = T.table1.Where(p => false).SingleOrDefault(defaultValue);
            Assert.AreEqual(defaultValue, v);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SingleOrDefaultInvalidOperationException6()
        {
            T.table1.SingleOrDefault();
        }
        [TestMethod]
        public void SingleOrDefaultException7()
        {
            var v = T.table1.Where(p => false).SingleOrDefault();
            Assert.AreEqual(default, v);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SingleOrDefaultInvalidOperationException8()
        {
            var defaultValue = new table1(9);
            T.table1.Where(p => true).SingleOrDefault(defaultValue);
        }
        [TestMethod]
        public void SingleOrDefaultException9()
        {
            var defaultValue = new table1(10);
            var v = T.table1.Where(p => false).SingleOrDefault(defaultValue);
            Assert.AreEqual(defaultValue.ID, v.ID);
        }
        [TestMethod]
        public void SingleOrDefaultException10()
        {
            var defaultValue = new table1(1);
            var v = T.table1.Where(p => p.ID == 1).SingleOrDefault(defaultValue);
            Assert.AreEqual(defaultValue.ID, v.ID);
        }
        [TestMethod]
        public void Contains()
        {
            {
                var defaultValue = T.table1.Where(p => p.ID == 1).Single();
                var v = T.table1.Contains(defaultValue);
                Assert.IsTrue(v);
            }
            {
                var defaultValue = new table1(100);
                var v = T.table1.Contains(defaultValue);
                Assert.IsFalse(v);
            }
        }
        [TestMethod]
        public void Intersect()
        {
            var r = new Random(1);
            for (var b = 0; b < 1000; b++)
            {
                var data1 = new table1[r.Next(10)];
                var low1 = 0;
                for (var a = 0; a < data1.Length; a++)
                {
                    var value = r.Next(low1, low1 + 5);
                    data1[a] = new table1(value);
                    low1 = value + 1;
                }
                var data2 = new table1[r.Next(10)];
                var low2 = 0;
                for (var a = 0; a < data2.Length; a++)
                {
                    var value = r.Next(low2, low2 + 5);
                    data2[a] = new table1(value);
                    low2 = value + 1;
                }
                var set1 = data1.ToSet();
                var set2 = data2.ToSet();
                var result1 = data1.Intersect(data2).ToArray();
                var result2 = set1.Intersect(set2).ToArray();
                Assert.AreEqual(0, result1.Except(result2).Count());
                Assert.AreEqual(0, result2.Except(result1).Count());
            }
        }
        [TestMethod]
        public void Union()
        {
            var r = new Random(1);
            for (var b = 0; b < 1000; b++)
            {
                var data1 = new table1[r.Next(10)];
                var low1 = 0;
                for (var a = 0; a < data1.Length; a++)
                {
                    var value = r.Next(low1, low1 + 5);
                    data1[a] = new table1(value);
                    low1 = value + 1;
                }
                var data2 = new table1[r.Next(10)];
                var low2 = 0;
                for (var a = 0; a < data2.Length; a++)
                {
                    var value = r.Next(low2, low2 + 5);
                    data2[a] = new table1(value);
                    low2 = value + 1;
                }
                var set1 = data1.ToSet();
                var set2 = data2.ToSet();
                var result1 = data1.Union(data2).ToArray();
                var result2 = set1.Union(set2).ToArray();
                Assert.AreEqual(0, result1.Except(result2).Count());
                Assert.AreEqual(0, result2.Except(result1).Count());
            }
        }
        [TestMethod]
        [ExpectedException(typeof(UniqueTupleException))]
        public void DUnion重複があった場合の例外UniqueTupleException1()
        {
            var a = new[]{
                new table1(1),
                new table1(2)
            }.ToSet();
            var b = new[]{
                new table1(2),
                new table1(3),
                new table1(4)
            }.ToSet();
            a.DUnion(b);
        }
        [TestMethod]
        [ExpectedException(typeof(UniqueTupleException))]
        public void DUnion重複があった場合の例外UniqueTupleException2()
        {
            var a = new[]{
                new table1(1),
                new table1(2)
            }.ToSet();
            var b = new[]{
                new table1(2),
                new table1(3),
                new table1(4)
            }.ToSet();
            b.DUnion(a);
        }
        [TestMethod]
        public void DUnion()
        {
            var r = new Random(1);
            for (var b = 0; b < 1000; b++)
            {
                var data1 = new table1[r.Next(10)];
                var low1 = 0;
                for (var a = 0; a < data1.Length; a++)
                {
                    var value = r.Next(low1, low1 + 5);
                    data1[a] = new table1(value);
                    low1 = value + 1;
                }
                var data2 = new table1[r.Next(10)];
                var low2 = 0;
                for (var a = 0; a < data2.Length; a++)
                {
                    var value = r.Next(low2, low2 + 5);
                    data2[a] = new table1(value);
                    low2 = value + 1;
                }
                var set1 = data1.ToSet();
                var set2 = data2.ToSet();
                var result1 = data1.Union(data2).ToArray();
                if (result1.Length == data1.Length + data2.Length)
                {
                    var result2 = set1.Union(set2).ToArray();
                    Assert.AreEqual(0, result1.Except(result2).Count());
                    Assert.AreEqual(0, result2.Except(result1).Count());
                }
                else
                {
                    try
                    {
                        set1.DUnion(set2).ToArray();
                        Assert.Fail("UniqueTupleException例外が発生するべき");
                    }
                    catch (UniqueTupleException)
                    {
                    }
                }
            }
        }
        [TestMethod]
        public void Except()
        {
            var r = new Random(1);
            for (var b = 0; b < 1000; b++)
            {
                var data1 = new table1[r.Next(10)];
                var low1 = 0;
                for (var a = 0; a < data1.Length; a++)
                {
                    var value = r.Next(low1, low1 + 5);
                    data1[a] = new table1(value);
                    low1 = value + 1;
                }
                var data2 = new table1[r.Next(10)];
                var low2 = 0;
                for (var a = 0; a < data2.Length; a++)
                {
                    var value = r.Next(low2, low2 + 5);
                    data2[a] = new table1(value);
                    low2 = value + 1;
                }
                var set1 = data1.ToSet();
                var set2 = data2.ToSet();
                var result1 = data1.Except(data2).ToArray();
                var result2 = set1.Except(set2).ToArray();
                Assert.AreEqual(0, result1.Except(result2).Count());
                Assert.AreEqual(0, result2.Except(result1).Count());
            }
        }
        [TestMethod]
        public void Equals()
        {
            var a = new[]{
                new table1(1),
                new table1(2)
            }.ToSet();
            var b = new[]{
                new table1(2),
                new table1(3),
                new table1(4)
            }.ToSet();
            var c = new[]{
                new table1(1),
                new table1(2)
            }.ToSet();
            var d = new[]{
                new table1(2),
                new table1(4)
            };
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.Equals(d));
            Assert.IsTrue(a.Equals(c));
        }
        [TestMethod]
        public void Update重複しないので行数が減らない()
        {
            var a = new[]{
                new table1(1),
                new table1(2)
            }.ToSet();
            var expected = new[]{
                new table1(1),
                new table1(3)
            }.ToSet();
            var b = a.Update(p => p.ID == 2, p => new table1(3));
            Assert.AreEqual(expected, b);
            a.UpdateWith(p => p.ID == 2, p => new table1(3));
            Assert.AreEqual(expected, a);
        }
        [TestMethod]
        public void Update重複するので行数が減る()
        {
            var a = new[]{
                new table1(1),
                new table1(2),
                new table1(3)
            }.ToSet();
            var expected = new[]{
                new table1(1),
                new table1(3)
            }.ToSet();
            var b = a.Update(p => p.ID == 2, p => new table1(3));
            Assert.AreEqual(expected, b);
            a.UpdateWith(p => p.ID == 2, p => new table1(3));
            Assert.AreEqual(expected, a);
        }
        [TestMethod]
        public void Delete()
        {
            var a = new[]{
                new table1(1),
                new table1(2)
            }.ToSet();
            var expected = new[]{
                new table1(1)
            }.ToSet();
            var r = a.Delete(p => p.ID == 2);
            Assert.AreEqual(expected, r);
        }
        [TestMethod]
        public void Entitiesに代入するとき()
        {
            var a = new[]{
                new table1(1),
                new table1(2)
            }.ToSet();
            var expected = new[]{
                new table1(1)
            }.ToSet();
            var r = a.Delete(p => p.ID == 2);
            Assert.AreEqual(expected, r);
        }
        [TestMethod]
        public void ToSet()
        {
            new[] { 1, 2 }.ToSet();
        }
        [TestMethod]
        public void ToSet_Entityメンバに代入しないためPrimaryKeyExceptionは発生しない()
        {
            var a = new[] {
                new StringTable1(1,"A"),
                new StringTable1(1,"B")
            };
            a.ToSet();
        }
        [TestMethod]
        public void ToPrimaryKeySet1()
        {
            var a = new[] {
                new StringTable(1),
                new StringTable(2)
            };
            a.ToSet();
        }
        [TestMethod]
        public void ToSet1()
        {
            var a = new[] {
                new StringTable(1),
                new StringTable(2)
            };
            a.ToSet();
        }
        [TestMethod]
        public void クエリキャッシュ()
        {
            for (var a = 0; a < 100; a++)
            {
                var a1 = a;
                this.変数Cache.Execute(() => a1);
            }
        }
        [TestMethod]
        public void WhereをPrimaryKeyメソッドで呼ぶ()
        {
            //var r=new table1[]{
            //    new table1(1),
            //    new table1(2)
            //};
            var r = new[]{
                new table1(1),
                new table1(2)
            };
            var s = r.ToSet();
            s.Where(p => p == new table1(2));
        }
    }
}
