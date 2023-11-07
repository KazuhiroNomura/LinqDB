using LinqDB.Databases.Dom;
using LinqDB.Helpers;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using LinqDB.Optimizers.Comparison;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using Array = System.Array;
//using Microsoft.CSharp.RuntimeBinder;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;
using AssemblyGenerator = Lokad.ILPack.AssemblyGenerator;
using Container = LinqDB.Databases.Container;
using Delegate = System.Delegate;
using ExtensionSet = LinqDB.Reflection.ExtensionSet;
using Regex = System.Text.RegularExpressions.Regex;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.CSharp.RuntimeBinder;
using static LinqDB.Optimizers.Common;
using LinqDB.Optimizers.VoidExpressionTraverser;
using LinqDB.Optimizers.VoidTSqlFragmentTraverser;
// ReSharper disable All
namespace LinqDB.Optimizers;
using Generic = System.Collections.Generic;
/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer:IDisposable {
    ////private const Int32 プリフィックス長 = 5;
    ////private const String Cラムダ跨 = "ラムダ跨ﾟ";
    ////private const String Cループ跨 = "ループ跨ﾟ";
    ////private const String Cローカル = "ローカルﾟ";
    ////private static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス) => Parameter.Name is not null&&Parameter.Name.StartsWith(プリフィックス,StringComparison.Ordinal);
    ////private static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス0,String プリフィックス1)=>
    ////    Parameter.Name is not null&&(Parameter.Name.StartsWith(プリフィックス0,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス1,StringComparison.Ordinal));
    ////private static Boolean プリフィックス一致(ParameterExpression Parameter,String プリフィックス0,String プリフィックス1,String プリフィックス2)=>
    ////    Parameter.Name is not null&&(Parameter.Name.StartsWith(プリフィックス0,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス1,StringComparison.Ordinal)||Parameter.Name.StartsWith(プリフィックス2,StringComparison.Ordinal));
    ////private const String Cラムダ局所 = nameof(Cラムダ局所);//ラムダの内部のラムダを跨がない2回以上評価される共通部分式の局所変数による最適化
    ////private const String Pラムダ引数 = nameof(Pラムダ引数);
    ////private const String Pワーク変数 = nameof(Pワーク変数);
    ////private const String Lループ = nameof(Lループ);//ループ展開されるラムダ名称
    ////private const String Lラムダ = nameof(Lラムダ);//ループ展開されないラムダ名称
    ////private const String D動的Get = nameof(D動的Get);
    ////private const String D動的Set = nameof(D動的Set);
    ////private const Int32 プレフィックス長 = 1;
    ////private const String Lラムダ跨 = ".";//自由変数,束縛変数,評価変数
    ////private const String Lループ跨 = "L";//ループに展開されるラムダを跨ぐ外だし出来る先行評価共通部分式の局所変数による最適化
    ////private const String A自動変数 = "A";//ラムダの内部のラムダを跨がない2回以上評価される共通部分式の局所変数による最適化
    ////private const String P関数引数 = "P";
    ////private const String W作業変数 = "W";
    ////private const String G動的変数 = "G";
    ////private const String S動的変数 = "S";
    ////private const String Lループ = nameof(Lループ);//ループ展開されるラムダ名称
    ////private const String Lラムダ = nameof(Lラムダ);//ループ展開されないラムダ名称
    ////private const String get_Item = nameof(get_Item);
    ////private const String op_Decrement        =nameof(op_Decrement);
    ////private const String op_Increment        =nameof(op_Increment );
    ////private const String op_Negation         =nameof(op_Negation);
    ////private const String op_UnaryNegation    =nameof(op_UnaryNegation);
    ////private const String op_UnaryPlus        =nameof(op_UnaryPlus);
    ////private const String op_Addition         =nameof(op_Addition);
    ////private const String op_Assign           =nameof(op_Assign);
    ////private const String op_BitwiseAnd=nameof(op_BitwiseAnd);
    ////private const String op_BitwiseOr=nameof(op_BitwiseOr);
    ////private const String op_Division         =nameof(op_Division);
    ////private const String op_Equality         =nameof(op_Equality);
    ////private const String op_ExclusiveOr      =nameof(op_ExclusiveOr);
    //private const string op_GreaterThan = nameof(op_GreaterThan);
    ////private const String op_GreaterThanOrEqual=nameof(op_GreaterThanOrEqual);
    ////private const String op_Inequality       =nameof(op_Inequality);
    ////private const String op_LeftShift        =nameof(op_LeftShift);
    //private const string op_LessThan = nameof(op_LessThan);
    ////private const String op_LessThanOrEqual  =nameof(op_LessThanOrEqual);
    ////private const String op_LogicalAnd       =nameof(op_LogicalAnd);
    ////private const String op_LogicalOr        =nameof(op_LogicalOr);
    ////private const String op_Modulus          =nameof(op_Modulus);
    ////private const String op_Multiply         =nameof(op_Multiply);
    ////private const String op_RightShift       =nameof(op_RightShift);
    ////private const String op_Subtraction      =nameof(op_Subtraction);
    ////private const String op_Implicit=nameof(op_Implicit);
    ////private const String op_Explicit=nameof(op_Explicit);
    //private const string op_True = nameof(op_True);
    //private const string op_False = nameof(op_False);
    ////private const BindingFlags Instance_NonPublic_Public =BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public;
    ////private const BindingFlags Static_NonPublic_Public =BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public;
    ////private const BindingFlags Static_NonPublic = BindingFlags.Static|BindingFlags.NonPublic;
    //private static readonly ConstantExpression Constant_false = Expression.Constant(false);
    //private static readonly ConstantExpression Constant_true = Expression.Constant(true);
    //private static readonly MemberExpression Constant_0M = Expression.Field(null,typeof(decimal).GetField(nameof(decimal.Zero)));
    //private static readonly MemberExpression Constant_1M = Expression.Field(null,typeof(decimal).GetField(nameof(decimal.One)));
    //private static readonly ConstantExpression Constant_0 = Expression.Constant(0);
    ////private static readonly ConstantExpression Constant_4 = Expression.Constant(4);
    ////private static readonly ConstantExpression Constant_8 = Expression.Constant(8);
    ////private static readonly ConstantExpression Constant_12 = Expression.Constant(12);
    ////private static readonly ConstantExpression Constant_13 = Expression.Constant(13);
    //private static readonly ConstantExpression Constant_100 = Expression.Constant(100);
    //private static readonly ConstantExpression Constant_100000 = Expression.Constant(100000);
    //private static readonly ConstantExpression Constant_1=Expression.Constant(1);
    //private static readonly ConstantExpression Constant_0L = Expression.Constant(0L);
    //private static readonly ConstantExpression Constant_1L=Expression.Constant(1L);
    //private static readonly ConstantExpression Constant_0F = Expression.Constant(0F);
    //private static readonly ConstantExpression Constant_1F = Expression.Constant(0F);
    //private static readonly ConstantExpression Constant_0D = Expression.Constant(0D);
    //private static readonly ConstantExpression Constant_1D = Expression.Constant(1D);
    //private static readonly ConstantExpression Constant_10D = Expression.Constant(10D);
    //private static readonly ConstantExpression Constant_null = Expression.Constant(null);
    //private static readonly DefaultExpression Default_void = Expression.Empty();
    public static bool 変化したか(MethodCallExpression MethodCall,Expression Object,Generic.IEnumerable<Expression> Arguments) =>
        MethodCall.Object==Object&&MethodCall.Arguments==Arguments;
    //private static Expression Convert必要なら(Expression e,Type Type) => Type!=e.Type
    //    ? Expression.Convert(
    //        e,
    //        Type
    //    )
    //    : e;
    //private static (Expression プローブ,Expression ビルド)ValueTupleでNewする(作業配列 作業配列,Generic.IList<(Expression プローブ, Expression ビルド)> Listプローブビルド,int Offset) {
    //    var 残りType数 = Listプローブビルド.Count-Offset;
    //    switch(残りType数) {
    //        case 1:return (
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple1,
    //                    Listプローブビルド[Offset+0].プローブ.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].プローブ
    //                )
    //            ),
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple1,
    //                    Listプローブビルド[Offset+0].ビルド.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].ビルド
    //                )
    //            )
    //        );
    //        case 2:return (
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple2,
    //                    Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ
    //                )
    //            ),
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple2,
    //                    Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド
    //                )
    //            )
    //        );
    //        case 3:return (
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple3,
    //                    Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ
    //                )
    //            ),
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple3,
    //                    Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド
    //                )
    //            )
    //        );
    //        case 4:return (
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple4,
    //                    Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ
    //                )
    //            ),
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple4,
    //                    Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド
    //                )
    //            )
    //        );
    //        case 5:return (
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple5,
    //                    Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ
    //                )
    //            ),
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple5,
    //                    Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド
    //                )
    //            )
    //        );
    //        case 6:return (
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple6,
    //                    Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ     ,Listプローブビルド[Offset+5].プローブ
    //                )
    //            ),
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple6,
    //                    Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type  ,Listプローブビルド[Offset+5].ビルド.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド       ,Listプローブビルド[Offset+5].ビルド
    //                )
    //            )
    //        );
    //        case 7:return (
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple7,
    //                    Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type,Listプローブビルド[Offset+6].プローブ.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ     ,Listプローブビルド[Offset+5].プローブ     ,Listプローブビルド[Offset+6].プローブ
    //                )
    //            ),
    //            Expression.New(
    //                作業配列.MakeValueTuple_ctor(
    //                    Reflection.ValueTuple.ValueTuple7,
    //                    Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type  ,Listプローブビルド[Offset+5].ビルド.Type  ,Listプローブビルド[Offset+6].ビルド.Type
    //                ),
    //                作業配列.Expressions設定(
    //                    Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド       ,Listプローブビルド[Offset+5].ビルド       ,Listプローブビルド[Offset+6].ビルド
    //                )
    //            )
    //        );
    //        default: {
    //            var (プローブ, ビルド)=ValueTupleでNewする(作業配列,Listプローブビルド,Offset+7);
    //            return (
    //                Expression.New(
    //                    作業配列.MakeValueTuple_ctor(
    //                        Reflection.ValueTuple.ValueTuple8,
    //                        Listプローブビルド[Offset+0].プローブ.Type,Listプローブビルド[Offset+1].プローブ.Type,Listプローブビルド[Offset+2].プローブ.Type,Listプローブビルド[Offset+3].プローブ.Type,Listプローブビルド[Offset+4].プローブ.Type,Listプローブビルド[Offset+5].プローブ.Type,Listプローブビルド[Offset+6].プローブ.Type,
    //                        プローブ.Type
    //                    ),
    //                    作業配列.Expressions設定(
    //                        Listプローブビルド[Offset+0].プローブ     ,Listプローブビルド[Offset+1].プローブ     ,Listプローブビルド[Offset+2].プローブ     ,Listプローブビルド[Offset+3].プローブ     ,Listプローブビルド[Offset+4].プローブ     ,Listプローブビルド[Offset+5].プローブ     ,Listプローブビルド[Offset+6].プローブ     ,
    //                        プローブ
    //                    )
    //                ),
    //                Expression.New(
    //                    作業配列.MakeValueTuple_ctor(
    //                        Reflection.ValueTuple.ValueTuple8,
    //                        Listプローブビルド[Offset+0].ビルド.Type  ,Listプローブビルド[Offset+1].ビルド.Type  ,Listプローブビルド[Offset+2].ビルド.Type  ,Listプローブビルド[Offset+3].ビルド.Type  ,Listプローブビルド[Offset+4].ビルド.Type  ,Listプローブビルド[Offset+5].ビルド.Type  ,Listプローブビルド[Offset+6].ビルド.Type  ,
    //                        ビルド.Type
    //                    ) ,
    //                    作業配列.Expressions設定(
    //                        Listプローブビルド[Offset+0].ビルド       ,Listプローブビルド[Offset+1].ビルド       ,Listプローブビルド[Offset+2].ビルド       ,Listプローブビルド[Offset+3].ビルド       ,Listプローブビルド[Offset+4].ビルド       ,Listプローブビルド[Offset+5].ビルド       ,Listプローブビルド[Offset+6].ビルド       ,
    //                        ビルド
    //                    )
    //                )
    //            );
    //        }
    //    }
    //}
    //private static NewExpression ValueTupleでNewする(作業配列 作業配列,Generic.IList<Expression> Arguments) {
    //    return CommonLibrary.ValueTupleでNewする(作業配列,Arguments);
    //}
    //private static bool ILで直接埋め込めるか(Type Type) =>
    //    Type.IsPrimitive||Type.IsEnum||Type==typeof(string);
    ///// <summary>
    ///// Constant定数がILに直接埋め込めるか判定する
    ///// </summary>
    ///// <param name="Constant"></param>
    ///// <returns>ILに埋め込めるか</returns>
    //private static bool ILで直接埋め込めるか(ConstantExpression Constant) =>
    //    !Constant.Type.IsValueType&&Constant.Value is null||ILで直接埋め込めるか(Constant.Type);
    //private static MethodCallExpression? ループ展開可能なSetのCall(Expression e) {
    //    if(e.NodeType!=ExpressionType.Call)
    //        return null;
    //    var MethodCall = (MethodCallExpression)e;
    //    return MethodCall.Method.DeclaringType==typeof(Sets.ExtensionSet)
    //        ? MethodCall
    //        : null;
    //}
    //private static bool ループ展開可能メソッドか(MethodInfo GenericMethodDefinition) {
    //    Debug.Assert(!GenericMethodDefinition.IsGenericMethod||GenericMethodDefinition.IsGenericMethodDefinition);
    //    var DeclaringType = GenericMethodDefinition.DeclaringType;
    //    if(typeof(Enumerable)==DeclaringType) {
    //        var Name = GenericMethodDefinition.Name;
    //        if(
    //            nameof(Enumerable.DistinctBy)==Name||
    //            nameof(Enumerable.ExceptBy)==Name||
    //            nameof(Enumerable.IntersectBy)==Name||
    //            nameof(Enumerable.UnionBy)==Name||
    //            nameof(Enumerable.MaxBy)==Name||
    //            nameof(Enumerable.MinBy)==Name||
    //            nameof(Enumerable.Empty)==Name||
    //            nameof(Enumerable.OrderBy)==Name||nameof(Enumerable.OrderByDescending)==Name||
    //            nameof(Enumerable.ThenBy)==Name||nameof(Enumerable.ThenByDescending)==Name
    //        ) {
    //            return false;
    //        }
    //        return true;
    //    }
    //    return typeof(Sets.ExtensionEnumerable)==DeclaringType||typeof(Sets.ExtensionSet)==DeclaringType;
    //}
    //private static bool ループ展開可能メソッドか(Expression Expression,out MethodCallExpression MethodCall) {
    //    if(Expression is MethodCallExpression MethodCall0) {
    //        MethodCall=MethodCall0;
    //        return ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall.Method));
    //    }
    //    MethodCall=null!;
    //    return false;
    //}
    //private static bool ループ展開可能メソッドか(MethodCallExpression MethodCall) =>
    //    ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall.Method));
    //private static Expression LambdaExpressionを展開1(Expression Lambda,Expression argument,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression) {
    //    Debug.Assert(typeof(Delegate).IsAssignableFrom(Lambda.Type));
    //    return Lambda is LambdaExpression Lambda1
    //        ? 変換_旧Parameterを新Expression.実行(
    //            Lambda1.Body,
    //            Lambda1.Parameters[0],
    //            argument
    //        )
    //        : Expression.Invoke(
    //            Lambda,
    //            argument
    //        );
    //}
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
    //public class ブローブビルドExpressionEqualityComparer:Generic.IEqualityComparer<(Expression ビルド, Expression プローブ)> {
    //    private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    //    public ブローブビルドExpressionEqualityComparer(ExpressionEqualityComparer ExpressionEqualityComparer) => this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    //    public bool Equals((Expression ビルド, Expression プローブ) x,(Expression ビルド, Expression プローブ) y) {
    //        var ExpressionEqualityComparer = this.ExpressionEqualityComparer;
    //        if(!ExpressionEqualityComparer.Equals(x.プローブ,y.プローブ)) return false;
    //        if(!ExpressionEqualityComparer.Equals(x.ビルド,y.ビルド)) return false;
    //        return true;
    //    }
    //    public int GetHashCode((Expression ビルド, Expression プローブ) obj) => 0;
    //}
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
    private static Type IEnumerable1のT(Type Type) {
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
    private Generic.Dictionary<ConstantExpression,(FieldInfo Disp, MemberExpression Member)> DictionaryConstant {
        get => this._変換_メソッド正規化_取得インライン不可能定数.DictionaryConstant;
        set {
            this._取得_Dictionary.DictionaryConstant=value;
            this._変換_メソッド正規化_取得インライン不可能定数.DictionaryConstant=value;
            this.判定InstanceMethodか.DictionaryConstant=value;
            Debug.Assert(this._作成_DynamicMethod.DictionaryConstant==value);
            Debug.Assert(this._作成_DynamicAssembly.DictionaryConstant==value);
            //this._作成_DynamicMethod.DictionaryConstant=value;
            //this._作成_DynamicAssembly.DictionaryConstant=value;
        }
    }
    private Generic.Dictionary<DynamicExpression,(FieldInfo Disp, MemberExpression Member)> DictionaryDynamic {
        get => this._取得_Dictionary.DictionaryDynamic;
        set {
            this._取得_Dictionary.DictionaryDynamic=value;
            this._作成_DynamicMethod.DictionaryDynamic=value;
            this._作成_DynamicAssembly.DictionaryDynamic=value;
        }
    }
    private Generic.Dictionary<LambdaExpression,(FieldInfo Disp, MemberExpression Member, MethodBuilder Impl)> DictionaryLambda {
        get => this._取得_Dictionary.DictionaryLambda;
        set {
            this._取得_Dictionary.DictionaryLambda=value;
            this._作成_DynamicMethod.DictionaryLambda=value;
            this._作成_DynamicAssembly.DictionaryLambda=value;
        }
    }
    private Generic.Dictionary<ParameterExpression,(FieldInfo Disp, MemberExpression Member)> Dictionaryラムダ跨ぎParameter {
        get => this._取得_Dictionary.Dictionaryラムダ跨ぎParameter;
        set {
            this._取得_Dictionary.Dictionaryラムダ跨ぎParameter=value;
            this._変換_跨ぎParameterの先行評価.Dictionaryラムダ跨ぎParameter=value;
            this._変換_跨ぎParameterの不要置換復元.Dictionaryラムダ跨ぎParameter=value;
            this._変換_局所Parameterの先行評価.ラムダ跨ぎParameters=value.Keys;
            this._検証_Parameterの使用状態.ラムダ跨ぎParameters=value.Keys;
            this._作成_DynamicMethod.Dictionaryラムダ跨ぎParameter=value;
            this._作成_DynamicAssembly.Dictionaryラムダ跨ぎParameter=value;
        }
    }
    private ParameterExpression DispParameter {
        get => this._作成_DynamicMethod.DispParameter;
        set {
            this._作成_DynamicMethod.DispParameter=value;
            this._作成_DynamicAssembly.DispParameter=value;
        }
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Optimizer() : this(typeof(object)) {
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
        var 変換_旧Parameterを新Expression1 = new 変換_旧Parameterを新Expression1(作業配列);
        var ListスコープParameter = this.ListスコープParameter;
        var ExpressionEqualityComparer = this._ExpressionEqualityComparer=new();
        //TSQLでは1度だけnewすればいいが
        var 判定_InstanceMethodか = this.判定InstanceMethodか=new(ExpressionEqualityComparer);
        this._変換_TSqlFragment正規化=new(ScriptGenerator);
        var ブローブビルドExpressionEqualityComparer = new ブローブビルドExpressionEqualityComparer(ExpressionEqualityComparer);
        var 判定_指定Parameter無_他Parameter有 = new 判定_指定Parameter無_他Parameter有();
        var 判定_指定Parameters無 = new 判定_指定Parameters無();
        var 判定_指定Parameter有_他Parameter無_Lambda内部走査 = new 判定_指定Parameter有_他Parameter無_Lambda内部走査();
        var 取得_OuterPredicate_InnerPredicate_プローブビルド = new 取得_OuterPredicate_InnerPredicate_プローブビルド(作業配列,判定_指定Parameter無_他Parameter有,判定_指定Parameter有_他Parameter無_Lambda内部走査,ブローブビルドExpressionEqualityComparer);
        var 変換_旧Expressionを新Expression1 = new 変換_旧Expressionを新Expression1(作業配列,ExpressionEqualityComparer);
        this._変換_TSqlFragmentからExpression=new(作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド,ExpressionEqualityComparer,変換_旧Parameterを新Expression1,変換_旧Expressionを新Expression1,判定_指定Parameters無,ScriptGenerator);
        this._変換_KeySelectorの匿名型をValueTuple=new(作業配列);
        var 変換_旧Parameterを新Expression2 = new 変換_旧Parameterを新Expression2(作業配列);
        this._変換_メソッド正規化_取得インライン不可能定数=new(作業配列,変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2,変換_旧Expressionを新Expression1);
        this._変換_WhereからLookup=new(作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド,判定_指定Parameters無);
        var Listループ跨ぎParameter = this.Listループ跨ぎParameter;
        this._変換_跨ぎParameterの先行評価=new(作業配列,ExpressionEqualityComparer,Listループ跨ぎParameter);
        this._変換_跨ぎParameterの不要置換復元=new(作業配列);
        var ExpressionEqualityComparer_Assign_Leftで比較 = new ExpressionEqualityComparer_Assign_Leftで比較();
        this._変換_局所Parameterの先行評価=new(作業配列,ListスコープParameter,ExpressionEqualityComparer_Assign_Leftで比較);
        this._取得_Dictionary=new();
        this._検証_変形状態=new();
        this._検証_Parameterの使用状態=new(Listループ跨ぎParameter);
        this._変換_インラインループ独立=new(作業配列,ExpressionEqualityComparer_Assign_Leftで比較,変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2);
        this._変換_Stopwatchに埋め込む=new(作業配列);
        this._作成_DynamicMethod=new(判定_InstanceMethodか);
        this._作成_DynamicAssembly=new(判定_InstanceMethodか);
        this.DictionaryConstant=new(ExpressionEqualityComparer);
        this.DictionaryDynamic=new();
        //this.DictionaryLambda=new(ExpressionEqualityComparer);
        this.DictionaryLambda=new(new LambdaEqualityComparer());
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
    private static object Get_ValueTuple(MemberExpression Member,object Tuple) {
        var Field = (FieldInfo)Member.Member;
        if(Member.Expression is MemberExpression Member1) {
            Tuple=Get_ValueTuple(Member1,Tuple);
        }
        var Value = Field.GetValue(Tuple);
        Debug.Assert(Value!=null);
        return Value;
    }
    private static MemberExpression ValueTuple_Item(ref Type TupleType,ref object TupleValue,ref int Item番号,ref Expression TupleExpression,object Value) {
        if(Item番号==8) {
            var Rest = TupleType.GetField("Rest");
            Debug.Assert(Rest!=null);
            TupleType=Rest.FieldType;
            var Value0 = Rest.GetValue(TupleValue);
            Debug.Assert(Value0!=null);
            TupleValue=Value0;
            TupleExpression=Expression.Field(TupleExpression,Rest);
            Item番号=1;
        }
        var TupleField = TupleType.GetField($"Item{Item番号++}");
        Debug.Assert(TupleField!=null);
        TupleField.SetValue(TupleValue,Value);
        return Expression.Field(TupleExpression,TupleField);
    }
    private static MemberExpression ValueTuple_Item(ref Type TupleType,ref object TupleValue,ref int Item番号,ref Expression TupleExpression) {
        if(Item番号==8) {
            var Rest = TupleType.GetField("Rest");
            Debug.Assert(Rest!=null);
            TupleType=Rest.FieldType;
            var Value0 = Rest.GetValue(TupleValue);
            Debug.Assert(Value0!=null);
            TupleValue=Value0;
            TupleExpression=Expression.Field(TupleExpression,Rest);
            Item番号=1;
        }
        var TupleField = TupleType.GetField($"Item{Item番号++}");
        return Expression.Field(TupleExpression,TupleField);
    }
    internal void Disp作成(ParameterExpression ContainerParameter,Information Information,string SQL) {
        var Disp_TypeBuilder = Information.Disp_TypeBuilder;
        var Impl_TypeBuilder = Information.Impl_TypeBuilder;
        Debug.Assert(Disp_TypeBuilder is not null);
        var Expression0 = this.SQLToExpression(ContainerParameter,SQL);
        var Lambda0 = (LambdaExpression)Expression0;
        var DictionaryConstant = this.DictionaryConstant=Information.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic=Information.DictionaryDynamic;
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
                        ImplTypes[b]=元Parameter.IsByRef ? 元Parameter.Type.MakeByRefType() : 元Parameter.Type;
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
                DictionaryLambda[a.Key]=(Delegate, a.Value.Member, Impl_MethodBuilder);
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

        var DispType = Information.CreateDispType();
        var DispParameter = this.DispParameter=Expression.Parameter(DispType,"Disp");
        Debug.Assert(Information.DispParameter is null);
        Information.DispParameter=DispParameter;
        {
            var Field番号 = 0;
            foreach(var a in DictionaryConstant.AsEnumerable()) {
                Debug.Assert($"Constant{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryConstant[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryDynamic.AsEnumerable()) {
                Debug.Assert($"CallSite{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryDynamic[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryLambda.AsEnumerable()) {
                Debug.Assert($"Delegate{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryLambda[a.Key]=(
                    Field,
                    Expression.Field(DispParameter,Field),
                    a.Value.Impl
                );
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable()) {
                Debug.Assert(a.Key.Name==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                Dictionaryラムダ跨ぎParameter[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
        }
    }
    internal void Impl作成(Information Information,ParameterExpression ContainerParameter) {
        Debug.Assert(Information.DispParameter is not null);
        var DispParameter = Information.DispParameter;
        this.DispParameter=DispParameter;
        var DictionaryConstant = this.DictionaryConstant=Information.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic=Information.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda=Information.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter=Information.Dictionaryラムダ跨ぎParameter;
        Debug.Assert(Information.Disp_Type is not null);
        var ContainerField = Information.Disp_Type.GetField("Container");
        Debug.Assert(ContainerField is not null);
        Dictionaryラムダ跨ぎParameter.Add(ContainerParameter,(ContainerField, Expression.Field(DispParameter,ContainerField)));
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
        var Lambda = this.Lambda最適化(Expression.Lambda(Body,Array.Empty<ParameterExpression>()));
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
        this.IsGenerateAssembly ? this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda) : this.DynamicMethod(typeof(object),Lambda);

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

    public Delegate CreateServerDelegate(LambdaExpression Lambda) {
        this._取得_Dictionary.実行(Lambda);
        return this.IsGenerateAssembly
            ? this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda)
            : this.DynamicMethod(typeof(object),Lambda);
    }
    private Delegate PrivateDelegate(LambdaExpression Lambda) {
        var Lambda0 = this.Lambda最適化(Lambda);
        this._取得_Dictionary.実行(Lambda0);
        return this.IsGenerateAssembly
            ? this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda0)
            : this.DynamicMethod(typeof(object),Lambda0);
    }

    public bool IsInline {
        get => this._変換_局所Parameterの先行評価.IsInline;
        set {
            this._変換_跨ぎParameterの先行評価.IsInline=value;
            this._変換_局所Parameterの先行評価.IsInline=value;
        }
    }
    public bool IsGenerateAssembly { get; set; } = true;
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T,TResult> CreateDelegate<T, TResult>(Expression<Func<T,TResult>> Lambda) =>
        (Func<T,TResult>)this.PrivateDelegate(Lambda);
    public Func<TContainer,TResult> CreateContainerDelegate<TContainer, TResult>(Expression<Func<TContainer,TResult>> Lambda) where TContainer : Container =>
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
    private Type Dynamicに対応するFunc(DynamicExpression Dynamic) {
        var Dynamic_Arguments = Dynamic.Arguments;
        var Dynamic_Arguments_Count = Dynamic_Arguments.Count;
        var Types_Length = Dynamic_Arguments_Count+2;
        var Types = new Type[Types_Length];
        Types[0]=typeof(CallSite);
        for(var a = 0;a<Dynamic_Arguments_Count;a++) Types[a+1]=Dynamic_Arguments[a].Type;
        Types[Dynamic_Arguments_Count+1]=Dynamic.Type;
        return Reflection.Func.Get(Dynamic_Arguments_Count+1).MakeGenericType(Types);
    }
    private Type Dynamicに対応するCallSite(DynamicExpression Dynamic) => this._作業配列.MakeGenericType(typeof(CallSite<>),this.Dynamicに対応するFunc(Dynamic));
    private Delegate DynamicAssemblyとDynamicMethod(Type ContainerType,LambdaExpression Lambda1) {
        //Lambda1=this.Lambda最適化(Lambda1);
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var Name = Lambda1.Name??"Disp";
        var AssemblyName = new AssemblyName { Name=Name };
        var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
        var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
        var Impl_TypeBuilder = Disp_TypeBuilder.DefineNestedType("Impl",TypeAttributes.NestedPublic|TypeAttributes.Sealed|TypeAttributes.Abstract);
        var Container_FieldBuilder = Disp_TypeBuilder.DefineField("Container",ContainerType,FieldAttributes.Public);
        var Disp_ctor = Disp_TypeBuilder.DefineConstructor(MethodAttributes.Public,CallingConventions.HasThis,this._作業配列.Types設定(ContainerType));
        Disp_ctor.InitLocals=false;
        Disp_ctor.DefineParameter(1,ParameterAttributes.None,"Container");
        var Disp_ctor_I = Disp_ctor.GetILGenerator();
        Disp_ctor_I.Ldarg_0();
        Disp_ctor_I.Ldarg_1();
        Disp_ctor_I.Stfld(Container_FieldBuilder);
        Debug.Assert(Disp_TypeBuilder is not null);
        var 作業配列 = this._作業配列;
        var Field番号 = 0;
        foreach(var a in DictionaryConstant.AsEnumerable())
            DictionaryConstant[a.Key]=(Disp_TypeBuilder.DefineField($"Constant{Field番号++}",a.Key.Type,FieldAttributes.Public)!, default!);
        foreach(var a in DictionaryDynamic.AsEnumerable())
            DictionaryDynamic[a.Key]=(Disp_TypeBuilder.DefineField($"Dynamic{Field番号++}",this.Dynamicに対応するCallSite(a.Key),FieldAttributes.Public)!, default!);
        var 判定InstanceMethodか = this.判定InstanceMethodか;
        var Types2 = 作業配列.Types2;
        Types2[0]=typeof(object);
        Types2[1]=typeof(IntPtr);
        foreach(var a in DictionaryLambda.AsEnumerable()) {
            var Lambda = a.Key;
            var LambdaParameters = Lambda.Parameters;
            var LambdaParametersCount = LambdaParameters.Count;
            var インスタンスメソッドか = 判定InstanceMethodか.実行(Lambda.Body)|true;
            Type[] DispTypes, ImplTypes;
            if(インスタンスメソッドか) {
                DispTypes=new Type[LambdaParametersCount];
                ImplTypes=new Type[LambdaParametersCount+1];
                ImplTypes[0]=Disp_TypeBuilder;
                for(var b = 0;b<LambdaParametersCount;b++) {
                    var 元Parameter = LambdaParameters[b];
                    var Type = 元Parameter.IsByRef ? 元Parameter.Type.MakeByRefType() : 元Parameter.Type;
                    DispTypes[b+0]=Type;
                    ImplTypes[b+1]=Type;
                }
            } else {
                DispTypes=ImplTypes=new Type[LambdaParametersCount];
                for(var b = 0;b<LambdaParametersCount;b++) {
                    var 元Parameter = LambdaParameters[b];
                    ImplTypes[b]=元Parameter.IsByRef ? 元Parameter.Type.MakeByRefType() : 元Parameter.Type;
                }
            }
            var Disp_MethodBuilder = Disp_TypeBuilder.DefineMethod($"Disp_Method{Field番号}",MethodAttributes.Public,Lambda.ReturnType,DispTypes);
            Disp_MethodBuilder.InitLocals=false;
            var Impl_MethodBuilder = Impl_TypeBuilder.DefineMethod($"Impl_Method{Field番号}",MethodAttributes.Public|MethodAttributes.Static,Lambda.ReturnType,ImplTypes);
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
            ushort Index = 1;
            for(var B2 = 0;B2<LambdaParametersCount;B2++) {
                Disp_MethodBuilder_I.Ldarg(Index++);
                var ParameterName = LambdaParameters[B2].Name;
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
            DictionaryLambda[a.Key]=(Delegate, default!, Impl_MethodBuilder);
            Field番号++;
        }
        Disp_ctor_I.Ret();
        var 跨番号 = 0;
        foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable())
            Dictionaryラムダ跨ぎParameter[a.Key]=(Disp_TypeBuilder.DefineField(a.Key.Name??$"[跨]{跨番号++}",a.Key.Type,FieldAttributes.Public), default!);
        //Disp作成
        var Disp_Type = Disp_TypeBuilder.CreateType();
        var DispParameter = Expression.Parameter(Disp_Type,"Disp");
        {
            var 番号 = 0;
            foreach(var a in DictionaryConstant.AsEnumerable()) {
                Debug.Assert($"Constant{番号}"==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryConstant[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryDynamic.AsEnumerable()) {
                Debug.Assert($"Dynamic{番号}"==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryDynamic[a.Key]=(Field, Expression.Field(DispParameter,Field));
                Debug.Assert(this.Dynamicに対応するCallSite(a.Key)==Field.FieldType);
                Debug.Assert(this.Dynamicに対応するCallSite(a.Key)==Expression.Field(DispParameter,Field).Type);
            }
            foreach(var a in DictionaryLambda.AsEnumerable()) {
                Debug.Assert($"Delegate{番号}"==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryLambda[a.Key]=(
                    Field,
                    Expression.Field(DispParameter,Field),
                    a.Value.Impl
                );
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable()) {
                Debug.Assert(a.Key.Name is null||a.Key.Name==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                Dictionaryラムダ跨ぎParameter[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
        }
        var s = インラインラムダテキスト(Lambda1);
        //var Tuple=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理(ContainerType,DictionaryConstant,DictionaryLambda,Dictionaryラムダ跨ぎParameter,out var TupleParameter1);
        this._作成_DynamicAssembly.Impl作成(Lambda1,DispParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter);
        Debug.Assert(Disp_Type.GetField("Container",Instance_NonPublic_Public) is not null);
        var (Tuple, TupleParameter)=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(ContainerType);
        {
            var _ = Impl_TypeBuilder.CreateType();
            //todo AssemblyGenerater.GenerateAssembly()の後GC.Collect()とGC.WaitForPendingFinalizers()することでファイルハンドルをファイナライザで解放させることを期待したがだダメだった
            //var t=Stopwatch.StartNew();
            //Console.Write("GenerateAssembly,");
            //new AssemblyGenerator()をフィールドに保存すると２度目以降前回のアセンブリ情報が残る
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Environment.CurrentDirectory}\{Name}.dll");
            //this.AssemblyGenerator.GenerateAssembly(DynamicAssembly,@$"{Folder}\{Name}.dll");
            //Console.WriteLine($"GenerateAssembly {t.ElapsedMilliseconds}ms");
        }
        this._作成_DynamicMethod.Impl作成(Lambda1,TupleParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter,Tuple);
        var Value = Get_ValueTuple(DictionaryLambda[Lambda1].Member,Tuple);
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
    private Delegate DynamicMethod(Type ContainerType,LambdaExpression Lambda1) {
        //var Lambda1=this.Lambda最適化(Lambda);
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var (Tuple, TupleParameter)=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(ContainerType);

        this._作成_DynamicMethod.Impl作成(Lambda1,TupleParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter,Tuple);
        var Value = Get_ValueTuple(DictionaryLambda[Lambda1].Member,Tuple);
        var Delegate1 = (Delegate)Value;
        return Delegate1;
    }

    private (object Tuple, ParameterExpression TupleParameter) DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(Type ContainerType) {
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var TargetFieldType数 = 1+DictionaryConstant.Count+DictionaryDynamic.Count+DictionaryLambda.Count+Dictionaryラムダ跨ぎParameter.Count;
        var FieldTypes = new Type[TargetFieldType数];
        {
            FieldTypes[0]=ContainerType;
            var index = 1;
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
        var 作業配列 = this._作業配列;
        var Switch = TargetFieldType数%7;
        var Offset = TargetFieldType数-Switch;
        Type DispType;
        if(TargetFieldType数<8) {
            DispType=Switch switch {
                1 => 作業配列.MakeGenericType(typeof(ClassTuple<>),FieldTypes[0]),
                2 => 作業配列.MakeGenericType(typeof(ClassTuple<,>),FieldTypes[0],FieldTypes[1]),
                3 => 作業配列.MakeGenericType(typeof(ClassTuple<,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2]),
                4 => 作業配列.MakeGenericType(typeof(ClassTuple<,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3]),
                5 => 作業配列.MakeGenericType(typeof(ClassTuple<,,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4]),
                6 => 作業配列.MakeGenericType(typeof(ClassTuple<,,,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4],FieldTypes[5]),
                _ => 作業配列.MakeGenericType(typeof(ClassTuple<,,,,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4],FieldTypes[5],FieldTypes[6])
            };
        } else {
            //Switch 16%7=2
            //Offset 16-2=14
            //1,2,3,4,5,6,7,(8,9,10)
            //1,2,3,4,5,6,7,(8,9,10,11,12,13,14,15,(16))
            //0,1,2,3,4,5,6,(7,8,9,10,11,12,13,(14,15))16個
            //0,1,2,3,4,5,6,(7,8,9,10,11,12,13,(14,15,16,17,18,19,20))21個
            DispType=Switch switch {
                1 => 作業配列.MakeGenericType(typeof(ValueTuple<>),FieldTypes[Offset+0]),
                2 => 作業配列.MakeGenericType(typeof(ValueTuple<,>),FieldTypes[Offset+0],FieldTypes[Offset+1]),
                3 => 作業配列.MakeGenericType(typeof(ValueTuple<,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2]),
                4 => 作業配列.MakeGenericType(typeof(ValueTuple<,,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3]),
                5 => 作業配列.MakeGenericType(typeof(ValueTuple<,,,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4]),
                6 => 作業配列.MakeGenericType(typeof(ValueTuple<,,,,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4],FieldTypes[Offset+5]),
                _ => 作業配列.MakeGenericType(typeof(ValueTuple<,,,,,,>),FieldTypes[Offset-=7],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4],FieldTypes[Offset+5],FieldTypes[Offset+6])
            };
            var Types8 = 作業配列.Types8;
            while((Offset-=7)>=0) {
                Debug.Assert(Offset%7==0);
                Types8[0]=FieldTypes[Offset+0];
                Types8[1]=FieldTypes[Offset+1];
                Types8[2]=FieldTypes[Offset+2];
                Types8[3]=FieldTypes[Offset+3];
                Types8[4]=FieldTypes[Offset+4];
                Types8[5]=FieldTypes[Offset+5];
                Types8[6]=FieldTypes[Offset+6];
                Types8[7]=DispType;
                DispType=(Offset==0 ? typeof(ClassTuple<,,,,,,,>) : typeof(ValueTuple<,,,,,,,>)).MakeGenericType(Types8);
            }
        }
        Debug.Assert(DispType.IsClass);
        var Disp = Activator.CreateInstance(DispType)!;
        var DispParameter = Expression.Parameter(DispType,"Tuple");
        this.DispParameter=DispParameter;
        {
            Expression TupleExpression = DispParameter;
            var DispType0 = DispType;
            var Disp0 = Disp;
            var Item番号 = 2;//1はContainerが入る
            foreach(var a in DictionaryConstant.AsEnumerable())
                DictionaryConstant[a.Key]=(default!, ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression,a.Key.Value));
            foreach(var a in DictionaryDynamic.AsEnumerable()) {
                var Dynamic = a.Key;
                var CallSite0 = CallSite.Create(this.Dynamicに対応するFunc(Dynamic),Dynamic.Binder);
                DictionaryDynamic[Dynamic]=(default!, ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression,CallSite0));
            }
            foreach(var a in DictionaryLambda.AsEnumerable())
                DictionaryLambda[a.Key]=(default!, ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression), default!);
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable())
                Dictionaryラムダ跨ぎParameter[a.Key]=(default!, ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression));
        }
        return (Disp, DispParameter);
    }
    internal static class DynamicReflection {
        public static readonly CSharpArgumentInfo CSharpArgumentInfo = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null);
        public static CSharpArgumentInfo[] CSharpArgumentInfoArray(int Count) {
            var Array = new CSharpArgumentInfo[Count];
            for(var a = 0;a<Count;a++)
                Array[a]=CSharpArgumentInfo;
            return Array;
        }
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray1 = { CSharpArgumentInfo };
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray2 = { CSharpArgumentInfo,CSharpArgumentInfo };
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray3 = { CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo };
        public static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray4 = { CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo };
        public static class CallSites {
            public static readonly FieldInfo ObjectObjectObjectObjectTarget = typeof(CallSite<Func<CallSite,object,object,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object,object,object>>.Target))!;
            public static readonly FieldInfo ObjectObjectObjectTarget = typeof(CallSite<Func<CallSite,object,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object,object>>.Target))!;
            public static readonly FieldInfo ObjectObjectTarget = typeof(CallSite<Func<CallSite,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object>>.Target))!;
            public static readonly FieldInfo ObjectBooleanTarget = typeof(CallSite<Func<CallSite,object,bool>>).GetField(nameof(CallSite<Func<CallSite,object,bool>>.Target))!;

            private static CallSite<Func<CallSite,object,object,object>> CallSite_Binary(ExpressionType NodeType) => CallSite<Func<CallSite,object,object,object>>.Create(RuntimeBinder.Binder.BinaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray2));
            public static readonly CallSite<Func<CallSite,object,object,object>> Add = CallSite_Binary(ExpressionType.Add);
            public static readonly CallSite<Func<CallSite,object,object,object>> AddAssign = CallSite_Binary(ExpressionType.AddAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> And = CallSite_Binary(ExpressionType.And);
            public static readonly CallSite<Func<CallSite,object,object,object>> AndAssign = CallSite_Binary(ExpressionType.AndAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Divide = CallSite_Binary(ExpressionType.Divide);
            public static readonly CallSite<Func<CallSite,object,object,object>> DivideAssign = CallSite_Binary(ExpressionType.DivideAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Equal = CallSite_Binary(ExpressionType.Equal);
            public static readonly CallSite<Func<CallSite,object,object,object>> ExclusiveOr = CallSite_Binary(ExpressionType.ExclusiveOr);
            public static readonly CallSite<Func<CallSite,object,object,object>> ExclusiveOrAssign = CallSite_Binary(ExpressionType.ExclusiveOrAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> GreaterThan = CallSite_Binary(ExpressionType.GreaterThan);
            public static readonly CallSite<Func<CallSite,object,object,object>> GreaterThanOrEqual = CallSite_Binary(ExpressionType.GreaterThanOrEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> LeftShift = CallSite_Binary(ExpressionType.LeftShift);
            public static readonly CallSite<Func<CallSite,object,object,object>> LeftShiftAssign = CallSite_Binary(ExpressionType.LeftShiftAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> LessThan = CallSite_Binary(ExpressionType.LessThan);
            public static readonly CallSite<Func<CallSite,object,object,object>> LessThanOrEqual = CallSite_Binary(ExpressionType.LessThanOrEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> Modulo = CallSite_Binary(ExpressionType.Modulo);
            public static readonly CallSite<Func<CallSite,object,object,object>> ModuloAssign = CallSite_Binary(ExpressionType.ModuloAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Multiply = CallSite_Binary(ExpressionType.Multiply);
            public static readonly CallSite<Func<CallSite,object,object,object>> MultiplyAssign = CallSite_Binary(ExpressionType.MultiplyAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> NotEqual = CallSite_Binary(ExpressionType.NotEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> Or = CallSite_Binary(ExpressionType.Or);
            public static readonly CallSite<Func<CallSite,object,object,object>> OrAssign = CallSite_Binary(ExpressionType.OrAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> RightShift = CallSite_Binary(ExpressionType.RightShift);
            public static readonly CallSite<Func<CallSite,object,object,object>> RightShiftAssign = CallSite_Binary(ExpressionType.RightShiftAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Subtract = CallSite_Binary(ExpressionType.Subtract);
            public static readonly CallSite<Func<CallSite,object,object,object>> SubtractAssign = CallSite_Binary(ExpressionType.SubtractAssign);
            private static CallSite<T> CallSite_Unary<T>(ExpressionType NodeType) where T : class => CallSite<T>.Create(RuntimeBinder.Binder.UnaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray1));
            private static CallSite<Func<CallSite,object,object>> CallSite_Unary(ExpressionType NodeType) => CallSite_Unary<Func<CallSite,object,object>>(NodeType);
            public static readonly CallSite<Func<CallSite,object,object>> Decrement = CallSite_Unary(ExpressionType.Decrement);
            public static readonly CallSite<Func<CallSite,object,object>> Increment = CallSite_Unary(ExpressionType.Increment);
            public static readonly CallSite<Func<CallSite,object,object>> Negate = CallSite_Unary(ExpressionType.Negate);
            public static readonly CallSite<Func<CallSite,object,object>> Not = CallSite_Unary(ExpressionType.Not);
            public static readonly CallSite<Func<CallSite,object,object>> OnesComplement = CallSite_Unary(ExpressionType.OnesComplement);
            public static readonly CallSite<Func<CallSite,object,object>> UnaryPlus = CallSite_Unary(ExpressionType.UnaryPlus);
            private static CallSite<Func<CallSite,object,bool>> CallSite_IsFalse_IsTrue(ExpressionType NodeType) => CallSite_Unary<Func<CallSite,object,bool>>(NodeType);
            public static readonly CallSite<Func<CallSite,object,bool>> IsFalse = CallSite_IsFalse_IsTrue(ExpressionType.IsFalse);
            public static readonly CallSite<Func<CallSite,object,bool>> IsTrue = CallSite_IsFalse_IsTrue(ExpressionType.IsTrue);
            public static readonly CallSite<Func<CallSite,object,object,object>> GetIndex = CallSite<Func<CallSite,object,object,object>>.Create(
                RuntimeBinder.Binder.GetIndex(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(DynamicReflection),
                    CSharpArgumentInfoArray2
                )
            );
            public static readonly CallSite<Func<CallSite,object,object,object,object>> SetIndex = CallSite<Func<CallSite,object,object,object,object>>.Create(
                RuntimeBinder.Binder.SetIndex(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(DynamicReflection),
                    CSharpArgumentInfoArray3
                )
            );
        }

        private static (FieldInfo Target, MethodInfo Invoke) F(Type CallSite) {
            var Target = CallSite.GetField("Target");
            Debug.Assert(Target is not null);
            var Invoke = Target.FieldType.GetMethod("Invoke");
            Debug.Assert(Invoke is not null);
            return (Target, Invoke);
        }
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectObjectObjectObject = F(typeof(CallSite<Func<CallSite,object,object,object,object>>));
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectObjectObject = F(typeof(CallSite<Func<CallSite,object,object,object>>));
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectObject = F(typeof(CallSite<Func<CallSite,object,object>>));
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectBoolea = F(typeof(CallSite<Func<CallSite,object,bool>>));
        private static FieldInfo F(string フィールド名) => typeof(CallSites).GetField(フィールド名,Static_NonPublic_Public)!;
        public static readonly FieldInfo Add = F(nameof(Add));
        public static readonly FieldInfo AddAssign = F(nameof(AddAssign));
        public static readonly FieldInfo And = F(nameof(And));
        public static readonly FieldInfo AndAssign = F(nameof(AndAssign));
        public static readonly FieldInfo Divide = F(nameof(Divide));
        public static readonly FieldInfo DivideAssign = F(nameof(DivideAssign));
        public static readonly FieldInfo Equal = F(nameof(Equal));
        public static readonly FieldInfo ExclusiveOr = F(nameof(ExclusiveOr));
        public static readonly FieldInfo ExclusiveOrAssign = F(nameof(ExclusiveOrAssign));
        public static readonly FieldInfo GreaterThan = F(nameof(GreaterThan));
        public static readonly FieldInfo GreaterThanOrEqual = F(nameof(GreaterThanOrEqual));
        public static readonly FieldInfo LeftShift = F(nameof(LeftShift));
        public static readonly FieldInfo LeftShiftAssign = F(nameof(LeftShiftAssign));
        public static readonly FieldInfo LessThan = F(nameof(LessThan));
        public static readonly FieldInfo LessThanOrEqual = F(nameof(LessThanOrEqual));
        public static readonly FieldInfo Modulo = F(nameof(Modulo));
        public static readonly FieldInfo ModuloAssign = F(nameof(ModuloAssign));
        public static readonly FieldInfo Multiply = F(nameof(Multiply));
        public static readonly FieldInfo MultiplyAssign = F(nameof(MultiplyAssign));
        public static readonly FieldInfo NotEqual = F(nameof(NotEqual));
        public static readonly FieldInfo Or = F(nameof(Or));
        public static readonly FieldInfo OrAssign = F(nameof(OrAssign));
        public static readonly FieldInfo RightShift = F(nameof(RightShift));
        public static readonly FieldInfo RightShiftAssign = F(nameof(RightShiftAssign));
        public static readonly FieldInfo Subtract = F(nameof(Subtract));
        public static readonly FieldInfo SubtractAssign = F(nameof(SubtractAssign));

        public static readonly FieldInfo Decrement = F(nameof(Decrement));
        public static readonly FieldInfo Increment = F(nameof(Increment));
        public static readonly FieldInfo Negate = F(nameof(Negate));
        public static readonly FieldInfo Not = F(nameof(Not));
        public static readonly FieldInfo OnesComplement = F(nameof(OnesComplement));
        public static readonly FieldInfo UnaryPlus = F(nameof(UnaryPlus));
        public static readonly FieldInfo IsFalse = F(nameof(IsFalse));
        public static readonly FieldInfo IsTrue = F(nameof(IsTrue));

        public static readonly FieldInfo GetIndex = F(nameof(GetIndex));
        public static readonly FieldInfo SetIndex = F(nameof(SetIndex));
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
    public TResult Execute<TResult>(Expression<Func<TResult>> Lambda) {
        var Delegate = this.CreateDelegate(Lambda);
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
    private static readonly Regex Regex単純な識別子 = new("[^ ,^$,^#,^<,^[,^>,^(]*[.]",RegexOptions.Compiled);
    /// <summary>
    /// 式木をわかりやすくテキストにする
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string インラインラムダテキスト(Expression e) {
        //[^ ]*[.]
        dynamic NonPublicAccessor = new NonPublicAccessor(typeof(Expression),e);
        var 変換前 = NonPublicAccessor.DebugView;
        //変換前=変換前.Replace("LinqDB.Sets.","");
        //変換前=変換前.Replace("System.Collections.Generic.","");
        //変換前=変換前.Replace("System.Collections.","");
        //変換前=変換前.Replace("System.","");
        変換前=変換前.Replace("\r\n{","{");
        var KeyValues = new Generic.List<(string Key, Generic.List<string> Values)>();
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
                    if(Line[^2]=='>'&&Line[^1]==')') Line=Line.Substring(0,Line.Length-1);
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
                var Result6 = Regex単純な識別子.Replace(Result5,"");
                return Result6;
            }
        }
    }
    public string 命令ツリー(Expression Expression) => this._取得_命令ツリー.実行(Expression);
    private static object Set_ValueTuple(object ValueTuple,int Index,object Value) {
        switch(Index) {
            case 0: ValueTuple.GetType().GetField("Item1")!.SetValue(ValueTuple,Value); break;
            case 1: ValueTuple.GetType().GetField("Item2")!.SetValue(ValueTuple,Value); break;
            case 2: ValueTuple.GetType().GetField("Item3")!.SetValue(ValueTuple,Value); break;
            case 3: ValueTuple.GetType().GetField("Item4")!.SetValue(ValueTuple,Value); break;
            case 4: ValueTuple.GetType().GetField("Item5")!.SetValue(ValueTuple,Value); break;
            case 5: ValueTuple.GetType().GetField("Item6")!.SetValue(ValueTuple,Value); break;
            case 6: ValueTuple.GetType().GetField("Item7")!.SetValue(ValueTuple,Value); break;
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
    private Type _Context = typeof(object);
    /// <summary>
    /// どのクラス内で実行するか指定
    /// </summary>
    public Type Context {
        get => this._Context;
        set => this._Context=value;
    }
    internal LambdaExpression Lambda非最適化(Expression Lambda00) {
        this.DictionaryConstant.Clear();
        var Lambda02 = this._変換_メソッド正規化_取得インライン不可能定数.実行(Lambda00);
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
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
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
        var Lambda02 = this._変換_メソッド正規化_取得インライン不可能定数.実行(Lambda01);
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
        var Lambda08 = this.IsInline ? this._変換_インラインループ独立.実行(Lambda07) : Lambda07;
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
