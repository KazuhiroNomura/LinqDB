using System.Collections.Generic;
using System.Reflection;
using LinqDB.Sets;

using Extension = LinqDB.Sets.ExtensionSet;
namespace LinqDB.Reflection;

using static Common;
/// <summary>
/// ループ展開するExtensionSet拡張クラスのstaticメソッド
/// </summary>
public static class ExtensionSet {
#if DEBUG
    static ExtensionSet() {
        var HashSet = new HashSet<int>();
        foreach(var Field in typeof(ExtensionEnumerable).GetFields(BindingFlags.Public|BindingFlags.Static))
            System.Diagnostics.Debug.Assert(HashSet.Add(Field.MetadataToken));
    }
#endif
    private static readonly Set<int> _Int32 = default!;
    //ExtensionSet,Enumerable共通のインラインメソッド
    public static readonly MethodInfo Aggregate_func = M(() => _Int32.Aggregate((a,b) => 0));
    public static readonly MethodInfo Aggregate_seed_func = M(() => _Int32.Aggregate(0,(a,b) => 0));
    public static readonly MethodInfo Aggregate_seed_func_resultSelector = M(() => _Int32.Aggregate(0,(a,b) => a,b => b*2));
    public static readonly MethodInfo All = M(() => _Int32.All(b => true));
    public static readonly MethodInfo Any = M(() => _Int32.Any());
    public static readonly MethodInfo AverageNullableDecimal = M(() => _Int32.Cast<decimal?>().Average());
    public static readonly MethodInfo AverageDecimal = M(() => _Int32.Cast<decimal>().Average());
    public static readonly MethodInfo AverageNullableDouble = M(() => _Int32.Cast<double?>().Average());
    public static readonly MethodInfo AverageDouble = M(() => _Int32.Cast<double>().Average());
    public static readonly MethodInfo AverageNullableSingle = M(() => _Int32.Cast<float?>().Average());
    public static readonly MethodInfo AverageSingle = M(() => _Int32.Cast<float>().Average());
    public static readonly MethodInfo AverageNullableInt64 = M(() => _Int32.Cast<long?>().Average());
    public static readonly MethodInfo AverageInt64 = M(() => _Int32.Cast<long>().Average());
    public static readonly MethodInfo AverageNullableInt32 = M(() => _Int32.Cast<int?>().Average());
    public static readonly MethodInfo AverageInt32 = M(() => _Int32.Cast<int>().Average());
    public static readonly MethodInfo AverageNullableDecimal_selector = M(() => _Int32.Average(p => (decimal?)0m));
    public static readonly MethodInfo AverageDecimal_selector = M(() => _Int32.Average(p => 0m));
    public static readonly MethodInfo AverageNullableDouble_selector = M(() => _Int32.Average(p => (double?)0d));
    public static readonly MethodInfo AverageDouble_selector = M(() => _Int32.Average(p => 0d));
    public static readonly MethodInfo AverageNullableSingle_selector = M(() => _Int32.Average(p => (float?)0f));
    public static readonly MethodInfo AverageSingle_selector = M(() => _Int32.Average(p => 0f));
    public static readonly MethodInfo AverageNullableInt64_selector = M(() => _Int32.Average(p => (long?)0L));
    public static readonly MethodInfo AverageInt64_selector = M(() => _Int32.Average(p => 0L));
    public static readonly MethodInfo AverageNullableInt32_selector = M(() => _Int32.Average(p => (int?)0));
    public static readonly MethodInfo AverageInt32_selector = M(() => _Int32.Average(p => 0));
    public static readonly MethodInfo Cast = M(() => _Int32.Cast<int>());
    public static readonly MethodInfo Contains_value = M(() => _Int32.Contains(0));
    public static readonly MethodInfo Count = M(() => _Int32.Count());
    public static readonly MethodInfo DefaultIfEmpty = M(() => _Int32.DefaultIfEmpty());
    public static readonly MethodInfo DefaultIfEmpty_defaultValue = M(() => _Int32.DefaultIfEmpty(0));
    public static readonly MethodInfo Except = M(() => _Int32.Except(default!));
    public static readonly MethodInfo GroupBy_keySelector = M(() => _Int32.GroupBy(p => p));
    public static readonly MethodInfo GroupBy_keySelector_elementSelector = M(() => _Int32.GroupBy(p => p,p => p));
    public static readonly MethodInfo GroupBy_keySelector_resultSelector = M(() => _Int32.GroupBy(p => p,(a,b) => a));
    public static readonly MethodInfo GroupBy_keySelector_elementSelector_resultSelector = M(() => _Int32.GroupBy(p => p,a => a,(a,b) => a));
    public static readonly MethodInfo GroupJoin = M(() => _Int32.GroupJoin(_Int32,o => 0,i => 0,(o,i) => 0));
    public static readonly MethodInfo Intersect = M(() => _Int32.Intersect(default!));
    public static readonly MethodInfo Join = M(() => _Int32.Join(_Int32,o => 0,i => 0,(o,i) => 0));
    public static readonly MethodInfo LongCount = M(() => _Int32.LongCount());
    public static readonly MethodInfo MaxNullableDecimal = M(() => _Int32.Cast<decimal?>().Max());
    public static readonly MethodInfo MaxDecimal = M(() => _Int32.Cast<decimal>().Max());
    public static readonly MethodInfo MaxNullableDouble = M(() => _Int32.Cast<double?>().Max());
    public static readonly MethodInfo MaxDouble = M(() => _Int32.Cast<double>().Max());
    public static readonly MethodInfo MaxNullableSingle = M(() => _Int32.Cast<float?>().Max());
    public static readonly MethodInfo MaxSingle = M(() => _Int32.Cast<float>().Max());
    public static readonly MethodInfo MaxNullableInt64 = M(() => _Int32.Cast<long?>().Max());
    public static readonly MethodInfo MaxInt64 = M(() => _Int32.Cast<long>().Max());
    public static readonly MethodInfo MaxNullableInt32 = M(() => _Int32.Cast<int?>().Max());
    public static readonly MethodInfo MaxInt32 = M(() => _Int32.Cast<int>().Max());
    public static readonly MethodInfo MaxTSource = M(() => _Int32.Cast<string>().Max());
    public static readonly MethodInfo MaxNullableDecimal_selector = M(() => _Int32.Max(p => (decimal?)0m));
    public static readonly MethodInfo MaxDecimal_selector = M(() => _Int32.Max(p => 0m));
    public static readonly MethodInfo MaxNullableDouble_selector = M(() => _Int32.Max(p => (double?)0d));
    public static readonly MethodInfo MaxDouble_selector = M(() => _Int32.Max(p => 0d));
    public static readonly MethodInfo MaxNullableSingle_selector = M(() => _Int32.Max(p => (float?)0f));
    public static readonly MethodInfo MaxSingle_selector = M(() => _Int32.Max(p => 0f));
    public static readonly MethodInfo MaxNullableInt64_selector = M(() => _Int32.Max(p => (long?)0L));
    public static readonly MethodInfo MaxInt64_selector = M(() => _Int32.Max(p => 0L));
    public static readonly MethodInfo MaxNullableInt32_selector = M(() => _Int32.Max(p => (int?)0));
    public static readonly MethodInfo MaxInt32_selector = M(() => _Int32.Max(p => 0));
    public static readonly MethodInfo MaxTSource_selector = M(() => _Int32.Max(p => ""));
    public static readonly MethodInfo MinNullableDecimal = M(() => _Int32.Cast<decimal?>().Min());
    public static readonly MethodInfo MinDecimal = M(() => _Int32.Cast<decimal>().Min());
    public static readonly MethodInfo MinNullableDouble = M(() => _Int32.Cast<double?>().Min());
    public static readonly MethodInfo MinDouble = M(() => _Int32.Cast<double>().Min());
    public static readonly MethodInfo MinNullableSingle = M(() => _Int32.Cast<float?>().Min());
    public static readonly MethodInfo MinSingle = M(() => _Int32.Cast<float>().Min());
    public static readonly MethodInfo MinNullableInt32 = M(() => _Int32.Cast<int?>().Min());
    public static readonly MethodInfo MinInt32 = M(() => _Int32.Cast<int>().Min());
    public static readonly MethodInfo MinNullableInt64 = M(() => _Int32.Cast<long?>().Min());
    public static readonly MethodInfo MinInt64 = M(() => _Int32.Cast<long>().Min());
    public static readonly MethodInfo MinTSource = M(() => _Int32.Cast<string>().Min());
    public static readonly MethodInfo MinNullableDecimal_selector = M(() => _Int32.Min(p => (decimal?)0m));
    public static readonly MethodInfo MinDecimal_selector = M(() => _Int32.Min(p => 0m));
    public static readonly MethodInfo MinNullableDouble_selector = M(() => _Int32.Min(p => (double?)0d));
    public static readonly MethodInfo MinDouble_selector = M(() => _Int32.Min(p => 0d));
    public static readonly MethodInfo MinNullableSingle_selector = M(() => _Int32.Min(p => (float?)0f));
    public static readonly MethodInfo MinSingle_selector = M(() => _Int32.Min(p => 0f));
    public static readonly MethodInfo MinNullableInt32_selector = M(() => _Int32.Min(p => (int?)0));
    public static readonly MethodInfo MinInt32_selector = M(() => _Int32.Min(p => 0));
    public static readonly MethodInfo MinNullableInt64_selector = M(() => _Int32.Min(p => (long?)0L));
    public static readonly MethodInfo MinInt64_selector = M(() => _Int32.Min(p => 0L));
    public static readonly MethodInfo MinTSource_selector = M(() => _Int32.Min(p => ""));
    public static readonly MethodInfo OfType = M(() => _Int32.OfType<int>());
    public static readonly MethodInfo Range = M(() => Extension.Range(0,0));
    public static readonly MethodInfo Select_selector = M(() => _Int32.Select(p => 0));
    public static readonly MethodInfo SelectMany_selector = M(() => _Int32.SelectMany(p => _Int32));
    public static readonly MethodInfo SelectMany_collectionSelector_resultSelector = M(() => _Int32.SelectMany(p => _Int32,(p,q) => 0));        //変形で削除される
    public static readonly MethodInfo Single = M(() => _Int32.Single());
    public static readonly MethodInfo SingleOrDefault = M(() => _Int32.SingleOrDefault());
    public static readonly MethodInfo SingleOrDefault_defaultValue = M(() => _Int32.SingleOrDefault(0));
    public static readonly MethodInfo SumNullableDecimal = M(() => _Int32.Cast<decimal?>().Sum());
    public static readonly MethodInfo SumDecimal = M(() => _Int32.Cast<decimal>().Sum());
    public static readonly MethodInfo SumNullableDouble = M(() => _Int32.Cast<double?>().Sum());
    public static readonly MethodInfo SumDouble = M(() => _Int32.Cast<double>().Sum());
    public static readonly MethodInfo SumNullableSingle = M(() => _Int32.Cast<float?>().Sum());
    public static readonly MethodInfo SumSingle = M(() => _Int32.Cast<float>().Sum());
    public static readonly MethodInfo SumNullableInt64 = M(() => _Int32.Cast<long?>().Sum());
    public static readonly MethodInfo SumInt64 = M(() => _Int32.Cast<long>().Sum());
    public static readonly MethodInfo SumNullableInt32 = M(() => _Int32.Cast<int?>().Sum());
    public static readonly MethodInfo SumInt32 = M(() => _Int32.Cast<int>().Sum());
    public static readonly MethodInfo SumNullableDecimal_selector = M(() => _Int32.Sum(p => (decimal?)1m));
    public static readonly MethodInfo SumDecimal_selector = M(() => _Int32.Sum(p => 0m));
    public static readonly MethodInfo SumNullableDouble_selector = M(() => _Int32.Sum(p => (double?)0d));
    public static readonly MethodInfo SumDouble_selector = M(() => _Int32.Sum(p => 0d));
    public static readonly MethodInfo SumNullableSingle_selector = M(() => _Int32.Sum(p => (float?)0f));
    public static readonly MethodInfo SumSingle_selector = M(() => _Int32.Sum(p => 0f));
    public static readonly MethodInfo SumNullableInt64_selector = M(() => _Int32.Sum(p => (long?)0L));
    public static readonly MethodInfo SumInt64_selector = M(() => _Int32.Sum(p => 0L));
    public static readonly MethodInfo SumNullableInt32_selector = M(() => _Int32.Sum(p => (int?)0));
    public static readonly MethodInfo SumInt32_selector = M(() => _Int32.Sum(p => 0));
    public static readonly MethodInfo Union = M(() => _Int32.Union(default!));
    public static readonly MethodInfo Where = M(() => _Int32.Where(p => true));



