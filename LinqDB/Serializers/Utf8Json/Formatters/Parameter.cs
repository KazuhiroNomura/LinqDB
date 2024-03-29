﻿
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ParameterExpression;
public class Parameter:IJsonFormatter<T> {
    public static readonly Parameter Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        var Serializer=Resolver.Serializer();
        var index0=Serializer.Parameters.LastIndexOf(value);
        writer.WriteInt32(index0);
        if(index0<0){
            writer.WriteValueSeparator();
            var index1=Serializer.ラムダ跨ぎParameters.LastIndexOf(value);
            writer.WriteInt32(index1);
            if(index1<0){
                writer.WriteValueSeparator();
                writer.WriteString(value.Name);
                writer.WriteValueSeparator();
                writer.WriteType(value.Type);
                writer.WriteValueSeparator();
                writer.WriteBoolean(value.IsByRef);
                Serializer.ラムダ跨ぎParameters.Add(value);
            }
        }
        writer.WriteEndArray(); 
        
        
        
        
        
        
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(Expressions.ExpressionType.Parameter);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        var Serializer=Resolver.Serializer();
        var index0=reader.ReadInt32();
        if(index0<0){
            reader.ReadIsValueSeparatorWithVerify();
            var index1=reader.ReadInt32();
            if(index1<0){
                reader.ReadIsValueSeparatorWithVerify();
                var name=reader.ReadString();
                reader.ReadIsValueSeparatorWithVerify();
                var type=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var IsByRef=reader.ReadBoolean();
                var Parameter=Expressions.Expression.Parameter(IsByRef?type.MakeByRefType():type,name);
                Serializer.ラムダ跨ぎParameters.Add(Parameter);
                return Parameter;
            } else{
                
                
                return Serializer.ラムダ跨ぎParameters[index1];
            }
        }else{
            
            return Serializer.Parameters[index0];
        }
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArray();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
