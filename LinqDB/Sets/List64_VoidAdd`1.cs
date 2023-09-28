﻿using Collections=System.Collections;
using System.Runtime.CompilerServices;

namespace LinqDB.Sets;
using Generic=Collections.Generic;

public abstract class List64_VoidAdd<T>:Generic.ICollection<T> {
    protected readonly Generic.List<T> 委譲 = new();
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
    public void Clear()=>this.委譲.Clear();
    public bool Contains(T item)=>this.委譲.Contains(item);
    public void CopyTo(T[] array,int arrayIndex)=>this.委譲.CopyTo(array,arrayIndex);
    public bool Remove(T item)=>this.委譲.Remove(item);
    int Generic.ICollection<T>.Count=>(int)this.LongCount;
    public bool IsReadOnly=>false;

    /// <summary>
    /// 値の列挙子
    /// </summary>
    /// <returns>Enumerator</returns>
    public abstract Generic.List<T>.Enumerator GetEnumerator();
    /// <summary>
    /// 要素の追加処理。ConcurrentAdd、VoidConcurrentAdd同士はスレッドセーフ。
    /// </summary>
    /// <param name="Item"></param>
    public void VoidConcurrentAdd(T Item) => this.委譲.Add(Item);
    Generic.IEnumerator<T> Generic.IEnumerable<T>.GetEnumerator() => this.委譲.GetEnumerator();
    Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.委譲.GetEnumerator();
    public long LongCount => this.委譲.Count;
    public long Count => this.委譲.Count;
}
