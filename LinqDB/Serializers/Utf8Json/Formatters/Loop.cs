﻿using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LoopExpression;
public class Loop:IJsonFormatter<T>{
    public static readonly Loop Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value.BreakLabel)){
            writer.WriteValueSeparator();
            Expression.Write(ref writer,value.Body,Resolver);
        } else{
            LabelTarget.Write(ref writer,value.BreakLabel,Resolver);
            writer.WriteValueSeparator();
            if(writer.TryWriteNil(value.ContinueLabel)){
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
            } else{
                LabelTarget.Write(ref writer,value.ContinueLabel,Resolver);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
            }
        }
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        T value;
        if(reader.TryReadNil()){
            reader.ReadNext();
            var body=Expression.Read(ref reader,Resolver);
            value=Expressions.Expression.Loop(body);
        } else{
            var breakLabel=LabelTarget.Read(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            if(reader.TryReadNil()){
                reader.ReadNext();
                var body=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Loop(body,breakLabel);
            } else{
                var continueLabel=LabelTarget.Read(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var body=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
            }
        }
        return value;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}