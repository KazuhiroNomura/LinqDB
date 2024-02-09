using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression EventRetentionSessionOption(EventRetentionSessionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MemoryPartitionSessionOption(MemoryPartitionSessionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression LiteralSessionOption(LiteralSessionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MaxDispatchLatencySessionOption(MaxDispatchLatencySessionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OnOffSessionOption(OnOffSessionOption x){
        throw this.単純NotSupportedException(x);
    }
}
