using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Helpers;
// ReSharper disable AssignNullToNotNullAttribute

namespace LinqDB.Optimizers.VoidExpressionTraverser;

internal sealed class 取得_CSharp:VoidExpressionTraverser,IDisposable {
    private readonly StringBuilder sb = new();
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    /// <summary>オブジェクトが、ガベージ コレクションによって収集される前に、リソースの解放とその他のクリーンアップ操作の実行を試みることができるようにします。</summary>
    ~取得_CSharp() => this.Dispose(false);
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
    private readonly List<ParameterExpression> Parameters = new();
    private int Indent数;
    private void Indent() {
        var sb=this.sb;
        for(var a = this.Indent数;a>0;a--) {
            sb.Append("    ");
        }
    }
    public string 実行(LambdaExpression Lambda) {
        this.Indent数=0;
        var AnonymousTypes = this.AnonymousTypes;
        AnonymousTypes.Clear();
        this.括弧を付けるか=false;
        var sb=this.sb;
        sb.Clear();
        var ListParameters = this.Parameters;
        ListParameters.Clear();
        sb.Append("static object ");
        //this.型の正式な名前(Lambda.ReturnType);
        //sb.Append(' ');
        var Lambda_Name = Lambda.Name??"Main";
        sb.Append(Lambda_Name);
        sb.Append('(');
        var Lambda_Parameters = Lambda.Parameters;
        if(Lambda_Parameters.Count==0) {
            sb.Append(')');
        } else {
            foreach(var a in Lambda_Parameters) {
                this.型の正式な名前(a.Type);
                sb.Append(' ');
                sb.Append(a.Name);
                sb.Append(',');
            }
            sb[^1]=')';
        }
        if(Lambda.Body is BlockExpression Block) {
            this.Block(Block);
        } else {
            sb.Append("=>");
            this.Traverse(Lambda.Body);
            sb.AppendLine(";");
        }
        var AnonymousTypes_Count = AnonymousTypes.Count;
        for(var a=0;a<AnonymousTypes_Count;a++){
            var AnonymousType = AnonymousTypes[a];
            var AnonymousTypeName = $"Result{a}";
            sb.AppendLine($"struct {AnonymousTypeName}:System.Equals<{AnonymousTypeName}>{{");
            var Parameters = AnonymousType.GetConstructors()[0].GetParameters();
            foreach(var Parameter in Parameters) {
                sb.Append("    public ");
                this.型の正式な名前(Parameter.ParameterType);
                sb.Append(' ');
                sb.Append(Parameter.Name);
                sb.AppendLine(";");
            }
            sb.Append("    public Result");
            sb.Append(a);
            sb.Append('(');
            foreach(var Parameter in Parameters) {
                this.型の正式な名前(Parameter.ParameterType);
                sb.Append(' ');
                sb.Append(Parameter.Name);
                sb.Append(',');
            }
            sb[^1]=')';
            sb.AppendLine("{");
            foreach(var Parameter in Parameters) {
                var Parameter_Name=Parameter.Name;
                sb.Append("        this.");
                sb.Append(Parameter_Name);
                sb.Append('=');
                sb.Append(Parameter_Name);
                sb.AppendLine(";");
            }
            sb.AppendLine("    }");
            sb.AppendLine($"    public bool Equals({AnonymousTypeName} other)=>");
            var Parameters_Length=Parameters.Length;
            if(Parameters_Length==0) {
                sb.AppendLine("        true;");
            } else if(Parameters_Length==1) {
                var Parameter_Name = Parameters[0].Name;
                sb.Append("        this.");
                sb.Append(Parameter_Name);
                sb.Append(".Equals(");
                sb.Append(Parameter_Name);
                sb.AppendLine(");");
            } else {
                for(var b = 0;b<Parameters_Length-1;b++) {
                    var Parameter = Parameters[b];
                    var Parameter_Name = Parameter.Name;
                    sb.Append("        this.");
                    sb.Append(Parameter_Name);
                    sb.Append(".Equals(other.");
                    sb.Append(Parameter_Name);
                    sb.AppendLine(")&&");
                }
                {
                    var Parameter_Name = Parameters[Parameters_Length-1].Name;
                    sb.Append("        this.");
                    sb.Append(Parameter_Name);
                    sb.Append(".Equals(other.");
                    sb.Append(Parameter_Name);
                    sb.AppendLine(");");
                }
            }
            sb.AppendLine($"    public bool Equals(object obj)=>this.Equals(({AnonymousTypeName})obj);");
            sb.AppendLine("}");
        }
        sb.Append(Lambda_Name);
        sb.Append("(Entities)");
        //var Statement =sb.ToString();
        ////var ﾟあ゙か゚ː・ゝゞヽ⁀ヾ〱‿〲〳〴〵=123;
        ////var ﾟ=123;
        ////var ﾟあ゙か゚ː・ゝゞヽ⁀ヾ〱‿〲〳〴〵=123;
        //var ﾟ= "4";
        //Console.WriteLine(ﾟ);
        //var options = ScriptOptions.Default.AddReferences(typeof(Object).Assembly).AddImports("System");
        //var Script = CSharpScript.Create(
        //    Statement,
        //    options
        //);
        return sb.ToString();
    }
    private void 共通Block(BlockExpression Block) {

    }
    protected override void TraverseExpressions(ReadOnlyCollection<Expression> Expressions) {
        base.TraverseExpressions(Expressions);
    }
    private void 型の正式な名前(Type Type,Type[]GenericArguments,Type[]GenericParameters) {
        var sb = this.sb;
        if(Type.DeclaringType is not null) {
            this.型の正式な名前(Type.DeclaringType,GenericArguments,GenericParameters);
        } else {
            sb.Append(Type.Namespace);
        }
        sb.Append('.');
        var Type_Name = Type.Name;
        var Index = Type_Name.IndexOf('`');
        if(Index>=0) {
            Type_Name=Type_Name[..Index];
        }
        sb.Append(Type_Name);
        if(Type.IsGenericTypeDefinition){
            sb.Append('<');
            var Type_GenericArguments = Type.GetGenericArguments();
            foreach(var DeclaringType_GenericArgument in Type_GenericArguments) {
                for(var a= GenericArguments.Length-1;a>=0;a--) {
                    if(GenericParameters[a].Name==DeclaringType_GenericArgument.Name) {
                        this.型の正式な名前(GenericArguments[a]);
                        //GenericArguments.RemoveAt(a);
                    }
                }
            }
            sb.Append('>');
        }
    }
    private readonly List<Type> AnonymousTypes = new();
    private void 型の正式な名前(Type? Type) {
        if(Type is null) return;
        var sb = this.sb;
        if(Type.IsAnonymous()){
            var Index = this.AnonymousTypes.IndexOf(Type);
            if(Index<0) {
                Index=this.AnonymousTypes.Count;
                this.AnonymousTypes.Add(Type);
            }
            sb.Append("Result");
            sb.Append(Index);
            return;
        }
        if(Type.IsGenericType) {
            var GenericArguments = Type.GetGenericArguments();
            if(Type.DeclaringType is not null) {
                //this.型の正式な名前(Type.DeclaringType);
                this.型の正式な名前(
                    Type.DeclaringType,
                    GenericArguments,//やはりこれはListで削除する必要があるかもしれん

                    Type.GetGenericTypeDefinition().GetGenericArguments()
                );
                sb.Append('.');
            } else if(Type.Namespace is not null){
                sb.Append(Type.Namespace);
                sb.Append('.');
            }
            var Type_Name = Type.Name;
            var Index = Type_Name.IndexOf('`');
            if(Index>=0) {
                Type_Name=Type_Name[..Index];
            }
            sb.Append(Type_Name);
            if(Type.DeclaringType is not null&&Type.DeclaringType.IsGenericType) {
                var DeclaringType_GenericParameters = Type.DeclaringType.GetGenericTypeDefinition().GetGenericArguments();
                var Type_GenericParameters = Type.GetGenericTypeDefinition().GetGenericArguments();
                var Type_GenericArguments = Type.GetGenericArguments();
                sb.Append('<');
                var sb_Length = sb.Length;
                for(var a = 0;a<Type_GenericParameters.Length;a++) {
                    var Name = Type_GenericParameters[a].Name;
                    var DeclaringType_GenericParameters_Length = DeclaringType_GenericParameters.Length;
                    var b = 0;
                    while(true) {
                        if(b==DeclaringType_GenericParameters_Length) {
                            this.型の正式な名前(Type_GenericArguments[a]);
                            break;
                        }
                        if(DeclaringType_GenericParameters[b].Name==Name) {
                            break;
                        }
                        b++;
                    }
                }
                if(sb_Length<sb.Length) {
                    sb.Append('>');
                } else {
                    sb.Length--;
                }
            } else {
                var Type_GenericArguments = Type.GetGenericArguments();
                if(Type_GenericArguments.Length>0) {
                    sb.Append('<');
                    foreach(var Type_GenericArgument in Type_GenericArguments) {
                        this.型の正式な名前(Type_GenericArgument);
                        sb.Append(',');
                    }
                    sb[^1]='>';
                }
            }
        } else {
            if(Type.DeclaringType is not null) {
                this.型の正式な名前(Type.DeclaringType);
            } else {
                sb.Append(Type.Namespace);
            }
            sb.Append('.');
            var Type_Name = Type.Name;
            var Index = Type_Name.IndexOf('`');
            if(Index>=0) {
                Type_Name=Type_Name[..Index];
            }
            sb.Append(Type_Name);
        }
    }
    private bool 括弧を付けるか;
    private void Block0(Expression Expression) {
        if(Expression is BlockExpression Block) {
            this.Block(Block);
        } else {
            this.Indent();
            this.Traverse(Expression);
        }
    }
    private class メソッド {
        public readonly List<メソッド> Listメソッド = new();
    }
#if true
    protected override void Block(BlockExpression Block) {
        var sb = this.sb;
        if(Block.Type!=typeof(void)) {
            var ローカルメソッド名変数名='_';
            this.Indent();
            sb.AppendLine("{");
            this.Indent数++;
            this.Indent();
            sb.AppendLine($"{ローカルメソッド名変数名}();");
            this.Indent();
            this.型の正式な名前(Block.Type);
            sb.Append(' ');
            sb.Append(ローカルメソッド名変数名);
            sb.Append("()");
        }
        sb.AppendLine("{");
        this.Indent数++;
        foreach(var a in Block.Variables) {
            this.Indent();
            this.型の正式な名前(a.Type);
            sb.Append(' ');
            sb.Append(a.Name);
            sb.AppendLine(";");
        }
        var Block_Expressions = Block.Expressions;
        var Block_Expressions_Count = Block_Expressions.Count;
        for(var a = 0;a<Block_Expressions_Count;a++) {
            var Block_Expression = Block_Expressions[a];
            //foreach(var Block_Expression in Block.Expressions) {
            if(Block_Expression is BlockExpression Block0) {
                this.Block(Block0);
            } else {
                this.Indent();
                if(a==Block_Expressions_Count-1) {
                    sb.Append("return ");
                }
                this.Traverse(Block_Expression);
                sb.AppendLine(";");
            }
        }
        if(Block.Type!=typeof(void)) {
            this.Indent数--;
            this.Indent();
            sb.AppendLine("}");
        }
        this.Indent数--;
        this.Indent();
        sb.AppendLine("}");
    }
#else
        private List<BlockExpression> Blocks = new List<BlockExpression>();
        protected override void Block(BlockExpression Block) {
            var Blocks = this.Blocks;
            Blocks.Clear();
            this.Indent();
            var sb = this.sb;
            sb.AppendLine("{");
            this.Indent数++;
            //this.Blocks=new List<BlockExpression>();
            var 番号 = 0;
            var Block_Expressions=Block.Expressions;
            var Block_Expressions_Count = Block_Expressions.Count-1;
            for(var a=0;a<Block_Expressions_Count;a++){
                var Block_Expression = Block_Expressions[a];
                if(Block_Expression is BlockExpression Block0) {
                    if(Block0.Type!=typeof(void)) {
                        var ローカルメソッド名 = '_'+番号++.ToString();
                        Blocks.Add(Block0);
                        this.Indent();
                        sb.Append(ローカルメソッド名);
                        sb.AppendLine("();");
                    } else {
                        this.Block(Block0);
                    }
                } else {
                    this.Indent();
                    this.Traverse(Block_Expression);
                    sb.AppendLine(";");
                }
            }
            {
                var Block_Expression = Block_Expressions[Block_Expressions_Count];
                if(Block_Expression is BlockExpression Block0) {
                    if(Block0.Type!=typeof(void)) {
                        var ローカルメソッド名 = '_'+番号++.ToString();
                        Blocks.Add(Block0);
                        this.Indent();
                        sb.Append("return ");
                        sb.Append(ローカルメソッド名);
                        sb.AppendLine("();");
                    } else {
                        this.Block(Block0);
                    }
                } else {
                    this.Indent();
                    sb.Append("return ");
                    this.Traverse(Block_Expression);
                    sb.AppendLine(";");
                }
            }
            //foreach(var a in Block.Variables) {
            //    this.Indent();
            //    this.型の正式な名前(a.Type);
            //    sb.Append(' ');
            //    sb.Append(a.Name);
            //    sb.AppendLine(";");
            //}
            //番号= 0;
            //foreach(var Block0 in Blocks) {
            //    var ローカルメソッド名 = '_'+番号++.ToString();
            //    //sb.AppendLine("{");
            //    this.Indent();
            //    this.型の正式な名前(Block0.Type);
            //    sb.Append(' ');
            //    sb.Append(ローカルメソッド名);
            //    sb.AppendLine("(){");
            //    foreach(var Block1 in Blocks) {
            //        this.Indent数++;
            //        this.Indent();
            //        this.型の正式な名前(Block1.Type);
            //        sb.Append(' ');
            //        sb.Append(ローカルメソッド名);
            //        sb.AppendLine("(){");
            //        this.Indent数--;
            //        this.Indent();
            //        sb.AppendLine("}");
            //    }
            //}
            //if(Block.Type!=typeof(void)) {
            //    this.Indent数--;
            //    this.Indent();
            //    sb.AppendLine("}");
            //}
            this.Indent数--;
            this.Indent();
            sb.AppendLine("}");
            this.Blocks=Blocks;
        }
#endif
    protected override void Call(MethodCallExpression MethodCall){
        var sb = this.sb;
        if(MethodCall.Object is not null) {
            this.Traverse(MethodCall.Object);
        } else {
            this.型の正式な名前(MethodCall.Method.DeclaringType);
        }
        sb.Append('.');
        sb.Append(MethodCall.Method.Name);
        if(MethodCall.Arguments.Count==0) {
            sb.Append("()");
        } else {
            sb.Append('(');
            foreach(var a in MethodCall.Arguments) {
                this.Traverse(a);
                sb.Append(',');
            }
            sb[^1]=')';
        }
    }
    protected override void Conditional(ConditionalExpression Conditional){
        var sb = this.sb;
        sb.Append("if(");
        this.Traverse(Conditional.Test);
        sb.AppendLine("){");
        this.Indent数++;
        if(Conditional.IfTrue is BlockExpression IfTrue) {
            this.Block(IfTrue);
            this.Indent数--;
            this.Indent();
            sb.AppendLine("}else{");
            this.Indent数++;
            if(Conditional.IfFalse is BlockExpression IfFalse) {
                this.Block(IfFalse);
            } else {
                this.Indent();
                this.Traverse(Conditional.IfFalse);
                sb.AppendLine(";");
            }
        } else {
            this.Indent();
            this.Traverse(Conditional.IfTrue);
            sb.AppendLine();
            this.Indent数--;
            this.Indent();
            sb.AppendLine("}else{");
            this.Indent数++;
            if(Conditional.IfFalse is BlockExpression IfFalse) {
                this.Block(IfFalse);
            } else {
                this.Indent();
                this.Traverse(Conditional.IfFalse);
                sb.AppendLine(";");
            }
        }
        this.Indent数--;
        this.Indent();
        sb.AppendLine("}");
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
                if(Constant_Type.IsEnum) {
                    Constant_Type=Enum.GetUnderlyingType(Constant_Type);
                }
                if(Constant_Type==typeof(sbyte)) {
                    Name=$"((SByte){Constant.Value})";
                } else if(Constant_Type==typeof(short)) {
                    Name=$"((Int16){Constant.Value})";
                } else if(Constant_Type==typeof(int)) {
                    Name=Constant.Value.ToString()!;
                } else if(Constant_Type==typeof(long)) {
                    Name=$"{Constant.Value}L";
                } else if(Constant_Type==typeof(IntPtr)) {
                    Name=$"((IntPtr){Constant.Value})";
                } else if(Constant_Type==typeof(byte)) {
                    Name=$"((Byte){Constant.Value})";
                } else if(Constant_Type==typeof(ushort)) {
                    Name=$"((UInt16){Constant.Value})";
                } else if(Constant_Type==typeof(uint)) {
                    Name=$"({Constant.Value}U)";
                } else if(Constant_Type==typeof(ulong)) {
                    Name=$"({Constant.Value}UL)";
                } else if(Constant_Type==typeof(UIntPtr)) {
                    Name=$"((UIntPtr){Constant.Value})";
                } else if(Constant_Type==typeof(bool)) {
                    Name=(bool)Constant.Value ? "true" : "false";
                } else if(Constant_Type==typeof(char)) {
                    Name=$"'{Constant.Value}'";
                } else if(Constant_Type==typeof(float)) {
                    Name=$"({Constant.Value}f)";
                } else if(Constant_Type==typeof(double)) {
                    Name=$"({Constant.Value}d)";
                } else {
                    throw new NotSupportedException(Constant.Value.ToString());
                }
            }
        }
        this.sb.Append(Name);
    }
    protected override void Default(DefaultExpression Default) {
        if(Default.Type==typeof(void)) {
        } else {
            this.sb.Append($"default({Default.Type.Name})");
        }
    }
    protected override void Goto(GotoExpression Goto) {
        switch(Goto.Kind) {
            case GotoExpressionKind.Goto:
                this.sb.Append($"goto {Goto.Target.Name}");
                break;
            case GotoExpressionKind.Continue:
                this.sb.Append("continue");
                break;
            case GotoExpressionKind.Break:
                this.sb.Append("break");
                break;
            case GotoExpressionKind.Return:
                this.sb.Append("return ");
                this.Traverse(Goto.Value);
                break;
            default:
                throw new NotSupportedException($"{Goto.Kind}は実装されていない。");
        }
    }
    protected override void Label(LabelExpression Label) {
        this.sb.Append($"{Label.Target.Name}:");
    }
    protected override void Lambda(LambdaExpression Lambda){
        var sb = this.sb;
        var Lambda_Parameters = Lambda.Parameters;
        if(Lambda_Parameters.Count==0) {
            sb.Append("()");
        } else if(Lambda_Parameters.Count==1) {
            sb.Append(Lambda_Parameters[0].Name);
        } else {
            sb.Append('(');
            foreach(var a in Lambda_Parameters) {
                sb.Append(a.Name);
                sb.Append(',');
            }
            sb[^1]=')';
        }
        sb.Append("=>");
        this.Traverse(Lambda.Body);
    }
    protected override void Loop(LoopExpression Loop) {
        var sb = this.sb;
        sb.Append("while(true)");
        if(Loop.Body is BlockExpression Block) {
            sb.AppendLine("{");
            this.Indent数++;
            //if(Loop.ContinueLabel is not null) {
            //    sb.Append(Loop.ContinueLabel.Name);
            //    sb.AppendLine(":");
            //}
            this.Block(Block);
            this.Indent数--;
            this.Indent();
            sb.AppendLine("}");
            //if(Loop.BreakLabel is not null) {
            //    sb.Append(Loop.BreakLabel.Name);
            //    sb.AppendLine(":;");
            //}
            //} else if(Loop.ContinueLabel is not null) {
            //    sb.AppendLine("{");
            //    this.Indent数++;
            //    sb.Append(Loop.ContinueLabel.Name);
            //    sb.AppendLine(":");
            //    this.Indent();
            //    this.Traverse(Loop.Body);
            //    //if(Loop.BreakLabel is not null) {
            //    //    sb.Append(Loop.BreakLabel.Name);
            //    //    sb.AppendLine(":;");
            //    //}
            //    this.Indent数--;
            //    this.Indent();
            //    sb.AppendLine("}");
            //} else if(Loop.BreakLabel is not null) {
            //    sb.AppendLine("{");
            //    this.Indent数++;
            //    this.Indent();
            //    this.Traverse(Loop.Body);
            //    sb.AppendLine(":");
            //    sb.Append(Loop.BreakLabel.Name);
            //    sb.AppendLine(":;");
            //    sb.AppendLine("}");
        } else {
            this.Indent();
            this.Traverse(Loop.Body);
            sb.AppendLine(";");
        }
    }
    private void Binary(BinaryExpression Binary,char 演算子){
        var 括弧を付けるか=this.括弧を付けるか;
        this.括弧を付けるか=true;
        var sb = this.sb;
        if(括弧を付けるか) {
            sb.Append('(');
        }
        this.Traverse(Binary.Left);
        sb.Append(演算子);
        this.Traverse(Binary.Right);
        if(括弧を付けるか) {
            sb.Append(')');
        }
        this.括弧を付けるか=括弧を付けるか;
    }
    private void Binary(BinaryExpression Binary,string 演算子) {
        var sb = this.sb;
        sb.Append('(');
        this.Traverse(Binary.Left);
        sb.Append(演算子);
        this.Traverse(Binary.Right);
        sb.Append(')');
    }
    private void BinaryChecked(BinaryExpression Binary,char 演算子) {
        var sb = this.sb;
        sb.Append("checked(");
        this.Traverse(Binary.Left);
        sb.Append(演算子);
        this.Traverse(Binary.Right);
        sb.Append(')');
    }
    private void BinaryChecked(BinaryExpression Binary,string 演算子) {
        var sb = this.sb;
        sb.Append("checked(");
        this.Traverse(Binary.Left);
        sb.Append(演算子);
        this.Traverse(Binary.Right);
        sb.Append(')');
    }
    /// <summary>
    /// a+b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Add(BinaryExpression Binary) => this.Binary(Binary,'+');
    /// <summary>
    /// checked(a+b)
    /// </summary>
    /// <param name="Binary"></param>
    protected override void AddChecked(BinaryExpression Binary) => this.BinaryChecked(Binary,'+');
    /// <summary>
    /// a&amp;
    /// </summary>
    /// <param name="Binary"></param>
    protected override void And(BinaryExpression Binary) => this.Binary(Binary,'&');
    /// <summary>
    /// a&amp;&amp;b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void AndAlso(BinaryExpression Binary) => this.Binary(Binary,"&&");
    /// <summary>
    /// a[b]
    /// </summary>
    /// <param name="Binary"></param>
    protected override void ArrayIndex(BinaryExpression Binary) {
        var sb = this.sb;
        this.Traverse(Binary.Left);
        sb.Append('[');
        this.Traverse(Binary.Right);
        sb.Append(']');
    }
    /// <summary>
    /// a=b
    /// </summary>
    /// <param name="Assign"></param>
    protected override void Assign(BinaryExpression Assign) => this.Binary(Assign,'=');
    /// <summary>
    /// a??b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Coalesce(BinaryExpression Binary) => this.Binary(Binary,"??");
    /// <summary>
    /// a/b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Divide(BinaryExpression Binary) => this.Binary(Binary,'/');
    /// <summary>
    /// a==b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Equal(BinaryExpression Binary) => this.Binary(Binary,"==");
    /// <summary>
    /// a^b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void ExclusiveOr(BinaryExpression Binary) => this.Binary(Binary,'^');
    /// <summary>
    /// a>b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void GreaterThan(BinaryExpression Binary) => this.Binary(Binary,'>');
    /// <summary>
    /// a>=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void GreaterThanOrEqual(BinaryExpression Binary) => this.Binary(Binary,">=");
    /// <summary>
    /// ac&lt;b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void LeftShift(BinaryExpression Binary) => this.Binary(Binary,"<<");
    /// <summary>
    /// a&lt;b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void LessThan(BinaryExpression Binary) => this.Binary(Binary,'<');
    /// <summary>
    /// a&lt;=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void LessThanOrEqual(BinaryExpression Binary) => this.Binary(Binary,"<=");
    /// <summary>
    /// a%b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Modulo(BinaryExpression Binary) => this.Binary(Binary,'%');
    /// <summary>
    /// a*b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Multiply(BinaryExpression Binary) => this.Binary(Binary,'*');
    /// <summary>
    /// checked(a*b)
    /// </summary>
    /// <param name="Binary"></param>
    protected override void MultiplyChecked(BinaryExpression Binary) => this.BinaryChecked(Binary,'*');
    /// <summary>
    /// a!=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void NotEqual(BinaryExpression Binary) => this.Binary(Binary,"!=");
    /// <summary>
    /// a|b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Or(BinaryExpression Binary) => this.Binary(Binary,'|');
    /// <summary>
    /// a||b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void OrElse(BinaryExpression Binary) => this.Binary(Binary,"||");
    /// <summary>
    /// VisualBasic a^b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Power(BinaryExpression Binary) {
        var sb = this.sb;
        sb.Append("System.Math.Power(");
        this.Traverse(Binary.Left);
        sb.Append(',');
        this.Traverse(Binary.Right);
        sb.Append(')');
    }
    /// <summary>
    /// a>>b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void RightShift(BinaryExpression Binary) => this.Binary(Binary,">>");
    /// <summary>
    /// a-b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void Subtract(BinaryExpression Binary) => this.Binary(Binary,'-');
    /// <summary>
    /// checked(a-b)
    /// </summary>
    /// <param name="Binary"></param>
    protected override void SubtractChecked(BinaryExpression Binary) => this.BinaryChecked(Binary,'-');
    /// <summary>
    /// a+=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void AddAssign(BinaryExpression Binary) => this.Binary(Binary,"+=");
    /// <summary>
    /// checked(a+=b)
    /// </summary>
    /// <param name="Binary"></param>
    protected override void AddAssignChecked(BinaryExpression Binary) => this.BinaryChecked(Binary,"+=");
    /// <summary>
    /// a&amp;=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void AndAssign(BinaryExpression Binary) => this.Binary(Binary,"&=");
    /// <summary>
    /// a/=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void DivideAssign(BinaryExpression Binary) => this.Binary(Binary,"/=");
    /// <summary>
    /// a^=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void ExclusiveOrAssign(BinaryExpression Binary) => this.Binary(Binary,"^=");
    /// <summary>
    /// a&lt;&lt;=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void LeftShiftAssign(BinaryExpression Binary) => this.Binary(Binary,"<<=");
    /// <summary>
    /// a%=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void ModuloAssign(BinaryExpression Binary) => this.Binary(Binary,"%=");
    /// <summary>
    /// a*=b
    /// </summary>
    /// <param name="Binary"></param>
    protected override void MultiplyAssign(BinaryExpression Binary) => this.Binary(Binary,"*=");
    /// <summary>
    /// checked(a*=b)
    /// </summary>
    /// <param name="Binary"></param>
    protected override void MultiplyAssignChecked(BinaryExpression Binary) => this.BinaryChecked(Binary,"*=");
    private void Unary(UnaryExpression Unary,char 演算子) {
        var 括弧を付けるか = this.括弧を付けるか;
        this.括弧を付けるか=true;
        var sb = this.sb;
        if(括弧を付けるか) {
            sb.Append('(');
        }
        sb.Append(演算子);
        this.Traverse(Unary.Operand);
        if(括弧を付けるか) {
            sb.Append(')');
        }
        this.括弧を付けるか=括弧を付けるか;
    }
    private void Unary(UnaryExpression Unary,string 演算子) {
        var sb = this.sb;
        sb.Append('(');
        sb.Append(演算子);
        this.Traverse(Unary.Operand);
        sb.Append(')');
    }
    private void UnaryChecked(UnaryExpression Unary,char 演算子) {
        var sb = this.sb;
        sb.Append("checked(");
        sb.Append(演算子);
        this.Traverse(Unary.Operand);
        sb.Append(')');
    }
    /// <summary>
    /// a.Length
    /// </summary>
    /// <param name="Unary"></param>
    protected override void ArrayLength(UnaryExpression Unary) {
        this.Traverse(Unary.Operand);
        this.sb.Append(".Length");
    }
    private void 共通Convert(UnaryExpression Unary,string 先頭文字列) {
        var sb = this.sb;
        sb.Append(先頭文字列);
        sb.Append(Unary.Type.FullName);
        sb.Append('(');
        this.Traverse(Unary.Operand);
        sb.Append(')');
    }
    /// <summary>
    /// (Type0)a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Convert(UnaryExpression Unary) => this.共通Convert(Unary,"((");
    /// <summary>
    /// checked((Type0)a)
    /// </summary>
    /// <param name="Unary"></param>
    protected override void ConvertChecked(UnaryExpression Unary) => this.共通Convert(Unary,"checked((");
    private void 共通IncrementDecrement(UnaryExpression Unary,char 演算子) {
        var sb = this.sb;
        sb.Append('(');
        this.Traverse(Unary.Operand);
        sb.Append(演算子);
        sb.Append("1)");
    }
    /// <summary>
    /// operator ++
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Decrement(UnaryExpression Unary) => this.共通IncrementDecrement(Unary,'-');
    /// <summary>
    /// operator --
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Increment(UnaryExpression Unary) => this.共通IncrementDecrement(Unary,'+');
    /// <summary>
    /// operator true
    /// </summary>
    /// <param name="Unary"></param>
    protected override void IsTrue(UnaryExpression Unary) => this.Traverse(Unary.Operand);
    /// <summary>
    /// operator false
    /// </summary>
    /// <param name="Unary"></param>
    protected override void IsFalse(UnaryExpression Unary) => this.Traverse(Unary.Operand);
    /// <summary>
    /// -a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Negate(UnaryExpression Unary) => this.Unary(Unary,'-');
    /// <summary>
    /// checked(-a)
    /// </summary>
    /// <param name="Unary"></param>
    protected override void NegateChecked(UnaryExpression Unary) => this.UnaryChecked(Unary,'-');
    /// <summary>
    /// !a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Not(UnaryExpression Unary) => this.Unary(Unary,'!');
    /// <summary>
    /// (Int32)a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Unbox(UnaryExpression Unary) => this.共通Convert(Unary,"((");
    /// <summary>
    /// ~a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void OnesComplement(UnaryExpression Unary) => this.Unary(Unary,'~');
    /// <summary>
    /// ++a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void PreIncrementAssign(UnaryExpression Unary) => this.Unary(Unary,"++");
    /// <summary>
    /// --a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void PreDecrementAssign(UnaryExpression Unary) => this.Unary(Unary,"--");
    private void UnaryPost(UnaryExpression Unary,string 演算子) {
        var sb = this.sb;
        sb.Append('(');
        this.Traverse(Unary.Operand);
        sb.Append(演算子);
        sb.Append(')');
    }
    /// <summary>
    /// a++
    /// </summary>
    /// <param name="Unary"></param>
    protected override void PostIncrementAssign(UnaryExpression Unary) => this.UnaryPost(Unary,"++");
    /// <summary>
    /// a--
    /// </summary>
    /// <param name="Unary"></param>
    protected override void PostDecrementAssign(UnaryExpression Unary) => this.UnaryPost(Unary,"--");
    /// <summary>
    /// a=>b+cを式木として扱う。
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Quote(UnaryExpression Unary) => this.Traverse(Unary.Operand);
    /// <summary>
    /// throw a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void Throw(UnaryExpression Unary) {
        this.sb.Append("throw ");
        this.Traverse(Unary.Operand);
    }
    /// <summary>
    /// a as Type0
    /// </summary>
    /// <param name="Unary"></param>
    protected override void TypeAs(UnaryExpression Unary) {
        var sb = this.sb;
        sb.Append('(');
        this.Traverse(Unary.Operand);
        sb.Append(" as ");
        sb.Append(Unary.Type.FullName);
        sb.Append(')');
    }
    /// <summary>
    /// +a
    /// </summary>
    /// <param name="Unary"></param>
    protected override void UnaryPlus(UnaryExpression Unary) => this.Unary(Unary,'+');
    protected override void MemberAccess(MemberExpression Member){
        var sb = this.sb;
        sb.Append('(');
        if(Member.Expression is not null) {
            this.Traverse(Member.Expression);
        } else {
            sb.Append(Member.Member.DeclaringType!.FullName);
        }
        sb.Append('.');
        sb.Append(Member.Member.Name);
        sb.Append(')');
    }
    protected override void ListInit(ListInitExpression ListInit){
        var sb = this.sb;
        this.New(ListInit.NewExpression);
        sb.Append('{');
        foreach(var Initializer in ListInit.Initializers) {
            sb.Append('{');
            foreach(var Argument in Initializer.Arguments) {
                this.Traverse(Argument);
                sb.Append(',');
            }
            sb[^1]='}';
        }
        sb.Append('}');
    }
    protected override void New(NewExpression New){
        var sb = this.sb;
        sb.Append("new ");
        this.型の正式な名前(New.Type);
        if(New.Arguments.Count==0) {
            sb.Append("()");
        } else {
            sb.Append('(');
            foreach(var Argument in New.Arguments) {
                this.Traverse(Argument);
                sb.Append(',');
            }
            sb[^1]=')';
        }
    }
    protected override void NewArrayInit(NewArrayExpression NewArray) {
        var sb = this.sb;
        sb.Append(NewArray.Type.FullName);
        if(NewArray.Expressions.Count>0) {
            sb.Append('{');
            foreach(var Argument in NewArray.Expressions) {
                this.Traverse(Argument);
                sb.Append(',');
            }
            sb[^1]='}';
        }
    }
    protected override void Parameter(ParameterExpression Parameter) {
        this.sb.Append(Parameter.Name);
    }
}
