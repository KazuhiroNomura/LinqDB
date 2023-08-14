using System;
using System.Reflection;

namespace LinqDB.Databases.Tables;
[Serializable]
public sealed class Table:Reflection{
    public Schema Schema { get; }
    public Table(PropertyInfo Property,Schema Schema) : base(Property) => this.Schema=Schema;
}
