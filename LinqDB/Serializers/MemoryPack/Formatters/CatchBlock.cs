using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MemoryPack;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using C=Serializer;
using T=Expressions.CatchBlock;

public class CatchBlock:MemoryPackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    [SuppressMessage("ReSharper","ConvertIfStatementToConditionalTernaryExpression")]
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value.Test);
        if(value.Variable is null){
            if(value.Filter is null) {
                writer.WriteVarInt(0);
                Expression.InternalSerialize(ref writer,value.Body);
            } else {
                writer.WriteVarInt(1);
                writer.WriteType(value.Test);
                Expression.InternalSerialize(ref writer,value.Body);
                Expression.InternalSerialize(ref writer,value.Filter);
            }
        } else{
            var ListParameter= C.Instance.ListParameter;
            ListParameter.Add(value.Variable);
            if(value.Filter is null) {
                writer.WriteVarInt(2);
                writer.WriteString(value.Variable.Name);
                Expression.InternalSerialize(ref writer,value.Body);
            } else {
                writer.WriteVarInt(3);
                writer.WriteString(value.Variable.Name);
                Expression.InternalSerialize(ref writer,value.Body);
                Expression.InternalSerialize(ref writer,value.Filter);
            }
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var test=reader.ReadType();
        var id=reader.ReadVarIntInt32();
        switch(id){
            case 0:{
                var body=Expression.InternalDeserialize(ref reader);
                value=Expressions.Expression.Catch(test,body);
                break;
            }
            case 1:{
                var body=Expression.InternalDeserialize(ref reader);
                var filter=Expression.InternalDeserialize(ref reader);
                value=Expressions.Expression.Catch(test,body,filter);
                break;
            }
            case 2:{
                var name=reader.ReadString();
                var ListParameter=C.Instance.ListParameter;
                ListParameter.Add(Expressions.Expression.Parameter(test,name));
                var body=Expression.InternalDeserialize(ref reader);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body);
                break;
            }
            case 3:{
                var name=reader.ReadString();
                var ListParameter=C.Instance.ListParameter;
                ListParameter.Add(Expressions.Expression.Parameter(test,name));
                var body=Expression.InternalDeserialize(ref reader);
                var filter=Expression.InternalDeserialize(ref reader);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,filter);
                break;
            }
        }
        //if(reader.ReadBoolean()){
        //    var body=Expression.Deserialize(ref reader);
        //    var filter=reader.TryPeekObjectHeader(out var count)?null:Expression.Deserialize(ref reader);
        //    //var filter=reader.ReadBoolean()?null:Expression.Deserialize(ref reader);
        //    value=Expressions.Expression.Catch(test,body,filter);
        //} else{
        //    var name=reader.ReadString();
        //    var ListParameter=C.Instance.ListParameter;
        //    ListParameter.Add(Expressions.Expression.Parameter(test,name));
        //    var body=Expression.Deserialize(ref reader);
        //    var filter=reader.TryPeekObjectHeader(out var count)?null:Expression.Deserialize(ref reader);
        //    //var filter=reader.ReadBoolean()?null:Expression.Deserialize(ref reader);
        //    ListParameter.RemoveAt(ListParameter.Count-1);
        //    value=Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,filter);
        //}
    }
}
