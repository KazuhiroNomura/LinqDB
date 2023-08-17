using System;
using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<ConstantExpression>{
    private IJsonFormatter<ConstantExpression> Constant=>this;
    public void Serialize(ref JsonWriter writer,ConstantExpression value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.Type,Resolver);
        //this.Serialize(ref writer,value.Type,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Value,Resolver);
        writer.WriteEndArray();
    }
    //private readonly object[] Objects2=new object[2];
    ConstantExpression IJsonFormatter<ConstantExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=Deserialize_T<object>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.Constant(value,type);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<ConstantExpression>{
    private IMessagePackFormatter<ConstantExpression> Constant=>this;
    public void Serialize(ref MessagePackWriter writer,ConstantExpression value,MessagePackSerializerOptions Resolver){
        Serialize_Type(ref writer,value.Type,Resolver);
        var type=value.Type;
        var Value=value.Value;
        if(Value is null)writer.WriteNil();
        else if(typeof(sbyte  )==type)writer.Write((sbyte  )Value);
        else if(typeof(short  )==type)writer.Write((short  )Value);
        else if(typeof(int    )==type)writer.Write((int    )Value);
        else if(typeof(long   )==type)writer.Write((long   )Value);
        else if(typeof(byte   )==type)writer.Write((byte   )Value);
        else if(typeof(ushort )==type)writer.Write((ushort )Value);
        else if(typeof(uint   )==type)writer.Write((uint   )Value);
        else if(typeof(ulong  )==type)writer.Write((ulong  )Value);
        else if(typeof(float  )==type)writer.Write((float  )Value);
        else if(typeof(double )==type)writer.Write((double )Value);
        else if(typeof(bool   )==type)writer.Write((bool   )Value);
        else if(typeof(string )==type)writer.Write((string )Value);
        else if(typeof(decimal)==type)Serialize_T(ref writer,(decimal)Value,Resolver);
        else if(typeof(Guid   )==type)Serialize_T(ref writer,(Guid)Value,Resolver);
        else MessagePackSerializer.Serialize(type,ref writer,value.Value,Resolver);
            //Serialize_T(ref writer,value.Value,Resolver);
        //MessagePackSerializer.Typeless.Serialize(ref writer,value.Value,Resolver);
            
    }
    ConstantExpression IMessagePackFormatter<ConstantExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=Deserialize_Type(ref reader,Resolver);
        object? value;
        if(reader.TryReadNil()) value=null;
        else if(typeof(sbyte  )==type)value=reader.ReadSByte();
        else if(typeof(short  )==type)value=reader.ReadInt16();
        else if(typeof(int    )==type)value=reader.ReadInt32();
        else if(typeof(long   )==type)value=reader.ReadInt64();
        else if(typeof(byte   )==type)value=reader.ReadByte();
        else if(typeof(ushort )==type)value=reader.ReadUInt16();
        else if(typeof(uint   )==type)value=reader.ReadUInt32();
        else if(typeof(ulong  )==type)value=reader.ReadUInt64();
        else if(typeof(float  )==type)value=reader.ReadSingle();
        else if(typeof(double )==type)value=reader.ReadDouble();
        else if(typeof(bool   )==type)value=reader.ReadBoolean();
        else if(typeof(string )==type)value=reader.ReadString();
        else if(typeof(decimal)==type)value=Deserialize_T<decimal>(ref reader,Resolver);
        else if(typeof(Guid   )==type)value=Deserialize_T<Guid>(ref reader,Resolver);
        else value=MessagePackSerializer.Deserialize(type,ref reader,Resolver);
        //var Formatter = options.Resolver.GetFormatter<object>();
        //var value = Formatter.Deserialize(ref reader,options);
        return Expression.Constant(value,type);
    }
}
