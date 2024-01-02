namespace TestLinqDB.Serializers.Formatters.Others;
public class DisplayClass : 共通
{
    //protected override テストオプション テストオプション=>テストオプション.MemoryPack_MessagePack_Utf8Json;
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        {
            var x=ClassDisplay取得();
            var Tuple=typeof(Tuple<>).MakeGenericType(x.GetType());
            var ctor=Tuple.GetConstructors()[0];
            var y=Activator.CreateInstance(
                Tuple,new object?[]{null}
            );
            this.ObjectシリアライズAssertEqual(y);
        }
        //if(!this.DictionarySerialize.TryGetValue(typeof(TBufferWriter),out var Write)){
        //    foreach(var Field in Fields){
        this.ObjectシリアライズAssertEqual(ClassDisplay取得());
        //    }
        //}
    }
}
