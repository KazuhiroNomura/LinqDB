using LinqDB.Sets;

namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class SetGroupingSet2<TSerializer>:CollectionTest<SetGroupingSet<int,int>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected SetGroupingSet2():base(new SetGroupingSet<int,int>()){
        var Data=this.Data;
        for(var a=0;a<10;a++){
            var Grouping=new GroupingSet<int,int>(a);
            Data.Add(Grouping);
            for(var b=0;b<10;b++) Grouping.Add(b);
        }
    }
}
