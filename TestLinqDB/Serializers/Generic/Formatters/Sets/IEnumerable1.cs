using S = LinqDB.Sets;
using LinqDB.Sets;

namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class IEnumerable1:CollectionTest<S.IEnumerable<Tables.Table>>{
    protected IEnumerable1(テストオプション テストオプション):base(テストオプション,new Set<Tables.Table>{new(1),new(2)}){}
}
