using Utf8Json;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.GotoExpression;
public class Goto:IJsonFormatter<T> {
    public static readonly Goto Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteInt32((int)value.Kind);
        writer.WriteValueSeparator();
        LabelTarget.Instance.Serialize(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        if(!writer.WriteIsNull(value.Value))Expression.Instance.Serialize(ref writer,value.Value,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
    }
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer, value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        PrivateSerialize(ref writer, value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var kind=(Expressions.GotoExpressionKind)reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        //var target=Deserialize_T<LabelTarget>(ref reader,Resolver);
        var target= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=reader.ReadIsNull()?null:Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return Expressions.Expression.MakeGoto(kind,target,value,type);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
