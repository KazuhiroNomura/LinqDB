using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestLinqDB.Optimizers;
internal sealed class DebugViewWriter:ExpressionVisitor{
    [Flags]
    private enum Flow{
        None=0,
        Space=1,
        NewLine=2,
        Break=0x8000
    }

    private readonly TextWriter _out;

    private int _column;

    private int _depth;

    private Flow _flow;

    private Queue<LambdaExpression> _lambdas;

    private Dictionary<LambdaExpression,int> _lambdaIds;

    private Dictionary<ParameterExpression,int> _paramIds;

    private Dictionary<LabelTarget,int> _labelIds;

    private int Depth=> this._depth;

    private DebugViewWriter(TextWriter file){
        this._out=file;
    }

    private void Indent(){
        this._depth+=4;
    }

    private void Dedent(){
        this._depth-=4;
    }

    private void NewLine(){
        this._flow=Flow.NewLine;
    }

    private static int GetId<T>(T e,ref Dictionary<T,int> ids){
        if(ids==null){
            ids=new Dictionary<T,int>();
            ids.Add(e,1);
            return 1;
        }
        if(!ids.TryGetValue(e,out var value)){
            value=ids.Count+1;
            ids.Add(e,value);
        }
        return value;
    }

    private int GetLambdaId(LambdaExpression le){
        return GetId(le,ref this._lambdaIds);
    }

    private int GetParamId(ParameterExpression p){
        return GetId(p,ref this._paramIds);
    }

    private int GetLabelTargetId(LabelTarget target){
        return GetId(target,ref this._labelIds);
    }

    internal static void WriteTo(Expression node,TextWriter writer){
        new DebugViewWriter(writer).WriteTo(node);
    }

    private void WriteTo(Expression node){
        if(node is LambdaExpression lambda){
            this.WriteLambda(lambda);
        } else{
            this.Visit(node);
        }
        while(this._lambdas!=null&&this._lambdas.Count>0){
            this.WriteLine();
            this.WriteLine();
            this.WriteLambda(this._lambdas.Dequeue());
        }
    }

    private void Out(string s){
        this.Out(Flow.None,s,Flow.None);
    }

    private void Out(Flow before,string s){
        this.Out(before,s,Flow.None);
    }

