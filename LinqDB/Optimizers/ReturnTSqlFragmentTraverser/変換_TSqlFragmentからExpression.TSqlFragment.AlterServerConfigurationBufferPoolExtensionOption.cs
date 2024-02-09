using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression AlterServerConfigurationBufferPoolExtensionContainerOption(AlterServerConfigurationBufferPoolExtensionContainerOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerConfigurationBufferPoolExtensionSizeOption(AlterServerConfigurationBufferPoolExtensionSizeOption x){
        throw this.単純NotSupportedException(x);
    }
}
