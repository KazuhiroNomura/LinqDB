using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
/// <summary>
/// Expressionを走査して返すトラバーサー
/// a+=b→a=a+b
/// a=bのaは評価される
/// </summary>
public class ReturnExpressionTraverser {
    protected readonly 作業配列 作業配列;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="作業配列"></param>
    protected ReturnExpressionTraverser(作業配列 作業配列)=>this.作業配列=作業配列;
    protected virtual Expression? TraverseNullable(Expression? Expression0) => Expression0 is null
       ?null
        : this.Traverse(Expression0);
    /// <summary>
    /// 式に対応するメソッドを呼び出す
    /// </summary>
    /// <param name="Expression0"></param>
    /// <returns></returns>
    protected virtual Expression Traverse(Expression Expression0) => Expression0.NodeType switch{
        ExpressionType.Add                  => this.Add                  ((BinaryExpression)Expression0),
        ExpressionType.AddAssign            => this.AddAssign            ((BinaryExpression)Expression0),
        ExpressionType.AddAssignChecked     => this.AddAssignChecked     ((BinaryExpression)Expression0),
        ExpressionType.AddChecked           => this.AddChecked           ((BinaryExpression)Expression0),
        ExpressionType.And                  => this.And                  ((BinaryExpression)Expression0),
        ExpressionType.AndAssign            => this.AndAssign            ((BinaryExpression)Expression0),
        ExpressionType.AndAlso              => this.AndAlso              ((BinaryExpression)Expression0),
        ExpressionType.ArrayIndex           => this.ArrayIndex           ((BinaryExpression)Expression0),
        ExpressionType.Assign               => this.Assign               ((BinaryExpression)Expression0),
        ExpressionType.Coalesce             => this.Coalesce             ((BinaryExpression)Expression0),
        ExpressionType.Divide               => this.Divide               ((BinaryExpression)Expression0),
        ExpressionType.DivideAssign         => this.DivideAssign         ((BinaryExpression)Expression0),
        ExpressionType.Equal                => this.Equal                ((BinaryExpression)Expression0),
        ExpressionType.ExclusiveOr          => this.ExclusiveOr          ((BinaryExpression)Expression0),
        ExpressionType.ExclusiveOrAssign    => this.ExclusiveOrAssign    ((BinaryExpression)Expression0),
        ExpressionType.GreaterThan          => this.GreaterThan          ((BinaryExpression)Expression0),
        ExpressionType.GreaterThanOrEqual   => this.GreaterThanOrEqual   ((BinaryExpression)Expression0),
        ExpressionType.LeftShift            => this.LeftShift            ((BinaryExpression)Expression0),
        ExpressionType.LeftShiftAssign      => this.LeftShiftAssign      ((BinaryExpression)Expression0),
        ExpressionType.LessThan             => this.LessThan             ((BinaryExpression)Expression0),
        ExpressionType.LessThanOrEqual      => this.LessThanOrEqual      ((BinaryExpression)Expression0),
        ExpressionType.Modulo               => this.Modulo               ((BinaryExpression)Expression0),
        ExpressionType.ModuloAssign         => this.ModuloAssign         ((BinaryExpression)Expression0),
        ExpressionType.Multiply             => this.Multiply             ((BinaryExpression)Expression0),
        ExpressionType.MultiplyAssign       => this.MultiplyAssign       ((BinaryExpression)Expression0),
        ExpressionType.MultiplyAssignChecked=> this.MultiplyAssignChecked((BinaryExpression)Expression0),
        ExpressionType.MultiplyChecked      => this.MultiplyChecked      ((BinaryExpression)Expression0),
        ExpressionType.NotEqual             => this.NotEqual             ((BinaryExpression)Expression0),
        ExpressionType.Or                   => this.Or                   ((BinaryExpression)Expression0),
        ExpressionType.OrAssign             => this.OrAssign             ((BinaryExpression)Expression0),
        ExpressionType.OrElse               => this.OrElse               ((BinaryExpression)Expression0),
        ExpressionType.Power                => this.Power                ((BinaryExpression)Expression0),
        ExpressionType.PowerAssign          => this.PowerAssign          ((BinaryExpression)Expression0),
        ExpressionType.RightShift           => this.RightShift           ((BinaryExpression)Expression0),
        ExpressionType.RightShiftAssign     => this.RightShiftAssign     ((BinaryExpression)Expression0),
        ExpressionType.Subtract             => this.Subtract             ((BinaryExpression)Expression0),
        ExpressionType.SubtractAssign       => this.SubtractAssign       ((BinaryExpression)Expression0),
        ExpressionType.SubtractAssignChecked=> this.SubtractAssignChecked((BinaryExpression)Expression0),
        ExpressionType.SubtractChecked      => this.SubtractChecked      ((BinaryExpression)Expression0),
        ExpressionType.Block                => this.Block                ((BlockExpression)Expression0),
        ExpressionType.Conditional          => this.Conditional          ((ConditionalExpression)Expression0),
        ExpressionType.Constant             => this.Constant             ((ConstantExpression)Expression0),
        ExpressionType.DebugInfo            => this.DebugInfo            ((DebugInfoExpression)Expression0),
        ExpressionType.Default              => this.Default              ((DefaultExpression)Expression0),
        ExpressionType.Dynamic              => this.Dynamic              ((DynamicExpression)Expression0),
        ExpressionType.Goto                 => this.Goto                 ((GotoExpression)Expression0),
        ExpressionType.Index                => this.Index                ((IndexExpression)Expression0),
        ExpressionType.Invoke               => this.Invoke               ((InvocationExpression)Expression0),
        ExpressionType.Label                => this.Label                ((LabelExpression)Expression0),
        ExpressionType.Lambda               => this.Lambda               ((LambdaExpression)Expression0),
        ExpressionType.ListInit             => this.ListInit             ((ListInitExpression)Expression0),
        ExpressionType.Loop                 => this.Loop                 ((LoopExpression)Expression0),
        ExpressionType.MemberAccess         => this.MemberAccess         ((MemberExpression)Expression0),
        ExpressionType.MemberInit           => this.MemberInit           ((MemberInitExpression)Expression0),
        ExpressionType.Call                 => this.Call                 ((MethodCallExpression)Expression0),
        ExpressionType.NewArrayBounds       => this.NewArrayBounds       ((NewArrayExpression)Expression0),
        ExpressionType.NewArrayInit         => this.NewArrayInit         ((NewArrayExpression)Expression0),
        ExpressionType.New                  => this.New                  ((NewExpression)Expression0),
        ExpressionType.Parameter            => this.Parameter            ((ParameterExpression)Expression0),
        ExpressionType.RuntimeVariables     => this.RuntimeVariables     ((RuntimeVariablesExpression)Expression0),
        ExpressionType.Switch               => this.Switch               ((SwitchExpression)Expression0),
        ExpressionType.Try                  => this.Try                  ((TryExpression)Expression0),
        ExpressionType.TypeEqual            => this.TypeEqual            ((TypeBinaryExpression)Expression0),
        ExpressionType.TypeIs               => this.TypeIs               ((TypeBinaryExpression)Expression0),
        ExpressionType.ArrayLength          => this.ArrayLength          ((UnaryExpression)Expression0),
        ExpressionType.Convert              => this.Convert              ((UnaryExpression)Expression0),
        ExpressionType.ConvertChecked       => this.ConvertChecked       ((UnaryExpression)Expression0),
        ExpressionType.Decrement            => this.Decrement            ((UnaryExpression)Expression0),
        ExpressionType.Increment            => this.Increment            ((UnaryExpression)Expression0),
        ExpressionType.IsFalse              => this.IsFalse              ((UnaryExpression)Expression0),
        ExpressionType.IsTrue               => this.IsTrue               ((UnaryExpression)Expression0),
        ExpressionType.Negate               => this.Negate               ((UnaryExpression)Expression0),
        ExpressionType.NegateChecked        => this.NegateChecked        ((UnaryExpression)Expression0),
        ExpressionType.Not                  => this.Not                  ((UnaryExpression)Expression0),
        ExpressionType.OnesComplement       => this.OnesComplement       ((UnaryExpression)Expression0),
        ExpressionType.PostDecrementAssign  => this.PostDecrementAssign  ((UnaryExpression)Expression0),
        ExpressionType.PostIncrementAssign  => this.PostIncrementAssign  ((UnaryExpression)Expression0),
        ExpressionType.PreDecrementAssign   => this.PreDecrementAssign   ((UnaryExpression)Expression0),
        ExpressionType.PreIncrementAssign   => this.PreIncrementAssign   ((UnaryExpression)Expression0),
        ExpressionType.Quote                => this.Quote                ((UnaryExpression)Expression0),
        ExpressionType.Throw                => this.Throw                ((UnaryExpression)Expression0),
        ExpressionType.TypeAs               => this.TypeAs               ((UnaryExpression)Expression0),
        ExpressionType.UnaryPlus            => this.UnaryPlus            ((UnaryExpression)Expression0),
        //ExpressionType.Unbox                => this.Unbox                ((UnaryExpression)Expression0),
        _                                   => this.Unbox                ((UnaryExpression)Expression0)
        //_ => throw new NotSupportedException($"{Expression0.NodeType}はサポートされてない"),
    };
    protected IList<Expression> TraverseExpressions(ReadOnlyCollection<Expression> Expressions0) {
        var Expressions0_Count = Expressions0.Count;
        var Expressions1 = new Expression[Expressions0_Count];
        var 変化したか = false;
        for(var a = 0;a < Expressions0_Count;a++) {
            var Expression0 = Expressions0[a];
            var Expression1 = this.Traverse(Expression0);
            if(Expression0!=Expression1)変化したか=true;
            Expressions1[a] = Expression1;
        }
        return 変化したか?Expressions1:Expressions0;
    }
    private Expression MakeBinary(BinaryExpression Binary0) {
        var Binary0_Left=Binary0.Left;
        var Binary0_Right=Binary0.Right;
        var Binary0_Conversion=Binary0.Conversion;
        var Binary1_Left=this.Traverse(Binary0_Left);
        var Binary1_Right=this.Traverse(Binary0_Right);
        var Binary1_Conversion= this.TraverseNullable(Binary0_Conversion);
        if(Binary0_Left==Binary1_Left)
            if(Binary0_Right==Binary1_Right)
                if(Binary0_Conversion==Binary1_Conversion)
                    return Binary0;
        return Expression.MakeBinary(Binary0.NodeType,Binary1_Left,Binary1_Right,Binary0.IsLiftedToNull,Binary0.Method,Binary1_Conversion as LambdaExpression);
    }
    /// <summary>
    /// a+=b→a=a+b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <param name="NodeType"></param>
    /// <returns></returns>
    protected virtual Expression MakeAssign(BinaryExpression Binary0, ExpressionType NodeType) {
        var Binary0_Left=Binary0.Left;
        var Binary0_Right=Binary0.Right;
        var Binary0_Conversion=Binary0.Conversion;
        var Binary1_Left=this.Traverse(Binary0_Left);
        var Binary1_Right=this.Traverse(Binary0_Right);
        var Binary1_Conversion= this.TraverseNullable(Binary0_Conversion);
        if(Binary0_Left==Binary1_Left)
            if(Binary0_Right==Binary1_Right)
                if(Binary0_Conversion==Binary1_Conversion)
                    return Binary0;
        return Expression.MakeBinary(NodeType,Binary1_Left,Binary1_Right,Binary0.IsLiftedToNull,Binary0.Method,Binary1_Conversion as LambdaExpression);
    }
    /// <summary>
    /// a+b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Add(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a+=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression AddAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.AddAssign);
    /// <summary>
    /// checked(a+=b)
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression AddAssignChecked(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.AddAssignChecked);
    /// <summary>
    /// checked(a+b)
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression AddChecked(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a&amp;b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression And(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a&amp;&amp;b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression AndAlso(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a&amp;=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression AndAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.AndAssign);
    /// <summary>
    /// a[b]
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression ArrayIndex(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a=b
    /// </summary>
    /// <param name="Assign0"></param>
    /// <returns></returns>
    protected virtual Expression Assign(BinaryExpression Assign0)=> this.MakeAssign(Assign0, ExpressionType.Assign);
    /// <summary>
    /// a??b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Coalesce(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a/b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Divide(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a/=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression DivideAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.DivideAssign);
    /// <summary>
    /// a==b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Equal(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a^b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression ExclusiveOr(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a^=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression ExclusiveOrAssign(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a>b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression GreaterThan(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a>=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression GreaterThanOrEqual(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a&lt;&lt;b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression LeftShift(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a&lt;&lt;=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression LeftShiftAssign(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a&lt;b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression LessThan(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a&lt;=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression LessThanOrEqual(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a%b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Modulo(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a%=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression ModuloAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.ModuloAssign);
    /// <summary>
    /// a*b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Multiply(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a*=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression MultiplyAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.MultiplyAssign);
    /// <summary>
    /// checked(a*=b)
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression MultiplyAssignChecked(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.MultiplyAssignChecked);
    /// <summary>
    /// checked(a*b)
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression MultiplyChecked(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a!=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression NotEqual(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a|b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Or(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a|=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression OrAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.OrAssign);
    /// <summary>
    /// a||b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression OrElse(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// VB a^=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression PowerAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.PowerAssign);
    /// <summary>
    /// VB a^b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Power(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a>>b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression RightShift(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a>>=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression RightShiftAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.RightShiftAssign);
    /// <summary>
    /// a-b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression Subtract(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// a-=b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression SubtractAssign(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.SubtractAssign);
    /// <summary>
    /// checked(a-=b)
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression SubtractAssignChecked(BinaryExpression Binary0)=> this.MakeAssign(Binary0, ExpressionType.SubtractAssignChecked);
    /// <summary>
    /// checked(a-b)
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected virtual Expression SubtractChecked(BinaryExpression Binary0)=> this.MakeBinary(Binary0);
    /// <summary>
    /// {
    ///     ;
    /// }
    /// </summary>
    /// <param name="Block0"></param>
    /// <returns></returns>
    protected virtual Expression Block(BlockExpression Block0) {
        var Block0_Expressions = Block0.Expressions;
        var Block0_Expressions_Count = Block0_Expressions.Count;
        if(Block0_Expressions_Count>=6||Block0.Variables.Count>=1||Block0_Expressions[Block0_Expressions_Count-1].Type!=Block0.Type){
            var Block1_Expressions=this.TraverseExpressions(Block0_Expressions);
            return ReferenceEquals(Block0_Expressions,Block1_Expressions)?Block0:Expression.Block(Block0.Type,Block0.Variables,Block1_Expressions);
        }
        var Block0_Expression0=Block0_Expressions[0];
        var Block1_Expression0=this.Traverse(Block0_Expression0);
        var b=Block1_Expression0==Block0_Expression0;
        if(Block0_Expressions_Count<=1){
            if(b) return Block0;
            return Block1_Expression0;
        }
        var Block0_Expression1=Block0_Expressions[1];
        var Block1_Expression1=this.Traverse(Block0_Expression1);
        b&=Block1_Expression1==Block0_Expression1;
        if(Block0_Expressions_Count<=2){
            if(b) return Block0;
            return Expression.Block(Block1_Expression0,Block1_Expression1);
        }
        var Block0_Expression2=Block0_Expressions[2];
        var Block1_Expression2=this.Traverse(Block0_Expression2);
        b&=Block1_Expression2==Block0_Expression2;
        if(Block0_Expressions_Count<=3){
            if(b) return Block0;
            return Expression.Block(Block1_Expression0,Block1_Expression1,Block1_Expression2);
        }
        var Block0_Expression3=Block0_Expressions[3];
        var Block1_Expression3=this.Traverse(Block0_Expression3);
        b&=Block1_Expression3==Block0_Expression3;
        if(Block0_Expressions_Count<=4){
            if(b) return Block0;
            return Expression.Block(Block1_Expression0,Block1_Expression1,Block1_Expression2,Block1_Expression3);
        }
        var Block0_Expression4=Block0_Expressions[4];
        var Block1_Expression4=this.Traverse(Block0_Expression4);
        b&=Block1_Expression4==Block0_Expression4;
        if(b)return Block0;
        return                               Expression.Block(Block1_Expression0,Block1_Expression1,Block1_Expression2,Block1_Expression3,Block1_Expression4);
    }
    /// <summary>
    /// a?b:c
    /// </summary>
    /// <param name="Conditional0"></param>
    /// <returns></returns>
    protected virtual Expression Conditional(ConditionalExpression Conditional0) {
        var Conditional0_Test=Conditional0.Test;
        var Conditional0_IfTrue=Conditional0.IfTrue;
        var Conditional0_IfFalse=Conditional0.IfFalse;
        var Conditional1_Test=this.Traverse(Conditional0_Test);
        var Conditional1_IfTrue=this.Traverse(Conditional0_IfTrue);
        var Conditional1_IfFalse=this.Traverse(Conditional0_IfFalse);
        if(Conditional0_Test==Conditional1_Test)
            if(Conditional0_IfTrue==Conditional1_IfTrue)
                if(Conditional0_IfFalse==Conditional1_IfFalse)
                    return Conditional0;
        return Expression.Condition(Conditional1_Test,Conditional1_IfTrue,Conditional1_IfFalse,Conditional0.Type);
    }
    /// <summary>
    /// "a"
    /// </summary>
    /// <param name="Constant0"></param>
    /// <returns></returns>
    protected virtual Expression Constant(ConstantExpression Constant0)=> Constant0;
    /// <summary>
    /// デバッグ情報
    /// </summary>
    /// <param name="DebugInfo0"></param>
    /// <returns></returns>
    protected virtual Expression DebugInfo(DebugInfoExpression DebugInfo0)=> Expression.DebugInfo(DebugInfo0.Document, DebugInfo0.StartLine, DebugInfo0.StartColumn, DebugInfo0.EndLine, DebugInfo0.EndColumn);
    /// <summary>
    /// default(T)
    /// </summary>
    /// <param name="Default0"></param>
    /// <returns></returns>
    protected virtual Expression Default(DefaultExpression Default0)=> Default0;
    /// <summary>
    /// (dynamic)a
    /// </summary>
    /// <param name="Dynamic0"></param>
    /// <returns></returns>
    protected virtual Expression Dynamic(DynamicExpression Dynamic0) {
        var Dynamic0_Arguments=Dynamic0.Arguments;
        var Dynamic0_Arguments_Count=Dynamic0_Arguments.Count;
        if(Dynamic0_Arguments_Count >= 5){
            var Dynamic1_Arguments=this.TraverseExpressions(Dynamic0_Arguments);
            return ReferenceEquals(Dynamic0_Arguments,Dynamic1_Arguments)?Dynamic0:Expression.Dynamic(Dynamic0.Binder,Dynamic0.Type,Dynamic1_Arguments);
        }
        var Dynamic0_Arguments_0 =Dynamic0_Arguments[0];
        var Dynamic1_Arguments_0=this.Traverse(Dynamic0_Arguments_0);
        var b=Dynamic0_Arguments_0==Dynamic1_Arguments_0;
        if(Dynamic0_Arguments_Count<=1)return b?Dynamic0:Expression.Dynamic(Dynamic0.Binder,Dynamic0.Type,Dynamic1_Arguments_0);
        var Dynamic0_Arguments_1=Dynamic0_Arguments[1];
        var Dynamic1_Arguments_1=this.Traverse(Dynamic0_Arguments_1);
        b&=Dynamic0_Arguments_1==Dynamic1_Arguments_1;
        if(Dynamic0_Arguments_Count<=2)return b?Dynamic0:Expression.Dynamic(Dynamic0.Binder,Dynamic0.Type,Dynamic1_Arguments_0,Dynamic1_Arguments_1);
        var Dynamic0_Arguments_2=Dynamic0_Arguments[2];
        var Dynamic1_Arguments_2=this.Traverse(Dynamic0_Arguments_2);
        b&=Dynamic0_Arguments_2==Dynamic1_Arguments_2;
        if(Dynamic0_Arguments_Count<=3)return b?Dynamic0:Expression.Dynamic(Dynamic0.Binder,Dynamic0.Type,Dynamic1_Arguments_0,Dynamic1_Arguments_1,Dynamic1_Arguments_2);
        var Dynamic0_Arguments_3=Dynamic0_Arguments[3];
        var Dynamic1_Arguments_3=this.Traverse(Dynamic0_Arguments_3);
        b&=Dynamic0_Arguments_3==Dynamic1_Arguments_3;
        return b?Dynamic0:Expression.Dynamic(Dynamic0.Binder,Dynamic0.Type,Dynamic1_Arguments_0,Dynamic1_Arguments_1,Dynamic1_Arguments_2,Dynamic1_Arguments_3);
    }
    /// <summary>
    /// goto Label;
    /// </summary>
    /// <param name="Goto0"></param>
    /// <returns></returns>
    protected virtual Expression Goto(GotoExpression Goto0) {
        var Goto0_Value=Goto0.Value;
        var Goto1_Value=this.TraverseNullable(Goto0_Value);
        if(Goto0_Value==Goto1_Value)return Goto0;
        return Expression.MakeGoto(Goto0.Kind,Goto0.Target,Goto1_Value,Goto0.Type);
    }
    /// <summary>
    /// VBのIndex付きプロパティ a[b,c]
    /// </summary>
    /// <param name="Index0"></param>
    /// <returns></returns>
    protected virtual Expression Index(IndexExpression Index0) {
        var Index0_Object=Index0.Object;
        var Index1_Object=this.Traverse(Index0_Object);
        var Index0_Arguments=Index0.Arguments;
        var Index1_Arguments=this.TraverseExpressions(Index0_Arguments);
        if(Index1_Object==Index0_Object)
            if(ReferenceEquals(Index0_Arguments,Index1_Arguments)) 
                return Index0;
        return Expression.MakeIndex(Index1_Object,Index0.Indexer,Index1_Arguments);
    }
    /// <summary>
    /// Delegate.Invoke(),普通はExpression→Argumentsの評価だが先行評価の場合Arguments→Expressionにする
    /// </summary>
    /// <param name="Invocation0"></param>
    /// <returns></returns>
    protected virtual Expression Invoke(InvocationExpression Invocation0) {
        var Invocation0_Arguments=Invocation0.Arguments;
        var Invocation1_Arguments=this.TraverseExpressions(Invocation0_Arguments);
        var Invocation0_Expression=Invocation0.Expression;
        var Invocation1_Expression=this.Traverse(Invocation0_Expression);
        if(Invocation0_Expression==Invocation1_Expression)
            if(ReferenceEquals(Invocation0_Arguments,Invocation1_Arguments)) 
                return Invocation0;
        return Expression.Invoke(Invocation1_Expression,Invocation1_Arguments);
    }
    /// <summary>
    /// Label1:
    /// </summary>
    /// <param name="Label0"></param>
    /// <returns></returns>
    protected virtual Expression Label(LabelExpression Label0) {
        var Label0_DefaultValue=Label0.DefaultValue;
        var Label1_DefaultValue=this.TraverseNullable(Label0_DefaultValue);
        if(Label0_DefaultValue==Label1_DefaultValue) return Label0;
        return Expression.Label(Label0.Target,Label1_DefaultValue);
    }
    /// <summary>
    /// ()=>3
    /// </summary>
    /// <param name="Lambda0"></param>
    /// <returns></returns>
    protected virtual Expression Lambda(LambdaExpression Lambda0) {
        var Lambda0_Body=Lambda0.Body;
        var Lambda1_Body=this.Traverse(Lambda0_Body);
        if(Lambda0_Body==Lambda1_Body)return Lambda0;
        return Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0.Parameters);
    }
    /// <summary>
    /// new List&lt;Int32>{1,2,3}
    /// </summary>
    /// <param name="ListInit0"></param>
    /// <returns></returns>
    protected virtual Expression ListInit(ListInitExpression ListInit0) {
        var ListInit0_Initializers=ListInit0.Initializers;
        var ListInit1_NewExpression=(NewExpression)this.New(ListInit0.NewExpression);
        var ListInit1_Initializers_Count=ListInit0_Initializers.Count;
        var ListInit1_Initializers=new ElementInit[ListInit1_Initializers_Count];
        var 変化したか=false;
        for(var a=0; a < ListInit1_Initializers_Count; a++) {
            var ListInit0_Initialize=ListInit0_Initializers[a];
            var ListInit0_Initialize_Arguments=ListInit0_Initialize.Arguments;
            var ListInit1_Initialize_Arguments=this.TraverseExpressions(ListInit0_Initialize_Arguments);
            if(ReferenceEquals(ListInit0_Initialize_Arguments,ListInit1_Initialize_Arguments)) {
                ListInit1_Initializers[a]=ListInit0_Initialize;
            } else {
                変化したか=true;
                ListInit1_Initializers[a]=Expression.ElementInit(
                    ListInit0_Initialize.AddMethod,
                    ListInit1_Initialize_Arguments
                );
            }
        }
        return 変化したか?Expression.ListInit(
            ListInit1_NewExpression,
            ListInit1_Initializers
        ): Expression.ListInit(
            ListInit1_NewExpression,
            ListInit0_Initializers
        );
    }
    /// <summary>
    /// while(true){}
    /// </summary>
    /// <param name="Loop0"></param>
    /// <returns></returns>
    protected virtual Expression Loop(LoopExpression Loop0) {
        var Loop0_Body=Loop0.Body;
        var Loop1_Body=this.Traverse(Loop0_Body);
        if(Loop0_Body==Loop1_Body)
            return Loop0;
        return Expression.Loop(
            Loop1_Body,
            Loop0.BreakLabel,
            Loop0.ContinueLabel
        );
    }
    /// <summary>
    /// a.b
    /// </summary>
    /// <param name="Member0"></param>
    /// <returns></returns>
    protected virtual Expression MemberAccess(MemberExpression Member0) {
        var Member0_Expression=Member0.Expression;
        var Member1_Expression=this.TraverseNullable(Member0_Expression);
        if(Member0_Expression==Member1_Expression)
            return Member0;
        return Expression.MakeMemberAccess(
            Member1_Expression,
            Member0.Member
        );
    }
    /// <summary>
    /// new C{
    ///     Assignment=2,
    ///     MemberBinding={a=3,b=4}.
    ///     ListBinding={1,2,3}
    /// }
    /// </summary>
    /// <param name="Bindings0"></param>
    /// <returns></returns>
    protected virtual IList<MemberBinding> Bindings(ReadOnlyCollection<MemberBinding> Bindings0) {
        var Bindings0_Count=Bindings0.Count;
        var Bindings1=new MemberBinding[Bindings0_Count];
        var 変化したか=false;
        for(var a=0; a < Bindings0_Count; a++) {
            var Binding0=Bindings0[a];
            switch (Binding0.BindingType) {
                case MemberBindingType.Assignment: {
                    var Binding0_Expression=((MemberAssignment)Binding0).Expression;
                    var Binding1_Expression=this.Traverse(Binding0_Expression);
                    if(Binding0_Expression==Binding1_Expression) {
                        Bindings1[a]=Binding0;
                    } else {
                        Bindings1[a]=Expression.Bind(
                            Binding0.Member,
                            Binding1_Expression
                        );
                        変化したか=true;
                    }
                    break;
                }
                case MemberBindingType.MemberBinding: {
                    var Binding0_Bindings=((MemberMemberBinding)Binding0).Bindings;
                    var Binding1_Bindings=this.Bindings(Binding0_Bindings);
                    if(ReferenceEquals(Binding0_Bindings,Binding1_Bindings)) {
                        Bindings1[a]=Binding0;
                    } else {
                        Bindings1[a]=Expression.MemberBind(
                            Binding0.Member,
                            Binding1_Bindings
                        );
                        変化したか=true;
                    }
                    break;
                }
                default: {
                    Debug.Assert(Binding0.BindingType==MemberBindingType.ListBinding);
                    var MemberListBinding0=(MemberListBinding)Binding0;
                    var MemberListBinding0_Initializers=MemberListBinding0.Initializers;
                    var MemberListBinding0_Initializers_Count=MemberListBinding0_Initializers.Count;
                    var MemberListBinding1_Initializers=new ElementInit[MemberListBinding0_Initializers_Count];
                    var 変化したか1=false;
                    for(var b=0; b < MemberListBinding0_Initializers_Count; b++) {
                        var MemberListBinding0_Initializer=MemberListBinding0_Initializers[b];
                        var MemberListBinding0_Initializer_Arguments=MemberListBinding0_Initializer.Arguments;
                        var MemberListBinding1_Initializer_Arguments=this.TraverseExpressions(MemberListBinding0_Initializer_Arguments);
                        if(ReferenceEquals(MemberListBinding0_Initializer_Arguments,MemberListBinding1_Initializer_Arguments)) {
                            MemberListBinding1_Initializers[b]=MemberListBinding0_Initializer;
                        } else {
                            MemberListBinding1_Initializers[b]=Expression.ElementInit(
                                MemberListBinding0_Initializer.AddMethod,
                                MemberListBinding1_Initializer_Arguments
                            );
                            変化したか1=true;
                        }
                    }
                    if(変化したか1){
                        Bindings1[a]=Expression.ListBind(
                            Binding0.Member,
                            MemberListBinding1_Initializers
                        );
                        変化したか=true;
                    } else{
                        Bindings1[a]=MemberListBinding0;
                    }
                    break;
                }
            }
        }
        return 変化したか?Bindings1 : Bindings0;
    }
    /// <summary>
    /// new Point{X=1,Y=2}
    /// </summary>
    /// <param name="MemberInit0"></param>
    /// <returns></returns>
    protected virtual Expression MemberInit(MemberInitExpression MemberInit0) {
        var MemberInit0_NewExpression=MemberInit0.NewExpression;
        var MemberInit0_Bindings=MemberInit0.Bindings;
        var MemberInit1_NewExpression= (NewExpression)this.New(MemberInit0_NewExpression);
        var MemberInit1_Bindings=this.Bindings(MemberInit0_Bindings);
        if(MemberInit0_NewExpression==MemberInit1_NewExpression)
            if(ReferenceEquals(MemberInit0_Bindings,MemberInit1_Bindings))
                return MemberInit0;
        return Expression.MemberInit(
            MemberInit1_NewExpression,
            MemberInit1_Bindings
        );
    }
    /// <summary>
    /// a(b)
    /// </summary>
    /// <param name="MethodCall0"></param>
    /// <returns></returns>
    protected virtual Expression Call(MethodCallExpression MethodCall0) {
        var MethodCall0_Object = MethodCall0.Object;
        var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall0_Arguments_Count = MethodCall0_Arguments.Count;
        if(MethodCall0_Object is null) {
            if(MethodCall0_Arguments_Count>=6){
                var MethodCall1_Arguments = this.TraverseExpressions(MethodCall0_Arguments);
                return ReferenceEquals(MethodCall0_Arguments,MethodCall1_Arguments)?MethodCall0:Expression.Call(MethodCall0.Method,MethodCall1_Arguments);
            }
            if(MethodCall0_Arguments_Count==0)return MethodCall0;
            var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments_0);
            var b = MethodCall0_Arguments_0==MethodCall1_Arguments_0;
            if(MethodCall0_Arguments_Count<=1)return b?MethodCall0:Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0);
            var MethodCall0_Arguments_1 = MethodCall0_Arguments[1];
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments_1);
            b&=MethodCall0_Arguments_1==MethodCall1_Arguments_1;
            if(MethodCall0_Arguments_Count<=2)return b?MethodCall0:Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
            var MethodCall0_Arguments_2 = MethodCall0_Arguments[2];
            var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments_2);
            b&=MethodCall0_Arguments_2==MethodCall1_Arguments_2;
            if(MethodCall0_Arguments_Count<=3)return b?MethodCall0:Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2);
            var MethodCall0_Arguments_3 = MethodCall0_Arguments[3];
            var MethodCall1_Arguments_3 = this.Traverse(MethodCall0_Arguments_3);
            b&=MethodCall0_Arguments_3==MethodCall1_Arguments_3;
            if(MethodCall0_Arguments_Count<=4)return b?MethodCall0:Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2,MethodCall1_Arguments_3);
            var MethodCall0_Arguments_4 = MethodCall0_Arguments[4];
            var MethodCall1_Arguments_4 = this.Traverse(MethodCall0_Arguments_4);
            b&=MethodCall0_Arguments_4==MethodCall1_Arguments_4;
            if(b) return MethodCall0;
            return Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2,MethodCall1_Arguments_3,MethodCall1_Arguments_4);
            //return b?MethodCall0:Expression.Call(MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2,MethodCall1_Arguments_3,MethodCall1_Arguments_4);
        } else {
            var MethodCall1_Object = this.Traverse(MethodCall0_Object);
            var b = MethodCall0_Object==MethodCall1_Object;
            if(MethodCall0_Arguments_Count>=4){
                var MethodCall1_Arguments = this.TraverseExpressions(MethodCall0_Arguments);
                if(b)
                    if(ReferenceEquals(MethodCall0_Arguments,MethodCall1_Arguments))
                        return MethodCall0;
                return Expression.Call(MethodCall1_Object,MethodCall0.Method,MethodCall1_Arguments);
                //return b&&ReferenceEquals(MethodCall0_Arguments,MethodCall1_Arguments)?MethodCall0:Expression.Call(MethodCall1_Object,MethodCall0.Method,MethodCall1_Arguments);
            }
            if(MethodCall0_Arguments_Count==0)return b?MethodCall0:Expression.Call(MethodCall1_Object,MethodCall0.Method);
            var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments_0);
            b&=MethodCall0_Arguments_0==MethodCall1_Arguments_0;
            if(MethodCall0_Arguments_Count<=1)return b?MethodCall0:Expression.Call(MethodCall1_Object,MethodCall0.Method,this.作業配列.Expressions設定(MethodCall1_Arguments_0));
            var MethodCall0_Arguments_1 = MethodCall0_Arguments[1];
            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments_1);
            b&=MethodCall0_Arguments_1==MethodCall1_Arguments_1;
            if(MethodCall0_Arguments_Count<=2)return b?MethodCall0:Expression.Call(MethodCall1_Object,MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
            var MethodCall0_Arguments_2 = MethodCall0_Arguments[2];
            var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments_2);
            b&=MethodCall0_Arguments_2==MethodCall1_Arguments_2;
            return b?MethodCall0:Expression.Call(MethodCall1_Object,MethodCall0.Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2);
        }
    }
    /// <summary>
    /// new Int32[3]
    /// </summary>
    /// <param name="NewArray0"></param>
    /// <returns></returns>
    protected virtual Expression NewArrayBounds(NewArrayExpression NewArray0) {
        Debug.Assert(NewArray0.Type.IsArray);
        var NewArray0_Expressions=NewArray0.Expressions;
        var NewArray1_Expressions=this.TraverseExpressions(NewArray0_Expressions);
        if(ReferenceEquals(NewArray1_Expressions,NewArray0_Expressions))
            return NewArray0;
        return Expression.NewArrayBounds(
            // ReSharper disable once AssignNullToNotNullAttribute
            NewArray0.Type.GetElementType(),
            NewArray1_Expressions
        );
    }
    /// <summary>
    /// new Int32[]{1,2,3}
    /// </summary>
    /// <param name="NewArray0"></param>
    /// <returns></returns>
    protected virtual Expression NewArrayInit(NewArrayExpression NewArray0) {
        Debug.Assert(NewArray0.Type.IsArray);
        var NewArray0_Expressions=NewArray0.Expressions;
        var NewArray1_Expressions=this.TraverseExpressions(NewArray0_Expressions);
        if(ReferenceEquals(NewArray1_Expressions,NewArray0_Expressions))
            return NewArray0;
        return Expression.NewArrayInit(
            // ReSharper disable once AssignNullToNotNullAttribute
            NewArray0.Type.GetElementType(),
            NewArray1_Expressions
        );
    }
    /// <summary>
    /// new T(a,b)
    /// </summary>
    /// <param name="New0"></param>
    /// <returns></returns>
    //[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    //[SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
    protected virtual Expression New(NewExpression New0) {
        if(New0.Constructor is null)
            return New0;
        var New0_Arguments=New0.Arguments;
        var New1_Arguments=this.TraverseExpressions(New0_Arguments);
        if(ReferenceEquals(New1_Arguments,New0_Arguments))
            return New0;
        return New0.Members is null
            ? Expression.New(New0.Constructor,New1_Arguments)
            : Expression.New(New0.Constructor,New1_Arguments,New0.Members);//Anonymousの場合
    }
    /// <summary>
    /// a
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <returns></returns>
    protected virtual Expression Parameter(ParameterExpression Parameter0)=> Parameter0;
    protected virtual Expression RuntimeVariables(RuntimeVariablesExpression RuntimeVariables0)=> RuntimeVariables0;
    /// <summary>
    /// switch(a){
    ///     case 0:break;
    ///     case 1:break;
    ///     default:
    /// }
    /// </summary>
    /// <param name="Switch0"></param>
    /// <returns></returns>
    protected virtual Expression Switch(SwitchExpression Switch0) {
        var Switch0_SwitchValue=Switch0.SwitchValue;
        var Switch0_Cases=Switch0.Cases;
        var Switch1_SwitchValue=this.Traverse(Switch0_SwitchValue);
        var Switch0_Cases_Count=Switch0_Cases.Count;
        var Switch1_Cases=new SwitchCase[Switch0_Cases_Count];
        var 変化したか=Switch0_SwitchValue !=Switch1_SwitchValue;
        for(var a=0; a < Switch0_Cases_Count; a++) {
            var Switch0_Case=Switch0_Cases[a];
            var Switch0_Case_TestValues=Switch0_Case.TestValues;
            var Switch0_Case_TestValues_Count=Switch0_Case_TestValues.Count;
            var Switch1_Case_TestValues=new Expression[Switch0_Case_TestValues_Count];
            var 変化したか1=false;
            for(var b=0; b < Switch0_Case_TestValues_Count; b++) {
                //c#上は定数(Constant)しかできないが式木で作った場合はそれにとらわれない
                var Switch0_Case_TestValue=Switch0_Case_TestValues[b];
                var Switch1_Case_TestValue=this.Traverse(Switch0_Case_TestValue);
                Switch1_Case_TestValues[b]=Switch1_Case_TestValue;
                if(Switch0_Case_TestValue !=Switch1_Case_TestValue)
                    変化したか1=true;
            }
            var Switch0_Case_Body=Switch0_Case.Body;
            var Switch1_Case_Body=this.Traverse(Switch0_Case_Body);
            if(Switch0_Case_Body !=Switch1_Case_Body || 変化したか1) {
                Switch1_Cases[a]=Expression.SwitchCase(Switch1_Case_Body,Switch1_Case_TestValues);
                変化したか=true;
            } else
                Switch1_Cases[a]=Switch0_Case;
        }
        var Switch0_DefaultBody=Switch0.DefaultBody;
        var Switch1_DefaultBody=this.Traverse(Switch0_DefaultBody);
        変化したか|=Switch0_DefaultBody!=Switch1_DefaultBody;
        return 変化したか
           ?Expression.Switch(
                Switch0.Type,
                Switch1_SwitchValue,
                Switch1_DefaultBody,
                Switch0.Comparison,
                Switch1_Cases
            )
            : Switch0;
    }
    /// <summary>
    /// try{
    /// }catch{
    /// }
    /// </summary>
    /// <param name="Try0"></param>
    /// <returns></returns>
    protected virtual Expression Try(TryExpression Try0) {
        var Try0_Handlers=Try0.Handlers;
        var Try0_Handlers_Count=Try0_Handlers.Count;
        var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
        var 変化したか=false;
        var Try0_Body=Try0.Body;
        var Try1_Body=this.Traverse(Try0_Body);
        if(Try0_Body !=Try1_Body)
            変化したか=true;
        var Try0_Finally=Try0.Finally;
        var Try1_Finally=this.TraverseNullable(Try0_Finally);
        if(Try0_Finally!=Try1_Finally)
            変化したか=true;
        for(var a=0; a < Try0_Handlers_Count; a++) {
            var Try0_Handler=Try0_Handlers[a];
            var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
            var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
            CatchBlock Try1_Handler;
            if(Try0_Handler.Body!=Try1_Handler_Body||Try0_Handler.Filter!=Try1_Handler_Filter) {
                変化したか=true;
                if(Try0_Handler.Variable is not null) {
                    Try1_Handler=Expression.Catch(
                        Try0_Handler.Variable,
                        Try1_Handler_Body,
                        Try1_Handler_Filter
                    );
                } else {
                    Try1_Handler=Expression.Catch(
                        Try0_Handler.Test,
                        Try1_Handler_Body,
                        Try1_Handler_Filter
                    );
                }
            } else {
                Try1_Handler=Try0_Handler;
            }
            Try1_Handlers[a]=Try1_Handler;
        }
        if(変化したか) {
            return Expression.TryCatchFinally(
                Try1_Body,
                Try1_Finally,
                Try1_Handlers
            );
        } else {
            return Try0;
        }
    }
    /// <summary>
    /// a as b
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression TypeAs(UnaryExpression Unary0) {
        var Unary0_Operand=Unary0.Operand;
        var Unary1_Operand=this.Traverse(Unary0_Operand);
        if(Unary0_Operand==Unary1_Operand)
            return Unary0;
        return Expression.TypeAs(
            Unary1_Operand,
            Unary0.Type
        );
    }
    /// <summary>
    /// a==typeof(Int32)
    /// </summary>
    /// <param name="TypeBinary0"></param>
    /// <returns></returns>
    protected virtual Expression TypeEqual(TypeBinaryExpression TypeBinary0) {
        var TypeBinary0_Expression=TypeBinary0.Expression;
        var TypeBinary1_Expression=this.Traverse(TypeBinary0_Expression);
        if(TypeBinary0_Expression==TypeBinary1_Expression)
            return TypeBinary0;
        return Expression.TypeEqual(
            TypeBinary1_Expression,
            TypeBinary0.TypeOperand
        );
    }
    /// <summary>
    /// a is b
    /// </summary>
    /// <param name="TypeBinary0"></param>
    /// <returns></returns>
    protected virtual Expression TypeIs(TypeBinaryExpression TypeBinary0) {
        var TypeBinary0_Expression=TypeBinary0.Expression;
        var TypeBinary1_Expression=this.Traverse(TypeBinary0_Expression);
        if(TypeBinary0_Expression==TypeBinary1_Expression)
            return TypeBinary0;
        return Expression.TypeIs(
            TypeBinary1_Expression,
            TypeBinary0.TypeOperand
        );
    }

    private Expression MakeUnary(UnaryExpression Unary0) {
        var Unary0_Operand=Unary0.Operand;
        var Unary1_Operand=this.Traverse(Unary0_Operand);
        if(Unary0_Operand==Unary1_Operand)
            return Unary0;
        return Expression.MakeUnary(
            Unary0.NodeType,
            Unary1_Operand,
            Unary0.Type,
            Unary0.Method
        );
    }
    /// <summary>
    /// a.Length
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression ArrayLength(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// (Int32)a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Convert(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// checked((Int32)a)
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression ConvertChecked(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// a-1
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Decrement(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// a+1
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Increment(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// a||b
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression IsFalse(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// a&amp;&amp;b
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression IsTrue(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// -a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Negate(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// checked(-a)
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression NegateChecked(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// !a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Not(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// ~a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression OnesComplement(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// a--
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression PostDecrementAssign(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// a++
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression PostIncrementAssign(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// --a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression PreDecrementAssign(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// ++a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression PreIncrementAssign(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// Expression&lt;Func&lt;Int32>>()=>3
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Quote(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// throw new Exception()
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Throw(UnaryExpression Unary0)=> Unary0.Operand is not null?this.MakeUnary(Unary0):Unary0;
    /// <summary>
    /// +a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression UnaryPlus(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
    /// <summary>
    /// (Int32)(Object)1
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected virtual Expression Unbox(UnaryExpression Unary0)=> this.MakeUnary(Unary0);
}
//2022/04/03 1359