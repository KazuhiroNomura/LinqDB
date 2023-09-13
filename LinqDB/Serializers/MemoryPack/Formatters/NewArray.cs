using System;
using MemoryPack;
using System.Linq.Expressions;
using System.Buffers;
using System.Diagnostics;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Extension;
using T=NewArrayExpression;
using C=Serializer;
public class NewArray:MemoryPackFormatter<T> {
    public static readonly NewArray Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeNewArray(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteNodeType(value.NodeType);
        Type.Serialize(ref writer,value.Type.GetElementType());
        writer.SerializeReadOnlyCollection(value.Expressions);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var NodeType=reader.ReadNodeType();
        var type= Type.Deserialize(ref reader);
        //var expressions=global::MemoryPack.Formatters.ArrayFormatter<Expression>() Deserialize_T<Expression[]>(ref reader);
        var expressions=reader.ReadArray<System.Linq.Expressions.Expression>();
        value=NodeType switch{
            ExpressionType.NewArrayBounds=>System.Linq.Expressions.Expression.NewArrayBounds(type,expressions!),
            ExpressionType.NewArrayInit=>System.Linq.Expressions.Expression.NewArrayInit(type,expressions!),
            _=>throw new NotImplementedException(NodeType.ToString())
        };
    }
}
