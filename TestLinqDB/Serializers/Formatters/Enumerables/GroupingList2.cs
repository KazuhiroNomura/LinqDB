
using LinqDB.Enumerables;
namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class GroupingList2 : CollectionTest<Grouping<int, double>>
{
    public GroupingList2() : base( new(1))
    {
        var Data = this.Data;
        for (var a = 0; a<10; a++) Data.Add(a);
    }
}
