using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                //foreach(var Parameter in value){
                writer.WriteBeginArray();
                Serialize_Type(ref writer,Parameter.Type,Resolver);
                //this.Serialize(ref writer,Parameter.Type,Resolver);
                writer.WriteValueSeparator();
                writer.WriteString(Parameter.Name);
                writer.WriteEndArray();
                if(a<Count-1){
                    writer.WriteValueSeparator();
                    a++;
                } else{
                    break;
                }
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
                reader.ReadIsBeginArrayWithVerify();
                //var type= this.Type.Deserialize(ref reader,Resolver);
                var type= Deserialize_Type(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var name=reader.ReadString();
                List.Add(Expression.Parameter(type,name));
                reader.ReadIsEndArrayWithVerify();
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
            Serialize_Type(ref writer,Parameter.Type,Resolver);
            writer.Write(Parameter.Name);
        }
    }
    internal ParameterExpression[]Deserialize宣言Parameters(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Count=reader.ReadArrayHeader();
        var Parameters=new ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var type= Deserialize_Type(ref reader,Resolver);
            var name=reader.ReadString();
            Parameters[a]=Expression.Parameter(type,name);
        }
        return Parameters;
    }
}
