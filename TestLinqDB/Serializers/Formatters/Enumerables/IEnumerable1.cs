using TestLinqDB.Serializers.Formatters;

namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class IEnumerable1 : CollectionTest<IEnumerable<Tables.Table>>
{
    public IEnumerable1() : base(C.O, new List<Tables.Table> { new(1), new(2) }) { }
}
