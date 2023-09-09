using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Common;
using T=Expressions.SwitchExpression;
public class Switch:MemoryPackFormatter<T> {
    public static readonly Switch Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        this.Serialize(ref writer,ref value);
    internal T DeserializeSwitch(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Type.Instance.Serialize(ref writer,value!.Type);
        Expression.Instance.Serialize(ref writer,value.SwitchValue);
        Method.Instance.SerializeNullable(ref writer,value.Comparison);
        SerializeReadOnlyCollection(ref writer,value.Cases);
        Expression.Instance.Serialize(ref writer,value.DefaultBody);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var type       = Type.Instance.Deserialize(ref reader);
        var switchValue= Expression.Instance.Deserialize(ref reader);
        var comparison = Method.Instance.DeserializeNullable(ref reader);
        var cases      =reader.ReadArray<Expressions.SwitchCase>();
        var defaultBody= Expression.Instance.Deserialize(ref reader);
        value=Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases!);
    }
}
