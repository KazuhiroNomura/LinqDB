using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression CreateSecurityPolicyStatement(CreateSecurityPolicyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterSecurityPolicyStatement(AlterSecurityPolicyStatement x){
        throw this.単純NotSupportedException(x);
    }
}
