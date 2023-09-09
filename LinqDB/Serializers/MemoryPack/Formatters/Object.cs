﻿using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=System.Object;
public class Object:MemoryPackFormatter<object>{
    public static readonly Object Instance=new();
    public void SerializeObject<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,object? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    public object DeserializeObject(ref Reader reader){
        object? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref object? value){
        if(value is null){
            writer.WriteNullObjectHeader();
            return;
        }
        var type=value.GetType();
        writer.WriteType(type);
        //writer.WriteValue(value.Value.GetType(),value.Value);
        //this.Serialize(ref writer,value.Type);
        switch(value){
            case sbyte   v:writer.WriteVarInt(v         );break;
            case short   v:writer.WriteVarInt(v         );break;
            case int     v:writer.WriteVarInt(v         );break;
            case long    v:writer.WriteVarInt(v         );break;
            case byte    v:writer.WriteVarInt(v         );break;
            case ushort  v:writer.WriteVarInt(v         );break;
            case uint    v:writer.WriteVarInt(v         );break;
            case ulong   v:writer.WriteVarInt(v         );break;
            case string  v:writer.WriteString(v         );break;
            default       :
                Serializer.変数Register(type);
                writer.WriteValue (type,value);break;
        }
        //if    (typeof(sbyte  )==type)writer.WriteVarInt((sbyte  )value);
        //else if(typeof(short  )==type)writer.WriteVarInt((short  )value);
        //else if(typeof(int    )==type)writer.WriteVarInt((int    )value);
        //else if(typeof(long   )==type)writer.WriteVarInt((long   )value);
        //else if(typeof(byte   )==type)writer.WriteVarInt((byte   )value);
        //else if(typeof(ushort )==type)writer.WriteVarInt((ushort )value);
        //else if(typeof(uint   )==type)writer.WriteVarInt((uint   )value);
        //else if(typeof(ulong  )==type)writer.WriteVarInt((ulong  )value);
        //else if(typeof(float  )==type)writer.WriteValue((float  )value);
        //else if(typeof(double )==type)writer.WriteValue((double )value);
        //else if(typeof(bool   )==type)writer.WriteValue((bool   )value);
        //else if(typeof(string )==type)writer.WriteString((string )value);
        //else if(typeof(decimal)==type)writer.WriteValue((decimal)value);
        //else if(typeof(Guid   )==type)writer.WriteValue((Guid   )value);
        //else{
        //    writer.WriteValue(type,value);
        //    //var Formatter=writer.GetFormatter(type);
        //    //Formatter.Serialize(ref writer,ref value);
        //}
            //this.Serialize(ref writer,value.Value);
        //MessagePackSerializer.Typeless.Serialize(ref writer,value.Value);
            
    }
    public override void Deserialize(ref Reader reader,scoped ref object? value){
        if(reader.PeekIsNull()){
            reader.Advance(1);
            value=null;
            return;
        }
        var type=reader.ReadType();
        //object? value=default;
        if     (typeof(sbyte  )==type)value=reader.ReadVarIntSByte();
        else if(typeof(short  )==type)value=reader.ReadVarIntInt16();
        else if(typeof(int    )==type)value=reader.ReadVarIntInt32();
        else if(typeof(long   )==type)value=reader.ReadVarIntInt64();
        else if(typeof(byte   )==type)value=reader.ReadVarIntByte();
        else if(typeof(ushort )==type)value=reader.ReadVarIntUInt16();
        else if(typeof(uint   )==type)value=reader.ReadVarIntUInt32();
        else if(typeof(ulong  )==type)value=reader.ReadVarIntUInt64();
        //else if(typeof(float  )==type){float   Constant_value=0       ;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(double )==type){double  Constant_value=0       ;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(bool   )==type){bool    Constant_value=default!;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(string )==type){value=reader.ReadString(); }
        //else if(typeof(decimal)==type){decimal Constant_value=0       ;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(Guid   )==type){Guid    Constant_value=default!;reader.ReadValue (ref Constant_value);value=Constant_value; }
        else{
            Serializer.変数Register(type);
            reader.ReadValue(type,ref value);
            //var Formatter=reader.GetFormatter(type);
            //Formatter.Deserialize(ref reader,ref value);
        }
    }
}
