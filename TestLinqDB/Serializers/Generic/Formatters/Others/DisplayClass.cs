

namespace TestLinqDB.Serializers.Generic.Formatters.Others;
public abstract class DisplayClass<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected DisplayClass():base(new AssertDefinition(new TSerializer())){}
    [Fact]public void Test(){
        this.AssertEqual(ClassDisplay取得());
    }
}
