using System;
using Collections=System.Collections;
using System.Diagnostics;
using Linq=System.Linq;
//using System.Linq;

namespace LinqDB.Sets;
using Generic=Collections.Generic;
internal sealed class SystemLinq_GroupingDebugView<TKey, TElement>
{
    private readonly GroupingSet<TKey,TElement>_grouping;

    private TElement[]? _cachedValues;

    public TKey Key => this._grouping.Key;

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public TElement[] Values => this._cachedValues??=this._grouping.ToArray();

    public SystemLinq_GroupingDebugView(GroupingSet<TKey, TElement> grouping)
    {
        this._grouping = grouping;
    }
}

/// <summary>
/// Set&lt;T>.GroupByの結果の実体
/// </summary>
/// <typeparam name="TElement">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
[DebuggerDisplay("Key = {Key}")]
[DebuggerTypeProxy(typeof(SystemLinq_GroupingDebugView<, >))]
public sealed class GroupingSet<TKey,TElement>:ImmutableSet<TElement>,IGroupingCollection<TKey,TElement>,IEquatable<IGrouping<TKey,TElement>>,IEquatable<Linq.IGrouping<TKey,TElement>>{
    public TKey Key{get;}
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    public GroupingSet(TKey Key)=>this.Key=Key;
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    /// <param name="Value">1つのタプル</param>
    public GroupingSet(TKey Key,TElement Value){
        this.Key=Key;
        this.InternalAdd(Value);
        this._LongCount=1;
    }
    public void Add(TElement item) {
        if(this.InternalAdd(item)) this._LongCount++;
    }
    public void Clear() => this.InternalClear();
    public bool Contains(TElement item) => this.InternalContains(item);
    public void CopyTo(TElement[] array,int arrayIndex) {
        foreach(var a in this) {
            array[arrayIndex++]=a;
        }
    }

    public bool Remove(TElement item) => this.InternalRemove(item);
    public int Count => (int)this._LongCount;
    public bool IsReadOnly => false;
    public override int GetHashCode()=>this.Key!.GetHashCode();
    private bool PrivateEquals(IGrouping<TKey,TElement> other)=>Generic.EqualityComparer<TKey>.Default.Equals(this.Key,other.Key)&&this.SetEquals(other);
    public bool Equals(IGrouping<TKey,TElement>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        return this.PrivateEquals(other);
    }
    public bool Equals(Linq.IGrouping<TKey,TElement>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        var value=new GroupingSet<TKey,TElement>(other.Key);
        foreach(var a in other) value.Add(a);
        return this.PrivateEquals(value);
    }
    //Linq.IGrouping<TKey,TElement>
    public override bool Equals(object? obj){
        switch(obj){
            case IGrouping<TKey,TElement>other:return this.Equals(other);
            case Linq.IGrouping<TKey,TElement>other:return this.Equals(other);
            default:return false;
        }
    }
}
