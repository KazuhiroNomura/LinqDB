using LinqDB.Sets;
using MemoryPack;

using System.Xml.Linq;

namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;
using Sets = LinqDB.Sets;


using Reader = MemoryPackReader;
// ReSharper disable once InconsistentNaming
public class IEnumerable:MemoryPackFormatter<Sets.IEnumerable> {
    public static readonly IEnumerable Instance=new();
    private IEnumerable(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.IEnumerable? value){
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(value);
        //var GenericTypeDefinition=type.GetGenericTypeDefinition();
        //type.GetValue("Memor")
        //if(typeof(Sets.Set<>)==GenericTypeDefinition) {
        //    typeof(Set<>)
        //}
        //var Count = value.LongCount;
        //var Formatter = writer.GetFormatter();
        //writer.WriteVarInt(Count);
        //foreach(var item in value) {
        //    var item0 = item;
        //    Formatter.Serialize(ref writer,ref item0);
        //}
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.IEnumerable? value){
        if(reader.TryReadNil()) return;
        
        
        var type = reader.ReadType();
        value=(Sets.IEnumerable?)reader.ReadValue(type);
        //var Formatter = reader.GetFormatter();
        //var value = new Sets.Set();
        //var Count = reader.ReadVarIntInt64();
        //for(long a = 0;a<Count;a++) {
        //    T? item = default;//ここでnull入れないと内部で作られない
        //    Formatter.Deserialize(ref reader,ref item);
        //    value.Add(item);
        //}
        //return value;
    }
}
