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
public class 辺(ExpressionEqualityComparer ExpressionEqualityComparer){
    //internal readonly E属性 属性;
    internal int 辺番号;
    private readonly Generic.HashSet<Expression> 節_一度出現したExpressions=new(ExpressionEqualityComparer);
    private readonly Generic.HashSet<Expression> 部分木_一度出現したExpressions=new(ExpressionEqualityComparer);
    private readonly Generic.HashSet<Expression> 部分木_二度出現したExpressions=new(ExpressionEqualityComparer);
    internal readonly Generic.List<辺>List親辺=new();
    internal readonly Generic.List<辺>List子辺=new();
    public string 親コメント{get;set;}="";
    public string 子コメント{get;set;}="";
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
            部分木_一度出現したExpressions.Intersect(this.節_一度出現したExpressions,ExpressionEqualityComparer)
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
    public Generic.IEnumerator<string> GetLineEnumerator(){
        var 列Array0=new Generic.List<(辺? 移動元,辺? 移動先)>{(null,null)};
        var Line=new StringBuilder();
        Line.Append('　');
        var 前回のLine=Line.ToString();
        var sb = new StringBuilder();
        var Count=this.Count-1;
        if(Count>=0){
            {
                var 辺=this[0];
                var 子辺Array=辺.List子辺.ToArray();
                子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
            }
            for(var a=1;a<Count;a++){
                var 辺=this[a];
                var 親辺Array = 辺.List親辺.ToArray();
                var 子辺Array = 辺.List子辺.ToArray();
                親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
                子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
            }
            {
                var 辺=this[Count];
                var 親辺Array = 辺.List親辺.ToArray();
                親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line);
                yield return Line.ToString();
            }
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
internal sealed class 作成_辺(辺 Top辺,List辺 List辺,Generic.Dictionary<LabelTarget,辺> Dictionary_LabelTarget_辺,ExpressionEqualityComparer ExpressionEqualityComparer):VoidExpressionTraverser_Quoteを処理しない{
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
        Dictionary_LabelTarget_辺.Clear();
        Top辺.Clear();
        List辺.Clear();
        List辺.Add(Top辺);
        this.辺=Top辺;
        this.Traverse(Expression0);
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
                    var 移動元=this.辺;
                    移動元.子コメント="Label2";//$"goto({Label.DefaultValue}){Goto.Target.Name! }"};
                    //├←┐    1 L1:←ここ
                    this.辺=移動先=new 辺(ExpressionEqualityComparer, 移動元){親コメント=$"({Label.DefaultValue}){ Label.Target.Name}:"};
                    List辺.Add(移動先);
                    Dictionary_LabelTarget_辺.Add(Label.Target,移動先);
                }
                return;
            }
            case ExpressionType.Goto:{
                //gotoが出たら次は新たな辺
                var Goto=(GotoExpression)Expression;
                if(Goto.Value is not null)
                    this.Traverse(Goto.Value);
                var デッドコード=new 辺(ExpressionEqualityComparer){子コメント="デッドコード"};
                if(Dictionary_LabelTarget_辺.TryGetValue(Goto.Target,out var 上辺)){
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
                    Dictionary_LabelTarget_辺.Add(Goto.Target,new 辺(ExpressionEqualityComparer,this.辺) { 子コメント=$"goto({Goto.Value}){Goto.Target.Name}"});
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
                    var 移動先=Dictionary_LabelTarget_辺[Loop.BreakLabel];
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
