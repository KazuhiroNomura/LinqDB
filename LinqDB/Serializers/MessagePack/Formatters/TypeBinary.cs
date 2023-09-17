using System;
using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using static Extension;
public class TypeBinary:IMessagePackFormatter<Expressions.TypeBinaryExpression>{
    public static readonly TypeBinary Instance=new();
    private static void PrivateWrite(ref Writer writer,Expressions.TypeBinaryExpression value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        Expression.Write(ref writer,value.Expression,Resolver);
        writer.WriteType(value.TypeOperand);
    }
    internal static void Write(ref Writer writer,Expressions.TypeBinaryExpression value,
        MessagePackSerializerOptions Resolver){
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,Expressions.TypeBinaryExpression? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value))return;
        
        
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    private static (Expressions.Expression expression,System.Type type)PrivateRead(ref Reader reader,MessagePackSerializerOptions Resolver){
        var expression=Expression.Read(ref reader,Resolver);
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static Expressions.TypeBinaryExpression ReadTypeEqual(ref Reader reader,MessagePackSerializerOptions Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static Expressions.TypeBinaryExpression ReadTypeIs(ref Reader reader,MessagePackSerializerOptions Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public Expressions.TypeBinaryExpression Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==3);
        var NodeType=reader.ReadNodeType();
        return NodeType switch{
            Expressions.ExpressionType.TypeEqual=>ReadTypeEqual(ref reader,Resolver),
            Expressions.ExpressionType.TypeIs=>ReadTypeIs(ref reader,Resolver),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
