using Collections=System.Collections;
using System.Diagnostics;
using LinqDB.Sets;
using System;
using System.Runtime.CompilerServices;
namespace LinqDB.Enumerables;
using Generic=Collections.Generic;

/// <summary>単方向リストのコンテナ</summary>
/// <typeparam name="T">リスト内の要素の型。</typeparam>
[DebuggerTypeProxy(typeof(SetDebugView<>))]
//[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
//public class List<T>:Generic.ICollection<T>{//},IMemoryPackable<AscList<T>>{
//    private static readonly Serializers.MemoryPack.Formatters.Enumerables.AscList<T> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Enumerables.AscList<T>.Instance;
//    private static readonly Serializers.MessagePack.Formatters.Enumerables.AscList<T> InstanceMessagePack=Serializers.MessagePack.Formatters.Enumerables.AscList<T>.Instance;
//    private static readonly Serializers.Utf8Json.Formatters.Enumerables.AscList<T> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Enumerables.AscList<T>.Instance;
//    [MemoryPack.MemoryPackInclude]
//    protected readonly Generic.List<T>_List;
//    public List(){
//        this._List=new();
//    }
//    [MemoryPack.MemoryPackConstructor]
//    public List(Generic.List<T> List)=>this._List=List;
//    /// <summary>
//    /// コレクション初期化につかった
//    /// </summary>
//    /// <param name="Item"></param>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public void Add(T Item)=>this._List.Add(Item);
//    /// <summary>
//    /// インラインループ独立の都合上"IsAdded"という名前。戻り値は必要ないのでvoid
//    /// </summary>
//    /// <param name="Item"></param>
//    public void IsAdded(T Item)=>this._List.Add(Item);
//    public int Count=> this._List.Count;
//    [MessagePack.IgnoreMember]
//    public long LongCount=> this._List.Count;
//    bool Generic.ICollection<T>.IsReadOnly => throw new NotImplementedException();
//    public virtual Generic.List<T>.Enumerator GetEnumerator()=>this._List.GetEnumerator();
//    Generic.IEnumerator<T> Generic.IEnumerable<T>.GetEnumerator()=>this._List.GetEnumerator();
//    Collections.IEnumerator Collections.IEnumerable.GetEnumerator()=>this._List.GetEnumerator();
//    void Generic.ICollection<T>.Clear()=>this._List.Clear();
//    bool Generic.ICollection<T>.Contains(T item)=>this._List.Contains(item);
//    void Generic.ICollection<T>.CopyTo(T[] array,int arrayIndex)=>this._List.CopyTo(array,arrayIndex);
//    bool Generic.ICollection<T>.Remove(T item)=>this._List.Remove(item);
//}
public class List<T>:Generic.List<T>{
    private static readonly Serializers.MemoryPack.Formatters.Enumerables.List<T> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Enumerables.List<T>.Instance;
    private static readonly Serializers.MessagePack.Formatters.Enumerables.List<T> InstanceMessagePack=Serializers.MessagePack.Formatters.Enumerables.List<T>.Instance;
    private static readonly Serializers.Utf8Json.Formatters.Enumerables.List<T> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Enumerables.List<T>.Instance;
    /// <summary>
    /// インラインループ独立の都合上"IsAdded"という名前。戻り値は必要ないのでvoid
    /// </summary>
    /// <param name="Item"></param>
    public void IsAdded(T Item)=>this.Add(Item);
}
