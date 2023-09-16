using System.Diagnostics;
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.NewExpression;
using static Extension;


public class New:MemoryPackFormatter<T> {
    public static readonly New Instance=new();
    internal static void InternalSerializeNew<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        Constructor.Write(ref writer,value.Constructor!);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    //internal static void InternalSerializeNew<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
    //    writer.WriteNodeType(ExpressionType.New);
    //    PrivateSerialize(ref writer,value);
    //}
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.New);
        InternalSerializeNew(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        InternalSerializeNew(ref writer,value);
    }
    internal static T InternaDeserialize(ref Reader reader){
        var constructor= Constructor.Read(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        return System.Linq.Expressions.Expression.New(
            constructor,
            arguments!
        );
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternaDeserialize(ref reader);
    }
}
