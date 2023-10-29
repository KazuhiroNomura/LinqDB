using LinqDB.Sets;
namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class GroupingSet2:CollectionTest<GroupingSet<int,double>>{
    protected GroupingSet2(テストオプション テストオプション):base(テストオプション,new(1)){
        var Data=this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
