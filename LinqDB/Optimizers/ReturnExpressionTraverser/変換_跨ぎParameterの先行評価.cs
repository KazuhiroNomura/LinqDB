using System;
using System.Diagnostics;
//using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using LinqDB.Optimizers.Comparer;
using LinqDB.Sets;
using LinqDB.Optimizers.VoidExpressionTraverser;

namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using Generic = System.Collections.Generic;
using static Common;
internal sealed class 変換_跨ぎParameterの先行評価:ReturnExpressionTraverser_Quoteを処理しない{
    /// <summary>
    /// ループ+ラムダ=ラムダ
    /// </summary>
    [Flags]private enum 場所{None=0b000,ループ跨ぎ=0b001,ラムダ跨ぎ=0b011}
    private sealed class 取得_先行評価式:VoidExpressionTraverser_Quoteを処理しない {
        private readonly Generic.List<(Generic.IEnumerable<ParameterExpression>Parameters,Generic.List<Generic.IEnumerable<ParameterExpression>>ListVariables)> List束縛Parameter情報;
        private sealed class 判定_移動できるか:VoidExpressionTraverser_Quoteを処理しない {
            private readonly Generic.List<(Generic.IEnumerable<ParameterExpression>Parameters,Generic.List<Generic.IEnumerable<ParameterExpression>>ListVariables)>List束縛Parameter情報;
            private readonly Generic.List<Generic.IEnumerable<ParameterExpression>> List内部Parameters=new();
            private readonly Generic.IEnumerable<Expression>ループ跨ぎParameters;
            //private readonly Generic.List<Generic.IEnumerable<ParameterExpression>> List移動できないVariables;
            public 判定_移動できるか(Generic.List<(Generic.IEnumerable<ParameterExpression> Parameters,Generic.List<Generic.IEnumerable<ParameterExpression>> ListVariables)> List束縛Parameter情報,Generic.IEnumerable<Expression> ループ跨ぎParameters) {
                this.List束縛Parameter情報=List束縛Parameter情報;
                this.ループ跨ぎParameters=ループ跨ぎParameters;
                //this.List移動できないVariables=List移動できないVariables;
            }
            internal Generic.Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=default!;
            private Generic.IEnumerable<ParameterExpression> ラムダ跨ぎParameters=>
                this.Dictionaryラムダ跨ぎParameter.Keys;
            private readonly ParameterExpression ContainerParameter=default!;
            private EResult Result;
            public enum EResult {
                移動できる,
                移動できない,
                NoLoopUnrollingがあったので移動できない
            }
            public EResult 実行(Expression Expression) {
                this.Result=EResult.移動できる;
                this.List内部Parameters.Clear();
                this.Traverse(Expression);
                return this.Result;
            }
            //protected override void Call(MethodCallExpression MethodCall) {
            //    if(false&&MethodCall.Method.IsGenericMethod&&Reflection.Helpers.NoLoopUnrolling==MethodCall.Method.GetGenericMethodDefinition())
            //        this.Result=EResult.NoLoopUnrollingがあったので移動できない;
            //    else
            //        base.Call(MethodCall);
            //}
            protected override void MakeAssign(BinaryExpression Binary)=>this.Result=EResult.移動できない;
            protected override void Parameter(ParameterExpression Parameter) {
                if(this.ContainerParameter==Parameter)return;
                if(this.ラムダ跨ぎParameters.Contains(Parameter))return;
                if(this.ループ跨ぎParameters.Contains(Parameter))return;
                foreach(var 内部Parameters in this.List内部Parameters)
                    if(内部Parameters.Contains(Parameter)) return;
                foreach(var a in this.List束縛Parameter情報){
                    //外部Lambdaの変数が内部で使われていたら外だしする
                    if(a.Parameters.Contains(Parameter)) return;
                    foreach(var Variables in a.ListVariables)
                        if(Variables.Contains(Parameter)){
                            //Blockの変数が内部で使われていても移動しない
                            this.Result=EResult.移動できない;
                            return;
                        }
                }
                this.Result=EResult.移動できない;
            }
            protected override void Lambda(LambdaExpression Lambda) {
                var List内部Parameters = this.List内部Parameters;
                var index = List内部Parameters.Count;
                var Lambda_Parameters=Lambda.Parameters;
                List内部Parameters.Add(Lambda_Parameters);
                this.Traverse(Lambda.Body);
                List内部Parameters.RemoveAt(index);
            }
        }
        private readonly 判定_移動できるか _判定_移動できるか;
        private readonly Generic.IEnumerable<Expression>ループ跨ぎParameters;
        private readonly Generic.List<Generic.IEnumerable<ParameterExpression>> List移動できないVariables=new();
        [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
        public 取得_先行評価式(Generic.List<(Generic.IEnumerable<ParameterExpression>Parameters,Generic.List<Generic.IEnumerable<ParameterExpression>>ListVariables)> List束縛Parameter情報,Generic.IEnumerable<Expression>ループ跨ぎParameters) {
            this.List束縛Parameter情報=List束縛Parameter情報;
            //List束縛Parameter情報を使う必要がア
            this._判定_移動できるか=new 判定_移動できるか(List束縛Parameter情報,ループ跨ぎParameters);
            this.ループ跨ぎParameters=ループ跨ぎParameters;
        }

        internal Generic.Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter{
            get=>this._判定_移動できるか.Dictionaryラムダ跨ぎParameter;
            set=>this._判定_移動できるか.Dictionaryラムダ跨ぎParameter=value;
        }
        private Generic.IEnumerable<Expression> ラムダ跨ぎParameters=>this.Dictionaryラムダ跨ぎParameter.Keys;
        private 場所 結果の場所;
        private Expression?結果Expression;
        public (場所 結果の場所, Expression? 分離Expression) 実行(Expression Expression) {
            this.結果の場所=場所.None;
            this.結果Expression=null;
            this.Traverse(Expression);
            return (this.結果の場所, this.結果Expression);
        }
        public bool IsInline;
        protected override void Block(BlockExpression Block) {
            var List移動できないVariables = this.List移動できないVariables;
            var List移動できないVariables_Count = List移動できないVariables.Count;
            List移動できないVariables.Add(Block.Variables);
            base.Block(Block);
            List移動できないVariables.RemoveAt(List移動できないVariables_Count);
            //var List束縛Parameter情報 = this.List束縛Parameter情報;
            //var ListVariables=List束縛Parameter情報[^1].ListVariables;
            //var ListVariables_Count = ListVariables.Count;
            //ListVariables.Add(Block.Variables);
            //base.Block(Block);
            //ListVariables.RemoveAt(ListVariables_Count);
        }
        protected override void Lambda(LambdaExpression Lambda) {
            var 結果の場所 = this.結果の場所;
            Debug.Assert(this.結果Expression is null);
            this.結果の場所=結果の場所|場所.ラムダ跨ぎ;
            this.Traverse(Lambda.Body);
            if(this.結果Expression is not null) return;
            this.結果の場所=結果の場所;
        }
        //protected override void MakeAssign(BinaryExpression Binary){
        //    if(Binary.Left is not ParameterExpression)
        //        this.Traverse(Binary.Left);
        //    this.Traverse(Binary.Right);
        //}
        protected override void Traverse(Expression e) {
            if(this.結果Expression is not null)return;
            switch(e.NodeType) {
                case ExpressionType.Constant: {
                    if(ILで直接埋め込めるか((ConstantExpression)e))return;
                    break;
                }
                case ExpressionType.Default:return;
                case ExpressionType.Parameter: {
                    if(this.ラムダ跨ぎParameters.Contains(e))return;
                    if(this.ループ跨ぎParameters.Contains(e))return;
                    break;
                }
            }
            if(e.Type!=typeof(void)) {
                if(this.結果の場所==場所.ループ跨ぎ) {
                    if((e.NodeType!=ExpressionType.Lambda)&&e.NodeType!=ExpressionType.Parameter) {
                        var Result = this._判定_移動できるか.実行(e);
                        if(Result==判定_移動できるか.EResult.移動できる) {
                            this.結果Expression=e;
                            return;
                        }
                        //if(Result==判定_移動できるか.EResult.NoLoopUnrollingがあったので移動できない)return;
                    }
                } else if(this.結果の場所==場所.ラムダ跨ぎ) {
                    if(e.NodeType!=ExpressionType.Lambda) {
                        var Result = this._判定_移動できるか.実行(e);
                        if(Result==判定_移動できるか.EResult.移動できる) {
                            this.結果Expression=e;
                            return;
                        }
                        //if(Result==判定_移動できるか.EResult.NoLoopUnrollingがあったので移動できない)return;
                    }
                }
            }
            base.Traverse(e);
        }
        protected override void Call(MethodCallExpression MethodCall) {
            var MethodCall_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall.Method);
            if(this.IsInline){
                if(ループ展開可能メソッドか(MethodCall_GenericMethodDefinition)){
                    var MethodCall0_Arguments=MethodCall.Arguments;
                    switch(MethodCall_GenericMethodDefinition.Name){
                        //ラムダ跨ぎ先行評価の不具合
                        case nameof(ExtensionSet.Inline):{
                            if(MethodCall0_Arguments.Count==1){
                                Debug.Assert(Reflection.ExtensionSet.Inline1==MethodCall_GenericMethodDefinition);
                                巻き上げ処理(MethodCall0_Arguments[0]);
                            } else{
                                Debug.Assert(MethodCall0_Arguments.Count==2);
                                Debug.Assert(Reflection.ExtensionSet.Inline2==MethodCall_GenericMethodDefinition);
                                巻き上げ処理(MethodCall0_Arguments[1]);
                            }
                            break;
                        }
                        case nameof(ExtensionSet.Intersect):
                        case nameof(ExtensionSet.Union):
                        case nameof(ExtensionSet.DUnion):
                        case nameof(ExtensionSet.Except):{
                            //ループの外出しをしない理由はインライン展開するときにそれぞれ別に処理する2パス方式だから
                            break;
                        }
                        default:{
                            Debug.Assert(nameof(ExtensionSet.Join)!=MethodCall_GenericMethodDefinition.Name,"SelectMany,Whereで処理される");
                            Debug.Assert(nameof(ExtensionSet.GroupJoin)!=MethodCall_GenericMethodDefinition.Name,"Select,Whereで処理される");
                            this.Traverse(MethodCall0_Arguments[0]);
                            if(this.結果Expression is not null) return;
                            var MethodCall0_Arguments_Count=MethodCall0_Arguments.Count;
                            for(var a=1;a<MethodCall0_Arguments_Count;a++)
                                if(巻き上げ処理(MethodCall0_Arguments[a]))
                                    return;
                            break;
                        }
                    }
                } else{
                    base.Call(MethodCall);
                }
            } else{
                base.Call(MethodCall);
            }
            bool 巻き上げ処理(Expression Expression0) {
                var 結果の場所 = this.結果の場所;
                Debug.Assert(this.結果Expression is null);
                if(Expression0 is LambdaExpression Lambda0) {
                    this.結果の場所=結果の場所|場所.ループ跨ぎ;
                    this.Traverse(Lambda0.Body);
                    if(this.結果Expression is not null)
                        return true;
                    this.結果の場所=結果の場所;
                } else if(Expression0.NodeType!=ExpressionType.Parameter){
                    Debug.Assert(判定_移動できるか.EResult.移動できる==this._判定_移動できるか.実行(Expression0));
                    this.結果Expression=Expression0;
                    return true;
                }
                return false;
            }
        }
    }
    private readonly 取得_先行評価式 _取得_先行評価式;
    private sealed class 変換_先行評価式:ReturnExpressionTraverser {
        private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        public 変換_先行評価式(作業配列 作業配列,ExpressionEqualityComparer ExpressionEqualityComparer) : base(作業配列) =>
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
        private bool 書き込み項か;
        private bool 読み込みがあるか;
        private bool 書き戻しがあるか;
        private Expression? 旧Expression;
        private Expression? 新Parameter;
        private 場所 現在探索場所;
        private 場所 希望探索場所;
        public bool IsInline;
        public (Expression Expression,bool 読み込みがあるか,bool 書き込みがあるか) 実行(Expression Expression,Expression 旧Expression,Expression 新Parameter,場所 希望探索場所) {
            this.読み込みがあるか=false;
            this.書き戻しがあるか=false;
            this.旧Expression=旧Expression;
            this.新Parameter=新Parameter;
            this.現在探索場所=場所.None;
            this.希望探索場所=希望探索場所;
            return (this.Traverse(Expression),this.読み込みがあるか,this.書き戻しがあるか);
        }
        protected override Expression MakeAssign(BinaryExpression Binary0,ExpressionType NodeType) {
            var 書き込み項か=this.書き込み項か;
            var Binary0_Left = Binary0.Left;
            var Binary0_Right = Binary0.Right;
            var Binary0_Conversion = Binary0.Conversion;
            this.書き込み項か=false;
            var Binary1_Right = this.Traverse(Binary0_Right);
            this.書き込み項か=true;
            var Binary1_Left=this.Traverse(Binary0_Left);
            this.書き込み項か=書き込み項か;
            var Binary1_Conversion = this.TraverseNullable(Binary0_Conversion);
            //if(Binary0_Right==Binary1_Right)
            //    if(Binary0_Left==Binary1_Left)
            //        if(Binary0_Conversion==Binary1_Conversion)
            //            return Binary0;
            return Expression.MakeBinary(NodeType,Binary1_Left,Binary1_Right,Binary0.IsLiftedToNull,Binary0.Method,Binary1_Conversion as LambdaExpression);
        }
        protected override Expression Block(BlockExpression Block0){
            var Block0_Expressions = Block0.Expressions;
            var Block0_Variables=Block0.Variables;
            //var スコープParameters=this.ExpressionEqualityComparer.スコープParameters;
            //var スコープParameters_Count=スコープParameters.Count;
            //スコープParameters.AddRange(Block0_Variables);
            var Block0_Expressions_Count = Block0_Expressions.Count;
            Debug.Assert(Block0_Expressions_Count>1||Block0_Variables.Count>0&&Block0_Expressions.Count==1,"最適化されてありえないはず。");
            var Block1_Expressions = new Expression[Block0_Expressions_Count];
            for(var a = 0;a<Block0_Expressions_Count;a++)
                Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
            //スコープParameters.RemoveRange(スコープParameters_Count,Block0_Variables.Count);
            return Expression.Block(Block0_Variables,Block1_Expressions);
        }
        protected override Expression Lambda(LambdaExpression Lambda0){
            var 現在探索場所 = this.現在探索場所;
            this.現在探索場所=現在探索場所|場所.ラムダ跨ぎ;
            var Lambda0_Parameters=Lambda0.Parameters;
            //var スコープParameters=this.ExpressionEqualityComparer.スコープParameters;
            //var スコープParameters_Count=スコープParameters.Count;
            //スコープParameters.AddRange(Lambda0_Parameters);
            var Lambda1_Body=this.Traverse(Lambda0.Body);
            //スコープParameters.RemoveRange(スコープParameters_Count,Lambda0_Parameters.Count);
            this.現在探索場所=現在探索場所;
            return Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0.Parameters);
        }
        protected override Expression Call(MethodCallExpression MethodCall0){
            var MethodCall_GenericMethodDefinition=GetGenericMethodDefinition(MethodCall0.Method);
            //if(false&&Reflection.Helpers.NoLoopUnrolling==MethodCall_GenericMethodDefinition)
            //    return MethodCall0;
            if(this.IsInline){
                if(ループ展開可能メソッドか(MethodCall0)){
                    var MethodCall0_Arguments=MethodCall0.Arguments;
                    var MethodCall0_Arguments_Count=MethodCall0_Arguments.Count;
                    var MethodCall1_Arguments=new Expression[MethodCall0_Arguments_Count];
                    switch(MethodCall_GenericMethodDefinition.Name){
                        case nameof(ExtensionSet.Inline):{
                            if(MethodCall0_Arguments.Count==1){
                                Debug.Assert(Reflection.ExtensionSet.Inline1==MethodCall_GenericMethodDefinition);
                                var MethodCall0_Arguments_0=MethodCall0_Arguments[0];
                                if(MethodCall0_Arguments_0 is LambdaExpression Lambda0){
                                    var 現在探索場所=this.現在探索場所;
                                    this.現在探索場所=現在探索場所|場所.ループ跨ぎ;
                                    var Lambda1_Body=this.Traverse(Lambda0.Body);
                                    MethodCall1_Arguments[0]=Expression.Lambda(
                                        Lambda0.Type,
                                        Lambda1_Body,
                                        Lambda0.Name,
                                        Lambda0.TailCall,
                                        Lambda0.Parameters
                                    );
                                    this.現在探索場所=現在探索場所;
                                } else{
                                    MethodCall1_Arguments[0]=this.Traverse(MethodCall0_Arguments_0);
                                }
                            } else{
                                Debug.Assert(MethodCall0_Arguments.Count==2);
                                Debug.Assert(Reflection.ExtensionSet.Inline2==MethodCall_GenericMethodDefinition);
                                MethodCall1_Arguments[0]=this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall0_Arguments_1=MethodCall0_Arguments[1];
                                if(MethodCall0_Arguments_1 is LambdaExpression Lambda0){
                                    var 現在探索場所=this.現在探索場所;
                                    this.現在探索場所=現在探索場所|場所.ループ跨ぎ;
                                    var Lambda1_Body=this.Traverse(Lambda0.Body);
                                    MethodCall1_Arguments[1]=Expression.Lambda(
                                        Lambda0.Type,
                                        Lambda1_Body,
                                        Lambda0.Name,
                                        Lambda0.TailCall,
                                        Lambda0.Parameters
                                    );
                                    this.現在探索場所=現在探索場所;
                                } else{
                                    MethodCall1_Arguments[1]=this.Traverse(MethodCall0_Arguments_1);
                                }
                            }
                            break;
                        }
                        default:{
                            MethodCall1_Arguments[0]=this.Traverse(MethodCall0_Arguments[0]);
                            var 現在探索場所=this.現在探索場所;
                            for(var a=1;a<MethodCall0_Arguments_Count;a++){
                                var MethodCall_Arguments_a=MethodCall0_Arguments[a];
                                //Debug.Assert(MethodCall_Arguments_a.NodeType==ExpressionType.Lambda
                                //したいところだがAggregate(a,b,func)のbがLambdaではないので使えない。
                                if(MethodCall_Arguments_a is LambdaExpression Lambda0){
                                    this.現在探索場所=現在探索場所|場所.ループ跨ぎ;
                                    var Lambda1_Body=this.Traverse(Lambda0.Body);
                                    this.現在探索場所=現在探索場所;
                                    MethodCall1_Arguments[a]=Expression.Lambda(
                                        Lambda0.Type,
                                        Lambda1_Body,
                                        Lambda0.Name,
                                        Lambda0.TailCall,
                                        Lambda0.Parameters
                                    );
                                } else{
                                    MethodCall1_Arguments[a]=this.Traverse(MethodCall_Arguments_a);
                                }
                            }
                            break;
                        }
                    }
                    return Expression.Call(
                        MethodCall0.Method,
                        MethodCall1_Arguments
                    );
                }
            }
            return base.Call(MethodCall0);
        }
        protected override Expression Traverse(Expression Expression0) {
            if(this.現在探索場所==this.希望探索場所){
                if(this.ExpressionEqualityComparer.Equals(Expression0,this.旧Expression!)){
                    if(this.書き込み項か)
                        this.書き戻しがあるか=true;
                    else
                        this.読み込みがあるか=true;
                    return this.新Parameter!;
                }
            }
            var 書き込み項か=this.書き込み項か;
            this.書き込み項か=false;
            var Expression1=base.Traverse(Expression0);
            this.書き込み項か=書き込み項か;
            return Expression1;
        }
    }
    private readonly 変換_先行評価式 _変換_先行評価式;
    private readonly Generic.List<(Generic.IEnumerable<ParameterExpression>Parameters,Generic.List<Generic.IEnumerable<ParameterExpression>>ListVariables)> List束縛Parameter情報 = new();
    private Generic.ICollection<ParameterExpression> ループ跨ぎParameters;
    //private readonly Dictionary<Expression,ParameterExpression> Dictionary_Expression_ループラムダ跨ぎParameter;
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    /// <param name="作業配列"></param>
    /// <param name="ExpressionEqualityComparer"></param>
    /// <param name="ループ跨ぎParameters"></param>
    public 変換_跨ぎParameterの先行評価(作業配列 作業配列,ExpressionEqualityComparer ExpressionEqualityComparer,Generic.ICollection<ParameterExpression> ループ跨ぎParameters) : base(作業配列){
        this._取得_先行評価式=new(this.List束縛Parameter情報,ループ跨ぎParameters);
        this._変換_先行評価式=new(作業配列,ExpressionEqualityComparer);
        this.ループ跨ぎParameters=ループ跨ぎParameters;
        //this.Dictionary_Expression_ループラムダ跨ぎParameter=new(ExpressionEqualityComparer);
    }

    private int 番号;
    public bool IsInline{
        get=>this._取得_先行評価式.IsInline;
        set{
            this._取得_先行評価式.IsInline=value;
            this._変換_先行評価式.IsInline=value;
        }
    }
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public Expression 実行(Expression Lambda0){
        this.番号=0;
        this.List束縛Parameter情報.Clear();
        //this.Dictionary_Expression_ループラムダ跨ぎParameter.Clear();
        var Lambda1=(LambdaExpression)this.Traverse(Lambda0);
        var Block1_Variables=this.Dictionaryラムダ跨ぎParameter.Keys.Concat(this.ループ跨ぎParameters);
        var Lambda1_Body=Lambda1.Body;
        if(Block1_Variables.Any())Lambda1_Body=Expression.Block(Block1_Variables,this.作業配列.Expressions設定(Lambda1_Body));
        return Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda1.Name,Lambda1.TailCall,Lambda1.Parameters);
    }
    internal Generic.Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter{
        get=>this._取得_先行評価式.Dictionaryラムダ跨ぎParameter;
        set=>this._取得_先行評価式.Dictionaryラムダ跨ぎParameter=value;
    }
    protected override Expression Block(BlockExpression Block0) {
        var Block0_Variables = Block0.Variables;
        var List束縛Parameter情報 = this.List束縛Parameter情報;
        var ListVariables = List束縛Parameter情報[^1].ListVariables;
        ListVariables.Add(Block0_Variables);
        var LinkedList = new Generic.LinkedList<Expression>(Block0.Expressions);
        this.外だし(LinkedList);
        Debug.Assert(LinkedList.Last!=null,"LinkedList.Last != null");
        Debug.Assert(!(Block0_Variables.Count==0&&LinkedList.Count==1&&Block0.Type==LinkedList.Last.Value.Type),"この式は最適化されて存在しないはず。");
        return Expression.Block(Block0.Type,Block0_Variables,LinkedList);
    }
    protected override Expression Lambda(LambdaExpression Lambda0) {
        var 旧ループ跨ぎParameters=this.ループ跨ぎParameters;
        var 新ループ跨ぎParameters=this.ループ跨ぎParameters=new Generic.List<ParameterExpression>();
        var List束縛Parameter情報 = this.List束縛Parameter情報;
        var List束縛Parameter情報_Count = List束縛Parameter情報.Count;
        List束縛Parameter情報.Add((Lambda0.Parameters,new()));
        var LinkedList = new Generic.LinkedList<Expression>();
        LinkedList.AddFirst(Lambda0.Body);
        this.外だし(LinkedList);
        List束縛Parameter情報.RemoveAt(List束縛Parameter情報_Count);
        this.ループ跨ぎParameters=旧ループ跨ぎParameters;
        return Expression.Lambda(
            Lambda0.Type,LinkedList.Count==1&&新ループ跨ぎParameters.Count==0 ? LinkedList.First!.Value : Expression.Block(新ループ跨ぎParameters,LinkedList),
            Lambda0.Name,Lambda0.TailCall,Lambda0.Parameters
        );
    }
    protected override Expression Call(MethodCallExpression MethodCall0) {
        if(!ループ展開可能メソッドか(MethodCall0))
            return base.Call(MethodCall0);
        var MethodCall0_Arguments = MethodCall0.Arguments;
        if(Reflection.ExtensionSet.Inline1==GetGenericMethodDefinition(MethodCall0.Method)){
            var MethodCall0_Arguments_0=MethodCall0_Arguments[0];
            Expression MethodCall1_Arguments_0;
            if(MethodCall0_Arguments_0 is LambdaExpression Lambda0)
                MethodCall1_Arguments_0=this.Lambda(Lambda0);
            else
                MethodCall1_Arguments_0=MethodCall0_Arguments_0;
            return Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0);
        }else if(Reflection.ExtensionSet.Inline2==GetGenericMethodDefinition(MethodCall0.Method)) {
            var MethodCall1_Arguments_0 =this.Traverse(MethodCall0_Arguments[0]);
            var MethodCall0_Arguments_1 = MethodCall0_Arguments[1];
            Expression MethodCall1_Arguments_1;
            if(MethodCall0_Arguments_1 is LambdaExpression Lambda0)
                MethodCall1_Arguments_1=this.Lambda(Lambda0);
            else
                MethodCall1_Arguments_1=MethodCall0_Arguments_1;
            return Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
        }
        var MethodCall0_Arguments_Count = MethodCall0_Arguments.Count;
        var MethodCall1_Arguments = new Expression[MethodCall0_Arguments_Count];
        MethodCall1_Arguments[0]=this.Traverse(MethodCall0_Arguments[0]);
        for(var a = 1;a<MethodCall0_Arguments_Count;a++) {
            var MethodCall0_Argument = MethodCall0_Arguments[a];
            if(MethodCall0_Argument is LambdaExpression Lambda0)
                MethodCall1_Arguments[a]=this.Lambda(Lambda0);
            else{
                MethodCall1_Arguments[a]=this.Traverse(MethodCall0_Argument);
            }
        }
        return Expression.Call(MethodCall0.Method,MethodCall1_Arguments);
    }
    private void 外だし(Generic.LinkedList<Expression> LinkedList) {
        var LinkedListNode = LinkedList.First;
        var 取得_先行評価式 = this._取得_先行評価式;
        var 変換_先行評価式 = this._変換_先行評価式;
        var 回数 = 0;
        do {
            Debug.Assert(LinkedListNode!=null);
            var LinkedListNode_Value = LinkedListNode.Value;
            if(LinkedListNode_Value.NodeType==ExpressionType.Assign) {
                LinkedListNode_Value=((BinaryExpression)LinkedListNode_Value).Right;
            }
            var (分離Expressionの場所, 旧)=取得_先行評価式.実行(LinkedListNode_Value);
            if(旧 is null) {
                LinkedListNode=LinkedListNode.Next;
            } else {
                Debug.Assert(旧.Type!=typeof(void));
                ParameterExpression 新;
                if(分離Expressionの場所==場所.ラムダ跨ぎ){
                    新=Expression.Parameter(旧.Type,$"ラムダ{(旧 is ParameterExpression Parameter?Parameter.Name:string.Empty)}.{this.番号++}");
                    this.Dictionaryラムダ跨ぎParameter.Add(新,default!);
                } else{
                    新=Expression.Parameter(旧.Type,$"ループ{(旧 is ParameterExpression Parameter?Parameter.Name:string.Empty)}.{this.番号++}");
                    this.ループ跨ぎParameters.Add(新);
                }
                var LinkedListNode0 = LinkedListNode;
                var 読み込みがあるか = false;
                var 書き込みがあるか = false;
                do {
                    var (LinkedListNode0_Value, 読み込みがあるか0, 書き込みがあるか0)=
                        変換_先行評価式.実行(LinkedListNode0.Value,旧,新,分離Expressionの場所);
                    読み込みがあるか|=読み込みがあるか0;
                    書き込みがあるか|=書き込みがあるか0;
                    LinkedListNode0.Value=LinkedListNode0_Value;
                    LinkedListNode0=LinkedListNode0.Next;
                } while(LinkedListNode0 is not null);
                //LinkedListNode=LinkedList.AddBefore(LinkedListNode,Expression.Assign(新,旧));
                if(読み込みがあるか)LinkedListNode=LinkedList.AddBefore(LinkedListNode,Expression.Assign(新,旧));
                if(書き込みがあるか)LinkedList.AddLast(Expression.Assign(旧,新));
            }
            回数++;
        } while(LinkedListNode is not null);
        for(var Node = LinkedList.First;Node is not null;Node=Node.Next) Node.Value=this.Traverse(Node.Value);
    }

}
