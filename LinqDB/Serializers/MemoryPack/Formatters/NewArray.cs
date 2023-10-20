using System;
using MemoryPack;
using System.Buffers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.NewArrayExpression;
public class NewArray:MemoryPackFormatter<T> {
    public static readonly NewArray Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value.Type.GetElementType());
        
        writer.WriteCollection(value.Expressions);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(value);

        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        writer.WriteNodeType(value);
        
        PrivateWrite(ref writer,value);
        
    }
    private static (Type type,Expressions.Expression?[]?expressions)PrivateRead(ref Reader reader){
        var type=reader.ReadType();
        
        var expressions=reader.ReadArray<Expressions.Expression>();
        return (type,expressions);
    }
    internal static T ReadNewArrayBounds(ref Reader reader){
        var (type,expressions)=PrivateRead(ref reader);
        return Expressions.Expression.NewArrayBounds(type,expressions!);
    }
    internal static T ReadNewArrayInit(ref Reader reader){
        var (type,expressions)=PrivateRead(ref reader);
        return Expressions.Expression.NewArrayInit(type,expressions!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        
        var NodeType=reader.ReadNodeType();
        System.Diagnostics.Debug.Assert(NodeType is Expressions.ExpressionType.NewArrayBounds or Expressions.ExpressionType.NewArrayInit);
        value=NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>ReadNewArrayBounds(ref reader),
            _                                        =>ReadNewArrayInit  (ref reader)
        };
        
        
    }
}
