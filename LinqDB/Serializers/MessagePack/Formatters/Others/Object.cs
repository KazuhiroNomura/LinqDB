using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters.Others;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = System.Object;
using Reflection;
public class Object :IMessagePackFormatter<T>{
    public static readonly Object Instance = new();
    private static void Write(ref Writer writer, T? value,O Resolver){
        writer.WriteArrayHeader(2);
        var type = value!.GetType();
        writer.WriteType(type);

        switch (value){
            case sbyte v: writer.Write(v); break;
            case short v: writer.Write(v); break;
            case int v: writer.Write(v); break;
            case long v: writer.Write(v); break;
            case byte v: writer.Write(v); break;
            case ushort v: writer.Write(v); break;
            case uint v: writer.Write(v); break;
            case ulong v: writer.Write(v); break;
            case float v: writer.Write(v); break;
            case double v: writer.Write(v); break;
            case bool v: writer.Write(v); break;
            case string v: writer.Write(v); break;
            case Expressions.Expression v: Expression.Write(ref writer, v, Resolver); break;
            case System.Type v: Type.Write(ref writer, v, Resolver); break;
            case ConstructorInfo v: Constructor.Write(ref writer, v, Resolver); break;
            case MethodInfo v: Method.Write(ref writer, v, Resolver); break;
            case PropertyInfo v: Property.Write(ref writer, v, Resolver); break;
            case EventInfo v: Event.Write(ref writer, v, Resolver); break;
            case FieldInfo v: Field.Write(ref writer, v, Resolver); break;
            default:{
                var Formatter = Resolver.GetFormatterDynamic(type)!;
                //nullだとテストで引っかかるようにする。
                if(Formatter==null){

                }
                Serializer.DynamicSerialize(Formatter, ref writer, value, Resolver);
                break;
            }
        }

    }
    internal static void WriteNullable(ref Writer writer, T? value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    public void Serialize(ref Writer writer, T? value,O Resolver)=>WriteNullable(ref writer, value, Resolver);
    private static T Read(ref Reader reader,O Resolver){
        T value;
        var count = reader.ReadArrayHeader();
        Debug.Assert(count==2);
        var type = reader.ReadType();

        if (typeof(sbyte)==type) value=reader.ReadSByte();
        else if (typeof(short)==type) value=reader.ReadInt16();
        else if (typeof(int)==type) value=reader.ReadInt32();
        else if (typeof(long)==type) value=reader.ReadInt64();
        else if (typeof(byte)==type) value=reader.ReadByte();
        else if (typeof(ushort)==type) value=reader.ReadUInt16();
        else if (typeof(uint)==type) value=reader.ReadUInt32();
        else if (typeof(ulong)==type) value=reader.ReadUInt64();
        else if (typeof(float)==type) value=reader.ReadSingle();
        else if (typeof(double)==type) value=reader.ReadDouble();
        else if (typeof(bool)==type) value=reader.ReadBoolean();
        else if (typeof(string)==type) value=reader.ReadString()!;
        //else if (typeof(System.Delegate).IsAssignableFrom(type)) value=Delegate.Read(ref reader, Resolver);
        else if (typeof(Expressions.Expression).IsAssignableFrom(type)) value=Expression.Read(ref reader, Resolver);
        else if (typeof(System.Type).IsAssignableFrom(type)) value=Type.Read(ref reader, Resolver);
        else if (typeof(ConstructorInfo).IsAssignableFrom(type)) value=Constructor.Read(ref reader, Resolver);
        else if (typeof(MethodInfo).IsAssignableFrom(type)) value=Method.Read(ref reader, Resolver);
        else if (typeof(PropertyInfo).IsAssignableFrom(type)) value=Property.Read(ref reader, Resolver);
        else if (typeof(EventInfo).IsAssignableFrom(type)) value=Event.Read(ref reader, Resolver);
        else if (typeof(FieldInfo).IsAssignableFrom(type)) value=Field.Read(ref reader, Resolver);
        else value=reader.Read(type, Resolver);

        return value;
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver) => reader.TryReadNil() ? null : Read(ref reader, Resolver);
    public T Deserialize(ref Reader reader,O Resolver) => ReadNullable(ref reader, Resolver)!;
}
