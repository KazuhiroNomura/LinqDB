using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace LinqDB.Optimizers;

partial class Optimizer {
    /// <summary>
    /// 複数出現した式が最初に出現した時点で変数に代入し、以降その変数を参照する。
    /// </summary>
    /// <example>(a*b)+(a*b)→(t=a*b)+t</example>
    private sealed class 変換_局所Parameterの先行評価:ReturnExpressionTraverser_Quoteを処理しない {
        private sealed class 取得_Labelに対応するExpressions:VoidExpressionTraverser_Quoteを処理しない{
            private readonly Dictionary<LabelTarget,List<Expression>>Dictionary_LabelTarget_Expressions;
            public 取得_Labelに対応するExpressions(Dictionary<LabelTarget,List<Expression>>Dictionary_LabelTarget_Expressions){
                this.Dictionary_LabelTarget_Expressions=Dictionary_LabelTarget_Expressions;
            }
            private List<Expression>? ListExpression;
            public void 実行(Expression e){
                this.ListExpression=null;
                this.Dictionary_LabelTarget_Expressions.Clear();
                this.Traverse(e);
            }
            protected override void Label(LabelExpression Label){
                this.TraverseNulllable(Label.DefaultValue);
                var ListExpression=this.ListExpression=new List<Expression>();
                this.Dictionary_LabelTarget_Expressions.Add(Label.Target,ListExpression);
            }
            //protected override void Conditional(ConditionalExpression Conditional){
            //    this.Traverse(Conditional.Test);
            //    this.Traverse(Conditional.IfTrue);
            //    this.Traverse(Conditional.IfFalse);
            //    base.Conditional(Conditional);
            //}
            protected override void Loop(LoopExpression Loop){
                if(Loop.ContinueLabel is not null){
                    this.Dictionary_LabelTarget_Expressions.Add(Loop.ContinueLabel,this.ListExpression=new List<Expression>());
                } else{
                    this.ListExpression=null;
                }
                this.Traverse(Loop.Body);
                if(Loop.BreakLabel is not null){
                    this.Dictionary_LabelTarget_Expressions.Add(Loop.BreakLabel,this.ListExpression=new List<Expression>());
                } else{
                    this.ListExpression=null;
                }
            }
            protected override void Block(BlockExpression Block){
                var Block_Expressions=Block.Expressions;
                var Block_Expressions_Count=Block_Expressions.Count;
                for(var a=0;a<Block_Expressions_Count;a++){
                    var Block_Expression=Block_Expressions[a];
                    if(Block_Expression.NodeType is ExpressionType.Block or ExpressionType.Loop or ExpressionType.Label){
                    }else if(Block_Expression.NodeType==ExpressionType.Goto)this.ListExpression=null;
                    else this.ListExpression?.Add(Block_Expression);
                    this.Traverse(Block_Expression);
                }
            }
        }
        private readonly 取得_Labelに対応するExpressions _取得_Labelに対応するExpressions;
        private sealed class 取得_二度出現したExpression:VoidExpressionTraverser_Quoteを処理しない{
            private readonly IReadOnlyDictionary<LabelTarget,List<Expression>>Dictionary_LabelTarget_Expressions;
            private readonly Dictionary<Expression,Expression?> Dictionary一度出現したExpression_二度出現したExpressionの一度目;
            private HashSet<Expression> HashSet一度出現したExpression;
            private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
            private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
            private readonly Stack<(HashSet<Expression>HashSet一度出現したExpression,HashSet<Expression>HashSet一度出現したExpression1,bool IfTrueまたはIfFalse外部か)> Stack_HashSet一度出現したExpression=new();
            private readonly HashSet<LabelTarget> HashSet_走査したLabelTarget=new();
            public 取得_二度出現したExpression(IReadOnlyDictionary<LabelTarget,List<Expression>>Dictionary_LabelTarget_Expressions,ExpressionEqualityComparer ExpressionEqualityComparer,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる){
                this.Dictionary_LabelTarget_Expressions=Dictionary_LabelTarget_Expressions;
                this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
                this.Dictionary一度出現したExpression_二度出現したExpressionの一度目=new(ExpressionEqualityComparer);
                this.HashSet一度出現したExpression=new HashSet<Expression>(ExpressionEqualityComparer);
                this.ExpressionEqualityComparer=ExpressionEqualityComparer;
            }
            internal IEnumerable<Expression> ラムダ跨ぎParameters=default!;
            private bool IfTrueまたはIfFalse外部か;
            private Expression? 二度出現した一度目のExpression;
            /// <summary>
            /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
            /// </summary>
            internal bool IsInline=true;
            private bool フローを走査した;
            public (Expression? Expression,bool フローを走査した)実行(Expression e){
                //this.探索停止するか=false;
                this.フローを走査した=false;
                this.HashSet_走査したLabelTarget.Clear();
                this.Stack_HashSet一度出現したExpression.Clear();
                this.IfTrueまたはIfFalse外部か=true;
                this.Dictionary一度出現したExpression_二度出現したExpressionの一度目.Clear();
                this.HashSet一度出現したExpression.Clear();
                this.二度出現した一度目のExpression=null;
                this.判定_左辺Expressionsが含まれる.Clear();
                this.Traverse(e);
                return (this.二度出現した一度目のExpression,this.フローを走査した);
            }
            protected override void Traverse(Expression Expression) {
                if(this.二度出現した一度目のExpression is not null||this.フローを走査した)return;
                switch(Expression.NodeType) {
                    case ExpressionType.DebugInfo or ExpressionType.Default or ExpressionType.Lambda or ExpressionType.PostDecrementAssign or ExpressionType.PostIncrementAssign or ExpressionType.PreIncrementAssign or ExpressionType.PreDecrementAssign or ExpressionType.Throw:
                        return;
                    case ExpressionType.Label:{
                        //var IfTrueまたはIfFalse外部か=this.Stack_IfTrueまたはIfFalse外部か.Pop();
                        var Label=(LabelExpression)Expression;
                        if(!this.HashSet_走査したLabelTarget.Add(Label.Target)){
                            this.フローを走査した=true;
                            return;
                        }
                        if(this.IfTrueまたはIfFalse外部か){
                            this.共通for_Dictionary一度出現したExpression_二度出現したExpressionの一度目();
                            this.TraverseNulllable(Label.DefaultValue);
                        }else{
                            var (HashSet一度出現したExpression1,HashSet一度出現したExpression,IfTrueまたはIfFalse外部か)=this.Stack_HashSet一度出現したExpression.Pop();
                            var HashSet一度出現したExpression2=this.HashSet一度出現したExpression;
                            Debug.Assert(this.IfTrueまたはIfFalse外部か==false);
                            //var Dictionary一度出現したExpression_二度出現したExpressionの一度目=this.Dictionary一度出現したExpression_二度出現したExpressionの一度目;
                            HashSet一度出現したExpression1.IntersectWith(HashSet一度出現したExpression2);
                            if(IfTrueまたはIfFalse外部か){
                                this.共通for_Dictionary一度出現したExpression_二度出現したExpressionの一度目();
                            } else{
                                HashSet一度出現したExpression.IntersectWith(HashSet一度出現したExpression1);
                            }
                            this.IfTrueまたはIfFalse外部か=IfTrueまたはIfFalse外部か;
                            this.TraverseNulllable(Label.DefaultValue);
                        }
                        return;
                    }
                    case ExpressionType.Loop:{
                        var BreakLabel=((LoopExpression)Expression).BreakLabel;
                        if(!this.HashSet_走査したLabelTarget.Add(BreakLabel)){
                            this.フローを走査した=true;
                            return;
                        }
                        if(this.IfTrueまたはIfFalse外部か){
                            this.共通for_Dictionary一度出現したExpression_二度出現したExpressionの一度目();
                        }else{
                            var (HashSet一度出現したExpression1,HashSet一度出現したExpression,IfTrueまたはIfFalse外部か)=this.Stack_HashSet一度出現したExpression.Pop();
                            var HashSet一度出現したExpression2=this.HashSet一度出現したExpression;
                            Debug.Assert(this.IfTrueまたはIfFalse外部か==false);
                            HashSet一度出現したExpression1.IntersectWith(HashSet一度出現したExpression2);
                            if(IfTrueまたはIfFalse外部か){
                                this.共通for_Dictionary一度出現したExpression_二度出現したExpressionの一度目();
                            } else {
                                HashSet一度出現したExpression.IntersectWith(HashSet一度出現したExpression1);
                            }
                            this.IfTrueまたはIfFalse外部か=IfTrueまたはIfFalse外部か;
                        }
                        return;
                    }
                    case ExpressionType.Goto:{
                        var Goto=(GotoExpression)Expression;
                        if(!this.HashSet_走査したLabelTarget.Add(Goto.Target)) return;
                        //var ListExpression=this.Dictionary_LabelTarget_Expressions[Goto.Target];
                        //var ListExpression_Count=ListExpression.Count;
                        //for(var a=0;a<ListExpression_Count;a++)this.Traverse(ListExpression[a]);
                        //return;
                        Expression=Expression.Block(this.Dictionary_LabelTarget_Expressions[Goto.Target]);
                        break;
                    }
                    case ExpressionType.Assign: {
                        var Assign = (BinaryExpression)Expression;
                        var Assign_Left = Assign.Left;
                        if(Assign_Left is IndexExpression Index)
                            base.Index(Index);
                        else if(Assign_Left is ParameterExpression){
                            this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                            this.Traverse(Assign.Right);
                        } else{
                            base.Traverse(Assign_Left);//.static_fieldなど
                        }
                        return;
                        //↓自動局所変数設定でうまくいかない例えば
                        //(t=a+a)==0のa+aは対象に入れてはいけない
                        //switch(Assign.Right.NodeType) {
                        //    case ExpressionType.Constant or ExpressionType.Default or ExpressionType.Parameter:return;
                        //    default:
                        //        this.Traverse(Assign.Right);
                        //        this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                        //        //if(Assign_Left is ParameterExpression Parameter)
                        //        //    this.判定_左辺Parametersが含まれる.Add(Parameter);
                        //        return;
                        //}
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
                if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
                    base.Traverse(Expression);
                    return;
                }
                if(Expression.Type!=typeof(void)) {
                    if(this.IfTrueまたはIfFalse外部か) {
                        this.共通Dictionary一度出現したExpression_二度出現したExpressionの一度目(Expression);
                    } else {
                        this.HashSet一度出現したExpression.Add(Expression);
                    }
                }
                base.Traverse(Expression);
            }
            protected override void AndAlso(BinaryExpression Binary){
                this.Traverse(Binary.Left);
            }
            protected override void OrElse(BinaryExpression Binary){
                this.Traverse(Binary.Left);
            }
            private static bool Labelで終わるか(Expression Expression){
                if(Expression.NodeType==ExpressionType.Goto) return true;
                if(Expression is BlockExpression Block){
                    var Block_Expressions=Block.Expressions;
                    var Block_Expressions_Count=Block_Expressions.Count;
                    for(var a=0;a<Block_Expressions_Count;a++)
                        if(Labelで終わるか(Block_Expressions[a]))
                            return true;
                }
                return false;
            }
            protected override void Conditional(ConditionalExpression Conditional){
                //this.Traverse(Conditional.Test);
                //if(this.二度出現した一度目のExpression is not null) return;
                //var HashSet一度出現したExpression = this.HashSet一度出現したExpression;
                //var IfTrueまたはIfFalse外部か = this.IfTrueまたはIfFalse外部か;
                //this.IfTrueまたはIfFalse外部か=false;
                //var HashSet一度出現したExpression0 = this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                //this.Traverse(Conditional.IfTrue);
                //var HashSet一度出現したExpression1 = this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                //this.Traverse(Conditional.IfFalse);
                //HashSet一度出現したExpression0.IntersectWith(HashSet一度出現したExpression1);
                //var Dictionary一度出現したExpression_二度出現したExpressionの一度目 = this.Dictionary一度出現したExpression_二度出現したExpressionの一度目;
                //this.IfTrueまたはIfFalse外部か=IfTrueまたはIfFalse外部か;
                //if(IfTrueまたはIfFalse外部か){
                //    foreach(var 一度出現したExpression in HashSet一度出現したExpression0){
                //        if(Dictionary一度出現したExpression_二度出現したExpressionの一度目.TryGetValue(一度出現したExpression,
                //               out var 二度出現した一度目のExpression)){
                //            this.二度出現した一度目のExpression=二度出現した一度目のExpression;
                //            break;
                //        }
                //        Dictionary一度出現したExpression_二度出現したExpressionの一度目.Add(一度出現したExpression,一度出現したExpression);
                //    }
                //} else{
                //    HashSet一度出現したExpression.IntersectWith(HashSet一度出現したExpression0);
                //}

                var HashSet一度出現したExpression = this.HashSet一度出現したExpression;
                this.Traverse(Conditional.Test);
                if(this.二度出現した一度目のExpression is not null) return;
                //var HashSet一度出現したExpression = this.HashSet一度出現したExpression;
                //var IfTrueまたはIfFalse外部か= this.IfTrueまたはIfFalse外部か;
                //this.IfTrueまたはIfFalse外部か=false;
                //var HashSetIfTrue一度出現したExpression =this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                ////Debug.Assert(!(Conditional.IfTrue.NodeType==ExpressionType.Default&&Conditional.IfTrue.Type==typeof(void)));
                ////Debug.Assert(!(Conditional.IfFalse.NodeType==ExpressionType.Default&&Conditional.IfFalse.Type==typeof(void)));
                //this.IfTrueまたはIfFalse外部か=false;
                //this.Traverse(Conditional.IfTrue);
                //var HashSetIfFalse一度出現したExpression = this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                //this.Traverse(Conditional.IfFalse);
                //HashSetIfTrue一度出現したExpression.IntersectWith(HashSetIfFalse一度出現したExpression);
                //var Dictionary一度出現したExpression_二度出現したExpressionの一度目 = this.Dictionary一度出現したExpression_二度出現したExpressionの一度目;
                //if(IfTrueまたはIfFalse外部か)
                //    foreach(var 一度出現したExpression in HashSetIfTrue一度出現したExpression) {
                //        if(Dictionary一度出現したExpression_二度出現したExpressionの一度目.TryGetValue(一度出現したExpression,out var 二度出現した一度目のExpression)) {
                //            this.二度出現した一度目のExpression=二度出現した一度目のExpression;
                //            break;
                //        }
                //        Dictionary一度出現したExpression_二度出現したExpressionの一度目.Add(一度出現したExpression,一度出現したExpression);
                //    }
                //else
                //    HashSet一度出現したExpression.IntersectWith(HashSetIfTrue一度出現したExpression);
                var IfTrueがLabelで終わるか = Labelで終わるか(Conditional.IfTrue);
                if(IfTrueがLabelで終わるか==Labelで終わるか(Conditional.IfFalse)) {
                    var IfTrueまたはIfFalse外部か = this.IfTrueまたはIfFalse外部か;
                    var HashSet一度出現したExpression1 = this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                    this.IfTrueまたはIfFalse外部か=false;
                    this.Traverse(Conditional.IfTrue);
                    Debug.Assert(IfTrueがLabelで終わるか&&IfTrueまたはIfFalse外部か||!IfTrueがLabelで終わるか);
                    var HashSet一度出現したExpression2 = this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                    this.IfTrueまたはIfFalse外部か=false;
                    this.Traverse(Conditional.IfFalse);
                    Debug.Assert(IfTrueがLabelで終わるか&&IfTrueまたはIfFalse外部か||!IfTrueがLabelで終わるか);
                    HashSet一度出現したExpression1.IntersectWith(HashSet一度出現したExpression2);
                    var Dictionary一度出現したExpression_二度出現したExpressionの一度目 = this.Dictionary一度出現したExpression_二度出現したExpressionの一度目;
                    if(IfTrueまたはIfFalse外部か){
                        foreach(var 一度出現したExpression in HashSet一度出現したExpression1){
                            if(Dictionary一度出現したExpression_二度出現したExpressionの一度目.TryGetValue(一度出現したExpression,out var 二度出現した一度目のExpression)){
                                this.二度出現した一度目のExpression=二度出現した一度目のExpression;
                                break;
                            }
                            Dictionary一度出現したExpression_二度出現したExpressionの一度目.Add(一度出現したExpression,一度出現したExpression);
                        }
                    } else{
                        HashSet一度出現したExpression.IntersectWith(HashSet一度出現したExpression1);
                    }
                    this.IfTrueまたはIfFalse外部か=IfTrueまたはIfFalse外部か;
                }else{
                    var IfTrueまたはIfFalse外部か = this.IfTrueまたはIfFalse外部か;
                    var HashSet一度出現したExpression1 = this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                    this.IfTrueまたはIfFalse外部か=false;
                    Expression Labelで終わるExpression,Labelで終わらないExpression;
                    if(IfTrueがLabelで終わるか) {
                        //if(x){
                        //    goto Label;
                        //}else{
                        //    Default
                        //}
                        //xxxxx
                        Labelで終わるExpression=Conditional.IfTrue;
                        Labelで終わらないExpression=Conditional.IfFalse;
                    } else {
                        Labelで終わるExpression=Conditional.IfFalse;
                        Labelで終わらないExpression=Conditional.IfTrue;
                    }
                    Debug.Assert(!Labelで終わるか(Conditional.IfFalse));
                    this.Traverse(Labelで終わるExpression);
                    Debug.Assert(!this.IfTrueまたはIfFalse外部か);
                    this.HashSet一度出現したExpression=new HashSet<Expression>(this.ExpressionEqualityComparer);
                    this.Traverse(Labelで終わらないExpression);
                    Debug.Assert(!this.IfTrueまたはIfFalse外部か);
                    this.Stack_HashSet一度出現したExpression.Push((HashSet一度出現したExpression,HashSet一度出現したExpression1,IfTrueまたはIfFalse外部か));
                    //this.Stack_IfTrueまたはIfFalse外部か.Push(IfTrueまたはIfFalse外部か);
                }
            }
            private void 共通for_Dictionary一度出現したExpression_二度出現したExpressionの一度目(){
                foreach(var 一度出現したExpression in this.HashSet一度出現したExpression)
                    this.共通Dictionary一度出現したExpression_二度出現したExpressionの一度目(一度出現したExpression);
            }
            private void 共通Dictionary一度出現したExpression_二度出現したExpressionの一度目(Expression 一度出現したExpression){
                if(this.Dictionary一度出現したExpression_二度出現したExpressionの一度目.TryGetValue(一度出現したExpression,out var 二度出現した一度目のExpression))
                    this.二度出現した一度目のExpression=二度出現した一度目のExpression;
                else
                    this.Dictionary一度出現したExpression_二度出現したExpressionの一度目.Add(一度出現したExpression,一度出現したExpression);
            }
            protected override void Switch(SwitchExpression Switch){
                this.Traverse(Switch.SwitchValue);
            }
            protected override void Call(MethodCallExpression MethodCall) {
                var MethodCall0_Method = MethodCall.Method;
                if(this.IsInline&&ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall0_Method))) {
                    switch(MethodCall0_Method.Name) {
                        case nameof(Sets.Helpers.NoEarlyEvaluation): {
                            Debug.Assert(Reflection.Helpers.NoEarlyEvaluation==MethodCall0_Method.GetGenericMethodDefinition());
                            return;
                        }
                        case nameof(Sets.ExtensionSet.Except): {
                            Debug.Assert(
                                MethodCall.Arguments.Count==3
                                &&
                                Reflection.ExtensionEnumerable.Except_comparer==MethodCall0_Method.GetGenericMethodDefinition()
                                ||
                                MethodCall.Arguments.Count==2
                                &&(
                                    Reflection.ExtensionEnumerable.Except==MethodCall0_Method.GetGenericMethodDefinition()
                                    ||
                                    Reflection.ExtensionSet.Except==MethodCall0_Method.GetGenericMethodDefinition()
                                )
                            );
                            var MethodCall0_Arguments = MethodCall.Arguments;
                            this.Traverse(MethodCall0_Arguments[1]);
                            this.Traverse(MethodCall0_Arguments[0]);
                            return;
                        }
                        default:
                            Debug.Assert(nameof(Sets.ExtensionSet.Join)!=MethodCall0_Method.Name);
                            break;
                    }
                }
                base.Call(MethodCall);
            }
        }
        private readonly 取得_二度出現したExpression _取得_二度出現したExpression;
        private sealed class 判定_左辺Expressionsが含まれる:VoidExpressionTraverser_Quoteを処理しない {
            private readonly HashSet<Expression> 左辺Expressions;
            public 判定_左辺Expressionsが含まれる(ExpressionEqualityComparer ExpressionEqualityComparer){
                this.左辺Expressions=new(ExpressionEqualityComparer);
            }
            private bool 左辺Expressionが含まれる;
            public bool 実行(Expression e) {
                this.左辺Expressionが含まれる=false;
                //this.指定Parameter=指定Parameter;
                this.Traverse(e);
                return this.左辺Expressionが含まれる;
            }
            public void Clear() => this.左辺Expressions.Clear();
            public void Add(Expression Expression) => this.左辺Expressions.Add(Expression);
            protected override void Lambda(LambdaExpression Lambda) {
            }

