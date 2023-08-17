using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class Test_ジェネリックEqualsの速度差
{
    private class TestClass
    {
        private readonly int a;
        public TestClass(int a) => this.a = a;
        public override bool Equals(object obj) => obj is TestClass o&&this.a==o.a;
        public override int GetHashCode() => this.a;
    }
    private class TestClassIEqutable : IEquatable<TestClassIEqutable>
    {
        private readonly int a;
        public TestClassIEqutable(int a) => this.a = a;
        public bool Equals(TestClassIEqutable? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return this.a == other.a;
        }
        public override bool Equals(object obj) => this.Equals((TestClassIEqutable)obj);
        public override int GetHashCode() => this.a;
    }
    private readonly struct TestStruct
    {
        private readonly int a;
        public TestStruct(int a) => this.a = a;
        public override bool Equals(object obj)
        {
            Contract.Assert(obj != null, "obj != null");
            var o = (TestStruct)obj;
            return this.a == o.a;
        }
        public override int GetHashCode() => this.a;
    }
    private readonly struct TestStructIEqutable : IEquatable<TestStructIEqutable>
    {
        private readonly int a;
        public TestStructIEqutable(int a) => this.a = a;
        public bool Equals(TestStructIEqutable other) => this.a == other.a;
        public override bool Equals(object obj)
        {
            Contract.Assert(obj != null, "obj != null");
            return this.Equals((TestStructIEqutable)obj);
        }
        public override int GetHashCode() => this.a;
    }
    private const int 回数 = 1000;
    [TestMethod]
    public void Class比較()
    {
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestClass(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestClass(b0);
                if (a0 == b0)
                    Assert.IsTrue(a.Equals(b));
                else
                    Assert.IsFalse(a.Equals(b));
            }
        }
    }
    [TestMethod]
    public void ClassIEqutable比較()
    {
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestClassIEqutable(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestClassIEqutable(b0);
                if (a0 == b0)
                    Assert.IsTrue(a.Equals(b));
                else
                    Assert.IsFalse(a.Equals(b));
            }
        }
    }
    [TestMethod]
    public void Struct比較()
    {
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestStruct(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestStruct(b0);
                if (a0 == b0)
                    Assert.IsTrue(a.Equals(b));
                else
                    Assert.IsFalse(a.Equals(b));
            }
        }
    }
    [TestMethod]
    public void StructIEqutable比較()
    {
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestStructIEqutable(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestStructIEqutable(b0);
                if (a0 == b0)
                    Assert.IsTrue(a.Equals(b));
                else
                    Assert.IsFalse(a.Equals(b));
            }
        }
    }
    [TestMethod]
    public void ClassDefaultで比較()
    {
        var e = EqualityComparer<TestClass>.Default;
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestClass(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestClass(b0);
                if (a0 == b0)
                    Assert.IsTrue(e.Equals(a, b));
                else
                    Assert.IsFalse(e.Equals(a, b));
            }
        }
    }
    [TestMethod]
    public void ClassIEqutableDefaultで比較()
    {
        var e = EqualityComparer<TestClassIEqutable>.Default;
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestClassIEqutable(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestClassIEqutable(b0);
                if (a0 == b0)
                    Assert.IsTrue(e.Equals(a, b));
                else
                    Assert.IsFalse(e.Equals(a, b));
            }
        }
    }
    [TestMethod]
    public void StructDefaultで比較()
    {
        var e = EqualityComparer<TestStruct>.Default;
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestStruct(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestStruct(b0);
                if (a0 == b0)
                    Assert.IsTrue(e.Equals(a, b));
                else
                    Assert.IsFalse(e.Equals(a, b));
            }
        }
    }
    [TestMethod]
    public void StructIEqutableDefaultで比較()
    {
        var e = EqualityComparer<TestStructIEqutable>.Default;
        for (var a0 = 0; a0 < 回数; a0++)
        {
            var a = new TestStructIEqutable(a0);
            for (var b0 = 0; b0 < 回数; b0++)
            {
                var b = new TestStructIEqutable(b0);
                if (a0 == b0)
                    Assert.IsTrue(e.Equals(a, b));
                else
                    Assert.IsFalse(e.Equals(a, b));
            }
        }
    }
}