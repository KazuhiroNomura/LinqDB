using Utf8Json;
namespace TestLinqDB.Serializers.Utf8Json;
public class Serializer:共通{
    [Fact]
    public void ctor(){
        _=new LinqDB.Serializers.Utf8Json.Serializer();
    }
    [Fact]
    public void Clear(){
        var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
        Serializer.Serialize("abc");
    }
    [Fact]
    public void SerializeDeserialize(){
        Assert.Throws<NotImplementedException>(()=>{
            var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
            var buffer=new byte[1024];
            var writer=new JsonWriter(buffer);
            Serializer.Serialize(ref writer,Serializer,global::Utf8Json.Resolvers.StandardResolver.Default);
        });
    }
    [Fact]
    public void Deserialize(){
        Assert.Throws<NotImplementedException>(()=>{
            var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
            var buffer=new byte[1024];
            var reader=new JsonReader(buffer);
            Serializer.Deserialize(ref reader,global::Utf8Json.Resolvers.StandardResolver.Default);
        });
    }
    [Fact]
    public void DeserializeTbyte(){
        var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
        var expected="abc";
        var bytes=Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<string>(bytes);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void DeserializeTStream(){
        var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
        var expected="abc";
        var memorystream=new MemoryStream();
        Serializer.Serialize(memorystream,expected);
        memorystream.Position=0;
        var actual=Serializer.Deserialize<string>(memorystream);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void DeserializeSerialize(){
        var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
        var expected=new{a="def"};
        var bytes=Serializer.Serialize<object>(expected);
        var actual=Serializer.Deserialize<object>(bytes);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void SerializeTStream(){
        var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
        var expected="abc";
        var memorystream=new MemoryStream();
        Serializer.Serialize(memorystream,expected);
        memorystream.Position=0;
        var actual=Serializer.Deserialize<string>(memorystream);
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void SerializeTbyte(){
        var Serializer=new LinqDB.Serializers.Utf8Json.Serializer();
        var expected="abc";
        var bytes=Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<string>(bytes);
        Assert.Equal(expected,actual);
    }
}
