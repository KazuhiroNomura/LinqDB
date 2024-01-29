using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Sets;
using LinqDB.Helpers;
using Math = LinqDB.Reflection.Math;
using Generic = System.Collections.Generic;
using LinqDB.Enumerables;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;

internal class 変換_インラインループ独立:変換_インラインループ {
    public 変換_インラインループ独立(作業配列 作業配列,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2):base(作業配列,変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2) {
    }
    private Expression Inline(MethodCallExpression MethodCall0){
        if(Reflection.ExtensionSet.Inline2==MethodCall0.Method.GetGenericMethodDefinition()) {
            var MethodCall0_Arguments = MethodCall0.Arguments;
            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
            var LambdaExpressionを展開1=this.LambdaExpressionを展開1(MethodCall1_Arguments_1,MethodCall1_Arguments_0);
            return MethodCall1_Arguments_0.NodeType==ExpressionType.Assign?Expression.Block(MethodCall1_Arguments_0,LambdaExpressionを展開1):LambdaExpressionを展開1;
            //if(MethodCall1_Arguments_0.NodeType==ExpressionType.Assign)
            //    //副作用があれば。ここでは代入だけにする。
            //    //a.Inline(b=>a+a)→a+a
            //    //(t=a).Inline(b=>a+a)→{t=a;a+a;}
            //    return Expression.Block(MethodCall1_Arguments_0,LambdaExpressionを展開1);
            //return LambdaExpressionを展開1;
        }
        Debug.Assert(Reflection.ExtensionSet.Inline1==MethodCall0.Method.GetGenericMethodDefinition());
        return MethodCall0.Arguments[0] is LambdaExpression MethodCall0_Lambda
            ?this.Traverse(MethodCall0_Lambda.Body)
            :base.Call(MethodCall0);
    }
    private Expression Aggregate(MethodCallExpression MethodCall0){
        var Method = MethodCall0.Method;
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var 作業配列 = this.作業配列;
        var MethodCall0_Arguments = MethodCall0.Arguments;
        if(MethodCall0_Arguments.Count==2) {
            Debug.Assert(Reflection.ExtensionSet.Aggregate_func==Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.Aggregate_func==Method.GetGenericMethodDefinition());
            var 要素なし = Expression.Parameter(typeof(bool),$"{変数名}要素なし");
            var Item = Expression.Parameter(MethodCall0.Type,$"{変数名}Item");
            var Expression1ループ = this.ループ展開(
                MethodCall0_Arguments[0],
                argument => Expression.IfThenElse(
                    要素なし,
                    Expression.Block(
                        Expression.Assign(要素なし,Constant_false),
                        Expression.Assign(Item,argument)
                    ),
                    Expression.Assign(
                        Item,
                        this.LambdaExpressionを展開2(
                            this.Traverse(MethodCall0_Arguments[1]),
                            Item,
                            argument
                        )
                    )
                )
            );
            var Throw = Throwシーケンスに要素が含まれていません(
                作業配列,
                Method.ReturnType,
                Method
            );
            return Expression.Block(
                作業配列.Parameters設定(
                    要素なし,
                    Item
                ),
                作業配列.Expressions設定(
                    Expression.Assign(
                        要素なし,
                        Constant_true
                    ),
                    Expression1ループ,
                    Expression.Condition(
                        要素なし,
                        Throw,
                        Item
                    )
                )
            );
        } else if(MethodCall0_Arguments.Count==3){
            Debug.Assert(Reflection.ExtensionSet.Aggregate_seed_func==Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.Aggregate_seed_func==Method.GetGenericMethodDefinition());
            var Item = Expression.Parameter(
                MethodCall0.Type,
                $"{変数名}seed"
            );
            var Expression0Assign = Expression.Assign(
                Item,
                this.Traverse(MethodCall0_Arguments[1])
            );
            var Expression1ループ = this.ループ展開(
                MethodCall0_Arguments[0],
                argument => Expression.Assign(
                    Item,
                    this.LambdaExpressionを展開2(
                        this.Traverse(MethodCall0_Arguments[2]),
                        Item,
                        argument
                    )
                )
            );
            return Expression.Block(
                作業配列.Parameters設定(Item),
                作業配列.Expressions設定(
                    Expression0Assign,
                    Expression1ループ,
                    Item
                )
            );
        } else{
            Debug.Assert(MethodCall0_Arguments.Count==4);
            Debug.Assert(Reflection.ExtensionSet.Aggregate_seed_func_resultSelector==Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.Aggregate_seed_func_resultSelector==Method.GetGenericMethodDefinition());
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
            var Item = Expression.Parameter(
                MethodCall1_Arguments_1.Type,
                $"{変数名}seed"
            );
            var Expression0 = Expression.Assign(
                Item,
                MethodCall1_Arguments_1
            );
            var Expression1ループ = this.ループ展開(
                MethodCall0_Arguments[0],
                argument => Expression.Assign(
                    Item,
                    this.LambdaExpressionを展開2(
                        this.Traverse(MethodCall0_Arguments[2]),
                        Item,
                        argument
                    )
                )
            );
            var Expression2 = this.LambdaExpressionを展開1(
                this.Traverse(MethodCall0_Arguments[3]),
                Item
            );
            return Expression.Block(
                作業配列.Parameters設定(Item),
                作業配列.Expressions設定(
                    Expression0,
                    Expression1ループ,
                    Expression2
                )
            );
        }
    }
    private Expression All(MethodCallExpression MethodCall0){
        var 変数名 = $"{MethodCall0.Method.Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var All_Break = Expression.Label(
            typeof(bool),
            変数名
        );
        return Expression.Block(
            this.ループ展開(
                MethodCall0_Arguments[0],
                argument => Expression.IfThenElse(
                    this.LambdaExpressionを展開1(
                        this.Traverse(MethodCall0_Arguments[1]),
                        argument
                    ),
                    Default_void,
                    Expression.Break(
                        All_Break,
                        Constant_false
                    )
                )
            ),
            Expression.Label(
                All_Break,
                Constant_true
            )
        );
    }
    private Expression Any(MethodCallExpression MethodCall0){
        var 変数名 = $"{MethodCall0.Method.Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
        if(ループ展開可能メソッドか(MethodCall0_Arguments_0,out var MethodCall0_MethodCall)) {
            var 作業配列 = this.作業配列;
            Debug.Assert(
                nameof(Enumerable.Join)!=MethodCall0_MethodCall.Method.Name&&
                Reflection.ExtensionSet.GroupBy_keySelector!=MethodCall0_MethodCall.Method.GetGenericMethodDefinition()
            );
            var MethodCall0_MethodCall_Arguments = MethodCall0_MethodCall.Arguments;
            switch(MethodCall0_MethodCall.Method.Name) {
                case nameof(Enumerable.Except): {
                    Debug.Assert(
                        Reflection.ExtensionSet.Except==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        Reflection.ExtensionEnumerable.Except==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()
                    );
                    var 変数名0 = 変数名+nameof(Enumerable.Except);
                    var second = MethodCall0_MethodCall_Arguments[1];
                    Type 作業_Type;
                    ParameterExpression 作業;
                    Expression Expression0;
                    if(MethodCall0_MethodCall.Method.DeclaringType==typeof(ExtensionSet)&&ループ展開可能なSetのCall(second) is null){
                        Debug.Assert(second.Type.IsGenericType&&!second.Type.IsInterface);
                        作業_Type=second.Type;
                        作業=Expression.Parameter(
                            作業_Type,
                            変数名0
                        );
                        Expression0=Expression.Assign(
                            作業,
                            this.Traverse(second)
                        );
                    } else {
                        作業_Type =typeof(Set<>).MakeGenericType(
                            IEnumerable1のGenericArguments(
                                MethodCall0_MethodCall.Type
                            )
                        );
                        作業=Expression.Parameter(
                            作業_Type,
                            変数名0
                        );
                        Expression0=Expression.Block(
                            Expression.Assign(
                                作業,
                                Expression.New(作業_Type)
                            ),
                            this.ループ展開(
                                second,
                                argument1 => Expression.Call(
                                    作業,
                                    作業_Type.GetMethod(nameof(Set<int>.IsAdded)),
                                    argument1
                                )
                            )
                        );
                    }
                    var Break = Expression.Label(
                        typeof(bool),
                        変数名0
                    );
                    var Expression1 = this.ループ展開(
                        MethodCall0_MethodCall_Arguments[0],
                        argument => Expression.IfThenElse(
                            Expression.Call(
                                作業,
                                作業_Type.GetMethod(nameof(ImmutableSet<int>.InternalContains),Instance_NonPublic_Public),
                                argument
                            ),
                            Default_void,
                            Expression.Break(
                                Break,
                                Constant_true
                            )
                        )
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(作業),
                        作業配列.Expressions設定(
                            Expression0,
                            Expression1,
                            Expression.Label(
                                Break,
                                Constant_false
                            )
                        )
                    );
                }
                case nameof(Enumerable.Intersect): {
                    var 変数名0 = 変数名+nameof(ExtensionSet.Intersect);
                    Type 作業_Type;
                    ParameterExpression 作業;
                    Expression Expression0;
                    var Break = Expression.Label(
                        typeof(bool),
                        変数名0
                    );
                    MethodInfo Contains;
                    if(MethodCall0_MethodCall_Arguments.Count==2) {
                        作業_Type=作業配列.MakeGenericType(
                            typeof(Set<>),
                            MethodCall0_MethodCall.Type.GetGenericArguments()[0]
                        );
                        Contains=作業_Type.GetMethod(nameof(ImmutableSet<int>.InternalContains),Instance_NonPublic_Public)!;
                        作業=Expression.Parameter(
                            作業_Type,
                            $"{変数名}作業"
                        );
                        var first = MethodCall0_MethodCall_Arguments[0];
                        if(ループ展開可能なSetのCall(first) is not null) {
                            Expression0=Expression.Assign(
                                作業,
                                this.Traverse(first)
                            );
                        } else {
                            Expression0=Expression.Block(
                                Expression.Assign(
                                    作業,
                                    Expression.New(作業_Type)
                                ),
                                this.ループ展開(
                                    first,
                                    argument => Expression.Call(
                                        作業,
                                        作業_Type.GetMethod(nameof(Generic.ICollection<int>.Add),Instance_NonPublic_Public),
                                        argument
                                    )
                                )
                            );
                        }
                    } else {
                        var T = IEnumerable1のT(MethodCall0_MethodCall.Type);
                        作業_Type=作業配列.MakeGenericType(
                            typeof(Generic.HashSet<>),
                            T
                        );
                        Contains=作業_Type.GetMethod(nameof(Generic.HashSet<int>.Contains))!;
                        作業=Expression.Parameter(
                            作業_Type,
                            $"{変数名}作業"
                        );
                        var Constructor = 作業配列.GetConstructor(
                            作業_Type,
                            作業配列.MakeGenericType(
                                typeof(Generic.IEqualityComparer<>),
                                T
                            )
                        );
                        Expression0=Expression.Block(
                            Expression.Assign(
                                作業,
                                Expression.New(
                                    Constructor,
                                    this.Traverse(MethodCall0_MethodCall_Arguments[2])
                                )
                            ),
                            this.ループ展開(
                                MethodCall0_MethodCall_Arguments[0],
                                argument => Expression.Call(
                                    作業,
                                    作業_Type.GetMethod(nameof(Generic.HashSet<int>.Add),Instance_NonPublic_Public),
                                    argument
                                )
                            )
                        );
                    }
                    var Expression1 = this.ループ展開(
                        MethodCall0_MethodCall_Arguments[1],
                        argument => Expression.IfThenElse(
                            Expression.Call(
                                作業,
                                Contains,
                                argument
                            ),
                            Expression.Break(
                                Break,
                                Constant_true
                            ),
                            Default_void
                        )
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(作業),
                        作業配列.Expressions設定(
                            Expression0,
                            Expression1,
                            Expression.Label(
                                Break,
                                Constant_false
                            )
                        )
                    );
                }
                case nameof(Enumerable.OfType): {
                    Debug.Assert(
                        Reflection.ExtensionSet.OfType==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        Reflection.ExtensionEnumerable.OfType==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()
                    );
                    var MethodCall0_MethodCall_Arguments_0 = MethodCall0_MethodCall.Arguments[0];
                    var 変換元Type = IEnumerable1のT(MethodCall0_MethodCall_Arguments_0.Type);
                    var 変換先Type = IEnumerable1のT(MethodCall0_MethodCall.Type);
                    Debug.Assert(変換元Type!=変換先Type);
                    var Break = Expression.Label(
                        typeof(bool),
                        変数名
                    );
                    var Break_true = Expression.Break(
                        Break,
                        Constant_true
                    );
                    var Label_false = Expression.Label(
                        Break,
                        Constant_false
                    );
                    //int.IsAssignableTo(object)は必ず成功する
                    if(変換元Type.IsAssignableTo(変換先Type)) return Constant_true;
                    return Expression.Block(
                        this.ループ展開(
                            MethodCall0_MethodCall_Arguments_0,
                            argument => Expression.IfThenElse(
                                Expression.TypeIs(
                                    argument,
                                    変換先Type
                                ),
                                Break_true,
                                Default_void
                            )
                        ),
                        Label_false
                    );
                }
                case nameof(Enumerable.SelectMany): {
                    Debug.Assert(
                        Reflection.ExtensionSet.SelectMany_selector==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        Reflection.ExtensionEnumerable.SelectMany_selector==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        Reflection.ExtensionEnumerable.SelectMany_indexSelector==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()
                    );
                    var Break = Expression.Label(
                        typeof(bool),
                        変数名
                    );
                    Debug.Assert(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector!=MethodCall0_MethodCall.Method.GetGenericMethodDefinition());
                    return Expression.Block(
                        this.ループ展開(
                            MethodCall0_MethodCall_Arguments[0],
                            argument1 => this.ループ展開(
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_MethodCall_Arguments[1]),
                                    argument1
                                ),
                                _ => Expression.Break(
                                    Break,
                                    Constant_true
                                )
                            )
                        ),
                        Expression.Label(
                            Break,
                            Constant_false
                        )
                    );
                }
                case nameof(Enumerable.Union): {
                    Debug.Assert(
                        Reflection.ExtensionSet.Union==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        Reflection.ExtensionEnumerable.Union==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        Reflection.ExtensionEnumerable.Union_comparer==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()
                    );
                    var Break = Expression.Label(
                        typeof(bool),
                        変数名
                    );
                    return Expression.Block(
                        this.ループ展開(
                            MethodCall0_MethodCall_Arguments[0],
                            _ => Expression.Break(
                                Break,
                                Constant_true
                            )
                        ),
                        this.ループ展開(
                            MethodCall0_MethodCall_Arguments[1],
                            _ => Expression.Break(
                                Break,
                                Constant_true
                            )
                        ),
                        Expression.Label(
                            Break,
                            Constant_false
                        )
                    );
                }
                default:{
                    Debug.Assert(nameof(Enumerable.Where)==MethodCall0_MethodCall.Method.Name);
                    Debug.Assert(
                        Reflection.ExtensionSet.Where==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        //Reflection.ExtensionSet.Where_Set2==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()||
                        Reflection.ExtensionEnumerable.Where==MethodCall0_MethodCall.Method.GetGenericMethodDefinition()
                    );
                    var Break = Expression.Label(
                        typeof(bool),
                        変数名
                    );
                    return Expression.Block(
                        this.ループ展開(
                            MethodCall0_MethodCall_Arguments[0],
                            argument => Expression.IfThenElse(
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_MethodCall_Arguments[1]),
                                    argument
                                ),
                                Expression.Break(
                                    Break,
                                    Constant_true
                                ),
                                Default_void
                            )
                        ),
                        Expression.Label(
                            Break,
                            Constant_false
                        )
                    );
                }
            }
        }
        var Any_Break = Expression.Label(typeof(bool),変数名);
        return Expression.Block(
            this.ループ展開(
                MethodCall0_Arguments_0,
                _ => Expression.Break(Any_Break,Constant_true)
            ),
            Expression.Label(Any_Break,Constant_false)
        );
    }
    private Expression Average(MethodCallExpression MethodCall0){
        var Method=MethodCall0.Method;
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall0_Type = MethodCall0.Type;
        var ListParameter = new List<ParameterExpression>();
        var ListExpression = new List<Expression>();
        var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
        var Average_Int64Count = Expression.Parameter(
            typeof(long),
            $"{変数名}Int64Count"
        );
        ListParameter.Add(Average_Int64Count);
        ListExpression.Add(
            Expression.Assign(
                Average_Int64Count,
                Constant_0L
            )
        );
        var GenericMethodDefinition=GetGenericMethodDefinition(Method);
        Type Sum_Type;
        if     (Reflection.ExtensionSet       .AverageInt32_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageInt64_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageSingle_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageDouble_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableInt32_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableInt64_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableSingle_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableDouble_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageInt32                  ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageInt64                  ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageSingle                 ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageDouble                 ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableInt32          ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableInt64          ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableSingle         ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionSet       .AverageNullableDouble         ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageInt32_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageInt64_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageSingle_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageDouble_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageNullableInt32_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageNullableInt64_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageNullableSingle_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        else if(Reflection.ExtensionEnumerable.AverageNullableDouble_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        else{
            Debug.Assert(
                Reflection.ExtensionSet.AverageDecimal_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageDouble_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageSingle_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageInt64_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageInt32_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableDecimal_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableDouble_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableSingle_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableInt64_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableInt32_selector==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageDecimal==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageDouble==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageSingle==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageInt64==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageInt32==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableDecimal==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableDouble==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableSingle==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableInt64==GenericMethodDefinition||
                Reflection.ExtensionSet.AverageNullableInt32==GenericMethodDefinition||
                Reflection.ExtensionEnumerable.AverageDecimal_selector==GenericMethodDefinition||
                Reflection.ExtensionEnumerable.AverageNullableDecimal_selector==GenericMethodDefinition||
                //Reflection.ExtensionEnumerable.AverageDecimal_selector==GenericMethodDefinition||
                //Reflection.ExtensionEnumerable.AverageNullableDecimal_selector==GenericMethodDefinition||
                false
            );
            Sum_Type=typeof(decimal);
        }
        var Sum = Expression.Parameter(
            Sum_Type,
            $"{変数名}Sum"
        );
        ListParameter.Add(Sum);
        ListExpression.Add(
            Expression.Assign(
                Sum,
                Expression.Default(Sum_Type)
            )
        );
        ListExpression.Add(
            this.ループ展開(
                MethodCall0_Arguments_0,
                argument => {
                    if(MethodCall0_Arguments.Count==1)return 共通(MethodCall0_Type,argument,Sum,Average_Int64Count);
                    Debug.Assert(MethodCall0_Arguments.Count==2);
                    var MethodCall0_Arguments_1=MethodCall0_Arguments[1];
                    var selector=Expression.Parameter(
                        MethodCall0_Arguments_1.Type.GetMethod(nameof(Action.Invoke))!.ReturnType,
                        $"{変数名}source"
                    );
                    var Assign=Expression.Assign(
                        selector,
                        this.LambdaExpressionを展開1(
                            this.Traverse(MethodCall0_Arguments_1),
                            argument
                        )
                    );
                    var 作業配列=this.作業配列;
                    var Parameters0=作業配列.Parameters設定(selector);
                    var Expressions0=作業配列.Expressions設定(
                        Assign,
                        共通(MethodCall0_Type,selector,Sum,Average_Int64Count)
                    );
                    return Expression.Block(
                        Parameters0,
                        Expressions0
                    );
                    static Expression 共通(Type MethodCall0_Type,Expression 共通argument,ParameterExpression Sum,ParameterExpression Average_Int64Count){
                        if(MethodCall0_Type.IsNullable()) {
	                        return Expression.IfThenElse(
	                            Expression.Property(
	                                共通argument,
	                                nameof(Nullable<int>.HasValue)
	                            ),
	                            Block_PreIncrementAssign_AddAssign(
	                                Average_Int64Count,
	                                Sum,
	                                Convert必要なら(
	                                    Expression.Property(
	                                        共通argument,
	                                        nameof(Nullable<int>.Value)
	                                    ),
	                                    Sum.Type
	                                )
	                            ),
	                            Default_void
	                        );
	                    }
                        return Block_PreIncrementAssign_AddAssign(
                            Average_Int64Count,
                            Sum,
                            Convert必要なら(
                                共通argument,
                                Sum.Type
                            )
                        );
                    }
                }
            )
        );
        Expression 行数0の処理;
        if(MethodCall0_Type.IsNullable())行数0の処理=Expression.Default(MethodCall0_Type);
        else 行数0の処理=Throw_ZeroTuple(MethodCall0.Method);
        ListExpression.Add(
            Expression.Condition(
                Expression.Equal(
                    Average_Int64Count,
                    Constant_0L
                ),
                行数0の処理,
                Convert必要なら(
                    Expression.Divide(
                        Sum,
                        Convert必要なら(
                            Average_Int64Count,
                            Sum_Type
                        )
                    ),
                    MethodCall0_Type
                )
            )
        );
        return Expression.Block(
            ListParameter,
            ListExpression
        );
    }
    private Expression Avedev(MethodCallExpression MethodCall0){
        var Method=MethodCall0.Method;
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
        var Int64Count = Expression.Parameter(
            typeof(long),
            $"{変数名}Int64Count"
        );
        var 作業配列=this.作業配列;
        var FILO_Enumerator_Type = 作業配列.MakeGenericType(typeof(ImmutableSet<>.FILO),typeof(double));
        var FILO_Enumerator = Expression.Parameter(
            FILO_Enumerator_Type,
            $"{変数名}FILO_Enumerator"
        );
        var DoubleCount = Expression.Parameter(
            typeof(double),
            $"{変数名}DoubleCount"
        );
        var Sum = Expression.Parameter(
            typeof(double),
            $"{変数名}Sum"
        );
        var selector = Expression.Parameter(
            typeof(double),
            $"{変数名}selector"
        );
        var Expressionループ = this.ループ展開(
            MethodCall0_Arguments[0],
            argument => Expression.Block(
                Expression.Assign(
                    selector,
                    this.LambdaExpressionを展開1(
                        MethodCall1_Arguments_1,
                        argument
                    )
                ),
                Expression.Call(
                    FILO_Enumerator,
                    FILO_Enumerator_Type.GetMethod(nameof(ImmutableSet<int>.FILO.Add)),
                    selector
                ),
                AddAssign(
                    Sum,
                    selector
                )
            )
        );
        var Average = Expression.Parameter(
            typeof(double),
            $"{変数名}Average"
        );
        var Subtract = Expression.Parameter(
            typeof(double),
            $"{変数名}Subtract"
        );
        var Loop終了 = Expression.Label($"{変数名}Loop終了");
        var Expression合計を初期化 = Expression.Assign(
            Sum,
            Constant_0D
        );
        return Expression.Block(
            作業配列.Parameters設定(
                Int64Count,
                FILO_Enumerator,
                Sum,
                selector,
                DoubleCount,
                Average,
                Subtract
            ),
            作業配列.Expressions設定(
                Call(
                    FILO_Enumerator,
                    nameof(ImmutableSet<int>.FILO.Constructor)
                ),
                Expression合計を初期化,
                Expressionループ,
                Expression.Assign(
                    Int64Count,
                    Field(
                        FILO_Enumerator,
                        nameof(ImmutableSet<int>.FILO.Count)
                    )
                ),
                Expression.IfThenElse(
                    Expression.Equal(
                        Int64Count,
                        Constant_0L
                    ),
                    Throw_ZeroTuple(Method),
                    Default_void
                ),
                Expression.Assign(
                    DoubleCount,
                    Expression.Convert(
                        Int64Count,
                        typeof(double)
                    )
                ),
                Expression.Assign(
                    Average,
                    Expression.Divide(
                        Sum,
                        DoubleCount
                    )
                ),
                Expression合計を初期化,
                Expression.Loop(
                    Expression.IfThenElse(
                        Call(
                            FILO_Enumerator,
                            nameof(System.Collections.IEnumerator.MoveNext)
                        ),
                        Expression.Block(
                            Expression.Assign(
                                Subtract,
                                Expression.Subtract(
                                    Expression.Property(
                                        FILO_Enumerator,
                                        nameof(System.Collections.IEnumerator.Current)
                                    ),
                                    Average
                                )
                            ),
                            Expression.IfThenElse(
                                Expression.LessThan(
                                    Subtract,
                                    Constant_0D
                                ),
                                Expression.Assign(
                                    Subtract,
                                    Expression.Negate(Subtract)
                                ),
                                Default_void
                            ),
                            AddAssign(
                                Sum,
                                Subtract
                            )
                        ),
                        Expression.Break(Loop終了)
                    ),
                    Loop終了
                ),
                Expression.Divide(
                    Sum,
                    DoubleCount
                )
            )
        );
}
    private Expression AsEnumerable(MethodCallExpression MethodCall0){
        Debug.Assert(Reflection.ExtensionEnumerable.AsEnumerable==MethodCall0.Method.GetGenericMethodDefinition());
        Debug.Assert(MethodCall0.Arguments.Count==1);
        return Expression.Call(
            MethodCall0.Method,
            this.Traverse(MethodCall0.Arguments[0])
        );
    }
    private Expression ToLookup(MethodCallExpression MethodCall0){
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var Method=MethodCall0.Method;
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var GenericMethodDefinition=Method.GetGenericMethodDefinition();
        var Dictionary_Type=typeof(ExtensionSet)==Method.DeclaringType?typeof(SetGroupingSet<,>):typeof(Enumerables.Lookup<,>);
        Dictionary_Type=Dictionary_Type.MakeGenericType(MethodCall0.Type.GetGenericArguments());
        Expression? comparer;
        if(Reflection.ExtensionSet.ToLookup_keySelector_comparer==GenericMethodDefinition||Reflection.ExtensionEnumerable.ToLookup_keySelector_comparer==GenericMethodDefinition) {
            comparer=this.Traverse(MethodCall0_Arguments[2]);
        } else if(Reflection.ExtensionSet.ToLookup_keySelector_elementSelector_comparer==GenericMethodDefinition||Reflection.ExtensionEnumerable.ToLookup_keySelector_elementSelector_comparer==GenericMethodDefinition) {
            comparer=this.Traverse(MethodCall0_Arguments[3]);
        } else {
            comparer=null;
        }
        var 作業配列 = this.作業配列;
        NewExpression New;
        if(comparer is null)New=Expression.New(Dictionary_Type);
        else New=Expression.New(
            作業配列.GetConstructor(Dictionary_Type,comparer.Type),
            comparer
        );
        var Parameters=new List<ParameterExpression>();
        var Dictionary = Expression.Parameter(Dictionary_Type,変数名);
        Parameters.Add(Dictionary);
        ParameterExpression?index=null;
        var Expression1ループ = this.ループ展開(
            MethodCall0_Arguments[0],
            argument => {
                var MethodCall1_Arguments_1=this.Traverse(MethodCall0_Arguments[1]);
                //if(argument.NodeType==ExpressionType.Parameter)return 共通(argument);
                var 作業配列1=this.作業配列;
                Expression? keySelector0=null;
                Expression? elementSelector0=null;
                //Expression? comparer0=null;
                //IReadOnlyList<ParameterExpression> Parameters0;
                ParameterExpression p;
                ParameterExpression? index=null;
                if(MethodCall1_Arguments_1 is LambdaExpression keySelector){
                    var Parameters0=keySelector.Parameters;
                    Parameters.AddRange(Parameters0);
                    p=Parameters0[0];
                    //Expression argument1=(Expression)p;
                    //var keySelector1=LambdaExpressionを展開1(
                    //    MethodCall1_Arguments_1,
                    //    argument1,
                    //    this.変換_旧Parameterを新Expression1
                    //);
                    keySelector0=this.変換_旧Parameterを新Expression1.実行(keySelector.Body,p,argument);
                    if(Parameters0.Count==1){
                        if(Reflection.ExtensionSet.ToLookup_keySelector_elementSelector==GenericMethodDefinition)
                            elementSelector0=this.LambdaExpressionを展開1(this.Traverse(MethodCall0_Arguments[2]),p);
                        else if(Reflection.ExtensionEnumerable.ToLookup_keySelector_elementSelector==GenericMethodDefinition)
                            elementSelector0=this.LambdaExpressionを展開1(this.Traverse(MethodCall0_Arguments[2]),p);
                        else if(Reflection.ExtensionSet.ToLookup_keySelector_elementSelector_comparer==GenericMethodDefinition)
                            elementSelector0=this.LambdaExpressionを展開1(this.Traverse(MethodCall0_Arguments[2]),p);
                        else if(Reflection.ExtensionEnumerable.ToLookup_keySelector_elementSelector_comparer==GenericMethodDefinition)
                            elementSelector0=this.LambdaExpressionを展開1(this.Traverse(MethodCall0_Arguments[2]),p);
                    } else{
                        if(Reflection.ExtensionEnumerable.ToLookup_indexKeySelector==GenericMethodDefinition)
                            index=Parameters0[1];
                        else{
                            Debug.Assert(Reflection.ExtensionEnumerable.ToLookup_indexKeySelector_comparer==GenericMethodDefinition);
                            index=Parameters0[1];
                        }
                    }
                } else{
                    p=Expression.Parameter(argument.Type,$"{変数名}argument");
                    index=Expression.Parameter(typeof(int),$"{変数名}index");
                    Parameters.Add(p);
                    Parameters.Add(index);
                    Debug.Assert(
                        Reflection.ExtensionSet.ToLookup_keySelector_elementSelector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.ToLookup_keySelector_elementSelector==GenericMethodDefinition||
                        Reflection.ExtensionSet.ToLookup_keySelector_elementSelector_comparer==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.ToLookup_keySelector_elementSelector_comparer==GenericMethodDefinition
                    );
                    elementSelector0=this.LambdaExpressionを展開1(this.Traverse(MethodCall0_Arguments[2]),p);
                    keySelector0=Expression.Invoke(MethodCall1_Arguments_1,argument);
                }
                Expression elementSelector;
                if(elementSelector0 is null)
                    elementSelector=p;
                else
                    elementSelector=elementSelector0;
                var Expression0=Expression.Assign(p,argument);
                var Expression1=Expression.Call(
                    Dictionary,
                    Dictionary_Type.GetMethod(nameof(SetGroupingSet<int,int>.AddKeyValue)),
                    keySelector0,
                    elementSelector
                );
                if(index is null)
                    return Expression.Block(
                        Expression0,
                        Expression1
                    );
                else
                    return Expression.Block(
                        Expression0,
                        Expression1,
                        Expression.Assign(
                            index,
                            Expression.Increment(
                                index
                            )
                        )
                    );
            }
        );
        var Assign=Expression.Assign(Dictionary,New);
        if(index is null)
            return Expression.Block(
                Parameters,
                作業配列.Expressions設定(
                    Assign,
                    Expression1ループ,
                    Dictionary
                )
            );
        else
            return Expression.Block(
                Parameters,
                作業配列.Expressions設定(
                    Assign,
                    Expression.Assign(index,Constant_0),
                    Expression1ループ,
                    Dictionary
                )
            );
    }
    private Expression Count(MethodCallExpression MethodCall0){
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var Method=MethodCall0.Method;
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
        if(ループ展開可能メソッドか(MethodCall0_Arguments_0,out _)){
            var 作業配列=this.作業配列;
            ParameterExpression Parameter;
            Expression Expression0,Expression1,Expression2;
            if(this.重複除去されているか(MethodCall0_Arguments_0)){
                var Parameter_Type=Method.ReturnType;
                Parameter=Expression.Parameter(
                    Parameter_Type,
                    変数名
                );
                Expression0=Expression.Assign(
                    Parameter,
                    Expression.Default(Parameter_Type)
                );
                Expression1=this.ループ展開(
                    MethodCall0_Arguments_0,
                    _=>IncrementAssign(Parameter)
                );
                Expression2=Parameter;
            } else{
                //Set(10).Select(p=>p/2).Count()==5
                (Parameter,var Add,Expression0)=具象SetType戻り値なしCountあり(MethodCall0_Arguments_0,変数名);//具象Type(MethodCall0_Arguments_0,変数名,false,true);
                Expression1=this.ループ展開(
                    MethodCall0_Arguments_0,
                    argument=>Expression.Call(
                        Parameter,
                        Add,
                        argument
                    )
                );
                Expression2=Expression.Call(Method,Parameter);
            }
            return Expression.Block(
                作業配列.Parameters設定(Parameter),
                作業配列.Expressions設定(
                    Expression0,
                    Expression1,
                    Expression2
                )
            );
        }
        Debug.Assert(Method==MethodCall0.Method);
        return Expression.Call(
            Method,
            this.Traverse(MethodCall0_Arguments_0)
        );
    }
    private Expression Harmean(MethodCallExpression MethodCall0){
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var Method=MethodCall0.Method;
        var GenericMethodDefinition = Method.GetGenericMethodDefinition();
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        Expression Constant_0, Constant_1;
        if(Reflection.ExtensionSet.HarmeanNullableDecimal_selector==GenericMethodDefinition||Reflection.ExtensionSet.HarmeanDecimal_selector==GenericMethodDefinition) {
            Constant_0=Constant_0M;
            Constant_1=Constant_1M;
        } else {
            Debug.Assert(Reflection.ExtensionSet.HarmeanNullableDouble_selector==GenericMethodDefinition||Reflection.ExtensionSet.HarmeanDouble_selector==GenericMethodDefinition);
            Constant_0=Constant_0D;
            Constant_1=Constant_1D;
        }
        var ListParameter = new List<ParameterExpression>();
        var ListExpression = new List<Expression>();
        if(MethodCall0.Type.IsNullable()) {
            var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
            var SelecterNullable_Type = MethodCall0.Type;
            Debug.Assert(SelecterNullable_Type==MethodCall1_Arguments_1.Type.GetMethod(nameof(Action.Invoke))!.ReturnType);
            Debug.Assert(Constant_0.Type==SelecterNullable_Type.GetGenericArguments()[0]);
            var SelecterNullable = Expression.Parameter(
                SelecterNullable_Type,
                $"{変数名}SelecterNullable"
            );
            ListParameter.Add(SelecterNullable);
            var Int64Count = Expression.Parameter(
                typeof(long),
                $"{変数名}Int64Count"
            );
            ListParameter.Add(Int64Count);
            ListExpression.Add(
                Expression.Assign(
                    Int64Count,
                    Constant_0L
                )
            );
            var Sum_Type = Constant_0.Type;
            var Sum = Expression.Parameter(
                Sum_Type,
                $"{変数名}Sum"
            );
            ListParameter.Add(Sum);
            ListExpression.Add(
                Expression.Assign(
                    Sum,
                    Constant_0
                )
            );
            BlockExpression ループ内部処理Block(Expression argument) => Expression.Block(
                Expression.Assign(
                    SelecterNullable,
                    this.LambdaExpressionを展開1(
                        MethodCall1_Arguments_1,
                        argument
                    )
                ),
                Expression.IfThenElse(
                    Expression.Property(
                        SelecterNullable,
                        nameof(Nullable<int>.HasValue)
                    ),
                    Block_PreIncrementAssign_AddAssign(
                        Int64Count,
                        Sum,
                        Expression.Divide(
                            Constant_1,
                            Expression.Property(
                                SelecterNullable,
                                nameof(Nullable<int>.Value)
                            )
                        )
                    ),
                    Default_void
                )
            );
            var MethodCall = ループ展開可能なSetのCall(MethodCall0_Arguments_0);
            if(MethodCall is not null) {
                if(this.重複除去されているか(MethodCall)) {
                    逆数の合計();
                } else {
                    //Setメソッドで結果が重複除去されていない
                    var (Parameter,IsAdded,Assign)=具象SetType戻り値ありCountなし(MethodCall0_Arguments_0,$"{変数名}作業");//具象Type(MethodCall0_Arguments_0,$"{変数名}作業",true,false);
                    ListParameter.Add(Parameter);
                    ListExpression.Add(Assign);
                    //逆数の合計
                    ListExpression.Add(
                        this.ループ展開(
                            MethodCall0_Arguments_0,
                            argument => Expression.IfThenElse(
                                Expression.Call(
                                    Parameter,
                                    IsAdded,
                                    argument
                                ),
                                ループ内部処理Block(argument),
                                Default_void
                            )
                        )
                    );
                }
            } else {
                逆数の合計();
            }
            ListExpression.Add(
                Expression.Condition(
                    Expression.Equal(
                        Int64Count,
                        Constant_0L
                    ),
                    Expression.Default(SelecterNullable_Type),
                    Expression.Convert(
                        Expression.Divide(
                            Convert必要なら(
                                Int64Count,
                                Sum_Type
                            ),
                            Sum
                        ),
                        SelecterNullable_Type
                    )
                )
            );
            void 逆数の合計(){
                ListExpression.Add(
                    this.ループ展開(
                        MethodCall0_Arguments_0,
                        ループ内部処理Block
                    )
                );
            }
        } else {
            var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
            var Item_Type = MethodCall1_Arguments_1.Type.GetMethod(nameof(Action.Invoke))!.ReturnType;
            Debug.Assert(MethodCall0.Method.ReturnType==Item_Type);
            var Item = Expression.Parameter(
                Item_Type,
                $"{変数名}Item"
            );
            ListParameter.Add(Item);
            var MethodCall = ループ展開可能なSetのCall(MethodCall0_Arguments_0);
            if(MethodCall is not null) {
                if(this.重複除去されているか(MethodCall)){
                    //逆数の合計
                    var Int64Count = Expression.Parameter(
                        typeof(long),
                        $"{変数名}Int64Count"
                    );
                    ListParameter.Add(Int64Count);
                    ListExpression.Add(
                        Expression.Assign(
                            Int64Count,
                            Constant_0L
                        )
                    );
                    ListExpression.Add(
                        Expression.Assign(
                            Item,
                            Expression.Default(Item_Type)
                        )
                    );
                    ListExpression.Add(
                        this.ループ展開(
                            MethodCall0_Arguments_0,
                            argument => Block_PreIncrementAssign_AddAssign(
                                Int64Count,
                                Item,
                                Expression.Divide(
                                    Constant_1,
                                    this.LambdaExpressionを展開1(
                                        MethodCall1_Arguments_1,
                                        argument
                                    )
                                )
                            )
                        )
                    );
                    ListExpression.Add(
                        Expression.Divide(
                            Convert必要なら(
                                Int64Count,
                                Item_Type
                            ),
                            Item
                        )
                    );
                    return Expression.Block(ListParameter,ListExpression);
                }
            }
            var (Parameter,IsAdded,Assign)=具象SetType戻り値ありCountあり(MethodCall0_Arguments_0,$"{変数名}作業");//具象Type(MethodCall0_Arguments_0,$"{変数名}作業",true,true);
            ListParameter.Add(Parameter);
            ListExpression.Add(Assign);
            ListExpression.Add(
                Expression.Assign(
                    Item,
                    Constant_0
                )
            );
            //逆数の合計
            ListExpression.Add(
                this.ループ展開(
                    MethodCall0_Arguments_0,
                    argument => Expression.IfThenElse(
                        Expression.Call(
                            Parameter,
                            IsAdded,
                            argument
                        ),
                        AddAssign(
                            Item,
                            Expression.Divide(
                                Constant_1,
                                this.LambdaExpressionを展開1(
                                    MethodCall1_Arguments_1,
                                    argument
                                )
                            )
                        ),
                        Default_void
                    )
                )
            );
            ListExpression.Add(
                //逆数の平均の逆数
                Expression.Divide(
                    Convert必要なら(
                        Expression.Property(
                            Parameter,
                            Parameter.Type.GetProperty(nameof(Set<int>.LongCount))
                        ),
                        Item_Type
                    ),
                    Item
                )
            );
        }
        return Expression.Block(ListParameter,ListExpression);
    }
    private Expression Geomean(MethodCallExpression MethodCall0){
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var Method=MethodCall0.Method;
        var GenericMethodDefinition = Method.GetGenericMethodDefinition();
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var ListParameter = new List<ParameterExpression>();
        var ListExpression = new List<Expression>();
        if(Reflection.ExtensionSet.GeomeanDouble_selector==GenericMethodDefinition) {
            var Item = Expression.Parameter(
                typeof(double),
                $"{変数名}Item"
            );
            ListParameter.Add(Item);
            var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
            // ReSharper disable once RedundantAssignment
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
            if(this.重複除去されているか(MethodCall0_Arguments_0)){
                var Geomean_Int64Count=Expression.Parameter(
                    typeof(long),
                    $"{変数名}Int64Count"
                );
                ListParameter.Add(Geomean_Int64Count);
                ListExpression.Add(
                    Expression.Assign(
                        Geomean_Int64Count,
                        Constant_0L
                    )
                );
                ListExpression.Add(
                    Expression.Assign(
                        Item,
                        Constant_0D
                    )
                );
                ListExpression.Add(
                    this.ループ展開(
                        MethodCall0_Arguments_0,
                        argument=>Block_PreIncrementAssign_AddAssign(
                            Geomean_Int64Count,
                            Item,
                            Expression.Call(
                                Math.Log10,
                                this.LambdaExpressionを展開1(
                                    MethodCall1_Arguments_1,
                                    argument
                                )
                            )
                        )
                    )
                );
                ListExpression.Add(
                    Expression.Call(
                        Math.Pow,
                        Constant_10D,
                        Expression.Divide(
                            Item,
                            Expression.Convert(
                                Geomean_Int64Count,
                                typeof(double)
                            )
                        )
                    )
                );
            } else{
                var (Parameter,IsAdded,Assign)=具象SetType戻り値ありCountあり(MethodCall0_Arguments_0,$"{変数名}作業");//具象Type(MethodCall0_Arguments_0,$"{変数名}作業",true,true);
                ListParameter.Add(Parameter);
                ListExpression.Add(Assign);
                ListExpression.Add(
                    Expression.Assign(
                        Item,
                        Constant_0D
                    )
                );
                ListExpression.Add(
                    this.ループ展開(
                        MethodCall0_Arguments_0,
                        argument=>Expression.IfThenElse(
                            Expression.Call(
                                Parameter,
                                IsAdded,
                                argument
                            ),
                            AddAssign(
                                Item,
                                Expression.Call(
                                    Math.Log10,
                                    this.LambdaExpressionを展開1(
                                        MethodCall1_Arguments_1,
                                        argument
                                    )
                                )
                            ),
                            Default_void
                        )
                    )
                );
                ListExpression.Add(
                    Expression.Call(
                        Math.Pow,
                        Constant_10D,
                        Expression.Divide(
                            Item,
                            Expression.Convert(
                                Expression.Property(
                                    Parameter,
                                    Parameter.Type.GetProperty(nameof(Set<int>.LongCount))
                                ),
                                typeof(double)
                            )
                        )
                    )
                );
            }
        } else {
            Debug.Assert(Reflection.ExtensionSet.GeomeanNullableDouble_selector==GenericMethodDefinition);
            var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
            var Sum_Type = typeof(double);
            var Sum = Expression.Parameter(
                Sum_Type,
                $"{変数名}Sum"
            );
            ListParameter.Add(Sum);
            var selector_Type = typeof(double?);
            var selector = Expression.Parameter(
                selector_Type,
                $"{変数名}selector"
            );
            ListParameter.Add(selector);
            var Geomean_Int64Count = Expression.Parameter(
                typeof(long),
                $"{変数名}Int64Count"
            );
            ListParameter.Add(Geomean_Int64Count);
            ListExpression.Add(
                Expression.Assign(
                    Geomean_Int64Count,
                    Constant_0L
                )
            );
            ListExpression.Add(
                Expression.Assign(
                    Sum,
                    Constant_0D
                )
            );
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
            if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
                //重複を許す。例えば{0,1,2,2,2,5}というシーケンスを許すEnumerable
                //逆数の合計
                ListExpression.Add(
                    this.ループ展開(
                        MethodCall0_Arguments_0,
                        argument => Expression.Block(
                            Expression.Assign(
                                selector,
                                this.LambdaExpressionを展開1(
                                    MethodCall1_Arguments_1,
                                    argument
                                )
                            ),
                            Expression.IfThenElse(
                                Expression.Property(
                                    selector,
                                    nameof(Nullable<int>.HasValue)
                                ),
                                Block_PreIncrementAssign_AddAssign(
                                    Geomean_Int64Count,
                                    Sum,
                                    Expression.Call(
                                        Math.Log10,
                                        Expression.Property(
                                            selector,
                                            nameof(Nullable<int>.Value)
                                        )
                                    )
                                ),
                                Default_void
                            )
                        )
                    )
                );
            } else {
                var MethodCall = ループ展開可能なSetのCall(MethodCall0_Arguments_0);
                if(MethodCall is not null) {
                    if(this.重複除去されているか(MethodCall)) {
                        ListExpression.Add(
                            this.ループ展開(
                                MethodCall0_Arguments_0,
                                argument => Expression.Block(
                                    Expression.Assign(
                                        selector,
                                        this.LambdaExpressionを展開1(
                                            MethodCall1_Arguments_1,
                                            argument
                                        )
                                    ),
                                    Expression.IfThenElse(
                                        Expression.Property(
                                            selector,
                                            nameof(Nullable<int>.HasValue)
                                        ),
                                        Block_PreIncrementAssign_AddAssign(
                                            Geomean_Int64Count,
                                            Sum,
                                            Expression.Call(
                                                Math.Log10,
                                                Expression.Property(
                                                    selector,
                                                    nameof(Nullable<int>.Value)
                                                )
                                            )
                                        ),
                                        Default_void
                                    )
                                )
                            )
                        );
                    } else {
                        //MethodCall0_Arguments_0が重複除去されていない。重複除去すべき。
                        var (Parameter,IsAdded,Assign)=具象SetType戻り値ありCountなし(MethodCall0_Arguments_0,$"{変数名}作業");//具象Type(MethodCall0_Arguments_0,$"{変数名}作業",true,false);
                        ListParameter.Add(Parameter);
                        ListExpression.Add(Assign);
                        ListExpression.Add(
                            this.ループ展開(
                                MethodCall0_Arguments_0,
                                argument => Expression.IfThenElse(
                                    Expression.Call(Parameter,IsAdded,argument),
                                    Expression.Block(
                                        Expression.Assign(
                                            selector,
                                            this.LambdaExpressionを展開1(
                                                MethodCall1_Arguments_1,
                                                argument
                                            )
                                        ),
                                        Expression.IfThenElse(
                                            Expression.Property(
                                                selector,
                                                nameof(Nullable<int>.HasValue)
                                            ),
                                            Block_PreIncrementAssign_AddAssign(
                                                Geomean_Int64Count,
                                                Sum,
                                                Expression.Call(
                                                    Math.Log10,
                                                    Expression.Property(
                                                        selector,
                                                        nameof(Nullable<int>.Value)
                                                    )
                                                )
                                            ),
                                            Default_void
                                        )
                                    ),
                                    Default_void
                                )
                            )
                        );
                    }
                } else {
                    var Result = Expression.Parameter(
                        MethodCall0_Arguments_0.Type,
                        $"{変数名}Result"
                    );
                    ListParameter.Add(Result);
                    ListExpression.Add(
                        Expression.Assign(
                            Result,
                            this.Traverse(MethodCall0_Arguments_0)
                        )
                    );
                    //逆数の合計
                    ListExpression.Add(
                        this.ループ展開(
                            Result,
                            argument => Expression.Block(
                                Expression.Assign(
                                    selector,
                                    this.LambdaExpressionを展開1(
                                        MethodCall1_Arguments_1,
                                        argument
                                    )
                                ),
                                Expression.IfThenElse(
                                    Expression.Property(
                                        selector,
                                        nameof(Nullable<int>.HasValue)
                                    ),
                                    Block_PreIncrementAssign_AddAssign(
                                        Geomean_Int64Count,
                                        Sum,
                                        Expression.Call(
                                            Math.Log10,
                                            Expression.Property(
                                                selector,
                                                nameof(Nullable<int>.Value)
                                            )
                                        )
                                    ),
                                    Default_void
                                )
                            )
                        )
                    );
                }
            }
            ListExpression.Add(
                Expression.Condition(
                    Expression.Equal(
                        Geomean_Int64Count,
                        Constant_0L
                    ),
                    Expression.New(selector_Type),
                    Expression.Convert(
                        Expression.Call(
                            Math.Pow,
                            Constant_10D,
                            Expression.Divide(
                                Sum,
                                Expression.Convert(
                                    Geomean_Int64Count,
                                    Sum_Type
                                )
                            )
                        ),
                        selector_Type
                    )
                )
            );
        }
        return Expression.Block(
            ListParameter,
            ListExpression
        );
    }
    /// <summary>
    /// Max,Min
    /// </summary>
    /// <param name="MethodCall0"></param>
    /// <param name="NodeType">MaxならLessThan、MinならGreaterThan</param>
    /// <returns></returns>
    private Expression MaxMin(MethodCallExpression MethodCall0,ExpressionType NodeType){
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var Method=MethodCall0.Method;
        //var GenericMethodDefinition = Method.GetGenericMethodDefinition();
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Type = MethodCall0.Type;
        var 要素あり = Expression.Parameter(typeof(bool),$"{変数名}シーケンスに要素が含まれているか");
        var ListParameter = new List<ParameterExpression> { 要素あり };
        var ListExpression = new List<Expression>{Expression.Assign(要素あり,Constant_false)};
        Type ElementType;
        MethodInfo? GetValueOrDefault;
        if(MethodCall0_Type.IsNullable()) {
            GetValueOrDefault=MethodCall0_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!;
            ElementType=GetValueOrDefault.ReturnType;
        } else {
            GetValueOrDefault=null;
            ElementType=MethodCall0_Type;
        }
        //ParameterExpression? Parameter_Comparer_Default;
        //if(Compare_Defaultを使うべきか(NodeType,ElementType)) {
        //    var Parameter_Comparer_Default_Type = this.作業配列.MakeGenericType(
        //        typeof(Comparer<>),
        //        ElementType
        //    );
        //    Parameter_Comparer_Default=Expression.Parameter(
        //        Parameter_Comparer_Default_Type,
        //        $"{変数名}Comparer<{ElementType.Name}>"
        //    );
        //    ListParameter.Add(Parameter_Comparer_Default);
        //    ListExpression.Add(
        //        Expression.Assign(
        //            Parameter_Comparer_Default,
        //            Expression.Property(
        //                null,
        //                Parameter_Comparer_Default_Type.GetProperty(nameof(Comparer<int>.Default))
        //            )
        //        )
        //    );
        //} else {
        //    Parameter_Comparer_Default=null;
        //}
        var Parameter_Value = Expression.Parameter(ElementType,$"{変数名}Value");
        ListParameter.Add(Parameter_Value);
        var Parameter_MaxMinValue = Expression.Parameter(ElementType,$"{変数名}Result");
        ListParameter.Add(Parameter_MaxMinValue);
        Expression? 終了処理ifTrue = null;
        Expression? 終了処理ifFalse = null;
        ListExpression.Add(
            this.ループ展開(
                MethodCall0_Arguments[0],
                argument => {
                    var ListParameter0 = new List<ParameterExpression>();
                    var ListExpression0 = new List<Expression>();
                    Expression Element;
                    if(MethodCall0_Arguments.Count==1)
                        Element=argument;
                    else
                        Element=this.LambdaExpressionを展開1(
                            this.Traverse(MethodCall0_Arguments[1]),
                            argument
                        );
                    Debug.Assert(MethodCall0_Type==Element.Type);
                    Expression Right;
                    if(GetValueOrDefault is not null) {
                        Debug.Assert(Element.NodeType!=ExpressionType.Parameter);
                        var Parameter_Nullable = Expression.Parameter(
                            Element.Type,
                            $"{変数名}Nullable"
                        );
                        ListParameter0.Add(Parameter_Nullable);
                        ListExpression0.Add(
                            Expression.Assign(Parameter_Nullable,Element)
                        );
                        ListExpression0.Add(
                            Expression.Assign(
                                Parameter_Value,
                                Expression.Call(
                                    Parameter_Nullable,
                                    GetValueOrDefault
                                )
                            )
                        );
                        Right=Parameter_Value;
                        終了処理ifTrue=Expression.New(
                            this.作業配列.GetConstructor(
                                MethodCall0.Type,
                                ElementType
                            ),
                            Parameter_MaxMinValue
                        );
                        終了処理ifFalse=Expression.Default(MethodCall0_Type);
                    } else {
                        Right=Element;
                        終了処理ifTrue=Parameter_MaxMinValue;
                        終了処理ifFalse=Expression.Throw(
                            Expression.New(
                                InvalidOperationException_ctor,
                                this.作業配列.Expressions設定(
                                    Expression.Constant(Method.ToString())
                                )
                            ),
                            ElementType
                        );
                    }
                    var Current = Right;
                    //if(Parameter_Comparer_Default is not null) {
                    //    Left=Expression.Call(
                    //        Parameter_Comparer_Default,
                    //        Parameter_Comparer_Default.Type.GetMethod(nameof(Comparer<int>.Compare))!,
                    //        Parameter_MaxMinValue,
                    //        Expression.Assign(Parameter_Value,Right)
                    //    );
                    //    Right=Constant_0;
                    //} else {
                    //    Left=Parameter_MaxMinValue;
                    //}
                    var Left=Parameter_MaxMinValue;
                    ListExpression0.Add(
                        Expression.IfThenElse(
                            要素あり,
                            Expression.IfThenElse(
                                Expression.MakeBinary(
                                    NodeType,
                                    Left,
                                    Right
                                ),
                                Expression.Assign(Parameter_MaxMinValue,Current),
                                Default_void
                            ),
                            Expression.Block(
                                Expression.Assign(要素あり,Constant_true),
                                Expression.Assign(Parameter_MaxMinValue,Current)
                            )
                        )
                    );
                    return Expression.Block(
                        ListParameter0,
                        ListExpression0
                    );
                }
            )
        );
        Debug.Assert(終了処理ifTrue is not null&&終了処理ifFalse is not null);
        ListExpression.Add(
            Expression.Condition(要素あり,終了処理ifTrue,終了処理ifFalse)
        );
        return Expression.Block(
            ListParameter,
            ListExpression
        );
    }
    private Expression Stdev(MethodCallExpression MethodCall0){
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var Method=MethodCall0.Method;
        var GenericMethodDefinition = GetGenericMethodDefinition(Method);
        var 変数名 = $"{Method.Name}ﾟ{this.番号++}ﾟ";
        var ListParameter = new List<ParameterExpression>();
        var ListExpression = new List<Expression>();
        var Stdev_Int64Count = Expression.Parameter(
            typeof(long),
            $"{変数名}Int64Count"
        );
        ListParameter.Add(Stdev_Int64Count);
        var DoubleCount = Expression.Parameter(
            typeof(double),
            $"{変数名}DoubleCount"
        );
        ListParameter.Add(DoubleCount);
        var DoubleSum = Expression.Parameter(
            typeof(double),
            $"{変数名}DoubleSum"
        );
        ListParameter.Add(DoubleSum);
        var Average = Expression.Parameter(
            typeof(double),
            $"{変数名}Average"
        );
        ListParameter.Add(Average);
        var Subtract = Expression.Parameter(
            typeof(double),
            $"{変数名}Subtract"
        );
        ListParameter.Add(Subtract);
        var DoubleSumを初期化 = Expression.Assign(
            DoubleSum,
            Constant_0D
        );
        ListExpression.Add(DoubleSumを初期化);
        var FILO_Enumerator_Type = this.作業配列.MakeGenericType(typeof(ImmutableSet<>.FILO),typeof(double));
        var FILO_Enumerator = Expression.Parameter(
            FILO_Enumerator_Type,
            $"{変数名}FILO_Enumerator"
        );
        ListParameter.Add(FILO_Enumerator);
        ListExpression.Add(
            Call(
                FILO_Enumerator,
                nameof(ImmutableSet<int>.FILO.Constructor)
            )
        );
        if(MethodCall0_Arguments.Count==1) {
            Debug.Assert(Reflection.ExtensionSet.StdevDouble==GenericMethodDefinition);
            ListExpression.Add(
                this.ループ展開(
                    MethodCall0_Arguments[0],
                    argument => Expression.Block(
                        Expression.Call(
                            FILO_Enumerator,
                            FILO_Enumerator_Type.GetMethod(nameof(ImmutableSet<int>.FILO.Add)),
                            argument
                        ),
                        AddAssign(
                            DoubleSum,
                            argument
                        )
                    )
                )
            );
        } else {
            Debug.Assert(Reflection.ExtensionSet.StdevDouble_selector==GenericMethodDefinition);
            var 値 = Expression.Parameter(typeof(double),$"{変数名}値");
            ListParameter.Add(値);
            ListExpression.Add(
                this.ループ展開(
                    MethodCall0_Arguments[0],
                    argument => Expression.Block(
                        Expression.Assign(
                            値,
                            this.LambdaExpressionを展開1(
                                this.Traverse(MethodCall0_Arguments[1]),
                                argument
                            )
                        ),
                        Expression.Call(
                            FILO_Enumerator,
                            FILO_Enumerator_Type.GetMethod(nameof(ImmutableSet<int>.FILO.Add))!,
                            値
                        ),
                        AddAssign(DoubleSum,値)
                    )
                )
            );
        }
        ListExpression.Add(
            Expression.Assign(
                Stdev_Int64Count,
                Field(
                    FILO_Enumerator,
                    nameof(ImmutableSet<int>.FILO.Count)
                )
            )
        );
        ListExpression.Add(
            Expression.IfThenElse(
                Expression.Equal(
                    Stdev_Int64Count,
                    Constant_0L
                ),
                Throw_ZeroTuple_Stdev,
                Default_void
            )
        );
        ListExpression.Add(
            Expression.Assign(
                DoubleCount,
                Expression.Convert(
                    Stdev_Int64Count,
                    typeof(double)
                )
            )
        );
        ListExpression.Add(
            Expression.Assign(
                Average,
                Expression.Divide(DoubleSum,DoubleCount)
            )
        );
        ListExpression.Add(DoubleSumを初期化);
        var Loop終了 = Expression.Label($"{変数名}Loop終了");
        ListExpression.Add(
            Expression.Loop(
                Expression.IfThenElse(
                    Call(
                        FILO_Enumerator,
                        nameof(System.Collections.IEnumerator.MoveNext)
                    ),
                    Expression.Block(
                        Expression.Assign(
                            Subtract,
                            Expression.Subtract(
                                Expression.Property(
                                    FILO_Enumerator,
                                    nameof(System.Collections.IEnumerator.Current)
                                ),
                                Average
                            )
                        ),
                        AddAssign(
                            DoubleSum,
                            Expression.Multiply(Subtract,Subtract)
                        )
                    ),
                    Expression.Break(Loop終了)
                ),
                Loop終了
            )
        );
        ListExpression.Add(
            Expression.Call(
                Math.Sqrt,
                Expression.Divide(
                    DoubleSum,
                    Expression.Decrement(DoubleCount)
                )
            )
        );
        return Expression.Block(
            ListParameter,
            ListExpression
        );
    }
    private Expression VarVarp(MethodCallExpression MethodCall0){
        var Method=MethodCall0.Method;
        var Name=Method.Name;
        var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments = MethodCall0.Arguments;
        //var GenericMethodDefinition = GetGenericMethodDefinition(Method);
        var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
        var ReturnType = MethodCall0.Type;
        var Int64Count = Expression.Parameter(
            typeof(long),
            $"{変数名}Int64Count"
        );
        var 作業配列=this.作業配列;
        var FILO_Enumerator_Type = 作業配列.MakeGenericType(typeof(ImmutableSet<>.FILO),ReturnType);
        var FILO_Enumerator = Expression.Parameter(
            FILO_Enumerator_Type,
            $"{変数名}FILO_Enumerator"
        );
        var TCount = Expression.Parameter(
            ReturnType,
            $"{変数名}TCount"
        );
        var Sum = Expression.Parameter(
            ReturnType,
            $"{変数名}Sum"
        );
        var selector値 = Expression.Parameter(
            ReturnType,
            $"{変数名}selector値"
        );
        var Expressionループ = this.ループ展開(
            MethodCall0_Arguments[0],
            argument => Expression.Block(
                Expression.Assign(
                    selector値,
                    this.LambdaExpressionを展開1(
                        MethodCall1_Arguments_1,
                        argument
                    )
                ),
                Expression.Call(
                    FILO_Enumerator,
                    FILO_Enumerator_Type.GetMethod(nameof(ImmutableSet<int>.FILO.Add)),
                    selector値
                ),
                AddAssign(
                    Sum,
                    selector値
                )
            )
        );
        var Subtract = Expression.Parameter(
            ReturnType,
            $"{変数名}Subtract"
        );
        var Average = Expression.Parameter(
            ReturnType,
            $"{変数名}Average"
        );
        var Loop終了 = Expression.Label($"{変数名}Loop終了");
        var ExpressionSumを初期化 = Expression.Assign(
            Sum,
            Expression.Default(ReturnType)
        );
        var (Throw_ZeroTuple, 割る数)=nameof(ExtensionSet.Varp)==Name
            ? (Throw_ZeroTuple_Varp, (Expression)TCount)
            : (Throw_ZeroTuple_Var, Expression.Decrement(TCount));
        return Expression.Block(
            作業配列.Parameters設定(
                Int64Count,
                FILO_Enumerator,
                Sum,
                selector値,
                TCount,
                Average,
                Subtract
            ),
            作業配列.Expressions設定(
                Call(
                    FILO_Enumerator,
                    nameof(ImmutableSet<int>.FILO.Constructor)
                ),
                ExpressionSumを初期化,
                Expressionループ,
                Expression.Assign(
                    Int64Count,
                    Field(
                        FILO_Enumerator,
                        nameof(ImmutableSet<int>.FILO.Count)
                    )
                ),
                Expression.IfThenElse(
                    Expression.Equal(
                        Int64Count,
                        Constant_0L
                    ),
                    Throw_ZeroTuple,
                    Default_void
                ),
                Expression.Assign(
                    TCount,
                    Expression.Convert(
                        Int64Count,
                        ReturnType
                    )
                ),
                Expression.Assign(
                    Average,
                    Expression.Divide(
                        Sum,
                        TCount
                    )
                ),
                ExpressionSumを初期化,
                Expression.Loop(
                    Expression.IfThenElse(
                        Call(
                            FILO_Enumerator,
                            nameof(System.Collections.IEnumerator.MoveNext)
                        ),
                        Expression.Block(
                            Expression.Assign(
                                Subtract,
                                Expression.Subtract(
                                    Expression.Property(
                                        FILO_Enumerator,
                                        nameof(System.Collections.IEnumerator.Current)
                                    ),
                                    Average
                                )
                            ),
                            AddAssign(
                                Sum,
                                Expression.Multiply(
                                    Subtract,
                                    Subtract
                                )
                            )
                        ),
                        Expression.Break(Loop終了)
                    ),
                    Loop終了
                ),
                Expression.Divide(
                    Sum,
                    割る数
                )
            )
        );
    }
    private Expression SequenceEqual(MethodCallExpression MethodCall0){
        var Method=MethodCall0.Method;
        var Name=Method.Name;
        var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments = MethodCall0.Arguments;
        //var GenericMethodDefinition = GetGenericMethodDefinition(Method);
        var first = MethodCall0_Arguments[0];
        var T = IEnumerable1のT(first.Type);
        var 作業配列=this.作業配列;
        var FIFO_Enumerator_Type = 作業配列.MakeGenericType(
            typeof(ImmutableSet<>.FIFO),
            T
        );
        var Constructor=FIFO_Enumerator_Type.GetMethod(nameof(ImmutableSet<int>.FIFO.Constructor),Instance_NonPublic_Public);
        Debug.Assert(Constructor is not null);
        var Add=FIFO_Enumerator_Type.GetMethod(nameof(ImmutableSet<int>.FIFO.Add));
        Debug.Assert(Add is not null);
        var FIFO_Enumerator1 = Expression.Parameter(
            FIFO_Enumerator_Type,
            $"{変数名}first"
        );
        var FIFO_Enumerator2 = Expression.Parameter(
            FIFO_Enumerator_Type,
            $"{変数名}second"
        );
        var Expression2=this.ループ展開(
            first,
            argument => Expression.Call(
                FIFO_Enumerator1,
                Add,
                argument
            )
        );
        var Expression3 = this.ループ展開(
            MethodCall0_Arguments[1],
            argument => Expression.Call(
                FIFO_Enumerator2,
                Add,
                argument
            )
        );
        return Expression.Block(
            作業配列.Parameters設定(
                FIFO_Enumerator1,
                FIFO_Enumerator2
            ),
            作業配列.Expressions設定(
                Expression.Call(
                    FIFO_Enumerator1,
                    Constructor
                ),
                Expression.Call(
                    FIFO_Enumerator2,
                    Constructor
                ),
                Expression2,
                Expression3,
                Expression.Call(
                    FIFO_Enumerator1,
                    FIFO_Enumerator_Type.GetMethod(nameof(ImmutableSet<int>.FIFO.SequenceEqual))!,
                    FIFO_Enumerator2
                )
            )
        );
    }
    private Expression Single(MethodCallExpression MethodCall0){
        var Method=MethodCall0.Method;
        var Name=Method.Name;
        var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall0_Method = MethodCall0.Method;
        var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
        var ElementType = MethodCall0_Method.ReturnType;
        var Item0 = Expression.Parameter(ElementType,$"{変数名}Item0");
        var Item1 = Expression.Parameter(ElementType,$"{変数名}Item1");
        var 要素なし = Expression.Parameter(typeof(bool),$"{変数名}要素なし");
        var Expressions0 = Expression.Assign(要素なし,Constant_true);
        var Expressions1 = this.ループ展開(
            MethodCall0_Arguments_0,
            argument => {
                var 作業配列0=this.作業配列;
                Expression 要素があった時のExpression = Expression.Throw(
                    Expression.New(
                        InvalidOperationException_ctor,
                        作業配列0.Expressions設定(
                            Expression.Constant($"{MethodCall0_Method}:{CommonLibrary.シーケンスに複数の要素が含まれています_MoreThanOneElement}")
                        )
                    )
                );
                //var argument0 = argument;
                if(!this.重複除去されているか(MethodCall0_Arguments_0)) {
                    MethodCallExpression EqualExpression;
                    var IEquatableType = 作業配列0.MakeGenericType(typeof(IEquatable<>),ElementType);
                    if(IEquatableType.IsAssignableFrom(ElementType)) {
                        var InterfaceMap = ElementType.GetInterfaceMap(IEquatableType);
                        Debug.Assert(InterfaceMap.InterfaceMethods[0]==IEquatableType.GetMethod(nameof(IEquatable<int>.Equals)));
                        EqualExpression=Expression.Call(
                            Item0,
                            InterfaceMap.TargetMethods[0],
                            作業配列0.Expressions設定(Item1)
                        );
                    } else {
                        Expression argument1 = Item1;
                        if(ElementType.IsValueType) {
                            //if(/*nameof(ExtendSet.SingleOrNull)==Name&&*/Item_Type.IsNullable()&&!argument.Type.IsNullable()) {
                            if(ElementType.IsNullable())
                                argument=Expression.Convert(argument,ElementType);
                            argument1=Expression.Convert(argument1,typeof(object));
                        }
                        EqualExpression=Expression.Call(
                            Item0,
                            Reflection.Object.Equals_,
                            作業配列0.Expressions設定(argument1)
                        );
                    }
                    要素があった時のExpression=Expression.IfThenElse(
                        EqualExpression,
                        Default_void,
                        要素があった時のExpression
                    );
                }
                return Expression.IfThenElse(
                    要素なし,
                    Expression.Block(
                        typeof(void),
                        作業配列0.Expressions設定(
                            Expression.Assign(Item1,argument),
                            Expression.Assign(要素なし,Constant_false),
                            Expression.Assign(Item0,Item1)
                        )
                    ),
                    要素があった時のExpression
                );
            }
        );
        Expression 要素なしifTrue;
        if(nameof(ExtensionSet.SingleOrDefault)==Name)
            if(MethodCall0_Arguments.Count==1)
                要素なしifTrue=Expression.Default(ElementType);
            else
                要素なしifTrue=this.Traverse(MethodCall0_Arguments[1]);
        else
            要素なしifTrue=Throwシーケンスに要素が含まれていません(
                this.作業配列,
                ElementType,
                Method
            );
        //var 要素なしifTrue=nameof(ExtensionSet.SingleOrDefault)==Name
        //    ?MethodCall0_Arguments.Count==1
        //        ?Expression.Default(ElementType)
        //        :this.Traverse(MethodCall0_Arguments[1])
        //    :Expression.Throw(
        //        New_ZeroTupleException,
        //        ElementType
        //    );
        //var Throw = Throwシーケンスに要素が含まれていません(
        //    this.作業配列,
        //    ElementType,
        //    Method
        //);
        var 作業配列=this.作業配列;
        return Expression.Block(
            作業配列.Parameters設定(要素なし,Item0,Item1),
            作業配列.Expressions設定(
                Expressions0,
                Expressions1,
                Expression.Condition(
                    要素なし,
                    要素なしifTrue,
                    Item0
                )
            )
        );
    }
    private Expression Sum(MethodCallExpression MethodCall0){
        var Method=MethodCall0.Method;
        var Name=Method.Name;
        var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
        var GenericMethodDefinition=GetGenericMethodDefinition(Method);
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var ListParameter = new List<ParameterExpression>();
        var ListExpression = new List<Expression>();
        var MethodCall0_Type = MethodCall0.Type;
        var Nullableか = MethodCall0_Type.IsNullable();
        var Sum_Type=Nullableか?MethodCall0_Type.GetGenericArguments()[0]:MethodCall0.Type;
        var Sum = Expression.Parameter(
            Sum_Type,
            $"{変数名}Item"
        );
        ListParameter.Add(Sum);
        ListExpression.Add(
            Expression.Assign(
                Sum,
                Expression.Default(Sum_Type)
            )
        );
        var 作業配列=this.作業配列;
        if(Nullableか){
            var Nullable = Expression.Parameter(
                MethodCall0_Type,
                $"{変数名}Nullable"
            );
            if(MethodCall0_Arguments.Count==1) {
                Debug.Assert(
                    MethodCall0_Arguments.Count==1&&(
                        Reflection.ExtensionSet.SumNullableInt32==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableInt64==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableDecimal==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableDouble==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableSingle==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableInt32==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableInt64==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableDecimal==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableDouble==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableSingle==GenericMethodDefinition
                    )
                );
                ListExpression.Add(
                    this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => Expression.Block(
                            作業配列.Parameters設定(Nullable),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    Nullable,
                                    argument
                                ),
                                Expression.IfThenElse(
                                    Expression.Property(
                                        Nullable,
                                        nameof(Nullable<int>.HasValue)
                                    ),
                                    AddAssign(
                                        Sum,
                                        Expression.Property(
                                            Nullable,
                                            nameof(Nullable<int>.Value)
                                        )
                                    ),
                                    Default_void
                                )
                            )
                        )
                    )
                );
            } else {
                Debug.Assert(
                    MethodCall0_Arguments.Count==2&&(
                        Reflection.ExtensionSet.SumNullableInt32_selector==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableInt64_selector==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableDecimal_selector==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableDouble_selector==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumNullableSingle_selector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableInt32_selector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableInt64_selector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableDecimal_selector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableDouble_selector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumNullableSingle_selector==GenericMethodDefinition
                    )
                );
                ListExpression.Add(
                    this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument =>{
                            var Assign=Expression.Assign(
                                Nullable,
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    argument
                                )
                            );
                            return Expression.Block(
                                作業配列.Parameters設定(Nullable),
                                作業配列.Expressions設定(
                                    Assign,
                                    Expression.IfThenElse(
                                        Expression.Property(
                                            Nullable,
                                            nameof(Nullable<int>.HasValue)
                                        ),
                                        AddAssign(
                                            Sum,
                                            Expression.Property(
                                                Nullable,
                                                nameof(Nullable<int>.Value)
                                            )
                                        ),
                                        Default_void
                                    )
                                )
                            );
                        })
                );
            }
            ListExpression.Add(
                Expression.New(
                    作業配列.GetConstructor(
                        MethodCall0_Type,
                        Sum_Type
                    ),
                    作業配列.Expressions設定(Sum)
                )
            );
        } else {
            if(MethodCall0_Arguments.Count==1) {
                Debug.Assert(
                    MethodCall0_Arguments.Count==1&&(
                        Reflection.ExtensionSet.SumInt32==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumInt64==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumDecimal==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumDouble==GenericMethodDefinition||
                        Reflection.ExtensionSet.SumSingle==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumInt32==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumInt64==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumDecimal==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumDouble==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SumSingle==GenericMethodDefinition
                    )
                );
                var MethodCall0_Arguments_0 = MethodCall0.Arguments[0];
                if(this.重複除去されているか(MethodCall0_Arguments_0)){
                    //Setの場合重複は除かれて合計すべきなのでそのまま合計
                    //Enumerableの場合重複があっても合計すべきなのでそのまま合計
                    ListExpression.Add(
                        this.ループ展開(
                            MethodCall0_Arguments_0,
                            argument=>AddAssign(
                                Sum,
                                argument
                            )
                        )
                    );
                } else{
                    //Setでかつ重複がある場合は重複を除いて合計すべき
                    var 作業_Type=作業配列.MakeGenericType(
                        typeof(Set<>),
                        IEnumerable1のT(MethodCall0_Arguments_0.Type)
                    );
                    var 作業=Expression.Parameter(
                        作業_Type,
                        $"{変数名}Sum"
                    );
                    ListParameter.Add(作業);
                    ListExpression.Add(
                        Expression.Assign(
                            作業,
                            Expression.New(作業_Type)
                        )
                    );
                    ListExpression.Add(
                        this.ループ展開(
                            MethodCall0.Arguments[0],
                            argument=>Expression.IfThenElse(
                                Expression.Call(
                                    作業,
                                    作業_Type.GetMethod(nameof(Set<int>.IsAdded)),
                                    argument
                                ),
                                AddAssign(
                                    Sum,
                                    argument
                                ),
                                Default_void
                            )
                        )
                    );
                }
            } else {
                Debug.Assert(
                    Reflection.ExtensionSet.SumInt32_selector==GenericMethodDefinition||
                    Reflection.ExtensionSet.SumInt64_selector==GenericMethodDefinition||
                    Reflection.ExtensionSet.SumDecimal_selector==GenericMethodDefinition||
                    Reflection.ExtensionSet.SumDouble_selector==GenericMethodDefinition||
                    Reflection.ExtensionSet.SumSingle_selector==GenericMethodDefinition||
                    Reflection.ExtensionEnumerable.SumInt32_selector==GenericMethodDefinition||
                    Reflection.ExtensionEnumerable.SumInt64_selector==GenericMethodDefinition||
                    Reflection.ExtensionEnumerable.SumDecimal_selector==GenericMethodDefinition||
                    Reflection.ExtensionEnumerable.SumDouble_selector==GenericMethodDefinition||
                    Reflection.ExtensionEnumerable.SumSingle_selector==GenericMethodDefinition
                );
                ListExpression.Add(
                    this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => AddAssign(
                            Sum,
                            this.LambdaExpressionを展開1(
                                this.Traverse(MethodCall0_Arguments[1]),
                                argument
                            )
                        )
                    )
                );
            }
            ListExpression.Add(Sum);
        }
        return Expression.Block(
            ListParameter,
            ListExpression
        );
    }
    //private Expression ToArray(MethodCallExpression MethodCall0){
    //    var Method=MethodCall0.Method;
    //    var Name=Method.Name;
    //    var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
    //    //var GenericMethodDefinition=GetGenericMethodDefinition(Method);
    //    var MethodCall0_Arguments = MethodCall0.Arguments;
    //    if(Method.DeclaringType!=typeof(Enumerable)) return base.Call(MethodCall0);
    //    var 作業配列=this.作業配列;
    //    var ListType = 作業配列.MakeGenericType(
    //        typeof(List<>),
    //        MethodCall0.Type.GetElementType()!
    //    );
    //    var List = Expression.Parameter(
    //        ListType,
    //        $"{変数名}List"
    //    );
    //    var Expression1ループ = this.ループ展開(
    //        MethodCall0_Arguments[0],
    //        argument => Expression.Call(
    //            List,
    //            ListType.GetMethod(nameof(System.Collections.IList.Add)),
    //            argument
    //        )
    //    );
    //    return Expression.Block(
    //        作業配列.Parameters設定(List),
    //        作業配列.Expressions設定(
    //            Expression.Assign(
    //                List,
    //                Expression.New(ListType)
    //            ),
    //            Expression1ループ,
    //            Call(
    //                List,
    //                nameof(List<int>.ToArray)
    //            )
    //        )
    //    );
    //}
    protected override Expression Call(MethodCallExpression MethodCall0) {
        var Method = MethodCall0.Method;
        var GenericMethodDefinition = GetGenericMethodDefinition(Method);
        var Name = Method.Name;
        var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
        var 作業配列 = this.作業配列;
        //Debug.Assert(Reflection.ExtensionSet.Contains_value!=GenericMethodDefinition);
        Debug.Assert(Method.ReturnType==MethodCall0.Type);
        if(ループ展開可能メソッドか(GenericMethodDefinition)) {
            switch(Method.Name) {
                case nameof(ExtensionSet.Inline       ):return this.Inline(MethodCall0);
                case nameof(Enumerable  .Aggregate    ):return this.Aggregate(MethodCall0);
                case nameof(Enumerable  .All          ):return this.All(MethodCall0);
                case nameof(Enumerable  .Any          ):return this.Any(MethodCall0);
                case nameof(Enumerable  .Average      ):return this.Average(MethodCall0);
                case nameof(ExtensionSet.Avedev       ):return this.Avedev(MethodCall0);
                case nameof(Enumerable  .AsEnumerable ):return this.AsEnumerable(MethodCall0);
                case nameof(ExtensionSet.ToLookup     ):return this.ToLookup(MethodCall0);
                case nameof(Enumerable  .Count        )or nameof(Enumerable.LongCount):return this.Count(MethodCall0);
                case nameof(ExtensionSet.Harmean      ):return this.Harmean(MethodCall0);
                case nameof(ExtensionSet.Geomean      ):return this.Geomean(MethodCall0);
                case nameof(Enumerable  .Max          ):return this.MaxMin(MethodCall0,ExpressionType.LessThan);
                case nameof(Enumerable  .Min          ):return this.MaxMin(MethodCall0,ExpressionType.GreaterThan);
                case nameof(ExtensionSet.Stdev        ):return this.Stdev(MethodCall0);
                case nameof(ExtensionSet.Var          )or nameof(ExtensionSet.Varp):return this.VarVarp(MethodCall0); 
                case nameof(Enumerable  .SequenceEqual):return this.SequenceEqual(MethodCall0);
                case nameof(Enumerable  .Single       )or nameof(Enumerable.SingleOrDefault): return this.Single(MethodCall0);
                case nameof(Enumerable  .Sum          ):return this.Sum(MethodCall0); 
                //case nameof(Enumerable  .ToArray      ):return this.ToArray(MethodCall0); 
                //case nameof(Enumerable  .Empty        ):return MethodCall0;
            }
            Debug.Assert(MethodCall0.Type!=typeof(void));
            ParameterExpression Parameter;
            MethodInfo Add;
            BinaryExpression Assign;
            //var (Result,Add,ResultAssign)=具象SetType戻り値なしCountあり(MethodCall0,変数名);//具象Type(MethodCall0,変数名,false,true);
            if(nameof(ExtensionSet.DUnion)==Name) 
                (Parameter,Add,Assign)=具象SetType戻り値ありCountあり(MethodCall0,変数名);
            else 
                (Parameter,Add,Assign)=具象SetType戻り値なしCountあり(MethodCall0,変数名);//具象Type(MethodCall0,変数名,false,true);

            var Expression1ループ = this.ループ展開(
                MethodCall0,
                argument => Expression.Call(
                    Parameter,
                    Add,
                    argument
                )
            );
            return Expression.Block(
                作業配列.Parameters設定(Parameter),
                作業配列.Expressions設定(
                    Assign,
                    Expression1ループ,
                    Parameter
                )
            );
        }
        return base.Call(MethodCall0);
    }
}
//2446
