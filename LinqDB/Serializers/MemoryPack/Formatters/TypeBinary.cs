using System;

using MemoryPack;
using System.Buffers;
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
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    private static (Expressions.Expression expression,Type type)PrivateRead(ref Reader reader){
        var expression=Expression.Read(ref reader);
        
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static T ReadTypeEqual(ref Reader reader){
        var (expression,type)=PrivateRead(ref reader);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static T ReadTypeIs(ref Reader reader){
        var (expression,type)=PrivateRead(ref reader);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        
        var NodeType=reader.ReadNodeType();
        System.Diagnostics.Debug.Assert(NodeType is Expressions.ExpressionType.TypeEqual or Expressions.ExpressionType.TypeIs);
        
        value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>ReadTypeEqual(ref reader),
            _                                   =>ReadTypeIs   (ref reader)
        };
        
        
    }
}
