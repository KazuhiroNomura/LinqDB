//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
namespace LinqDB.Sets;
//public interface ILookup2<TKey,TElement>:System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey,TElement>>,System.Collections.IEnumerable{
//    int Count { get; }
//    System.Collections.Generic.IEnumerable<TElement> this[TKey key] { get; }
//    bool Contains(TKey key);
//}

public interface ILookup<TKey,out TElement>:IEnumerable<IGrouping<TKey,TElement>>,IEnumerable{
    //int Count { get; }
    IEnumerable<TElement> this[TKey key] { get; }
    bool Contains(TKey key);
    //bool TryGetValue(TKey Key,ref IEnumerable<TElement> Collection);
    //int Count{get;}
    //IEnumerable<TElement> this[TKey key]{get;}
    //bool Contains(TKey key);
}
//class Lookup2<TValue,TKey,TCollection>:ImmutableSet<KeyValueCollection<TValue,TKey,TCollection>>,ILookup<TKey,TValue> where TCollection:ICollection<TValue>{
//    public Generic.IEnumerator<IGrouping<TKey,TValue>> GetEnumerator(){
//        throw new NotImplementedException();
//    }
//    Collections.IEnumerator IEnumerable.GetEnumerator(){
//        return GetEnumerator();
//    }
//    public int Count{get;}
//    public IEnumerable<TValue> this[TKey key]=>throw new NotImplementedException();
//    public bool Contains(TKey key){
//        throw new NotImplementedException();
//    }
//}
//Lookup<TValue, TKey, TCollection>:ImmutableSet<KeyValueCollection<TValue,TKey,TCollection>>,ILookup<TKey,TValue>where TCollection:ICollection<TValue> 

