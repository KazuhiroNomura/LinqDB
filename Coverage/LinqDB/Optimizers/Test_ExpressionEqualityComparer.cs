using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Optimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Binder = Microsoft.CSharp.RuntimeBinder;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_ExpressionEqualityComparer : ATest
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
    [TestMethod] public void AndChecked() => this.実行結果が一致するか確認(() => _Boolean & _Boolean);
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

    [TestMethod]
    public void TypeAs()
        => this.実行結果が一致するか確認(() => (Object_String as string) + (Object_String as string));

    [TestMethod]
    public void UnaryPlus()
        => this.実行結果が一致するか確認(() => +_Static_class_演算子オーバーロード1 - +_Static_class_演算子オーバーロード1);

    [TestMethod] public void Unbox() => this.実行結果が一致するか確認(() => (int)Object_Int32 + (int)Object_Int32);

    [TestMethod]
    public void Block()
    {
        //if(a_Variables_Count!=b_Variables_Count) return false;
        var p = Expression.Parameter(typeof(int));
        var q = Expression.Parameter(typeof(double));
        IsFalse(
            Expression.Block(
                new[] {
                    p,q
                },
                Expression.Constant(0)
            ),
            Expression.Block(
                new[] {
                    p
                },
                Expression.Constant(0)
            )
        );
        //for(var i=0;i<a_Variables_Count;i++) {
        //    if(a_Variable.Type!=b_Variable.Type)return false;
        IsFalse(
            Expression.Block(
                new[] {
                    q
                },
                Expression.Constant(0)
            ),
            Expression.Block(
                new[] {
                    p
                },
                Expression.Constant(0)
            )
        );
        //}
        IsTrue(
            Expression.Block(
                new[] {
                    p
                },
                Expression.Constant(0)
            ),
            Expression.Block(
                new[] {
                    p
                },
                Expression.Constant(0)
            )
        );
    }

    [TestMethod]
    public void Conditional()
        => this.実行結果が一致するか確認(() => (_Boolean ? _Int32 : 10) + (_Boolean ? _Int32 : 10));

    [TestMethod] public void Constant() => this.実行結果が一致するか確認(() => _List[1] + _List[1]);

    [TestMethod]
    public void DebugInfo()
    {
        var SymbolDocument0 = Expression.SymbolDocument("ソースファイル名0.cs");
        var SymbolDocument1 = Expression.SymbolDocument("ソースファイル名1.cs");
        //a.Document==b.Document&&a.StartLine==b.StartLine&&a.StartColumn==b.StartColumn&&a.EndLine==b.EndLine&&a.EndColumn==b.EndColumn;
        IsFalse(
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4),
            Expression.DebugInfo(SymbolDocument1, 11, 22, 33, 44)
        );
        IsFalse(
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4),
            Expression.DebugInfo(SymbolDocument0, 11, 22, 33, 44)
        );
        IsFalse(
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4),
            Expression.DebugInfo(SymbolDocument0, 1, 22, 33, 44)
        );
        IsFalse(
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4),
            Expression.DebugInfo(SymbolDocument0, 1, 2, 33, 44)
        );
        IsFalse(
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4),
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 44)
        );
        IsTrue(
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4),
            Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4)
        );
    }

    //Default
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public struct TestDynamic<T>
    {
        public readonly T メンバー1;
        public readonly T メンバー2;

        public TestDynamic(T メンバー)
        {
            this.メンバー1 = メンバー;
            this.メンバー2 = メンバー;
        }
    }

    public static dynamic Dynamicメンバーアクセス(dynamic a)
    {
        return a.メンバー;
    }

    private static void IsTrue(Expression a, Expression b)
        => Assert.IsTrue(Optimizer.Test_ExpressionEqualityComparer(a, b));

    private static void IsFalse(Expression a, Expression b)
        => Assert.IsFalse(Optimizer.Test_ExpressionEqualityComparer(a, b));

    [TestMethod]
    public void Dynamic()
    {
        var CSharpArgumentInfo1 = Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None, null);
        var CSharpArgumentInfoArray1 = new[] {
            CSharpArgumentInfo1
        };
        var CSharpArgumentInfoArray2 = new[] {
            CSharpArgumentInfo1,
            CSharpArgumentInfo1
        };
        var CSharpArgumentInfoArray3 = new[] {
            CSharpArgumentInfo1,
            CSharpArgumentInfo1,
            CSharpArgumentInfo1
        };
        //if(!this.SequenceEqual(a.Arguments,b.Arguments)) return false;
        {
            IsFalse(
                Expression.Dynamic(
                    Binder.Binder.UnaryOperation(
                        Binder.CSharpBinderFlags.None,
                        ExpressionType.Increment,
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(1, typeof(object))
                ),
                Expression.Dynamic(
                    Binder.Binder.UnaryOperation(
                        Binder.CSharpBinderFlags.None,
                        ExpressionType.Increment,
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(2, typeof(object))
                )
            );
        }
        //if(a_ConvertBinder!=null) {
        //    if(a_ConvertBinder.ReturnType!=b_ConvertBinder.ReturnType)return false;
        {
            var Constant_1L = Expression.Constant(1L);
            IsFalse(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(int),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
            IsTrue(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
        }
        //    return a_ConvertBinder.Explicit==b_ConvertBinder.Explicit;
        {
            var Constant_1L = Expression.Constant(1L);
            IsTrue(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
            IsFalse(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.None,
                        typeof(double),
                        typeof(Test_ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
        }
        //if(a_GetMemberBinder!=null) {
        {
            IsTrue(
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.None,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                ),
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.None,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                )
            );
        }
        //    return a_GetMemberBinder.Name.Equals(b_GetMemberBinder.Name,StringComparison.Ordinal);
        {
            IsFalse(
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                ),
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー2),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                )
            );
            IsTrue(
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                ),
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                )
            );
        }
        //}
        //if(a_SetMemberBinder!=null) {
        //    return a_SetMemberBinder.Name.Equals(b_SetMemberBinder.Name,StringComparison.Ordinal);
        {
            IsFalse(
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                ),
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー2),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                )
            );
            IsTrue(
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                ),
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                )
            );
        }
        //}
        //if(a_GetIndexBinder!=null) {
        {
            const int expected = 2;
            var Array = new[] {
                1,expected,3
            };
            IsTrue(
                Expression.Dynamic(
                    Binder.Binder.GetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1)
                ),
                Expression.Dynamic(
                    Binder.Binder.GetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1)
                )
            );
        }
        //}
        //if(a_SetIndexBinder!=null) {
        //    return a_SetIndexBinder.CallInfo.Equals(b_SetIndexBinder.CallInfo);
        {
            var Array = new[] {
                1,2,3
            };
            IsTrue(
                Expression.Dynamic(
                    Binder.Binder.SetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray3
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1),
                    Expression.Constant(2)
                ),
                Expression.Dynamic(
                    Binder.Binder.SetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(Test_ExpressionEqualityComparer),
                        CSharpArgumentInfoArray3
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1),
                    Expression.Constant(2)
                )
            );
        }
        //}
        //return true;
        {
            var オペランド = Expression.Dynamic(
                Binder.Binder.BinaryOperation(
                    Binder.CSharpBinderFlags.None,
                    ExpressionType.Add,
                    typeof(Test_ExpressionEqualityComparer),
                    new[] {
                        Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None,null),
                        Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None,null)
                    }
                ),
                typeof(object),
                Expression.Constant(1),
                Expression.Constant(1)
            );
            IsTrue(
                オペランド,
                オペランド
            );
        }
    }
    [TestMethod]
    public void Goto0(){
        //a.Target==b.Target&&this.Equals(a.Value,b.Value);
        var Label1 = Expression.Label(typeof(int),"Label1");
        var Label2 = Expression.Label(typeof(int),"Label2");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Block(
                        Expression.Goto(Label1, Expression.Constant(1)),
                        Expression.Label(Label1, Expression.Constant(11))
                    ),
                    Expression.Block(
                        Expression.Goto(Label2, Expression.Constant(2)),
                        Expression.Label(Label2, Expression.Constant(22))
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Goto1(){
        var Label1 = Expression.Label(typeof(int),"Label1");
        var Label2 = Expression.Label(typeof(int),"Label2");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Block(
                        Expression.Goto(Label1, Expression.Constant(1)),
                        Expression.Label(Label1, Expression.Constant(11))
                    ),
                    Expression.Block(
                        Expression.Goto(Label2, Expression.Constant(1)),
                        Expression.Label(Label2, Expression.Constant(11))
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Goto2(){
        var Label1 = Expression.Label(typeof(int),"Label1");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Goto(Label1, Expression.Constant(1)),
                    Expression.Goto(Label1, Expression.Constant(2)),
                    Expression.Label(Label1, Expression.Constant(22))
                )
            )
        );
    }

    //private sealed class Target<
    //    T00,T01
    //>:Target {
    //    public T00 v00;
    //    public T01 v01;

    //    public override Object this[Int32 index]{
    //        get{
    //            switch(index) {
    //                case 0:
    //                    return this.v00;
    //                case 1:
    //                    return this.v01;
    //                default:
    //                    throw new IndexOutOfRangeException("Target2["+index+"]は範囲外");
    //            }
    //        }
    //        set {
    //            switch(index) {
    //                case 0:
    //                this.v00 =(T00)value;
    //                    break;
    //                case 1:
    //                this.v01 =(T01)value;
    //                    break;
    //                default:
    //                    throw new IndexOutOfRangeException("Target2["+index+"]は範囲外");
    //            }
    //        }
    //    }
    //}

    //[TestMethod] public void Index() {
    //    {
    //        var Target=new Target<Int32,Int32> {
    //            v00=11,
    //            v01=22
    //        };
    //        var D=Expression.Lambda<Func<Int32>>(
    //            Expression.Add(
    //                Expression.Field(
    //                    Expression.Constant(Target),
    //                    "v00"
    //                ),
    //                Expression.Field(
    //                    Expression.Constant(Target),
    //                    "v01"
    //                )
    //            )
    //        ).Compile();
    //        Assert.AreEqual(33,D());
    //    }
    //    //a.Indexer==b.Indexer&&this.Equals(a.Object,b.Object)&&this.SequentialEquals(a.Arguments,b.Arguments);
    //    {
    //        var 配列1=new[] {
    //            1,2,3
    //        };
    //        var 配列2=new[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Int32>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列2),
    //                        Expression.Constant(2)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //    {
    //        var 配列1=new[] {
    //            1,2,3
    //        };
    //        var 配列2=new[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Int32>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列2),
    //                        Expression.Constant(1)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //    {
    //        var 配列1=new[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Int32>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(2)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //    {
    //        var 配列1=new Decimal[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Decimal>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //}

    [TestMethod] public void Invoke() => this.実行結果が一致するか確認(() => _Delegate(4) + _Delegate(4));
    [TestMethod]public void Lambda0(){
        var p = Expression.Parameter(typeof(int));
        IsTrue(
            Expression.Lambda(
                p,
                p
            ),
            Expression.Lambda(
                p,
                p
            )
        );
        var q = Expression.Parameter(typeof(int));
        IsFalse(
            Expression.Lambda(
                p,
                p
            ),
            Expression.Lambda(
                q,
                p
            )
        );
    }
    [TestMethod]public void Lambda1(){
        var v = Expression.Parameter(typeof(int),"v");
        var a0 = Expression.Parameter(typeof(int),"a0");
        var a1 = Expression.Parameter(typeof(int),"a1");
        IsTrue(
            Expression.Lambda<Func<int,int>>(
                a0,
                a0
            ),
            Expression.Lambda<Func<int,int>>(
                a1,
                a1
            )
        );
    }
    [TestMethod]public void Lambda2(){
        var v0 = Expression.Parameter(typeof(int),"v0");
        var v1 = Expression.Parameter(typeof(int),"v1");
        var a0 = Expression.Parameter(typeof(int),"a0");
        var a1 = Expression.Parameter(typeof(int),"a1");
        IsTrue(
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a0,
                    a0
                ),
                v0
            ),
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a1,
                    a1
                ),
                v0
            )
        );
        IsFalse(
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a0,
                    a0
                ),
                v0
            ),
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a1,
                    a1
                ),
                v1
            )
        );
        IsFalse(
            Expression.Block(
                typeof(void),
                Expression.Lambda<Func<int,int>>(
                    a0,
                    a0
                ),
                v0
            ),
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a1,
                    a1
                ),
                v0
            )
        );
    }
    [TestMethod]
    public void ListInit() => this.実行結果が一致するか確認(() => new
    {
        a = new List<int> {
            1,
            2
        },
        b = new List<int> {
            1,
            2
        }
    });

    [TestMethod]
    public void Loop()
    {
        var VoidBreak0 = Expression.Label();
        var Continue0 = Expression.Label();
        var カウンタ = Expression.Parameter(typeof(int), "カウンタ");
        //if(a_BreakLabel==null) {
        //    if(b_BreakLabel!=null) return false;
        IsFalse(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                ),
                Expression.Label(VoidBreak0)
            )
        );
        IsTrue(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            )
        );
        IsTrue(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            )
        );
        //}
        //if(b_BreakLabel==null) return false;
        IsFalse(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                ),
                Expression.Label(VoidBreak0)
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            )
        );
        //if(a_ContinueLabel!=null) {
        //    if(b_ContinueLabel==null) return false;
        IsFalse(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            )
        );
        IsTrue(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            )
        );
        //} else {
        //    if(b_ContinueLabel!=null) return false;
        IsFalse(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            )
        );
        IsTrue(
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            ),
            Expression.Block(
                new[] {
                    カウンタ
                },
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            )
        );
        //}
    }

    [TestMethod]
    public void MemberAccess()
        =>
            this.実行結果が一致するか確認(
                () => _Static_class_演算子オーバーロード1.Int32フィールド + _Static_class_演算子オーバーロード1.Int32フィールド);

    [TestMethod]
    public void MemberInit() => this.実行結果が一致するか確認(() => new
    {
        a = new class_演算子オーバーロード
        {
            Int32フィールド = 3
        },
        b = new class_演算子オーバーロード
        {
            Int32フィールド = 3
        }
    });

    [TestMethod] public void Call() => this.実行結果が一致するか確認(() => Function() + Function());

    [TestMethod]
    public void NewArrayBounds() => this.実行結果が一致するか確認(() => new
    {
        a = new int[10],
        b = new int[10]
    });

    [TestMethod]
    public void NewArrayInit() => this.実行結果が一致するか確認(() => new
    {
        a = new[] {
            1,2,3
        },
        b = new[] {
            1,2,3
        }
    });

    [TestMethod]
    public void New() => this.実行結果が一致するか確認(() => new
    {
        a = new class_演算子オーバーロード(),
        b = new class_演算子オーバーロード()
    });

    [TestMethod] public void Calesce() => this.実行結果が一致するか確認(() => (_NullableInt32 ?? 4) + (_NullableInt32 ?? 4));
    //共通部分式でParameterは最速なので先行評価しないため、カバレッジが出来方法が思いつかない。
    [TestMethod]
    public void Parameter()
    {
        //if(a_Index!=b_Index) return false;
        var p = Expression.Parameter(typeof(int));
        var q = Expression.Parameter(typeof(int));
        IsFalse(
            Expression.Lambda<Func<int, int, int>>(
                p, p, q),
            Expression.Lambda<Func<int, int, int>>(
                q, p, q)
        );
        //if(a_Index>=0) return true;
        IsTrue(
            Expression.Lambda<Func<int, int, int>>(
                p, p, q),
            Expression.Lambda<Func<int, int, int>>(
                p, p, q)
        );
        //this.a_Parameters.Add(a);
        //this.b_Parameters.Add(b);
        //return a==b;
        IsTrue(
            p,
            p
        );
        IsFalse(
            p,
            q
        );
    }

    //Switch
    //Try
    //実際どんな式か分からない。
    [TestMethod]
    public void TypeEqual()
    {
        this.実行結果が一致するか確認(() =>
            // ReSharper disable once OperatorIsCanBeUsed
            Object_Int32.GetType() == typeof(int) &&
            // ReSharper disable once OperatorIsCanBeUsed
            Object_Int32.GetType() == typeof(int)
        );
    }

    [TestMethod]
    public void TypeIs()
        => this.実行結果が一致するか確認(() => Object_Int32 is int || Object_Int32 is int);

    private static double L(Func<double> f) => f();

    [TestMethod]
    public void Default()
    {
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<double>>(
                Expression.Add(
                    Expression.Call(
                        typeof(Test_ExpressionEqualityComparer).GetMethod(
                            nameof(Test_ExpressionEqualityComparer.L),
                            BindingFlags.Static | BindingFlags.NonPublic),
                        Expression.Lambda<Func<double>>(
                            Expression.Default(typeof(double))
                        )
                    ),
                    Expression.Call(
                        typeof(Test_ExpressionEqualityComparer).GetMethod(
                            nameof(Test_ExpressionEqualityComparer.L),
                            BindingFlags.Static | BindingFlags.NonPublic),
                        Expression.Lambda<Func<double>>(
                            Expression.Default(typeof(double))
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Switch()
    {
        //if(a.Comparison!=b.Comparison) return false;
        IsFalse(
            Expression.Switch(
                Expression.Constant(123),
                Expression.Constant(0m),
                Expression.SwitchCase(
                    Expression.Constant(64m),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant("A"),
                Expression.Constant(1m),
                Expression.SwitchCase(
                    Expression.Constant(64m),
                    Expression.Constant("A")
                )
            )
        );
        //if(!this.PrivateEquals(a.DefaultBody,b.DefaultBody)) return false;
        IsFalse(
            Expression.Switch(
                Expression.Constant(123),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(1),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            )
        );
        //if(!this.PrivateEquals(a.SwitchValue,b.SwitchValue)) return false;
        IsFalse(
            Expression.Switch(
                Expression.Constant(123),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(63),
                    Expression.Constant(124)
                )
            )
        );
        //if(a_Cases.Count!=b_Cases.Count) return false;
        IsFalse(
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(66),
                    Expression.Constant(124)
                ),
                Expression.SwitchCase(
                    Expression.Constant(63),
                    Expression.Constant(123)
                )
            )
        );
        //for(var c=0;c<a_Cases_Count;c++) {
        //    if(!this.PrivateEquals(a_Case.Body,b_Case.Body)) return false;
        IsFalse(
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-63),
                    Expression.Constant(124)
                )
            )
        );
        //    if(!this.SequenceEqual(a_Case.TestValues,b_Case.TestValues)) return false;
        IsFalse(
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(123)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(124)
                )
            )
        );
        //}
        {
            var Switch = Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(124)
                )
            );
            IsTrue(
                Switch,
                Switch
            );
        }
    }

    [TestMethod]
    public void Try()
    {
        //if(!this.PrivateEquals(a.Body,b.Body)) return false;
        IsFalse(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(1),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
        IsTrue(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
    }

    [TestMethod]
    public void TryFault()
    {
        //if(!this.PrivateEquals(a.Fault,b.Fault)) return false;
        IsFalse(
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(0)
            ),
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(1)
            )
        );
        IsTrue(
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(0)
            ),
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(0)
            )
        );
    }

    [TestMethod]
    public void TryFinally()
    {
        //if(!this.PrivateEquals(a.Finally,b.Finally)) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.TryFinally(
                    Expression.Constant(0),
                    Expression.Default(typeof(int))
                )
            )
        );
        IsFalse(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            ),
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(int))
            )
        );
        IsTrue(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            ),
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            )
        );
    }

    [TestMethod]
    public void TryCatch_Handlers(){
        //if(a_Handlers_Count!=b_Handlers.Count) return false;
        IsFalse(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(AmbiguousMatchException),
                    Expression.Constant(0)
                ),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
        IsTrue(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
    }

    [TestMethod]
    public void TryCatch_Handler_Body()
    {
        //for(var c=0;c<a_Handlers_Count;c++) {
        //    if(!this.PrivateEquals(a_Handler.Body  ,b_Handler.Body  )) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(1)
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void TryCatch_Filter0()
    {
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(0),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0),
                        Expression.Constant(true)

                    )
                )
            )
        );
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
    }
    [TestMethod]
    public void TryCatch_Filter1()
    {
        //    if(!this.PrivateEquals(a_Handler.Filter,b_Handler.Filter)) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0),
                            Expression.Constant(true)

                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0),
                            Expression.Constant(false)
                        )
                    )
                )
            )
        );
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
    }
    [TestMethod]
    public void TryCatch_Filter2(){
        var Variable=Expression.Parameter(typeof(Exception));
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            Variable,
                            Expression.Constant(0),
                            Expression.Equal(Variable,Expression.Default(Variable.Type))

                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            Variable,
                            Expression.Constant(1),
                            Expression.Equal(
                                Expression.Property(
                                    Variable,
                                    "Message"
                                ),
                                Expression.Default(typeof(string))
                            )
                        )
                    )
                )
            )
        );
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
    }

    [TestMethod]
    public void TryCatch_Test()
    {
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)

                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(NotSupportedException),
                            Expression.Constant(0)
                        )
                    )
                )
            )
        );
    }

    [TestMethod]
    public void TryCatch()
    {
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    )
                )
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void TryCatchFinally(){
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.TryCatchFinally(
                    Expression.Constant(0),
                    Expression.Default(typeof(int)),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(1)
                    )
                )
            )
        );
        //if(!this.PrivateEquals(a.Finally,b.Finally)) return false;
        IsFalse(
            Expression.TryCatchFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void)),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatchFinally(
                Expression.Constant(0),
                Expression.Default(typeof(int)),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(1)
                )
            )
        );
        IsTrue(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            ),
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            )
        );
    }

    [TestMethod]
    public void IsTrue()
    {
        //todo ダメだった
        //            this._変数Cache.AssertExecute(()=>(class_演算子オーバーロード2&&class_演算子オーバーロード2)-(class_演算子オーバーロード2&&class_演算子オーバーロード2));
        //_Field ? _Field : (_Field | b);
        //Test_ExpressionEqualityComparer.class_演算子オーバーロード2.op_False(_Field) ? _Field : (_Field & b);
        var Constant = Expression.Constant(_Static_class_演算子オーバーロード1);
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<class_演算子オーバーロード>>(
                Expression.Condition(
                    Expression.IsTrue(Constant),
                    Constant,
                    Expression.Or(
                        Constant, Constant
                    )
                )
            )
        );
    }

    [TestMethod]
    public void IsFalse()
    {
        //todo ダメだった
        //            this._変数Cache.AssertExecute(()=>(class_演算子オーバーロード2&&class_演算子オーバーロード2)-(class_演算子オーバーロード2&&class_演算子オーバーロード2));
        //_Field ? _Field : (_Field | b);
        //Test_ExpressionEqualityComparer.class_演算子オーバーロード2.op_False(_Field) ? _Field : (_Field & b);
        var Constant = Expression.Constant(_Static_class_演算子オーバーロード1);
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<class_演算子オーバーロード>>(
                Expression.Condition(
                    Expression.IsFalse(Constant),
                    Constant,
                    Expression.And(
                        Constant, Constant
                    )
                )
            )
        );
    }

    [TestMethod]
    public void SequencialEqual()
    {
        //if(a_Count!=b.Count) return false;
        IsFalse(
            Expression.Block(
                Expression.Constant(0),
                Expression.Constant(0)
            ),
            Expression.Block(
                Expression.Constant(0)
            )
        );
        //for(var i=0;i<a_Count;i++) {
        //    if(!this.PrivateEquals(a[i],b[i])) return false;
        IsFalse(
            Expression.Block(
                Expression.Constant(0)
            ),
            Expression.Block(
                Expression.Constant(1)
            )
        );
        //}
        IsTrue(
            Expression.Block(
                Expression.Constant(0)
            ),
            Expression.Block(
                Expression.Constant(0)
            )
        );
    }

    [TestMethod]
    public void PrivateEquals()
    {
        //if(a==null)return b==null;
        IsFalse(
            null,
            Expression.Constant(0)
        );
        IsTrue(
            null,
            null
        );
        //if(b==null)return false;
        IsFalse(
            Expression.Constant(0),
            null
        );
        //if(a.NodeType!=b.NodeType||a.Type!=b.Type) return false;
        IsFalse(
            Expression.Constant(0),
            Expression.Default(typeof(int))
        );
        //Assign
        //if(a_Left.NodeType!=b_Left.NodeType) return false;
        {
            var p = Expression.Parameter(typeof(int));
            var q = Expression.Parameter(typeof(int[]));
            IsFalse(
                Expression.Assign(
                    p,
                    Expression.Constant(0)
                ),
                Expression.Assign(
                    Expression.ArrayAccess(
                        q,
                        Expression.Constant(0)
                    ),
                    Expression.Constant(0)
                )
            );
        }
        //if(a_Left.NodeType!=ExpressionType.Parameter) return this.T(a_Assign,b_Assign);
        {
            var p = Expression.Parameter(typeof(int[]));
            IsTrue(
                Expression.Assign(
                    Expression.ArrayAccess(
                        p,
                        Expression.Constant(0)
                    ),
                    Expression.Constant(0)
                ),
                Expression.Assign(
                    Expression.ArrayAccess(
                        p,
                        Expression.Constant(0)
                    ),
                    Expression.Constant(0)
                )
            );
        }
        //if(!this.PrivateEquals(a_Assign.Right,b_Assign.Right)) return false;
        {
            var p = Expression.Parameter(typeof(int));
            var q = Expression.Parameter(typeof(int));
            IsFalse(
                Expression.Assign(
                    p,
                    Expression.Constant(0)
                ),
                Expression.Assign(
                    q,
                    Expression.Constant(1)
                )
            );
        }
        //if(a_局所Index!=b_局所Index) return false;
        {
            var p = Expression.Parameter(typeof(int));
            var q = Expression.Parameter(typeof(int));
            IsFalse(
                Expression.Block(
                    new[] {
                        p,q
                    },
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                ),
                Expression.Block(
                    new[] {
                        p,q
                    },
                    Expression.Assign(
                        q,
                        Expression.Constant(0)
                    )
                )
            );
        }
        //if(a_局所Index>=0) return true;
        {
            var p = Expression.Parameter(typeof(int));
            var q = Expression.Parameter(typeof(int));
            IsTrue(
                Expression.Block(
                    new[] {
                        p,q
                    },
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                ),
                Expression.Block(
                    new[] {
                        p,q
                    },
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                )
            );
        }
        {
            var p = Expression.Parameter(typeof(int));
            var q = Expression.Parameter(typeof(int));
            IsTrue(
                Expression.Block(
                    new[] {
                        q
                    },
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                ),
                Expression.Block(
                    new[] {
                        p
                    },
                    Expression.Assign(
                        q,
                        Expression.Constant(0)
                    )
                )
            );
        }
    }

    private class List1 : IEnumerable<int>
    {
        public void Add(int value) { }

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    private sealed class List2 : List1
    {
        public new void Add(int value) { }
    }

    [TestMethod]
    public void InitializersEquals()
    {
        var List1 = typeof(List1);
        var List2 = typeof(List2);
        var AddMethod1 = typeof(List1).GetMethod("Add");
        var AddMethod2 = typeof(List2).GetMethod("Add");
        var ctor1 = List1.GetConstructor(Type.EmptyTypes);
        var ctor2 = List2.GetConstructor(Type.EmptyTypes);
        //if(a_Initializers.Count!=b_Initializers.Count) return false;
        Debug.Assert(ctor1 != null, "ctor1 != null");
        IsFalse(
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0),
                Expression.Constant(0)
            )
        );
        //for(var c=0;c<a_Initializers_Count;c++){
        //    if(a.AddMethod!=b.AddMethod) return false;
        Debug.Assert(ctor2 != null, "ctor2 != null");
        IsFalse(
            Expression.ListInit(
                Expression.New(ctor2),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor2),
                AddMethod2,
                Expression.Constant(0)
            )
        );
        //    if(!this.SequenceEqual(a.Arguments,b.Arguments)) return false;
        IsFalse(
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(1)
            )
        );
        //}
        IsTrue(
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            )
        );
    }

    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    private class BindCollection
    {
        public int Int32フィールド1;
        public int Int32フィールド2;
        public BindCollection BindCollectionフィールド1;
        public BindCollection BindCollectionフィールド2;
        public readonly List<int> Listフィールド1 = new();
        public readonly List<int> Listフィールド2 = new();

        public BindCollection(int v)
        {
            this.Int32フィールド1 = 0;
            this.Int32フィールド2 = 0;
            this.BindCollectionフィールド1 = null;
            this.BindCollectionフィールド2 = null;
        }
    }

    [TestMethod]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public void MemberBindingsEquals()
    {
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1));
        var Int32フィールド2 = Type.GetField(nameof(BindCollection.Int32フィールド2));
        var BindCollectionフィールド1 = Type.GetField(nameof(BindCollection.BindCollectionフィールド1));
        var BindCollectionフィールド2 = Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1));
        var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1 = Expression.Constant(1);
        var Constant_2 = Expression.Constant(2);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expression.New(
            ctor,
            Constant_1
        );
        //if(a_Bindings.Count!=b_Bindings.Count) return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                ),
                Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
        //for(var c=0;c<a_Bindings_Count;c++){
        //    if(a.BindingType!=b.BindingType) return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        typeof(List<int>).GetMethod("Add"),
                        Constant_1
                    )
                )
            )
        );
        //    switch(a.BindingType){
        //        case MemberBindingType.Assignment:{
        //            if(a1.Member!=b1.Member)return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
        //            if(!this.Equals(a1.Expression,b1.Expression))return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_2
                )
            )
        );
        //            break;
        IsTrue(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            )
        );
        //}
        //        case MemberBindingType.MemberBinding:{
        //            if(a1.Member!=b1.Member)return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド2,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            )
        );
        //            if(!this.MemberBindingsEquals(a1.Bindings,b1.Bindings)) return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    ),
                    Expression.Bind(
                        Int32フィールド2,
                        Constant_1
                    )
                )
            )
        );
        //            break;
        IsTrue(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            )
        );
        //        }
        //        default:{
        var Add = typeof(List<int>).GetMethod("Add");
        //            if(a1.Member!=b1.Member)return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド2,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            )
        );
        //            if(!InitializersEquals(a1.Initializers,b1.Initializers)) return false;
        IsFalse(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_2
                    )
                )
            )
        );
        //            break;
        IsTrue(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            )
        );
        //        }
        //    }
        //}
        //return true;
    }
    private readonly struct A : IEquatable<A>
    {
        public static A operator +(A a, A b) => new(a.v + b.v);
        public static A Add1(A a, A b) => new(a.v + b.v);
        public static A Add2(A a, A b) => new(a.v + b.v);
        public bool Equals(A other) => this.v == other.v;
        public override bool Equals(object obj) => this.Equals((A)obj);
        public override int GetHashCode() => this.v;
        private readonly int v;
        public A(int v)
        {
            this.v = v;
        }
    }
    [TestMethod]
    public void T_Binary()
    {
        //a.Method == b.Method && 
        //                        this.PrivateEquals(a.Left, b.Left) && 
        //                                                              this.PrivateEquals(a.Right, b.Right);
        //            var Methods = new[] { null, typeof(A).GetMethod(nameof(A.Add1)), typeof(A).GetMethod(nameof(A.Add2)) };
        var Methods = new[] { null, typeof(A).GetMethod("op_Addition") };
        var 値配列 = new[] { new A(1), new A(2) };
        foreach (var Method0 in Methods)
        {
            foreach (var Method1 in Methods)
            {
                foreach (var Left0 in 値配列)
                {
                    foreach (var Left1 in 値配列)
                    {
                        foreach (var Right0 in 値配列)
                        {
                            foreach (var Right1 in 値配列)
                            {
                                Optimizer.Test_ExpressionEqualityComparer(
                                    Expression.Add(
                                        Expression.Constant(Left0),
                                        Expression.Constant(Right0),
                                        Method0
                                    ),
                                    Expression.Add(
                                        Expression.Constant(Left1),
                                        Expression.Constant(Right1),
                                        Method1
                                    )
                                );
                            }
                        }
                    }
                }
            }
        }
    }
}
