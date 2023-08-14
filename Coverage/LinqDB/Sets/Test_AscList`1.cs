using LinqDB.Sets;
using LinqDB.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Sets;

[TestClass]
public class Test_AscList
{
    private const int 要素数 = 80000;
    [TestMethod]
    public void Add()
    {
        var s = new AscList<int>();
        dynamic n = new NonPublicAccessor(s);
        for (var a = 0; a < 要素数; a++)
            n.Add(a);
        var Count = 0;
        foreach (var a in s)
        {
            Assert.AreEqual(a,Count);
            Count++;
        }
        Assert.AreEqual(Count, 要素数);
    }
}