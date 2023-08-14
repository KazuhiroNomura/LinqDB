using System.Collections.Generic;
using System.Linq.Expressions;
namespace LinqDB.Optimizers;

partial class Optimizer {
    private sealed class 変換_ラムダ跨ぎParameterをMemberに:ReturnExpressionTraverser {
        /// <summary>
        /// Cラムダ跨ぎ変数をTarget.v00,.v01,.v02というTargetのフィールドに変換する
        /// </summary>
        private readonly IReadOnlyDictionary<Expression, MemberExpression> Dictionary_Parameter_Member;
        public 変換_ラムダ跨ぎParameterをMemberに(作業配列 作業配列,IReadOnlyDictionary<Expression, MemberExpression> Dictionary_Parameter_Member) : base(作業配列)=>
            this.Dictionary_Parameter_Member=Dictionary_Parameter_Member;
        public Expression 実行(Expression e)=>
            this.Traverse(e);
        protected override Expression Traverse(Expression Expression0){
            if(this.Dictionary_Parameter_Member.TryGetValue(Expression0, out var Expression1))return Expression1;
            return base.Traverse(Expression0);
        }
    }
}
