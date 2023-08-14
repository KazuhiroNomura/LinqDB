using System.Collections.Generic;
namespace LinqDB.Databases.Dom;
public interface IParameters {
    IEnumerable<IParameter> Parameters { get; }
}
