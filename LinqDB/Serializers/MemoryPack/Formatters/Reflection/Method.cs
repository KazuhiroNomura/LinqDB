

using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters.Reflection;

using Reader = MemoryPackReader;
using T = System.Reflection.MethodInfo;

public class Method:MemoryPackFormatter<T>{
    public static readonly Method Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{

        var type=value.ReflectedType!;
        writer.WriteType(type);



        var array=writer.Serializer().TypeMethods.Get(type);
        if(value.IsGenericMethod){
            var index=System.Array.IndexOf(array,value.GetGenericMethodDefinition());
            writer.WriteVarInt(index);
            foreach(var GenericArguments in value.GetGenericArguments()){
                
                writer.WriteType(GenericArguments);
            }
        }else{
            
            
            var index=System.Array.IndexOf(array,value);
            writer.WriteVarInt(index);
        }

    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)=>WriteNullable(ref writer,value);
    internal static T Read(ref Reader reader){

        var type=reader.ReadType();



        var array=reader.Serializer().TypeMethods.Get(type);
        var index=reader.ReadVarIntInt32();
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
    internal static T? ReadNullable(ref Reader reader)=>reader.TryReadNil()?null:Read(ref reader);
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=ReadNullable(ref reader);
}
