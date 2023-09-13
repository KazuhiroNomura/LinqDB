using MemoryPack;
using Expressions=System.Linq.Expressions;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.LabelExpression;
using C=Serializer;


public class Label:MemoryPackFormatter<T> {
    public static readonly Label Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        Instance.Serialize(ref writer,ref value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        LabelTarget.Serialize(ref writer,value!.Target);
        Expression.SerializeNullable(ref writer,value.DefaultValue);
    }
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var target= LabelTarget.DeserializeLabelTarget(ref reader);
        var defaultValue= Expression.InternalDeserializeNullable(ref reader);
        value=Expressions.Expression.Label(target,defaultValue);
    }
}
