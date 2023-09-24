using LinqDB.Sets;

using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace LinqDB.Databases.Schemas;
public class system {
    /// <summary>
    /// スキーマ情報
    /// </summary>
    public Set<Tables.Schema,PrimaryKeys.Reflection>Schemas { get; }
    /// <summary>
    /// テーブル情報
    /// </summary>
    public Set<Tables.Table,PrimaryKeys.Reflection> Tables { get; }
    /// <summary>
    /// ビュー情報
    /// </summary>
    public Set<Tables.View,PrimaryKeys.Reflection> Views { get; }
    /// <summary>
    /// テーブル列情報
    /// </summary>
    public Set<Tables.TableColumn,PrimaryKeys.Reflection> TableColumns { get; }
    /// <summary>
    /// ビュー列情報
    /// </summary>
    public Set<Tables.ViewColumn,PrimaryKeys.Reflection> ViewColumn { get; }
    internal system(Set<Tables.Schema,PrimaryKeys.Reflection> Schemas,Set<Tables.Table,PrimaryKeys.Reflection> Tables,Set<Tables.View,PrimaryKeys.Reflection> Views,Set<Tables.TableColumn,PrimaryKeys.Reflection> TableColumns,Set<Tables.ViewColumn,PrimaryKeys.Reflection> ViewColumn) {
        this.Schemas=Schemas;
        this.Tables=Tables;
        this.Views=Views;
        this.TableColumns=TableColumns;
        this.ViewColumn=ViewColumn;
    }
}
