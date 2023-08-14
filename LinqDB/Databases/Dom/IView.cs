namespace LinqDB.Databases.Dom;
public interface IView:IName,ISQL,IColumns {
    ISchema Schema { get; }
}
