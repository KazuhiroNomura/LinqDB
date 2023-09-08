using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using LinqDB.Serializers.MemoryPack;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.CatchBlock;
public class CatchBlock:IMessagePackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    private const int ArrayHeader=4;
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        writer.WriteType(value.Test);
        if(value.Variable is null){
            writer.WriteNil();
            Expression.Instance.Serialize(ref writer,value.Body,Resolver);
            if(value.Filter is null)writer.WriteNil();
            else Expression.Instance.Serialize(ref writer,value.Filter,Resolver);
        } else{
            writer.Write(value.Variable.Name);
            Serializer.Instance.ListParameter.Add(value.Variable);
            Expression.Instance.Serialize(ref writer,value.Body,Resolver);
            if(value.Filter is null)writer.WriteNil();
            else Expression.Instance.Serialize(ref writer,value.Filter,Resolver);
            var ListParameter=Serializer.Instance.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var test=reader.ReadType();
        if(reader.TryReadNil()){
            var body= Expression.Instance.Deserialize(ref reader,Resolver);
            var filter=reader.TryReadNil()?null:Expression.Instance.Deserialize(ref reader,Resolver);
            return Expressions.Expression.Catch(test,body,@filter);
        } else{
            var name=reader.ReadString();
            var ListParameter=Serializer.Instance.ListParameter;
            ListParameter.Add(Expressions.Expression.Parameter(test,name));
            var body= Expression.Instance.Deserialize(ref reader,Resolver);
            var filter=reader.TryReadNil()?null:Expression.Instance.Deserialize(ref reader,Resolver);
            ListParameter.RemoveAt(ListParameter.Count-1);
            return Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,@filter);
        }
    }
}
