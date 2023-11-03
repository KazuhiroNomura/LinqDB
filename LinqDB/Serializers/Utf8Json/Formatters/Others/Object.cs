
using System;
using System.Reflection;
using LinqDB.Helpers;
using Utf8Json;



using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = System.Object;
public class Object :IJsonFormatter<T>{
    public static readonly Object Instance = new();
    private static void Write(ref Writer writer, T value,O Resolver){
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
            case Expressions.SymbolDocumentInfo v:writer.WriteSymbolDocumentInfo(v,Resolver);break;
            case Type                   v:writer.WriteType          (v         );break;
            case ConstructorInfo        v:writer.WriteConstructor   (v,Resolver);break;
            case MethodInfo             v:writer.WriteMethod        (v,Resolver);break;
            case PropertyInfo           v:writer.WriteProperty      (v,Resolver);break;
            case EventInfo              v:writer.WriteEvent         (v,Resolver);break;
            case FieldInfo              v:writer.WriteField         (v,Resolver);break;
            default:{
                if(type.IsArray){
                    writer.Write(Resolver.GetFormatterDynamic(type)!,value,Resolver);
                }else if(type.GetIEnumerableT(out var Interface)){
                    var Instance=typeof(Enumerables.IEnumerable<>).MakeGenericType(Interface.GetGenericArguments()).GetValue("Instance");
                    writer.Write(Instance,value,Resolver);
                } else{
                    var Formatter = Resolver.GetFormatterDynamic(type);
                    if(Formatter is not null){
                        writer.Write(Formatter,value,Resolver);
                    } else{
                        writer.Write(value,type,Resolver);
                    }
                }
                break;
            }
        }
        writer.WriteEndArray();
    }
    internal static void WriteNullable(ref Writer writer, T value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    public void Serialize(ref Writer writer, T? value,O Resolver) => WriteNullable(ref writer, value, Resolver);
    private static T Read(ref Reader reader,O Resolver)
    {
        object value;
        reader.ReadIsBeginArrayWithVerify();
        var TypeName= reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        switch(TypeName){
            case"SByte"             :value=reader.ReadSByte             (        );break;
            case"Byte"              :value=reader.ReadByte              (        );break;
            case"Int16"             :value=reader.ReadInt16             (        );break;
            case"UInt16"            :value=reader.ReadUInt16            (        );break;
            case"Int32"             :value=reader.ReadInt32             (        );break;
            case"UInt32"            :value=reader.ReadUInt32            (        );break;
            case"Int64"             :value=reader.ReadInt64             (        );break;
            case"UInt64"            :value=reader.ReadUInt64            (        );break;
            case"Single"            :value=reader.ReadSingle            (        );break;
            case"Double"            :value=reader.ReadDouble            (        );break;
            case"Boolean"           :value=reader.ReadBoolean           (        );break;
            case"Char"              :value=reader.ReadChar              (        );break;
            case"Decimal"           :value=reader.ReadDecimal           (Resolver);break;
            case"TimeSpan"          :value=reader.ReadTimeSpan          (Resolver);break;
            case"DateTime"          :value=reader.ReadDateTime          (Resolver);break;
            case"DateTimeOffset"    :value=reader.ReadDateTimeOffset    (Resolver);break;
            case"SymbolDocumentInfo":value=reader.ReadSymbolDocumentInfo(Resolver);break;
            case"String"            :value=reader.ReadString            (        );break;
            case"Type"              :value=reader.ReadType              (        );break;
            case"ConstructorInfo"   :value=reader.ReadConstructor       (Resolver);break;
            case"MethodInfo"        :value=reader.ReadMethod            (Resolver);break;
            case"PropertyInfo"      :value=reader.ReadProperty          (Resolver);break;
            case"EventInfo"         :value=reader.ReadEvent             (Resolver);break;
            case"FieldInfo"         :value=reader.ReadField             (Resolver);break;
            default:{
                var type=TypeName.StringType();
                if(type.IsArray){
                    value=reader.Read(Resolver.GetFormatterDynamic(type),Resolver);
                }else if(type.GetIEnumerableT(out var Interface)){
                    var Instance=typeof(Enumerables.IEnumerable<>).MakeGenericType(Interface.GetGenericArguments()).GetValue("Instance");
                    value=reader.Read(Instance,Resolver);
                }else if(typeof(Expressions.Expression).IsAssignableFrom(type)){
                    value=reader.ReadExpression(Resolver);
                } else{
                    var Formatter = Resolver.GetFormatterDynamic(type);
                    value=Formatter is not null?reader.Read(Formatter,Resolver):null!;
                }
                //value=typeof(Expressions.Expression).IsAssignableFrom(type)?Expression.Read(ref reader, Resolver):reader.Read(type,Resolver);
                break;
            }
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver) => reader.TryReadNil() ? null : Read(ref reader, Resolver);
    public T Deserialize(ref Reader reader,O Resolver) => ReadNullable(ref reader, Resolver)!;
}
