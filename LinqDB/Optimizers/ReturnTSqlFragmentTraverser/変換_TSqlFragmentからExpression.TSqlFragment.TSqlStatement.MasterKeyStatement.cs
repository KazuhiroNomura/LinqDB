using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression CreateMasterKeyStatement(CreateMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterMasterKeyStatement(AlterMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
}
