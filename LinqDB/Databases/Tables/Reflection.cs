using System;
using System.Reflection;
#pragma warning disable CS0660, CS0661
#pragma warning disable CS0659
namespace LinqDB.Databases.Tables;
[Serializable]
public abstract class Reflection:Sets.Entity<PrimaryKeys.Reflection,Container>, IEquatable<Reflection> {
    public string Name => this.ProtectedKey.Name;
    public Type Type => this.Property.PropertyType;
    public PropertyInfo Property { get; }
    protected Reflection(PropertyInfo Property) : base(new PrimaryKeys.Reflection(Property.Name)) {
        this.Property=Property;
    }
    public bool Equals(Reflection? other) {
        if(other is null) return false;
        if(!this.Property.Equals(other.Property)) return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is Reflection other&&this.Equals(other);
    public static bool operator ==(Reflection a,Reflection b) => a.Equals(b);
    public static bool operator !=(Reflection a,Reflection b) => !a.Equals(b);
    public override string ToString() => $"{this.Property.DeclaringType!.Name}.{this.Name}";
}
