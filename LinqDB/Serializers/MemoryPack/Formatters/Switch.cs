using MemoryPack;
using Expressions=System.Linq.Expressions;
using MemoryPack.Formatters;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Switch:MemoryPackFormatter<Expressions.SwitchExpression>{
    private readonly 必要なFormatters Formatters;
    public Switch(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.SwitchExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal Expressions.SwitchExpression DeserializeSwitch(ref MemoryPackReader reader){
        Expressions.SwitchExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.SwitchExpression? value){
        var Formatters=this.Formatters;
        Formatters.Type.Serialize(ref writer,value!.Type);
        Formatters.Expression.Serialize(ref writer,value.SwitchValue);
        Formatters.Method.SerializeNullable(ref writer,value.Comparison);
        //this.Serialize(ref writer,value.Cases);
        必要なFormatters.Serialize(ref writer,value.Cases);
        Formatters.Expression.Serialize(ref writer,value.DefaultBody);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.SwitchExpression? value){
        var Formatters=this.Formatters;
        var type       = Formatters.Type.DeserializeType(ref reader);
        var switchValue= Formatters.Expression.Deserialize(ref reader);
        var comparison = Formatters.Method.DeserializeNullable(ref reader);
        var cases      =reader.ReadArray<Expressions.SwitchCase>();
        var defaultBody= Formatters.Expression.Deserialize(ref reader);
        value=Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases!);
    }
}
