using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Diagnostics;
namespace LinqDB.Optimizers.VoidExpressionTraverser;

/// <summary>
/// Expressionを走査して情報を得る
/// a+=b→a=a+b
/// a=bのaは評価される
/// </summary>
internal class VoidExpressionTraverser {
    protected void TraverseNulllable(Expression? e) {
        if(e is not null)this.Traverse(e);
    }
    /// <summary>
    /// 走査する起点メソッド。
    /// </summary>
    /// <param name="Expression"></param>
    protected virtual void Traverse(Expression Expression) {
        switch(Expression.NodeType){
            case ExpressionType.Add:
                this.Add((BinaryExpression)Expression);
                return;
            case ExpressionType.AddAssign:
                this.AddAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.AddAssignChecked:
                this.AddAssignChecked((BinaryExpression)Expression);
                return;
            case ExpressionType.AddChecked:
                this.AddChecked((BinaryExpression)Expression);
                return;
            case ExpressionType.And:
                this.And((BinaryExpression)Expression);
                return;
            case ExpressionType.AndAssign:
                this.AndAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.AndAlso:
                this.AndAlso((BinaryExpression)Expression);
                return;
            case ExpressionType.ArrayIndex:
                this.ArrayIndex((BinaryExpression)Expression);
                return;
            case ExpressionType.Assign:
                this.Assign((BinaryExpression)Expression);
                return;
            case ExpressionType.Coalesce:
                this.Coalesce((BinaryExpression)Expression);
                return;
            case ExpressionType.Divide:
                this.Divide((BinaryExpression)Expression);
                return;
            case ExpressionType.DivideAssign:
                this.DivideAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.Equal:
                this.Equal((BinaryExpression)Expression);
                return;
            case ExpressionType.ExclusiveOr:
                this.ExclusiveOr((BinaryExpression)Expression);
                return;
            case ExpressionType.ExclusiveOrAssign:
                this.ExclusiveOrAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.GreaterThan:
                this.GreaterThan((BinaryExpression)Expression);
                return;
            case ExpressionType.GreaterThanOrEqual:
                this.GreaterThanOrEqual((BinaryExpression)Expression);
                return;
            case ExpressionType.LeftShift:
                this.LeftShift((BinaryExpression)Expression);
                return;
            case ExpressionType.LeftShiftAssign:
                this.LeftShiftAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.LessThan:
                this.LessThan((BinaryExpression)Expression);
                return;
            case ExpressionType.LessThanOrEqual:
                this.LessThanOrEqual((BinaryExpression)Expression);
                return;
            case ExpressionType.Modulo:
                this.Modulo((BinaryExpression)Expression);
                return;
            case ExpressionType.ModuloAssign:
                this.ModuloAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.Multiply:
                this.Multiply((BinaryExpression)Expression);
                return;
            case ExpressionType.MultiplyAssign:
                this.MultiplyAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.MultiplyAssignChecked:
                this.MultiplyAssignChecked((BinaryExpression)Expression);
                return;
            case ExpressionType.MultiplyChecked:
                this.MultiplyChecked((BinaryExpression)Expression);
                return;
            case ExpressionType.NotEqual:
                this.NotEqual((BinaryExpression)Expression);
                return;
            case ExpressionType.Or:
                this.Or((BinaryExpression)Expression);
                return;
            case ExpressionType.OrAssign:
                this.OrAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.OrElse:
                this.OrElse((BinaryExpression)Expression);
                return;
            case ExpressionType.Power:
                this.Power((BinaryExpression)Expression);
                return;
            case ExpressionType.PowerAssign:
                this.PowerAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.RightShift:
                this.RightShift((BinaryExpression)Expression);
                return;
            case ExpressionType.RightShiftAssign:
                this.RightShiftAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.Subtract:
                this.Subtract((BinaryExpression)Expression);
                return;
            case ExpressionType.SubtractAssign:
                this.SubtractAssign((BinaryExpression)Expression);
                return;
            case ExpressionType.SubtractAssignChecked:
                this.SubtractAssignChecked((BinaryExpression)Expression);
                return;
            case ExpressionType.SubtractChecked:
                this.SubtractChecked((BinaryExpression)Expression);
                return;
            case ExpressionType.Block:
                this.Block((BlockExpression)Expression);
                return;
            case ExpressionType.Conditional:
                this.Conditional((ConditionalExpression)Expression);
                return;
            case ExpressionType.Constant:
                this.Constant((ConstantExpression)Expression);
                return;
            case ExpressionType.DebugInfo:
                this.DebugInfo((DebugInfoExpression)Expression);
                return;
            case ExpressionType.Default:
                this.Default((DefaultExpression)Expression);
                return;
            case ExpressionType.Dynamic:
                this.Dynamic((DynamicExpression)Expression);
                return;
            case ExpressionType.Goto:
                this.Goto((GotoExpression)Expression);
                return;
            case ExpressionType.Index:
                this.Index((IndexExpression)Expression);
                return;
            case ExpressionType.Invoke:
                this.Invoke((InvocationExpression)Expression);
                return;
            case ExpressionType.Label:
                this.Label((LabelExpression)Expression);
                return;
            case ExpressionType.Lambda:
                this.Lambda((LambdaExpression)Expression);
                return;
            case ExpressionType.ListInit:
                this.ListInit((ListInitExpression)Expression);
                return;
            case ExpressionType.Loop:
                this.Loop((LoopExpression)Expression);
                return;
            case ExpressionType.MemberAccess:
                this.MemberAccess((MemberExpression)Expression);
                return;
            case ExpressionType.MemberInit:
                this.MemberInit((MemberInitExpression)Expression);
                return;
            case ExpressionType.Call:
                this.Call((MethodCallExpression)Expression);
                return;
            case ExpressionType.NewArrayBounds:
                this.NewArrayBound((NewArrayExpression)Expression);
                return;
            case ExpressionType.NewArrayInit:
                this.NewArrayInit((NewArrayExpression)Expression);
                return;
            case ExpressionType.New:
                this.New((NewExpression)Expression);
                return;
            case ExpressionType.Parameter:
                this.Parameter((ParameterExpression)Expression);
                return;
            case ExpressionType.RuntimeVariables:
                this.RuntimeVariables((RuntimeVariablesExpression)Expression);
                return;
            case ExpressionType.Switch:
                this.Switch((SwitchExpression)Expression);
                return;
            case ExpressionType.Try:
                this.Try((TryExpression)Expression);
                return;
            case ExpressionType.TypeEqual:
                this.TypeEqual((TypeBinaryExpression)Expression);
                return;
            case ExpressionType.TypeIs:
                this.TypeIs((TypeBinaryExpression)Expression);
                return;
            case ExpressionType.ArrayLength:
                this.ArrayLength((UnaryExpression)Expression);
                return;
            case ExpressionType.Convert:
                this.Convert((UnaryExpression)Expression);
                return;
            case ExpressionType.ConvertChecked:
                this.ConvertChecked((UnaryExpression)Expression);
                return;
            case ExpressionType.Decrement:
                this.Decrement((UnaryExpression)Expression);
                return;
            case ExpressionType.Increment:
                this.Increment((UnaryExpression)Expression);
                return;
            case ExpressionType.IsFalse:
                this.IsFalse((UnaryExpression)Expression);
                return;
            case ExpressionType.IsTrue:
                this.IsTrue((UnaryExpression)Expression);
                return;
            case ExpressionType.Negate:
                this.Negate((UnaryExpression)Expression);
                return;
            case ExpressionType.NegateChecked:
                this.NegateChecked((UnaryExpression)Expression);
                return;
            case ExpressionType.Not:
                this.Not((UnaryExpression)Expression);
                return;
            case ExpressionType.OnesComplement:
                this.OnesComplement((UnaryExpression)Expression);
                return;
            case ExpressionType.PostDecrementAssign:
                this.PostDecrementAssign((UnaryExpression)Expression);
                return;
            case ExpressionType.PostIncrementAssign:
                this.PostIncrementAssign((UnaryExpression)Expression);
                return;
            case ExpressionType.PreDecrementAssign:
                this.PreDecrementAssign((UnaryExpression)Expression);
                return;
            case ExpressionType.PreIncrementAssign:
                this.PreIncrementAssign((UnaryExpression)Expression);
                return;
            case ExpressionType.Quote:
                this.Quote((UnaryExpression)Expression);
                return;
            case ExpressionType.Throw:
                this.Throw((UnaryExpression)Expression);
                return;
            case ExpressionType.TypeAs:
                this.TypeAs((UnaryExpression)Expression);
                return;
            case ExpressionType.UnaryPlus:
                this.UnaryPlus((UnaryExpression)Expression);
                return;
            case ExpressionType.Unbox:
                this.Unbox((UnaryExpression)Expression);
                return;
            default:
                throw new NotSupportedException($"{Expression.NodeType}はサポートされていない。");
        }
    }
    /// <summary>
    /// ReadOnlyCollection&lt;Expression>の走査。
    /// </summary>
    /// <param name="Expressions"></param>
    protected virtual void TraverseExpressions(ReadOnlyCollection<Expression> Expressions) {
        //foreachを使うとSZArrayHelper.GetEnumerator()内部でnewしてたのでやらない
        var Expressions_Count=Expressions.Count;
        for(var a = 0;a<Expressions_Count;a++)
            this.Traverse(Expressions[a]);
    }
    /// <summary>
    /// BinaryExpressionの走査
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void MakeBinary(BinaryExpression Binary) {
        this.Traverse(Binary.Left);
        this.Traverse(Binary.Right);
        this.TraverseNulllable(Binary.Conversion);
    }
    /// <summary>
    /// 代入式の右辺値だけを走査
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void MakeAssign(BinaryExpression Binary) => this.MakeBinary(Binary);
    /// <summary>
    /// a+b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Add(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// checked(a+b)
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void AddChecked(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a&amp;
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void And(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a&amp;&amp;b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void AndAlso(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a[b]
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void ArrayIndex(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a=b
    /// </summary>
    /// <param name="Assign"></param>
    protected virtual void Assign(BinaryExpression Assign)=> this.MakeAssign(Assign);
    /// <summary>
    /// a??b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Coalesce(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a/b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Divide(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a==b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Equal(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a^b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void ExclusiveOr(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a>b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void GreaterThan(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a>=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void GreaterThanOrEqual(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// ac&lt;b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void LeftShift(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a&lt;b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void LessThan(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a&lt;=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void LessThanOrEqual(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a%b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Modulo(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a*b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Multiply(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// checked(a*b)
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void MultiplyChecked(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a!=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void NotEqual(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a|b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Or(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a|=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void OrAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a||b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void OrElse(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// VisualBasic a^=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void PowerAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// VisualBasic a^b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Power(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a>>b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void RightShift(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a=>>b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void RightShiftAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a-b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void Subtract(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a-=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void SubtractAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// checked(a-=b)
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void SubtractAssignChecked(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// checked(a-b)
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void SubtractChecked(BinaryExpression Binary)=> this.MakeBinary(Binary);
    /// <summary>
    /// a+=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void AddAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// checked(a+=b)
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void AddAssignChecked(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a&amp;=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void AndAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a/=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void DivideAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a^=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void ExclusiveOrAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a&lt;&lt;=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void LeftShiftAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a%=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void ModuloAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// a*=b
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void MultiplyAssign(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// checked(a*=b)
    /// </summary>
    /// <param name="Binary"></param>
    protected virtual void MultiplyAssignChecked(BinaryExpression Binary)=> this.MakeAssign(Binary);
    /// <summary>
    /// {}
    /// </summary>
    /// <param name="Block"></param>
    protected virtual void Block(BlockExpression Block)=> this.TraverseExpressions(Block.Expressions);
    /// <summary>
    /// a?b:c
    /// </summary>
    /// <param name="Conditional"></param>
    protected virtual void Conditional(ConditionalExpression Conditional) {
        this.Traverse(Conditional.Test);
        this.Traverse(Conditional.IfTrue);
        this.Traverse(Conditional.IfFalse);
    }
    /// <summary>
    /// "A",1などのリテラル
    /// </summary>
    /// <param name="Constant"></param>
    protected virtual void Constant(ConstantExpression Constant) {
    }
    /// <summary>
    /// デバッグ情報。ステップに対応するソースコードの先頭と末尾。
    /// </summary>
    /// <param name="DebugInfo"></param>
    protected virtual void DebugInfo(DebugInfoExpression DebugInfo) {
    }
    /// <summary>
    /// default(Type0)
    /// </summary>
    /// <param name="Default"></param>
    protected virtual void Default(DefaultExpression Default) {
    }
    /// <summary>
    /// dynamic a
    /// a.b
    /// </summary>
    /// <param name="Dynamic"></param>
    protected virtual void Dynamic(DynamicExpression Dynamic)=> this.TraverseExpressions(Dynamic.Arguments);
    /// <summary>
    /// goto Label0
    /// </summary>
    /// <param name="Goto"></param>
    protected virtual void Goto(GotoExpression Goto)=>this.TraverseNulllable(Goto.Value);
    /// <summary>
    /// VisualBasic Index付きプロパティ
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    protected virtual void Index(IndexExpression Index) {
        this.Traverse(Index.Object);
        this.TraverseExpressions(Index.Arguments);
    }
    /// <summary>
    /// Delegate.Invoke(),普通はExpression→Argumentsの評価だが先行評価の場合Arguments→Expressionにする
    /// </summary>
    /// <param name="Invocation"></param>
    protected virtual void Invoke(InvocationExpression Invocation) {
        this.TraverseExpressions(Invocation.Arguments);
        this.Traverse(Invocation.Expression);
    }
    /// <summary>
    /// Label0:
    /// </summary>
    /// <param name="Label"></param>
    protected virtual void Label(LabelExpression Label)=> this.TraverseNulllable(Label.DefaultValue);
    /// <summary>
    /// a=>b
    /// </summary>
    /// <param name="Lambda"></param>
    protected virtual void Lambda(LambdaExpression Lambda)=> this.Traverse(Lambda.Body);
    /// <summary>
    /// new List&lt;T>{a,b}
    /// </summary>
    /// <param name="ListInit"></param>
    protected virtual void ListInit(ListInitExpression ListInit) {
        this.New(ListInit.NewExpression);
        foreach(var ListInit_Initializer in ListInit.Initializers) {
            this.TraverseExpressions(ListInit_Initializer.Arguments);
        }
    }
    /// <summary>
    /// while(true)
    /// </summary>
    /// <param name="Loop"></param>
    protected virtual void Loop(LoopExpression Loop)=> this.Traverse(Loop.Body);
    /// <summary>
    /// a.b
    /// </summary>
    /// <param name="Member"></param>
    protected virtual void MemberAccess(MemberExpression Member)=> this.TraverseNulllable(Member.Expression);
    private void Bindings(ReadOnlyCollection<MemberBinding> Bindings) {
        foreach(var Binding in Bindings) {
            switch(Binding.BindingType) {
                case MemberBindingType.Assignment: {
                    this.Traverse(((MemberAssignment)Binding).Expression);
                    break;
                }
                case MemberBindingType.MemberBinding: {
                    this.Bindings(((MemberMemberBinding)Binding).Bindings);
                    break;
                }
                default: {
                    Debug.Assert(Binding.BindingType is MemberBindingType.ListBinding);
                    foreach(var Initializer in ((MemberListBinding)Binding).Initializers) {
                        this.TraverseExpressions(Initializer.Arguments);
                    }
                    break;
                }
            }
        }
    }
    /// <summary>
    /// new Type0{a=1}
    /// </summary>
    /// <param name="MemberInit"></param>
    protected virtual void MemberInit(MemberInitExpression MemberInit) {
        this.New(MemberInit.NewExpression);
        this.Bindings(MemberInit.Bindings);
    }
    /// <summary>
    /// a(b)
    /// </summary>
    /// <param name="MethodCall"></param>
    protected virtual void Call(MethodCallExpression MethodCall) {
        if(MethodCall.Object is not null) {
            this.Traverse(MethodCall.Object);
        }
        this.TraverseExpressions(MethodCall.Arguments);
    }
    /// <summary>
    /// NewArrayの走査
    /// </summary>
    /// <param name="NewArray"></param>
    protected virtual void MakeNewArray(NewArrayExpression NewArray) {
        Debug.Assert(NewArray.Type.IsArray);
        this.TraverseExpressions(NewArray.Expressions);
    }
    /// <summary>
    /// new Type0[a,b]
    /// </summary>
    /// <param name="NewArray"></param>
    protected virtual void NewArrayBound(NewArrayExpression NewArray) => this.MakeNewArray(NewArray);
    /// <summary>
    /// new Type0[]{a,b}
    /// </summary>
    /// <param name="NewArray"></param>
    protected virtual void NewArrayInit(NewArrayExpression NewArray) => this.MakeNewArray(NewArray);
    /// <summary>
    /// new a(b)
    /// </summary>
    /// <param name="New"></param>
    protected virtual void New(NewExpression New) {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if(New.Constructor  is not null)
            this.TraverseExpressions(New.Arguments);
    }
    /// <summary>
    /// a
    /// </summary>
    /// <param name="Parameter"></param>
    protected virtual void Parameter(ParameterExpression Parameter) {
    }
    /// <summary>
    /// 使用方法不明
    /// </summary>
    /// <param name="RuntimeVariables"></param>
    protected virtual void RuntimeVariables(RuntimeVariablesExpression RuntimeVariables) {
    }
    /// <summary>
    /// switch(a){
    ///     case b:
    ///     case c:
    ///     default:
    /// }
    /// </summary>
    /// <param name="Switch"></param>
    protected virtual void Switch(SwitchExpression Switch) {
        this.Traverse(Switch.SwitchValue);
        foreach(var Switch_Case in Switch.Cases) {
            foreach(var Switch_Case_TestValue in Switch_Case.TestValues)
                this.Traverse(Switch_Case_TestValue);
            this.Traverse(Switch_Case.Body);
        }
        this.Traverse(Switch.DefaultBody);
    }
    /// <summary>
    /// try{
    ///     a;
    /// }catch(Type0 b){
    ///     c;
    /// }catch(Type1 d){
    ///     e;
    /// }finall{
    ///     f;
    /// }
    /// </summary>
    /// <param name="Try"></param>
    protected virtual void Try(TryExpression Try) {
        this.Traverse(Try.Body);
        foreach(var Try_Handler in Try.Handlers){
            this.TraverseNulllable(Try_Handler.Filter);
            this.Traverse(Try_Handler.Body);
        }
        this.TraverseNulllable(Try.Finally);
        this.TraverseNulllable(Try.Fault);
    }
    /// <summary>
    /// TypeBinaryExpressionの走査
    /// </summary>
    /// <param name="TypeBinary"></param>
    protected virtual void MakeTypeBinary(TypeBinaryExpression TypeBinary) => this.Traverse(TypeBinary.Expression);
    /// <summary>
    /// 使用方法不明
    /// </summary>
    /// <param name="TypeBinary"></param>
    protected virtual void TypeEqual(TypeBinaryExpression TypeBinary)=> this.MakeTypeBinary(TypeBinary);
    /// <summary>
    /// a is Type0
    /// </summary>
    /// <param name="TypeBinary"></param>
    protected virtual void TypeIs(TypeBinaryExpression TypeBinary)=> this.MakeTypeBinary(TypeBinary);
    /// <summary>
    /// UnaryExpressionの走査
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void MakeUnary(UnaryExpression Unary) => this.Traverse(Unary.Operand);
    /// <summary>
    /// a.Length
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void ArrayLength(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// (Type0)a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Convert(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// checked((Type0)a)
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void ConvertChecked(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// operator ++
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Decrement(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// operator --
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Increment(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// operator true
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void IsTrue(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// operator false
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void IsFalse(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// -a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Negate(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// checked(-a)
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void NegateChecked(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// !a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Not(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// (Int32)a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Unbox(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// ~a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void OnesComplement(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// ++a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void PreIncrementAssign(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// --a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void PreDecrementAssign(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// a++
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void PostIncrementAssign(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// a--
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void PostDecrementAssign(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// a=>b+cを式木として扱う。
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Quote(UnaryExpression Unary)=> this.MakeUnary(Unary);
    /// <summary>
    /// throw a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void Throw(UnaryExpression Unary){
        //if(Unary.Operand is not null) 
        this.MakeUnary(Unary);
    }
    /// <summary>
    /// a as Type0
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void TypeAs(UnaryExpression Unary) => this.MakeUnary(Unary);
    /// <summary>
    /// +a
    /// </summary>
    /// <param name="Unary"></param>
    protected virtual void UnaryPlus(UnaryExpression Unary)=> this.MakeUnary(Unary);
}

//825 2022/04/26