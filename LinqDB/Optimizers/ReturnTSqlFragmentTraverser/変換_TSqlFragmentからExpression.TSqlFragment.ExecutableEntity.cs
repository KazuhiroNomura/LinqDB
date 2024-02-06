using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using System.Xml.Linq;
using e = System.Linq.Expressions;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
using System.Globalization;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Helpers;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
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
