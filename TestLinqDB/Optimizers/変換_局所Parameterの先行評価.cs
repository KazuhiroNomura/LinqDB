using System.Linq.Expressions;
using LinqDB.Databases.Tables;
using LinqDB.Helpers;
using LinqDB.Sets;
//using Exception=System.Exception;
using Expression = System.Linq.Expressions.Expression;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers;
public class 変換_局所Parameterの先行評価:共通{
    [Fact]public void Assign(){
        this.Expression実行AssertEqual((int p)=>p*p+p*p);
    }
    [Fact]public void Block(){
        var p=Expression.Parameter(typeof(int),"p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Block(new []{p},p),
                p
            )
        );
    }
    [Fact]public void Lambda(){
        //while(true) {
        //    if(二度出現した一度目のExpression is null) break;
        this.Expression実行AssertEqual((int p)=>p*p+p*p);
        //}
        //if(Block1_Variables.Count>0) Lambda2_Body=Expression.Block(Block1_Variables,this.作業配列.Expressions設定(Lambda2_Body));
        this.Expression実行AssertEqual((int p)=>p*p+p*p);
        this.Expression実行AssertEqual((int p)=>p);
    }
    [Fact]public void Conditional(){
        //if(Conditional0_Test==Conditional1_Test)
        //    if(Conditional0_IfTrue==Conditional1_IfTrue)
        //        if(Conditional0_IfFalse==Conditional1_IfFalse)
        this.Expression実行AssertEqual((int p)=>p>0?p*p:p+p);
        this.Expression実行AssertEqual((int p)=>p>0?p*p:(p+p)-(p+p));
        this.Expression実行AssertEqual((int p)=>p>0?(p+p)-(p+p):p*p);
        this.Expression実行AssertEqual((int p)=>(p+p)-(p+p)>0?p:p*p);
        //Expression 共通(Expression Expression0){
        //    while(true) {
        //        if(二度出現した一度目のExpression is null)break;
        this.Expression実行AssertEqual((int p)=>p>0?p*p+p*p:p*p-p*p);
        //    }
        //}
    }
    [Fact]public void 取得_Labelに対応するExpressions_Label(){
        var Label=Expression.Label(typeof(int),"Label1");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Label(Label,Expression.Constant(1))
            )
        );
    }
    [Fact]public void 取得_Labelに対応するExpressions_Loop(){
        var Continue=Expression.Label(typeof(void),"Continue");
        var Break=Expression.Label(typeof(int),"Break");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Loop(
                        Expression.Goto(Break,Expression.Constant(1)),
                        Break,
                        Continue
                    )
                )
            )
        );
        //if(Loop.ContinueLabel is not null)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break,Expression.Constant(1)),
                    Break,
                    Continue
                )
            )
        );
        //else
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break,Expression.Constant(1)),
                    Break
                )
            )
        );
        //if(Loop.BreakLabel is not null)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break,Expression.Constant(1)),
                    Break
                )
            )
        );
        //else
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Loop(
                        Expression.Goto(Break,Expression.Constant(1))
                    ),
                    Expression.Label(Break,Expression.Constant(2))
                )
            )
        );
    }
    [Fact]public void 取得_Labelに対応するExpressions_Block(){
        //for(var a=0;a<Block_Expressions_Count;a++){
        //    switch(Block_Expression.NodeType){
        //        case ExpressionType.Block:
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Constant(1)
                )
            )
        );
        //        case ExpressionType.Loop:
        var Break=Expression.Label(typeof(int),"Break");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break,Expression.Constant(1)),
                    Break
                )
            )
        );
        //        case ExpressionType.Label:continue;
        //        case ExpressionType.Goto:this.ListExpression=null; break;
        //        default:this.ListExpression?.Add(Block_Expression); break;
        //    }
        //}
        var Continue=Expression.Label(typeof(void),"Continue");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Loop(
                        Expression.Goto(Break,Expression.Constant(1)),
                        Break,
                        Continue
                    )
                )
            )
        );
        //if(Loop.ContinueLabel is not null)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break,Expression.Constant(1)),
                    Break,
                    Continue
                )
            )
        );
        //else
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break,Expression.Constant(1)),
                    Break
                )
            )
        );
        //if(Loop.BreakLabel is not null)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break,Expression.Constant(1)),
                    Break
                )
            )
        );
        //else
    }
}
