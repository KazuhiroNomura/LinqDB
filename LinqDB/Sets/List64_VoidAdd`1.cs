using System.Collections;
using System.Collections.Generic;
namespace LinqDB.Sets;

public abstract class List64_VoidAdd<T>:IEnumerable<T>, IVoidAdd<T> {
    protected readonly List<T> 委譲 = new();
    /// <summary>
    /// 要素の追加処理。
    /// </summary>
    /// <param name="Item"></param>
    public void Add(T Item) => this.委譲.Add(Item);
    /// <summary>
    /// 要素の追加処理。
    /// </summary>
    /// <param name="Item"></param>
    public void VoidAdd(T Item) => this.委譲.Add(Item);
    /// <summary>
    /// 値の列挙子
    /// </summary>
    /// <returns>Enumerator</returns>
    public abstract List<T>.Enumerator GetEnumerator();
    /// <summary>
    /// 要素の追加処理。ConcurrentAdd、VoidConcurrentAdd同士はスレッドセーフ。
    /// </summary>
    /// <param name="Item"></param>
    public void VoidConcurrentAdd(T Item) => this.委譲.Add(Item);
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.委譲.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.委譲.GetEnumerator();
    public long Count => this.委譲.Count;
}