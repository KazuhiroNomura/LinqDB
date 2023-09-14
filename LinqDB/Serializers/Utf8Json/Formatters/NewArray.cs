using System;
using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;
using MessagePack;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.NewArrayExpression;
using static Extension;
public class NewArray:IJsonFormatter<T> {
    public static readonly NewArray Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteType(value.Type.GetElementType());
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Expressions,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    private static (System.Type type,Expressions.Expression[]expressions)PrivateDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        return (type,expressions);
    }
    internal static T InternalDeserializeNewArrayBounds(ref Reader reader,IJsonFormatterResolver Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayBounds(type,expressions);
    }
    internal static T InternalDeserializeNewArrayInit(ref Reader reader,IJsonFormatterResolver Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayInit(type,expressions);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var value=NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>InternalDeserializeNewArrayBounds(ref reader,Resolver),
            Expressions.ExpressionType.NewArrayInit=>InternalDeserializeNewArrayInit(ref reader,Resolver),
            _=>throw new NotImplementedException(Resolver.ToString())
        };
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
