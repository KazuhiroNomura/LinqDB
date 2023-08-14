using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB;

[TestClass]
public class ReturnExpressionTraverser : ATest
{
    private void 演算Assign(ExpressionType NodeType)
    {
        var p = Expression.Parameter(typeof(int), "p");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(2)
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(1)
                    )
                )
            )
        );
    }

    private void 演算Binary(ExpressionType NodeType)
    {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.MakeBinary(
                    NodeType,
                    Expression.Constant(2),
                    Expression.Constant(2)
                )
            )
        );
    }
    private void 論理演算Binary(ExpressionType NodeType)
    {
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.MakeBinary(
                    NodeType,
                    Expression.Constant(false),
                    Expression.Constant(true)
                )
            )
        );
    }
    private void 算術論理演算Binary(ExpressionType NodeType)
    {
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.MakeBinary(
                    NodeType,
                    Expression.Constant(6),
                    Expression.Constant(3)
                )
            )
        );
    }
    [TestMethod]
    public void Add() => this.演算Binary(ExpressionType.Add);
    [TestMethod]
    public void AddAssign() => this.演算Assign(ExpressionType.AddAssign);
    [TestMethod]
    public void AddAssignChecked() => this.演算Assign(ExpressionType.AddAssignChecked);
    [TestMethod]
    public void AddChecked() => this.演算Binary(ExpressionType.AddChecked);
    [TestMethod]
    public void And() => this.論理演算Binary(ExpressionType.And);
    [TestMethod]
    public void AndAlso() => this.論理演算Binary(ExpressionType.AndAlso);
    [TestMethod]
    public void AndAssign() => this.演算Assign(ExpressionType.AndAssign);
    [TestMethod]
    public void ArrayIndex() => this.Execute2(() => Array[0]);
    [TestMethod]
    public void ArrayLength() => this.Execute2(() => Array.Length);
    [TestMethod]
    public void Assign()
    {
        var p = Expression.Parameter(typeof(int), "p");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(2)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Bindings()
    {
        //for(var a = 0;a<Bindings0_Count;a++) {
        //    switch(Binding0.BindingType) {
        //        case MemberBindingType.Assignment: {
        this.Execute2(() => new class_演算子オーバーロード { Int32フィールド = 3 });
        //        case MemberBindingType.ListBinding: {
        this.Execute2(() => new class_演算子オーバーロード { StructCollectionフィールド = { 3 } });
        //        case MemberBindingType.MemberBinding: {
        this.Execute2(() => new class_演算子オーバーロード
        {
            class_演算子オーバーロード2フィールド ={
                StructCollectionフィールド = {0}
            }
        });
        //        default: throw new NotSupportedException("MemberBindingType="+Binding0.BindingType+"は不正");
        //    }
        //}
    }
    [TestMethod]
    public void Block()
    {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Constant(1)
                )
            )
        );
    }
    [TestMethod]
    public void Call() => this.Execute2(() => Function() + Function());
    [TestMethod]
    public void Calesce() => this.Execute2(() => (_NullableInt32 ?? 4) + (_NullableInt32 ?? 4));
    [TestMethod]
    public void Conditional() => this.Execute2(() => (_Boolean ? _Int32 : 10) + (_Boolean ? _Int32 : 10));
    [TestMethod]
    public void Constant() => this.Execute2(() => 1m);
    [TestMethod]
    public void Convert() => this.Execute2(() => (double)_Int32 + (double)_Int32);
    [TestMethod]
    public void ConvertChecked() => this.Execute2(() => (double)_Int32 + (double)_Int32);
    [TestMethod]
    public void DebugInfo()
    {
        var SymbolDocument = Expression.SymbolDocument("ソースファイル名.cs");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.DebugInfo(SymbolDocument, 1, 1, 2, 1),
                    Expression.Constant(1)
                )
            )
        );
    }






























    [TestMethod] public void Subtract() => this.演算Binary(ExpressionType.Subtract);
    [TestMethod] public void SubtractChecked() => this.演算Binary(ExpressionType.SubtractChecked);
    [TestMethod] public void Multiply() => this.演算Binary(ExpressionType.Multiply);
    [TestMethod] public void MultiplyChecked() => this.演算Binary(ExpressionType.Subtract);
    [TestMethod] public void Divide() => this.演算Binary(ExpressionType.Divide);
    [TestMethod] public void Modulo() => this.演算Binary(ExpressionType.Modulo);
    [TestMethod] public void Or() => this.論理演算Binary(ExpressionType.Or);
    [TestMethod] public void ExclusiveOr() => this.論理演算Binary(ExpressionType.ExclusiveOr);
    [TestMethod] public void OrElse() => this.論理演算Binary(ExpressionType.OrElse);
    [TestMethod] public void Equal() => this.算術論理演算Binary(ExpressionType.Equal);
    [TestMethod] public void NotEqual() => this.算術論理演算Binary(ExpressionType.Equal);
    [TestMethod] public void GreaterThan() => this.算術論理演算Binary(ExpressionType.Equal);
    [TestMethod] public void GreaterThanOrEqual() => this.算術論理演算Binary(ExpressionType.Equal);
    [TestMethod] public void LessThan() => this.算術論理演算Binary(ExpressionType.Equal);
    [TestMethod] public void LessThanOrEqual() => this.算術論理演算Binary(ExpressionType.Equal);
    [TestMethod] public void LeftShift() => this.演算Binary(ExpressionType.Modulo);
    [TestMethod] public void RightShift() => this.演算Binary(ExpressionType.RightShift);
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
    //DebugInfo
    //Default
    //Dynamic
    //Goto
    [TestMethod] public void Index() => this.Execute2(() => _List[0] + _List[0]);
    [TestMethod] public void Invoke() => this.Execute2(() => _Delegate(4) + _Delegate(4));
    //Label
    [TestMethod] public void Lambda() => this.Execute2(() => Lambda(p => p * 2) + Lambda(p => p * 2));
    [TestMethod] public void ListInit() => this.Execute2(() => new { a = new List<int> { 1, 2 }, b = new List<int> { 1, 2 } });
    //Loop
    [TestMethod] public void MemberAccess() => this.Execute2(() => 変数_普通ではサポートされないな演算子.メンバー + 変数_普通ではサポートされないな演算子.メンバー);
    [TestMethod] public void MemberInit() => this.Execute2(() => new { a = new 普通ではサポートされないな演算子 { メンバー = 3 }, b = new 普通ではサポートされないな演算子 { メンバー = 3 } });
    [TestMethod] public void NewArrayBounds() => this.Execute2(() => new { a = new int[10], b = new int[10] });
    [TestMethod] public void NewArrayInit() => this.Execute2(() => new { a = new[] { 1, 2, 3 }, b = new[] { 1, 2, 3 } });
    [TestMethod] public void New() => this.Execute2(() => new { a = new 普通ではサポートされないな演算子(), b = new 普通ではサポートされないな演算子() });
    //共通部分式でParameterは最速なので先行評価しないため、カバレッジが出来方法が思いつかない。
    [TestMethod] public void Parameter() => this.Execute2(() => Lambda(p => p * p));
    //Switch
    //Try
    //実際どんな式か分からない。
    [TestMethod]
    public void TypeEqual() => this.Execute2(() =>
        // ReSharper disable once OperatorIsCanBeUsed
        Object_Int32.GetType() == typeof(int) &&
        // ReSharper disable once OperatorIsCanBeUsed
        Object_Int32.GetType() == typeof(double));
    [TestMethod] public void TypeIs() => this.Execute2(() => Object_Int32 is int && Object_Int32 is double);
}