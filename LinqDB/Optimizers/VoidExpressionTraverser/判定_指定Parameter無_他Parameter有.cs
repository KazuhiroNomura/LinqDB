using System.Collections.Generic;
using System.Linq.Expressions;






// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.VoidExpressionTraverser;


internal sealed class 判定_指定Parameter無_他Parameter有:VoidExpressionTraverser_Quoteを処理しない {
    private readonly List<ParameterExpression> Parameters = new();
    private bool 指定Parameters無;
    private bool 他Parameters有;
    public bool 実行(Expression e,IEnumerable<ParameterExpression> 指定Parameters) {
        var Parameters = this.Parameters;
        Parameters.Clear();
        Parameters.AddRange(指定Parameters);
        this.指定Parameters無=true;
        this.他Parameters有=false;
        this.Traverse(e);
        return this.指定Parameters無&&this.他Parameters有;
    }
    protected override void Lambda(LambdaExpression Lambda) {
        var Lambda_Parameters = Lambda.Parameters;
        var Parameters = this.Parameters;
        var ListParameter_Count = Parameters.Count;
        Parameters.AddRange(Lambda_Parameters);
        this.Traverse(Lambda.Body);
        Parameters.RemoveRange(ListParameter_Count,Lambda_Parameters.Count);
    }
    protected override void Parameter(ParameterExpression Parameter) {
        if(this.Parameters.Contains(Parameter)) {
            this.指定Parameters無=false;
        } else {
            this.他Parameters有=true;
        }
    }
}
