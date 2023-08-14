using System;

namespace LinqDB.Databases.Dom;

[Serializable]
[AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct|AttributeTargets.Enum,Inherited = false)]
public sealed class MetaAttribute:Attribute {
    public readonly double Left;
    public readonly double Top;
    public MetaAttribute(double Left,double Top) {
        this.Left=Left;
        this.Top=Top;
    }
}