using System;
using System.Linq;
using Collections=System.Collections;
namespace LinqDB.Sets;
using Generic=Collections.Generic;

/// <summary>
/// キーとコレクションを表す。
/// </summary>
/// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TCollection"></typeparam>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage","CA2231:値型 Equals のオーバーライドで、演算子 equals をオーバーロードします",Justification = "<保留中>")]
//public readonly struct KeyValueCollection<TValue,TKey, TCollection>:IEquatable<KeyValueCollection<TValue,TKey,TCollection>>, IGrouping<TKey,TValue>,ICollection<TValue> where TCollection:Generic.ICollection<TValue> {
public class KeyValueCollection<TValue,TKey, TCollection>:IEquatable<KeyValueCollection<TValue,TKey,TCollection>>, IGrouping<TKey,TValue>,ICollection<TValue> where TCollection:Generic.ICollection<TValue> {
    //internal TKey Key;
    public TKey Key { get; }
    internal readonly TCollection Collection;
    /// <param name="Key"></param>
    /// <param name="Collection"></param>
    public KeyValueCollection(TKey Key,TCollection Collection) {
        this.Key=Key;
        this.Collection=Collection;
    }
    public bool Equals(KeyValueCollection<TValue,TKey,TCollection>? other) =>other is not null&&Generic.EqualityComparer<TKey>.Default.Equals(this.Key,other.Key);
    public override bool Equals(object? obj) => this.Equals(obj as KeyValueCollection<TValue,TKey,TCollection>);
    public Generic.IEnumerator<TValue> GetEnumerator() =>this.Collection.GetEnumerator();
    Collections.IEnumerator Collections.IEnumerable.GetEnumerator() =>this.Collection.GetEnumerator();
    public override int GetHashCode() => this.Key!.GetHashCode();
    public void Add(TValue Item) => this.Collection.Add(Item);
    //IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    public void Clear()=>this.Collection.Clear();
    public bool Contains(TValue item)=>this.Collection.Contains(item);
    //public bool Contains(TValue item)=>this.Collection.Contains(item);
    public void CopyTo(TValue[] array,int arrayIndex)=>this.Collection.CopyTo(array,arrayIndex);
    public bool Remove(TValue item)=>this.Collection.Remove(item);
    public int Count=>this.Collection.Count;
    public bool IsReadOnly=>true;

    public long LongCount =>this.Collection.Count;
}