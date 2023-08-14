using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>単方向リストの逆方向コンテナ</summary>
/// <typeparam name="T">リスト内の要素の型。</typeparam>
public sealed class DescList<T>:AscList<T>{
    public override List<T>.Enumerator GetEnumerator() {
        this.委譲.Reverse();
        return this.委譲.GetEnumerator();
    }
}