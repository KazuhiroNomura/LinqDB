﻿using System;
using System.Collections.Generic;
using System.Reflection;
using LinqDB.Sets;
using System.Linq;
using Extension = System.Linq.Enumerable;
namespace LinqDB.Reflection;

using static Common;
/// <summary>
/// ループ展開するEnumerable拡張クラスのstaticメソッド
/// </summary>
public static class ExtensionEnumerable {
#if DEBUG
    static ExtensionEnumerable() {
        var HashSet = new System.Collections.Generic.HashSet<int>();
        foreach(var Field in typeof(ExtensionEnumerable).GetFields(BindingFlags.Public|BindingFlags.Static))
            System.Diagnostics.Debug.Assert(HashSet.Add(Field.MetadataToken));
    }
#endif
    private const System.Collections.Generic.IEnumerable<object> _Object= null!;
    private const System.Collections.Generic.IEnumerable<int> _Int32 = null!;
    //ExtensionSet,Enumerable共通のインラインメソッド
    public static readonly MethodInfo Aggregate_func = M(() => _Int32.Aggregate((a,b) => 0));
    public static readonly MethodInfo Aggregate_seed_func = M(() => _Int32.Aggregate(0,(a,b) => 0));
    public static readonly MethodInfo Aggregate_seed_func_resultSelector = M(() => _Int32.Aggregate(0,(a,b) => a,b => b*2));
    public static readonly MethodInfo All = M(() => _Int32.All(b => true));
    public static readonly MethodInfo Any = M(() => _Int32.Any());
    public static readonly MethodInfo AverageDecimal = M(() => _Object.Cast<decimal>().Average());
    public static readonly MethodInfo AverageDouble = M(() => _Object.Cast<double>().Average());
    public static readonly MethodInfo AverageSingle = M(() => _Object.Cast<float>().Average());
    public static readonly MethodInfo AverageInt64 = M(() => _Object.Cast<long>().Average());
    public static readonly MethodInfo AverageInt32 = M(() => _Object.Cast<int>().Average());
    public static readonly MethodInfo AverageNullableDecimal = M(() => _Object.Cast<decimal?>().Average());
    public static readonly MethodInfo AverageNullableDouble = M(() => _Object.Cast<double?>().Average());
    public static readonly MethodInfo AverageNullableSingle = M(() => _Object.Cast<float?>().Average());
    public static readonly MethodInfo AverageNullableInt64 = M(() => _Object.Cast<long?>().Average());
    public static readonly MethodInfo AverageNullableInt32 = M(() => _Object.Cast<int?>().Average());
    public static readonly MethodInfo AverageDecimal_selector = M(() => _Int32.Average(p => 0m));
    public static readonly MethodInfo AverageDouble_selector = M(() => _Int32.Average(p => 0d));
    public static readonly MethodInfo AverageSingle_selector = M(() => _Int32.Average(p => 0f));
    public static readonly MethodInfo AverageInt64_selector = M(() => _Int32.Average(p => 0L));
    public static readonly MethodInfo AverageInt32_selector = M(() => _Int32.Average(p => 0));
    public static readonly MethodInfo AverageNullableDecimal_selector = M(() => _Int32.Average(p => (decimal?)0m));
    public static readonly MethodInfo AverageNullableDouble_selector = M(() => _Int32.Average(p => (double?)0d));
    public static readonly MethodInfo AverageNullableSingle_selector = M(() => _Int32.Average(p => (float?)0f));
    public static readonly MethodInfo AverageNullableInt64_selector = M(() => _Int32.Average(p => (long?)0L));
    public static readonly MethodInfo AverageNullableInt32_selector = M(() => _Int32.Average(p => (int?)0));
    public static readonly MethodInfo Cast = M(() => _Int32.Cast<int>());
    public static readonly MethodInfo Contains_value = M(() => _Int32.Contains(0));
    public static readonly MethodInfo Count = M(() => _Int32.Count());
    public static readonly MethodInfo DefaultIfEmpty = M(() => _Int32.DefaultIfEmpty());
    public static readonly MethodInfo DefaultIfEmpty_defaultValue = M(() => _Int32.DefaultIfEmpty(0));
    public static readonly MethodInfo Except = M(() => _Int32.Except(default!));
    /// <summary>
    /// GroupBy_keySelector_elementSelector
    /// 最適化で消される対象
    /// </summary>
    public static readonly MethodInfo GroupBy_keySelector = M(() => _Int32.GroupBy(p => p));
    public static readonly MethodInfo GroupBy_keySelector_elementSelector = M(() => _Int32.GroupBy(p => p,p => p));
    /// <summary>
    /// GroupBy_keySelector_elementSelector.Select_selector
    /// 最適化で消される対象
    /// </summary>
    public static readonly MethodInfo GroupBy_keySelector_resultSelector = M(() => _Int32.GroupBy(p => p,(a,b) => a));
    /// <summary>
    /// GroupBy_keySelector_elementSelector.Select_selector
    /// 最適化で消される対象
    /// </summary>
    public static readonly MethodInfo GroupBy_keySelector_elementSelector_resultSelector = M(() => _Int32.GroupBy(p => p,a => a,(a,b) => a));
    public static readonly MethodInfo GroupJoin = M(() => _Int32.GroupJoin(_Int32,o => 0,i => 0,(o,i) => 0));
    public static readonly MethodInfo Intersect = M(() => _Int32.Intersect(default!));
    public static readonly MethodInfo Join = M(() => _Int32.Join(_Int32,o => 0,i => 0,(o,i) => 0));
    public static readonly MethodInfo LongCount = M(() => _Int32.LongCount());
    public static readonly MethodInfo MaxNullableDecimal = M(() => _Object.Cast<decimal?>().Max());
    public static readonly MethodInfo MaxDecimal = M(() => _Object.Cast<decimal>().Max());
    public static readonly MethodInfo MaxNullableDouble = M(() => _Object.Cast<double?>().Max());
    public static readonly MethodInfo MaxDouble = M(() => _Object.Cast<double>().Max());
    public static readonly MethodInfo MaxNullableSingle = M(() => _Object.Cast<float?>().Max());
    public static readonly MethodInfo MaxSingle = M(() => _Object.Cast<float>().Max());
    public static readonly MethodInfo MaxNullableInt64 = M(() => _Object.Cast<long?>().Max());
    public static readonly MethodInfo MaxInt64 = M(() => _Object.Cast<long>().Max());
    public static readonly MethodInfo MaxNullableInt32 = M(() => _Object.Cast<int?>().Max());
    public static readonly MethodInfo MaxInt32 = M(() => _Object.Cast<int>().Max());
    public static readonly MethodInfo MaxTSource = M(() => _Object.Cast<string>().Max());
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
    public static readonly MethodInfo MinNullableDecimal = M(() => _Object.Cast<decimal?>().Min());
    public static readonly MethodInfo MinDecimal = M(() => _Object.Cast<decimal>().Min());
    public static readonly MethodInfo MinNullableDouble = M(() => _Object.Cast<double?>().Min());
    public static readonly MethodInfo MinDouble = M(() => _Object.Cast<double>().Min());
    public static readonly MethodInfo MinNullableSingle = M(() => _Object.Cast<float?>().Min());
    public static readonly MethodInfo MinSingle = M(() => _Object.Cast<float>().Min());
    public static readonly MethodInfo MinNullableInt32 = M(() => _Object.Cast<int?>().Min());
    public static readonly MethodInfo MinInt32 = M(() => _Object.Cast<int>().Min());
    public static readonly MethodInfo MinNullableInt64 = M(() => _Object.Cast<long?>().Min());
    public static readonly MethodInfo MinInt64 = M(() => _Object.Cast<long>().Min());
    public static readonly MethodInfo MinTSource = M(() => _Object.Cast<string>().Min());
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
    /// <summary>
    /// SelectMany_collectionSelector.Select_selector
    /// 最適化で消される対象
    /// </summary>
    public static readonly MethodInfo SelectMany_collectionSelector_resultSelector = M(() => _Int32.SelectMany(p => _Int32,(p,q) => 0));        //変形で削除される
    public static readonly MethodInfo Single = M(() => _Int32.Single());
    public static readonly MethodInfo SingleOrDefault = M(() => _Int32.SingleOrDefault());
    public static readonly MethodInfo SingleOrDefault_defaultValue = M(() => _Int32.SingleOrDefault(0));
    public static readonly MethodInfo SumNullableDecimal = M(() => _Object.Cast<decimal?>().Sum());
    public static readonly MethodInfo SumDecimal = M(() => _Object.Cast<decimal>().Sum());
    public static readonly MethodInfo SumNullableDouble = M(() => _Object.Cast<double?>().Sum());
    public static readonly MethodInfo SumDouble = M(() => _Object.Cast<double>().Sum());
    public static readonly MethodInfo SumNullableSingle = M(() => _Object.Cast<float?>().Sum());
    public static readonly MethodInfo SumSingle = M(() => _Object.Cast<float>().Sum());
    public static readonly MethodInfo SumNullableInt64 = M(() => _Object.Cast<long?>().Sum());
    public static readonly MethodInfo SumInt64 = M(() => _Object.Cast<long>().Sum());
    public static readonly MethodInfo SumNullableInt32 = M(() => _Object.Cast<int?>().Sum());
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
    //Enumerable独自インラインメソッド
    public static readonly MethodInfo AsEnumerable =M(() => _Int32.AsEnumerable());
    public static readonly MethodInfo Any_predicate = M(() => _Int32.Any(p => true));
    public static readonly MethodInfo Distinct0 = M(() => _Int32.Distinct());
    public static readonly MethodInfo Distinct1 = M(() => _Int32.Distinct(null));
    public static readonly MethodInfo Except_comparer = M(() => _Int32.Except(default!,null));
    public static readonly MethodInfo GroupBy_keySelector_comparer = M(() => _Int32.GroupBy(p => p,null));
    /// <summary>
    /// GroupBy_keySelector_comparer.Select_selector
    /// 最適化で消される対象
    /// </summary>
    public static readonly MethodInfo GroupBy_keySelector_resultSelector_comparer = M(() => _Int32.GroupBy(p => p,(a,b) => a,null));
    public static readonly MethodInfo GroupBy_keySelector_elementSelector_comparer = M(() => _Int32.GroupBy(p => p,a => a,null));
    public static readonly MethodInfo GroupBy_keySelector_elementSelector_resultSelector_comparer = M(() => _Int32.GroupBy(p => p,a => a,(a,b) => a,null));
    public static readonly MethodInfo GroupJoin_comparer = M(() => _Int32.GroupJoin(_Int32,a => a,b => b,(a,b) => a,null));
    public static readonly MethodInfo InternalMax_selector = M(() => _Int32.Max(p => ""));
    public static readonly MethodInfo InternalMin_selector = M(() => _Int32.Min(p => ""));
    public static readonly MethodInfo Intersect_comparer = M(() => _Int32.Intersect(default!,null));
    public static readonly MethodInfo Join_comparer = M(() => _Int32.Join(_Int32,a => a,b => b,(a,b) => a,null));
    public static readonly MethodInfo ToLookup_keySelector = M(() => _Int32.ToLookup(p => p));
    public static readonly MethodInfo ToLookup_keySelector_comparer = M(() => _Int32.ToLookup(p => p,EqualityComparer<int>.Default));
    public static readonly MethodInfo ToLookup_keySelector_elementSelector = M(() => _Int32.ToLookup(p => p,p=>p));
    public static readonly MethodInfo ToLookup_keySelector_elementSelector_comparer = M(() => _Int32.ToLookup(p => p,p=>p,EqualityComparer<int>.Default));
    public static readonly MethodInfo ToLookup_indexKeySelector = M(() => _Int32.ToLookup((p,index) => p+index));
    public static readonly MethodInfo ToLookup_indexKeySelector_comparer = M(() => _Int32.ToLookup((p,index) => p+index,null!));
    public static readonly MethodInfo Repeat = M(() => Extension.Repeat(0,0));
    public static readonly MethodInfo Reverse = M(() => _Int32.Reverse());
    public static readonly MethodInfo Select_indexSelector = M(() => _Int32.Select((p,i) => p));
    public static readonly MethodInfo SelectMany_indexSelector = M(() => _Int32.SelectMany((p,i) => _Int32));
    /// <summary>
    /// SelectMany_indexCollectionSelector.Select_selector
    /// 最適化で消される対象
    /// </summary>
    public static readonly MethodInfo SelectMany_indexCollectionSelector_resultSelector = M(() => _Int32.SelectMany((p,i) => _Int32,(a,b) => a));//変形で削除される
    public static readonly MethodInfo Single_predicate = M(() => _Int32.Single(null!));
    public static readonly MethodInfo SingleOrDefault_predicate = M(() => _Int32.SingleOrDefault(null!));
    public static readonly MethodInfo SingleOrDefault_predicate_defaultValue = M(() => _Int32.SingleOrDefault(p=>true,0));
    public static readonly MethodInfo Take_count = M(() => _Int32.Take(0));
    public static readonly MethodInfo Take_range= M(() => _Int32.Take(new System.Range()));
    public static readonly MethodInfo TakeWhile = M(() => _Int32.TakeWhile(p => true));
    public static readonly MethodInfo TakeWhile_index = M(() => _Int32.TakeWhile((p,i) => true));
    public static readonly MethodInfo ToArray = M(() => _Int32.ToArray());
    public static readonly MethodInfo Union_comparer = M(() => _Int32.Union(default!,null));
    public static readonly MethodInfo Where_index = M(() => _Int32.Where((p,i) => true));
}