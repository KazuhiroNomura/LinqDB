
using LinqDB.Enumerables;
namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class GroupingList2<TSerializer>:CollectionTest<GroupingList<int,double>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected GroupingList2():base(new(1)){
        var Data=this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
