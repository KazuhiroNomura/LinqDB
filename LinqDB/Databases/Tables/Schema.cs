using System;
using System.Reflection;

namespace LinqDB.Databases.Tables;
[Serializable]
public sealed class Schema:Reflection {
    public Schema(PropertyInfo Property) : base(Property) {
    }
}
