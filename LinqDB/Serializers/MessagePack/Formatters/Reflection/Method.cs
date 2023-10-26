using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = MethodInfo;
public class Method:IMessagePackFormatter<G>{
    public static readonly Method Instance=new();
    internal static void Write(ref Writer writer,G? value,O Resolver){

        var type=value!.ReflectedType;

        var array=Resolver.Serializer().TypeMethods.Get(type);
        if(value.IsGenericMethod){
            var GenericArguments=value.GetGenericArguments();
            writer.WriteArrayHeader(2+GenericArguments.Length);
            writer.WriteType(type);
            var index=System.Array.IndexOf(array,value.GetGenericMethodDefinition());
            writer.WriteInt32(index);
            foreach(var GenericArgument in GenericArguments){
                
                writer.WriteType(GenericArgument);
            }
        }else{
            writer.WriteArrayHeader(2);
            writer.WriteType(type);
            var index=System.Array.IndexOf(array,value);
            writer.WriteInt32(index);
        }

    }
    internal static void WriteNullable(ref Writer writer,G? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,G? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static G Read(ref Reader reader,O Resolver){
        var count=reader.ReadArrayHeader();
        var type=reader.ReadType();



        var array=Resolver.Serializer().TypeMethods.Get(type);
        var index=reader.ReadInt32();
        var method=array[index];
        if(method.IsGenericMethod){
            var GenericArguments=method.GetGenericArguments();
            for(var a=0;a<GenericArguments.Length;a++){

                GenericArguments[a]=reader.ReadType();
            }
            method=method.MakeGenericMethod(GenericArguments);
        }
        
        return method;
    }
    internal static G? ReadNullable(ref Reader reader,O Resolver)=>reader.TryReadNil()?null:Read(ref reader,Resolver);
    public G Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
