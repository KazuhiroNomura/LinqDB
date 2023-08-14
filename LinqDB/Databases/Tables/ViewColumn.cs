using System;
using System.Reflection;
namespace LinqDB.Databases.Tables;
[Serializable]
public sealed class ViewColumn:Reflection {
    public View View { get; }
    public ViewColumn(PropertyInfo Property,View View) : base(Property) => this.View=View;
}
