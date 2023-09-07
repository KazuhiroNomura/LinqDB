using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using MemoryPack.Formatters;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.ListInitExpression;



public class ListInit:MemoryPackFormatter<T> {
    public static readonly ListInit Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeListInit(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    private static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit>Formatter=new();
    //private static readonly ArrayFormatter<ElementInit>DeserializeInitializers=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        New.Instance.Serialize(ref writer,value!.NewExpression);
        var Initializers=value.Initializers;
        Formatter.Serialize(ref writer,ref Initializers!);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        var @new=New.Instance.DeserializeNew(ref reader);
        var Initializers=reader.ReadArray<Expressions.ElementInit>();
        value=Expressions.Expression.ListInit(@new,Initializers!);
    }
}
