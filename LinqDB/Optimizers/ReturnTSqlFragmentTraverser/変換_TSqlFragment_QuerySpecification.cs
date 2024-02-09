//#pragma warning disable CA1822 // Mark members as static
using System.Diagnostics;
using System;
using Generic = System.Collections.Generic;
using System.Linq.Expressions;
using LinqDB.Helpers;
using LinqDB.Optimizers.Comparer;

namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression {
    private sealed class ExpressionEqualityComparerGroupBy:AExpressionEqualityComparer{
        //internal ParameterExpression ラムダ跨ぎParameter;
        protected override bool ProtectedAssign後処理(ParameterExpression x,ParameterExpression y)=>@false;
        protected override bool Equals後処理(ParameterExpression x,ParameterExpression y)=>x==y;
        protected override bool T(LambdaExpression x,LambdaExpression y){
            if(x.Type!=y.Type||x.TailCall!=y.TailCall) return @false;
            var Lambdx_x_Parameters = x.Parameters;
            var Lambdx_y_Parameters = y.Parameters;
            var Lambdx_x_Parameters_Count = Lambdx_x_Parameters.Count;
            if(Lambdx_x_Parameters_Count!=Lambdx_y_Parameters.Count) return @false;
            var x_Parameters = this.x_Parameters;
            var y_Parameters = this.y_Parameters;
            var x_Parameters_Count = x_Parameters.Count;
            Debug.Assert(x_Parameters_Count==y_Parameters.Count);
            x_Parameters.AddRange(Lambdx_x_Parameters);
            y_Parameters.AddRange(Lambdx_y_Parameters);
            Debug.Assert(this.x_LabelTargets.Count==this.y_LabelTargets.Count);
            var r = this.ProtectedPrivateEquals(x.Body,y.Body);
            Debug.Assert(this.x_LabelTargets.Count==this.y_LabelTargets.Count);
            x_Parameters.RemoveRange(x_Parameters_Count,Lambdx_x_Parameters_Count);
            y_Parameters.RemoveRange(x_Parameters_Count,Lambdx_x_Parameters_Count);
            return r;
        }
    }
    //private sealed class 変換_旧Expressionを新Expression1GroupBy:変換_旧Expressionを新Expression1 {
    //    public 変換_旧Expressionを新Expression1GroupBy(作業配列 作業配列) : base(作業配列,new ExpressionEqualityComparerGroupBy()) {
    //    }
    //    public Expression 実行(Expression e,Expression 旧Expression,Expression 新Expression,ParameterExpression ラムダ跨ぎParameter){
    //        //this.ラムダ跨ぎParameter=ラムダ跨ぎParameter;
    //        return base.実行(e,旧Expression,新Expression);
    //    }
    //}
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
}
