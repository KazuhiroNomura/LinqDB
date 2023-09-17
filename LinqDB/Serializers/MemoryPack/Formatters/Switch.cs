
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Extension;
using T=Expressions.SwitchExpression;
public class Switch:MemoryPackFormatter<T> {
    public static readonly Switch Instance=new();
    
    
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value!.Type);
        
        Expression.Write(ref writer,value.SwitchValue);
        
        Method.WriteNullable(ref writer,value.Comparison);
        
        writer.SerializeReadOnlyCollection(value.Cases);
        
        Expression.WriteNullable(ref writer,value.DefaultBody);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        
        writer.WriteNodeType(Expressions.ExpressionType.Switch);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        PrivateSerialize(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var type       = reader.ReadType();
        
        var switchValue= Expression.Read(ref reader);
        
        var comparison = Method.ReadNullable(ref reader);
        
        var cases      =reader.ReadArray<Expressions.SwitchCase>();
        
        var defaultBody= Expression.ReadNullable(ref reader);
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        
        
        value=Read(ref reader);
    }
}
