using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LoopExpression;
public class Loop:IJsonFormatter<T> {
    public static readonly Loop Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
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
        writer.WriteEndArray();
        //writer.WriteBeginArray();
        ////writer.WriteString(nameof(ExpressionType.Loop));
        ////writer.WriteValueSeparator();
        //LabelTarget.Instance.Serialize(ref writer,value.BreakLabel,Resolver);
        //writer.WriteValueSeparator();
        //if(value.ContinueLabel is null)
        //	writer.WriteNull();
        //LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel,Resolver);
        //writer.WriteValueSeparator();
        //Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        //writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        T value;
        reader.ReadIsBeginArrayWithVerify();
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
        reader.ReadIsEndArrayWithVerify();
        return value;
        //reader.ReadIsBeginArrayWithVerify();
        //var breakLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        //reader.ReadIsValueSeparatorWithVerify();
        //var continueLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        //reader.ReadIsValueSeparatorWithVerify();
        //var body= Expression.Instance.Deserialize(ref reader,Resolver);
        ////var type=reader.ReadType()
        //reader.ReadIsEndArrayWithVerify();
        //return Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
}
