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
