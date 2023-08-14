using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>単方向リストのコンテナ</summary>
/// <typeparam name="T">リスト内の要素の型。</typeparam>
public class AscList<T>:List64_VoidAdd<T>{
    /// <summary>
    /// 空のList
    /// </summary>
    public static readonly AscList<T> EmptyList=new();
    public override List<T>.Enumerator GetEnumerator() => this.委譲.GetEnumerator();
}