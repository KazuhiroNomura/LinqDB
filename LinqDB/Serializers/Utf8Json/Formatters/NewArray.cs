using System;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.NewArrayExpression;
public class NewArray:IJsonFormatter<T> {
    public static readonly NewArray Instance=new();




    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type.GetElementType());
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Expressions,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value,Resolver);
    }
    
    
    private static (Type type,Expressions.Expression[]expressions)PrivateRead(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
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
        reader.ReadIsBeginArrayWithVerify();
        
        var NodeType=reader.ReadNodeType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>ReadNewArrayBounds(ref reader,Resolver),
            _                                        =>ReadNewArrayInit(ref reader,Resolver)
        };
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
