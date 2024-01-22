
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Sets;
using System.Globalization;
using System.Linq.Expressions;

using TestLinqDB.特殊パターン;


//using Exception=System.Exception;
using Expression = System.Linq.Expressions.Expression;
using SwitchCase = System.Linq.Expressions.SwitchCase;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 作成_辺 : 共通{
    //protected override テストオプション テストオプション=>テストオプション.式木の最適化を試行;
    [Fact]public void Traverse(){
        //switch(Expression.NodeType){
        //    case ExpressionType.DebugInfo:
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.DebugInfo(
                        Expression.SymbolDocument("abc"),1,1,1,1
                    ),
                    Expression.Constant(1)
                )
            )
        );
        //    case ExpressionType.Default:
        //    case ExpressionType.Lambda:
        //    case ExpressionType.PostDecrementAssign:
        //    case ExpressionType.PostIncrementAssign:
        //    case ExpressionType.PreDecrementAssign:
        //    case ExpressionType.PreIncrementAssign:
        //    case ExpressionType.Throw:
        //        return;
        //    case ExpressionType.Call:{
        //        if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression).Method)) return;
        this.Expression実行AssertEqual(()=>"a".NoEarlyEvaluation());
        this.Expression実行AssertEqual(()=>int.Parse("123"));
        //    }
        //    case ExpressionType.Constant:{
        //        if(ILで直接埋め込めるか((ConstantExpression)Expression)) return;
        this.Expression実行AssertEqual(()=>1);
        this.Expression実行AssertEqual(()=>1m);
        //    }
        //    case ExpressionType.Parameter:{
        //        if(this.ラムダ跨ぎParameters.Contains(Expression)) break;
        var a=Expression.Parameter(typeof(int),"a");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Block(
                    Expression.Lambda<Func<int>>(
                        Expression.Assign(a,Expression.Constant(0))
                    ),
                    a
                ),
                a
            )
        );
        //    }
        //}
    }
    [Fact]public void Conditional(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Expression.NotEqual(_1m,_1m),
                Expression.Add(_1m,_1m),
                Expression.Multiply(_1m,_1m)
            )
        );
    }
    [Fact]public void Goto(){
        //if(Dictionary_LabelTarget_辺.TryGetValue(Goto.Target,out var 移動先)){
        var Label1 = Expression.Label();
        var Label2 = Expression.Label();
        var Label3 = Expression.Label();
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    Expression.Goto(Label2),
                    Expression.Label(Label1),
                    Expression.Goto(Label3),
                    Expression.Label(Label2),
                    Expression.Goto(Label1),
                    Expression.Label(Label3),
                    Expression.Constant(true)
                )
            )
        );
        //} else{
        //}
    }
    [Fact]public void Label(){
        //if(Dictionary_LabelTarget_辺.TryGetValue(Label.Target,out var 移動先)){
        var Label1 = Expression.Label();
        var Label2 = Expression.Label();
        var Label3 = Expression.Label();
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    Expression.Goto(Label2),
                    Expression.Label(Label1),
                    Expression.Goto(Label3),
                    Expression.Label(Label2),
                    Expression.Goto(Label1),
                    Expression.Label(Label3),
                    Expression.Constant(true)
                )
            )
        );
        //} else{
        //}
    }
    [Fact]public void Assign(){
        //if(Assign_Left.NodeType is not ExpressionType.Parameter) base.Traverse(Assign_Left);
        var array=new int[3];
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Assign(
                    Expression.ArrayAccess(
                        Expression.Constant(array),
                        Expression.Constant(0)
                    ),
                    Expression.Constant(0)
                )
            )
        );
        var a=Expression.Parameter(typeof(int),"a");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Block(
                    Expression.Lambda<Func<int>>(
                        Expression.Assign(a,Expression.Constant(0))
                    ),
                    a
                ),
                a
            )
        );
    }
    [Fact]public void Loop(){
        var Continue=Expression.Label(typeof(void),"Continue");
        var Break=Expression.Label(typeof(int),"Break");
        //if(Loop.ContinueLabel is not null){
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
        //} else
        var Label = Expression.Label(typeof(bool));
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    Expression.Constant(1m),
                    Expression.Constant(1m),
                    Expression.Loop(
                        Expression.Goto(Label,Expression.Constant(true)),
                        Label
                    )
                )
            )
        );
        //if(Loop.BreakLabel is not null){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break,
                    Continue
                )
            )
        );
        //}
        this.Optimizer_Lambda最適化(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Constant(1m),
                    Expression.Constant(1m),
                    Expression.Loop(
                        Expression.Constant(1m)
                    )
                )
            )
        );
    }
    [Fact]
    public void Switch(){
        var @int = Expression.Parameter(typeof(int), "int");
        var e = Expression.Switch(
            @int,
            @int,
            Expression.SwitchCase(
                @int,
                Expression.Constant(1)
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int, int>>(
                e,
                @int
            )
        );
    }
    [Fact]
    public void Try(){
        //for(var a=0;a<Try_Handlers_Count;a++){
        //    if(Try_Handle.Variable is not null) sb.Append($"{Try_Handle.Variable.Name}");
        {
            var ex = Expression.Parameter(typeof(Exception));
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryCatch(
                        Expression.Constant(1m),
                        Expression.Catch(
                            ex,
                            Expression.Constant(1m)
                        )
                    )
                )
            );
        }
        //}
        //if(Try_Fault is not null){
        {
            var ParameterInt32 = Expression.Parameter(typeof(int), "int32");
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<int, int>>(
                    Expression.TryFault(
                        ParameterInt32,
                        Expression.AddAssign(ParameterInt32, ParameterInt32)
                    ),
                    ParameterInt32
                )
            );        }
        //} else{
        //    if(Try_Finally is not null){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Expression.TryCatch(
                    Expression.Constant(1m),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(1m)
                    )
                )
            )
        );
        //    }
        {
            var p = Expression.Parameter(typeof(int), "int32");
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<int, int>>(
                    Expression.TryCatchFinally(
                        p,
                        Expression.AddAssign(p, p)
                    ), p
                )
            );
        }
        //}
    }
}
