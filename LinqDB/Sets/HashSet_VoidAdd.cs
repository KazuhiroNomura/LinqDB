using System.Collections.Generic;
namespace LinqDB.Sets;

public sealed class HashSet_VoidAdd<T>:HashSet<T>, IVoidAdd<T> {
    /// <summary>
    /// 要素の追加処理。
    /// </summary>
    /// <param name="Item"></param>
    public void VoidAdd(T Item) => this.Add(Item);
    public new long Count => base.Count;
}