using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    //TSqlFragment.BulkInsertOption
    private e.Expression LiteralBulkInsertOption(LiteralBulkInsertOption x){
        throw this.単純NotSupportedException(x);
    }
    //TSqlFragment.BulkInsertOption
    private e.Expression OrderBulkInsertOption(OrderBulkInsertOption x){throw this.単純NotSupportedException(x);}
}
