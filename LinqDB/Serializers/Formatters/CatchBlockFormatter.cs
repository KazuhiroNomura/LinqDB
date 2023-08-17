using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<CatchBlock>{
    public void Serialize(ref JsonWriter writer,CatchBlock value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.Test,Resolver);
        Serialize_Type(ref writer,value.Test,Resolver);
        writer.WriteValueSeparator();
        if(value.Variable is null){
            writer.WriteInt32(0);
        } else{
            writer.WriteInt32(1);
            writer.WriteValueSeparator();
            writer.WriteString(value.Variable.Name);
            this.ListParameter.Add(value.Variable);
        }
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Filter,Resolver);
        writer.WriteEndArray();
    }
    CatchBlock IJsonFormatter<CatchBlock>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        //var test= this.Type.Deserialize(ref reader,Resolver);
        var test= Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var 変数か=reader.ReadInt32();
        string? name;
        if(変数か==0) name=null;
        else{
            reader.ReadIsValueSeparatorWithVerify();
            name=reader.ReadString();
        }
        reader.ReadIsValueSeparatorWithVerify();
        var body= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var @filter= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return name is null?Expression.Catch(test,body,@filter):Expression.Catch(Expression.Parameter(test,name),body,@filter);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<CatchBlock>{
    public void Serialize(ref MessagePackWriter writer,CatchBlock value,MessagePackSerializerOptions Resolver){
        Serialize_T(ref writer,value.Test,Resolver);
        if(value.Variable is null){
            writer.WriteNil();
            //writer.WriteInt32(0);
        } else{
            //writer.WriteInt32(1);
            writer.Write(value.Variable.Name);
            this.ListParameter.Add(value.Variable);
        }
        this.Serialize(ref writer,value.Body,Resolver);
        this.Serialize(ref writer,value.Filter,Resolver);
        if(value.Variable is not null){
            var ListParameter=this.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    CatchBlock IMessagePackFormatter<CatchBlock>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var test= Deserialize_Type(ref reader,Resolver);
        if(reader.TryReadNil()){
            var body= this.Deserialize(ref reader,Resolver);
            var @filter= this.Deserialize(ref reader,Resolver);
            return Expression.Catch(test,body,@filter);
        } else{
            var name=reader.ReadString();
            var ListParameter=this.ListParameter;
            ListParameter.Add(Expression.Parameter(test,name));
            var body= this.Deserialize(ref reader,Resolver);
            var @filter= this.Deserialize(ref reader,Resolver);
            ListParameter.RemoveAt(ListParameter.Count-1);
            return Expression.Catch(Expression.Parameter(test,name),body,@filter);
        }
    }
}
