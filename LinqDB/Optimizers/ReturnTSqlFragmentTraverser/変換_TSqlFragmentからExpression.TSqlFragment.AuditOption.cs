using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression QueueDelayAuditOption(QueueDelayAuditOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AuditGuidAuditOption(AuditGuidAuditOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OnFailureAuditOption(OnFailureAuditOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression StateAuditOption(StateAuditOption x){
        throw this.単純NotSupportedException(x);
    }
}
