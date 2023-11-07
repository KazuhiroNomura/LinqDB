using System.Linq.Expressions;
// ReSharper disable All
namespace LinqDB.Optimizers.Comparison;
/// <summary>
/// 先行評価式の(t=x)の場合tを評価するようにする
/// </summary>
internal class ExpressionEqualityComparer_Assign_Leftで比較 : ExpressionEqualityComparer
{
    protected override Expression Assignの比較対象(Expression Expression0)
    {
        if (Expression0.NodeType==ExpressionType.Assign)
        {
        }
        return Expression0.NodeType==ExpressionType.Assign&&
               ((BinaryExpression)Expression0).Left is ParameterExpression Parameter
                ? Parameter
                : Expression0;
    }
    public override int GetHashCode(Expression e) =>
        (int)(e.NodeType==ExpressionType.Assign
            ? ((BinaryExpression)e).Left
            : e).NodeType;
}
