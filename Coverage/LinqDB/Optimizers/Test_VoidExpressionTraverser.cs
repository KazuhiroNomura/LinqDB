using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_VoidExpressionTraverser : ATest
{

    [TestMethod] public void Add() => this.Execute2(() => _Int32 + _Int32);
    [TestMethod] public void AddChecked() => this.Execute2(() => checked(_Int32 + _Int32));
    [TestMethod] public void Subtract() => this.Execute2(() => _Int32 - _Int32);
    [TestMethod] public void SubtractChecked() => this.Execute2(() => checked(_Int32 - _Int32));
    [TestMethod] public void Multiply() => this.Execute2(() => _Int32 * _Int32);
    [TestMethod] public void MultiplyChecked() => this.Execute2(() => checked(_Int32 * _Int32));
    [TestMethod] public void Divide() => this.Execute2(() => _Int32 / _Int32);
    [TestMethod] public void Modulo() => this.Execute2(() => _Int32 % _Int32);
    [TestMethod] public void And() => this.Execute2(() => _Boolean & _Boolean);
    [TestMethod] public void Or() => this.Execute2(() => _Boolean | _Boolean);
    [TestMethod] public void ExclusiveOr() => this.Execute2(() => _Boolean ^ _Boolean);
    [TestMethod] public void AndAlso() => this.Execute2(() => _Boolean && _Boolean);
    [TestMethod] public void OrElse() => this.Execute2(() => _Boolean || _Boolean);
    [TestMethod] public void Equal() => this.Execute2(() => _Int32 == 3);
    [TestMethod] public void NotEqual() => this.Execute2(() => _Int32 != 3);
    [TestMethod] public void GreaterThan() => this.Execute2(() => _Int32 > 3);
    [TestMethod] public void GreaterThanOrEqual() => this.Execute2(() => _Int32 >= 3);
    [TestMethod] public void LessThan() => this.Execute2(() => _Int32 < 3);
    [TestMethod] public void LessThanOrEqual() => this.Execute2(() => _Int32 <= 3);
    [TestMethod] public void LeftShift() => this.Execute2(() => _Int32 << 3);
    [TestMethod] public void RightShift() => this.Execute2(() => _Int32 >> 3);
    [TestMethod] public void ArrayIndex() => this.Execute2(() => Array[0]);
    [TestMethod] public void ArrayLength() => this.Execute2(() => Array.Length);
    [TestMethod] public void Convert() => this.Execute2(() => (double)_Int32 + (double)_Int32);
    [TestMethod] public void ConvertChecked() => this.Execute2(() => (double)_Int32 + (double)_Int32);
    //[TestMethod] public void Increment         ()=>this._変数Cache.AssertExecute(()=>_Int32+1);
    //[TestMethod] public void Decrement         ()=>this._変数Cache.AssertExecute(()=>_Int32-1);
    //[TestMethod] public void IsFalse           ()=>this._変数Cache.AssertExecute(()=>class_演算子オーバーロード2--);
    //[TestMethod] public void IsTrue            ()=>this._変数Cache.AssertExecute(()=>class_演算子オーバーロード2++);
    [TestMethod] public void Negate() => this.Execute2(() => -_Int32 + -_Int32);
    [TestMethod] public void NegateChecked() => this.Execute2(() => checked(-_Int32) + checked(-_Int32));
    [TestMethod] public void Not() => this.Execute2(() => !_Boolean && !_Boolean);
    [TestMethod] public void OnesComplement() => this.Execute2(() => ~_Int32 + ~_Int32);
    [TestMethod] public void TypeAs() => this.Execute2(() => (Object_String as string) + (Object_String as string));
    [TestMethod] public void UnaryPlus() => this.Execute2(() => +変数_普通ではサポートされないな演算子 - +変数_普通ではサポートされないな演算子);
    [TestMethod] public void Unbox() => this.Execute2(() => (int)Object_Int32 + (int)Object_Int32);
    //Block
    [TestMethod] public void Conditional() => this.Execute2(() => (_Boolean ? _Int32 : 10) + (_Boolean ? _Int32 : 10));
    [TestMethod] public void Constant() => this.Execute2(() => _List[1] + _List[1]);
    //DebugInfo
    //Default
    //Dynamic
    //Goto
    [TestMethod]
    public void Index()
    {
        var Array1 = new[] { 1, 2, 3 };
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.ArrayAccess(
                    Expression.Constant(Array1),
                    Expression.Constant(1)
                )
            )
        );
    }
    [TestMethod] public void Invoke() => this.Execute2(() => _Delegate(4) + _Delegate(4));
    //Label
    [TestMethod] public void Lambda() => this.Execute2(() => Lambda(p => p * 2) + Lambda(p => p * 2));
    [TestMethod] public void ListInit() => this.Execute2(() => new { a = new List<int> { 1, 2 }, b = new List<int> { 1, 2 } });
    //Loop
    [TestMethod] public void MemberAccess() => this.Execute2(() => 変数_普通ではサポートされないな演算子.メンバー + 変数_普通ではサポートされないな演算子.メンバー);
    [TestMethod] public void MemberInit() => this.Execute2(() => new { a = new 普通ではサポートされないな演算子 { メンバー = 3 }, b = new 普通ではサポートされないな演算子 { メンバー = 3 } });
    [TestMethod] public void Call() => this.Execute2(() => Function() + Function());
    [TestMethod] public void NewArrayBounds() => this.Execute2(() => new { a = new int[10], b = new int[10] });
    [TestMethod] public void NewArrayInit() => this.Execute2(() => new { a = new[] { 1, 2, 3 }, b = new[] { 1, 2, 3 } });
    [TestMethod] public void New() => this.Execute2(() => new { a = new 普通ではサポートされないな演算子(), b = new 普通ではサポートされないな演算子() });
    [TestMethod] public void Calesce() => this.Execute2(() => (_NullableInt32 ?? 4) + (_NullableInt32 ?? 4));
    //共通部分式でParameterは最速なので先行評価しないため、カバレッジが出来方法が思いつかない。
    [TestMethod] public void Parameter() => this.Execute2(() => Lambda(p => p * p));
    //Switch
    //Try
    //実際どんな式か分からない。
    [TestMethod] [SuppressMessage("ReSharper", "OperatorIsCanBeUsed")] public void TypeEqual() => this.Execute2(() => Object_Int32.GetType() == typeof(int) || Object_Int32.GetType() == typeof(double));
    [TestMethod] public void TypeIs() => this.Execute2(() => Object_Int32 is int && Object_Int32 is double);
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
        this.Execute2(l);
    }
}