using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using e = System.Linq.Expressions;
using LinqDB.Helpers;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    /// <summary>
    /// 単純検索CASE
    /// CASE N_NAME 
    ///     WHEN 'BRAZIL' THEN 'brazil' 
    ///     WHEN 'EUROPE' THEN 'europe' 
    ///     ELSE 'Z' 
    /// END
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression SimpleCaseExpression(SimpleCaseExpression x){
        var WhenClauses=x.WhenClauses;
        var WhenClauses_Count=WhenClauses.Count;
        var Switch_SwitchValue=this.ScalarExpression(x.InputExpression);
        var Switch_DefaultBody=x.ElseExpression is not null?this.ScalarExpression(x.ElseExpression):null;
        var Case_Bodies=new(e.Expression TestValue,e.Expression Body)[WhenClauses_Count];
        //Switch_SwitchValueがnullならSwitch_DefaultBody
        //TestValueがnullならSwitch_DefaultBody
        //Body,Switch_DefaultBodyのいずれかがNullableなら戻り値はNullable
        e.Expression? Condition;
        if(Switch_SwitchValue.Type.IsNullable()){
            Condition=e.Expression.Property(Switch_SwitchValue,"HasValue");
            Switch_SwitchValue=GetValueOrDefault(Switch_SwitchValue);
        }else
            Condition=null;
        Type? ResultType=null;
        //var Result_Type=Switch_DefaultBody.Type;
        //this.Convertデータ型を合わせるNullableは想定する()
        for(var a=0;a<WhenClauses_Count;a++){
            var WhenClause=WhenClauses[a];
            var TestValue=this.ScalarExpression(WhenClause.WhenExpression);
            var Body=this.ScalarExpression(WhenClause.ThenExpression);
            if(TestValue.Type.IsNullable()){
                var HasValue=e.Expression.Property(TestValue,"HasValue");
                Condition=Condition is null ? HasValue:e.Expression.AndAlso(Condition,HasValue);
                Case_Bodies[a]=(GetValueOrDefault(TestValue),Body);
            }else
                Case_Bodies[a]=(TestValue,Body);
            if(Body.Type.IsNullable())ResultType=Body.Type;
            else if(ResultType is null)ResultType=Body.Type;
        }
        //var Call=Expressions.Expression.Call(
        //    Method,
        //    入力Expressions
        //);
        //var Call=非NullのExpression(Condition,Case_Bodies);
        //if(Condition is null)return Call;
        //return Expressions.Expression.Condition(
        //    Condition,
        //    Call,
        //    Expressions.Expression.Constant(null,Call.Type)
        //);
        var SwitchCases=new e.SwitchCase[WhenClauses_Count];
        var SwitchCase_TestValues=this.作業配列.Expressions1;
        var Switch_SwitchValue_Type=Switch_SwitchValue.Type;
        for(var a=0;a<WhenClauses_Count;a++){
            ref var Case_Body=ref Case_Bodies[a];
            var Case_Body_TestValue=this.Convertデータ型を合わせるNullableは想定する(Case_Body.TestValue,Switch_SwitchValue_Type);
            SwitchCase_TestValues[0]=Case_Body_TestValue;
            SwitchCases[a]=e.Expression.SwitchCase(
                this.Convertデータ型を合わせるNullableは想定する(Case_Body.Body,ResultType!),
                SwitchCase_TestValues
            );
        }
        Switch_DefaultBody??=e.Expression.Default(ResultType);
        var Switch=e.Expression.Switch(Switch_SwitchValue,Switch_DefaultBody,SwitchCases);
        if(Condition is null) return Switch;
        return e.Expression.Condition(Condition, Switch, e.Expression.Default(ResultType));
    }
    /// <summary>
    /// 検索CASE
    /// CASE
    ///     WHEN N_NAME='BRAZIL' THEN 'brazil' 
    ///     WHEN N_NAME='EUROPE' THEN 'europe' 
    ///     ELSE 'Z' 
    /// END
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression SearchedCaseExpression(SearchedCaseExpression x){
        var WhenClauses=x.WhenClauses;
        var WhenClauses_Count=WhenClauses.Count;
        var Result=x.ElseExpression is null?Constant_null:this.ScalarExpression(x.ElseExpression);
        for(var a = WhenClauses_Count-1;a>=0;a--) {
            var WhenClause = WhenClauses[a];
            var IfTrue = this.ScalarExpression(WhenClause.ThenExpression);
            (IfTrue,Result)=this.Convertデータ型を合わせるNullableは想定する(IfTrue,Result);
            Result=e.Expression.Condition(
                this.BooleanExpression(WhenClause.WhenExpression),
                IfTrue,
                Result
            );
        }
        return Result;
    }
}
