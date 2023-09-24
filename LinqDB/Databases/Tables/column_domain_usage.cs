using System;
using System.Text;
using LinqDB.Sets;
namespace LinqDB.Databases.Tables;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
public partial class column_domain_usage:Entity, IEquatable<column_domain_usage> {
    public readonly string domain_catalog;
    public readonly string domain_schema;
    public readonly string domain_name;
    public readonly string table_catalog;
    public readonly string table_schema;
    public readonly string table_name;
    public readonly string column_name;
    public column_domain_usage(string domain_catalog,string domain_schema,string domain_name,string table_catalog,string table_schema,string table_name,string column_name) {
        this.domain_catalog=domain_catalog;
        this.domain_schema=domain_schema;
        this.domain_name=domain_name;
        this.table_catalog=table_catalog;
        this.table_schema=table_schema;
        this.table_name=table_name;
        this.column_name=column_name;
    }
    public bool Equals(column_domain_usage? other) {
        if(other is null) return false;
        if(!this.domain_catalog.Equals(other.domain_catalog)) return false;
        if(!this.domain_schema.Equals(other.domain_schema)) return false;
        if(!this.domain_name.Equals(other.domain_name)) return false;
        if(!this.table_catalog.Equals(other.table_catalog)) return false;
        if(!this.table_schema.Equals(other.table_schema)) return false;
        if(!this.table_name.Equals(other.table_name)) return false;
        if(!this.column_name.Equals(other.column_name)) return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is column_domain_usage other&&this.Equals(other);

    public override int GetHashCode() => HashCode.Combine(this.domain_catalog,this.domain_schema,this.domain_name,this.table_catalog,this.table_schema,this.table_name,this.column_name);

    //public static bool operator ==(column_domain_usage a,column_domain_usage b) => a.Equals(b);
    //public static bool operator !=(column_domain_usage a,column_domain_usage b) => !a.Equals(b);
    protected override void ToStringBuilder(StringBuilder sb) {
        ProtectedToStringBuilder(sb,nameof(this.domain_catalog),this.domain_catalog);
        ProtectedToStringBuilder(sb,nameof(this.domain_schema),this.domain_schema);
        ProtectedToStringBuilder(sb,nameof(this.domain_name),this.domain_name);
        ProtectedToStringBuilder(sb,nameof(this.table_catalog),this.table_catalog);
        ProtectedToStringBuilder(sb,nameof(this.table_schema),this.table_schema);
        ProtectedToStringBuilder(sb,nameof(this.table_name),this.table_name);
        ProtectedToStringBuilder(sb,nameof(this.column_name),this.column_name);
    }
}
