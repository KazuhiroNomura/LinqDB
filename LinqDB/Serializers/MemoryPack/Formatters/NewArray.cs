using System;
using MemoryPack;
using System.Linq.Expressions;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Common;
using T=NewArrayExpression;
using C=Serializer;
public class NewArray:MemoryPackFormatter<T> {
    public static readonly NewArray Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeNewArray(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteNodeType(value.NodeType);
        Type.Instance.Serialize(ref writer,value.Type.GetElementType());
        SerializeReadOnlyCollection(ref writer,value.Expressions);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var NodeType=reader.ReadNodeType();
        var type= Type.Instance.Deserialize(ref reader);
        //var expressions=global::MemoryPack.Formatters.ArrayFormatter<Expression>() Deserialize_T<Expression[]>(ref reader);
        var expressions=reader.ReadArray<System.Linq.Expressions.Expression>();
        value=NodeType switch{
            ExpressionType.NewArrayBounds=>System.Linq.Expressions.Expression.NewArrayBounds(type,expressions!),
            ExpressionType.NewArrayInit=>System.Linq.Expressions.Expression.NewArrayInit(type,expressions!),
            _=>throw new NotImplementedException(NodeType.ToString())
        };
    }
}
