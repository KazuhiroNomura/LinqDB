/*
 * a.Union(a)→a
 * a.Except(a)→Empty
 */
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
//using System.Runtime.Remoting.Messaging;
// ReSharper disable MemberHidesStaticFromOuterClass
namespace LinqDB.Optimizers;
partial class Optimizer {
    /// <summary>
    /// 不要なBlockを除く。Blockの連続を1つに。
    /// </summary>
    private sealed class 変換_最終正規化:ReturnExpressionTraverser_Quoteを処理しない {
        //private sealed class 判定_Parametersが存在するか:VoidExpressionTraverser_Quoteを処理しない {
        //    private Boolean 存在した;
        //    private IList<ParameterExpression>? Parameters;
        //    public Boolean 実行(Expression e,IList<ParameterExpression>? Parameters) {
        //        this.存在した=false;
        //        this.Parameters=Parameters;
        //        this.Traverse(e);
        //        return this.存在した;
        //    }
        //    public void Add(ParameterExpression Parameter){
        //        if(this.Parameters.Contains(Parameter))
        //            this.存在した=true;
        //    protected override void Lambda(LambdaExpression Lambda) {
        //    }
        //}
        private sealed class 判定_内部LambdaにParameterが存在するか:VoidExpressionTraverser_Quoteを処理しない {
            private bool 存在した;
            private ParameterExpression? 探したいParameter;
            private bool ラムダ内部;
            public bool 実行(Expression e,ParameterExpression 探したいParameter) {
                this.存在した=false;
                this.ラムダ内部=false;
                this.探したいParameter=探したいParameter;
                this.Traverse(e);
                return this.存在した;
            }

            protected override void Parameter(ParameterExpression Parameter) {
                if(this.ラムダ内部&&this.探したいParameter==Parameter)
                    this.存在した=true;
            }
            protected override void Lambda(LambdaExpression Lambda) {
                var ラムダ内部=this.ラムダ内部;
                this.ラムダ内部=true;
                this.Traverse(Lambda.Body);
                this.ラムダ内部=ラムダ内部;
            }
        }
        private readonly 判定_内部LambdaにParameterが存在するか _判定_内部LambdaにParameterが存在するか=new();
        public 変換_最終正規化(作業配列 作業配列) : base(作業配列) {
        }
        internal readonly Dictionary<ParameterExpression,FieldInfo> Dictionaryラムダ跨ぎParameter=default!;
        public Expression　実行(Expression e) {
            return this.Traverse(e);
        }

        protected override Expression Block(BlockExpression Block0){
            var 判定_内部LambdaにParameterが存在するか=this._判定_内部LambdaにParameterが存在するか;
            var Block0_Variables=Block0.Variables;
            var Block1_Variables=Block0_Variables.ToList();
            var Dictionaryラムダ跨ぎParameter=this.Dictionaryラムダ跨ぎParameter;
            foreach(var Variable in Block0.Variables)
                if(判定_内部LambdaにParameterが存在するか.実行(Block0,Variable)){
                    Block1_Variables.Remove(Variable);
                    Dictionaryラムダ跨ぎParameter.Add(Variable,default!);
                }
            if(Block0.Expressions.Count==1&&Block1_Variables.Count==0)return this.Traverse(Block0.Expressions[0]);
            var Block0_Expressions=Block0.Expressions;
            var Block0_Expressions_Count=Block0_Expressions.Count;
            var Block1_Expressions=new Expression[Block0_Expressions_Count];
            for(var a = 0;a<Block0_Expressions_Count;a++) {
                var Block1_Expression=this.Traverse(Block0_Expressions[a]);
                Block1_Expressions[a]=Block1_Expression;
            }
            return Expression.Block(Block1_Variables,Block1_Expressions);
        }
    }
}