//using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;

using Serializers.MessagePack.Formatters;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace Serializers.MessagePack;
public class FormatterResolver:共通 {
    [Fact]public void ctor(){
        new LinqDB.Serializers.MemoryPack.Serializer();
    }
    [Fact]public void GetFormatter(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        //    if(type.IsDisplay()){
        Serializer.Serialize(ClassDisplay取得());
        //    }
        Assert.Throws<global::MessagePack.MessagePackSerializationException>(()=>Serializer.Serialize(this));
        //}
    }
    [Fact]public void DeserializeTbyte(){
        var Serializer=new LinqDB.Serializers.MessagePack.Serializer();
        var expected="abc";
        var bytes=Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<string>(bytes);
        Assert.Equal(expected, actual);
    }
}
