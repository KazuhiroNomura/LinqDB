using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers;
partial class Optimizer {
    internal sealed class 判定_InstanceMethodか:VoidExpressionTraverser {
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ExpressionEqualityComparer"></param>
        public 判定_InstanceMethodか(ExpressionEqualityComparer ExpressionEqualityComparer){
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
        }
        internal Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant=default!;
        internal Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=default!;
        internal IEnumerable<LambdaExpression> Lambdas=default!;
        private bool Quote内か;
        private bool InstanceMethodか;
        public bool 実行(Expression e) {
            this.Quote内か=false;
            this.InstanceMethodか=false;
            this.Traverse(e);
            return this.InstanceMethodか;
        }
        protected override void Constant(ConstantExpression Constant) {
            if(this.DictionaryConstant.ContainsKey(Constant))
                this.InstanceMethodか=true;
        }

        protected override void Dynamic(DynamicExpression Dynamic){
            this.InstanceMethodか=true;
        }
        protected override void Parameter(ParameterExpression Parameter){
            if(this.Dictionaryラムダ跨ぎParameter.ContainsKey(Parameter))
                this.InstanceMethodか=true;
        }

        protected override void Quote(UnaryExpression Unary) {
            var Quote内か = this.Quote内か;
            this.Quote内か=true;
            //if(this.Dictionary_Quote_FieldBuilder.ContainsKey(Unary))
            //    this.指定Constantが存在する=true;
            //else
            //    base.Quote(Unary);
            base.Quote(Unary);
            this.Quote内か=Quote内か;
        }
        protected override void Lambda(LambdaExpression Lambda) {
            if(!this.Quote内か&&this.Lambdas.Contains(Lambda,this.ExpressionEqualityComparer))
                this.InstanceMethodか=true;
            else
                base.Lambda(Lambda);
        }
    }
}
