using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
using MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class ListInit:MemoryPackFormatter<Expressions.ListInitExpression>{
    private readonly 必要なFormatters Formatters;
    public ListInit(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.ListInitExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.ListInitExpression DeserializeListInit(ref MemoryPackReader reader){
        Expressions.ListInitExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    private static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit>SerializeInitializers=new();
    //private static readonly ArrayFormatter<ElementInit>DeserializeInitializers=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.ListInitExpression? value){
        this.Formatters.New.Serialize(ref writer,value!.NewExpression);
        var Initializers=value.Initializers;
        SerializeInitializers.Serialize(ref writer,ref Initializers!);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.ListInitExpression? value){
        var New=this.Formatters.New.DeserializeNew(ref reader);
        var Initializers=reader.ReadArray<Expressions.ElementInit>();
        value=Expressions.Expression.ListInit(New,Initializers!);
    }
}
