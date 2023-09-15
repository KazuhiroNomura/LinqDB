using System.Buffers;
using System.Diagnostics;
//using System.Linq.Expressions;

using MemoryPack;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;

using static Extension;
using C = Serializer;
using T = Expressions.BlockExpression;

public class Block:MemoryPackFormatter<T> {
    public static readonly Block Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        Instance.Serialize(ref writer,ref value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        var ListParameter= writer.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value!.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteType(value.Type);
        //var Variables=value.Variables;
        writer.Serialize宣言Parameters(Variables);
        writer.SerializeReadOnlyCollection(value.Expressions);
        Debug.Assert(Variables!=null,nameof(Variables)+" != null");
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var ListParameter= reader.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type= reader.ReadType();
        var variables= reader.Deserialize宣言Parameters();
        ListParameter.AddRange(variables!);
        var expressions=reader.ReadArray<Expressions.Expression>();
        ListParameter.RemoveRange(ListParameter_Count,variables!.Length);
        value=Expressions.Expression.Block(type,variables!,expressions!);
    }
}
