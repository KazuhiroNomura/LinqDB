using MemoryPack;
using MessagePack;
namespace Serializers.MessagePack.Formatters;

[Serializable,MessagePackObject(true),MemoryPackable]
public partial record シリアライズ対象(
    int @int,
    string @string
);
public class 自然なシリアライズ:共通{
    [Fact]public void Test0(){
        var value=new シリアライズ対象(1,"s");
        //var FormatterType=typeof(Anonymous<>).MakeGenericType(value.GetType());
        //dynamic formatter=Activator.CreateInstance(FormatterType)!;
        //MemoryPackFormatterProvider.Register(formatter);
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(value);
    }
}
