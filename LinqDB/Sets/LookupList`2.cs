namespace LinqDB.Sets;
//public sealed class LookupList<TValue, TKey>:Lookup<TValue,TKey,List<TValue>>,Linq.ILookup<TKey,TValue>{
//    private static readonly Serializers.MemoryPack.Formatters.Sets.LookupList<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.LookupList<TKey,TElement>.Instance;
//    private static readonly Serializers.MessagePack.Formatters.Sets.LookupList<TKey,TElement> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.LookupList<TKey,TElement>.Instance;
//    private static readonly Serializers.Utf8Json.Formatters.Sets.LookupList<TKey,TElement> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.LookupList<TKey,TElement>.Instance;
//    public LookupList():this(EqualityComparer<TKey>.Default){}
//    public LookupList(IEqualityComparer<TKey> KeyComparer):base(KeyComparer){}

//    Generic.IEnumerable<TValue> Linq.ILookup<TKey,TValue>.this[TKey key] => throw new NotImplementedException();

//    public int Count =>checked((int)this._LongCount);


//    internal override KeyValueCollection<TValue,TKey,List<TValue>> InternalKeyValue(TKey Key,TValue Value)=>new(Key,new(){Value});

//    bool Linq.ILookup<TKey,TValue>.Contains(TKey key)=>this.ContainsKey(key);

//    IEnumerator<Linq.IGrouping<TKey,TValue>> Generic.IEnumerable<Linq.IGrouping<TKey,TValue>>.GetEnumerator() {
//        foreach(var a in this) yield return a;
//    }
//    /*
//bool Linq.ILookup<TKey,TValue>.Contains(TKey key)=>this.ContainsKey(key);
//IEnumerator<Linq.IGrouping<TKey,TValue>> Generic.IEnumerable<Linq.IGrouping<TKey,TValue>>.GetEnumerator(){
//   foreach(var a in this) yield return a;
//}
//int Linq.ILookup<TKey,TValue>.Count =>(int)this.LongCount;
//Generic.IEnumerable<TValue> Linq.ILookup<TKey,TValue>.this[TKey key]=>this.GetIndex(key);
//*/
//}