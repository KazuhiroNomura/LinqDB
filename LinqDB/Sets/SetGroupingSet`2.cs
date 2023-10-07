using System;
using Generic=System.Collections.Generic;
using Linq=System.Linq;
namespace LinqDB.Sets;
/// <summary>
/// Set.GroupByの結果。要素はGroupingSet。Set.GroupJoin,Set.Joinのビルド結果で使うDictionaryコレクション
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TElement">値のType</typeparam>
public sealed class SetGroupingSet<TKey,TElement>:ImmutableSet<GroupingSet<TKey,TElement>>,Generic.ICollection<GroupingSet<TKey,TElement>>,
    IEquatable<IEnumerable<IGrouping<TKey,TElement>>>,
    IEquatable<Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>>{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private new static readonly Serializers.MemoryPack.Formatters.Sets.SetGroupingSet<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.SetGroupingSet<TKey,TElement>.Instance;
    private new static readonly Serializers.MessagePack.Formatters.Sets.SetGroupingSet<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.SetGroupingSet<TKey,TElement>.Instance;
    private new static readonly Serializers.Utf8Json.Formatters.Sets.SetGroupingSet<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.SetGroupingSet<TKey,TElement>.Instance;
    //static SetGroupingSet()=> MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Sets.SetGroupingSet<TKey,TElement>.Instance);
    /// <summary>
    /// キー比較用EqualityComparer
    /// </summary>
    private readonly Generic.IEqualityComparer<TKey> KeyComparer;

    public int Count => (int)this.LongCount;

    public bool IsReadOnly => throw new NotImplementedException();

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
    internal void AddKeyValue(TKey Key,TElement Value) {
        var HashCode = (long)(uint)Key!.GetHashCode();
        if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
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
        InternalAdd後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(new GroupingSet<TKey,TElement>(Key,Value)));
        this._LongCount++;
    }
    //internal abstract IGroupingCollection<TKey,TValue> InternalKeyValue(TKey Key,TValue Value);
    //internal void Add(TGrouping item){
    //    if(this.InternalAdd(item))this._LongCount++;
    //}
    //public int Count=>(int)base.LongCount;
    //internal override IGroupingCollection<TKey,TElement> InternalKeyValue(TKey Key,TElement Value)=>new GroupingSet<TKey,TElement>(Key,Value);
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
    public void Add(GroupingSet<TKey,TElement> item) {
        if(this.InternalAdd(item))
            this._LongCount++;
    }
    public void Clear()=>this.InternalClear();
    public bool Contains(GroupingSet<TKey,TElement> item)=>this.InternalContains(item);
    public void CopyTo(GroupingSet<TKey,TElement>[] array,int arrayIndex) {
        throw new NotImplementedException();
    }
    public bool Remove(GroupingSet<TKey,TElement> item) {
        throw new NotImplementedException();
    }
}