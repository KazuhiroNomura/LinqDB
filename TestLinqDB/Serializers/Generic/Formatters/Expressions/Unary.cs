
//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
using System.Linq.Expressions;
public abstract class Unary<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Unary():base(new AssertDefinition(new TSerializer())){}
    private static 演算子 Unary演算子(演算子 a) => ~a;
    private static bool IsTrue演算子(演算子 a) => a.HasValue;
    static int UnaryDouble(double a)
    {
        return (int)a;
    }
    [Fact]
    public void Serialize(){
        this.MemoryMessageJson_Expression_Assert全パターン(default(UnaryExpression));
        var ConstantArray = Expression.Constant(new int[10]);
        var Constant1 = Expression.Constant(1);
        var Constant1_1d = Expression.Constant(1.1);
        var ConstantTrue = Expression.Constant(true);
        var Constant演算子 = Expression.Constant(new 演算子(true));
        var Constant演算子1 = Expression.Constant(new 演算子1(true));
        var Parameter演算子 = Expression.Parameter(typeof(演算子));
        var ParameterInt32 = Expression.Parameter(typeof(int));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.ArrayLength(Expression.Constant(new int[1])));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.ArrayLength(ConstantArray));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Quote(Expression.Lambda(ConstantArray)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Throw(Expression.New(typeof(InvalidOperationException).GetConstructor(Type.EmptyTypes)!)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.TypeAs(Expression.New(typeof(Exception).GetConstructor(Type.EmptyTypes)!), typeof(InvalidOperationException)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Unbox(Expression.Constant(1, typeof(object)), typeof(int)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Convert(Constant1_1d, typeof(int)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.ConvertChecked(Constant1_1d, typeof(int)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Decrement(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Increment(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.IsFalse(ConstantTrue));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.IsTrue(ConstantTrue));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Negate(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.NegateChecked(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Not(Constant1));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.OnesComplement(Constant1));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Decrement(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Increment(Constant1_1d));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PostDecrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PostIncrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PreDecrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PreIncrementAssign(ParameterInt32));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.UnaryPlus(Constant1_1d));

        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Convert(Constant演算子, typeof(演算子1)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.ConvertChecked(Constant演算子, typeof(演算子1)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Convert(Constant演算子1, typeof(演算子)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.ConvertChecked(Constant演算子1, typeof(演算子)));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Decrement(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Increment(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.IsFalse(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.IsTrue(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Negate(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.NegateChecked(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Decrement(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Increment(Constant演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PostDecrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PostIncrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PreDecrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.PreIncrementAssign(Parameter演算子));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.UnaryPlus(Constant演算子));


        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Convert(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.ConvertChecked(Constant1_1d, typeof(int), GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Decrement(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Increment(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.IsFalse(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.IsTrue(Constant演算子, GetMethod(nameof(IsTrue演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Negate(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.NegateChecked(Constant1_1d, GetMethod(() => UnaryDouble(0))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.OnesComplement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Decrement(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Increment(Constant演算子, GetMethod(nameof(Unary演算子))));
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.UnaryPlus(Constant演算子, GetMethod(nameof(Unary演算子))));
    }
}
