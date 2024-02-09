using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression ResampleStatisticsOption(ResampleStatisticsOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OnOffStatisticsOption(OnOffStatisticsOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression LiteralStatisticsOption(LiteralStatisticsOption x){
        throw this.単純NotSupportedException(x);
    }
}
