//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Collections=System.Collections;
namespace LinqDB.Sets;
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
