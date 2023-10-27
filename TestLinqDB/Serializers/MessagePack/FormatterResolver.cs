//using System.Linq.Expressions;
using MessagePack;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.MessagePack;
public class FormatterResolver : 共通
{
    [Fact]
    public void ctor()
    {
        new LinqDB.Serializers.MemoryPack.Serializer();
    }
    [Fact]
    public void GetFormatter()
    {
        var Serializer = new LinqDB.Serializers.MessagePack.Serializer();
        //    if(type.IsDisplay()){
        Serializer.Serialize(ClassDisplay取得());
        //    }
        var ex = Assert.Throws<MessagePackSerializationException>(() => Serializer.Serialize(this));
        Assert.True(ex.InnerException is FormatterNotRegisteredException);
        //}
    }
    [Fact]
    public void DeserializeTbyte()
    {
        var Serializer = new LinqDB.Serializers.MessagePack.Serializer();
        var expected = "abc";
        var bytes = Serializer.Serialize(expected);
        var actual = Serializer.Deserialize<string>(bytes);
        Assert.Equal(expected, actual);
    }
}
