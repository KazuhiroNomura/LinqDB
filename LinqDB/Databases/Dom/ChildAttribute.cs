namespace LinqDB.Databases.Dom;

public sealed class ChildAttribute:RelationAttribute {
    public ChildAttribute(params string[] Properties) : base(Properties){}
}