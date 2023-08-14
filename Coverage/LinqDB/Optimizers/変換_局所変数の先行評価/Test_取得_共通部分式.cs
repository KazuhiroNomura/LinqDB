using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers.変換_局所変数の先行評価;

[TestClass]
public class Test_局所変数の先行評価: ATest
{
    private static int Staticメソッド() => 1;
    [TestMethod]
    public void Traverse()
    {
        var i = Expression.Parameter(typeof(int));
        //if(Expression==null) return;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Constant(1),
                    Expression.Constant(2)
                )
            )
        );
        //switch(NodeType){
        //    case ExpressionType.Assign:{
        //        if(Binary_Left.NodeType==ExpressionType.Parameter){
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { i },
                    Expression.Assign(
                        i,
                        Expression.Constant(1)
                    )
                )
            )
        );
        //        } else{
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Assign(
                    Expression.ArrayAccess(
                        Expression.Constant(new int[1]),
                        Expression.Constant(0)
                    ),
                    Expression.Constant(1)
                )
            )
        );
        //        }
        //    }
        //    case ExpressionType.Parameter:
        //        if(((ParameterExpression)Expression).Name.Substring(0,変数名長)==Cラムダ跨ぎ) break;
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1.Int32フィールド));
        //        return;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { i },
                    Expression.Assign(
                        i,
                        Expression.Constant(0)
                    ),
                    Expression.Add(
                        i,
                        i
                    )
                )
            )
        );
        //    case ExpressionType.DebugInfo:
        //    case ExpressionType.Default:
        //    case ExpressionType.Goto:
        //    case ExpressionType.Lambda:
        //    case ExpressionType.PostDecrementAssign:
        //    case ExpressionType.PostIncrementAssign:
        //    case ExpressionType.PreIncrementAssign:
        //    case ExpressionType.PreDecrementAssign:
        //    case ExpressionType.Throw:
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Default(typeof(int))
            )
        );
        //    case ExpressionType.Constant:
        //        if(Expression.Type.Is定数をILで直接埋め込めるTypeか()) return;
        this.Execute2(() => "ABC");
        this.Execute2(() => 1m);
        //    case ExpressionType.MemberAccess:{
        //        if(((MemberExpression)Expression).Member.GetCustomAttribute(typeof(NoOptimizeAttribute))!=null)return;
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1._最適化されないメンバー));
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1.Int32フィールド));
        //    }
        //    case ExpressionType.Call:{
        //        if(((MethodCallExpression)Expression).Method.GetCustomAttribute(typeof(NoOptimizeAttribute))!=null) return;
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1.最適化されないメソッド()));
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1.メソッド()));
        //    }
        //}
        //switch(NodeType){
        //    case ExpressionType.Conditional:{
        //        foreach(var ifTrueで二度出現したExpression順序 in Dictionary_ifTrueで二度出現したExpression順序){
        //            if(!Dictionary_ifFalseで二度出現したExpression順序.ContainsKey(ifTrueで二度出現したExpression順序.Key))continue;
        //            if(Dictionary二度出現したExpression順序.ContainsKey(ifTrueで二度出現したExpression順序.Key))continue;
        this.Execute2(() => Lambda(a => a * 3 + a * 3 == 0 ? a * 3 + a * 3 : a * 3 + a * 3 + 3));
        this.Execute2(() => Lambda(a => a == 0 ? a * 3 + a * 3 + a * 3 : a * 3 + a * 3 + a * 3));
        //        }
        this.Execute2(() => Lambda(a => a == 0 ? a + 1 : a + 2));
        //        foreach(var ifTrueで一度出現したExpression順序 in Dictionary_ifTrueで一度出現したExpression順序){
        //            if(!Dictionary_ifFalseで一度出現したExpression順序.ContainsKey(ifTrueで一度出現したExpression順序.Key))continue;
        this.Execute2(() => Lambda(a => a == 0 ? a + 1 : a + 2));
        //            Int32 順序2;
        //            if(Dictionary一度出現したExpression順序.TryGetValue(ifTrueで一度出現したExpression順序.Key,out 順序2)){
        //                if(Dictionary二度出現したExpression順序.ContainsKey(ifTrueで一度出現したExpression順序.Key))continue;
        this.Execute2(() => Lambda(a => a * 4 + a * 4 == 0 ? a * 4 + a * 4 : a * 4 + a * 4 + 4));
        this.Execute2(() => Lambda(a => a * 5 + a * 5 == 0 ? a * 5 + a * 5 : a * 5 + a * 5 + 5));
        //            } else {
        this.Execute2(() => Lambda(a => a == 0 ? a * 6 : a * 6 + 1));
        //            }
        //        }
        //    }
        //    case ExpressionType.Call:{
        //        if(EnumerableSetループ展開可能なGenericMethodDefinitionか(MethodCall_GenericMethodDefinition)) {
        //            if(Reflection.ExtendSet.Aggregate_seed_func_resultSelector==MethodCall_GenericMethodDefinition||Reflection.ExtendEnumerable.Aggregate_seed_func_resultSelector==MethodCall_GenericMethodDefinition) {
        //                if(resultSelctor!=null) {
        this.Execute引数パターン(a => ArrN<int>(a).Aggregate(1, (x, y) => x + y, c => c + 1));
        this.Execute引数パターン(a => EnuN<int>(a).Aggregate(1, (x, y) => x + y, c => c + 1));
        this.Execute引数パターン(a => SetN<int>(a).Aggregate(1, (x, y) => x + y, c => c + 1));
        //                }
        //            }
        this.Execute引数パターン(a => ArrN<int>(a).Sum());
        this.Execute引数パターン(a => EnuN<int>(a).Sum());
        this.Execute引数パターン(a => SetN<int>(a).Sum());
        //        }
        this.Execute2(() => Staticメソッド());
        //    }
        //    case ExpressionType.Label:{
        var Label1 = Expression.Label(typeof(int));
        var Label2 = Expression.Label(typeof(int));
        this.Execute2(
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
        //    }
        //    case ExpressionType.Loop: {
        {
            var Break = Expression.Label();
            var 作業 = Expression.Variable(typeof(decimal));
            this.Execute2(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        new[] { 作業 },
                        Expression.Loop(
                            Expression.Block(
                                Expression.Assign(
                                    作業,
                                    Expression.Add(
                                        Expression.Constant(1m),
                                        Expression.Constant(1m)
                                    )
                                ),
                                Expression.Break(Break)
                            ),
                            Break
                        ),
                        作業
                    )
                )
            );
        }
        //    }
        //}
        //base.Traverse(Expression);
        //共通部分式処理:
        //if(Expression.Type==typeof(void))return;
        {
            var Break = Expression.Label();
            var 作業 = Expression.Variable(typeof(decimal));
            this.Execute2(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        new[] { 作業 },
                        Expression.Loop(
                            Expression.Block(
                                Expression.Assign(
                                    作業,
                                    Expression.Add(
                                        Expression.Constant(1m),
                                        Expression.Constant(1m)
                                    )
                                ),
                                Expression.Break(Break)
                            ),
                            Break
                        ),
                        作業
                    )
                )
            );
        }
        //Int32 順序0;
        //if(this.Dictionary一度出現したExpression順序.TryGetValue(Expression,out 順序0)) {
        //if(this.Dictionary二度出現したExpression順序.ContainsKey(Expression)) return;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { i },
                    Expression.Assign(
                        i,
                        Expression.Constant(0)
                    ),
                    Expression.Add(
                        Expression.Add(
                            Expression.Add(
                                i,
                                i
                            ),
                            Expression.Add(
                                i,
                                i
                            )
                        ),
                        Expression.Add(
                            i,
                            i
                        )
                    )
                )
            )
        );
        this.Execute2(() => Lambda(a => (a == 0 ? a + 1 : a + 2) + (a == 0 ? a + 1 : a + 2)));
        //}
        this.Execute2(() => Staticメソッド());
    }
}