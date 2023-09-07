using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.LabelExpression;
using C=MemoryPackCustomSerializer;


public class Label:MemoryPackFormatter<T> {
    public static readonly Label Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeLabel(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        LabelTarget.Instance.Serialize(ref writer,value.Target);
        Expression.Instance.Serialize(ref writer,value.DefaultValue);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var target= LabelTarget.Instance.DeserializeLabelTarget(ref reader);
        var defaultValue= Expression.Instance.Deserialize(ref reader);
        value=Expressions.Expression.Label(target,defaultValue);
    }
}
