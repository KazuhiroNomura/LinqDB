using System.Diagnostics;
using System.Linq;
using Generic = System.Collections.Generic;
using System.Linq.Expressions;
using LinqDB.Helpers;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.VoidExpressionTraverser;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
/// <summary>
/// 複数出現した式が最初に出現した時点で変数に代入し、以降その変数を参照する。
/// </summary>
/// <example>(a*b)+(a*b)→(t=a*b)+t</example>
public sealed class 変換_局所Parameterの先行評価:ReturnExpressionTraverser{
    private readonly 作成_辺 _作成_辺;
    private sealed class 作成_二度出現したExpression(List辺 List辺,Generic.List<ParameterExpression> ListスコープParameter):VoidExpressionTraverser_Quoteを処理しない{
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        /// <summary>
        /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
        /// </summary>
        internal bool IsInline=true;
        private int 辺番号;
        private 辺 Top辺=>List辺[0];
        private 辺 辺=default!;
        //private Expression? 二度出現Expression;
        public void 実行(Expression Expression0){
            this.辺番号=1;
            this.辺=this.Top辺;
            this.Traverse(Expression0);
            Debug.Assert(this.辺番号==List辺.Count);
            List辺.一度出現したExpressionを上位に移動();
        }
        protected override void Traverse(Expression Expression){
            //if(this.二度出現Expression is not null) return;
            switch(Expression.NodeType) {
                case ExpressionType.DebugInfo:
                case ExpressionType.Default:
                case ExpressionType.Lambda :
                case ExpressionType.PostDecrementAssign:
                case ExpressionType.PostIncrementAssign:
                case ExpressionType.PreDecrementAssign:
                case ExpressionType.PreIncrementAssign:
                case ExpressionType.Throw:
                    return;
                case ExpressionType.Label:{
                    var Label=(LabelExpression)Expression;
                    if(Label.DefaultValue is not null)
                        this.Traverse(Label.DefaultValue);
                    this.辺インクリメント();
                    return;
                }
                case ExpressionType.Goto:{
                    var Goto=(GotoExpression)Expression;
                    if(Goto.Value is not null)
                        this.Traverse(Goto.Value);
                    this.辺インクリメント();
                    return;
                }
                case ExpressionType.Loop:{
                    var Loop=(LoopExpression)Expression;
                    this.辺インクリメント();
                    this.Traverse(Loop.Body);
                    if(Loop.BreakLabel is not null)
                        this.辺インクリメント();
                    return;
                }
                case ExpressionType.Assign: {
                    var Assign = (BinaryExpression)Expression;
                    var Assign_Left = Assign.Left;
                    if(Assign_Left.NodeType is ExpressionType.Parameter) {
                        //LambdaをTraverseしない
                        //baseを読んでいるのはt=1mという先行評価のための代入式の右辺を無限に先行評価してしまうため
                        if(Assign.Right is not LambdaExpression)
                            base.Traverse(Assign.Right);//a=1mの1mを２度出現とみなす
                    } else {
                        base.Traverse(Assign_Left);//.static_field,array[ここ]など
                        this.Traverse(Assign.Right);
                    }
                    return;
                }
                case ExpressionType.Call: {
                    if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression).Method))return;
                    break;
                }
                case ExpressionType.Constant: {
                    if(ILで直接埋め込めるか((ConstantExpression)Expression))return;
                    break;
                }
                case ExpressionType.Parameter: {
                    if(this.ラムダ跨ぎParameters.Contains(Expression))break;
                    return;
                }
                case ExpressionType.MemberAccess:{
                    var Member=(MemberExpression)Expression;
                    if(Member.Expression is not null)
                        if(Member.Expression.Type.IsValueType){
                            //辺を走査し続けるために必要
                            base.Traverse(Expression);
                            return;
                        }
                    break;
                }
            }
            if(Expression.Type!=typeof(void))
                if(this.辺.節一度出現Expressions.Add(Expression)) {
                } else {
                    this.辺.節二度出現Expressions.Add(Expression);
                    return;
                }
            base.Traverse(Expression);
        }
        private void 辺インクリメント()=>this.辺=List辺[this.辺番号++];
        protected override void Block(BlockExpression Block){
            var ListスコープParameter_Count = ListスコープParameter.Count;
            ListスコープParameter.AddRange(Block.Variables);
            base.Block(Block);
            ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
        }
        protected override void Conditional(ConditionalExpression Conditional){
            //test　　　　0 test
            //├────┐1 br_false 2 ifFalse
            //ifTrue　　│
            //└───┐│  goto 3 endif
            //┌───┼┘2 ifFalse:
            //ifFalse │　
            //├───┘　3 endif:
            this.Traverse(Conditional.Test);
            this.辺インクリメント();
            this.Traverse(Conditional.IfTrue);
            this.辺インクリメント();
            this.Traverse(Conditional.IfFalse);
            this.辺インクリメント();
        }
        protected override void Switch(SwitchExpression Switch){
            this.Traverse(Switch.SwitchValue);
            foreach(var Case in Switch.Cases){
                this.辺インクリメント();
                this.Traverse(Case.Body);
            }
            this.辺インクリメント();
            this.Traverse(Switch.DefaultBody);
            this.辺インクリメント();
        }
        protected override void Try(TryExpression Try){
            this.辺インクリメント();
            this.Traverse(Try.Body);
            var Try_Handlers=Try.Handlers;
            var Try_Handlers_Count=Try_Handlers.Count;
            for(var a=0;a<Try_Handlers_Count;a++){
                var Try_Handle=Try_Handlers[a];
                this.辺インクリメント();
                this.TraverseNulllable(Try_Handle.Filter);
                this.Traverse(Try_Handle.Body);
            }
            if(Try.Fault is not null){
                this.辺インクリメント();
                this.Traverse(Try.Fault);
            }else if(Try.Finally is not null){
                this.辺インクリメント();
                this.Traverse(Try.Finally);
            }
            this.辺インクリメント();
        }
        protected override void Call(MethodCallExpression MethodCall) {
            if(this.IsInline){
                if(ループ展開可能メソッドか(MethodCall)) {
                    if(nameof(Sets.ExtensionSet.Except)==MethodCall.Method.Name){
                        var MethodCall0_Arguments = MethodCall.Arguments;
                        this.Traverse(MethodCall0_Arguments[1]);
                        this.Traverse(MethodCall0_Arguments[0]);
                        return;
                    }
                }
            }
            base.Call(MethodCall);
        }
    }
    private readonly 作成_二度出現したExpression _作成_辺に二度出現したExpression;
    private sealed class 取得_二度出現したExpression(List辺 List辺):VoidExpressionTraverser_Quoteを処理しない{
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        /// <summary>
        /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
        /// </summary>
        internal bool IsInline=true;
        private int 辺番号;
        private 辺 Top辺=>List辺[0];
        private 辺 辺=default!;
        //private 置換開始
        private int 計算量;
        public (Expression?二度出現した一度目のExpression,辺?辺) 実行(Expression Expression0){
            this.計算量=0;
            this.辺番号=1;
            this.辺=this.Top辺;
            this.Traverse(Expression0);
            //Trace.WriteLine($"取得_二度出現したExpression2.計算量 {this.計算量}");
            var (二度出現した一度目のExpression,辺)=List辺.二度出現したExpressionと辺();
            return(二度出現した一度目のExpression,辺);
        }
        protected override void Traverse(Expression Expression) {
            //Debug.Assert(Expression.NodeType is not(ExpressionType.AndAlso or ExpressionType.OrElse));
            this.計算量++;
            switch(Expression.NodeType) {
                case ExpressionType.DebugInfo:
                case ExpressionType.Default:
                case ExpressionType.Lambda :
                case ExpressionType.PostDecrementAssign:
                case ExpressionType.PostIncrementAssign:
                case ExpressionType.PreDecrementAssign:
                case ExpressionType.PreIncrementAssign:
                case ExpressionType.Throw:
                    return;
                case ExpressionType.Label:{
                    var Label=(LabelExpression)Expression;
                    if(Label.DefaultValue is not null)
                        this.Traverse(Label.DefaultValue);
                    this.辺インクリメント();
                    return;
                }
                case ExpressionType.Goto:{
                    var Goto=(GotoExpression)Expression;
                    if(Goto.Value is not null)
                        this.Traverse(Goto.Value);
                    this.辺インクリメント();
                    return;
                }
                case ExpressionType.Loop:{
                    var Loop=(LoopExpression)Expression;
                    this.辺インクリメント();
                    this.Traverse(Loop.Body);
                    if(Loop.BreakLabel is not null)
                        this.辺インクリメント();
                    return;
                }
                case ExpressionType.Assign: {
                    var Assign = (BinaryExpression)Expression;
                    var Assign_Left = Assign.Left;
                    if(Assign_Left.NodeType is not ExpressionType.Parameter)
                        base.Traverse(Assign_Left);//.static_field,array[ここ]など
                    else{
                    }
                    this.Traverse(Assign.Right);
                    return;
                }
                case ExpressionType.Call: {
                    if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression).Method))return;
                    break;
                }
                case ExpressionType.Constant: {
                    if(ILで直接埋め込めるか((ConstantExpression)Expression))return;
                    break;
                }
                case ExpressionType.Parameter: {
                    if(this.ラムダ跨ぎParameters.Contains(Expression))break;
                    return;
                }
            }
            if(Expression.Type!=typeof(void))
                if(this.辺.二度出現したExpression is null)
                    if(this.辺.節子孫二度出現Expressions.Contains(Expression)) 
                        this.辺.二度出現したExpression=Expression;
            base.Traverse(Expression);
        }
        private void 辺インクリメント()=>this.辺=List辺[this.辺番号++];
        protected override void Conditional(ConditionalExpression Conditional){
            //test　　　　0 test
            //├────┐1 br_false 2 ifFalse
            //ifTrue　　│
            //└───┐│  goto 3 endif
            //┌───┼┘2 ifFalse:
            //ifFalse │　
            //├───┘　3 endif:
            this.Traverse(Conditional.Test);
            this.辺インクリメント();
            this.Traverse(Conditional.IfTrue);
            this.辺インクリメント();
            this.Traverse(Conditional.IfFalse);
            this.辺インクリメント();
        }
        protected override void Switch(SwitchExpression Switch){
            this.Traverse(Switch.SwitchValue);
            foreach(var Case in Switch.Cases){
                this.辺インクリメント();
                this.Traverse(Case.Body);
            }
            this.辺インクリメント();
            this.Traverse(Switch.DefaultBody);
            this.辺インクリメント();
        }
        protected override void Try(TryExpression Try){
            this.辺インクリメント();
            this.Traverse(Try.Body);
            var Try_Handlers=Try.Handlers;
            var Try_Handlers_Count=Try_Handlers.Count;
            for(var a=0;a<Try_Handlers_Count;a++){
                this.辺インクリメント();
                var Try_Handle=Try_Handlers[a];
                this.TraverseNulllable(Try_Handle.Filter);
                this.Traverse(Try_Handle.Body);
            }
            if(Try.Fault is not null){
                this.辺インクリメント();
                this.Traverse(Try.Fault);
            } else if(Try.Finally is not null){
                this.辺インクリメント();
                this.Traverse(Try.Finally);
            }
            this.辺インクリメント();
        }
        protected override void Call(MethodCallExpression MethodCall) {
            if(this.IsInline){
                if(ループ展開可能メソッドか(MethodCall)) {
                    if(nameof(Sets.ExtensionSet.Except)==MethodCall.Method.Name){
                        var MethodCall0_Arguments = MethodCall.Arguments;
                        this.Traverse(MethodCall0_Arguments[1]);
                        this.Traverse(MethodCall0_Arguments[0]);
                        return;
                    }
                }
            }
            base.Call(MethodCall);
        }
    }
    private readonly 取得_二度出現したExpression _取得_辺から二度出現したExpression;
    private sealed class 変換_二度出現したExpression(作業配列 作業配列,List辺 List辺,Generic.List<ParameterExpression> ListスコープParameter,ExpressionEqualityComparer_Assign_Leftで比較 ExpressionEqualityComparer):ReturnExpressionTraverser(作業配列){
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer=ExpressionEqualityComparer;
        internal Generic.IEnumerable<ParameterExpression> ラムダ跨ぎParameters=default!;
        private Expression? 二度出現した一度目のExpression;
        private ParameterExpression? 二度目以降のParameter;
        private Expression 一度目のAssign=default!;
        /// <summary>
        /// Except argumentsを1,0の順で置換する。NoEvaluationで置換しないようにする
        /// </summary>
        internal bool IsInline;
        private int 辺番号;
        private 辺 辺=default!;
        private int 計算量;
        public Expression 実行(Expression Expression0,Expression 二度出現した一度目のExpression,ParameterExpression 二度目以降のParameter){
            this.計算量=0;
            this.辺番号=1;
            this.辺=List辺[0];
            this.二度出現した一度目のExpression = 二度出現した一度目のExpression;
            this.二度目以降のParameter = 二度目以降のParameter;
            this.一度目のAssign = Expression.Assign(二度目以降のParameter,二度出現した一度目のExpression);
            var Expression1=this.Traverse(Expression0);
            return Expression1;
        }
        protected override Expression Traverse(Expression Expression0) {
            this.計算量++;
            switch(Expression0.NodeType){
                case ExpressionType.DebugInfo:
                case ExpressionType.Default:
                case ExpressionType.Lambda :
                case ExpressionType.PostDecrementAssign:
                case ExpressionType.PostIncrementAssign:
                case ExpressionType.PreDecrementAssign:
                case ExpressionType.PreIncrementAssign:
                case ExpressionType.Throw:
                    return Expression0;
                case ExpressionType.Label: {
                    var Label0 = (LabelExpression)Expression0;
                    var Label1=this.Label(Label0);
                    this.辺インクリメント();
                    return Label1;
                }
                case ExpressionType.Goto:{
                    var Goto0=(GotoExpression)Expression0;
                    var Goto1=this.Goto(Goto0);
                    this.辺インクリメント();
                    return Goto1;
                }
                //case ExpressionType.Conditional:{
                //    var Conditional0=(ConditionalExpression)Expression0;
                //    break;
                //}
                case ExpressionType.Loop: {
                    var Loop0 = (LoopExpression)Expression0;
                    this.辺インクリメント();
                    var Loop1=this.Loop(Loop0);
                    if(Loop0.BreakLabel is not null)
                        this.辺インクリメント();
                    return Loop1;
                }
                case ExpressionType.Assign: {
                    var Assign0 = (BinaryExpression)Expression0;
                    var Assign0_Left = Assign0.Left;
                    Expression Assign1_Left;
                    if(Assign0_Left.NodeType is ExpressionType.Parameter){
                        Assign1_Left=Assign0_Left;
                    } else
                        Assign1_Left= base.Traverse(Assign0_Left);//.static_field,array[ここ]など
                    var Assign0_Right=Assign0.Right;
                    var Assign1_Right=this.Traverse(Assign0_Right);
                    if(Assign0_Left==Assign1_Left)
                        if(Assign0_Right==Assign1_Right)
                            return Assign0;
                    return Expression.Assign(Assign1_Left,Assign1_Right);
                }
                case ExpressionType.Call: {
                    if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression0).Method))return Expression0;
                    break;
                }
                case ExpressionType.Constant: {
                    if(ILで直接埋め込めるか((ConstantExpression)Expression0))return Expression0;
                    break;
                }
                case ExpressionType.Parameter: {
                    if(this.ラムダ跨ぎParameters.Contains(Expression0))break;
                    return Expression0;
                }
            }
            if(Expression0.Type!=typeof(void)){
                if(this.ExpressionEqualityComparer.Equals(Expression0,this.二度出現した一度目のExpression)){
                    if(this.辺.節子孫二度出現Expressions.Remove(Expression0))
                        return this.一度目のAssign;
                    if(this.辺.節祖先二度出現Expressions.Contains(Expression0))
                        return this.二度目以降のParameter!;
                }
            }
            return base.Traverse(Expression0);
        }
        private void 辺インクリメント()=>this.辺=List辺[this.辺番号++];
        protected override Expression Block(BlockExpression Block0){
            var ListスコープParameter_Count = ListスコープParameter.Count;
            ListスコープParameter.AddRange(Block0.Variables);
            var Block1=base.Block(Block0);
            ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
            return Block1;
        }
        protected override Expression Conditional(ConditionalExpression Conditional0){
            var Conditional0_Test=Conditional0.Test;
            var Conditional0_IfTrue=Conditional0.IfTrue;
            var Conditional0_IfFalse=Conditional0.IfFalse;
            var Conditional1_Test=this.Traverse(Conditional0_Test);
            this.辺インクリメント();
            var Conditional1_IfTrue=this.Traverse(Conditional0_IfTrue);
            this.辺インクリメント();
            var Conditional1_IfFalse=this.Traverse(Conditional0_IfFalse);
            this.辺インクリメント();
            var Test=Conditional0.Test;
            var IfTrue=Conditional0.IfTrue;
            var IfFalse=Conditional0.IfFalse;
            if(Test==Conditional1_Test)
                if(IfTrue==Conditional1_IfTrue)
                    if(IfFalse==Conditional1_IfFalse)
                        return Conditional0;
            return Expression.Condition(Conditional1_Test,Conditional1_IfTrue,Conditional1_IfFalse,Conditional0.Type);
        }
        protected override Expression Switch(SwitchExpression Switch0){
            var Switch0_SwitchValue=Switch0.SwitchValue;
            var Switch0_Cases=Switch0.Cases;
            var Switch1_SwitchValue=this.Traverse(Switch0_SwitchValue);
            this.辺インクリメント();
            var Switch0_Cases_Count=Switch0_Cases.Count;
            var Switch1_Cases=new SwitchCase[Switch0_Cases_Count];
            var 変化したか=Switch0_SwitchValue !=Switch1_SwitchValue;
            for(var a=0; a < Switch0_Cases_Count; a++) {
                var Switch0_Case=Switch0_Cases[a];
                var Switch0_Case_Body=Switch0_Case.Body;
                var Switch1_Case_Body=this.Traverse(Switch0_Case_Body);
                this.辺インクリメント();
                if(Switch0_Case_Body !=Switch1_Case_Body) {
                    Switch1_Cases[a]=Expression.SwitchCase(Switch1_Case_Body,Switch0_Case.TestValues);
                    変化したか=true;
                } else
                    Switch1_Cases[a]=Switch0_Case;
            }
            var Switch0_DefaultBody=Switch0.DefaultBody;
            var Switch1_DefaultBody=this.Traverse(Switch0_DefaultBody);
            this.辺インクリメント();
            変化したか|=Switch0_DefaultBody!=Switch1_DefaultBody;
            if(!変化したか) return Switch0;
            return Expression.Switch(
                Switch0.Type,
                Switch1_SwitchValue,
                Switch1_DefaultBody,
                Switch0.Comparison,
                Switch1_Cases
            );
        }
        protected override Expression Try(TryExpression Try0){
#if true
            Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
            var Try0_Handlers=Try0.Handlers;
            var Try0_Handlers_Count=Try0_Handlers.Count;
            var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
            this.辺インクリメント();
            var Try1_Body=this.Traverse(Try0.Body);
            for(var a=0;a<Try0_Handlers_Count;a++) {
                var Try0_Handler=Try0_Handlers[a];
                Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
                var Try0_Handler_Variable=Try0_Handler.Variable;
                this.辺インクリメント();
                var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
                var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                CatchBlock Try1_Handler;
                if(Try0_Handler.Filter!=Try1_Handler_Filter||Try0_Handler.Body!=Try1_Handler_Body)
                    if(Try0_Handler_Variable is not null)
                        Try1_Handler=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
                    else
                        Try1_Handler=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
                else
                    Try1_Handler=Try0_Handler;
                Try1_Handlers[a]=Try1_Handler;
            }
            this.辺インクリメント();
            if(Try0.Fault is not null){
                var Try1_Fault=this.Traverse(Try0.Fault);
                this.辺インクリメント();
                return Expression.TryFault(Try1_Body,Try1_Fault);
            }
            if(Try0.Finally is not null){
                var Try1_Finally=this.Traverse(Try0.Finally);
                this.辺インクリメント();
                return Expression.TryCatchFinally(Try1_Body,Try1_Finally,Try1_Handlers);
            }
            return Expression.TryCatch(Try1_Body,Try1_Handlers);
#else
            Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
            var Try0_Handlers=Try0.Handlers;
            var Try0_Handlers_Count=Try0_Handlers.Count;
            var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
            var 変化したか=false;
            var Try0_Body=Try0.Body;
            this.辺インクリメント();
            var Try1_Body=this.Traverse(Try0_Body);
            if(Try0_Body!=Try1_Body)
                変化したか=true;
            for(var a=0;a<Try0_Handlers_Count;a++) {
                var Try0_Handler=Try0_Handlers[a];
                Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
                var Try0_Handler_Variable=Try0_Handler.Variable;
                this.辺インクリメント();
                var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
                var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                CatchBlock Try1_Handler;
                if(Try0_Handler.Filter!=Try1_Handler_Filter||Try0_Handler.Body!=Try1_Handler_Body){
                    変化したか=true;
                    if(Try0_Handler_Variable is not null)
                        Try1_Handler=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
                    else
                        Try1_Handler=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
                } else
                    Try1_Handler=Try0_Handler;
                Try1_Handlers[a]=Try1_Handler;
            }
            if(Try0.Fault is not null){
                this.辺インクリメント();
                var Try0_Fault=Try0.Fault;
                var Try1_Fault=this.Traverse(Try0_Fault);
                this.辺インクリメント();
                if(Try0_Fault!=Try1_Fault)
                    変化したか=true;
                if(変化したか)
                    return Expression.TryFault(Try1_Body,Try1_Fault);
            } else if(Try0.Finally is not null){
                this.辺インクリメント();
                var Try0_Finally=Try0.Finally;
                var Try1_Finally=this.Traverse(Try0_Finally);
                this.辺インクリメント();
                if(Try0_Finally!=Try1_Finally)
                    変化したか=true;
                if(変化したか)
                    return Expression.TryCatchFinally(Try1_Body,Try1_Finally,Try1_Handlers);
            } else{
                this.辺インクリメント();
                if(変化したか)
                    return Expression.TryCatch(Try1_Body,Try1_Handlers);
            }
            return Try0;
#endif
        }
        protected override Expression Call(MethodCallExpression MethodCall0) {
            if(this.IsInline){
                if(ループ展開可能メソッドか(MethodCall0)) {
                    if(nameof(Sets.ExtensionSet.Except)==MethodCall0.Method.Name){
                        Debug.Assert(
                            MethodCall0.Arguments.Count==3
                            &&
                            Reflection.ExtensionEnumerable.Except_comparer==MethodCall0.Method.GetGenericMethodDefinition()
                            ||
                            MethodCall0.Arguments.Count==2
                            &&(
                                Reflection.ExtensionEnumerable.Except==MethodCall0.Method.GetGenericMethodDefinition()
                                ||
                                Reflection.ExtensionSet.Except==MethodCall0.Method.GetGenericMethodDefinition()
                            )
                        );
                        var MethodCall0_Arguments = MethodCall0.Arguments;
                        var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                        var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                        if(MethodCall0.Arguments.Count==3) return Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,this.Traverse(MethodCall0_Arguments[2]));
                        else return Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
                    }
                }
            }
            return base.Call(MethodCall0);
        }
    }
    private readonly 変換_二度出現したExpression _変換_二度出現したExpression;
    private readonly Generic.List<ParameterExpression> ListスコープParameter;
    private readonly Generic.Dictionary<LabelTarget,辺> Dictionary_LabelTarget_辺に関する情報=new();
    private readonly List辺 List辺=new();
    internal 変換_局所Parameterの先行評価(作業配列 作業配列) : base(作業配列){
        var ListスコープParameter=this.ListスコープParameter=new();
        var ExpressionEqualityComparer=new ExpressionEqualityComparer_Assign_Leftで比較(ListスコープParameter);
        var Dictionary_LabelTarget_辺に関する情報=this.Dictionary_LabelTarget_辺に関する情報;
        var Top辺に関する情報=new 辺(ExpressionEqualityComparer){辺番号=0,子コメント = "開始"};
        var List辺=this.List辺;
        this._作成_辺=new(Top辺に関する情報,List辺,Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer);
        this._作成_辺に二度出現したExpression=new(List辺,ListスコープParameter);
        this._取得_辺から二度出現したExpression=new(List辺);
        this._変換_二度出現したExpression=new(作業配列,List辺,ListスコープParameter,ExpressionEqualityComparer);
    }
    /// <summary>
    /// NonPublicAccessorで使う
    /// </summary>
    public 変換_局所Parameterの先行評価() : this(new 作業配列())=>this.ラムダ跨ぎParameters=new Generic.List<ParameterExpression>();
    internal Generic.IEnumerable<ParameterExpression> ラムダ跨ぎParameters{
        set{
            this._作成_辺.ラムダ跨ぎParameters=value;
            this._作成_辺に二度出現したExpression.ラムダ跨ぎParameters=value;
            this._取得_辺から二度出現したExpression.ラムダ跨ぎParameters=value;
            this._変換_二度出現したExpression.ラムダ跨ぎParameters=value;
        }
    }
    private Generic.List<ParameterExpression> Block_Variables = new();
    private int 番号;
    /// <summary>
    /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
    /// </summary>
    public bool IsInline {
        get=>this._変換_二度出現したExpression.IsInline;
        set{
            this._作成_辺.IsInline=value;
            this._作成_辺に二度出現したExpression.IsInline=value;
            this._取得_辺から二度出現したExpression.IsInline=value;
            this._変換_二度出現したExpression.IsInline=value;
        }
    }
    public Expression 実行(Expression Expression0) {
        this.ListスコープParameter.Clear();
        return this.Traverse(Expression0);
    }
    //protected override Expression Assign(BinaryExpression Assign0){
    //    base.Assign()
    //    return Expression.Assign(
    //        this.Traverse(Assign0.Left),
    //        this.Traverse(Assign0.Right)
    //    );
    //}
    protected override Expression Block(BlockExpression Block0){
        var ListスコープParameter = this.ListスコープParameter;
        var ListスコープParameter_Count = ListスコープParameter.Count;
        ListスコープParameter.AddRange(Block0.Variables);
        var Block1=base.Block(Block0);
        ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
        return Block1;
    }
    //private Expression 先行評価スコープブロック(Expression Expression0){
    //    var ListスコープParameter = this.ListスコープParameter;
    //    var ListスコープParameter_Count = ListスコープParameter.Count;
    //    var Expression1=this.先行評価(Expression0);
    //    ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
    //    return Expression1;
    //}
    private Expression 先行評価(Expression Expression0){
        var ListスコープParameter=this.ListスコープParameter;
        var Expression1 = Expression0;
        var Block0_Variables = this.Block_Variables;
        var Block1_Variables = this.Block_Variables=new();
        var BlockVariables = this.Block_Variables;
        var 作成_辺=this._作成_辺;
        var 作成_辺に二度出現したExpression = this._作成_辺に二度出現したExpression;
        var 取得_辺から二度出現したExpression = this._取得_辺から二度出現したExpression;
        var 変換_二度出現したExpression = this._変換_二度出現したExpression;
        var List辺=this.List辺;
        while(true) {
            //if(aa==2){

            //}
            //dynamic p=new NonPublicAccessor(Expression1);
            //var d=p.DebugView;
            //Trace.WriteLine((string)d);
            作成_辺.実行(Expression1);
            //if(ListスコープParameter.Count>100){
            //    Debug.Fail("");
            //}
            作成_辺に二度出現したExpression.実行(Expression1);
            var (二度出現した一度目のExpression,辺)=取得_辺から二度出現したExpression.実行(Expression1);
            if(二度出現した一度目のExpression is null)
                break;
            Debug.Assert(辺!=null,nameof(Optimizers.ReturnExpressionTraverser.辺)+" != null");
            List辺.Reset();
            辺.親でAssignしたExpressionを設定();
            var Variable = Expression.Variable(二度出現した一度目のExpression.Type,$"局所{this.番号++}");
            BlockVariables.Add(Variable);
            ListスコープParameter.Add(Variable);
            var Expression2=変換_二度出現したExpression.実行(Expression1,二度出現した一度目のExpression,Variable);
            Expression1=Expression2;
        }
        //Trace.WriteLine(作成_辺.Analize);
        var Expression3 = this.Traverse(Expression1);
        //Trace.WriteLine(CommonLibrary.インラインラムダテキスト(Expression3));
        this.Block_Variables=Block0_Variables;
        if(Block1_Variables.Count>0) Expression3=Expression.Block(Block1_Variables,this.作業配列.Expressions設定(Expression3));
        return Expression3;
    }
    protected override Expression Lambda(LambdaExpression Lambda0){
        var ListスコープParameter = this.ListスコープParameter;
        var ListスコープParameter_Count = ListスコープParameter.Count;
        var Lambda0_Parameters = Lambda0.Parameters;
        ListスコープParameter.AddRange(Lambda0_Parameters);
        var Lambda0_Body=Lambda0.Body;
        var Lambda1_Body=this.先行評価(Lambda0.Body);
        Expression Lambda1;
        if(Lambda0_Body==Lambda1_Body) 
            Lambda1=Lambda0;
        else 
            Lambda1 = Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0_Parameters);
        ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
        return Lambda1;
    }
    //protected override Expression Try(TryExpression Try0){
    //    Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
    //    var Try0_Handlers=Try0.Handlers;
    //    var Try0_Handlers_Count=Try0_Handlers.Count;
    //    var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
    //    var 変化したか=false;
    //    var Try0_Body=Try0.Body;
    //    var Try1_Body=this.先行評価スコープブロック(Try0_Body);
    //    if(Try0_Body!=Try1_Body) 変化したか=true;
    //    for(var a=0;a<Try0_Handlers_Count;a++){
    //        var Try0_Handler=Try0_Handlers[a];
    //        Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
    //        var Try0_Handler_Variable=Try0_Handler.Variable;
    //        var Try0_Handler_Body=Try0_Handler.Body;
    //        var Try0_Handler_Filter=Try0_Handler.Filter;
    //        var Try1_Handler_Body=this.先行評価スコープブロック(Try0_Handler_Body);
    //        CatchBlock Try1_Handler;
    //        if(Try0_Handler_Variable is not null){
    //            if(Try0_Handler_Filter is not null){
    //                var Try1_Handler_Filter=this.先行評価スコープブロック(Try0_Handler_Filter);
    //                if(Try0_Handler_Body!=Try1_Handler_Body||Try0_Handler_Filter!=Try1_Handler_Filter){
    //                    変化したか=true;
    //                    Try1_Handler=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
    //                } else
    //                    Try1_Handler=Try0_Handler;
    //            } else{
    //                if(Try0_Handler_Body!=Try1_Handler_Body){
    //                    変化したか=true;
    //                    Try1_Handler=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body);
    //                } else
    //                    Try1_Handler=Try0_Handler;
    //            }
    //        } else{
    //            if(Try0_Handler_Filter is not null){
    //                var Try1_Handler_Filter=this.先行評価スコープブロック(Try0_Handler_Filter);
    //                if(Try0_Handler_Body!=Try1_Handler_Body||Try0_Handler_Filter!=Try1_Handler_Filter){
    //                    変化したか=true;
    //                    Try1_Handler=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
    //                } else
    //                    Try1_Handler=Try0_Handler;
    //            } else{
    //                if(Try0_Handler_Body!=Try1_Handler_Body){
    //                    変化したか=true;
    //                    Try1_Handler=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body);
    //                } else
    //                    Try1_Handler=Try0_Handler;
    //            }
    //        }
    //        Try1_Handlers[a]=Try1_Handler;
    //    }
    //    if(Try0.Fault is not null){
    //        Debug.Assert(Try0.Finally is null);
    //        var Try0_Fault=Try0.Fault;
    //        var Try1_Fault=this.Traverse(Try0_Fault);
    //        if(Try0_Fault!=Try1_Fault) 
    //            変化したか=true;
    //        if(変化したか) 
    //            return Expression.TryFault(Try1_Body,Try1_Fault);
    //    } else{
    //        var Try0_Finally=Try0.Finally;
    //        if(Try0_Finally is not null){
    //            var Try1_Finally=this.Traverse(Try0_Finally);
    //            if(Try0_Finally!=Try1_Finally) 
    //                変化したか=true;
    //            if(変化したか) 
    //                return Expression.TryCatchFinally(Try1_Body,Try1_Finally,Try1_Handlers);
    //        }else if(変化したか) 
    //            return Expression.TryCatch(Try1_Body,Try1_Handlers);
    //    }
    //    Debug.Assert(!変化したか);
    //    return Try0;
    //}
    private Expression AndAlso_OrElse(Expression test,Expression ifTrue,Expression ifFalse) {
        if(test.Type==typeof(bool)) {
            return Expression.Condition(
                test,
                ifTrue,
                ifFalse
            );
        } else {
            return Expression.Condition(
                Expression.Call(
                    test.Type.GetMethod(op_True)!,
                    test
                ),
                ifTrue,
                ifFalse
            );
        }
    }
    private Expression AndAlso_OrElse(ParameterExpression p,Expression test,Expression ifTrue,Expression ifFalse) {
        if(test.Type==typeof(bool)) {
            return Expression.Block(
                this.作業配列.Parameters設定(p),
                Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse
                )                
            );
        } else {
            return Expression.Block(
                this.作業配列.Parameters設定(p),
                Expression.Condition(
                    Expression.Call(
                        test.Type.GetMethod(op_True)!,
                        test
                    ),
                    ifTrue,
                    ifFalse
                )
            );
        }
    }
    /// <summary>
    /// a&amp;&amp;b→operator true(a)?a&amp;b:a
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected override Expression AndAlso(BinaryExpression Binary0) {
        var Binary1_Test = this.Traverse(Binary0.Left);
        var Binary1_Right=this.Traverse(Binary0.Right);
        var Binary2_Left=Binary1_Test;
        if(Binary1_Test.NodeType is ExpressionType.Assign){
            var Binary=(BinaryExpression)Binary1_Test;
            Binary2_Left=Binary.Left;
        }
        if(Binary2_Left is ParameterExpression Parameter){
            return this.AndAlso_OrElse(
                Binary1_Test,
                Expression.And(Parameter,Binary1_Right),
                Parameter
            );
        } else{
            var p=Expression.Parameter(Binary1_Test.Type,"AndAlso");
            return this.AndAlso_OrElse(
                p,
                Expression.Assign(p,Binary1_Test),
                Expression.And(p,Binary1_Right),
                p
            );
        }
    }
    /// <summary>
    /// a||b→operator true(a)?a:a|b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected override Expression OrElse(BinaryExpression Binary0) {
        var Binary1_Test = this.Traverse(Binary0.Left);
        var Binary1_Right=this.Traverse(Binary0.Right);
        var Binary2_Left=Binary1_Test;
        if(Binary1_Test.NodeType is ExpressionType.Assign){
            var Binary=(BinaryExpression)Binary1_Test;
            Binary2_Left=Binary.Left;
        }
        if(Binary2_Left is ParameterExpression Parameter){
            return this.AndAlso_OrElse(
                Binary1_Test,
                Parameter,
                Expression.And(Parameter,Binary1_Right)
            );
        } else{
            var p=Expression.Parameter(Binary1_Test.Type,"AndAlso");
            return this.AndAlso_OrElse(
                p,
                Expression.Assign(p,Binary1_Test),
                p,
                Expression.Or(p,Binary1_Right)
            );
        }
    }
}
//20231128  719
//20231208 2516
//20240116 1136