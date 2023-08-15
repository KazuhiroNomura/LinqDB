using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using MessagePack;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter{//}:IJsonFormatter<ReadOnlyCollection<ParameterExpression>> {
    //private IJsonFormatter<ReadOnlyCollection<ParameterExpression>> Parameters=>this;
    //internal static DeclareParameterFormatter Instance{get;set;}
    //private readonly List<ParameterExpression> ListParameter;
    //internal static void Create(List<ParameterExpression> ListParameter){
    //    Instance=new(ListParameter);
    //}
    //private DeclareParameterFormatter(List<ParameterExpression> ListParameter){
    //    this.ListParameter=ListParameter;
    //}
    public void Serialize宣言Parameters(ref JsonWriter writer,ReadOnlyCollection<ParameterExpression>value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        var Count=value.Count;
        if(Count>0) {
            var a=0;
            while(true){
                var Parameter=value[a];
                writer.WriteBeginObject();
                writer.WriteString(Parameter.Name);
                writer.WriteNameSeparator();
                Serialize_Type(ref writer,Parameter.Type,Resolver);
                writer.WriteEndObject();
                Debug.Assert((a>=Count-1)==(a+1>=Count));
                if(a>=Count-1)break;
                writer.WriteValueSeparator();
                a++;
            }
        }
        writer.WriteEndArray();
    }
    internal List<ParameterExpression>Deserialize宣言Parameters(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var List=new List<ParameterExpression>();
        var t=reader;
        if(!t.ReadIsEndArray()){
            while(true){
                reader.ReadIsBeginObjectWithVerify();
                var name=reader.ReadString();
                reader.ReadIsNameSeparatorWithVerify();
                var type= Deserialize_Type(ref reader,Resolver);
                List.Add(Expression.Parameter(type,name));
                reader.ReadIsEndObjectWithVerify();
                t=reader;
                if(!t.ReadIsValueSeparator()) break;
            }
        }
        reader.ReadIsEndArrayWithVerify();
        return List;
    }
}
partial class ExpressionMessagePackFormatter{
    public void Serialize宣言Parameters(ref MessagePackWriter writer,ReadOnlyCollection<ParameterExpression>value,MessagePackSerializerOptions Resolver) {
        var Count=value.Count;
        writer.WriteArrayHeader(Count);
        for(var a=0;a<Count;a++){
            var Parameter=value[a];
            writer.Write(Parameter.Name);
            Serialize_Type(ref writer,Parameter.Type,Resolver);
        }
    }
    internal ParameterExpression[]Deserialize宣言Parameters(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Count=reader.ReadArrayHeader();
        var Parameters=new ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var name=reader.ReadString();
            var type= Deserialize_Type(ref reader,Resolver);
            Parameters[a]=Expression.Parameter(type,name);
        }
        return Parameters;
    }
}
