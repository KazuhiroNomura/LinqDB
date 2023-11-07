using System.Collections.Generic;
using System.Linq.Expressions;






// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.VoidExpressionTraverser;


internal sealed class 判定_指定Parameter無_他Parameter有:VoidExpressionTraverser_Quoteを処理しない {
    private readonly List<ParameterExpression> ListParameter = new();
    private bool 指定Parametersが存在せず;
    private bool 他Parametersが存在する;
    public bool 実行(Expression e,ParameterExpression 指定Parameter) {
        var ListParameter = this.ListParameter;
        ListParameter.Clear();
        ListParameter.Add(指定Parameter);
        this.指定Parametersが存在せず=true;
        this.他Parametersが存在する=false;
        this.Traverse(e);
        return this.指定Parametersが存在せず&&this.他Parametersが存在する;
    }
    protected override void Lambda(LambdaExpression Lambda) {
        var Lambda_Parameters = Lambda.Parameters;
        var ListParameter = this.ListParameter;
        var ListParameter_Count = ListParameter.Count;
        ListParameter.AddRange(Lambda_Parameters);
        this.Traverse(Lambda.Body);
        ListParameter.RemoveRange(ListParameter_Count,Lambda_Parameters.Count);
    }
    protected override void Parameter(ParameterExpression Parameter) {
        if(this.ListParameter.Contains(Parameter)) {
            this.指定Parametersが存在せず=false;
        } else {
            this.他Parametersが存在する=true;
        }
    }
}
