using System.Linq.Expressions;
using System.Diagnostics;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.ReturnExpressionTraverser;

public class 変換_旧Parameterを新Expression1:ReturnExpressionTraverser {
    private ParameterExpression? 旧Parameter;
    private Expression? 新Expression;
    public 変換_旧Parameterを新Expression1(作業配列 作業配列) : base(作業配列){}
    public Expression 実行(Expression Expression0,ParameterExpression 旧Parameter,Expression 新Expression) {
        Debug.Assert(旧Parameter.Type.IsAssignableFrom(新Expression.Type));
        this.旧Parameter=旧Parameter;
        this.新Expression=新Expression;
        return this.Traverse(Expression0);
    }
    protected override Expression Parameter(ParameterExpression Parameter0){
        if(Parameter0==this.旧Parameter!)
            return this.新Expression!;
        return Parameter0;
    }
}
