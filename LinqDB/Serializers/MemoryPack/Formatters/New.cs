using MemoryPack;

using System.Buffers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= NewExpression;
using static Common;


public class New:MemoryPackFormatter<T> {
    public static readonly New Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeNew(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        Constructor.Instance.Serialize(ref writer,value.Constructor!);
        SerializeReadOnlyCollection(ref writer,value.Arguments);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var constructor= Constructor.Instance.Deserialize(ref reader);
        var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
        value=System.Linq.Expressions.Expression.New(
            constructor,
            arguments!
        );
    }
}
