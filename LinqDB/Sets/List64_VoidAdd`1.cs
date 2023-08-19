using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LinqDB.Sets;

public abstract class List64_VoidAdd<T>:ICollection<T> {
    protected readonly List<T> 委譲 = new();
    /// <summary>
    /// DUnionの2度目は戻り値のあるIsAddedメソッドを使う。本メソッドは戻り値は必要ないが"IsAdded"という名前を検索するのでこれにしている。
    /// </summary>
    /// <param name="Item"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void IsAdded(T Item)=>this.Add(Item);
    /// <summary>
    /// 要素の追加処理。
    /// </summary>
    /// <param name="Item"></param>
    public void Add(T Item) => this.委譲.Add(Item);
    public void Clear(){
        throw new System.NotImplementedException();
    }
    public bool Contains(T item){
        throw new System.NotImplementedException();
    }
    public void CopyTo(T[] array,int arrayIndex){
        throw new System.NotImplementedException();
    }
    public bool Remove(T item){
        throw new System.NotImplementedException();
    }
    int ICollection<T>.Count=>(int)this.Count;
    public bool IsReadOnly=>false;

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