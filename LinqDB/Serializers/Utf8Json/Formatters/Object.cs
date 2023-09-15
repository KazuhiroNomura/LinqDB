using Utf8Json;

using System.Diagnostics;
using System.Reflection;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = System.Object;
public class Object:IJsonFormatter<T>{
    public static readonly Object Instance=new();
    private readonly object[] Objects3=new object[3];
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
      
        var type=value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        switch(value){
            case sbyte   v:writer.WriteSByte (v);break;
            case short   v:writer.WriteInt16 (v);break;
            case int     v:writer.WriteInt32 (v);break;
            case long    v:writer.WriteInt64 (v);break;
            case byte    v:writer.WriteByte  (v);break;
            case ushort  v:writer.WriteUInt16(v);break;
            case uint    v:writer.WriteUInt32(v);break;
            case ulong   v:writer.WriteUInt64(v);break;
            case string  v:writer.WriteString(v);break;
            case System.Delegate        v:Delegate  .Instance.Serialize(ref writer,v,Resolver);break;
            case Expressions.Expression v:Expression .Instance.Serialize(ref writer,v,Resolver);break;
            case System.Type            v:Type       .Instance.Serialize(ref writer,v,Resolver);break;
            case ConstructorInfo        v:Constructor.Instance.Serialize(ref writer,v,Resolver);break;
            case MethodInfo             v:Method     .Instance.Serialize(ref writer,v,Resolver);break;
            case PropertyInfo           v:Property   .Instance.Serialize(ref writer,v,Resolver);break;
            case EventInfo              v:Event      .Instance.Serialize(ref writer,v,Resolver);break;
            case FieldInfo              v:Field      .Instance.Serialize(ref writer,v,Resolver);break;
            case MemberInfo             v:Member     .Instance.Serialize(ref writer,v,Resolver);break;
            default:{
                var Formatter=Resolver.GetFormatterDynamic(type);
                //var Formatter=Resolver.GetFormatterDynamic(type);
                var Serialize=Formatter.GetType().GetMethod("Serialize");
                Debug.Assert(Serialize is not null);
                var Objects3=this.Objects3;
                Objects3[0]=writer;
                Objects3[1]=value;
                Objects3[2]=Resolver;
                Serialize.Invoke(Formatter,Objects3);
                writer=(Writer)Objects3[0];
                break;
            }
        }
        writer.WriteEndArray();
    }
    private readonly object[] Objects2=new object[2];
    public object Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        object value;
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        //object? value=default;
        if     (typeof(sbyte  )==type)value=reader.ReadSByte();
        else if(typeof(short  )==type)value=reader.ReadInt16();
        else if(typeof(int    )==type)value=reader.ReadInt32();
        else if(typeof(long   )==type)value=reader.ReadInt64();
        else if(typeof(byte   )==type)value=reader.ReadByte();
        else if(typeof(ushort )==type)value=reader.ReadUInt16();
        else if(typeof(uint   )==type)value=reader.ReadUInt32();
        else if(typeof(ulong  )==type)value=reader.ReadUInt64();
        else if(typeof(float  )==type)value=reader.ReadSingle();
        else if(typeof(double )==type)value=reader.ReadDouble();
        else if(typeof(bool   )==type)value=reader.ReadBoolean();
        else if(typeof(string )==type)value=reader.ReadString();
        else if(typeof(System.Delegate       ).IsAssignableFrom(type))value=Delegate  .Instance.Deserialize(ref reader,Resolver);
        //else if(typeof(decimal)==type)result=global::Utf8Json.Formatters.DecimalFormatter.Default.Deserialize(ref reader,Resolver);
        //else if(typeof(Guid   )==type)result=global::Utf8Json.Formatters.GuidFormatter.Default.Deserialize(ref reader,Resolver);
        else if(typeof(Expressions.Expression).IsAssignableFrom(type))value=Expression .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(System.Type           ).IsAssignableFrom(type))value=Type       .Instance.Deserialize(ref reader,Resolver);
        //else if(typeof(MemberInfo            ).IsAssignableFrom(type))value=Member     .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(ConstructorInfo       ).IsAssignableFrom(type))value=Constructor.Instance.Deserialize(ref reader,Resolver);
        else if(typeof(MethodInfo            ).IsAssignableFrom(type))value=Method     .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(PropertyInfo          ).IsAssignableFrom(type))value=Property   .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(EventInfo             ).IsAssignableFrom(type))value=Event      .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(FieldInfo             ).IsAssignableFrom(type))value=Field      .Instance.Deserialize(ref reader,Resolver);
        else{
            //var Formatter=Resolver.GetFormatterDynamic(type);
            //var Deserialize=Formatter.GetType().GetMethod("Deserialize");
            //Debug.Assert(Deserialize is not null);
            //var Objects2=this.Objects2;
            //Objects2[0]=reader;
            //Objects2[1]=Resolver;
            //value=Deserialize.Invoke(Formatter,Objects2)!;
            //reader=(Reader)Objects2[0];
            value=reader.ReadValue(type,this.Objects2,Resolver);
        }
            //global::Utf8Json.Formatters.GuidFormatter.Default.Deserialize(ref reader,Resolver);}
            //var Formatter=reader.GetFormatter(type);
            //Formatter.Deserialize(ref reader,ref value);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
