using LinqDB.Sets;
using LinqDB.Databases.Tables;
using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace LinqDB.Databases.Schemas;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
public partial class information_schema {
    /// <summary>
    /// テーブル情報
    /// </summary>
    public ImmutableSet<tables> tables { get; }
    /// <summary>
    /// 列情報
    /// </summary>
    public ImmutableSet<columns> columns { get; }
    /// <summary>
    /// リレーション情報
    /// </summary>
    public ImmutableSet<referential_constraints> referential_constraints { get;}
    internal information_schema(ImmutableSet<tables> tables,ImmutableSet<columns> columns,ImmutableSet<referential_constraints> referential_constraints) {
        this.tables=tables;
        this.columns=columns;
        this.referential_constraints=referential_constraints;
    }
}
