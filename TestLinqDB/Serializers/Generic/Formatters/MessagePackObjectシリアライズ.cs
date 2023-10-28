using MemoryPack;
using MessagePack;
namespace TestLinqDB.Serializers.Generic.Formatters;

[Serializable,MessagePackObject(true),MemoryPackable]
public partial record シリアライズ対象(
    int @int,
    string @string
);
public abstract class MessagePackObjectシリアライズ<TSerializer> : 共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected MessagePackObjectシリアライズ():base(new AssertDefinition(new TSerializer())){}
    [Fact]public void Test0(){
        var value=new シリアライズ対象(1,"s");
        //var FormatterType=typeof(Anonymous<>).MakeGenericType(value.GetType());
        //dynamic formatter=Activator.CreateInstance(FormatterType)!;
        //MemoryPackFormatterProvider.Register(formatter);
        this.MemoryMessageJson_T_Assert全パターン(value);
    }
}
