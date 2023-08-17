using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;
namespace LinqDB.Optimizers;
partial class Optimizer {
    /// <summary>
    /// ラムダを跨ぐParameterExpressionを取得
    /// Blockから一旦Parameterを削除し、内部で使われていたら追加する
    /// LambdaExpressionを取得
    /// Quoteを取得
    /// </summary>
    private sealed class 変換_Lambda_Quote_ラムダ跨ぎParameter:ReturnExpressionTraverser {
        private sealed class 判定_内部LambdaにParameterが存在するか:VoidExpressionTraverser_Quoteを処理しない {
            private bool 存在した;
            private ParameterExpression? 探したいParameter;
            private bool ラムダ内部;
            public bool 実行(Expression e,ParameterExpression 探したいParameter) {
                this.存在した=false;
                this.ラムダ内部=false;
                this.探したいParameter=探したいParameter;
                this.Traverse(e);
                return this.存在した;
            }
            protected override void Parameter(ParameterExpression Parameter) {
                if(this.ラムダ内部&&this.探したいParameter==Parameter)
                    this.存在した=true;
            }
            protected override void Lambda(LambdaExpression Lambda) {
                var ラムダ内部=this.ラムダ内部;
                this.ラムダ内部=true;
                this.Traverse(Lambda.Body);
                this.ラムダ内部=ラムダ内部;
            }
        }
        private readonly 判定_内部LambdaにParameterが存在するか _判定_内部LambdaにParameterが存在するか=new();
        private readonly IEnumerable<ParameterExpression> ループ跨ぎParameters;
        private readonly List<ParameterExpression> Block_Variables=new();
        public 変換_Lambda_Quote_ラムダ跨ぎParameter(作業配列 作業配列,IEnumerable<ParameterExpression> ループ跨ぎParameters) : base(作業配列) {
            this.ループ跨ぎParameters=ループ跨ぎParameters;
        }
        internal Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic=default!;
        internal Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=default!;
        internal Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)>DictionaryLambda=default!;
        public Expression 実行(Expression Lambda0) {
            this.DictionaryDynamic.Clear();
            var Block_Variables=this.Block_Variables;
            Block_Variables.Clear();
            var Lambda=(LambdaExpression)Lambda0;
            var Lambda1_Body=this.Traverse(Lambda.Body);
            //var Lambda1 = (LambdaExpression)base.Lambda(Lambda0);
            var Block1=Expression.Block(Block_Variables,this._作業配列.Expressions設定(Lambda1_Body));
            var Lambda1=Expression.Lambda(Lambda.Type,Block1,Lambda.Name,Lambda.TailCall,Lambda.Parameters);
            if(!this.DictionaryLambda.ContainsKey(Lambda1)) this.DictionaryLambda.Add(Lambda1,default!);
            return Lambda1;
            //var Lambda1=(LambdaExpression)this.Lambda((LambdaExpression)Lambda0);
            //var Block1=Expression.Block(Block_Variables,this._作業配列.Expressions設定(Lambda1.Body));
            //return Expression.Lambda(Lambda1.Type,Block1,Lambda1.Name,Lambda1.TailCall,Lambda1.Parameters);
        }
        //private static CallSite<T> CallSite_Unary<T>(ExpressionType NodeType) where T:class=>CallSite<T>.Create(RuntimeBinder.Binder.UnaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray1));
        //private static CallSite<Func<CallSite,object,object>> CallSite_Unary(ExpressionType NodeType)=> CallSite_Unary<Func<CallSite,object,object>>(NodeType);
        protected override Expression Dynamic(DynamicExpression Dynamic0){
            var Dynamic1=(DynamicExpression)base.Dynamic(Dynamic0);
            if(this.DictionaryDynamic.ContainsKey(Dynamic1)) return Dynamic1;
            //var CallSite0=Dynamic1.Binder switch{
            //    UnaryOperationBinder UnaryOperationBinder=>UnaryOperationBinder.Operation switch{
            //        ExpressionType.Decrement         =>DynamicReflection.CallSites.Decrement         ,
            //        ExpressionType.Increment         =>DynamicReflection.CallSites.Increment         ,
            //        ExpressionType.Negate            =>DynamicReflection.CallSites.Negate            ,
            //        ExpressionType.Not               =>DynamicReflection.CallSites.Not               ,
            //        ExpressionType.OnesComplement    =>DynamicReflection.CallSites.OnesComplement    ,
            //        ExpressionType.UnaryPlus         =>DynamicReflection.CallSites.UnaryPlus         ,
            //        ExpressionType.IsFalse           =>DynamicReflection.CallSites.IsFalse           ,
            //        ExpressionType.IsTrue            =>DynamicReflection.CallSites.IsTrue            ,
            //        _=>throw new NotSupportedException(Dynamic1.Binder.ToString())
            //    },
            //    BinaryOperationBinder BinaryOperationBinder=>BinaryOperationBinder.Operation switch{
            //        ExpressionType.Add               =>DynamicReflection.CallSites.Add               ,
            //        ExpressionType.AddAssign         =>DynamicReflection.CallSites.AddAssign         ,
            //        ExpressionType.And               =>DynamicReflection.CallSites.And               ,
            //        ExpressionType.AndAssign         =>DynamicReflection.CallSites.AndAssign         ,
            //        ExpressionType.Divide            =>DynamicReflection.CallSites.Divide            ,
            //        ExpressionType.DivideAssign      =>DynamicReflection.CallSites.DivideAssign      ,
            //        ExpressionType.Equal             =>DynamicReflection.CallSites.Equal             ,
            //        ExpressionType.ExclusiveOr       =>DynamicReflection.CallSites.ExclusiveOr       ,
            //        ExpressionType.ExclusiveOrAssign =>DynamicReflection.CallSites.ExclusiveOrAssign ,
            //        ExpressionType.GreaterThan       =>DynamicReflection.CallSites.GreaterThan       ,
            //        ExpressionType.GreaterThanOrEqual=>DynamicReflection.CallSites.GreaterThanOrEqual,
            //        ExpressionType.LeftShift         =>DynamicReflection.CallSites.LessThanOrEqual   ,
            //        ExpressionType.LessThanOrEqual   =>DynamicReflection.CallSites.LessThanOrEqual   ,
            //        ExpressionType.Modulo            =>DynamicReflection.CallSites.Modulo            ,
            //        ExpressionType.ModuloAssign      =>DynamicReflection.CallSites.ModuloAssign      ,
            //        ExpressionType.Multiply          =>DynamicReflection.CallSites.Multiply          ,
            //        ExpressionType.MultiplyAssign    =>DynamicReflection.CallSites.MultiplyAssign    ,
            //        ExpressionType.NotEqual          =>DynamicReflection.CallSites.NotEqual          ,
            //        ExpressionType.Or                =>DynamicReflection.CallSites.Or                ,
            //        ExpressionType.OrAssign          =>DynamicReflection.CallSites.OrAssign          ,
            //        ExpressionType.RightShift        =>DynamicReflection.CallSites.RightShift        ,
            //        ExpressionType.RightShiftAssign  =>DynamicReflection.CallSites.RightShiftAssign  ,
            //        ExpressionType.Subtract          =>DynamicReflection.CallSites.Subtract          ,
            //        ExpressionType.SubtractAssign    =>DynamicReflection.CallSites.SubtractAssign    ,
            //        _=>throw new NotSupportedException(Dynamic1.Binder.ToString())
            //    },
            //    //x.Member
            //    GetMemberBinder GetMemberBinder=>CallSite.Create(
            //        Dynamic1.DelegateType,
            //        RuntimeBinder.Binder.GetMember(
            //            RuntimeBinder.CSharpBinderFlags.None,
            //            GetMemberBinder.Name,
            //            typeof(Optimizer),
            //            DynamicReflection.CSharpArgumentInfoArray1
            //        )
            //    ),
            //    //x.Member=y
            //    SetMemberBinder SetMemberBinder=>CallSite.Create(
            //        Dynamic1.DelegateType,
            //        RuntimeBinder.Binder.SetMember(
            //            RuntimeBinder.CSharpBinderFlags.None,
            //            SetMemberBinder.Name,
            //            typeof(Optimizer),
            //            DynamicReflection.CSharpArgumentInfoArray2
            //        )
            //    ),
            //    //x[y]
            //    GetIndexBinder GetIndexBinder=>CallSite.Create(
            //        Dynamic1.DelegateType,
            //        RuntimeBinder.Binder.GetIndex(
            //            RuntimeBinder.CSharpBinderFlags.None,
            //            typeof(Optimizer),
            //            DynamicReflection.CSharpArgumentInfoArray2
            //        )
            //    ),
            //    //x[y]=z
            //    SetIndexBinder SetIndexBinder=>CallSite.Create(
            //        Dynamic1.DelegateType,
            //        RuntimeBinder.Binder.SetIndex(
            //            RuntimeBinder.CSharpBinderFlags.None,
            //            typeof(Optimizer),
            //            DynamicReflection.CSharpArgumentInfoArray3
            //        )
            //    ),
            //    //(int)x
            //    ConvertBinder ConvertBinder=>CallSite.Create(
            //        Dynamic1.DelegateType,
            //        RuntimeBinder.Binder.Convert(
            //            RuntimeBinder.CSharpBinderFlags.None,
            //            ConvertBinder.Type,
            //            typeof(Optimizer)
            //        )
            //    ),
            //    //x(y,z)
            //    InvokeBinder InvokeBinder=>CallSite.Create(
            //        Dynamic1.DelegateType,
            //        RuntimeBinder.Binder.Invoke(
            //            RuntimeBinder.CSharpBinderFlags.None,
            //            typeof(Optimizer),
            //            DynamicReflection.CSharpArgumentInfoArray(Dynamic1.Arguments.Count)
            //        )
            //    ),
            //    //x.Method(y,z)
            //    InvokeMemberBinder InvokeMemberBinder=>CallSite.Create(
            //        Dynamic1.DelegateType,
            //        RuntimeBinder.Binder.InvokeMember(
            //            RuntimeBinder.CSharpBinderFlags.None,
            //            InvokeMemberBinder.Name,
            //            Dynamic1.Arguments.Select(p=>p.Type),
            //            typeof(Optimizer),
            //            DynamicReflection.CSharpArgumentInfoArray(Dynamic1.Arguments.Count)
            //        )
            //    ),
            //    //CreateInstanceBinder SetIndexBinder=>CallSite.Create(
            //    //    Dynamic.DelegateType,
            //    //    RuntimeBinder.Binder.SetIndex(
            //    //        RuntimeBinder.CSharpBinderFlags.None,
            //    //        typeof(Optimizer),
            //    //        DynamicReflection.CSharpArgumentInfoArray3
            //    //    )
            //    //),
            //    _=>throw new NotSupportedException(Dynamic1.Binder.ToString())
            //};
            //DictionaryDynamic[Dynamic1]=(
            //    default!,
            //    ValueTuple_Item(ref DispType0,ref Disp0,ref Item番号,ref TupleExpression,CallSite0)
            //);
            this.DictionaryDynamic.Add(Dynamic1,default!);
            return Dynamic1;
        }
        protected override Expression Call(MethodCallExpression MethodCall0) {
            if(Reflection.Object.GetType_==MethodCall0.Method&&MethodCall0.Object!.Type.IsValueType)
                return Expression.Call(
                    Expression.Convert(this.Traverse(MethodCall0.Object),typeof(object)),
                    MethodCall0.Method
                );
            return base.Call(MethodCall0);
        }

