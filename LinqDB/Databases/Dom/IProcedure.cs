namespace LinqDB.Databases.Dom;

public interface IProcedure:IName,ISQL,IParameters {
    ISchema Schema { get; }
}
