using System;

namespace LinqDB.Databases.Dom;

public abstract class RelationAttribute:Attribute {
    public readonly string[] ColumnNames;
    protected RelationAttribute(params string[] Properties) => this.ColumnNames=Properties;
}