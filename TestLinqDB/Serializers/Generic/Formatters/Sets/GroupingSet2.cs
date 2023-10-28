using LinqDB.Sets;
namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class GroupingSet2<TSerializer>:CollectionTest<GroupingSet<int,double>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected GroupingSet2():base(new GroupingSet<int,double>(1)){
        var Data=this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
