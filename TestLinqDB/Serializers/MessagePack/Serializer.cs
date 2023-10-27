//using System.Linq.Expressions;
using MessagePack;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.MessagePack;
public class Serializer:共通{
    [Fact]
    public void ctor(){
        new LinqDB.Serializers.MemoryPack.Serializer();
    }
    [Fact]
    public void Clear(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        Serializer.Serialize("abc");
    }
    [Fact]
    public void Serialize(){
        Assert.Throws<NotImplementedException>(()=>{
            var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
            var writer=new MessagePackWriter();
            Serializer.Serialize(ref writer,Serializer,MessagePackSerializerOptions.Standard);
        });
    }
    [Fact]
    public void Deserialize(){
        Assert.Throws<NotImplementedException>(()=>{
            var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
            ReadOnlyMemory<byte> buffer=new byte[100];
            var reader=new MessagePackReader(buffer);
            Serializer.Deserialize(ref reader,MessagePackSerializerOptions.Standard);
        });
    }
    [Fact]
    public void DeserializeTbyte(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        var expected="abc";
        var bytes=Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<string>(bytes);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void DeserializeTStream(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        var expected="abc";
        var memorystream=new MemoryStream();
        Serializer.Serialize(memorystream,expected);
        memorystream.Position=0;
        var actual=Serializer.Deserialize<string>(memorystream);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void DeserializeSerialize(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        var expected=new{a="def"};
        var bytes=Serializer.Serialize<object>(expected);
        var actual=Serializer.Deserialize<object>(bytes);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void SerializeTStream(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        var expected="abc";
        var memorystream=new MemoryStream();
        Serializer.Serialize(memorystream,expected);
        memorystream.Position=0;
        var actual=Serializer.Deserialize<string>(memorystream);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void SerializeTbyte(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        var expected="abc";
        var bytes=Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<string>(bytes);
        Assert.Equal(expected,actual);
    }
}
