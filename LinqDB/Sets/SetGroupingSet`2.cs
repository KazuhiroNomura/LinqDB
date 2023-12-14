using System;
using Generic=System.Collections.Generic;
namespace LinqDB.Sets;
/// <summary>
/// Set.GroupByの結果。要素はGroupingSet。Set.GroupJoin,Set.Joinのビルド結果で使うDictionaryコレクション
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TElement">値のType</typeparam>
public sealed class SetGroupingSet<TKey,TElement>:Set<GroupingSet<TKey,TElement>>,ILookup<TKey,TElement>{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private new static readonly Serializers.MemoryPack.Formatters.Sets.SetGroupingSet<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.SetGroupingSet<TKey,TElement>.Instance;
    private new static readonly Serializers.MessagePack.Formatters.Sets.SetGroupingSet<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.SetGroupingSet<TKey,TElement>.Instance;
    private new static readonly Serializers.Utf8Json.Formatters.Sets.SetGroupingSet<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.SetGroupingSet<TKey,TElement>.Instance;
    /// <summary>
    /// キー比較用EqualityComparer
    /// </summary>
    private readonly Generic.IEqualityComparer<TKey> KeyComparer;
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public SetGroupingSet()=>this.KeyComparer=Generic.EqualityComparer<TKey>.Default;
    /// <summary>
    /// 比較方法を指定したコンストラクタ
    /// </summary>
    /// <param name="KeyComparer">比較方法</param>
    public SetGroupingSet(Generic.IEqualityComparer<TKey> KeyComparer)=>this.KeyComparer=KeyComparer;
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
                    LinkedNode._LinkedNodeItem=new LinkedNodeItemT(new GroupingSet<TKey,TElement>(Key,Value));
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
        InternalIsAdded後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(new GroupingSet<TKey,TElement>(Key,Value)));
        this._LongCount++;
    }
    public bool Contains(TKey Key)=>this.GetCollection(Key) is not null;
    public IEnumerable<TElement> this[TKey Key]=>this.GetCollection(Key)??EmptyCollection;
    private static readonly IEnumerable<TElement> EmptyCollection =new Set<TElement>();
    private IEnumerable<TElement>?GetCollection(TKey Key){
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key!.GetHashCode());
        if(TreeNode is not null) {
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem){
                if(KeyComparer.Equals(a.Item.Key,Key)) return a.Item;
            }
        }
        return null;
    }

    bool ILookup<TKey,TElement>.Contains(TKey key) {
        throw new NotImplementedException();
    }

    Generic.IEnumerator<IGrouping<TKey,TElement>> Generic.IEnumerable<IGrouping<TKey,TElement>>.GetEnumerator()=>this.変数Enumerator;
    //public Generic.IEnumerator<IGrouping<TKey,TElement>> GetEnumerator()=>this.変数Enumerator;
    //public bool TryGetValue(TKey Key,ref IEnumerable<TElement> Collection){
    //    if(Key is null) return false;
    //    var Item=this.GetCollection(Key);
    //    if(Item is not null){
    //        Collection=Item;
    //        return true;
    //    }
    //    return false;
    //}
    //public IEnumerable<TElement> GetTKeyValue(TKey Key) => this.GetValue(Key,EmptyCollection);
}