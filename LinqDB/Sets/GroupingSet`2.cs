using System;
using System.Diagnostics;
using Linq=System.Linq;
using System.Text.Json.Serialization;
//using System.Linq;

namespace LinqDB.Sets;
using Generic=System.Collections.Generic;

/// <summary>
/// Set&lt;T>.GroupByの結果の実体
/// </summary>
/// <typeparam name="TElement">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
[DebuggerDisplay("Key = {Key}")]
[DebuggerTypeProxy(typeof(SystemLinq_GroupingDebugView<, >))]
public sealed class GroupingSet<TKey,TElement>:Set<TElement>
    ,IGrouping<TKey,TElement>/*,ICollection<TElement>*/,IEquatable<IGrouping<TKey,TElement>>
    ,IEquatable<Linq.IGrouping<TKey,TElement>>{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private new static readonly Serializers.MemoryPack.Formatters.Sets.GroupingSet<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.GroupingSet<TKey,TElement>.Instance;
    private new static readonly Serializers.MessagePack.Formatters.Sets.GroupingSet<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.GroupingSet<TKey,TElement>.Instance;
    private new static readonly Serializers.Utf8Json.Formatters.Sets.GroupingSet<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.GroupingSet<TKey,TElement>.Instance;
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
    //static GroupingSet()=>MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Sets.GroupingSet<TKey,TElement>.Instance);
    public TKey _Key;
    [JsonInclude]
    public TKey Key=>this._Key;
    //public TKey Key{get;}
    ///// <summary>
    ///// MessagePackが必要とする
    ///// </summary>
    //public GroupingSet()=>this._Key=default!;
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    public GroupingSet(TKey Key)=>this._Key=Key;
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    /// <param name="Value">1つのタプル</param>
    public GroupingSet(TKey Key,TElement Value){
        this._Key=Key;
        this.InternalAdd(Value);
        this._LongCount=1;
    }
    public override int GetHashCode()=>this.Key is null?0:this.Key!.GetHashCode();
    private bool PrivateEquals(IGrouping<TKey,TElement> other)=> Generic.EqualityComparer<TKey>.Default.Equals(this.Key,other.Key)&&this.SetEquals(other);
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
internal sealed class SystemLinq_GroupingDebugView<TKey,TElement>{
    private readonly GroupingSet<TKey,TElement> _grouping;

    private TElement[]? _cachedValues;

    public TKey Key=>this._grouping.Key;

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public TElement[] Values=>this._cachedValues??=this._grouping.ToArray();

    public SystemLinq_GroupingDebugView(GroupingSet<TKey,TElement> grouping){
        this._grouping=grouping;
    }
}
