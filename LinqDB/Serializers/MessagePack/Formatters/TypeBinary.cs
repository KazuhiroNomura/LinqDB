using System;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.TypeBinaryExpression;
public class TypeBinary:IMessagePackFormatter<T> {
    public static readonly TypeBinary Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value);
        
        Expression.Write(ref writer,value.Expression,Resolver);
        
        writer.WriteType(value.TypeOperand);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value,Resolver);
    }
    private static (Expressions.Expression expression,Type type)PrivateRead(ref Reader reader,O Resolver){
        var expression=Expression.Read(ref reader,Resolver);
        
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static T ReadTypeEqual(ref Reader reader,O Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static T ReadTypeIs(ref Reader reader,O Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        System.Diagnostics.Debug.Assert(count==3);
        var NodeType=reader.ReadNodeType();
        System.Diagnostics.Debug.Assert(NodeType is Expressions.ExpressionType.TypeEqual or Expressions.ExpressionType.TypeIs);
        
        return NodeType switch{
            Expressions.ExpressionType.TypeEqual=>ReadTypeEqual(ref reader,Resolver),
            _=>ReadTypeIs(ref reader,Resolver)
        };
        
        
    }
}
