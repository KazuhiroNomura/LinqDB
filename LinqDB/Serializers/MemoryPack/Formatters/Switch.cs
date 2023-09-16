using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Extension;
using T=Expressions.SwitchExpression;
public class Switch:MemoryPackFormatter<T> {
    public static readonly Switch Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value!.Type);
        Expression.Write(ref writer,value.SwitchValue);
        Method.InternalSerializeNullable(ref writer,value.Comparison);
        writer.SerializeReadOnlyCollection(value.Cases);
        Expression.SerializeNullable(ref writer,value.DefaultBody);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Switch);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var type       = reader.ReadType();
        var switchValue= Expression.Read(ref reader);
        var comparison = Method.InternalDeserializeNullable(ref reader);
        var cases      =reader.ReadArray<Expressions.SwitchCase>();
        var defaultBody= Expression.InternalDeserializeNullable(ref reader);
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Read(ref reader);
    }
}
