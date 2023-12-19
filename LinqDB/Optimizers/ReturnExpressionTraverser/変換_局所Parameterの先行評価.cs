#define 省略
using System.Diagnostics;
using System.Linq;
using Generic=System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.VoidExpressionTraverser;
//using LinqDB.Sets;

namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
/// <summary>
/// 複数出現した式が最初に出現した時点で変数に代入し、以降その変数を参照する。
/// </summary>
/// <example>(a*b)+(a*b)→(t=a*b)+t</example>
public sealed class 変換_局所Parameterの先行評価:ReturnExpressionTraverser{
    public class 辺{
        //internal readonly E属性 属性;
        internal int 辺番号;
        private readonly Generic.HashSet<Expression> 節_一度出現したExpressions;
        private readonly Generic.HashSet<Expression> 部分木_一度出現したExpressions;
        private readonly Generic.HashSet<Expression> 部分木_二度出現したExpressions;
        internal readonly Generic.List<辺>List親辺=new();
        internal readonly Generic.List<辺>List子辺=new();
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        public string 親コメント{get;set;}="";
        public string 子コメント{get;set;}="";
        public 辺(ExpressionEqualityComparer ExpressionEqualityComparer){
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
            this.節_一度出現したExpressions=new(ExpressionEqualityComparer);
            this.部分木_一度出現したExpressions=new(ExpressionEqualityComparer);
            this.部分木_二度出現したExpressions=new(ExpressionEqualityComparer);
        }
        public 辺(ExpressionEqualityComparer ExpressionEqualityComparer,辺 辺):this(ExpressionEqualityComparer){
            辺.List子辺.Add(this);
            this.List親辺.Add(辺);
        }
        internal bool 探索済みか;
        private Expression? 部分木_二度出現したExpression;
        public static void 接続(辺 親,辺 子){
            親.List子辺.Add(子);
            子.List親辺.Add(親);
        }
        public bool この辺に存在するか(Expression Expression)=>this.節_一度出現したExpressions.Contains(Expression);
        public bool この辺に二度出現存在するか_削除する(Expression Expression)=>this.部分木_二度出現したExpressions.Remove(Expression);
        public void Clear(){
            this.部分木_二度出現したExpression=null;
            this.節_一度出現したExpressions.Clear();
            this.部分木_二度出現したExpressions.Clear();
            this.List親辺.Clear();
            this.List子辺.Clear();
        }
        /// <summary>
        /// 取得_二度出現したExpression1で使う
        /// 辺単位で一度出現したExpressions、二度出現したExpressionsを作る
        /// </summary>
        /// <param name="Expression"></param>
        public void Add(Expression Expression){
            if(!this.節_一度出現したExpressions.Add(Expression))
                this.部分木_二度出現したExpressions.Add(Expression);
        }
        /// <summary>
        /// 取得_二度出現したExpression1.実行()で使う
        /// 最もルートに近い式を返す
        /// </summary>
        /// <returns></returns>
        public Generic.IEnumerable<Expression> Create部分木_二度出現したExpressions(){
            if(this.探索済みか) return this.部分木_一度出現したExpressions;
            this.探索済みか=true;
            var 部分木_一度出現したExpressions=this.部分木_一度出現したExpressions;
            部分木_一度出現したExpressions.Clear();
            Debug.Assert(部分木_一度出現したExpressions.Count==0);
            using var Enumerator = this.List子辺.GetEnumerator();
            if(Enumerator.MoveNext()){
                部分木_一度出現したExpressions.UnionWith(Enumerator.Current.Create部分木_二度出現したExpressions());
                while(Enumerator.MoveNext())
                    部分木_一度出現したExpressions.IntersectWith(Enumerator.Current.Create部分木_二度出現したExpressions());
            }
            //仮想一度出現したExpressions.UnionWith(this.実体一度出現したExpressions);
            //var 節と節以外の部分木でそれぞれ一度づつ出現することで二度出現したとみなすExpressions=部分木_一度出現したExpressions.Intersect(this.節_一度出現したExpressions,this.ExpressionEqualityComparer);
            this.部分木_二度出現したExpressions.UnionWith(
                部分木_一度出現したExpressions.Intersect(this.節_一度出現したExpressions,this.ExpressionEqualityComparer)
            );
            部分木_一度出現したExpressions.UnionWith(this.節_一度出現したExpressions);
            return 部分木_一度出現したExpressions;
        }
        /// <summary>
        /// 取得_二度出現したExpression2で使う
        /// 最もルートに近い辺の式を返す
        /// </summary>
        /// <returns></returns>
        public(Expression?Expression,辺?辺に関する情報) 二度出現したExpressionと辺(){
            if(this.探索済みか) return(null,this);
            this.探索済みか=true;
            if(this.部分木_二度出現したExpression is not null)
                return(this.部分木_二度出現したExpression,this);
            foreach(var a in this.List子辺){
                var (Expression,辺に関する情報)=a.二度出現したExpressionと辺();
                if(Expression is not null)
                    return(Expression,辺に関する情報);
            }
            return(null,this);
        }
        /// <summary>
        /// 変換_局所Parameterの先行評価で使う
        /// この辺の中で最もルートに近い二度出現したExpressionをマークする
        /// </summary>
        /// <param name="Expression"></param>
        public void 二度出現したExpressionの部分木にマークする(Expression Expression){
            if(this.部分木_二度出現したExpression is not null)
                return;
            if(this.部分木_二度出現したExpressions.Contains(Expression)) this.部分木_二度出現したExpression=Expression;
        }
        public override string ToString(){
            return$"辺番号{this.辺番号} {this.子コメント}";
        }
    }
    /// <summary>
    /// 辺に関する情報のグラフを作成する。辺に関する情報のListを作成する。
    /// </summary>
    private sealed class 作成_辺(辺 Top辺,List辺 List辺,Generic.Dictionary<LabelTarget,辺> Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer ExpressionEqualityComparer):VoidExpressionTraverser_Quoteを処理しない{
        //private readonly Generic.Dictionary<LabelTarget,辺>Dictionary_LabelTarget_辺に関する情報=Dictionary_LabelTarget_辺に関する情報;
        //private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
        //private readonly ExpressionEqualityComparer ExpressionEqualityComparer=ExpressionEqualityComparer;
        //private readonly 辺 Top辺=Top辺;
        //private readonly List辺 List辺=List辺;
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        //private int 辺番号;
        private 辺 辺=default!;
        private int 計算量;
        public void 実行(Expression Expression0){
            this.計算量=0;
            Dictionary_LabelTarget_辺に関する情報.Clear();
            //this.Dictionary_LabelTarget_辺に関する情報.Clear();
            Top辺.Clear();
            //var List辺=this.List辺;
            //var Top辺=this.Top辺;
            List辺.Clear();
            List辺.Add(Top辺);
            this.辺=Top辺;
            //判定_左辺Expressionsが含まれる.Clear();
            this.Traverse(Expression0);
            //Trace.WriteLine($"取得_二度出現したExpression0.計算量 {this.計算量}");
            Trace.WriteLine(List辺.フロー);
        }
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
                case ExpressionType.Label:{
                    //:が出たら新たな辺
                    var Label=(LabelExpression)Expression;
                    if(Label.DefaultValue is not null)
                        this.Traverse(Label.DefaultValue);
                    if(Dictionary_LabelTarget_辺に関する情報.TryGetValue(Label.Target,out var 移動先)){
                        //gotoで指定したラベルでまだ定義されてない奴
                        //└┐0 goto 下
                        //..................
                        //┌┘1 下:←ここ
                        移動先.親コメント=$"({Label.DefaultValue}){Label.Target.Name}:";
                        List辺.Add(移動先);
                        this.辺=移動先;
                    } else{
                        //始めて出現。後でgoto命令で飛んでループを形成する
                        var 移動元=this.辺;
                        移動元.子コメント="Label2";//$"goto({Label.DefaultValue}){Goto.Target.Name! }"};
                        //├←┐    1 L1:←ここ
                        this.辺=移動先=new 辺(ExpressionEqualityComparer, 移動元){親コメント=$"({Label.DefaultValue}){ Label.Target.Name}:"};
                        List辺.Add(移動先);
                        Dictionary_LabelTarget_辺に関する情報.Add(Label.Target,移動先);
                    }
                    return;
                }
                case ExpressionType.Goto:{
                    //gotoが出たら次は新たな辺
                    var Goto=(GotoExpression)Expression;
                    if(Goto.Value is not null)
                        this.Traverse(Goto.Value);
                    var デッドコード=new 辺(ExpressionEqualityComparer){子コメント="デッドコード"};
                    if(Dictionary_LabelTarget_辺に関する情報.TryGetValue(Goto.Target,out var 上辺)){
                        //├┐  0 上辺:
                        //..................
                        //└┘    goto 上辺:←ここ
                        //.Label
                        //.LabelTarget 上辺:;
                        //..................
                        //.Goto 上辺 { };    
                        辺.接続(this.辺,上辺);
                    } else {
                        //下にジャンプ。条件分岐で多い。
                        //└┐0 goto 下:←new 辺に関する情報
                        //││          ←new 辺に関する情報 デッドコード
                        Dictionary_LabelTarget_辺に関する情報.Add(Goto.Target,new 辺(ExpressionEqualityComparer,this.辺) { 子コメント=$"goto({Goto.Value}){Goto.Target.Name}"});
                        //this.Dictionary_LabelTarget_辺に関する情報.Add(Goto.Target,new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号,this.辺に関する情報) { 親コメント=$"({Label.DefaultValue}){ Label.Target.Name}:"};
                    }
                    List辺.Add(this.辺=デッドコード);
                    return;
                }
                case ExpressionType.Loop:{
                    var Loop=(LoopExpression)Expression;
                    var Begin_Loop=this.辺;
                    Begin_Loop.子コメント="Begin Loop";
                    var LoopBody0=this.辺=new(ExpressionEqualityComparer,Begin_Loop){親コメント="Loop"};
                    List辺.Add(LoopBody0);
                    this.Traverse(Loop.Body);
                    var LoopBody1=this.辺;
                    //辺.接続(LoopBody0,LoopBody1);
                    辺.接続(LoopBody1,LoopBody0);
                    //var EndLoop=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,this.辺に関する情報){親コメント="End Loop"};
                    //List辺に関する情報.Add(EndLoop);
                    if(Loop.BreakLabel is not null){
                        var 移動先=Dictionary_LabelTarget_辺に関する情報[Loop.BreakLabel];
                        //gotoで指定したラベルでまだ定義されてない奴
                        //└┐0 goto 下
                        //..................
                        //┌┘1 下:←ここ
                        //変換_局所Parameterの先行評価.辺.接続(LoopBody1,);
                        //LoopBody1.List子辺.Add(移動先);
                        移動先.親コメント=$"End Loop {Loop.BreakLabel.Name}:";
                        List辺.Add(移動先);
                        this.辺=移動先;
                    } else{
                        List辺.Add(this.辺=new 辺(ExpressionEqualityComparer){子コメント="End Loop"});
                    }
                    return;
                }
                case ExpressionType.Assign: {
                    var Assign = (BinaryExpression)Expression;
                    var Assign_Left = Assign.Left;
                    if(Assign_Left.NodeType is ExpressionType.Parameter) {
                    } else
                        base.Traverse(Assign_Left);//.static_field,array[ここ]など
                    this.Traverse(Assign.Right);
                    //if(Assign.Left is not ParameterExpression)
                    //    base.Traverse(Assign.Left);
                    //this.Traverse(Assign.Right);
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
            //if(判定_左辺Expressionsが含まれる.実行(Expression))
            //    return;
            base.Traverse(Expression);
        }
        //private void AndAlso_OrElse(BinaryExpression Binary){
        //    //└─┬┐IfTrue(a)
        //    //┌─┘│
        //    //a|b 　│
        //    //└─┐│
        //    //┌─┼┘
        //    //a　 │　
        //    //└─┼┐
        //    //┌─┴┘
        //    var Left=Binary.Left;
        //    var Right=Binary.Right;
        //    this.Traverse(Left);
        //    var Test = this.辺に関する情報;
        //    var Left_ToString=Left.ToString();
        //    Test!.子コメント=Left_ToString;
        //    var True0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Binary.ToString() };
        //    this.List辺に関する情報.Add(True0);
        //    this.Traverse(Expression.And(Left,Right));
        //    var True1=this.辺に関する情報;
        //    var False0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Left_ToString };
        //    this.List辺に関する情報.Add(False0);
        //    this.Traverse(Left);
        //    var False1=this.辺に関する情報;
        //    var End=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号);
        //    this.List辺に関する情報.Add(End);
        //    辺に関する情報.接続(True1,End);
        //    辺に関する情報.接続(False1,End);
        //}
        //protected override void AndAlso(BinaryExpression Binary){
        //    throw new NotImplementedException();
        //    ////a&&b
        //    ////(t0=a)&&b
        //    ////if((t0=a).op_True)
        //    ////    t0&b
        //    ////else
        //    ////    t0
        //    ////a&&b&&c
        //    ////(t1=(t0=a)&&b)&&c
        //    ////if((t0=a).op_True)
        //    ////    if((t1=t0&b).op_True)
        //    ////        t1&c
        //    ////    else
        //    ////        t1
        //    ////else
        //    ////    t0
        //    //var Left=Binary.Left;
        //    //var Right=Binary.Right;
        //    //var Left_ToString=Left.ToString();
        //    //this.Traverse(Left);
        //    //var Test = this.辺に関する情報;
        //    //Test.子コメント=$"AndAlso {Left_ToString}";
        //    //var End_AndAlso=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号){ 親コメント="End AndAlso"};
        //    //var List辺に関する情報=this.List辺に関する情報;
        //    //List辺に関する情報.Add(this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Binary.ToString() });
        //    //this.Traverse(Expression.And(Left,Right));
        //    //辺に関する情報.接続(this.辺に関する情報,End_AndAlso);
        //    //List辺に関する情報.Add(this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Left_ToString });
        //    //this.Traverse(Left);
        //    //辺に関する情報.接続(this.辺に関する情報,End_AndAlso);
        //    //List辺に関する情報.Add(this.辺に関する情報=this.辺に関する情報=End_AndAlso);
        //}
        //protected override void OrElse(BinaryExpression Binary) {
        //    //throw new NotImplementedException();
        //    //a||b
        //    //(t0=a)||b
        //    //if((t0=a).op_True)
        //    //    t0
        //    //else
        //    //    t0|b
        //    //a||b||c
        //    //(t1=(t0=a)||b)||c
        //    //if((t0=a).op_True)
        //    //    t0
        //    //else
        //    //    if((t1=t0|b).op_True)
        //    //        t1
        //    //    else
        //    //        t1|c
        //    var Left = Binary.Left;
        //    var Right = Binary.Right;
        //    var Left_ToString = Left.ToString();
        //    this.Traverse(Left);
        //    var Test = this.辺;
        //    Test.子コメント=$"OrElse {Left_ToString}";
        //    var End_OrElse = new 辺(this.ExpressionEqualityComparer,ref this.辺番号) { 親コメント="End OrElse" };
        //    var List辺 = this.List辺;
        //    List辺.Add(this.辺=new(this.ExpressionEqualityComparer,ref this.辺番号,Test) { 親コメント=Left_ToString });
        //    this.Traverse(Left);
        //    辺.接続(this.辺,End_OrElse);
        //    List辺.Add(this.辺=new(this.ExpressionEqualityComparer,ref this.辺番号,Test) { 親コメント=Binary.ToString() });
        //    this.Traverse(Expression.Or(Left,Right));
        //    辺.接続(this.辺,End_OrElse);
        //    List辺.Add(this.辺=End_OrElse);
        //}
        protected override void Conditional(ConditionalExpression Conditional){
            //test　　　　0 If test
            //├────┐2 True IfTrue
            //ifTrue　　│
            //└───┐│  goto 1 End If
            //┌───┼┘3 ifFalse:
            //ifFalse │　
            //├───┘　1 End If:
            var IfTest=Conditional.Test;
            var IfTrue=Conditional.IfTrue;
            var IfFalse=Conditional.IfFalse;
            this.Traverse(Conditional.Test);
            var begin_if = this.辺;
            begin_if.子コメント=$"begin if {IfTest}";
            var end_if=new 辺(ExpressionEqualityComparer){親コメント="end if"};
            //var List辺=this.List辺;
            List辺.Add(this.辺=new(ExpressionEqualityComparer, begin_if){親コメント=$"true {IfTrue}"});
            this.Traverse(IfTrue);
            辺.接続(this.辺,end_if);
            List辺.Add(this.辺=new 辺(ExpressionEqualityComparer,begin_if) { 親コメント=$"false {IfFalse}" });
            this.Traverse(IfFalse);
            辺.接続(this.辺,end_if);
            List辺.Add(this.辺=this.辺=end_if);
        }
        protected override void Switch(SwitchExpression Switch){
            //var List辺=this.List辺;
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
    private readonly 作成_辺 _作成_辺;
    private sealed class 作成_二度出現したExpression:VoidExpressionTraverser_Quoteを処理しない{
        private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
        private readonly Generic.List<ParameterExpression> ListスコープParameter;
        private readonly List辺 List辺;
        public 作成_二度出現したExpression(List辺 List辺,Generic.List<ParameterExpression> ListスコープParameter,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる){
            this.List辺=List辺;
            this.ListスコープParameter=ListスコープParameter;
            this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
        }
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        /// <summary>
        /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
        /// </summary>
        internal bool IsInline=true;
        private int 辺番号;
        private 辺 Top辺=>this.List辺[0];
        private 辺 辺=default!;
        private int 計算量;
        public void 実行(Expression Expression0){
            this.計算量=0;
            this.辺番号=1;
            this.辺=this.Top辺;
            this.判定_左辺Expressionsが含まれる.Clear();
            this.Traverse(Expression0);
            this.List辺.一度出現したExpressionを上位に移動();
            //Trace.WriteLine($"取得_二度出現したExpression1.計算量 {this.計算量}");
            Debug.Assert(this.辺番号==this.List辺.Count);
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
                    this.辺インクリメント();
                    return;
                }
                case ExpressionType.Assign: {
                    var Assign = (BinaryExpression)Expression;
                    var Assign_Left = Assign.Left;
                    if(Assign_Left.NodeType is ExpressionType.Parameter)
                        this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                    else
                        base.Traverse(Assign_Left);//.static_field,array[ここ]など
                    this.Traverse(Assign.Right);
                    //var Assign_Left = Assign.Left;
                    //if(Assign_Left is IndexExpression Index)
                    //    base.Index(Index);
                    //else if(Assign_Left is ParameterExpression){
                    //    this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                    //    this.Traverse(Assign.Right);
                    //} else{
                    //    base.Traverse(Assign_Left);//.static_fieldなど
                    //}
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
            //if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
            //    base.Traverse(Expression);
            //    return;
            //}
            if(Expression.Type!=typeof(void))
                this.辺.Add(Expression);
            base.Traverse(Expression);
        }
        private void 辺インクリメント()=>this.辺=this.List辺[this.辺番号++];
        protected override void Block(BlockExpression Block){
            var ListスコープParameter = this.ListスコープParameter;
            var ListスコープParameter_Count = ListスコープParameter.Count;
            ListスコープParameter.AddRange(Block.Variables);
            base.Block(Block);
            ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
        }
        //protected override void AndAlso(BinaryExpression Binary){
        //    //a&&b
        //    //(t0=a)&&b
        //    //if((t0=a).op_True)
        //    //    t0&b
        //    //else
        //    //    t0
        //    //a&&b&&c
        //    //(t1=(t0=a)&&b)&&c
        //    //if((t0=a).op_True)
        //    //    if((t1=t0&b).op_True)
        //    //        t1&c
        //    //    else
        //    //        t1
        //    //else
        //    //    t0
        //    var Binary_Left=Binary.Left;
        //    this.辺.Add(Binary_Left);
        //    this.Traverse(Binary_Left);
        //    this.辺インクリメント();
        //    this.Traverse(Expression.And(Binary_Left,Binary.Right,Binary.Method));
        //    this.辺インクリメント();
        //    this.Traverse(Binary_Left);
        //    this.辺インクリメント();
        //}
        //protected override void OrElse(BinaryExpression Binary){
        //    //a||b
        //    //(t0=a)||b
        //    //if((t0=a).op_True)
        //    //    t0
        //    //else
        //    //    t0|b
        //    //a||b||c
        //    //(t1=(t0=a)||b)||c
        //    //if((t0=a).op_True)
        //    //    t0
        //    //else
        //    //    if((t1=t0|b).op_True)
        //    //        t1
        //    //    else
        //    //        t1|c
        //    var Binary_Left=Binary.Left;
        //    this.辺.Add(Binary_Left);
        //    this.Traverse(Binary.Left);
        //    this.辺インクリメント();
        //    this.Traverse(Binary_Left);
        //    this.辺インクリメント();
        //    this.Traverse(Expression.Or(Binary_Left,Binary.Right,Binary.Method));
        //    this.辺インクリメント();
        //}
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
    private readonly 作成_二度出現したExpression _作成_二度出現したExpression;
    private sealed class 取得_二度出現したExpression:VoidExpressionTraverser_Quoteを処理しない{
        private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
        private readonly List辺 List辺;
        public 取得_二度出現したExpression(List辺 List辺,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる){
            this.List辺=List辺;
            this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
        }
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        /// <summary>
        /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
        /// </summary>
        internal bool IsInline=true;
        private int 辺番号;
        private 辺 Top辺=>this.List辺[0];
        private 辺 辺=default!;
        //private 置換開始
        private int 計算量;
        public (Expression?二度出現した一度目のExpression,辺?辺に関する情報) 実行(Expression Expression0){
            this.計算量=0;
            this.辺番号=1;
            this.辺=this.Top辺;
            this.判定_左辺Expressionsが含まれる.Clear();
            this.Traverse(Expression0);
            //Trace.WriteLine($"取得_二度出現したExpression2.計算量 {this.計算量}");
            return this.List辺.二度出現したExpressionと辺();
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
                    this.辺インクリメント();
                    return;
                }
                case ExpressionType.Assign: {
                    var Assign = (BinaryExpression)Expression;
                    var Assign_Left = Assign.Left;
                    if(Assign_Left.NodeType is ExpressionType.Parameter)
                        this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                    else
                        base.Traverse(Assign_Left);//.static_field,array[ここ]など
                    this.Traverse(Assign.Right);
                    //var Assign_Left = Assign.Left;
                    //if(Assign_Left is IndexExpression Index)
                    //    base.Index(Index);
                    //else if(Assign_Left is ParameterExpression){
                    //    this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                    //    this.Traverse(Assign.Right);
                    //} else{
                    //    base.Traverse(Assign_Left);//.static_fieldなど
                    //}
                    return;
                }
                case ExpressionType.Call: {
                    if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression).Method))return;
                    //var MethodCall= (MethodCallExpression)Expression;
                    //var GenericMethodDefinition=GetGenericMethodDefinition(MethodCall.Method);
                    //if(Reflection.Helpers.NoEarlyEvaluation==GenericMethodDefinition)return;
                    //var MethodCall0_Method = MethodCall.Method;
                    //if(this.IsInline){
                    //    if(ループ展開可能メソッドか(GenericMethodDefinition)) {
                    //        if(nameof(Sets.ExtensionSet.Except)==MethodCall0_Method.Name){
                    //            var MethodCall0_Arguments = MethodCall.Arguments;
                    //            this.Traverse(MethodCall0_Arguments[1]);
                    //            this.Traverse(MethodCall0_Arguments[0]);
                    //            return;
                    //        }
                    //    }
                    //}
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
                //case ExpressionType.Block:{
                //    var Block=(BlockExpression)Expression;
                //    if(this.判定_分岐があるか.実行(Block)){
                //        base.Block(Block);
                //        return;
                //    }
                //    break;
                //}
            }
            if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
                base.Traverse(Expression);
                return;
            }
            if(Expression.Type!=typeof(void))
                this.辺.二度出現したExpressionの部分木にマークする(Expression);
            base.Traverse(Expression);
        }
        private void 辺インクリメント()=>this.辺=this.List辺[this.辺番号++];
        //private void AndAlso_OrElse(BinaryExpression Binary){
        //    //└┬┐　true(a)
        //    //┌┘│
        //    //│　│　a&b
        //    //└─┼┐
        //    //┌─┘│
        //    //│　　│a
        //    //├──┘
        //    this.Traverse(Binary.Left);
        //    this.辺インクリメント();
        //    this.Traverse(Binary.Right);
        //    this.辺インクリメント();
        //    //this.Traverse(Binary.Left);
        //    //this.辺インクリメント();
        //}
        //protected override void AndAlso(BinaryExpression Binary){
        //    //a&&b
        //    //(t0=a)&&b
        //    //if((t0=a).op_True)
        //    //    t0&b
        //    //else
        //    //    t0
        //    //a&&b&&c
        //    //(t1=(t0=a)&&b)&&c
        //    //if((t0=a).op_True)
        //    //    if((t1=t0&b).op_True)
        //    //        t1&c
        //    //    else
        //    //        t1
        //    //else
        //    //    t0
        //    this.Traverse(Binary.Left);
        //    this.辺インクリメント();
        //    this.Traverse(Binary.Right);
        //    this.辺インクリメント();
        //}
        //protected override void OrElse(BinaryExpression Binary)=>this.AndAlso_OrElse(Binary);
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
            //var MethodCall0_Method = MethodCall.Method;
            //if(this.IsInline&&ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall0_Method))) {
            //    switch(MethodCall0_Method.Name) {
            //        case nameof(Sets.Helpers.NoEarlyEvaluation): {
            //            Debug.Assert(Reflection.Helpers.NoEarlyEvaluation==MethodCall0_Method.GetGenericMethodDefinition());
            //            return;
            //        }
            //        case nameof(Sets.ExtensionSet.Except): {
            //            Debug.Assert(
            //                MethodCall.Arguments.Count==3
            //                &&
            //                Reflection.ExtensionEnumerable.Except_comparer==MethodCall0_Method.GetGenericMethodDefinition()
            //                ||
            //                MethodCall.Arguments.Count==2
            //                &&(
            //                    Reflection.ExtensionEnumerable.Except==MethodCall0_Method.GetGenericMethodDefinition()
            //                    ||
            //                    Reflection.ExtensionSet.Except==MethodCall0_Method.GetGenericMethodDefinition()
            //                )
            //            );
            //            var MethodCall0_Arguments = MethodCall.Arguments;
            //            this.Traverse(MethodCall0_Arguments[1]);
            //            this.Traverse(MethodCall0_Arguments[0]);
            //            return;
            //        }
            //        default:
            //            Debug.Assert(nameof(Sets.ExtensionSet.Join)!=MethodCall0_Method.Name);
            //            break;
            //    }
            //}
            //base.Call(MethodCall);
        }
    }
    private readonly 取得_二度出現したExpression _取得_二度出現したExpression;
    private sealed class 判定_左辺Expressionsが含まれる:VoidExpressionTraverser_Quoteを処理しない {
        private readonly Generic.HashSet<Expression> 左辺Expressions;
        public 判定_左辺Expressionsが含まれる(ExpressionEqualityComparer ExpressionEqualityComparer) {
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

        protected override void Traverse(Expression e) {
            if(this.左辺Expressions.Contains(e)) this.左辺Expressionが含まれる=true;
            else base.Traverse(e);
        }
    }
    private sealed class 変換_二度出現したExpression:ReturnExpressionTraverser{
        private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
        private readonly Generic.List<ParameterExpression> ListスコープParameter;
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        private readonly List辺 List辺;
        public 変換_二度出現したExpression(作業配列 作業配列,List辺 List辺,Generic.List<ParameterExpression> ListスコープParameter,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる,ExpressionEqualityComparer_Assign_Leftで比較 ExpressionEqualityComparer) : base(作業配列){
            this.List辺=List辺;
            this.ListスコープParameter=ListスコープParameter;
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
            this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
        }
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
        private bool 既に置換された式を走査中;
        private int 計算量;
        public Expression 実行(Expression Expression0,Expression 二度出現した一度目のExpression,辺 二度出現した一度目のExpressionが属する辺に関する情報,ParameterExpression 二度目以降のParameter){
            this.計算量=0;
            this.既に置換された式を走査中=false;
            this.辺番号=1;
            this.辺=this.List辺[0];
            this.二度出現した一度目のExpression = 二度出現した一度目のExpression;
            this.判定_左辺Expressionsが含まれる.Clear();
            this.二度目以降のParameter = 二度目以降のParameter;
            this.一度目のAssign = Expression.Assign(二度目以降のParameter,二度出現した一度目のExpression);
            //this.判定_左辺Expressionsが含まれる.Clear();
            var Expression1=this.Traverse(Expression0);
            //Trace.WriteLine($"変換_二度出現したExpression.計算量 {this.計算量}");
            Debug.Assert(this.辺番号==this.List辺.Count);
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
                case ExpressionType.Loop: {
                    var Loop0 = (LoopExpression)Expression0;
                    this.辺インクリメント();
                    var Loop1=this.Loop(Loop0);
                    this.辺インクリメント();
                    return Loop1;
                }
                case ExpressionType.Assign: {
                    var Assign0 = (BinaryExpression)Expression0;
                    var Assign0_Left = Assign0.Left;
                    //var Assign1_Left = base.Traverse(Assign0_Left);
                    //this.判定_左辺Expressionsが含まれる.Add(Assign0_Left);
                    //var Assign1_Right=this.Traverse(Assign0.Right);
                    Expression Assign1_Left;
                    if(Assign0_Left.NodeType is ExpressionType.Parameter){
                        this.判定_左辺Expressionsが含まれる.Add(Assign0_Left);
                        Assign1_Left=Assign0_Left;
                    } else
                        Assign1_Left= base.Traverse(Assign0_Left);//.static_field,array[ここ]など
                    return Expression.Assign(Assign1_Left,this.Traverse(Assign0.Right));
                }
                case ExpressionType.Call: {
                    if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression0).Method))return Expression0;
                    //var MethodCall0= (MethodCallExpression)Expression0;
                    //var GenericMethodDefinition=GetGenericMethodDefinition(MethodCall0.Method);
                    //if(Reflection.Helpers.NoEarlyEvaluation==GenericMethodDefinition)return Expression0;
                    //var MethodCall0_Method = MethodCall0.Method;
                    //if(this.IsInline){
                    //    if(ループ展開可能メソッドか(GenericMethodDefinition)) {
                    //        switch(MethodCall0_Method.Name) {
                    //            case nameof(Sets.ExtensionSet.Except): {
                    //                Debug.Assert(
                    //                    MethodCall0.Arguments.Count==3
                    //                    &&
                    //                    Reflection.ExtensionEnumerable.Except_comparer==MethodCall0_Method.GetGenericMethodDefinition()
                    //                    ||
                    //                    MethodCall0.Arguments.Count==2
                    //                    &&(
                    //                        Reflection.ExtensionEnumerable.Except==MethodCall0_Method.GetGenericMethodDefinition()
                    //                        ||
                    //                        Reflection.ExtensionSet.Except==MethodCall0_Method.GetGenericMethodDefinition()
                    //                    )
                    //                );
                    //                var MethodCall0_Arguments = MethodCall0.Arguments;
                    //                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                    //                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                    //                if(MethodCall0.Arguments.Count==3) return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,this.Traverse(MethodCall0_Arguments[2]));
                    //                else return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
                    //            }
                    //            default:
                    //                Debug.Assert(nameof(Sets.ExtensionSet.Join)!=MethodCall0_Method.Name);
                    //                break;
                    //        }
                    //    }
                    //}
                    break;
                }
                case ExpressionType.Constant: {
                    if(ILで直接埋め込めるか((ConstantExpression)Expression0))return Expression0;
                    break;
                }
                case ExpressionType.Parameter: {
                    if(this.ラムダ跨ぎParameters.Contains(Expression0))break;
                    return Expression0;
                    //Debug.Assert(!this.ラムダ跨ぎParameters.Contains(Expression0));
                    //return Expression0;
                }
            }
            //if(this.判定_左辺Expressionsが含まれる.実行(Expression0)) 
            //    return Expression0;
            if(Expression0.Type!=typeof(void)){
                if(!this.既に置換された式を走査中){
                    if(this.ExpressionEqualityComparer.Equals(Expression0,this.二度出現した一度目のExpression)){
                        this.既に置換された式を走査中=true;
                        if(this.辺.この辺に二度出現存在するか_削除する(Expression0)){
                            base.Traverse(Expression0);
                            this.既に置換された式を走査中=false;
                            return this.一度目のAssign;
                        }
                        Debug.Assert(this.辺.この辺に存在するか(Expression0));
                        base.Traverse(Expression0);
                        this.既に置換された式を走査中=false;
                        return this.二度目以降のParameter!;
                    }
                }
            }
            return base.Traverse(Expression0);
        }
        private void 辺インクリメント()=>this.辺=this.List辺[this.辺番号++];
        protected override Expression Block(BlockExpression Block0){
            var ListスコープParameter = this.ListスコープParameter;
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
                //var Switch0_Case_TestValues=Switch0_Case.TestValues;
                //var Switch0_Case_TestValues_Count=Switch0_Case_TestValues.Count;
                //var Switch1_Case_TestValues=new Expression[Switch0_Case_TestValues_Count];
                //var 変化したか1=false;
                //for(var b=0; b < Switch0_Case_TestValues_Count; b++) {
                //    var Switch0_Case_TestValue=Switch0_Case_TestValues[b];
                //    var Switch1_Case_TestValue=this.Traverse(Switch0_Case_TestValue);
                //    //c#上は定数(Constant)しかできないが式木で作った場合はそれにとらわれない
                //    //this.辺インクリメント();
                //    Switch1_Case_TestValues[b]=Switch1_Case_TestValue;
                //    if(Switch0_Case_TestValue !=Switch1_Case_TestValue)
                //        変化したか1=true;
                //}
                //var Switch0_Case_Body=Switch0_Case.Body;
                //var Switch1_Case_Body=this.Traverse(Switch0_Case_Body);
                //this.辺インクリメント();
                //if(Switch0_Case_Body !=Switch1_Case_Body || 変化したか1) {
                //    Switch1_Cases[a]=Expression.SwitchCase(Switch1_Case_Body,Switch1_Case_TestValues);
                //    変化したか=true;
                //} else
                //    Switch1_Cases[a]=Switch0_Case;
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
    public sealed class List辺:Generic.List<辺>{
        public string フロー {
            get {
                var Count = this.Count;
                var 列Array0=new Generic.List<(辺? 移動元,辺? 移動先)>{(null,null)};
                var Line=new StringBuilder();
                Line.Append('　');
                var 前回のLine=Line.ToString();
                var 辺に関する情報Array = this.ToArray();
                var sb = new StringBuilder();
                for(var a = 0;a<Count;a++) {
                    var 辺 = 辺に関する情報Array[a];
                    辺.辺番号=a;
                    var 親辺Array = 辺.List親辺.ToArray();
                    var 子辺Array = 辺.List子辺.ToArray();
                    親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                    子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                }
                return sb.ToString();
            }
        }
        public void 一度出現したExpressionを上位に移動(){
            foreach(var a in this) a.探索済みか=false;
            this[0].Create部分木_二度出現したExpressions();
        }
        public (Expression?Expression,辺?辺に関する情報) 二度出現したExpressionと辺(){
            foreach(var a in this) a.探索済みか=false;
            return this[0].二度出現したExpressionと辺();
        }
        private static void Line初期化(Generic.List<(辺? 移動元,辺? 移動先)>列Array0,StringBuilder Line){
            for(var b = 0;b<列Array0.Count;b++)
                if(列Array0[b].移動元 is null)
                    Line[b]='　';
                else
                    Line[b]='│';
        }
        /// <summary>
        /// 親とはラベルのことであり、このラベルを使うジャンプ命令の属する複数辺を保持する
        /// </summary>
        /// <param name="親辺Array"></param>
        /// <param name="列Array"></param>
        /// <param name="前回のLine"></param>
        /// <param name="辺"></param>
        /// <param name="sb"></param>
        /// <param name="Line"></param>
        private static void 親(辺[] 親辺Array,Generic.List<(辺? 移動元,辺? 移動先)> 列Array,ref string 前回のLine,辺 辺,StringBuilder sb,StringBuilder Line){
            var 親辺Array_Length=親辺Array.Length;
            if(親辺Array_Length<=0)return;
            Line初期化(列Array,Line);
            Line[0]='┌';
            var 上Count=0;
            var 下Count=0;
            var 書き込みした右端列=-1;
            for(var a=0;a<親辺Array_Length;a++){
                var 親辺=親辺Array[a];
                var Count=列Array.Count;
                for(var b=0;b<Count;b++){
                    var 列=列Array[b];
                    //Debug.Assert(列.移動元==親辺);
                    if(列.移動元==親辺) {
                        Debug.Assert(列.移動先==辺);
                        if(Line[b]!='┘')
                            if(書き込みした右端列<b)
                                書き込みした右端列=b;
                        Line[b]='┘';
                        上Count++;
                        goto 終了;
                        //if(列.移動先==辺){
                        //    //Debug.Assert(Line[b]!='┘');
                        //    if(Line[b]!='┘')
                        //        if(書き込みした右端列<b)
                        //            書き込みした右端列=b;
                        //    Line[b]='┘';
                        //    上Count++;
                        //    goto 終了;
                        //}
                    }
                }
                for(var b=0;b<Count;b++){
                    if(Line[b]is'　'){
                        列Array[b]=(親辺,辺);
                        Debug.Assert(書き込みした右端列<b); 
                        書き込みした右端列=b;
                        Line[b]='┐';
                        下Count++;
                        goto 終了;
                    }
                }
                書き込みした右端列=Line.Length;
                列Array.Add((親辺,辺));
                Line.Append('┐');
                下Count++;
                終了: ;
            }
            if(書き込みした右端列>0){
                for(var a=1;a<書き込みした右端列;a++){
                    switch(Line[a]){
                        case '　':
                            Line[a]='─';
                            break;
                        case '┐':
                            Line[a]='┬';
                            break;
                        case '┘':
                            Line[a]='┴';
                            列Array[a]=(null,null);
                            break;
                        case '│':
                            Line[a]='┼';
                            break;
                    }
                }
                if(Line[書き込みした右端列]=='┘') 列Array[書き込みした右端列]=(null,null);
            }
            var 行=Line.ToString();
            前回のLine=行;
            sb.AppendLine($"{行}{辺.辺番号},{辺.親コメント}");
        }
        /// <summary>
        /// 親とはジャンプ命令のことであり、この複数のジャンプ命令(if,switch,goto)の飛び先のラベルの属する複数辺を保持する
        /// </summary>
        /// <param name="子辺Array"></param>
        /// <param name="列Array0"></param>
        /// <param name="前回のLine"></param>
        /// <param name="辺"></param>
        /// <param name="sb"></param>
        /// <param name="Line"></param>
        private static void 子(辺[] 子辺Array,Generic.List<(辺? 移動元,辺? 移動先)> 列Array0,ref string 前回のLine,辺 辺,StringBuilder sb,StringBuilder Line){
            var 子辺Array_Length=子辺Array.Length;
            if(子辺Array_Length<=0)return;
            Line初期化(列Array0,Line);
            Line[0]='└';
            var ループか=false;
            var 書き換えLineIndexEnd=-1;
            for(var a=0;a<子辺Array_Length;a++){
                var 子辺=子辺Array[a];
                var Count=列Array0.Count;
                for(var b=0;b<Count;b++){
                    var 列=列Array0[b];

                    if(列.移動元==辺) {
                        if(列.移動先==子辺) {
                            if(Line[b]=='│') {
                                Debug.Assert(書き換えLineIndexEnd<b);
                                if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                                Line[b]='┘';
                                ループか=true;
                                goto 終了;
                            }
                        }
                    }
                }
                for(var b=0;b<Count;b++){
                    if(Line[b]=='　'){
                        Debug.Assert(列Array0[b].移動元 is null);
                        列Array0[b]=(辺,子辺);
                        Debug.Assert(書き換えLineIndexEnd<b);
                        if(書き換えLineIndexEnd<b)
                            書き換えLineIndexEnd=b;
                        Line[b]='┐';
                        goto 終了;
                    } 
                }
                書き換えLineIndexEnd=Line.Length;
                列Array0.Add((辺,子辺));
                Line.Append('┐');
                終了: ;
            }
            for(var a=0;a<書き換えLineIndexEnd;a++){
                switch(Line[a]){
                    case '　':Line[a]='─'; break;
                    //case '┘':
                    //    Line[a]='┴';
                    //    break;
                    case '│':Line[a]='┼'; break;
                    case '┐':Line[a]='┬'; break;
                }
            }
            if(ループか)
                列Array0[書き換えLineIndexEnd]=(null,null);
            var 行=Line.ToString();
            前回のLine=行;
            sb.AppendLine($"{行}{辺.辺番号},{辺.子コメント}");
        }
    }
    private readonly Generic.Dictionary<LabelTarget,辺> Dictionary_LabelTarget_辺に関する情報=new();
    internal 変換_局所Parameterの先行評価(作業配列 作業配列) : base(作業配列){
        var ListスコープParameter=this.ListスコープParameter=new();
        var ExpressionEqualityComparer=new ExpressionEqualityComparer_Assign_Leftで比較(ListスコープParameter);
        var 判定_左辺Parametersが含まれる=new 判定_左辺Expressionsが含まれる(ExpressionEqualityComparer);
        var Dictionary_LabelTarget_辺に関する情報=this.Dictionary_LabelTarget_辺に関する情報;
        var Top辺に関する情報=new 辺(ExpressionEqualityComparer){辺番号=0,子コメント = "開始"};
        var List辺=new List辺();
        this._作成_辺=new(Top辺に関する情報,List辺,Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer);
        this._作成_二度出現したExpression=new(List辺,ListスコープParameter,判定_左辺Parametersが含まれる);
        this._取得_二度出現したExpression=new(List辺,判定_左辺Parametersが含まれる);
        this._変換_二度出現したExpression=new(作業配列,List辺,ListスコープParameter,判定_左辺Parametersが含まれる,ExpressionEqualityComparer);
    }
    /// <summary>
    /// NonPublicAccessorで使う
    /// </summary>
    public 変換_局所Parameterの先行評価() : this(new 作業配列())=>this.ラムダ跨ぎParameters=new Generic.List<ParameterExpression>();
    internal Generic.IEnumerable<ParameterExpression> ラムダ跨ぎParameters{
        set{
            this._作成_辺.ラムダ跨ぎParameters=value;
            this._作成_二度出現したExpression.ラムダ跨ぎParameters=value;
            this._取得_二度出現したExpression.ラムダ跨ぎParameters=value;
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
            this._作成_二度出現したExpression.IsInline=value;
            this._取得_二度出現したExpression.IsInline=value;
            this._変換_二度出現したExpression.IsInline=value;
        }
    }
    public Expression 実行(Expression Expression0) {
        this.ListスコープParameter.Clear();
        return this.Traverse(Expression0);
        //Debug.Assert(Expression0.NodeType==ExpressionType.Lambda&&this.ListスコープParameter.Count==0);
        //this.番号=0;
        //var Lambda1 = (LambdaExpression)this.Traverse(Expression0);
        //return Expression.Lambda(Lambda1.Type,Lambda1.Body,Lambda1.Name,Lambda1.TailCall,Lambda1.Parameters);
    }

    protected override Expression Assign(BinaryExpression Assign0)=>Expression.Assign(
        this.Traverse(Assign0.Left),
        this.Traverse(Assign0.Right)
    );
    protected override Expression Block(BlockExpression Block0){
        var ListスコープParameter = this.ListスコープParameter;
        var ListスコープParameter_Count = ListスコープParameter.Count;
        ListスコープParameter.AddRange(Block0.Variables);
        var Block1=base.Block(Block0);
        ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
        return Block1;
    }
    protected override Expression Lambda(LambdaExpression Lambda0){
        var ListスコープParameter = this.ListスコープParameter;
        var ListスコープParameter_Count = ListスコープParameter.Count;
        var Lambda0_Parameters = Lambda0.Parameters;
        ListスコープParameter.AddRange(Lambda0_Parameters);
        var Block0_Variables = this.Block_Variables;
        var Block1_Variables = this.Block_Variables=new();
        var Lambda1_Body = Lambda0.Body;
        var BlockVariables = this.Block_Variables;
        var 辺を作る=this._作成_辺;
        var 辺に二度出現したExpressionを作る = this._作成_二度出現したExpression;
        var 辺から二度出現したExpressionを取得 = this._取得_二度出現したExpression;
        var 変換_二度出現したExpression = this._変換_二度出現したExpression;
        while(true) {
            辺を作る.実行(Lambda1_Body);
            辺に二度出現したExpressionを作る.実行(Lambda1_Body);
            var (二度出現した一度目のExpression,辺)=辺から二度出現したExpressionを取得.実行(Lambda1_Body);
            if(二度出現した一度目のExpression is null)
                break;
            var Variable = Expression.Variable(二度出現した一度目のExpression.Type,$"局所{this.番号++}");
            BlockVariables.Add(Variable);
            ListスコープParameter.Add(Variable);
            var Expression0 = 変換_二度出現したExpression.実行(Lambda1_Body,二度出現した一度目のExpression,辺,Variable);
            Lambda1_Body=Expression0;
        }
        var Lambda2_Body = this.Traverse(Lambda1_Body);
        this.Block_Variables=Block0_Variables;
        if(Block1_Variables.Count>0) Lambda2_Body=Expression.Block(Block1_Variables,this.作業配列.Expressions設定(Lambda2_Body));
        Expression Lambda1;
        if(Lambda1_Body==Lambda2_Body) 
            Lambda1=Lambda0;
        else 
            Lambda1 = Expression.Lambda(Lambda0.Type,Lambda2_Body,Lambda0.Name,Lambda0.TailCall,Lambda0_Parameters);
        ListスコープParameter.RemoveRange(ListスコープParameter_Count,ListスコープParameter.Count-ListスコープParameter_Count);
        return Lambda1;
    }
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
//         1716