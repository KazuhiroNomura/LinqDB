using LinqDB.Sets;
using TestLinqDB.Serializers.Formatters;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class GroupingSet2 : CollectionTest<GroupingSet<int, double>>
{
    public GroupingSet2() : base(C.O, new(1))
    {
        var Data = this.Data;
        for (var a = 0; a<10; a++) Data.Add(a);
    }
}
