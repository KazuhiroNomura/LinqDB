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
    //private e.Expression SetClause(SetClause x)=>x switch{
    //    FunctionCallSetClause y=>this.FunctionCallSetClause(y),
    //    _=>throw this.単純NotSupportedException(x)
    //};
    //private e.Expression FunctionCallSetClause(FunctionCallSetClause x){throw this.単純NotSupportedException(x);}
    private(string ParameterName,e.Expression NewValue)SetClause(SetClause x)=>x switch{
        AssignmentSetClause y=>this.AssignmentSetClause(y),
        //FunctionCallSetClause y=>this.FunctionCallSetClause(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private(string ParameterName,e.Expression NewValue)AssignmentSetClause(AssignmentSetClause x){
        //x.AssignmentKind=AssignmentKind.
        var Identifiers=x.Column.MultiPartIdentifier.Identifiers;
        var ParameterName=this.Identifier(Identifiers[Identifiers.Count-1]);
        //var Column=this.ColumnReferenceExpression(x.Column.MultiPartIdentifier.Identifiers);
        Debug.Assert(x.Variable is null);
        //var Variable=this.VariableReference(x.Variable);
        var NewValue=this.ScalarExpression(x.NewValue);
        return(ParameterName,NewValue);
    }
    private e.Expression FunctionCallSetClause(FunctionCallSetClause x){throw this.単純NotSupportedException(x);}
}
