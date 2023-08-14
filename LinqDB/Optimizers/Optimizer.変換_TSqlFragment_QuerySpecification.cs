//#pragma warning disable CA1822 // Mark members as static
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using LinqDB.Sets;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Optimizers;
/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer {
    /// <summary>
    /// TSQLからLINQに変換する。
    /// </summary>
    private partial class 変換_TSqlFragmentからExpression {
        private Expressions.Expression QuerySpecification(QuerySpecification x) {
            var 作業配列 = this.作業配列;
            //var StackSubquery単位の情報 = this._StackSubquery単位の情報;
            var x_SelectElements = x.SelectElements;
            ref var RefPeek = ref this.RefPeek;
            //Debug.Assert(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Count==0);
            //var RefPeek0_List_ColumnExpression = RefPeek0.List_ColumnExpression;
            Type Element_Type;
            Expressions.Expression Source;
            Expressions.ParameterExpression ss;
            if(x.FromClause is not null) {
                (Source, ss)=this.FromClause(x.FromClause);
                Element_Type=ss.Type;
            } else {
                Source=Expressions.Expression.Call(null,Reflection.Container.TABLE_DUM);
                Element_Type=IEnumerable1のT(Source.Type);
                ss=Expressions.Expression.Parameter(Element_Type,"ss");
            }
            if(x.WhereClause is not null) {
                var predicate_Body = this.WhereClause(x.WhereClause);
                Source=Expressions.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type),
                    Source,
                    Expressions.Expression.Lambda(predicate_Body,作業配列.Parameters設定(ss))
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
                var Key = Expressions.Expression.Parameter(keySelector_Body.Type,"Key");
                //var Key_Argument = keySelector_Body.Arguments;
                var 変換_旧Expressionを新Expression1=this.変換_旧Expressionを新Expression1;
                //Expressions.Expression ValueTuple = Key;
                ////Expressions.Expression keySelector_Body1= keySelector_Body;
                //var Item番号 = 1;
                //for(var a = 0;a<RefPeek_List_GroupByExpression_Count;a++)
                //    RefPeek_List_GroupByExpression[a]=変換_旧Expressionを新Expression1.実行(
                //        keySelector_Body,
                //        RefPeek_List_GroupByExpression[a],
                //        ValueTuple_Item(ref ValueTuple,ref Item番号));
                //Source.GroupBy(ss=>,(Key,Group)=>Group.Sum(ge=>))
                //Element_Type=Source.Type.GetGenericArguments()[0];
                var Group = Expressions.Expression.Parameter(
                    作業配列.MakeGenericType(typeof(ImmutableSet<>),Element_Type),
                    "Group"
                );
                RefPeek.集約関数のParameter=ss;
                RefPeek.集約関数のSource=Group;
                Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
                Debug.Assert(RefPeek_List_ColumnExpression.Count==0);                
                foreach(var SelectElement in x_SelectElements)
                    this.SelectElement(SelectElement);
                Expressions.Expression resultSelector_Body= ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression,0);
                Expressions.Expression ValueTuple = Key;
                //Expressions.Expression keySelector_Body1= keySelector_Body;
                var Item番号 = 1;
                for(var a = 0;a<RefPeek_List_GroupByExpression_Count;a++)
                    resultSelector_Body=変換_旧Expressionを新Expression1.実行(resultSelector_Body,RefPeek_List_GroupByExpression[a],ValueTuple_Item(ref ValueTuple,ref Item番号));
                var GroupBy = Expressions.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.GroupBy_keySelector_resultSelector,Element_Type,keySelector_Body.Type,resultSelector_Body.Type),
                    Source,
                    Expressions.Expression.Lambda(
                        keySelector_Body,
                        作業配列.Parameters設定(ss)
                    ),
                    Expressions.Expression.Lambda(
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
                    ss=Expressions.Expression.Parameter(
                        Element_Type,
                        "_"
                    );
                    //TABLE_DEE.Select(ss=>selector_Body)
                    //(s=new Set<>();s.Add(selector_Body);s)
                    Source=TABLE_DEE;
                }
                if(x.HavingClause is not null) {
                    var predicate_Body = this.HavingClause(x.HavingClause);
                    Source=Expressions.Expression.Call(
                        作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type),
                        Source,
                        Expressions.Expression.Lambda(
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
                //    return Expressions.Expression.Block(RefPeek_List_ColumnExpression);
                //} else {
                //    var selector_Body = ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression,0);
                //    var Result=Expressions.Expression.Call(
                //        作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,Element_Type,selector_Body.Type),
                //        Source,
                //        Expressions.Expression.Lambda(
                //            selector_Body,
                //            作業配列.Parameters設定(ss)
                //        )
                //    );
                //    return Result;
                //}
                var selector_Body = ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression,0);
                var Result=Expressions.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,Element_Type,selector_Body.Type),
                    Source,
                    Expressions.Expression.Lambda(
                        selector_Body,
                        作業配列.Parameters設定(ss)
                    )
                );
                return Result;
            }
        }
    }
}