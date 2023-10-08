
using System;
using System.Reflection;
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters.Others;

using Reader = MemoryPackReader;
using T = System.Object;
using Reflection;
using System.Runtime.InteropServices;

public class Object : MemoryPackFormatter<T>{
    public static readonly Object Instance = new();
    private static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, T value) where TBufferWriter :IBufferWriter<byte>{

        var type = value.GetType();
        writer.WriteType(type);

        switch (value){
            case sbyte          v:writer.WriteUnmanaged(v);break;
            case short          v:writer.WriteUnmanaged(v);break;
            case int            v:writer.WriteUnmanaged(v);break;
            case long           v:writer.WriteUnmanaged(v);break;
            case byte           v:writer.WriteUnmanaged(v);break;
            case ushort         v:writer.WriteUnmanaged(v);break;
            case uint           v:writer.WriteUnmanaged(v);break;
            case ulong          v:writer.WriteUnmanaged(v);break;
            case float          v:writer.WriteUnmanaged(v);break;
            case double         v:writer.WriteUnmanaged(v);break;
            case bool           v:writer.WriteUnmanaged(v);break;
            case char           v:writer.WriteUnmanaged(v);break;
            case decimal        v:writer.WriteUnmanaged(v);break;
            case TimeSpan       v:writer.WriteUnmanaged(v);break;
            case DateTime       v:writer.WriteUnmanaged(v);break;
            case DateTimeOffset v:writer.WriteUnmanaged(v);break;
            case string         v:writer.WriteString   (v);break;
            case Expressions.Expression v:Expression .Write(ref writer,v);break;
            case System.Type            v:Type       .Write(ref writer,v);break;
            case ConstructorInfo        v:Constructor.Write(ref writer,v);break;
            case MethodInfo             v:Method     .Write(ref writer,v);break;
            case PropertyInfo           v:Property   .Write(ref writer,v);break;
            case EventInfo              v:Event      .Write(ref writer,v);break;
            case FieldInfo              v:Field      .Write(ref writer,v);break;
            default:{ writer.Write(type,value);break;}
        }

    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, T? value) where TBufferWriter :IBufferWriter<byte>{
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref object? value) => WriteNullable(ref writer, value);
    private static T Read(ref Reader reader){
        T value;

        var TypeName= reader.ReadString();
        switch(TypeName){
            case"SByte"         :{reader.ReadUnmanaged(out sbyte          value0);value=value0;break;}
            case"Byte"          :{reader.ReadUnmanaged(out byte           value0);value=value0;break;}
            case"Int16"         :{reader.ReadUnmanaged(out short          value0);value=value0;break;}
            case"UInt16"        :{reader.ReadUnmanaged(out ushort         value0);value=value0;break;}
            case"Int32"         :{reader.ReadUnmanaged(out int            value0);value=value0;break;}
            case"UInt32"        :{reader.ReadUnmanaged(out uint           value0);value=value0;break;}
            case"Int64"         :{reader.ReadUnmanaged(out long           value0);value=value0;break;}
            case"UInt64"        :{reader.ReadUnmanaged(out ulong          value0);value=value0;break;}
            case"Single"        :{reader.ReadUnmanaged(out float          value0);value=value0;break;}
            case"Double"        :{reader.ReadUnmanaged(out double         value0);value=value0;break;}
            case"Boolean"       :{reader.ReadUnmanaged(out bool           value0);value=value0;break;}
            case"Char"          :{reader.ReadUnmanaged(out char           value0);value=value0;break;}
            case"Decimal"       :{reader.ReadUnmanaged(out decimal        value0);value=value0;break;}
            case"TimeSpan"      :{reader.ReadUnmanaged(out TimeSpan       value0);value=value0;break;}
            case"DateTime"      :{reader.ReadUnmanaged(out DateTime       value0);value=value0;break;}
            case"DateTimeOffset":{reader.ReadUnmanaged(out DateTimeOffset value0);value=value0;break;}
            default:{
                var type=TypeName.StringType();
                if     (typeof(Expressions.Expression).IsAssignableFrom(type))value=Expression .Read(ref reader);
                else if(typeof(System.Type           ).IsAssignableFrom(type))value=Type       .Read(ref reader);
                else if(typeof(ConstructorInfo       ).IsAssignableFrom(type))value=Constructor.Read(ref reader);
                else if(typeof(MethodInfo            ).IsAssignableFrom(type))value=Method     .Read(ref reader);
                else if(typeof(PropertyInfo          ).IsAssignableFrom(type))value=Property   .Read(ref reader);
                else if(typeof(EventInfo             ).IsAssignableFrom(type))value=Event      .Read(ref reader);
                else if(typeof(FieldInfo             ).IsAssignableFrom(type))value=Field      .Read(ref reader);
                else value=reader.Read(type)!;
                break;
            }
        }
        //Type.GetType(reader.ReadString())!;
        ////var TypeName= reader.ReadString();
        //var type=reader.ReadType();
        ////Type.GetType(reader.ReadString())!;
        //if     (typeof(sbyte   )==type){reader.ReadUnmanaged(out sbyte    value0);value=value0;}
        //else if(typeof(short   )==type){reader.ReadUnmanaged(out short    value0);value=value0;}
        //else if(typeof(int     )==type){reader.ReadUnmanaged(out int      value0);value=value0;}
        //else if(typeof(long    )==type){reader.ReadUnmanaged(out long     value0);value=value0;}
        //else if(typeof(byte    )==type){reader.ReadUnmanaged(out byte     value0);value=value0;}
        //else if(typeof(ushort  )==type){reader.ReadUnmanaged(out ushort   value0);value=value0;}
        //else if(typeof(uint    )==type){reader.ReadUnmanaged(out uint     value0);value=value0;}
        //else if(typeof(ulong   )==type){reader.ReadUnmanaged(out ulong    value0);value=value0;}
        //else if(typeof(float   )==type){reader.ReadUnmanaged(out float    value0);value=value0;}
        //else if(typeof(double  )==type){reader.ReadUnmanaged(out double   value0);value=value0;}
        //else if(typeof(bool    )==type){reader.ReadUnmanaged(out bool     value0);value=value0;}
        //else if(typeof(char    )==type){reader.ReadUnmanaged(out char     value0);value=value0;}
        //else if(typeof(decimal )==type){reader.ReadUnmanaged(out decimal  value0);value=value0;}
        //else if(typeof(TimeSpan)==type){reader.ReadUnmanaged(out TimeSpan value0);value=value0;}
        //else if(typeof(DateTime)==type){reader.ReadUnmanaged(out DateTime value0);value=value0;}
        //else if (typeof(Expressions.Expression).IsAssignableFrom(type)) value=Expression.Read(ref reader);
        //else if (typeof(System.Type).IsAssignableFrom(type)) value=Type.Read(ref reader);
        //else if (typeof(ConstructorInfo).IsAssignableFrom(type)) value=Constructor.Read(ref reader);
        //else if (typeof(MethodInfo).IsAssignableFrom(type)) value=Method.Read(ref reader);
        //else if (typeof(PropertyInfo).IsAssignableFrom(type)) value=Property.Read(ref reader);
        //else if (typeof(EventInfo).IsAssignableFrom(type)) value=Event.Read(ref reader);
        //else if (typeof(FieldInfo).IsAssignableFrom(type)) value=Field.Read(ref reader);
        //else value=reader.Read(type)!;

        return value;
    }
    internal static T? ReadNullable(ref Reader reader) => reader.TryReadNil() ? null : Read(ref reader);
    public override void Deserialize(ref Reader reader, scoped ref T? value) => value=ReadNullable(ref reader);
}
