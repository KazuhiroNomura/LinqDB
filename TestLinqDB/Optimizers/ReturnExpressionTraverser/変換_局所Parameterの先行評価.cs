using LinqDB.Sets;

using System.Diagnostics;
using System.Globalization;

using TestLinqDB.特殊パターン;


//using Exception=System.Exception;
using Expression = System.Linq.Expressions.Expression;
using SwitchCase = System.Linq.Expressions.SwitchCase;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_局所Parameterの先行評価 : 共通{
    //protected override テストオプション テストオプション=>テストオプション.式木の最適化を試行;
    [Fact]public void 変形確認1(){
        var p = Expression.Parameter(typeof(decimal));
        var Assign=Expression.Assign(
            p,
            Expression.Constant(0m)
        );
        var Add=Expression.Add(p,p);
        Add=Expression.Add(Add,Add);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { p },
                    Add,
                    Assign,
                    Add
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { p },
                    Add,
                    Assign,
                    Add,
                    Assign,
                    Add
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(0m)
                    ),
                    Expression.Add(
                        p,
                        p
                    ),
                    Expression.Add(
                        p,
                        Expression.Add(
                            p,
                            p
                        )
                    )
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.Block(
                    Expression.Add(
                        p,
                        p
                    ),
                    Expression.Add(
                        p,
                        Expression.Add(
                            p,
                            p
                        )
                    )
                ),
                p
            )
        );
        ////ラムダを跨ぐ
    }
    [Fact]public void 変形確認2TryCatch(){
        var _1m = Expression.Constant(1m);
        var _2m = Expression.Constant(2m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Condition(
                    Expression.Equal(_1m,_1m),
                    Expression.Add(_1m,_1m),
                    Expression.Subtract(_1m,_1m)
                ),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Condition(
                        Expression.NotEqual(_1m,_1m),
                        Expression.Divide(_1m,_1m),
                        Expression.Multiply(_1m,_1m)
                    )
                )
            )
        );
    }
    [Fact]public void 変形確認3TryCatch(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Add(_1m,_1m),
                Expression.TryCatch(
                    Expression.Condition(
                        Expression.Equal(_1m,_1m),
                        Expression.Add(_1m,_1m),
                        Expression.Subtract(_1m,_1m)
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Condition(
                            Expression.NotEqual(_1m,_1m),
                            Expression.Add(_1m,_1m),
                            Expression.Multiply(_1m,_1m)
                        )
                    )
                )
            )
        );
    }
    [Fact]public void 変形確認5Condition後式(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,_1m),
                    Expression.Subtract(_1m,_1m)
                ),
                Expression.Equal(_1m,_1m)
            )
        );
    }
    [Fact]public void 変形確認6Condition後式0(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,_1m),
                    _1m
                ),
                Expression.Add(_1m,_1m)
            )
        );
    }
    [Fact]public void 変形確認6Condition後式1(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    _1m,
                    Expression.Add(_1m,_1m)
                ),
                Expression.Add(_1m,_1m)
            )
        );
    }
    [Fact]public void 変形確認4Condition前式0(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Expression.Equal(_1m,_1m),
                Expression.Add(_1m,_1m),
                Expression.Subtract(_1m,_1m)
            )
        );
    }
    [Fact]public void 変形確認7Condition前式1(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Equal(_1m,_1m),
                    Expression.Equal(_1m,_1m),
                    Expression.NotEqual(_1m,_1m)
                )
            )
        );
    }
    [Fact]public void 変形確認8Condition後式0(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,Expression.Add(_1m,_1m)),
                    Expression.Add(_1m,Expression.Add(_1m,_1m))
                ),
                Expression.Add(_1m,Expression.Add(_1m,_1m))
            )
        );
    }
    [Fact]public void 変形確認8Condition後式1(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,Expression.Add(_1m,_1m)),
                    Expression.Subtract(_1m,Expression.Add(_1m,_1m))
                ),
                Expression.Add(_1m,Expression.Add(_1m,_1m))
            )
        );
    }
    [Fact]public void 変形確認8Condition後式2(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,Expression.Add(_1m,_1m)),
                    Expression.Subtract(_1m,Expression.Add(_1m,_1m))
                ),
                Expression.Multiply(_1m,Expression.Add(_1m,_1m))
            )
        );
    }
    [Fact]public void 変形確認8Condition後式3(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,_1m),
                    Expression.Subtract(_1m,_1m)
                ),
                _1m
            )
        );
    }
    [Fact]public void 変形確認9Condition後式(){
        var _1m = Expression.Constant(1m);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Add(_1m,Expression.Add(_1m,_1m)),
                    Expression.Subtract(_1m,Expression.Add(_1m,_1m))
                ),
                Expression.Equal(_1m,Expression.Add(_1m,_1m)),
                Expression.Add(_1m,Expression.Add(_1m,_1m)),
                Expression.Subtract(_1m,Expression.Add(_1m,_1m))
            )
        );
    }
    public class 辺を作る : 共通{
        //protected override テストオプション テストオプション=>テストオプション.式木の最適化を試行;
        [Fact]public void Loop無限(){
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
        [Fact]public void Loop無条件break(){
            var Break=Expression.Label(typeof(decimal));
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        Expression.Constant(1m),
                        Expression.Constant(1m),
                        Expression.Loop(
                            Expression.Break(
                                Break,
                                Expression.Constant(1m)
                            ),
                            Break
                        )
                    )
                )
            );
        }
        [Fact]public void Loop条件break(){
            var p=Expression.Parameter(typeof(bool),"p");
            var Label=Expression.Label(typeof(bool));
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<bool,bool>>(
                    Expression.Block(
                        Expression.Constant(1m),
                        Expression.Constant(1m),
                        Expression.Loop(
                            Expression.IfThenElse(
                                p,
                                Expression.Default(typeof(void)),
                                Expression.Goto(Label,Expression.Constant(true))
                            ),
                            Label
                        )
                    ),
                    p
                )
            );
        }
        [Fact]
        public void Traverse()
        {
            //switch(Expression.NodeType) {
            //    case ExpressionType.DebugInfo:
            //    case ExpressionType.Default:
            //    case ExpressionType.Lambda :
            //    case ExpressionType.PostDecrementAssign:
            //    case ExpressionType.PostIncrementAssign:
            //    case ExpressionType.PreDecrementAssign:
            //    case ExpressionType.PreIncrementAssign:
            //    case ExpressionType.Throw:
            {
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Throw(Expression.New(typeof(NotImplementedException)),typeof(bool))
                        )
                    )
                );
            }
            //    case ExpressionType.Label:{
            //        if(Label.DefaultValue is not null)
            {
                var Label = Expression.Label(typeof(int));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int>>(
                        Expression.Block(
                            Expression.Goto(Label, Expression.Constant(1)),
                            Expression.Label(Label, Expression.Constant(2))
                        )
                    )
                );
            }
            //        if(this.Dictionary_LabelTarget_辺に関する情報.TryGetValue(Label.Target,out var 移動先)){
            {
                var Label = Expression.Label();
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Goto(Label),
                            Expression.Label(Label),
                            Expression.Constant(true)
                        )
                    )
                );
            }
            //        } else{
            //        }
            {
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
            }
            //    }
            //    case ExpressionType.Goto:{
            //        if(Goto.Value is not null)
            //            if(this.Dictionary_LabelTarget_辺に関する情報.TryGetValue(Goto.Target,out var 上辺)){
            //_2
            //            } else {
            //_3
            //            }
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Goto(Label,Expression.Constant(false)),
                            Expression.Label(Label,Expression.Constant(true))
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Loop:{
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Loop(
                                Expression.Goto(Label,Expression.Constant(false)),
                                Label
                            )
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Assign: {
            //        if(Assign_Left is IndexExpression Index)
            //        else if(Assign_Left is ParameterExpression){
            {
                var p = Expression.Parameter(typeof(int), "p");
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int>>(
                        Expression.Block(
                            new[]{p},
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Assign(p, Expression.Constant(1))
                        )
                    )
                );
            }
            //        } else{
            //        }
            //    }
            //    case ExpressionType.Call: {
            //        if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression).Method))return;
            {
                var _1m = 1m;
                this.Expression実行AssertEqual(
                    () => _1m+_1m+_1m.NoEarlyEvaluation()
                );
            }
            {
                var _1m = 1m;
                var set = new Set<int>();
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set, EqualityComparer<int>.Default) }
                );
            }
            //    }
            //    case ExpressionType.Constant: {
            //        if(ILで直接埋め込めるか((ConstantExpression)Expression))return;
            {
                var _1 = 1;
                this.Expression実行AssertEqual(
                    () => _1+_1+1
                );
            }
            //    }
            //    case ExpressionType.Parameter: {
            //        if(this.ラムダ跨ぎParameters.Contains(Expression))break;
            {
                var p = Expression.Parameter(typeof(int), "p");
                var _1 = 1;
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int,Func<int>>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Lambda<Func<int>>(p)
                        ), p
                    )
                );
                var q = Expression.Parameter(typeof(int), "q");
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int,Func<int,int>>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            p,
                            Expression.Lambda<Func<int,int>>(
                                Expression.Block(
                                    p,
                                    Expression.Add(
                                        Expression.Add(q, q),
                                        Expression.Add(q, q)
                                    )
                                ),
                                q
                            )
                        ), p
                    )
                );
            }
            //    }
            //}
            //if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
            //}
        }
        [Fact]public void Conditional()
        {
            //if(Test==Conditional1_Test)
            //    if(IfTrue==Conditional1_IfTrue)
            //        if(IfFalse==Conditional1_IfFalse)
            {
                var @true = Expression.Constant(true);
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Condition(
                            Expression.Equal(
                                Expression.Constant(1m),
                                Expression.Constant(1m)
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            //        else
            {
                var @true = Expression.Constant(true);
                var operator_true = Expression.Constant(new operator_true(true));
                var operator_false = Expression.Constant(new operator_true(false));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            @true,
                            operator_false,
                            Expression.Condition(
                                Expression.Call(
                                    operator_true.Type.GetMethod("op_True")!,
                                    operator_true
                                ),
                                operator_true,
                                operator_true
                            )
                        )
                    )
                );
            }
            //    else
            {
                var @true = Expression.Constant(true);
                var operator_true = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            @true,
                            Expression.Condition(
                                Expression.Call(
                                    operator_true.Type.GetMethod("op_True")!,
                                    operator_true
                                ),
                                operator_true,
                                operator_true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    operator_true.Type.GetMethod("op_True")!,
                                    operator_true
                                ),
                                operator_true,
                                operator_true
                            )
                        )
                    )
                );
            }
            //else
            //_0

            {
                var @true = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            Expression.Call(
                                @true.Type.GetMethod("op_True")!,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            {
                var Constant = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.AndAlso(
                            Constant,
                            Expression.New(typeof(operator_true))
                        )
                    )
                );
            }
        }
        [Fact]
        public void Conditional0(){
            {
                var p=Expression.Parameter(typeof(int),"p");
                var @true=Expression.Constant(true);
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int,int>>(
                        Expression.Condition(
                            Expression.Equal(
                                p,
                                p
                            ),
                            Expression.Condition(
                                @true,
                                p,
                                Expression.Add(
                                    Expression.Add(p,p),
                                    Expression.Add(p,p)
                                )
                            ),
                            Expression.Condition(
                                @true,
                                p,
                                p
                            )
                        ),
                        p
                    )
                );
            }
        }
        [Fact]
        public void Switch()
        {
            //for(var a=0; a < Switch0_Cases_Count; a++) {
            //    if(Switch0_Case_Body !=Switch1_Case_Body) {
            {
                var p = Expression.Parameter(typeof(int), "p");
                var pp = Expression.Add(p, p);
                var e = Expression.Switch(
                    pp,
                    pp,
                    Expression.SwitchCase(pp, Expression.Constant(0))
                );
                this.Expression実行AssertEqual(Expression.Lambda<Func<int, int>>(e, p));
            }
            //    } else
            {
                var p = Expression.Parameter(typeof(int), "p");
                var pp = Expression.Add(p, p);
                var ppp = Expression.Add(pp, pp);
                var e = Expression.Switch(
                    pp,
                    pp,
                    Expression.SwitchCase(pp, Expression.Constant(0)),
                    Expression.SwitchCase(
                        Expression.Switch(
                            pp,
                            p,
                            Expression.SwitchCase(p, Expression.Constant(1)),
                            Expression.SwitchCase(p, Expression.Constant(2))
                        ), Expression.Constant(3)
                    )
                );
                this.Expression実行AssertEqual(Expression.Lambda<Func<int, int>>(e, p));
            }
            //}
            //if(!変化したか) return Switch0;
            {
                var p = Expression.Parameter(typeof(int), "p");
                var pp = Expression.Add(p, p);
                var ppp = Expression.Add(pp, pp);
                var e = Expression.Switch(
                    pp,
                    pp,
                    Expression.SwitchCase(pp, Expression.Constant(0)),
                    Expression.SwitchCase(
                        Expression.Switch(
                            pp,
                            p,
                            Expression.SwitchCase(p, Expression.Constant(1)),
                            Expression.SwitchCase(p, Expression.Constant(2))
                        ), Expression.Constant(3)
                    )
                );
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int,int>>(
                        Expression.Switch(
                            pp,
                            pp,
                            Expression.SwitchCase(pp, Expression.Constant(0)),
                            Expression.SwitchCase(
                                Expression.Block(
                                    pp,
                                    Expression.Switch(
                                        p,
                                        p,
                                        Expression.SwitchCase(p, Expression.Constant(1)),
                                        Expression.SwitchCase(p, Expression.Constant(2))
                                    )
                                ),
                                Expression.Constant(3)
                            )
                        ),
                        p
                    )
                );
            }
            //_0
        }
        [Fact]
        public void Call()
        {
            //var MethodCall0_Method = MethodCall0.Method;
            //if(this.IsInline&&ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall0_Method))) {
            //    switch(MethodCall0_Method.Name) {
            //        case nameof(Sets.ExtensionSet.Except): {
            //            if(MethodCall0.Arguments.Count==3) return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,this.Traverse(MethodCall0_Arguments[2]));
            {
                var set = new Set<int>();
                var _1m = 1m;
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Join(set, o => o, i => i, (o, i) => o+i) }
                );
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set, EqualityComparer<int>.Default) }
                );
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set) }
                );
            }
            //            else return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
            //        }
            //        default:
            //    }
            //}
        }
    }
    public class 変換_二度出現したExpression : 共通{
        //protected override テストオプション テストオプション=>テストオプション.式木の最適化を試行;
        [Fact]
        public void Traverse()
        {
            //switch(Expression0.NodeType){
            //    case ExpressionType.DebugInfo:
            //    case ExpressionType.Default:
            //    case ExpressionType.Lambda :
            //    case ExpressionType.PostDecrementAssign:
            //    case ExpressionType.PostIncrementAssign:
            //    case ExpressionType.PreDecrementAssign:
            //    case ExpressionType.PreIncrementAssign:
            //    case ExpressionType.Throw:
            {
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Throw(Expression.New(typeof(NotImplementedException)), typeof(bool))
                        )
                    )
                );
            }
            //    case ExpressionType.Label: {
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Label(Label,Expression.Constant(true))
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Goto:{
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Goto(Label,Expression.Constant(false)),
                            Expression.Label(Label,Expression.Constant(true))
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Loop: {
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Loop(
                                Expression.Goto(Label,Expression.Constant(false)),
                                Label
                            )
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Assign: {
            {
                var p = Expression.Parameter(typeof(int), "p");
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int>>(
                        Expression.Block(
                            new[]{p},
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Assign(p, Expression.Constant(1))
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Call: {
            //        if(Reflection.Helpers.NoEarlyEvaluation==GenericMethodDefinition)return Expression0;
            {
                var _1m = 1m;
                this.Expression実行AssertEqual(
                    () => _1m+_1m+_1m.NoEarlyEvaluation()
                );
            }
            //        if(ループ展開可能メソッドか(GenericMethodDefinition)) {
            //            switch(MethodCall0_Method.Name) {
            //                case nameof(Sets.ExtensionSet.Except): {
            //                    if(MethodCall0.Arguments.Count==3) return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,this.Traverse(MethodCall0_Arguments[2]));
            {
                var _1m = 1m;
                var set = new Set<int>();
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set, EqualityComparer<int>.Default) }
                );
            }
            //                    else return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
            {
                var _1m = 1m;
                var set = new Set<int>();
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set) }
                );
            }
            //                }
            //                default:
            {
                var _1m = 1m;
                var set = new Set<int>();
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Join(set, o => o, i => i, (o, i) => o+i) }
                );
            }
            //            }
            //        }
            {
                var _1m = 1m;
                this.Expression実行AssertEqual(
                    () => _1m+_1m+_1m.ToString(CultureInfo.InvariantCulture)
                );
            }
            //    }
            //    case ExpressionType.Constant: {
            //        if(ILで直接埋め込めるか((ConstantExpression)Expression0))return Expression0;
            {
                var _1 = 1;
                this.Expression実行AssertEqual(
                    () => _1+_1+1
                );
            }
            //    }
            //    case ExpressionType.Parameter: {
            //        if(!this.ラムダ跨ぎParameters.Contains(Expression0))return Expression0;
            {
                var p = Expression.Parameter(typeof(int), "p");
                var _1 = 1;
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int,Func<int>>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Lambda<Func<int>>(p)
                        ), p
                    )
                );
                var q = Expression.Parameter(typeof(int), "q");
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int,Func<int,int>>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            p,
                            Expression.Lambda<Func<int,int>>(
                                Expression.Block(
                                    p,
                                    Expression.Add(
                                        Expression.Add(q, q),
                                        Expression.Add(q, q)
                                    )
                                ),
                                q
                            )
                        ), p
                    )
                );
            }
            //    }
            //}
            //if(Expression0.Type!=typeof(void)){
            //    if(!this.既に置換された式を走査中){
            //        if(this.ExpressionEqualityComparer.Equals(Expression0,this.二度出現した一度目のExpression)){
            //            if(this.辺.この辺に二度出現存在するか_削除する(Expression0)){
            {
                var @true = Expression.Constant(true);
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Condition(
                            Expression.Equal(
                                Expression.Constant(1m),
                                Expression.Constant(1m)
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            //            }
            //_13
            //            if(this.辺.この辺に存在するか(Expression0)){
            {
                var @true = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            Expression.Call(
                                @true.Type.GetMethod("op_True")!,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            //            }
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Label(Label,Expression.Constant(true))
                        )
                    )
                );
            }
            //        }
            //_0
            //    }
            //_1
            //}
        }
        [Fact]
        public void Try(){
            var ex=Expression.Parameter(typeof(Exception),"ex");
            //for(var a=0;a<Try0_Handlers_Count;a++) {
            //    if(Try0_Handler.Filter!=Try1_Handler_Filter||Try0_Handler.Body!=Try1_Handler_Body){
            //        if(Try0_Handler_Variable is not null)
            //(0)
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryCatch(
                        Expression.Constant(0m),
                        Expression.Catch(
                            ex,
                            Expression.Add(
                                Expression.Constant(0m),
                                Expression.Constant(0m)
                            )
                        )
                    )
                )
            );
            //        else
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryCatch(
                        Expression.Constant(0m),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0m),
                            Expression.Equal(
                                Expression.Constant(0m),
                                Expression.Constant(0m)
                            )
                        )
                    )
                )
            );
            //    } else
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryCatch(
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        ),
                        Expression.Catch(
                            ex,
                            Expression.Default(typeof(decimal))
                        )
                    )
                )
            );
            //}
            //if(Try0.Fault is not null){
            //  if(Try0_Fault!=Try1_Fault)
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryFault(
                        Expression.Constant(0m),
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        )
                    )
                )
            );
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryFault(
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        ),
                        Expression.Constant(0m)
                    )
                )
            );
            //} else if(Try0.Finally is not null){
            //    if(Try0_Finally!=Try1_Finally)
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryFinally(
                        Expression.Constant(0m),
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        )
                    )
                )
            );
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.TryFinally(
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        ),
                        Expression.Constant(0m)
                    )
                )
            );
            //} else{
            //(0)
            //}
        }
        [Fact]
        public void Conditional()
        {
            //if(Test==Conditional1_Test)
            //    if(IfTrue==Conditional1_IfTrue)
            //        if(IfFalse==Conditional1_IfFalse)
            {
                var @true = Expression.Constant(true);
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Condition(
                            Expression.Equal(
                                Expression.Constant(1m),
                                Expression.Constant(1m)
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            //        else
            {
                var @true = Expression.Constant(true);
                var operator_true = Expression.Constant(new operator_true(true));
                var operator_false = Expression.Constant(new operator_true(false));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            @true,
                            operator_false,
                            Expression.Condition(
                                Expression.Call(
                                    operator_true.Type.GetMethod("op_True")!,
                                    operator_true
                                ),
                                operator_true,
                                operator_true
                            )
                        )
                    )
                );
            }
            //    else
            {
                var @true = Expression.Constant(true);
                var operator_true = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            @true,
                            Expression.Condition(
                                Expression.Call(
                                    operator_true.Type.GetMethod("op_True")!,
                                    operator_true
                                ),
                                operator_true,
                                operator_true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    operator_true.Type.GetMethod("op_True")!,
                                    operator_true
                                ),
                                operator_true,
                                operator_true
                            )
                        )
                    )
                );
            }
            //else
            //_0

            {
                var @true = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            Expression.Call(
                                @true.Type.GetMethod("op_True")!,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            {
                var Constant = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.AndAlso(
                            Constant,
                            Expression.New(typeof(operator_true))
                        )
                    )
                );
            }
        }
        [Fact]
        public void Switch()
        {
            //for(var a=0; a < Switch0_Cases_Count; a++) {
            //    if(Switch0_Case_Body !=Switch1_Case_Body) {
            {
                var p = Expression.Parameter(typeof(int), "p");
                var pp = Expression.Add(p, p);
                var e = Expression.Switch(
                    pp,
                    pp,
                    Expression.SwitchCase(pp, Expression.Constant(0))
                );
                this.Expression実行AssertEqual(Expression.Lambda<Func<int, int>>(e, p));
            }
            //    } else
            {
                var p = Expression.Parameter(typeof(int), "p");
                var pp = Expression.Add(p, p);
                var ppp = Expression.Add(pp, pp);
                var e = Expression.Switch(
                    pp,
                    pp,
                    Expression.SwitchCase(pp, Expression.Constant(0)),
                    Expression.SwitchCase(
                        Expression.Switch(
                            pp,
                            p,
                            Expression.SwitchCase(p, Expression.Constant(1)),
                            Expression.SwitchCase(p, Expression.Constant(2))
                        ), Expression.Constant(3)
                    )
                );
                this.Expression実行AssertEqual(Expression.Lambda<Func<int, int>>(e, p));
            }
            //}
            //if(!変化したか) return Switch0;
            {
                var p = Expression.Parameter(typeof(int), "p");
                var pp = Expression.Add(p, p);
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int,int>>(
                        Expression.Switch(
                            pp,
                            pp,
                            Expression.SwitchCase(pp, Expression.Constant(0)),
                            Expression.SwitchCase(
                                Expression.Block(
                                    pp,
                                    Expression.Switch(
                                        p,
                                        p,
                                        Expression.SwitchCase(p, Expression.Constant(1)),
                                        Expression.SwitchCase(p, Expression.Constant(2))
                                    )
                                ),
                                Expression.Constant(3)
                            )
                        ),
                        p
                    )
                );
            }
            //_0
        }
        [Fact]
        public void Call()
        {
            //var MethodCall0_Method = MethodCall0.Method;
            //if(this.IsInline&&ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall0_Method))) {
            //    switch(MethodCall0_Method.Name) {
            //        case nameof(Sets.ExtensionSet.Except): {
            //            if(MethodCall0.Arguments.Count==3) return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,this.Traverse(MethodCall0_Arguments[2]));
            {
                var set = new Set<int>();
                var _1m = 1m;
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Join(set, o => o, i => i, (o, i) => o+i) }
                );
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set, EqualityComparer<int>.Default) }
                );
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set) }
                );
            }
            //            else return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
            //        }
            //        default:
            //    }
            //}
        }
    }
    public class 判定_左辺Expressionが含まれる : 共通{
        //protected override テストオプション テストオプション=>テストオプション.式木の最適化を試行|テストオプション.ローカル実行|テストオプション.アセンブリ保存;
        [Fact]
        public void Traverse()
        {
            //todo 手動変数を巻き上げないように確認したい

            //switch(Expression0.NodeType){
            //    case ExpressionType.DebugInfo:
            //    case ExpressionType.Default:
            //    case ExpressionType.Lambda :
            //    case ExpressionType.PostDecrementAssign:
            //    case ExpressionType.PostIncrementAssign:
            //    case ExpressionType.PreDecrementAssign:
            //    case ExpressionType.PreIncrementAssign:
            //    case ExpressionType.Throw:
            {
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Throw(Expression.New(typeof(NotImplementedException)), typeof(bool))
                        )
                    )
                );
            }
            //    case ExpressionType.Label: {
            {
                var Label = Expression.Label(typeof(decimal));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<decimal>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Label(Label,Expression.Constant(1m))
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Goto:{
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Goto(Label,Expression.Constant(true)),
                            Expression.Label(Label,Expression.Constant(false))
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Loop: {
            {
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
            }
            //    }
            //    case ExpressionType.Assign: {
            {
                var p = Expression.Parameter(typeof(int), "p");
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int>>(
                        Expression.Block(
                            new[]{p},
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Assign(p, Expression.Constant(1))
                        )
                    )
                );
            }
            //    }
            //    case ExpressionType.Call: {
            //        if(Reflection.Helpers.NoEarlyEvaluation==GenericMethodDefinition)return Expression0;
            {
                var _1m = 1m;
                this.Expression実行AssertEqual(
                    () => _1m+_1m+_1m.NoEarlyEvaluation()
                );
            }
            //        if(ループ展開可能メソッドか(GenericMethodDefinition)) {
            //            switch(MethodCall0_Method.Name) {
            //                case nameof(Sets.ExtensionSet.Except): {
            //                    if(MethodCall0.Arguments.Count==3) return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,this.Traverse(MethodCall0_Arguments[2]));
            {
                var _1m = 1m;
                var set = new Set<int>();
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set, EqualityComparer<int>.Default) }
                );
            }
            //                    else return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
            {
                var _1m = 1m;
                var set = new Set<int>();
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Except(set) }
                );
            }
            //                }
            //                default:
            {
                var _1m = 1m;
                var set = new Set<int>();
                this.Expression実行AssertEqual(
                    () => new { a = _1m+_1m, b = set.Join(set, o => o, i => i, (o, i) => o+i) }
                );
            }
            //            }
            //        }
            {
                var _1m = 1m;
                this.Expression実行AssertEqual(
                    () => _1m+_1m+_1m.ToString(CultureInfo.InvariantCulture)
                );
            }
            //    }
            //    case ExpressionType.Constant: {
            //        if(ILで直接埋め込めるか((ConstantExpression)Expression0))return Expression0;
            {
                var _1 = 1;
                this.Expression実行AssertEqual(
                    () => _1+_1+1
                );
            }
            //    }
            //    case ExpressionType.Parameter: {
            //        if(!this.ラムダ跨ぎParameters.Contains(Expression0))return Expression0;
            {
                var p = Expression.Parameter(typeof(int), "p");
                var _1 = 1;
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<Func<int>>>(
                        Expression.Block(
                            new[]{p},
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Lambda<Func<int>>(p)
                        )
                    )
                );
                var q = Expression.Parameter(typeof(int), "q");
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<Func<int>>>(
                        Expression.Block(
                            new[]{p,q},
                            Expression.Assign(p, Expression.Constant(1)),
                            Expression.Assign(q, Expression.Constant(2)),
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            p,
                            Expression.Lambda<Func<int>>(
                                Expression.Block(
                                    p,
                                    Expression.Add(
                                        Expression.Add(q, q),
                                        Expression.Add(q, q)
                                    )
                                )
                            )
                        )
                    )
                );
            }
            //    }
            //}
            //if(Expression0.Type!=typeof(void)){
            //    if(!this.既に置換された式を走査中){
            //        if(this.ExpressionEqualityComparer.Equals(Expression0,this.二度出現した一度目のExpression)){
            //            if(this.辺.この辺に二度出現存在するか_削除する(Expression0)){
            {
                var @true = Expression.Constant(true);
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Condition(
                            Expression.Equal(
                                Expression.Constant(1m),
                                Expression.Constant(1m)
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                @true,
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            //            }
            //_13
            //            if(this.辺.この辺に存在するか(Expression0)){
            {
                var @true = Expression.Constant(new operator_true(true));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<operator_true>>(
                        Expression.Condition(
                            Expression.Call(
                                @true.Type.GetMethod("op_True")!,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            ),
                            Expression.Condition(
                                Expression.Call(
                                    @true.Type.GetMethod("op_True")!,
                                    @true
                                ),
                                @true,
                                @true
                            )
                        )
                    )
                );
            }
            //            }
            {
                var Label = Expression.Label(typeof(bool));
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool>>(
                        Expression.Block(
                            Expression.Constant(1m),
                            Expression.Constant(1m),
                            Expression.Label(Label,Expression.Constant(true))
                        )
                    )
                );
            }
            //        }
            //_0
            //    }
            //_1
            //}
        }
    }
    public class List辺 : 共通{
        //protected override テストオプション テストオプション=>テストオプション.式木の最適化を試行;
        [Fact]public void 特定パターン0(){
            var L0 = Expression.Label("L0");
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        Expression.Constant(1m),
                        Expression.Goto(L0),
                        Expression.Label(L0),
                        Expression.Constant(1m)
                    )
                )
            );
        }
        [Fact]public void 特定パターン1(){
            var L0 = Expression.Label("L0");
            var L1 = Expression.Label("L1");
            var L2 = Expression.Label("L2");
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        Expression.Goto(L1),
                        Expression.Label(L0),
                        Expression.Constant(1m),
                        Expression.Goto(L2),
                        Expression.Label(L1),
                        Expression.Constant(1m),
                        Expression.Goto(L0),
                        Expression.Label(L2),
                        Expression.Constant(1m)
                    )
                )
            );
        }
        [Fact]public void 親(){
            //for(var a=0;a<親辺Array_Length;a++){
            //    for(var b=0;b<Count;b++){
            //        if(列.移動元==親辺) {
            //            if(書き込みした右端列<b)
            {
                var r = Expression.Parameter(typeof(bool), "r");
                var p = Expression.Parameter(typeof(bool), "p");
                var p_And_p = Expression.And(p, p);
                var p_Or_p = Expression.Or(p, p);
                var ifFalse = Expression.Label("ifFalse");
                var endif = Expression.Label("endif");
                var Block = Expression.Block(
                    new[] { r },
                    Expression.IfThen(
                        Expression.Not(p_And_p),
                        Expression.Goto(ifFalse)
                    ),
                    Expression.Assign(r, p_Or_p),
                    Expression.Goto(endif),
                    Expression.Label(ifFalse),
                    Expression.Assign(r, p_And_p),
                    Expression.Label(endif),
                    r
                );
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<bool, bool>>(
                        Block,
                        p
                    )
                );
            }
            {
                var Label1 = Expression.Label(typeof(int), "Label1");
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int>>(
                        Expression.Block(
                            Expression.Goto(Label1, Expression.Constant(1)),
                            Expression.Label(Label1, Expression.Constant(11))
                        )
                    )
                );
            }
            //        }
            {
                var @int = Expression.Parameter(typeof(int), "int");
                var e = Expression.Switch(
                    @int,
                    @int,
                    //Expression.SwitchCase(
                    //    @int,
                    //    Expression.Constant(0)
                    //),
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
            //    }
            //    for(var b=0;b<Count;b++){
            //        if(Line[b]is'　'){
            {
                var L0 = Expression.Label("L0");
                var L1 = Expression.Label("L1");
                var L2 = Expression.Label("L2");
                var L3 = Expression.Label("L3");
                var L4 = Expression.Label("L4");
                var Block = Expression.Block(
                    Expression.Goto(L0),
                    Expression.Label(L1),
                    Expression.Constant(1m),
                    Expression.Goto(L2),
                    Expression.Label(L0),
                    Expression.Constant(1m),
                    Expression.Goto(L1),
                    Expression.Label(L3),
                    Expression.Constant(3m),
                    Expression.Goto(L4),
                    Expression.Label(L2),
                    Expression.Constant(2m),
                    Expression.Goto(L3),
                    Expression.Label(L4),
                    Expression.Constant(2m)
                );
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<decimal>>(
                        Block
                    )
                );
            }
            {
                var L0 = Expression.Label("L0");
                var L1 = Expression.Label("L1");
                var L2 = Expression.Label("L2");
                var Block = Expression.Block(
                    Expression.Goto(L0),
                    Expression.Label(L1),
                    Expression.Constant(1m),
                    Expression.Goto(L2),
                    Expression.Label(L0),
                    Expression.Constant(1m),
                    Expression.Goto(L1),
                    Expression.Label(L2),
                    Expression.Constant(1m)
                );
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<decimal>>(
                        Block
                    )
                );
            }
            //        }
            //    }
            //}
            //if(書き込みした右端列>0){
            //    for(var a=1;a<書き込みした右端列;a++){
            //        switch(Line[a]){
            //            case '　':
            //_0と同じ
            //            case '┐':
            {
                var p = Expression.Parameter(typeof(int), "p");
                var 先頭 = Expression.Label();
                var SwitchCases = new List<SwitchCase>();
                var TestValues = new List<Expression>();
                var Goto = Expression.Goto(先頭);
                var r = new Random(1);
                var case定数 = 0;
                for (var a = 0; a<10; a++)
                {
                    for (var b = r.Next(1, 10); b>=0; b--) TestValues.Add(Expression.Constant(case定数++));
                    var SwitchCase = Expression.SwitchCase(
                        Goto,
                        TestValues
                    );
                    SwitchCases.Add(SwitchCase);
                    TestValues.Clear();
                }
                var index = Expression.Parameter(typeof(int), "index");
                var endswitch = Expression.Label(typeof(int),"endswitch");
                var e = Expression.Block(
                    new[]{index},
                    Expression.Assign(index,Expression.Constant(100)),
                    Expression.Label(先頭),
                    Expression.IfThen(
                        Expression.GreaterThan(
                            Expression.Constant(0),
                            Expression.PostDecrementAssign(index)
                        ),
                        Expression.Goto(endswitch,p)
                    ),
                    Expression.Switch(
                        p, Goto, SwitchCases.ToArray()
                    ),
                    Expression.Label(endswitch,p)
                );
                this.Expression実行AssertEqual(
                    Expression.Lambda<Func<int, int>>(
                        e,
                        p
                    )
                );
            }
            //            case '┘':
            //_2と同じ
            //            case '│':
            //_2と同じ
            //        }
            //    }
            //    if(Line[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
            //_3
            //_3
            //}
        }
        [Fact]
        public void 子(){
            //var 子辺Array_Length=子辺Array.Length;
            //if(子辺Array_Length<=0)return;
            this.変換_局所Parameterの先行評価_実行(//0
                Expression.Loop(
                    Expression.Default(typeof(void))
                )
            );
            //for(var a=0;a<子辺Array_Length;a++){
            //    for(var b=0;b<Count;b++){
            //        if(列.移動元==辺) {
            //            if(列.移動先==子辺) {
            //                if(Line[b]=='│') {
            //                    if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
            //0
            //                }
            //            }
            //        }
            //0
            //    }
            //    for(var b=0;b<Count;b++){
            //        if(Line[b]=='　'){
            //            if(書き換えLineIndexEnd<b)
            this.変換_局所Parameterの先行評価_実行(//1
                Expression.Condition(
                    Expression.Condition(
                        Expression.Constant(false),
                        Expression.Constant(false),
                        Expression.Constant(false)
                    ),
                    Expression.Condition(
                        Expression.Constant(false),
                        Expression.Constant(false),
                        Expression.Constant(false)
                    ),
                    Expression.Condition(
                        Expression.Constant(false),
                        Expression.Constant(false),
                        Expression.Constant(false)
                    )
                )
            );
            //                書き換えLineIndexEnd=b;
            //        } 
            //0
            //    }
            //}
            //for(var a=0;a<書き換えLineIndexEnd;a++){
            //    switch(Line[a]){
            //        case '　':
            //0
            //        case '┘':
            //        case '│':
            //1
            //        case '┐':
            //1
            //    }
            //0
            //}
            //if(ループか)
            //0
            //0
            //if(子辺Array_Length<=0)return;
            //for(var a=0;a<子辺Array_Length;a++){
            //    for(var b=0;b<Count;b++){
            //        if(列.移動元==辺){
            //            if(列.移動先==子辺){
            //                if(Line[b]=='│'){
            //                    if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
            //                    Line[b]='┘';
            //                    ループか=true;
            //                    goto 終了;
            //                }
            //            }
            //        }
            //    }
            //    for(var b=0;b<Count;b++){
            //        if(Line[b]=='　'){
            //            if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
            //        } 
            //    }
            //}
            //for(var a=0;a<書き換えLineIndexEnd;a++){
            //    switch(Line[a]){
            //        case '　':
            //        case '┘':
            //        case '│':
            //        case '┐':
            //    }
            //}
            //if(ループか)
            //    列Array0[書き換えLineIndexEnd]=(null,null);
            var p = Expression.Parameter(typeof(decimal), "p");
            var pp=Expression.Add(p,p);
            var Break=Expression.Label("Break");
            var Block=Expression.Block(
                Expression.Loop(
                    Expression.Break(Break),
                    Break
                )
            );
            this.変換_局所Parameterの先行評価_実行(
                Expression.Loop(
                    Expression.Break(Break),
                    Break
                )
            );
        }
    }
    [Fact]
    public void Assign()
    {
        this.Expression実行AssertEqual((int p) => p*p+p*p);
    }
    [Fact]
    public void Block()
    {
        var p = Expression.Parameter(typeof(int), "p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(new[] { p }, p),
                p
            )
        );
    }
    [Fact]
    public void Lambda()
    {
        //while(true) {
        //    if(二度出現した一度目のExpression is null) break;
        this.Expression実行AssertEqual((int p) => p*p+p*p);
        //}
        //if(Block1_Variables.Count>0) Lambda2_Body=Expression.Block(Block1_Variables,this.作業配列.Expressions設定(Lambda2_Body));
        this.Expression実行AssertEqual((int p) => p*p+p*p);
        this.Expression実行AssertEqual((int p) => p);
    }
    [Fact]
    public void Conditional()
    {
        //if(Conditional0_Test==Conditional1_Test)
        //    if(Conditional0_IfTrue==Conditional1_IfTrue)
        //        if(Conditional0_IfFalse==Conditional1_IfFalse)
        this.Expression実行AssertEqual((int p) => p>0 ? p*p : p+p);
        this.Expression実行AssertEqual((int p) => p>0 ? p*p : p+p-(p+p));
        this.Expression実行AssertEqual((int p) => p>0 ? p+p-(p+p) : p*p);
        this.Expression実行AssertEqual((int p) => p+p-(p+p)>0 ? p : p*p);
        //Expression 共通(Expression Expression0){
        //    while(true) {
        //        if(二度出現した一度目のExpression is null)break;
        this.Expression実行AssertEqual((int p) => p>0 ? p*p+p*p : p*p-p*p);
        //    }
        //}
    }
    [Fact]
    public void 取得_Labelに対応するExpressions_Label()
    {
        var Label = Expression.Label(typeof(int), "Label1");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Label(Label, Expression.Constant(1))
            )
        );
    }
    [Fact]
    public void 取得_Labelに対応するExpressions_Loop()
    {
        var Continue = Expression.Label(typeof(void), "Continue");
        var Break = Expression.Label(typeof(int), "Break");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Loop(
                        Expression.Goto(Break, Expression.Constant(1)),
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
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break,
                    Continue
                )
            )
        );
        //else
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break
                )
            )
        );
        //if(Loop.BreakLabel is not null)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break
                )
            )
        );
        //else
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Loop(
                        Expression.Goto(Break, Expression.Constant(1))
                    ),
                    Expression.Label(Break, Expression.Constant(2))
                )
            )
        );
    }
    [Fact]
    public void 取得_Labelに対応するExpressions_Block()
    {
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
        var Break = Expression.Label(typeof(int), "Break");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break
                )
            )
        );
        //        case ExpressionType.Label:continue;
        //        case ExpressionType.Goto:this.ListExpression=null; break;
        //        default:this.ListExpression?.Add(Block_Expression); break;
        //    }
        //}
        var Continue = Expression.Label(typeof(void), "Continue");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Loop(
                        Expression.Goto(Break, Expression.Constant(1)),
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
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break,
                    Continue
                )
            )
        );
        //else
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break
                )
            )
        );
        //if(Loop.BreakLabel is not null)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Loop(
                    Expression.Goto(Break, Expression.Constant(1)),
                    Break
                )
            )
        );
        //else
    }
    private static readonly operator_true Op = new(true);
    // private static bool boolを返す関数()=>true;
    [Fact]
    public void AndAlso_OrElse()
    {
        //if(test.Type==typeof(bool)) {
        this.Expression実行AssertEqual(() => true&&ReferenceEquals(null, ""));
        //} else {
        var Constant = Expression.Constant(Op);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true>>(
                Expression.AndAlso(
                    Constant,
                    Expression.New(typeof(operator_true))
                )
            )
        );
        //}
    }
    [Fact]
    public void AndAlso(){
        //if(Binary1_Test.NodeType is ExpressionType.Assign){
        var Constant = Expression.Constant(Op);//定数が先行評価で変数にAssignされるため
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true>>(
                Expression.AndAlso(Constant, Constant)
            )
        );
        //}
        //if(Binary2_Left is ParameterExpression Parameter){
        var p = Expression.Parameter(typeof(operator_true));
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true,operator_true>>(
                Expression.Block(
                    new []{p},
                    Expression.Assign(p,Constant),
                    Expression.AndAlso(p,Constant)
                ),
                p
            )
        );
        //} else{
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true,operator_true>>(
                Expression.Block(
                    new []{p},
                    Expression.Assign(p,Constant),
                    Expression.AndAlso(Constant, p)
                ),
                p
            )
        );
        //}
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true,operator_true>>(
                Expression.Block(
                    Expression.Assign(p,Constant),
                    Expression.AndAlso(
                        p,
                        Expression.New(typeof(operator_true))
                    )
                ),
                p
            )
        );
        this.Expression実行AssertEqual(() =>
            Tuple.Create(Op, Op).Let(
                p =>
                    p.Item1&&p.Item2
            )
        );
    }
    [Fact]
    public void OrElse(){
        //if(Binary1_Test.NodeType is ExpressionType.Assign){
        var Constant = Expression.Constant(Op);//定数が先行評価で変数にAssignされるため
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true>>(
                Expression.OrElse(Constant, Constant)
            )
        );
        //}
        //if(Binary2_Left is ParameterExpression Parameter){
        var p = Expression.Parameter(typeof(operator_true));
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true,operator_true>>(
                Expression.Block(
                    new []{p},
                    Expression.Assign(p,Constant),
                    Expression.OrElse(p,Constant)
                ),
                p
            )
        );
        //} else{
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true,operator_true>>(
                Expression.Block(
                    new []{p},
                    Expression.Assign(p,Constant),
                    Expression.OrElse(Constant, p)
                ),
                p
            )
        );
        //}
        ////if(Binary1_Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.Or(Binary1_Left,Binary1_Right);
        //var Constant = Expression.Constant(Op);
        //this.Expression実行AssertEqual(
        //    Expression.Lambda<Func<operator_true>>(
        //        Expression.OrElse(Constant, Constant)
        //    )
        //);
        //var p = Expression.Parameter(typeof(特殊パターン.operator_true));
        //this.Expression実行AssertEqual(
        //    Expression.Lambda<Func<operator_true>>(
        //        Expression.OrElse(Constant, p),
        //        p
        //    )
        //);
        ////if(Binary1_Left.NodeType is ExpressionType.Constant or ExpressionType.Parameter){
        //this.Expression実行AssertEqual(
        //    Expression.Lambda<Func<operator_true>>(
        //        Expression.OrElse(
        //            Constant,
        //            Expression.New(typeof(特殊パターン.operator_true))
        //        ),
        //        p
        //    )
        //);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<operator_true,operator_true>>(
                Expression.Block(
                    new []{p},
                    Expression.Assign(p,Constant),
                    Expression.OrElse(
                        p,
                        Expression.New(typeof(operator_true))
                    )
                ),
                p
            )
        );
        this.Expression実行AssertEqual(() =>
            Tuple.Create(Op, Op).Let(
                p =>
                    p.Item1||p.Item2
            )
        );
    }
}
