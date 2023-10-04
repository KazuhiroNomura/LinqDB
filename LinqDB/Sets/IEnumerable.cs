//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
namespace LinqDB.Sets;
public interface IEnumerable:System.Collections.IEnumerable
{
    long LongCount { get; }
    //Collections.IEnumerator GetEnumerator();
}

//public interface ICollection<T> :System.Collections.Generic.ICollection<T>
//{
//}
public interface IEnumerable<out T> :IEnumerable,System.Collections.Generic.IEnumerable<T>
{
    //new Generic.IEnumerator<T> GetEnumerator();
}
