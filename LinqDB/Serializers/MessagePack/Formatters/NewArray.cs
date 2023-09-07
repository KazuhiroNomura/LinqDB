using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.NewArrayExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class NewArray:IMessagePackFormatter<Expressions.NewArrayExpression>{
    public static readonly NewArray Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.NewArrayExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteNodeType(value.NodeType);
        writer.WriteType(value.Type.GetElementType());
        SerializeReadOnlyCollection(ref writer,value.Expressions,Resolver);
    }
    public Expressions.NewArrayExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var NodeType=(Expressions.ExpressionType)reader.ReadByte();
        var type=reader.ReadType();
        var expressions= DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        return NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>Expressions.Expression.NewArrayBounds(type,expressions),
            Expressions.ExpressionType.NewArrayInit=>Expressions.Expression.NewArrayInit(type,expressions),
            _=>throw new NotImplementedException(Resolver.ToString())
        };
    }
}
