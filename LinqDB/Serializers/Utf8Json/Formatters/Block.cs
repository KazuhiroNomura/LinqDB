using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using LinqDB.Serializers.MessagePack;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.BlockExpression;
using C=Utf8JsonCustomSerializer;
using static Common;
public class Block:IJsonFormatter<T> {
    public static readonly Block Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.Type,Resolver);
        Type.Instance.Serialize(ref writer,value.Type,Resolver);
        writer.WriteValueSeparator();
        Serialize宣言Parameters(ref writer,value.Variables,Resolver);
        writer.WriteValueSeparator();
        SerializeReadOnlyCollection(ref writer,value.Expressions,Resolver);
        writer.WriteEndArray();
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var variables= Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(variables);
        reader.ReadIsValueSeparatorWithVerify();
        var expressions=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        ListParameter.RemoveRange(ListParameter_Count,variables.Count);
        return Expressions.Expression.Block(type,variables,expressions);
    }
}
