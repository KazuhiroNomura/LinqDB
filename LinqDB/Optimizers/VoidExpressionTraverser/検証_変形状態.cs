using System;
using System.Linq.Expressions;
using LinqDB.Reflection;
//using Microsoft.CSharp.RuntimeBinder;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
// ReSharper disable All
namespace LinqDB.Optimizers.VoidExpressionTraverser;
using Generic = System.Collections.Generic;
using static Common;
internal sealed class 検証_変形状態:VoidExpressionTraverser_Quoteを処理しない{
    public void 実行(Expression e)=>this.Traverse(e);
    protected override void Traverse(Expression Expression) {
        if(Expression is BinaryExpression Binary) {
            var Binary_Method = Binary.Method;
            if(Binary_Method is not null&&!Binary_Method.IsStatic)
                throw new InvalidOperationException("Binary演算子のメソッドはstaticであるべき");
        }
        if(Expression is UnaryExpression Unary) {
            var Unary_Method = Unary.Method;
            if(Unary_Method is not null&&!Unary_Method.IsStatic)
                throw new InvalidOperationException("Unary演算子のメソッドはstaticであるべき");
        }
        base.Traverse(Expression);
    }

    protected override void Call(MethodCallExpression MethodCall) {
        var GenericMethodDefinition = GetGenericMethodDefinition(MethodCall.Method);
        if(ExtensionSet.Select_selector==GenericMethodDefinition&&MethodCall.Arguments[0] is LambdaExpression selector&&selector.Body==selector.Parameters[0]){
            throw new InvalidOperationException("Select_selector(p=>p)は削除されるべき");
        }
        var MethodCall_Object = MethodCall.Object;
        if(MethodCall_Object is not null) {
            this.Traverse(MethodCall_Object);
        }
        this.TraverseExpressions(MethodCall.Arguments);
        var MethodCall_Arguments = MethodCall.Arguments;
        if(MethodCall_Arguments.Count==0){
            return;
        }
        if(!(MethodCall_Arguments[0] is MethodCallExpression MethodCall_MethodCall)){
            return;
        }
        var MethodCall_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall_MethodCall.Method);
        //プローブ.Type==typeof(Object)
        //    ? nameof(DictionaryAscList<Int32,Int32>.GetObjectValue)
        //    : nameof(DictionaryAscList<Int32,Int32>.GetTKeyValue),
        //if(
        //    (
        //        nameof(Sets.LookupList<int,int>.GetObjectValue)==GenericMethodDefinition.Name||
        //        nameof(Sets.LookupList<int,int>.GetTKeyValue)==GenericMethodDefinition.Name
        //    )&&
        //    ExtensionEnumerable.Lookup==MethodCall_GenericMethodDefinition||
        //    (
        //        nameof(Sets.SetGroupingSet<int,int>.GetObjectValue)==GenericMethodDefinition.Name||
        //        nameof(Sets.LookupSet<int,int>.GetTKeyValue)==GenericMethodDefinition.Name
        //    )&&
        //    ExtensionSet.Lookup==MethodCall_GenericMethodDefinition
        //){
        //    throw new InvalidOperationException("Dictionary.Equalが連続してはいけない。Dictionaryは上位のラムダに移動してthisメンバにより参照されるはず。");
        //}
    }
}
