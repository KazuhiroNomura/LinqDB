using System.Collections;
using System.Reflection;
using LinqDB.Optimizers.Comparison;
using LinqDB.Sets;
using MemoryPack;

using IEnumerable = System.Collections.IEnumerable;
namespace TestLinqDB.特殊パターン;
internal class ClassIEnumerableInt32Double : System.Collections.Generic.IEnumerable<int>, System.Collections.Generic.IEnumerable<double>
{
    public IEnumerator<int> GetEnumerator()
    {
        for (var a = 0; a<10; a++) yield return a;
    }
    IEnumerator<double> System.Collections.Generic.IEnumerable<double>.GetEnumerator()
    {
        for (var a = 0; a<10; a++) yield return a;
    }
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
internal class ClassIEnumerableInt32 : System.Collections.Generic.IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        for (var a = 0; a<10; a++) yield return a;
    }
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
public class 特定パターン : 共通
{
    [Fact]
    public void ClassIEnumerableInt32シリアライズ()
    {
        System.Collections.Generic.IEnumerable<int> input = new ClassIEnumerableInt32();
        this.AssertEqual(input);
    }
    [Fact]
    public void ClassIEnumerableInt32Doubleシリアライズ()
    {
        System.Collections.Generic.IEnumerable<int> input = new ClassIEnumerableInt32Double();
        this.AssertEqual(input);
    }
    [Fact]
    public void IEnumerableInt32シリアライズ()
    {
        System.Collections.Generic.IEnumerable<int> input = new[] { 1, 2, 3 };
        //現状
        //ListをIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //今後
        //ListをIEnumerableでシリアライズするとIEnumerableの内部でListでデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        this.AssertEqual(input);
    }
    [Fact]
    public void UnionBy結果シリアライズ()
    {
        var a = new[] { 3, 5, 7 };
        var b = new[] { 4, 6, 8 };
        var input = a.UnionBy(b, x => x+1);
        var g = input.GetType().GetMethod("GetEnumerator", BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
        this.AssertEqual(input);
    }
    [Fact]
    public void IEnumerableAnonymous()
    {
        var st = (new[] { 3, 5, 7 }).ToSet();
        this.Expression実行AssertEqual(() => st.Select(o => new { o }));
    }
    [Fact]
    public void CharArray()
    {
        var Serializer = this.MemoryPack;
        {
            var expected = new[] { 'a', 'b', 'c' };
            var bytes = Serializer.Serialize<object>(expected);
            var bytes0 = new byte[bytes.Length+1];
            Array.Copy(bytes, bytes0, bytes.Length);
            var output = Serializer.Deserialize<object>(bytes0);
            Assert.Equal(expected, output!, new 汎用Comparer());
        }
        {
            var expected = 'A';
            //var bytes = Serializer.Serialize(expected);
            //var output = Serializer.Deserialize<char>(bytes);
            //Assert.Equal(expected,output!,new 汎用Comparer());
            SerializeDeserialize<object>(this.MemoryPack, expected);
        }
        {
            var input = new[] { 'a', 'b', 'c' };
            var m = new MemoryStream();
            Serializer.Serialize(m, input);
            m.Position=0;
            var output = Serializer.Deserialize<char[]>(m);
            Assert.Equal(input, output!, new 汎用Comparer());
        }
        {
            var expected = new[] { 'a', 'b', 'c' };
            //LinqDB.Serializers.MemoryPack.CharArrayFormatter.Instance.Serialize();
            //var bytes=Serializer.Serialize<object>(expected);
            //var output=Serializer.Deserialize<object>(bytes);
            //Assert.Equal(expected,output!,new 汎用Comparer());
            SerializeDeserialize<object>(this.MemoryPack, expected);
        }
    }
    [Fact]
    public void MemoryPackでCharArray()
    {
        MemoryPackFormatterProvider.Register(new MemoryPack.Formatters.ArrayFormatter<char>());
        var input = new[] { 'a', 'b', 'c' };
        var bytes = MemoryPackSerializer.Serialize(input);
        var output = MemoryPackSerializer.Deserialize<char[]>(bytes);
        Assert.True(input.SequenceEqual(output));
    }
}
