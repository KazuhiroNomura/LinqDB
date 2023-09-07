using MemoryPack;
using System.Linq.Expressions;

using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using C=MemoryPackCustomSerializer;
using T=Expressions.CatchBlock;

public class CatchBlock:MemoryPackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Type.Instance.Serialize(ref writer,value!.Test);
        if (value.Variable is not null){
            writer.WriteVarInt((byte)(true ? 1 : 0));
            writer.WriteString(value.Variable.Name);
            C.Instance.ListParameter.Add(value.Variable);
        }else{
            writer.WriteVarInt((byte)(false ? 1 : 0));
        }
        Expression.Instance.Serialize(ref writer,value.Body);
        Expression.Instance.Serialize(ref writer,value.Filter);
        if(value.Variable is not null){
            var ListParameter= C.Instance.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        var test= Type.Instance.Deserialize(ref reader);
        if(reader.ReadBoolean()){
            var name=reader.ReadString();
            var ListParameter= C.Instance.ListParameter;
            ListParameter.Add(Expressions.Expression.Parameter(test,name));
            var body= Expression.Instance.Deserialize(ref reader);
            var filter= Expression.Instance.Deserialize(ref reader);
            ListParameter.RemoveAt(ListParameter.Count-1);
            value=Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,filter);
        } else{
            var body= Expression.Instance.Deserialize(ref reader);
            var filter= Expression.Instance.Deserialize(ref reader);
            value=Expressions.Expression.Catch(test,body,filter);
        }
    }
}
