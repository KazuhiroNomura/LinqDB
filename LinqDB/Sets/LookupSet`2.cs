using System;
using System.Collections.Generic;

using Collections=System.Collections;
namespace LinqDB.Sets;
using Linq=System.Linq;
using Generic= Collections.Generic;
public sealed class LookupSet<TValue, TKey>:Lookup<TValue,TKey,Set<TValue>>,ILookup<TKey,TValue>{
    public LookupSet()  { }
    public LookupSet(IEqualityComparer<TKey> KeyComparer):base(KeyComparer){}

    Generic.IEnumerable<TValue> Linq.ILookup<TKey,TValue>.this[TKey key] => throw new NotImplementedException();

    //IEnumerable<TValue> ILookup<TKey,TValue>.this[TKey key] => throw new NotImplementedException();

    public int Count => throw new NotImplementedException();

    public bool Contains(TKey key) {
        throw new NotImplementedException();
    }

    internal override KeyValueCollection<TValue,TKey,Set<TValue>> InternalKeyValue(TKey Key,TValue Value)=>new(Key,new(){ Value });

    //bool ILookup<TKey,TValue>.Contains(TKey key)=>this.ContainsKey(key);
    IEnumerator<IGrouping<TKey,TValue>> Generic.IEnumerable<IGrouping<TKey,TValue>>.GetEnumerator() {
        foreach(var a in this) yield return a;
    }

    IEnumerator<Linq.IGrouping<TKey,TValue>> Generic.IEnumerable<Linq.IGrouping<TKey,TValue>>.GetEnumerator() {
        throw new NotImplementedException();
    }
    bool Linq.ILookup<TKey,TValue>.Contains(TKey key)=>this.ContainsKey(key);
    //Generic.IEnumerator<System.Linq.IGrouping<TKey,TValue>> Generic.IEnumerable<System.Linq.IGrouping<TKey,TValue>>.GetEnumerator() {
    //    foreach(var a in this) yield return a;
    //}

    //Generic.IEnumerator<IGrouping<TKey,TValue>> Generic.IEnumerable<IGrouping<TKey,TValue>>.GetEnumerator() {
    //    foreach(var a in this) yield return a;
    //}
    //int System.Linq.ILookup<TKey,TValue>.Count=>checked((int)this._LongCount);

    //Generic.IEnumerable<TValue> System.Linq.ILookup<TKey,TValue>.this[TKey key]=>this.GetIndex(key);
    /*
public int Count{get;}

IEnumerable<TValue> ILookup<TKey,TValue>.this[TKey key] => throw new NotImplementedException();
public IEnumerable<TValue> this[TKey key]=>throw new NotImplementedException();
public bool Contains(TKey key){
throw new NotImplementedException();
}
bool ILookup<TKey,TValue>.Contains(TKey key) {
throw new NotImplementedException();
}
Generic.IEnumerator<IGrouping<TKey,TValue>> Generic.IEnumerable<IGrouping<TKey,TValue>>.GetEnumerator() {
foreach(var a in this) yield return a;
}
*/
}