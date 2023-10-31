using System.Collections.Generic;

namespace LinqDB.Enumerables;

/// <summary>単方向リストの逆方向コンテナ</summary>
/// <typeparam name="T">リスト内の要素の型。</typeparam>
public sealed class DescList<T> : System.Collections.Generic.List<T>, IEnumerable<T>{
    public new IEnumerator<T> GetEnumerator(){
        var List=this;
        for(var a=List.Count-1;a>=0;a--) yield return List[a];
    }
}