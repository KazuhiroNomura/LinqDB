using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression SpatialIndexRegularOption(SpatialIndexRegularOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BoundingBoxSpatialIndexOption(BoundingBoxSpatialIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression GridsSpatialIndexOption(GridsSpatialIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CellsPerObjectSpatialIndexOption(CellsPerObjectSpatialIndexOption x){
        throw this.単純NotSupportedException(x);
    }
}
