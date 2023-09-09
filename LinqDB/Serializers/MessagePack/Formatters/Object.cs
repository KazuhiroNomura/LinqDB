using MessagePack.Formatters;
using MessagePack;
using System.Diagnostics;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=System.Object;
public class Object:IMessagePackFormatter<object>{
    public static readonly Object Instance=new();
    private const int ArrayHeader=2;
    //private const int InternalArrayHeader=ArrayHeader+1;
    public void Serialize(ref Writer writer,object? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        var type=value!.GetType();
        writer.WriteType(type);
        //writer.WriteValue(value.Value.GetType(),value.Value);
        //this.Serialize(ref writer,value.Type);
        switch(value){
            case sbyte   v:writer.Write(v         );break;
            case short   v:writer.Write(v         );break;
            case int     v:writer.Write(v         );break;
            case long    v:writer.Write(v         );break;
            case byte    v:writer.Write(v         );break;
            case ushort  v:writer.Write(v         );break;
            case uint    v:writer.Write(v         );break;
            case ulong   v:writer.Write(v         );break;
            case string  v:writer.Write(v         );break;
            default:{
                var Formatter=Resolver.Resolver.GetFormatterDynamic(type);
                Serializer.DynamicSerialize(Formatter,ref writer,value,Resolver);break;
            }
        }
    }
    public object Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        //if(reader.TryReadNil()) value=null;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        //object? value;
        if     (typeof(sbyte  )==type)return reader.ReadSByte();
        else if(typeof(short  )==type)return reader.ReadInt16();
        else if(typeof(int    )==type)return reader.ReadInt32();
        else if(typeof(long   )==type)return reader.ReadInt64();
        else if(typeof(byte   )==type)return reader.ReadByte();
        else if(typeof(ushort )==type)return reader.ReadUInt16();
        else if(typeof(uint   )==type)return reader.ReadUInt32();
        else if(typeof(ulong  )==type)return reader.ReadUInt64();
        else if(typeof(float  )==type)return reader.ReadSingle();
        else if(typeof(double )==type)return reader.ReadDouble();
        else if(typeof(bool   )==type)return reader.ReadBoolean();
        else if(typeof(string )==type)return reader.ReadString()!;
        else{
            var Formatter=Resolver.Resolver.GetFormatterDynamic(type);
            return Serializer.DynamicDeserialize(Formatter,ref reader,Resolver);
        }
    }
}
