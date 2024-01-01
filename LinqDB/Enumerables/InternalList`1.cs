using Collections=System.Collections;using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using LinqDB.Sets;
using Linq = System.Linq;
namespace LinqDB.Enumerables;
using Generic=Collections.Generic;
public class InternalList<T>:Generic.ICollection<T>{
    [MemoryPack.MemoryPackInclude]
    protected readonly Generic.List<T>List=new();
    /// <summary>
    /// コレクション初期化につかった
    /// </summary>
    /// <param name="Item"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T Item)=>this.List.Add(Item);
    /// <summary>
    /// インラインループ独立の都合上"IsAdded"という名前。戻り値は必要ないのでvoid
    /// </summary>
    /// <param name="Item"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void IsAdded(T Item)=>this.List.Add(Item);
    public int Count=> this.List.Count;
    [MessagePack.IgnoreMember]
    public long LongCount=> this.List.Count;

    bool Generic.ICollection<T>.IsReadOnly => throw new NotImplementedException();

    public virtual Generic.List<T>.Enumerator GetEnumerator()=>this.List.GetEnumerator();
    Generic.IEnumerator<T> Generic.IEnumerable<T>.GetEnumerator()=>this.List.GetEnumerator();
    Collections.IEnumerator Collections.IEnumerable.GetEnumerator()=>this.List.GetEnumerator();

    void Generic.ICollection<T>.Clear()=>this.List.Clear();
    bool Generic.ICollection<T>.Contains(T item)=>this.List.Contains(item);
    void Generic.ICollection<T>.CopyTo(T[] array,int arrayIndex)=>this.List.CopyTo(array,arrayIndex);
    bool Generic.ICollection<T>.Remove(T item)=>this.List.Remove(item);
}
