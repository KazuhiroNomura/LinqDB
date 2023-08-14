namespace LinqDB.Databases.Dom;

public interface IParameter:IName,IDataType {
    bool has_default_value{ get; }
    object default_value { get; }
}
