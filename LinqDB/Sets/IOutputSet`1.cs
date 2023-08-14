#define 並列化
using System.Collections.Generic;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ArrangeStaticMemberQualifier
namespace LinqDB.Sets;

/// <summary>
/// 具象Typeでインターフェース探索と既定クラス探索を別に行うのは大変なのでそのインターフェース。
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOutputSet<out T>:IEnumerable<T>,IOutputSet {
    //new ImmutableSet<T>.Enumerator GetEnumerator();
    T Sampling { get; }
    //ImmutableSet<T>.TreeNode TreeRoot { get; }
}