        protected override Expression Lambda(LambdaExpression Lambda0) {
            var Lambda1 = (LambdaExpression)base.Lambda(Lambda0);
            if(!this.DictionaryLambda.ContainsKey(Lambda1)) this.DictionaryLambda.Add(Lambda1,default!);
            return Lambda1;
        }
        protected override Expression Assign(BinaryExpression Binary0) {
            //if(Binary0.Left is ParameterExpression Parameter&& Parameter.Name != null && this.ループ跨ぎParameters.Contains(Parameter) && !this.Block_Variables!.Contains(Parameter))
            if(Binary0.Left is ParameterExpression { Name: { } } Parameter&&this.ループ跨ぎParameters.Contains(Parameter)&&!this.Block_Variables!.Contains(Parameter))
                this.Block_Variables!.Add(Parameter);
            return base.Assign(Binary0);
        }
        protected override Expression Block(BlockExpression Block0) {
            var 判定_内部LambdaにParameterが存在するか=this._判定_内部LambdaにParameterが存在するか;
            //var Block0_Variables=Block0.Variables;
            //var Block1_Variables=Block0_Variables.ToList();
            //Blockのローカル変数はラムダを跨いでるのは削除する。
            var Block1_Variables=Block0.Variables.ToList();
            var Dictionaryラムダ跨ぎParameter=this.Dictionaryラムダ跨ぎParameter;
            foreach(var Variable in Block0.Variables)
                if(判定_内部LambdaにParameterが存在するか.実行(Block0,Variable)){
                    Block1_Variables.Remove(Variable);
                    Dictionaryラムダ跨ぎParameter.Add(Variable,default!);
                }
            var Block0_Expressions=Block0.Expressions;
            var Block0_Expressions_Count=Block0_Expressions.Count;
            var Block1_Expressions=new Expression[Block0_Expressions_Count];
            for(var a = 0;a<Block0_Expressions_Count;a++)
                Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
            return Expression.Block(Block1_Variables,Block1_Expressions);
        }
    }
    //private sealed class 変換_Lambda_Quote_ラムダ跨ぎParameter:ReturnExpressionTraverser {
    //    private readonly IEnumerable<ParameterExpression> ループ跨ぎParameters;
    //    //private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    //    public 変換_Lambda_Quote_ラムダ跨ぎParameter(作業配列 作業配列,IEnumerable<ParameterExpression> ループ跨ぎParameters) : base(作業配列) {
    //        //this.DictionaryLambda_FieldBuilder=DictionaryLambda_FieldBuilder;
    //        //this.Dictionary_Quote_FieldBuilder=Dictionary_Quote_FieldBuilder;
    //        //this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    //        this.ループ跨ぎParameters=ループ跨ぎParameters;
    //    }
    //    internal Dictionary<LambdaExpression,(FieldInfo Disp_LambdaField, MethodBuilder Impl_MethodBuilder)> DictionaryLambda_FieldBuilder = default!;
    //    //internal Dictionary<UnaryExpression,FieldInfo> Dictionary_Quote_FieldBuilder=default!;
    //    //private IEnumerable<ParameterExpression> _ラムダ跨ぎParameters = default!;
    //    //internal IEnumerable<ParameterExpression> ラムダ跨ぎParameters {
    //    //    set => this._ラムダ跨ぎParameters=value;
    //    //}
    //    public Expression 実行(Expression Lambda) {
    //        //this.DictionaryLambda_FieldBuilder.Clear();
    //        //this.Dictionary_Quote_FieldBuilder.Clear();
    //        return this.Lambda((LambdaExpression)Lambda);
    //    }
    //    protected override Expression Lambda(LambdaExpression Lambda0) {
    //        var Lambda1 = base.Lambda(Lambda0);
    //        if(!this.DictionaryLambda_FieldBuilder.ContainsKey(Lambda0)) this.DictionaryLambda_FieldBuilder.Add(Lambda0,default!);
    //        return Lambda1;
    //    }
    //    protected override Expression Assign(BinaryExpression Binary0) {
    //        //if(Binary0.Left is ParameterExpression Parameter&& Parameter.Name != null && this.ループ跨ぎParameters.Contains(Parameter) && !this.Block_Variables!.Contains(Parameter))
    //        if(Binary0.Left is ParameterExpression { Name: { } } Parameter&&this.ループ跨ぎParameters.Contains(Parameter)&&!this.Block_Variables!.Contains(Parameter))
    //            this.Block_Variables!.Add(Parameter);
    //        return base.Assign(Binary0);
    //    }
    //    protected override Expression Quote(UnaryExpression Unary0) {
    //        //if(!this.Dictionary_Quote_FieldBuilder.ContainsKey(Unary0))this.Dictionary_Quote_FieldBuilder.Add(Unary0,default!);
    //        return Unary0;
    //    }
    //    private List<ParameterExpression>? Block_Variables;
    //    protected override Expression Block(BlockExpression Block0) {
    //        var Block1_Expressions = this.TraverseExpressionsNullable(Block0.Expressions);
    //        if(Block1_Expressions is null) return Block0;
    //        var Block0_Variables = this.Block_Variables;
    //        var Block1_Variables = this.Block_Variables=new List<ParameterExpression>(Block0.Variables);
    //        this.Block_Variables=Block0_Variables;
    //        if(Block1_Variables.Count==0&&Block1_Expressions.Count==1) return Block1_Expressions[0];
    //        return Expression.Block(Block0.Type,Block1_Variables,Block1_Expressions);
    //    }
    //}
    //private sealed class 変換_Lambda_Quote_ラムダ跨ぎParameter:VoidExpressionTraverser{
    //    private readonly IEnumerable<ParameterExpression> ループ跨ぎParameters;
    //        //private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    //    public 変換_Lambda_Quote_ラムダ跨ぎParameter(作業配列 作業配列,IEnumerable<ParameterExpression> ループ跨ぎParameters){
    //        //this.DictionaryLambda_FieldBuilder=DictionaryLambda_FieldBuilder;
    //        //this.Dictionary_Quote_FieldBuilder=Dictionary_Quote_FieldBuilder;
    //        //this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    //        this.ループ跨ぎParameters=ループ跨ぎParameters;
    //    }
    //    internal Dictionary<LambdaExpression,(FieldInfo DispLambdaField,MethodBuilder Impl_MethodBuilder)> DictionaryLambda_FieldBuilder=default!;
    //    //internal Dictionary<UnaryExpression,FieldInfo> Dictionary_Quote_FieldBuilder=default!;
    //    private IEnumerable<ParameterExpression> _ラムダ跨ぎParameters=default!;
    //    internal IEnumerable<ParameterExpression> ラムダ跨ぎParameters{
    //        set=>this._ラムダ跨ぎParameters=value;
    //    }
    //    public void 実行(Expression Lambda) {
    //        this.Lambda((LambdaExpression)Lambda);
    //    }
    //    protected override void Lambda(LambdaExpression Lambda0) {
    //        base.Lambda(Lambda0);
    //        if(!this.DictionaryLambda_FieldBuilder.ContainsKey(Lambda0))this.DictionaryLambda_FieldBuilder.Add(Lambda0,default!);
    //    }
    //    protected override void Assign(BinaryExpression Binary0) {
    //        if(Binary0.Left is ParameterExpression Parameter &&
    //            this.ループ跨ぎParameters.Contains(Parameter)&&
    //            !this.Block_Variables!.Contains(Parameter))
    //            this.Block_Variables!.Add(Parameter);
    //        base.Assign(Binary0);
    //    }
    //    protected override void Quote(UnaryExpression Unary0) {
    //    }
    //    private List<ParameterExpression>? Block_Variables;
    //    protected override void Block(BlockExpression Block0) {
    //        var Block1_Expressions = this.TraverseExpressionsNullable(Block0.Expressions);
    //        if(Block1_Expressions is null) return Block0;
    //        var Block0_Variables = this.Block_Variables;
    //        var Block1_Variables=this.Block_Variables=new List<ParameterExpression>(Block0.Variables);
    //        this.Block_Variables=Block0_Variables;
    //        if(Block1_Variables.Count==0&&Block1_Expressions.Count==1)return Block1_Expressions[0];
    //        return Expression.Block(Block0.Type,Block1_Variables,Block1_Expressions);
    //    }
    //}
}
//2022/04/03 84