using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using e = System.Linq.Expressions;
using LinqDB.Helpers;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private Type SqlDataTypeReference(SqlDataTypeReference x){
        var DBType=x.Name.BaseIdentifier.Value;
        return CommonLibrary.SQLのTypeからTypeに変換(DBType);
    }
    private Type UserDataTypeReference(UserDataTypeReference x){
        var DBType = x.Name.BaseIdentifier.Value;
        return CommonLibrary.SQLのTypeからTypeに変換(DBType);
    }
}
