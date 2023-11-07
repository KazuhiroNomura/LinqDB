using LinqDB.Helpers;

using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
//using Microsoft.CSharp.RuntimeBinder;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.CSharp.RuntimeBinder;
// ReSharper disable All
namespace LinqDB.Optimizers.VoidExpressionTraverser;
using Generic = System.Collections.Generic;
internal abstract class VoidExpressionTraverser_Quoteを処理しない:VoidExpressionTraverser {
    protected sealed override void Quote(UnaryExpression Unary) {
    }
}