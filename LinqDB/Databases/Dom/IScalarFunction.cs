using System;
namespace LinqDB.Databases.Dom;
public interface IScalarFunction:IName,IParameters,ISQL {
    Type? Type { get;}
    ISchema Schema { get; }
}
