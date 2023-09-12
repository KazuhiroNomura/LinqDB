using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.InvocationExpression;
public class Invocation:IJsonFormatter<T> {
    public static readonly Invocation Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var NodeTypeName=reader.ReadString();
        //reader.ReadIsValueSeparatorWithVerify();
        //var NodeType=Enum.Parse<ExpressionType>(NodeTypeName);
        var expression=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=reader.DeserializeArray<Expressions.Expression>(Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Invoke(expression,arguments);
    }
}
