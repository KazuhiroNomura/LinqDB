﻿using S = LinqDB.Sets;
using LinqDB.Sets;
namespace TestLinqDB.Serializers.Formatters.Sets;
public class IEnumerable1 : CollectionTest<S.IEnumerable<Tables.Table>>
{
    public IEnumerable1() : base(new Set<Tables.Table> { new(1), new(2) }) { }
}
