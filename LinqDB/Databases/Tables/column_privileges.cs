using System;
using System.Text;
using LinqDB.Sets;
namespace LinqDB.Databases.Tables;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
public partial class column_privileges:Entity, IEquatable<column_privileges> {
    public readonly string grantor;
    public readonly string grantee;
    public readonly string table_catalog;
    public readonly string table_schema;
    public readonly string table_name;
    public readonly string column_name;
    public readonly string privilege_type;
    public readonly string is_grantable;
    public column_privileges(string grantor,string grantee,string table_catalog,string table_schema,string table_name,string column_name,string privilege_type,string is_grantable) {
        this.grantor=grantor;
        this.grantee=grantee;
        this.table_catalog=table_catalog;
        this.table_schema=table_schema;
        this.table_name=table_name;
        this.column_name=column_name;
        this.privilege_type=privilege_type;
        this.is_grantable=is_grantable;
    }
    public bool Equals(column_privileges? other){
        if(other is null) return false;
        if(!this.grantor.Equals(other.grantor)) return false;
        if(!this.grantee.Equals(other.grantee)) return false;
        if(!this.table_catalog.Equals(other.table_catalog)) return false;
        if(!this.table_schema.Equals(other.table_schema)) return false;
        if(!this.table_name.Equals(other.table_name)) return false;
        if(!this.column_name.Equals(other.column_name)) return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is column_privileges other&&this.Equals(other);
    public override int GetHashCode() => HashCode.Combine(this.grantor,this.grantee,this.table_catalog,this.table_schema,this.table_name,this.column_name,this.privilege_type,this.is_grantable);
    //public static bool operator ==(column_privileges a,column_privileges b) => a.Equals(b);
    //public static bool operator !=(column_privileges a,column_privileges b) => !a.Equals(b);
    protected override void ToStringBuilder(StringBuilder sb) {
        ProtectedToStringBuilder(sb,nameof(this.grantor),this.grantor);
        ProtectedToStringBuilder(sb,nameof(this.grantee),this.grantee);
        ProtectedToStringBuilder(sb,nameof(this.table_catalog),this.table_catalog);
        ProtectedToStringBuilder(sb,nameof(this.table_schema),this.table_schema);
        ProtectedToStringBuilder(sb,nameof(this.table_name),this.table_name);
        ProtectedToStringBuilder(sb,nameof(this.column_name),this.column_name);
    }
}
