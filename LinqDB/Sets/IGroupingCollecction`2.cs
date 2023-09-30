//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Collections=System.Collections;
namespace LinqDB.Sets;
using Linq=System.Linq;
public interface IGroupingCollection<out TKey,TElement> :IGrouping<TKey,TElement>,ICollection<TElement>
{
}
