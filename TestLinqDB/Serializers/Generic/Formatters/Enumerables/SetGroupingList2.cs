using LinqDB.Enumerables;
using LinqDB.Sets;

namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class SetGroupingList2<TSerializer>:CollectionTest<SetGroupingList<int,int>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected SetGroupingList2():base(new SetGroupingList<int,int>()){
        var Data=this.Data;
        for(var a=0;a<10;a++){
            var Grouping=new GroupingList<int,int>(a);
            Data.Add(Grouping);
            for(var b=0;b<10;b++) Grouping.Add(b);
        }
    }
}
