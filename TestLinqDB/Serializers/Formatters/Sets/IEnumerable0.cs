using G = LinqDB.Sets;
using LinqDB.Sets;
using TestLinqDB.Serializers.Formatters;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class IEnumerable0 : CollectionTest<IEnumerable>
{
    public IEnumerable0() : base(C.O, new Set<Tables.Table> { new(1), new(2) }) { }
}
