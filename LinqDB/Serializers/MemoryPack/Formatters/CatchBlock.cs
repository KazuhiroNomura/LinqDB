using MemoryPack;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class CatchBlock:MemoryPackFormatter<Expressions.CatchBlock>{
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.CatchBlock? value){
        MemoryPackCustomSerializer.Type.Serialize(ref writer,value!.Test);
        if (value.Variable is not null){
            writer.WriteVarInt((byte)(true ? 1 : 0));
            writer.WriteString(value.Variable.Name);
            MemoryPackCustomSerializer.ListParameter.Add(value.Variable);
        }else{
            writer.WriteVarInt((byte)(false ? 1 : 0));
        }
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Body);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Filter);
        if(value.Variable is not null){
            var ListParameter=MemoryPackCustomSerializer.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.CatchBlock? value){
        var test= MemoryPackCustomSerializer.Type.DeserializeType(ref reader);
        if(MemoryPackCustomSerializer.ReadBoolean(ref reader)){
            var name=reader.ReadString();
            var ListParameter=MemoryPackCustomSerializer.ListParameter;
            ListParameter.Add(Expressions.Expression.Parameter(test,name));
            var body= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
            var filter= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
            ListParameter.RemoveAt(ListParameter.Count-1);
            value=Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,filter);
        } else{
            var body= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
            var filter= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
            value=Expressions.Expression.Catch(test,body,filter);
        }
    }
}
