using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.LabelExpression;
public class Label:MemoryPackFormatter<T> {
    public static readonly Label Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        LabelTarget.Write(ref writer,value!.Target);
        
        Expression.WriteNullable(ref writer,value.DefaultValue);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        
        writer.WriteNodeType(Expressions.ExpressionType.Label);
        
        PrivateWrite(ref writer,value);

    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        PrivateWrite(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var target= LabelTarget.Read(ref reader);
        var defaultValue= Expression.ReadNullable(ref reader);
        
        return Expressions.Expression.Label(target,defaultValue);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
