namespace TestLinqDB.Serializers.Formatters.Sets;

using TestLinqDB.Serializers.Formatters;
using G = LinqDB.Sets;
public class IGrouping2 : CollectionTest<G.IGrouping<int, double>>
{
    public IGrouping2() : base(new G.GroupingSet<int, double>(1))
    {
        var Data = (G.GroupingSet<int, double>)this.Data;
        for (var a = 0; a<10; a++) Data.Add(a);
    }
}