    private void Out(string s,Flow after){
        this.Out(Flow.None,s,after);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Out(Flow before,string s,Flow after){
        switch(this.GetFlow(before)){
            case Flow.Space:
                this.Write(" ");
                break;
            case Flow.NewLine:
                this.WriteLine();
                this.Write(new string(' ',this.Depth));
                break;
        }
        this.Write(s);
        this._flow=after;
    }

    private void WriteLine(){
        this._out.WriteLine();
        this._column=0;
    }

    private void Write(string s){
        this._out.Write(s);
        this._column+=s.Length;
    }

    private Flow GetFlow(Flow flow){
        var val= this.CheckBreak(this._flow);
        flow=this.CheckBreak(flow);
        return(Flow)Math.Max((int)val,(int)flow);
    }

    private Flow CheckBreak(Flow flow){
        if((flow&Flow.Break)!=0){
            flow=((this._column<=120+this.Depth)?(flow&~Flow.Break):Flow.NewLine);
        }
        return flow;
    }

    private void VisitExpressions<T>(char open,IReadOnlyList<T> expressions) where T:Expression{
        this.VisitExpressions(open,',',expressions);
    }

    private void VisitExpressions<T>(char open,char separator,IReadOnlyList<T> expressions) where T:Expression{
        this.VisitExpressions(open,separator,expressions,delegate(T e){ this.Visit(e);});
    }

    private void VisitDeclarations(IReadOnlyList<ParameterExpression> expressions){
        this.VisitExpressions('(',',',expressions,delegate(ParameterExpression variable){
            this.Out(variable.Type.ToString());
            if(variable.IsByRef){
                this.Out("&");
            }
            this.Out(" ");
            this.VisitParameter(variable);
        });
    }

    private void VisitExpressions<T>(char open,char separator,IReadOnlyList<T> expressions,Action<T> visit){
        this.Out(open.ToString());
        if(expressions!=null){
            this.Indent();
            var flag=true;
            foreach(var expression in expressions){
                if(flag){
                    if(open=='{'||expressions.Count>1){
                        this.NewLine();
                    }
                    flag=false;
                } else{
                    this.Out(separator.ToString(),Flow.NewLine);
                }
                visit(expression);
            }
            this.Dedent();
        }
        var c=open switch{
            '('=>')',
            '{'=>'}',
            '['=>']',
            _=>throw new NotSupportedException()
        };
        if(open=='{'){
            this.NewLine();
        }
        this.Out(c.ToString(),Flow.Break);
    }

    protected override Expression VisitBinary(BinaryExpression node){
        if(node.NodeType==ExpressionType.ArrayIndex){
            this.ParenthesizedVisit(node,node.Left);
            this.Out("[");
            this.Visit(node.Right);
            this.Out("]");
        } else{
            var flag=NeedsParentheses(node,node.Left);
            var flag2=NeedsParentheses(node,node.Right);
            var before=Flow.Space;
            string s;
            switch(node.NodeType){
                case ExpressionType.Assign:
                    s="=";
                    break;
                case ExpressionType.Equal:
                    s="==";
                    break;
                case ExpressionType.NotEqual:
                    s="!=";
                    break;
                case ExpressionType.AndAlso:
                    s="&&";
                    before=Flow.Space|Flow.Break;
                    break;
                case ExpressionType.OrElse:
                    s="||";
                    before=Flow.Space|Flow.Break;
                    break;
                case ExpressionType.GreaterThan:
                    s=">";
                    break;
                case ExpressionType.LessThan:
                    s="<";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    s=">=";
                    break;
                case ExpressionType.LessThanOrEqual:
                    s="<=";
                    break;
                case ExpressionType.Add:
                    s="+";
                    break;
                case ExpressionType.AddAssign:
                    s="+=";
                    break;
                case ExpressionType.AddAssignChecked:
                    s="#+=";
                    break;
                case ExpressionType.AddChecked:
                    s="#+";
                    break;
                case ExpressionType.Subtract:
                    s="-";
                    break;
                case ExpressionType.SubtractAssign:
                    s="-=";
                    break;
                case ExpressionType.SubtractAssignChecked:
                    s="#-=";
                    break;
                case ExpressionType.SubtractChecked:
                    s="#-";
                    break;
                case ExpressionType.Divide:
                    s="/";
                    break;
                case ExpressionType.DivideAssign:
                    s="/=";
                    break;
                case ExpressionType.Modulo:
                    s="%";
                    break;
                case ExpressionType.ModuloAssign:
                    s="%=";
                    break;
                case ExpressionType.Multiply:
                    s="*";
                    break;
                case ExpressionType.MultiplyAssign:
                    s="*=";
                    break;
                case ExpressionType.MultiplyAssignChecked:
                    s="#*=";
                    break;
                case ExpressionType.MultiplyChecked:
                    s="#*";
                    break;
                case ExpressionType.LeftShift:
                    s="<<";
                    break;
                case ExpressionType.LeftShiftAssign:
                    s="<<=";
                    break;
                case ExpressionType.RightShift:
                    s=">>";
                    break;
                case ExpressionType.RightShiftAssign:
                    s=">>=";
                    break;
                case ExpressionType.And:
                    s="&";
                    break;
                case ExpressionType.AndAssign:
                    s="&=";
                    break;
                case ExpressionType.Or:
                    s="|";
                    break;
                case ExpressionType.OrAssign:
                    s="|=";
                    break;
                case ExpressionType.ExclusiveOr:
                    s="^";
                    break;
                case ExpressionType.ExclusiveOrAssign:
                    s="^=";
                    break;
                case ExpressionType.Power:
                    s="**";
                    break;
                case ExpressionType.PowerAssign:
                    s="**=";
                    break;
                case ExpressionType.Coalesce:
                    s="??";
                    break;
                default:
                    throw new InvalidOperationException();
            }
            if(flag){
                this.Out("(",Flow.None);
            }
            this.Visit(node.Left);
            if(flag){
                this.Out(Flow.None,")",Flow.Break);
            }
            this.Out(before,s,Flow.Space|Flow.Break);
            if(flag2){
                this.Out("(",Flow.None);
            }
            this.Visit(node.Right);
            if(flag2){
                this.Out(Flow.None,")",Flow.Break);
            }
        }
        return node;
    }

    protected override Expression VisitParameter(ParameterExpression node){
        this.Out("$");
        if(string.IsNullOrEmpty(node.Name)){
            this.Out("var"+this.GetParamId(node));
        } else{
            this.Out(GetDisplayName(node.Name));
        }
        return node;
    }

    protected override Expression VisitLambda<T>(Expression<T> node){
        this.Out($".Lambda {this.GetLambdaName(node)}<{node.Type}>");
        if(this._lambdas==null){
            this._lambdas=new Queue<LambdaExpression>();
        }
        if(!this._lambdas.Contains(node)){
            this._lambdas.Enqueue(node);
        }
        return node;
    }

    private static bool IsSimpleExpression(Expression node){
        if(node is BinaryExpression binaryExpression){
            if(!(binaryExpression.Left is BinaryExpression)){
                return!(binaryExpression.Right is BinaryExpression);
            }
            return false;
        }
        return false;
    }

    protected override Expression VisitConditional(ConditionalExpression node){
        if(IsSimpleExpression(node.Test)){
            this.Out(".If (");
            this.Visit(node.Test);
            this.Out(") {",Flow.NewLine);
        } else{
            this.Out(".If (",Flow.NewLine);
            this.Indent();
            this.Visit(node.Test);
            this.Dedent();
            this.Out(Flow.NewLine,") {",Flow.NewLine);
        }
        this.Indent();
        this.Visit(node.IfTrue);
        this.Dedent();
        this.Out(Flow.NewLine,"} .Else {",Flow.NewLine);
        this.Indent();
        this.Visit(node.IfFalse);
        this.Dedent();
        this.Out(Flow.NewLine,"}");
        return node;
    }

    protected override Expression VisitConstant(ConstantExpression node){
        var value=node.Value;
        if(value==null){
            this.Out("null");
        } else if(value is string&&node.Type==typeof(string)){
            this.Out($"\"{value}\"");
        } else if(value is char&&node.Type==typeof(char)){
            this.Out($"'{value}'");
        } else if((value is int&&node.Type==typeof(int))||(value is bool&&node.Type==typeof(bool))){
            this.Out(value.ToString());
        } else{
            var constantValueSuffix=GetConstantValueSuffix(node.Type);
            if(constantValueSuffix!=null){
                this.Out(value.ToString());
                this.Out(constantValueSuffix);
            } else{
                this.Out($".Constant<{node.Type}>({value})");
            }
        }
        return node;
    }

    private static string GetConstantValueSuffix(Type type){
        if(type==typeof(uint)){
            return"U";
        }
        if(type==typeof(long)){
            return"L";
        }
        if(type==typeof(ulong)){
            return"UL";
        }
        if(type==typeof(double)){
            return"D";
        }
        if(type==typeof(float)){
            return"F";
        }
        if(type==typeof(decimal)){
            return"M";
        }
        return null;
    }

    protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node){
        this.Out(".RuntimeVariables");
        this.VisitExpressions('(',node.Variables);
        return node;
    }

