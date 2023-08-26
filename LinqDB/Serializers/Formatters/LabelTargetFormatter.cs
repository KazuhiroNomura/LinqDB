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
        if(this.Dictionary_LabelTarget_int.TryGetValue(value,out var 番号)){
            writer.WriteInt32(番号);
        } else{
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            番号=Dictionary_LabelTarget_int.Count;
            this.Dictionary_int_LabelTarget.Add(番号,value);
            Dictionary_LabelTarget_int.Add(value,番号);
            writer.WriteInt32(-1);
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
        var 番号=reader.ReadInt32();
        Expressions.LabelTarget target;
        if(番号>-1){
            target=this.Dictionary_int_LabelTarget[番号];
        } else{
            reader.ReadIsValueSeparatorWithVerify();
            var type=Deserialize_Type(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            var Dictionary_LabelTarget_int_Count=Dictionary_LabelTarget_int.Count;
            this.Dictionary_int_LabelTarget.Add(Dictionary_LabelTarget_int_Count,target);
            Dictionary_LabelTarget_int.Add(target,Dictionary_LabelTarget_int_Count);
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
    Expressions.LabelTarget IMessagePackFormatter<Expressions.LabelTarget>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var 番号=reader.ReadInt32();
        Expressions.LabelTarget target;
        if(番号>-1){
            target=this.Dictionary_int_LabelTarget[番号];
        } else{
            var type=Deserialize_Type(ref reader,Resolver);
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            var Dictionary_LabelTarget_int_Count=Dictionary_LabelTarget_int.Count;
            this.Dictionary_int_LabelTarget.Add(Dictionary_LabelTarget_int_Count,target);
            Dictionary_LabelTarget_int.Add(target,Dictionary_LabelTarget_int_Count);
        }
        return target;
    }
}
