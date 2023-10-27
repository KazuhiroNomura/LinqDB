
using System;
using System.Reflection;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = System.Object;
using Reflection;
public class Object :IJsonFormatter<G>{
    public static readonly Object Instance = new();
    private static void Write(ref Writer writer, G value,O Resolver){
        writer.WriteBeginArray();
        var type = value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        switch (value){
            case sbyte                  v:writer.WriteSByte         (v         );break;
            case byte                   v:writer.WriteByte          (v         );break;
            case short                  v:writer.WriteInt16         (v         );break;
            case ushort                 v:writer.WriteUInt16        (v         );break;
            case int                    v:writer.WriteInt32         (v         );break;
            case uint                   v:writer.WriteUInt32        (v         );break;
            case long                   v:writer.WriteInt64         (v         );break;
            case ulong                  v:writer.WriteUInt64        (v         );break;
            case float                  v:writer.WriteSingle        (v         );break;
            case double                 v:writer.WriteDouble        (v         );break;
            case bool                   v:writer.WriteBoolean       (v         );break;
            case char                   v:writer.WriteChar          (v         );break;
            case decimal                v:writer.WriteDecimal       (v,Resolver);break;
            case TimeSpan               v:writer.WriteTimeSpan      (v,Resolver);break;
            case DateTime               v:writer.WriteDateTime      (v,Resolver);break;
            case DateTimeOffset         v:writer.WriteDateTimeOffset(v,Resolver);break;
            case string                 v:writer.WriteString        (v         );break;
            case Expressions.Expression v:writer.WriteExpression    (v,Resolver);break;
            case System.Type            v:writer.WriteType          (v         );break;
            case ConstructorInfo        v:writer.WriteConstructor   (v,Resolver);break;
            case MethodInfo             v:writer.WriteMethod        (v,Resolver);break;
            case PropertyInfo           v:writer.WriteProperty      (v,Resolver);break;
            case EventInfo              v:writer.WriteEvent         (v,Resolver);break;
            case FieldInfo              v:writer.WriteField         (v,Resolver);break;
            default:{
                writer.Write(type,value,Resolver);
                break;
            }
        }
        writer.WriteEndArray();
    }




    internal static void WriteNullable(ref Writer writer, G value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    public void Serialize(ref Writer writer, G? value,O Resolver) => WriteNullable(ref writer, value, Resolver);
    private static G Read(ref Reader reader,O Resolver)
    {
        object value;
        reader.ReadIsBeginArrayWithVerify();
        var TypeName= reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        switch(TypeName){
            case"SByte"          :value=reader.ReadSByte         ();break;
            case"Byte"           :value=reader.ReadByte          ();break;
            case"Int16"          :value=reader.ReadInt16         ();break;
            case"UInt16"         :value=reader.ReadUInt16        ();break;
            case"Int32"          :value=reader.ReadInt32         ();break;
            case"UInt32"         :value=reader.ReadUInt32        ();break;
            case"Int64"          :value=reader.ReadInt64         ();break;
            case"UInt64"         :value=reader.ReadUInt64        ();break;
            case"Single"         :value=reader.ReadSingle        ();break;
            case"Double"         :value=reader.ReadDouble        ();break;
            case"Boolean"        :value=reader.ReadBoolean       ();break;
            case"Char"           :value=reader.ReadChar          ();break;
            case"Decimal"        :value=reader.ReadDecimal       (Resolver);break;
            case"TimeSpan"       :value=reader.ReadTimeSpan      (Resolver);break;
            case"DateTime"       :value=reader.ReadDateTime      (Resolver);break;
            case"DateTimeOffset" :value=reader.ReadDateTimeOffset(Resolver);break;
            case"String"         :value=reader.ReadString        ();break;
            //case"Expression"     :value=reader.ReadExpression    (Resolver);break;
            case"Type"           :value=reader.ReadType          ();break;
            case"ConstructorInfo":value=reader.ReadConstructor   (Resolver);break;
            case"MethodInfo"     :value=reader.ReadMethod        (Resolver);break;
            case"PropertyInfo"   :value=reader.ReadProperty      (Resolver);break;
            case"EventInfo"      :value=reader.ReadEvent         (Resolver);break;
            case"FieldInfo"      :value=reader.ReadField         (Resolver);break;
            default:{
                var type=TypeName.StringType();
                value=typeof(Expressions.Expression).IsAssignableFrom(type)?Expression.Read(ref reader, Resolver):reader.Read(type,Resolver);
                break;
            }
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    internal static G? ReadNullable(ref Reader reader,O Resolver) => reader.TryReadNil() ? null : Read(ref reader, Resolver);
    public G Deserialize(ref Reader reader,O Resolver) => ReadNullable(ref reader, Resolver)!;
}
