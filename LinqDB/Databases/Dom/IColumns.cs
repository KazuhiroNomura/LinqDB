using System.Reflection.Emit;
using System.Collections.Generic;
using LinqDB.Helpers;

namespace LinqDB.Databases.Dom;
public interface IColumns{
    IEnumerable<IColumn> Columns { get; }
}