    private void OutMember(Expression node,Expression instance,MemberInfo member){
        if(instance!=null){
            this.ParenthesizedVisit(node,instance);
            this.Out("."+member.Name);
        } else{
            this.Out(member.DeclaringType.ToString()+"."+member.Name);
        }
    }

    protected override Expression VisitMember(MemberExpression node){
        this.OutMember(node,node.Expression,node.Member);
        return node;
    }

    protected override Expression VisitInvocation(InvocationExpression node){
        this.Out(".Invoke ");
        this.ParenthesizedVisit(node,node.Expression);
        this.VisitExpressions('(',node.Arguments);
        return node;
    }

    private static bool NeedsParentheses(Expression parent,Expression child){
        if(child==null){
            return false;
        }
        switch(parent.NodeType){
            case ExpressionType.Decrement:
            case ExpressionType.Increment:
            case ExpressionType.Unbox:
            case ExpressionType.IsTrue:
            case ExpressionType.IsFalse:
                return true;
            default:{
                var operatorPrecedence=GetOperatorPrecedence(child);
                var operatorPrecedence2=GetOperatorPrecedence(parent);
                if(operatorPrecedence==operatorPrecedence2){
                    switch(parent.NodeType){
                        case ExpressionType.And:
                        case ExpressionType.AndAlso:
                        case ExpressionType.ExclusiveOr:
                        case ExpressionType.Or:
                        case ExpressionType.OrElse:
                            return false;
                        case ExpressionType.Add:
                        case ExpressionType.AddChecked:
                        case ExpressionType.Multiply:
                        case ExpressionType.MultiplyChecked:
                            return false;
                        case ExpressionType.Divide:
                        case ExpressionType.Modulo:
                        case ExpressionType.Subtract:
                        case ExpressionType.SubtractChecked:{
                            var binaryExpression=parent as BinaryExpression;
                            return child==binaryExpression.Right;
                        }
                        default:
                            return true;
                    }
                }
                if(child!=null&&child.NodeType==ExpressionType.Constant&&(parent.NodeType==ExpressionType.Negate||parent.NodeType==ExpressionType.NegateChecked)){
                    return true;
                }
                return operatorPrecedence<operatorPrecedence2;
            }
        }
    }

