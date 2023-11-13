using System;
using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>
/// 集合演算に便利な拡張メソッド。インライン展開しないのでSetクラスには書かない。
/// </summary>
public static class Helpers {
    /// <summary>
    /// 左辺式を評価してパラメータ変数で使用できるようにする。
    /// </summary>
    /// <param name="input">評価したい式</param>
    /// <param name="func">パラメータ変数を使うデリゲート</param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult Let<T,TResult>(this T input,Func<T,TResult> func)=>func(input);
    public static TResult Let2<TResult>(Func<TResult> func)=>func();
    ///// <summary>
    ///// DictionarySet`2からキーが一致する集合を返す。
    ///// </summary>
    ///// <param name="source"></param>
    ///// <param name="key"></param>
    ///// <typeparam name="TSource"></typeparam>
    ///// <typeparam name="TKey"></typeparam>
    ///// <returns></returns>
    //public static ImmutableSet<TSource> Dictionary_Equal<TSource, TKey>(this DictionarySet<TSource,TKey> source,TKey key) => source[key];
    ///// <summary>
    ///// ハッシュアルゴリズムの等価比較に使う。
    ///// </summary>
    ///// <param name="source"></param>
    ///// <param name="key"></param>
    ///// <typeparam name="TKey"></typeparam>
    ///// <typeparam name="T"></typeparam>
    ///// <returns></returns>
    //public static IEnumerable<T> Dictionary_Equal<T, TKey>(this DictionaryAscList<T,TKey> source,TKey key) => source[key];
    ///// <summary>
    ///// ハッシュアルゴリズムの等価比較に使う。
    ///// </summary>
    ///// <param name="source"></param>
    ///// <param name="key"></param>
    ///// <typeparam name="TKey"></typeparam>
    ///// <typeparam name="T"></typeparam>
    ///// <returns></returns>
    //public static IEnumerable<T> Dictionary_Equal<T, TKey>(this DictionaryAscList<T,TKey> source,Object key) => source[key];
    /// <summary>
    /// valuesからSetを作る
    /// </summary>
    /// <param name="values">元データ</param>
    /// <typeparam name="T">要素の型</typeparam>
    /// <returns>作られたSet</returns>
    public static Set<T> ToSet<T>(this System.Collections.Generic.IEnumerable<T> values)=> new(values);
    /// <summary>
    /// valuesからSetを作る
    /// </summary>
    /// <param name="values">元データ</param>
    /// <typeparam name="T">要素の型</typeparam>
    public static Set<T> ToSet<T>(this T[] values)=> new(values);
    /// <summary>
    /// ループの外出しまたはラムダの外出しをしない。
    /// </summary>
    /// <param name="input">外出ししたくない式</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T NoLoopUnrolling<T>(this T input) => input;
    /// <summary>
    /// ループの外出しまたはラムダの外出しをしない。
    /// </summary>
    /// <param name="input">先行評価したくない式</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T NoEarlyEvaluation<T>(this T input) => input;
    //public static bool Equals<T>(T a,T b) => EqualityComparer<T>.Default.Equals(a,b);
}