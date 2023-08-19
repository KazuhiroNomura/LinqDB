using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>
/// Set&lt;T>.GroupByの結果の実体
/// </summary>
/// <typeparam name="TValue">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
public sealed class GroupingSet<TKey, TValue>:ImmutableGroupingSet<TKey,TValue>, ICollection<TValue> {
    /// <summary>
    /// 既定コンストラクタ。2回ループするときの作業用に必要。
    /// </summary>
    public GroupingSet():base(default!){ }
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    public GroupingSet(TKey Key) : base(Key) { }
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    /// <param name="Value">1つのタプル</param>
    public GroupingSet(TKey Key,TValue Value) : base(Key,Value) {}

    public bool IsReadOnly => throw new System.NotImplementedException();

    int ICollection<TValue>.Count => throw new System.NotImplementedException();

    public void Add(TValue item) {
        if(this.InternalAdd(item))this._Count++;
    }

    public void Clear() {
        throw new System.NotImplementedException();
    }

    public bool Contains(TValue item) {
        throw new System.NotImplementedException();
    }

    public void CopyTo(TValue[] array,int arrayIndex) {
        throw new System.NotImplementedException();
    }

    public bool Remove(TValue item) {
        throw new System.NotImplementedException();
    }
}