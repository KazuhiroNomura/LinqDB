using LinqDB.Sets;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace LinqDB.Databases.Schemas;
public class system {
    /// <summary>
    /// スキーマ情報
    /// </summary>
    public Set<PrimaryKeys.Reflection,Tables.Schema>Schemas { get; }
    /// <summary>
    /// テーブル情報
    /// </summary>
    public Set<PrimaryKeys.Reflection,Tables.Table> Tables { get; }
    /// <summary>
    /// ビュー情報
    /// </summary>
    public Set<PrimaryKeys.Reflection,Tables.View> Views { get; }
    /// <summary>
    /// テーブル列情報
    /// </summary>
    public Set<PrimaryKeys.Reflection,Tables.TableColumn> TableColumns { get; }
    /// <summary>
    /// ビュー列情報
    /// </summary>
    public Set<PrimaryKeys.Reflection,Tables.ViewColumn> ViewColumn { get; }
    internal system(Set<PrimaryKeys.Reflection,Tables.Schema> Schemas,Set<PrimaryKeys.Reflection,Tables.Table> Tables,Set<PrimaryKeys.Reflection,Tables.View> Views,Set<PrimaryKeys.Reflection,Tables.TableColumn> TableColumns,Set<PrimaryKeys.Reflection,Tables.ViewColumn> ViewColumn) {
        this.Schemas=Schemas;
        this.Tables=Tables;
        this.Views=Views;
        this.TableColumns=TableColumns;
        this.ViewColumn=ViewColumn;
    }
}
