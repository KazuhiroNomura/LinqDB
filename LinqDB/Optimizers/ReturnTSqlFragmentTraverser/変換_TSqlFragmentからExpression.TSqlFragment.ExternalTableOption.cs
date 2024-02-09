using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression ExternalTableLiteralOrIdentifierOption(ExternalTableLiteralOrIdentifierOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExternalTableDistributionOption(ExternalTableDistributionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExternalTableRejectTypeOption(ExternalTableRejectTypeOption x){
        throw this.単純NotSupportedException(x);
    }
}
