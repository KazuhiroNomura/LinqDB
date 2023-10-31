using LinqDB.Sets;
using TestLinqDB.Serializers.Formatters;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class SetGroupingSet2 : CollectionTest<SetGroupingSet<int, int>>
{
    public SetGroupingSet2() : base(C.O, new SetGroupingSet<int, int>())
    {
        var Data = this.Data;
        for (var a = 0; a<10; a++)
        {
            var Grouping = new GroupingSet<int, int>(a);
            Data.Add(Grouping);
            for (var b = 0; b<10; b++) Grouping.Add(b);
        }
    }
}
