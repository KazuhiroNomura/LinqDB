using System;
using System.Text;
using LinqDB.Sets;
namespace LinqDB.Databases.Tables;
[Serializable]
public class referential_constraints:Entity, IEquatable<referential_constraints> {
    public readonly string constraint_catalog;
    public readonly string constraint_schema;
    public readonly string constraint_name;
    public readonly string unique_constraint_catalog;
    public readonly string unique_constraint_schema;
    public readonly string unique_constraint_name;
    public readonly rule update_rule;
    public readonly rule delete_rule;
    public referential_constraints(string constraint_catalog,string constraint_schema,string constraint_name,string unique_constraint_catalog,string unique_constraint_schema,string unique_constraint_name,rule update_rule,rule delete_rule) {
        this.constraint_catalog=constraint_catalog;
        this.constraint_schema=constraint_schema;
        this.constraint_name=constraint_name;
        this.unique_constraint_catalog=unique_constraint_catalog;
        this.unique_constraint_schema=unique_constraint_schema;
        this.unique_constraint_name=unique_constraint_name;
        this.update_rule=update_rule;
        this.delete_rule=delete_rule;
    }
    public bool Equals(referential_constraints? other) {
        if(other is null) return false;
        if(!this.constraint_catalog.Equals(other.constraint_catalog)) return false;
        if(!this.constraint_schema.Equals(other.constraint_schema)) return false;
        if(!this.constraint_name.Equals(other.constraint_name)) return false;
        if(!this.unique_constraint_catalog.Equals(other.unique_constraint_catalog)) return false;
        if(!this.unique_constraint_schema.Equals(other.unique_constraint_schema)) return false;
        if(!this.unique_constraint_name.Equals(other.unique_constraint_name)) return false;
        if(this.update_rule!=other.update_rule) return false;
        if(this.delete_rule!=other.delete_rule) return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is referential_constraints other&&this.Equals(other);

    public override int GetHashCode() => HashCode.Combine(this.constraint_catalog,this.constraint_schema,this.constraint_name,this.unique_constraint_catalog,this.unique_constraint_schema,this.unique_constraint_name,(int)this.update_rule,(int)this.delete_rule);

    public static bool operator ==(referential_constraints a,referential_constraints b) => a.Equals(b);
    public static bool operator !=(referential_constraints a,referential_constraints b) => !a.Equals(b);
    protected override void ToStringBuilder(StringBuilder sb) {
        ProtectedToStringBuilder(sb,nameof(this.constraint_catalog),this.constraint_catalog);
        ProtectedToStringBuilder(sb,nameof(this.constraint_schema),this.constraint_schema);
        ProtectedToStringBuilder(sb,nameof(this.constraint_name),this.constraint_name);
        ProtectedToStringBuilder(sb,nameof(this.unique_constraint_catalog),this.unique_constraint_catalog);
        ProtectedToStringBuilder(sb,nameof(this.unique_constraint_schema),this.unique_constraint_schema);
        ProtectedToStringBuilder(sb,nameof(this.unique_constraint_name),this.unique_constraint_name);
        ProtectedToStringBuilder(sb,nameof(this.update_rule),this.update_rule);
        ProtectedToStringBuilder(sb,nameof(this.delete_rule),this.delete_rule);
    }
}
