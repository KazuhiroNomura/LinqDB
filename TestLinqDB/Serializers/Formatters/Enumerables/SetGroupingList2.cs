using LinqDB.Enumerables;
using LinqDB.Sets;
namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class SetGroupingList2 : CollectionTest<SetGroupingList<int, int>>
{
    public SetGroupingList2() : base(C.O, new())
    {
        var Data = this.Data;
        for (var a = 0; a<10; a++)
        {
            var Grouping = new GroupingList<int, int>(a);
            Data.Add(Grouping);
            for (var b = 0; b<10; b++) Grouping.Add(b);
        }
    }
}
