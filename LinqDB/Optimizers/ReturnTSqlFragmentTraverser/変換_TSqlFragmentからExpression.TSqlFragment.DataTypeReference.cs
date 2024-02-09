using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private Type ParameterizedDataTypeReference(ParameterizedDataTypeReference x)=>x switch{
        SqlDataTypeReference y=>this.SqlDataTypeReference(y),
        UserDataTypeReference y=>this.UserDataTypeReference(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private Type XmlDataTypeReference(XmlDataTypeReference x){
        throw this.単純NotSupportedException(x);
    }
}
