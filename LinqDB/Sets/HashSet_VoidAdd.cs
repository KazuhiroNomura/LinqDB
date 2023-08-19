using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqDB.Sets;

public sealed class HashSet_VoidAdd<T>:HashSet<T> {
    /// <summary>
    /// DUnion,Intersectの2度目は戻り値のあるIsAddedメソッドを使う。本メソッドは戻り値は必要ないが"IsAdded"という名前を検索するのでこれにしている。
    /// </summary>
    /// <param name="Item"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void IsAdded(T Item)=>this.Add(Item);
    public new long Count => base.Count;
}