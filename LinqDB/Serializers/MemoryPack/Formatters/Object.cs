using MemoryPack;
using Microsoft.CodeAnalysis;

using System.Buffers;
using System.Reflection;
using LinqDB.Reflection;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=System.Object;
public class Object:MemoryPackFormatter<object>{
    public static readonly Object Instance=new();
    //public static void SerializeObject<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,object? value)where TBufferWriter:IBufferWriter<byte>{
    //    Instance.Serialize(ref writer,ref value);
    //}
    //public static object DeserializeObject(ref Reader reader){
    //    object? value=default;
    //    Instance.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
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
            case sbyte                  v:writer.WriteVarInt(v);break;
            case short                  v:writer.WriteVarInt(v);break;
            case int                    v:writer.WriteVarInt(v);break;
            case long                   v:writer.WriteVarInt(v);break;
            case byte                   v:writer.WriteVarInt(v);break;
            case ushort                 v:writer.WriteVarInt(v);break;
            case uint                   v:writer.WriteVarInt(v);break;
            case ulong                  v:writer.WriteVarInt(v);break;
            case string                 v:writer.WriteString(v);break;
            case System.Delegate        v:Delegate2  .Serialize(ref writer,v);break;
            case Expressions.Expression v:Expression .InternalSerialize(ref writer,v);break;
            case System.Type            v:Type       .Serialize(ref writer,v);break;
            case ConstructorInfo        v:Constructor.Serialize(ref writer,v);break;
            case MethodInfo             v:Method     .Serialize(ref writer,v);break;
            case PropertyInfo           v:Property   .Serialize(ref writer,v);break;
            case EventInfo              v:Event      .Serialize(ref writer,v);break;
            case FieldInfo              v:Field      .Serialize(ref writer,v);break;
            //case MemberInfo             v:Member     .Serialize(ref writer,v);break;
            default:
                Serializer.RegisterAnonymousDisplay(type);
                writer.WriteValue(type,value);
                break;
        }
    }
    internal static T Deserialize(ref Reader reader) {
        T? value = default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
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
        else if(typeof(System.Delegate       ).IsAssignableFrom(type))value=Delegate2  .Deserialize(ref reader);
        else if(typeof(Expressions.Expression).IsAssignableFrom(type))value=Expression .InternalDeserialize(ref reader);
        else if(typeof(System.Type           ).IsAssignableFrom(type))value=Type       .Deserialize(ref reader);
        else if(typeof(ConstructorInfo       ).IsAssignableFrom(type))value=Constructor.Deserialize(ref reader);
        else if(typeof(MethodInfo            ).IsAssignableFrom(type))value=Method     .Deserialize(ref reader);
        else if(typeof(PropertyInfo          ).IsAssignableFrom(type))value=Property   .Deserialize(ref reader);
        else if(typeof(EventInfo             ).IsAssignableFrom(type))value=Event      .Deserialize(ref reader);
        else if(typeof(FieldInfo             ).IsAssignableFrom(type))value=Field      .Deserialize(ref reader);
        //else if(typeof(MemberInfo            ).IsAssignableFrom(type))value=Member     .Deserialize(ref reader);
        else{
            Serializer.RegisterAnonymousDisplay(type);
            reader.ReadValue(type,ref value);
        }
        //else if(typeof(float  )==type){float   Constant_value=0       ;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(double )==type){double  Constant_value=0       ;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(bool   )==type){bool    Constant_value=default!;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(string )==type){value=reader.ReadString(); }
        //else if(typeof(decimal)==type){decimal Constant_value=0       ;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else if(typeof(Guid   )==type){Guid    Constant_value=default!;reader.ReadValue (ref Constant_value);value=Constant_value; }
        //else{
        //    Serializer.RegisterAnonymousDisplay(type);
        //    if     (typeof(Expressions.Expression).IsAssignableFrom(type))value=Expression .Deserialize(ref reader);
        //    else if(typeof(System.Type           ).IsAssignableFrom(type))value=Type       .Deserialize(ref reader);
        //    else if(typeof(MemberInfo            ).IsAssignableFrom(type))value=Member     .Deserialize(ref reader);
        //    else if(typeof(ConstructorInfo       ).IsAssignableFrom(type))value=Constructor.Deserialize(ref reader);
        //    else if(typeof(MethodInfo            ).IsAssignableFrom(type))value=Method     .Deserialize(ref reader);
        //    else if(typeof(PropertyInfo          ).IsAssignableFrom(type))value=Property   .Deserialize(ref reader);
        //    else if(typeof(EventInfo             ).IsAssignableFrom(type))value=Event      .Deserialize(ref reader);
        //    else if(typeof(FieldInfo             ).IsAssignableFrom(type))value=Field      .Deserialize(ref reader);
        //    else reader.ReadValue(type,ref value);
        //    //var Formatter=reader.GetFormatter(type);
        //    //Formatter.Deserialize(ref reader,ref value);
        //}
    }
}
