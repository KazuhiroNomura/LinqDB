using System;
using System.Collections.Generic;
using LinqDB.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB.Helpers;

[TestClass]
public class Test_AnonymousComparer
{
    [TestMethod]
    public void ComparerCreate()
    {
        var EqualityComparer = AnonymousComparer.Create((double a, double b) => a.CompareTo(b));
        for (var a = 0d; a < 100d; a++)
        for (var b = 0d; b < 100d; b++)
            Assert.AreEqual(a.CompareTo(b), EqualityComparer.Compare(a, b));
    }
    [TestMethod, ExpectedException(typeof(NullReferenceException))]
    public void ComparerCreateException()
    {
        var EqualityComparer = AnonymousComparer.Create<double>(null!);
        for (var a = 0d; a < 100d; a++)
        for (var b = 0d; b < 100d; b++)
            Assert.AreEqual(a.CompareTo(b), EqualityComparer.Compare(a, b));
    }
    [TestMethod]
    public void EqualityComparerCreate()
    {
        var EqualityComparer = AnonymousComparer.Create((double a) => a.GetHashCode());
        for (var a = 0d; a < 100d; a += 0.1)
        {
            for (var b = 0d; b < 100d; b += 0.1)
            {
                Assert.AreEqual(a.GetHashCode(), EqualityComparer.GetHashCode(a));
                Assert.AreEqual(b.GetHashCode(), EqualityComparer.GetHashCode(b));
                Assert.AreEqual(a.Equals(b), b.Equals(a));
                Assert.AreEqual(a.GetHashCode(), EqualityComparer.GetHashCode(a));
                Assert.AreEqual(b.GetHashCode(), EqualityComparer.GetHashCode(b));
            }
        }
    }
    [TestMethod]
    public void HashCodeEqualityCreate()
    {
        var EqualityComparer = AnonymousComparer.Create((double a, double b) => a.Equals(b), a => a.GetHashCode());
        for (var a = 0d; a < 100d; a++)
        for (var b = 0d; b < 100d; b++)
            Assert.AreEqual(a.Equals(b), EqualityComparer.Equals(a, b));
    }
}