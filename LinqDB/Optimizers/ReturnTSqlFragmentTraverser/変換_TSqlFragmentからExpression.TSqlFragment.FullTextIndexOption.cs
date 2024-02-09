using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression ChangeTrackingFullTextIndexOption(ChangeTrackingFullTextIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression StopListFullTextIndexOption(StopListFullTextIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SearchPropertyListFullTextIndexOption(SearchPropertyListFullTextIndexOption x){
        throw this.単純NotSupportedException(x);
    }
}
