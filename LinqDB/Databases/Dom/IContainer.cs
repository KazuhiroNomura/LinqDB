using System.Collections.Generic;
namespace LinqDB.Databases.Dom;
public interface IContainer:IName {
    IEnumerable<ISchema> Schemas { get; }
    IEnumerable<IRelation> Relations { get; } 
}
