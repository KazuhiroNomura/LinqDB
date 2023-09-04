using MemoryPack;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class CatchBlock:MemoryPackFormatter<Expressions.CatchBlock>{
    private readonly 必要なFormatters Formatters;
    public CatchBlock(必要なFormatters Formatters)=>this.Formatters=Formatters;
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.CatchBlock? value){
        this.Formatters.Type.Serialize(ref writer,value!.Test);
        if (value.Variable is not null){
            writer.WriteVarInt((byte)(true ? 1 : 0));
            writer.WriteString(value.Variable.Name);
            this.Formatters.ListParameter.Add(value.Variable);
        }else{
            writer.WriteVarInt((byte)(false ? 1 : 0));
        }
        this.Formatters.Expression.Serialize(ref writer,value.Body);
        this.Formatters.Expression.Serialize(ref writer,value.Filter);
        if(value.Variable is not null){
            var ListParameter=this.Formatters.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.CatchBlock? value){
        var test= this.Formatters.Type.DeserializeType(ref reader);
        if(必要なFormatters.ReadBoolean(ref reader)){
            var name=reader.ReadString();
            var ListParameter=this.Formatters.ListParameter;
            ListParameter.Add(Expressions.Expression.Parameter(test,name));
            var body= this.Formatters.Expression.Deserialize(ref reader);
            var filter= this.Formatters.Expression.Deserialize(ref reader);
            ListParameter.RemoveAt(ListParameter.Count-1);
            value=Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,filter);
        } else{
            var body= this.Formatters.Expression.Deserialize(ref reader);
            var filter= this.Formatters.Expression.Deserialize(ref reader);
            value=Expressions.Expression.Catch(test,body,filter);
        }
    }
}
