using System;

namespace LinqDB.Databases.Dom;
public interface IDataType {
    Type Type { get; }
    bool IsNullable { get; }
    Type Nullableを考慮したType {
        get {
            if(this.IsNullable&&this.Type.IsValueType) {
                return typeof(Nullable<>).MakeGenericType(this.Type);
            }
            return this.Type;
        }
    }
    /// <summary>
    /// 参照型に?を付けるか
    /// </summary>
    public bool NullableAttribute => this.IsNullable&&!this.Type.IsValueType;
}