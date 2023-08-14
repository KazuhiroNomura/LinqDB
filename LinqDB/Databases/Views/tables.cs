using System;
using System.Text;
using LinqDB.Sets;
namespace LinqDB.Databases.Views;
[Serializable]
public class tables:Entity, IEquatable<tables> {
    public readonly string table_catalog;
    public readonly string table_schema;
    public readonly string table_name;
    public readonly table_type table_type;
    public tables(string table_catalog,string table_schema,string table_name,table_type table_type) {
        this.table_catalog=table_catalog;
        this.table_schema=table_schema;
        this.table_name=table_name;
        this.table_type=table_type;
    }
    public bool Equals(tables? other){
        if(other is null) return false;
        if(!this.table_catalog.Equals(other.table_catalog)) return false;
        if(!this.table_schema.Equals(other.table_schema)) return false;
        if(!this.table_name.Equals(other.table_name)) return false;
        if(!this.table_type.Equals(other.table_type)) return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is tables other&&this.Equals(other);
    public override int GetHashCode() => HashCode.Combine(this.table_catalog,this.table_schema,this.table_name,(int)this.table_type);
    public static bool operator ==(tables a,tables b) => a.Equals(b);
    public static bool operator !=(tables a,tables b) => !a.Equals(b);
    protected override void ToStringBuilder(StringBuilder sb) {
        ProtectedToStringBuilder(sb,nameof(this.table_catalog),this.table_catalog);
        ProtectedToStringBuilder(sb,nameof(this.table_schema),this.table_schema);
        ProtectedToStringBuilder(sb,nameof(this.table_name),this.table_name);
        ProtectedToStringBuilder(sb,nameof(this.table_type),this.table_type);
    }
}
