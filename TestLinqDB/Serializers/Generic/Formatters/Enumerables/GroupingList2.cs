
using LinqDB.Enumerables;
namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class GroupingList2:CollectionTest<GroupingList<int,double>>{
    protected GroupingList2(テストオプション テストオプション):base(テストオプション,new(1)){
        var Data=this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
