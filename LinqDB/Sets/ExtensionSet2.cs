using System.Diagnostics;
// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable RedundantAssignment
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable PossibleNullReferenceException

// ReSharper disable LoopCanBeConvertedToQuery
namespace LinqDB.Sets;

/// <summary>
/// 集合演算用拡張メソッド。
/// </summary>
public static class ExtensionSet2{
    /// <summary>
    /// sourceにsetを追加する。
    /// </summary>
    /// <param name="source">追加される。</param>
    /// <param name="set">これを追加する。</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns>追加したタプル数。</returns>
    public static long Insert<TSource>(this ImmutableSet<TSource> source,ImmutableSet<TSource> set) {
        long count = 0;
        foreach(var a in set) {
            if(source.InternalAdd(a)) {
                count++;
            }
        }
        source._Count=count;
        return count;
    }
    /// <summary>現在の <see cref="ImmutableSet{T}" /> オブジェクトを、そのオブジェクトと指定されたコレクションの (両方に存在するのではなく) どちらか一方に存在する要素だけが格納されるように変更します。</summary>
    /// <param name="first">secondオブジェクトと比較するコレクション。</param>
    /// <param name="second">firstオブジェクトと比較するコレクション。</param>
    public static ImmutableSet<T> SymmetricExcept<T>(this ImmutableSet<T> first,ImmutableSet<T> second) {
        var Result = new Set<T>();
        var Count = 0L;
        foreach(var a in first) {
            if(!ExtensionSet.Contains(second,a)) {
                var r = Result.InternalAdd(a);
                Debug.Assert(r);
                Count++;
            }
        }
        foreach(var a in second) {
            if(!ExtensionSet.Contains(first,a)) {
                var r = Result.InternalAdd(a);
                Debug.Assert(r);
                Count++;
            }
        }
        Result._Count=Count;
        return Result;
    }
}