    private static int GetOperatorPrecedence(Expression node){
        switch(node.NodeType){
            case ExpressionType.Coalesce:
            case ExpressionType.Assign:
            case ExpressionType.AddAssign:
            case ExpressionType.AndAssign:
            case ExpressionType.DivideAssign:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.LeftShiftAssign:
            case ExpressionType.ModuloAssign:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.OrAssign:
            case ExpressionType.PowerAssign:
            case ExpressionType.RightShiftAssign:
            case ExpressionType.SubtractAssign:
            case ExpressionType.AddAssignChecked:
            case ExpressionType.MultiplyAssignChecked:
            case ExpressionType.SubtractAssignChecked:
                return 1;
            case ExpressionType.OrElse:
                return 2;
            case ExpressionType.AndAlso:
                return 3;
            case ExpressionType.Or:
                return 4;
            case ExpressionType.ExclusiveOr:
                return 5;
            case ExpressionType.And:
                return 6;
            case ExpressionType.Equal:
            case ExpressionType.NotEqual:
                return 7;
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.TypeAs:
            case ExpressionType.TypeIs:
            case ExpressionType.TypeEqual:
                return 8;
            case ExpressionType.LeftShift:
            case ExpressionType.RightShift:
                return 9;
            case ExpressionType.Add:
            case ExpressionType.AddChecked:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractChecked:
                return 10;
            case ExpressionType.Divide:
            case ExpressionType.Modulo:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyChecked:
                return 11;
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
            case ExpressionType.Negate:
            case ExpressionType.UnaryPlus:
            case ExpressionType.NegateChecked:
            case ExpressionType.Not:
            case ExpressionType.Decrement:
            case ExpressionType.Increment:
            case ExpressionType.Throw:
            case ExpressionType.Unbox:
            case ExpressionType.PreIncrementAssign:
            case ExpressionType.PreDecrementAssign:
            case ExpressionType.OnesComplement:
            case ExpressionType.IsTrue:
            case ExpressionType.IsFalse:
                return 12;
            case ExpressionType.Power:
                return 13;
            default:
                return 14;
            case ExpressionType.Constant:
            case ExpressionType.Parameter:
                return 15;
        }
    }

