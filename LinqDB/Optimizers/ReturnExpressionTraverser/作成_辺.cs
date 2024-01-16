using System.Linq;
using Generic=System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.VoidExpressionTraverser;



namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;





internal sealed class 作成_辺(辺 Top辺,List辺 List辺,Generic.Dictionary<LabelTarget,辺> Dictionary_LabelTarget_辺,ExpressionEqualityComparer ExpressionEqualityComparer):VoidExpressionTraverser_Quoteを処理しない{
    internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
    private 辺 辺=default!;
    private int 計算量;







































    private int 辺番号;
    public void 実行(Expression Expression0){
        this.計算量=0;
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
        this.計算量++;
        //Debug.Assert(Expression.NodeType is not(ExpressionType.AndAlso or ExpressionType.OrElse));
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
            //case ExpressionType.Label:{
            //    //:が出たら新たな辺
            //    var Label=(LabelExpression)Expression;
            //    if(Label.DefaultValue is not null)
            //        this.Traverse(Label.DefaultValue);
            //    if(Dictionary_LabelTarget_辺.TryGetValue(Label.Target,out var 移動先)){
            //        //gotoで指定したラベルでまだ定義されてない奴
            //        //└┐0 goto 下
            //        //..................
            //        //┌┘1 下:←ここ
            //        移動先.親コメント=$"({Label.DefaultValue}){Label.Target.Name}:";
            //        List辺.Add(移動先);
            //        this.辺=移動先;
            //    } else{
            //        //始めて出現。後でgoto命令で飛んでループを形成する
            //        var 移動元=this.辺;
            //        移動元.子コメント="Label2";//$"goto({Label.DefaultValue}){Goto.Target.Name! }"};
            //        //├←┐    1 L1:←ここ
            //        this.辺=移動先=new 辺(ExpressionEqualityComparer, 移動元){親コメント=$"({Label.DefaultValue}){ Label.Target.Name}:"};
            //        List辺.Add(移動先);
            //        Dictionary_LabelTarget_辺.Add(Label.Target,移動先);
            //    }
            //    return;
            //}
            //case ExpressionType.Goto:{
            //    //gotoが出たら次は新たな辺
            //    var Goto=(GotoExpression)Expression;
            //    if(Goto.Value is not null)
            //        this.Traverse(Goto.Value);
            //    var デッドコード=new 辺(ExpressionEqualityComparer){子コメント="デッドコード"};
            //    if(Dictionary_LabelTarget_辺.TryGetValue(Goto.Target,out var 上辺)){
            //        //├┐  0 上辺:
            //        //..................
            //        //└┘    goto 上辺:←ここ
            //        //.Label
            //        //.LabelTarget 上辺:;
            //        //..................
            //        //.Goto 上辺 { };    
            //        辺.接続(this.辺,上辺);
            //    } else {
            //        //下にジャンプ。条件分岐で多い。
            //        //└┐0 goto 下:←new 辺に関する情報
            //        //││          ←new 辺に関する情報 デッドコード
            //        Dictionary_LabelTarget_辺.Add(Goto.Target,new 辺(ExpressionEqualityComparer,this.辺) { 子コメント=$"goto({Goto.Value}){Goto.Target.Name}"});
            //        //this.Dictionary_LabelTarget_辺に関する情報.Add(Goto.Target,new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号,this.辺に関する情報) { 親コメント=$"({Label.DefaultValue}){ Label.Target.Name}:"};
            //    }
            //    List辺.Add(this.辺=デッドコード);
            //    return;
            //}
            //case ExpressionType.Loop:{
            //    var Loop=(LoopExpression)Expression;
            //    var Begin_Loop=this.辺;
            //    Begin_Loop.子コメント="Begin Loop";
            //    var LoopBody0=this.辺=new(ExpressionEqualityComparer,Begin_Loop){親コメント="Loop"};
            //    List辺.Add(LoopBody0);
            //    this.Traverse(Loop.Body);
            //    var LoopBody1=this.辺;
            //    //辺.接続(LoopBody0,LoopBody1);
            //    辺.接続(LoopBody1,LoopBody0);
            //    //var EndLoop=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,this.辺に関する情報){親コメント="End Loop"};
            //    //List辺に関する情報.Add(EndLoop);
            //    if(Loop.BreakLabel is not null){
            //        var 移動先=Dictionary_LabelTarget_辺[Loop.BreakLabel];
            //        //gotoで指定したラベルでまだ定義されてない奴
            //        //└┐0 goto 下
            //        //..................
            //        //┌┘1 下:←ここ
            //        //変換_局所Parameterの先行評価.辺.接続(LoopBody1,);
            //        //LoopBody1.List子辺.Add(移動先);
            //        移動先.親コメント=$"End Loop {Loop.BreakLabel.Name}:";
            //        List辺.Add(移動先);
            //        this.辺=移動先;
            //    } else{
            //        List辺.Add(this.辺=new 辺(ExpressionEqualityComparer){子コメント="End Loop"});
            //    }
            //    return;
            //}
            //case ExpressionType.Assign: {
            //    var Assign = (BinaryExpression)Expression;
            //    var Assign_Left = Assign.Left;
            //    if(Assign_Left.NodeType is ExpressionType.Parameter) {
            //    } else
            //        base.Traverse(Assign_Left);//.static_field,array[ここ]など
            //    this.Traverse(Assign.Right);
            //    //if(Assign.Left is not ParameterExpression)
            //    //    base.Traverse(Assign.Left);
            //    //this.Traverse(Assign.Right);
            //    return;
            //}
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
        //if(判定_左辺Expressionsが含まれる.実行(Expression))
        //    return;
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
        var If = this.辺;
        If.子コメント=$"begin if {Conditional_Test}";
        var Endif=new 辺(ExpressionEqualityComparer){親コメント="end if"};
        //var List辺=this.List辺;
        var IfTrue=this.辺=new(ExpressionEqualityComparer,If){親コメント=$"true {Conditional_IfTrue}"};
        List辺.Add(IfTrue);
        this.Traverse(Conditional_IfTrue);
        辺.接続(this.辺,Endif);
        List辺.Add(this.辺=new 辺(ExpressionEqualityComparer,If) { 親コメント=$"false {Conditional_IfFalse}" });
        this.Traverse(Conditional_IfFalse);
        辺.接続(this.辺,Endif);
        List辺.Add(this.辺=this.辺=Endif);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    protected override void Goto(GotoExpression Goto){
        if(Goto.Value is not null)
            this.Traverse(Goto.Value);
        var デッドコード=new 辺(ExpressionEqualityComparer){親コメント = "デッドコード先頭",子コメント="デッドコード末尾"};
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
        } else {
            //下にジャンプ。条件分岐で多い。
            //└┐0 goto 下:←new 辺に関する情報
            //││          ←new 辺に関する情報 デッドコード
            //this.辺.子コメント=$"goto({Goto.Value}){Goto.Target.Name}";
            //Dictionary_LabelTarget_辺.Add(Goto.Target,new 辺(ExpressionEqualityComparer,this.辺) { 子コメント=$"goto({Goto.Value}){Goto.Target.Name}"});
            Dictionary_LabelTarget_辺.Add(Goto.Target,new 辺(ExpressionEqualityComparer,移動元){ 子コメント=$"goto({Goto.Value}){Goto.Target.Name}"});
            //Dictionary_LabelTarget_辺.Add(Goto.Target,new 辺(ExpressionEqualityComparer));
        }
        List辺.Add(this.辺=デッドコード);
    }
    protected override void Label(LabelExpression Label){
        if(Label.DefaultValue is not null)
            this.Traverse(Label.DefaultValue);
        var 移動元=this.辺;
        if(Dictionary_LabelTarget_辺.TryGetValue(Label.Target,out var 移動先)){
            //gotoで指定したラベルでまだ定義されてない奴
            //└┐0 goto 下
            //..................
            //┌┘1 下:←ここ
            移動先.親コメント=$"({Label.DefaultValue}){Label.Target.Name}:";
            List辺.Add(移動先);
            //辺.接続(移動元,移動先);
            this.辺=移動先;
        } else{
            //始めて出現。後でgoto命令で飛んでループを形成する
            //移動元.子コメント=$"goto({Label.DefaultValue}){Label.Target.Name!}";
            //├←┐    1 L1:←ここ
            this.辺=移動先=new 辺(ExpressionEqualityComparer, 移動元){親コメント=$"({Label.DefaultValue}){ Label.Target.Name}:"};
            //this.辺=移動先=new 辺(ExpressionEqualityComparer){親コメント=$"({Label.DefaultValue}){ Label.Target.Name}:"};
            //this.辺=移動先=new 辺(ExpressionEqualityComparer, 移動元);
            List辺.Add(移動先);
            Dictionary_LabelTarget_辺.Add(Label.Target,移動先);
        }
        移動先.辺番号=++this.辺番号;
    }
    
    
    
    
    
    
    
    
    protected override void Assign(BinaryExpression Assign){
        var Assign_Left = Assign.Left;
        if(Assign_Left.NodeType is not ExpressionType.Parameter)
            base.Traverse(Assign_Left);//.static_field,array[index]など
        this.Traverse(Assign.Right);
    }











































































































































































































































































