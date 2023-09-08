using System.Buffers;
using System.Diagnostics;
using System.Linq.Expressions;

using MemoryPack;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using static Common;
using C=Serializer;
using T= BlockExpression;

public class Block:MemoryPackFormatter<T> {
    public static readonly Block Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeBlock(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        var ListParameter= C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value!.Variables;
        ListParameter.AddRange(Variables);
        var type=value.Type;
        Type.Instance.Serialize(ref writer,ref type);
        //var Variables=value.Variables;
        Serialize宣言Parameters(ref writer,Variables);
        SerializeReadOnlyCollection(ref writer,value.Expressions);
        Debug.Assert(Variables!=null,nameof(Variables)+" != null");
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var ListParameter= C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type= Type.Instance.Deserialize(ref reader);
        var variables= Deserialize宣言Parameters(ref reader);
        ListParameter.AddRange(variables!);
        var expressions=reader.ReadArray<Expressions.Expression>();
        ListParameter.RemoveRange(ListParameter_Count,variables!.Length);
        value=Expressions.Expression.Block(type,variables!,expressions!);
    }
}
