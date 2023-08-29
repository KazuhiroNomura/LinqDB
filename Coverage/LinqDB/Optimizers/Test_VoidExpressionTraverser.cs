using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_VoidExpressionTraverser : ATest
{

    [TestMethod] public void Add() => this.実行結果が一致するか確認(() => _Int32 + _Int32);
    [TestMethod] public void AddChecked() => this.実行結果が一致するか確認(() => checked(_Int32 + _Int32));
    [TestMethod] public void Subtract() => this.実行結果が一致するか確認(() => _Int32 - _Int32);
    [TestMethod] public void SubtractChecked() => this.実行結果が一致するか確認(() => checked(_Int32 - _Int32));
    [TestMethod] public void Multiply() => this.実行結果が一致するか確認(() => _Int32 * _Int32);
    [TestMethod] public void MultiplyChecked() => this.実行結果が一致するか確認(() => checked(_Int32 * _Int32));
    [TestMethod] public void Divide() => this.実行結果が一致するか確認(() => _Int32 / _Int32);
    [TestMethod] public void Modulo() => this.実行結果が一致するか確認(() => _Int32 % _Int32);
    [TestMethod] public void And() => this.実行結果が一致するか確認(() => _Boolean & _Boolean);
    [TestMethod] public void Or() => this.実行結果が一致するか確認(() => _Boolean | _Boolean);
    [TestMethod] public void ExclusiveOr() => this.実行結果が一致するか確認(() => _Boolean ^ _Boolean);
    [TestMethod] public void AndAlso() => this.実行結果が一致するか確認(() => _Boolean && _Boolean);
    [TestMethod] public void OrElse() => this.実行結果が一致するか確認(() => _Boolean || _Boolean);
    [TestMethod] public void Equal() => this.実行結果が一致するか確認(() => _Int32 == 3);
    [TestMethod] public void NotEqual() => this.実行結果が一致するか確認(() => _Int32 != 3);
    [TestMethod] public void GreaterThan() => this.実行結果が一致するか確認(() => _Int32 > 3);
    [TestMethod] public void GreaterThanOrEqual() => this.実行結果が一致するか確認(() => _Int32 >= 3);
    [TestMethod] public void LessThan() => this.実行結果が一致するか確認(() => _Int32 < 3);
    [TestMethod] public void LessThanOrEqual() => this.実行結果が一致するか確認(() => _Int32 <= 3);
    [TestMethod] public void LeftShift() => this.実行結果が一致するか確認(() => _Int32 << 3);
    [TestMethod] public void RightShift() => this.実行結果が一致するか確認(() => _Int32 >> 3);
    [TestMethod] public void ArrayIndex() => this.実行結果が一致するか確認(() => Array[0]);
    [TestMethod] public void ArrayLength() => this.実行結果が一致するか確認(() => Array.Length);
    [TestMethod] public void Convert() => this.実行結果が一致するか確認(() => (double)_Int32 + (double)_Int32);
    [TestMethod] public void ConvertChecked() => this.実行結果が一致するか確認(() => (double)_Int32 + (double)_Int32);
    //[TestMethod] public void Increment         ()=>this._変数Cache.AssertExecute(()=>_Int32+1);
    //[TestMethod] public void Decrement         ()=>this._変数Cache.AssertExecute(()=>_Int32-1);
    //[TestMethod] public void IsFalse           ()=>this._変数Cache.AssertExecute(()=>class_演算子オーバーロード2--);
    //[TestMethod] public void IsTrue            ()=>this._変数Cache.AssertExecute(()=>class_演算子オーバーロード2++);
    [TestMethod] public void Negate() => this.実行結果が一致するか確認(() => -_Int32 + -_Int32);
    [TestMethod] public void NegateChecked() => this.実行結果が一致するか確認(() => checked(-_Int32) + checked(-_Int32));
    [TestMethod] public void Not() => this.実行結果が一致するか確認(() => !_Boolean && !_Boolean);
    [TestMethod] public void OnesComplement() => this.実行結果が一致するか確認(() => ~_Int32 + ~_Int32);
    [TestMethod] public void TypeAs() => this.実行結果が一致するか確認(() => (Object_String as string) + (Object_String as string));
    [TestMethod] public void UnaryPlus() => this.実行結果が一致するか確認(() => +変数_普通ではサポートされないな演算子 - +変数_普通ではサポートされないな演算子);
    [TestMethod] public void Unbox() => this.実行結果が一致するか確認(() => (int)Object_Int32 + (int)Object_Int32);
    //Block
    [TestMethod] public void Conditional() => this.実行結果が一致するか確認(() => (_Boolean ? _Int32 : 10) + (_Boolean ? _Int32 : 10));
    [TestMethod] public void Constant() => this.実行結果が一致するか確認(() => _List[1] + _List[1]);
    //DebugInfo
    //Default
    //Dynamic
    //Goto
    [TestMethod]
    public void Index()
    {
        var Array1 = new[] { 1, 2, 3 };
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.ArrayAccess(
                    Expression.Constant(Array1),
                    Expression.Constant(1)
                )
            )
        );
    }
    [TestMethod] public void Invoke() => this.実行結果が一致するか確認(() => _Delegate(4) + _Delegate(4));
    //Label
    [TestMethod] public void Lambda() => this.実行結果が一致するか確認(() => Lambda(p => p * 2) + Lambda(p => p * 2));
    [TestMethod] public void ListInit() => this.実行結果が一致するか確認(() => new { a = new List<int> { 1, 2 }, b = new List<int> { 1, 2 } });
    //Loop
    [TestMethod] public void MemberAccess() => this.実行結果が一致するか確認(() => 変数_普通ではサポートされないな演算子.メンバー + 変数_普通ではサポートされないな演算子.メンバー);
    [TestMethod] public void MemberInit() => this.実行結果が一致するか確認(() => new { a = new 普通ではサポートされないな演算子 { メンバー = 3 }, b = new 普通ではサポートされないな演算子 { メンバー = 3 } });
    [TestMethod] public void Call() => this.実行結果が一致するか確認(() => Function() + Function());
    [TestMethod] public void NewArrayBounds() => this.実行結果が一致するか確認(() => new { a = new int[10], b = new int[10] });
    [TestMethod] public void NewArrayInit() => this.実行結果が一致するか確認(() => new { a = new[] { 1, 2, 3 }, b = new[] { 1, 2, 3 } });
    [TestMethod] public void New() => this.実行結果が一致するか確認(() => new { a = new 普通ではサポートされないな演算子(), b = new 普通ではサポートされないな演算子() });
    [TestMethod] public void Calesce() => this.実行結果が一致するか確認(() => (_NullableInt32 ?? 4) + (_NullableInt32 ?? 4));
    //共通部分式でParameterは最速なので先行評価しないため、カバレッジが出来方法が思いつかない。
    [TestMethod] public void Parameter() => this.実行結果が一致するか確認(() => Lambda(p => p * p));
    //Switch
    //Try
    //実際どんな式か分からない。
    [TestMethod] [SuppressMessage("ReSharper", "OperatorIsCanBeUsed")] public void TypeEqual() => this.実行結果が一致するか確認(() => Object_Int32.GetType() == typeof(int) || Object_Int32.GetType() == typeof(double));
    [TestMethod] public void TypeIs() => this.実行結果が一致するか確認(() => Object_Int32 is int && Object_Int32 is double);
    [TestMethod]
    public void TryExpression()
    {
        var l = Expression.Lambda<Func<int>>(
            Expression.TryCatch(
                Expression.Throw(
                    Expression.New(typeof(Exception)),
                    typeof(int)
                ),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(1)
                )
            )
        );
        this.実行結果が一致するか確認(l);
    }
}