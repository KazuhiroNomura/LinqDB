//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Collections=System.Collections;
namespace LinqDB.Sets;
using Linq=System.Linq;
using Generic=Collections.Generic;
public interface ILookup<TKey,TElement>:Linq.ILookup<TKey,TElement>,IEnumerable<IGrouping<TKey,TElement>>{
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

