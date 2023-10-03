//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
namespace LinqDB.Sets;
using Linq=System.Linq;
public interface IGrouping<out TKey, out TElement> :Linq.IGrouping<TKey,TElement>,IEnumerable<TElement>
{
}
