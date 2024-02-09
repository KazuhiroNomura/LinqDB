using Microsoft.SqlServer.TransactSql.ScriptDom;
//using System.Collections.Generic;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    //private e.Expression Set TableReferenceWithAlias(TableReferenceWithAlias x)=>x switch{
    //    FullTextTableReference y=>this.FullTextTableReference(y),
    //    //GlobalFunctionTableReference y=>this.GlobalFunctionTableReference(y),
    //    //InternalOpenRowset y=>this.InternalOpenRowset(y),
    //    //OpenJsonTableReference y=>this.OpenJsonTableReference(y),
    //    //OpenQueryTableReference y=>this.OpenQueryTableReference(y),
    //    //OpenXmlTableReference y=>this.OpenXmlTableReference(y),
    //    //SemanticTableReference y=>this.SemanticTableReference(y),
    //    //UnpivotedTableReference y=>this.UnpivotedTableReference(y),
    //    AdHocTableReference y => this.AdHocTableReference(y),
    //    BuiltInFunctionTableReference y => this.BuiltInFunctionTableReference(y),
    //    NamedTableReference y => this.NamedTableReference(y),
    //    PivotedTableReference y => this.PivotedTableReference(y),
    //    TableReferenceWithAliasAndColumns y => this.TableReferenceWithAliasAndColumns(y),
    //    VariableTableReference y => this.VariableTableReference(y),
    //    _ => throw this.単純NotSupportedException(x)
    //};
    /// <summary>
    /// Set,Elementを返すがElementは必要ないのではと思うがSelectされた列を参照する式をDictionaryにAddするためParameterが必要
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private(e.Expression Set,e.ParameterExpression Element)TableReferenceWithAlias(TableReferenceWithAlias x)=>x switch{
        FullTextTableReference y=>this.FullTextTableReference(y),
        //GlobalFunctionTableReference y=>this.GlobalFunctionTableReference(y),
        //InternalOpenRowset y=>this.InternalOpenRowset(y),
        //OpenJsonTableReference y=>this.OpenJsonTableReference(y),
        //OpenQueryTableReference y=>this.OpenQueryTableReference(y),
        //OpenXmlTableReference y=>this.OpenXmlTableReference(y),
        //SemanticTableReference y=>this.SemanticTableReference(y),
        //UnpivotedTableReference y=>this.UnpivotedTableReference(y),
        AdHocTableReference               y=>this.AdHocTableReference(y),
        BuiltInFunctionTableReference     y=>this.BuiltInFunctionTableReference(y),
        NamedTableReference               y=>this.NamedTableReference(y),
        PivotedTableReference             y=>this.PivotedTableReference(y),
        TableReferenceWithAliasAndColumns y=>this.TableReferenceWithAliasAndColumns(y),
        VariableTableReference            y=>this.VariableTableReference(y),
        _=>throw this.単純NotSupportedException(x)
    };
}
