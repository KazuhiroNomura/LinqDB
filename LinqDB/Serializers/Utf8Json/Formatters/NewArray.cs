using System;
using Expressions = System.Linq.Expressions;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.NewArrayExpression;
using static Extension;
public class NewArray:IJsonFormatter<T> {
    public static readonly NewArray Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteType(value.Type.GetElementType());
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Expressions,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        writer.WriteString(value!.NodeType.ToString());
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    private static (System.Type type,Expressions.Expression[]expressions)PrivateDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        return (type,expressions);
    }
    internal static T ReadNewArrayBounds(ref Reader reader,IJsonFormatterResolver Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayBounds(type,expressions);
    }
    internal static T ReadNewArrayInit(ref Reader reader,IJsonFormatterResolver Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayInit(type,expressions);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var value=NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>ReadNewArrayBounds(ref reader,Resolver),
            Expressions.ExpressionType.NewArrayInit=>ReadNewArrayInit(ref reader,Resolver),
            _=>throw new NotImplementedException(Resolver.ToString())
        };
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
