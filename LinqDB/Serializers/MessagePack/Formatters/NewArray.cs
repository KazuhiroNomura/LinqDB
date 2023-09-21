using System;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.NewArrayExpression;
public class NewArray:IMessagePackFormatter<T> {
    public static readonly NewArray Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        writer.WriteType(value.Type.GetElementType());
        writer.WriteCollection(value.Expressions,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value);
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value);
        PrivateWrite(ref writer,value,Resolver);
    }
    private static (Type type,Expressions.Expression[]expressions)PrivateRead(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        return (type,expressions);
    }
    internal static T ReadNewArrayBounds(ref Reader reader,O Resolver){
        var (type,expressions)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.NewArrayBounds(type,expressions);
    }
    internal static T ReadNewArrayInit(ref Reader reader,O Resolver){
        var (type,expressions)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.NewArrayInit(type,expressions);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        System.Diagnostics.Debug.Assert(3==count);
        var NodeType=reader.ReadNodeType();
        
        return NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>ReadNewArrayBounds(ref reader,Resolver),
            Expressions.ExpressionType.NewArrayInit=>ReadNewArrayInit(ref reader,Resolver),
            _=>throw new NotImplementedException(Resolver.ToString())
        };
        
        
    }
}
