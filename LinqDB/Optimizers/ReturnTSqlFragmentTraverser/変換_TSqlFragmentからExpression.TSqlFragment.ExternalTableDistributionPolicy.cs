using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression ExternalTableReplicatedDistributionPolicy(ExternalTableReplicatedDistributionPolicy x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExternalTableRoundRobinDistributionPolicy(ExternalTableRoundRobinDistributionPolicy x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExternalTableShardedDistributionPolicy(ExternalTableShardedDistributionPolicy x){
        throw this.単純NotSupportedException(x);
    }
}