    private void ParenthesizedVisit(Expression parent,Expression nodeToVisit){
        if(NeedsParentheses(parent,nodeToVisit)){
            this.Out("(");
            this.Visit(nodeToVisit);
            this.Out(")");
        } else{
            this.Visit(nodeToVisit);
        }
    }

    protected override Expression VisitMethodCall(MethodCallExpression node){
        this.Out(".Call ");
        if(node.Object!=null){
            this.ParenthesizedVisit(node,node.Object);
        } else if(node.Method.DeclaringType!=null){
            this.Out(node.Method.DeclaringType.ToString());
        } else{
            this.Out("<UnknownType>");
        }
        this.Out(".");
        this.Out(node.Method.Name);
        this.VisitExpressions('(',node.Arguments);
        return node;
    }

    protected override Expression VisitNewArray(NewArrayExpression node){
        if(node.NodeType==ExpressionType.NewArrayBounds){
            this.Out(".NewArray "+node.Type.GetElementType().ToString());
            this.VisitExpressions('[',node.Expressions);
        } else{
            this.Out(".NewArray "+node.Type.ToString(),Flow.Space);
            this.VisitExpressions('{',node.Expressions);
        }
        return node;
    }

    protected override Expression VisitNew(NewExpression node){
        this.Out(".New "+node.Type.ToString());
        this.VisitExpressions('(',node.Arguments);
        return node;
    }

    protected override ElementInit VisitElementInit(ElementInit node){
        if(node.Arguments.Count==1){
            this.Visit(node.Arguments[0]);
        } else{
            this.VisitExpressions('{',node.Arguments);
        }
        return node;
    }

    protected override Expression VisitListInit(ListInitExpression node){
        this.Visit(node.NewExpression);
        this.VisitExpressions('{',',',node.Initializers,delegate(ElementInit e){ this.VisitElementInit(e);});
        return node;
    }

    protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment){
        this.Out(assignment.Member.Name);
        this.Out(Flow.Space,"=",Flow.Space);
        this.Visit(assignment.Expression);
        return assignment;
    }

    protected override MemberListBinding VisitMemberListBinding(MemberListBinding binding){
        this.Out(binding.Member.Name);
        this.Out(Flow.Space,"=",Flow.Space);
        this.VisitExpressions('{',',',binding.Initializers,delegate(ElementInit e){ this.VisitElementInit(e);});
        return binding;
    }

    protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding){
        this.Out(binding.Member.Name);
        this.Out(Flow.Space,"=",Flow.Space);
        this.VisitExpressions('{',',',binding.Bindings,delegate(MemberBinding e){ this.VisitMemberBinding(e);});
        return binding;
    }

    protected override Expression VisitMemberInit(MemberInitExpression node){
        this.Visit(node.NewExpression);
        this.VisitExpressions('{',',',node.Bindings,delegate(MemberBinding e){ this.VisitMemberBinding(e);});
        return node;
    }

    protected override Expression VisitTypeBinary(TypeBinaryExpression node){
        this.ParenthesizedVisit(node,node.Expression);
        switch(node.NodeType){
            case ExpressionType.TypeIs:
                this.Out(Flow.Space,".Is",Flow.Space);
                break;
            case ExpressionType.TypeEqual:
                this.Out(Flow.Space,".TypeEqual",Flow.Space);
                break;
        }
        this.Out(node.TypeOperand.ToString());
        return node;
    }

    protected override Expression VisitUnary(UnaryExpression node){
        switch(node.NodeType){
            case ExpressionType.Convert:
                this.Out("("+node.Type.ToString()+")");
                break;
            case ExpressionType.ConvertChecked:
                this.Out("#("+node.Type.ToString()+")");
                break;
            case ExpressionType.Not:
                this.Out((node.Type==typeof(bool))?"!":"~");
                break;
            case ExpressionType.OnesComplement:
                this.Out("~");
                break;
            case ExpressionType.Negate:
                this.Out("-");
                break;
            case ExpressionType.NegateChecked:
                this.Out("#-");
                break;
            case ExpressionType.UnaryPlus:
                this.Out("+");
                break;
            case ExpressionType.Quote:
                this.Out("'");
                break;
            case ExpressionType.Throw:
                if(node.Operand==null){
                    this.Out(".Rethrow");
                } else{
                    this.Out(".Throw",Flow.Space);
                }
                break;
            case ExpressionType.IsFalse:
                this.Out(".IsFalse");
                break;
            case ExpressionType.IsTrue:
                this.Out(".IsTrue");
                break;
            case ExpressionType.Decrement:
                this.Out(".Decrement");
                break;
            case ExpressionType.Increment:
                this.Out(".Increment");
                break;
            case ExpressionType.PreDecrementAssign:
                this.Out("--");
                break;
            case ExpressionType.PreIncrementAssign:
                this.Out("++");
                break;
            case ExpressionType.Unbox:
                this.Out(".Unbox");
                break;
        }
        this.ParenthesizedVisit(node,node.Operand);
        switch(node.NodeType){
            case ExpressionType.TypeAs:
                this.Out(Flow.Space,".As",Flow.Space|Flow.Break);
                this.Out(node.Type.ToString());
                break;
            case ExpressionType.ArrayLength:
                this.Out(".Length");
                break;
            case ExpressionType.PostDecrementAssign:
                this.Out("--");
                break;
            case ExpressionType.PostIncrementAssign:
                this.Out("++");
                break;
        }
        return node;
    }

    protected override Expression VisitBlock(BlockExpression node){
        this.Out(".Block");
        //if(node.Type!=node.GetExpression(node.ExpressionCount-1).Type){
        //    this.Out($"<{node.Type}>");
        //}
        this.VisitDeclarations(node.Variables);
        this.Out(" ");
        this.VisitExpressions('{',';',node.Expressions);
        return node;
    }

    protected override Expression VisitDefault(DefaultExpression node){
        this.Out(".Default("+node.Type.ToString()+")");
        return node;
    }

    protected override Expression VisitLabel(LabelExpression node){
        this.Out(".Label",Flow.NewLine);
        this.Indent();
        this.Visit(node.DefaultValue);
        this.Dedent();
        this.NewLine();
        this.DumpLabel(node.Target);
        return node;
    }

    protected override Expression VisitGoto(GotoExpression node){
        this.Out("."+node.Kind,Flow.Space);
        this.Out(this.GetLabelTargetName(node.Target),Flow.Space);
        this.Out("{",Flow.Space);
        this.Visit(node.Value);
        this.Out(Flow.Space,"}");
        return node;
    }

    protected override Expression VisitLoop(LoopExpression node){
        this.Out(".Loop",Flow.Space);
        if(node.ContinueLabel!=null){
            this.DumpLabel(node.ContinueLabel);
        }
        this.Out(" {",Flow.NewLine);
        this.Indent();
        this.Visit(node.Body);
        this.Dedent();
        this.Out(Flow.NewLine,"}");
        if(node.BreakLabel!=null){
            this.Out("",Flow.NewLine);
            this.DumpLabel(node.BreakLabel);
        }
        return node;
    }

    protected override SwitchCase VisitSwitchCase(SwitchCase node){
        foreach(var testValue in node.TestValues){
            this.Out(".Case (");
            this.Visit(testValue);
            this.Out("):",Flow.NewLine);
        }
        this.Indent();
        this.Indent();
        this.Visit(node.Body);
        this.Dedent();
        this.Dedent();
        this.NewLine();
        return node;
    }

    protected override Expression VisitSwitch(SwitchExpression node){
        this.Out(".Switch ");
        this.Out("(");
        this.Visit(node.SwitchValue);
        this.Out(") {",Flow.NewLine);
        ExpressionVisitor.Visit(node.Cases,this.VisitSwitchCase);
        if(node.DefaultBody!=null){
            this.Out(".Default:",Flow.NewLine);
            this.Indent();
            this.Indent();
            this.Visit(node.DefaultBody);
            this.Dedent();
            this.Dedent();
            this.NewLine();
        }
        this.Out("}");
        return node;
    }

    protected override CatchBlock VisitCatchBlock(CatchBlock node){
        this.Out(Flow.NewLine,"} .Catch ("+node.Test.ToString());
        if(node.Variable!=null){
            this.Out(Flow.Space,"");
            this.VisitParameter(node.Variable);
        }
        if(node.Filter!=null){
            this.Out(") .If (",Flow.Break);
            this.Visit(node.Filter);
        }
        this.Out(") {",Flow.NewLine);
        this.Indent();
        this.Visit(node.Body);
        this.Dedent();
        return node;
    }

    protected override Expression VisitTry(TryExpression node){
        this.Out(".Try {",Flow.NewLine);
        this.Indent();
        this.Visit(node.Body);
        this.Dedent();
        ExpressionVisitor.Visit(node.Handlers,this.VisitCatchBlock);
        if(node.Finally!=null){
            this.Out(Flow.NewLine,"} .Finally {",Flow.NewLine);
            this.Indent();
            this.Visit(node.Finally);
            this.Dedent();
        } else if(node.Fault!=null){
            this.Out(Flow.NewLine,"} .Fault {",Flow.NewLine);
            this.Indent();
            this.Visit(node.Fault);
            this.Dedent();
        }
        this.Out(Flow.NewLine,"}");
        return node;
    }

    protected override Expression VisitIndex(IndexExpression node){
        if(node.Indexer!=null){
            this.OutMember(node,node.Object,node.Indexer);
        } else{
            this.ParenthesizedVisit(node,node.Object);
        }
        this.VisitExpressions('[',node.Arguments);
        return node;
    }

    protected override Expression VisitExtension(Expression node){
        this.Out($".Extension<{node.GetType()}>");
        if(node.CanReduce){
            this.Out(Flow.Space,"{",Flow.NewLine);
            this.Indent();
            this.Visit(node.Reduce());
            this.Dedent();
            this.Out(Flow.NewLine,"}");
        }
        return node;
    }

    protected override Expression VisitDebugInfo(DebugInfoExpression node){
        this.Out($".DebugInfo({node.Document.FileName}: {node.StartLine}, {node.StartColumn} - {node.EndLine}, {node.EndColumn})");
        return node;
    }

    private void DumpLabel(LabelTarget target){
        this.Out(".LabelTarget "+this.GetLabelTargetName(target)+":");
    }

    private string GetLabelTargetName(LabelTarget target){
        if(string.IsNullOrEmpty(target.Name)){
            return"#Label"+this.GetLabelTargetId(target);
        }
        return GetDisplayName(target.Name);
    }

    private void WriteLambda(LambdaExpression lambda){
        this.Out($".Lambda {this.GetLambdaName(lambda)}<{lambda.Type}>");
        this.VisitDeclarations(lambda.Parameters);
        this.Out(Flow.Space,"{",Flow.NewLine);
        this.Indent();
        this.Visit(lambda.Body);
        this.Dedent();
        this.Out(Flow.NewLine,"}");
    }

    private string GetLambdaName(LambdaExpression lambda){
        if(string.IsNullOrEmpty(lambda.Name)){
            return"#Lambda"+this.GetLambdaId(lambda);
        }
        return GetDisplayName(lambda.Name);
    }

    private static bool ContainsWhiteSpace(string name){
        foreach(var c in name){
            if(char.IsWhiteSpace(c)){
                return true;
            }
        }
        return false;
    }

    private static string QuoteName(string name){
        return"'"+name+"'";
    }

    private static string GetDisplayName(string name){
        if(ContainsWhiteSpace(name)){
            return QuoteName(name);
        }
        return name;
    }
}
