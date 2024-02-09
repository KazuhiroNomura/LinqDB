using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression ScalarExpressionRestoreOption(ScalarExpressionRestoreOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MoveRestoreOption(MoveRestoreOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression StopRestoreOption(StopRestoreOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileStreamRestoreOption(FileStreamRestoreOption x){
        throw this.単純NotSupportedException(x);
    }
}