    //ExtensionSet独自メソッド
    public static readonly MethodInfo AvedevDouble_selector = M(() => _Int32.Avedev(p => 1d));
    public static readonly MethodInfo Delete = M(() => _Int32.Delete(p => true));
    public static readonly MethodInfo DUnion = M(() => _Int32.DUnion(_Int32));
    public static readonly MethodInfo GeomeanNullableDouble_selector = M(() => _Int32.Geomean(p => default(double?)));
    public static readonly MethodInfo GeomeanDouble_selector = M(() => _Int32.Geomean(p => 0.0));
    public static readonly MethodInfo InternalMax_selector = M(() => _Int32.Max(p => ""));
    public static readonly MethodInfo InternalMin_selector = M(() => _Int32.Min(p => ""));
    public static readonly MethodInfo HarmeanNullableDecimal_selector = M(() => _Int32.Harmean(p => default(decimal?)));
    public static readonly MethodInfo HarmeanDecimal_selector = M(() => _Int32.Harmean(p => 0m));
    public static readonly MethodInfo HarmeanNullableDouble_selector = M(() => _Int32.Harmean(p => default(double?)));
    public static readonly MethodInfo HarmeanDouble_selector = M(() => _Int32.Harmean(p => 0.0));
    public static readonly MethodInfo StdevDouble = M(() => _Int32.Cast<double>().Stdev());
    public static readonly MethodInfo StdevDouble_selector = M(() => _Int32.Stdev(p => 1d));
    public static readonly MethodInfo VarDecimal_selector = M(() => _Int32.Var(p => 0m));
    public static readonly MethodInfo VarDouble_selector = M(() => _Int32.Var(p => 0d));
    public static readonly MethodInfo VarpDecimal_selector = M(() => _Int32.Varp(p => 0m));
    public static readonly MethodInfo VarpDouble_selector = M(() => _Int32.Varp(p => 0d));
    public static readonly MethodInfo Inline1 = M(() => Extension.Inline(() => 0));
    public static readonly MethodInfo Inline2 = M(() => new object().Inline(p => 0));
    public static readonly MethodInfo Lookup = M(() => _Int32.Lookup(p => p));
    //public static readonly MethodInfo Update = M(() => _Int32.Update(p => true,p => 0));
}