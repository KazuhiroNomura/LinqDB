using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression CreateExternalDataSourceStatement(CreateExternalDataSourceStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterExternalDataSourceStatement(AlterExternalDataSourceStatement x){
        throw this.単純NotSupportedException(x);
    }
}
