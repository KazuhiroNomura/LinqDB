using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression CreateFullTextCatalogStatement(CreateFullTextCatalogStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterFullTextCatalogStatement(AlterFullTextCatalogStatement x){
        throw this.単純NotSupportedException(x);
    }
}
