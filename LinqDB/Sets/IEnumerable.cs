//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Collections=System.Collections;
namespace LinqDB.Sets;
using Linq=System.Linq;
using Generic=Collections.Generic;
public interface IEnumerable:Collections.IEnumerable
{
    long LongCount { get; }
    //Collections.IEnumerator GetEnumerator();
}

//public interface ICollection<T> :System.Collections.Generic.ICollection<T>
//{
//}
public interface IEnumerable<out T> :IEnumerable,Generic.IEnumerable<T>
{
    //new Generic.IEnumerator<T> GetEnumerator();
}
public interface ICollection<T> : IEnumerable<T>,Generic.ICollection<T>
{

	//bool IsReadOnly { get; }
	//void Add(T item);
	//void Clear();


	//void CopyTo(T[] array, int arrayIndex);
	//bool Remove(T item);
}
public interface IGrouping<out TKey, out TElement> :Linq.IGrouping<TKey,TElement>,IEnumerable<TElement>
{
}
public interface IGroupingCollection<out TKey,TElement> :IGrouping<TKey,TElement>,ICollection<TElement>
{
}
//KeyValueCollection<TValue,TKey, TCollection>:IGrouping<TKey,TValue>,ICollection<TValue> where TCollection:ICollection<TValue> {
// ReSharper disable once PossibleInterfaceMemberAmbiguity
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

