using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression MaxSizeAuditTargetOption(MaxSizeAuditTargetOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MaxRolloverFilesAuditTargetOption(MaxRolloverFilesAuditTargetOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression LiteralAuditTargetOption(LiteralAuditTargetOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OnOffAuditTargetOption(OnOffAuditTargetOption x){
        throw this.単純NotSupportedException(x);
    }
}
