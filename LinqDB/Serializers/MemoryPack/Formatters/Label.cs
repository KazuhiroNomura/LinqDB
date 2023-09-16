using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.LabelExpression;


public class Label:MemoryPackFormatter<T> {
    public static readonly Label Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        LabelTarget.Serialize(ref writer,value!.Target);
        Expression.SerializeNullable(ref writer,value.DefaultValue);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Label);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader){
        var target= LabelTarget.DeserializeLabelTarget(ref reader);
        var defaultValue= Expression.InternalDeserializeNullable(ref reader);
        return Expressions.Expression.Label(target,defaultValue);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternalDeserialize(ref reader);
    }
}
