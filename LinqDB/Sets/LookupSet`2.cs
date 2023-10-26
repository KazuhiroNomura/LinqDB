using Collections=System.Collections;
namespace LinqDB.Sets;
//public sealed class LookupSet<TElement, TKey>:ImmutableSet<KeyValueCollection<TElement,TKey,Set<TElement>>>,ILookup<TKey,TElement>{
//    private static readonly Serializers.MemoryPack.Formatters.Sets.LookupSet<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.LookupSet<TKey,TElement>.Instance;
//    private static readonly Serializers.MessagePack.Formatters.Sets.LookupSet<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.LookupSet<TKey,TElement>.Instance;
//    private static readonly Serializers.Utf8Json.Formatters.Sets.LookupSet<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.LookupSet<TKey,TElement>.Instance;
//    public  LookupSet(Generic.IEqualityComparer<TKey> KeyComparer) => this.KeyComparer=KeyComparer;
//    /// <summary>
//    /// キー比較用EqualityComparer
//    /// </summary>
//    [NonSerialized]
//    private readonly Generic.IEqualityComparer<TKey> KeyComparer;
//    public LookupSet() : this(Generic.EqualityComparer<TKey>.Default){}
//    /// <summary>
//    /// 指定したキーと値をディクショナリに追加する。
//    /// KeyがなければTGrouping(Key,Value)
//    /// KeyがあればTGroupingを取り出しAdd(Value)
//    /// </summary>
//    /// <param name="Key">追加する要素のキー。</param>
//    /// <param name="Value">追加する要素の値。</param>
//    public void AddKeyValue(TKey Key,TElement Value) {
//        var HashCode = (long)(uint)Key!.GetHashCode();
//        if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
//            var KeyComparer = this.KeyComparer;
//            LinkedNodeT LinkedNode = TreeNode;
//            while(true) {
//                var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
//                if(LinkedNode_LinkedNodeItem is null) {
//                    LinkedNode._LinkedNodeItem=new LinkedNodeItemT(new(Key,new(){ Value }));
//                    this._LongCount++;
//                    return;
//                }
//                if(KeyComparer.Equals(LinkedNode_LinkedNodeItem.Item.Key,Key)) {
//                    LinkedNode_LinkedNodeItem.Item.Add(Value);
//                    return;
//                }
//                LinkedNode=LinkedNode_LinkedNodeItem;
//            }
//        }
//        InternalAdd後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(new(Key,new(){ Value })));
//        this._LongCount++;
//    }
//    private Set<TElement>?GetCollection(TKey Key){
//        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key!.GetHashCode());
//        if(TreeNode is not null) {
//            var KeyComparer = this.KeyComparer;
//            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem){
//                if(KeyComparer.Equals(a.Item.Key,Key)) return a.Item.Collection;
//            }
//        }
//        return null;
//    }
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool ContainsKey(TKey Key)=>this.GetCollection(Key) is not null;
//    //bool System.Linq.ILookup<TKey,TElement>.Contains(TKey key)=>this.ContainsKey(key);
//    //Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> Generic.IEnumerable<System.Linq.IGrouping<TKey,TElement>>.GetEnumerator() {
//    //    foreach(var a in this) yield return a;
//    //}

//    //Generic.IEnumerator<IGrouping<TKey,TElement>> Generic.IEnumerable<IGrouping<TKey,TElement>>.GetEnumerator() {
//    //    foreach(var a in this) yield return a;
//    //}
//    //int System.Linq.ILookup<TKey,TElement>.Count=>checked((int)this._LongCount);

//    //Generic.IEnumerable<TElement> System.Linq.ILookup<TKey,TElement>.this[TKey key]=>this.GetIndex(key);
//    /// <summary>
//    /// 指定したキーに関連付けられている値を取得します。
//    /// </summary>
//    /// <param name="Key"></param>
//    /// <param name="Default"></param>
//    /// <returns></returns>
//    private Set<TElement> GetValue(TKey Key,Set<TElement> Default){
//        var Collection=this.GetCollection(Key);
//        return Collection??Default;
//    }
//    /// <summary>
//    /// 指定したキーに関連付けられている値を取得します。
//    /// </summary>
//    /// <param name="Key"></param>
//    /// <param name="Default"></param>
//    /// <returns></returns>
//    private Set<TElement> GetValue(object Key,Set<TElement> Default) {
//        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
//        if(TreeNode is not null) {
//            var KeyComparer = this.KeyComparer;
//            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
//                if(a.Item.Key!.Equals(Key)) {
//                    return a.Item.Collection;
//                }
//            }
//        }
//        return Default;
//    }

//    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
//    /// <returns>検索できた場合は true。それ以外の場合は false。</returns>
//    /// <param name="Key">取得する値のキー。</param>
//    /// <param name="Collection">キーが見つかった場合は、指定したキーに関連付けられている値が格納されます。</param>
//    /// <exception cref="ArgumentNullException">
//    ///   <paramref name="Key" /> が null です。</exception>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool TryGetValue(TKey Key,ref Set<TElement> Collection){
//        if(Key is null) return false;
//        var Item=this.GetCollection(Key);
//        if(Item is not null){
//            Collection=Item;
//            return true;
//        }
//        return false;
//    }

//    //int ILookup<TKey,TElement>.Count => throw new NotImplementedException();

//    private Set<TElement> GetIndex(TKey key){
//        Set<TElement> value=default!;
//        if(this.TryGetValue(key,ref value)) return value;
//        throw new NotImplementedException();
//    }
//    public Set<TElement> this[TKey key]=>this.GetIndex(key);
//    private static readonly Set<TElement> EmptyCollection =new();



//    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
//    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
//    /// <param name="Key"></param>
//    public Set<TElement> GetTKeyValue(TKey Key) => this.GetValue(Key,EmptyCollection);
//    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
//    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
//    /// <param name="Key"></param>
//    public Set<TElement> GetObjectValue(object Key) => this.GetValue(Key,EmptyCollection);

//    Generic.IEnumerable<TElement> Linq.ILookup<TKey,TElement>.this[TKey key] => throw new NotImplementedException();

//    //IEnumerable<TElement> ILookup<TKey,TElement>.this[TKey key] => throw new NotImplementedException();

//    public int Count => throw new NotImplementedException();

//    //public bool Contains(TKey key) {
//    //    throw new NotImplementedException();
//    //}

//    //bool ILookup<TKey,TElement>.Contains(TKey key)=>this.ContainsKey(key);
//    //IEnumerator<IGrouping<TKey,TElement>> Generic.IEnumerable<IGrouping<TKey,TElement>>.GetEnumerator() {
//    //    foreach(var a in this) yield return a;
//    //}
//    IEnumerator<IGrouping<TKey,TElement>> Generic.IEnumerable<IGrouping<TKey,TElement>>.GetEnumerator()=>this.GetEnumerator();

//    IEnumerator<Linq.IGrouping<TKey,TElement>> Generic.IEnumerable<Linq.IGrouping<TKey,TElement>>.GetEnumerator()=>this.GetEnumerator();
//    bool Linq.ILookup<TKey,TElement>.Contains(TKey key)=>this.ContainsKey(key);
//    //Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> Generic.IEnumerable<System.Linq.IGrouping<TKey,TElement>>.GetEnumerator() {
//    //    foreach(var a in this) yield return a;
//    //}

//    //Generic.IEnumerator<IGrouping<TKey,TElement>> Generic.IEnumerable<IGrouping<TKey,TElement>>.GetEnumerator() {
//    //    foreach(var a in this) yield return a;
//    //}
//    //int System.Linq.ILookup<TKey,TElement>.Count=>checked((int)this._LongCount);

//    //Generic.IEnumerable<TElement> System.Linq.ILookup<TKey,TElement>.this[TKey key]=>this.GetIndex(key);
//    /*
//public int Count{get;}

//IEnumerable<TElement> ILookup<TKey,TElement>.this[TKey key] => throw new NotImplementedException();
//public IEnumerable<TElement> this[TKey key]=>throw new NotImplementedException();
//public bool Contains(TKey key){
//throw new NotImplementedException();
//}
//bool ILookup<TKey,TElement>.Contains(TKey key) {
//throw new NotImplementedException();
//}
//Generic.IEnumerator<IGrouping<TKey,TElement>> Generic.IEnumerable<IGrouping<TKey,TElement>>.GetEnumerator() {
//foreach(var a in this) yield return a;
//}
//*/
//}