            protected override void Traverse(Expression e){
                if(this.左辺Expressions.Contains(e))this.左辺Expressionが含まれる=true;
                else base.Traverse(e);
            }
        }
        private sealed class 変換_二度出現したExpression:ReturnExpressionTraverser{
            private readonly IReadOnlyDictionary<LabelTarget,List<Expression>> Dictionary_LabelTarget_Expressions;
            private readonly HashSet<LabelTarget> HashSet処理した=new();
            //private List<Expression>? ListExpression;
            private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
            private readonly ExpressionEqualityComparer _ExpressionEqualityComparer_Assign_Leftで比較;
            public 変換_二度出現したExpression(作業配列 作業配列,IReadOnlyDictionary<LabelTarget,List<Expression>>Dictionary_LabelTarget_Expressions,ExpressionEqualityComparer_Assign_Leftで比較 ExpressionEqualityComparer_Assign_Leftで比較,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる) : base(作業配列){
                this.Dictionary_LabelTarget_Expressions=Dictionary_LabelTarget_Expressions;
                this._ExpressionEqualityComparer_Assign_Leftで比較=ExpressionEqualityComparer_Assign_Leftで比較;
                this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
            }
            internal IEnumerable<ParameterExpression> ラムダ跨ぎParameters=default!;
            private Expression? 二度出現した一度目のExpression;
            private ParameterExpression? 二度目以降のParameter;
            private Expression? 一度目のAssign;
            /// <summary>
            /// Except argumentsを1,0の順で置換する。NoEvaluationで置換しないようにする
            /// </summary>
            internal bool IsInline=true;
            public Expression 実行(Expression Expression0,Expression 二度出現した一度目のExpression,ParameterExpression 二度目以降のParameter){
                this.置換中=false;
                this.このLabelが出現するまでスキップする=null;
                this.二度出現した一度目のExpression = 二度出現した一度目のExpression;
                this.判定_左辺Expressionsが含まれる.Clear();
                this.二度目以降のParameter = 二度目以降のParameter;
                this.一度目のAssign = Expression.Assign(二度目以降のParameter,二度出現した一度目のExpression);
                this.判定_左辺Expressionsが含まれる.Clear();
                this.HashSet処理した.Clear();
                var Expression1=this.Traverse(Expression0);
                return Expression1;
            }
            private LabelTarget? このLabelが出現するまでスキップする;
            //private enum E状態{
            //    置換前,
            //    置換中,
            //    置換後
            //};
            //private E状態 状態=E状態.置換前;
            private bool 置換中;
            private List<Expression>.Enumerator Enumerator=new List<Expression>().GetEnumerator();
            protected override Expression Traverse(Expression Expression0) {
                //if(this.判定_左辺Expressionsが含まれる.実行(Expression0)){
                //    Expression0=base.Traverse(Expression0);
                //}
                switch(Expression0.NodeType){
                    case ExpressionType.DebugInfo or ExpressionType.Default or ExpressionType.Lambda or ExpressionType.PostDecrementAssign or ExpressionType.PostIncrementAssign or ExpressionType.PreIncrementAssign or ExpressionType.PreDecrementAssign or ExpressionType.Throw:
                        return Expression0;
                    case ExpressionType.Goto:{
                        //var IfTrueまたはIfFalse外部か=this.Stack_IfTrueまたはIfFalse外部か.Pop();
                        var Goto=(GotoExpression)Expression0;
                        this.このLabelが出現するまでスキップする=Goto.Target;
                        this.置換中=false;
                        //this.Enumerator=this.Dictionary_LabelTarget_Expressions[Goto.Target].GetEnumerator();
                        //return base.Traverse(Expression0);
                        if(!this.HashSet処理した.Contains(Goto.Target)){
                            this.HashSet処理した.Add(Goto.Target);
                            var ListExpression0=this.Dictionary_LabelTarget_Expressions[Goto.Target];
                            var ListExpression0_Count=ListExpression0.Count;
                            for(var a=0;a<ListExpression0_Count;a++)
                                ListExpression0[a]=this.Traverse(ListExpression0[a]);
                        }
                        //return Goto;
                        return base.Traverse(Expression0);
                    }
                    case ExpressionType.Block:{
                        if(this.置換中&&this.Enumerator.MoveNext())
                            return this.Enumerator.Current;
                        this.このLabelが出現するまでスキップする=null;
                        break;
                    }
                    case ExpressionType.Label:{
                        var Label=(LabelExpression)Expression0;
                        this.置換中=Label.Target==this.このLabelが出現するまでスキップする;
                        return this.Label(Label);
                    }
                    case ExpressionType.Assign: {
                        var Assign0 = (BinaryExpression)Expression0;
                        var Assign0_Left = Assign0.Left;
                        var Assign1_Left = base.Traverse(Assign0_Left);
                        this.判定_左辺Expressionsが含まれる.Add(Assign0_Left);
                        var Assign1_Right=this.Traverse(Assign0.Right);
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
                        if(!this.ラムダ跨ぎParameters.Contains(Expression0))return Expression0;
                        break;
                    }
                }
                if(Expression0.NodeType!=ExpressionType.Block){
                    if(this.置換中&&this.Enumerator.MoveNext())
                        return this.Enumerator.Current;
                    this.このLabelが出現するまでスキップする=null;
                }
                //if
                //    a*b
                //a*b
                //a*b
                //↓
                //if
                //    a*b
                //t=a*b
                //t
                //に置換される。理想は↓
                //if
                //    t=a*b
                //t
                //t
                //しかし現状は出来ない
                if(this.一度目のAssign is not null){
                    if(Expression0==this.二度出現した一度目のExpression){
                        var 一度目のAssign=this.一度目のAssign;
                        this.一度目のAssign=null;
                        return 一度目のAssign;
                    }
                }else if(this._ExpressionEqualityComparer_Assign_Leftで比較.Equals(Expression0,this.二度出現した一度目のExpression)){
                    return this.二度目以降のParameter!;
                    //return this.判定_左辺Expressionsが含まれる.実行(Expression0)?Expression0:this.二度目以降のParameter!;
                }
                return base.Traverse(Expression0);
            }
            protected override Expression Block(BlockExpression Block0){
                var Block0_Expressions = Block0.Expressions;
                var Block0_Expressions_Count = Block0_Expressions.Count;
                var Block1_Expressions=new Expression[Block0_Expressions_Count];
                for(var a=0;a<Block0_Expressions_Count;a++){
                    var Block0_Expression=Block0_Expressions[a];
                    if(Block0_Expression is LabelExpression Label){
                        Block1_Expressions[a]=this.Label(Label);
                        this.置換中=Label.Target==this.このLabelが出現するまでスキップする;
                    }else if(Block0_Expression.NodeType!=ExpressionType.Block&&this.置換中&&this.Enumerator.MoveNext()){
                        Block1_Expressions[a]=this.Enumerator.Current;
                    } else{
                        Block1_Expressions[a]=this.Traverse(Block0_Expression);
                    }
                    //if(Block0_Expression is LabelExpression Label){
                    //    Block1_Expressions[a]=this.Label(Label);
                    //    this.置換中=Label.Target==this.このLabelが出現するまでスキップする;
                    //}else if(Block0_Expression.NodeType!=ExpressionType.Block){
                    //    if(this.置換中&&this.Enumerator.MoveNext()){
                    //        Block1_Expressions[a]=this.Enumerator.Current!;
                    //    } else{
                    //        this.このLabelが出現するまでスキップする=null;
                    //        Block1_Expressions[a]=this.Traverse(Block0_Expression);
                    //    }
                    //}
                }
                return Expression.Block(Block0.Type,Block0.Variables,Block1_Expressions);
            }
            protected override Expression Call(MethodCallExpression MethodCall0) {
                var MethodCall0_Method = MethodCall0.Method;
                if(this.IsInline&&ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall0_Method))) {
                    switch(MethodCall0_Method.Name) {
                        case nameof(Sets.Helpers.NoEarlyEvaluation): {
                            Debug.Assert(Reflection.Helpers.NoEarlyEvaluation==MethodCall0_Method.GetGenericMethodDefinition());
                            return MethodCall0;
                        }
                        case nameof(Sets.ExtensionSet.Except): {
                            Debug.Assert(
                                MethodCall0.Arguments.Count==3
                                &&
                                Reflection.ExtensionEnumerable.Except_comparer==MethodCall0_Method.GetGenericMethodDefinition()
                                ||
                                MethodCall0.Arguments.Count==2
                                &&(
                                    Reflection.ExtensionEnumerable.Except==MethodCall0_Method.GetGenericMethodDefinition()
                                    ||
                                    Reflection.ExtensionSet.Except==MethodCall0_Method.GetGenericMethodDefinition()
                                )
                            );
                            var MethodCall0_Arguments = MethodCall0.Arguments;
                            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            return MethodCall0.Arguments.Count==3
                                ? Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,this.Traverse(MethodCall0_Arguments[2]))
                                : Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
                        }
                        default:
                            Debug.Assert(nameof(Sets.ExtensionSet.Join)!=MethodCall0_Method.Name);
                            break;
                    }
                }
                return base.Call(MethodCall0);
            }
            protected override Expression Lambda(LambdaExpression Lambda0) => Lambda0;
        }
        private readonly 変換_二度出現したExpression _変換_二度出現したExpression;
        private sealed class 変換_フロー:ReturnExpressionTraverser{
            private readonly IReadOnlyDictionary<LabelTarget,List<Expression>> Dictionary_LabelTarget_Expressions;
            private static readonly List<Expression>ListEmpty=new();
            public 変換_フロー(作業配列 作業配列,IReadOnlyDictionary<LabelTarget,List<Expression>>Dictionary_LabelTarget_Expressions):base(作業配列){
                this.Dictionary_LabelTarget_Expressions=Dictionary_LabelTarget_Expressions;
            }
            private List<Expression>ListExpression=ListEmpty;
            private int index;
            private List<Expression>.Enumerator Enumerator;
            public Expression 実行(Expression Expression0){
                this.index=0;
                this.Enumerator=ListEmpty.GetEnumerator();
                var Expression1=this.Traverse(Expression0);
                Debug.Assert(!this.Enumerator.MoveNext());
                return Expression1;
            }
            protected override Expression Block(BlockExpression Block0){
                var Block0_Expressions = Block0.Expressions;
                var Block0_Expressions_Count = Block0_Expressions.Count;
                var Block1_Expressions=new Expression[Block0_Expressions_Count];
                for(var a=0;a<Block0_Expressions_Count;a++){
                    var Block0_Expression=Block0_Expressions[a];
                    if(Block0_Expression.NodeType!=ExpressionType.Block&&this.Enumerator.MoveNext()){
                        this.index++;
                        Debug.Assert(Block0_Expression.NodeType!=ExpressionType.Label);
                        Block1_Expressions[a]=this.Traverse(this.Enumerator.Current);
                    } else{
                        Block1_Expressions[a]=this.Traverse(Block0_Expression);
                    }
                }
                return Expression.Block(Block0.Type,Block0.Variables,Block1_Expressions);
            }
            protected override Expression Traverse(Expression Expression0) {
                if(Expression0.NodeType==ExpressionType.Label){
                    this.ListExpression=this.Dictionary_LabelTarget_Expressions[((LabelExpression)Expression0).Target];
                    this.Enumerator=this.Dictionary_LabelTarget_Expressions[((LabelExpression)Expression0).Target].GetEnumerator();
                    return Expression0;
                }
                return base.Traverse(Expression0);
            }
        }
        private readonly 変換_フロー _変換_フロー;
        private readonly List<ParameterExpression> ListスコープParameter;
        internal 変換_局所Parameterの先行評価(作業配列 作業配列,List<ParameterExpression> ListスコープParameter,ExpressionEqualityComparer_Assign_Leftで比較 ExpressionEqualityComparer_Assign_Leftで比較) : base(作業配列){
            var 判定_左辺Parametersが含まれる=new 判定_左辺Expressionsが含まれる(ExpressionEqualityComparer_Assign_Leftで比較);
            var Dictionary_LabelTarget_Expressions=new Dictionary<LabelTarget,List<Expression>>();
            this._取得_Labelに対応するExpressions=new(Dictionary_LabelTarget_Expressions);
            this._取得_二度出現したExpression=new(Dictionary_LabelTarget_Expressions,ExpressionEqualityComparer_Assign_Leftで比較,判定_左辺Parametersが含まれる);
            this._変換_二度出現したExpression=new(作業配列,Dictionary_LabelTarget_Expressions,ExpressionEqualityComparer_Assign_Leftで比較,判定_左辺Parametersが含まれる);
            this._変換_フロー=new(作業配列,Dictionary_LabelTarget_Expressions);
            this.ListスコープParameter=ListスコープParameter;
        }
        internal IEnumerable<ParameterExpression> ラムダ跨ぎParameters{
            set{
                this._取得_二度出現したExpression.ラムダ跨ぎParameters=value;
                this._変換_二度出現したExpression.ラムダ跨ぎParameters=value;
            }
        }
        private List<ParameterExpression> Block_Variables = new();
        private int 番号;
        /// <summary>
        /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
        /// </summary>
        public bool IsInline {
            get=>this._変換_二度出現したExpression.IsInline;
            set{
                this._取得_二度出現したExpression.IsInline=value;
                this._変換_二度出現したExpression.IsInline=value;
            }
        }
        public Expression 実行(Expression Expression0) {
            Debug.Assert(Expression0.NodeType==ExpressionType.Lambda&&this.ListスコープParameter.Count==0);
            this._取得_Labelに対応するExpressions.実行(Expression0);
            this.番号=0;
            var Lambda1 = (LambdaExpression)this.Traverse(Expression0);
            //var Lambda2=Lambda1;
            var Lambda2=(LambdaExpression)this._変換_フロー.実行(Lambda1);
            return Expression.Lambda(Lambda2.Type,Lambda2.Body,Lambda2.Name,Lambda2.TailCall,Lambda2.Parameters);
        }

        protected override Expression Assign(BinaryExpression Assign0)=>Expression.Assign(
            this.Traverse(Assign0.Left),
            this.Traverse(Assign0.Right)
        );
        protected override Expression Invoke(InvocationExpression Invocation0) {
            //評価順序を既定と変える。
            //a.Invoke(b)をb→aの順で評価したい。
            var Invocation0_Arguments=Invocation0.Arguments;
            var Invocation1_Arguments = this.TraverseExpressions(Invocation0_Arguments);
            var Invocation0_Expression = Invocation0.Expression;
            var Invocation1_Expression = this.Traverse(Invocation0_Expression);
            if(ReferenceEquals(Invocation0_Arguments,Invocation1_Arguments)&&Invocation0_Expression==Invocation1_Expression) return Invocation0;
            return Expression.Invoke(Invocation1_Expression,Invocation1_Arguments);
        }
        protected override Expression Lambda(LambdaExpression Lambda0){
            var ListスコープParameter = this.ListスコープParameter;
            var ListスコープParameter_Count = ListスコープParameter.Count;
            var Lambda0_Parameters = Lambda0.Parameters;
            ListスコープParameter.AddRange(Lambda0_Parameters);
            var Block0_Variables = this.Block_Variables;
            var Block1_Variables = this.Block_Variables=new List<ParameterExpression>();
            var Lambda1_Body = Lambda0.Body;
            var BlockVariables = this.Block_Variables;
            var 取得_二度出現したExpression = this._取得_二度出現したExpression;
            var 変換_旧Expressionを新Expressionに二度置換 = this._変換_二度出現したExpression;
            while(true) {
                var (二度出現した一度目のExpression,フローを走査した)=取得_二度出現したExpression.実行(Lambda1_Body);
                if(二度出現した一度目のExpression is null) break;
                var Variable = Expression.Variable(二度出現した一度目のExpression.Type,$"局所{this.番号++}");
                BlockVariables.Add(Variable);
                ListスコープParameter.Add(Variable);
                var Expression0 = 変換_旧Expressionを新Expressionに二度置換.実行(Lambda1_Body,二度出現した一度目のExpression,Variable);
                Lambda1_Body=Expression0;
            }
            var Lambda2_Body = this.Traverse(Lambda1_Body);
            this.Block_Variables=Block0_Variables;
            if(Block1_Variables.Count>0) Lambda2_Body=Expression.Block(Block1_Variables,this._作業配列.Expressions設定(Lambda2_Body));
            var Lambda1 = Expression.Lambda(Lambda0.Type,Lambda2_Body,Lambda0.Name,Lambda0.TailCall,Lambda0_Parameters);
            ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
            return Lambda1;
        }
        protected override Expression Conditional(ConditionalExpression Conditional0){
            var Conditional0_Test=Conditional0.Test;
            var Conditional0_IfTrue=Conditional0.IfTrue;
            var Conditional0_IfFalse=Conditional0.IfFalse;
            var Conditional1_Test=this.Traverse(Conditional0_Test);
            var Conditional1_IfTrue=共通(Conditional0_IfTrue);
            var Conditional1_IfFalse=共通(Conditional0_IfFalse);
            if(Conditional0_Test==Conditional1_Test && Conditional0_IfTrue==Conditional1_IfTrue && Conditional0_IfFalse==Conditional1_IfFalse)return Conditional0;
            return Expression.Condition(Conditional1_Test,Conditional1_IfTrue,Conditional1_IfFalse,Conditional0.Type);
            Expression 共通(Expression Expression0){
                var Block_Variables=this.Block_Variables;
                var 取得_二度出現したExpression = this._取得_二度出現したExpression;
                var 変換_旧Expressionを新Expressionに二度置換 = this._変換_二度出現したExpression;
                while(true) {
                    var (二度出現した一度目のExpression,フローを走査した)=取得_二度出現したExpression.実行(Expression0);
                    if(二度出現した一度目のExpression is null)break;
                    var Variable=Expression.Variable(二度出現した一度目のExpression.Type,$"局所{this.番号++}");
                    Block_Variables.Add(Variable);
                    var Expression1=変換_旧Expressionを新Expressionに二度置換.実行(Expression0,二度出現した一度目のExpression,Variable);
                    Expression0=Expression1;
                }
                return this.Traverse(Expression0);
            }
        }
    }
}