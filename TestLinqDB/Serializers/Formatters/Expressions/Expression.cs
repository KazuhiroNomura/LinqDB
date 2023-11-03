using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Expression1 : 共通
{
    [Fact]
    public void Binary()
    {
        共通<int>(ExpressionType.Assign);
        共通<string>(ExpressionType.Coalesce);
        共通<int>(ExpressionType.Add);
        共通<int>(ExpressionType.AddChecked);
        共通<int>(ExpressionType.And);
        共通<bool>(ExpressionType.AndAlso);
        共通<int>(ExpressionType.Divide);
        共通<int>(ExpressionType.ExclusiveOr);
        共通<int>(ExpressionType.LeftShift);
        共通<int>(ExpressionType.Modulo);
        共通<int>(ExpressionType.Multiply);
        共通<int>(ExpressionType.MultiplyChecked);
        共通<int>(ExpressionType.Or);
        共通<bool>(ExpressionType.OrElse);
        共通<double>(ExpressionType.Power);
        共通<int>(ExpressionType.RightShift);
        共通<int>(ExpressionType.Subtract);
        共通<int>(ExpressionType.SubtractChecked);
        共通<int>(ExpressionType.AddAssign);
        共通<int>(ExpressionType.AddAssignChecked);
        共通<int>(ExpressionType.DivideAssign);
        共通<int>(ExpressionType.AndAssign);
        共通<int>(ExpressionType.ExclusiveOrAssign);
        共通<int>(ExpressionType.LeftShiftAssign);
        共通<int>(ExpressionType.ModuloAssign);
        共通<int>(ExpressionType.MultiplyAssign);
        共通<int>(ExpressionType.MultiplyAssignChecked);
        共通<int>(ExpressionType.OrAssign);
        共通<double>(ExpressionType.PowerAssign);
        共通<int>(ExpressionType.RightShiftAssign);
        共通<int>(ExpressionType.SubtractAssign);
        共通<int>(ExpressionType.SubtractAssignChecked);
        共通<int>(ExpressionType.Equal);
        共通<int>(ExpressionType.GreaterThan);
        共通<int>(ExpressionType.GreaterThanOrEqual);
        共通<int>(ExpressionType.LessThan);
        共通<int>(ExpressionType.LessThanOrEqual);
        共通<int>(ExpressionType.NotEqual);
        void 共通<T>(ExpressionType NodeType)
        {
            var Parameter = Expression.Parameter(typeof(T), typeof(T).Name);
            var Constant = Expression.Constant(default(T));
            Expression input = Expression.Block(
                new[] { Parameter },
                Expression.MakeBinary(NodeType, Parameter, Constant)
            );
            this.AssertEqual(input);
        }
    }
    private static 演算子 Unary演算子(演算子 a) => ~a;
    private static bool IsTrue演算子(演算子 a) => a.HasValue;
    static int UnaryDouble(double a)
    {
        return (int)a;
    }
    [Fact]
    public void Unary()
    {
        var ConstantArray = Expression.Constant(new int[10]);
        var Constant1 = Expression.Constant(1);
        var Constant1_1d = Expression.Constant(1.1);
        var ConstantTrue = Expression.Constant(true);
        var Constant演算子 = Expression.Constant(new 演算子(true));
        var Constant演算子1 = Expression.Constant(new 演算子1(true));
        var Parameter演算子 = Expression.Parameter(typeof(演算子));
        var ParameterInt32 = Expression.Parameter(typeof(int));
        var x = Expression.ArrayLength(Expression.Constant(new int[1]));
        this.ExpressionシリアライズAssertEqual(Expression.ArrayLength(Expression.Constant(new int[1])));
        共通(Expression.ArrayLength(ConstantArray));
        共通(Expression.Quote(Expression.Lambda(ConstantArray)));
        共通(Expression.Throw(Expression.New(typeof(InvalidOperationException).GetConstructor(Type.EmptyTypes)!)));
        共通(Expression.TypeAs(Expression.New(typeof(Exception).GetConstructor(Type.EmptyTypes)!), typeof(InvalidOperationException)));
        共通(Expression.Unbox(Expression.Constant(1, typeof(object)), typeof(int)));
        共通(Expression.Convert(Constant1_1d, typeof(int)));
        共通(Expression.ConvertChecked(Constant1_1d, typeof(int)));
        共通(Expression.Decrement(Constant1_1d));
        共通(Expression.Increment(Constant1_1d));
        共通(Expression.IsFalse(ConstantTrue));
        共通(Expression.IsTrue(ConstantTrue));
        共通(Expression.Negate(Constant1_1d));
        共通(Expression.NegateChecked(Constant1_1d));
        共通(Expression.Not(Constant1));
        共通(Expression.OnesComplement(Constant1));
        共通(Expression.Decrement(Constant1_1d));
        共通(Expression.Increment(Constant1_1d));
        共通(Expression.PostDecrementAssign(ParameterInt32));
        共通(Expression.PostIncrementAssign(ParameterInt32));
        共通(Expression.PreDecrementAssign(ParameterInt32));
        共通(Expression.PreIncrementAssign(ParameterInt32));
        共通(Expression.UnaryPlus(Constant1_1d));

        共通(Expression.Convert(Constant演算子, typeof(演算子1)));
        共通(Expression.ConvertChecked(Constant演算子, typeof(演算子1)));
        共通(Expression.Convert(Constant演算子1, typeof(演算子)));
        共通(Expression.ConvertChecked(Constant演算子1, typeof(演算子)));
        共通(Expression.Decrement(Constant演算子));
        共通(Expression.Increment(Constant演算子));
        共通(Expression.IsFalse(Constant演算子));
        共通(Expression.IsTrue(Constant演算子));
        共通(Expression.Negate(Constant演算子));
        共通(Expression.NegateChecked(Constant演算子));
        共通(Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        共通(Expression.Decrement(Constant演算子));
        共通(Expression.Increment(Constant演算子));
        共通(Expression.PostDecrementAssign(Parameter演算子));
        共通(Expression.PostIncrementAssign(Parameter演算子));
        共通(Expression.PreDecrementAssign(Parameter演算子));
        共通(Expression.PreIncrementAssign(Parameter演算子));
        共通(Expression.UnaryPlus(Constant演算子));


        共通(Expression.Convert(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        共通(Expression.ConvertChecked(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        共通(Expression.Decrement(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        共通(Expression.Increment(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        共通(Expression.IsFalse(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        共通(Expression.IsTrue(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        共通(Expression.Negate(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        共通(Expression.NegateChecked(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        共通(Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        共通(Expression.Decrement(Constant演算子, GetMethod(nameof(Unary演算子))));
        共通(Expression.Increment(Constant演算子, GetMethod(nameof(Unary演算子))));
        共通(Expression.UnaryPlus(Constant演算子, GetMethod(nameof(Unary演算子))));
        void 共通(UnaryExpression Unary) => this.ExpressionシリアライズAssertEqual(
            Expression.Block(
                new[] { Parameter演算子, ParameterInt32 },
                Unary
            )
        );
    }
}
