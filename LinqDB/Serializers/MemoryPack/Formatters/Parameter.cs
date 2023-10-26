
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader=MemoryPackReader;
using T=Expressions.ParameterExpression;
public class Parameter:MemoryPackFormatter<T> {
    public static readonly Parameter Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        var Serializer=writer.Serializer();
        var index0=Serializer.Parameters.LastIndexOf(value);
        writer.WriteVarInt(index0);
        if(index0<0){

            var index1=Serializer.ラムダ跨ぎParameters.LastIndexOf(value);
            writer.WriteVarInt(index1);
            if(index1<0){

                writer.WriteString(value.Name);
                
                writer.WriteType(value.Type);
                
                writer.WriteBoolean(value.IsByRef);
                Serializer.ラムダ跨ぎParameters.Add(value);
            }
        }






        
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Parameter);
 
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        PrivateWrite(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var Serializer=reader.Serializer();
        var index0=reader.ReadVarIntInt32();
        if(index0<0){
            
            var index1=reader.ReadVarIntInt32();
            if(index1<0){

                var name=reader.ReadString();
                var type=reader.ReadType();
                var IsByRef=reader.ReadBoolean();
                var Parameter=Expressions.Expression.Parameter(IsByRef?type.MakeByRefType():type,name);
                Serializer.ラムダ跨ぎParameters.Add(Parameter);
                return Parameter;
            } else{
                
                
                return Serializer.ラムダ跨ぎParameters[index1];
            }
        }else{
            
            return Serializer.Parameters[index0];
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;

        value=Read(ref reader);
        
        
    }
}
