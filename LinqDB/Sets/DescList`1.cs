using System.Collections.Generic;
using System.Collections.Immutable;
using LinqDB.Enumerables;

namespace LinqDB.Sets;

/// <summary>単方向リストの逆方向コンテナ</summary>
/// <typeparam name="T">リスト内の要素の型。</typeparam>
public sealed class DescList<T>:AscList<T>{
    public override List<T>.Enumerator GetEnumerator(){
        var List=this.List;
        List.Reverse();
        return List.GetEnumerator();
    }
}