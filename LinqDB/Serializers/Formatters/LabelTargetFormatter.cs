using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<LabelTarget>{
    private IJsonFormatter<LabelTarget> LabelTarget=>this;
    public void Serialize(ref JsonWriter writer,LabelTarget? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.Type,Resolver);
        //writer.WriteValueSeparator();
        //writer.WriteString(value.Name);
        //writer.WriteEndArray();
        if(this.Dictionary_LabelTarget_int.TryGetValue(value,out var 番号)){
            writer.WriteInt32(番号);
        } else{
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            番号=Dictionary_LabelTarget_int.Count;
            this.Dictionary_int_LabelTarget.Add(番号,value);
            Dictionary_LabelTarget_int.Add(value,番号);
            writer.WriteInt32(-1);
            writer.WriteValueSeparator();
            //this.Serialize(ref writer,value.Type,Resolver);
            Serialize_Type(ref writer,value.Type,Resolver);
            writer.WriteValueSeparator();
            writer.WriteString(value.Name);
            //Serialize_T(ref writer,value.Name,Resolver);
        }
        writer.WriteEndArray();
    }
    LabelTarget IJsonFormatter<LabelTarget>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var type=Deserialize_Type(ref reader,Resolver);
        //reader.ReadIsValueSeparatorWithVerify();
        //var name=reader.ReadString();
        //reader.ReadIsEndArrayWithVerify();
        //return Expression.Label(type,name);

        var 番号=reader.ReadInt32();
        LabelTarget target;
        if(番号==-1){
            reader.ReadIsValueSeparatorWithVerify();
            var type=Deserialize_Type(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var name=reader.ReadString();
            target=Expression.Label(type,name);
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            var Dictionary_LabelTarget_int_Count=Dictionary_LabelTarget_int.Count;
            this.Dictionary_int_LabelTarget.Add(Dictionary_LabelTarget_int_Count,target);
            Dictionary_LabelTarget_int.Add(target,Dictionary_LabelTarget_int_Count);
        } else{
            target=this.Dictionary_int_LabelTarget[番号];
        }
        reader.ReadIsEndArrayWithVerify();
        return target;
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<LabelTarget>{
    private IMessagePackFormatter<LabelTarget> MSLabelTarget=>this;
    public void Serialize(ref MessagePackWriter writer,LabelTarget? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        if(this.Dictionary_LabelTarget_int.TryGetValue(value,out var 番号)){
            writer.WriteInt32(番号);
        } else{
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            番号=Dictionary_LabelTarget_int.Count;
            this.Dictionary_int_LabelTarget.Add(番号,value);
            Dictionary_LabelTarget_int.Add(value,番号);
            writer.WriteInt32(-1);
            Serialize_Type(ref writer,value.Type,Resolver);
            writer.Write(value.Name);
        }
    }
    LabelTarget IMessagePackFormatter<LabelTarget>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var 番号=reader.ReadInt32();
        LabelTarget target;
        if(番号==-1){
            var type=Deserialize_Type(ref reader,Resolver);
            var name=reader.ReadString();
            target=Expression.Label(type,name);
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            var Dictionary_LabelTarget_int_Count=Dictionary_LabelTarget_int.Count;
            this.Dictionary_int_LabelTarget.Add(Dictionary_LabelTarget_int_Count,target);
            Dictionary_LabelTarget_int.Add(target,Dictionary_LabelTarget_int_Count);
        } else{
            target=this.Dictionary_int_LabelTarget[番号];
        }
        return target;
    }
}
//class MessagePackFormatter<T>:IMessagePackFormatter<T>{
//    public void Serialize(ref MessagePackWriter writer,T value,MessagePackSerializerOptions options){
//        throw new NotImplementedException();
//    }
//    T IMessagePackFormatter<T>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
//        throw new NotImplementedException();
//    }
//}
//class MessagePackFormatter<T>:IMessagePackFormatter<T>{
//    public void Serialize(ref MessagePackWriter writer,T value,MessagePackSerializerOptions options){
//        throw new NotImplementedException();
//    }
//    T IMessagePackFormatter<T>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
//        throw new NotImplementedException();
//    }
//}

