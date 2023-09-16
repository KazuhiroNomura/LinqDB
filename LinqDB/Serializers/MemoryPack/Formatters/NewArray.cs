using System;
using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.NewArrayExpression;
using static Extension;
public class NewArray:MemoryPackFormatter<T> {
    public static readonly NewArray Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(value!.NodeType);
        writer.WriteType(value!.Type.GetElementType());
        writer.SerializeReadOnlyCollection(value.Expressions);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        InternalSerialize(ref writer,value);
    }
    private static (System.Type type,Expressions.Expression?[]?expressions)PrivateDeserialize(ref Reader reader){
        var type=reader.ReadType();
        var expressions=reader.ReadArray<Expressions.Expression>();
        return (type,expressions);
    }
    internal static T InternalDeserializeNewArrayBounds(ref Reader reader){
        var (type,expressions)=PrivateDeserialize(ref reader);
        return Expressions.Expression.NewArrayBounds(type,expressions!);
    }
    internal static T InternalDeserializeNewArrayInit(ref Reader reader){
        var (type,expressions)=PrivateDeserialize(ref reader);
        return Expressions.Expression.NewArrayInit(type,expressions!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var NodeType=reader.ReadNodeType();
        value=NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>InternalDeserializeNewArrayBounds(ref reader),
            Expressions.ExpressionType.NewArrayInit=>InternalDeserializeNewArrayInit(ref reader),
            _=>throw new NotImplementedException(NodeType.ToString())
        };
    }
}
