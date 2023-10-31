using System.Diagnostics;
using System;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters.Others;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = System.Object;
public class Object :IMessagePackFormatter<T>{
    public static readonly Object Instance = new();
    private static void Write(ref Writer writer, T? value,O Resolver){
        writer.WriteArrayHeader(2);
        var type = value!.GetType();
        writer.WriteType(type);

        switch (value){
            case sbyte                  v:writer.WriteInt8          (v                    );break;
            case byte                   v:writer.WriteUInt8         (v                    );break;
            case short                  v:writer.WriteInt16         (v                    );break;
            case ushort                 v:writer.WriteUInt16        (v                    );break;
            case int                    v:writer.WriteInt32         (v                    );break;
            case uint                   v:writer.WriteUInt32        (v                    );break;
            case long                   v:writer.WriteInt64         (v                    );break;
            case ulong                  v:writer.WriteUInt64        (v                    );break;
            case float                  v:writer.Write              (v                    );break;
            case double                 v:writer.Write              (v                    );break;
            case bool                   v:writer.WriteBoolean       (v                    );break;
            case char                   v:writer.WriteChar          (v,Resolver           );break;
            case decimal                v:writer.WriteDecimal       (v,Resolver           );break;
            case TimeSpan               v:writer.WriteTimeSpan      (v,Resolver           );break;
            case DateTime               v:writer.WriteDateTime      (v,Resolver           );break;
            case DateTimeOffset         v:writer.WriteDateTimeOffset(v,Resolver           );break;
            case string                 v:writer.Write              (v                    );break;
            case Expressions.Expression v:writer.WriteExpression    (v,Resolver);break;
            case System.Type            v:writer.WriteType          (v,Resolver);break;
            case ConstructorInfo        v:writer.WriteConstructor   (v,Resolver);break;
            case MethodInfo             v:writer.WriteMethod        (v,Resolver);break;
            case PropertyInfo           v:writer.WriteProperty      (v,Resolver);break;
            case EventInfo              v:writer.WriteEvent         (v,Resolver);break;
            case FieldInfo              v:writer.WriteField         (v,Resolver);break;
            default:{
                var Formatter = Resolver.GetFormatterDynamic(type)!;
                //nullだとテストで引っかかるようにする。
                if(Formatter==null){

                }
                writer.Write(Formatter,value,Resolver);
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
        T? value;
        var count = reader.ReadArrayHeader();
        Debug.Assert(count==2);
        var TypeName= reader.ReadString();
        
        switch(TypeName){
            case"SByte"          :value=reader.ReadSByte         (        );break;
            case"Byte"           :value=reader.ReadByte          (        );break;
            case"Int16"          :value=reader.ReadInt16         (        );break;
            case"UInt16"         :value=reader.ReadUInt16        (        );break;
            case"Int32"          :value=reader.ReadInt32         (        );break;
            case"UInt32"         :value=reader.ReadUInt32        (        );break;
            case"Int64"          :value=reader.ReadInt64         (        );break;
            case"UInt64"         :value=reader.ReadUInt64        (        );break;
            case"Single"         :value=reader.ReadSingle        (        );break;
            case"Double"         :value=reader.ReadDouble        (        );break;
            case"Boolean"        :value=reader.ReadBoolean       (        );break;
            case"Char"           :value=reader.ReadChar          (        );break;
            case"Decimal"        :value=reader.ReadDecimal       (Resolver);break;
            case"TimeSpan"       :value=reader.ReadTimeSpan      (Resolver);break;
            case"DateTime"       :value=reader.ReadDateTime      (        );break;
            case"DateTimeOffset" :value=reader.ReadDateTimeOffset(Resolver);break;
            case"String"         :value=reader.ReadString        (        );break;
            case"Expression"     :value=reader.ReadExpression    (Resolver);break;
            case"Type"           :value=reader.ReadType          (        );break;
            case"ConstructorInfo":value=reader.ReadConstructor   (Resolver);break;
            case"MethodInfo"     :value=reader.ReadMethod        (Resolver);break;
            case"PropertyInfo"   :value=reader.ReadProperty      (Resolver);break;
            case"EventInfo"      :value=reader.ReadEvent         (Resolver);break;
            case"FieldInfo"      :value=reader.ReadField         (Resolver);break;
            default              :{
                var type=TypeName.StringType();
                value=typeof(Expressions.Expression).IsAssignableFrom(type)?Expression.Read(ref reader, Resolver):reader.Read(type,Resolver);
                break;
            }
        }
        return value!;
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver) => reader.TryReadNil() ? null : Read(ref reader, Resolver);
    public T Deserialize(ref Reader reader,O Resolver) => ReadNullable(ref reader, Resolver)!;
}
