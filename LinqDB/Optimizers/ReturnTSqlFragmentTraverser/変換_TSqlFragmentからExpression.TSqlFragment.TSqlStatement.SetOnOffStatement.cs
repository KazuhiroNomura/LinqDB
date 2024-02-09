using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    /// <summary>
    /// SET NOCOUNTなどのオプション設定
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression PredicateSetStatement(PredicateSetStatement x)=>Default_void;
    private e.Expression SetStatisticsStatement(SetStatisticsStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetOffsetsStatement(SetOffsetsStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetIdentityInsertStatement(SetIdentityInsertStatement x){throw this.単純NotSupportedException(x);}
}
