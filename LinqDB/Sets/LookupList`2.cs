using System;
using System.Collections.Generic;
namespace LinqDB.Sets;
using Linq = System.Linq;
using Generic = System.Collections.Generic;
public sealed class LookupList<TValue, TKey>:Lookup<TValue,TKey,List<TValue>>,Linq.ILookup<TKey,TValue>{
    public LookupList():this(Generic.EqualityComparer<TKey>.Default){}
    public LookupList(IEqualityComparer<TKey> KeyComparer):base(KeyComparer){}

    Generic.IEnumerable<TValue> Linq.ILookup<TKey,TValue>.this[TKey key] => throw new NotImplementedException();

    public int Count =>checked((int)this._LongCount);


    internal override KeyValueCollection<TValue,TKey,List<TValue>> InternalKeyValue(TKey Key,TValue Value)=>new(Key,new(){Value});

    bool Linq.ILookup<TKey,TValue>.Contains(TKey key)=>this.ContainsKey(key);

    IEnumerator<Linq.IGrouping<TKey,TValue>> Generic.IEnumerable<Linq.IGrouping<TKey,TValue>>.GetEnumerator() {
        foreach(var a in this) yield return a;
    }
    /*
bool Linq.ILookup<TKey,TValue>.Contains(TKey key)=>this.ContainsKey(key);
IEnumerator<Linq.IGrouping<TKey,TValue>> Generic.IEnumerable<Linq.IGrouping<TKey,TValue>>.GetEnumerator(){
   foreach(var a in this) yield return a;
}
int Linq.ILookup<TKey,TValue>.Count =>(int)this.LongCount;
Generic.IEnumerable<TValue> Linq.ILookup<TKey,TValue>.this[TKey key]=>this.GetIndex(key);
*/
}