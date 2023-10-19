using System;
using Collections=System.Collections;
using System.Runtime.CompilerServices;

namespace LinqDB.Sets;
using Generic=Collections.Generic;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
public sealed partial class HashSet_VoidAdd<T>:Generic.IEnumerable<T>//:ICollection<T>, ISet<T>, IReadOnlyCollection<T>, IReadOnlySet<T>, ISerializable, IDeserializationCallback
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
    public Generic.HashSet<T>.Enumerator GetEnumerator()=>this.HashSet.GetEnumerator();
    Collections.IEnumerator Collections.IEnumerable.GetEnumerator()=>this.GetEnumerator();
    Generic.IEnumerator<T> Generic.IEnumerable<T>.GetEnumerator()=>this.GetEnumerator();
}