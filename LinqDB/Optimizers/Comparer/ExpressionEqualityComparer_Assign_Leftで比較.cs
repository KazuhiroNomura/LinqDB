using System.Collections.Generic;
using System.Linq.Expressions;
// ReSharper disable All
namespace LinqDB.Optimizers.Comparer;
/// <summary>
/// 先行評価式の(t=x)の場合tを評価するようにする
/// </summary>
internal class ExpressionEqualityComparer_Assign_Leftで比較:ExpressionEqualityComparer{
    private readonly List<ParameterExpression> スコープParameters;
    public ExpressionEqualityComparer_Assign_Leftで比較(List<ParameterExpression> スコープParameters){
        this.スコープParameters=スコープParameters;
    }
    protected override bool Equals後処理(ParameterExpression x,ParameterExpression y){
        var スコープParameters=this.スコープParameters;
        var x_Index=スコープParameters.IndexOf(x);
        var y_Index=スコープParameters.IndexOf(y);
        if(x_Index==y_Index)
            if(y_Index>=0)
                return true;
        return @false;
    }
    protected override Expression Assignの比較対象(Expression Expression0){
        if(Expression0.NodeType==ExpressionType.Assign)
            if(((BinaryExpression)Expression0).Left is ParameterExpression Parameter)
                return Parameter;
        return Expression0;
    }
    public override int GetHashCode(Expression Expression0){
        if(Expression0.NodeType==ExpressionType.Assign)
            return(int)((BinaryExpression)Expression0).Left.NodeType;
        else
            return(int)Expression0.NodeType;
    }
}
