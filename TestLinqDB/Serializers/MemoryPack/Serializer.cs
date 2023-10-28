//using System.Linq.Expressions;

//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MemoryPack;
namespace TestLinqDB.Serializers.MemoryPack;
public class Serializer:共通{
    [Fact]
    public void ctor(){
        new LinqDB.Serializers.MemoryPack.Serializer();
    }
    [Fact]public void GetService(){
        Assert.Throws<NotImplementedException>(()=>{
            var Serializer=new LinqDB.Serializers.MemoryPack.Serializer();
            Serializer.GetService(typeof(void));
        });
    }
    [Fact]public void Clear(){
        var Serializer=new LinqDB.Serializers.MemoryPack.Serializer();
        Serializer.Serialize("abc");
    }
    [Fact]
    public void SerializeDeserializeTStream(){
        var Serializer=new LinqDB.Serializers.MemoryPack.Serializer();
        var stream=new MemoryStream();
        var expected=1m;
        Serializer.Serialize(stream,expected);
        stream.Position=0;
        var actual=Serializer.Deserialize<decimal>(stream);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void SerializeDeserializeTBytes(){
        var Serializer=new LinqDB.Serializers.MemoryPack.Serializer();
        var expected=1m;
        var bytes=Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<decimal>(bytes);
        Assert.Equal(expected, actual);
    }
}
