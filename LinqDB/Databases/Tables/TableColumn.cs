using System;
using System.Reflection;

namespace LinqDB.Databases.Tables;
[Serializable]
public sealed class TableColumn:Reflection {
    public Table Table { get; }
    public TableColumn(PropertyInfo Property,Table Table) : base(Property) => this.Table=Table;
}
