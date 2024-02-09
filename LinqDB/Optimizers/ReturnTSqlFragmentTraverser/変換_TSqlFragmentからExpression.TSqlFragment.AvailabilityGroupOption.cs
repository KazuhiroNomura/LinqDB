using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    //TSqlFragment.AvailabilityGroupOption
    private e.Expression LiteralAvailabilityGroupOption(LiteralAvailabilityGroupOption x){throw this.単純NotSupportedException(x);}    
}
