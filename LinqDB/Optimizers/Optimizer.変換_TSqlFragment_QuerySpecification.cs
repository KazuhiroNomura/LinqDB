//#pragma warning disable CA1822 // Mark members as static
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using LinqDB.Sets;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace LinqDB.Optimizers;
/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer {
    /// <summary>
    /// TSQLからLINQに変換する。
    /// </summary>
    private partial class 変換_TSqlFragmentからExpression {
        private sealed class ExpressionEqualityComparerGroupBy:ExpressionEqualityComparer {
            protected override void Clear(){
                this.x_Parameters.Clear();
                this.y_Parameters.Clear();
                this.x_LabelTargets.Clear();
                this.y_LabelTargets.Clear();
            }
            public override bool Equals(ParameterExpression? x,ParameterExpression? y){
                var x_Index0 = this.x_Parameters.IndexOf(x);
                var y_Index0 = this.y_Parameters.IndexOf(y);
                if(x_Index0!=y_Index0) return @false;
                if(x_Index0>=0) return true;
                //Let(x=>.v),Let(y=>.w)は一致しない。
                //Let(x=>.v=x),Let(y=>.w=y)は一致しない。
                //Let(x=>.v),Let(y=>.v)は一致する。
                //Let(x=>.v=x),Let(y=>.v=y)は一致する。
                var x_ラムダ跨ぎParameters = this.x_ラムダ跨ぎParameters;
                var y_ラムダ跨ぎParameters = this.y_ラムダ跨ぎParameters;
                var x_Index1 = x_ラムダ跨ぎParameters.IndexOf(x);
                var y_Index1 = y_ラムダ跨ぎParameters.IndexOf(y);
                if(x_Index1!=y_Index1) return @false;
                if(x_Index1>=0){
                    return x==y;
                }
                return @false;
            }
        }
        private sealed class 変換_旧Expressionを新Expression1GroupBy:変換_旧Expressionを新Expression1{
            private readonly ExpressionEqualityComparerGroupBy ExpressionEqualityComparerGroupBy;
            public 変換_旧Expressionを新Expression1GroupBy(作業配列 作業配列):base(作業配列,new ExpressionEqualityComparerGroupBy()){
                this.ExpressionEqualityComparerGroupBy=(ExpressionEqualityComparerGroupBy)this.ExpressionEqualityComparer;
            }
            public Expression 実行(Expression e,Expression 旧Expression,Expression 新Expression,ParameterExpression ラムダ跨ぎParameter){
                var ExpressionEqualityComparerGroupBy=this.ExpressionEqualityComparerGroupBy;
                ExpressionEqualityComparerGroupBy.x_ラムダ跨ぎParameters.Add(ラムダ跨ぎParameter);
                ExpressionEqualityComparerGroupBy.y_ラムダ跨ぎParameters.Add(ラムダ跨ぎParameter);
                return base.実行(e,旧Expression,新Expression);
            }
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
                    作業配列.MakeGenericType(typeof(IEnumerable<>),Element_Type),
                    "Group"
                );
                RefPeek.集約関数のParameter=ss;
                RefPeek.集約関数のSource=Group;
                Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
                Debug.Assert(RefPeek_List_ColumnExpression.Count==0);                
                foreach(var SelectElement in x_SelectElements)
                    this.SelectElement(SelectElement);
                Expression resultSelector_Body= ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression);
                Expression ValueTuple = Key;
                //Expression keySelector_Body1= keySelector_Body;
                var Item番号 = 1;
                for(var a = 0;a<RefPeek_List_GroupByExpression_Count;a++)
                    resultSelector_Body=変換_旧Expressionを新Expression1.実行(resultSelector_Body,RefPeek_List_GroupByExpression[a],ValueTuple_Item(ref ValueTuple,ref Item番号),ss);
                var x0=作業配列.MakeGenericMethod(Reflection.ExtensionSet.GroupBy_keySelector_resultSelector,Element_Type,keySelector_Body.Type,resultSelector_Body.Type);
                var y=Expression.Lambda(
                    keySelector_Body,
                    作業配列.Parameters設定(ss)
                );
                var z=Expression.Lambda(
                    resultSelector_Body,
                    作業配列.Parameters設定(Key,Group)
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
                var selector_Body = ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression);
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
}