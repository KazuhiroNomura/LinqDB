using System.Diagnostics;
using MemoryPack;
using System.Linq.Expressions;

using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using C=Serializer;
using T=Expressions.CatchBlock;

public class CatchBlock:MemoryPackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value.Test);
        if(value.Variable is null){
            writer.WriteBoolean(true);
            Expression.Instance.Serialize(ref writer,value.Body);
            if(value.Filter is null){
                writer.WriteBoolean(true);
            } else{
                writer.WriteBoolean(false);
                Expression.Instance.Serialize(ref writer,value.Filter);
            }
        } else{
            writer.WriteBoolean(false);
            writer.WriteString(value.Variable.Name);
            C.Instance.ListParameter.Add(value.Variable);
            Expression.Instance.Serialize(ref writer,value.Body);
            if(value.Filter is null){
                writer.WriteBoolean(true);
            } else{
                writer.WriteBoolean(false);
                Expression.Instance.Serialize(ref writer,value.Filter);
            }
            var ListParameter= C.Instance.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var test=reader.ReadType();
        if(reader.ReadBoolean()){
            var body=Expression.Instance.Deserialize(ref reader);
            var filter=reader.ReadBoolean()?null:Expression.Instance.Deserialize(ref reader);
            value=Expressions.Expression.Catch(test,body,filter);
        } else{
            var name=reader.ReadString();
            var ListParameter=C.Instance.ListParameter;
            ListParameter.Add(Expressions.Expression.Parameter(test,name));
            var body=Expression.Instance.Deserialize(ref reader);
            var filter=reader.ReadBoolean()?null:Expression.Instance.Deserialize(ref reader);
            ListParameter.RemoveAt(ListParameter.Count-1);
            value=Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,filter);
        }
    }
}
