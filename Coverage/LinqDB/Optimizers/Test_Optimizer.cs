using System.Collections;
using System.Linq.Expressions;
using LinqDB.Optimizers;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq;
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_Optimizer : ATest
{
    //[NonSerialized]
    //private readonly global::Lite.Optimizers.Optimizer Optimizer = new global::Lite.Optimizers.Optimizer();
    //public TResult Execute<TResult>(Expression<Func<TResult>> Lambda) =>
    //    this.Optimizer.Execute(
    //        Lambda
    //    );
    //public void AssertExecute<TResult>(Expression<Func<TResult>> Lambda) =>
    //    this.Optimizer.AssertExecute(
    //        Lambda
    //    );
    [TestMethod]
    public void 必要ならConvert()
    {
        this.Execute2(() => new Set<double> { 1 }.Average(p => p + 1));
    }
    [TestMethod]
    public void ValueTupleでNewする()
    {
        //switch(残りType数) {
        //    case 1:
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1, k2 = o + 2, k3 = o + 3, k4 = o + 4, k5 = o + 5, k6 = o + 6, k7 = o + 7 },
                i => new { k0 = i + 0, k1 = i + 1, k2 = i + 2, k3 = i + 3, k4 = i + 4, k5 = i + 5, k6 = i + 6, k7 = i + 7 },
                (o, i) => new { o, i }
            )
        );
        //    case 2:
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1 },
                i => new { k0 = i + 0, k1 = i + 1 },
                (o, i) => new { o, i }
            )
        );
        //    case 3:
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1, k2 = o + 2 },
                i => new { k0 = i + 0, k1 = i + 1, k2 = i + 2 },
                (o, i) => new { o, i }
            )
        );
        //    case 4:
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1, k2 = o + 2, k3 = o + 3 },
                i => new { k0 = i + 0, k1 = i + 1, k2 = i + 2, k3 = i + 3 },
                (o, i) => new { o, i }
            )
        );
        //    case 5:
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1, k2 = o + 2, k3 = o + 3, k4 = o + 4 },
                i => new { k0 = i + 0, k1 = i + 1, k2 = i + 2, k3 = i + 3, k4 = i + 4 },
                (o, i) => new { o, i }
            )
        );
        //    case 6:
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1, k2 = o + 2, k3 = o + 3, k4 = o + 4, k5 = o + 5 },
                i => new { k0 = i + 0, k1 = i + 1, k2 = i + 2, k3 = i + 3, k4 = i + 4, k5 = i + 5 },
                (o, i) => new { o, i }
            )
        );
        //    case 7:
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1, k2 = o + 2, k3 = o + 3, k4 = o + 4, k5 = o + 5, k6 = o + 6 },
                i => new { k0 = i + 0, k1 = i + 1, k2 = i + 2, k3 = i + 3, k4 = i + 4, k5 = i + 5, k6 = i + 6 },
                (o, i) => new { o, i }
            )
        );
        //    default: {
        this.Execute2(() =>
            new Set<int>().Join(
                new Set<int>(),
                o => new { k0 = o + 0, k1 = o + 1, k2 = o + 2, k3 = o + 3, k4 = o + 4, k5 = o + 5, k6 = o + 6, k7 = o + 7, k8 = o + 8 },
                i => new { k0 = i + 0, k1 = i + 1, k2 = i + 2, k3 = i + 3, k4 = i + 4, k5 = i + 5, k6 = i + 6, k7 = i + 7, k8 = i + 8 },
                (o, i) => new { o, i }
            )
        );
        //    }
        //}
    }
    [Serializable]
    public struct Value<T> where T : struct
    {
        private readonly T value;
        public Value(T value) => this.value = value;
        public static implicit operator Value<T>(T value) => new(value);
        public static implicit operator T(Value<T> value) => value.value;
    }
    [TestMethod]
    public void IsNullable()
    {
        //Type.IsGenericType&&Type.GetGenericTypeDefinition()==typeof(Nullable<>);
        var a = new Value<int>(1);
        int? b = 2;
        this.Execute2(() => a + a);
        this.Execute2(() => b + b);
    }
    [TestMethod]
    public void Is定数をILで直接埋め込めるTypeか()
    {
        //Type.IsPrimitive||Type.IsEnum||Type==typeof(String);
        this.Execute2(() => 1);
        this.Execute2(() => EEnum.A);
        this.Execute2(() => "ABC");
        this.Execute2(() => 2m);
    }
    [TestMethod]
    public void EnumerableSetループ展開可能なGenericMethodDefinitionか0(){
        //if(
        //    Reflection.ExtendSet.Insert==GenericMethodDefinition||
        this.Execute2(() => new Set<int> { 1 }.Insert(new Set<int> { 2 }));
        //    Reflection.ExtendEnumerable.OrderBy==GenericMethodDefinition||
        this.Execute2(() => new[] { 1 }.OrderBy(p => p + 1));
        //    Reflection.ExtendEnumerable.OrderBy_comparer==GenericMethodDefinition||
        this.Execute2(() => new[] { 1 }.OrderBy(p => p + 1, Comparer<int>.Default));
        //    Reflection.ExtendEnumerable.OrderByDescending==GenericMethodDefinition||
        this.Execute2(() => new[] { 1 }.OrderByDescending(p => p + 1));
        //    Reflection.ExtendEnumerable.OrderByDescending_comparer==GenericMethodDefinition||
        this.Execute2(() => new[] { 1 }.OrderByDescending(p => p + 1, Comparer<int>.Default));
        //    Reflection.ExtendEnumerable.ThenBy==GenericMethodDefinition||
        this.Execute2(() => new[] { 1 }.OrderBy(p => p + 1).ThenBy(p => p + 2));
        //    Reflection.ExtendEnumerable.ThenBy_comparer==GenericMethodDefinition||
        this.Execute2(() => new[] { 1 }.OrderBy(p => p + 1).ThenBy(p => p + 2, Comparer<int>.Default));
        //    Reflection.ExtendEnumerable.ThenByDescending==GenericMethodDefinition||
        this.Execute2(() => new[] { 1 }.OrderBy(p => p + 1).ThenByDescending(p => p + 2));
        //    Reflection.ExtendEnumerable.ThenByDescending_comparer==GenericMethodDefinition
        this.Execute2(() => new[] { 1 }.OrderBy(p => p + 1).ThenByDescending(p => p + 2, Comparer<int>.Default));
        //)
    }
    [TestMethod]
    public void EnumerableSetループ展開可能なGenericMethodDefinitionか1(){
        //    DeclaringType==typeof(InternalEnumerable)||
        this.Execute2(() => new[] { 1 }.Join(new[] { 1 }, o => o, i => i, (o, i) => new { o, i }));
        //    DeclaringType==typeof(Enumerable)||
        this.Execute2(() => new[] { 1 }.Select(p => p + 1));
        //    DeclaringType==typeof(InternalSet)||
        this.Execute2(() => new Set<int> { 1 }.Join(new Set<int> { 1 }, o => o, i => i, (o, i) => new { o, i }));
        //    DeclaringType==typeof(Set);
        this.Execute2(() => new Set<int> { 1 }.Select(p => p + 1));
    }
    [TestMethod]
    public void LambdaExpressionを展開1()
    {
        //if(Lambda is LambdaExpression Lambda1) {
        this.Execute2(() => new Set<int> { 1 }.Select(p => p + 1));
        //}
        // ReSharper disable once RedundantCast
        this.Execute2(() => new Set<int> { 1 }.Select((Func<int, int>)(p => p + 1)));
    }
    [TestMethod]
    public void Test_ExpressionEqualityComparer()
    {
        Optimizer.Test_ExpressionEqualityComparer(
            Expression.Constant(2),
            Expression.Constant(2)
        );
    }
    [TestMethod]
    public void 祖先のSet1を取得()
    {
        Optimizer.Test_ExpressionEqualityComparer(
            Expression.Constant(2),
            Expression.Constant(2)
        );
    }
    private class EnumerableClass : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            for (var a = 0; a < 100; a++)
                yield return a;
        }
    }
    [TestMethod]
    public void TypeからIEnumerable1を取得()
    {
        Optimizer.Test_ExpressionEqualityComparer(
            Expression.Constant(2),
            Expression.Constant(2)
        );
        //if(Type.IsInterface&&Type.IsGenericType&&Type.GetGenericTypeDefinition()==typeof(IEnumerable<>))
        {
            IEnumerable<int> IEnumerable = new[] { 1 };
            this.Execute標準ラムダループ(
                () => IEnumerable.ToArray());
        }
        //if(Interface==null)
        {
            IEnumerable IEnumerable = new EnumerableClass();
            this.Execute標準ラムダループ(
                () => IEnumerable.OfType<int>());
        }
        //Contract.Assert(typeof(IEnumerable<>)==Interface.GetGenericTypeDefinition());
        //return Type.GetInterface(IEnumerable1FullName);
    }
    [TestMethod]
    public void GetGenericMethodDefinition()
    {
        IEnumerable<int> IEnumerable = new[] { 1 };
        //    ? Method.GetGenericMethodDefinition()
        this.Execute2(() => IEnumerable.ToArray());
        //    : Method;
        this.Execute2(() => IEnumerable.ToString());
    }
    [TestMethod]
    public void AndAlsoで繋げる()
    {
        this.Execute2(() => KeySet変数().Where(p => p.ID1 == 1 && p.ID3 == -1));
    }
    [TestMethod]
    public void ループ展開可能なEnumerableかSetのGenericMethodDefinitionか()
    {
        this.Execute引数パターン(a => ArrN<int>(a).OrderByDescending(p => p));
        this.Execute引数パターン(a => SetN<int>(a + 1).Except(SetN<int>(a)).SingleOrDefault());
    }
}