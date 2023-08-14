using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Sets;

[TestClass]
public class Test_DictionarySet
{
    private const int 最小値 = 1, 最大値 = 10;
    [TestMethod]
    public void ContainsKey()
    {
        var d = new LookupSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        for (var b = 0; b < a; b++)
            d.AddKeyValue(a, b);
        Assert.IsFalse(d.ContainsKey(最小値 - 1));
        Assert.IsFalse(d.ContainsKey(11));
        for (var a = 最小値; a <= 最大値; a++)
            Assert.IsTrue(d.ContainsKey(a));
    }
    [TestMethod]
    public void Count()
    {
        var d = new LookupSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        {
            for (var b = 0; b < a; b++)
                d.AddKeyValue(a, b);
            Assert.AreEqual(a, d.GetTKeyValue(a).Count);
        }
    }
    //GetHashCode(U)
    //共通初期化()
    //Initialize(Int32)
    [TestMethod]
    public void ctor0()
    {
        // ReSharper disable once ObjectCreationAsStatement
        new LookupSet<int, int>();
    }
    [TestMethod]
    public void AddKeyValue()
    {
        var d = new LookupSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        for (var b = 0; b < a; b++)
            d.AddKeyValue(a, b);
    }
    //Insert
    [TestMethod]
    public void TryGetValue()
    {
        {
            var d = new LookupSet<int, int>();
            for (var a = 最小値; a <= 最大値; a++)
            for (var b = 0; b < a; b++)
                d.AddKeyValue(a, b);
            Set<int> value = default!;
            Assert.IsFalse(d.TryGetValue(0, ref value));
            Assert.IsFalse(d.ContainsKey(最小値 - 1));
            Assert.IsFalse(d.ContainsKey(11));
            for (var a = 最小値; a <= 最大値; a++)
                Assert.IsTrue(d.ContainsKey(a));
        }
        {
            var d = new LookupSet<int, int>();
            d.AddKeyValue(1, 2);
            d.AddKeyValue(2, 4);
            d.AddKeyValue(unchecked(1 + int.MaxValue), 3);
            //KeyValuePair<Int32,Int32>value;
            Set<int> value = default!;
            Assert.IsFalse(d.TryGetValue(0, ref value));
            Assert.IsTrue(d.TryGetValue(1, ref value));
            Assert.AreEqual(new Set<int> { 2 }, value);
            Assert.IsTrue(d.TryGetValue(2, ref value));
            Assert.AreEqual(new Set<int> { 4 }, value);
            Assert.IsTrue(d.TryGetValue(unchecked(1 + int.MaxValue), ref value));
            Assert.AreEqual(new Set<int> { 3 }, value);
        }
    }
}