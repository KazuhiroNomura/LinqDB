using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression AlterIndexStatement(AlterIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateIndexStatement(CreateIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateSelectiveXmlIndexStatement(CreateSelectiveXmlIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateXmlIndexStatement(CreateXmlIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
}
