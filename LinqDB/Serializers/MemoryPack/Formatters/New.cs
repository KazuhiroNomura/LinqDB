using LinqDB.Serializers.MemoryPack.Formatters.Reflection;
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.NewExpression;
using static Extension;


public class New:MemoryPackFormatter<T> {
    public static readonly New Instance=new();
    internal static void WriteNew<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        Constructor.Write(ref writer,value.Constructor!);
        writer.WriteCollection(value.Arguments);
    }
    //internal static void InternalSerializeNew<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
    //    writer.WriteNodeType(ExpressionType.New);
    //    PrivateWrite(ref writer,value);
    //}
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.New);
        WriteNew(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        WriteNew(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var constructor= Constructor.Read(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        return Expressions.Expression.New(
            constructor,
            arguments!
        );
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
