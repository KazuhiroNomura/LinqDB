using System;
using System.Text;
using LinqDB.Sets;
namespace LinqDB.Databases.Tables;
[Serializable]
public class columns:Entity, IEquatable<columns> {
    public readonly string table_catalog;
    public readonly string table_schema;
    public readonly string table_name;
    public readonly string column_name;
    //public readonly Int32 ordinal_position;
    public readonly object column_default;
    public readonly bool is_nullable;
    public readonly Type data_type;
    //public readonly Int32 character_maximum_length;
    //public readonly Int32 character_octet_length;
    //public readonly Byte numeric_precision;
    //public readonly Int16 numeric_precision_radix;
    //public readonly Int32 numeric_scale;
    //public readonly Int16 datetime_precision;
    //public readonly String character_set_catalog;
    //public readonly String character_set_schema;
    //public readonly String character_set_name;
    //public readonly String collation_catalog;
    //public readonly String collation_schema;
    //public readonly String collation_name;
    //public readonly String domain_catalog;
    //public readonly String domain_schema;
    //public readonly String domain_name;
    //public columns(String table_catalog,String table_schema,String table_name,String column_name,Int32 ordinal_position,String column_default,Boolean is_nullable,Type data_type,Int32 character_maximum_length,Int32 character_octet_length,Byte numeric_precision,Int16 numeric_precision_radix,Int32 numeric_scale,Int16 datetime_precision,String character_set_catalog,String character_set_schema,String character_set_name,String collation_catalog,String collation_schema,String collation_name,String domain_catalog,String domain_schema,String domain_name) {
    public columns(string table_catalog,string table_schema,string table_name,string column_name,object column_default,bool is_nullable,Type data_type) {
        this.table_catalog=table_catalog;
        this.table_schema=table_schema;
        this.table_name=table_name;
        this.column_name=column_name;
        this.column_default=column_default;
        this.is_nullable=is_nullable;
        this.data_type=data_type;
    }
    public bool Equals(columns? other) {
        if(other is null) return false;
        //if(!this.table_catalog.Equals(other.table_catalog)) return false;
        //if(!this.table_schema.Equals(other.table_schema)) return false;
        //if(!this.table_name.Equals(other.table_name)) return false;
        //if(!this.column_name.Equals(other.column_name)) return false;
        //if(!this.ordinal_position.Equals(other.ordinal_position)) return false;
        //if(!this.column_default.Equals(other.column_default)) return false;
        //if(!this.is_nullable.Equals(other.is_nullable)) return false;
        //if(!this.data_type.Equals(other.data_type)) return false;
        //if(!this.character_maximum_length.Equals(other.character_maximum_length)) return false;
        //if(!this.character_octet_length.Equals(other.character_octet_length)) return false;
        //if(!this.numeric_precision.Equals(other.numeric_precision)) return false;
        //if(!this.numeric_precision_radix.Equals(other.numeric_precision_radix)) return false;
        //if(!this.numeric_scale.Equals(other.numeric_scale)) return false;
        //if(!this.datetime_precision.Equals(other.datetime_precision)) return false;
        //if(!this.character_set_catalog.Equals(other.character_set_catalog)) return false;
        //if(!this.character_set_schema.Equals(other.character_set_schema)) return false;
        //if(!this.character_set_name.Equals(other.character_set_name)) return false;
        //if(!this.collation_catalog.Equals(other.collation_catalog)) return false;
        //if(!this.collation_schema.Equals(other.collation_schema)) return false;
        //if(!this.collation_name.Equals(other.collation_name)) return false;
        //if(!this.domain_catalog.Equals(other.domain_catalog)) return false;
        //if(!this.domain_schema.Equals(other.domain_schema)) return false;
        //if(!this.domain_name.Equals(other.domain_name)) return false;
        if(!this.table_catalog.Equals(other.table_catalog)) return false;
        if(!this.table_schema.Equals(other.table_schema)) return false;
        if(!this.table_name.Equals(other.table_name)) return false;
        if(!this.column_name.Equals(other.column_name)) return false;
        if(!this.column_default.Equals(other.column_default)) return false;
        if(!this.is_nullable.Equals(other.is_nullable)) return false;
        if(this.data_type != other.data_type) return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is columns other && this.Equals(other);

    public override int GetHashCode() => HashCode.Combine(this.table_catalog,this.table_schema,this.table_name,this.column_name,this.column_default,this.is_nullable,this.data_type);

    public static bool operator ==(columns a,columns b) => a.Equals(b);
    public static bool operator !=(columns a,columns b) => !a.Equals(b);
    protected override void ToStringBuilder(StringBuilder sb) {
        //ProtectedToStringBuilder(sb,nameof(this.table_catalog),this.table_catalog);
        //ProtectedToStringBuilder(sb,nameof(this.table_schema),this.table_schema);
        //ProtectedToStringBuilder(sb,nameof(this.table_name),this.table_name);
        //ProtectedToStringBuilder(sb,nameof(this.column_name),this.column_name);
        //ProtectedToStringBuilder(sb,nameof(this.ordinal_position),this.ordinal_position);
        //ProtectedToStringBuilder(sb,nameof(this.column_default),this.column_default);
        //ProtectedToStringBuilder(sb,nameof(this.is_nullable),this.is_nullable);
        //ProtectedToStringBuilder(sb,nameof(this.data_type),this.data_type);
        //ProtectedToStringBuilder(sb,nameof(this.character_maximum_length),this.character_maximum_length);
        //ProtectedToStringBuilder(sb,nameof(this.character_octet_length),this.character_octet_length);
        //ProtectedToStringBuilder(sb,nameof(this.numeric_precision),this.numeric_precision);
        //ProtectedToStringBuilder(sb,nameof(this.numeric_precision_radix),this.numeric_precision_radix);
        //ProtectedToStringBuilder(sb,nameof(this.numeric_scale),this.numeric_scale);
        //ProtectedToStringBuilder(sb,nameof(this.datetime_precision),this.datetime_precision);
        //ProtectedToStringBuilder(sb,nameof(this.character_set_catalog),this.character_set_catalog);
        //ProtectedToStringBuilder(sb,nameof(this.character_set_schema),this.character_set_schema);
        //ProtectedToStringBuilder(sb,nameof(this.character_set_name),this.character_set_name);
        //ProtectedToStringBuilder(sb,nameof(this.collation_catalog),this.collation_catalog);
        //ProtectedToStringBuilder(sb,nameof(this.collation_schema),this.collation_schema);
        //ProtectedToStringBuilder(sb,nameof(this.collation_name),this.collation_name);
        //ProtectedToStringBuilder(sb,nameof(this.domain_catalog),this.domain_catalog);
        //ProtectedToStringBuilder(sb,nameof(this.domain_schema),this.domain_schema);
        //ProtectedToStringBuilder(sb,nameof(this.domain_name),this.domain_name);
        ProtectedToStringBuilder(sb,nameof(this.table_catalog),this.table_catalog);
        ProtectedToStringBuilder(sb,nameof(this.table_schema),this.table_schema);
        ProtectedToStringBuilder(sb,nameof(this.table_name),this.table_name);
        ProtectedToStringBuilder(sb,nameof(this.column_name),this.column_name);
        ProtectedToStringBuilder(sb,nameof(this.column_default),this.column_default);
        ProtectedToStringBuilder(sb,nameof(this.is_nullable),this.is_nullable);
        ProtectedToStringBuilder(sb,nameof(this.data_type),this.data_type);
        //ProtectedToStringBuilder(sb,nameof(this.domain_catalog),this.domain_catalog);
        //ProtectedToStringBuilder(sb,nameof(this.domain_schema),this.domain_schema);
        //ProtectedToStringBuilder(sb,nameof(this.domain_name),this.domain_name);
    }
}
