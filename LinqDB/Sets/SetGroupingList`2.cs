using System;
using System.Runtime.CompilerServices;
using LinqDB.Enumerables;
using Generic = System.Collections.Generic;


namespace LinqDB.Sets;

/// <summary>
/// Enumerable.GroupByの結果。要素はGroupingAscList。
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TElement">値のType</typeparam>
public sealed class SetGroupingList<TKey,TElement>:Set<GroupingList<TKey,TElement>>
    //IEquatable<IEnumerable<System.Linq.IGrouping<TKey,TElement>>>,
    //IEquatable<Generic.IEnumerable<System.Linq.IGrouping<TKey,TElement>>>

{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private new static readonly Serializers.MemoryPack.Formatters.Enumerables.SetGroupingList<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Enumerables.SetGroupingList<TKey,TElement>.Instance;
    private new static readonly Serializers.MessagePack.Formatters.Enumerables.SetGroupingList<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Enumerables.SetGroupingList<TKey,TElement>.Instance;
    private new static readonly Serializers.Utf8Json.Formatters.Enumerables.SetGroupingList<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Enumerables.SetGroupingList<TKey,TElement>.Instance;
    /// <summary>
    /// キー比較用EqualityComparer
    /// </summary>
    private readonly Generic.IEqualityComparer<TKey> KeyComparer;


    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public SetGroupingList()=>this.KeyComparer=Generic.EqualityComparer<TKey>.Default;
    /// <summary>
    /// 比較方法を指定したコンストラクタ
    /// </summary>
    /// <param name="KeyComparer">比較方法</param>
    public SetGroupingList(Generic.IEqualityComparer<TKey> KeyComparer)=>this.KeyComparer=KeyComparer;

    public bool Equals(IEnumerable<IGrouping<TKey,TElement>>? other) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 指定したキーと値をディクショナリに追加する。
    /// KeyがなければTGrouping(Key,Value)
    /// KeyがあればTGroupingを取り出しAdd(Value)
    /// </summary>
    /// <param name="Key">追加する要素のキー。</param>
    /// <param name="Value">追加する要素の値。参照型の場合、null の値を使用できます。</param>
    public void AddKeyValue(TKey Key,TElement Value) {
        var HashCode = (long)(uint)Key!.GetHashCode();
        if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
            var KeyComparer = this.KeyComparer;
            LinkedNodeT LinkedNode = TreeNode;
            while(true) {
                var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
                if(LinkedNode_LinkedNodeItem is null) {
                    LinkedNode._LinkedNodeItem=new LinkedNodeItemT(new GroupingList<TKey,TElement>(Key,Value));
                    this._LongCount++;
                    return;
                }
                if(KeyComparer.Equals(LinkedNode_LinkedNodeItem.Item.Key,Key)) {
                    LinkedNode_LinkedNodeItem.Item.Add(Value);
                    return;
                }
                LinkedNode=LinkedNode_LinkedNodeItem;
            }
        }
        InternalAdd後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(new GroupingList<TKey,TElement>(Key,Value)));
        this._LongCount++;
    }
    private static readonly InternalList<TElement> EmptyCollection =new();


    private InternalList<TElement>?GetCollection(TKey Key){
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key!.GetHashCode());
        if(TreeNode is not null) {
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem){
                if(KeyComparer.Equals(a.Item.Key,Key)) return a.Item;
            }
        }
        return null;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(TKey Key)=>this.GetCollection(Key) is not null;
    //bool System.Linq.ILookup<TKey,TElement>.Contains(TKey key)=>this.ContainsKey(key);
    //Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> Generic.Set<System.Linq.IGrouping<TKey,TElement>>.GetEnumerator() {
    //    foreach(var a in this) yield return a;
    //}

    //Generic.IEnumerator<IGrouping<TKey,TElement>> Generic.Set<IGrouping<TKey,TElement>>.GetEnumerator() {
    //    foreach(var a in this) yield return a;
    //}
    //int System.Linq.ILookup<TKey,TElement>.Count=>checked((int)this._LongCount);

    //Generic.Set<TElement> System.Linq.ILookup<TKey,TElement>.this[TKey key]=>this.GetIndex(key);
    /// <summary>
    /// 指定したキーに関連付けられている値を取得します。
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Default"></param>
    /// <returns></returns>
    private InternalList<TElement> GetValue(TKey Key,InternalList<TElement> Default){
        var Collection=this.GetCollection(Key);
        return Collection??Default;
    }
    /// <summary>
    /// 指定したキーに関連付けられている値を取得します。
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Default"></param>
    /// <returns></returns>
    private InternalList<TElement> GetValue(object Key,InternalList<TElement> Default) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
        if(TreeNode is not null) {
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(a.Item.Key!.Equals(Key)) {
                    return a.Item;
                }
            }
        }
        return Default;
    }

    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>検索できた場合は true。それ以外の場合は false。</returns>
    /// <param name="Key">取得する値のキー。</param>
    /// <param name="Collection">キーが見つかった場合は、指定したキーに関連付けられている値が格納されます。</param>
    /// <exception cref="ArgumentNullException">
    ///   <paramref name="Key" /> が null です。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(TKey Key,ref InternalList<TElement> Collection){
        if(Key is null) return false;
        var Item=this.GetCollection(Key);
        if(Item is not null){
            Collection=Item;
            return true;
        }
        return false;
    }

    //int ILookup<TKey,TElement>.Count => throw new NotImplementedException();

    private InternalList<TElement> GetIndex(TKey key){
        InternalList<TElement> value=default!;
        if(this.TryGetValue(key,ref value)) return value;
        throw new NotImplementedException();
    }
    public InternalList<TElement> this[TKey key]=>this.GetIndex(key);

    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public InternalList<TElement> GetTKeyValue(TKey Key) => this.GetValue(Key,EmptyCollection);
    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public InternalList<TElement> GetObjectValue(object Key) => this.GetValue(Key,EmptyCollection);
}