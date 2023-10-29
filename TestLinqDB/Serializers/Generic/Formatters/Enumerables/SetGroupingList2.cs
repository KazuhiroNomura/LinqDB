using LinqDB.Enumerables;
using LinqDB.Sets;

namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class SetGroupingList2:CollectionTest<SetGroupingList<int,int>>{
    protected SetGroupingList2(テストオプション テストオプション):base(テストオプション,new()){
        var Data=this.Data;
        for(var a=0;a<10;a++){
            var Grouping=new GroupingList<int,int>(a);
            Data.Add(Grouping);
            for(var b=0;b<10;b++) Grouping.Add(b);
        }
    }
}
