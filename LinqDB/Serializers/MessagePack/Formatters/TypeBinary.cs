using System;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using C=MessagePackCustomSerializer;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.UnaryExpression;
using static Common;
public class TypeBinary:IMessagePackFormatter<Expressions.TypeBinaryExpression>{
    public static readonly TypeBinary Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.TypeBinaryExpression? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteType(value.TypeOperand);
    }
    public Expressions.TypeBinaryExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var NodeType=reader.ReadNodeType();
        var expression= Expression.Instance.Deserialize(ref reader,Resolver);
        var type=reader.ReadType();
        return NodeType switch{
            Expressions.ExpressionType.TypeEqual=>Expressions.Expression.TypeEqual(expression,type),
            Expressions.ExpressionType.TypeIs=>Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
