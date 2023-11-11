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
    /// <summary>現在の <see cref="IEnumerable{T}" /> オブジェクトを、そのオブジェクトと指定されたコレクションの (両方に存在するのではなく) どちらか一方に存在する要素だけが格納されるように変更します。</summary>
    /// <param name="first">secondオブジェクトと比較するコレクション。</param>
    /// <param name="second">firstオブジェクトと比較するコレクション。</param>
    public static IEnumerable<T> SymmetricExcept<T>(this IEnumerable<T> first,IEnumerable<T> second){
        var first0=(Set<T>)first;
        var second0=(Set<T>)second;
        var Result = new Set<T>();
        var Count = 0L;
        foreach(var a in first) {
            if(!second.Contains(a)) {
                var r = Result.InternalIsAdded(a);
                Debug.Assert(r);
                Count++;
            }
        }
        foreach(var a in second) {
            if(!first.Contains(a)) {
                var r = Result.InternalIsAdded(a);
                Debug.Assert(r);
                Count++;
            }
        }
        Result._LongCount=Count;
        return Result;
    }
}
