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
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using Generic = System.Collections.Generic;
internal abstract class ReturnExpressionTraverser_Quoteを処理しない:ReturnExpressionTraverser {
    protected ReturnExpressionTraverser_Quoteを処理しない(作業配列 作業配列) : base(作業配列){}
    //protected sealed override Expression Quote(UnaryExpression Unary0) => Unary0;
}
