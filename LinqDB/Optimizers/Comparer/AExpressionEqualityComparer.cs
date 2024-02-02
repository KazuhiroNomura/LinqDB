using LinqDB.Helpers;

using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
//using Microsoft.CSharp.RuntimeBinder;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.CSharp.RuntimeBinder;
// ReSharper disable All
namespace LinqDB.Optimizers.Comparer;
using Generic = System.Collections.Generic;
public abstract class AExpressionEqualityComparer:Generic.IEqualityComparer<Expression>
//,Generic.IEqualityComparer<ParameterExpression>//,Generic.IEqualityComparer<LabelTarget>,Generic.IEqualityComparer<CatchBlock>,Generic.IEqualityComparer<CSharpArgumentInfo>,Generic.IEqualityComparer<SwitchCase>,
//,Generic.IEqualityComparer<MemberBinding>//,Generic.IEqualityComparer<MemberAssignment>,Generic.IEqualityComparer<MemberListBinding>,Generic.IEqualityComparer<MemberMemberBinding>
//,Generic.IEqualityComparer<ElementInit>
//,Generic.IEqualityComparer<SymbolDocumentInfo>
{
    protected static bool @false=>false;
    protected 汎用Comparer ObjectComparer=>new();

    /// <summary>
    /// 式木のハッシュコード。NodeTypeとTypeを使う。
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public virtual int GetHashCode(Expression e)=>e.GetHashCode();
    protected readonly Generic.List<ParameterExpression> x_Parameters=new();
    protected readonly Generic.List<ParameterExpression> y_Parameters=new();
    protected readonly Generic.List<LabelTarget> x_LabelTargets=new();
    protected readonly Generic.List<LabelTarget> y_LabelTargets=new();
    internal virtual void Clear(){
        this.x_Parameters.Clear();
        this.y_Parameters.Clear();
        this.x_LabelTargets.Clear();
        this.y_LabelTargets.Clear();
    }
    protected virtual bool ProtectedAssign(BinaryExpression x,BinaryExpression y){
        if(x==y) return true;
        var x_Left=x.Left;
        var y_Left=y.Left;
        if(x_Left.NodeType!=y_Left.NodeType) return @false;
        if(x_Left.NodeType!=ExpressionType.Parameter) return this.T(x,y);
        if(!this.ProtectedPrivateEquals(x.Right,y.Right)) return @false;
        if(!this.NullableEquals(x.Conversion,y.Conversion)) return @false;
        var x_Parameter=(ParameterExpression)x_Left;
        var y_Parameter=(ParameterExpression)y_Left;
        var x_Index0=this.x_Parameters.IndexOf(x_Parameter);
        var y_Index0=this.y_Parameters.IndexOf(y_Parameter);
        if(x_Index0!=y_Index0) return @false;
        if(x_Index0>=0) return true;
        return this.ProtectedAssign後処理(x_Parameter,y_Parameter);
    }
    /// <summary>
    /// ラムダ跨ぎParameterが出現したときの扱い
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    protected abstract bool ProtectedAssign後処理(ParameterExpression x,ParameterExpression y);
    public bool Equals(ParameterExpression? x,ParameterExpression? y){
        if(x==y) return true;
        var x_Index0=this.x_Parameters.IndexOf(x);
        var y_Index0=this.y_Parameters.IndexOf(y);
        if(x_Index0!=y_Index0) return @false;
        if(x_Index0>=0) return true;
        return this.Equals後処理(x,y);
    }
    /// <summary>
    /// Parameters外のParameterのスコープParameterによる比較するか単にfalseにするか
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    protected abstract bool Equals後処理(ParameterExpression x,ParameterExpression y);
    /// <summary>
    /// 式木同士が一致するか。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(Expression? x,Expression? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        this.Clear();
        return this.NullableEquals(x,y);
    }

    /// <summary>
    /// Cラムダ局所に代入している場合はその左辺で比較したい。デフォルトではそのままAssign式を比較する。「
    /// </summary>
    /// <param name="Expression0"></param>
    /// <returns></returns>
    protected virtual Expression Assignの比較対象(Expression Expression0)=>Expression0;
    private protected bool NullableEquals(Expression? x,Expression? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        //if(x is null) return y is null;
        //if(y is null) return @false;
        return this.ProtectedPrivateEquals(x,y);
    }
    protected private bool ProtectedPrivateEquals(Expression x0,Expression y0){
        var x=this.Assignの比較対象(x0);
        var y=this.Assignの比較対象(y0);
        if(x==y) return true;
        if(x is null^y is null) return @false;
        //if(a1 is null^b1 is null) return @false;
        if(x!.NodeType!=y!.NodeType) return @false;
        if(x.Type!=y.Type) return @false;
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch(x.NodeType){
            case ExpressionType.Add:
            case ExpressionType.AddAssign:
            case ExpressionType.AddAssignChecked:
            case ExpressionType.AddChecked:
            case ExpressionType.And:
            case ExpressionType.AndAssign:
            case ExpressionType.AndAlso:
            case ExpressionType.ArrayIndex:
            case ExpressionType.Coalesce:
            case ExpressionType.Divide:
            case ExpressionType.DivideAssign:
            case ExpressionType.Equal:
            case ExpressionType.ExclusiveOr:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LeftShift:
            case ExpressionType.LeftShiftAssign:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.Modulo:
            case ExpressionType.ModuloAssign:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.MultiplyAssignChecked:
            case ExpressionType.MultiplyChecked:
            case ExpressionType.NotEqual:
            case ExpressionType.Or:
            case ExpressionType.OrAssign:
            case ExpressionType.OrElse:
            case ExpressionType.Power:
            case ExpressionType.PowerAssign:
            case ExpressionType.RightShift:
            case ExpressionType.RightShiftAssign:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractAssign:
            case ExpressionType.SubtractAssignChecked:
            case ExpressionType.SubtractChecked:
                return this.T((BinaryExpression)x,(BinaryExpression)y);
            case ExpressionType.Assign:
                return this.ProtectedAssign((BinaryExpression)x,(BinaryExpression)y);
            case ExpressionType.ArrayLength:
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
            case ExpressionType.Decrement:
            case ExpressionType.Increment:
            case ExpressionType.IsFalse:
            case ExpressionType.IsTrue:
            case ExpressionType.Negate:
            case ExpressionType.NegateChecked:
            case ExpressionType.Not:
            case ExpressionType.OnesComplement:
            case ExpressionType.PostDecrementAssign:
            case ExpressionType.PostIncrementAssign:
            case ExpressionType.PreDecrementAssign:
            case ExpressionType.PreIncrementAssign:
            case ExpressionType.Quote:
            case ExpressionType.Throw:
            case ExpressionType.TypeAs:
            case ExpressionType.UnaryPlus:
            case ExpressionType.Unbox:
                return this.T((UnaryExpression)x,(UnaryExpression)y);
            case ExpressionType.Block:
                return this.T((BlockExpression)x,(BlockExpression)y);
            case ExpressionType.Conditional:
                return this.T((ConditionalExpression)x,(ConditionalExpression)y);
            case ExpressionType.Constant:
                return this.T((ConstantExpression)x,(ConstantExpression)y);
            case ExpressionType.DebugInfo:
                return this.T((DebugInfoExpression)x,(DebugInfoExpression)y);
            case ExpressionType.Default:
                return this.T((DefaultExpression)x,(DefaultExpression)y);
            case ExpressionType.Dynamic:
                return this.T((DynamicExpression)x,(DynamicExpression)y);
            case ExpressionType.Goto:
                return this.T((GotoExpression)x,(GotoExpression)y);
            case ExpressionType.Index:
                return this.T((IndexExpression)x,(IndexExpression)y);
            case ExpressionType.Invoke:
                return this.T((InvocationExpression)x,(InvocationExpression)y);
            case ExpressionType.Label:
                return this.T((LabelExpression)x,(LabelExpression)y);
            case ExpressionType.Lambda:
                return this.T((LambdaExpression)x,(LambdaExpression)y);
            case ExpressionType.ListInit:
                return this.T((ListInitExpression)x,(ListInitExpression)y);
            case ExpressionType.Loop:
                return this.T((LoopExpression)x,(LoopExpression)y);
            case ExpressionType.MemberAccess:
                return this.T((MemberExpression)x,(MemberExpression)y);
            case ExpressionType.MemberInit:
                return this.T((MemberInitExpression)x,(MemberInitExpression)y);
            case ExpressionType.Call:
                return this.T((MethodCallExpression)x,(MethodCallExpression)y);
            case ExpressionType.NewArrayBounds:
            case ExpressionType.NewArrayInit:
                return this.T((NewArrayExpression)x,(NewArrayExpression)y);
            case ExpressionType.New:
                return this.T((NewExpression)x,(NewExpression)y);
            case ExpressionType.Parameter:
                return this.Equals((ParameterExpression)x,(ParameterExpression)y);
            case ExpressionType.RuntimeVariables:
                return this.T((RuntimeVariablesExpression)x,(RuntimeVariablesExpression)y);
            case ExpressionType.Switch:
                return this.T((SwitchExpression)x,(SwitchExpression)y);
            case ExpressionType.Try:
                return this.T((TryExpression)x,(TryExpression)y);
            case ExpressionType.TypeEqual:
            case ExpressionType.TypeIs:
                return this.T((TypeBinaryExpression)x,(TypeBinaryExpression)y);
            default:
                throw new NotSupportedException($"{x.NodeType}はサポートされていない");
        }
    }
    protected bool T(BinaryExpression x,BinaryExpression y){
        if(x==y) return true;
        if(x.Method!=y.Method) return @false;
        if(!this.ProtectedPrivateEquals(x.Left,y.Left))return @false;
        if(!this.ProtectedPrivateEquals(x.Right,y.Right))return @false;
        if(!this.NullableEquals(x.Conversion,y.Conversion))return @false;
        return true;
    }
    protected bool T(BlockExpression x,BlockExpression y){
        if(x==y) return true;
        //if(!this.T(x.Variables,y.Variables)) return @false;
        //return this,x.Expressions,y.Expressions);
        if(x.Type!=y.Type) return @false;
        var x_Variables=x.Variables;
        var y_Variables=y.Variables;
        var x_Variables_Count=x_Variables.Count;
        var y_Variables_Count=y_Variables.Count;
        if(x_Variables_Count!=y_Variables_Count) return @false;
        var x_Parameters=this.x_Parameters;
        var y_Parameters=this.y_Parameters;
        for(var i=0;i<x_Variables_Count;i++){
            var x_Variable=x_Variables[i];
            var y_Variable=y_Variables[i];
            if(x_Variable.Type!=y_Variable.Type) return @false;
        }
        var x_Parameters_Count=x_Parameters.Count;
        Debug.Assert(x_Parameters_Count==y_Parameters.Count);
        x_Parameters.AddRange(x_Variables);
        y_Parameters.AddRange(y_Variables);
        var r=this.SequenceEqual(x.Expressions,y.Expressions);
        x_Parameters.RemoveRange(x_Parameters_Count,x_Variables_Count);
        y_Parameters.RemoveRange(x_Parameters_Count,x_Variables_Count);
        if(!r) return @false;
        return true;
    }
    protected bool T(ConditionalExpression x,ConditionalExpression y){
        if(x==y) return true;
        if(!this.ProtectedPrivateEquals(x.Test,y.Test))return @false;
        if(!this.ProtectedPrivateEquals(x.IfTrue,y.IfTrue))return @false;
        if(!this.ProtectedPrivateEquals(x.IfFalse,y.IfFalse)) return @false;
        return true;
    }
    protected bool T(ConstantExpression x,ConstantExpression y){
        if(x==y) return true;
        if(ReferenceEquals(x.Value,y.Value)) return true;
        if(x.Type!=y.Type)return @false;
        if(!this.ObjectComparer.Equals(x.Value,y.Value)) return @false;
        return true;
    }
    //Equals(x.Value,y.Value);
    public int GetHashCode(SymbolDocumentInfo obj)=>0;
    internal bool lnternalEquals(SymbolDocumentInfo? x,SymbolDocumentInfo? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        if(x!.DocumentType!=y!.DocumentType)return @false;
        if(x.Language!=y.Language)return @false;
        if(x.FileName!=y.FileName) return @false;
        return true;
    }
    protected bool T(DebugInfoExpression x,DebugInfoExpression y){
        if(x==y) return true;
        if(x.StartLine!=y.StartLine)return @false;
        if(x.StartColumn!=y.StartColumn)return @false;
        if(x.EndLine!=y.EndLine)return @false;
        if(x.EndColumn!=y.EndColumn)return @false;
        return this.lnternalEquals(x.Document,y.Document);
    }
    protected bool T(DefaultExpression x,DefaultExpression y){
        if(x==y) return true;
        return x.Type==y.Type;
    }
    protected bool T(DynamicExpression x,DynamicExpression y){
        if(x==y) return true;
        if(!this.SequenceEqual(x.Arguments,y.Arguments)) return @false;
        var x_Binder=x.Binder;
        var y_Binder=y.Binder;
        Debug.Assert(x_Binder.GetType()==y_Binder.GetType(),"SequenceEqualの抜け穴パターンがあるか？");
        switch(x_Binder,y_Binder){
            case(DynamicMetaObjectBinder a0,DynamicMetaObjectBinder b0):{
                //if(a0.ReturnType!=b0.ReturnType) return @false;
                switch(a0,b0){
                    case(BinaryOperationBinder a1,BinaryOperationBinder b1):{
                        if(a1.Operation!=b1.Operation) return @false;
                        return true;
                    }
                    case(ConvertBinder a1,ConvertBinder b1):{
                        if(a1.Explicit!=b1.Explicit) return @false;
                        Debug.Assert(a1.ReturnType==a1.Type);
                        Debug.Assert(b1.ReturnType==b1.Type);
                        return true;
                    }
                    case(CreateInstanceBinder a1,CreateInstanceBinder b1):{
                        if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return @false;
                        if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames)) return @false;
                        return true;
                    }
                    case(DeleteIndexBinder a1,DeleteIndexBinder b1):{
                        if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return @false;
                        if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames)) return @false;
                        return true;
                    }
                    case(DeleteMemberBinder a1,DeleteMemberBinder b1):{
                        if(a1.IgnoreCase!=b1.IgnoreCase) return @false;
                        if(a1.Name!=b1.Name) return @false;
                        return true;
                    }
                    case(GetIndexBinder a1,GetIndexBinder b1):{
                        if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return @false;
                        if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames)) return @false;
                        return true;
                    }
                    case(GetMemberBinder a1,GetMemberBinder b1):{
                        Debug.Assert(a1.IgnoreCase==b1.IgnoreCase,"GetMemberBinder 本当はVBとかで破るパターンあるんじゃないのか");
                        if(a1.IgnoreCase!=b1.IgnoreCase) return @false;
                        if(a1.Name!=b1.Name) return @false;
                        return true;
                    }
                    case(InvokeBinder a1,InvokeBinder b1):{
                        if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return @false;
                        if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames)) return @false;
                        return true;
                    }
                    case(InvokeMemberBinder a1,InvokeMemberBinder b1):{
                        Debug.Assert(a1.IgnoreCase==b1.IgnoreCase,"InvokeMemberBinder 本当はVBとかで破るパターンあるんじゃないのか");
                        if(a1.Name!=b1.Name) return @false;
                        if(a1.IgnoreCase!=b1.IgnoreCase) return @false;
                        if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames)) return @false;
                        return true;
                    }
                    case(SetIndexBinder a1,SetIndexBinder b1):{
                        Debug.Assert(a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames),"CallInfo.Argumentsが違うパターンもあるんじゃないか");
                        if(a1.CallInfo.ArgumentCount!=b1.CallInfo.ArgumentCount) return @false;
                        if(!a1.CallInfo.ArgumentNames.SequenceEqual(b1.CallInfo.ArgumentNames)) return @false;
                        return true;
                    }
                    case(SetMemberBinder a1,SetMemberBinder b1):{
                        Debug.Assert(a1.IgnoreCase==b1.IgnoreCase,"本当はVBとかで破るパターンあるんじゃないのか");
                        if(!a1.Name.Equals(b1.Name,StringComparison.Ordinal)) return @false;
                        return true;
                    }
                    case(UnaryOperationBinder a1,UnaryOperationBinder b1):{
                        if(a1.Operation!=b1.Operation) return @false;
                        return true;
                    }
                    default:return @false;
                    //throw new NotSupportedException($"{a0.GetType()},{b0.GetType()}が一致しない");
                }
            }
            default:return @false;
            //throw new NotSupportedException($"{x.GetType()},{y.GetType()}が一致しない");
        }
    }
    //private bool InitializersEquals(ReadOnlyCollection<ElementInit> x_Initializers,ReadOnlyCollection<ElementInit> y_Initializers) {
    //    if(x_Initializers.Count!=y_Initializers.Count)
    //        return @false;
    //    var x_Initializers_Count = x_Initializers.Count;
    //    for(var c = 0;c<x_Initializers_Count;c++) {
    //        this.Equals(x_Initializers[c],)
    //        var x = x_Initializers[c];
    //        var y = y_Initializers[c];
    //        if(x.AddMethod!=y.AddMethod)
    //            return @false;
    //        if(!this,x.Arguments,y.Arguments))
    //            return @false;
    //    }
    //    return true;
    //}
    //private bool MemberBindingsEquals(ReadOnlyCollection<MemberBinding> x,ReadOnlyCollection<MemberBinding> y) {
    //    if(x.Count!=y.Count)
    //        return @false;
    //    var x_Bindings_Count = x.Count;
    //    for(var c=0;c<x_Bindings_Count;c++)
    //        if(!this.InternalEquals(x[c],y[c])) return false;
    //    return true;
    //}
    protected bool T(MemberInitExpression x,MemberInitExpression y){
        if(x==y) return true;
        if(this.ProtectedPrivateEquals(x.NewExpression,y.NewExpression))
            return this.SequenceEqual(x.Bindings,y.Bindings);
        return @false;
    }
    protected bool T(NewArrayExpression x,NewArrayExpression y){
        if(x==y) return true;
        return this.SequenceEqual(x.Expressions,y.Expressions);
    }
    //パラメータが同じならオーバーロードでコンストラクタが異なることはあり得ない。
    //しかし異なる型の同じパラメータコンストラクタはあり得る。
    protected bool T(NewExpression x,NewExpression y){
        if(x==y) return true;
        if(x.Constructor==y.Constructor) return this.SequenceEqual(x.Arguments,y.Arguments);
        return @false;
    }
    public int GetHashCode(LabelTarget obj)=>0;
    private protected bool NullableEquals(LabelTarget? x,LabelTarget? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        //if(x is null)
        //    if(y is null)
        //        return true;
        //    else
        //        return @false;
        if(x!.Type==y!.Type)
            if(x.Name==y.Name)
                if(this.x_LabelTargets.IndexOf(x)==this.y_LabelTargets.IndexOf(y))
                    return true;
        return @false;
        //if(x is null^y is null) return @false;
        //return x!.Type==y!.Type&&x.Name==y.Name&&
        //       this.x_LabelTargets.IndexOf(x)==this.y_LabelTargets.IndexOf(y);
    }
    protected bool T(LabelExpression x,LabelExpression y){
        if(x==y) return true;
        if(!this.NullableEquals(x.Target,y.Target)) return @false;
        var x_LabelTargets=this.x_LabelTargets;
        var y_LabelTargets=this.y_LabelTargets;
        var x_LabelTargets_Count=x_LabelTargets.Count;
        var y_LabelTargets_Count=y_LabelTargets.Count;
        Debug.Assert(x_LabelTargets_Count==y_LabelTargets_Count);
        x_LabelTargets.Add(x.Target);
        y_LabelTargets.Add(y.Target);
        var r=this.NullableEquals(x.DefaultValue,y.DefaultValue);
        x_LabelTargets.RemoveRange(x_LabelTargets_Count,1);
        y_LabelTargets.RemoveRange(x_LabelTargets_Count,1);
        return r;
    }
    protected abstract bool T(LambdaExpression x,LambdaExpression y);
    protected bool T(ListInitExpression x,ListInitExpression y){
        if(x==y) return true;
        if(this.ProtectedPrivateEquals(x.NewExpression,y.NewExpression)) return this.SequenceEqual(x.Initializers,y.Initializers);
        return @false;
    }
    protected bool T(LoopExpression x,LoopExpression y){
        if(x==y) return true;
        var x_BreakLabel=x.BreakLabel;
        var y_BreakLabel=y.BreakLabel;
        var x_ContinueLabel=x.ContinueLabel;
        var y_ContinueLabel=y.ContinueLabel;
        var x_LabelTargets=this.x_LabelTargets;
        var y_LabelTargets=this.y_LabelTargets;
        var x_LabelTargets_Count=x_LabelTargets.Count;
        var y_LabelTargets_Count=y_LabelTargets.Count;
        Debug.Assert(x_LabelTargets_Count==y_LabelTargets_Count);
        if(x_BreakLabel is not null){
            if(y_BreakLabel is null) return @false;
            x_LabelTargets.Add(x_BreakLabel);
            y_LabelTargets.Add(y_BreakLabel);
        } else if(y_BreakLabel is not null) return @false;
        if(x_ContinueLabel is not null){
            if(y_ContinueLabel is null) return @false;
            x_LabelTargets.Add(x_ContinueLabel);
            y_LabelTargets.Add(y_ContinueLabel);
        } else if(y_ContinueLabel is not null) return @false;
        var r=this.ProtectedPrivateEquals(x.Body,y.Body);
        Debug.Assert(x_LabelTargets.Count==y_LabelTargets.Count);
        x_LabelTargets.RemoveRange(x_LabelTargets_Count,x_LabelTargets.Count-x_LabelTargets_Count);
        y_LabelTargets.RemoveRange(y_LabelTargets_Count,y_LabelTargets.Count-y_LabelTargets_Count);
        return r;
    }
    protected bool T(MemberExpression x,MemberExpression y){
        if(x==y) return true;
        if(x.Member!=y.Member)return @false;
        if(!this.NullableEquals(x.Expression,y.Expression))return @false;
        return true;
    }
    protected bool T(IndexExpression x,IndexExpression y){
        if(x==y) return true;
        if(x.Indexer!=y.Indexer) return @false;
        if(!this.ProtectedPrivateEquals(x.Object,y.Object))return @false;
        if(!this.SequenceEqual(x.Arguments,y.Arguments))return @false;
        return true;
    }
    protected bool T(MethodCallExpression x,MethodCallExpression y){
        if(x==y) return true;
        if(x.Method is not DynamicMethod)
            if(y.Method is not DynamicMethod){
                if(x.Method!=y.Method) return @false;
                if(!this.NullableEquals(x.Object,y.Object))return @false;
            }else return @false;
        else if(y.Method is not DynamicMethod)
            return @false;
        return this.SequenceEqual(x.Arguments,y.Arguments);
        //if(x.Method is DynamicMethod)
        //    if(y.Method is DynamicMethod)
        //        return this.SequenceEqual(x.Arguments,y.Arguments);
        //if(x.Method!=y.Method) return @false;
        //if(!this.PrivateProtectedEquals(x.Object,y.Object))return @false;
        //return this.SequenceEqual(x.Arguments,y.Arguments);
    }
    protected bool T(InvocationExpression x,InvocationExpression y){
        if(x==y) return true;
        if(!this.ProtectedPrivateEquals(x.Expression,y.Expression)) return @false;
        return this.SequenceEqual(x.Arguments,y.Arguments);
    }
    protected bool T(GotoExpression x,GotoExpression y){
        if(x==y) return true;
        if(this.x_LabelTargets.IndexOf(x.Target)!=this.y_LabelTargets.IndexOf(y.Target)) return @false;
        return this.NullableEquals(x.Value,y.Value);
    }
    public int GetHashCode(ParameterExpression obj)=>0;
    //protected abstract bool Equals(ParameterExpression? x,ParameterExpression? y);
    protected bool T(RuntimeVariablesExpression x,RuntimeVariablesExpression y){
        if(x==y) return true;
        return this.SequenceEqual(x.Variables,y.Variables);
    }
    protected bool T(SwitchExpression x,SwitchExpression y){
        if(x==y) return true;
        if(x.Comparison!=y.Comparison) return @false;
        if(!this.ProtectedPrivateEquals(x.DefaultBody,y.DefaultBody)) return @false;
        if(!this.ProtectedPrivateEquals(x.SwitchValue,y.SwitchValue)) return @false;
        var x_Cases=x.Cases;
        var y_Cases=y.Cases;
        var x_Cases_Count=x_Cases.Count;
        if(x_Cases.Count!=y_Cases.Count) return @false;
        for(var c=0;c<x_Cases_Count;c++){
            var x_Case=x_Cases[c];
            var y_Case=y_Cases[c];
            if(!this.ProtectedPrivateEquals(x_Case.Body,y_Case.Body)) return @false;
            if(!this.SequenceEqual(x_Case.TestValues,y_Case.TestValues)) return @false;
        }
        return true;
    }
    protected bool T(TryExpression x,TryExpression y){
        if(x==y) return true;
        if(!this.ProtectedPrivateEquals(x.Body,y.Body)) return @false;
        if(!this.NullableEquals(x.Fault,y.Fault)) return @false;
        if(!this.NullableEquals(x.Finally,y.Finally)) return @false;
        var x_Handlers=x.Handlers;
        var y_Handlers=y.Handlers;
        var x_Handlers_Count=x_Handlers.Count;
        if(x_Handlers_Count!=y_Handlers.Count) return @false;
        for(var c=0;c<x_Handlers_Count;c++)
            if(!this.NullableEquals(x_Handlers[c],y_Handlers[c]))
                return false;
        return true;
    }
    protected bool T(TypeBinaryExpression x,TypeBinaryExpression y){
        if(x==y) return true;
        if(x.Type!=y.Type) return @false;
        if(x.TypeOperand!=y.TypeOperand) return @false;
        return this.ProtectedPrivateEquals(x.Expression,y.Expression);
    }
    protected bool T(UnaryExpression x,UnaryExpression y){
        if(x==y) return true;
        if(x.Method!=y.Method) return @false;
        if(x.Type!=y.Type) return @false;
        //return this.Equals(x.Operand,y.Operand);
        //x.Operand is not null&&y.Operand is not null&&this.ProtectedEquals(x.Operand,y.Operand)||
        //    x.Operand is null&&y.Operand is null);
        return this.NullableEquals(x.Operand,y.Operand);
    }
    public int GetHashCode(ElementInit obj)=>0;
    private protected bool NullableEquals(ElementInit? x,ElementInit? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        if(x!.AddMethod!=y!.AddMethod) return @false;
        return this.SequenceEqual(x.Arguments,y.Arguments);
    }
    public int GetHashCode(MemberBinding obj)=>0;
    private protected bool NullableEquals(MemberBinding? x,MemberBinding? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        if(x!.BindingType!=y!.BindingType) return @false;
        return x.BindingType switch{
            MemberBindingType.Assignment=>this.NullableEquals((MemberAssignment)x,(MemberAssignment)y),
            MemberBindingType.ListBinding=>this.NullableEquals((MemberListBinding)x,(MemberListBinding)y),
            _=>this.NullableEquals((MemberMemberBinding)x,(MemberMemberBinding)y),
        };
    }
    public int GetHashCode(MemberAssignment obj)=>0;
    private protected bool NullableEquals(MemberAssignment? x,MemberAssignment? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        if(x!.Member!=y!.Member) return @false;
        return this.Equals(x.Expression,y.Expression);
    }
    public int GetHashCode(MemberListBinding obj)=>0;
    private protected bool NullableEquals(MemberListBinding? x,MemberListBinding? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        if(x!.Member!=y!.Member) return @false;
        return this.SequenceEqual(x.Initializers,y.Initializers);
    }
    public int GetHashCode(MemberMemberBinding obj)=>0;
    private protected bool NullableEquals(MemberMemberBinding? x,MemberMemberBinding? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        if(x!.Member!=y!.Member) return @false;
        return this.SequenceEqual(x.Bindings,y.Bindings);
    }
    public int GetHashCode(CSharpArgumentInfo obj)=>0;
    private protected bool NullableEquals(CSharpArgumentInfo? x,CSharpArgumentInfo? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        dynamic x0=new NonPublicAccessor(x),y0=new NonPublicAccessor(y);
        if(x0.Flags!=y0.Flags) return @false;
        if(x0.IsByRefOrOut!=y0.IsByRefOrOut) return @false;
        if(x0.IsStaticType!=y0.IsStaticType) return @false;
        if(x0.LiteralConstant!=y0.LiteralConstant) return @false;
        if(x0.Name!=y0.Name) return @false;
        if(x0.NamedArgument!=y0.NamedArgument) return @false;
        if(x0.UseCompileTimeType!=y0.UseCompileTimeType) return @false;
        return true;
    }
    public int GetHashCode(CatchBlock obj)=>0;
    private protected bool NullableEquals(CatchBlock? x,CatchBlock? y){
        if(x==y) return true;
        Debug.Assert((x is null^y is null)==(x is null^y is null));
        if(x is null^y is null) return @false;
        if(x!.Test!=y!.Test) return @false;
        var x_Variable=x.Variable;
        var y_Variable=y.Variable;
        if(x_Variable is null^y_Variable is null) return @false;
        var x_Parameters=this.x_Parameters;
        var y_Parameters=this.y_Parameters;
        var x_Parameters_Count=x_Parameters.Count;
        Debug.Assert(x_Parameters_Count==y_Parameters.Count);
        if(x_Variable is not null){
            x_Parameters.Add(x_Variable);
            y_Parameters.Add(y_Variable);
        }
        if(!this.ProtectedPrivateEquals(x.Body,y.Body)) return @false;
        if(!this.NullableEquals(x.Filter,y.Filter)) return @false;
        if(x_Variable is not null){
            x_Parameters.RemoveAt(x_Parameters_Count);
            y_Parameters.RemoveAt(x_Parameters_Count);
        }
        return true;
    }

    public int GetHashCode(SwitchCase obj)=>0;
    private protected bool NullableEquals(SwitchCase? x,SwitchCase? y){
        if(x==y) return true;
        if(x is null^y is null) return @false;
        if(!this.ProtectedPrivateEquals(x!.Body,y!.Body))return @false;
        if(!this.SequenceEqual(x.TestValues,y.TestValues)) return @false;
        return true;
    }
    private bool SequenceEqual(Generic.IList<Expression> x,Generic.IList<Expression> y){
        var x_Count=x.Count;
        if(x_Count!=y.Count) return @false;
        for(var i=0;i<x_Count;i++)
            if(!this.NullableEquals(x[i],y[i]))
                return @false;
        return true;
    }
    private bool SequenceEqual(Generic.IList<MemberBinding> x,Generic.IList<MemberBinding> y){
        var x_Count=x.Count;
        if(x_Count!=y.Count) return @false;
        for(var i=0;i<x_Count;i++)
            if(!this.NullableEquals(x[i],y[i]))
                return @false;
        return true;
    }
    private bool SequenceEqual(Generic.IList<ElementInit> x,Generic.IList<ElementInit> y){
        var x_Count=x.Count;
        if(x_Count!=y.Count) return @false;
        for(var i=0;i<x_Count;i++)
            if(!this.NullableEquals(x[i],y[i]))
                return @false;
        return true;
    }
    private bool SequenceEqual(Generic.IList<ParameterExpression> x,Generic.IList<ParameterExpression> y){
        var x_Count=x.Count;
        if(x_Count!=y.Count) return @false;
        for(var i=0;i<x_Count;i++)
            if(!this.NullableEquals(x[i],y[i]))
                return @false;
        return true;
    }
}
