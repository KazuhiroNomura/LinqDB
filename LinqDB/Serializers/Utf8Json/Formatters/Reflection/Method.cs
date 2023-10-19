
using System.Reflection;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = MethodInfo;
public class Method:IJsonFormatter<G>{
    public static readonly Method Instance=new();
    internal static void Write(ref Writer writer,G value,O Resolver){
        writer.WriteBeginArray();
        var type=value.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        var array=Resolver.Serializer().TypeMethods.Get(type);
        if(value.IsGenericMethod){
            var index=System.Array.IndexOf(array,value.GetGenericMethodDefinition());
            writer.WriteInt32(index);
            foreach(var GenericArguments in value.GetGenericArguments()){
                writer.WriteValueSeparator();
                writer.WriteType(GenericArguments);
            }
        }else{
            
            
            var index=System.Array.IndexOf(array,value);
            writer.WriteInt32(index);
        }
        writer.WriteEndArray();
    }
    internal static void WriteNullable(ref Writer writer,G? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,G? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static G Read(ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var array=Resolver.Serializer().TypeMethods.Get(type);
        var index=reader.ReadInt32();
        var method=array[index];
        if(method.IsGenericMethod){
            var GenericArguments=method.GetGenericArguments();
            for(var a=0;a<GenericArguments.Length;a++){
                reader.ReadIsValueSeparatorWithVerify();
                GenericArguments[a]=reader.ReadType();
            }
            method=method.MakeGenericMethod(GenericArguments);
        }
        reader.ReadIsEndArrayWithVerify();
        return method;
    }
    internal static G? ReadNullable(ref Reader reader,O Resolver)=>reader.TryReadNil()?null:Read(ref reader,Resolver);
    public G Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
