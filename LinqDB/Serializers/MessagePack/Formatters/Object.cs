using MessagePack.Formatters;
using MessagePack;
using System.Diagnostics;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using LinqDB.Helpers;

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
            case sbyte   v:writer.Write(v);break;
            case short   v:writer.Write(v);break;
            case int     v:writer.Write(v);break;
            case long    v:writer.Write(v);break;
            case byte    v:writer.Write(v);break;
            case ushort  v:writer.Write(v);break;
            case uint    v:writer.Write(v);break;
            case ulong   v:writer.Write(v);break;
            case float   v:writer.Write(v);break;
            case double  v:writer.Write(v);break;
            case bool    v:writer.Write(v);break;
            case string  v:writer.Write(v);break;
            case System.Delegate        v:Delegate2  .Instance.Serialize(ref writer,v,Resolver);break;
            case Expressions.Expression v:Expression .Instance.Serialize(ref writer,v,Resolver);break;
            case System.Type            v:Type       .Instance.Serialize(ref writer,v,Resolver);break;
            case ConstructorInfo        v:Constructor.Instance.Serialize(ref writer,v,Resolver);break;
            case MethodInfo             v:Method     .Instance.Serialize(ref writer,v,Resolver);break;
            case PropertyInfo           v:Property   .Instance.Serialize(ref writer,v,Resolver);break;
            case EventInfo              v:Event      .Instance.Serialize(ref writer,v,Resolver);break;
            case FieldInfo              v:Field      .Instance.Serialize(ref writer,v,Resolver);break;
            case MemberInfo             v:Member     .Instance.Serialize(ref writer,v,Resolver);break;
            default:{
                //object Formatter;
                //if(type.IsDisplay()){
                //    //あらかじめ設定してあるResolverに設定する
                //    var FormatterType=typeof(DisplayClass<>).MakeGenericType(type);
                //    var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
                //    Formatter=Instance.GetValue(null)!;
                //} else{
                //    Formatter=Resolver.Resolver.GetFormatterDynamic(type)!;
                //}
                var Formatter=Resolver.Resolver.GetFormatterDynamic(type)!;
                Serializer.DynamicSerialize(Formatter,ref writer,value,Resolver);
                //writer.WriteValue(value,Resolver);
                //object Formatter;
                //if(type.IsDisplay()){
                //    if(DisplayClass.DictionarySerialize.TryGetValue(type,out var Foramtter)) return;
                //    var FormatterType = typeof(DisplayClass<>).MakeGenericType(type);
                //    Formatter=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
                //} else{
                //    Formatter=Serializer.Instance.Options.Resolver.GetFormatterDynamic(type)!;
                //}
                break;
            }
        }
    }
    public object Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        object value;
        //if(reader.TryReadNil()) return null!;
        //if(reader.TryReadNil()) value=null;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        //object? value;
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
        else if(typeof(string )==type)value=reader.ReadString()!;
        else if(typeof(System.Delegate       ).IsAssignableFrom(type))value=Delegate2  .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(Expressions.Expression).IsAssignableFrom(type))value=Expression .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(System.Type           ).IsAssignableFrom(type))value=Type       .Instance.Deserialize(ref reader,Resolver);
        //else if(typeof(MemberInfo            ).IsAssignableFrom(type))value=Member     .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(ConstructorInfo       ).IsAssignableFrom(type))value=Constructor.Instance.Deserialize(ref reader,Resolver);
        else if(typeof(MethodInfo            ).IsAssignableFrom(type))value=Method     .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(PropertyInfo          ).IsAssignableFrom(type))value=Property   .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(EventInfo             ).IsAssignableFrom(type))value=Event      .Instance.Deserialize(ref reader,Resolver);
        else if(typeof(FieldInfo             ).IsAssignableFrom(type))value=Field      .Instance.Deserialize(ref reader,Resolver);
        else{
            //reader.ReadValue(ref value);
            var Formatter=Resolver.Resolver.GetFormatterDynamic(type);
            value=Serializer.DynamicDeserialize(Formatter,ref reader,Resolver);
        }
        return value;
    }
}
