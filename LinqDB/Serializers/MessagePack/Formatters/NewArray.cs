using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.NewArrayExpression;
using static Extension;
public class NewArray:IMessagePackFormatter<T> {
    public static readonly NewArray Instance=new();
    private const int ArrayHeader=3;
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        writer.WriteNodeType(value.NodeType);
        writer.WriteType(value.Type.GetElementType());
        writer.SerializeReadOnlyCollection(value.Expressions,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    private static (System.Type type,Expressions.Expression[]expressions)PrivateDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        return (type,expressions);
    }
    internal static T ReadNewArrayBounds(ref Reader reader,MessagePackSerializerOptions Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayBounds(type,expressions);
    }
    internal static T ReadNewArrayInit(ref Reader reader,MessagePackSerializerOptions Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayInit(type,expressions);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var NodeType=reader.ReadNodeType();
        return NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>ReadNewArrayBounds(ref reader,Resolver),
            Expressions.ExpressionType.NewArrayInit=>ReadNewArrayInit(ref reader,Resolver),
            _=>throw new NotImplementedException(Resolver.ToString())
        };
    }
}
