using System;
using System.Buffers;
using MemoryPack;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.TypeBinaryExpression;
public class TypeBinary:MemoryPackFormatter<T> {
    public static readonly TypeBinary Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(value);
        Expression.Write(ref writer,value.Expression);
        writer.WriteType(value.TypeOperand);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Write(ref writer,value);
    }
    private static (Expressions.Expression expression,System.Type type)PrivateDeserialize(ref Reader reader){
        var expression=Expression.Read(ref reader);
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static T ReadTypeEqual(ref Reader reader){
        var (expression,type)=PrivateDeserialize(ref reader);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static T ReadTypeIs(ref Reader reader){
        var (expression,type)=PrivateDeserialize(ref reader);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var NodeType=reader.ReadNodeType();
        value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>ReadTypeEqual(ref reader),
            Expressions.ExpressionType.TypeIs=>ReadTypeIs(ref reader),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
