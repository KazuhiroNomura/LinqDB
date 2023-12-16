//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;

namespace TestLinqDB.Serializers.Formatters.Expressions;
using System.Linq.Expressions;
public class Unary : 共通
{
    private static 演算子 Unary演算子(演算子 a) => ~a;
    private static bool IsTrue演算子(演算子 a) => a.HasValue;
    static int UnaryDouble(double a) => (int)a;
    [Fact]
    public void Serialize()
    {
        var ConstantArray = Expression.Constant(new int[10]);
        var Constant1 = Expression.Constant(1);
        var Constant1_1d = Expression.Constant(1.1);
        var ConstantTrue = Expression.Constant(true);
        var Constant演算子 = Expression.Constant(new 演算子(true));
        var Constant演算子1 = Expression.Constant(new 演算子1(true));
        var Parameter演算子 = Expression.Parameter(typeof(演算子));
        var ParameterInt32 = Expression.Parameter(typeof(int));
        this.ExpressionシリアライズAssertEqual(Expression.ArrayLength(Expression.Constant(new int[1])));
        this.ExpressionシリアライズAssertEqual(Expression.ArrayLength(ConstantArray));
        this.ExpressionシリアライズAssertEqual(Expression.Quote(Expression.Lambda(ConstantArray)));
        this.ExpressionシリアライズAssertEqual(Expression.Throw(Expression.New(typeof(InvalidOperationException).GetConstructor(Type.EmptyTypes)!)));
        this.ExpressionシリアライズAssertEqual(Expression.TypeAs(Expression.New(typeof(Exception).GetConstructor(Type.EmptyTypes)!), typeof(InvalidOperationException)));
        this.ExpressionシリアライズAssertEqual(Expression.Unbox(Expression.Constant(1, typeof(object)), typeof(int)));
        this.ExpressionシリアライズAssertEqual(Expression.Convert(Constant1_1d, typeof(int)));
        this.ExpressionシリアライズAssertEqual(Expression.ConvertChecked(Constant1_1d, typeof(int)));
        this.ExpressionシリアライズAssertEqual(Expression.Decrement(Constant1_1d));
        this.ExpressionシリアライズAssertEqual(Expression.Increment(Constant1_1d));
        this.ExpressionシリアライズAssertEqual(Expression.IsFalse(ConstantTrue));
        this.ExpressionシリアライズAssertEqual(Expression.IsTrue(ConstantTrue));
        this.ExpressionシリアライズAssertEqual(Expression.Negate(Constant1_1d));
        this.ExpressionシリアライズAssertEqual(Expression.NegateChecked(Constant1_1d));
        this.ExpressionシリアライズAssertEqual(Expression.Not(Constant1));
        this.ExpressionシリアライズAssertEqual(Expression.OnesComplement(Constant1));
        this.ExpressionシリアライズAssertEqual(Expression.Decrement(Constant1_1d));
        this.ExpressionシリアライズAssertEqual(Expression.Increment(Constant1_1d));
        this.ExpressionシリアライズAssertEqual(Expression.PostDecrementAssign(ParameterInt32));
        this.ExpressionシリアライズAssertEqual(Expression.PostIncrementAssign(ParameterInt32));
        this.ExpressionシリアライズAssertEqual(Expression.PreDecrementAssign(ParameterInt32));
        this.ExpressionシリアライズAssertEqual(Expression.PreIncrementAssign(ParameterInt32));
        this.ExpressionシリアライズAssertEqual(Expression.UnaryPlus(Constant1_1d));

        this.ExpressionシリアライズAssertEqual(Expression.Convert(Constant演算子, typeof(演算子1)));
        this.ExpressionシリアライズAssertEqual(Expression.ConvertChecked(Constant演算子, typeof(演算子1)));
        this.ExpressionシリアライズAssertEqual(Expression.Convert(Constant演算子1, typeof(演算子)));
        this.ExpressionシリアライズAssertEqual(Expression.ConvertChecked(Constant演算子1, typeof(演算子)));
        this.ExpressionシリアライズAssertEqual(Expression.Decrement(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.Increment(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.IsFalse(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.IsTrue(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.Negate(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.NegateChecked(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.ExpressionシリアライズAssertEqual(Expression.Decrement(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.Increment(Constant演算子));
        this.ExpressionシリアライズAssertEqual(Expression.PostDecrementAssign(Parameter演算子));
        this.ExpressionシリアライズAssertEqual(Expression.PostIncrementAssign(Parameter演算子));
        this.ExpressionシリアライズAssertEqual(Expression.PreDecrementAssign(Parameter演算子));
        this.ExpressionシリアライズAssertEqual(Expression.PreIncrementAssign(Parameter演算子));
        this.ExpressionシリアライズAssertEqual(Expression.UnaryPlus(Constant演算子));


        this.ExpressionシリアライズAssertEqual(Expression.Convert(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        this.ExpressionシリアライズAssertEqual(Expression.ConvertChecked(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        this.ExpressionシリアライズAssertEqual(Expression.Decrement(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.ExpressionシリアライズAssertEqual(Expression.Increment(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.ExpressionシリアライズAssertEqual(Expression.IsFalse(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        this.ExpressionシリアライズAssertEqual(Expression.IsTrue(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        this.ExpressionシリアライズAssertEqual(Expression.Negate(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.ExpressionシリアライズAssertEqual(Expression.NegateChecked(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.ExpressionシリアライズAssertEqual(Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.ExpressionシリアライズAssertEqual(Expression.Decrement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.ExpressionシリアライズAssertEqual(Expression.Increment(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.ExpressionシリアライズAssertEqual(Expression.UnaryPlus(Constant演算子, GetMethod(nameof(Unary演算子))));
    }
}
