using System;
using System.Text;
using LinqDB.Sets;
namespace LinqDB.Databases.Tables;
[Serializable]
public class check_constraints:Entity, IEquatable<check_constraints> {
    public readonly string constraint_catalog;
    public readonly string constraint_schema;
    public readonly string constraint_name;
    public readonly string check_clause;
    public check_constraints(string constraint_catalog,string constraint_schema,string constraint_name,string check_clause) {
        this.constraint_catalog=constraint_catalog;
        this.constraint_schema=constraint_schema;
        this.constraint_name=constraint_name;
        this.check_clause=check_clause;
    }
    public bool Equals(check_constraints? other){
        if(other is null) return false;
        if(!this.constraint_catalog.Equals(other.constraint_catalog)) return false;
        if(!this.constraint_schema.Equals(other.constraint_schema)) return false;
        if(!this.constraint_name.Equals(other.constraint_name)) return false;
        if(!this.check_clause.Equals(other.check_clause)) return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is check_constraints other && this.Equals(other);
    public override int GetHashCode()=>HashCode.Combine(
        this.constraint_catalog,
        this.constraint_schema,
        this.constraint_name,
        this.check_clause);
    public static bool operator ==(check_constraints a,check_constraints b) => a.Equals(b);
    public static bool operator !=(check_constraints a,check_constraints b) => !a.Equals(b);
    protected override void ToStringBuilder(StringBuilder sb) {
        ProtectedToStringBuilder(sb,nameof(this.constraint_catalog),this.constraint_catalog);
        ProtectedToStringBuilder(sb,nameof(this.constraint_schema),this.constraint_schema);
        ProtectedToStringBuilder(sb,nameof(this.constraint_name),this.constraint_name);
        ProtectedToStringBuilder(sb,nameof(this.check_clause),this.check_clause);
    }
}
