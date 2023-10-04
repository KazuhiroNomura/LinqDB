using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class IEnumerable<T>:MemoryPackFormatter<G.IEnumerable<T>> {
    public static readonly IEnumerable<T> Instance=new();
    private IEnumerable(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IEnumerable<T>? value){
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteType(type);
        writer.WriteValue(type, value);
        //var GenericTypeDefinition=type.GetGenericTypeDefinition();
        //type.GetValue("Memor")
        //if(typeof(Sets.Set<>)==GenericTypeDefinition) {
        //    typeof(Set<>)
        //}
        //var Count = value.LongCount;
        //var Formatter = writer.GetFormatter<T>();
        //writer.WriteVarInt(Count);
        //foreach(var item in value) {
        //    var item0 = item;
        //    Formatter.Serialize(ref writer,ref item0);
        //}
    }
    public override void Deserialize(ref Reader reader,scoped ref G.IEnumerable<T>? value){
        if(reader.TryReadNil()) return;
        
        
        var type = reader.ReadType();
        value=(G.IEnumerable<T>?)reader.ReadValue(type);
        //var Formatter = reader.GetFormatter<T>();
        //var value = new Sets.Set<T>();
        //var Count = reader.ReadVarIntInt64();
        //for(long a = 0;a<Count;a++) {
        //    T? item = default;//ここでnull入れないと内部で作られない
        //    Formatter.Deserialize(ref reader,ref item);
        //    value.Add(item);
        //}
        //return value;
    }
}
