using LinqDB.Databases.Dom;
using LinqDB.Helpers;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Array = System.Array;
//using Microsoft.CSharp.RuntimeBinder;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;
using AssemblyGenerator = Lokad.ILPack.AssemblyGenerator;
using Container = LinqDB.Databases.Container;
using Delegate = System.Delegate;
using ExtensionEnumerable = LinqDB.Reflection.ExtensionEnumerable;
using ExtensionSet = LinqDB.Reflection.ExtensionSet;
using Regex = System.Text.RegularExpressions.Regex;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.CSharp.RuntimeBinder;
// ReSharper disable All
namespace LinqDB.Optimizers;
using Generic=System.Collections.Generic;
/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer:IDisposable{
    //private const Int32 プリフィックス長 = 5;
    //private const String Cラムダ跨 = "ラムダ跨ﾟ";
    //private const String Cループ跨 = "ループ跨ﾟ";
    //private const String Cローカル = "ローカルﾟ";
    //private static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス) => Parameter.Name is not null&&Parameter.Name.StartsWith(プリフィックス,StringComparison.Ordinal);
    //private static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス0,String プリフィックス1)=>
    //    Parameter.Name is not null&&(Parameter.Name.StartsWith(プリフィックス0,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス1,StringComparison.Ordinal));
    //private static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス0,String プリフィックス1,String プリフィックス2)=>
    //    Parameter.Name is not null&&(Parameter.Name.StartsWith(プリフィックス0,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス1,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス2,StringComparison.Ordinal));
    //private const String Cラムダ局所 = nameof(Cラムダ局所);//ラムダの内部のラムダを跨がない2回以上評価される共通部分式の局所変数による最適化
    //private const String Pラムダ引数 = nameof(Pラムダ引数);
    //private const String Pワーク変数 = nameof(Pワーク変数);
    //private const String Lループ = nameof(Lループ);//ループ展開されるラムダ名称
    //private const String Lラムダ = nameof(Lラムダ);//ループ展開されないラムダ名称
    //private const String D動的Get = nameof(D動的Get);
    //private const String D動的Set = nameof(D動的Set);
    //private const Int32 プレフィックス長 = 1;
    //private const String Lラムダ跨 = ".";//自由変数,束縛変数,評価変数
    //private const String Lループ跨 = "L";//ループに展開されるラムダを跨ぐ外だし出来る先行評価共通部分式の局所変数による最適化
    //private const String A自動変数 = "A";//ラムダの内部のラムダを跨がない2回以上評価される共通部分式の局所変数による最適化
    //private const String P関数引数 = "P";
    //private const String W作業変数 = "W";
    //private const String G動的変数 = "G";
    //private const String S動的変数 = "S";
    //private const String Lループ = nameof(Lループ);//ループ展開されるラムダ名称
    //private const String Lラムダ = nameof(Lラムダ);//ループ展開されないラムダ名称
    //private const String get_Item = nameof(get_Item);
    //private const String op_Decrement        =nameof(op_Decrement);
    //private const String op_Increment        =nameof(op_Increment );
    //private const String op_Negation         =nameof(op_Negation);
    //private const String op_UnaryNegation    =nameof(op_UnaryNegation);
    //private const String op_UnaryPlus        =nameof(op_UnaryPlus);
    //private const String op_Addition         =nameof(op_Addition);
    //private const String op_Assign           =nameof(op_Assign);
    //private const String op_BitwiseAnd=nameof(op_BitwiseAnd);
    //private const String op_BitwiseOr=nameof(op_BitwiseOr);
    //private const String op_Division         =nameof(op_Division);
    //private const String op_Equality         =nameof(op_Equality);
    //private const String op_ExclusiveOr      =nameof(op_ExclusiveOr);
    private const string op_GreaterThan = nameof(op_GreaterThan);
    //private const String op_GreaterThanOrEqual=nameof(op_GreaterThanOrEqual);
    //private const String op_Inequality       =nameof(op_Inequality);
    //private const String op_LeftShift        =nameof(op_LeftShift);
    private const string op_LessThan = nameof(op_LessThan);
    //private const String op_LessThanOrEqual  =nameof(op_LessThanOrEqual);
    //private const String op_LogicalAnd       =nameof(op_LogicalAnd);
    //private const String op_LogicalOr        =nameof(op_LogicalOr);
    //private const String op_Modulus          =nameof(op_Modulus);
    //private const String op_Multiply         =nameof(op_Multiply);
    //private const String op_RightShift       =nameof(op_RightShift);
    //private const String op_Subtraction      =nameof(op_Subtraction);
    //private const String op_Implicit=nameof(op_Implicit);
    //private const String op_Explicit=nameof(op_Explicit);
    private const string op_True = nameof(op_True);
    private const string op_False = nameof(op_False);
    private const BindingFlags Instance_NonPublic_Public =BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public;
    private const BindingFlags Static_NonPublic_Public =BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public;
    private const BindingFlags Static_NonPublic = BindingFlags.Static|BindingFlags.NonPublic;
    private static readonly ConstantExpression Constant_false = Expression.Constant(false);
    private static readonly ConstantExpression Constant_true = Expression.Constant(true);
    private static readonly MemberExpression Constant_0M = Expression.Field(null,typeof(decimal).GetField(nameof(decimal.Zero)));
    private static readonly MemberExpression Constant_1M = Expression.Field(null,typeof(decimal).GetField(nameof(decimal.One)));
    private static readonly ConstantExpression Constant_0 = Expression.Constant(0);
    //private static readonly ConstantExpression Constant_4 = Expression.Constant(4);
    //private static readonly ConstantExpression Constant_8 = Expression.Constant(8);
    //private static readonly ConstantExpression Constant_12 = Expression.Constant(12);
    //private static readonly ConstantExpression Constant_13 = Expression.Constant(13);
    private static readonly ConstantExpression Constant_100 = Expression.Constant(100);
    private static readonly ConstantExpression Constant_100000 = Expression.Constant(100000);
    private static readonly ConstantExpression Constant_1=Expression.Constant(1);
    private static readonly ConstantExpression Constant_0L = Expression.Constant(0L);
    private static readonly ConstantExpression Constant_1L=Expression.Constant(1L);
    private static readonly ConstantExpression Constant_0F = Expression.Constant(0F);
    private static readonly ConstantExpression Constant_1F = Expression.Constant(0F);
    private static readonly ConstantExpression Constant_0D = Expression.Constant(0D);
    private static readonly ConstantExpression Constant_1D = Expression.Constant(1D);
    private static readonly ConstantExpression Constant_10D = Expression.Constant(10D);
    private static readonly ConstantExpression Constant_null = Expression.Constant(null);
    private static readonly DefaultExpression Default_void = Expression.Empty();
    private static Expression Convert必要なら(Expression e,Type Type) => Type!=e.Type
        ? Expression.Convert(
            e,
            Type
        )
        : e;
    private static (Expression プローブ,Expression ビルド)ValueTupleでNewする(作業配列 作業配列,Generic.IList<(Expression プローブ, Expression ビルド)> Listプローブビルド,int Offset) {
        var 残りType数 = Listプローブビルド.Count-Offset;
        switch(残りType数) {
            case 1:return (
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
            case 2:return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple2,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple2,
                        Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド
                    )
                )
            );
            case 3:return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple3,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple3,
                        Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド
                    )
                )
            );
            case 4:return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple4,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple4,
                        Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド
                    )
                )
            );
            case 5:return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple5,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple5,
                        Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド
                    )
                )
            );
            case 6:return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple6,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ     ,Listプローブビルド[Offset+5].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple6,
                        Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type  ,Listプローブビルド[Offset+5].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド       ,Listプローブビルド[Offset+5].ビルド
                    )
                )
            );
            case 7:return (
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple7,
                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type,Listプローブビルド[Offset+6].プローブ.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ     ,Listプローブビルド[Offset+5].プローブ     ,Listプローブビルド[Offset+6].プローブ
                    )
                ),
                Expression.New(
                    作業配列.MakeValueTuple_ctor(
                        Reflection.ValueTuple.ValueTuple7,
                        Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type  ,Listプローブビルド[Offset+5].ビルド.Type  ,Listプローブビルド[Offset+6].ビルド.Type
                    ),
                    作業配列.Expressions設定(
                        Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド       ,Listプローブビルド[Offset+5].ビルド       ,Listプローブビルド[Offset+6].ビルド
                    )
                )
            );
            default: {
                var (プローブ, ビルド)=ValueTupleでNewする(作業配列,Listプローブビルド,Offset+7);
                return (
                    Expression.New(
                        作業配列.MakeValueTuple_ctor(
                            Reflection.ValueTuple.ValueTuple8,
                            Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type,Listプローブビルド[Offset+6].プローブ.Type,
                            プローブ.Type
                        ),
                        作業配列.Expressions設定(
                            Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ     ,Listプローブビルド[Offset+5].プローブ     ,Listプローブビルド[Offset+6].プローブ     ,
                            プローブ
                        )
                    ),
                    Expression.New(
                        作業配列.MakeValueTuple_ctor(
                            Reflection.ValueTuple.ValueTuple8,
                            Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type  ,Listプローブビルド[Offset+5].ビルド.Type  ,Listプローブビルド[Offset+6].ビルド.Type  ,
                            ビルド.Type
                        ) ,
                        作業配列.Expressions設定(
                            Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド       ,Listプローブビルド[Offset+5].ビルド       ,Listプローブビルド[Offset+6].ビルド       ,
                            ビルド
                        )
                    )
                );
            }
        }
    }
    private static NewExpression ValueTupleでNewする(作業配列 作業配列,Generic.IList<Expression> Arguments) {
        return CommonLibrary.ValueTupleでNewする(作業配列,Arguments);
    }
    private static bool ILで直接埋め込めるか(Type Type) =>
        Type.IsPrimitive||Type.IsEnum||Type==typeof(string);
    /// <summary>
    /// Constant定数がILに直接埋め込めるか判定する
    /// </summary>
    /// <param name="Constant"></param>
    /// <returns>ILに埋め込めるか</returns>
    private static bool ILで直接埋め込めるか(ConstantExpression Constant) =>
        !Constant.Type.IsValueType&&Constant.Value is null||ILで直接埋め込めるか(Constant.Type);
    private static MethodCallExpression? ループ展開可能なSetのCall(Expression e) {
        if(e.NodeType!=ExpressionType.Call)
            return null;
        var MethodCall = (MethodCallExpression)e;
        return MethodCall.Method.DeclaringType==typeof(Sets.ExtensionSet)
            ? MethodCall
            : null;
    }
    private static bool ループ展開可能メソッドか(MethodInfo GenericMethodDefinition) {
        Debug.Assert(!GenericMethodDefinition.IsGenericMethod||GenericMethodDefinition.IsGenericMethodDefinition);
        var DeclaringType = GenericMethodDefinition.DeclaringType;
        if(typeof(Enumerable)==DeclaringType) {
            var Name = GenericMethodDefinition.Name;
            if(
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
    private static bool ループ展開可能メソッドか(Expression Expression,out MethodCallExpression MethodCall) {
        if(Expression is MethodCallExpression MethodCall0) {
            MethodCall=MethodCall0;
            return ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall.Method));
        }
        MethodCall=null!;
        return false;
    }
    private static bool ループ展開可能メソッドか(MethodCallExpression MethodCall) =>
        ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall.Method));
    private static Expression LambdaExpressionを展開1(Expression Lambda,Expression argument,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression) {
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
    ///// <summary>
    ///// テストプロジェクト用に公開するExpressionを比較するメソッド。
    ///// </summary>
    ///// <param name="a"></param>
    ///// <param name="b"></param>
    ///// <returns></returns>
    //public static bool Test_ExpressionEqualityComparer(Expression a,Expression b) =>
    //    new ExpressionEqualityComparer(new List<ParameterExpression>()).Equals(a,b);
    /// <summary>
    /// ビルド,プローブ式木の等価を比較する
    /// </summary>
    public class ブローブビルドExpressionEqualityComparer:Generic.IEqualityComparer<(Expression ビルド,Expression プローブ)> {
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        public ブローブビルドExpressionEqualityComparer(ExpressionEqualityComparer ExpressionEqualityComparer) => this.ExpressionEqualityComparer=ExpressionEqualityComparer;
        public bool Equals((Expression ビルド,Expression プローブ) x,(Expression ビルド,Expression プローブ) y) {
            var ExpressionEqualityComparer=this.ExpressionEqualityComparer;
            if(!ExpressionEqualityComparer.Equals(x.プローブ,y.プローブ)) return false;
            if(!ExpressionEqualityComparer.Equals(x.ビルド,y.ビルド)) return false;
            return true;
        }
        public int GetHashCode((Expression ビルド, Expression プローブ) obj) =>0;
    }
    /// <summary>
    /// 式木の等価を比較する
    /// </summary>
    public class ExpressionEqualityComparer:Generic.IEqualityComparer<Expression>,Generic.IEqualityComparer<LabelTarget>,Generic.IEqualityComparer<CatchBlock>,Generic.IEqualityComparer<CSharpArgumentInfo>,Generic.IEqualityComparer<SwitchCase>{
        private readonly EnumerableSetEqualityComparer ObjectComparer;
        /// <summary>
        /// 比較するときに可視パラメーター
        /// </summary>
        internal readonly Generic.IList<ParameterExpression> スコープParameters;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="スコープParameters"></param>
        public ExpressionEqualityComparer(Generic.IList<ParameterExpression> スコープParameters){
            this.スコープParameters=スコープParameters;
            this.ObjectComparer=new(this);
        }
        public ExpressionEqualityComparer():this(new Generic.List<ParameterExpression>()){
        }

        /// <summary>
        /// 式木のハッシュコード。NodeTypeとTypeを使う。
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual int GetHashCode(Expression e) {
            var NodeType = e.NodeType;
            switch(NodeType) {
                case ExpressionType.Call:{
                    var Method=((MethodCallExpression)e).Method;
                    if(Method is DynamicMethod) return 0;
                    return Method.MetadataToken;
                }
                case ExpressionType.Constant:
                    var Constant = (ConstantExpression)e;
                    if(Constant.Value is not null)
                        return Constant.Value.GetHashCode();
                    break;
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)e).Member.MetadataToken;
                case ExpressionType.New:
                    var Constructor=((NewExpression)e).Constructor;
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if(Constructor is not null) return Constructor.MetadataToken;
                    break;
            }
            return (int)e.NodeType+e.Type.MetadataToken.GetHashCode();
        }
        //private readonly List<int> a_Indexes= new();
        //private readonly List<int> b_Indexes= new();
        private Generic.List<ParameterExpression> a_ラムダ跨ぎParameters= new();
        private Generic.List<ParameterExpression> b_ラムダ跨ぎParameters= new();
        private readonly Generic.List<ParameterExpression> a_Parameters = new();
        private readonly Generic.List<ParameterExpression> b_Parameters = new();
        private readonly Generic.List<LabelTarget> a_LabelTargets = new();
        private readonly Generic.List<LabelTarget> b_LabelTargets = new();
        /// <summary>
        /// 式木同士が一致するか。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(Expression? a,Expression? b) {
            if(a==b) return true;
            if(a==null&&b!=null||a!=null&&b==null) return false;
            this.a_ラムダ跨ぎParameters.Clear();
            this.b_ラムダ跨ぎParameters.Clear();
            this.a_Parameters.Clear();
            this.b_Parameters.Clear();
            this.a_LabelTargets.Clear();
            this.b_LabelTargets.Clear();
            var r=this.InternalEquals(a,b);
            return r;
        }

        /// <summary>
        /// Cラムダ局所に代入している場合はその左辺で比較したい。デフォルトではそのままAssign式を比較する。「
        /// </summary>
        /// <param name="Expression0"></param>
        /// <returns></returns>
        protected virtual Expression Assignの比較対象(Expression Expression0)=>Expression0;
        private bool InternalEquals(Expression? a0,Expression? b0) {
            if(a0 is null)
                return b0 is null;
            if(b0 is null)
                return false;
            return this.PrivateEquals(a0,b0);
        }
        private bool PrivateEquals(Expression? a0,Expression? b0){
            var a1=this.Assignの比較対象(a0);
            var b1=this.Assignの比較対象(b0);
            if(a1.NodeType!=b1?.NodeType||a1.Type!=b1.Type)
                return false;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch(a1.NodeType) {
                case ExpressionType.Add:
                case ExpressionType.AddAssign:
                case ExpressionType.AddAssignChecked:
                case ExpressionType.AddChecked:
                case ExpressionType.And:
                case ExpressionType.AndAssign:
                case ExpressionType.AndAlso:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Coalesce:
                case ExpressionType.Divide:
                case ExpressionType.DivideAssign:
                case ExpressionType.Equal:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.ExclusiveOrAssign:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.LeftShiftAssign:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Modulo:
                case ExpressionType.ModuloAssign:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyAssign:
                case ExpressionType.MultiplyAssignChecked:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.NotEqual:
                case ExpressionType.Or:
                case ExpressionType.OrAssign:
                case ExpressionType.OrElse:
                case ExpressionType.Power:
                case ExpressionType.PowerAssign:
                case ExpressionType.RightShift:
                case ExpressionType.RightShiftAssign:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractAssign:
                case ExpressionType.SubtractAssignChecked:
                case ExpressionType.SubtractChecked:
                    return this.T((BinaryExpression)a1,(BinaryExpression)b1);
                case ExpressionType.Assign: {
                    var a_Assign = (BinaryExpression)a1;
                    var b_Assign = (BinaryExpression)b1;
                    var a_Left = a_Assign.Left;
                    var b_Left = b_Assign.Left;
                    if(a_Left.NodeType!=b_Left.NodeType) return false;
                    if(a_Left.NodeType!=ExpressionType.Parameter) return this.T(a_Assign,b_Assign);
                    if(!this.PrivateEquals(a_Assign.Right,b_Assign.Right))
                        return false;
                    if(!this.InternalEquals(a_Assign.Conversion,b_Assign.Conversion))
                        return false;
                    var a_Parameter= (ParameterExpression)a_Left;
                    var b_Parameter= (ParameterExpression)b_Left;
                    var a_Index0 = this.a_Parameters.IndexOf(a_Parameter);
                    var b_Index0 = this.b_Parameters.IndexOf(b_Parameter);
                    if(a_Index0!=b_Index0) return false;
                    if(a_Index0>=0) return true;
                    var スコープParameters = this.スコープParameters;
                    var a_Index1 = スコープParameters.IndexOf(a_Parameter);
                    var b_Index1 = スコープParameters.IndexOf(b_Parameter);
                    if(a_Index1!=b_Index1) return false;
                    if(a_Index1>=0) return true;
                    //Let(a=>.v),Let(b=>.w)は一致しない。
                    //Let(a=>.v=a),Let(b=>.w=b)は一致する。大域変数.v,.wが代入左辺地なら一致する。
                    var a_ラムダ跨ぎParameters = this.a_ラムダ跨ぎParameters;
                    var b_ラムダ跨ぎParameters = this.b_ラムダ跨ぎParameters;
                    Debug.Assert(a_ラムダ跨ぎParameters is null&&b_ラムダ跨ぎParameters is null||
                                 a_ラムダ跨ぎParameters is not null&&b_ラムダ跨ぎParameters is not null);
                    if(a_ラムダ跨ぎParameters is null)return false;
                    if(a_ラムダ跨ぎParameters.Contains(a_Parameter)) return b_ラムダ跨ぎParameters!.Contains(b_Parameter);
                    if(b_ラムダ跨ぎParameters!.Contains(b_Parameter)) return false;
                    a_ラムダ跨ぎParameters.Add(a_Parameter);
                    b_ラムダ跨ぎParameters.Add(b_Parameter);
                    return this.PrivateEquals(a_Assign.Right,b_Assign.Right);
                }
                case ExpressionType.ArrayLength:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Decrement:
                case ExpressionType.Increment:
                case ExpressionType.IsFalse:
                case ExpressionType.IsTrue:
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.OnesComplement:
                case ExpressionType.PostDecrementAssign:
                case ExpressionType.PostIncrementAssign:
                case ExpressionType.PreDecrementAssign:
                case ExpressionType.PreIncrementAssign:
                case ExpressionType.Quote:
                case ExpressionType.Throw:
                case ExpressionType.TypeAs:
                case ExpressionType.UnaryPlus:
                case ExpressionType.Unbox:
                    return this.T((UnaryExpression)a1,(UnaryExpression)b1);
                case ExpressionType.Block:
                    return this.T((BlockExpression)a1,(BlockExpression)b1);
                case ExpressionType.Conditional:
                    return this.T((ConditionalExpression)a1,(ConditionalExpression)b1);
                case ExpressionType.Constant:
                    return this.T((ConstantExpression)a1,(ConstantExpression)b1);
                case ExpressionType.DebugInfo:
                    return this.T((DebugInfoExpression)a1,(DebugInfoExpression)b1);
                case ExpressionType.Default:
                    return this.T((DefaultExpression)a1,(DefaultExpression)b1);
                case ExpressionType.Dynamic:
                    return this.T((DynamicExpression)a1,(DynamicExpression)b1);
                case ExpressionType.Goto:
                    return this.T((GotoExpression)a1,(GotoExpression)b1);
                case ExpressionType.Index:
                    return this.T((IndexExpression)a1,(IndexExpression)b1);
                case ExpressionType.Invoke:
                    return this.T((InvocationExpression)a1,(InvocationExpression)b1);
                case ExpressionType.Label:
                    return this.T((LabelExpression)a1,(LabelExpression)b1);
                case ExpressionType.Lambda:
                    return this.T((LambdaExpression)a1,(LambdaExpression)b1);
                case ExpressionType.ListInit:
                    return this.T((ListInitExpression)a1,(ListInitExpression)b1);
                case ExpressionType.Loop:
                    return this.T((LoopExpression)a1,(LoopExpression)b1);
                case ExpressionType.MemberAccess:
                    return this.T((MemberExpression)a1,(MemberExpression)b1);
                case ExpressionType.MemberInit:
                    return this.T((MemberInitExpression)a1,(MemberInitExpression)b1);
                case ExpressionType.Call:
                    return this.T((MethodCallExpression)a1,(MethodCallExpression)b1);
                case ExpressionType.NewArrayBounds:
                case ExpressionType.NewArrayInit:
                    return this.T((NewArrayExpression)a1,(NewArrayExpression)b1);
                case ExpressionType.New:
                    return this.T((NewExpression)a1,(NewExpression)b1);
                case ExpressionType.Parameter:
                    return this.T((ParameterExpression)a1,(ParameterExpression)b1);
                case ExpressionType.RuntimeVariables:
                    return this.T((RuntimeVariablesExpression)a1,(RuntimeVariablesExpression)b1);
                case ExpressionType.Switch:
                    return this.T((SwitchExpression)a1,(SwitchExpression)b1);
                case ExpressionType.Try:
                    return this.T((TryExpression)a1,(TryExpression)b1);
                case ExpressionType.TypeEqual:
                case ExpressionType.TypeIs:
                    return this.T((TypeBinaryExpression)a1,(TypeBinaryExpression)b1);
                default:
                    throw new NotSupportedException($"{a1.NodeType}はサポートされていない");
            }
        }
        private bool T(BinaryExpression a,BinaryExpression b) => a.Method==b.Method&&this.PrivateEquals(a.Left,b.Left)&&this.PrivateEquals(a.Right,b.Right)&&this.InternalEquals(a.Conversion,b.Conversion);
        private bool T(BlockExpression a,BlockExpression b){
            //if(!this.T(a.Variables,b.Variables)) return false;
            //return this.SequenceEqual(a.Expressions,b.Expressions);
            if(a.Type!=b.Type) return false;
            var a_Variables = a.Variables;
            var b_Variables = b.Variables;
            var a_Variables_Count = a_Variables.Count;
            var b_Variables_Count = b_Variables.Count;
            if(a_Variables_Count!=b_Variables_Count)return false;
            var a_Parameters = this.a_Parameters;
            var b_Parameters = this.b_Parameters;
            for(var i = 0;i<a_Variables_Count;i++) {
                var a_Variable = a_Variables[i];
                var b_Variable = b_Variables[i];
                if(a_Variable.Type!=b_Variable.Type)return false;
            }
            var a_Parameters_Count = a_Parameters.Count;
            Debug.Assert(a_Parameters_Count==b_Parameters.Count);
            a_Parameters.AddRange(a_Variables);
            b_Parameters.AddRange(b_Variables);
            var r = this.SequenceEqual(a.Expressions,b.Expressions);
            a_Parameters.RemoveRange(a_Parameters_Count,a_Variables_Count);
            b_Parameters.RemoveRange(a_Parameters_Count,a_Variables_Count);
            return r;
        }
        private bool T(ConditionalExpression a,ConditionalExpression b) =>
            this.PrivateEquals(a.Test,b.Test)&&
            this.PrivateEquals(a.IfTrue,b.IfTrue)&&
            this.PrivateEquals(a.IfFalse,b.IfFalse);
        private bool T(ConstantExpression a,ConstantExpression b)=>
            ReferenceEquals(a.Value,b.Value)||
            a.Type==b.Type&&this.ObjectComparer.Equals(a.Value,b.Value);
            //Equals(a.Value,b.Value);
        private bool T(SymbolDocumentInfo a,SymbolDocumentInfo b) =>
            a.DocumentType==b.DocumentType&&
            a.Language==b.Language&&
            a.FileName==b.FileName;
        private bool T(DebugInfoExpression a,DebugInfoExpression b) =>
            this.T(a.Document,b.Document)&&
            a.StartLine==b.StartLine&&
            a.StartColumn==b.StartColumn&&
            a.EndLine==b.EndLine&&
            a.EndColumn==b.EndColumn;
        private bool T(DefaultExpression a,DefaultExpression b) => a.Type==b.Type;
        private bool T(DynamicExpression a,DynamicExpression b) {
            if(!this.SequenceEqual(a.Arguments,b.Arguments))
                return false;
            var a_Binder = a.Binder;
            var b_Binder = b.Binder;
            Debug.Assert(a_Binder.GetType()==b_Binder.GetType(),"SequenceEqualの抜け穴パターンがあるか？");
            switch(a_Binder,b_Binder){
                case(DynamicMetaObjectBinder a0,DynamicMetaObjectBinder b0):{
                    if(a0.ReturnType!=b0.ReturnType)return false;
                    switch(a0, b0) {
                        case (BinaryOperationBinder a1, BinaryOperationBinder b1): {
                            if(a1.Operation!=b1.Operation) return false;
                            return true;
                        }
                        case (ConvertBinder a1, ConvertBinder b1): {
                            if(a1.Explicit!=b1.Explicit) return false;
                            Debug.Assert(a1.ReturnType==a1.Type);
                            Debug.Assert(b1.ReturnType==b1.Type);
                            return true;
                        }
                        case (CreateInstanceBinder a1, CreateInstanceBinder b1): {
                            if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return false;
                            if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames))return false;
                            return true;
                        }
                        case (DeleteIndexBinder a1, DeleteIndexBinder b1): {
                            if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return false;
                            if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames))return false;
                            return true;
                        }
                        case (DeleteMemberBinder a1, DeleteMemberBinder b1): {
                            if(a1.IgnoreCase!=b1.IgnoreCase) return false;
                            if(a1.Name!=b1.Name) return false;
                            return true;
                        }
                        case (GetIndexBinder a1, GetIndexBinder b1): {
                            if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return false;
                            if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames))return false;
                            return true;
                        }
                        case (GetMemberBinder a1, GetMemberBinder b1): {
                            Debug.Assert(a1.IgnoreCase==b1.IgnoreCase,"GetMemberBinder 本当はVBとかで破るパターンあるんじゃないのか");
                            if(a1.IgnoreCase!=b1.IgnoreCase) return false;
                            if(a1.Name!=b1.Name)return false;
                            return true;
                        }
                        case (InvokeBinder a1, InvokeBinder b1): {
                            if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return false;
                            if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames))return false;
                            return true;
                        }
                        case (InvokeMemberBinder a1, InvokeMemberBinder b1): {
                            Debug.Assert(a1.IgnoreCase==b1.IgnoreCase,"InvokeMemberBinder 本当はVBとかで破るパターンあるんじゃないのか");
                            if(a1.Name!=b1.Name) return false;
                            if(a1.IgnoreCase!=b1.IgnoreCase) return false;
                            if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames))return false;
                            return true;
                        }
                        case (SetIndexBinder a1, SetIndexBinder b1): {
                            Debug.Assert(a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames),"CallInfo.Argumentsが違うパターンもあるんじゃないか");
                            if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return false;
                            if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames))return false;
                            return true;
                        }
                        case (SetMemberBinder a1, SetMemberBinder b1): {
                            Debug.Assert(a1.IgnoreCase==b1.IgnoreCase,"本当はVBとかで破るパターンあるんじゃないのか");
                            return a1.Name.Equals(b1.Name,StringComparison.Ordinal);
                        }
                        case (UnaryOperationBinder a1, UnaryOperationBinder b1): {
                            if(a1.Operation!=b1.Operation) return false;
                            return true;
                        }
                        default:
                            throw new NotSupportedException($"{a0.GetType()},{b0.GetType()}が一致しない");
                    }
                }
                default:
                    throw new NotSupportedException($"{a.GetType()},{b.GetType()}が一致しない");
            }
        }
        private bool InitializersEquals(ReadOnlyCollection<ElementInit> a_Initializers,
            ReadOnlyCollection<ElementInit> b_Initializers) {
            if(a_Initializers.Count!=b_Initializers.Count)
                return false;
            var a_Initializers_Count = a_Initializers.Count;
            for(var c = 0;c<a_Initializers_Count;c++) {
                var a = a_Initializers[c];
                var b = b_Initializers[c];
                if(a.AddMethod!=b.AddMethod)
                    return false;
                if(!this.SequenceEqual(a.Arguments,b.Arguments))
                    return false;
            }
            return true;
        }
        private bool MemberBindingsEquals(ReadOnlyCollection<MemberBinding> a_Bindings,
            ReadOnlyCollection<MemberBinding> b_Bindings) {
            if(a_Bindings.Count!=b_Bindings.Count)
                return false;
            var a_Bindings_Count = a_Bindings.Count;
            for(var c = 0;c<a_Bindings_Count;c++) {
                var a = a_Bindings[c];
                var b = b_Bindings[c];
                //Debug.Assert(a is not null&&b is not null);
                if(a.BindingType!=b.BindingType)
                    return false;
                switch(a.BindingType) {
                    case MemberBindingType.Assignment: {
                        var a1 = (MemberAssignment)a;
                        var b1 = (MemberAssignment)b;
                        if(a1.Member!=b1.Member)
                            return false;
                        if(!this.PrivateEquals(a1.Expression,b1.Expression))
                            return false;
                        break;
                    }
                    case MemberBindingType.MemberBinding: {
                        var a1 = (MemberMemberBinding)a;
                        var b1 = (MemberMemberBinding)b;
                        if(a1.Member!=b1.Member)
                            return false;
                        if(!this.MemberBindingsEquals(a1.Bindings,b1.Bindings))
                            return false;
                        break;
                    }
                    case MemberBindingType.ListBinding: {
                        var a1 = (MemberListBinding)a;
                        var b1 = (MemberListBinding)b;
                        if(a1.Member!=b1.Member)
                            return false;
                        if(!this.InitializersEquals(a1.Initializers,b1.Initializers))
                            return false;
                        break;
                    }
                    default:
                        throw new NotSupportedException($"{a.BindingType}はサポートされてない");
                }
            }
            return true;
        }
        private bool T(MemberInitExpression a,MemberInitExpression b) =>
            this.PrivateEquals(a.NewExpression,b.NewExpression)&&
            this.MemberBindingsEquals(a.Bindings,b.Bindings);
        private bool T(NewArrayExpression a,NewArrayExpression b) => this.SequenceEqual(a.Expressions,b.Expressions);
        //パラメータが同じならオーバーロードでコンストラクタが異なることはあり得ない。
        //しかし異なる型の同じパラメータコンストラクタはあり得る。
        private bool T(NewExpression a,NewExpression b) =>
            this.SequenceEqual(a.Arguments,b.Arguments)&&
            a.Constructor==b.Constructor;
        public bool Equals(LabelTarget? a,LabelTarget? b){
            if(a==b) return true;
            if(a==null&&b!=null||a!=null&&b==null) return false;
            return a!.Type==b!.Type&&a.Name==b.Name&&
                this.a_LabelTargets.IndexOf(a)==this.b_LabelTargets.IndexOf(b);
        }
        private bool T(LabelExpression a,LabelExpression b){
            if(!this.Equals(a.Target,b.Target)) return false;
            var a_LabelTargets = this.a_LabelTargets;
            var b_LabelTargets = this.b_LabelTargets;
            var a_LabelTargets_Count = a_LabelTargets.Count;
            var b_LabelTargets_Count = b_LabelTargets.Count;
            Debug.Assert(a_LabelTargets_Count==b_LabelTargets_Count);
            a_LabelTargets.Add(a.Target);
            b_LabelTargets.Add(b.Target);
            var r = this.InternalEquals(a.DefaultValue,b.DefaultValue);
            a_LabelTargets.RemoveRange(a_LabelTargets_Count,1);
            b_LabelTargets.RemoveRange(a_LabelTargets_Count,1);
            return r;
        }
        private bool T(LambdaExpression a,LambdaExpression b){
            if(a.Type!=b.Type||a.TailCall!=b.TailCall) return false;
            var a_ラムダ跨ぎParameters=this.a_ラムダ跨ぎParameters;
            var b_ラムダ跨ぎParameters=this.b_ラムダ跨ぎParameters;
            this.a_ラムダ跨ぎParameters=new();
            this.b_ラムダ跨ぎParameters=new();
            var Lambda_a_Parameters = a.Parameters;
            var Lambda_b_Parameters = b.Parameters;
            var Lambda_a_Parameters_Count = Lambda_a_Parameters.Count;
            if(Lambda_a_Parameters_Count!=Lambda_b_Parameters.Count) return false;
            var a_Parameters = this.a_Parameters;
            var b_Parameters = this.b_Parameters;
            var a_Parameters_Count = a_Parameters.Count;
            Debug.Assert(a_Parameters_Count==b_Parameters.Count);
            a_Parameters.AddRange(Lambda_a_Parameters);
            b_Parameters.AddRange(Lambda_b_Parameters);
            Debug.Assert(this.a_LabelTargets.Count==this.b_LabelTargets.Count);
            //var count=this.count++;
            var r = this.PrivateEquals(a.Body,b.Body);
            Debug.Assert(this.a_LabelTargets.Count==this.b_LabelTargets.Count);
            a_Parameters.RemoveRange(a_Parameters_Count,Lambda_a_Parameters_Count);
            b_Parameters.RemoveRange(a_Parameters_Count,Lambda_a_Parameters_Count);
            this.a_ラムダ跨ぎParameters=a_ラムダ跨ぎParameters;
            this.b_ラムダ跨ぎParameters=b_ラムダ跨ぎParameters;
            return r;
        }
        private bool T(ListInitExpression a,ListInitExpression b) =>
            this.PrivateEquals(a.NewExpression,b.NewExpression)&&
            this.InitializersEquals(a.Initializers,b.Initializers);
        private bool T(LoopExpression a,LoopExpression b) {
            var a_BreakLabel = a.BreakLabel;
            var b_BreakLabel = b.BreakLabel;
            var a_ContinueLabel = a.ContinueLabel;
            var b_ContinueLabel = b.ContinueLabel;
            var a_LabelTargets = this.a_LabelTargets;
            var b_LabelTargets = this.b_LabelTargets;
            var a_LabelTargets_Count = a_LabelTargets.Count;
            var b_LabelTargets_Count = b_LabelTargets.Count;
            Debug.Assert(a_LabelTargets_Count==b_LabelTargets_Count);
            if(a_BreakLabel is not null) {
                if(b_BreakLabel is null)
                    return false;
                a_LabelTargets.Add(a_BreakLabel);
                b_LabelTargets.Add(b_BreakLabel);
            } else if(b_BreakLabel is not null)return false;
            if(a_ContinueLabel is not null) {
                if(b_ContinueLabel is null)
                    return false;
                a_LabelTargets.Add(a_ContinueLabel);
                b_LabelTargets.Add(b_ContinueLabel);
            } else if(b_ContinueLabel is not null)return false;
            var r = this.PrivateEquals(a.Body,b.Body);
            Debug.Assert(a_LabelTargets.Count==b_LabelTargets.Count);
            a_LabelTargets.RemoveRange(a_LabelTargets_Count,a_LabelTargets.Count-a_LabelTargets_Count);
            b_LabelTargets.RemoveRange(b_LabelTargets_Count,b_LabelTargets.Count-b_LabelTargets_Count);
            return r;
        }
        private bool T(MemberExpression a,MemberExpression b) => a.Member==b.Member&&this.InternalEquals(a.Expression,b.Expression);
        private bool T(IndexExpression a,IndexExpression b) =>
            a.Indexer==b.Indexer&&
            this.PrivateEquals(a.Object,b.Object)&&
            this.SequenceEqual(a.Arguments,b.Arguments);
        private bool T(MethodCallExpression a,MethodCallExpression b){
            var a_Method = a.Method;
            var b_Method = b.Method;
            if(
                a_Method is DynamicMethod&&
                b_Method is DynamicMethod
            ){
                return this.SequenceEqual(a.Arguments,b.Arguments);
            }
            return a.Method==b.Method&&
                   this.InternalEquals(a.Object,b.Object)&&
                   this.SequenceEqual(a.Arguments,b.Arguments);
        }
        private bool T(InvocationExpression a,InvocationExpression b) =>
            this.PrivateEquals(a.Expression,b.Expression)&&
            this.SequenceEqual(a.Arguments,b.Arguments);
        private bool T(GotoExpression a,GotoExpression b) =>
            this.a_LabelTargets.IndexOf(a.Target)==this.b_LabelTargets.IndexOf(b.Target)&&
            this.InternalEquals(a.Value,b.Value);
        private bool SequenceEqual(Generic.IList<Expression> a,Generic.IList<Expression> b) {
            var a_Count = a.Count;
            if(a_Count!=b.Count)
                return false;
            for(var i=0;i<a_Count;i++){
                if(!this.PrivateEquals(a[i],b[i])){
                    Debug.Assert(this.a_LabelTargets.Count==this.b_LabelTargets.Count);
                    return false;
                }
                Debug.Assert(this.a_LabelTargets.Count==this.b_LabelTargets.Count);
            }
            return true;
        }
        private bool T(ParameterExpression a,ParameterExpression b) {
            var a_Index0 = this.a_Parameters.IndexOf(a);
            var b_Index0 = this.b_Parameters.IndexOf(b);
            if(a_Index0!=b_Index0)return false;
            if(a_Index0>=0) return true;
            //探索開始時点の
            var スコープParameters = this.スコープParameters;
            var a_Index1 = スコープParameters.IndexOf(a);
            var b_Index1 = スコープParameters.IndexOf(b);
            if(a_Index1!=b_Index1) return false;
            if(a_Index1>=0) return true;
            //Let(a=>.v),Let(b=>.w)は一致しない。
            //Let(a=>.v=a),Let(b=>.w=b)は一致する。大域変数.v,.wが代入左辺地なら一致する。

            var a_ラムダ跨ぎParameters=this.a_ラムダ跨ぎParameters;
            var b_ラムダ跨ぎParameters=this.b_ラムダ跨ぎParameters;
            //Debug.Assert(a_ラムダ跨ぎParameters is null&&b_ラムダ跨ぎParameters is null||a_ラムダ跨ぎParameters is not null&&b_ラムダ跨ぎParameters is not null);
            //if(a_ラムダ跨ぎParameters is null){
            //    return false;
            //}
            if(a_ラムダ跨ぎParameters.Contains(a))return b_ラムダ跨ぎParameters.Contains(b);
            if(b_ラムダ跨ぎParameters.Contains(b))return false;
            a_ラムダ跨ぎParameters.Add(a);
            b_ラムダ跨ぎParameters.Add(b);
            return true;
            //if(a_Index0>=0||a_Index1>=0) return true;
            //return a==b;
        }
        private bool T(RuntimeVariablesExpression a,RuntimeVariablesExpression b) => a.Variables.SequenceEqual(b.Variables);
        private bool T(SwitchExpression a,SwitchExpression b) {
            if(a.Comparison!=b.Comparison)
                return false;
            if(!this.PrivateEquals(a.DefaultBody,b.DefaultBody))
                return false;
            if(!this.PrivateEquals(a.SwitchValue,b.SwitchValue))
                return false;
            var a_Cases = a.Cases;
            var b_Cases = b.Cases;
            var a_Cases_Count = a_Cases.Count;
            if(a_Cases.Count!=b_Cases.Count)
                return false;
            for(var c = 0;c<a_Cases_Count;c++) {
                var a_Case = a_Cases[c];
                var b_Case = b_Cases[c];
                if(!this.PrivateEquals(a_Case.Body,b_Case.Body))
                    return false;
                if(!this.SequenceEqual(a_Case.TestValues,b_Case.TestValues))
                    return false;
            }
            return true;
        }
        private bool T(TryExpression a,TryExpression b) {
            if(!this.PrivateEquals(a.Body,b.Body))return false;
            if(!this.InternalEquals(a.Fault,b.Fault))return false;
            if(!this.InternalEquals(a.Finally,b.Finally))return false;
            var a_Handlers = a.Handlers;
            var b_Handlers = b.Handlers;
            var a_Handlers_Count = a_Handlers.Count;
            if(a_Handlers_Count!=b_Handlers.Count)
                return false;
            for(var c = 0;c<a_Handlers_Count;c++) {
                var a_Handler = a_Handlers[c];
                var b_Handler = b_Handlers[c];
                if(a_Handler.Test!=b_Handler.Test)return false;
                var a_Handler_Variable=a_Handler.Variable;
                var b_Handler_Variable=b_Handler.Variable;
                if(a_Handler_Variable is null^b_Handler_Variable is null) return false;
                var a_Parameters = this.a_Parameters;
                var b_Parameters = this.b_Parameters;
                var a_Parameters_Count = a_Parameters.Count;
                Debug.Assert(a_Parameters_Count==b_Parameters.Count);
                if(a_Handler_Variable is not null){
                    a_Parameters.Add(a_Handler_Variable);
                    b_Parameters.Add(b_Handler_Variable);
                }
                if(!this.PrivateEquals(a_Handler.Body,b_Handler.Body))return false;
                if(!this.InternalEquals(a_Handler.Filter,b_Handler.Filter))return false;
                if(a_Handler_Variable is not null) {
                    a_Parameters.RemoveAt(a_Parameters_Count);
                    b_Parameters.RemoveAt(a_Parameters_Count);
                }
            }
            return true;
        }
        private bool T(TypeBinaryExpression a,TypeBinaryExpression b) =>
            this.PrivateEquals(a.Expression,b.Expression)&&
            a.TypeOperand==b.TypeOperand&&
            a.Type==b.Type;
        private bool T(UnaryExpression a,UnaryExpression b) =>
            a.Method==b.Method&&a.Type==b.Type&&(
                a.Operand is not null&&b.Operand is not null&&this.PrivateEquals(a.Operand,b.Operand)||
                a.Operand is null&&b.Operand is null);


        public int GetHashCode([DisallowNull] LabelTarget obj) {
            throw new NotImplementedException();
        }

        public bool Equals(CatchBlock? a,CatchBlock? b) {
            if(a==b) return true;
            if(a==null&&b!=null||a!=null&&b==null) return false;
            if(a!.Test!=b!.Test)return false;
            var a_Variable=a.Variable;
            var b_Variable=b.Variable;
            if(a_Variable is null^b_Variable is null) return false;
            var a_Parameters = this.a_Parameters;
            var b_Parameters = this.b_Parameters;
            var a_Parameters_Count = a_Parameters.Count;
            Debug.Assert(a_Parameters_Count==b_Parameters.Count);
            if(a_Variable is not null){
                a_Parameters.Add(a_Variable);
                b_Parameters.Add(b_Variable);
            }
            if(!this.PrivateEquals(a.Body,b.Body))return false;
            if(!this.InternalEquals(a.Filter,b.Filter))return false;
            if(a_Variable is not null) {
                a_Parameters.RemoveAt(a_Parameters_Count);
                b_Parameters.RemoveAt(a_Parameters_Count);
            }
            return true;
        }

        public int GetHashCode([DisallowNull] CatchBlock obj) {
            throw new NotImplementedException();
        }

        public bool Equals(CSharpArgumentInfo? x,CSharpArgumentInfo? y) {
            if(x==y) return true;
            if(x==null&&y!=null||x!=null&&y==null) return false;
            dynamic x0=new NonPublicAccessor(x),y0=new NonPublicAccessor(y);
            if(x0.Flags             !=y0.Flags             ) return false;
            if(x0.IsByRefOrOut      !=y0.IsByRefOrOut      ) return false;
            if(x0.IsStaticType      !=y0.IsStaticType      ) return false;
            if(x0.LiteralConstant   !=y0.LiteralConstant   ) return false;
            if(x0.Name              !=y0.Name              ) return false;
            if(x0.NamedArgument     !=y0.NamedArgument     ) return false;
            if(x0.UseCompileTimeType!=y0.UseCompileTimeType) return false;
            return true;
        }

        public int GetHashCode([DisallowNull] CSharpArgumentInfo obj) {
            throw new NotImplementedException();
        }
        public bool Equals(SwitchCase? x,SwitchCase? y){
            if(x==y) return true;
            if(x==null&&y!=null||x!=null&&y==null) return false;
            return this.PrivateEquals(x!.Body,y!.Body)&&x.TestValues.SequenceEqual(y.TestValues,this);
        }
        public int GetHashCode(SwitchCase obj){
            return HashCode.Combine(obj.Body,obj.TestValues);
        }
    }
    private class ExpressionEqualityComparer_Assign_Leftで比較:ExpressionEqualityComparer {
        internal ExpressionEqualityComparer_Assign_Leftで比較(Generic.List<ParameterExpression> スコープParameters) : base(スコープParameters) {
        }
        protected override Expression Assignの比較対象(Expression Expression0){
            if(Expression0.NodeType==ExpressionType.Assign){
            }
            return Expression0.NodeType==ExpressionType.Assign&&
                   ((BinaryExpression)Expression0).Left is ParameterExpression Parameter
                    ?Parameter
                    :Expression0;
        }
        public override int GetHashCode(Expression e) => base.GetHashCode(
            e.NodeType==ExpressionType.Assign
                ? ((BinaryExpression)e).Left
                : e
        );
    }
    private readonly 作業配列 _作業配列 = new();

    private static Type IEnumerable1(Type Type) {
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
    private static Type IEnumerable1のT(Type Type)
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
    private static Type[] IEnumerable1のGenericArguments(Type Type) {
        var IEnumerable1 = Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
        if(IEnumerable1 is not null) {
            return IEnumerable1.GetGenericArguments();
        }
        if(Type.IsGenericType&&typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition()) {
            return Type.GetGenericArguments();
        }
        throw new NotSupportedException();
    }
    private static MethodInfo GetGenericMethodDefinition(MethodInfo Method) =>
        Method.IsGenericMethod
            ? Method.GetGenericMethodDefinition()
            : Method;
    /// <summary>
    /// Where((ValueTule&lt;,>p=>p.Item1==p.Item2)は移動できない
    /// Where((ValueTule&lt;,>p=>p.Item1.Item1==p.Item.Item2)は移動できる
    /// pは要素数2の匿名型かValueTule&lt;,>
    /// </summary>
    internal abstract class VoidExpressionTraverser_Quoteを処理しない:VoidExpressionTraverser {
        protected sealed override void Quote(UnaryExpression Unary) {
        }
    }
    private abstract class ReturnExpressionTraverser_Quoteを処理しない:ReturnExpressionTraverser {
        protected ReturnExpressionTraverser_Quoteを処理しない(作業配列 作業配列) : base(作業配列){}
        //protected sealed override Expression Quote(UnaryExpression Unary0) => Unary0;
    }
    private sealed class 検証_変形状態:VoidExpressionTraverser_Quoteを処理しない{
        public void 実行(Expression e)=>this.Traverse(e);
        protected override void Traverse(Expression e) {
            if(e is BinaryExpression Binary) {
                var Binary_Method = Binary.Method;
                if(Binary_Method is not null&&!Binary_Method.IsStatic)
                    throw new InvalidOperationException("Binary演算子のメソッドはstaticであるべき");
            }
            if(e is UnaryExpression Unary) {
                var Unary_Method = Unary.Method;
                if(Unary_Method is not null&&!Unary_Method.IsStatic)
                    throw new InvalidOperationException("Unary演算子のメソッドはstaticであるべき");
            }
            base.Traverse(e);
        }

        protected override void Call(MethodCallExpression MethodCall) {
            var GenericMethodDefinition = GetGenericMethodDefinition(MethodCall.Method);
            if(ExtensionSet.Select_selector==GenericMethodDefinition&&MethodCall.Arguments[0] is LambdaExpression selector&&selector.Body==selector.Parameters[0]){
                throw new InvalidOperationException("Select_selector(p=>p)は削除されるべき");
            }
            var MethodCall_Object = MethodCall.Object;
            if(MethodCall_Object is not null) {
                this.Traverse(MethodCall_Object);
            }
            this.TraverseExpressions(MethodCall.Arguments);
            var MethodCall_Arguments = MethodCall.Arguments;
            if(MethodCall_Arguments.Count==0){
                return;
            }
            if(!(MethodCall_Arguments[0] is MethodCallExpression MethodCall_MethodCall)){
                return;
            }
            var MethodCall_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall_MethodCall.Method);
            //プローブ.Type==typeof(Object)
            //    ? nameof(DictionaryAscList<Int32,Int32>.GetObjectValue)
            //    : nameof(DictionaryAscList<Int32,Int32>.GetTKeyValue),
            if(
                (
                    nameof(Sets.LookupList<int,int>.GetObjectValue)==GenericMethodDefinition.Name||
                    nameof(Sets.LookupList<int,int>.GetTKeyValue)==GenericMethodDefinition.Name
                )&&
                ExtensionEnumerable.Lookup==MethodCall_GenericMethodDefinition||
                (
                    nameof(Sets.LookupSet<int,int>.GetObjectValue)==GenericMethodDefinition.Name||
                    nameof(Sets.LookupSet<int,int>.GetTKeyValue)==GenericMethodDefinition.Name
                )&&
                ExtensionSet.Lookup==MethodCall_GenericMethodDefinition
            ){
                throw new InvalidOperationException("Dictionary.Equalが連続してはいけない。Dictionaryは上位のラムダに移動してthisメンバにより参照されるはず。");
            }
        }
    }
    private static Expression AndAlsoで繋げる(Expression? predicate,Expression e) => predicate is null ? e : Expression.AndAlso(predicate,e);
    private readonly Generic.List<ParameterExpression> ListスコープParameter = new();
    private readonly ExpressionEqualityComparer _ExpressionEqualityComparer;
    private readonly Generic.List<ParameterExpression> Listループ跨ぎParameter = new();
    private readonly SQLServer.TSql160Parser Parser = new(true);
    private readonly 変換_TSqlFragment正規化 _変換_TSqlFragment正規化;
    private readonly 変換_TSqlFragmentからExpression _変換_TSqlFragmentからExpression;
    private readonly 変換_KeySelectorの匿名型をValueTuple _変換_KeySelectorの匿名型をValueTuple;
    private readonly 変換_メソッド正規化_取得インライン不可能定数 _変換_メソッド正規化_取得インライン不可能定数;
    private readonly 変換_WhereからLookup _変換_WhereからLookup;
    /// <summary>
    /// Listラムダ跨ぎParameter,Listループ跨ぎParameter設定
    /// </summary>
    private readonly 変換_跨ぎParameterの先行評価 _変換_跨ぎParameterの先行評価;
    private readonly 変換_跨ぎParameterの不要置換復元 _変換_跨ぎParameterの不要置換復元;
    private readonly 変換_局所Parameterの先行評価 _変換_局所Parameterの先行評価;
    private readonly 変換_Stopwatchに埋め込む _変換_Stopwatchに埋め込む;
    private readonly 変換_インラインループ独立 _変換_インラインループ独立;
    private readonly 取得_Dictionary _取得_Dictionary;
    private readonly 検証_Parameterの使用状態 _検証_Parameterの使用状態;
    private readonly 取得_CSharp _取得_CSharp = new();
    private readonly 判定_InstanceMethodか 判定InstanceMethodか;

    private readonly 検証_変形状態 _検証_変形状態;
    private readonly 作成_DynamicMethod _作成_DynamicMethod;
    private readonly 作成_DynamicAssembly _作成_DynamicAssembly;
    private readonly 取得_命令ツリー _取得_命令ツリー = new();
    /// <summary>
    /// IL生成時に使う。変換_跨ぎParameterをBlock_Variablesに、
    /// </summary>
    private Generic.Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant{
        get=>this._変換_メソッド正規化_取得インライン不可能定数.DictionaryConstant;
        set{
            this._取得_Dictionary.DictionaryConstant=value;
            this._変換_メソッド正規化_取得インライン不可能定数.DictionaryConstant=value;
            this.判定InstanceMethodか.DictionaryConstant=value;
            Debug.Assert(this._作成_DynamicMethod.DictionaryConstant==value);
            Debug.Assert(this._作成_DynamicAssembly.DictionaryConstant==value);
            //this._作成_DynamicMethod.DictionaryConstant=value;
            //this._作成_DynamicAssembly.DictionaryConstant=value;
        }
    }
    private Generic.Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic{
        get=>this._取得_Dictionary.DictionaryDynamic;
        set{
            this._取得_Dictionary.DictionaryDynamic=value;
            this._作成_DynamicMethod.DictionaryDynamic=value;
            this._作成_DynamicAssembly.DictionaryDynamic=value;
        }
    }
    private Generic.Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)> DictionaryLambda{
        get=>this._取得_Dictionary.DictionaryLambda;
        set{
            this._取得_Dictionary.DictionaryLambda=value;
            this._作成_DynamicMethod.DictionaryLambda=value;
            this._作成_DynamicAssembly.DictionaryLambda=value;
        }
    }
    private Generic.Dictionary<ParameterExpression, (FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter{
        get=>this._取得_Dictionary.Dictionaryラムダ跨ぎParameter;
        set{
            this._取得_Dictionary.Dictionaryラムダ跨ぎParameter=value;
            this._変換_跨ぎParameterの先行評価.Dictionaryラムダ跨ぎParameter=value;
            this._変換_跨ぎParameterの不要置換復元.Dictionaryラムダ跨ぎParameter=value;
            this._変換_局所Parameterの先行評価.ラムダ跨ぎParameters=value.Keys;
            this._検証_Parameterの使用状態.ラムダ跨ぎParameters=value.Keys;
            this._作成_DynamicMethod.Dictionaryラムダ跨ぎParameter=value;
            this._作成_DynamicAssembly.Dictionaryラムダ跨ぎParameter=value;
        }
    }
    private ParameterExpression DispParameter{
        get=>this._作成_DynamicMethod.DispParameter;
        set{
            this._作成_DynamicMethod.DispParameter=value;
            this._作成_DynamicAssembly.DispParameter=value;
        }
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Optimizer():this(typeof(object)) {
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="BaseType"></param>
    private Optimizer(Type BaseType) {
        var 作業配列 = this._作業配列;
        var ScriptGenerator = new SQLServer.Sql160ScriptGenerator(
            new SQLServer.SqlScriptGeneratorOptions {
                KeywordCasing=SQLServer.KeywordCasing.Lowercase,
                IncludeSemicolons=true,
                NewLineBeforeFromClause=true,
                NewLineBeforeJoinClause=true,
                NewLineBeforeWhereClause=true,
                NewLineBeforeGroupByClause=true,
                NewLineBeforeOrderByClause=true,
                NewLineBeforeHavingClause=true,
            }
        );
        var 変換_旧Parameterを新Expression1                              =new 変換_旧Parameterを新Expression1(作業配列);
        var ListスコープParameter                                        =this.ListスコープParameter;
        var ExpressionEqualityComparer=this._ExpressionEqualityComparer  =new ExpressionEqualityComparer(ListスコープParameter);
        //TSQLでは1度だけnewすればいいが
        var 判定_InstanceMethodか=this.判定InstanceMethodか              =new(ExpressionEqualityComparer);
        this._変換_TSqlFragment正規化                                    =new(ScriptGenerator);
        var ブローブビルドExpressionEqualityComparer                     =new ブローブビルドExpressionEqualityComparer(ExpressionEqualityComparer);
        var 判定_指定Parameter無_他Parameter有                           =new 判定_指定Parameter無_他Parameter有();
        var 判定_指定Parameters無                                        =new 判定_指定Parameters無();
        var 判定_指定Parameter有_他Parameter無_Lambda内部走査            =new 判定_指定Parameter有_他Parameter無_Lambda内部走査();
        var 取得_OuterPredicate_InnerPredicate_プローブビルド            =new 取得_OuterPredicate_InnerPredicate_プローブビルド(作業配列,判定_指定Parameter無_他Parameter有,判定_指定Parameter有_他Parameter無_Lambda内部走査,ブローブビルドExpressionEqualityComparer);
        var 変換_旧Expressionを新Expression1                             =new 変換_旧Expressionを新Expression1(作業配列,ExpressionEqualityComparer);
        this._変換_TSqlFragmentからExpression                            =new(作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド,ExpressionEqualityComparer,変換_旧Parameterを新Expression1,変換_旧Expressionを新Expression1,判定_指定Parameters無,ScriptGenerator);
        this._変換_KeySelectorの匿名型をValueTuple                       =new(作業配列);
        var 変換_旧Parameterを新Expression2                              =new 変換_旧Parameterを新Expression2(作業配列);
        this._変換_メソッド正規化_取得インライン不可能定数               =new(作業配列,変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2,変換_旧Expressionを新Expression1);
        this._変換_WhereからLookup                                       =new(作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド,判定_指定Parameters無);
        var Listループ跨ぎParameter                                      =this.Listループ跨ぎParameter;
        this._変換_跨ぎParameterの先行評価                               =new(作業配列,ExpressionEqualityComparer,Listループ跨ぎParameter);
        this._変換_跨ぎParameterの不要置換復元                           =new(作業配列);
        var ExpressionEqualityComparer_Assign_Leftで比較                 =new ExpressionEqualityComparer_Assign_Leftで比較(ListスコープParameter);
        this._変換_局所Parameterの先行評価                               =new(作業配列,ListスコープParameter,ExpressionEqualityComparer_Assign_Leftで比較);
        this._取得_Dictionary                                            =new();
        this._検証_変形状態                                              =new();
        this._検証_Parameterの使用状態                                   =new(Listループ跨ぎParameter);
        this._変換_インラインループ独立                                  =new(作業配列,ExpressionEqualityComparer_Assign_Leftで比較,変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2);
        this._変換_Stopwatchに埋め込む                                   =new(作業配列);
        this._作成_DynamicMethod                                         =new(判定_InstanceMethodか);
        this._作成_DynamicAssembly                                       =new(判定_InstanceMethodか);
        this.DictionaryConstant=new(ExpressionEqualityComparer);
        this.DictionaryDynamic=new();
        this.DictionaryLambda=new(ExpressionEqualityComparer);
        this.Dictionaryラムダ跨ぎParameter=new();
    }
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    /// <summary>オブジェクトが、ガベージ コレクションによって収集される前に、リソースの解放とその他のクリーンアップ操作の実行を試みることができるようにします。</summary>
    ~Optimizer() => this.Dispose(false);
    public void Dispose() {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// 破棄されているか
    /// </summary>
    private bool IsDisposed {
        get; set;
    }
    /// <summary>
    /// ファイナライザでDispose(false)する。
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing) {
        if(this.IsDisposed) {
            this.IsDisposed=true;
            if(disposing) {
                this._取得_命令ツリー.Dispose();
                this._取得_CSharp.Dispose();
            }
        }
    }
    private SQLServer.TSqlFragment SQLからTSqlFragment(string SQL) {
        var Parser = this.Parser;
        var Parsed = Parser.Parse(new StringReader(SQL),out var errors);
        if(errors!.Count!=0) {
            var sb = new StringBuilder();
            foreach(var error in errors) {
                sb.AppendLine(error.Message);
            }
            throw new System.Data.SyntaxErrorException(sb.ToString());
        }
        return Parsed;
    }
    private string? SQL;
    /// <summary>
    /// クライアントで使う
    /// </summary>
    /// <param name="Parameter"></param>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public Expression SQLToExpression(ParameterExpression Parameter,string SQL) {
        var TSqlFragment = this.SQLからTSqlFragment(SQL);
        this.SQL=SQL;
        this._変換_TSqlFragment正規化.実行(TSqlFragment);
        return this._変換_TSqlFragmentからExpression.実行(Parameter,TSqlFragment);
    }
    private static object Get_ValueTuple(MemberExpression Member,object Tuple){
        var Field=(FieldInfo)Member.Member;
        if(Member.Expression is MemberExpression Member1){
            Tuple=Get_ValueTuple(Member1,Tuple);
        }
        var Value=Field.GetValue(Tuple);
        Debug.Assert(Value!=null);
        return Value;
    }
    private static MemberExpression ValueTuple_Item(ref Type TupleType,ref object TupleValue,ref int Item番号,ref Expression TupleExpression,object Value) {
        if(Item番号==8){
            var Rest=TupleType.GetField("Rest");
            Debug.Assert(Rest!=null);
            TupleType=Rest.FieldType;
            var Value0=Rest.GetValue(TupleValue);
            Debug.Assert(Value0!=null);
            TupleValue=Value0;
            TupleExpression=Expression.Field(TupleExpression,Rest);
            Item番号=1;
        }
        var TupleField=TupleType.GetField($"Item{Item番号++}");
        Debug.Assert(TupleField!=null);
        TupleField.SetValue(TupleValue,Value);
        return Expression.Field(TupleExpression,TupleField);
    }
    private static MemberExpression ValueTuple_Item(ref Type TupleType,ref object TupleValue,ref int Item番号,ref Expression TupleExpression) {
        if(Item番号==8){
            var Rest=TupleType.GetField("Rest");
            Debug.Assert(Rest!=null);
            TupleType=Rest.FieldType;
            var Value0=Rest.GetValue(TupleValue);
            Debug.Assert(Value0!=null);
            TupleValue=Value0;
            TupleExpression=Expression.Field(TupleExpression,Rest);
            Item番号=1;
        }
        var TupleField=TupleType.GetField($"Item{Item番号++}");
        return Expression.Field(TupleExpression,TupleField);
    }
    internal void Disp作成(ParameterExpression ContainerParameter,Information Information,string SQL) {
        var Disp_TypeBuilder = Information.Disp_TypeBuilder;
        var Impl_TypeBuilder = Information.Impl_TypeBuilder;
        Debug.Assert(Disp_TypeBuilder is not null);
        var Expression0 = this.SQLToExpression(ContainerParameter,SQL);
        var Lambda0 = (LambdaExpression)Expression0;
        var DictionaryConstant = this.DictionaryConstant=Information.DictionaryConstant;
        var DictionaryDynamic= this.DictionaryDynamic=Information.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda=Information.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter=Information.Dictionaryラムダ跨ぎParameter;
        var Lambda1 = Information.Lambda=this.Lambda最適化(Lambda0);
        this._取得_Dictionary.実行(Lambda1);
        var Disp_ctor_I = Information.Disp_ctor_I;
        {
            var Field番号 = 0;
            foreach(var a in DictionaryConstant.AsEnumerable())
                DictionaryConstant[a.Key]=(Disp_TypeBuilder.DefineField($"Constant{Field番号++}",a.Key.Type,FieldAttributes.Public)!, default!);
            foreach(var a in DictionaryDynamic.AsEnumerable())
                DictionaryDynamic[a.Key]=(Disp_TypeBuilder.DefineField($"CallSite{Field番号++}",a.Key.Type,FieldAttributes.Public)!, default!);
            var 判定InstanceMethodか = this.判定InstanceMethodか;
            var 作業配列 = this._作業配列;
            var Types2 = 作業配列.Types2;
            Types2[0]=typeof(object);
            Types2[1]=typeof(IntPtr);
            foreach(var a in DictionaryLambda.AsEnumerable()) {
                var Lambda = a.Key;
                var Lambda_Parameters = Lambda.Parameters;
                var Lambda_Parameters_Count = Lambda_Parameters.Count;
                var インスタンスメソッドか = 判定InstanceMethodか.実行(Lambda.Body)|true;
                Type[] DispTypes, ImplTypes;
                if(インスタンスメソッドか) {
                    DispTypes=new Type[Lambda_Parameters_Count];
                    ImplTypes=new Type[Lambda_Parameters_Count+1];
                    ImplTypes[0]=Disp_TypeBuilder;
                    for(var b = 0;b<Lambda_Parameters_Count;b++) {
                        var 元Parameter = Lambda_Parameters[b];
                        var Type = 元Parameter.IsByRef
                            ? 元Parameter.Type.MakeByRefType()
                            : 元Parameter.Type;
                        DispTypes[b+0]=Type;
                        ImplTypes[b+1]=Type;
                    }
                } else {
                    DispTypes=ImplTypes=new Type[Lambda_Parameters_Count];
                    for(var b = 0;b<Lambda_Parameters_Count;b++) {
                        var 元Parameter = Lambda_Parameters[b];
                        ImplTypes[b]=元Parameter.IsByRef?元Parameter.Type.MakeByRefType():元Parameter.Type;
                    }
                }
                var Disp_MethodBuilder = Disp_TypeBuilder.DefineMethod($"DispMethod{Field番号}",MethodAttributes.Public,Lambda.ReturnType,DispTypes);
                Disp_MethodBuilder.InitLocals=false;
                var Impl_MethodBuilder = Impl_TypeBuilder.DefineMethod($"ImplMethod{Field番号}",MethodAttributes.Public|MethodAttributes.Static,Lambda.ReturnType,ImplTypes);
                Impl_MethodBuilder.InitLocals=false;
                var Disp_MethodBuilder_I = Disp_MethodBuilder.GetILGenerator();
                int Disp_index = 1, Impl_index;
                if(インスタンスメソッドか) {
                    Disp_MethodBuilder_I.Ldarg_0();
                    Impl_MethodBuilder.DefineParameter(1,ParameterAttributes.None,"Disp");
                    Impl_index=2;
                } else {
                    Impl_index=1;
                }
                ushort index = 1;
                for(var b = 0;b<Lambda_Parameters_Count;b++) {
                    Disp_MethodBuilder_I.Ldarg(index++);
                    var ParameterName = Lambda_Parameters[b].Name;
                    Disp_MethodBuilder.DefineParameter(Disp_index++,ParameterAttributes.None,ParameterName);
                    Impl_MethodBuilder.DefineParameter(Impl_index++,ParameterAttributes.None,ParameterName);
                }
                Disp_MethodBuilder_I.Call(Impl_MethodBuilder);
                Disp_MethodBuilder_I.Ret();
                var Delegate = Disp_TypeBuilder.DefineField($"Delegate{Field番号}",Lambda.Type,FieldAttributes.Public);
                Disp_ctor_I.Ldarg_0();
                Disp_ctor_I.Ldarg_0();
                Disp_ctor_I.Ldftn(Disp_MethodBuilder);
                Disp_ctor_I.Newobj(Lambda.Type.GetConstructor(Types2)!);
                Disp_ctor_I.Stfld(Delegate);
                DictionaryLambda[a.Key]=(Delegate, a.Value.Member,Impl_MethodBuilder);
                Field番号++;
            }
            Disp_ctor_I.Ret();
            {
                var (_, _, Impl_MethodBuilder)=DictionaryLambda[Lambda1];
                var I = Information.SchemaのMethod.GetILGenerator();
                I.Ldarg_0();
                //10 TF,SF
                var Count = Lambda0.Parameters.Count;
                for(var a = 1;a<=Count;a++) I.Ldarg((ushort)a);
                //11 V,TF,SF
                I.Call(Impl_MethodBuilder);
                I.Ret();
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.Where(p => p.Key!=ContainerParameter))
                Dictionaryラムダ跨ぎParameter[a.Key]=(Disp_TypeBuilder.DefineField(a.Key.Name,a.Key.Type,FieldAttributes.Public), a.Value.Member);
        }

        var DispType=Information.CreateDispType();
        var DispParameter=this.DispParameter=Expression.Parameter(DispType,"Disp");
        Debug.Assert(Information.DispParameter is null);
        Information.DispParameter=DispParameter;
        {
            var Field番号=0;
            foreach(var a in DictionaryConstant.AsEnumerable()){
                Debug.Assert($"Constant{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field=DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryConstant[a.Key]=(Field,Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryDynamic.AsEnumerable()){
                Debug.Assert($"CallSite{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field=DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryDynamic[a.Key]=(Field,Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryLambda.AsEnumerable()){
                Debug.Assert($"Delegate{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field=DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryLambda[a.Key]=(
                    Field,
                    Expression.Field(DispParameter,Field),
                    a.Value.Impl
                );
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable()){
                Debug.Assert(a.Key.Name==a.Value.Disp.Name);
                Field番号++;
                var Field=DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                Dictionaryラムダ跨ぎParameter[a.Key]=(Field,Expression.Field(DispParameter,Field));
            }
        }
    }
    internal void Impl作成(Information Information,ParameterExpression ContainerParameter){
        Debug.Assert(Information.DispParameter is not null);
        var DispParameter=Information.DispParameter;
        this.DispParameter=DispParameter;
        var DictionaryConstant=this.DictionaryConstant=Information.DictionaryConstant;
        var DictionaryDynamic=this.DictionaryDynamic=Information.DictionaryDynamic;
        var DictionaryLambda=this.DictionaryLambda= Information.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter=this.Dictionaryラムダ跨ぎParameter = Information.Dictionaryラムダ跨ぎParameter;
        Debug.Assert(Information.Disp_Type is not null);
        var ContainerField=Information.Disp_Type.GetField("Container");
        Debug.Assert(ContainerField is not null);
        Dictionaryラムダ跨ぎParameter.Add(ContainerParameter,(ContainerField,Expression.Field(DispParameter,ContainerField)));
        Debug.Assert(Information.Lambda is not null);
        this._作成_DynamicAssembly.Impl作成(Information.Lambda,DispParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter);
        Information.CreateImplType();
    }
    /// <summary>
    /// 動的な型しかないときにTSQLを最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <param name="Container"></param>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public Func<object> CreateDelegate(Container Container,string SQL) {
        var TSqlFragment = this.SQLからTSqlFragment(SQL);
        this._変換_TSqlFragment正規化.実行(TSqlFragment);
        var Body = this._変換_TSqlFragmentからExpression.実行(Container,TSqlFragment);
        var Lambda=this.Lambda最適化(Expression.Lambda(Body,Array.Empty<ParameterExpression>()));
        return (Func<object>)this.DynamicMethod(Container.GetType(),Lambda);
    }
    /// <summary>
    /// 最適化した式木を返す
    /// </summary>
    /// <param name="Expression"></param>
    /// <returns></returns>
    public Expression CreateExpression(Expression Expression) =>
        this.Lambda最適化(Expression);
    /// <summary>
    /// 非最適化したDelegateを返す
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Delegate Create非最適化Delegate(LambdaExpression Lambda) =>
        this.IsGenerateAssembly?this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda):this.DynamicMethod(typeof(object),Lambda);

    /// <summary>
    /// 最適化したたDelegateを返す
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Delegate CreateDelegate(LambdaExpression Lambda) =>
        this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Action<T> CreateDelegate<T>(Expression<Action<T>> Lambda) =>
        (Action<T>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Action CreateDelegate(Expression<Action> Lambda) =>
        (Action)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<TResult> CreateDelegate<TResult>(Expression<Func<TResult>> Lambda) =>
        (Func<TResult>)this.PrivateDelegate(Lambda);

    public Delegate CreateServerDelegate(LambdaExpression Lambda){
        this._取得_Dictionary.実行(Lambda);
        return this.IsGenerateAssembly
            ?this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda)
            :this.DynamicMethod(typeof(object),Lambda);
    }
    private Delegate PrivateDelegate(LambdaExpression Lambda){
        var Lambda0=this.Lambda最適化(Lambda);
        this._取得_Dictionary.実行(Lambda0);
        return this.IsGenerateAssembly
            ?this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda0)
            :this.DynamicMethod(typeof(object),Lambda0);
    }

    public bool IsInline{
        get=>this._変換_局所Parameterの先行評価.IsInline;
        set{
            this._変換_跨ぎParameterの先行評価.IsInline=value;
            this._変換_局所Parameterの先行評価.IsInline=value;
        }
    }
    public bool IsGenerateAssembly{get;set;}=true;
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T,TResult> CreateDelegate<T, TResult>(Expression<Func<T,TResult>> Lambda)=>
        (Func<T,TResult>)this.PrivateDelegate(Lambda);
    public Func<TContainer,TResult> CreateContainerDelegate<TContainer, TResult>(Expression<Func<TContainer,TResult>> Lambda)where TContainer:Container =>
        (Func<TContainer,TResult>)this.PrivateDelegate(Lambda);
    //public Func<TContainer,T1,TResult> CreateDelegate<TContainer,T1,TResult>(Expression<Func<TContainer,T1,TResult>> Lambda)where TContainer:Container=>
    //    (Func<TContainer,T1,TResult>)this.PrivateDelegate(typeof(object),Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T1,T2,TResult> CreateDelegate<T1, T2, TResult>(Expression<Func<T1,T2,TResult>> Lambda) =>
        (Func<T1,T2,TResult>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T1,T2,T3,TResult> CreateDelegate<T1, T2, T3, TResult>(Expression<Func<T1,T2,T3,TResult>> Lambda) =>
        (Func<T1,T2,T3,TResult>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T1,T2,T3,T4,TResult> CreateDelegate<T1, T2, T3, T4, TResult>(Expression<Func<T1,T2,T3,T4,TResult>> Lambda) =>
        (Func<T1,T2,T3,T4,TResult>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// コンパイルした時のアセンブリファイル名
    /// </summary>
    public string? AssemblyFileName { get; set; }
    private Type Dynamicに対応するFunc(DynamicExpression Dynamic){
        var Dynamic_Arguments=Dynamic.Arguments;
        var Dynamic_Arguments_Count=Dynamic_Arguments.Count;
        var Types_Length=Dynamic_Arguments_Count+2;
        var Types=new Type[Types_Length];
        Types[0]=typeof(CallSite);
        for(var a=0;a<Dynamic_Arguments_Count;a++) Types[a+1]=Dynamic_Arguments[a].Type;
        Types[Dynamic_Arguments_Count+1]=Dynamic.Type;
        return Reflection.Func.Get(Dynamic_Arguments_Count+1).MakeGenericType(Types);
    }
    private Type Dynamicに対応するCallSite(DynamicExpression Dynamic)=>this._作業配列.MakeGenericType(typeof(CallSite<>),this.Dynamicに対応するFunc(Dynamic));
    private Delegate DynamicAssemblyとDynamicMethod(Type ContainerType,LambdaExpression Lambda1){
        //Lambda1=this.Lambda最適化(Lambda1);
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic=this.DictionaryDynamic;
        var DictionaryLambda=this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var Name=Lambda1.Name??"Disp";
        var AssemblyName=new AssemblyName{Name=Name};
        var DynamicAssembly=AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        var ModuleBuilder=DynamicAssembly.DefineDynamicModule("動的");
        var Disp_TypeBuilder=ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
        var Impl_TypeBuilder=Disp_TypeBuilder.DefineNestedType("Impl",TypeAttributes.NestedPublic|TypeAttributes.Sealed|TypeAttributes.Abstract);
        var Container_FieldBuilder=Disp_TypeBuilder.DefineField("Container",ContainerType,FieldAttributes.Public);
        var Disp_ctor=Disp_TypeBuilder.DefineConstructor(MethodAttributes.Public,CallingConventions.HasThis,this._作業配列.Types設定(ContainerType));
        Disp_ctor.InitLocals=false;
        Disp_ctor.DefineParameter(1,ParameterAttributes.None,"Container");
        var Disp_ctor_I=Disp_ctor.GetILGenerator();
        Disp_ctor_I.Ldarg_0();
        Disp_ctor_I.Ldarg_1();
        Disp_ctor_I.Stfld(Container_FieldBuilder);
        Debug.Assert(Disp_TypeBuilder is not null);
        var 作業配列=this._作業配列;
        var Field番号=0;
        foreach(var a in DictionaryConstant.AsEnumerable())
            DictionaryConstant[a.Key]=(Disp_TypeBuilder.DefineField($"Constant{Field番号++}",a.Key.Type,FieldAttributes.Public)!,default!);
        foreach(var a in DictionaryDynamic.AsEnumerable())
            DictionaryDynamic[a.Key]=(Disp_TypeBuilder.DefineField($"Dynamic{Field番号++}",this.Dynamicに対応するCallSite(a.Key),FieldAttributes.Public)!,default!);
        var 判定InstanceMethodか=this.判定InstanceMethodか;
        var Types2=作業配列.Types2;
        Types2[0]=typeof(object);
        Types2[1]=typeof(IntPtr);
        foreach(var a in DictionaryLambda.AsEnumerable()){
            var Lambda=a.Key;
            var LambdaParameters=Lambda.Parameters;
            var LambdaParametersCount=LambdaParameters.Count;
            var インスタンスメソッドか=判定InstanceMethodか.実行(Lambda.Body)|true;
            Type[] DispTypes,ImplTypes;
            if(インスタンスメソッドか){
                DispTypes=new Type[LambdaParametersCount];
                ImplTypes=new Type[LambdaParametersCount+1];
                ImplTypes[0]=Disp_TypeBuilder;
                for(var b=0;b<LambdaParametersCount;b++){
                    var 元Parameter=LambdaParameters[b];
                    var Type=元Parameter.IsByRef?元Parameter.Type.MakeByRefType():元Parameter.Type;
                    DispTypes[b+0]=Type;
                    ImplTypes[b+1]=Type;
                }
            } else{
                DispTypes=ImplTypes=new Type[LambdaParametersCount];
                for(var b=0;b<LambdaParametersCount;b++){
                    var 元Parameter=LambdaParameters[b];
                    ImplTypes[b]=元Parameter.IsByRef?元Parameter.Type.MakeByRefType():元Parameter.Type;
                }
            }
            var Disp_MethodBuilder=Disp_TypeBuilder.DefineMethod($"Disp_Method{Field番号}",MethodAttributes.Public,Lambda.ReturnType,DispTypes);
            Disp_MethodBuilder.InitLocals=false;
            var Impl_MethodBuilder=Impl_TypeBuilder.DefineMethod($"Impl_Method{Field番号}",MethodAttributes.Public|MethodAttributes.Static,Lambda.ReturnType,ImplTypes);
            Impl_MethodBuilder.InitLocals=false;
            var Disp_MethodBuilder_I=Disp_MethodBuilder.GetILGenerator();
            int Disp_index=1,Impl_index;
            if(インスタンスメソッドか){
                Disp_MethodBuilder_I.Ldarg_0();
                Impl_MethodBuilder.DefineParameter(1,ParameterAttributes.None,"Disp");
                Impl_index=2;
            } else{
                Impl_index=1;
            }
            ushort Index=1;
            for(var B2=0;B2<LambdaParametersCount;B2++){
                Disp_MethodBuilder_I.Ldarg(Index++);
                var ParameterName=LambdaParameters[B2].Name;
                Disp_MethodBuilder.DefineParameter(Disp_index++,ParameterAttributes.None,ParameterName);
                Impl_MethodBuilder.DefineParameter(Impl_index++,ParameterAttributes.None,ParameterName);
            }
            Disp_MethodBuilder_I.Call(Impl_MethodBuilder);
            Disp_MethodBuilder_I.Ret();
            var Delegate=Disp_TypeBuilder.DefineField($"Delegate{Field番号}",Lambda.Type,FieldAttributes.Public);
            Disp_ctor_I.Ldarg_0();
            Disp_ctor_I.Ldarg_0();
            Disp_ctor_I.Ldftn(Disp_MethodBuilder);
            Disp_ctor_I.Newobj(Lambda.Type.GetConstructor(Types2)!);
            Disp_ctor_I.Stfld(Delegate);
            DictionaryLambda[a.Key]=(Delegate,default!,Impl_MethodBuilder);
            Field番号++;
        }
        Disp_ctor_I.Ret();
        foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable())
            Dictionaryラムダ跨ぎParameter[a.Key]=(Disp_TypeBuilder.DefineField(a.Key.Name,a.Key.Type,FieldAttributes.Public),default!);
        //Disp作成
        var Disp_Type=Disp_TypeBuilder.CreateType();
        var DispParameter=Expression.Parameter(Disp_Type,"Disp");
        {
            var 番号=0;
            foreach(var a in DictionaryConstant.AsEnumerable()){
                Debug.Assert($"Constant{番号}"==a.Value.Disp.Name);
                番号++;
                var Field=Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryConstant[a.Key]=(Field,Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryDynamic.AsEnumerable()){
                Debug.Assert($"Dynamic{番号}"==a.Value.Disp.Name);
                番号++;
                var Field=Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryDynamic[a.Key]=(Field,Expression.Field(DispParameter,Field));
                Debug.Assert(this.Dynamicに対応するCallSite(a.Key)==Field.FieldType);
                Debug.Assert(this.Dynamicに対応するCallSite(a.Key)==Expression.Field(DispParameter,Field).Type);
            }
            foreach(var a in DictionaryLambda.AsEnumerable()){
                Debug.Assert($"Delegate{番号}"==a.Value.Disp.Name);
                番号++;
                var Field=Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryLambda[a.Key]=(
                    Field,
                    Expression.Field(DispParameter,Field),
                    a.Value.Impl
                );
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable()){
                Debug.Assert(a.Key.Name==a.Value.Disp.Name);
                番号++;
                var Field=Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                Dictionaryラムダ跨ぎParameter[a.Key]=(Field,Expression.Field(DispParameter,Field));
            }
        }
        var s=インラインラムダテキスト(Lambda1);
        //var Tuple=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理(ContainerType,DictionaryConstant,DictionaryLambda,Dictionaryラムダ跨ぎParameter,out var TupleParameter1);
        this._作成_DynamicAssembly.Impl作成(Lambda1,DispParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter);
        Debug.Assert(Disp_Type.GetField("Container",Instance_NonPublic_Public) is not null);
        var (Tuple,TupleParameter)=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(ContainerType);
        {
            var _=Impl_TypeBuilder.CreateType();
            var Folder=Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            //todo AssemblyGenerater.GenerateAssembly()の後GC.Collect()とGC.WaitForPendingFinalizers()することでファイルハンドルをファイナライザで解放させることを期待したがだダメだった
            //var t=Stopwatch.StartNew();
            //Console.Write("GenerateAssembly,");
            //new AssemblyGenerator()をフィールドに保存すると２度目以降前回のアセンブリ情報が残る
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\{Name}.dll");
            //this.AssemblyGenerator.GenerateAssembly(DynamicAssembly,@$"{Folder}\{Name}.dll");
            //Console.WriteLine($"GenerateAssembly {t.ElapsedMilliseconds}ms");
        }
        this._作成_DynamicMethod.Impl作成(Lambda1,TupleParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter,Tuple);
        var Value= Get_ValueTuple(DictionaryLambda[Lambda1].Member,Tuple);
        var Delegate1 = (Delegate)Value;
        return Delegate1;
    }
    //private readonly AssemblyGenerator AssemblyGenerator=new();
    /// <summary>
    /// 動的ラムダ。
    /// </summary>
    /// <param name="ContainerType"></param>
    /// <param name="Lambda1"></param>
    /// <returns></returns>
    private Delegate DynamicMethod(Type ContainerType,LambdaExpression Lambda1){
        //var Lambda1=this.Lambda最適化(Lambda);
        var DictionaryConstant=this.DictionaryConstant;
        var DictionaryDynamic=this.DictionaryDynamic;
        var DictionaryLambda=this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var (Tuple,TupleParameter)=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(ContainerType);

        this._作成_DynamicMethod.Impl作成(Lambda1,TupleParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter,Tuple);
        var Value= Get_ValueTuple(DictionaryLambda[Lambda1].Member,Tuple);
        var Delegate1 = (Delegate)Value;
        return Delegate1;
    }

    private(object Tuple,ParameterExpression TupleParameter) DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(Type ContainerType){
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic=this.DictionaryDynamic;
        var DictionaryLambda=this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var TargetFieldType数 = 1+DictionaryConstant.Count+DictionaryDynamic.Count+DictionaryLambda.Count+Dictionaryラムダ跨ぎParameter.Count;
        var FieldTypes=new Type[TargetFieldType数];
        {
            FieldTypes[0]=ContainerType;
            var index=1;
            foreach(var a in DictionaryConstant.Keys)
                FieldTypes[index++]=a.Type;
            foreach(var a in DictionaryDynamic.AsEnumerable())
                FieldTypes[index++]=this.Dynamicに対応するCallSite(a.Key);
            foreach(var a in DictionaryLambda.Keys)
                FieldTypes[index++]=a.Type;
            foreach(var a in Dictionaryラムダ跨ぎParameter.Keys)
                FieldTypes[index++]=a.Type;
        }
        //末尾再帰をループで処理
        var 作業配列=this._作業配列;
        var Switch=TargetFieldType数%7;
        var Offset=TargetFieldType数-Switch;
        Type DispType;
        if(TargetFieldType数<8){
            DispType=Switch switch{
                1=>作業配列.MakeGenericType(typeof(ClassTuple<      >),FieldTypes[0]),
                2=>作業配列.MakeGenericType(typeof(ClassTuple<,     >),FieldTypes[0],FieldTypes[1]),
                3=>作業配列.MakeGenericType(typeof(ClassTuple<,,    >),FieldTypes[0],FieldTypes[1],FieldTypes[2]),
                4=>作業配列.MakeGenericType(typeof(ClassTuple<,,,   >),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3]),
                5=>作業配列.MakeGenericType(typeof(ClassTuple<,,,,  >),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4]),
                6=>作業配列.MakeGenericType(typeof(ClassTuple<,,,,, >),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4],FieldTypes[5]),
                _=>作業配列.MakeGenericType(typeof(ClassTuple<,,,,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4],FieldTypes[5],FieldTypes[6])
            };
        } else{
            //Switch 16%7=2
            //Offset 16-2=14
            //1,2,3,4,5,6,7,(8,9,10)
            //1,2,3,4,5,6,7,(8,9,10,11,12,13,14,15,(16))
            //0,1,2,3,4,5,6,(7,8,9,10,11,12,13,(14,15))16個
            //0,1,2,3,4,5,6,(7,8,9,10,11,12,13,(14,15,16,17,18,19,20))21個
            DispType=Switch switch{
                1=>作業配列.MakeGenericType(typeof(ValueTuple<      >),FieldTypes[Offset+0 ]),
                2=>作業配列.MakeGenericType(typeof(ValueTuple<,     >),FieldTypes[Offset+0 ],FieldTypes[Offset+1]),
                3=>作業配列.MakeGenericType(typeof(ValueTuple<,,    >),FieldTypes[Offset+0 ],FieldTypes[Offset+1],FieldTypes[Offset+2]),
                4=>作業配列.MakeGenericType(typeof(ValueTuple<,,,   >),FieldTypes[Offset+0 ],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3]),
                5=>作業配列.MakeGenericType(typeof(ValueTuple<,,,,  >),FieldTypes[Offset+0 ],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4]),
                6=>作業配列.MakeGenericType(typeof(ValueTuple<,,,,, >),FieldTypes[Offset+0 ],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4],FieldTypes[Offset+5]),
                _=>作業配列.MakeGenericType(typeof(ValueTuple<,,,,,,>),FieldTypes[Offset-=7],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4],FieldTypes[Offset+5],FieldTypes[Offset+6])
            };
            var Types8=作業配列.Types8;
            while((Offset-=7)>=0){
                Debug.Assert(Offset%7==0);
                Types8[0]=FieldTypes[Offset+0];
                Types8[1]=FieldTypes[Offset+1];
                Types8[2]=FieldTypes[Offset+2];
                Types8[3]=FieldTypes[Offset+3];
                Types8[4]=FieldTypes[Offset+4];
                Types8[5]=FieldTypes[Offset+5];
                Types8[6]=FieldTypes[Offset+6];
                Types8[7]=DispType;
                DispType=(Offset==0?typeof(ClassTuple<,,,,,,,>):typeof(ValueTuple<,,,,,,,>)).MakeGenericType(Types8);
            }
        }
        Debug.Assert(DispType.IsClass);
        var Disp=Activator.CreateInstance(DispType)!;
        var DispParameter=Expression.Parameter(DispType,"Tuple");
        this.DispParameter=DispParameter;
        {
            Expression TupleExpression = DispParameter;
            var DispType0 = DispType;
            var Disp0 = Disp;
            var Item番号 = 2;//1はContainerが入る
            foreach(var a in DictionaryConstant.AsEnumerable())
                DictionaryConstant[a.Key]=(default!,ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression,a.Key.Value));
            foreach(var a in DictionaryDynamic.AsEnumerable()){
                var Dynamic=a.Key;
                var CallSite0=CallSite.Create(this.Dynamicに対応するFunc(Dynamic),Dynamic.Binder);
                DictionaryDynamic[Dynamic]=(default!,ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression,CallSite0));
            }
            foreach(var a in DictionaryLambda.AsEnumerable())
                DictionaryLambda[a.Key]=(default!,ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression),default!);
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable())
                Dictionaryラムダ跨ぎParameter[a.Key]=(default!,ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression));
        }
        return (Disp,DispParameter);
    }
    internal static class DynamicReflection {
        public static readonly CSharpArgumentInfo CSharpArgumentInfo = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null);
        public static CSharpArgumentInfo[] CSharpArgumentInfoArray(int Count){
            var Array=new CSharpArgumentInfo[Count];
            for(var a=0;a<Count;a++)
                Array[a]=CSharpArgumentInfo;
            return Array;
        }
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray1={CSharpArgumentInfo};
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray2={CSharpArgumentInfo,CSharpArgumentInfo};
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray3={CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo};
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray4={CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo};
        public static class CallSites {
            public static readonly FieldInfo ObjectObjectObjectObjectTarget=typeof(CallSite<Func<CallSite,object,object,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object,object,object>>.Target))!;
            public static readonly FieldInfo ObjectObjectObjectTarget=typeof(CallSite<Func<CallSite,object,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object,object>>.Target))!;
            public static readonly FieldInfo ObjectObjectTarget=typeof(CallSite<Func<CallSite,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object>>.Target))!;
            public static readonly FieldInfo ObjectBooleanTarget=typeof(CallSite<Func<CallSite,object,bool>>).GetField(nameof(CallSite<Func<CallSite,object,bool>>.Target))!;

            private static CallSite<Func<CallSite,object,object,object>> CallSite_Binary(ExpressionType NodeType)=> CallSite<Func<CallSite,object,object,object>>.Create(RuntimeBinder.Binder.BinaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray2));
            public static readonly CallSite<Func<CallSite,object,object,object>> Add               =CallSite_Binary(ExpressionType.Add);
            public static readonly CallSite<Func<CallSite,object,object,object>> AddAssign         =CallSite_Binary(ExpressionType.AddAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> And               =CallSite_Binary(ExpressionType.And);
            public static readonly CallSite<Func<CallSite,object,object,object>> AndAssign         =CallSite_Binary(ExpressionType.AndAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Divide            =CallSite_Binary(ExpressionType.Divide);
            public static readonly CallSite<Func<CallSite,object,object,object>> DivideAssign      =CallSite_Binary(ExpressionType.DivideAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Equal             =CallSite_Binary(ExpressionType.Equal);
            public static readonly CallSite<Func<CallSite,object,object,object>> ExclusiveOr       =CallSite_Binary(ExpressionType.ExclusiveOr);
            public static readonly CallSite<Func<CallSite,object,object,object>> ExclusiveOrAssign =CallSite_Binary(ExpressionType.ExclusiveOrAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> GreaterThan       =CallSite_Binary(ExpressionType.GreaterThan);
            public static readonly CallSite<Func<CallSite,object,object,object>> GreaterThanOrEqual=CallSite_Binary(ExpressionType.GreaterThanOrEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> LeftShift         =CallSite_Binary(ExpressionType.LeftShift);
            public static readonly CallSite<Func<CallSite,object,object,object>> LeftShiftAssign   =CallSite_Binary(ExpressionType.LeftShiftAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> LessThan          =CallSite_Binary(ExpressionType.LessThan);
            public static readonly CallSite<Func<CallSite,object,object,object>> LessThanOrEqual   =CallSite_Binary(ExpressionType.LessThanOrEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> Modulo            =CallSite_Binary(ExpressionType.Modulo);
            public static readonly CallSite<Func<CallSite,object,object,object>> ModuloAssign      =CallSite_Binary(ExpressionType.ModuloAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Multiply          =CallSite_Binary(ExpressionType.Multiply);
            public static readonly CallSite<Func<CallSite,object,object,object>> MultiplyAssign    =CallSite_Binary(ExpressionType.MultiplyAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> NotEqual          =CallSite_Binary(ExpressionType.NotEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> Or                =CallSite_Binary(ExpressionType.Or);
            public static readonly CallSite<Func<CallSite,object,object,object>> OrAssign          =CallSite_Binary(ExpressionType.OrAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> RightShift        =CallSite_Binary(ExpressionType.RightShift);
            public static readonly CallSite<Func<CallSite,object,object,object>> RightShiftAssign  =CallSite_Binary(ExpressionType.RightShiftAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Subtract          =CallSite_Binary(ExpressionType.Subtract);
            public static readonly CallSite<Func<CallSite,object,object,object>> SubtractAssign    =CallSite_Binary(ExpressionType.SubtractAssign);
            private static CallSite<T> CallSite_Unary<T>(ExpressionType NodeType) where T:class=>CallSite<T>.Create(RuntimeBinder.Binder.UnaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray1));
            private static CallSite<Func<CallSite,object,object>> CallSite_Unary(ExpressionType NodeType)=> CallSite_Unary<Func<CallSite,object,object>>(NodeType);
            public static readonly CallSite<Func<CallSite,object,object>> Decrement     =CallSite_Unary(ExpressionType.Decrement);
            public static readonly CallSite<Func<CallSite,object,object>> Increment     =CallSite_Unary(ExpressionType.Increment);
            public static readonly CallSite<Func<CallSite,object,object>> Negate        =CallSite_Unary(ExpressionType.Negate);
            public static readonly CallSite<Func<CallSite,object,object>> Not           =CallSite_Unary(ExpressionType.Not);
            public static readonly CallSite<Func<CallSite,object,object>> OnesComplement=CallSite_Unary(ExpressionType.OnesComplement);
            public static readonly CallSite<Func<CallSite,object,object>> UnaryPlus     =CallSite_Unary(ExpressionType.UnaryPlus);
            private static CallSite<Func<CallSite,object,bool>> CallSite_IsFalse_IsTrue(ExpressionType NodeType)=>CallSite_Unary<Func<CallSite,object,bool>>(NodeType);
            public static readonly CallSite<Func<CallSite,object,bool>> IsFalse=CallSite_IsFalse_IsTrue(ExpressionType.IsFalse);
            public static readonly CallSite<Func<CallSite,object,bool>> IsTrue =CallSite_IsFalse_IsTrue(ExpressionType.IsTrue);
            public static readonly CallSite<Func<CallSite,object,object,object>> GetIndex=CallSite<Func<CallSite,object,object,object>>.Create(
                RuntimeBinder.Binder.GetIndex(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(DynamicReflection),
                    CSharpArgumentInfoArray2
                )
            );
            public static readonly CallSite<Func<CallSite,object,object,object,object>> SetIndex=CallSite<Func<CallSite,object,object,object,object>>.Create(
                RuntimeBinder.Binder.SetIndex(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(DynamicReflection),
                    CSharpArgumentInfoArray3
                )
            );
        }

        private static(FieldInfo Target,MethodInfo Invoke) F(Type CallSite){
            var Target=CallSite.GetField("Target");
            Debug.Assert(Target is not null);
            var Invoke=Target.FieldType.GetMethod("Invoke");
            Debug.Assert(Invoke is not null);
            return(Target,Invoke);
        }
        public static readonly (FieldInfo Target,MethodInfo Invoke)ObjectObjectObjectObject=F(typeof(CallSite<Func<CallSite,object,object,object,object>>));
        public static readonly (FieldInfo Target,MethodInfo Invoke)ObjectObjectObject      =F(typeof(CallSite<Func<CallSite,object,object,object>>));
        public static readonly (FieldInfo Target,MethodInfo Invoke)ObjectObject            =F(typeof(CallSite<Func<CallSite,object,object>>));
        public static readonly (FieldInfo Target,MethodInfo Invoke)ObjectBoolea            =F(typeof(CallSite<Func<CallSite,object,bool>>));
        private static FieldInfo F(string フィールド名)=> typeof(CallSites).GetField(フィールド名,Static_NonPublic_Public)!;
        public static readonly FieldInfo Add               =F(nameof(Add));
        public static readonly FieldInfo AddAssign         =F(nameof(AddAssign));
        public static readonly FieldInfo And               =F(nameof(And));
        public static readonly FieldInfo AndAssign         =F(nameof(AndAssign));
        public static readonly FieldInfo Divide            =F(nameof(Divide));
        public static readonly FieldInfo DivideAssign      =F(nameof(DivideAssign));
        public static readonly FieldInfo Equal             =F(nameof(Equal));
        public static readonly FieldInfo ExclusiveOr       =F(nameof(ExclusiveOr));
        public static readonly FieldInfo ExclusiveOrAssign =F(nameof(ExclusiveOrAssign));
        public static readonly FieldInfo GreaterThan       =F(nameof(GreaterThan));
        public static readonly FieldInfo GreaterThanOrEqual=F(nameof(GreaterThanOrEqual));
        public static readonly FieldInfo LeftShift         =F(nameof(LeftShift));
        public static readonly FieldInfo LeftShiftAssign   =F(nameof(LeftShiftAssign));
        public static readonly FieldInfo LessThan          =F(nameof(LessThan));
        public static readonly FieldInfo LessThanOrEqual   =F(nameof(LessThanOrEqual));
        public static readonly FieldInfo Modulo            =F(nameof(Modulo));
        public static readonly FieldInfo ModuloAssign      =F(nameof(ModuloAssign));
        public static readonly FieldInfo Multiply          =F(nameof(Multiply));
        public static readonly FieldInfo MultiplyAssign    =F(nameof(MultiplyAssign));
        public static readonly FieldInfo NotEqual          =F(nameof(NotEqual));
        public static readonly FieldInfo Or                =F(nameof(Or));
        public static readonly FieldInfo OrAssign          =F(nameof(OrAssign));
        public static readonly FieldInfo RightShift        =F(nameof(RightShift));
        public static readonly FieldInfo RightShiftAssign  =F(nameof(RightShiftAssign));
        public static readonly FieldInfo Subtract          =F(nameof(Subtract));
        public static readonly FieldInfo SubtractAssign    =F(nameof(SubtractAssign));

        public static readonly FieldInfo Decrement         =F(nameof(Decrement));
        public static readonly FieldInfo Increment         =F(nameof(Increment));
        public static readonly FieldInfo Negate            =F(nameof(Negate));
        public static readonly FieldInfo Not               =F(nameof(Not));
        public static readonly FieldInfo OnesComplement    =F(nameof(OnesComplement));
        public static readonly FieldInfo UnaryPlus         =F(nameof(UnaryPlus));
        public static readonly FieldInfo IsFalse           =F(nameof(IsFalse));
        public static readonly FieldInfo IsTrue            =F(nameof(IsTrue));

        public static readonly FieldInfo GetIndex          =F(nameof(GetIndex));
        public static readonly FieldInfo SetIndex          =F(nameof(SetIndex));
    }

    private static string 整形したDebugView(Expression Lambda) {
        var 旧String = インラインラムダテキスト(Lambda);
        var 新StringBuilder = new StringBuilder();
        //var キー定義 = new Regex(@"^\..*>\( \{$",RegexOptions.Singleline);
        var キー本体 = new Regex(@"^\..*\}$",RegexOptions.Singleline);
        {
            var キー参照 = new Regex(@"\..*>\)",RegexOptions.Multiline);
            for(var キー定義_Match = キー本体.Match(旧String);キー定義_Match.Success;キー定義_Match=キー定義_Match.NextMatch()) {
                var 前index = 0;
                for(var キー参照_Match = キー参照.Match(旧String);キー参照_Match.Success;キー参照_Match=キー参照_Match.NextMatch()) {
                    var 後index = キー参照_Match.Index;
                    新StringBuilder.Append(旧String[前index..後index]);
                    前index=後index;
                    新StringBuilder.Append(キー定義_Match.Value);
                    新StringBuilder.Append(')');
                }
                旧String=新StringBuilder.ToString();
                新StringBuilder.Clear();
            }
        }
        {
            var キー参照 = new Regex(@"\..*>,",RegexOptions.Multiline);
            for(var キー定義_Match = キー本体.Match(旧String);キー定義_Match.Success;キー定義_Match=キー定義_Match.NextMatch()) {
                var 前index = 0;
                for(var キー参照_Match = キー参照.Match(旧String);キー参照_Match.Success;キー参照_Match=キー参照_Match.NextMatch()) {
                    var 後index = キー参照_Match.Index;
                    新StringBuilder.Append(旧String[前index..後index]);
                    前index=後index;
                    新StringBuilder.Append(キー定義_Match.Value);
                    新StringBuilder.Append(',');
                }
                旧String=新StringBuilder.ToString();
                新StringBuilder.Clear();
            }
        }
        return 旧String;
    }
    //private static readonly Stopwatch 標準 = new Stopwatch(), 独自 = new Stopwatch();
    private static void 実行計画文字列のAssert(string[] expecteds,string テキスト化された実行計画) {
        if(expecteds.Length>0) {
            var sb = new StringBuilder();
            foreach(var expected in expecteds)
                sb.AppendLine(expected);
            Debug.Assert(テキスト化された実行計画.Length==sb.Length);
            for(var a = 0;a<テキスト化された実行計画.Length;a++)
                Debug.Assert(テキスト化された実行計画[a]==sb[a]);
        }
    }
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<TResult>(Expression<Func<TResult>> Lambda){
        var Delegate=this.CreateDelegate(Lambda);
        return Delegate();
    }

    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値</param>
    /// <typeparam name="T">入力値のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T, TResult>(Expression<Func<T,TResult>> Lambda,T Input1) =>
        this.CreateDelegate(Lambda)(Input1);
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値1</param>
    /// <param name="Input2">入力値2</param>
    /// <typeparam name="T1">入力値1のType</typeparam>
    /// <typeparam name="T2">入力値2のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T1, T2, TResult>(Expression<Func<T1,T2,TResult>> Lambda,T1 Input1,T2 Input2) =>
        this.CreateDelegate(Lambda)(Input1,Input2);
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値1</param>
    /// <param name="Input2">入力値2</param>
    /// <param name="Input3">入力値3</param>
    /// <typeparam name="T1">入力値1のType</typeparam>
    /// <typeparam name="T2">入力値2のType</typeparam>
    /// <typeparam name="T3">入力値2のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T1, T2, T3, TResult>(Expression<Func<T1,T2,T3,TResult>> Lambda,T1 Input1,T2 Input2,T3 Input3) =>
        this.CreateDelegate(Lambda)(Input1,Input2,Input3);
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値1</param>
    /// <param name="Input2">入力値2</param>
    /// <param name="Input3">入力値3</param>
    /// <param name="Input4">入力値4</param>
    /// <typeparam name="T1">入力値1のType</typeparam>
    /// <typeparam name="T2">入力値2のType</typeparam>
    /// <typeparam name="T3">入力値3のType</typeparam>
    /// <typeparam name="T4">入力値4のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T1, T2, T3, T4, TResult>(Expression<Func<T1,T2,T3,T4,TResult>> Lambda,T1 Input1,T2 Input2,T3 Input3,T4 Input4) =>
        this.CreateDelegate(Lambda)(Input1,Input2,Input3,Input4);
    ///// <summary>
    ///// F#のFSharpExpr(Expr)からデリゲートを作って呼び出す
    ///// </summary>
    ///// <param name="Expr">ラムダを表すexpr</param>
    ///// 
    ///// 
    ///// 
    ///// <typeparam name="TResult">戻り値のType</typeparam>
    ///// <returns>実行結果</returns>
    //public TResult Execute<TResult>(FSharpExpr<TResult> Expr) =>
    //    this.CreateDelegate(
    //        Expression.Lambda<Func<TResult>>(
    //            LeafExpressionConverter.QuotationToExpression(Expr),
    //            Array.Empty<ParameterExpression>()
    //        )
    //    )();
    /// <summary>
    /// Lambdaを最適化してデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda"></param>
    public void Execute(Expression<Action> Lambda) =>
        this.CreateDelegate(Lambda)();
    //private static readonly Regex RegexLambda=new("[#].*{",RegexOptions.Compiled);
    private static readonly Regex Regex単純な識別子=new("[^ ,^$,^#,^<,^[,^>,^(]*[.]",RegexOptions.Compiled);
    /// <summary>
    /// 式木をわかりやすくテキストにする
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string インラインラムダテキスト(Expression e){
        //[^ ]*[.]
        dynamic NonPublicAccessor = new NonPublicAccessor(typeof(Expression),e);
        var 変換前 = NonPublicAccessor.DebugView;
        //変換前=変換前.Replace("LinqDB.Sets.","");
        //変換前=変換前.Replace("System.Collections.Generic.","");
        //変換前=変換前.Replace("System.Collections.","");
        //変換前=変換前.Replace("System.","");
        変換前=変換前.Replace("\r\n{","{");
        var KeyValues = new Generic.List<(string Key,Generic.List<string> Values)>();
        {
            var r = new StringReader(変換前);
            var ラムダ式定義 = new Generic.List<string>();
            var ラムダ定義の1行目 = new Regex(@"^\.Lambda .*\(.*$",RegexOptions.Compiled);
            while(true) {
                var Line = r.ReadLine();
                if(string.IsNullOrEmpty(Line)) {
                    KeyValues.Add(("", ラムダ式定義));
                    break;
                }
                ラムダ式定義.Add(Line);
            }
            while(true) {
                var Line = r.ReadLine();
                if(Line is null)
                    break;
                if(ラムダ定義の1行目.IsMatch(Line)) {
                    var Key = Line[..Line.IndexOf("(",StringComparison.Ordinal)];
                    ラムダ式定義=new Generic.List<string>();
                    KeyValues.Add((Key, ラムダ式定義));
                    ラムダ式定義.Add(Line);
                } else if(string.Equals(Line,"}",StringComparison.Ordinal)) {
                    ラムダ式定義.Add(Line);
                } else if(!string.Equals(Line,string.Empty,StringComparison.Ordinal)) {
                    if(Line[^2]=='>'&&Line[^1]==')')Line=Line.Substring(0,Line.Length-1);
                    ラムダ式定義.Add(Line);
                }
            }
        }
        {
            //Debug.Assert(ルートラムダ is not null,"ルートラムダ != null");
            //var ルートラムダ2=ルートラムダ;
            //再試行:
            //var r = new StringReader(ルートラムダ);
            //var sb = new StringBuilder();
            while(true) {
                for(var a = KeyValues.Count-1;a>=0;a--) {
                    var a_KeyValue = KeyValues[a];
                    var a_Values = a_KeyValue.Values;
                    for(var b = a_Values.Count-1;b>=0;b--) {
                        var 変換元Line = a_Values[b];
                        for(var c = KeyValues.Count-1;c>=0;c--) {
                            var 変換先Value = KeyValues[c];
                            var 変換先キー = 変換先Value.Key;
                            var Lambda位置Index = 変換元Line.IndexOf(変換先キー,StringComparison.Ordinal);
                            if(Lambda位置Index>0) {
                                var 変換先Values = 変換先Value.Values;
                                a_Values[b]=変換元Line[..Lambda位置Index]+変換先Values[0];
                                var Index = 変換元Line.TakeWhile(文字 => 文字==' ').Count();
                                for(var d = 1;d<変換先Values.Count-1;d++)
                                    a_Values.Insert(b+d,new string(' ',Index)+変換先Values[d]);
                                a_Values.Insert(b+変換先Values.Count-1,new string(' ',Index)+変換先Values[^1]+変換元Line[(Lambda位置Index+変換先キー.Length)..]);
                                break;
                            }
                        }
                    }
                }
                var sb = new StringBuilder();
                foreach(var Value in KeyValues[0].Values) sb.AppendLine(Value);
                //var RegxLambda = new Regex(@"\.Lambda #Lambda.*<",RegexOptions.Compiled);
                //foreach(var Value in KeyValues[0].Values) {
                //    var Value2 = RegxLambda.Replace(Value,"");
                //    if(Value2!=Value) {
                //        Value2=Value2.Replace(">(","(");
                //    }
                //    sb.AppendLine(Value2);
                //}
                var Result1 = sb.ToString();
                var Constant = new Regex(@"\.Constant<.*?>\(.*?\)",RegexOptions.Compiled);
                var Result2 = Constant.Replace(Result1,"");
                //var Result3 = Result2.Replace("ExtensionSet","");
                //var Result4 = Result3.Replace("ExtensionEnumerable","");
                //var NodeType= new Regex(@"#Lambda.*\(",RegexOptions.Compiled);
                //var Result5 = NodeType.Replace(Result4,"(");
                var NodeTypeを除く = new Regex(@" \..*? ",RegexOptions.Compiled);
                //var Regx5= new Regex(@"Lambda #Lambda.*<",RegexOptions.Compiled);
                var Result5 = NodeTypeを除く.Replace(Result2," ");
                //var Result6 = Result5.Replace(" ."," ");
                if(Result5[0]=='.') Result5=Result5[1..];
                var Result6=Regex単純な識別子.Replace(Result5,"");
                return Result6;
            }
        }
    }
    public string 命令ツリー(Expression Expression)=>this._取得_命令ツリー.実行(Expression);
    private static object Set_ValueTuple(object ValueTuple,int Index,object Value) {
        switch(Index) {
            case 0:ValueTuple.GetType().GetField("Item1")!.SetValue(ValueTuple,Value);break;
            case 1:ValueTuple.GetType().GetField("Item2")!.SetValue(ValueTuple,Value);break;
            case 2:ValueTuple.GetType().GetField("Item3")!.SetValue(ValueTuple,Value);break;
            case 3:ValueTuple.GetType().GetField("Item4")!.SetValue(ValueTuple,Value);break;
            case 4:ValueTuple.GetType().GetField("Item5")!.SetValue(ValueTuple,Value);break;
            case 5:ValueTuple.GetType().GetField("Item6")!.SetValue(ValueTuple,Value);break;
            case 6:ValueTuple.GetType().GetField("Item7")!.SetValue(ValueTuple,Value);break;
            default:
                var Rest = ValueTuple.GetType().GetField("Rest")!;
                Rest.SetValue(
                    ValueTuple,
                    Set_ValueTuple(
                        Rest.GetValue(ValueTuple)!,
                        Index-7,
                        Value
                    )
                );
                break;
        }
        return ValueTuple;
    }
    ///// <summary>
    ///// 最適化レベル
    ///// </summary>
    //public OptimizeLevels OptimizeLevel { get; set; }
    private Type _Context= typeof(object);
    /// <summary>
    /// どのクラス内で実行するか指定
    /// </summary>
    public Type Context {
        get => this._Context;
        set => this._Context=value;
    }
    internal LambdaExpression Lambda非最適化(Expression Lambda00) {
        this.DictionaryConstant.Clear();
        var Lambda02=this._変換_メソッド正規化_取得インライン不可能定数.実行(Lambda00);
        //プロファイル=false;
        //var List計測 = new List<A計測>();
        //var ConstantList計測 = Expression.Constant(List計測);
        //プロファイル=false;
        //if(プロファイル)HashSetConstant.Add(ConstantList計測);
        this._検証_変形状態.実行(Lambda02);
        return (LambdaExpression)Lambda02;
    }
    public LambdaExpression Lambda最適化(Expression Lambda00) {
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic=this.DictionaryDynamic;
        var DictionaryLambda=this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        DictionaryConstant.Clear();
        DictionaryDynamic.Clear();
        DictionaryLambda.Clear();
        Dictionaryラムダ跨ぎParameter.Clear();
        this.ListスコープParameter.Clear();
        //分離するアイデア。ここで以下を取得する
        //DictionaryConstant add

        var Lambda01 = this._変換_KeySelectorの匿名型をValueTuple.実行(Lambda00);
        //以下で更新されるコレクション
        //DictionaryConstant read
        var Lambda02=this._変換_メソッド正規化_取得インライン不可能定数.実行(Lambda01);
        //プロファイル=false;
        //var List計測 = new List<A計測>();
        //var ConstantList計測 = Expression.Constant(List計測);
        //プロファイル=false;
        //if(プロファイル)HashSetConstant.Add(ConstantList計測);
        var Lambda04 = this._変換_WhereからLookup.実行(Lambda02);

        //Dictionaryラムダ跨ぎParameter add
        var Lambda05 = this._変換_跨ぎParameterの先行評価.実行(Lambda04);
        var Lambda06 = this._変換_跨ぎParameterの不要置換復元.実行(Lambda05);
        var Lambda07 = this._変換_局所Parameterの先行評価.実行(Lambda06);
        this._検証_変形状態.実行(Lambda07);
        var Lambda08 =this.IsInline?this._変換_インラインループ独立.実行(Lambda07):Lambda07;
        //DictionaryDynamic add
        //DictionaryLambda  add
        //Dictionaryラムダ跨ぎParameter read
        //var Lambda09=this._変換_跨ぎParameterをBlock_Variablesに.実行(Lambda08);
        //分離するアイデア。ここで以下を取得する
        //DictionaryLambda,DictionaryDynamic add
        return (LambdaExpression)Lambda08;
    }
    private static bool equals(object obj) => obj is string&&obj.Equals("ABC");
}
//2122 20220516
//2708 20220514
//3186 20220513
//2773 20220504
