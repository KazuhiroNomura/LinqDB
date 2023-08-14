using LinqDB.Helpers;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Sets;

[TestClass]
public class Test_DecList
{
    private const int 要素数 = 80000;
    [TestMethod]
    public void Add()
    {
        var s = new DescList<int>();
        dynamic n = new NonPublicAccessor(s);
        for (var a = 0; a < 要素数; a++)
            n.Add(a);
        var index = 要素数;
        foreach(var a in s) {
            index--;
            Assert.AreEqual(a,index);
        }
        Assert.AreEqual(0,index);
    }
}