using System.Collections.Generic;
using System.Linq.Expressions;
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
    private sealed class 取得_Dictionary:VoidExpressionTraverser {
        internal Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant=default!;
        internal Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic=default!;
        internal Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=default!;
        internal Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)>DictionaryLambda=default!;
        private readonly List<ParameterExpression> Parameters=new();
        public void 実行(Expression Lambda) {
            this.DictionaryDynamic.Clear();
            this.Dictionaryラムダ跨ぎParameter.Clear();
            this.DictionaryLambda.Clear();
            this.Parameters.Clear();
            this.Lambda((LambdaExpression)Lambda);
        }
        protected override void Constant(ConstantExpression Constant){
            if(ILで直接埋め込めるか(Constant.Type))return;
            this.DictionaryConstant.TryAdd(Constant,default!);
        }
        protected override void Dynamic(DynamicExpression Dynamic){
            base.Dynamic(Dynamic);
            this.DictionaryDynamic.TryAdd(Dynamic,default!);
        }
        protected override void Lambda(LambdaExpression Lambda){
            this.Traverse(Lambda.Body);
            this.DictionaryLambda.TryAdd(Lambda,default!);
        }
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
        protected override void Block(BlockExpression Block){
            var 判定_内部LambdaにParameterが存在するか=this._判定_内部LambdaにParameterが存在するか;
            var Dictionaryラムダ跨ぎParameter=this.Dictionaryラムダ跨ぎParameter;
            foreach(var Variable in Block.Variables)
                if(判定_内部LambdaにParameterが存在するか.実行(Block,Variable))
                    Dictionaryラムダ跨ぎParameter.Add(Variable,default!);
            base.Block(Block);
        }
        //protected override void Parameter(ParameterExpression Parameter){
        //    if(this.Parameters.Contains(Parameter))return;
        //    if(!this.Dictionaryラムダ跨ぎParameter.ContainsKey(Parameter))this.Dictionaryラムダ跨ぎParameter.Add(Parameter,default!);
        //}
        //protected override void Try(TryExpression Try){
        //    this.Traverse(Try.Body);
        //    var Parameters=this.Parameters;
        //    foreach(var Try_Handler in Try.Handlers){
        //        if(Try_Handler.Variable is null){
        //            this.Traverse(Try_Handler.Body);
        //        } else{
        //            Parameters.Add(Try_Handler.Variable);
        //            this.Traverse(Try_Handler.Body);
        //            Parameters.RemoveAt(Parameters.Count-1);
        //        }
        //    }
        //    if(Try.Finally is not null){
        //        Debug.Assert(Try.Fault is null);
        //        this.Traverse(Try.Finally);
        //    }else if(Try.Fault is not null){
        //        Debug.Assert(Try.Finally is null);
        //        this.Traverse(Try.Fault);
        //    }
        //}
    }
}
