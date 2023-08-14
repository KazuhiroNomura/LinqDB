namespace LinqDB.Databases.Dom;
public interface IColumn:IName,IDataType {
    bool IsPrimaryKey { get; }
}
