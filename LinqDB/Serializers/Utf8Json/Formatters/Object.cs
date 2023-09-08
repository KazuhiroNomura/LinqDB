using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
using Utf8Json;

using System.Diagnostics;
using LinqDB.Helpers;
using MessagePack;
using System;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using C=Serializer;
public class Object:IJsonFormatter<object>{
    public static readonly Object Instance=new();
    private readonly object[] Objects3=new object[3];
    public void Serialize(ref Writer writer,object? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        Debug.Assert(value!=null,nameof(value)+" != null");
        var type=value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        /*
        if(typeof(Expressions.Expression).IsAssignableFrom(type)){
            var Formatter= Resolver.GetFormatter<Expressions.Expression>();
            //var Formatter = formatterResolver.GetFormatter<LambdaExpression>();
            Formatter.Serialize(ref writer,(Expressions.Expression)(object)value, Resolver);
            //Formatter.Serialize(ref writer,(LambdaExpression)(object)value,formatterResolver);
            //}else if(typeof(T).IsDisplay()){
            //    return Return(new DisplayClassJsonFormatter<T>());
        }else 
        */
        switch(value){
            case sbyte   v:writer.WriteSByte (v);break;
            case short   v:writer.WriteInt16 (v);break;
            case int     v:writer.WriteInt32 (v);break;
            case long    v:writer.WriteInt64 (v);break;
            case byte    v:writer.WriteByte  (v);break;
            case ushort  v:writer.WriteUInt16(v);break;
            case uint    v:writer.WriteUInt32(v);break;
            case ulong   v:writer.WriteUInt64(v);break;
            case string  v:writer.WriteString(v);break;
            default:{
                var Formatter= Resolver.GetFormatterDynamic(type);
                var Serialize= Formatter.GetType().GetMethod("Serialize");
                Debug.Assert(Serialize is not null);
                var Objects3=this.Objects3;
                Objects3[0]=writer;
                Objects3[1]=value;
                Objects3[2]=Resolver;
                Serialize.Invoke(Formatter, Objects3);
                writer=(Writer)Objects3[0];
                break;
            }
        }
        writer.WriteEndArray();
    }
    private readonly object[] Objects2=new object[2];
    public object Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(!reader.ReadBoolean())return null!;
        if(reader.ReadIsNull()) return null!;
        object result;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        //object? value=default;
        if     (typeof(sbyte  )==type)result=reader.ReadSByte();
        else if(typeof(short  )==type)result=reader.ReadInt16();
        else if(typeof(int    )==type)result=reader.ReadInt32();
        else if(typeof(long   )==type)result=reader.ReadInt64();
        else if(typeof(byte   )==type)result=reader.ReadByte();
        else if(typeof(ushort )==type)result=reader.ReadUInt16();
        else if(typeof(uint   )==type)result=reader.ReadUInt32();
        else if(typeof(ulong  )==type)result=reader.ReadUInt64();
        else if(typeof(float  )==type)result=reader.ReadSingle();
        else if(typeof(double )==type)result=reader.ReadDouble();
        else if(typeof(bool   )==type)result=reader.ReadBoolean();
        else if(typeof(string )==type)result=reader.ReadString();
        //else if(typeof(decimal)==type)result=global::Utf8Json.Formatters.DecimalFormatter.Default.Deserialize(ref reader,Resolver);
        //else if(typeof(Guid   )==type)result=global::Utf8Json.Formatters.GuidFormatter.Default.Deserialize(ref reader,Resolver);
        else{
            var Formatter= Resolver.GetFormatterDynamic(type);
            var Deserialize= Formatter.GetType().GetMethod("Deserialize");
            Debug.Assert(Deserialize is not null);
            var Objects2=this.Objects2;
            Objects2[0]=reader;
            Objects2[1]=Resolver;
            result=Deserialize.Invoke(Formatter, Objects2)!;
            reader=(Reader)Objects2[0];
            //global::Utf8Json.Formatters.GuidFormatter.Default.Deserialize(ref reader,Resolver);}
            //var Formatter=reader.GetFormatter(type);
            //Formatter.Deserialize(ref reader,ref value);
        }
        reader.ReadIsEndArrayWithVerify();
        return result;
    }
}
