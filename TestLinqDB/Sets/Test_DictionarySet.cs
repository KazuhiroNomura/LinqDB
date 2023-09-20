using LinqDB.Sets;

namespace Sets;


public class Test_DictionarySet
{
    private const int 最小値 = 1, 最大値 = 10;
    [Fact]
    public void ContainsKey()
    {
        var d = new LookupSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        for (var b = 0; b < a; b++)
            d.AddKeyValue(a, b);
        Assert.False(d.ContainsKey(最小値 - 1));
        Assert.False(d.ContainsKey(11));
        for (var a = 最小値; a <= 最大値; a++)
            Assert.True(d.ContainsKey(a));
    }
    [Fact]
    public void Count()
    {
        var d = new LookupSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        {
            for (var b = 0; b < a; b++)
                d.AddKeyValue(a, b);
            Assert.Equal(a, d.GetTKeyValue(a).Count);
        }
    }
    //GetHashCode(U)
    //共通初期化()
    //Initialize(Int32)
    [Fact]
    public void ctor0()
    {
        // ReSharper disable once ObjectCreationAsStatement
        new LookupSet<int, int>();
    }
    [Fact]
    public void AddKeyValue()
    {
        var d = new LookupSet<int, int>();
        for (var a = 最小値; a <= 最大値; a++)
        for (var b = 0; b < a; b++)
            d.AddKeyValue(a, b);
    }
    //Insert
    [Fact]
    public void TryGetValue()
    {
        {
            var d = new LookupSet<int, int>();
            for (var a = 最小値; a <= 最大値; a++)
            for (var b = 0; b < a; b++)
                d.AddKeyValue(a, b);
            Set<int> value = default!;
            Assert.False(d.TryGetValue(0, ref value));
            Assert.False(d.ContainsKey(最小値 - 1));
            Assert.False(d.ContainsKey(11));
            for (var a = 最小値; a <= 最大値; a++)
                Assert.True(d.ContainsKey(a));
        }
        {
            var d = new LookupSet<int, int>();
            d.AddKeyValue(1, 2);
            d.AddKeyValue(2, 4);
            d.AddKeyValue(unchecked(1 + int.MaxValue), 3);
            //KeyValuePair<Int32,Int32>value;
            Set<int> value = default!;
            Assert.False(d.TryGetValue(0, ref value));
            Assert.True(d.TryGetValue(1, ref value));
            Assert.Equal(new Set<int> { 2 }, value);
            Assert.True(d.TryGetValue(2, ref value));
            Assert.Equal(new Set<int> { 4 }, value);
            Assert.True(d.TryGetValue(unchecked(1 + int.MaxValue), ref value));
            Assert.Equal(new Set<int> { 3 }, value);
        }
    }
}