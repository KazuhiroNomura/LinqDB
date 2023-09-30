using System;
using Generic=System.Collections.Generic;
using Linq=System.Linq;
namespace LinqDB.Sets;
/// <summary>
/// Set.GroupByの結果。要素はGroupingSet。Set.GroupJoin,Set.Joinのビルド結果で使うDictionaryコレクション
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TElement">値のType</typeparam>
[Serializable]
public sealed class SetGroupingSet<TKey, TElement>:SetGrouping<TKey,TElement,IGroupingCollection<TKey,TElement>>,
    IEquatable<IEnumerable<IGrouping<TKey,TElement>>>,
    IEquatable<Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>>
    {
    //private static readonly EqualityComparer<TKey,TElement> _EqualityComparer=new();
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public SetGroupingSet(){}
    /// <summary>
    /// 比較方法を指定したコンストラクタ
    /// </summary>
    /// <param name="KeyComparer">比較方法</param>
    public SetGroupingSet(Generic.IEqualityComparer<TKey> KeyComparer) : base(KeyComparer) { }
    internal override IGroupingCollection<TKey,TElement> InternalKeyValue(TKey Key,TElement Value)=>new GroupingSet<TKey,TElement>(Key,Value);
    //internal override GroupingSet<TKey,TElement> InternalKeyValue(TKey Key,TElement Value)=>new(Key,Value);
    public override int GetHashCode(){
        return base.GetHashCode();
    }
    public bool Equals(IEnumerable<IGrouping<TKey,TElement>>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        return base.Equals(other);
    }
    public bool Equals(Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        if(other is IEnumerable<IGrouping<TKey,TElement>> value0)return this.Equals(value0);
        var value1=new SetGroupingSet<TKey,TElement>();
        var Count=0L;
        foreach(var a in other){
            var Grouping=new GroupingSet<TKey,TElement>(a.Key);
            foreach(var b in a){
                Grouping.Add(b);
            }
            value1.InternalAdd(Grouping);
            Count++;
        }
        value1._LongCount=Count;
        return this.Equals(value1);
    }
    public override bool Equals(object? obj){
        switch(obj){
            case IEnumerable<IGrouping<TKey,TElement>>other:return this.Equals(other);
            case Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>other:return this.Equals(other);
            default:return false;
        }
    }
}