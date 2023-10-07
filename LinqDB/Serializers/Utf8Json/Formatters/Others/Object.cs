using System.Reflection;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = System.Object;
using Reflection;
public class Object :IJsonFormatter<G>{
    public static readonly Object Instance = new();
    private static void Write(ref Writer writer, G value, IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        var type = value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        switch (value){
            case sbyte v: writer.WriteSByte(v); break;
            case short v: writer.WriteInt16(v); break;
            case int v: writer.WriteInt32(v); break;
            case long v: writer.WriteInt64(v); break;
            case byte v: writer.WriteByte(v); break;
            case ushort v: writer.WriteUInt16(v); break;
            case uint v: writer.WriteUInt32(v); break;
            case ulong v: writer.WriteUInt64(v); break;
            case float v:writer.WriteSingle(v); break;
            case double v: writer.WriteDouble(v); break;
            case bool v:writer.WriteBoolean(v); break;
            case string v: writer.WriteString(v); break;
            case Expressions.Expression v: Expression.Write(ref writer, v, Resolver); break;
            case System.Type v: Type.Write(ref writer, v, Resolver); break;
            case ConstructorInfo v: Constructor.Write(ref writer, v, Resolver); break;
            case MethodInfo v: Method.Write(ref writer, v, Resolver); break;
            case PropertyInfo v: Property.Write(ref writer, v, Resolver); break;
            case EventInfo v: Event.Write(ref writer, v, Resolver); break;
            case FieldInfo v: Field.Write(ref writer, v, Resolver); break;
            default:{
                writer.Write(type,value,Resolver);
                
                break;
            }
        }
        writer.WriteEndArray();
    }
    internal static void WriteNullable(ref Writer writer, G value, IJsonFormatterResolver Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    public void Serialize(ref Writer writer, G? value, IJsonFormatterResolver Resolver) => WriteNullable(ref writer, value, Resolver);
    private static G Read(ref Reader reader, IJsonFormatterResolver Resolver)
    {
        object value;
        reader.ReadIsBeginArrayWithVerify();

        var type = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
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
        else if (typeof(string)==type) value=reader.ReadString();
        //else if (typeof(System.Delegate).IsAssignableFrom(type)) value=Delegate.Read(ref reader, Resolver);
        else if (typeof(Expressions.Expression).IsAssignableFrom(type)) value=Expression.Read(ref reader, Resolver);
        else if (typeof(System.Type).IsAssignableFrom(type)) value=Type.Read(ref reader, Resolver);
        else if (typeof(ConstructorInfo).IsAssignableFrom(type)) value=Constructor.Read(ref reader, Resolver);
        else if (typeof(MethodInfo).IsAssignableFrom(type)) value=Method.Read(ref reader, Resolver);
        else if (typeof(PropertyInfo).IsAssignableFrom(type)) value=Property.Read(ref reader, Resolver);
        else if (typeof(EventInfo).IsAssignableFrom(type)) value=Event.Read(ref reader, Resolver);
        else if (typeof(FieldInfo).IsAssignableFrom(type)) value=Field.Read(ref reader, Resolver);
        else value=reader.Read(type, Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    internal static G? ReadNullable(ref Reader reader, IJsonFormatterResolver Resolver) => reader.TryReadNil() ? null : Read(ref reader, Resolver);
    public G Deserialize(ref Reader reader, IJsonFormatterResolver Resolver) => ReadNullable(ref reader, Resolver)!;
}
