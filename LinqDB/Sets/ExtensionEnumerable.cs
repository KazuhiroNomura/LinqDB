using System;
using System.Collections.Generic;

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
    private static LookupList<TValue,TKey> PrivateDictionaryList<TValue, TKey>(this System.Collections.Generic.IEnumerable<TValue> source,Func<TValue,TKey> keySelector,IEqualityComparer<TKey> comparer) {
        var r = new LookupList<TValue,TKey>(comparer);
        foreach(var value in source)
            r.AddKeyValue(
                keySelector(value),
                value
            );
        return r;
    }
    /// <summary>
    /// ハッシュアルゴリズムに使う。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static LookupList<TValue,TKey> Lookup<TValue, TKey>(this System.Collections.Generic.IEnumerable<TValue> source,Func<TValue,TKey> keySelector) => PrivateDictionaryList(source,keySelector,EqualityComparer<TKey>.Default);
    /// <summary>
    /// ハッシュアルゴリズムに使う。comparerで比較する。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static LookupList<TValue,TKey> Lookup<TValue, TKey>(this System.Collections.Generic.IEnumerable<TValue> source,Func<TValue,TKey> keySelector,IEqualityComparer<TKey> comparer) => PrivateDictionaryList(source,keySelector,comparer);
    private static LookupList<TValue,TKey> PrivateDictionaryList<TValue, TKey>(this System.Collections.Generic.IEnumerable<TValue> source,Func<TValue,int,TKey> keySelector,IEqualityComparer<TKey> comparer) {
        var r = new LookupList<TValue,TKey>(comparer);
        var Index=0;
        foreach(var value in source)
            r.AddKeyValue(
                keySelector(value,Index++),
                value
            );
        return r;
    }
    /// <summary>
    /// ハッシュアルゴリズムに使う。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static LookupList<TValue,TKey> Lookup<TValue, TKey>(this System.Collections.Generic.IEnumerable<TValue> source,Func<TValue,int,TKey> keySelector) => PrivateDictionaryList(source,keySelector,EqualityComparer<TKey>.Default);
    /// <summary>
    /// ハッシュアルゴリズムに使う。comparerで比較する。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static LookupList<TValue,TKey> Lookup<TValue, TKey>(this System.Collections.Generic.IEnumerable<TValue> source,Func<TValue,int,TKey> keySelector,IEqualityComparer<TKey> comparer) => PrivateDictionaryList(source,keySelector,comparer);
    ///// <summary>
    ///// キーが等しいかどうかに基づいて 2つの集合の要素を相互に関連付け、その結果をグループ化します。
    ///// </summary>
    ///// <returns>2つの集合に対してグループ化結合を実行して取得する、TResult型の要素が格納されている <see cref="IEnumerable{TResult}" />。</returns>
    ///// <param name="outer">結合する最初の集合。</param>
    ///// <param name="inner">最初の集合に結合する集合。</param>
    ///// <param name="outerKeySelector">最初の集合の各要素から結合キーを抽出する関数。</param>
    ///// <param name="resultSelector">最初の集合の要素と、2 番目の集合の一致する要素のコレクションから結果の要素を作成する関数。</param>
    ///// <typeparam name="TOuter">最初の集合の要素の型。</typeparam>
    ///// <typeparam name="TInner">2 番目の集合の要素の型。</typeparam>
    ///// <typeparam name="TKey">キー セレクター関数によって返されるキーの型。</typeparam>
    ///// <typeparam name="TResult">結果の要素の型。</typeparam>
    //internal static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,LookupList<TInner,TKey> inner,Func<TOuter,TKey> outerKeySelector,Func<TOuter,IEnumerable<TInner>,TResult> resultSelector) {
    //    var EmptySet = AscList<TInner>.EmptyList;
    //    var innerValue = EmptySet;
    //    var r = new HashSet<TResult>();
    //    foreach(var a in outer) {
    //        r.Add(inner.TryGetValue(outerKeySelector(a),ref innerValue) ? resultSelector(a,innerValue) : resultSelector(a,EmptySet));
    //    }
    //    return r;
    //}
    ///// <summary>
    ///// ビルド済みDictionaryとSetをJoinする
    ///// </summary>
    ///// <typeparam name="TOuter"></typeparam>
    ///// <typeparam name="TInner"></typeparam>
    ///// <typeparam name="TKey"></typeparam>
    ///// <typeparam name="TResult"></typeparam>
    ///// <param name="outer"></param>
    ///// <param name="inner"></param>
    ///// <param name="outerKeySelector"></param>
    ///// <param name="resultSelector"></param>
    ///// <returns></returns>
    //internal static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,LookupList<TInner,TKey> inner,Func<TOuter,TKey> outerKeySelector,Func<TOuter,TInner,TResult> resultSelector){
    //    var r = new AscList<TResult>();
    //    var Collection = AscList<TInner>.EmptyList;
    //    foreach(var outerValue in outer) {
    //        if(inner.TryGetValue(outerKeySelector(outerValue),ref Collection)) {
    //            foreach(var innerValue in Collection) {
    //                r.Add(resultSelector(outerValue,innerValue));
    //            }
    //        }
    //    }
    //    return r;
    //}
    ///// <summary>ジェネリック 集合の各要素に対して変換関数を呼び出し、結果の最大値を返します。</summary>
    ///// <returns>集合の最大値。要素がないときに例外を発生させない。</returns>
    ///// <param name="source">最大値を確認する対象となる値の集合。</param>
    ///// <param name="selector">各要素に適用する変換関数。</param>
    ///// <typeparam name="TSource">
    /////   <paramref name="source" /> の要素の型。</typeparam>
    ///// <typeparam name="TResult">
    /////   <paramref name="selector" /> によって返される値の型。</typeparam>
    //internal static シーケンスが必要な戻り値<TResult> InternalMax<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> selector) {
    //    using var Enumerator = source!.GetEnumerator();
    //    var Default = Comparer<TResult>.Default;
    //    while(Enumerator.MoveNext()) {
    //        var Item0 = selector!(Enumerator.Current);
    //        while(Enumerator.MoveNext()) {
    //            var Item1 = selector(Enumerator.Current);
    //            if(Default.Compare(Item0,Item1)<0) {
    //                Item0=Item1;
    //            }
    //        }
    //        return new シーケンスが必要な戻り値<TResult> {
    //            Value=Item0
    //        };
    //    }
    //    return new シーケンスが必要な戻り値<TResult> {
    //        Exception=new InvalidOperationException(MethodBase.GetCurrentMethod()+":"+LinqDB.Helpers.CommonLibrary.シーケンスに要素が含まれていません_NoElements)
    //    };
    //}
    ///// <summary>ジェネリック 集合の各要素に対して変換関数を呼び出し、結果の最小値を返します。</summary>
    ///// <returns>集合の最小値。要素がないときに例外を発生させない。</returns>
    ///// <param name="source">最小値を確認する対象となる値の集合。</param>
    ///// <param name="selector">各要素に適用する変換関数。</param>
    ///// <typeparam name="TSource">
    /////   <paramref name="source" /> の要素の型。</typeparam>
    ///// <typeparam name="TResult">
    /////   <paramref name="selector" /> によって返される値の型。</typeparam>
    //public static シーケンスが必要な戻り値<TResult> InternalMin<TSource, TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> selector) {
    //    using var Enumerator = source!.GetEnumerator();
    //    var Default = Comparer<TResult>.Default;
    //    while(Enumerator.MoveNext()) {
    //        var Item0 = selector!(Enumerator.Current);
    //        while(Enumerator.MoveNext()) {
    //            var Item1 = selector(Enumerator.Current);
    //            if(Default.Compare(Item0,Item1)>0) {
    //                Item0=Item1;
    //            }
    //        }
    //        return new シーケンスが必要な戻り値<TResult> {
    //            Value=Item0
    //        };
    //    }
    //    return new シーケンスが必要な戻り値<TResult> {
    //        Exception=new InvalidOperationException(MethodBase.GetCurrentMethod()+":"+LinqDB.Helpers.CommonLibrary.シーケンスに要素が含まれていません_NoElements)
    //    };
    //}
}