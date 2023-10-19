using System;
using Collections=System.Collections;
using System.Runtime.CompilerServices;

namespace LinqDB.Sets;
using Generic=Collections.Generic;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
public sealed partial class HashSet_VoidAdd<T>:Generic.ICollection<T>
{
    private readonly Generic.HashSet<T> HashSet=new();
    /// <summary>
    /// DUnion,Intersectの2度目は戻り値のあるIsAddedメソッドを使う。本メソッドは戻り値は必要ないが"IsAdded"という名前を検索するのでこれにしている。
    /// </summary>
    /// <param name="Item"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void IsAdded(T Item)=>this.HashSet.Add(Item);
    [MessagePack.IgnoreMember]
    public long LongCount => this.HashSet.Count;

    int Generic.ICollection<T>.Count => this.HashSet.Count;

    bool Generic.ICollection<T>.IsReadOnly => false;

    public Generic.HashSet<T>.Enumerator GetEnumerator()=>this.HashSet.GetEnumerator();
    Collections.IEnumerator Collections.IEnumerable.GetEnumerator()=>this.GetEnumerator();
    Generic.IEnumerator<T> Generic.IEnumerable<T>.GetEnumerator()=>this.GetEnumerator();

    void Generic.ICollection<T>.Add(T item)=>this.HashSet.Add(item);
    void Generic.ICollection<T>.Clear()=>this.HashSet.Clear();
    bool Generic.ICollection<T>.Contains(T item)=>this.HashSet.Contains(item);
    void Generic.ICollection<T>.CopyTo(T[] array,int arrayIndex)=>this.HashSet.CopyTo(array,arrayIndex);
    bool Generic.ICollection<T>.Remove(T item)=>this.HashSet.Remove(item);
}