namespace LinqDB.Databases.Dom;
public interface ITableFunction:IName,ISQL,IColumns,IParameters {
    ISchema Schema { get; }
}
