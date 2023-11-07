using System.Collections.Generic;
using System.Linq.Expressions;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.VoidExpressionTraverser;

internal sealed class 判定_指定Parameter有_他Parameter無_Lambda内部走査:VoidExpressionTraverser_Quoteを処理しない {
    private readonly List<ParameterExpression> 全Parameters = new();
    private bool 指定Parameters有, 他Parameter無;
    public bool 実行(Expression e,ParameterExpression 指定Parameter) {
        var 全Parameters = this.全Parameters;
        全Parameters.Clear();
        全Parameters.Add(指定Parameter);
        this.他Parameter無=true;
        this.指定Parameters有=false;
        this.Traverse(e);
        return this.指定Parameters有&&this.他Parameter無;
    }
    protected override void Lambda(LambdaExpression Lambda) {
        var 全Parameters = this.全Parameters;
        var 全Parameters_Count = 全Parameters.Count;
        var e0_Parameters = Lambda.Parameters;
        全Parameters.AddRange(e0_Parameters);
        this.Traverse(Lambda.Body);
        全Parameters.RemoveRange(全Parameters_Count,e0_Parameters.Count);
    }
    protected override void Parameter(ParameterExpression Parameter) {
        if(this.全Parameters.Contains(Parameter)) {
            this.指定Parameters有=true;
        } else {
            this.他Parameter無=false;
        }
    }
}
