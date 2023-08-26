using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.SqlServer.Dac.Model;
using System.Reflection;

namespace LinqDB.Optimizers;

partial class Optimizer {
    private sealed class 変換_跨ぎParameterの不要置換復元:ReturnExpressionTraverser_Quoteを処理しない {
        private Dictionary<Expression,Expression> DictionaryParameterExpression=new();

        internal 変換_跨ぎParameterの不要置換復元(作業配列 作業配列) : base(作業配列){
        }
        internal IReadOnlyDictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=default!;
        private bool IsByRef;
        public Expression 実行(Expression Expression0){
            this.IsByRef=false;
            return this.Traverse(Expression0);
        }

        protected override Expression Assign(BinaryExpression Assign0){
            var Assign0_Left=Assign0.Left;
            var Assign=Expression.Assign(
                base.Traverse(Assign0_Left),
                this.Traverse(Assign0.Right)
            );
            if(Assign0_Left is ParameterExpression Parameter&&this.Dictionaryラムダ跨ぎParameter.ContainsKey(Parameter)){
                if(!this.DictionaryParameterExpression.TryAdd(Assign0_Left,Assign0.Right))
                    this.DictionaryParameterExpression[Assign0_Left]=Assign0.Right;
            }
            return Assign;
        }
        protected override Expression Invoke(InvocationExpression Invocation0) {
            //評価順序を既定と変える。
            //a.Invoke(b)をb→aの順で評価したい。
            var Invocation0_Arguments=Invocation0.Arguments;
            var Invocation1_Arguments = this.TraverseExpressions(Invocation0_Arguments);
            var Invocation0_Expression = Invocation0.Expression;
            var Invocation1_Expression = this.Traverse(Invocation0_Expression);
            if(ReferenceEquals(Invocation0_Arguments,Invocation1_Arguments)&&Invocation0_Expression==Invocation1_Expression) return Invocation0;
            return Expression.Invoke(Invocation1_Expression,Invocation1_Arguments);
        }
        protected override Expression Lambda(LambdaExpression Lambda0){
            var DictionaryParameterExpression = this.DictionaryParameterExpression;
            this.DictionaryParameterExpression=new();
            var Lambda1_Body = this.Traverse(Lambda0.Body);
            var Lambda1 = Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0.Parameters);
            this.DictionaryParameterExpression=DictionaryParameterExpression;
            return Lambda1;
        }
        protected override Expression Call(MethodCallExpression MethodCall0){
            var MethodCall1_Object = this.TraverseNullable(MethodCall0.Object);
            var Method=MethodCall0.Method;
            var MethodCall0_Arguments = MethodCall0.Arguments;
            var MethodCall1_Arguments_Count = MethodCall0_Arguments.Count;
            var MethodCall1_Arguments= new Expression[MethodCall1_Arguments_Count];
            var Parameters=Method.GetParameters();
            for(var a=0;a<MethodCall1_Arguments_Count;a++){
                this.IsByRef=Parameters[a].ParameterType.IsByRef;
                MethodCall1_Arguments[a]=this.Traverse(MethodCall0_Arguments[a]);
            }
            this.IsByRef=false;
            if(MethodCall1_Object is null) return Expression.Call(MethodCall0.Method,MethodCall1_Arguments);
            else return Expression.Call(MethodCall1_Object,MethodCall0.Method,MethodCall1_Arguments);
        }
        protected override Expression Traverse(Expression Expression0){
            //todo このExpressionが配置される場所がrefであったならば置換しないようにする
            if(this.IsByRef){
                if(this.DictionaryParameterExpression.ContainsKey(Expression0)) this.DictionaryParameterExpression.Remove(Expression0);
            }else if(this.DictionaryParameterExpression.TryGetValue(Expression0,out var Expression1)){
                return Expression1;
            }
            return base.Traverse(Expression0);
        }
    }
}