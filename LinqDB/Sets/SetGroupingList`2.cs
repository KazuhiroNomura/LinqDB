using System;
using LinqDB.Enumerables;
using Generic = System.Collections.Generic;

namespace LinqDB.Sets;

/// <summary>
/// Enumerable.GroupByの結果。要素はGroupingAscList。
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TElement">値のType</typeparam>
public sealed class SetGroupingList<TKey,TElement>:ImmutableSet<GroupingList<TKey,TElement>>,Generic.ICollection<GroupingList<TKey,TElement>>
    //IEquatable<IEnumerable<System.Linq.IGrouping<TKey,TElement>>>,
    //IEquatable<Generic.IEnumerable<System.Linq.IGrouping<TKey,TElement>>>

{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private new static readonly Serializers.MessagePack.Formatters.Enumerables.SetGroupingList<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Enumerables.SetGroupingList<TKey,TElement>.Instance;
    private new static readonly Serializers.Utf8Json.Formatters.Enumerables.SetGroupingList<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Enumerables.SetGroupingList<TKey,TElement>.Instance;
    static SetGroupingList()=> MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Enumerables.SetGroupingList<TKey,TElement>.Instance);
    //IEquatable<IEnumerable<IGrouping<TKey,TElement>>>,
    //IEquatable<Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>>{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    //private new static readonly Serializers.MemoryPack.Formatters.Sets.SetGroupingAscList<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.SetGroupingList<TKey,TElement>.Instance;
    //private new static readonly Serializers.MessagePack.Formatters.Sets.SetGroupingAscList<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.SetGroupingList<TKey,TElement>.Instance;
    //private new static readonly Serializers.Utf8Json.Formatters.Sets.SetGroupingAscList<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.SetGroupingList<TKey,TElement>.Instance;
    //SetGrouping<TKey,TElement,GroupingAscList<TKey,TElement>>{
//#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
//    private static readonly Serializers.MessagePack.Formatters.Sets.SetGroupingAscList<TKey,TElement> MessagePack=new();
//#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
    /// <summary>
    /// キー比較用EqualityComparer
    /// </summary>
    private readonly Generic.IEqualityComparer<TKey> KeyComparer;

    public int Count=>(int)this._LongCount;

    public bool IsReadOnly => throw new NotImplementedException();

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
    internal void AddKeyValue(TKey Key,TElement Value) {
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
    public void Add(GroupingList<TKey,TElement> item) {
        if(this.InternalAdd(item))
            this._LongCount++;
    }
    public void Clear()=>this.InternalClear();
    public bool Contains(GroupingList<TKey,TElement> item)=>this.InternalContains(item);
    public void CopyTo(GroupingList<TKey,TElement>[] array,int arrayIndex) {
        throw new NotImplementedException();
    }
    public bool Remove(GroupingList<TKey,TElement> item) {
        throw new NotImplementedException();
    }
    //internal override GroupingAscList<TKey,TElement> InternalKeyValue(TKey Key,TElement Value)=>new GroupingAscList<TKey,TElement>(Key,Value);
}