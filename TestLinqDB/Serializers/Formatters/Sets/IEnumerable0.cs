using G = LinqDB.Sets;
using LinqDB.Sets;
namespace TestLinqDB.Serializers.Formatters.Sets;
public class IEnumerable0 : CollectionTest<IEnumerable>
{
    public IEnumerable0() : base(new Set<Tables.Table> { new(1), new(2) }) { }
}
