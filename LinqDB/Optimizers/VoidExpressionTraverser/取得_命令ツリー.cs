using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
// ReSharper disable AssignNullToNotNullAttribute

namespace LinqDB.Optimizers.VoidExpressionTraverser;
using static Common;

internal sealed class 取得_命令ツリー:VoidExpressionTraverser,IDisposable {
    private readonly StringBuilder IndentStringBuilder = new();
    private readonly StringBuilder sb = new();
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    /// <summary>オブジェクトが、ガベージ コレクションによって収集される前に、リソースの解放とその他のクリーンアップ操作の実行を試みることができるようにします。</summary>
    ~取得_命令ツリー() => this.Dispose(false);
    public void Dispose() {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// 破棄されているか
    /// </summary>
    private bool IsDisposed {
        get; set;
    }
    /// <summary>
    /// ファイナライザでDispose(false)する。
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing) {
        if(this.IsDisposed) {
            this.IsDisposed=true;
            if(disposing) {
            }
        }
    }
    private void AddIndent(char c) {
        var IndentStringBuilder = this.IndentStringBuilder;
        for(var a = IndentStringBuilder.Length-1;a>=0;a--) {
            var 一つ前の文字 = IndentStringBuilder[a];
            if(一つ前の文字=='├') {
                IndentStringBuilder[a]='│';
            } else if(一つ前の文字=='└') {
                IndentStringBuilder[a]='　';
            }
        }
        IndentStringBuilder.Append(c);
    }
    private void RemoveIndent() {
        var IndentStringBuilder = this.IndentStringBuilder;
        IndentStringBuilder.Length--;
    }
    private void UpdateIndent() {
        var IndentStringBuilder = this.IndentStringBuilder;
        var sb_Length= IndentStringBuilder.Length;
        if(sb_Length>0&&IndentStringBuilder[sb_Length-1]=='│') {
            IndentStringBuilder[sb_Length-1]='├';
        }
    }
    private void Indent(char c) {
        var IndentStringBuilder = this.IndentStringBuilder;
        IndentStringBuilder.Length--;
        IndentStringBuilder.Append(c);
    }
    //private String String_Indent = "";
    public string 実行(Expression Lambda){
        var sb=this.sb;
        sb.Clear();
        this.IndentStringBuilder.Clear();
        this.Traverse(Lambda);
        return sb.ToString();
    }
    private void Write(string s) {
        var sb = this.sb;
        sb.Append(this.IndentStringBuilder);
        sb.Append(s);
    }
    private void WriteLine(string s) {
        var sb = this.sb;
        sb.Append(this.IndentStringBuilder);
        sb.Append(s);
        sb.AppendLine();
    }
    private void Write(ReadOnlyCollection<Expression> Expressions) {
        var Expressions_Count = Expressions.Count;
        if(Expressions_Count>0) {
            if(Expressions_Count==1) {
                this.AddIndent('└');
                this.Traverse(Expressions[0]);
            } else {
                this.AddIndent('├');
                for(var a = 0;a<Expressions_Count;a++){
                    this.Indent(a<Expressions_Count-1?'├':'└');
                    this.Traverse(Expressions[a]);
                }
            }
            this.RemoveIndent();
        }
    }
    private readonly Stack<ReadOnlyCollection<ParameterExpression>> StackParameters = new();
    protected override void Block(BlockExpression Block) {
        var sb = this.sb;
        this.Write(nameof(ExpressionType.Block));
        var Block_Variables=Block.Variables;
        var StackParameters = this.StackParameters;
        StackParameters.Push(Block_Variables);
        var Block_Variables_Count=Block_Variables.Count;
        if(Block_Variables_Count>0) {
            sb.Append('(');
            {
                var Block_Variable = Block_Variables[0];
                sb.Append(Block_Variable.Type.Name);
                sb.Append(' ');
                sb.Append(Block_Variable.Name);
            }
            for(var a = 1;a<Block_Variables_Count;a++) {
                sb.Append(',');
                var Block_Variable = Block_Variables[a];
                sb.Append(Block_Variable.Type.Name);
                sb.Append(' ');
                sb.Append(Block_Variable.Name);
            }
            sb.Append(')');
        }
        sb.AppendLine();
        this.Write(Block.Expressions);
        StackParameters.Pop();
    }
    protected override void Call(MethodCallExpression MethodCall){
        var MethodCall_Method=MethodCall.Method;
        if(ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall_Method))) {
            this.WriteLine(MethodCall_Method.Name);
            this.Write(MethodCall.Arguments);
        } else {
            this.WriteLine($"{nameof(ExpressionType.Call)} {MethodCall_Method.ToString()}");
            if(MethodCall_Method.IsStatic) {
                this.Write(MethodCall.Arguments);
            } else {
                var MethodCall_Arguments = MethodCall.Arguments;
                var MethodCall_Arguments_Count = MethodCall_Arguments.Count;
                if(MethodCall_Arguments_Count==0) {
                    this.AddIndent('└');
                    this.Traverse(MethodCall.Object);
                } else {
                    this.AddIndent('├');
                    this.Traverse(MethodCall.Object);
                    for(var a = 0;a<MethodCall_Arguments_Count;a++){
                        this.Indent(a<MethodCall_Arguments_Count-1?'├':'└');
                        this.Traverse(MethodCall_Arguments[a]);
                    }
                }
                this.RemoveIndent();
            }
        }
    }
    protected override void Conditional(ConditionalExpression Conditional){
        this.WriteLine(nameof(ExpressionType.Conditional));
        this.AddIndent('├');
        this.Traverse(Conditional.Test);
        this.Indent('├');
        this.Traverse(Conditional.IfTrue);
        this.Indent('└');
        this.Traverse(Conditional.IfFalse);
        this.RemoveIndent();
    }
    protected override void Constant(ConstantExpression Constant){
        string Name;
        if(Constant.Value is null) {
            Name="null";
        } else {
            var Constant_Type = Constant.Type;
            if(Constant_Type==typeof(string)) {
                Name="\""+Constant.Value+"\"";
            } else if(Constant_Type==typeof(decimal)) {
                Name=Constant.Value+"M";
            } else {
                if(Constant_Type.IsEnum)Constant_Type=Enum.GetUnderlyingType(Constant_Type);
                if     (Constant_Type==typeof(sbyte))  Name="(SByte)"+Constant.Value;
                else if(Constant_Type==typeof(short))  Name="(Int16)"+Constant.Value;
                else if(Constant_Type==typeof(int))    Name=Constant.Value.ToString()!;
                else if(Constant_Type==typeof(long))   Name="(Int64)"+Constant.Value;
                else if(Constant_Type==typeof(IntPtr)) Name="(IntPtr)"+Constant.Value;
                else if(Constant_Type==typeof(byte))   Name="(Byte)"+Constant.Value;
                else if(Constant_Type==typeof(ushort)) Name="(UInt16)"+Constant.Value;
                else if(Constant_Type==typeof(uint))   Name=Constant.Value+"u";
                else if(Constant_Type==typeof(ulong))  Name=Constant.Value+"ul";
                else if(Constant_Type==typeof(UIntPtr))Name="(UIntPtr)"+Constant.Value;
                else if(Constant_Type==typeof(bool))   Name=(bool)Constant.Value ? "true" : "false";
                else if(Constant_Type==typeof(char))   Name="'"+Constant.Value+'"';
                else if(Constant_Type==typeof(float))  Name=Constant.Value+"f";
                else if(Constant_Type==typeof(double)) Name=Constant.Value+"d";
                else                                   Name="[Object]";
                //if(Constant_Type==typeof(sbyte)) {
                //    Name="(SByte)"+Constant.Value;
                //} else if(Constant_Type==typeof(short)) {
                //    Name="(Int16)"+Constant.Value;
                //} else if(Constant_Type==typeof(int)) {
                //    Name=Constant.Value.ToString()!;
                //} else if(Constant_Type==typeof(long)) {
                //    Name="(Int64)"+Constant.Value;
                //} else if(Constant_Type==typeof(IntPtr)) {
                //    Name="(IntPtr)"+Constant.Value;
                //} else if(Constant_Type==typeof(byte)) {
                //    Name="(Byte)"+Constant.Value;
                //} else if(Constant_Type==typeof(ushort)) {
                //    Name="(UInt16)"+Constant.Value;
                //} else if(Constant_Type==typeof(uint)) {
                //    Name=Constant.Value+"u";
                //} else if(Constant_Type==typeof(ulong)) {
                //    Name=Constant.Value+"ul";
                //} else if(Constant_Type==typeof(UIntPtr)) {
                //    Name="(UIntPtr)"+Constant.Value;
                //} else if(Constant_Type==typeof(bool)) {
                //    Name=(bool)Constant.Value ? "true" : "false";
                //} else if(Constant_Type==typeof(char)) {
                //    Name="'"+Constant.Value+'"';
                //} else if(Constant_Type==typeof(float)) {
                //    Name=Constant.Value+"f";
                //} else if(Constant_Type==typeof(double)) {
                //    Name=Constant.Value+"d";
                //} else {
                //    Name="[Object]";
                //}
            }
        }
        this.WriteLine($"{nameof(ExpressionType.Constant)} {Name}");
    }
    protected override void Default(DefaultExpression Default) => this.WriteLine("default "+Default.Type.Name);
    protected override void Goto(GotoExpression Goto){
        this.WriteLine(Goto.Kind+" "+Goto.Target.Name);
        if(Goto.Value is null) return;
        this.AddIndent('└');
        this.Traverse(Goto.Value);
        this.RemoveIndent();
    }
    protected override void Label(LabelExpression Label) {
        this.WriteLine("Label "+Label.Target.Name);
        if(Label.DefaultValue is null) return;
        this.AddIndent('└');
        this.Traverse(Label.DefaultValue);
        this.RemoveIndent();
    }
    protected override void Lambda(LambdaExpression Lambda){
        var sb = this.sb;
        this.Write(nameof(ExpressionType.Lambda));
        var Lambda_Parameters=Lambda.Parameters;
        var StackParameters = this.StackParameters;
        StackParameters.Push(Lambda_Parameters);
        var Lambda_Parameters_Count =Lambda_Parameters.Count;
        if(Lambda_Parameters_Count>0) {
            sb.Append('(');
            {
                var Lambda_Parameter = Lambda_Parameters[0];
                sb.Append(Lambda_Parameter.Type.Name);
                sb.Append(' ');
                sb.Append(Lambda_Parameter.Name);
            }
            for(var a = 1;a<Lambda_Parameters_Count;a++) {
                sb.Append(',');
                var Lambda_Parameter = Lambda_Parameters[a];
                sb.Append(Lambda_Parameter.Type.Name);
                sb.Append(' ');
                sb.Append(Lambda_Parameter.Name);
            }
            sb.Append(')');
        }
        sb.AppendLine();
        this.AddIndent('└');
        this.Traverse(Lambda.Body);
        this.RemoveIndent();
        StackParameters.Pop();
    }
    protected override void Loop(LoopExpression Loop) {
        this.WriteLine("Loop");
        if(Loop.ContinueLabel is null) {
            if(Loop.BreakLabel is null) {
                this.AddIndent('└');
                this.Traverse(Loop.Body);
            } else {
                this.AddIndent('├');
                this.Traverse(Loop.Body);
                this.Indent('└');
                this.WriteLine($"{nameof(Loop.BreakLabel)} {Loop.BreakLabel.Name}");
            }
        } else {
            this.AddIndent('├');
            this.WriteLine($"{nameof(Loop.ContinueLabel)} {Loop.ContinueLabel.Name}");
            if(Loop.BreakLabel is null) {
                this.Indent('└');
                this.Traverse(Loop.Body);
            } else {
                this.Traverse(Loop.Body);
                this.Indent('└');
                this.WriteLine($"{nameof(Loop.BreakLabel)} {Loop.BreakLabel.Name}");
            }
        }
        this.RemoveIndent();
    }
    protected override void MakeBinary(BinaryExpression Binary){
        this.WriteLine(Binary.NodeType.ToString());
        this.AddIndent('├');
        this.Traverse(Binary.Left);
        this.Indent('└');
        this.Traverse(Binary.Right);
        this.RemoveIndent();
    }
    protected override void MakeUnary(UnaryExpression Unary) {
        if(Unary.Method is not null) {
            this.WriteLine($"{Unary.NodeType} {Unary.Method.Name}");
        } else if(Unary.Type!=Unary.Operand.Type) {
            this.WriteLine($"{Unary.NodeType} {Unary.Type.Name}");
        } else {
            this.WriteLine(Unary.NodeType.ToString());
        }
        this.AddIndent('└');
        this.Traverse(Unary.Operand);
        this.RemoveIndent();
    }
    protected override void MemberAccess(MemberExpression Member){
        this.WriteLine($"{nameof(ExpressionType.MemberAccess)} {Member.Member.Name}");
        this.AddIndent('└');
        this.TraverseNulllable(Member.Expression);
        this.RemoveIndent();
    }
    protected override void ListInit(ListInitExpression ListInit){
        this.New(ListInit.NewExpression);
        this.AddIndent('[');
        var ListInit_Initializers=ListInit.Initializers;
        var ListInit_Initializers_Count=ListInit_Initializers.Count;
        for(var a = 0;a<ListInit_Initializers_Count;a++) {
            this.AddIndent('[');
            this.Write(ListInit_Initializers[a].Arguments);
            this.RemoveIndent();
        }
        this.RemoveIndent();
    }
    protected override void New(NewExpression New){
        this.WriteLine($"{nameof(ExpressionType.New)} {New.Type}");
        this.Write(New.Arguments);
    }
    protected override void NewArrayInit(NewArrayExpression NewArray) {
        this.WriteLine($"{nameof(ExpressionType.New)} {NewArray.Type}{{}}");
        this.Write(NewArray.Expressions);
    }
    protected override void Parameter(ParameterExpression Parameter) {
        this.WriteLine($"{nameof(ExpressionType.Parameter)} {Parameter.Name}");
    }
}
