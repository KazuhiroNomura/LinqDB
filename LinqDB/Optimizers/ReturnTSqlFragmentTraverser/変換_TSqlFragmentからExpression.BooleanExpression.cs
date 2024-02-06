using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Data.SqlTypes;
using e = System.Linq.Expressions;
using LinqDB.Helpers;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    //TSqlFragment.BooleanExpression
    private e.Expression BooleanBinaryExpression(BooleanBinaryExpression x){
        var FirstExpression=this.BooleanExpression(x.FirstExpression);
        var SecondExpression=this.BooleanExpression(x.SecondExpression);
        return x.BinaryExpressionType switch{
            BooleanBinaryExpressionType.And=>e.Expression.AndAlso(FirstExpression,SecondExpression),
            BooleanBinaryExpressionType.Or=>e.Expression.OrElse(FirstExpression,SecondExpression),
            _=>throw this.単純NotSupportedException(x)
        };
       
    }
    //TSqlFragment.BooleanExpression
    private e.Expression BooleanComparisonExpression(BooleanComparisonExpression x){
        var FirstExpression0=this.ScalarExpression(x.FirstExpression);
        var SecondExpression0=this.ScalarExpression(x.SecondExpression);
        var Result=this.Booleanを返すComparer(FirstExpression0,SecondExpression0,(Left,Right)=>{
            (Left,Right)=this.Convertデータ型を合わせるNullableは想定しない(Left,Right);
            Debug.Assert(Left.Type==Right.Type);
            if(typeof(bool)==Left.Type) {
                Left=e.Expression.Condition(Left,Constant_1,Constant_0);
                Right=e.Expression.Condition(Right,Constant_1,Constant_0);
            }
            e.Expression Result0=x.ComparisonType switch {
                BooleanComparisonType.Equals               =>e.Expression.Equal(Left,Right),
                BooleanComparisonType.GreaterThan          =>e.Expression.GreaterThan(Left,Right),
                BooleanComparisonType.LessThan             =>e.Expression.LessThan(Left,Right),
                BooleanComparisonType.GreaterThanOrEqualTo =>e.Expression.GreaterThanOrEqual(Left,Right),
                BooleanComparisonType.LessThanOrEqualTo    =>e.Expression.LessThanOrEqual(Left,Right),
                BooleanComparisonType.NotEqualToBrackets   =>e.Expression.NotEqual(Left,Right),                    //Left<>Right
                BooleanComparisonType.NotEqualToExclamation=>e.Expression.NotEqual(Left,Right),                    //Left!=Right
                BooleanComparisonType.NotLessThan          =>e.Expression.Not(e.Expression.LessThan(Left,Right)),  //Left!<Right
                _                                          =>e.Expression.Not(e.Expression.GreaterThan(Left,Right))//Left!>Right
                //BooleanComparisonType.NotGreaterThan       =>e.Expression.Not(e.Expression.GreaterThan(Left,Right)),//Left!>Right
                //_ => throw this.単純NotSupportedException(x.ComparisonType)
            };
            if(Result0.Type==typeof(SqlBoolean)) 
                return e.Expression.Call(Result0,IsTrue_get);
            return Result0;
        });
        return Result;
    }
    //TSqlFragment.BooleanExpression
    private e.Expression BooleanExpressionSnippet(BooleanExpressionSnippet x){
        throw this.単純NotSupportedException(x);
    }
    //TSqlFragment.BooleanExpression
    private e.Expression BooleanIsNullExpression(BooleanIsNullExpression x){
        var Expression=this.ScalarExpression(x.Expression);
        return e.Expression.Equal(
            Expression,
            e.Expression.Default(Expression.Type)
        );
    }
    //TSqlFragment.BooleanExpression
    private e.Expression BooleanNotExpression(BooleanNotExpression x)=>e.Expression.Not(this.BooleanExpression(x.Expression));
    //TSqlFragment.BooleanExpression
    private e.Expression BooleanParenthesisExpression(BooleanParenthesisExpression x)=>this.BooleanExpression(x.Expression);
    /// <summary>
    /// @byte1 between 97 and 102
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    //TSqlFragment.BooleanExpression
    private e.Expression BooleanTernaryExpression(BooleanTernaryExpression x){
        var FirstExpression=this.ScalarExpression(x.FirstExpression);
        var SecondExpression=this.ScalarExpression(x.SecondExpression);
        var ThirdExpression=this.ScalarExpression(x.ThirdExpression);
        e.Expression? Predicate=null;
        if(FirstExpression.Type.IsNullable()) {
            Predicate=e.Expression.Property(FirstExpression,"HasValue");
            FirstExpression = GetValueOrDefault(FirstExpression);
        }
        if(SecondExpression.Type.IsNullable()) {
            var HasValue=e.Expression.Property(SecondExpression,"HasValue");
            Predicate=Predicate is null?HasValue:e.Expression.AndAlso(Predicate,HasValue);
            SecondExpression = GetValueOrDefault(SecondExpression);
        }
        if(ThirdExpression.Type.IsNullable()) {
            var HasValue = e.Expression.Property(ThirdExpression,"HasValue");
            Predicate=Predicate is null ? HasValue:e.Expression.AndAlso(Predicate,HasValue);
            ThirdExpression = GetValueOrDefault(ThirdExpression);
        }
        //e.Expression Between;
        //if(FirstExpression.Type.IsNullable()) {
        //    e.Expression test = e.Expression.Property(FirstExpression,"HasValue");
        //    FirstExpression = GetValueOrDefault(FirstExpression);
        //    if(SecondExpression.Type.IsNullable()) {
        //        test = e.Expression.AndAlso(test,e.Expression.Property(SecondExpression,"HasValue"));
        //        SecondExpression = GetValueOrDefault(SecondExpression);
        //    }
        //    if(ThirdExpression.Type.IsNullable()) {
        //        test = e.Expression.AndAlso(test,e.Expression.Property(ThirdExpression,"HasValue"));
        //        ThirdExpression = GetValueOrDefault(ThirdExpression);
        //    }
        //    Between = e.Expression.AndAlso(
        //        e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
        //        e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
        //    );
        //    return e.Expression.Condition(test,Between,Constant_false);
        //} else {
        //    if(SecondExpression.Type.IsNullable()) {
        //        e.Expression test = e.Expression.Property(SecondExpression,"HasValue");
        //        SecondExpression = GetValueOrDefault(SecondExpression);
        //        if(ThirdExpression.Type.IsNullable()) {
        //            test = e.Expression.AndAlso(test,e.Expression.Property(ThirdExpression,"HasValue"));
        //            ThirdExpression = GetValueOrDefault(ThirdExpression);
        //        }
        //        Between = e.Expression.AndAlso(
        //            e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
        //            e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
        //        );
        //        return e.Expression.Condition(test,Between,Constant_false);
        //    } else {
        //        if(ThirdExpression.Type.IsNullable()) {
        //            var test = e.Expression.Property(ThirdExpression,"HasValue");
        //            ThirdExpression = GetValueOrDefault(ThirdExpression);
        //            Between = e.Expression.AndAlso(
        //                e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
        //                e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
        //            );
        //            return e.Expression.Condition(test,Between,Constant_false);
        //        } else {
        //            return e.Expression.AndAlso(
        //                e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
        //                e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
        //            );
        //        }
        //    }
        //}
        var (SecondExpression0,ThirdExpression0 )=this.Convertデータ型を合わせるNullableは想定しない(SecondExpression,ThirdExpression);
        var (FirstExpression0 ,SecondExpression1)=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,SecondExpression0);
        var (FirstExpression1 ,ThirdExpression1 )=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,ThirdExpression0);
        Debug.Assert(this.ExpressionEqualityComparer.Equals(FirstExpression0, FirstExpression1));
        Debug.Assert(SecondExpression1.Type==ThirdExpression1.Type);
        Debug.Assert(FirstExpression1.Type==SecondExpression1.Type);
        FirstExpression=FirstExpression1;
        SecondExpression=SecondExpression1;
        ThirdExpression=ThirdExpression1;
        var FirstExpression_Type =FirstExpression.Type;
        //var SecondExpression_Type=SecondExpression.Type;
        //var ThirdExpression_Type=ThirdExpression.Type;
        //if(FirstExpression_Type==typeof(DateTimeOffset)){
        //    SecondExpression=this.Convertデータ型を合わせるNullableは想定しない(SecondExpression,typeof(DateTimeOffset));
        //    ThirdExpression=this.Convertデータ型を合わせるNullableは想定しない(ThirdExpression,typeof(DateTimeOffset));
        //}else if(SecondExpression_Type==typeof(DateTimeOffset)){
        //    FirstExpression=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,typeof(DateTimeOffset));
        //    ThirdExpression=this.Convertデータ型を合わせるNullableは想定しない(ThirdExpression,typeof(DateTimeOffset));
        //}else if(ThirdExpression_Type==typeof(DateTimeOffset)){
        //    FirstExpression=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,typeof(DateTimeOffset));
        //    SecondExpression=this.Convertデータ型を合わせるNullableは想定しない(SecondExpression,typeof(DateTimeOffset));
        //}
        e.Expression Between;
        if(FirstExpression_Type==typeof(string)){
            Between=e.Expression.AndAlso(
                e.Expression.LessThanOrEqual(e.Expression.Call(Reflection.String.CompareOrdinal,SecondExpression,FirstExpression),Constant_0),
                e.Expression.LessThanOrEqual(e.Expression.Call(Reflection.String.CompareOrdinal,FirstExpression,ThirdExpression),Constant_0)
            );
        }else if(FirstExpression_Type==typeof(DateTime)){
            Between=e.Expression.AndAlso(
                e.Expression.Call(Reflection.DateTime.op_LessThanOrEqual,SecondExpression,FirstExpression),
                e.Expression.Call(Reflection.DateTime.op_LessThanOrEqual,FirstExpression,ThirdExpression)
            );
        }else if(FirstExpression_Type==typeof(DateTimeOffset)){
            Between=e.Expression.AndAlso(
                e.Expression.Call(Reflection.DateTimeOffset.op_LessThanOrEqual,SecondExpression,FirstExpression),
                e.Expression.Call(Reflection.DateTimeOffset.op_LessThanOrEqual,FirstExpression,ThirdExpression)
            );
        }else{
            Between=e.Expression.AndAlso(
                e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
                e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
            );
        }
        if(Predicate is not null)Between=e.Expression.AndAlso(Predicate,Between);
        return x.TernaryExpressionType switch{
            BooleanTernaryExpressionType.Between=>Between,
            BooleanTernaryExpressionType.NotBetween=>e.Expression.Not(Between),
            _=>throw new NotSupportedException(x.TernaryExpressionType.ToString())
        };
        //switch(x.TernaryExpressionType){
        //    case BooleanTernaryExpressionType.Between:return Between;
        //    case BooleanTernaryExpressionType.NotBetween:return e.Expression.Not(Between);
        //    default:throw new NotSupportedException(x.TernaryExpressionType.ToString());
        //}
    }
    //TSqlFragment.BooleanExpression
    private e.Expression EventDeclarationCompareFunctionParameter(EventDeclarationCompareFunctionParameter x){throw this.単純NotSupportedException(x);}
    /// <summary>
    /// TSqlFragment.BooleanExpression
    /// WHERE EXISTS(SELECT * FROM)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression ExistsPredicate(ExistsPredicate x){
        var StackSubquery単位の情報=this._StackSubquery単位の情報;
        StackSubquery単位の情報.Push();
        var QueryExpression=this.QueryExpression(x.Subquery.QueryExpression);//Subqueryしない理由はSingleOrDefaultしたくないから。
        StackSubquery単位の情報.Pop();
        return e.Expression.Call(
            this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Any,IEnumerable1のT(QueryExpression.Type)),
            QueryExpression
        );
    }
    /// <summary>
    ///TSqlFragment.BooleanExpression
    /// L_SHIPMODE(select f from ...),L_SHIPMODE IN('MAIL','SHIP')
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression InPredicate(InPredicate x){
        //where x.Expresssion in x.Qubquery
        e.Expression Result;
        var ScalarExpression=this.ScalarExpression(x.Expression);
        if(x.Subquery is not null){
            //WHERE O_ORDERKEY IN(SELECT * FROM)
            Debug.Assert(x.Values is not null&&x.Values.Count==0);
            var ScalarSubquery=this.Subquery(x.Subquery);
            Result=e.Expression.Call(
                this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Contains_value,ScalarSubquery.Type.GetGenericArguments()[0]),
                ScalarSubquery,
                ScalarExpression
            );
        }else{
            //WHERE O_ORDERKEY IN(1,2,3)
            var Values=x.Values;
            var Values_Count=Values.Count;
            var ScalarExpression_Type=ScalarExpression.Type;
            Result= 共通(0);
            for(var a=1;a<Values_Count;a++)Result=this.NULL_OrElse(Result,共通(a));
            e.Expression 共通(int a)=>this.Booleanを返すComparer(
                ScalarExpression,
                this.Convertデータ型を合わせるNullableは想定する(
                    this.ScalarExpression(Values[a]),
                    ScalarExpression_Type
                ),
                (Left,Right)=>e.Expression.Equal(Left,Right)
            );
        }
        if(x.NotDefined){
            return e.Expression.Not(Result);
        }
        return Result;
    }
    //TSqlFragment.BooleanExpression
    private e.Expression LikePredicate(LikePredicate x){
        var FirstExpression=this.ScalarExpression(x.FirstExpression);
        var SecondExpression=this.ScalarExpression(x.SecondExpression);
        e.Expression Result=e.Expression.Call(
            e.Expression.Call(
                Reflection.Regex.LikeからRegex,
                SecondExpression
            ),
            Reflection.Regex.IsMatch,
            this.作業配列.Expressions設定(FirstExpression)
        );
        if(x.NotDefined){
            Result=e.Expression.Not(Result);
        }
        return Result;
    }
    //TSqlFragment.BooleanExpression
    private e.Expression FullTextPredicate(FullTextPredicate x){throw this.単純NotSupportedException(x);}
    //TSqlFragment.BooleanExpression
    private e.Expression GraphMatchExpression(GraphMatchExpression x){throw this.単純NotSupportedException(x);}
    //TSqlFragment.BooleanExpression
    private e.Expression GraphMatchPredicate(GraphMatchPredicate x){throw this.単純NotSupportedException(x);}
    //TSqlFragment.BooleanExpression
    private e.Expression SubqueryComparisonPredicate(SubqueryComparisonPredicate x){throw this.単純NotSupportedException(x);}
    //TSqlFragment.BooleanExpression
    private e.Expression TSEqualCall(TSEqualCall x){throw this.単純NotSupportedException(x);}
    //TSqlFragment.BooleanExpression
    private e.Expression UpdateCall(UpdateCall x){throw this.単純NotSupportedException(x);}
}
