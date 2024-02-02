using System;

namespace LinqDB.Databases.Dom;

public abstract class RelationAttribute(params string[] Properties):Attribute {
    public readonly string[] ColumnNames=Properties;
}