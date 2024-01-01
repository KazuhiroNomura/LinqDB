using System;
using LinqDB.Enumerables;
using Generic = System.Collections.Generic;

// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable RedundantAssignment

// ReSharper disable LoopCanBeConvertedToQuery
namespace LinqDB.Sets;

/// <summary>
/// 集合演算用拡張メソッド。
/// </summary>
public static class ExtensionEnumerable{
    ///// <summary>
    ///// 共通Sourceで異なる複数のselectorでValuTupleに返す。
    ///// </summary>
    ///// <typeparam name="TSource"></typeparam>
    ///// <typeparam name="TResult"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="aggregatesSelector"></param>
    ///// <returns></returns>
    //public static TResult Aggregates<TSource, TResult>(this IEnumerable<TSource> source,Func<IEnumerable<TSource>,TResult> aggregatesSelector) {
    //    Debug.Assert(source is not null&&aggregatesSelector is not null);
    //    Debug.Assert(source is not null&&aggregatesSelector is not null);
    //    return aggregatesSelector(source);
    //}
    private static Lookup<TKey,TElement> PrivateDictionaryList<TElement, TKey>(this Generic.IEnumerable<TElement> source,Func<TElement,TKey> keySelector,Generic.IEqualityComparer<TKey> comparer) {
        var r = new Lookup<TKey,TElement>(comparer);
        foreach(var value in source)
            r.AddKeyValue(
                keySelector(value),
                value
            );
        return r;
    }
    private static Lookup<TKey,TElement> PrivateDictionaryList<TElement, TKey>(this Generic.IEnumerable<TElement> source,Func<TElement,int,TKey> keySelector,Generic.IEqualityComparer<TKey> comparer) {
        var r = new Lookup<TKey,TElement>(comparer);
        var Index=0;
        foreach(var value in source)
            r.AddKeyValue(
                keySelector(value,Index++),
                value
            );
        return r;
    }
    ///// <summary>
    ///// ハッシュアルゴリズムに使う。
    ///// </summary>
    ///// <param name="source"></param>
    ///// <param name="keySelector"></param>
    ///// <typeparam name="TSource"></typeparam>
    ///// <typeparam name="TKey"></typeparam>
    ///// <returns></returns>
    //public static SetGroupingList<TKey,TSource> ToLookup<TSource, TKey>(this Generic.IEnumerable<TSource> source,Func<TSource,TKey> keySelector) => PrivateDictionaryList(source,keySelector,Generic.EqualityComparer<TKey>.Default);
    ///// <summary>
    ///// ハッシュアルゴリズムに使う。comparerで比較する。
    ///// </summary>
    ///// <param name="source"></param>
    ///// <param name="keySelector"></param>
    ///// <param name="comparer"></param>
    ///// <typeparam name="TElement"></typeparam>
    ///// <typeparam name="TKey"></typeparam>
    ///// <returns></returns>
    //public static SetGroupingList<TKey,TElement> ToLookup<TElement, TKey>(this Generic.IEnumerable<TElement> source,Func<TElement,TKey> keySelector,Generic.IEqualityComparer<TKey> comparer) => PrivateDictionaryList(source,keySelector,comparer);
    /// <summary>
    /// ハッシュアルゴリズムに使う。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static System.Linq.ILookup<TKey, TSource>ToLookup<TSource, TKey>(this Generic.IEnumerable<TSource> source,Func<TSource,int,TKey> keySelector) => PrivateDictionaryList(source,keySelector,Generic.EqualityComparer<TKey>.Default);
    /// <summary>
    /// ハッシュアルゴリズムに使う。comparerで比較する。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static System.Linq.ILookup<TKey, TSource>ToLookup<TSource, TKey>(this Generic.IEnumerable<TSource> source,Func<TSource,int,TKey> keySelector,Generic.IEqualityComparer<TKey> comparer) => PrivateDictionaryList(source,keySelector,comparer);
}