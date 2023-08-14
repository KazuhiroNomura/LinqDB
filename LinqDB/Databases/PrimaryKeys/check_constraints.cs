using System;
using System.Text;
using LinqDB.CRC;

namespace LinqDB.Databases.PrimaryKeys;
[Serializable]
public struct check_constraints:IEquatable<check_constraints> {
    public readonly string constraint_catalog;
    public readonly string constraint_schema;
    public readonly string constraint_name;
    public check_constraints(string constraint_catalog,string constraint_schema,string constraint_name) {
        this.constraint_catalog=constraint_catalog;
        this.constraint_schema=constraint_schema;
        this.constraint_name=constraint_name;
    }
    public bool Equals(check_constraints other) {
        if(!this.constraint_catalog.Equals(other.constraint_catalog)) return false;
        if(!this.constraint_schema.Equals(other.constraint_schema)) return false;
        if(!this.constraint_name.Equals(other.constraint_name)) return false;
        return true;
    }
    internal void InputHashCode(ref CRC32 CRC) {
        CRC.Input(this.constraint_catalog);
        CRC.Input(this.constraint_schema);
        CRC.Input(this.constraint_name);
    }
    public override int GetHashCode() {
        var CRC = new CRC32();
        this.InputHashCode(ref CRC);
        return CRC.GetHashCode();
    }
    internal void ToStringBuilder(StringBuilder sb) {
        sb.Append(nameof(this.constraint_catalog)+'=').AppendLine(this.constraint_catalog);
        sb.Append(nameof(this.constraint_schema)+'=').AppendLine(this.constraint_schema);
        sb.Append(nameof(this.constraint_name)+'=').AppendLine(this.constraint_name);
    }
    public override string ToString() {
        var sb = new StringBuilder();
        this.ToStringBuilder(sb);
        return sb.ToString();
    }
    public static bool operator ==(check_constraints a,check_constraints b) => a.Equals(b);
    public static bool operator !=(check_constraints a,check_constraints b) => !a.Equals(b);
    public override bool Equals(object? obj) => obj is check_constraints other&&this.Equals(other);
}
