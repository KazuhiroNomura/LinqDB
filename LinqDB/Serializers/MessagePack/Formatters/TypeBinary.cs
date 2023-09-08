using System;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.UnaryExpression;
using static Common;
public class TypeBinary:IMessagePackFormatter<Expressions.TypeBinaryExpression>{
    public static readonly TypeBinary Instance=new();
    internal static void InternalSerializeExpression(ref Writer writer,Expressions.TypeBinaryExpression value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
    }
    private static (Expressions.Expression expression,System.Type type)PrivateDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var expression=Expression.Instance.Deserialize(ref reader,Resolver);
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static Expressions.TypeBinaryExpression InternalDeserializeTypeEqual(ref Reader reader,MessagePackSerializerOptions Resolver){
        var (expression,type)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static Expressions.TypeBinaryExpression InternalDeserializeTypeIs(ref Reader reader,MessagePackSerializerOptions Resolver){
        var (expression,type)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.TypeIs(expression,type);
    }
    private const int ArrayHeader=3;
    public void Serialize(ref Writer writer,Expressions.TypeBinaryExpression? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        writer.WriteNodeType(value!.NodeType);
        InternalSerializeExpression(ref writer,value,Resolver);
        writer.WriteType(value.TypeOperand);
    }
    public Expressions.TypeBinaryExpression Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var NodeType=reader.ReadNodeType();
        return NodeType switch{
            Expressions.ExpressionType.TypeEqual=>InternalDeserializeTypeEqual(ref reader,Resolver),
            Expressions.ExpressionType.TypeIs=>InternalDeserializeTypeIs(ref reader,Resolver),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
