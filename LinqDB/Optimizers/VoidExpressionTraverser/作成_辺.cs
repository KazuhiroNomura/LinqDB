using System.Linq;
using Generic = System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.ReturnExpressionTraverser;
namespace LinqDB.Optimizers.VoidExpressionTraverser;
using static Common;
internal sealed class 作成_辺(辺 Top辺,List辺 List辺,Generic.Dictionary<LabelTarget,辺> Dictionary_LabelTarget_辺,ExpressionEqualityComparer ExpressionEqualityComparer):VoidExpressionTraverser_Quoteを処理しない{
    internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
    private 辺 辺=default!;
    private int 辺番号;
    public void 実行(Expression Expression0){
        this.辺番号=0;
        Dictionary_LabelTarget_辺.Clear();
        Top辺.Clear();
        List辺.Clear();
        List辺.Add(Top辺);
        this.辺=Top辺;
        this.Traverse(Expression0);
    }
    internal string Analize=>List辺.Analize;
    /// <summary>
    /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
    /// </summary>
    internal bool IsInline=true;
    protected override void Traverse(Expression Expression){
        switch(Expression.NodeType){
            case ExpressionType.DebugInfo:
            case ExpressionType.Default:
            case ExpressionType.Lambda:
            case ExpressionType.PostDecrementAssign:
            case ExpressionType.PostIncrementAssign:
            case ExpressionType.PreDecrementAssign:
            case ExpressionType.PreIncrementAssign:
            case ExpressionType.Throw:
                return;
            case ExpressionType.Call:{
                if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression).Method)) return;
                break;
            }
            case ExpressionType.Constant:{
                if(ILで直接埋め込めるか((ConstantExpression)Expression)) return;
                break;
            }
            case ExpressionType.Parameter:{
                if(this.ラムダ跨ぎParameters.Contains(Expression)) break;
                return;
            }
        }
        base.Traverse(Expression);
    }
    protected override void Conditional(ConditionalExpression Conditional){
        //test　　　　0 If test
        //├────┐2 True IfTrue
        //ifTrue　　│
        //└───┐│  goto 1 End If
        //┌───┼┘3 ifFalse:
        //ifFalse │　
        //├───┘　1 End If:
        var Conditional_Test=Conditional.Test;
        var Conditional_IfTrue=Conditional.IfTrue;
        var Conditional_IfFalse=Conditional.IfFalse;
        this.Traverse(Conditional_Test);
        var IfTest辺=this.辺;
        IfTest辺.子コメント=$"IfTest({Conditional_Test})";
        var EndIf辺=new 辺(ExpressionEqualityComparer){親コメント="end if"};
        var IfTrue=this.辺=new(ExpressionEqualityComparer,IfTest辺,++this.辺番号){親コメント=$"IfTrue {Conditional_IfTrue}"};
        List辺.Add(IfTrue);
        this.Traverse(Conditional_IfTrue);
        辺.接続(this.辺,EndIf辺);
        List辺.Add(this.辺=new 辺(ExpressionEqualityComparer,IfTest辺,++this.辺番号){親コメント=$"IfFalse {Conditional_IfFalse}"});
        this.Traverse(Conditional_IfFalse);
        辺.接続(this.辺,EndIf辺);
        List辺.Add(this.辺=this.辺=EndIf辺);
        EndIf辺.辺番号=++this.辺番号;
    }
    protected override void Goto(GotoExpression Goto){
        if(Goto.Value is not null) this.Traverse(Goto.Value);
        var デッドコード=new 辺(ExpressionEqualityComparer){親コメント="デッドコード先頭",子コメント="デッドコード末尾",デッドコードか=true};
        var 移動元=this.辺;
        移動元.子コメント=$"goto({Goto.Value}){Goto.Target.Name}";
        if(Dictionary_LabelTarget_辺.TryGetValue(Goto.Target,out var 移動先)){
            //├┐  0 上辺:
            //..................
            //└┘    goto 上辺:←ここ
            //.Label
            //.LabelTarget 上辺:;
            //..................
            //.Goto 上辺 { };    
            辺.接続(移動元,移動先);
        } else{
            //下にジャンプ。条件分岐で多い。
            //└┐0 goto 下:←new 辺に関する情報
            //││          ←new 辺に関する情報 デッドコード
            //Dictionary_LabelTarget_辺.Add(Goto.Target,new 辺(ExpressionEqualityComparer,移動元){ 親コメント=$"goto({Goto.Value}){Goto.Target.Name}"});
            Dictionary_LabelTarget_辺.Add(Goto.Target,new 辺(ExpressionEqualityComparer,移動元,++this.辺番号));
        }
        List辺.Add(this.辺=デッドコード);
    }
    protected override void Label(LabelExpression Label){
        if(Label.DefaultValue is not null) this.Traverse(Label.DefaultValue);
        var 移動元=this.辺;
        if(Dictionary_LabelTarget_辺.TryGetValue(Label.Target,out var 移動先)){
            //gotoで指定したラベルでまだ定義されてない奴
            //└┐0 goto 下
            //..................
            //┌┘1 下:←ここ
            移動先.親コメント=$"({Label.DefaultValue}){Label.Target.Name}:";
            List辺.Add(移動先);
            this.辺=移動先;
        } else{
            //始めて出現。後でgoto命令で飛んでループを形成する
            //移動元.子コメント=$"goto({Label.DefaultValue}){Label.Target.Name!}";
            //├←┐    1 L1:←ここ
            this.辺=移動先=new 辺(ExpressionEqualityComparer,移動元,++this.辺番号){親コメント=$"({Label.DefaultValue}){Label.Target.Name}:"};
            List辺.Add(移動先);
            Dictionary_LabelTarget_辺.Add(Label.Target,移動先);
        }
        移動先.辺番号=++this.辺番号;
    }
    protected override void Assign(BinaryExpression Assign){
        var Assign_Left=Assign.Left;
        if(Assign_Left.NodeType is not ExpressionType.Parameter) base.Traverse(Assign_Left);//.static_field,array[index]など
        this.Traverse(Assign.Right);
    }
    protected override void Loop(LoopExpression Loop){
        var 外辺=this.辺;
        辺 Loop辺;
        if(Loop.ContinueLabel is not null){
            Loop辺=new 辺(ExpressionEqualityComparer,外辺,++this.辺番号){親コメント=$"Loop {Loop.ContinueLabel.Name}:"};
            Dictionary_LabelTarget_辺.Add(Loop.ContinueLabel,Loop辺);
        } else
            Loop辺=new 辺(ExpressionEqualityComparer,外辺,++this.辺番号){親コメント="Loop"};
        List辺.Add(Loop辺);
        this.辺=Loop辺;
        this.Traverse(Loop.Body);
        var EndLoop辺=this.辺;
        辺.接続(EndLoop辺,Loop辺);
        EndLoop辺.子コメント="EndLoop";
        if(Loop.BreakLabel is not null){
            var LoopBreak辺=Dictionary_LabelTarget_辺[Loop.BreakLabel];
            LoopBreak辺.親コメント=$"LoopBreak {Loop.BreakLabel.Name}:";
            List辺.Add(LoopBreak辺);
            this.辺=LoopBreak辺;
        }
    }
    protected override void Switch(SwitchExpression Switch){
        this.Traverse(Switch.SwitchValue);
        var SwitchValue辺=this.辺;
        SwitchValue辺.子コメント=$"begin switch {SwitchValue辺}";
        var sb=new StringBuilder();
        var Cases=Switch.Cases;
        var Cases_Count=Cases.Count;
        var EndSwitch辺=new 辺(ExpressionEqualityComparer){親コメント="end switch"};
        for(var a=0;a<Cases_Count;a++){
            var Case=Cases[a];
            sb.Append("case ");
            foreach(var TestValue in Case.TestValues){
                sb.Append(TestValue);
                sb.Append(',');
            }
            sb[^1]=':';
            var Body=Case.Body;
            sb.Append(Body);
            List辺.Add(this.辺=new(ExpressionEqualityComparer,SwitchValue辺,++this.辺番号){親コメント=sb.ToString()});
            this.Traverse(Body);
            辺.接続(this.辺,EndSwitch辺);
            sb.Clear();
        }
        var DefaultBody=Switch.DefaultBody;
        List辺.Add(this.辺=new(ExpressionEqualityComparer,SwitchValue辺,++this.辺番号){親コメント=$"default:{DefaultBody}"});
        this.Traverse(DefaultBody);
        辺.接続(this.辺,EndSwitch辺);
        List辺.Add(this.辺=EndSwitch辺);
    }
    protected override void Try(TryExpression Try){
        var 外辺=this.辺;
        var EndTry=new 辺(ExpressionEqualityComparer){親コメント="end try"};
        共通("try");
        this.Traverse(Try.Body);
        var Body=this.辺;
        Body.子コメント=$"try {Body}";
        var sb=new StringBuilder();
        var Try_Handlers=Try.Handlers;
        var Try_Handlers_Count=Try_Handlers.Count;
        for(var a=0;a<Try_Handlers_Count;a++){
            var Try_Handle=Try_Handlers[a];
            sb.Append($"catch({Try_Handle.Test.FullName}");
            if(Try_Handle.Variable is not null) sb.Append($"{Try_Handle.Variable.Name}");
            var Handler_Body=Try_Handle.Body;
            sb.Append(')').Append(Handler_Body);
            共通(sb.ToString());
            this.Traverse(Handler_Body);
            sb.Clear();
        }
        var Try_Fault=Try.Fault;
        if(Try_Fault is not null){
            共通($"fault {Try_Fault}");
            this.Traverse(Try_Fault);
        } else{
            var Try_Finally=Try.Finally;
            if(Try_Finally is not null){
                共通($"finally {Try_Finally}");
                this.Traverse(Try_Finally);
            }
        }
        List辺.Add(this.辺=EndTry);
        void 共通(string 親コメント){
            var 辺=this.辺=new 辺(ExpressionEqualityComparer,外辺,++this.辺番号){親コメント=親コメント};
            List辺.Add(this.辺=辺);
            辺.接続(辺,EndTry);
        }
    }
    protected override void Call(MethodCallExpression MethodCall){
        if(this.IsInline){
            if(ループ展開可能メソッドか(MethodCall)){
                if(nameof(Sets.ExtensionSet.Except)==MethodCall.Method.Name){
                    var MethodCall0_Arguments=MethodCall.Arguments;
                    this.Traverse(MethodCall0_Arguments[1]);
                    this.Traverse(MethodCall0_Arguments[0]);
                    return;
                }
            }
        }
        base.Call(MethodCall);
    }
}
//20240122 228
