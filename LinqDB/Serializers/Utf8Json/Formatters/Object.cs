using Utf8Json;

using System.Diagnostics;
using System.Reflection;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using C=Serializer;
public class Object:IJsonFormatter<object>{
    public static readonly Object Instance=new();
    private readonly object[] Objects3=new object[3];
    public void Serialize(ref Writer writer,object? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var type=value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        /*
        if(typeof(Expressions.Expression).IsAssignableFrom(type)){
            var Formatter= Resolver.GetFormatter<Expressions.Expression>();
            //var Formatter = formatterResolver.GetFormatter<LambdaExpression>();
            Formatter.Serialize(ref writer,(Expressions.Expression)(object)value, Resolver);
            //Formatter.Serialize(ref writer,(LambdaExpression)(object)value,formatterResolver);
            //}else if(typeof(T).IsDisplay()){
            //    return Return(new DisplayClassJsonFormatter<T>());
        }else 
        */
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
            default:{
                if     (typeof(Expressions.Expression).IsAssignableFrom(type))Expression .Instance.Serialize(ref writer,(Expressions.Expression)value,Resolver);
                else if(typeof(System.Type           ).IsAssignableFrom(type))Type       .Instance.Serialize(ref writer,(System.Type           )value,Resolver);
                else if(typeof(MemberInfo            ).IsAssignableFrom(type))Member     .Instance.Serialize(ref writer,(MemberInfo            )value,Resolver);
                else if(typeof(ConstructorInfo       ).IsAssignableFrom(type))Constructor.Instance.Serialize(ref writer,(ConstructorInfo       )value,Resolver);
                else if(typeof(MethodInfo            ).IsAssignableFrom(type))Method     .Instance.Serialize(ref writer,(MethodInfo            )value,Resolver);
                else if(typeof(PropertyInfo          ).IsAssignableFrom(type))Property   .Instance.Serialize(ref writer,(PropertyInfo          )value,Resolver);
                else if(typeof(EventInfo             ).IsAssignableFrom(type))Event      .Instance.Serialize(ref writer,(EventInfo             )value,Resolver);
                else if(typeof(FieldInfo             ).IsAssignableFrom(type))Field      .Instance.Serialize(ref writer,(FieldInfo             )value,Resolver);
                else{
                    var Formatter=Resolver.GetFormatterDynamic(type);
                    var Serialize=Formatter.GetType().GetMethod("Serialize");
                    Debug.Assert(Serialize is not null);
                    var Objects3=this.Objects3;
                    Objects3[0]=writer;
                    Objects3[1]=value;
                    Objects3[2]=Resolver;
                    Serialize.Invoke(Formatter,Objects3);
                    writer=(Writer)Objects3[0];
                }
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
        //else if(typeof(decimal)==type)result=global::Utf8Json.Formatters.DecimalFormatter.Default.Deserialize(ref reader,Resolver);
        //else if(typeof(Guid   )==type)result=global::Utf8Json.Formatters.GuidFormatter.Default.Deserialize(ref reader,Resolver);
        else{
            if     (typeof(Expressions.Expression).IsAssignableFrom(type))value=Expression .Instance.Deserialize(ref reader,Resolver);
            else if(typeof(System.Type           ).IsAssignableFrom(type))value=Type       .Instance.Deserialize(ref reader,Resolver);
            else if(typeof(MemberInfo            ).IsAssignableFrom(type))value=Member     .Instance.Deserialize(ref reader,Resolver);
            else if(typeof(ConstructorInfo       ).IsAssignableFrom(type))value=Constructor.Instance.Deserialize(ref reader,Resolver);
            else if(typeof(MethodInfo            ).IsAssignableFrom(type))value=Method     .Instance.Deserialize(ref reader,Resolver);
            else if(typeof(PropertyInfo          ).IsAssignableFrom(type))value=Property   .Instance.Deserialize(ref reader,Resolver);
            else if(typeof(EventInfo             ).IsAssignableFrom(type))value=Event      .Instance.Deserialize(ref reader,Resolver);
            else if(typeof(FieldInfo             ).IsAssignableFrom(type))value=Field      .Instance.Deserialize(ref reader,Resolver);
            else{
                var Formatter=Resolver.GetFormatterDynamic(type);
                var Deserialize=Formatter.GetType().GetMethod("Deserialize");
                Debug.Assert(Deserialize is not null);
                var Objects2=this.Objects2;
                Objects2[0]=reader;
                Objects2[1]=Resolver;
                value=Deserialize.Invoke(Formatter,Objects2)!;
                reader=(Reader)Objects2[0];
            }
            //global::Utf8Json.Formatters.GuidFormatter.Default.Deserialize(ref reader,Resolver);}
            //var Formatter=reader.GetFormatter(type);
            //Formatter.Deserialize(ref reader,ref value);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
