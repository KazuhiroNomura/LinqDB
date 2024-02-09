using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Reflection;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private(PropertyInfo Schema,MethodInfo Method)ExecutableProcedureReference(ExecutableProcedureReference x){
        if(x.AdHocDataSource is not null){
            var AdHocDataSource=this.AdHocDataSource(x.AdHocDataSource);

        }
        return this.ProcedureReferenceName(x.ProcedureReference);

        //return ProcedureReference;
        //throw this.単純NotSupportedException(x);
    }
    private (PropertyInfo Schema,MethodInfo Method) ExecutableStringList(ExecutableStringList x){
        throw this.単純NotSupportedException(x);
    }
}
