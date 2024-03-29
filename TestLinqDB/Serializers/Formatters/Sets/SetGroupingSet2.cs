﻿using LinqDB.Sets;
namespace TestLinqDB.Serializers.Formatters.Sets;
public class SetGroupingSet2 : CollectionTest<SetGroupingSet<int, int>>
{
    public SetGroupingSet2() : base(new SetGroupingSet<int, int>())
    {
        var Data = this.Data;
        for (var a = 0; a<10; a++)
        {
            var Grouping = new Grouping<int, int>(a);
            Data.Add(Grouping);
            for (var b = 0; b<10; b++) Grouping.Add(b);
        }
    }
}
