using TestLinqDB.Serializers.Formatters;

namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class IEnumerable0 : CollectionTest<System.Collections.IEnumerable>
{
    public IEnumerable0() : base(C.O, new List<Tables.Table> { new(1), new(2) }) { }
}
