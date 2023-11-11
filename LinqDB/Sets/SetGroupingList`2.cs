using System;
using System.Runtime.CompilerServices;
using LinqDB.Enumerables;
using Generic = System.Collections.Generic;
using Linq=System.Linq;
namespace LinqDB.Sets;
/// <summary>
/// Enumerable.GroupByの結果。要素はGroupingAscList。
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TElement">値のType</typeparam>
public sealed class SetGroupingList<TKey,TElement>:Set<GroupingList<TKey,TElement>>,Linq.ILookup<TKey,TElement>{
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
    /// <summary>
    /// 指定したキーと値をディクショナリに追加する。
    /// KeyがなければTGrouping(Key,Value)
    /// KeyがあればTGroupingを取り出しAdd(Value)
    /// </summary>
    /// <param name="Key">追加する要素のキー。</param>
    /// <param name="Value">追加する要素の値。参照型の場合、null の値を使用できます。</param>
    public void AddKeyValue(TKey Key,TElement Value) {
        var HashCode = (long)(uint)Key!.GetHashCode();
        if(this.InternalIsAdded前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
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
        InternalIsAdded後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(new GroupingList<TKey,TElement>(Key,Value)));
        this._LongCount++;
    }
    public bool Contains(TKey Key)=>this.GetCollection(Key) is not null;
    public Generic.IEnumerable<TElement> this[TKey Key]=>this.GetCollection(Key)??EmptyCollection;
    private static readonly Generic.IEnumerable<TElement> EmptyCollection =new InternalList<TElement>();
    private Generic.IEnumerable<TElement>? GetCollection(TKey Key) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key!.GetHashCode());
        if(TreeNode is not null) {
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(KeyComparer.Equals(a.Item.Key,Key)) return a.Item;
            }
        }
        return null;
    }
    Generic.IEnumerator<Linq.IGrouping<TKey,TElement>> Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>.GetEnumerator()=>this.変数Enumerator;
}