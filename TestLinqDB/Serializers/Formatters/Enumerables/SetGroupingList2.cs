using LinqDB.Enumerables;
namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class SetGroupingList2 : CollectionTest<LinqDB.Enumerables.Lookup<int, int>>
{
    public SetGroupingList2() : base(new())
    {
        var Data = this.Data;
        for (var a = 0; a<10; a++)
        {
            var Grouping = new Grouping<int, int>(a);
            Data.Add(Grouping);
            for (var b = 0; b<10; b++) Grouping.Add(b);
        }
    }
}
