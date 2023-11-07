using LinqDB.Helpers;

using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
//using Microsoft.CSharp.RuntimeBinder;
using Microsoft.CSharp.RuntimeBinder;
// ReSharper disable All
namespace LinqDB.Optimizers.Comparison;
using Generic = System.Collections.Generic;
public sealed class ExpressionReferenceEqualityComparer : Generic.IEqualityComparer<Expression>
//,Generic.IEqualityComparer<ParameterExpression>//,Generic.IEqualityComparer<LabelTarget>,Generic.IEqualityComparer<CatchBlock>,Generic.IEqualityComparer<CSharpArgumentInfo>,Generic.IEqualityComparer<SwitchCase>,
//,Generic.IEqualityComparer<MemberBinding>//,Generic.IEqualityComparer<MemberAssignment>,Generic.IEqualityComparer<MemberListBinding>,Generic.IEqualityComparer<MemberMemberBinding>
//,Generic.IEqualityComparer<ElementInit>
//,Generic.IEqualityComparer<SymbolDocumentInfo>
{
    protected static bool @false => false;
    protected 汎用Comparer ObjectComparer => new();

    /// <summary>
    /// 式木のハッシュコード。NodeTypeとTypeを使う。
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public int GetHashCode(Expression e) => e.GetHashCode();
    public bool Equals(ParameterExpression? x, ParameterExpression? y) => x==y;
    /// <summary>
    /// 式木同士が一致するか。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(Expression? x, Expression? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        return this.InternalEquals(x, y);
    }
    internal bool InternalEquals(Expression? x0, Expression? y0)
    {
        if (x0 is null)
            return y0 is null;
        if (y0 is null)
            return @false;
        return this.ProtectedEquals(x0, y0);
    }
    protected bool ProtectedEquals(Expression x, Expression y)
    {
        if (x.NodeType!=y?.NodeType||x.Type!=y.Type)
            return @false;
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (x.NodeType)
        {
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
            case ExpressionType.Assign:
                return this.T((BinaryExpression)x, (BinaryExpression)y);
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
                return this.T((UnaryExpression)x, (UnaryExpression)y);
            case ExpressionType.Block:
                return this.T((BlockExpression)x, (BlockExpression)y);
            case ExpressionType.Conditional:
                return this.T((ConditionalExpression)x, (ConditionalExpression)y);
            case ExpressionType.Constant:
                return this.T((ConstantExpression)x, (ConstantExpression)y);
            case ExpressionType.DebugInfo:
                return this.T((DebugInfoExpression)x, (DebugInfoExpression)y);
            case ExpressionType.Default:
                return this.T((DefaultExpression)x, (DefaultExpression)y);
            case ExpressionType.Dynamic:
                return this.T((DynamicExpression)x, (DynamicExpression)y);
            case ExpressionType.Goto:
                return this.T((GotoExpression)x, (GotoExpression)y);
            case ExpressionType.Index:
                return this.T((IndexExpression)x, (IndexExpression)y);
            case ExpressionType.Invoke:
                return this.T((InvocationExpression)x, (InvocationExpression)y);
            case ExpressionType.Label:
                return this.T((LabelExpression)x, (LabelExpression)y);
            case ExpressionType.Lambda:
                return this.T((LambdaExpression)x, (LambdaExpression)y);
            case ExpressionType.ListInit:
                return this.T((ListInitExpression)x, (ListInitExpression)y);
            case ExpressionType.Loop:
                return this.T((LoopExpression)x, (LoopExpression)y);
            case ExpressionType.MemberAccess:
                return this.T((MemberExpression)x, (MemberExpression)y);
            case ExpressionType.MemberInit:
                return this.T((MemberInitExpression)x, (MemberInitExpression)y);
            case ExpressionType.Call:
                return this.T((MethodCallExpression)x, (MethodCallExpression)y);
            case ExpressionType.NewArrayBounds:
            case ExpressionType.NewArrayInit:
                return this.T((NewArrayExpression)x, (NewArrayExpression)y);
            case ExpressionType.New:
                return this.T((NewExpression)x, (NewExpression)y);
            case ExpressionType.Parameter:
                return this.Equals((ParameterExpression)x, (ParameterExpression)y);
            case ExpressionType.RuntimeVariables:
                return this.T((RuntimeVariablesExpression)x, (RuntimeVariablesExpression)y);
            case ExpressionType.Switch:
                return this.T((SwitchExpression)x, (SwitchExpression)y);
            case ExpressionType.Try:
                return this.T((TryExpression)x, (TryExpression)y);
            case ExpressionType.TypeEqual:
            case ExpressionType.TypeIs:
                return this.T((TypeBinaryExpression)x, (TypeBinaryExpression)y);
            default:
                throw new NotSupportedException($"{x.NodeType}はサポートされていない");
        }
    }
    protected bool T(BinaryExpression x, BinaryExpression y) => x.Method==y.Method&&this.ProtectedEquals(x.Left, y.Left)&&this.ProtectedEquals(x.Right, y.Right)&&this.InternalEquals(x.Conversion, y.Conversion);
    protected bool T(BlockExpression x, BlockExpression y)
    {
        //if(!this.T(x.Variables,y.Variables)) return @false;
        //return this,x.Expressions,y.Expressions);
        if (x.Type!=y.Type) return @false;
        if (!this.SequenceEqual(x.Variables, y.Variables)) return @false;
        return this.SequenceEqual(x.Expressions, y.Expressions);
    }
    protected bool T(ConditionalExpression x, ConditionalExpression y) =>
        this.ProtectedEquals(x.Test, y.Test)&&
        this.ProtectedEquals(x.IfTrue, y.IfTrue)&&
        this.ProtectedEquals(x.IfFalse, y.IfFalse);
    protected bool T(ConstantExpression x, ConstantExpression y) =>
        ReferenceEquals(x.Value, y.Value)||
        x.Type==y.Type&&this.ObjectComparer.Equals(x.Value, y.Value);
    //Equals(x.Value,y.Value);
    public int GetHashCode(SymbolDocumentInfo obj) => 0;
    internal bool lnternalEquals(SymbolDocumentInfo? x, SymbolDocumentInfo? y) =>
        x==y||x==null&&y==null||
        x!=null&&y!=null&&
        x.DocumentType==y!.DocumentType&&
        x.Language==y.Language&&
        x.FileName==y.FileName;
    protected bool T(DebugInfoExpression x, DebugInfoExpression y) =>
        this.lnternalEquals(x.Document, y.Document)&&
        x.StartLine==y.StartLine&&
        x.StartColumn==y.StartColumn&&
        x.EndLine==y.EndLine&&
        x.EndColumn==y.EndColumn;
    protected bool T(DefaultExpression x, DefaultExpression y) => x.Type==y.Type;
    protected bool T(DynamicExpression x, DynamicExpression y)
    {
        if (!this.SequenceEqual(x.Arguments, y.Arguments))
            return @false;
        var x_Binder = x.Binder;
        var y_Binder = y.Binder;
        Debug.Assert(x_Binder.GetType()==y_Binder.GetType(), "SequenceEqualの抜け穴パターンがあるか？");
        switch (x_Binder, y_Binder)
        {
            case (DynamicMetaObjectBinder x0, DynamicMetaObjectBinder y0):
                {
                    if (x0.ReturnType!=y0.ReturnType) return @false;
                    switch (x0, y0)
                    {
                        case (BinaryOperationBinder x1, BinaryOperationBinder y1):
                            {
                                if (x1.Operation!=y1.Operation) return @false;
                                return true;
                            }
                        case (ConvertBinder x1, ConvertBinder y1):
                            {
                                if (x1.Explicit!=y1.Explicit) return @false;
                                Debug.Assert(x1.ReturnType==x1.Type);
                                Debug.Assert(y1.ReturnType==y1.Type);
                                return true;
                            }
                        case (CreateInstanceBinder x1, CreateInstanceBinder y1):
                            {
                                if (x1.CallInfo.ArgumentCount!=y1.CallInfo.ArgumentCount) return @false;
                                if (!x1.CallInfo.ArgumentNames.SequenceEqual(y1.CallInfo.ArgumentNames)) return @false;
                                return true;
                            }
                        case (DeleteIndexBinder x1, DeleteIndexBinder y1):
                            {
                                if (x1.CallInfo.ArgumentCount!=y1.CallInfo.ArgumentCount) return @false;
                                if (!x1.CallInfo.ArgumentNames.SequenceEqual(y1.CallInfo.ArgumentNames)) return @false;
                                return true;
                            }
                        case (DeleteMemberBinder x1, DeleteMemberBinder y1):
                            {
                                if (x1.IgnoreCase!=y1.IgnoreCase) return @false;
                                if (x1.Name!=y1.Name) return @false;
                                return true;
                            }
                        case (GetIndexBinder x1, GetIndexBinder y1):
                            {
                                if (x1.CallInfo.ArgumentCount!=y1.CallInfo.ArgumentCount) return @false;
                                if (!x1.CallInfo.ArgumentNames.SequenceEqual(y1.CallInfo.ArgumentNames)) return @false;
                                return true;
                            }
                        case (GetMemberBinder x1, GetMemberBinder y1):
                            {
                                Debug.Assert(x1.IgnoreCase==y1.IgnoreCase, "GetMemberBinder 本当はVBとかで破るパターンあるんじゃないのか");
                                if (x1.IgnoreCase!=y1.IgnoreCase) return @false;
                                if (x1.Name!=y1.Name) return @false;
                                return true;
                            }
                        case (InvokeBinder x1, InvokeBinder y1):
                            {
                                if (x1.CallInfo.ArgumentCount!=y1.CallInfo.ArgumentCount) return @false;
                                if (!x1.CallInfo.ArgumentNames.SequenceEqual(y1.CallInfo.ArgumentNames)) return @false;
                                return true;
                            }
                        case (InvokeMemberBinder x1, InvokeMemberBinder y1):
                            {
                                Debug.Assert(x1.IgnoreCase==y1.IgnoreCase, "InvokeMemberBinder 本当はVBとかで破るパターンあるんじゃないのか");
                                if (x1.Name!=y1.Name) return @false;
                                if (x1.IgnoreCase!=y1.IgnoreCase) return @false;
                                if (!x1.CallInfo.ArgumentNames.SequenceEqual(y1.CallInfo.ArgumentNames)) return @false;
                                return true;
                            }
                        case (SetIndexBinder x1, SetIndexBinder y1):
                            {
                                Debug.Assert(x1.CallInfo.ArgumentNames.SequenceEqual(y1.CallInfo.ArgumentNames), "CallInfo.Argumentsが違うパターンもあるんじゃないか");
                                if (x1.CallInfo.ArgumentCount!=y1.CallInfo.ArgumentCount) return @false;
                                if (!x1.CallInfo.ArgumentNames.SequenceEqual(y1.CallInfo.ArgumentNames)) return @false;
                                return true;
                            }
                        case (SetMemberBinder x1, SetMemberBinder y1):
                            {
                                Debug.Assert(x1.IgnoreCase==y1.IgnoreCase, "本当はVBとかで破るパターンあるんじゃないのか");
                                return x1.Name.Equals(y1.Name, StringComparison.Ordinal);
                            }
                        case (UnaryOperationBinder x1, UnaryOperationBinder y1):
                            {
                                if (x1.Operation!=y1.Operation) return @false;
                                return true;
                            }
                        default:
                            throw new NotSupportedException($"{x0.GetType()},{y0.GetType()}が一致しない");
                    }
                }
            default:
                throw new NotSupportedException($"{x.GetType()},{y.GetType()}が一致しない");
        }
    }
    protected bool T(MemberInitExpression x, MemberInitExpression y) =>
        this.ProtectedEquals(x.NewExpression, y.NewExpression)&&
        this.SequenceEqual(x.Bindings, y.Bindings);
    protected bool T(NewArrayExpression x, NewArrayExpression y) => this.SequenceEqual(x.Expressions, y.Expressions);
    //パラメータが同じならオーバーロードでコンストラクタが異なることはあり得ない。
    //しかし異なる型の同じパラメータコンストラクタはあり得る。
    protected bool T(NewExpression x, NewExpression y) =>
        this.SequenceEqual(x.Arguments, y.Arguments)&&
        x.Constructor==y.Constructor;
    public int GetHashCode(LabelTarget obj) => 0;
    internal bool InternalEquals(LabelTarget? x, LabelTarget? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        if (x!.Type!=y!.Type||x.Name!=y.Name) return @false;
        return x==y;
    }
    protected bool T(LabelExpression x, LabelExpression y)
    {
        if (!this.InternalEquals(x.Target, y.Target)) return @false;
        return this.InternalEquals(x.DefaultValue, y.DefaultValue);
    }
    protected bool T(LambdaExpression x, LambdaExpression y)
    {
        if (x.Type!=y.Type||x.TailCall!=y.TailCall) return @false;
        return this.ProtectedEquals(x.Body, y.Body);
    }
    protected bool T(ListInitExpression x, ListInitExpression y) =>
        this.ProtectedEquals(x.NewExpression, y.NewExpression)&&
        this.SequenceEqual(x.Initializers, y.Initializers);
    protected bool T(LoopExpression x, LoopExpression y)
    {
        var x_BreakLabel = x.BreakLabel;
        var y_BreakLabel = y.BreakLabel;
        if (x_BreakLabel!=y_BreakLabel) return @false;
        var x_ContinueLabel = x.ContinueLabel;
        var y_ContinueLabel = y.ContinueLabel;
        if (x_ContinueLabel!=y_ContinueLabel) return @false;
        if (x_BreakLabel is null^y_BreakLabel is null) return @false;
        if (x_ContinueLabel is null^y_ContinueLabel is null) return @false;
        return this.ProtectedEquals(x.Body, y.Body);
    }
    protected bool T(MemberExpression x, MemberExpression y) => x.Member==y.Member&&this.InternalEquals(x.Expression, y.Expression);
    protected bool T(IndexExpression x, IndexExpression y) =>
        x.Indexer==y.Indexer&&
        this.ProtectedEquals(x.Object, y.Object)&&
        this.SequenceEqual(x.Arguments, y.Arguments);
    protected bool T(MethodCallExpression x, MethodCallExpression y)
    {
        var x_Method = x.Method;
        var y_Method = y.Method;
        if (
            x_Method is DynamicMethod&&
            y_Method is DynamicMethod
        )
        {
            return this.SequenceEqual(x.Arguments, y.Arguments);
        }
        return x.Method==y.Method&&
               this.InternalEquals(x.Object, y.Object)&&
               this.SequenceEqual(x.Arguments, y.Arguments);
    }
    protected bool T(InvocationExpression x, InvocationExpression y) =>
        this.ProtectedEquals(x.Expression, y.Expression)&&
        this.SequenceEqual(x.Arguments, y.Arguments);
    protected bool T(GotoExpression x, GotoExpression y) =>
        x.Target==y.Target&&
        this.InternalEquals(x.Value, y.Value);
    public int GetHashCode(ParameterExpression obj) => 0;
    //protected abstract bool Equals(ParameterExpression? x,ParameterExpression? y);
    protected bool T(RuntimeVariablesExpression x, RuntimeVariablesExpression y) => this.SequenceEqual(x.Variables, y.Variables);
    protected bool T(SwitchExpression x, SwitchExpression y)
    {
        if (x.Comparison!=y.Comparison)
            return @false;
        if (!this.ProtectedEquals(x.DefaultBody, y.DefaultBody))
            return @false;
        if (!this.ProtectedEquals(x.SwitchValue, y.SwitchValue))
            return @false;
        var x_Cases = x.Cases;
        var y_Cases = y.Cases;
        var x_Cases_Count = x_Cases.Count;
        if (x_Cases.Count!=y_Cases.Count)
            return @false;
        for (var c = 0; c<x_Cases_Count; c++)
        {
            var x_Case = x_Cases[c];
            var y_Case = y_Cases[c];
            if (!this.ProtectedEquals(x_Case.Body, y_Case.Body))
                return @false;
            if (!this.SequenceEqual(x_Case.TestValues, y_Case.TestValues))
                return @false;
        }
        return true;
    }
    protected bool T(TryExpression x, TryExpression y)
    {
        if (!this.ProtectedEquals(x.Body, y.Body)) return @false;
        if (!this.InternalEquals(x.Fault, y.Fault)) return @false;
        if (!this.InternalEquals(x.Finally, y.Finally)) return @false;
        var x_Handlers = x.Handlers;
        var y_Handlers = y.Handlers;
        var x_Handlers_Count = x_Handlers.Count;
        if (x_Handlers_Count!=y_Handlers.Count)
            return @false;
        for (var c = 0; c<x_Handlers_Count; c++)
            if (!this.InternalEquals(x_Handlers[c], y_Handlers[c])) return false;
        return true;
    }
    protected bool T(TypeBinaryExpression x, TypeBinaryExpression y) =>
        this.ProtectedEquals(x.Expression, y.Expression)&&
        x.TypeOperand==y.TypeOperand&&
        x.Type==y.Type;
    protected bool T(UnaryExpression x, UnaryExpression y) =>
        x.Method==y.Method&&x.Type==y.Type&&(
            x.Operand is not null&&y.Operand is not null&&this.ProtectedEquals(x.Operand, y.Operand)||
            x.Operand is null&&y.Operand is null);
    public int GetHashCode(ElementInit obj) => 0;
    internal bool InternalEquals(ElementInit? x, ElementInit? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        if (x!.AddMethod!=y!.AddMethod) return @false;
        return this.SequenceEqual(x.Arguments, y.Arguments);
    }
    //public bool Equals(ElementInit? x,ElementInit? y) {
    //    this.Clear();
    //    return this.InternalEquals(x,y);
    //}
    //public bool Equals(MemberBinding? x,MemberBinding? y) {
    //    this.Clear();
    //    return this.InternalEquals(x,y);
    //}
    internal bool InternalEquals(MemberBinding? x, MemberBinding? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        if (x!.BindingType!=y!.BindingType) return @false;
        return x.BindingType switch
        {
            MemberBindingType.Assignment => this.InternalEquals((MemberAssignment)x, (MemberAssignment)y),
            MemberBindingType.ListBinding => this.InternalEquals((MemberListBinding)x, (MemberListBinding)y),
            _ => this.InternalEquals((MemberMemberBinding)x, (MemberMemberBinding)y),
        };
    }
    public int GetHashCode(MemberBinding obj) => 0;
    public int GetHashCode(MemberAssignment obj) => 0;
    internal bool InternalEquals(MemberAssignment? x, MemberAssignment? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        if (x!.Member!=y!.Member) return @false;
        return this.Equals(x.Expression, y.Expression);
    }
    public int GetHashCode(MemberListBinding obj) => 0;
    internal bool InternalEquals(MemberListBinding? x, MemberListBinding? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        if (x!.Member!=y!.Member) return @false;
        return this.SequenceEqual(x.Initializers, y.Initializers);
    }
    public int GetHashCode(MemberMemberBinding obj) => 0;
    internal bool InternalEquals(MemberMemberBinding? x, MemberMemberBinding? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        if (x!.Member!=y!.Member) return @false;
        return this.SequenceEqual(x.Bindings, y.Bindings);
    }
    public int GetHashCode(CSharpArgumentInfo obj) => 0;
    internal bool InternalEquals(CSharpArgumentInfo? x, CSharpArgumentInfo? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        dynamic x0 = new NonPublicAccessor(x), y0 = new NonPublicAccessor(y);
        if (x0.Flags!=y0.Flags) return @false;
        if (x0.IsByRefOrOut!=y0.IsByRefOrOut) return @false;
        if (x0.IsStaticType!=y0.IsStaticType) return @false;
        if (x0.LiteralConstant!=y0.LiteralConstant) return @false;
        if (x0.Name!=y0.Name) return @false;
        if (x0.NamedArgument!=y0.NamedArgument) return @false;
        if (x0.UseCompileTimeType!=y0.UseCompileTimeType) return @false;
        return true;
    }
    public int GetHashCode(CatchBlock obj) => 0;
    internal bool InternalEquals(CatchBlock? x, CatchBlock? y)
    {
        if (x==y) return true;
        Debug.Assert((x is null^y is null)==(x is null^y is null));
        if (x is null^y is null) return @false;
        if (x!.Test!=y!.Test) return @false;
        var x_Variable = x.Variable;
        var y_Variable = y.Variable;
        if (x_Variable is null^y_Variable is null) return @false;
        if (!this.ProtectedEquals(x.Body, y.Body)) return @false;
        if (!this.InternalEquals(x.Filter, y.Filter)) return @false;
        return true;
    }

    public int GetHashCode(SwitchCase obj) => 0;
    internal bool InternalEquals(SwitchCase? x, SwitchCase? y)
    {
        if (x==y) return true;
        if (x is null^y is null) return @false;
        return this.ProtectedEquals(x!.Body, y!.Body)&&this.SequenceEqual(x.TestValues, y.TestValues);
    }
    private bool SequenceEqual(Generic.IList<Expression> x, Generic.IList<Expression> y)
    {
        var x_Count = x.Count;
        if (x_Count!=y.Count) return @false;
        for (var i = 0; i<x_Count; i++)
            if (!this.InternalEquals(x[i], y[i])) return @false;
        return true;
    }
    private bool SequenceEqual(Generic.IList<MemberBinding> x, Generic.IList<MemberBinding> y)
    {
        var x_Count = x.Count;
        if (x_Count!=y.Count) return @false;
        for (var i = 0; i<x_Count; i++)
            if (!this.InternalEquals(x[i], y[i])) return @false;
        return true;
    }
    private bool SequenceEqual(Generic.IList<ElementInit> x, Generic.IList<ElementInit> y)
    {
        var x_Count = x.Count;
        if (x_Count!=y.Count) return @false;
        for (var i = 0; i<x_Count; i++)
            if (!this.InternalEquals(x[i], y[i])) return @false;
        return true;
    }
    private bool SequenceEqual(Generic.IList<ParameterExpression> x, Generic.IList<ParameterExpression> y)
    {
        var x_Count = x.Count;
        if (x_Count!=y.Count) return @false;
        for (var i = 0; i<x_Count; i++)
            if (!this.InternalEquals(x[i], y[i])) return @false;
        return true;
    }
}
