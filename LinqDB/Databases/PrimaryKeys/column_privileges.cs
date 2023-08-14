using System;
using System.Text;
using LinqDB.CRC;

namespace LinqDB.Databases.PrimaryKeys;
[Serializable]
public struct column_privileges:IEquatable<column_privileges> {
    public readonly string domain_catalog;
    public readonly string domain_schema;
    public readonly string domain_name;
    public readonly string table_catalog;
    public readonly string table_schema;
    public readonly string table_name;
    public column_privileges(string domain_catalog,string domain_schema,string domain_name,string table_catalog,string table_schema,string table_name) {
        this.domain_catalog=domain_catalog;
        this.domain_schema=domain_schema;
        this.domain_name=domain_name;
        this.table_catalog=table_catalog;
        this.table_schema=table_schema;
        this.table_name=table_name;
    }
    public bool Equals(column_privileges other) {
        if(!this.domain_catalog.Equals(other.domain_catalog)) return false;
        if(!this.domain_schema.Equals(other.domain_schema)) return false;
        if(!this.domain_name.Equals(other.domain_name)) return false;
        if(!this.table_catalog.Equals(other.table_catalog)) return false;
        if(!this.table_schema.Equals(other.table_schema)) return false;
        if(!this.table_name.Equals(other.table_name)) return false;
        return true;
    }
    internal void InputHashCode(ref CRC32 CRC) {
        CRC.Input(this.domain_catalog);
        CRC.Input(this.domain_schema);
        CRC.Input(this.domain_name);
        CRC.Input(this.table_catalog);
        CRC.Input(this.table_schema);
        CRC.Input(this.table_name);
    }
    public override int GetHashCode() {
        var CRC = new CRC32();
        this.InputHashCode(ref CRC);
        return CRC.GetHashCode();
    }
    internal void ToStringBuilder(StringBuilder sb) {
        sb.Append(nameof(this.domain_catalog)+'=').AppendLine(this.domain_catalog);
        sb.Append(nameof(this.domain_schema)+'=').AppendLine(this.domain_schema);
        sb.Append(nameof(this.domain_name)+'=').AppendLine(this.domain_name);
        sb.Append(nameof(this.table_catalog)+'=').AppendLine(this.table_catalog);
        sb.Append(nameof(this.table_schema)+'=').AppendLine(this.table_schema);
        sb.Append(nameof(this.table_name)+'=').AppendLine(this.table_name);
    }
    public override string ToString() {
        var sb = new StringBuilder();
        this.ToStringBuilder(sb);
        return sb.ToString();
    }
    public static bool operator ==(column_privileges a,column_privileges b) => a.Equals(b);
    public static bool operator !=(column_privileges a,column_privileges b) => !a.Equals(b);
    public override bool Equals(object? obj) =>obj is column_privileges other&&this.Equals(other);
}
