#define 省略
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using Generic=System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Enumerables;
using LinqDB.Optimizers.Comparison;
using LinqDB.Optimizers.VoidExpressionTraverser;
//using LinqDB.Sets;

namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
using static LinqDB.Optimizers.ReturnExpressionTraverser.変換_局所Parameterの先行評価;

/// <summary>
/// 複数出現した式が最初に出現した時点で変数に代入し、以降その変数を参照する。
/// </summary>
/// <example>(a*b)+(a*b)→(t=a*b)+t</example>
public sealed class 変換_局所Parameterの先行評価:ReturnExpressionTraverser_Quoteを処理しない {
    //[DebuggerDisplay("{辺番号}")]
    //private class 辺に関する情報{
    //    public readonly int 辺番号;
    //    public readonly List<Expression> 出現順Expressions=new();
    //    private readonly HashSet<Expression> 一度出現したExpressions;
    //    private readonly List<辺に関する情報>上辺=new();
    //    //private readonly List<辺に関する情報>下辺=new();
    //    private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    //    //public 辺に関する情報(ExpressionEqualityComparer ExpressionEqualityComparer):this(ExpressionEqualityComparer,0){
    //    //}
    //    public 辺に関する情報(ExpressionEqualityComparer ExpressionEqualityComparer,ref int 辺番号){
    //        this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    //        this.一度出現したExpressions=new(ExpressionEqualityComparer);
    //        this.辺番号=辺番号;
    //        辺番号++;
    //    }
    //    public 辺に関する情報 Create(ref int 辺番号){
    //        var r=new 辺に関する情報(this.ExpressionEqualityComparer,ref 辺番号);
    //        r.上辺.Add(this);
    //        return r;
    //    }
    //    public void Add(Expression Expression)=>this.一度出現したExpressions.Add(Expression);
    //    public void Add(辺に関する情報 上辺)=>this.上辺.Add(上辺);
    //    public HashSet<Expression> Expressions{
    //        get{
    //            var 新辺一度出現したExpressions = new HashSet<Expression>(this.ExpressionEqualityComparer);
    //            if(this.上辺.Count>0) {
    //                using var Enumerator = this.上辺.GetEnumerator();
    //                if(Enumerator.MoveNext()) {
    //                    新辺一度出現したExpressions.UnionWith(Enumerator.Current.Expressions);
    //                    while(Enumerator.MoveNext()) {
    //                        新辺一度出現したExpressions.IntersectWith(Enumerator.Current.Expressions);
    //                    }
    //                }
    //            }
    //            新辺一度出現したExpressions.UnionWith(this.一度出現したExpressions);
    //            //foreach(var a in this.上辺)
    //            //    新辺一度出現したExpressions.IntersectWith(a.一度出現したExpressions);
    //            return 新辺一度出現したExpressions;
    //        }
    //    }
    //    //private int PrivateKeyが最初に出現する辺番号(Expression Key){
    //    //    if(this.一度出現したExpressions.Contains(Key))
    //    //        return this.辺番号;
    //    //    foreach(var a in this.上辺){
    //    //        var 辺番号=this.PrivateKeyが最初に出現する辺番号(Key);
    //    //        if(辺番号>=0) return 辺番号;
    //    //    }
    //    //    return-1;
    //    //}
    //    public int Keyが最初に出現する辺番号(Expression Key){
    //        if(this.一度出現したExpressions.Contains(Key))
    //            return this.辺番号;
    //        foreach(var a in this.上辺){
    //            var 辺番号=a.Keyが最初に出現する辺番号(Key);
    //            if(辺番号>=0) return 辺番号;
    //        }
    //        return-1;
    //    }
    //    public override string ToString(){
    //        var sb=new StringBuilder();
    //        if(this.上辺.Count>0){
    //            sb.Append('{');
    //            foreach(var a in this.上辺){
    //                sb.Append(a);
    //                sb.Append(',');
    //            }
    //            sb.Length--;
    //            sb.Append("}");
    //        }
    //        sb.Append(this.辺番号);
    //        return sb.ToString();
    //    }
    //    //public void Clear(){
    //    //    this.出現順Expressions.Clear();
    //    //    this.一度出現したExpressions.Clear();
    //    //}
    //}
    [DebuggerDisplay("{Display}")]
    public class 辺に関する情報{
        //internal readonly E属性 属性;
        internal readonly int 辺番号;
        //public readonly Generic.List<Expression> 出現順Expressions=new();
        //private readonly Generic.HashSet<Expression> 全ての出現したExpressions;
        //public readonly Generic.List<Expression> Expressions=new();
        private readonly Generic.HashSet<Expression> 節_一度出現したExpressions;
        private readonly Generic.HashSet<Expression> 部分木_一度出現したExpressions;
        private readonly Generic.HashSet<Expression> 部分木_二度出現したExpressions;
        //public 辺に関する情報 上辺0上辺0;
        //public 辺に関する情報 下辺0;
        //public 辺に関する情報? 直列上;
        //public 辺に関する情報? 直列下;
        internal readonly List<辺に関する情報>List親辺=new();
        internal readonly List<辺に関する情報>List子辺=new();
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        public string 親コメント{get;init;}="";
        public string 子コメント{get;set;}="";
        //private readonly Generic.HashSet<Expression>作業Expressions;
        //private bool 二度出現した=false;
        //public 辺に関する情報(ExpressionEqualityComparer ExpressionEqualityComparer):this(ExpressionEqualityComparer,0){
        //}
        public 辺に関する情報(ExpressionEqualityComparer ExpressionEqualityComparer){
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
            //this.全ての出現したExpressions=new(ExpressionEqualityComparer);
            this.節_一度出現したExpressions=new(ExpressionEqualityComparer);
            this.部分木_一度出現したExpressions=new(ExpressionEqualityComparer);
            this.部分木_二度出現したExpressions=new(ExpressionEqualityComparer);
            //this.下辺の一度出現したExpressions=new(ExpressionEqualityComparer);
            //this.作業Expressions=new Generic.HashSet<Expression>(this.ExpressionEqualityComparer);
            this.辺番号=0;
            this.子コメント="最上位";
        }
        public 辺に関する情報(ExpressionEqualityComparer ExpressionEqualityComparer,ref int 辺番号){
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
            this.節_一度出現したExpressions=new(ExpressionEqualityComparer);
            this.部分木_一度出現したExpressions=new(ExpressionEqualityComparer);
            this.部分木_二度出現したExpressions=new(ExpressionEqualityComparer);
            //this.下辺の一度出現したExpressions=new(ExpressionEqualityComparer);
            //this.作業Expressions=new Generic.HashSet<Expression>(this.ExpressionEqualityComparer);
            this.辺番号=辺番号;
            辺番号++;
        }
        public 辺に関する情報(ExpressionEqualityComparer ExpressionEqualityComparer,ref int 辺番号,辺に関する情報 辺に関する情報):this(ExpressionEqualityComparer,ref 辺番号){
            辺に関する情報.List子辺.Add(this);
            this.List親辺.Add(辺に関する情報);
        }
        internal bool 探索済みか;
        private Expression? 部分木_二度出現したExpression;
        public static void 接続(辺に関する情報 親,辺に関する情報 子){
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
        private static readonly Generic.HashSet<Expression> EmptySet=new();
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
        public(Expression?Expression,辺に関する情報?辺に関する情報) 二度出現したExpressionと辺(){
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
        public string Display=>this.ToString();
        public override string ToString(){
            return$"辺番号{this.辺番号} {this.子コメント}";
        }
    }
    private sealed class 判定_分岐があるか:VoidExpressionTraverser_Quoteを処理しない{
        //private readonly List<int> List辺番号;
        //public 判定_分岐があるか(List<int> List辺番号)=>this.List辺番号=List辺番号;
        private bool 分岐があった;
        public bool 実行(BlockExpression Block) {
            this.分岐があった=false;
            this.Block(Block);
            return this.分岐があった;
        }
        protected override void Conditional(ConditionalExpression Conditional)=>this.分岐があった=true;
        protected override void Loop(LoopExpression Loop)=>this.分岐があった=true;
        protected override void Goto(GotoExpression Goto)=>this.分岐があった=true;
        protected override void Switch(SwitchExpression Switch)=>this.分岐があった=true;
    }
    /// <summary>
    /// 辺に関する情報のグラフを作成する。辺に関する情報のListを作成する。
    /// </summary>
    private sealed class 取得_二度出現したExpression0:VoidExpressionTraverser_Quoteを処理しない{
        private readonly 判定_分岐があるか 判定_分岐があるか=new();
        private readonly Generic.Dictionary<LabelTarget,辺に関する情報>Dictionary_LabelTarget_辺に関する情報;
        private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        private readonly 辺に関する情報 Top辺に関する情報;
        private readonly List辺に関する情報 List辺に関する情報;
        //private readonly 辺に関する情報[] 辺に関する情報Array=new 辺に関する情報[辺に関する情報Array_Length];
        public 取得_二度出現したExpression0(辺に関する情報 Top辺に関する情報,List辺に関する情報 List辺に関する情報,Generic.Dictionary<LabelTarget,辺に関する情報> Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer ExpressionEqualityComparer,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる){
            this.Top辺に関する情報=Top辺に関する情報;
            this.List辺に関する情報=List辺に関する情報;
            this.Dictionary_LabelTarget_辺に関する情報=Dictionary_LabelTarget_辺に関する情報;
            this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
        }
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        internal bool IsInline=true;
        private int 辺番号;
        private 辺に関する情報 辺に関する情報=default!;
        private int 計算量;
        public void 実行(Expression Expression0){
            this.計算量=0;
            this.辺番号=1;
            this.Dictionary_LabelTarget_辺に関する情報.Clear();
            this.Top辺に関する情報.Clear();
            var List辺に関する情報=this.List辺に関する情報;
            var Top辺に関する情報=this.Top辺に関する情報;
            List辺に関する情報.Clear();
            List辺に関する情報.Add(Top辺に関する情報);
            this.辺に関する情報=Top辺に関する情報;
            this.判定_左辺Expressionsが含まれる.Clear();
            this.Traverse(Expression0);
            Trace.WriteLine($"取得_二度出現したExpression0.計算量 {this.計算量}");
            Trace.WriteLine(List辺に関する情報.フロー);
        }
        protected override void Traverse(Expression Expression){
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
                    //:が出たら新たな辺
                    var Label=(LabelExpression)Expression;
                    if(Label.DefaultValue is not null)
                        this.Traverse(Label.DefaultValue);
                    if(this.Dictionary_LabelTarget_辺に関する情報.TryGetValue(Label.Target,out var 移動先)){
                        //└┐0 goto 下
                        //..................
                        //┌┘1 下:←ここ
                        //.Goto 下 { };    0   [1,[0]]ここで作った"辺に関する情報"
                        //..................
                        //.Label
                        //.LabelTarget 下:;1   {0}1←ここ
                        this.List辺に関する情報.Add(移動先);
                        this.辺に関する情報=移動先;
                    } else{
                        //始めて出現。後でgoto命令で飛んでループを形成する
                        var 移動元=this.辺に関する情報;
                        if(移動元 is null){
                            //┌←┐    1 L1:←ここ
                            移動先=new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号){親コメント = Label.Target.Name!};
                        } else{
                            //├←┐    1 L1:←ここ
                            移動先=new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号, 移動元){親コメント = Label.Target.Name!};
                        }
                        this.List辺に関する情報.Add(移動先);
                        this.Dictionary_LabelTarget_辺に関する情報.Add(Label.Target,this.辺に関する情報=移動先);
                    }
                    return;
                }
                case ExpressionType.Goto:{
                    //gotoが出たら次は新たな辺
                    var Goto=(GotoExpression)Expression;
                    if(Goto.Value is not null)
                        this.Traverse(Goto.Value);
                    if(this.Dictionary_LabelTarget_辺に関する情報.TryGetValue(Goto.Target,out var 後辺)){
                        //├┐  0 上辺:
                        //..................
                        //└┘    goto 上辺:←ここ
                        //.Label
                        //.LabelTarget 上辺:;
                        //..................
                        //.Goto 上辺 { };    
                        辺に関する情報.接続(this.辺に関する情報,後辺);
                        //var 前辺=this.辺に関する情報!;
                        //前辺.List子辺.Add(後辺);
                        //後辺.List親辺.Add(前辺);
                        //var 移動元=this.辺に関する情報!;

                        //var s0=移動元.上辺String;
                        //var s1=移動元.下辺String;
                        ////移動先.Add移動先(移動元);
                        ////移動元.Add移動元(移動先);
                        //移動先.Add移動元(移動元);
                        //移動元.Add移動先(移動先);
                    } else {
                        //下にジャンプ。条件分岐で多い。
                        //└┐  0 goto 下:←ここ
                        //.Goto 下 { };   ←ここ
                        //└─┐
                        //└┐│goto XX:←ここ
                        //.Goto ...
                        //.Goto XX { };←ここ
                        if(this.辺に関する情報 is not null) {
                            //└┐  0 goto 下:←ここ
                            //.Goto 下 { };   ←ここ
                            var 辺に関する情報=new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号,this.辺に関する情報) { 親コメント=Goto.Target.Name! };
                            //this.List辺に関する情報.Add(辺に関する情報);
                            this.Dictionary_LabelTarget_辺に関する情報.Add(Goto.Target,辺に関する情報);
                        } else {
                            //└─┐
                            //└┐│goto XX:←ここ
                            //.Goto ...
                            //.Goto XX { };←ここ
                            var 辺に関する情報=new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号) { 親コメント=Goto.Target.Name! };
                           // this.List辺に関する情報.Add(辺に関する情報);
                            this.Dictionary_LabelTarget_辺に関する情報.Add(Goto.Target,辺に関する情報);
                        }
                        //this.Dictionary_LabelTarget_辺に関する情報.Add(Goto.Target,this.辺に関する情報);
                    }
                    var デッドコード=this.辺に関する情報=new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号){親コメント="デッドコード"};
                    this.List辺に関する情報.Add(デッドコード);
                    return;
                }
                case ExpressionType.Loop:{
                    var Loop=(LoopExpression)Expression;
                    var BeginLoop=this.辺に関する情報;
                    var LoopBody0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,BeginLoop){親コメント="Begin Loop"};
                    var List辺に関する情報=this.List辺に関する情報;
                    List辺に関する情報.Add(LoopBody0);
                    this.Traverse(Loop.Body);
                    var LoopBody1=this.辺に関する情報;
                    辺に関する情報.接続(LoopBody1,LoopBody0);
                    var EndLoop=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,LoopBody1){親コメント="End Loop"};
                    List辺に関する情報.Add(EndLoop);
                    return;
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
                case ExpressionType.Block:{
                    var Block=(BlockExpression)Expression;
                    if(this.判定_分岐があるか.実行(Block)){
                        base.Block(Block);
                        return;
                    }
                    break;
                }
            }
            if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
                base.Traverse(Expression);
                return;
            }
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
        protected override void AndAlso(BinaryExpression Binary){
            //a&&b
            //(t0=a)&&b
            //if((t0=a).op_True)
            //    t0&b
            //else
            //    t0
            //a&&b&&c
            //(t1=(t0=a)&&b)&&c
            //if((t0=a).op_True)
            //    if((t1=t0&b).op_True)
            //        t1&c
            //    else
            //        t1
            //else
            //    t0
            var Left=Binary.Left;
            var Right=Binary.Right;
            this.Traverse(Left);
            var Test = this.辺に関する情報;
            var Left_ToString=Left.ToString();
            Test.子コメント=Left_ToString;
            var True0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Binary.ToString() };
            this.List辺に関する情報.Add(True0);
            this.Traverse(Expression.And(Left,Right));
            var True1=this.辺に関する情報;
            var False0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Left_ToString };
            this.List辺に関する情報.Add(False0);
            this.Traverse(Left);
            var False1=this.辺に関する情報;
            var End=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号);
            this.List辺に関する情報.Add(End);
            辺に関する情報.接続(True1,End);
            辺に関する情報.接続(False1,End);
        }
        protected override void OrElse(BinaryExpression Binary){
            //a||b
            //(t0=a)||b
            //if((t0=a).op_True)
            //    t0
            //else
            //    t0|b
            //a||b||c
            //(t1=(t0=a)||b)||c
            //if((t0=a).op_True)
            //    t0
            //else
            //    if((t1=t0|b).op_True)
            //        t1
            //    else
            //        t1|c
            var Left=Binary.Left;
            var Right=Binary.Right;
            this.Traverse(Left);
            var Test = this.辺に関する情報;
            var Left_ToString=Left.ToString();
            Test.子コメント=Left_ToString;
            var True0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Left_ToString};
            this.List辺に関する情報.Add(True0);
            this.Traverse(Left);
            var True1=this.辺に関する情報;
            var False0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号,Test ) { 親コメント=Binary.ToString()};
            this.List辺に関する情報.Add(False0);
            this.Traverse(Expression.Or(Left,Right));
            var False1=this.辺に関する情報;
            var End=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号);
            this.List辺に関する情報.Add(End);
            辺に関する情報.接続(True1,End);
            辺に関する情報.接続(False1,End);
        }
        protected override void Conditional(ConditionalExpression Conditional){
            //test　　　　0 test
            //├────┐1 br_false 2 ifFalse
            //ifTrue　　│
            //└───┐│  goto 3 endif
            //┌───┼┘2 ifFalse:
            //ifFalse │　
            //├───┘　3 endif:
            var IfTest=Conditional.Test;
            var IfTrue=Conditional.IfTrue;
            var IfFalse=Conditional.IfFalse;
            this.Traverse(Conditional.Test);
            var Test1 = this.辺に関する情報;
            Test1.子コメント=$"IfTest {IfTest}";
            var True0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号, Test1){親コメント=$"IfTrue {IfTrue}"};
            this.List辺に関する情報.Add(True0);
            this.Traverse(IfTrue);
            var True1=this.辺に関する情報;
            var False0=this.辺に関する情報=new 辺に関する情報(this.ExpressionEqualityComparer,ref this.辺番号,Test1) { 親コメント=$"IfFalse {IfFalse}" };
            this.List辺に関する情報.Add(False0);
            this.Traverse(IfFalse);
            var False1=this.辺に関する情報;
            var End=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号){親コメント="EndIf"};
            this.List辺に関する情報.Add(End);
            辺に関する情報.接続(True1,End);
            辺に関する情報.接続(False1,End);
        }
        protected override void Switch(SwitchExpression Switch){
            this.Traverse(Switch.SwitchValue);
            var Test1 = this.辺に関する情報;
            var sb=new StringBuilder();
            var Cases=Switch.Cases;
            var Cases_Count=Cases.Count;
            var Endに対応する辺Array=new 辺に関する情報[Cases_Count];
            for(var a=0;a<Cases_Count;a++){
                var Case=Cases[a];
                //foreach(var Case in Cases){
                foreach(var TestValue in Case.TestValues){
                    sb.Append(TestValue);
                    sb.Append(',');
                }
                sb[^1]=':';
                var Body=Case.Body;
                sb.Append(Body);
                var Case0=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号, Test1){親コメント=$"Case {sb}"};
                this.List辺に関する情報.Add(Case0);
                this.Traverse(Body);
                Endに対応する辺Array[a]=this.辺に関する情報;
            }
            var End=this.辺に関する情報=new(this.ExpressionEqualityComparer,ref this.辺番号){親コメント="EndIf"};
            this.List辺に関する情報.Add(End);
            foreach(var Endに対応する辺 in Endに対応する辺Array){
                辺に関する情報.接続(Endに対応する辺,End);
            }
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
    private readonly 取得_二度出現したExpression0 _取得_辺;
    private sealed class 取得_二度出現したExpression1:VoidExpressionTraverser_Quoteを処理しない{
        private readonly 判定_分岐があるか 判定_分岐があるか=new();
        private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
        private readonly List辺に関する情報 List辺に関する情報;
        public 取得_二度出現したExpression1(List辺に関する情報 List辺に関する情報,Generic.Dictionary<LabelTarget,辺に関する情報> Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer ExpressionEqualityComparer,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる){
            this.List辺に関する情報=List辺に関する情報;
            this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
        }
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        /// <summary>
        /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
        /// </summary>
        internal bool IsInline=true;
        private int 辺番号;
        private 辺に関する情報 Top辺に関する情報=>this.List辺に関する情報[0];
        private 辺に関する情報 辺に関する情報=default!;
        private int 計算量;
        public void 実行(Expression Expression0){
            this.計算量=0;
            this.辺番号=1;
            this.辺に関する情報=this.Top辺に関する情報;
            this.判定_左辺Expressionsが含まれる.Clear();
            this.Traverse(Expression0);
            this.List辺に関する情報.一度出現したExpressionを上位に移動();
            Trace.WriteLine($"取得_二度出現したExpression1.計算量 {this.計算量}");
        }
        private void 辺インクリメント(){
            this.辺に関する情報=this.List辺に関する情報[this.辺番号++];
        }
        protected override void Traverse(Expression Expression) {
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
                    if(Assign_Left is IndexExpression Index)
                        base.Index(Index);
                    else if(Assign_Left is ParameterExpression){
                        this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                        base.Traverse(Assign.Right);
                    } else{
                        base.Traverse(Assign_Left);//.static_fieldなど
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
                case ExpressionType.Block:{
                    var Block=(BlockExpression)Expression;
                    if(this.判定_分岐があるか.実行(Block)){
                        base.Block(Block);
                        return;
                    }
                    break;
                }
            }
            if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
                base.Traverse(Expression);
                return;
            }
            if(Expression.Type!=typeof(void))
                this.辺に関する情報.Add(Expression);
            base.Traverse(Expression);
        }
        protected override void AndAlso(BinaryExpression Binary){
            //a&&b
            //(t0=a)&&b
            //if((t0=a).op_True)
            //    t0&b
            //else
            //    t0
            //a&&b&&c
            //(t1=(t0=a)&&b)&&c
            //if((t0=a).op_True)
            //    if((t1=t0&b).op_True)
            //        t1&c
            //    else
            //        t1
            //else
            //    t0
            var Binary_Left=Binary.Left;
            this.辺に関する情報.Add(Binary_Left);
            this.Traverse(Binary_Left);
            this.辺インクリメント();
            this.Traverse(Expression.And(Binary_Left,Binary.Right,Binary.Method));
            this.辺インクリメント();
            this.Traverse(Binary_Left);
            this.辺インクリメント();
        }
        protected override void OrElse(BinaryExpression Binary){
            //a||b
            //(t0=a)||b
            //if((t0=a).op_True)
            //    t0
            //else
            //    t0|b
            //a||b||c
            //(t1=(t0=a)||b)||c
            //if((t0=a).op_True)
            //    t0
            //else
            //    if((t1=t0|b).op_True)
            //        t1
            //    else
            //        t1|c
            var Binary_Left=Binary.Left;
            this.辺に関する情報.Add(Binary_Left);
            this.Traverse(Binary.Left);
            this.辺インクリメント();
            this.Traverse(Binary_Left);
            this.辺インクリメント();
            this.Traverse(Expression.Or(Binary_Left,Binary.Right,Binary.Method));
            this.辺インクリメント();
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
    private readonly 取得_二度出現したExpression1 _取得_二度出現したExpression1;
    private sealed class 取得_二度出現したExpression2:VoidExpressionTraverser_Quoteを処理しない{
        private readonly 判定_分岐があるか 判定_分岐があるか=new();
        private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
        private readonly List辺に関する情報 List辺に関する情報;
        public 取得_二度出現したExpression2(List辺に関する情報 List辺に関する情報,判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる){
            this.List辺に関する情報=List辺に関する情報;
            this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
        }
        internal Generic.IEnumerable<Expression> ラムダ跨ぎParameters=default!;
        /// <summary>
        /// Except argumentsを1,0の順で評価する。NoEvaluationで先行評価しないようにする
        /// </summary>
        internal bool IsInline=true;
        private int 辺番号;
        private 辺に関する情報 Top辺に関する情報=>this.List辺に関する情報[0];
        private 辺に関する情報 辺に関する情報=default!;
        private int 計算量;
        public (Expression?二度出現した一度目のExpression,辺に関する情報?辺に関する情報) 実行(Expression Expression0){
            this.計算量=0;
            this.辺番号=1;
            this.辺に関する情報=this.Top辺に関する情報;
            this.判定_左辺Expressionsが含まれる.Clear();
            this.Traverse(Expression0);
            Trace.WriteLine($"取得_二度出現したExpression2.計算量 {this.計算量}");
            return this.List辺に関する情報.二度出現したExpressionと辺();
        }
        private void 辺インクリメント(){
            this.辺に関する情報=this.List辺に関する情報[this.辺番号++];
        }
        protected override void Traverse(Expression Expression) {
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
                    if(Assign_Left is IndexExpression Index)
                        base.Index(Index);
                    else if(Assign_Left is ParameterExpression){
                        this.判定_左辺Expressionsが含まれる.Add(Assign_Left);
                        this.Traverse(Assign.Right);
                    } else{
                        base.Traverse(Assign_Left);//.static_fieldなど
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
                case ExpressionType.Block:{
                    var Block=(BlockExpression)Expression;
                    if(this.判定_分岐があるか.実行(Block)){
                        base.Block(Block);
                        return;
                    }
                    break;
                }
            }
            if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
                base.Traverse(Expression);
                return;
            }
            if(Expression.Type!=typeof(void))
                this.辺に関する情報.二度出現したExpressionの部分木にマークする(Expression);
            base.Traverse(Expression);
        }
        private void AndAlso_OrElse(BinaryExpression Binary){
            //└┬┐　true(a)
            //┌┘│
            //│　│　a&b
            //└─┼┐
            //┌─┘│
            //│　　│a
            //├──┘
            this.Traverse(Binary.Left);
            this.辺インクリメント();
            this.Traverse(Binary.Right);
            this.辺インクリメント();
            //this.Traverse(Binary.Left);
            //this.辺インクリメント();
        }
        protected override void AndAlso(BinaryExpression Binary){
            //a&&b
            //(t0=a)&&b
            //if((t0=a).op_True)
            //    t0&b
            //else
            //    t0
            //a&&b&&c
            //(t1=(t0=a)&&b)&&c
            //if((t0=a).op_True)
            //    if((t1=t0&b).op_True)
            //        t1&c
            //    else
            //        t1
            //else
            //    t0
            this.Traverse(Binary.Left);
            this.辺インクリメント();
            this.Traverse(Binary.Right);
            this.辺インクリメント();
        }
        protected override void OrElse(BinaryExpression Binary)=>this.AndAlso_OrElse(Binary);
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
    private readonly 取得_二度出現したExpression2 _取得_二度出現したExpression2;
    private sealed class 判定_左辺Expressionsが含まれる:VoidExpressionTraverser_Quoteを処理しない {
        private readonly Generic.HashSet<Expression> 左辺Expressions;
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
        private readonly Generic.Dictionary<LabelTarget,辺に関する情報>Dictionary_LabelTarget_辺に関する情報;
        private readonly 判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる;
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        //private readonly Generic.List<int> List辺番号;
        private readonly List辺に関する情報 List辺に関する情報;
        public 変換_二度出現したExpression(作業配列 作業配列,List辺に関する情報 List辺に関する情報,Generic.Dictionary<LabelTarget,辺に関する情報> Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer_Assign_Leftで比較 ExpressionEqualityComparer,
            判定_左辺Expressionsが含まれる 判定_左辺Expressionsが含まれる) : base(作業配列){
            this.List辺に関する情報=List辺に関する情報;
            this.Dictionary_LabelTarget_辺に関する情報=Dictionary_LabelTarget_辺に関する情報;
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
            this.判定_左辺Expressionsが含まれる=判定_左辺Expressionsが含まれる;
        }
        internal Generic.IEnumerable<ParameterExpression> ラムダ跨ぎParameters=default!;
        private Expression? 二度出現した一度目のExpression;
        private ParameterExpression? 二度目以降のParameter;
        private Expression 一度目のAssign=default!;
        //private 辺に関する情報? 二度出現した一度目のExpressionが属する辺に関する情報;
        /// <summary>
        /// Except argumentsを1,0の順で置換する。NoEvaluationで置換しないようにする
        /// </summary>
        internal bool IsInline=true;
        private int 辺番号;
        private 辺に関する情報 辺に関する情報=default!;
        private bool 既に置換された式を走査中;
        private int 計算量;
        public Expression 実行(Expression Expression0,Expression 二度出現した一度目のExpression,辺に関する情報 二度出現した一度目のExpressionが属する辺に関する情報,ParameterExpression 二度目以降のParameter){
            this.計算量=0;
            this.既に置換された式を走査中=false;
            this.辺番号=1;
            this.辺に関する情報=this.List辺に関する情報[0];
            this.二度出現した一度目のExpression = 二度出現した一度目のExpression;
            //this.二度出現した一度目のExpressionが属する辺に関する情報=二度出現した一度目のExpressionが属する辺に関する情報;
            this.判定_左辺Expressionsが含まれる.Clear();
            this.二度目以降のParameter = 二度目以降のParameter;
            this.一度目のAssign = Expression.Assign(二度目以降のParameter,二度出現した一度目のExpression);
            this.判定_左辺Expressionsが含まれる.Clear();
            var Expression1=this.Traverse(Expression0);
            Trace.WriteLine($"変換_二度出現したExpression.計算量 {this.計算量}");
            return Expression1;
        }
        private void 辺インクリメント(){
            this.辺に関する情報=this.List辺に関する情報[this.辺番号++];
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
                    //this.辺番号=this.Dictionary_LabelTarget_辺に関する情報[Label0.Target].辺番号;
                    this.辺に関する情報=this.Dictionary_LabelTarget_辺に関する情報[Label0.Target];
                    return Label1;
                }
                case ExpressionType.Goto:{
                    var Goto0=(GotoExpression)Expression0;
                    var Goto1=this.Goto(Goto0);
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
            if(Expression0.Type!=typeof(void)){
                if(!this.既に置換された式を走査中){
                    if(this.ExpressionEqualityComparer.Equals(Expression0,this.二度出現した一度目のExpression)){
                        if(this.辺に関する情報.この辺に二度出現存在するか_削除する(Expression0)){
                            this.既に置換された式を走査中=true;
                            base.Traverse(Expression0);
                            this.既に置換された式を走査中=false;
                            return this.一度目のAssign;
                        }
                        if(this.辺に関する情報.この辺に存在するか(Expression0)){
                            this.既に置換された式を走査中=true;
                            base.Traverse(Expression0);
                            this.既に置換された式を走査中=false;
                            return this.二度目以降のParameter!;
                        }
                    }
                }
            }
            return base.Traverse(Expression0);
        }
        protected override Expression Conditional(ConditionalExpression Conditional0){
            var Conditional0_Test=Conditional0.Test;
            var Conditional0_IfTrue=Conditional0.IfTrue;
            var Conditional0_IfFalse=Conditional0.IfFalse;
            //var List子辺=this.辺に関する情報.List親辺;

            var Conditional1_Test=this.Traverse(Conditional0_Test);
            //this.辺に関する情報=List子辺[0];
            this.辺インクリメント();
            var Conditional1_IfTrue=this.Traverse(Conditional0_IfTrue);
            this.辺インクリメント();
            var Conditional1_IfFalse=this.Traverse(Conditional0_IfFalse);
            this.辺インクリメント();
            //this.辺に関する情報=this.辺に関する情報.List子辺[0];
            var Test=Conditional0.Test;
            var IfTrue=Conditional0.IfTrue;
            var IfFalse=Conditional0.IfFalse;
            if(Test==Conditional1_Test)
                if(IfTrue==Conditional1_IfTrue)
                    if(IfFalse==Conditional1_IfFalse)
                        return Conditional0;
            return Expression.Condition(Conditional1_Test,Conditional1_IfTrue,Conditional1_IfFalse,Conditional0.Type);
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
    private readonly Generic.List<ParameterExpression> ListスコープParameter;
    public sealed class List辺に関する情報:List<辺に関する情報>{
        const int 列数=20;
        //const int 最大列数 = 10;
        public string フロー {
            get {
                var Count = this.Count;
                //var 列Array0=new(辺に関する情報? 移動元,辺に関する情報? 移動先)[最大列数];
                var 列Array0=new List<(辺に関する情報? 移動元,辺に関する情報? 移動先)>{(null,null)};
                var Line=new StringBuilder();
                Line.Append('　');
                var 前回のLine=Line.ToString();
                var 辺に関する情報Array = this.ToArray();
                //列Array0[0]=(null, 辺に関する情報Array[0]);
                var 直前文字 = '┌';
                辺に関する情報? 上辺=null;
                var sb = new StringBuilder();
                for(var a = 0;a<Count;a++) {
                    var 辺 = 辺に関する情報Array[a];
                    var 下辺=a<Count-1?辺に関する情報Array[a+1]:null;
                    var 親辺Array = 辺.List親辺.ToArray();
                    var 子辺Array = 辺.List子辺.ToArray();
                    //for(var c=列offset;c<最大列数;c++){
                    //    if(列Array0[c].移動元 is null)
                    //        Line[c]='　';
                    //    else
                    //        Line[c]='│';
                    //}
                    親(親辺Array,列Array0,ref 前回のLine,辺,sb,Line,上辺,ref 直前文字);
                    //for(var b=0;b<Line.Length;b++)
                    //    if(Line[b]is'┌'or'┬'or'┼'or'┐'or'│')
                    //        Line[b]='│';
                    //    else
                    //        Line[b]='　';
                    //sb.AppendLine(Line.ToString());
                    子(子辺Array,列Array0,ref 前回のLine,辺,sb,Line,下辺,ref 直前文字);
                    上辺=辺;
                }
                var s = sb.ToString();
                //Trace.WriteLine(s);
                return sb.ToString();
            }
        }
        public void 一度出現したExpressionを上位に移動(){
            foreach(var a in this) a.探索済みか=false;
            this[0].Create部分木_二度出現したExpressions();
        }
        public (Expression?Expression,辺に関する情報?辺に関する情報) 二度出現したExpressionと辺(){
            foreach(var a in this) a.探索済みか=false;
            return this[0].二度出現したExpressionと辺();
        }
        
        private static void Output(string Line,StringBuilder sb,string コメント){
            //Trace.WriteLine(Line+コメント);
            sb.AppendLine(Line+コメント);
        }
        private static void 書き込み(StringBuilder Line,int index,char c){
            if(index<Line.Length)
                Line[index]=c;
            else
                Line.Append(c);
        }
        private static void Line初期化(List<(辺に関する情報? 移動元,辺に関する情報? 移動先)>列Array0,StringBuilder Line){
            for(var b = 0;b<列Array0.Count;b++)
                if(列Array0[b].移動元 is null)
                    Line[b]='　';
                else
                    Line[b]='│';
        }
        private static void 親(辺に関する情報[] 親辺Array,List<(辺に関する情報? 移動元,辺に関する情報? 移動先)>列Array0,ref string 前回のLine,辺に関する情報 辺,StringBuilder sb,StringBuilder Line,辺に関する情報? 上辺,ref char 直前文字){
            var 親辺Array_Length=親辺Array.Length;
            if(親辺Array_Length<=0)return;
#if 省略
#else
            if(親辺Array_Length==1)
                if(上辺 is not null)
                    if(上辺.List子辺.Count==1)
                        if(上辺.List子辺[0]==辺)
                            return;
#endif
            //if(直前文字=='├') return;
            //if(親辺Array.Contains(上辺))
            //    if(親辺Array_Length==1) {
            //        //直前文字='│';
            //        //列Array0[0]=(上辺, 辺に関する情報);
            //        return;
            //    } else
            //        直前文字='├';
            //else
            //直前文字='┌';
            Line初期化(列Array0,Line);
            Line[0]='┌';
            var サフィックス="";
            var ループか=false;
            var 書き換えLineIndexEnd=-1;
            for(var a=0;a<親辺Array_Length;a++){
                var 親辺=親辺Array[a];
                var Count=列Array0.Count;
                for(var b=0;b<Count;b++){
                    var 列=列Array0[b];
                    if(列.移動元==親辺) {
                        if(列.移動先==辺) {
                            if(Line[b]!='┘') {
                                サフィックス=列.ToString();
                                //親辺Array[a]=null!;
                                if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                                Line[b]='┘';
                                goto 終了;
                            }
                        }
                    }
                }
                for(var b=0;b<Count;b++){
                    if(Line[b]is'　'){
                        //if(列Array0[b].移動元 is null){
                        列Array0[b]=(親辺,辺);
                        サフィックス=列Array0[b].ToString();
                        if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                        Line[b]='┐';
                        ループか=true;
                        goto 終了;
                    } else{
                        Debug.Assert(Line[b]!='　');
                    }
                }
                書き換えLineIndexEnd=Line.Length;
                列Array0.Add((親辺,辺));
                Line.Append('┐');
                終了: ;
            }
            {
                var a=0;
                if(Line[a]=='┐'){
                    if(前回のLine[0]is'┌'or'├')
                        Line[a]='├';
                    else
                        Line[a]='┌';
                } else if(Line[a]=='┘'){
                    Debug.Fail("gg");
                    //Line[a]='┴';
                }
            }
            for(var a=1;a<書き換えLineIndexEnd;a++){
                if(Line[a]=='　'){
                    Line[a]='─';
                } else if(Line[a]=='┐'){
                    Line[a]='┬';
                } else if(Line[a]=='┘'){
                    Line[a]='┴';
                    列Array0[a]=(null,null);
                } else if(Line[a]=='│'){
                    Line[a]='┼';
                }
            }
            if(!ループか)
                列Array0[書き換えLineIndexEnd]=(null,null);
            直前文字=Line[0];
            var 行=Line.ToString();
            前回のLine=行;
            Output(行,sb,$"{辺.辺番号},{辺.親コメント}");
        }
        private static void 子(辺に関する情報[] 子辺Array,List<(辺に関する情報? 移動元,辺に関する情報? 移動先)>列Array0,ref string 前回のLine,辺に関する情報 辺,StringBuilder sb,StringBuilder Line,辺に関する情報? 下辺,ref char 直前文字){
            var 子辺Array_Length=子辺Array.Length;
            if(子辺Array_Length<=0)return;
#if 省略
#else
            if(子辺Array_Length==1)
                if(下辺 is not null)
                    if(下辺.List親辺.Count==1)
                        if(下辺.List親辺[0]==辺)
                            return;
#endif
            //if(子辺Array_Length==1)
            //    if(子辺Array[0]==下辺)
            //        return;
            //    if(子辺Array_Length==1)
            //sb.Append(直前文字);
            //列Array0.Clear();
            Line初期化(列Array0,Line);
            Line[0]='└';
            //Line.Append('└');
            var ループか=false;
            var 書き換えLineIndexEnd=-1;
            for(var a=0;a<子辺Array_Length;a++){
                var 子辺=子辺Array[a];
                var Count=列Array0.Count;
                //var Length=Line.Length;
                for(var b=0;b<Count;b++){
                    var 列=列Array0[b];
                    if(列.移動元==辺){
                        if(列.移動先==子辺){
                            if(Line[b]=='│'){
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
                        //列Array0.Add((辺,子辺));
                        if(書き換えLineIndexEnd<b) 書き換えLineIndexEnd=b;
                        Line[b]='┐';
                        //後半を埋める(c);
                        goto 終了;
                    } 
                }
                書き換えLineIndexEnd=Line.Length;
                列Array0.Add((辺,子辺));
                Line.Append('┐');
                終了: ;
            }
            //var サフィックス="";
            for(var a=0;a<書き換えLineIndexEnd;a++){
                if(Line[a]=='　'){
                    Line[a]='─';
                }else if(Line[a]=='┘'){
                    Line[a]='┴';
                }else if(Line[a]=='│'){
                    Line[a]='┼';
                } else if(Line[a]=='┐'){
                    //ref var 列=ref 列Array0[a];
                    //列=(辺に関する情報,列.移動先);
                    //サフィックス=列.ToString();
                    Line[a]='┬';
                }
            }
            if(ループか)
                列Array0[書き換えLineIndexEnd]=(null,null);
            直前文字=Line[0];
            var 行=Line.ToString();
            前回のLine=行;
            Output(行,sb,$"{辺.辺番号},{辺.子コメント}");
        }
        //private static void Line初期化(List<(辺に関する情報? 移動元,辺に関する情報? 移動先)>列Array0,StringBuilder Line){
        //    for(var b=0;b<列Array0.Length-1;b++)
        //        if(列Array0[b].移動元 is null)
        //            Line[b]='　';
        //        else
        //            Line[b]='│';
        //}
    }
    //private readonly List<辺に関する情報>_List辺に関する情報;
    private readonly List辺に関する情報 _List辺に関する情報;
    private readonly Generic.Dictionary<LabelTarget,辺に関する情報> Dictionary_LabelTarget_辺に関する情報=new();
    internal 変換_局所Parameterの先行評価(作業配列 作業配列) : base(作業配列){
        var ListスコープParameter=this.ListスコープParameter=new();
        var ExpressionEqualityComparer=new ExpressionEqualityComparer_Assign_Leftで比較(ListスコープParameter);
        var 判定_左辺Parametersが含まれる=new 判定_左辺Expressionsが含まれる(ExpressionEqualityComparer);
        var Dictionary_LabelTarget_辺に関する情報=this.Dictionary_LabelTarget_辺に関する情報;
        //var List変換対象=new Generic.List<辺に関する情報>();
        var Top辺に関する情報=new 辺に関する情報(ExpressionEqualityComparer);
        var List辺に関する情報=this._List辺に関する情報=new();
        this._取得_辺=new(Top辺に関する情報,List辺に関する情報,Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer,判定_左辺Parametersが含まれる);
        this._取得_二度出現したExpression1=new(List辺に関する情報,Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer,判定_左辺Parametersが含まれる);
        this._取得_二度出現したExpression2=new(List辺に関する情報,判定_左辺Parametersが含まれる);
        this._変換_二度出現したExpression=new(作業配列,List辺に関する情報,Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer,判定_左辺Parametersが含まれる);
    }
    internal Generic.IEnumerable<ParameterExpression> ラムダ跨ぎParameters{
        set{
            this._取得_辺.ラムダ跨ぎParameters=value;
            this._取得_二度出現したExpression1.ラムダ跨ぎParameters=value;
            this._取得_二度出現したExpression2.ラムダ跨ぎParameters=value;
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
            this._取得_二度出現したExpression2.IsInline=value;
            this._変換_二度出現したExpression.IsInline=value;
        }
    }
    public Expression 実行(Expression Expression0) {
        this.ListスコープParameter.Clear();
        Debug.Assert(Expression0.NodeType==ExpressionType.Lambda&&this.ListスコープParameter.Count==0);
        this.番号=0;
        var Lambda1 = (LambdaExpression)this.Traverse(Expression0);
        return Expression.Lambda(Lambda1.Type,Lambda1.Body,Lambda1.Name,Lambda1.TailCall,Lambda1.Parameters);
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
    public string フロー=>this._List辺に関する情報.フロー;
    protected override Expression Lambda(LambdaExpression Lambda0){
        var ListスコープParameter = this.ListスコープParameter;
        var ListスコープParameter_Count = ListスコープParameter.Count;
        var Lambda0_Parameters = Lambda0.Parameters;
        ListスコープParameter.AddRange(Lambda0_Parameters);
        var Block0_Variables = this.Block_Variables;
        var Block1_Variables = this.Block_Variables=new();
        var Lambda1_Body = Lambda0.Body;
        var BlockVariables = this.Block_Variables;
        var 取得_辺=this._取得_辺;
        var 取得_二度出現したExpression1 = this._取得_二度出現したExpression1;
        var 取得_二度出現したExpression2 = this._取得_二度出現したExpression2;
        var 変換_旧Expressionを新Expressionに二度置換 = this._変換_二度出現したExpression;
        while(true) {
            取得_辺.実行(Lambda1_Body);
            取得_二度出現したExpression1.実行(Lambda1_Body);
            var (二度出現した一度目のExpression,辺に関する情報)=取得_二度出現したExpression2.実行(Lambda1_Body);
            if(二度出現した一度目のExpression is null){
                //ここで置換再開直前Label0,置換再開Expression0をインクリメントする
                break;
            }
            var Variable = Expression.Variable(二度出現した一度目のExpression.Type,$"局所{this.番号++}");
            BlockVariables.Add(Variable);
            ListスコープParameter.Add(Variable);
            var Expression0 = 変換_旧Expressionを新Expressionに二度置換.実行(Lambda1_Body,二度出現した一度目のExpression,辺に関する情報,Variable);
            Lambda1_Body=Expression0;
        }
        var Lambda2_Body = this.Traverse(Lambda1_Body);
        this.Block_Variables=Block0_Variables;
        if(Block1_Variables.Count>0) Lambda2_Body=Expression.Block(Block1_Variables,this.作業配列.Expressions設定(Lambda2_Body));
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
        if(Conditional0_Test==Conditional1_Test)
            if(Conditional0_IfTrue==Conditional1_IfTrue)
                if(Conditional0_IfFalse==Conditional1_IfFalse)
                    return Conditional0;
        return Expression.Condition(Conditional1_Test,Conditional1_IfTrue,Conditional1_IfFalse,Conditional0.Type);
        Expression 共通(Expression Expression0){
            var Block_Variables=this.Block_Variables;
            var 取得_二度出現したExpression1 = this._取得_二度出現したExpression1;
            var 取得_二度出現したExpression2 = this._取得_二度出現したExpression2;
            var 変換_旧Expressionを新Expressionに二度置換 = this._変換_二度出現したExpression;
            while(true) {
                var (二度出現した一度目のExpression,辺に関する情報)  = 取得_二度出現したExpression2.実行(Expression0);
                if(二度出現した一度目のExpression is null)break;
                var Variable=Expression.Variable(二度出現した一度目のExpression.Type,$"局所{this.番号++}");
                Block_Variables.Add(Variable);
                var Expression1=変換_旧Expressionを新Expressionに二度置換.実行(Expression0,二度出現した一度目のExpression,辺に関する情報,Variable);
                Expression0=Expression1;
            }
            return this.Traverse(Expression0);
        }
    }
}
//20231128  719
//20231208 2516
//         1716