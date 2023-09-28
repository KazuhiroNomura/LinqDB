using System;
using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>
/// Enumerable.GroupByの結果の実体。
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TValue">値のType</typeparam>
[Serializable]
public sealed class SetGroupingAscList<TKey,TValue>:SetGrouping<TKey,TValue,GroupingAscList<TKey,TValue>>{
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public SetGroupingAscList() {}
    /// <summary>
    /// 比較方法を指定したコンストラクタ
    /// </summary>
    /// <param name="KeyComparer">比較方法</param>
    public SetGroupingAscList(IEqualityComparer<TKey> KeyComparer):base(KeyComparer){}
    internal override GroupingAscList<TKey,TValue> InternalKeyValue(TKey Key,TValue Value)=>new(Key,Value);
}