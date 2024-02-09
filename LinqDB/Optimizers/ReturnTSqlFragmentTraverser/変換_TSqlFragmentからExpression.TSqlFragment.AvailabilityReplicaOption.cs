using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression LiteralReplicaOption(LiteralReplicaOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AvailabilityModeReplicaOption(AvailabilityModeReplicaOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FailoverModeReplicaOption(FailoverModeReplicaOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression PrimaryRoleReplicaOption(PrimaryRoleReplicaOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SecondaryRoleReplicaOption(SecondaryRoleReplicaOption x){
        throw this.単純NotSupportedException(x);
    }
}
