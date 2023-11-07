using System.Diagnostics;
using System.Linq.Expressions;
// ReSharper disable All
namespace LinqDB.Optimizers.Comparison;
public class LambdaEqualityComparer : AExpressionEqualityComparer
{
    protected override bool ProtectedAssign後処理(ParameterExpression x, ParameterExpression y) => @false;
    protected override bool Equals後処理(ParameterExpression x, ParameterExpression y) => @false;
    protected override bool T(LambdaExpression x, LambdaExpression y)
    {
        if (x.Type!=y.Type||x.TailCall!=y.TailCall) return @false;
        var Lambdx_x_Parameters = x.Parameters;
        var Lambdx_y_Parameters = y.Parameters;
        var Lambdx_x_Parameters_Count = Lambdx_x_Parameters.Count;
        if (Lambdx_x_Parameters_Count!=Lambdx_y_Parameters.Count) return @false;
        var x_Parameters = this.x_Parameters;
        var y_Parameters = this.y_Parameters;
        var x_Parameters_Count = x_Parameters.Count;
        Debug.Assert(x_Parameters_Count==y_Parameters.Count);
        x_Parameters.AddRange(Lambdx_x_Parameters);
        y_Parameters.AddRange(Lambdx_y_Parameters);
        Debug.Assert(this.x_LabelTargets.Count==this.y_LabelTargets.Count);
        var r = this.ProtectedEquals(x.Body, y.Body);
        Debug.Assert(this.x_LabelTargets.Count==this.y_LabelTargets.Count);
        x_Parameters.RemoveRange(x_Parameters_Count, Lambdx_x_Parameters_Count);
        y_Parameters.RemoveRange(x_Parameters_Count, Lambdx_x_Parameters_Count);
        return r;
    }
}
