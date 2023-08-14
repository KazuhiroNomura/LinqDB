using System;
using System.Reflection;
namespace LinqDB.Databases.Tables;
[Serializable]
public sealed class View:Reflection{
    public Schema Schema { get; }
    public View(PropertyInfo Property,Schema Schema) : base(Property) => this.Schema=Schema;
}
