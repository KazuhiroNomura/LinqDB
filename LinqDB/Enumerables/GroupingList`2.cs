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
/// <summary>
/// IEnumerable&lt;T>.GroupByの結果の実体
/// </summary>
/// <typeparam name="TElement">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
[DebuggerDisplay("Key = {Key}")]
[DebuggerTypeProxy(typeof(SystemLinq_GroupingDebugView<,>))]
public sealed class GroupingList<TKey,TElement>:InternalList<TElement>
    ,Linq.IGrouping<TKey,TElement>
    ,IEquatable<IGrouping<TKey,TElement>>
    ,IEquatable<Linq.IGrouping<TKey,TElement>>{//.IGrouping<TKey,TValue>{
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
    private static readonly Serializers.MemoryPack.Formatters.Enumerables.GroupingList<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Enumerables.GroupingList<TKey,TElement>.Instance;
    private static readonly Serializers.MessagePack.Formatters.Enumerables.GroupingList<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Enumerables.GroupingList<TKey,TElement>.Instance;
    private static readonly Serializers.Utf8Json.Formatters.Enumerables.GroupingList<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Enumerables.GroupingList<TKey,TElement>.Instance;
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    //static GroupingList()=>MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Enumerables.GroupingList<TKey,TElement>.Instance);
    /// <summary>キーを取得します。</summary>
    /// <returns>キー。</returns>
    public TKey Key{get;}
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="Key"></param>
    public GroupingList(TKey Key)=>this.Key=Key;
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    /// <param name="Value">1つのタプル</param>
    public GroupingList(TKey Key,TElement Value):this(Key){
        this.Key=Key;
        this.Add(Value);
    }
    /// <summary>
    /// 文字列で表現する
    /// </summary>
    public override string ToString(){
        var sb=new StringBuilder("(").Append(this.Key).Append(')');
        foreach(var a in this) sb.Append(a).Append(',');
        sb.Length--;
        return sb.ToString();
    }
    /// <summary>
    /// IEnumerable&lt;T>.GroupByの結果を順序も考慮したHashCode
    /// </summary>
    /// <returns>HashCode</returns>
    public override int GetHashCode()=>this.Key!.GetHashCode();
    private bool PrivateEquals(IGrouping<TKey,TElement> other)=>System.Collections.Generic.EqualityComparer<TKey>.Default.Equals(this.Key,other.Key)&&this.Equals(other);
    public bool Equals(IGrouping<TKey,TElement>? other){
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        return this.PrivateEquals(other);
    }
    public bool Equals(Linq.IGrouping<TKey,TElement>? other){
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        var value=new GroupingSet<TKey,TElement>(other.Key);
        foreach(var a in other) value.Add(a);
        return this.PrivateEquals(value);
    }
    //Linq.IGrouping<TKey,TElement>
    public override bool Equals(object? obj)=>obj switch{
        IGrouping<TKey,TElement> other=>this.Equals(other),
        Linq.IGrouping<TKey,TElement> other=>this.Equals(other),
        _=>false
    };
}
