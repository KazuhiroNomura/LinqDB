using MemoryPack;

using System.Buffers;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class New:MemoryPackFormatter<NewExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,NewExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal NewExpression DeserializeNew(ref MemoryPackReader reader){
        NewExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref NewExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        MemoryPackCustomSerializer.Constructor.Serialize(ref writer,value.Constructor!);

        MemoryPackCustomSerializer.Serialize(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref NewExpression? value){
        //if(reader.TryReadNil()) return;
        var constructor= MemoryPackCustomSerializer.Constructor.DeserializeConstructorInfo(ref reader);
        var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
        value=System.Linq.Expressions.Expression.New(
            constructor,
            arguments!
        );
    }
}
