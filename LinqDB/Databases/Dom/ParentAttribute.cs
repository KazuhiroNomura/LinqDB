namespace LinqDB.Databases.Dom;

public sealed class ParentAttribute:RelationAttribute {
    public ParentAttribute(params string[] Properties) : base(Properties){}
}