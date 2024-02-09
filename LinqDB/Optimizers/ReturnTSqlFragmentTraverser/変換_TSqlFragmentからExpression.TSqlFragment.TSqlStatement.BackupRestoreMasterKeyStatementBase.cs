using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression BackupServiceMasterKeyStatement(BackupServiceMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BackupMasterKeyStatement(BackupMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RestoreMasterKeyStatement(RestoreMasterKeyStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression RestoreServiceMasterKeyStatement(RestoreServiceMasterKeyStatement x){throw this.単純NotSupportedException(x);}
}
