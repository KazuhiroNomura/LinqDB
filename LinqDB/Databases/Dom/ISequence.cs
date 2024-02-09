namespace LinqDB.Databases.Dom;

public interface ISequence:IName {
    ISchema Schema { get; }
    object start_value{get;}
    object increment{get;}
    object current_value{get;}
}
