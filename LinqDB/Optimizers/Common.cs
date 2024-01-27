using LinqDB.Helpers;

using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Optimizers.ReturnExpressionTraverser;
//using LinqDB.Optimizers.ReturnExpressionTraverser;
//using LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
//using LinqDB.Optimizers.VoidExpressionTraverser;
//using Microsoft.CSharp.RuntimeBinder;
using Delegate = System.Delegate;
// ReSharper disable All
namespace LinqDB.Optimizers;
using static System.Net.Mime.MediaTypeNames;

using Generic = System.Collections.Generic;
/// <summary>
/// Expressionを最適化する
/// </summary>
internal static class Common {
    //internal const Int32 プリフィックス長 = 5;
    //internal const String Cラムダ跨 = "ラムダ跨ﾟ";
    //internal const String Cループ跨 = "ループ跨ﾟ";
    //internal const String Cローカル = "ローカルﾟ";
    //internal static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス) => Parameter.Name is not null&&Parameter.Name.StartsWith(プリフィックス,StringComparison.Ordinal);
    //internal static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス0,String プリフィックス1)=>
    //    Parameter.Name is not null&&(Parameter.Name.StartsWith(プリフィックス0,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス1,StringComparison.Ordinal));
    //internal static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス0,String プリフィックス1,String プリフィックス2)=>
    //    Parameter.Name is not null&&(Parameter.Name.StartsWith(プリフィックス0,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス1,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス2,StringComparison.Ordinal));
    //internal const String Cラムダ局所 = nameof(Cラムダ局所);//ラムダの内部のラムダを跨がない2回以上評価される共通部分式の局所変数による最適化
    //internal const String Pラムダ引数 = nameof(Pラムダ引数);
    //internal const String Pワーク変数 = nameof(Pワーク変数);
    //internal const String Lループ = nameof(Lループ);//ループ展開されるラムダ名称
    //internal const String Lラムダ = nameof(Lラムダ);//ループ展開されないラムダ名称
    //internal const String D動的Get = nameof(D動的Get);
    //internal const String D動的Set = nameof(D動的Set);
    //internal const Int32 プレフィックス長 = 1;
    //internal const String Lラムダ跨 = ".";//自由変数,束縛変数,評価変数
    //internal const String Lループ跨 = "L";//ループに展開されるラムダを跨ぐ外だし出来る先行評価共通部分式の局所変数による最適化
    //internal const String A自動変数 = "A";//ラムダの内部のラムダを跨がない2回以上評価される共通部分式の局所変数による最適化
    //internal const String P関数引数 = "P";
    //internal const String W作業変数 = "W";
    //internal const String G動的変数 = "G";
    //internal const String S動的変数 = "S";
    //internal const String Lループ = nameof(Lループ);//ループ展開されるラムダ名称
    //internal const String Lラムダ = nameof(Lラムダ);//ループ展開されないラムダ名称
    //internal const String get_Item = nameof(get_Item);
    //internal const String op_Decrement        =nameof(op_Decrement);
    //internal const String op_Increment        =nameof(op_Increment );
    //internal const String op_Negation         =nameof(op_Negation);
    //internal const String op_UnaryNegation    =nameof(op_UnaryNegation);
    //internal const String op_UnaryPlus        =nameof(op_UnaryPlus);
    //internal const String op_Addition         =nameof(op_Addition);
    //internal const String op_Assign           =nameof(op_Assign);
    //internal const String op_BitwiseAnd=nameof(op_BitwiseAnd);
    //internal const String op_BitwiseOr=nameof(op_BitwiseOr);
    //internal const String op_Division         =nameof(op_Division);
    //internal const String op_Equality         =nameof(op_Equality);
    //internal const String op_ExclusiveOr      =nameof(op_ExclusiveOr);
    internal const string op_GreaterThan = nameof(op_GreaterThan);
    //internal const String op_GreaterThanOrEqual=nameof(op_GreaterThanOrEqual);
    //internal const String op_Inequality       =nameof(op_Inequality);
    //internal const String op_LeftShift        =nameof(op_LeftShift);
    internal const string op_LessThan = nameof(op_LessThan);
    //internal const String op_LessThanOrEqual  =nameof(op_LessThanOrEqual);
    //internal const String op_LogicalAnd       =nameof(op_LogicalAnd);
    //internal const String op_LogicalOr        =nameof(op_LogicalOr);
    //internal const String op_Modulus          =nameof(op_Modulus);
    //internal const String op_Multiply         =nameof(op_Multiply);
    //internal const String op_RightShift       =nameof(op_RightShift);
    //internal const String op_Subtraction      =nameof(op_Subtraction);
    //internal const String op_Implicit=nameof(op_Implicit);
    //internal const String op_Explicit=nameof(op_Explicit);
    internal const string op_True = nameof(op_True);
    internal const string op_False = nameof(op_False);
    //internal const BindingFlags Instance_NonPublic_Public =BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public;
    //internal const BindingFlags Static_NonPublic_Public =BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public;
    //internal const BindingFlags Static_NonPublic = BindingFlags.Static|BindingFlags.NonPublic;
    internal static readonly ConstantExpression Constant_false = Expression.Constant(false);
    internal static readonly ConstantExpression Constant_true = Expression.Constant(true);
    internal static readonly MemberExpression Constant_0M = Expression.Field(null,typeof(decimal).GetField(nameof(decimal.Zero)));
    internal static readonly MemberExpression Constant_1M = Expression.Field(null,typeof(decimal).GetField(nameof(decimal.One)));
    internal static readonly ConstantExpression Constant_0 = Expression.Constant(0);
    //internal static readonly ConstantExpression Constant_4 = Expression.Constant(4);
    //internal static readonly ConstantExpression Constant_8 = Expression.Constant(8);
    //internal static readonly ConstantExpression Constant_12 = Expression.Constant(12);
    //internal static readonly ConstantExpression Constant_13 = Expression.Constant(13);
    internal static readonly ConstantExpression Constant_100 = Expression.Constant(100);
    internal static readonly ConstantExpression Constant_100000 = Expression.Constant(100000);
    internal static readonly ConstantExpression Constant_1=Expression.Constant(1);
    internal static readonly ConstantExpression Constant_0L = Expression.Constant(0L);
    internal static readonly ConstantExpression Constant_1L=Expression.Constant(1L);
    internal static readonly ConstantExpression Constant_0F = Expression.Constant(0F);
    internal static readonly ConstantExpression Constant_1F = Expression.Constant(0F);
    internal static readonly ConstantExpression Constant_0D = Expression.Constant(0D);
    internal static readonly ConstantExpression Constant_1D = Expression.Constant(1D);
    internal static readonly ConstantExpression Constant_10D = Expression.Constant(10D);
    internal static readonly ConstantExpression Constant_null = Expression.Constant(null);
    internal static readonly DefaultExpression Default_void = Expression.Empty();
    internal const BindingFlags Instance_NonPublic_Public =BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public;
    internal const BindingFlags Static_NonPublic_Public =BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public;
    internal const BindingFlags Static_NonPublic = BindingFlags.Static|BindingFlags.NonPublic;
    internal static Expression AndAlsoで繋げる(Expression? predicate,Expression e)=>predicate is null?e:Expression.AndAlso(predicate,e);
    /// <summary>
    /// a&amp;&amp;b→operator true(a)?a&amp;b:a
    /// </summary>
    /// <param name="Left"></param>
    /// <param name="Right"></param>
    /// <returns></returns>
    internal static Expression AndAlsoに相当するCondition(Expression Left,Expression Right) {
        if(Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.And(Left,Right);
        var Type=Left.Type;
        var p=Expression.Parameter(Left.Type,"AndAlso");
        var test=Expression.Assign(p,Left);
        if(Type==typeof(bool)) {
            return Expression.Block(
                new[]{p},
                Expression.Condition(
                    test,
                    Expression.And(p,Right),
                    p
                )                
            );
        } else {
            return Expression.Block(
                new[]{p},
                Expression.Condition(
                    Expression.Call(
                        test.Type.GetMethod(op_True)!,
                        test
                    ),
                    Expression.And(p,Right),
                    p
                )
            );
        }
    }
    internal static Expression Convert必要なら(Expression e,Type Type) => Type!=e.Type
        ? Expression.Convert(
            e,
            Type
        )
        : e;
    internal static (Expression プローブ, Expression ビルド) ValueTupleでNewしてプローブとビルドに分解(作業配列 作業配列,Generic.IList<(Expression プローブ, Expression ビルド)> Listプローブビルド,int Offset) {
        var 残りType数 = Listプローブビルド.Count-Offset;
        switch(残りType数) {
            case 1:
                return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple1,
                        Listプローブビルド[Offset+0].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple1,
                        Listプローブビルド[Offset+0].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド
                    )
                )
            );
            case 2:
                return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple2,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ,Listプローブビルド[Offset+1].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple2,
                        Listプローブビルド[Offset+0].ビルド.Type,Listプローブビルド[Offset+1].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド,Listプローブビルド[Offset+1].ビルド
                    )
                )
            );
            case 3:
                return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple3,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ,Listプローブビルド[Offset+1].プローブ,Listプローブビルド[Offset+2].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple3,
                        Listプローブビルド[Offset+0].ビルド.Type,Listプローブビルド[Offset+1].ビルド.Type,Listプローブビルド[Offset+2].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド,Listプローブビルド[Offset+1].ビルド,Listプローブビルド[Offset+2].ビルド
                    )
                )
            );
            case 4:
                return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple4,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ,Listプローブビルド[Offset+1].プローブ,Listプローブビルド[Offset+2].プローブ,Listプローブビルド[Offset+3].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple4,
                        Listプローブビルド[Offset+0].ビルド.Type,Listプローブビルド[Offset+1].ビルド.Type,Listプローブビルド[Offset+2].ビルド.Type,Listプローブビルド[Offset+3].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド,Listプローブビルド[Offset+1].ビルド,Listプローブビルド[Offset+2].ビルド,Listプローブビルド[Offset+3].ビルド
                    )
                )
            );
            case 5:
                return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple5,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ,Listプローブビルド[Offset+1].プローブ,Listプローブビルド[Offset+2].プローブ,Listプローブビルド[Offset+3].プローブ,Listプローブビルド[Offset+4].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple5,
                        Listプローブビルド[Offset+0].ビルド.Type,Listプローブビルド[Offset+1].ビルド.Type,Listプローブビルド[Offset+2].ビルド.Type,Listプローブビルド[Offset+3].ビルド.Type,Listプローブビルド[Offset+4].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド,Listプローブビルド[Offset+1].ビルド,Listプローブビルド[Offset+2].ビルド,Listプローブビルド[Offset+3].ビルド,Listプローブビルド[Offset+4].ビルド
                    )
                )
            );
            case 6:
                return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple6,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ,Listプローブビルド[Offset+1].プローブ,Listプローブビルド[Offset+2].プローブ,Listプローブビルド[Offset+3].プローブ,Listプローブビルド[Offset+4].プローブ,Listプローブビルド[Offset+5].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple6,
                        Listプローブビルド[Offset+0].ビルド.Type,Listプローブビルド[Offset+1].ビルド.Type,Listプローブビルド[Offset+2].ビルド.Type,Listプローブビルド[Offset+3].ビルド.Type,Listプローブビルド[Offset+4].ビルド.Type,Listプローブビルド[Offset+5].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド,Listプローブビルド[Offset+1].ビルド,Listプローブビルド[Offset+2].ビルド,Listプローブビルド[Offset+3].ビルド,Listプローブビルド[Offset+4].ビルド,Listプローブビルド[Offset+5].ビルド
                    )
                )
            );
            case 7:
                return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple7,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type,Listプローブビルド[Offset+6].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ,Listプローブビルド[Offset+1].プローブ,Listプローブビルド[Offset+2].プローブ,Listプローブビルド[Offset+3].プローブ,Listプローブビルド[Offset+4].プローブ,Listプローブビルド[Offset+5].プローブ,Listプローブビルド[Offset+6].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple7,
                        Listプローブビルド[Offset+0].ビルド.Type,Listプローブビルド[Offset+1].ビルド.Type,Listプローブビルド[Offset+2].ビルド.Type,Listプローブビルド[Offset+3].ビルド.Type,Listプローブビルド[Offset+4].ビルド.Type,Listプローブビルド[Offset+5].ビルド.Type,Listプローブビルド[Offset+6].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド,Listプローブビルド[Offset+1].ビルド,Listプローブビルド[Offset+2].ビルド,Listプローブビルド[Offset+3].ビルド,Listプローブビルド[Offset+4].ビルド,Listプローブビルド[Offset+5].ビルド,Listプローブビルド[Offset+6].ビルド
                    )
                )
            );
            default: {
                var (プローブ, ビルド)=ValueTupleでNewしてプローブとビルドに分解(作業配列,Listプローブビルド,Offset+7);
                return (
                    Expression.New(
                        作業配列.MakeValueTuple_ctor(
                            Reflection.ValueTuple.ValueTuple8,
                            Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type,Listプローブビルド[Offset+6].プローブ.Type,
                            プローブ.Type
                        ),
                        作業配列.Expressions設定(
                            Listプローブビルド[Offset+0].プローブ,Listプローブビルド[Offset+1].プローブ,Listプローブビルド[Offset+2].プローブ,Listプローブビルド[Offset+3].プローブ,Listプローブビルド[Offset+4].プローブ,Listプローブビルド[Offset+5].プローブ,Listプローブビルド[Offset+6].プローブ,
                            プローブ
                        )
                    ),
                    Expression.New(
                        作業配列.MakeValueTuple_ctor(
                            Reflection.ValueTuple.ValueTuple8,
                            Listプローブビルド[Offset+0].ビルド.Type,Listプローブビルド[Offset+1].ビルド.Type,Listプローブビルド[Offset+2].ビルド.Type,Listプローブビルド[Offset+3].ビルド.Type,Listプローブビルド[Offset+4].ビルド.Type,Listプローブビルド[Offset+5].ビルド.Type,Listプローブビルド[Offset+6].ビルド.Type,
                            ビルド.Type
                        ),
                        作業配列.Expressions設定(
                            Listプローブビルド[Offset+0].ビルド,Listプローブビルド[Offset+1].ビルド,Listプローブビルド[Offset+2].ビルド,Listプローブビルド[Offset+3].ビルド,Listプローブビルド[Offset+4].ビルド,Listプローブビルド[Offset+5].ビルド,Listプローブビルド[Offset+6].ビルド,
                            ビルド
                        )
                    )
                );
            }
        }
    }
    //internal static NewExpression ValueTupleでNewする(作業配列 作業配列,Generic.IList<Expression> Arguments) {
    //    return CommonLibrary.ValueTupleでNewする(作業配列,Arguments);
    //}
    //internal static bool ILで直接埋め込めるか(Type Type) =>
    //    Type.IsPrimitive||Type.IsEnum||Type==typeof(string);
    /// <summary>
    /// Constant定数がILに直接埋め込めるか判定する
    /// </summary>
    /// <param name="Constant"></param>
    /// <returns>ILに埋め込めるか</returns>
    internal static bool ILで直接埋め込めるか(ConstantExpression Constant){
        if(Constant.Value is null)return true;
        //if(!Constant.Type.IsValueType)return true;
        //    if(Constant.Value is null)return true;
        if(Constant.Type.IsPrimitive)return true;
        if(Constant.Type.IsEnum)return true;
        if(Constant.Type==typeof(string))return true;
        return false;
    }
    internal static MethodCallExpression? ループ展開可能なSetのCall(Expression e) {
        if(e.NodeType!=ExpressionType.Call)
            return null;
        var MethodCall = (MethodCallExpression)e;
        return MethodCall.Method.DeclaringType==typeof(Sets.ExtensionSet)
            ? MethodCall
            : null;
    }
    internal static Expression LambdaExpressionを展開1(Expression Lambda,Expression argument,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression) {
        Debug.Assert(typeof(Delegate).IsAssignableFrom(Lambda.Type));
        return Lambda is LambdaExpression Lambda1
            ? 変換_旧Parameterを新Expression.実行(
                Lambda1.Body,
                Lambda1.Parameters[0],
                argument
            )
            : Expression.Invoke(
                Lambda,
                argument
            );
    }




    public static MethodInfo GetGenericMethodDefinition(MethodInfo Method){
        if(Method.IsGenericMethod){
            return Method.GetGenericMethodDefinition();
        } else{
            return Method;
        }
    }
    internal static bool ループ展開可能メソッドか(MethodInfo GenericMethodDefinition) {
        Debug.Assert(!GenericMethodDefinition.IsGenericMethod||GenericMethodDefinition.IsGenericMethodDefinition);
        var DeclaringType = GenericMethodDefinition.DeclaringType;
        if(typeof(Enumerable)==DeclaringType) {
            var Name = GenericMethodDefinition.Name;
            if(
                nameof(Enumerable.ToArray)==Name||
                nameof(Enumerable.DistinctBy)==Name||
                nameof(Enumerable.ExceptBy)==Name||
                nameof(Enumerable.IntersectBy)==Name||
                nameof(Enumerable.UnionBy)==Name||
                nameof(Enumerable.MaxBy)==Name||
                nameof(Enumerable.MinBy)==Name||
                nameof(Enumerable.Empty)==Name||
                nameof(Enumerable.OrderBy)==Name||nameof(Enumerable.OrderByDescending)==Name||
                nameof(Enumerable.ThenBy)==Name||nameof(Enumerable.ThenByDescending)==Name
            ) {
                return false;
            }
            return true;
        }
        return typeof(Sets.ExtensionEnumerable)==DeclaringType||typeof(Sets.ExtensionSet)==DeclaringType;
    }
    internal static bool ループ展開可能メソッドか(Expression Expression,out MethodCallExpression MethodCall) {
        if(Expression is MethodCallExpression MethodCall0) {
            MethodCall=MethodCall0;
            return ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall.Method));
        }
        MethodCall=null!;
        return false;
    }
    internal static bool ループ展開可能メソッドか(MethodCallExpression MethodCall) =>
        ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall.Method));
    internal static Type IEnumerable1のT(Type Type)
    {
        //if(Type==typeof(XDocument)) return typeof(XDocument);
        var IEnumerable1 = Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
        if(IEnumerable1 is not null) {
            return IEnumerable1.GetGenericArguments()[0];
        }
        if(Type.IsGenericType&&typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition()) {
            return Type.GetGenericArguments()[0];
        }
        var IEnumerable = Type.GetInterface(CommonLibrary.Collections_IEnumerable_FullName);
        if(IEnumerable is not null||typeof(System.Collections.IEnumerable)==Type) {
            return typeof(object);
        }
        throw new NotSupportedException();
    }
    internal static Type[] IEnumerable1のGenericArguments(Type Type) {
        var IEnumerable1 = Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
        if(IEnumerable1 is not null) {
            return IEnumerable1.GetGenericArguments();
        }
        if(Type.IsGenericType&&typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition()) {
            return Type.GetGenericArguments();
        }
        throw new NotSupportedException();
    }
    internal static Type IEnumerable1(Type Type) {
        var IEnumerable1 = typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition()
            ? Type
            : Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
        if(IEnumerable1 is not null) {
            return IEnumerable1;
        }
        return typeof(System.Collections.IEnumerable)==Type
            ? Type
            : Type.GetInterface(CommonLibrary.Collections_IEnumerable_FullName)!;
    }
    public static Expression GetValueOrDefault(this Expression 入力)=>Expression.Call(入力,入力.Type.GetMethod("GetValueOrDefault",Type.EmptyTypes));
}
//2122 20220516
//2708 20220514
//3186 20220513
//2773 20220504
