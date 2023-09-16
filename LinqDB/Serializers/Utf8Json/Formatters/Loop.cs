using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LoopExpression;
public class Loop:IJsonFormatter<T>{
    public static readonly Loop Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value.BreakLabel)){
            writer.WriteValueSeparator();
            Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        } else{
            LabelTarget.Instance.Serialize(ref writer,value.BreakLabel,Resolver);
            writer.WriteValueSeparator();
            if(writer.WriteIsNull(value.ContinueLabel)){
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Body,Resolver);
            } else{
                LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel,Resolver);
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Body,Resolver);
            }
        }
    }
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        T value;
        if(reader.ReadIsNull()){
            reader.ReadNext();
            var body=Expression.Instance.Deserialize(ref reader,Resolver);
            value=Expressions.Expression.Loop(body);
        } else{
            var breakLabel=LabelTarget.Instance.Deserialize(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            if(reader.ReadIsNull()){
                reader.ReadNext();
                var body=Expression.Instance.Deserialize(ref reader,Resolver);
                value=Expressions.Expression.Loop(body,breakLabel);
            } else{
                var continueLabel=LabelTarget.Instance.Deserialize(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var body=Expression.Instance.Deserialize(ref reader,Resolver);
                value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
            }
        }
        return value;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}