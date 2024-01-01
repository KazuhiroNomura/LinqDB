namespace TestLinqDB.Serializers.Formatters.Sets;

using TestLinqDB.Serializers.Formatters;
using G = LinqDB.Sets;
public class IGrouping2 : CollectionTest<G.IGrouping<int, double>>
{
    public IGrouping2() : base(new G.Grouping<int, double>(1))
    {
        var Data = (G.Grouping<int, double>)this.Data;
        for (var a = 0; a<10; a++) Data.Add(a);
    }
}
