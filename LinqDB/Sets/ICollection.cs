//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Collections=System.Collections;
namespace LinqDB.Sets;
using Generic= Collections.Generic;
public interface ICollection<T>:IEnumerable<T>, Generic.ICollection<T>
{
}
