using System.Collections.Generic;
namespace System.Linq;
public interface IGroupingCollection<out TKey,TElement>:IGrouping<TKey,TElement>,ICollection<TElement>{
}
