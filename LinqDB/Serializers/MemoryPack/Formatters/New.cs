using MemoryPack;

using System.Buffers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= NewExpression;
using static Extension;


public class New:MemoryPackFormatter<T> {
    public static readonly New Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeNew(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        Constructor.Serialize(ref writer,value.Constructor!);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var constructor= Constructor.Deserialize(ref reader);
        var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
        value=System.Linq.Expressions.Expression.New(
            constructor,
            arguments!
        );
    }
}
