using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.ListInitExpression;



public class ListInit:MemoryPackFormatter<T> {
    public static readonly ListInit Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        New.WriteNew(ref writer,value!.NewExpression);
        writer.WriteCollection(value.Initializers);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.ListInit);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        PrivateSerialize(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var @new=New.Read(ref reader);
        var Initializers=reader.ReadArray<Expressions.ElementInit>();
        return Expressions.Expression.ListInit(@new,Initializers!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
