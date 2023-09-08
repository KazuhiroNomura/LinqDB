using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using System.Linq.Expressions;
using LinqDB.Serializers.MessagePack;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.CatchBlock;
using C=Serializer;



using static Common;
public class CatchBlock:IJsonFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.Test,Resolver);
        Type.Instance.Serialize(ref writer,value.Test,Resolver);
        writer.WriteValueSeparator();
        if(value.Variable is null){
            writer.WriteInt32(0);
        } else{
            writer.WriteInt32(1);
            writer.WriteValueSeparator();
            writer.WriteString(value.Variable.Name);
            C.Instance.ListParameter.Add(value.Variable);
        }
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Filter,Resolver);
        if(value.Variable is not null){
            var ListParameter=C.Instance.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        //var test= this.Type.Deserialize(ref reader,Resolver);
        var test= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var 変数か=reader.ReadInt32();
        ParameterExpression? Variable;
        if(変数か==0){
            Variable=null;
        } else{
            reader.ReadIsValueSeparatorWithVerify();
            var name=reader.ReadString();
            Variable=Expressions.Expression.Parameter(test,name);
            C.Instance.ListParameter.Add(Variable);
        }
        reader.ReadIsValueSeparatorWithVerify();
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var @filter= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        if(Variable is null){
            return Expressions.Expression.Catch(test,body,@filter);
        }else{
            var ListParameter=C.Instance.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
            return Expressions.Expression.Catch(Variable,body,@filter);
        }
    }
}
