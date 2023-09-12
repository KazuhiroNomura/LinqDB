using System;
using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.CatchBlock;
using C=Serializer;



using static Extension;
public class CatchBlock:IJsonFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        writer.WriteType(value.Test);
        writer.WriteValueSeparator();
        if(value.Variable is null){
            if(value.Filter is null){
                writer.WriteInt32(0);
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Body,Resolver);
            } else{
                writer.WriteInt32(1);
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Body,Resolver);
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Filter,Resolver);
            }
        } else{
            if(value.Filter is null){
                writer.WriteInt32(2);
                writer.WriteValueSeparator();
                writer.WriteString(value.Variable.Name);
                var ListParameter=C.Instance.ListParameter;
                ListParameter.Add(value.Variable);
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Body,Resolver);
                ListParameter.RemoveAt(ListParameter.Count-1);
            } else{
                writer.WriteInt32(3);
                writer.WriteValueSeparator();
                writer.WriteString(value.Variable.Name);
                var ListParameter=C.Instance.ListParameter;
                ListParameter.Add(value.Variable);
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Body,Resolver);
                writer.WriteValueSeparator();
                Expression.Instance.Serialize(ref writer,value.Filter,Resolver);
                ListParameter.RemoveAt(ListParameter.Count-1);
            }
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        T value;
        reader.ReadIsBeginArrayWithVerify();
        var test= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var id=reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        switch(id){
            case 0:{
                var body= Expression.Instance.Deserialize(ref reader,Resolver);
                value=Expressions.Expression.Catch(test,body);
                break;
            }
            case 1:{
                var body= Expression.Instance.Deserialize(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var filter= Expression.Instance.Deserialize(ref reader,Resolver);
                value=Expressions.Expression.Catch(test,body,filter);
                break;
            }
            case 2:{
                var name=reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var ListParameter=C.Instance.ListParameter;
                ListParameter.Add(Variable);
                reader.ReadIsValueSeparatorWithVerify();
                var body= Expression.Instance.Deserialize(ref reader,Resolver);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Variable,body);
                break;
            }
            case 3:{
                var name=reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var ListParameter=C.Instance.ListParameter;
                ListParameter.Add(Variable);
                reader.ReadIsValueSeparatorWithVerify();
                var body= Expression.Instance.Deserialize(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var filter= Expression.Instance.Deserialize(ref reader,Resolver);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Variable,body,filter);
                break;
            }
            default:
                throw new NotSupportedException($"Utf8JsonのCatchのidが不正な{id}だった");
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
