using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using MemoryPack.Formatters;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.ListInitExpression;



public class ListInit:MemoryPackFormatter<T> {
    public static readonly ListInit Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeListInit(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    private static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit>Formatter=new();
    //private static readonly ArrayFormatter<ElementInit>DeserializeInitializers=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        New.Serialize(ref writer,value!.NewExpression);
        var Initializers=value.Initializers;
        Formatter.Serialize(ref writer,ref Initializers!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var @new=New.DeserializeNew(ref reader);
        var Initializers=reader.ReadArray<Expressions.ElementInit>();
        value=Expressions.Expression.ListInit(@new,Initializers!);
    }
}
