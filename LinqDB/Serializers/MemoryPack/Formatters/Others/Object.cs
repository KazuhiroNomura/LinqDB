
using System;
using System.Reflection;

using MemoryPack;
using System.Buffers;
using LinqDB.Helpers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters.Others;


using Reader = MemoryPackReader;
using T = System.Object;
public class Object : MemoryPackFormatter<T>{
    public static readonly Object Instance = new();
    private static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, T value) where TBufferWriter :IBufferWriter<byte>{

        var type = value.GetType();
        writer.WriteType(type);

        switch (value){
            case sbyte                          v:writer.WriteUnmanaged         (v);break;
            case byte                           v:writer.WriteUnmanaged         (v);break;
            case short                          v:writer.WriteUnmanaged         (v);break;
            case ushort                         v:writer.WriteUnmanaged         (v);break;
            case int                            v:writer.WriteUnmanaged         (v);break;
            case uint                           v:writer.WriteUnmanaged         (v);break;
            case long                           v:writer.WriteUnmanaged         (v);break;
            case ulong                          v:writer.WriteUnmanaged         (v);break;
            case float                          v:writer.WriteUnmanaged         (v);break;
            case double                         v:writer.WriteUnmanaged         (v);break;
            case bool                           v:writer.WriteUnmanaged         (v);break;
            case char                           v:writer.WriteUnmanaged         (v);break;
            case decimal                        v:writer.WriteUnmanaged         (v);break;
            case TimeSpan                       v:writer.WriteUnmanaged         (v);break;
            case DateTime                       v:writer.WriteUnmanaged         (v);break;
            case DateTimeOffset                 v:writer.WriteUnmanaged         (v);break;
            case string                         v:writer.WriteString            (v);break;
            case Expressions.Expression         v:writer.WriteExpression        (v);break;
            case Expressions.SymbolDocumentInfo v:writer.WriteSymbolDocumentInfo(v);break;
            case Type                           v:writer.WriteType              (v);break;
            case ConstructorInfo                v:writer.WriteConstructor       (v);break;
            case MethodInfo                     v:writer.WriteMethod            (v);break;
            case PropertyInfo                   v:writer.WriteProperty          (v);break;
            case EventInfo                      v:writer.WriteEvent             (v);break;
            case FieldInfo                      v:writer.WriteField             (v);break;
            default:{
                if(type.IsArray){
                    writer.Write(writer.GetFormatter(type),value);
                }else if(type.GetIEnumerableT(out var Interface)){
                    var Instance=typeof(Enumerables.IEnumerable<>).MakeGenericType(Interface.GetGenericArguments()).GetValue("Instance");
                    writer.Write(Instance,value);
                } else{
                    var Formatter=FormatterResolver.GetFormatterDynamic(type);
                    if(Formatter is not null){
                        writer.Write(Formatter,value);
                    } else{
                        writer.Write(writer.GetFormatter(type),value);
                    }
                }
                break;
            }
        }
        
    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, T? value) where TBufferWriter :IBufferWriter<byte>{
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref object? value) => WriteNullable(ref writer, value);
    private static T Read(ref Reader reader){
        T? value;


        var TypeName= reader.ReadString();
        
        switch(TypeName){
            case"SByte"             :{reader.ReadUnmanaged(out sbyte          value0);value=value0;break;}
            case"Byte"              :{reader.ReadUnmanaged(out byte           value0);value=value0;break;}
            case"Int16"             :{reader.ReadUnmanaged(out short          value0);value=value0;break;}
            case"UInt16"            :{reader.ReadUnmanaged(out ushort         value0);value=value0;break;}
            case"Int32"             :{reader.ReadUnmanaged(out int            value0);value=value0;break;}
            case"UInt32"            :{reader.ReadUnmanaged(out uint           value0);value=value0;break;}
            case"Int64"             :{reader.ReadUnmanaged(out long           value0);value=value0;break;}
            case"UInt64"            :{reader.ReadUnmanaged(out ulong          value0);value=value0;break;}
            case"Single"            :{reader.ReadUnmanaged(out float          value0);value=value0;break;}
            case"Double"            :{reader.ReadUnmanaged(out double         value0);value=value0;break;}
            case"Boolean"           :{reader.ReadUnmanaged(out bool           value0);value=value0;break;}
            case"Char"              :{reader.ReadUnmanaged(out char           value0);value=value0;break;}
            case"Decimal"           :{reader.ReadUnmanaged(out decimal        value0);value=value0;break;}
            case"TimeSpan"          :{reader.ReadUnmanaged(out TimeSpan       value0);value=value0;break;}
            case"DateTime"          :{reader.ReadUnmanaged(out DateTime       value0);value=value0;break;}
            case"DateTimeOffset"    :{reader.ReadUnmanaged(out DateTimeOffset value0);value=value0;break;}
            case"SymbolDocumentInfo":value=reader.ReadSymbolDocumentInfo()!;break;
            case"String"            :value=reader.ReadString()!;break;
            case"Type"              :value=reader.ReadType          ();break;
            case"ConstructorInfo"   :value=reader.ReadConstructor   ();break;
            case"MethodInfo"        :value=reader.ReadMethod        ();break;
            case"PropertyInfo"      :value=reader.ReadProperty      ();break;
            case"EventInfo"         :value=reader.ReadEvent         ();break;
            case"FieldInfo"         :value=reader.ReadField         ();break;
            default:{
                var type=TypeName.StringType();
                if(type.IsArray){
                    value=reader.Read(reader.GetFormatter(type));
                }else if(type.GetIEnumerableT(out var Interface)){
                    var Instance=typeof(Enumerables.IEnumerable<>).MakeGenericType(Interface.GetGenericArguments()).GetValue("Instance");
                    value=reader.Read(Instance);
                }else if(typeof(Expressions.Expression).IsAssignableFrom(type)){
                    value=reader.ReadExpression();
                } else{
                    value=reader.Read(FormatterResolver.GetFormatterDynamic(type)??reader.GetFormatter(type));
                }
                //value=typeof(Expressions.Expression).IsAssignableFrom(type)?reader.ReadExpression():reader.Read(type);
                break;
            }
        }
        return value!;
    }
    internal static T? ReadNullable(ref Reader reader) => reader.TryReadNil() ? null : Read(ref reader);
    public override void Deserialize(ref Reader reader, scoped ref T? value) => value=ReadNullable(ref reader);
}
