using System.Linq.Expressions;
//using Microsoft.CSharp.RuntimeBinder;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
// ReSharper disable All
namespace LinqDB.Optimizers.VoidExpressionTraverser;
using Generic = System.Collections.Generic;
internal abstract class VoidExpressionTraverser_Quoteを処理しない:VoidExpressionTraverser {
    protected sealed override void Quote(UnaryExpression Unary) {
    }
}