using System.Collections.Generic;
using System.Diagnostics;
using LinqDB.Sets;

namespace LinqDB.Enumerables;

/// <summary>単方向リストのコンテナ</summary>
/// <typeparam name="T">リスト内の要素の型。</typeparam>
[DebuggerTypeProxy(typeof(SetDebugView<>))]
public class AscList<T>: List64_VoidAdd<T>
{
    ///// <summary>
    ///// 空のList
    ///// </summary>
    //public static readonly AscList<T> EmptyList=new();
    public override List<T>.Enumerator GetEnumerator() => this.委譲.GetEnumerator();
}