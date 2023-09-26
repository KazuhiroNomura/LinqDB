using System;
using System.Diagnostics;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.CatchBlock;
public class CatchBlock:IJsonFormatter<T> {
    public static readonly CatchBlock Instance = new();
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        if(value!.Variable is null) {
            if(value.Filter is null) {

                writer.WriteInt32(0);
                writer.WriteValueSeparator();
                writer.WriteType(value.Test);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
            } else {
                
                writer.WriteInt32(1);
                writer.WriteValueSeparator();
                writer.WriteType(value.Test);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Filter,Resolver);
            }
        } else {
            var Parameters=Resolver.Serializer().Parameters;
            Parameters.Add(value.Variable);
            if(value.Filter is null) {
                
                writer.WriteInt32(2);
                writer.WriteValueSeparator();
                writer.WriteType(value.Test);
                writer.WriteValueSeparator();
                writer.WriteString(value.Variable.Name);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
            } else {
                
                writer.WriteInt32(3);
                writer.WriteValueSeparator();
                writer.WriteType(value.Test);
                writer.WriteValueSeparator();
                writer.WriteString(value.Variable.Name);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Filter,Resolver);
            }
            Parameters.RemoveAt(Parameters.Count-1);
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;

        T value;
        reader.ReadIsBeginArrayWithVerify();
        var id = reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        var test = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        switch(id) {
            case 0: {
                var body = Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Catch(test,body);
                break;
            }
            case 1: {
                var body = Expression.Read(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var filter = Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Catch(test,body,filter);
                break;
            }
            case 2: {
                var name = reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var Parameters=Resolver.Serializer().Parameters;
                Parameters.Add(Variable);
                reader.ReadIsValueSeparatorWithVerify();
                var body = Expression.Read(ref reader,Resolver);
                Parameters.RemoveAt(Parameters.Count-1);
                value=Expressions.Expression.Catch(Variable,body);
                break;
            }
            default:{
                Debug.Assert(id==3);
                var name = reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var Parameters=Resolver.Serializer().Parameters;
                Parameters.Add(Variable);
                reader.ReadIsValueSeparatorWithVerify();
                var body = Expression.Read(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var filter = Expression.Read(ref reader,Resolver);
                Parameters.RemoveAt(Parameters.Count-1);
                value=Expressions.Expression.Catch(Variable,body,filter);
                break;
            }
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
