using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace LinqDB.Sets;

/// <summary>
/// Set.GroupByの結果、Set.GroupJoin,Set.Joinのビルド結果で使うDictionaryコレクション
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TValue">値のType</typeparam>
[Serializable]
public sealed class SetGroupingSet<TKey, TValue>:SetGrouping<TKey,TValue,GroupingSet<TKey,TValue>> {
    //public ImmutableSet<TValue> this[TKey key] {
    //    get {
    //        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)key!.GetHashCode());
    //        if(TreeNode is not null) {
    //            var KeyComparer = this.KeyComparer;
    //            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
    //                if(KeyComparer.Equals(a.Item.Key,key)) {
    //                    return a.Item;
    //                }
    //            }
    //        }
    //        throw new NotImplementedException();
    //    }
    //}
    private SetGroupingSet(SerializationInfo SerializationInfo,StreamingContext StreamingContext) : base(SerializationInfo,StreamingContext) {
    }
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public SetGroupingSet(){}
    /// <summary>
    /// 比較方法を指定したコンストラクタ
    /// </summary>
    /// <param name="KeyComparer">比較方法</param>
    public SetGroupingSet(IEqualityComparer<TKey> KeyComparer) : base(KeyComparer) { }
    internal override GroupingSet<TKey,TValue> InternalKeyValue(TKey Key,TValue Value)=>new(Key,Value);
}