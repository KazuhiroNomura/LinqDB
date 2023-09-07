using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LoopExpression;
using static Common;
public class Loop:IJsonFormatter<T> {
    public static readonly Loop Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        //writer.WriteString(nameof(ExpressionType.Loop));
        //writer.WriteValueSeparator();
        LabelTarget.Instance.Serialize(ref writer,value.BreakLabel,Resolver);
        writer.WriteValueSeparator();
        LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var breakLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var continueLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        //var type=reader.ReadType()
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
}
