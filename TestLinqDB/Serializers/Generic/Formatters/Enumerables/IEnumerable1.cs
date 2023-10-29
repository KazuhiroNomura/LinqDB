namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class IEnumerable1:CollectionTest<IEnumerable<Tables.Table>>{
    protected IEnumerable1(テストオプション テストオプション) : base(テストオプション,new List<Tables.Table> { new(1),new(2) }) {}
}
