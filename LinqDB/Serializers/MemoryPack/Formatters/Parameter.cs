
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader=MemoryPackReader;
using T=Expressions.ParameterExpression;
public class Parameter:MemoryPackFormatter<T> {
    public static readonly Parameter Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Parameter);
        
        var index=writer.Serializer().ListParameter.LastIndexOf(value);

        writer.WriteVarInt(index);
        if(index<0){
            
            writer.WriteType(value.Type);
            
            writer.WriteString(value.Name);
        }
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        writer.WriteType(value!.Type);

        writer.WriteString(value!.Name);

    }
    internal static T Read(ref Reader reader){
        var index=reader.ReadVarIntInt32();
        var ListParameter=reader.Serializer().ListParameter;
        if(index>=0) return ListParameter[index];


        
        var type=reader.ReadType();
        
        var name=reader.ReadString();
        var Parameter=Expressions.Expression.Parameter(type,name);
        ListParameter.Add(Parameter);
        return Parameter;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;

        var type=reader.ReadType();

        var name=reader.ReadString();
        value=Expressions.Expression.Parameter(type,name);
    }
}
