using LinqDB.Enumerables;
using LinqDB.Sets;
using LinqDB.Helpers;

namespace TestLinqDB.Sets;


public class Test_AscList
{
    private const int 要素数 = 80000;
    [Fact]
    public void Add()
    {
        var s = new AscList<int>();
        dynamic n = new NonPublicAccessor(s);
        for (var a = 0; a < 要素数; a++)
            n.Add(a);
        var Count = 0;
        foreach (var a in s)
        {
            Assert.Equal(a,Count);
            Count++;
        }
        Assert.Equal(Count, 要素数);
    }
}