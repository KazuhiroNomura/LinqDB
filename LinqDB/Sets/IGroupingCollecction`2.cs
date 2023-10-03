//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
namespace LinqDB.Sets;
public interface IGroupingCollection<out TKey,TElement> :IGrouping<TKey,TElement>,ICollection<TElement>
{
}
