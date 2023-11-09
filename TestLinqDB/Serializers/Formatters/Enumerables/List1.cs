namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class List1 : CollectionTest<LinqDB.Enumerables.List<int>>
{
    public List1() : base(C.O, new())
    {
        //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<LinqDB.Enumerables.List<int>,int>();
        var Data = this.Data;
        for (var a = 0; a<10; a++)
        {
            Data.Add(a);
        }
    }
}
