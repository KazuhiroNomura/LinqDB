using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LinqDB.Helpers;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB;

[TestClass]
[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
public class Enumerable2 : ATest
{
    [TestMethod]
    public void EnumerableAggregate0()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Aggregate((a, b) => a + b);
    }
    [TestMethod]
    public void EnumerableAggregate1()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Aggregate(1, (a, b) => a + b);
    }
    [TestMethod]
    public void EnumerableAggregate2()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Aggregate(1, (a, b) => a + b, ab => ab * 2);
    }
    [TestMethod]
    public void EnumerableAll()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.All(p => true);
        A.All(p => false);
    }
    [TestMethod]
    public void EnumerableAny0()
    {
        var A = new[] { 1, 2, 3, 4 };
        var A1 = A;
        A1.Any();
        A = System.Array.Empty<int>();
        A.Any();
    }
    [TestMethod]
    public void EnumerableAny1()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Any(p => true);
        A.Any(p => false);
    }
    [TestMethod]
    public void EnumerableAsEnumerable()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.AsEnumerable();
    }
    [TestMethod]
    public void EnumerableJoin()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Join(A, o => o, i => i, (o, i) => o + i);
    }
    [TestMethod]
    public void EnumerableOfType()
    {
        object[] A = { (sbyte)1, (short)2, 3, (long)4, (byte)5, (ushort)6, (uint)7, (ulong)8, true, '9', 10f, 11d, 12m, "13" };
        A.OfType<sbyte>();
        A.OfType<short>();
        A.OfType<int>();
        A.OfType<long>();
        A.OfType<byte>();
        A.OfType<ushort>();
        A.OfType<uint>();
        A.OfType<ulong>();
        A.OfType<bool>();
        A.OfType<char>();
        A.OfType<float>();
        A.OfType<double>();
        A.OfType<decimal>();
        A.OfType<string>();
    }
    [TestMethod]
    public void EnumerableOrderBy0()
    {
        {
            var A = new[] { -4, -3, -2, -1 };
            A.OrderBy(p => +p);
        }
        {
            var A = new[] { 3, 1, 0, 3 };
            A.OrderBy(this.ThenByNeg);
        }
        {
            var A = new[] { 1, 2, 3, 4 };
            {
                var first = A.OrderBy(this.ThenByNeg);
                var second = A.OrderBy(this.ThenByNeg);
                Assert.IsTrue(first.SequenceEqual(second));
                Assert.IsTrue(first.SequenceEqual(first));
            }
            {
                var first = A.OrderBy(p => p / 2);
                var second = A.OrderBy(p => p / 2);
                Assert.IsTrue(first.SequenceEqual(second));
                Assert.IsTrue(first.SequenceEqual(first));
            }
        }
        {
            var A = new[] { 4, 3, 2, 1 };
            A.OrderBy(this.ThenByNeg);
            A.OrderBy(p => +p);
        }
    }
    private const int 試行回数 = 16;
    [TestMethod]
    public void EnumerableOrderBy1()
    {
        for (var count = 0; count < 試行回数; count++)
        {
            var A = new int[count];
            for (var seed = 1; seed <= 試行回数; seed++)
            {
                var r = new Random(seed);
                for (var b = 0; b < count; b++)
                {
                    A[b] = r.Next(count);
                }
                A.OrderBy(p => +p);
                A.OrderBy(this.ThenByNeg);
            }
        }
    }
    [TestMethod]
    public void EnumerableOrderBy2_Delegate()
    {
        for (var count = 0; count < 試行回数; count++)
        {
            var A = new int[count];
            for (var seed = 1; seed <= 試行回数; seed++)
            {
                var r = new Random(seed);
                for (var b = 0; b < count; b++)
                {
                    A[b] = r.Next(count);
                }
                A.OrderBy(this.OrderBySelector);
                A.OrderBy(this.OrderBySelectorDesc);
            }
        }
    }
    private int OrderBySelector(int p) => +p;
    private int OrderBySelectorDesc(int p) => -p;
    [TestMethod]
    public void EnumerableOrderByDescending1()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.OrderByDescending(p => +p);
        A.OrderByDescending(p => -p);
    }
    [TestMethod]
    public void EnumerableOrderByDescending2()
    {
        for (var count = 0; count < 試行回数; count++)
        {
            var A = new int[count];
            for (var seed = 1; seed <= 試行回数; seed++)
            {
                var r = new Random(seed);
                for (var b = 0; b < count; b++)
                {
                    A[b] = r.Next(count);
                }
                A.OrderByDescending(p => +p);
                A.OrderByDescending(this.ThenByNeg);
            }
        }
    }
    [TestMethod]
    public void EnumerableThenBy0()
    {
        var A = new[] { 1, 2, 3, 4 };
        {
            var OrderBy = A.OrderBy(p => p / 2);
            var first = A.OrderBy(p => p / 2).ThenBy(p => +p);
            var second = A.OrderBy(p => p / 2).ThenBy(p => +p);
            Assert.IsTrue(first.SequenceEqual(second));
            Assert.IsTrue(first.SequenceEqual(OrderBy.ThenBy(p => +p)));
        }
    }
    [TestMethod]
    public void EnumerableThenBy1()
    {
        {
            var A = new int[2];
            {
                var r = new Random(2);
                for (var b = 0; b < 2; b++)
                {
                    A[b] = r.Next(2);
                }
                A.OrderBy(p => +p / 4).ThenBy(p => +p / 2).ThenBy(p => p);
            }
        }
        for (var count = 0; count < 試行回数; count++)
        {
            var A = new int[count];
            {
                var seed = 1;
                var r = new Random(seed);
                for (var b = 0; b < count; b++)
                {
                    A[b] = r.Next(count);
                }
                A.OrderBy(p => +p / 4).ThenBy(p => +p);
                A.OrderBy(p => +p / 4).ThenBy(this.ThenByNeg);
                A.OrderBy(p => -p / 4).ThenBy(p => +p);
                A.OrderBy(p => -p / 4).ThenBy(this.ThenByNeg);
                A.OrderBy(p => +p / 4).ThenBy(p => +p / 2).ThenBy(p => p);
                A.OrderBy(p => +p / 4).ThenBy(p => -p / 2).ThenBy(p => p);
                A.OrderBy(p => -p / 4).ThenBy(p => +p / 2).ThenBy(p => p);
                A.OrderBy(p => -p / 4).ThenBy(p => -p / 2).ThenBy(p => p);
            }
        }
    }
    private int ThenByPlus(int p) => +p;
    private int ThenByNeg(int p) => -p;
    private int ThenByPlus2(int p) => +p / 2;
    private int ThenByNeg2(int p) => -p / 2;
    [TestMethod]
    public void EnumerableThenBy_Delegate()
    {
        {
            var A = new int[2];
            {
                var r = new Random(2);
                for (var b = 0; b < 2; b++)
                {
                    A[b] = r.Next(2);
                }
                A.OrderBy(p => +p / 4).ThenBy(this.ThenByPlus2).ThenBy(this.ThenByPlus);
            }
        }
        for (var count = 0; count < 試行回数; count++)
        {
            var A = new int[count];
            {
                var seed = 1;
                var r = new Random(seed);
                for (var b = 0; b < count; b++)
                {
                    A[b] = r.Next(count);
                }
                A.OrderBy(p => +p / 4).ThenBy(this.ThenByPlus);
                A.OrderBy(p => +p / 4).ThenBy(this.ThenByNeg);
                A.OrderBy(p => -p / 4).ThenBy(this.ThenByPlus);
                A.OrderBy(p => -p / 4).ThenBy(this.ThenByNeg);
                A.OrderBy(p => +p / 4).ThenBy(this.ThenByPlus2).ThenBy(this.ThenByPlus);
                A.OrderBy(p => +p / 4).ThenBy(this.ThenByNeg2).ThenBy(this.ThenByPlus);
                A.OrderBy(p => -p / 4).ThenBy(this.ThenByPlus2).ThenBy(this.ThenByPlus);
                A.OrderBy(p => -p / 4).ThenBy(this.ThenByNeg2).ThenBy(this.ThenByPlus);
            }
        }
    }
    [TestMethod]
    public void EnumerableThenByDescending0()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.OrderByDescending(p => 0).ThenByDescending(p => +p);
        A.OrderByDescending(p => 0).ThenByDescending(this.ThenByNeg);
    }
    [TestMethod]
    public void EnumerableOrderBy最適化()
    {
        const int count = 1000;
        var A = new int[count];
        var r = new Random(1);
        for (var b = 0; b < count; b++)
        {
            A[b] = r.Next(count);
        }
        A.OrderBy(p => +p);
    }
    [TestMethod]
    public void EnumerableOrderByDescending0()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.OrderByDescending(p => +p);
        A.OrderByDescending(this.ThenByNeg);
    }
    [TestMethod]
    public void EnumerableRange()
    {
        Enumerable.Range(0, 10);
    }
    [TestMethod]
    public void EnumerableRepeat()
    {
        Enumerable.Repeat(2, 3);
    }
    [TestMethod]
    public void EnumerableReverse()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Reverse();
    }
    [TestMethod]
    public void EnumerableSelect()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Select(a => a + 1);
    }
    [TestMethod]
    public void EnumerableSelectMany0()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.SelectMany(a => A.Select(b => a + b));
    }
    [TestMethod]
    public void EnumerableSelectMany1()
    {
        var A = new[] { 1, 2, 3, 4 };
        // ReSharper disable once ConvertToLocalFunction
        Func<int, int, int> d = (a, b) => a + b;
        A.SelectMany(p => A, d);
    }
    [TestMethod]
    public void EnumerableSelectMany2()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.SelectMany(p => A, (a, b) => a + b);
    }
    [TestMethod]
    public void EnumerableSelectMany3()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.SelectMany(a => A.Select(b => b));
    }
    [TestMethod]
    public void EnumerableSequenceEqual0_0()
    {
        var A = new[] { 1, 2, 3, 4 };
        var B = new[] { 2, 3, 4, 1 };
        var C = new[] { 1, 2, 3 };
        C.SequenceEqual(A, EqualityComparer<int>.Default);
        A.SequenceEqual(C, EqualityComparer<int>.Default);
        A.SequenceEqual(A, EqualityComparer<int>.Default);
        A.SequenceEqual(B, EqualityComparer<int>.Default);
        B.SequenceEqual(A, EqualityComparer<int>.Default);
    }

    private const int 配列長 = 20;
    [TestMethod]
    public void EnumerableSequenceEqual0_1()
    {
        var r = new Random(1);
        var ArrayArray = new int[配列長][];
        for (var a = 0; a < 配列長; a++)
        {
            var Array = new int[a];
            for (var b = 0; b < a; b++)
            {
                Array[b] = r.Next(a);
            }
            ArrayArray[a] = Array;
        }
        for (var a = 0; a < 配列長; a++)
        {
            var first = ArrayArray[a];
            for (var b = 0; b < 配列長; b++)
            {
                var second = ArrayArray[b];
                first.SequenceEqual(second, EqualityComparer<int>.Default);
            }
        }
    }
    [TestMethod]
    public void EnumerableSequenceEqual1_0()
    {
        var A = new[] { 1, 2, 3, 4 };
        var B = new[] { 2, 3, 4, 1 };
        var C = new[] { 1, 2, 3 };
        C.SequenceEqual(A);
        A.SequenceEqual(C);
        A.SequenceEqual(A);
        A.SequenceEqual(B);
        B.SequenceEqual(A);
    }
    [TestMethod]
    public void EnumerableSequenceEqual1_1()
    {
        var A = new[] { 1, 2, 3, 4 };
        var B = new[] { 2, 3, 4, 1 };
        var C = new[] { 1, 2, 3 };
        EqualityComparer<int>.Default.Let(Default => C.SequenceEqual(A, Default));
        EqualityComparer<int>.Default.Let(Default => A.SequenceEqual(C, Default));
        EqualityComparer<int>.Default.Let(Default => A.SequenceEqual(A, Default));
        EqualityComparer<int>.Default.Let(Default => A.SequenceEqual(B, Default));
        EqualityComparer<int>.Default.Let(Default => B.SequenceEqual(A, Default));
    }
    [TestMethod]
    public void EnumerableSequenceEqual1_2()
    {
        var r = new Random(1);
        var ArrayArray = new int[配列長][];
        for (var a = 0; a < 配列長; a++)
        {
            var Array = new int[a];
            for (var b = 0; b < a; b++)
            {
                Array[b] = r.Next(a);
            }
            ArrayArray[a] = Array;
        }
        for (var a = 0; a < 配列長; a++)
        {
            var first = ArrayArray[a];
            for (var b = 0; b < 配列長; b++)
            {
                var second = ArrayArray[b];
                first.SequenceEqual(second);
            }
        }
    }
    [TestMethod]
    public void EnumerableSequenceEqual1_3()
    {
        var r = new Random(1);
        var ArrayArray = new int[配列長][];
        for (var a = 0; a < 配列長; a++)
        {
            var Array = new int[a];
            for (var b = 0; b < a; b++)
            {
                Array[b] = r.Next(a);
            }
            ArrayArray[a] = Array;
        }
        for (var a = 0; a < 配列長; a++)
        {
            var first = ArrayArray[a];
            for (var b = 0; b < 配列長; b++)
            {
                var second = ArrayArray[b];
                EqualityComparer<int>.Default.Let(Default => first.SequenceEqual(second, Default));
            }
        }
    }
    [TestMethod]
    public void EnumerableSequenceEqual5()
    {
        var A = new[] { 1, 2, 3, 4 };
        var B = new[] { 2, 3, 4, 1 };
        var C = new[] { 1, 2, 3 };
        var c = AnonymousComparer.Create((int a, int b) => a == b, a => a.GetHashCode());
        C.SequenceEqual(A, c);
        A.SequenceEqual(C, c);
        A.SequenceEqual(A, c);
        A.SequenceEqual(B, c);
        B.SequenceEqual(A, c);
    }
    [TestMethod]
    public void EnumerableSingle0()
    {
        var A = new[] { 1 };
        A.Single();
    }
    [TestMethod]
    public void EnumerableSingle1()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.SequenceEqual(A);
    }
    private static readonly int[] data = { 1, 2, 3, 4 };
    [TestMethod]
    public void EnumerableSumDouble()
    {
        var data2 = data.Select(p => (double)p).ToArray();
        data2.Sum(p => p);
    }
    [TestMethod]
    public void EnumerableSumDecimal()
    {
        var data2 = data.Select(p => (decimal)p).ToArray();
        data2.Sum(p => p);
    }
    [TestMethod]
    public void EnumerableSumInt32()
    {
        var data2 = data.Select(p => p).ToArray();
        data2.Sum(p => p);
    }
    [TestMethod]
    public void EnumerableSumInt64()
    {
        var data2 = data.Select(p => (long)p).ToArray();
        data2.Sum(p => p);
    }
    [TestMethod]
    public void EnumerableToArray0()
    {
        IEnumerable<int> A = new[] { 1, 2, 3, 4 };
        Assert.AreEqual(4, A.ToArray().Length);
    }
    [TestMethod]
    public void EnumerableToArray1()
    {
        IEnumerable<int> A = new[] { 1, 2, 3, 4 };
        Assert.AreEqual(4, A.Select(p => p + 1).ToArray().Length);
    }
    [TestMethod]
    public void EnumerableUnion0()
    {
        var A = new[] { 1, 1, 2, 2 };
        var B = new[] { 2, 2, 3, 3 };
        A.Union(B);
    }
    [TestMethod]
    public void EnumerableWhere0()
    {
        var A = new[] { 1, 2, 3, 4 };
        A.Where(p => p % 2 == 1);
    }
    private class Class1<T> : IEnumerable<T>
    {
        protected string 状態;
        public IEnumerator<T> GetEnumerator()
        {
            this.状態 = "public IEnumerator<T> Class1<T>.GetEnumerator()が実体Class1";
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            this.状態 = "IEnumerator IEnumerator.GetEnumerator()が実体Class1";
            yield break;
        }
        public override string ToString() => this.状態;
    }
    private class Class2<T> : Class1<T>, IEnumerable<T>
    {
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            this.状態 = "IEnumerator<T> IEnumerable<T>.GetEnumerator()が実体Class2";
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            this.状態 = "IEnumerator IEnumerator.GetEnumerator()が実体Class2";
            yield break;
        }
    }
    [TestMethod]
    public void インターフェースを継承しなおせるか()
    {
        {
            var c = new Class1<int>();
            using (var e = c.GetEnumerator())
            {
                e.MoveNext();
            }
            using (var e = ((IEnumerable<int>)c).GetEnumerator())
            {
                e.MoveNext();
            }
        }
        {
            var c = new Class2<int>();
            using (var e = c.GetEnumerator())
            {
                e.MoveNext();
            }
            using (var e = c.GetEnumerator())
            {
                e.MoveNext();
            }
            using (var e = ((IEnumerable<int>)c).GetEnumerator())
            {
                e.MoveNext();
            }
        }
    }
}