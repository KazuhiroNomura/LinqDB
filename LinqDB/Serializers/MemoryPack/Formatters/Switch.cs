using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Extension;
using T=Expressions.SwitchExpression;
public class Switch:MemoryPackFormatter<T> {
    public static readonly Switch Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeSwitch(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Type.Serialize(ref writer,value!.Type);
        Expression.Serialize(ref writer,value.SwitchValue);
        Method.SerializeNullable(ref writer,value.Comparison);
        writer.SerializeReadOnlyCollection(value.Cases);
        Expression.SerializeNullable(ref writer,value.DefaultBody);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var type       = Type.Deserialize(ref reader);
        var switchValue= Expression.Deserialize(ref reader);
        var comparison = Method.DeserializeNullable(ref reader);
        var cases      =reader.ReadArray<Expressions.SwitchCase>();
        var defaultBody= Expression.DeserializeNullable(ref reader);
        value=Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases!);
    }
}
