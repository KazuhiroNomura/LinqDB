using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using LinqDB.Helpers;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable All
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
using Profiling;

/// <summary>
/// プロファイル出来るように式木に計測を埋め込む
/// </summary>
public sealed class 変換_Stopwatchに埋め込む(作業配列 作業配列,計測Maneger 計測Maneger,Dictionary<LabelTarget,計測> Dictionary_LabelTarget_辺)
    :ReturnExpressionTraverser(作業配列){
    private static void TypeString(StringBuilder sb,Type e) {
        if(e.IsAnonymous()){
            sb.Append('{');
            foreach(var Parameter in e.GetConstructors()[0].GetParameters()){
                TypeString(sb,Parameter.ParameterType);
                sb.Append(Parameter.Name).Append(',');
            }
            sb[^1]='}';
        } else {
            if(!e.IsGenericType){
                sb.Append(e.FullName);
                return;
            }
            var Name = e.FullName!;
            //Debug.Assert(Name is not null);
            var index1 =Name.IndexOf('`');
            sb.Append(
                index1>=0
                    ?Name[..index1]
                    :Name).Append('<');
            foreach(var GenericArgument in e.GetGenericArguments()) {
                TypeString(sb,GenericArgument);
                sb.Append(',');
            }
            sb[^1]='>';
        }
    }
    private static void ParameterString(StringBuilder sb,MethodBase Method){
        sb.Append('(');
        foreach(var Parameter in Method.GetParameters()){
            TypeString(sb,Parameter.ParameterType);
            sb.Append(' ');
            sb.Append(Parameter.Name);
            sb.Append(',');
        }
        sb[^1]=')';
    }
    internal static class Reflection{
        public static readonly MethodInfo Start= typeof(Stopwatch).GetMethod(nameof(Stopwatch.Start))!;
        public static readonly MethodInfo Stop = typeof(Stopwatch).GetMethod(nameof(Stopwatch.Stop))!;
    }
    private readonly StringBuilder sb=new();
    private int 制御番号;
    public Expression 実行(Expression e){
        this.制御番号=-1;
        Dictionary_LabelTarget_辺.Clear();
        //計測Maneger.Clear();
        this.ルート演算計測=null;
        this._演算計測=null;
        this.制御計測=null;
        var Stopwatch=計測Maneger.Stopwatch;
        Stopwatch.Reset();
        var Lambda0=(LambdaExpression)e;
        var Lambda0_Body=Lambda0.Body;
        var Lambda1_Body=this.Traverse(Lambda0_Body);
        BlockExpression Block;
        var t0=Expression.Parameter(typeof(計測Maneger),"List計測");
        var Assign0=Expression.Assign(
            t0,
            Expression.Constant(計測Maneger)
        );
        var サンプリング開始=Expression.Call(t0,計測Maneger.Reflection.サンプリング開始);
        var サンプリング終了=Expression.Call(t0,計測Maneger.Reflection.サンプリング終了);
        var t1=Expression.Parameter(typeof(Stopwatch),"Stopwatch");
        var Assign1=Expression.Assign(
            t1,
            Expression.Constant(Stopwatch)
        );
        var Start=Expression.Call(t1,Reflection.Start);
        var Stop=Expression.Call(t1,Reflection.Stop);
        if(Lambda1_Body.Type==typeof(void))
            Block=Expression.Block(
                this.作業配列.Parameters設定(t0,t1),
                Assign0,
                Assign1,
                サンプリング開始,
                Start,
                Lambda1_Body,
                Stop,
                サンプリング終了
            );
        else{
            var t2=Expression.Parameter(Lambda1_Body.Type,"Lambda1_Body");
            Block=Expression.Block(
                this.作業配列.Parameters設定(t0,t1,t2),
                Assign0,
                Assign1,
                サンプリング開始,
                Start,
                Expression.Assign(t2,Lambda1_Body),
                Stop,
                サンプリング終了,
                t2
            );
        }
        var Lambda1=Expression.Lambda(
            Lambda0.Type,
            Block,
            Lambda0.TailCall,
            Lambda0.Parameters
        );
        return Lambda1;
    }
    //private readonly StringBuilder sb=new();
    //private readonly List<string>List表とツリー=new();
    //internal readonly Stopwatch Stopwatch=new();
    //public long ElapsedMilliseconds=>this.Stopwatch.ElapsedMilliseconds;
    //public string Analize{
    //    get{
    //        if(this.制御計測 is null) return string.Empty;
    //        var List表とツリー=this.List表とツリー;
    //        List表とツリー.Clear();
    //        var sb=this.sb;
    //        this.制御計測.Consoleに出力(List表とツリー,sb,this.Stopwatch.ElapsedMilliseconds);
    //        sb.Clear();
    //        foreach(var a in List表とツリー) sb.AppendLine(a);
    //        return sb.ToString();
    //    }
    //}
    public string Analize=>/*this.制御計測.Consoleに出力()*/ 計測Maneger.Analize(this.ルート演算計測);

    protected override Expression Block(BlockExpression Block0) {
        var sb = this.sb;
        sb.Clear();
        if(Block0.Variables.Count>0) {
            sb.Append('(');
            foreach(var Parameter in Block0.Variables) {
                TypeString(sb,Parameter.Type);
                sb.Append(' ');
                sb.Append(Parameter.Name);
                sb.Append(',');
            }
            sb[^1]=')';
        }
        var 親子演算計測=this.計測する前処理演算("Block");
        //var EndCondition = new 仮想ノード(++this.制御番号,"Block","");



        var Block0_Expressions = Block0.Expressions;
        var Block0_Expressions_Count = Block0_Expressions.Count;
        var Block1_Expressions = new Expression[Block0_Expressions_Count];
        var Block0_Expressions_Count_1 = Block0_Expressions_Count-1;
        if(Block0_Expressions_Count_1>=0) {
            for(var a = 0;a<Block0_Expressions_Count_1;a++)
                Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
            Block1_Expressions[Block0_Expressions_Count_1]=this.Traverse(Block0_Expressions[Block0_Expressions_Count_1]);
        }
        //this.演算計測=親子演算計測.親演算計測;
        return this.計測しない後処理(
            親子演算計測,
            Expression.Block(
                Block0.Type,
                Block0.Variables,
                Block1_Expressions
            )
        );
    }
    //private (計測? 親演算計測,計測 子演算計測)仮想ノード前処理演算(Expression Expression,string Name,string? Value =null){
    //    if(Value is null) Value="";
    //    ref var 制御計測=ref this.制御計測;
    //    int 制御番号;
    //    if(制御計測 is null)
    //        制御番号=++this.制御番号;
    //    else
    //        制御番号=this.制御番号;//上の制御計測.制御番号;
    //    //var 親の演算計測 = this.演算計測;
    //    var 子演算計測=new 仮想ノード(制御番号,Name,Value);
    //    if(制御計測 is null)
    //        制御計測=子演算計測;
    //    var 親演算計測=this.演算計測;
    //    if(親演算計測 is not null)
    //        親演算計測.List子演算.Add(子演算計測);
    //    this.演算計測=子演算計測;
    //    List計測.Add(子演算計測);
    //    //return 親の演算計測;
    //    return (親演算計測,子演算計測);
    //}
    //private 計測 計測しない前処理演算制御(Expression 対象ノード,string Name,string Value = "") {
    //    var 子演算計測 =new 計測しない(++this.制御番号,Name,Value);
    //    this.制御計測=子演算計測;
    //    var 親演算計測 = this.演算計測!;
    //    親演算計測.List子演算.Add(子演算計測);
    //    this.演算計測=子演算計測;
    //    List計測.Add(子演算計測);
    //    //return 今回の計測;
    //    return 親演算計測;
    //}
    //private (計測 親の演算計測,計測する左辺値 今回の計測する左辺値) 計測する前処理左辺値(Expression Expression,string Name,string Value = ""){
    //    var 上の制御計測=this.制御計測;
    //    int 制御番号;
    //    if(上の制御計測 is null)
    //        制御番号=++this.制御番号;
    //    else
    //        制御番号=上の制御計測.制御番号;
    //    var 親の演算計測 = this.演算計測;
    //    var 今回の計測する左辺値=new 計測する左辺値(制御番号,Name,Value,Expression){};
    //    if(親の演算計測 is null) {
    //        親の演算計測=this.演算計測=今回の計測する左辺値;
    //    } else {
    //        親の演算計測.List子演算.Add(今回の計測する左辺値);
    //    }
    //    this.演算計測=今回の計測する左辺値;
    //    List計測.Add(今回の計測する左辺値);
    //    return (親の演算計測,今回の計測する左辺値);
    //}
    private(計測? 親演算計測,計測 子演算計測) 計測する前処理演算(string Name,string? Value=null){
        ref var 制御計測=ref this.制御計測;
        int 制御番号;
        if(制御計測 is null)
            制御番号=++this.制御番号;
        else
            制御番号=this.制御番号;//上の制御計測.制御番号;
        //var 親の演算計測 = this.演算計測;
        var 子演算計測=new 計測(計測Maneger,制御番号,Name,Value);
        if(制御計測 is null)
            制御計測=子演算計測;
        var 親演算計測=this.演算計測;
        if(親演算計測 is not null)
            親演算計測.List子演算.Add(子演算計測);
        this.演算計測=子演算計測;
        //return 親の演算計測;
        return (親演算計測,子演算計測);
    }
    //private 計測? 計測する前処理演算(object 対象ノード,string Name,string? Value = null){
    //    if(Value is null) Value="";
    //    var 制御計測= this.制御計測;
    //    int 制御番号;
    //    if(制御計測 is null)
    //        制御番号=++this.制御番号;
    //    else
    //        制御番号=制御計測.制御番号;
    //    var 子演算計測 =new 計測しない(制御番号,Name,Value);
    //    if(制御計測 is null)
    //        this.制御計測=子演算計測;
    //    var 親演算計測 = this.演算計測;
    //    if(親演算計測 is not null)
    //        親演算計測.List子演算.Add(子演算計測);
    //    this.演算計測=子演算計測;
    //    List計測.Add(子演算計測);
    //    return 親演算計測;
    //}
    //private(計測? 親演算計測,計測Label 子演算計測) 計測Label前処理演算(string Name,string? Value=null) {
    //    var 制御計測= this.制御計測;
    //    int 制御番号;
    //    if(制御計測 is null)
    //        制御番号=++this.制御番号;
    //    else
    //        制御番号=制御計測.制御番号;
    //    var 子演算計測 =new 計測Label(制御番号,Name,Value);
    //    if(制御計測 is null)
    //        this.制御計測=子演算計測;
    //    var 親演算計測 = this.演算計測;
    //    if(親演算計測 is not null)
    //        親演算計測.List子演算.Add(子演算計測);
    //    this.演算計測=子演算計測;
    //    List計測.Add(子演算計測);
    //    return (親演算計測,子演算計測);
    //}
    //private (計測? 親演算計測,計測 子演算計測)計測する前処理Binding(MemberBinding MemberBinding,string Name,string Value = "") {
    //    var 上の制御計測= this.制御計測!;
    //    var 親演算計測 = this.演算計測;
    //    var 子演算計測 =new 計測する右辺値(上の制御計測.制御番号,Name,Value);
    //    this.制御計測=子演算計測;
    //    if(親演算計測 is null) {
    //        親演算計測=this.演算計測=子演算計測;
    //    } else {
    //        親演算計測.List子演算.Add(子演算計測);
    //    }
    //    this.演算計測=子演算計測;
    //    List計測.Add(子演算計測);
    //    return (親演算計測,子演算計測);
    //}
    private Expression 計測しない後処理((計測? 親演算計測,計測 子演算計測)親子演算計測,Expression Expression1){
        this.演算計測=親子演算計測.親演算計測;
        return Expression1;
    }
    private Expression 計測する後処理((計測? 親演算計測,計測 子演算計測)親子演算計測,Expression Expression1){
        this.演算計測=親子演算計測.親演算計測;
        //return Expression1;
        return Expression.Block(
            Expression.Call(
                Expression.Constant(
                    親子演算計測.子演算計測
                ),
                計測.Reflection.Count,
                Expression.Constant(親子演算計測.子演算計測.NameValue)
            ),
            Expression1
        );
    }
    //private Expression 計測する後処理左辺値((計測 親の演算計測,計測する左辺値 今回の計測する左辺値)ヘッダ,Expression Expression1){
    //    this.演算計測=ヘッダ.今回の計測する左辺値;
    //    var 計測Expression = Expression.Constant(ヘッダ.今回の計測する左辺値);
    //    var Start=Expression.Call(
    //        計測Expression,
    //        計測する左辺値.Reflection.Start
    //    );
    //    return Expression.Call(
    //        Start,
    //        this.作業配列.MakeGenericMethod(計測する左辺値.Reflection.StopReturnRef,Expression1.Type),
    //        Expression1
    //    );
    //}
    private 計測? ルート演算計測;
    private 計測? _演算計測;
    private 計測? 演算計測{
        get=>this._演算計測;
        set{
            this._演算計測=value;
            if(this.ルート演算計測 is null) this.ルート演算計測=value;
        }
    }
    private 計測? 制御計測;
    protected override Expression Call(MethodCallExpression MethodCall0){
        Debug.Assert(MethodCall0.Method is DynamicMethod||MethodCall0.Method.DeclaringType is not null,"MethodCall0_Method.DeclaringType != null");
        var MethodCall0_Method = MethodCall0.Method;
        (計測? 親演算計測,計測 子演算計測) 親子演算計測;
        if(ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall0_Method))) {
            親子演算計測=this.計測する前処理演算(MethodCall0_Method.Name);
        } else {
            var sb=new StringBuilder();
            if(MethodCall0_Method.DeclaringType is not null) {
                TypeString(sb,MethodCall0_Method.DeclaringType);
                sb.Append('.');
            }
            sb.Append(MethodCall0_Method.Name);
            ParameterString(sb,MethodCall0_Method);
            親子演算計測=this.計測する前処理演算(nameof(ExpressionType.Call),
                sb.ToString()
                
            );
        }
        MethodCallExpression MethodCall1;
        if(MethodCall0_Method.IsStatic){
            MethodCall1=Expression.Call(
                MethodCall0_Method,
                this.TraverseExpressions(MethodCall0.Arguments)
            );
        } else if(MethodCall0.Arguments.Count==0){
            MethodCall1=Expression.Call(
                this.Traverse(MethodCall0.Object),
                MethodCall0_Method
            );
        } else{
            MethodCall1=Expression.Call(
                this.Traverse(MethodCall0.Object),
                MethodCall0_Method,
                this.TraverseExpressions(MethodCall0.Arguments)
            );
        }
        return this.計測する後処理(親子演算計測,MethodCall1);
    }
    private Expression 共通Binary(BinaryExpression Binary0){
        (計測? 親演算計測,計測 子演算計測) 親子演算計測;
        if(Binary0.Method is not null)
            親子演算計測=this.計測する前処理演算(Binary0.NodeType.ToString(),Binary0.Method.Name);
        else
            親子演算計測=this.計測する前処理演算(Binary0.NodeType.ToString());
        return this.計測する後処理(
            親子演算計測,
            Expression.MakeBinary(
                Binary0.NodeType,
                this.Traverse(Binary0.Left),
                this.Traverse(Binary0.Right),
                Binary0.IsLiftedToNull,
                Binary0.Method,
                this.TraverseNullable(Binary0.Conversion) as LambdaExpression
            )
        );
    }
    private Expression 共通Unary(UnaryExpression Unary0) {
        (計測? 親演算計測,計測 子演算計測) 親子演算計測;
        if(Unary0.Method is not null) {
            if(Unary0.Type!=Unary0.Operand.Type) {
                親子演算計測=this.計測する前処理演算(Unary0.NodeType.ToString(),$"{Unary0.Type.FullName} {Unary0.Method.Name}");
            } else {
                親子演算計測=this.計測する前処理演算(Unary0.NodeType.ToString(),Unary0.Method.Name);
            }
        } else{
            if(Unary0.Type!=Unary0.Operand.Type){
                親子演算計測=this.計測する前処理演算(Unary0.NodeType.ToString(),Unary0.Type.FullName);
            } else{
                親子演算計測=this.計測する前処理演算(Unary0.NodeType.ToString());
            }
        }
        return this.計測する後処理(
            親子演算計測,
            Expression.MakeUnary(
                Unary0.NodeType,
                this.Traverse(Unary0.Operand),
                Unary0.Type,
                Unary0.Method
            )
        );
    }
    private Expression 共通TypeBinary(TypeBinaryExpression TypeBinary0,string op) {
        var TypeOperand = TypeBinary0.TypeOperand;
        return this.計測する後処理(
            this.計測する前処理演算(op,TypeOperand.Name),
            Expression.TypeIs(
                this.Traverse(TypeBinary0.Expression),
                TypeOperand
            )
        );
    }
    /// <summary>
    /// MemberAccessで使われる
    /// </summary>
    /// <param name="Expression0"></param>
    /// <returns></returns>
    private Expression? PointerTraverseNullable(Expression? Expression0) {
        if(Expression0 is null)
            return null;
        var NodeType = Expression0.NodeType;
        if(NodeType==ExpressionType.Parameter) {
            if(Expression0.Type.IsValueType) {
                var Parameter0 = (ParameterExpression)Expression0;
                this.計測する前処理演算(nameof(ExpressionType.Parameter),Parameter0.Name);
                return Expression0;
            }
            return Expression0;
        }
        if(NodeType==ExpressionType.MemberAccess) {
            var Member0 = (MemberExpression)Expression0;
            Expression? Member1_Expression;
            if(Expression0.Type.IsValueType)
                Member1_Expression=this.PointerTraverseNullable(Member0.Expression);
            else
                Member1_Expression=this.TraverseNullable(Member0.Expression);
            return Expression.MakeMemberAccess(Member1_Expression,Member0.Member);
        }
        return this.Traverse(Expression0);
    }
    protected override Expression Assign(BinaryExpression Assign0){
        Debug.Assert(
            Assign0.Left.NodeType==ExpressionType.Parameter||
            Assign0.Left.NodeType==ExpressionType.MemberAccess||
            Assign0.Left.NodeType==ExpressionType.ArrayIndex||
            Assign0.Left.NodeType==ExpressionType.Index
        );
#if プロファイルにAssignを対応
        var Assign0_Left = Assign0.Left;
        var Binary0_Left_NodeType = Assign0_Left.NodeType;
        var 親子演算計測=this.計測する前処理演算(nameof(ExpressionType.Assign));
        var Assign1_Left =Assign0_Left;
        計測? 親演算計測;
        switch(Binary0_Left_NodeType){
            case ExpressionType.Parameter: {
                //.NETのバージョンによっては変数の参照も計測できるかもしれない。
                var Parameter0 = (ParameterExpression)Assign0_Left;
                //Assign1_Left=this.Parameter(Parameter0);
                親演算計測=計測する前処理演算(nameof(ExpressionType.Parameter), Parameter0.Name);
                break;
            }
            case ExpressionType.MemberAccess:{
                var Member0 = (MemberExpression)Assign0_Left;
                親演算計測=計測する前処理演算(nameof(ExpressionType.MemberAccess),Member0.Member.Name);
                //Assign1_Left=Expression.MakeMemberAccess(
                //    this.PointerTraverseNullable(Member0.Expression),
                //    Member0.Member
                //);
                Assign1_Left=this.MemberAccess(Member0);
                break;
            }
            default:{
                Debug.Assert(ExpressionType.Index==Binary0_Left_NodeType);
                //Int32[]
                //ArrayAccess
                var Index0 = (IndexExpression)Assign0_Left;
                親演算計測= 計測する前処理演算(nameof(ExpressionType.Index));
                //Assign1_Left=Expression.MakeIndex(
                //    this.Traverse(Index0.Object),
                //    Index0.Indexer,
                //    this.TraverseExpressions(Index0.Arguments)
                //);
                Assign1_Left=this.Index(Index0);
                break;
            }
        }
        this.演算計測=親演算計測;
        var Assign1_Right=this.Traverse(Assign0.Right);
        this.演算計測=親子演算計測.親演算計測;
        return Expression.Block(
            //Expression.Assign(
            //    Expression.Call(
            //        Expression.Constant(子演算計測),
            //        this.作業配列.MakeGenericMethod(
            //            計測.Reflection.Assign,
            //            Binary1_Left.Type
            //        ),
            //        Binary1_Left
            //    ),
            //    this.Traverse(Assign0.Right)
            //)
            Expression.Call(
                //Expression.Constant(親演算計測),
                this.作業配列.MakeGenericMethod(
                    計測.Reflection.Assign,
                    Assign1_Left.Type
                ),
                Expression.Constant(親演算計測),
                Assign1_Left,
                Assign1_Right
            )
        );
#else
        var Assign0_Left = Assign0.Left;
        var Binary0_Left_NodeType = Assign0_Left.NodeType;
        var 親子演算計測=this.計測する前処理演算(nameof(ExpressionType.Assign));
        var Assign1_Left =Assign0_Left;
        計測? 親演算計測;
        switch(Binary0_Left_NodeType){
            case ExpressionType.Parameter: {
                //.NETのバージョンによっては変数の参照も計測できるかもしれない。
                var Parameter0 = (ParameterExpression)Assign0_Left;
                //Assign1_Left=this.Parameter(Parameter0);
                親演算計測=計測する前処理演算(nameof(ExpressionType.Parameter), Parameter0.Name);
                break;
            }
            case ExpressionType.MemberAccess:{
                var Member0 = (MemberExpression)Assign0_Left;
                親演算計測=計測する前処理演算(nameof(ExpressionType.MemberAccess),Member0.Member.Name);
                //Assign1_Left=Expression.MakeMemberAccess(
                //    this.PointerTraverseNullable(Member0.Expression),
                //    Member0.Member
                //);
                Assign1_Left=this.MemberAccess(Member0);
                break;
            }
            default:{
                Debug.Assert(ExpressionType.Index==Binary0_Left_NodeType);
                //Int32[]
                //ArrayAccess
                var Index0 = (IndexExpression)Assign0_Left;
                親演算計測= 計測する前処理演算(nameof(ExpressionType.Index));
                //Assign1_Left=Expression.MakeIndex(
                //    this.Traverse(Index0.Object),
                //    Index0.Indexer,
                //    this.TraverseExpressions(Index0.Arguments)
                //);
                Assign1_Left=this.Index(Index0);
                break;
            }
        }
        this.演算計測=親演算計測;
        var Assign1_Right=this.Traverse(Assign0.Right);
        this.演算計測=親子演算計測.親演算計測;
        return Expression.Block(
            Expression.Call(
                Expression.Constant(
                    親子演算計測.子演算計測
                ),
                計測.Reflection.Count,
                Expression.Constant(親子演算計測.子演算計測.NameValue)
            ),
            Expression.Assign(
                Assign1_Left,
                Assign1_Right
            )
        );
        //return Expression.Assign(
        //    Assign1_Left,
        //    Assign1_Right
        //);
#endif
        //↓Assignの情報取得
        計測? 計測する前処理演算(string Name,string ? Value=null){
            if(Value is null) Value="";
            ref var 制御計測 = ref this.制御計測;
            int 制御番号;
            if(制御計測 is null)
                制御番号=++this.制御番号;
            else
                制御番号=this.制御番号;//上の制御計測.制御番号;
            var 親の演算計測 = this.演算計測;
            var 子演算計測 = new 計測(計測Maneger,制御番号,Name,Value);
            if(制御計測 is null)
                制御計測=子演算計測;
            var 親演算計測 = this.演算計測;
            if(親演算計測 is not null)
                親演算計測.List子演算.Add(子演算計測);
            this.演算計測=子演算計測;
            return 親の演算計測;
            //return 子演算計測;
        }
    }
    protected override Expression Add(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression AddChecked(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression ArrayIndex(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression Subtract(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression SubtractChecked(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression Multiply(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression MultiplyChecked(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression Divide(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression Modulo(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression And(BinaryExpression Binary) => this.共通Binary(Binary);
    protected override Expression Or(BinaryExpression Binary) => this.共通Binary(Binary);
    protected override Expression ExclusiveOr(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression Equal(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression NotEqual(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression GreaterThan(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression LessThan(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression GreaterThanOrEqual(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression LessThanOrEqual(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression LeftShift(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression RightShift(BinaryExpression Binary0) => this.共通Binary(Binary0);
    //protected override Expression AndAlso(BinaryExpression Binary) => this.共通Binary(Binary);
    //protected override Expression OrElse(BinaryExpression Binary) => this.共通Binary(Binary);
    protected override Expression Power(BinaryExpression Binary0) => this.共通Binary(Binary0);
    protected override Expression TypeAs(UnaryExpression Unary) => this.共通Unary(Unary);
    //protected override Expression TypeEqual(TypeBinaryExpression TypeBinary0) => this.共通TypeBinary(TypeBinary0,"type_equal");
    protected override Expression TypeIs(TypeBinaryExpression TypeBinary0)=> this.共通TypeBinary(TypeBinary0,"is");
    protected override Expression OnesComplement(UnaryExpression Unary0) => this.共通Unary(Unary0);
    protected override Expression ArrayLength(UnaryExpression Unary0) => this.共通Unary(Unary0);
    protected override Expression Convert(UnaryExpression Unary0) => this.共通Unary(Unary0);
    protected override Expression ConvertChecked(UnaryExpression Unary0) => this.共通Unary(Unary0);
    protected override Expression Increment(UnaryExpression Unary) => this.共通Unary(Unary);
    protected override Expression Decrement(UnaryExpression Unary0) => this.共通Unary(Unary0);
    //protected override Expression PostIncrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
    //protected override Expression PostDecrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
    //protected override Expression PreIncrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
    //protected override Expression PreDecrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
    protected override Expression Negate(UnaryExpression Unary0) => this.共通Unary(Unary0);
    protected override Expression NegateChecked(UnaryExpression Unary) => this.共通Unary(Unary);
    protected override Expression New(NewExpression New0){
        var sb = new StringBuilder();
        if(New0.Type.IsAnonymous()){
            sb.Append('{');
            foreach(var Parameter in New0.Type.GetConstructors()[0].GetParameters()){
                TypeString(sb,Parameter.ParameterType);
                sb.Append(' ').Append(Parameter.Name).Append(',');
            }
            sb[^1]='}';
            return this.計測する後処理(
                this.計測する前処理演算(nameof(ExpressionType.New),sb.ToString()),
                Expression.New(
                    New0.Constructor,
                    this.TraverseExpressions(New0.Arguments),
                    New0.Members
                )
            );
        } else {
            var Constructor = New0.Constructor;
            if(Constructor is not null) {
                TypeString(sb,New0.Type);
                ParameterString(sb,Constructor);
                NewExpression New1;
                if(New0.Members is not null){
                    New1=Expression.New(
                        Constructor,
                        this.TraverseExpressions(New0.Arguments),
                        New0.Members
                    );
                } else{
                    New1=Expression.New(
                        Constructor,
                        this.TraverseExpressions(New0.Arguments)
                    );
                }
                return this.計測する後処理(
                    this.計測する前処理演算(nameof(ExpressionType.New),sb.ToString()),
                    New1
                );
            } else {
                return this.計測する後処理(
                    this.計測する前処理演算(nameof(ExpressionType.New),sb.ToString()),
                    Expression.New(New0.Type)
                );
            }
        }
    }
    protected override Expression NewArrayBounds(NewArrayExpression NewArray0) {
        var NewArray0_Expressions = NewArray0.Expressions;
        var ElementType=NewArray0.Type.GetElementType()!;
        return this.計測する後処理(
            this.計測する前処理演算(nameof(ExpressionType.NewArrayBounds),$"{ElementType.Name}[{NewArray0_Expressions.Count}]"),
            Expression.NewArrayBounds(
                ElementType,
                this.TraverseExpressions(NewArray0_Expressions)
            )
        );
    }
    protected override Expression NewArrayInit(NewArrayExpression NewArray0) {
        Debug.Assert(NewArray0.Type.IsArray);
        var NewArray0_Expressions = NewArray0.Expressions;
        var ElementType = NewArray0.Type.GetElementType()!;
        return this.計測する後処理(
            this.計測する前処理演算(nameof(ExpressionType.NewArrayInit),$"{ElementType.Name}[{NewArray0_Expressions.Count}]"),
            Expression.NewArrayInit(
                ElementType,
                this.TraverseExpressions(NewArray0_Expressions)
            )
        );
    }
    #if false
    protected override Expression Conditional(ConditionalExpression Conditional0) {
        //var 親演算計測=this.演算計測;
        var 親子演算計測 = this.計測する前処理演算(Conditional0,nameof(ExpressionType.Conditional));
        var List子演算 = 親子演算計測.子演算計測.List子演算;
        var Conditional0_IfTest = Conditional0.Test;
        var Conditional0_IfTrue = Conditional0.IfTrue;
        var Conditional0_IfFalse = Conditional0.IfFalse;
        var 演算計測 = this.演算計測!;

        var Conditional1_Test = this.Traverse(Conditional0_IfTest);
        var Test計測0 = List子演算[^1];
        Test計測0.Value=$"{Conditional0_IfTest}";
        var Test計測1 = new 仮想ノード(Test計測0,"→");
        //Debug.Assert(this.制御番号==BeginTest.制御番号);
        //EndTest.Value=BeginTest.Value;
        Test計測0.List子演算.Add(Test計測1);
        var EndCondition = new 仮想ノード("←");
        //var List制御計測 = this.List制御計測;
        var Conditional1_IfTrue = TrueFalse共通(Conditional0_IfTrue,"True:");
        var Conditional1_IfFalse = TrueFalse共通(Conditional0_IfFalse,"False:");
        //List制御計測.Add(this.制御計測=EndCondition);
        List子演算.Add(EndCondition);
        EndCondition.制御番号=++this.制御番号;
        this.制御計測=EndCondition;
        return this.計測する後処理(
            親子演算計測,
            Expression.Condition(
                Conditional1_Test,
                Conditional1_IfTrue,
                Conditional1_IfFalse,
                Conditional0.Type
            )
        );
        Expression TrueFalse共通(Expression Conditional0_IfTrueFalse,string Name) {
            this.演算計測=演算計測;
            this.制御計測=null;
            var Conditional1_IfTrueFalse = this.Traverse(Conditional0_IfTrueFalse);
            var TrueFalse計測0 = List子演算[^1];
            var TrueFalse計測1 = new 仮想ノード(TrueFalse計測0,"→");
            TrueFalse計測0.List子演算.Add(TrueFalse計測1);
            計測.接続(Test計測1,TrueFalse計測0);
            計測.接続(TrueFalse計測1,EndCondition);
            //List制御計測.Add(TrueFalse計測0);
            return Conditional1_IfTrueFalse;
            //return Conditional2_IfTrueFalse;
        }
    }
#else
    protected override Expression Conditional(ConditionalExpression Conditional0){
        var 親子演算計測= this.計測する前処理演算(nameof(ExpressionType.Conditional));
        var List子演算 = 親子演算計測.子演算計測.List子演算;
        var 演算計測 = this.演算計測!;
        var Conditional0_IfTest = Conditional0.Test;
        var Conditional0_IfTrue = Conditional0.IfTrue;
        var Conditional0_IfFalse = Conditional0.IfFalse;

        var Conditional1_Test = this.Traverse(Conditional0_IfTest);
        var Test計測0 = List子演算[^1];
        Test計測0.Value=$"{Conditional0_IfTest}";
        var Test計測1 = new 計測(計測Maneger,Test計測0,"→");
        //Debug.Assert(this.制御番号==BeginTest.制御番号);
        //EndTest.Value=BeginTest.Value;
        Test計測0.List子演算.Add(Test計測1);
        var EndCondition = new 計測(計測Maneger,"←");
        //var List制御計測 = this.List制御計測;
        var Conditional1_IfTrue = TrueFalse共通(Conditional0_IfTrue,"[EndIfTrue]");
        var Conditional1_IfFalse = TrueFalse共通(Conditional0_IfFalse,"[EndIfFalse]");
        //List制御計測.Add(this.制御計測=EndCondition);
        List子演算.Add(EndCondition);
        EndCondition.制御番号=++this.制御番号;
        //this.制御計測=EndCondition;
        //this.演算計測=親子演算計測.親演算計測;
        //this.演算計測=親子演算計測.親演算計測;
        return this.計測する後処理(
            親子演算計測,
            Expression.Condition(
                //Expression.Block(
                //    Expression.Call(
                //        Expression.Constant(Test計測1),
                //        計測.Reflection.Count,
                //        Expression.Constant(親子演算計測.子演算計測.NameValue)
                //    ),
                //    Conditional1_Test
                //),
                Conditional1_Test,
                Conditional1_IfTrue,
                Conditional1_IfFalse,
                Conditional0.Type
            )
        );
        //return Expression.Block(
        //    Expression.Call(
        //        Expression.Constant(親子演算計測.子演算計測),
        //        計測.Reflection.Count
        //    ),
        //    Expression.Call(
        //        Expression.Constant(EndCondition),
        //        計測.Reflection.Count
        //    ),
        //    Expression.Condition(
        //        Expression.Block(
        //            Expression.Call(
        //                Expression.Constant(Test計測1),
        //                計測.Reflection.Count
        //            ),
        //            Conditional1_Test
        //        ),
        //        Conditional1_IfTrue,
        //        Conditional1_IfFalse,
        //        Conditional0.Type
        //    )
        //);
        Expression TrueFalse共通(Expression Conditional0_IfTrueFalse,string Name) {
            this.演算計測=演算計測;
            this.制御計測=null;
            var Conditional1_IfTrueFalse = this.Traverse(Conditional0_IfTrueFalse);
            var TrueFalse計測0=List子演算[^1];
            if(TrueFalse計測0.Value is null)
                TrueFalse計測0.Value="←";
            else
                TrueFalse計測0.Value+='←';
            var TrueFalse計測1 = new 計測(計測Maneger,TrueFalse計測0,Name+"→");
            TrueFalse計測0.List子演算.Add(TrueFalse計測1);
            計測.接続(Test計測1,TrueFalse計測0);
            計測.接続(TrueFalse計測1,EndCondition);
            //List制御計測.Add(TrueFalse計測0);
            //return Conditional1_IfTrueFalse;
            return Expression.Block(
                Expression.Call(
                    Expression.Constant(TrueFalse計測1),
                    計測.Reflection.Count,
                    Expression.Constant(Name)
                ),
                Conditional1_IfTrueFalse
            );
            //return Conditional2_IfTrueFalse;
        }
    }
#endif
    /*
    protected override Expression Switch(SwitchExpression Switch0){
        var 計測=this.計測する前処理演算(nameof(ExpressionType.Switch));
        var List計測=this.演算計測!.List子演算;
        var Switch0_SwitchValue=Switch0.SwitchValue;
        var Switch1_SwitchValue=this.Traverse(Switch0_SwitchValue);
        var BeginSwitchValue = List計測[^1];
        BeginSwitchValue.Value=$"begin switch {BeginSwitchValue}";
        var EndSwitchValue = new 計測(計測Maneger,BeginSwitchValue,"EndSwitchValue");
        BeginSwitchValue.List子演算.Add(EndSwitchValue);
        var sb=new StringBuilder();
        var Switch0_Cases=Switch0.Cases;
        var Switch0_Cases_Count=Switch0_Cases.Count;
        var Switch1_Cases=new SwitchCase[Switch0_Cases_Count];
        //var End_Switch=new 計測する{親コメント="end switch"};
        for(var a=0;a<Switch0_Cases_Count;a++){
            var Case0=Switch0_Cases[a];
            sb.Append("case ");
            foreach(var TestValue in Case0.TestValues){
                sb.Append(TestValue);
                sb.Append(',');
            }
            sb[^1]=':';
            var Case0_Body=Case0.Body;
            sb.Append(Case0_Body);
            //List辺.Add(this.兄弟直前計測=new 計測する{親コメント=sb.ToString()});
            var Case1_Body=this.Traverse(Case0_Body);
            var Body=List計測[^1];
            Switch1_Cases[a]=Expression.SwitchCase(Case1_Body,Case0.TestValues);
            Profiling.計測.接続(BeginSwitchValue,Body);
            sb.Clear();
        }
        var Switch0_DefaultBody=Switch0.DefaultBody;
        var Switch1_DefaultBody=this.Traverse(Switch0_DefaultBody);
        var DefaultBody=List計測[^1];
        Profiling.計測.接続(BeginSwitchValue,DefaultBody);
        return this.計測する後処理(
            計測,
            Expression.Switch(
                Switch1_SwitchValue,
                Switch1_DefaultBody,
                Switch0.Comparison,
                Switch1_Cases
            )
        );
    }
    */
    protected override Expression Switch(SwitchExpression Switch0) {
        var 親子演算計測 = this.計測する前処理演算(nameof(ExpressionType.Switch));
        var List子計測 = this.演算計測!.List子演算;
        //var 演算計測 = this.演算計測!;
        var Switch0_SwitchValue = Switch0.SwitchValue;
        var Switch1_SwitchValue = this.Traverse(Switch0_SwitchValue);
        var 計測0SwitchValue = List子計測[^1];
        //計測0SwitchValue.Value=$"switch value";
        var 計測1SwitchValue = new 計測(計測Maneger,計測0SwitchValue,"→");
        計測0SwitchValue.List子演算.Add(計測1SwitchValue);
        var 計測EndSwitch = new 計測(計測Maneger,"←");
        計測.接続(計測1SwitchValue,計測EndSwitch);
        var sb = new StringBuilder();
        var Switch0_Cases = Switch0.Cases;
        var Switch0_Cases_Count = Switch0_Cases.Count;
        var Switch1_Cases = new SwitchCase[Switch0_Cases_Count];
        //var End_Switch=new 計測する{親コメント="end switch"};
        for(var a = 0;a<Switch0_Cases_Count;a++) {
            var Case0 = Switch0_Cases[a];
            sb.Append("case ");
            foreach(var TestValue in Case0.TestValues) {
                sb.Append(TestValue);
                sb.Append(',');
            }
            sb[^1]=':';
            sb.Append('←');
            //List辺.Add(this.兄弟直前計測=new 計測する{親コメント=sb.ToString()});
            var 計測case = new 計測(計測Maneger,sb.ToString());
            計測.接続(計測1SwitchValue,計測case );
            List子計測.Add(計測case);
            this.演算計測=計測case;
            //計測.接続(計測1SwitchValue,計測case);
            var Case1_Body = this.Traverse(Case0.Body);
            var List子計測0 = this.演算計測!.List子演算;
            var 計測0Case_Body = List子計測0[^1];
            計測0Case_Body.Value=sb.ToString();
            Switch1_Cases[a]=Expression.SwitchCase(Case1_Body,Case0.TestValues);
            var 計測EndCase = new 計測(計測Maneger,計測0Case_Body,"→");
            List子計測0.Add(計測EndCase);
            //計測.接続(計測1Case_Body,計測EndSwitch);
            //計測.接続(計測0SwitchValue,計測case);
            計測.接続(計測EndCase,計測EndSwitch);
            sb.Clear();
        }
        var 計測default= new 計測(計測Maneger,"default:←");
        計測.接続(計測1SwitchValue,計測default);
        List子計測.Add(計測default);
        this.演算計測=計測default;
        var Switch0_DefaultBody = Switch0.DefaultBody;
        var Switch1_DefaultBody = this.Traverse(Switch0_DefaultBody);
        var List子計測1 = this.演算計測!.List子演算;
        var 計測0DefaultBody = List子計測1[^1];
        計測0DefaultBody.Value="default:←";
        List子計測.Add(計測EndSwitch);
        var 計測1DefaultBody = new 計測(計測Maneger,"→");
        List子計測1.Add(計測1DefaultBody);
        計測.接続(計測1DefaultBody,計測EndSwitch);
        計測EndSwitch.制御番号=++this.制御番号;
        return this.計測する後処理(
            親子演算計測,
            Expression.Switch(
                Switch1_SwitchValue,
                Switch1_DefaultBody,
                Switch0.Comparison,
                Switch1_Cases
            )
        );
        //Expression TrueFalse共通(Expression Conditional0_IfTrueFalse,string Name) {
        //    this.演算計測=演算計測;
        //    this.制御計測=null;
        //    var Conditional1_IfTrueFalse = this.Traverse(Conditional0_IfTrueFalse);
        //    var TrueFalse計測0=List子演算![^1];
        //    if(TrueFalse計測0.Value is null)
        //        TrueFalse計測0.Value="←";
        //    else
        //        TrueFalse計測0.Value+='←';
        //    var TrueFalse計測1 = new 計測(計測Maneger,TrueFalse計測0,Name+"→");
        //    TrueFalse計測0.List子演算.Add(TrueFalse計測1);
        //    計測.接続(SwitchValue計測1,TrueFalse計測0);
        //    計測.接続(TrueFalse計測1,EndSwitch);
        //    //List制御計測.Add(TrueFalse計測0);
        //    //return Conditional1_IfTrueFalse;
        //    return Expression.Block(
        //        Expression.Call(
        //            Expression.Constant(TrueFalse計測1),
        //            親子演算計測.Reflection.Count,
        //            Expression.Constant(Name)
        //        ),
        //        Conditional1_IfTrueFalse
        //    );
        //    //return Conditional2_IfTrueFalse;
        //}
    }
    protected override Expression Try(TryExpression Try0) {
        var 親子計測 = this.計測する前処理演算(nameof(ExpressionType.Try));
        var List子計測=this.演算計測!.List子演算;
        var Try0_Handlers=Try0.Handlers;
        var Try0_Handlers_Count=Try0_Handlers.Count;
        var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
        var Try0_Body=Try0.Body;
        var Try1_Body=this.Traverse(Try0_Body);
        var 計測0Body = List子計測[^1];
        var 計測finally= new 計測(計測Maneger,"finally←");
        //計測0Body.Value=$"Try {計測0Body}";
        //var EndTry = new 計測(計測Maneger,計測0Body,"EndTry");
        //計測0Body.List子演算.Add(EndTry);
        //var List計測0=this.演算計測!.List子演算;
        for(var a=0;a<Try0_Handlers_Count;a++) {
            var 計測catch= new 計測(計測Maneger,++this.制御番号,"catch");
            //計測.接続(計測0Body,計測catch);
            List子計測.Add(計測catch);
            //計測0Body.Value+='→';
            //計測.接続(計測catch,計測finally);
            this.演算計測=計測catch;
            var Try0_Handler=Try0_Handlers[a];
            Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
            var Try0_Handler_Variable=Try0_Handler.Variable;
            CatchBlock Try1_Handler;
            if(Try0_Handler_Variable is not null){
                計測catch.Value=$"{Try0_Handler_Variable.Type.FullName} {Try0_Handler_Variable.Name}";
                var Try1_Handler_Filter=Try0_Handler.Filter;
                if(Try1_Handler_Filter is not null){
                    Try1_Handler_Filter=this.Traverse(Try1_Handler_Filter);
                    //var Handler_Filter=List子計測[^1];
                    計測.接続(this.演算計測.List子演算[^1],計測finally);
                } else{
                    計測.接続(計測catch,計測finally);
                }
                計測.接続(計測0Body,計測catch);
                var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                //var Handler_Body=List子計測[^1];
                //計測.接続(List子計測[^2],Handler_Body);
                Try1_Handler=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
            } else {
                計測catch.Value=$"{Try0_Handler.Test.FullName}";
                var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                var Handler_Body=List子計測[^1];
                //計測.接続(計測0Body,Handler_Body);
                var Try1_Handler_Filter=Try0_Handler.Filter;
                if(Try1_Handler_Filter is not null){
                    Try1_Handler_Filter=this.Traverse(Try1_Handler_Filter);
                    var Handler_Filter=List子計測[^1];
                    計測.接続(Handler_Filter,計測finally);
                    this.演算計測=Handler_Filter;
                    //計測.接続(List子計測[^2],Handler_Filter);
                } else{
                    計測.接続(計測catch,計測finally);
                    this.演算計測=計測catch;
                }
                Try1_Handler=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
            }
            Try1_Handlers[a]=Try1_Handler;
        }
        var Try1_Finally=Try0.Finally;
        if(Try1_Finally is not null){
            計測finally.制御番号=++this.制御番号;
            //var 計測finally= new 計測(計測Maneger,++this.制御番号,"finally←");
            計測0Body.Value+='→';
            計測.接続(計測0Body,計測finally);
            this.演算計測=計測finally;
            Try1_Finally=this.Traverse(Try1_Finally);
            //var Finally=List子計測[^1];
            List子計測.Add(計測finally);
            //計測.接続(List子計測[^2],Finally);
        }
        if(Try0.Fault is not null){
            Debug.Assert(Try1_Finally is null);
            var Try0_Fault=Try0.Fault;
            var Try1_Fault=this.Traverse(Try0_Fault);
            var Fault=List子計測[^1];
            計測.接続(List子計測[^2],Fault);
            return this.計測する後処理(
                親子計測,
                Expression.TryFault(Try1_Body,Try1_Fault)
            );
        } else{
            return this.計測する後処理(
                親子計測,
                Expression.TryCatchFinally(Try1_Body,Try1_Finally,Try1_Handlers)
            );
        }
    }
    protected override Expression Goto(GotoExpression Goto0) {
        
        ////var 親計測Expression = Expression.Constant(this.演算計測!.親演算);
        ////var Start=Expression.Call(親計測Expression,計測.Reflection.Start);
        //MethodCallExpression? 親Stop;
        //if(this.演算計測!.親演算 is null) 親Stop=null;
        //else
        //    親Stop=Expression.Call(
        //        Expression.Constant(this.演算計測!.親演算),
        //        計測.Reflection.Count
        //    );
        ////return Expression.Block(Start,Expression1,Stop);


        //ref var ref_制御計測=ref this.制御計測;
        //int 制御番号1;
        //if(ref_制御計測 is null)
        //    制御番号1=++this.制御番号;
        //else
        //    制御番号1=this.制御番号;//上の制御計測.制御番号;
        ////var 親の演算計測 = this.演算計測;
        //var 子演算計測=new 計測(計測Maneger,制御番号1,Goto0.Kind.ToString(),"");
        //if(ref_制御計測 is null)
        //    ref_制御計測=子演算計測;
        //var 親演算計測=this.演算計測;
        //if(親演算計測 is not null)
        //    親演算計測.List子演算.Add(子演算計測);
        //this.演算計測=子演算計測;
        //List計測.Add(子演算計測);
        //return 親の演算計測;
        //var 親子演算計測 = ((計測? 親演算計測,計測 子演算計測))(親演算計測,子演算計測);
        var Goto0_Target = Goto0.Target;
        var 親子演算計測= this.計測する前処理演算(Goto0.Kind.ToString(),$"({Goto0.Value}){Goto0_Target.Name}→");














        //var 親子演算計測 =new 計測Label(++this.制御番号,Goto0.Kind.ToString(),$"({Goto0.Value}){Goto0_Target.Name}→");
        //var List子演算 = 親子演算計測.子演算計測.List子演算;
        //this.演算計測=子演算計測;
        var Goto1_Value=this.TraverseNullable(Goto0.Value);
        //var Goto1_Value演算 = this.演算計測.List子演算[^1];
        var ジャンプ元計測 = 親子演算計測.子演算計測;//this.制御計測!;
        if(!Dictionary_LabelTarget_辺.TryGetValue(Goto0_Target,out var ジャンプ先計測Label)) {
            ジャンプ先計測Label=new 計測(計測Maneger,-1,"Goto時に定義された仮のLabel",$"({Goto0.Value}){Goto0_Target.Name}");
            Dictionary_LabelTarget_辺.Add(Goto0_Target,ジャンプ先計測Label);
        }
        this.演算計測=親子演算計測.親演算計測;
        計測.接続(ジャンプ元計測,ジャンプ先計測Label);
        this.制御計測=null;
        //var Goto計測Label=new 計測Label(this.制御番号,"aaaa","");
        return this.計測する後処理(
            親子演算計測,
            Expression.MakeGoto(Goto0.Kind,Goto0.Target,Goto1_Value,Goto0.Type)
        );
        //var 計測Expression = Expression.Constant(子演算計測);
        //var Count=Expression.Call(計測Expression,計測.Reflection.Count);
        //if(Goto1_Value is not null)
        //    if(親Stop is null)
        //        Goto1_Value =Expression.Block(Count,Goto1_Value);
        //    else
        //        Goto1_Value =Expression.Block(親Stop,Count,Goto1_Value);
        //else
        //    if(親Stop is null)
        //        Goto1_Value =Count;
        //    else
        //        Goto1_Value =Expression.Block(親Stop,Count);
        //return Expression.MakeGoto(Goto0.Kind,Goto0.Target,Goto1_Value,Goto0.Type);
        /*
        var Goto0_Target=Goto0.Target;
        ref var 制御計測 = ref this.制御計測;
        int 制御番号;
        if(制御計測 is null)
            制御番号=++this.制御番号;
        else
            制御番号=this.制御番号;//上の制御計測.制御番号;
        //var 親の演算計測 = this.演算計測;
        var 子演算計測 = new 計測Label(制御番号,"Label000",$"{Goto0_Target.Name}:←");
        if(制御計測 is null)
            制御計測=子演算計測;
        var 親演算計測 = this.演算計測;
        if(親演算計測 is not null)
            親演算計測.List子演算.Add(子演算計測);
        this.演算計測=子演算計測;
        List計測.Add(子演算計測);
        //return 親の演算計測;
        var 親子演算計測 = (親演算計測, 子演算計測);
        var 計測Expression = Expression.Constant(親子演算計測.子演算計測);
        var Count = Expression.Call(計測Expression,計測Label.Reflection.Count);
        */
    }
    #if false
    protected override Expression Label(LabelExpression Label0) {
        if(Label0.DefaultValue is null) {
            共通();
            return Expression.Label(Label0.Target);
        } else {
            var Label1_DefaultValue = this.Traverse(Label0.DefaultValue);
            共通();
            return Expression.Label(Label0.Target,Label1_DefaultValue);
        }
        void 共通() {
            if(Dictionary_LabelTarget_辺.TryGetValue(Label0.Target,out var 移動先)) {
                //gotoで指定したラベルでまだ定義されてない奴
                //└┐0 goto 下
                //..................
                //┌┘1 下:←ここ
                //移動先.Value=$"{Label0.Target.Name!}({Label0.DefaultValue}):";
                移動先.Name="Label";
                移動先.Value=$"{Label0.Target.Name}:←";
                移動先.制御番号=++this.制御番号;
                this.制御計測=移動先;
                if(this.演算計測 is not null)
                    this.演算計測.List子演算.Add(移動先);
                this.List制御計測.Add(移動先);
                //this.兄弟直前計測=移動先;
            } else {
                //始めて出現。後でgoto命令で飛んでループを形成する
                //var 前処理=this.計測する前処理演算(Label0,nameof(ExpressionType.Label),$"{Label0.Target.Name}:←");
                //var 今回計測=List計測[^1];
                ////├←┐    1 L1:←ここ
                //Dictionary_LabelTarget_辺.Add(Label0.Target,今回計測);
                //this.演算計測=前処理;
                //this.制御計測=今回計測;
                var 前処理 = this.計測する前処理演算(Label0,nameof(ExpressionType.Label),$"{Label0.Target.Name}:←");
                var 今回計測 = List計測[^1];
                //├←┐    1 L1:←ここ
                Dictionary_LabelTarget_辺.Add(Label0.Target,今回計測);
                this.演算計測=前処理.親演算計測;
                this.制御計測=今回計測;
            }
        }
    }
#else
    protected override Expression Label(LabelExpression Label0){
        var Label1_DefaultValue=this.TraverseNullable(Label0.DefaultValue);
        if(Dictionary_LabelTarget_辺.TryGetValue(Label0.Target,out var 移動先)){
            //gotoで指定したラベルでまだ定義されてない奴
            //└┐0 goto 下
            //..................
            //┌┘1 下:←ここ
            //移動先.Value=$"{Label0.Target.Name!}({Label0.DefaultValue}):";
            移動先.Name="Label";
            移動先.Value=$"{Label0.Target.Name}:←";
            移動先.制御番号=++this.制御番号;
            this.制御計測=移動先;
            if(this.演算計測 is not null)
                this.演算計測.List子演算.Add(移動先);
            //this.List制御計測.Add(移動先);
            //this.兄弟直前計測=移動先;
        } else{
            var 子演算計測 =new 計測(計測Maneger,++this.制御番号,nameof(ExpressionType.Label),$"{Label0.Target.Name}:←");
            if(this.制御計測 is null)
                this.制御計測=子演算計測;
            var 親演算計測 = this.演算計測;
            if(親演算計測 is not null)
                親演算計測.List子演算.Add(子演算計測);
            //List計測.Add(子演算計測);
            移動先=子演算計測;
            //var 前処理=List計測[^1];
            //├←┐    1 L1:←ここ
            Dictionary_LabelTarget_辺.Add(Label0.Target,移動先);
            //this.演算計測=前処理;
            this.制御計測=親演算計測;
        }
        var 計測Expression = Expression.Constant(移動先);
        var Count=Expression.Call(
            計測Expression,
            計測.Reflection.Count,
            Expression.Constant(移動先.NameValue)
        );
        if(Label1_DefaultValue is null)
            return Expression.Label(
                Label0.Target,
                Count
            );
        else
            return Expression.Label(
                Label0.Target,
                Expression.Block(
                    Count,
                    Label1_DefaultValue
                )
            );
    }
    #endif
    protected override Expression Constant(ConstantExpression Constant0){
        string Name;
        if(Constant0.Value is null){
            Name="null";
        } else {
            var Constant_Type = Constant0.Type;
            if(Constant_Type==typeof(string)){
                Name="\"" + Constant0.Value + "\"";
            }else if(Constant_Type==typeof(decimal)){
                Name=Constant0.Value + "M";
            }else {
                if(Constant_Type.IsEnum){
                    Constant_Type=Enum.GetUnderlyingType(Constant_Type);
                }
                if(Constant_Type==typeof(sbyte)){
                    Name="(SByte)" + Constant0.Value;
                }else if(Constant_Type==typeof(short)){
                    Name="(Int16)" + Constant0.Value;
                }else if(Constant_Type==typeof(int)){
                    Name=Constant0.Value.ToString()!;
                }else if(Constant_Type==typeof(long)){
                    Name="(Int64)" + Constant0.Value;
                }else if(Constant_Type==typeof(IntPtr)){
                    Name="(IntPtr)" + Constant0.Value;
                }else if(Constant_Type==typeof(byte)){
                    Name="(Byte)" + Constant0.Value;
                }else if(Constant_Type==typeof(ushort)){
                    Name="(UInt16)" + Constant0.Value;
                }else if(Constant_Type==typeof(uint)){
                    Name=Constant0.Value + "u";
                }else if(Constant_Type==typeof(ulong)){
                    Name=Constant0.Value + "ul";
                }else if(Constant_Type==typeof(UIntPtr)){
                    Name="(UIntPtr)" + Constant0.Value;
                }else if(Constant_Type==typeof(bool)){
                    Name=(bool)Constant0.Value ? "true" : "false";
                }else if(Constant_Type==typeof(char)){
                    Name="'" + Constant0.Value + '"';
                }else if(Constant_Type==typeof(float)){
                    Name=Constant0.Value + "f";
                }else if(Constant_Type==typeof(double)){
                    Name=Constant0.Value + "d";
                } else{
                    Name="[Object]";
                }
            }
        }
        var 親の演算計測=this.計測する前処理演算(nameof(ExpressionType.Constant),Name);
        return this.計測する後処理(
            親の演算計測,
            Constant0
        );
    }

    protected override Expression Default(DefaultExpression Default0)=>this.計測する後処理(
        this.計測する前処理演算(nameof(ExpressionType.Default),Default0.Type.Name),
        Default0
    );

    protected override Expression Parameter(ParameterExpression Parameter0)=>this.計測する後処理(
        this.計測する前処理演算(nameof(ExpressionType.Parameter),Parameter0.Name),
        Parameter0
    );
    protected override Expression Index(IndexExpression Index0) => this.計測する後処理(
        this.計測する前処理演算(nameof(ExpressionType.Index)),
        Expression.MakeIndex(
            this.Traverse(Index0.Object),
            Index0.Indexer,
            this.TraverseExpressions(Index0.Arguments)
        )
    );
    protected override Expression Invoke(InvocationExpression Invocation0) => this.計測する後処理(
        this.計測する前処理演算(nameof(ExpressionType.Invoke)),
        Expression.Invoke(
            this.Traverse(Invocation0.Expression),
            this.TraverseExpressions(Invocation0.Arguments)
        )
    );
    protected override Expression MemberAccess(MemberExpression Member0){
        Debug.Assert(Member0.Member.DeclaringType is not null,"Member0_Member.DeclaringType != null");
        var Member0_Member =Member0.Member;
        return this.計測する後処理(
            this.計測する前処理演算(nameof(ExpressionType.MemberAccess),Member0_Member.Name),
            Expression.MakeMemberAccess(
                Member0.Expression is not null?this.Traverse(Member0.Expression):null,
                Member0_Member
            )
        );
    }
    protected override IList<MemberBinding> Bindings(ReadOnlyCollection<MemberBinding> Bindings0) {
        var Bindings0_Count = Bindings0.Count;
        var Bindings1 = new MemberBinding[Bindings0_Count];
        var 変化したか = false;
        for(var a = 0;a < Bindings0_Count;a++) {
            var Binding0 = Bindings0[a];
            switch(Binding0.BindingType) {
                case MemberBindingType.Assignment:{
                    var MemberAssignment=(MemberAssignment)Binding0;
                    var Binding0_Expression = MemberAssignment.Expression;
                    var 前処理データ = this.計測する前処理演算(MemberAssignment.Member.Name);
                    var Binding1_Expression = this.Traverse(Binding0_Expression);
                    Bindings1[a]=Expression.Bind(
                        Binding0.Member,
                        this.計測する後処理(
                            前処理データ,
                            Binding1_Expression
                        )
                    );
                    変化したか=true;
                    break;
                }
                case MemberBindingType.MemberBinding: {
                    var MemberMemberBinding = (MemberMemberBinding)Binding0;
                    var 親計測=this.計測する前処理演算(MemberMemberBinding.Member.Name).親演算計測;
                    var Binding0_Bindings = MemberMemberBinding.Bindings;
                    var Binding1_Bindings = this.Bindings(Binding0_Bindings);
                    if(Binding0_Bindings==Binding1_Bindings){
                        Bindings1[a]=Binding0;
                    } else {
                        Bindings1[a]=Expression.MemberBind(
                            Binding0.Member,
                            Binding1_Bindings
                        );
                        変化したか=true;
                    }
                    this.演算計測=親計測;
                    break;
                }
                case MemberBindingType.ListBinding: {
                    var MemberListBinding0 = (MemberListBinding)Binding0;
                    var MemberListBinding0_Initializers = MemberListBinding0.Initializers;
                    var MemberListBinding0_Initializers_Count = MemberListBinding0_Initializers.Count;
                    var MemberListBinding1_Initializers = new ElementInit[MemberListBinding0_Initializers_Count];
                    var 親計測=this.計測する前処理演算(MemberListBinding0.Member.Name).親演算計測;
                    var 変化したか1 = false;
                    for(var b = 0;b < MemberListBinding0_Initializers_Count;b++) {
                        var MemberListBinding0_Initializer = MemberListBinding0_Initializers[b];
                        var 親計測2=this.計測する前処理演算(MemberListBinding0_Initializer.AddMethod.ToString()).親演算計測;
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
                        this.演算計測=親計測2;
                    }
                    if(変化したか1) {
                        Bindings1[a]=Expression.ListBind(
                            Binding0.Member,
                            MemberListBinding1_Initializers
                        );
                        変化したか=true;
                    } else{
                        Bindings1[a]=MemberListBinding0;
                    }
                    this.演算計測=親計測;
                    break;
                }
                default:
                    throw new NotSupportedException($"{Binding0.BindingType}はサポートされていない");
            }
        }
        return 変化したか ? Bindings1 : Bindings0;
    }

    protected override Expression MemberInit(MemberInitExpression MemberInit0){
        var 前処理データ=this.計測する前処理演算(nameof(ExpressionType.MemberInit));
        var MemberInit0_NewExpression = MemberInit0.NewExpression;
        var 親計測1=this.計測する前処理演算(MemberInit0_NewExpression.Type.Name).親演算計測;
        var MemberInit1_NewExpression = (NewExpression)base.New(MemberInit0_NewExpression);
        this.演算計測=親計測1;
        var MemberInit0_Bindings = MemberInit0.Bindings;
        var MemberInit1_Bindings = this.Bindings(MemberInit0_Bindings);
        return this.計測する後処理(
            前処理データ,
            Expression.MemberInit(
                MemberInit1_NewExpression,
                MemberInit1_Bindings
            )
        );
    }
    protected override Expression Lambda(LambdaExpression Lambda0) {
        var sb = new StringBuilder(Lambda0.Name+"(");
        var Lambda_Parameters = Lambda0.Parameters;
        var Lambda_Parameters_Count = Lambda_Parameters.Count;
        if(Lambda_Parameters_Count > 0) {
            Type文字列(Lambda_Parameters[0].Type,sb);
            sb.Append(' ').Append(Lambda_Parameters[0].Name);
            for(var a = 1;a < Lambda_Parameters_Count;a++) {
                sb.Append(',');
                Type文字列(Lambda_Parameters[a].Type,sb);
                sb.Append(' ').Append(Lambda_Parameters[a].Name);
            }
        }
        sb.Append(")=>");
        var 親計測 = this.計測する前処理演算(nameof(ExpressionType.Lambda),sb.ToString()).親演算計測;
        var Lambda1_Body = this.Traverse(Lambda0.Body);
        this.演算計測=親計測;
        return Expression.Lambda(
            Lambda0.Type,
            Lambda1_Body,
            Lambda0.Parameters
        );
    }
    private static string Type文字列(Type Type){
        var sb=new StringBuilder();
        Type文字列(Type,sb);
        return sb.ToString();
    }
    private static void Type文字列(Type Type,StringBuilder sb){
        if(Type.IsGenericType){
            var Name=Type.Name;
            var Index=Name.LastIndexOf('`');
            if(Index>=0)
                Name=Name[..Index];
            sb.Append(Type.Namespace).Append('.').Append(Name).Append("<");
            foreach(var a in Type.GetGenericArguments()){
                Type文字列(a,sb);
                sb.Append('.');
            }
            sb.Length--;
            sb.Append(">");
        } else{
            sb.Append(Type.Namespace).Append('.').Append(Type.Name);
        }
    }
    protected override Expression ListInit(ListInitExpression ListInit0){
        var 前処理データ = this.計測する前処理演算(nameof(ExpressionType.ListInit),nameof(ExpressionType.ListInit));
        var ListInit0_NewExpression =ListInit0.NewExpression;
        var Name=Type文字列(ListInit0_NewExpression.Type);
        var 親計測1=this.計測する前処理演算(nameof(ExpressionType.New),Name).親演算計測;
        var ListInit1_NewExpression = (NewExpression)base.New(ListInit0_NewExpression);
        this.演算計測=親計測1;
        var ListInit0_Initializers = ListInit0.Initializers;
        var ListInit0_Initializers_Count = ListInit0_Initializers.Count;
        var ListInit1_Initializers = new ElementInit[ListInit0_Initializers_Count];
        for(var a = 0;a<ListInit0_Initializers_Count;a++) {
            var ListInit0_Initializer = ListInit0_Initializers[a];
            var 親計測2=this.計測する前処理演算(nameof(ListInit0_Initializer.AddMethod),ListInit0_Initializer.AddMethod.Name).親演算計測;
            ListInit1_Initializers[a]=Expression.ElementInit(
                ListInit0_Initializer.AddMethod,
                this.TraverseExpressions(ListInit0_Initializer.Arguments)
            );
            this.演算計測=親計測2;
        }
        return this.計測する後処理(
            前処理データ,
            Expression.ListInit(
                ListInit1_NewExpression,
                ListInit1_Initializers
            )
        );
    }
    protected override Expression Loop(LoopExpression Loop0) {
#if false
        var 親子演算計測 = this.計測する前処理演算(Loop0,nameof(ExpressionType.Loop));
        var List子演算 = 親子演算計測.子演算計測.List子演算;
        var Loop0_Body= Loop0.Body;
        this.制御計測=null;
        計測する右辺値? Break=null;
        if(Loop0.BreakLabel is not null){
            Break=new 計測する右辺値(++this.制御番号,"Label",$"{Loop0.BreakLabel.Name}:←");
            Dictionary_LabelTarget_辺[Loop0.BreakLabel]=Break;
        }
        仮想ノード? ContinueLabel=null;
        if(Loop0.ContinueLabel is not null){
            ContinueLabel=new 仮想ノード(++this.制御番号,"Label",$"{Loop0.ContinueLabel.Name}:←");
            Dictionary_LabelTarget_辺[Loop0.ContinueLabel]=ContinueLabel;
            this.制御計測=ContinueLabel;
        }
        var Loop1_Body= this.Traverse(Loop0_Body);
        var Body計測0 = List子演算[^1];
        var Body計測1 = new 仮想ノード(Body計測0,"→");
        List子演算.Add(Body計測1);
        計測.接続(Body計測1,Body計測0);
        var Loop1=Expression.Loop(
            Loop1_Body,
            Loop0.BreakLabel,
            Loop0.ContinueLabel
        );
        if(Loop0.BreakLabel is not null)
            List子演算.Add(Break);
        if(ContinueLabel is not null){
            List子演算.Add(ContinueLabel);
        }
        Body計測0.Value="←";
        this.制御計測=null;
        return this.計測する後処理(親子演算計測,Loop1);
#else
        var 親子演算計測 = this.計測する前処理演算(nameof(ExpressionType.Loop));
        var List子演算 = 親子演算計測.子演算計測.List子演算;
        var Loop0_Body = Loop0.Body;
        this.制御計測=null;
        if(Loop0.ContinueLabel is null)
            this.制御計測=null;
        else{
            var 親子演算計測0=this.計測する前処理演算("ContinueLabel",$"{Loop0.ContinueLabel.Name}:←");
            var 親演算計測=親子演算計測0.親演算計測!;// 計測Maneger[^1];
            //todo nullの時どうしよう
            Dictionary_LabelTarget_辺[Loop0.ContinueLabel]=親演算計測;
            this.制御計測=親演算計測;
        }
        var Loop1_Body = this.Traverse(Loop0_Body);
        //var Body計測0 = List子演算[^1];
        
        //if(Loop0.ContinueLabel is null){
        //    var ループ末尾の暗黙のcontinue=new 仮想ノード(Body計測0,"→");
        //    List子演算.Add(ループ末尾の暗黙のcontinue);
        //    計測.接続(ループ末尾の暗黙のcontinue,Body計測0);
        //    Body計測0.Value="←";
        //}
        var Loop1 = Expression.Loop(
            Loop1_Body,
            Loop0.BreakLabel,
            Loop0.ContinueLabel
        );
        if(Loop0.BreakLabel is null)
            this.制御計測=null;
        else{
            var 移動先=Dictionary_LabelTarget_辺[Loop0.BreakLabel];
            移動先.Name="BrekLabel";
            移動先.Value=$"{Loop0.BreakLabel.Name}:←";
            移動先.制御番号=++this.制御番号;
            this.制御計測=移動先;
            if(this.演算計測 is not null)
                this.演算計測.List子演算.Add(移動先);
            //this.List制御計測.Add(移動先);
        }
        return this.計測する後処理(親子演算計測,Loop1);
#endif
//#if false
//        var Loop0_Body = Loop0.Body;
//        var ContinueLabel=Loop0.ContinueLabel;
//        Expression result;
//        if(ContinueLabel is null) ContinueLabel=Expression.Label("Continue");
//        if(Loop0.BreakLabel is not null)
//            result = Expression.Block(
//                Expression.Label(ContinueLabel),
//                Loop0_Body,
//                Expression.Continue(ContinueLabel),
//                Expression.Label(
//                    Loop0.BreakLabel,
//                    Expression.Default(
//                        Loop0.BreakLabel.Type
//                    )
//                )
//            );
//        else
//            result = Expression.Block(
//                Expression.Label(ContinueLabel),
//                Loop0_Body,
//                Expression.Continue(ContinueLabel)
//            );
//        var Loop1_Body=this.Traverse(result);
//        return Loop1_Body;
//#else
//        var Loop0_Body = Loop0.Body;
//        var ContinueLabel=Loop0.ContinueLabel;
//        Expression result;
//        if(ContinueLabel is null) ContinueLabel=Expression.Label("Continue");
//        if(Loop0.BreakLabel is not null)
//            result = Expression.Block(
//                Expression.Label(ContinueLabel),
//                Loop0_Body,
//                Expression.Continue(ContinueLabel),
//                Expression.Label(
//                    Loop0.BreakLabel,
//                    Expression.Default(
//                        Loop0.BreakLabel.Type
//                    )
//                )
//            );
//        else
//            result = Expression.Block(
//                Expression.Label(ContinueLabel),
//                Loop0_Body,
//                Expression.Continue(ContinueLabel)
//            );
//        return result;
//#endif
    }
}
//20231228 1509