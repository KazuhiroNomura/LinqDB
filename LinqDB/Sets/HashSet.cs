using System;
using Collections=System.Collections;
using System.Runtime.CompilerServices;

namespace LinqDB.Sets;
using Generic=Collections.Generic;
///// <summary>
///// System.Collection.Generic.HashSet{T}と同じだがIsAddedメソッドがある。
///// </summary>
///// <typeparam name="T"></typeparam>
////[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
//public sealed partial class HashSet<T>:Generic.ICollection<T>{
//    private static readonly Serializers.MemoryPack.Formatters.Sets.HashSet<T> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.HashSet<T>.Instance;
//    private static readonly Serializers.MessagePack.Formatters.Sets.HashSet<T> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.HashSet<T>.Instance;
//    private static readonly Serializers.Utf8Json.Formatters.Sets.HashSet<T> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.HashSet<T>.Instance;
//    private readonly Generic.HashSet<T> _HashSet=new();
//    /// <summary>
//    /// DUnion,Intersectの2度目は戻り値のあるIsAddedメソッドを使う。本メソッドは戻り値は必要ないが"IsAdded"という名前を検索するのでこれにしている。
//    /// </summary>
//    /// <param name="Item"></param>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public void IsAdded(T Item)=>this._HashSet.Add(Item);
//    [MessagePack.IgnoreMember]
//    public long LongCount => this._HashSet.Count;

//    int Generic.ICollection<T>.Count => this._HashSet.Count;

//    bool Generic.ICollection<T>.IsReadOnly => false;

//    public Generic.HashSet<T>.Enumerator GetEnumerator()=>this._HashSet.GetEnumerator();
//    Collections.IEnumerator Collections.IEnumerable.GetEnumerator()=>this.GetEnumerator();
//    Generic.IEnumerator<T> Generic.IEnumerable<T>.GetEnumerator()=>this.GetEnumerator();

//    void Generic.ICollection<T>.Add(T item)=>this._HashSet.Add(item);
//    void Generic.ICollection<T>.Clear()=>this._HashSet.Clear();
//    bool Generic.ICollection<T>.Contains(T item)=>this._HashSet.Contains(item);
//    void Generic.ICollection<T>.CopyTo(T[] array,int arrayIndex)=>this._HashSet.CopyTo(array,arrayIndex);
//    bool Generic.ICollection<T>.Remove(T item)=>this._HashSet.Remove(item);
//}
/// <summary>
/// System.Collection.Generic.HashSet{T}と同じだがIsAddedメソッドがある。
/// </summary>
/// <typeparam name="T"></typeparam>
//[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
public sealed partial class HashSet<T>:Generic.HashSet<T>{
    private static readonly Serializers.MemoryPack.Formatters.Sets.HashSet<T> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.HashSet<T>.Instance;
    private static readonly Serializers.MessagePack.Formatters.Sets.HashSet<T> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.HashSet<T>.Instance;
    private static readonly Serializers.Utf8Json.Formatters.Sets.HashSet<T> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.HashSet<T>.Instance;
    /// <summary>
    /// DUnion,Intersectの2度目は戻り値のあるIsAddedメソッドを使う。本メソッドは戻り値は必要ないが"IsAdded"という名前を検索するのでこれにしている。
    /// </summary>
    /// <param name="Item"></param>
    public void IsAdded(T Item)=>this.Add(Item);
}