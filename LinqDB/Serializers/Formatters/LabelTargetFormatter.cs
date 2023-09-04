using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.LabelTarget>{
    private IJsonFormatter<Expressions.LabelTarget> LabelTarget=>this;
    public void Serialize(ref JsonWriter writer,Expressions.LabelTarget? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        if(this.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteInt32(index);
        } else{
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            this.ListLabelTarget.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteInt32(index);
            writer.WriteValueSeparator();
            Serialize_Type(ref writer,value.Type,Resolver);
            writer.WriteValueSeparator();
            writer.WriteString(value.Name);
        }
        writer.WriteEndArray();
    }
    Expressions.LabelTarget IJsonFormatter<Expressions.LabelTarget>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var index=reader.ReadInt32();
        var ListLabelTarget=this.ListLabelTarget;
        Debug.Assert(this.Dictionary_LabelTarget_int.Count==this.ListLabelTarget.Count);
        Expressions.LabelTarget target;
        if(index<ListLabelTarget.Count){
            target=ListLabelTarget[index];
        } else{
            reader.ReadIsValueSeparatorWithVerify();
            var type=Deserialize_Type(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            Debug.Assert(index==Dictionary_LabelTarget_int.Count);
            index=Dictionary_LabelTarget_int.Count;
            ListLabelTarget.Add(target);
            Dictionary_LabelTarget_int.Add(target,index);
        }
        reader.ReadIsEndArrayWithVerify();
        return target;
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.LabelTarget>{
    private IMessagePackFormatter<Expressions.LabelTarget> MSLabelTarget=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.LabelTarget? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        if(this.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteInt32(index);
        } else{
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            this.ListLabelTarget.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteInt32(index);
            Serialize_Type(ref writer,value.Type,Resolver);
            writer.Write(value.Name);
        }
    }
    Expressions.LabelTarget IMessagePackFormatter<Expressions.LabelTarget>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var index=reader.ReadInt32();
        var ListLabelTarget=this.ListLabelTarget;
        Expressions.LabelTarget target;
        if(index<ListLabelTarget.Count){
            target=ListLabelTarget[index];
        } else{
            var type=Deserialize_Type(ref reader,Resolver);
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            ListLabelTarget.Add(target);
            Dictionary_LabelTarget_int.Add(target,index);
        }
        return target;
    }
}
