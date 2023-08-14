using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using static BackendClient.ExtendAggregate;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class ベンチマーク:ATest {
    private readonly int[] OrderByテスト用array = new int[OrderBy要素数];
    //各テストを実行する前にコードを実行するには、TestInitialize を使用
    [TestInitialize]
    public override void MyTestInitialize() {
        var r = new Random(1);
        for(var b = 0;b<OrderBy要素数;b++) {
            this.OrderByテスト用array[b]=r.Next(100000);
        }
    }
    private const int OrderBy要素数 = 80000;
    [TestMethod]
    public void 標準OrderBy_ThenBy0速度() {
        this.OrderByテスト用array.OrderBy(p => p%100000/10000).ThenBy(p => p%10000/1000).ThenBy(p => p%1000/100).ThenBy(p => p%100/10).ThenBy(p => p%10/1).ToArray();
    }
    [TestMethod]
    public void 最適化OrderBy_ThenBy0速度() {
        this.Execute2(() => this.OrderByテスト用array.OrderBy(p => p%100000/10000).ThenBy(p => p%10000/1000).ThenBy(p => p%1000/100).ThenBy(p => p%100/10).ThenBy(p => p%10/1));
    }
    [TestMethod]
    public void 最適化OrderBy_ThenBy0_ToArray速度() {
        this.Execute2(() => this.OrderByテスト用array.OrderBy(p => p%100000/10000).ThenBy(p => p%10000/1000).ThenBy(p => p%1000/100).ThenBy(p => p%100/10).ThenBy(p => p%10/1).ToArray());
    }
    private const int Equalsの速度 = 100000000;
    private static bool IEquatableの速度<T>(T a,T b) where T : IEquatable<T> {
        var c = true;
        for(var d = 0;d<Equalsの速度;d++) {
            c&=a.Equals(b);
        }
        return c;
    }
    private static bool EqualityComparerの速度<T>(T a,T b) {
        var Default = EqualityComparer<T>.Default;
        var c = true;
        for(var d = 0;d<Equalsの速度;d++) {
            c&=Default.Equals(a,b);
        }
        return c;
    }
    [TestMethod]
    public void IEquatableの速度() {
        var s = Stopwatch.StartNew();
        IEquatableの速度(1,2);
        Trace.WriteLine("IEquatableの速度"+s.ElapsedMilliseconds+"ms");
    }
    [TestMethod]
    public void EqualityComparerの速度() {
        var s = Stopwatch.StartNew();
        EqualityComparerの速度(1,2);
        Trace.WriteLine("EqualityComparerの速度"+s.ElapsedMilliseconds+"ms");
    }
    [TestMethod]
    public void SetとReadOnlySet() {
        const int ArrayLength = ReadOnlyMultiSequence<int>.ArrayLength;
        var 要素数Array = new int[] { 1,2,3,ArrayLength-2,ArrayLength-1,ArrayLength,ArrayLength+1,ArrayLength+2,ArrayLength*3-2,ArrayLength*3-1,ArrayLength*3,ArrayLength*3+1,ArrayLength*3+2 };
        //var 要素数Array = new Int32[] { ArrayLength};
        foreach(var 要素数 in 要素数Array) {
            var Set = new Set<int>();
            for(var a = 0;a<要素数;a++) {
                Set.Add(a);
            }
            {
                var Setの速度 = Stopwatch.StartNew();
                foreach(var a in Set) {
                }
                Trace.WriteLine($"{nameof(Setの速度)}{Setの速度.ElapsedMilliseconds}ms");
                var Count = 0;
                foreach(var a in Set) {
                    Count++;
                }
                Assert.AreEqual(要素数,Count);
            }
            {
                var ReadOnlySet = new ReadOnlyMultiSequence<int>(Set);
                var ReadOnlySetの速度 = Stopwatch.StartNew();
                foreach(var a in ReadOnlySet) {
                }
                Trace.WriteLine($"{nameof(ReadOnlySetの速度)}{ReadOnlySetの速度.ElapsedMilliseconds}ms");
                var Count = 0;
                foreach(var a in ReadOnlySet) {
                    Count++;
                }
                Assert.AreEqual(要素数,Count);
            }
            {
                var ReadOnlySequence = new ReadOnlySingleSequence<int>(Set);
                var ReadOnlySequenceの速度 = Stopwatch.StartNew();
                foreach(var a in ReadOnlySequence) {
                }
                Trace.WriteLine($"{nameof(ReadOnlySequenceの速度)}{ReadOnlySequenceの速度.ElapsedMilliseconds}ms");
                var Count = 0;
                foreach(var a in ReadOnlySequence) {
                    Count++;
                }
                Assert.AreEqual(要素数,Count);
            }
        }
    }
    [TestMethod]
    public void SetとReadOnlySetの速度() {
        const int 要素数 = 10000000;
        var Set = new Set<int>();
        for(var a = 0;a<要素数;a++) {
            Set.Add(a);
        }
        {
            var Setの速度 = Stopwatch.StartNew();
            foreach(var a in Set) {
            }
            Trace.WriteLine($"{nameof(Setの速度)}{Setの速度.ElapsedMilliseconds}ms");
            var Count = 0;
            foreach(var a in Set) {
                Count++;
            }
            Assert.AreEqual(要素数,Count);
        }
        {
            var ReadOnlySet = new ReadOnlyMultiSequence<int>(Set);
            var ReadOnlySetの速度 = Stopwatch.StartNew();
            foreach(var a in ReadOnlySet) {
            }
            Trace.WriteLine($"{nameof(ReadOnlySetの速度)}{ReadOnlySetの速度.ElapsedMilliseconds}ms");
            var Count = 0;
            foreach(var a in ReadOnlySet) {
                Count++;
            }
            Assert.AreEqual(要素数,Count);
        }
        {
            var ReadOnlySequence = new ReadOnlySingleSequence<int>(Set);
            var ReadOnlySequenceの速度 = Stopwatch.StartNew();
            foreach(var a in ReadOnlySequence) {
            }
            Trace.WriteLine($"{nameof(ReadOnlySequenceの速度)}{ReadOnlySequenceの速度.ElapsedMilliseconds}ms");
            var Count = 0;
            foreach(var a in ReadOnlySequence) {
                Count++;
            }
            Assert.AreEqual(要素数,Count);
        }
    }
    [TestMethod]
    public void SetをReadOnlySetを使って高速化() {
        const int 最大回数 = 10;
        const int 要素数 = 10000000;
        Trace.WriteLine($"{nameof(要素数)}{要素数}");
        var Set = new Set<int>();
        for(var a = 0;a<要素数;a++) {
            Set.Add(a);
        }
        {
            var Setの速度 = Stopwatch.StartNew();
            foreach(var a in Set) {
            }
            Trace.WriteLine($"    {nameof(Setの速度)}{Setの速度.ElapsedMilliseconds}ms");
        }
        for(var 回数 = 1;回数<最大回数;回数++){
            var ReadOnlySetの速度 = Stopwatch.StartNew();
            var ReadOnlySet = new ReadOnlyMultiSequence<int>(Set);
            for(var b = 0;b<回数;b++) {
                foreach(var a in ReadOnlySet) {
                }
            }
            Trace.WriteLine($"    {回数}回{nameof(ReadOnlySetの速度)}{ReadOnlySetの速度.ElapsedMilliseconds/回数}ms");
        }
        for(var 回数 = 1;回数<最大回数;回数++) {
            var ReadOnlySequenceの速度 = Stopwatch.StartNew();
            var ReadOnlySequence = new ReadOnlySingleSequence<int>(Set);
            for(var b = 0;b<回数;b++) {
                foreach(var a in ReadOnlySequence) {
                }
            }
            Trace.WriteLine($"    {回数}回{nameof(ReadOnlySequenceの速度)}{ReadOnlySequenceの速度.ElapsedMilliseconds/回数}ms");
        }
    }
}