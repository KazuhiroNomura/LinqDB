//#pragma warning disable CA1822 // Mark members as static
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
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
            var r = this.ProtectedEquals(x.Body,y.Body);
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
    private Expression QuerySpecification(QuerySpecification x) {
        var 作業配列 = this.作業配列;
        //var StackSubquery単位の情報 = this._StackSubquery単位の情報;
        var x_SelectElements = x.SelectElements;
        ref var RefPeek = ref this.RefPeek;
        //Debug.Assert(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Count==0);
        //var RefPeek0_List_ColumnExpression = RefPeek0.List_ColumnExpression;
        Type Element_Type;
        Expression Source;
        ParameterExpression ss;
        if(x.FromClause is not null) {
            (Source, ss)=this.FromClause(x.FromClause);
            Element_Type=ss.Type;
        } else {
            Source=Expression.Call(null,Reflection.Container.TABLE_DUM);
            Element_Type=IEnumerable1のT(Source.Type);
            ss=Expression.Parameter(Element_Type,"ss");
        }
        if(x.WhereClause is not null) {
            var predicate_Body = this.WhereClause(x.WhereClause);
            Source=Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type),
                Source,
                Expression.Lambda(predicate_Body,作業配列.Parameters設定(ss))
            );
        }
        //Debug.Assert(StackSubquery単位の情報==this._StackSubquery単位の情報);
        var RefPeek_List_ColumnAlias = RefPeek.List_ColumnAlias;
        var RefPeek_List_ColumnExpression = RefPeek.List_ColumnExpression;
        RefPeek_List_ColumnAlias.Clear();
        RefPeek_List_ColumnExpression.Clear();
        //Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
        //Debug.Assert(RefPeek_List_ColumnExpression.Count==0);
        if(x.GroupByClause is not null) {
            //GroupBy(ss=>[keySelector_Body])
            //RefPeek0_List_ColumnAlias.Clear();
            //RefPeek0_List_ColumnAlias.Clear();
            var RefPeek_List_GroupByExpression = RefPeek.List_GroupByExpression;
            RefPeek_List_GroupByExpression.Clear();
            var keySelector_Body = this.GroupByClause(x.GroupByClause);
            var RefPeek_List_GroupByExpression_Count = RefPeek_List_GroupByExpression.Count;
            var Key = Expression.Parameter(keySelector_Body.Type,"Key");
            //var Key_Argument = keySelector_Body.Arguments;
            var 変換_旧Expressionを新Expression1=this.変換_旧Expressionを新Expression1;

            //{
            //    Expression ValueTuple0=Key;
            //    //Expression keySelector_Body1= keySelector_Body;
            //    var Item番号0=1;
            //    for(var a=0;a<RefPeek_List_GroupByExpression_Count;a++)
            //        RefPeek_List_GroupByExpression[a]=変換_旧Expressionを新Expression1.実行(
            //            keySelector_Body,
            //            RefPeek_List_GroupByExpression[a],
            //            ValueTuple_Item(ref ValueTuple0,ref Item番号0));
            //    //Source.GroupBy(ss =>,(Key,Group) => Group.Sum(ge =>))
            //    var Element_Type0=Source.Type.GetGenericArguments()[0];
            //}
            var Group = Expression.Parameter(
                作業配列.MakeGenericType(typeof(Sets.IEnumerable<>),Element_Type),
                "Group"
            );
            RefPeek.集約関数のParameter=ss;
            RefPeek.集約関数のSource=Group;
            Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
            Debug.Assert(RefPeek_List_ColumnExpression.Count==0);                
            foreach(var SelectElement in x_SelectElements)
                this.SelectElement(SelectElement);
            Expression resultSelector_Body= CommonLibrary.ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression);
            Expression ValueTuple = Key;
            //Expression keySelector_Body1= keySelector_Body;
            var Item番号 = 1;
            for(var a = 0;a<RefPeek_List_GroupByExpression_Count;a++)
                resultSelector_Body=変換_旧Expressionを新Expression1.実行(
                    resultSelector_Body,
                    RefPeek_List_GroupByExpression[a],
                    ValueTuple_Item(ref ValueTuple,ref Item番号)
                );
            var GroupBy = Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.GroupBy_keySelector_resultSelector,Element_Type,keySelector_Body.Type,resultSelector_Body.Type),
                Source,
                Expression.Lambda(
                    keySelector_Body,
                    作業配列.Parameters設定(ss)
                ),
                Expression.Lambda(
                    resultSelector_Body,
                    作業配列.Parameters設定(Key,Group)
                )
            );
            return GroupBy;
        } else {
            var 集約関数が有るか=false;
            var 判定_集約関数があるか = this._判定_集約関数があるか;
            var x_SelectElements_Count=x_SelectElements.Count;
            var a = 0;
            while(true){
                if(a<x_SelectElements_Count){
                    if(判定_集約関数があるか.実行(x_SelectElements[a])){
                        集約関数が有るか=true;
                        break;
                    }
                }else{
                    if(判定_集約関数があるか.実行(x.HavingClause)) {
                        集約関数が有るか=true;
                    }
                    break;
                }
                a++;
            }

//                foreach(var SelectElement in x_SelectElements) {
//                    if(判定_集約関数があるか.実行(SelectElement)) {
//                        集約関数が有るか=true;
//                        goto 集約関数が有る;
//                    }
//                }
//                if(!集約関数が有るか&&判定_集約関数があるか.実行(x.HavingClause)) {
//                    集約関数が有るか=true;
//                }
//集約関数が有る:
            RefPeek.集約関数のParameter=ss;
            if(集約関数が有るか&&RefPeek.集約関数のSource is null) {
                //GROUP BYがない集約関数を含む1行を返すSELECT
                //MethodCallを分解するして最小の式に変換する
                //select f0=sum(f.a),f1=avg(f.b)from F f group by 1
                //TABLE_DEE.Select(_=>new{f0=F.Sum(f=>f.c),f1=F.Sum(f.b))})
                Debug.Assert(Source is not null);
                RefPeek.集約関数のSource=Source;
                Element_Type=typeof(Databases.AttributeEmpty);
                ss=Expression.Parameter(
                    Element_Type,
                    "_"
                );
                //TABLE_DEE.Select(ss=>selector_Body)
                //(s=new Set<>();s.Add(selector_Body);s)
                Source=TABLE_DEE;
            }
            if(x.HavingClause is not null) {
                var predicate_Body = this.HavingClause(x.HavingClause);
                Source=Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type),
                    Source,
                    Expression.Lambda(
                        predicate_Body,
                        作業配列.Parameters設定(ss)
                    )
                );
            }
            //var List_ColumnExpression = RefPeek0.List_ColumnExpression;
            //List_ColumnExpression.Clear();
            //RefPeek0_List_ColumnAlias.Clear();
            Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
            Debug.Assert(RefPeek_List_ColumnExpression.Count==0);
            foreach(var SelectElement in x_SelectElements)
                this.SelectElement(SelectElement);
            //if(RefPeek_List_ColumnAlias.Count==0){
            //    //select @root_id = log_id from graph--代入としてのselect
            //    return Expression.Block(RefPeek_List_ColumnExpression);
            //} else {
            //    var selector_Body = ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression,0);
            //    var Result=Expression.Call(
            //        作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,Element_Type,selector_Body.Type),
            //        Source,
            //        Expression.Lambda(
            //            selector_Body,
            //            作業配列.Parameters設定(ss)
            //        )
            //    );
            //    return Result;
            //}
            var selector_Body =CommonLibrary.ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression);
            var Result=Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,Element_Type,selector_Body.Type),
                Source,
                Expression.Lambda(
                    selector_Body,
                    作業配列.Parameters設定(ss)
                )
            );
            return Result;
        }
    }
}
