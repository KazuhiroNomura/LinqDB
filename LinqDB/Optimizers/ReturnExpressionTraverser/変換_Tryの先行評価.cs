/*
 * a.Union(a)→a
 * a.Except(a)→Empty
 */
using System.Diagnostics;
using System.Linq.Expressions;
using LinqDB.Enumerables;
using LinqDB.Optimizers.VoidExpressionTraverser;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
/// <summary>
/// 型つきTryは形無しでtry,catch内部で変数に代入してそのご変数で評価する必要がある
/// try{x}catch{y}→try{t=x}catch{t=y},t
/// </summary>
internal sealed class 変換_Tryの先行評価:ReturnExpressionTraverser{
    private sealed class 取得_IsTry:VoidExpressionTraverser_Quoteを処理しない{
        private bool IsTry;
        public bool 実行(Expression e){
            this.IsTry=false;
            this.Traverse(e);
            return this.IsTry;
        }
        protected override void Traverse(Expression Expression){
            if(this.IsTry) return;
            base.Traverse(Expression);
        }
        protected override void Try(TryExpression Try){
            if(Try.Type==typeof(void))
                base.Try(Try);
            else
                this.IsTry=true;
        }
    }
    private readonly 取得_IsTry _取得_IsTry=new();
    private sealed class 変換Try:ReturnExpressionTraverser{
        private sealed class 変換Assign:ReturnExpressionTraverser{
            public 変換Assign(作業配列 作業配列):base(作業配列){}
            private ParameterExpression 新Parameter=default!;
            public Expression 実行(Expression Expression0,ParameterExpression 新Parameter){
                this.新Parameter=新Parameter;
                return this.Traverse(Expression0);
            }
            protected override Expression Traverse(Expression Expression0){
                if(Expression0.Type==typeof(void)) return Expression0;
                switch(Expression0.NodeType){
                    case ExpressionType.Try:{
                        var Try0=(TryExpression)Expression0;
                        Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
                        var Try0_Handlers=Try0.Handlers;
                        var Try0_Handlers_Count=Try0_Handlers.Count;
                        var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
                        var 新Parameter=this.新Parameter;
                        var Try1_Body=Expression.Assign(新Parameter,Try0.Body);
                        for(var a=0;a<Try0_Handlers_Count;a++){
                            var Try0_Handler=Try0_Handlers[a];
                            Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
                            var Try0_Handler_Variable=Try0_Handler.Variable;
                            var Try1_Handler_Body=Expression.Assign(新Parameter,Try0_Handler.Body);
                            var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
                            if(Try0_Handler_Variable is not null)
                                Try1_Handlers[a]=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
                            else
                                Try1_Handlers[a]=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
                        }
                        if(Try0.Fault is not null){
                            Debug.Assert(Try0.Finally is null);
                            var Try1_Fault=this.Traverse(Try0.Fault);
                            return Expression.TryFault(Try1_Body,Try1_Fault);
                        } else
                            return Expression.TryCatchFinally(Try1_Body,Try0.Finally,Try1_Handlers);
                    }
                    case ExpressionType.Block:{
                        var Block0=(BlockExpression)Expression0;
                        var Block0_Expressions = Block0.Expressions;
                        var Block0_Expressions_Count = Block0_Expressions.Count;
                        var Block1_Expressions = new Expression[Block0_Expressions_Count];
                        var Block0_Expressions_Count_1=Block0_Expressions_Count-1;
                        for(var a = 0;a<Block0_Expressions_Count_1;a++)
                            Block1_Expressions[a]=Block0_Expressions[a];
                        var Block0_Expressions_N=Block0_Expressions[Block0_Expressions_Count_1];
                        if(Block0_Expressions_N.NodeType==ExpressionType.Try)
                            Block1_Expressions[Block0_Expressions_Count_1]=this.Try((TryExpression)Block0_Expressions_N);
                        else
                            Block1_Expressions[Block0_Expressions_Count_1]=Expression.Assign(this.新Parameter,Block0_Expressions_N);
                        return Expression.Block(Block0.Type,Block0.Variables,Block1_Expressions);
                    }
                    default:{
                        return Expression.Assign(this.新Parameter,Expression0);
                    }
                }
            }
        }
        private readonly 変換Assign _変換Assign;
        private readonly List<ParameterExpression> ListParameter;
        private readonly List<Expression> ListAssign;
        private readonly 取得_IsTry _取得_IsTry=new();
        public 変換Try(作業配列 作業配列,List<ParameterExpression> ListParameter,List<Expression> ListAssign):base(作業配列){
            this.ListParameter=ListParameter;
            this.ListAssign=ListAssign;
            this._変換Assign=new(作業配列);
        }
        private int ListIndex;
        public Expression 実行(Expression e){
            this.ListIndex=0;
            return this.Traverse(e);
        }
        protected override Expression Traverse(Expression Expression0){
            if(Expression0.Type==typeof(void)) return Expression0;
            switch(Expression0.NodeType){
                case ExpressionType.Try:{
                    var Try0=(TryExpression)Expression0;
                    Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
                    var Try0_Handlers=Try0.Handlers;
                    var Try0_Handlers_Count=Try0_Handlers.Count;
                    var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
                    var 新Parameter=Expression.Parameter(Try0.Type,$"Try{this.ListIndex++}");
                    this.ListParameter.Add(新Parameter);
                    var ListAssign=this.ListAssign;
                    var Try1_Body= this._変換Assign.実行(Try0.Body,新Parameter);
                    Try1_Body=Expression.Block(typeof(void),Try1_Body);
                    for(var a=0;a<Try0_Handlers_Count;a++){
                        var Try0_Handler=Try0_Handlers[a];
                        Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
                        var Try0_Handler_Variable=Try0_Handler.Variable;
                        var Try1_Handler_Body = this._変換Assign.実行(Try0_Handler.Body,新Parameter);
                        Try1_Handler_Body=Expression.Block(typeof(void),Try1_Handler_Body);
                        if(Try0_Handler_Variable is not null)
                            Try1_Handlers[a]=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try0_Handler.Filter);
                        else
                            Try1_Handlers[a]=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try0_Handler.Filter);
                    }
                    TryExpression Try1;
                    if(Try0.Fault is not null){
                        Debug.Assert(Try0.Finally is null);
                        var Try1_Fault=this.Traverse(Try0.Fault);
                        Try1=Expression.TryFault(Try1_Body,Try1_Fault);
                    } else
                        Try1=Expression.TryCatchFinally(Try1_Body,Try0.Finally,Try1_Handlers);
                    ListAssign.Add(Try1);
                    return 新Parameter;
                }
                case ExpressionType.Block:{
                    var Block0=(BlockExpression)Expression0;
                    var Block0_Expressions=Block0.Expressions;
                    var Block0_Expressions_Count=Block0_Expressions.Count;
                    var Block1_Expressions=new Expression[Block0_Expressions_Count];
                    for(var a=0;a<Block0_Expressions_Count;a++){
                        Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
                    }
                    return Expression.Block(Block0.Type,Block0.Variables,Block1_Expressions);
                }
                default:{
                    if(this._取得_IsTry.実行(Expression0)) return base.Traverse(Expression0);
                    var 新Parameter=Expression.Parameter(Expression0.Type,$"Try{this.ListIndex++}");
                    this.ListParameter.Add(新Parameter);
                    var Assign=Expression.Block(
                        typeof(void),
                        Expression.Assign(
                            新Parameter,
                            Expression0
                        )
                    );
                    this.ListAssign.Add(Assign);
                    return 新Parameter;
                }
            }
        }
    }
    private readonly 変換Try _変換Try;
    private readonly List<Expression> ListAssign;
    private readonly List<ParameterExpression> ListParameter;
    public 変換_Tryの先行評価(作業配列 作業配列):base(作業配列){
        var ListAssign=this.ListAssign=new();
        var ListParameter=this.ListParameter=new();
        this._変換Try=new(作業配列,ListParameter,ListAssign);

    }
    public Expression 実行(Expression Expression0){
        var Expression1=this.Traverse(Expression0);
        return Expression1;
    }
    protected override Expression Lambda(LambdaExpression Lambda0) {
        var Lambda0_Body = Lambda0.Body;
        var Lambda1_Body = this.Tryを先行評価(Lambda0_Body);
        if(Lambda0_Body==Lambda1_Body) return Lambda0;
        return Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0.Parameters);
    }
    protected override Expression Block(BlockExpression Block0) {
        var Block0_Expressions = Block0.Expressions;
        var Block0_Expressions_Count = Block0_Expressions.Count;
        var Block1_Expressions = new Expression[Block0_Expressions_Count];
        for(var a=0;a<Block0_Expressions_Count;a++){
            var Expression0=Block0_Expressions[a];
            var Expression1=this.Tryを先行評価(Expression0);
            Block1_Expressions[a]=Expression1;
        }
        return Expression.Block(Block0.Type,Block0.Variables,Block1_Expressions);
    }
        protected override Expression Try(TryExpression Try0){
            if(Try0.Type==typeof(void)) return base.Try(Try0);
            Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
            var Try0_Handlers = Try0.Handlers;
            var Try0_Handlers_Count = Try0_Handlers.Count;
            var Try1_Handlers = new CatchBlock[Try0_Handlers_Count];
            var Try1_Body = this.Tryを先行評価(Try0.Body);
            for(var a = 0;a<Try0_Handlers_Count;a++) {
                var Try0_Handler = Try0_Handlers[a];
                Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
                var Try0_Handler_Variable = Try0_Handler.Variable;
                var Try1_Handler_Body=this.Tryを先行評価(Try0_Handler.Body);
                var Try1_Handler_Filter = this.TraverseNullable(Try0_Handler.Filter);
                if(Try0_Handler_Variable is not null)
                    Try1_Handlers[a]=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
                else
                    Try1_Handlers[a]=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
            }
            if(Try0.Finally is not null)
                return Expression.TryCatchFinally(
                    Try1_Body,
                    this.Traverse(Try0.Finally),
                    Try1_Handlers
                );
            Debug.Assert(Try0.Finally is null);
            if(Try0.Fault is not null) {
                Debug.Assert(Try0.Finally is null);
                return Expression.TryFault(Try1_Body,this.Traverse(Try0.Fault));
            }
            return Expression.TryCatchFinally(Try1_Body,this.TraverseNullable(Try0.Finally),Try1_Handlers);
        }
    private Expression Tryを先行評価(Expression Expression0){
        //引数のtrycatchを引数を置く場所から前に移動して変数で置換したい。
        if(Expression0.NodeType==ExpressionType.Block) return this.Block((BlockExpression)Expression0);
        var Expression1=this.Traverse(Expression0);
        if(!this._取得_IsTry.実行(Expression0))return Expression1;
        var ListParameter=this.ListParameter;
        var ListAssign=this.ListAssign;
        ListAssign.Clear();
        ListParameter.Clear();
        var Expression2=this._変換Try.実行(Expression1);
        var Expressions=new List<Expression>();
        while(true){
            if(Expressions.Count>100){

            }
            ListAssign.Reverse();
            var start=Expressions.Count;
            Expressions.AddRange(ListAssign);
            var end=Expressions.Count;
            ListAssign.Clear();
            for(var a=start;a<end;a++){
                this._変換Try.実行(Expressions[a]);
            }
            if(ListAssign.Count==0) break;
        }
        Expressions.Reverse();
        Expressions.Add(Expression2);
        return Expression.Block(this.ListParameter,Expressions);
    }
}
//20240109 419