    protected override void Loop(LoopExpression Loop){
        var 外辺=this.辺;
        //外辺.子コメント="Begin Loop";
        辺 Loop辺0;
        if(Loop.ContinueLabel is not null){
            Loop辺0=new 辺(ExpressionEqualityComparer,外辺){親コメント=$"Loop {Loop.ContinueLabel.Name}:"};
            Dictionary_LabelTarget_辺.Add(Loop.ContinueLabel,Loop辺0);
        } else
            Loop辺0=new 辺(ExpressionEqualityComparer,外辺){親コメント="Loop"};
        List辺.Add(Loop辺0);
        this.辺=Loop辺0;
        //辺.接続(外辺,LoopBody0);
        this.Traverse(Loop.Body);
        var Loop辺1=this.辺;
        //辺.接続(LoopBody0,LoopBody1);
        辺.接続(Loop辺1,Loop辺0);
        Loop辺1.子コメント="End Loop";
        //var EndLoop=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,this.辺に関する情報){親コメント="End Loop"};
        //List辺に関する情報.Add(EndLoop);
        if(Loop.BreakLabel is not null){
            var Break辺=Dictionary_LabelTarget_辺[Loop.BreakLabel];
            Break辺.親コメント=$"Loop Break {Loop.BreakLabel.Name}:";
            List辺.Add(Break辺);
            this.辺=Break辺;
        }
    }
    protected override void Switch(SwitchExpression Switch){
        this.Traverse(Switch.SwitchValue);
        var SwitchValue = this.辺;
        SwitchValue.子コメント=$"begin switch {SwitchValue}";
        var sb=new StringBuilder();
        var Cases=Switch.Cases;
        var Cases_Count=Cases.Count;
        var End_Switch=new 辺(ExpressionEqualityComparer){親コメント="end switch"};
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
            List辺.Add(this.辺=new(ExpressionEqualityComparer, SwitchValue){親コメント=sb.ToString()});
            this.Traverse(Body);
            辺.接続(this.辺,End_Switch);
            sb.Clear();
        }
        var DefaultBody=Switch.DefaultBody;
        List辺.Add(this.辺=new(ExpressionEqualityComparer, SwitchValue){親コメント=$"default:{DefaultBody}"});
        this.Traverse(DefaultBody);
        辺.接続(this.辺,End_Switch);
        List辺.Add(this.辺=End_Switch);
    }
    protected override void Try(TryExpression Try){
        var 外辺=this.辺;
        var EndTry=new 辺(ExpressionEqualityComparer){親コメント="end try"};
        共通("try");
        this.Traverse(Try.Body);
        var Body = this.辺;
        Body.子コメント=$"try {Body}";
        var sb=new StringBuilder();
        var Try_Handlers=Try.Handlers;
        var Try_Handlers_Count=Try_Handlers.Count;
        for(var a=0;a<Try_Handlers_Count;a++){
            var Try_Handle=Try_Handlers[a];
            sb.Append($"catch({Try_Handle.Test.FullName}");
            if(Try_Handle.Variable is not null)
                sb.Append($"{Try_Handle.Variable.Name}");
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
            var 辺=this.辺=new 辺(ExpressionEqualityComparer,外辺){親コメント=親コメント};
            List辺.Add(this.辺=辺);        
            辺.接続(辺,EndTry);
        }
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
