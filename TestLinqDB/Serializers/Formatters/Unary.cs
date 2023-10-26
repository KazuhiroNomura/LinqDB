
//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.Formatters;
using Expressions = System.Linq.Expressions;
public class Unary : 共通
{
    private static 演算子 Unary演算子(演算子 a) => ~a;
    private static bool IsTrue演算子(演算子 a) => a.HasValue;
    static int UnaryDouble(double a)
    {
        return (int)a;
    }
    [Fact]
    public void Serialize(){
        this.MemoryMessageJson_Expression_Assert全パターン(default(Expressions.UnaryExpression));
        var ConstantArray = Expressions.Expression.Constant(new int[10]);
        var Constant1 = Expressions.Expression.Constant(1);
        var Constant1_1d = Expressions.Expression.Constant(1.1);
        var ConstantTrue = Expressions.Expression.Constant(true);
        var Constant演算子 = Expressions.Expression.Constant(new 演算子(true));
        var Constant演算子1 = Expressions.Expression.Constant(new 演算子1(true));
        var Parameter演算子 = Expressions.Expression.Parameter(typeof(演算子));
        var ParameterInt32 = Expressions.Expression.Parameter(typeof(int));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.ArrayLength(Expressions.Expression.Constant(new int[1])));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.ArrayLength(ConstantArray));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Quote(Expressions.Expression.Lambda(ConstantArray)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Throw(Expressions.Expression.New(typeof(InvalidOperationException).GetConstructor(Type.EmptyTypes)!)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.TypeAs(Expressions.Expression.New(typeof(Exception).GetConstructor(Type.EmptyTypes)!), typeof(InvalidOperationException)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Unbox(Expressions.Expression.Constant(1, typeof(object)), typeof(int)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Convert(Constant1_1d, typeof(int)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.ConvertChecked(Constant1_1d, typeof(int)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Decrement(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Increment(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.IsFalse(ConstantTrue));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.IsTrue(ConstantTrue));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Negate(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.NegateChecked(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Not(Constant1));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.OnesComplement(Constant1));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Decrement(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Increment(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PostDecrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PostIncrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PreDecrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PreIncrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.UnaryPlus(Constant1_1d));

        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Convert(Constant演算子, typeof(演算子1)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.ConvertChecked(Constant演算子, typeof(演算子1)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Convert(Constant演算子1, typeof(演算子)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.ConvertChecked(Constant演算子1, typeof(演算子)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Decrement(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Increment(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.IsFalse(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.IsTrue(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Negate(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.NegateChecked(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Decrement(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Increment(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PostDecrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PostIncrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PreDecrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.PreIncrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.UnaryPlus(Constant演算子));


        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Convert(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.ConvertChecked(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Decrement(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Increment(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.IsFalse(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.IsTrue(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Negate(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.NegateChecked(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Decrement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Increment(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.UnaryPlus(Constant演算子, GetMethod(nameof(Unary演算子))));
    }
}
