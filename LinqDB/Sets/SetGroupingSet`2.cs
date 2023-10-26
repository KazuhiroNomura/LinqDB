using System;
using System.Runtime.CompilerServices;

using Generic=System.Collections.Generic;
using Linq=System.Linq;
namespace LinqDB.Sets;
/// <summary>
/// Set.GroupByの結果。要素はGroupingSet。Set.GroupJoin,Set.Joinのビルド結果で使うDictionaryコレクション
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TElement">値のType</typeparam>
public sealed class SetGroupingSet<TKey,TElement>:Set<GroupingSet<TKey,TElement>>,
    IEquatable<Set<IGrouping<TKey,TElement>>>,
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
    public override int GetHashCode()=>base.GetHashCode();
    private bool Equals(SetGroupingSet<TKey,TElement>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        Set<TElement> Collection=null!;
        foreach(var a in other){
            if(other.TryGetValue(a.Key,ref Collection)){
                if(!a.Equals(Collection)) return false;
            }
        }
        return true;
    }
    public bool Equals(Set<IGrouping<TKey,TElement>>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        if(other is SetGroupingSet<TKey,TElement> value0)return this.Equals(value0);
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
    public bool Equals(Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        if(other is Set<IGrouping<TKey,TElement>> value0)return this.Equals(value0);
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
            case Set<IGrouping<TKey,TElement>>other:return this.Equals(other);
            case Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>other:return this.Equals(other);
            default:return false;
        }
    }
    //public void Add(GroupingSet<TKey,TElement> item) {
    //    if(this.InternalAdd(item))
    //        this._LongCount++;
    //}
    //public void Clear()=>this.InternalClear();
    //public bool Contains(GroupingSet<TKey,TElement> item)=>this.InternalContains(item);
    //public void CopyTo(GroupingSet<TKey,TElement>[] array,int arrayIndex) {
    //    throw new NotImplementedException();
    //}
    //public bool Remove(GroupingSet<TKey,TElement> item) {
    //    throw new NotImplementedException();
    //}
    private static readonly Set<TElement> EmptyCollection =new();


    private Set<TElement>?GetCollection(TKey Key){
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
    private Set<TElement> GetValue(TKey Key,Set<TElement> Default){
        var Collection=this.GetCollection(Key);
        return Collection??Default;
    }
    /// <summary>
    /// 指定したキーに関連付けられている値を取得します。
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Default"></param>
    /// <returns></returns>
    private Set<TElement> GetValue(object Key,Set<TElement> Default) {
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
    public bool TryGetValue(TKey Key,ref Set<TElement> Collection){
        if(Key is null) return false;
        var Item=this.GetCollection(Key);
        if(Item is not null){
            Collection=Item;
            return true;
        }
        return false;
    }

    //int ILookup<TKey,TElement>.Count => throw new NotImplementedException();

    private Set<TElement> GetIndex(TKey key){
        Set<TElement> value=default!;
        if(this.TryGetValue(key,ref value)) return value;
        throw new NotImplementedException();
    }
    public Set<TElement> this[TKey key]=>this.GetIndex(key);

    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public Set<TElement> GetTKeyValue(TKey Key) => this.GetValue(Key,EmptyCollection);
    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public Set<TElement> GetObjectValue(object Key) => this.GetValue(Key,EmptyCollection);
}