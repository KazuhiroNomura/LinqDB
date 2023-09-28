using System.Collections;
using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>
/// Set&lt;T>.GroupByの結果の実体
/// </summary>
/// <typeparam name="TValue">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
public sealed class GroupingSet<TKey,TValue>:ImmutableSet<TValue>,IGrouping<TKey,TValue>,ICollection<TValue>{
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
    public GroupingSet(TKey Key,TValue Value):this(Key){
        this.InternalAdd(Value);
        this._LongCount=1;
    }



    public void Add(TValue item){
        if(this.InternalAdd(item)) this._LongCount++;
    }


    public void Clear()=>this.InternalClear();
    public bool Contains(TValue item)=>this.InternalContains(item);
    public void CopyTo(TValue[] array,int arrayIndex){
        throw new System.NotImplementedException();
    }

    public bool Remove(TValue item)=>this.InternalRemove(item);
    public int Count{get;}
    public bool IsReadOnly=>false;
    public override int GetHashCode()=>this.Key!.GetHashCode();
}
