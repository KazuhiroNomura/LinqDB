using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
namespace LinqDB.Optimizers;
partial class Optimizer {
    /// <summary>
    /// ラムダを跨ぐParameterExpressionを取得
    /// Blockから一旦Parameterを削除し、内部で使われていたら追加する
    /// LambdaExpressionを取得
    /// Quoteを取得
    /// </summary>
    private sealed class 変換_跨ぎParameterをBlock_Variablesに:ReturnExpressionTraverser {
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
        private readonly IEnumerable<ParameterExpression> ループ跨ぎParameters;
        //private readonly List<ParameterExpression> Block_Variables=new();
        public 変換_跨ぎParameterをBlock_Variablesに(作業配列 作業配列,IEnumerable<ParameterExpression> ループ跨ぎParameters) : base(作業配列) {
            this.ループ跨ぎParameters=ループ跨ぎParameters;
        }
        internal Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic=default!;
        internal Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=default!;
        internal Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)>DictionaryLambda=default!;
        private List<ParameterExpression> Block_Variables=default!;
        public Expression 実行(Expression Lambda0) {
            this.DictionaryDynamic.Clear();
            //var Block_Variables=this.Block_Variables;
            //Block_Variables.Clear();
            //var Lambda=(LambdaExpression)Lambda0;
            //var Lambda1_Body=this.Traverse(Lambda.Body);
            ////var Lambda1 = (LambdaExpression)base.Lambda(Lambda0);
            //var Block1=Expression.Block(Block_Variables,this._作業配列.Expressions設定(Lambda1_Body));
            //var Lambda1=Expression.Lambda(Lambda.Type,Block1,Lambda.Name,Lambda.TailCall,Lambda.Parameters);
            //if(!this.DictionaryLambda.ContainsKey(Lambda1)) this.DictionaryLambda.Add(Lambda1,default!);
            //return Lambda1;
            //var Lambda1 = (LambdaExpression)this.Lambda((LambdaExpression)Lambda0);
            //var Block1 = Expression.Block(Block_Variables,this._作業配列.Expressions設定(Lambda1.Body));
            //return Expression.Lambda(Lambda1.Type,Block1,Lambda1.Name,Lambda1.TailCall,Lambda1.Parameters);
            return(LambdaExpression)this.Lambda((LambdaExpression)Lambda0);
        }
        //private static CallSite<T> CallSite_Unary<T>(ExpressionType NodeType) where T:class=>CallSite<T>.Create(RuntimeBinder.Binder.UnaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray1));
        //private static CallSite<Func<CallSite,object,object>> CallSite_Unary(ExpressionType NodeType)=> CallSite_Unary<Func<CallSite,object,object>>(NodeType);
        protected override Expression Dynamic(DynamicExpression Dynamic0){
            var Dynamic1=(DynamicExpression)base.Dynamic(Dynamic0);
            if(this.DictionaryDynamic.ContainsKey(Dynamic1)) return Dynamic1;
            this.DictionaryDynamic.Add(Dynamic1,default!);
            return Dynamic1;
        }
        protected override Expression Call(MethodCallExpression MethodCall0) {
            if(Reflection.Object.GetType_==MethodCall0.Method&&MethodCall0.Object!.Type.IsValueType)
                return Expression.Call(
                    Expression.Convert(this.Traverse(MethodCall0.Object),typeof(object)),
                    MethodCall0.Method
                );
            return base.Call(MethodCall0);
        }
        protected override Expression Lambda(LambdaExpression Lambda0) {
            var 旧Block_Variables = this.Block_Variables;
            var 新Block_Variables = this.Block_Variables=new();
            var Lambda0_Body=Lambda0.Body;
            var Lambda1_Body = this.Traverse(Lambda0.Body);
            if(新Block_Variables.Count>0){
                var Block1 = Expression.Block(新Block_Variables,this._作業配列.Expressions設定(Lambda1_Body));
                Lambda0 = Expression.Lambda(Lambda0.Type,Block1,Lambda0.Name,Lambda0.TailCall,Lambda0.Parameters);
            } else if(Lambda0_Body!=Lambda1_Body){
                Lambda0 = Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0.Parameters);
            }
            if(!this.DictionaryLambda.ContainsKey(Lambda0)) this.DictionaryLambda.Add(Lambda0,default!);
            this.Block_Variables=旧Block_Variables;
            return Lambda0;
        }
        protected override Expression Assign(BinaryExpression Assign0) {
            //if(Binary0.Left is ParameterExpression Parameter&& Parameter.Name == null && this.ループ跨ぎParameters.Contains(Parameter) && !this.Block_Variables!.Contains(Parameter))
            //if(Binary0.Left is ParameterExpression{Name: null} Parameter&&this.ループ跨ぎParameters.Contains(Parameter)&&!this.Block_Variables!.Contains(Parameter))
            //if(Binary0.Left is ParameterExpression Parameter&& Parameter.Name != null && this.ループ跨ぎParameters.Contains(Parameter) && !this.Block_Variables!.Contains(Parameter))
            if(Assign0.Left is ParameterExpression { Name: { } } Parameter&&this.ループ跨ぎParameters.Contains(Parameter)&&!this.Block_Variables!.Contains(Parameter))
                this.Block_Variables!.Add(Parameter);
            return base.Assign(Assign0);
        }
        protected override Expression Block(BlockExpression Block0) {
            var 判定_内部LambdaにParameterが存在するか=this._判定_内部LambdaにParameterが存在するか;
            //Blockのローカル変数はラムダを跨いでるのは削除する。
            var Block1_Variables=Block0.Variables.ToList();
            var Dictionaryラムダ跨ぎParameter=this.Dictionaryラムダ跨ぎParameter;
            foreach(var Variable in Block0.Variables)
                if(判定_内部LambdaにParameterが存在するか.実行(Block0,Variable)){
                    //内部ラムダでアクセスしてるのでローカル変数ではない
                    Block1_Variables.Remove(Variable);
                    Dictionaryラムダ跨ぎParameter.Add(Variable,default!);
                }
            var Block0_Expressions=Block0.Expressions;
            var Block0_Expressions_Count=Block0_Expressions.Count;
            var Block1_Expressions=new Expression[Block0_Expressions_Count];
            for(var a = 0;a<Block0_Expressions_Count;a++)
                Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
            return Expression.Block(Block1_Variables,Block1_Expressions);
        }
    }
}
