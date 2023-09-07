using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
using T=Expressions.GotoExpression;
public class Goto:IJsonFormatter<T> {
    public static readonly Goto Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        writer.WriteInt32((int)value.Kind);
        writer.WriteValueSeparator();
        LabelTarget.Instance.Serialize(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Value,Resolver);
        writer.WriteValueSeparator();
        //this.Serialize(ref writer,value.Type,Resolver);
        Type.Instance.Serialize(ref writer,value.Type,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();

        var kind=(Expressions.GotoExpressionKind)reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        //var target=Deserialize_T<LabelTarget>(ref reader,Resolver);
        var target= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.MakeGoto(kind,target,value,type);
    }
}
