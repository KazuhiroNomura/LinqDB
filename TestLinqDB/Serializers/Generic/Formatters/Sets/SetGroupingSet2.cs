using LinqDB.Sets;

namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class SetGroupingSet2:CollectionTest<SetGroupingSet<int,int>>{
    protected SetGroupingSet2(テストオプション テストオプション):base(テストオプション,new SetGroupingSet<int,int>()){
        var Data=this.Data;
        for(var a=0;a<10;a++){
            var Grouping=new GroupingSet<int,int>(a);
            Data.Add(Grouping);
            for(var b=0;b<10;b++) Grouping.Add(b);
        }
    }
}
