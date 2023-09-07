using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using LinqDB.Serializers.Utf8Json.Formatters;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ConstantExpression;
using C=MessagePackCustomSerializer;
public class Constant:IMessagePackFormatter<Expressions.ConstantExpression>{
    public static readonly Constant Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.ConstantExpression value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteArrayHeader(2);
        writer.WriteType(value.Type);
        Object.Instance.Serialize(ref writer,value.Value,Resolver);
        //if     (typeof(sbyte  )==type)writer.Write((sbyte  )Value);
        //else if(typeof(short  )==type)writer.Write((short  )Value);
        //else if(typeof(int    )==type)writer.Write((int    )Value);
        //else if(typeof(long   )==type)writer.Write((long   )Value);
        //else if(typeof(byte   )==type)writer.Write((byte   )Value);
        //else if(typeof(ushort )==type)writer.Write((ushort )Value);
        //else if(typeof(uint   )==type)writer.Write((uint   )Value);
        //else if(typeof(ulong  )==type)writer.Write((ulong  )Value);
        //else if(typeof(float  )==type)writer.Write((float  )Value);
        //else if(typeof(double )==type)writer.Write((double )Value);
        //else if(typeof(bool   )==type)writer.Write((bool   )Value);
        //else if(typeof(string )==type)writer.Write((string )Value);
        //else if(typeof(decimal)==type)Object.Instance.Serialize(ref writer,(decimal)Value,Resolver);
        //else if(typeof(Guid   )==type)Object.Instance.Serialize(ref writer,(Guid)Value,Resolver);
        //else MessagePackSerializer.Serialize(type,ref writer,value.Value,Resolver);
            //SerializeReadOnlyCollection(ref writer,value.Value,Resolver);
        //MessagePackSerializer.Typeless.Serialize(ref writer,value.Value,Resolver);
            
    }
    public Expressions.ConstantExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var type=reader.ReadType();
        //else if(typeof(sbyte  )==type)value=reader.ReadSByte();
        //else if(typeof(short  )==type)value=reader.ReadInt16();
        //else if(typeof(int    )==type)value=reader.ReadInt32();
        //else if(typeof(long   )==type)value=reader.ReadInt64();
        //else if(typeof(byte   )==type)value=reader.ReadByte();
        //else if(typeof(ushort )==type)value=reader.ReadUInt16();
        //else if(typeof(uint   )==type)value=reader.ReadUInt32();
        //else if(typeof(ulong  )==type)value=reader.ReadUInt64();
        //else if(typeof(float  )==type)value=reader.ReadSingle();
        //else if(typeof(double )==type)value=reader.ReadDouble();
        //else if(typeof(bool   )==type)value=reader.ReadBoolean();
        //else if(typeof(string )==type)value=reader.ReadString();
        var value= Object.Instance.Deserialize(ref reader,Resolver);
        //var value = Formatter.Deserialize(ref reader,options);
        return Expressions.Expression.Constant(value,type);
    }
}
