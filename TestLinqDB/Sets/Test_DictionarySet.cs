using LinqDB.Sets;

namespace TestLinqDB.Sets;


public class Test_DictionarySet
{
    private const int 最小値 = 1, 最大値 = 10;
    [Fact]
    public void ContainsKey()
    {
        var d = new SetGroupingSet<int,int>();
        for (var a = 最小値; a <= 最大値; a++)
        for (var b = 0; b < a; b++)
            d.AddKeyValue(a, b);
        Assert.False(d.Contains(最小値 - 1));
        Assert.False(d.Contains(11));
        for (var a = 最小値; a <= 最大値; a++)
            Assert.True(d.Contains(a));
    }
    [Fact]
    public void Count()
    {
        var d = new SetGroupingSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        {
            for (var b = 0; b < a; b++)
                d.AddKeyValue(a, b);
            Assert.Equal(a, d[a].Count);
        }
    }
    //GetHashCode(U)
    //共通初期化()
    //Initialize(Int32)
    [Fact]
    public void ctor0()
    {
        // ReSharper disable once ObjectCreationAsStatement
        new SetGroupingSet<int, int>();
    }
    [Fact]
    public void AddKeyValue()
    {
        var d = new SetGroupingSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        for (var b = 0; b < a; b++)
            d.AddKeyValue(a, b);
    }
    //Insert
    [Fact]
    public void TryGetValue()
    {
        {
            var d = new SetGroupingSet<int, int>();
            for (var a = 最小値; a <= 最大値; a++)
            for (var b = 0; b < a; b++)
                d.AddKeyValue(a, b);
            LinqDB.Sets.IEnumerable<int> value = default!;
            Assert.Equal(0,d[0].Count);
            Assert.False(d.Contains(最小値 - 1));
            Assert.False(d.Contains(11));
            for (var a = 最小値; a <= 最大値; a++)
                Assert.True(d.Contains(a));
        }
        {
            var d = new SetGroupingSet<int, int>();
            d.AddKeyValue(1, 2);
            d.AddKeyValue(2, 4);
            d.AddKeyValue(unchecked(1 + int.MaxValue), 3);
            //KeyValuePair<Int32,Int32>value;
            LinqDB.Sets.IEnumerable<int> value = default!;
            Assert.Equal(0,d[0].Count);
            Assert.Equal(1,d[1].Count);
            Assert.Equal(new Set<int> { 2 }, d[1]);
            Assert.Equal(1,d[2].Count);
            Assert.Equal(new Set<int> { 4 }, d[2]);
            Assert.Equal(1,d[unchecked(1 + int.MaxValue)].Count);
            Assert.Equal(new Set<int> { 3 }, d[unchecked(1 + int.MaxValue)]);
        }
    }
}