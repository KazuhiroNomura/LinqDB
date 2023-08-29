using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter{
    private void Serialize宣言Parameters(ref JsonWriter writer,ReadOnlyCollection<Expressions.ParameterExpression>value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        var Count=value.Count;
        if(Count>0){
            for(var a=0;;a++){
                var Parameter=value[a];
                writer.WriteBeginObject();
                writer.WriteString(Parameter.Name);
                writer.WriteNameSeparator();
                Serialize_Type(ref writer,Parameter.Type,Resolver);
                writer.WriteEndObject();
                if(a==Count-1) break;
                writer.WriteValueSeparator();
            }
        }
        writer.WriteEndArray();
    }
    private List<Expressions.ParameterExpression>Deserialize宣言Parameters(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        var List=new List<Expressions.ParameterExpression>();
        //var t=reader;
        reader.ReadIsBeginArrayWithVerify();
        while(reader.ReadIsBeginObject()){
            var name=reader.ReadString();
            reader.ReadIsNameSeparatorWithVerify();
            var type= Deserialize_Type(ref reader,Resolver);
            List.Add(Expressions.Expression.Parameter(type,name));
            reader.ReadIsEndObjectWithVerify();
            //var count=0;
            //if(!t.ReadIsEndObjectWithSkipValueSeparator(ref count)) break;
            if(!reader.ReadIsValueSeparator()) break;
        }
        reader.ReadIsEndArrayWithVerify();
        return List;
    }
}
partial class ExpressionMessagePackFormatter{
    private void Serialize宣言Parameters(ref MessagePackWriter writer,ReadOnlyCollection<Expressions.ParameterExpression>value,MessagePackSerializerOptions Resolver) {
        writer.WriteArrayHeader(value.Count);
        foreach(var Parameter in value){
            writer.Write(Parameter.Name);
            Serialize_Type(ref writer,Parameter.Type,Resolver);
        }
    }
    private Expressions.ParameterExpression[]Deserialize宣言Parameters(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Count=reader.ReadArrayHeader();
        var Parameters=new Expressions.ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var name=reader.ReadString();
            var type= Deserialize_Type(ref reader,Resolver);
            Parameters[a]=Expressions.Expression.Parameter(type,name);
        }
        return Parameters;
    }
}
