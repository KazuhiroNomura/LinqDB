using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.BlockExpression;
using C=Serializer;
using static Extension;
public class Block:IJsonFormatter<T> {
    public static readonly Block Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        writer.Serialize宣言Parameters(value.Variables,Resolver);
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Expressions,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var variables=reader.Deserialize宣言Parameters(Resolver);
        ListParameter.AddRange(variables);
        reader.ReadIsValueSeparatorWithVerify();
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        ListParameter.RemoveRange(ListParameter_Count,variables.Count);
        return Expressions.Expression.Block(type,variables,expressions);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
