#pragma warning disable CA1822 // Mark members as static
using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using System.Xml.Linq;
using e = System.Linq.Expressions;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
using System.Globalization;
using System.Text;
using LinqDB.Optimizers.VoidExpressionTraverser;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Optimizers.Comparer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.SqlServer.Types;
using LinqDB.Helpers;
using System.Net;
//using ColumnReferenceExpression=Microsoft.SqlServer.TransactSql.ScriptDom.ColumnReferenceExpression;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal partial class 変換_TSqlFragmentからExpression{
    static 変換_TSqlFragmentからExpression(){
    }
    private readonly Stack<e.LabelTarget>Stack_WHILEのBreak先=new();
    private readonly List<e.MemberExpression> List_Schema=new();
    private readonly List<e.ParameterExpression> List_Parameter=new();
    private readonly List<e.ParameterExpression> List_定義型TableVariable = new();
    private readonly List<(e.ParameterExpression Variable, string[] Names)> List_匿名型TableVariable = new();
    private readonly List<e.ParameterExpression> List_ScalarVariable = new();
    private static MethodInfo? FindFunction(Type Schema,string Name)=>
        Schema.GetMethod(Name,BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.IgnoreCase);
    private e.MemberExpression? FindSchema(string Name) {
        var List_Schema = this.List_Schema;
        foreach(var Schema in List_Schema) {
            var Member = Schema.Member;
            if(string.Equals(Name,Member.Name,StringComparison.OrdinalIgnoreCase)) return Schema;
        }
        return null;
    }
    private System.Collections.Generic.IEnumerable<e.ParameterExpression>Variables=>this.List_ScalarVariable.Concat(
        this.List_定義型TableVariable
    ).Concat(
        this.List_匿名型TableVariable.Select(p=>p.Variable)
    );
    private void AddScalarVariable(e.ParameterExpression Variable) {
        this.List_ScalarVariable.Add(Variable);
    }
    private void AddTableVariable(e.ParameterExpression Variable) {
        this.List_定義型TableVariable.Add(Variable);
    }
    private void AddTableVariable(e.ParameterExpression Variable,string[]Names) {
        this.List_匿名型TableVariable.Add((Variable, Names));
    }
    private static e.ParameterExpression? PrivateFindVariable(List<e.ParameterExpression>List,string Name) {
        foreach(var a in List)
            if(a.Name==Name)
                return a;
        return null;
    }
    private e.ParameterExpression FindParameterVariable(string Name) {
        e.ParameterExpression? result;
        if((result=PrivateFindVariable(this.List_Parameter,Name)) is not null) return result;
        if((result=PrivateFindVariable(this.List_ScalarVariable,Name)) is not null) return result;
        if((result=PrivateFindVariable(this.List_定義型TableVariable,Name)) is not null) return result;
        foreach(var Parameter in this.List_Parameter)
            if(Parameter.Name==Name)
                return Parameter;
        throw new NotSupportedException($"{Name}というパラメーターはない");
    }
    private e.ParameterExpression FindScalarVariable(string Name) {
        return PrivateFindVariable(this.List_ScalarVariable,Name)??throw new NotSupportedException($"{Name}というスカラ変数はない");
    }
    private e.ParameterExpression FindTableVariable(string Name) {
        var result=PrivateFindVariable(this.List_定義型TableVariable,Name);
        if(result is not null) return result;
        foreach(var 匿名型TableVariable in this.List_匿名型TableVariable)
            if(匿名型TableVariable.Variable.Name==Name)
                return 匿名型TableVariable.Variable;
        throw new NotSupportedException($"{Name}というテーブル変数はない");
    }
    private Type Nullableまたは参照型(Type Type)=>Type.Nullableまたは参照型(this.作業配列.Types1);
    private e.Expression ConvertNullable(e.Expression Expression) {
        var Type = Expression.Type;
        if(!Type.IsValueType)return Expression;//文字列など参照型はNullable<>にしない
        if(Type.IsNullable()) return Expression;
        return e.Expression.Convert(Expression,this.作業配列.MakeGenericType(typeof(Nullable<>),Type));
    }
    private readonly e.MemberExpression Encoding=e.Expression.Property(null,typeof(Encoding).GetProperty("Unicode"));
    private readonly MethodInfo Encoding_GetString=typeof(Encoding).GetMethod("GetString",new Type[]{typeof(byte[]) })!;
    private readonly MethodInfo Encoding_GetBytes=typeof(Encoding).GetMethod("GetBytes",new Type[]{typeof(string) })!;
    private e.Expression Booleanを返すComparer(e.Expression Left,e.Expression Right,Func<e.Expression,e.Expression,e.Expression> 非NullのExpression) {
        //SQL                   C#
        //0<=0=true
        //0<=1=true
        //1<=0=false
        //1<=1=true
        //0<=n=false
        //1<=n=false
        //n<=0=false
        //n<=1=false
        //n<=n=false
        if(Left.Type.IsNullable()) {
            e.Expression test = e.Expression.Property(Left,"HasValue");
            Left = Common.GetValueOrDefault(Left);
            if(Right.Type.IsNullable()) {
                test = e.Expression.AndAlso(test,e.Expression.Property(Right,"HasValue"));
                Right = GetValueOrDefault(Right);
            }
            if(Left.Type==typeof(SqlHierarchyId)){

            }
            var ifTrue = 非NullのExpression(Left,Right);
            return e.Expression.AndAlso(test,ifTrue);
        } else if(Right.Type.IsNullable()) {
            var test = e.Expression.Property(Right,"HasValue");
            Right = GetValueOrDefault(Right);
            var ifTrue = 非NullのExpression(Left,Right);
            return e.Expression.AndAlso(test,ifTrue);
        } else {
            return 非NullのExpression(Left,Right);
        }
    }
    private e.Expression NULL_AndAlso(e.Expression Left,e.Expression Right) {
        //SQL                   C#
        //false and false=false
        //false and true =false
        //true  and false=false
        //true  and true =true
        //false and null =false
        //true  and null =false null
        //null  and false=false
        //null  and true =false null
        //null  and null =false null
        if(Left.Type.IsNullable())Left=e.Expression.AndAlso(e.Expression.Property(Left,"HasValue"),GetValueOrDefault(Left));
        if(Right.Type.IsNullable())Right=e.Expression.AndAlso(e.Expression.Property(Right,"HasValue"),GetValueOrDefault(Right));
        return e.Expression.AndAlso(Left,Right);
    }
    private e.Expression NULL_OrElse(e.Expression Left,e.Expression Right) {
        //SQL                   C#
        //false or false=false
        //false or true =true
        //true  or false=true
        //true  or true =true
        //false or null =false null
        //true  or null =true  true
        //null  or false=false null
        //null  or true =true  true
        //null  or null =false null
        //var Result = e.Expression.OrElse(Left,Right);
        if(Left.Type.IsNullable())Left=e.Expression.AndAlso(e.Expression.Property(Left,"HasValue"),GetValueOrDefault(Left));
        if(Right.Type.IsNullable())Right=e.Expression.AndAlso(e.Expression.Property(Right,"HasValue"),GetValueOrDefault(Right));
        return e.Expression.OrElse(Left,Right);
    }
    private enum 種類{ Nullable,ValueType,Reference}
    private e.Expression NULLを返す1(e.Expression arg0,Func<e.Expression,e.Expression> 非NullのExpression){
        Debug.Assert(arg0.Type.IsValueType);
        e.Expression Condition;
        if(arg0.Type.IsNullable()){
            Condition=e.Expression.Property(arg0,"HasValue");
            arg0=GetValueOrDefault(arg0);
        }else return 非NullのExpression(arg0);
        var Result0=非NullのExpression(arg0);
        var Result0_Type=this.作業配列.MakeGenericType(typeof(Nullable<>),Result0.Type);
        Debug.Assert(Result0_Type.IsValueType);
        return e.Expression.Condition(Condition,e.Expression.Convert(Result0,Result0_Type),e.Expression.Constant(null,Result0_Type));
    }
    private e.Expression NULLを返す2(e.Expression arg0,e.Expression arg1,Func<e.Expression,e.Expression,e.Expression> 非NullのExpression){
        e.Expression Condition;
        if(arg0.Type.IsNullable()){
            Condition=e.Expression.Property(arg0,"HasValue");
            arg0=GetValueOrDefault(arg0);
            if(arg1.Type.IsNullable()){
                Condition=e.Expression.AndAlso(Condition,e.Expression.Property(arg1,"HasValue"));
                arg1=GetValueOrDefault(arg1);
            }
        }else{
            if(arg1.Type.IsNullable()){
                Condition=e.Expression.Property(arg1,"HasValue");
                arg1=GetValueOrDefault(arg1);
            }else{
                return 非NullのExpression(arg0,arg1);
            }
        }
        var Result0=非NullのExpression(arg0,arg1);
        var Result0_Type=this.作業配列.MakeGenericType(typeof(Nullable<>),Result0.Type);
        Debug.Assert(Result0_Type.IsValueType);
        return e.Expression.Condition(Condition,e.Expression.Convert(Result0,Result0_Type),e.Expression.Constant(null,Result0_Type));
    }
    private e.Expression NULLを返す3(e.Expression arg0,e.Expression arg1,e.Expression arg2,Func<e.Expression,e.Expression,e.Expression,e.Expression> 非NullのExpression){
        Debug.Assert(arg0.Type.IsValueType&&arg1.Type.IsValueType&&arg2.Type.IsValueType);
        e.Expression Condition;
        if(arg0.Type.IsNullable()){
            Condition=e.Expression.Property(arg0,"HasValue");
            arg0=GetValueOrDefault(arg0);
            if(arg1.Type.IsNullable()){
                Condition=e.Expression.AndAlso(Condition,e.Expression.Property(arg1,"HasValue"));
                arg1=GetValueOrDefault(arg1);
                if(arg2.Type.IsNullable()){
                    Condition=e.Expression.AndAlso(Condition,e.Expression.Property(arg2,"HasValue"));
                    arg2=GetValueOrDefault(arg2);
                }
            }
        }else{
            if(arg1.Type.IsNullable()){
                Condition=e.Expression.Property(arg1,"HasValue");
                arg1=GetValueOrDefault(arg1);
                if(arg2.Type.IsNullable()){
                    Condition=e.Expression.AndAlso(Condition,e.Expression.Property(arg2,"HasValue"));
                    arg2=GetValueOrDefault(arg2);
                }
            }else{
                if(arg2.Type.IsNullable()){
                    Condition=e.Expression.Property(arg2,"HasValue");
                    arg2=GetValueOrDefault(arg2);
                } else { 
                    return 非NullのExpression(arg0,arg1,arg2);
                }
            }
        }
        var Result0=非NullのExpression(arg0,arg1,arg2);
        var Result0_Type=this.作業配列.MakeGenericType(typeof(Nullable<>),Result0.Type);
        Debug.Assert(Result0_Type.IsValueType);
        return e.Expression.Condition(Condition,e.Expression.Convert(Result0,Result0_Type),e.Expression.Constant(null,Result0_Type));
    }
    private static e.Expression GetValueOrDefault(e.Expression 入力)=>入力.GetValueOrDefault();
    private static Type DataTypeReferenceからTypeに変換(DataTypeReference x){
        var DBType=x.Name.BaseIdentifier.Value;
        return CommonLibrary.SQLのTypeからTypeに変換(DBType);
    }
    private static readonly Type[] Types1=new Type[1];
    private static Type[] Types設定(Type Type0){
        Types1[0]=Type0;
        return Types1;
    }
    private static readonly Type[] Types2=new Type[2];
    private static Type[] Types設定(Type Type0,Type Type1){
        Types2[0]=Type0;
        Types2[1]=Type1;
        return Types2;
    }
    private class 判定_集約関数があるか:VoidTSqlFragmentTraverser.VoidTSqlFragmentTraverser{
        public 判定_集約関数があるか():base(new Sql160ScriptGenerator()){}
        private bool 集約関数があるか;
        public bool 実行(TSqlFragment x){
            this.集約関数があるか=false;
            this.TSqlFragment(x);
            return this.集約関数があるか;
        }
        protected override void FunctionCall(FunctionCall x){
            switch(x.FunctionName.Value.ToUpperInvariant()){
                case "COUNT":
                case "AVG":
                case "MAX":
                case "MIN":
                case "SUM":
                    this.集約関数があるか=true;
                    break;
                default:base.FunctionCall(x);break;
            }
        }
    }
    private readonly 判定_集約関数があるか _判定_集約関数があるか=new();
    public string SQL取得(TSqlFragment x){
        this.ScriptGenerator.GenerateScript(x,out var SQL);
        return SQL;
    }
    private readonly 作業配列 作業配列;
    private readonly 変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1;
    private readonly 変換_旧Expressionを新Expression1 変換_旧Expressionを新Expression1;
    //private readonly TSql150Parser Parser=new TSql150Parser(false);
    private readonly 取得_OuterPredicate_InnerPredicate_プローブビルド 取得_OuterPredicate_InnerPredicate_プローブビルド;
    //private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    private readonly SqlScriptGenerator ScriptGenerator;
    private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    private readonly 取得_出力Table 判定指定Table;
    private readonly List<e.Expression>出力TableExpressions=new List<e.Expression>();
    //private readonly List<e.Expression>指定TableExpressions=new List<e.Expression>();
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="作業配列"></param>
    /// <param name="取得_OuterPredicate_InnerPredicate_プローブビルド"></param>
    /// <param name="ExpressionEqualityComparer"></param>
    /// <param name="変換_旧Parameterを新Expression1"></param>
    /// <param name="変換_旧Expressionを新Expression1"></param>
    /// <param name="判定_指定Parametersが存在しない"></param>
    /// <param name="ScriptGenerator"></param>
    public 変換_TSqlFragmentからExpression(作業配列 作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド 取得_OuterPredicate_InnerPredicate_プローブビルド,ExpressionEqualityComparer ExpressionEqualityComparer,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1,変換_旧Expressionを新Expression1 変換_旧Expressionを新Expression1,判定_指定Parameters無 判定_指定Parametersが存在しない,SqlScriptGenerator ScriptGenerator){
        this.作業配列=作業配列;
        this.変換_旧Parameterを新Expression1=変換_旧Parameterを新Expression1;
        this.変換_旧Expressionを新Expression1=new 変換_旧Expressionを新Expression1(作業配列,new ExpressionReferenceEqualityComparer());
        this.取得_OuterPredicate_InnerPredicate_プローブビルド=取得_OuterPredicate_InnerPredicate_プローブビルド;
        this.ScriptGenerator=ScriptGenerator;
        this._StackSubquery単位の情報=new(ExpressionEqualityComparer);
        this.ExpressionEqualityComparer=ExpressionEqualityComparer;
        this.判定指定Table=new(ExpressionEqualityComparer,this.出力TableExpressions);
    }
    //private readonly static e.MemberExpression CultureInfo_CurrentCulture=e.Expression.Property(
    //    null,
    //    Reflection.CultureInfo.CurrentCulture
    //);
    private static readonly e.MethodCallExpression TABLE_DEE=e.Expression.Call(
        null,
        Reflection.Container.TABLE_DEE
    );
    [DebuggerDisplay("DatabaseSchemaTable_Expression={Dictionary_DatabaseSchemaTable_ColumnExpression.Count}")]
    private struct Subquery単位の情報{
        /// <summary>
        /// FROM,JOINに列挙したテーブルを参照するExpression
        /// 例、SELECT FROM A,B,C Xだとしたら{"x.Item1","x.Item2.Item1","x.Item2.Item2"}
        /// left joinでDefaultIfEmptyでnullが返された場合nullを返すようにConditionalで囲うため
        /// </summary>
        public readonly List<e.Expression> List_TableExpression=new();
        /// <summary>
        /// FROMのサブクエリを呼び出した後に格納されていることを期待する要素の別名。
        /// 例、SELECT A,B,C FROM T Xだとしたら{"X.A","X.B","X.C"}
        /// SelectScalaExpressionで追加される
        /// </summary>
        public readonly List<string> List_ColumnAlias=new();
        /// <summary>
        /// FROMのサブクエリを呼び出した後に格納されていることを期待する要素の式。
        /// 例 SELECT 1+2 A,3+4 Bだとしたら{1+2,3+4}
        /// SelectScalaExpressionで追加される
        /// ValueTupleでNewするで単体で使われるので(String,Expression)に一体化しない
        /// </summary>
        public readonly List<e.Expression> List_ColumnExpression=new();
        ///// <summary>
        ///// FROMのサブクエリを呼び出した後に格納されていることを期待する要素の別名。
        ///// 例、SELECT 1+X.A A,B=X.B+4 FROM T Xだとしたら{("A",1+X.A),("B",X.B+4)}
        ///// </summary>
        //public readonly List<(String ColumnAlias,Expressions.Expression Expression)> List_ColumnAlias_ColumnExpression=new();
        /// <summary>
        /// GROUP BYの式
        /// </summary>
        public readonly List<e.Expression> List_GroupByExpression=new();
        /// <summary>
        ///[[データベース.]スキーマ.]表名からExpressionを表す。
        /// </summary>
        public readonly SortedDictionary<string,e.Expression?> Dictionary_DatabaseSchemaTable_TableExpression=new(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        ///[[[データベース.]スキーマ.]表名.]列名からExpressionを表す。
        /// </summary>
        public readonly SortedDictionary<string,e.Expression?> Dictionary_DatabaseSchemaTable_ColumnExpression=new(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        ///[表名エイリアス].*から列名の集合を表す。
        /// </summary>
        public readonly SortedDictionary<string,string[]> Dictionary_TableAlias_ColumnAliases=new(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// *から列名の集合を表す。
        /// </summary>
        public readonly List<string> List_アスタリスクColumnAlias=new();
        /// <summary>
        /// *から列式の集合を表す。
        /// </summary>
        public readonly List<e.Expression> List_アスタリスクColumnExpression=new();
        //public readonly SortedDictionary<String,String[]> Dictionary_ColumnAliases=new(StringComparer.OrdinalIgnoreCase);
        public bool 集約関数の内部か;
        public e.Expression? 集約関数のSource;
        public e.ParameterExpression? 集約関数のParameter;
        public Subquery単位の情報(ExpressionEqualityComparer ExpressionEqualityComparer){
            this.集約関数の内部か=false;
            this.集約関数のSource=null;
            this.集約関数のParameter=null;
        }
        /// <summary>
        ///[[[データベース.]スキーマ.]表名.]列名からExpressionを表す。
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public e.Expression this[string Key]=>this.Dictionary_DatabaseSchemaTable_ColumnExpression[Key]!;
        public void Clear(){
            //this.List_TableExpression.Clear();
            this.Dictionary_DatabaseSchemaTable_TableExpression.Clear();
            this.Dictionary_DatabaseSchemaTable_ColumnExpression.Clear();
            this.Dictionary_TableAlias_ColumnAliases.Clear();
            this.List_ColumnAlias.Clear();
            this.List_ColumnExpression.Clear();
            this.List_GroupByExpression.Clear();
            this.List_アスタリスクColumnAlias.Clear();
            this.List_アスタリスクColumnExpression.Clear();
            //this.Dictionary_With名_Set_ColumnAliases.Clear();
            this.集約関数の内部か=false;
            this.集約関数のSource=null;
            this.集約関数のParameter=null;
        }
    }
    [DebuggerDisplay("index={index}")]
    private sealed class StackSubquery単位の情報{
        private const int 要素数=100;
        private readonly Subquery単位の情報[] ArraySubquery単位の情報=new Subquery単位の情報[要素数];
        public StackSubquery単位の情報(ExpressionEqualityComparer ExpressionEqualityComparer){
            var ArraySubquery単位の情報=this.ArraySubquery単位の情報;
            for(var a=0;a<要素数;a++){
                ArraySubquery単位の情報[a]=new(ExpressionEqualityComparer);
            }
        }
        private int index;
        public e.Expression this[string Key]{
            get{
                var ArraySubquery単位の情報=this.ArraySubquery単位の情報;
                for(var a = this.index;a>=0;a--)
                    if(ArraySubquery単位の情報[a].Dictionary_DatabaseSchemaTable_ColumnExpression.TryGetValue(Key,out var Value))return Value!;
                throw new KeyNotFoundException($"{Key}に対応するExpressionが存在しなかった");
            }
        }
        public void Clear(){
            this.index=0;
            this.ArraySubquery単位の情報[0].Clear();
        }
        public void Push(){
            this.index++;
            this.ArraySubquery単位の情報[this.index].Clear();
        }
        public void Pop(){
            Debug.Assert(this.index>=0);
            this.index--;
        }
        public ref Subquery単位の情報 RefPeek=>ref this.ArraySubquery単位の情報[this.index];
    }
    private readonly StackSubquery単位の情報 _StackSubquery単位の情報;
    private ref Subquery単位の情報 RefPeek=>ref this._StackSubquery単位の情報.RefPeek;
    /// <summary>
    /// WITH共通部分式
    /// </summary>
    public readonly SortedDictionary<string,(e.ParameterExpression Set,string[]ColumnAliases)> Dictionary_With名_Set_ColumnAliases=new(StringComparer.OrdinalIgnoreCase);
    //GROUP BYで指定した式がSELECTでに現れたときにKeyのメンバーとして指定するようにする。
    //SELECT SUM(T1.F1),SUM(T2.F2),T1.F3,T2.F4
    //FROM T T1,T T2
    //GROUP BY T1.F3,T2.F4
    //↓
    //SELECT SUM(ge.T1.F1),SUM(ge.T2.F2),ge.T1.F3,ge.T2.F4
    //FROM(
    //    SELECT T1.F1,T2.F2,T1.F3,T2.F4
    //    FROM T T1,T T2
    //)ge
    //GROUP BY ge.T1.F3,ge.T2.F4
    //↓
    //T.SelectMany(t=>T,(o,i)=>(o,i)
    //.GroupBy(g=>(g.Item1.F3,g.Item2.F4))
    //.Select(ge=>(ge.Sum(gs=>gs.Item1.F1),ge.Sum(gs=>gs.Item2.F2),ge.Key.Item1.F3,ge.Key.Item2.F4))
    //private Expressions.ParameterExpression ContainerParameter=null!;
    private e.Expression Container=null!;
    private Type ContainerType=null!;
    private int 番号;
    ///// <summary>
    ///// SQLからLINQの式木を作る。
    ///// </summary>
    ///// <param name="TSqlFragment"></param>
    ///// <returns></returns>
    //public Expressions.LambdaExpression 実行<TContainer>(TSqlFragment TSqlFragment){
    //    this.番号=0;
    //    this._StackSubquery単位の情報.Clear();
    //    var Container=this.Container=Expressions.Expression.Parameter(typeof(TContainer),nameof(this.Container));
    //    this.ContainerType=typeof(TContainer);
    //    return Expressions.Expression.Lambda<Func<Object,Object>>(
    //        this.Private実行(TSqlFragment),
    //        this.作業配列.Parameters設定(Container)
    //    );
    //}
    //public Expressions.Expression 実行(TSqlFragment TSqlFragment,Type Type){
    //    this.番号=0;
    //    this._StackSubquery単位の情報.Clear();
    //    this.Container=Expressions.Expression.Parameter(Type,nameof(this.Container));
    //    this.ContainerType=Type;
    //    return Expressions.Expression.Lambda<Func<Object>>(
    //        this.Private実行(TSqlFragment),
    //        Array.Empty<Expressions.ParameterExpression>()
    //    );
    //}
    //private Boolean ScalarSubquery内部か;
    private e.Expression Private実行(e.Expression Container,TSqlFragment x){
        this.Container=Container;
        var Parameter_Type=Container.Type;
        this.ContainerType=Parameter_Type;
        this.番号=0;
        //this._StackSubquery単位の情報.Clear();
        //this.Dictionary_With名_Set_ColumnAliases.Clear();
        ////this.Dictionary_Name_ScalarFunction.Clear();
        ////this.Dictionary_Name_Variable.Clear();
        //this.List_Variable.Clear();
        //this.List_Parameter.Clear();
        var List_Schema=this.List_Schema;
        List_Schema.Clear();
        foreach(var Property in Parameter_Type.GetProperties(BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly)){ 
            var MemberExpression=e.Expression.Property(Container,Property);
            //Dictionary_Name_Schema.Add(Property.DeclaringType.Namespace+'.'+Property_Name,MemberExpression);
            //Dictionary_Name_Schema.Add(Property_Name,MemberExpression);
            List_Schema.Add(MemberExpression);
        }
        return this.Private実行(x);
    }
    /// <summary>
    /// サーバーで使う
    /// </summary>
    /// <param name="Container"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public e.Expression 実行(Databases.Container Container,TSqlFragment x)=>this.Private実行(e.Expression.Constant(Container),x);
    /// <summary>
    /// クライアントで使う
    /// </summary>
    /// <param name="Parameter"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public e.Expression 実行(e.ParameterExpression Parameter,TSqlFragment x)=>this.Private実行(Parameter,x);
    public e.Expression Private実行(TSqlFragment x){
        var Result=this.TSqlFragment(x);
        if(Result.Type.IsValueType){
            //Result=Expressions.Expression.Convert(Result,typeof(Object));
        }
        return Result;
    }
    /// <summary>
    /// 2つのオペランドの型を合わせる。Nullableは存在しない前提。
    /// </summary>
    /// <param name="Left"></param>
    /// <param name="Right"></param>
    /// <exception cref="NotSupportedException"></exception>
    private (e.Expression Left,e.Expression Right)Convertデータ型を合わせるNullableは想定しない(e.Expression Left,e.Expression Right) {
        var Left_Type = Left.Type;
        var Right_Type = Right.Type;
        if(Left_Type==Right_Type) return(Left,Right);
        Debug.Assert(!Left_Type.IsNullable()&&!Right_Type.IsNullable());
        if     (typeof(object)==Left_Type ){
            return ConvertLeftRight(Left,Right,Right_Type);
        } else if(typeof(object)==Right_Type){
            return ConvertLeftRight(Left,Right,Left_Type);
        } else if(typeof(byte)==Left_Type) { 
            //if(typeof(byte   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            if(typeof(short  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            if(typeof(int    )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            if(typeof(long   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(long   ));
            if(typeof(float  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(double )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(decimal)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(bool   )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1,Constant_0));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Byte.Parse_s,Right));
            if(typeof(byte[] )==Right_Type)return(Left,e.Expression.ArrayIndex(Right,Constant_0));
        } else if(typeof(short)==Left_Type) { 
            if(typeof(byte   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            //if(typeof(short  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            if(typeof(int    )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            if(typeof(long   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(long   ));
            if(typeof(float  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(double )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(decimal)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(bool   )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1,Constant_0));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Int16.Parse_s,Right));
            if(typeof(byte[] )==Right_Type)return(Left,e.Expression.Call(Reflection.BitConverter.ToInt16,Right,Constant_0));
        } else if(typeof(int)==Left_Type) { 
            if(typeof(byte   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            if(typeof(short  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            //if(typeof(int    )==Right_Type)return ConvertLeftRight(Left,Right,typeof(int    ));
            if(typeof(long   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(long   ));
            if(typeof(float  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(double )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(decimal)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(bool   )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1,Constant_0));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Int32.Parse_s,Right));
            if(typeof(byte[] )==Right_Type)return(Left,e.Expression.Call(Reflection.BitConverter.ToInt32,Right,Constant_0));
        } else if(typeof(long)==Left_Type) { 
            if(typeof(byte   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(long   ));
            if(typeof(short  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(long   ));
            if(typeof(int    )==Right_Type)return ConvertLeftRight(Left,Right,typeof(long   ));
            //if(typeof(long   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(long   ));
            if(typeof(float  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(double )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(decimal)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(bool   )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1L,Constant_0L));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Int64.Parse_s,Right));
            if(typeof(byte[] )==Right_Type)return(Left,e.Expression.Call(Reflection.BitConverter.ToInt64,Right,Constant_0));
        } else if(typeof(float)==Left_Type) { 
            if(typeof(byte   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(float  ));
            if(typeof(short  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(float  ));
            if(typeof(int    )==Right_Type)return ConvertLeftRight(Left,Right,typeof(float  ));
            if(typeof(long   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(float  ));
            //if(typeof(float  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(double )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(decimal)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(bool   )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1F,Constant_0F));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Single.Parse_s,Right));
        } else if(typeof(double)==Left_Type) { 
            if(typeof(byte   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(short  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(int    )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(long   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(float  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            //if(typeof(double )==Right_Type)return ConvertLeftRight(Left,Right,typeof(double ));
            if(typeof(decimal)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(bool   )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1D,Constant_0D));
            //else if(typeof(Boolean)==Right_Type                                                                                                             )Left=e.Expression.NotEqual(Left,Constant_0D);
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Double.Parse_s,Right));
        } else if(typeof(decimal)==Left_Type) { 
            if(typeof(byte  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(short )==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(int   )==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(long  )==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(float )==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(double)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            //if(typeof(decimal)==Right_Type)return ConvertLeftRight(Left,Right,typeof(decimal));
            if(typeof(bool  )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1M,Constant_0M));
            if(typeof(string)==Right_Type)return(Left,e.Expression.Call(Reflection.Decimal.Parse_s,Right));
        } else if(typeof(bool)==Left_Type) { 
            if(typeof(byte   )==Right_Type)return(Convert必要なら(e.Expression.Condition(Left,Constant_1 ,Constant_0 ),Right_Type),Right);
            if(typeof(short  )==Right_Type)return(Convert必要なら(e.Expression.Condition(Left,Constant_1 ,Constant_0 ),Right_Type),Right);
            if(typeof(int    )==Right_Type)return(Convert必要なら(e.Expression.Condition(Left,Constant_1 ,Constant_0 ),Right_Type),Right);
            if(typeof(long   )==Right_Type)return(Convert必要なら(e.Expression.Condition(Left,Constant_1L,Constant_0L),Right_Type),Right);
            if(typeof(float  )==Right_Type)return(Convert必要なら(e.Expression.Condition(Left,Constant_1F,Constant_0F),Right_Type),Right);
            if(typeof(double )==Right_Type)return(Convert必要なら(e.Expression.Condition(Left,Constant_1D,Constant_0D),Right_Type),Right);
            if(typeof(decimal)==Right_Type)return(Convert必要なら(e.Expression.Condition(Left,Constant_1M,Constant_0M),Right_Type),Right);
            //if(typeof(bool  )==Right_Type)return(Left,e.Expression.Condition(Right,Constant_1M,Constant_0M));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Boolean.Parse_s,Right));
            if(typeof(byte[] )==Right_Type)return(Left, e.Expression.NotEqual(e.Expression.ArrayIndex(Right,Constant_0),Constant_0));
        } else if(typeof(DateTime)==Left_Type) { 
            if(typeof(short  )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTime.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(int    )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTime.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(long   )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTime.Ticks),Right);
            if(typeof(float  )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTime.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(double )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTime.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(decimal)==Right_Type)return(e.Expression.Property(Left,Reflection.DateTime.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.DateTime.Parse_input,Right));
            //DateTime a;
            //DateTimeOffset b;
            //b=a;
            //a=(DateTime)b;
            //DateTimeOffset.ParseExact(Value,CommonLibrary.日時Formats
            //var x=日時Formats[0];
        } else if(typeof(DateTimeOffset)==Left_Type) { 
            if(typeof(short  )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTimeOffset.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(int    )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTimeOffset.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(long   )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTimeOffset.Ticks),Right);
            if(typeof(float  )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTimeOffset.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(double )==Right_Type)return(e.Expression.Property(Left,Reflection.DateTimeOffset.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(decimal)==Right_Type)return(e.Expression.Property(Left,Reflection.DateTimeOffset.Ticks),this.Convertデータ型を合わせるNullableは想定しない(Right,typeof(long)));
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.DateTimeOffset.Parse_s,Right));
        } else if(typeof(Guid)==Left_Type) { 
            if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.Guid.Parse_s,Right));
        } else if(typeof(string)==Left_Type) { 
            //if(Left is e.ConstantExpression Constant) {
            //    var Value=(string)Constant.Value;
            //    if(typeof(byte)==Right_Type||typeof(short)==Right_Type||typeof(int)==Right_Type){
            //        if     (int .TryParse(Value,out var @int ))return Convertデータ型を合わせるNullableは想定しない(e.Expression.Constant(@int),Right);
            //        else if(long.TryParse(Value,out var @long))return Convertデータ型を合わせるNullableは想定しない(e.Expression.Constant(@long),Right);
            //    }else if(typeof(float)==Right_Type||typeof(double)==Right_Type){
            //        if     (float .TryParse(Value,out var @float ))return Convertデータ型を合わせるNullableは想定しない(e.Expression.Constant(@float),Right);
            //        else if(double.TryParse(Value,out var @double))return Convertデータ型を合わせるNullableは想定しない(e.Expression.Constant(@double),Right);
            //    }
            //}
            if(typeof(byte          )==Right_Type)return(e.Expression.Call(Reflection.Byte          .Parse_s,Left),Right);
            if(typeof(short         )==Right_Type)return(e.Expression.Call(Reflection.Int16         .Parse_s,Left),Right);
            if(typeof(int           )==Right_Type)return(e.Expression.Call(Reflection.Int32         .Parse_s,Left),Right);
            if(typeof(long          )==Right_Type)return(e.Expression.Call(Reflection.Int64         .Parse_s,Left),Right);
            if(typeof(float         )==Right_Type)return(e.Expression.Call(Reflection.Single        .Parse_s,Left),Right);
            if(typeof(double        )==Right_Type)return(e.Expression.Call(Reflection.Double        .Parse_s,Left),Right);
            if(typeof(decimal       )==Right_Type)return(e.Expression.Call(Reflection.Decimal       .Parse_s,Left),Right);
            if(typeof(bool          )==Right_Type)return(e.Expression.Call(Reflection.Boolean       .Parse_s,Left),Right);
            if(typeof(DateTime      )==Right_Type)return(e.Expression.Call(Reflection.DateTime      .Parse_input,Left),Right);
            if(typeof(DateTimeOffset)==Right_Type)return(e.Expression.Call(Reflection.DateTimeOffset.Parse_s,Left),Right);
            if(typeof(Guid          )==Right_Type)return(e.Expression.Call(Reflection.Guid          .Parse_s,Left),Right);
            if(typeof(byte[]        )==Right_Type)return(e.Expression.Call(this.Encoding,this.Encoding_GetBytes,Left),Right);
        } else if(typeof(byte[]     )==Left_Type) { 
            if(typeof(byte          )==Right_Type)return(e.Expression.ArrayIndex(Left,Constant_0),Right);
            if(typeof(short         )==Right_Type)return(e.Expression.Call(Reflection.BitConverter.ToInt16,Left,Constant_0),Right);
            if(typeof(int           )==Right_Type)return(e.Expression.Call(Reflection.BitConverter.ToInt32,Left,Constant_0),Right);
            if(typeof(long          )==Right_Type)return(e.Expression.Call(Reflection.BitConverter.ToInt64,Left,Constant_0),Right);
            if(typeof(string        )==Right_Type)return(e.Expression.Call(this.Encoding,this.Encoding_GetString,Left),Right);
        }
        throw new NotSupportedException($"{Left_Type.FullName}と{Right_Type.FullName}は演算に出来ない");
        //throw new NotSupportedException($"{Left_Type.FullName}と{Right_Type.FullName}は合わせれない")
        static (e.Expression Left,e.Expression Right)ConvertLeftRight(e.Expression Left,e.Expression Right,Type Type)=>(Convert必要なら(Left,Type),Convert必要なら(Right,Type));
    }
    /// <summary>
    /// 2つのオペランドの型を合わせる。Nullableも適用される。
    /// </summary>
    /// <param name="Left"></param>
    /// <param name="Right"></param>
    /// <exception cref="NotSupportedException"></exception>
    private (e.Expression Left,e.Expression Right)Convertデータ型を合わせるNullableは想定する(e.Expression Left,e.Expression Right) {
        var Left_Type=Left.Type;
        var Right_Type=Right.Type;
        if(Left_Type==Right_Type)return(Left,Right);
        var IsNullable=false;
        e.Expression Left0;
        bool Left_IsNullable;
        if(Left_Type.IsNullable()){
            Left0=GetValueOrDefault(Left);
            Left_IsNullable=true;
            IsNullable=true;
        } else {
            Left0=Left;
            Left_IsNullable=false;
        }
        e.Expression Right0;
        bool Right_IsNullable;
        if(Right_Type.IsNullable()){
            Right0=GetValueOrDefault(Right);
            Right_IsNullable=true;
            IsNullable=true;
        } else {
            Right0=Right;
            Right_IsNullable=false;
        }

        var (Left1,Right1)=this.Convertデータ型を合わせるNullableは想定しない(Left0,Right0);
        Debug.Assert(Left1.Type==Right1.Type);
        if(IsNullable) {
            Left1=this.ConvertNullable(Left1);
            if(Left_IsNullable){
                Left1=e.Expression.Condition(
                    e.Expression.Property(Left,"HasValue"),
                    Left1,
                    e.Expression.Default(Left1.Type)
                );
            }
            Right1=this.ConvertNullable(Right1);
            if(Right_IsNullable){
                Right1=e.Expression.Condition(
                    e.Expression.Property(Right,"HasValue"),
                    Right1,
                    e.Expression.Default(Right1.Type)
                );
            }
        }
        return(Left1,Right1);
    }
    /// <summary>
    /// 代入元の型を代入先の型に変換する
    /// </summary>
    /// <param name="変更元"></param>
    /// <param name="変更先_Type"></param>
    /// <returns></returns>
    private e.Expression Convertデータ型を合わせるNullableは想定しない(e.Expression 変更元,Type 変更先_Type){
        var 変更元_Type=変更元.Type;
        //if(変更先_Type.IsAssignableFrom(変更元_Type))return 変更元;
        if(変更元_Type==変更先_Type)return 変更元;
        if(typeof(object  )==変更元_Type)return Convert必要なら(変更元,変更先_Type);
        if(typeof(object  )==変更先_Type)return e.Expression.Convert(変更元,typeof(object));
        //if(typeof(string  )==変更先_Type)return e.Expression.Call(変更元,Reflection.Object.ToString_);
        if(typeof(byte)==変更元_Type){
            //if(typeof(byte          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(short         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(int           )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(long          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(float         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(double        )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(decimal       )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string        )==変更先_Type)return e.Expression.Call(変更元,Reflection.Byte.ToString_);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .FromBinary,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            if(typeof(DateTimeOffset)==変更先_Type) return e.Expression.Call(Reflection.DateTimeOffset.New_Int64_TimeSpan,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(short)==変更元_Type){
            if(typeof(byte          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            //if(typeof(short         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(int           )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(long          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(float         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(double        )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(decimal       )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string        )==変更先_Type) return e.Expression.Call(変更元,Reflection.Int16.ToString_);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .FromBinary,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            if(typeof(DateTimeOffset)==変更先_Type) return e.Expression.Call(Reflection.DateTimeOffset.New_Int64_TimeSpan,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(int)==変更元_Type){
            if(typeof(byte          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(short         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            //if(typeof(int           )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(long          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(float         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(double        )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(decimal       )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string        )==変更先_Type)return e.Expression.Call(変更元,Reflection.Int32.ToString_);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .FromBinary,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.New_Int64_TimeSpan,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //if(typeof(byte[]        )==変更先_Type)return e.Expression.Call(Reflection.BitConverter.GetBytes_int,変更元);
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(long)==変更元_Type){
            if(typeof(byte          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(short         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(int           )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            //if(typeof(long          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(float         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(double        )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(decimal       )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string        )==変更先_Type)return e.Expression.Call(変更元,Reflection.Int64.ToString_);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .FromBinary,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.New_Int64_TimeSpan,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(float)==変更元_Type){
            if(typeof(byte          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(short         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(int           )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(long          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            //if(typeof(float         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(double        )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(decimal       )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string        )==変更先_Type)return e.Expression.Call(変更元,Reflection.Single.ToString_);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .FromBinary,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.New_Int64_TimeSpan,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(double)==変更元_Type){
            if(typeof(byte          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(short         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(int           )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(long          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(float         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            //if(typeof(double        )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(decimal       )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string        )==変更先_Type)return e.Expression.Call(変更元,Reflection.Double.ToString_);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .FromBinary,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.New_Int64_TimeSpan,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(decimal)==変更元_Type){
            if(typeof(byte          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(short         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(int           )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(long          )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(float         )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(double        )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            //if(typeof(decimal )==変更先_Type)return e.Expression.Convert(変更元,変更先_Type);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string        )==変更先_Type)return e.Expression.Call(変更元,Reflection.Decimal.ToString_);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .FromBinary,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.New_Int64_TimeSpan,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(bool)==変更元_Type){
            if(typeof(byte   )==変更先_Type)return e.Expression.Condition(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_1,変更先_Type),this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更先_Type));
            if(typeof(short  )==変更先_Type)return e.Expression.Condition(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_1,変更先_Type),this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更先_Type));
            if(typeof(int    )==変更先_Type)return e.Expression.Condition(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_1,変更先_Type),this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更先_Type));
            if(typeof(long   )==変更先_Type)return e.Expression.Condition(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_1,変更先_Type),this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更先_Type));
            if(typeof(float  )==変更先_Type)return e.Expression.Condition(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_1,変更先_Type),this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更先_Type));
            if(typeof(double )==変更先_Type)return e.Expression.Condition(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_1,変更先_Type),this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更先_Type));
            if(typeof(decimal)==変更先_Type)return e.Expression.Condition(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_1,変更先_Type),this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更先_Type));
            //if(typeof(bool) == 変更先_Type) return e.Expression.NotEqual(変更元,this.Convertデータ型を合わせるNullableは想定しない(Constant_0,変更元_Type));
            if(typeof(string )==変更先_Type)return e.Expression.Call(変更元,Reflection.Boolean.ToString_);
            //if(typeof(DateTime      ) == 変更先_Type) return e.Expression.New(Reflection.DateTime      .ctor_Int64,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //if(typeof(DateTimeOffset) == 変更先_Type) return e.Expression.New(Reflection.DateTimeOffset.ctor_Int64,this.Convertデータ型を合わせるNullableは想定しない(変更元,typeof(long)));
            //Guid
            //byte[]
            //XDocument
        } else if(typeof(string)==変更元_Type){
            if(typeof(byte          )==変更先_Type)return e.Expression.Call(Reflection.Byte.Parse_s,変更元);
            if(typeof(short         )==変更先_Type)return e.Expression.Call(Reflection.Int16.Parse_s,変更元);
            if(typeof(int           )==変更先_Type)return e.Expression.Call(Reflection.Int32.Parse_s,変更元);
            if(typeof(long          )==変更先_Type)return e.Expression.Call(Reflection.Int64.Parse_s,変更元);
            if(typeof(float         )==変更先_Type)return e.Expression.Call(Reflection.Single.Parse_s,変更元);
            if(typeof(double        )==変更先_Type)return e.Expression.Call(Reflection.Double.Parse_s,変更元);
            if(typeof(decimal       )==変更先_Type)return e.Expression.Call(Reflection.Decimal.Parse_s,変更元);
            if(typeof(bool          )==変更先_Type)return e.Expression.NotEqual(e.Expression.Call(Reflection.Int32.Parse_s,変更元),Constant_0);
            //if(typeof(string) == 変更先_Type) return e.Expression.Call(Reflection.Decimal.ToString_,変更元);
            if(typeof(DateTime      )==変更先_Type)return e.Expression.Call(Reflection.DateTime      .Parse_input,変更元);
            if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.Parse_s,変更元);
            if(typeof(Guid          )==変更先_Type)return e.Expression.Call(Reflection.Guid.Parse_s,変更元);
            if(typeof(byte[]        )==変更先_Type)return e.Expression.Call(this.Encoding,this.Encoding_GetBytes,変更元);
            //XDocument
        } else if(typeof(DateTimeOffset)==変更元_Type){
            //if(typeof(byte    )==変更先_Type)return e.Expression.Call(Reflection.Byte.Parse_s,変更元);
            //if(typeof(short   )==変更先_Type)return e.Expression.Call(Reflection.Int16.Parse_s,変更元);
            //if(typeof(int     )==変更先_Type)return e.Expression.Call(Reflection.Int32.Parse_s,変更元);
            //if(typeof(long    )==変更先_Type)return e.Expression.Call(Reflection.Int64.Parse_s,変更元);
            //if(typeof(float   )==変更先_Type)return e.Expression.Call(Reflection.Single.Parse_s,変更元);
            //if(typeof(double  )==変更先_Type)return e.Expression.Call(Reflection.Double.Parse_s,変更元);
            //if(typeof(decimal )==変更先_Type)return e.Expression.Call(Reflection.Decimal.Parse_s,変更元);
            //if(typeof(bool    )==変更先_Type)return e.Expression.NotEqual(e.Expression.Call(Reflection.Int32.Parse_s,変更元),Constant_0);
            if(typeof(string) == 変更先_Type) return e.Expression.Call(変更元,Reflection.DateTimeOffset.ToString_);
            //if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.Parse_s,変更元);
            //if(typeof(Guid    )==変更先_Type)return e.Expression.Call(Reflection.Guid.Parse_s,変更元);
            //if(typeof(byte[]  )==変更先_Type)return e.Expression.Call(Encoding,Encoding_GetBytes,変更元);
            //XDocument
        } else if(typeof(Guid) == 変更元_Type) {
            //if(typeof(byte    )==変更先_Type)return e.Expression.Call(Reflection.Byte.Parse_s,変更元);
            //if(typeof(short   )==変更先_Type)return e.Expression.Call(Reflection.Int16.Parse_s,変更元);
            //if(typeof(int     )==変更先_Type)return e.Expression.Call(Reflection.Int32.Parse_s,変更元);
            //if(typeof(long    )==変更先_Type)return e.Expression.Call(Reflection.Int64.Parse_s,変更元);
            //if(typeof(float   )==変更先_Type)return e.Expression.Call(Reflection.Single.Parse_s,変更元);
            //if(typeof(double  )==変更先_Type)return e.Expression.Call(Reflection.Double.Parse_s,変更元);
            //if(typeof(decimal )==変更先_Type)return e.Expression.Call(Reflection.Decimal.Parse_s,変更元);
            //if(typeof(bool    )==変更先_Type)return e.Expression.NotEqual(e.Expression.Call(Reflection.Int32.Parse_s,変更元),Constant_0);
            if(typeof(string) == 変更先_Type) return e.Expression.Call(変更元,Reflection.Guid.ToString_);
            //if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.Parse_s,変更元);
            //if(typeof(Guid    )==変更先_Type)return e.Expression.Call(Reflection.Guid.Parse_s,変更元);
            if(typeof(byte[]  )==変更先_Type)return e.Expression.Call(変更元,Reflection.Guid.ToByteArray);
            //XDocument
        } else if(typeof(byte[]) == 変更元_Type) {
            if(typeof(byte    )==変更先_Type) return e.Expression.ArrayAccess(変更元,Constant_0);
            if(typeof(short   )==変更先_Type) return e.Expression.Call(Reflection.BitConverter.ToInt16,変更元,Constant_0);
            if(typeof(int     )==変更先_Type) return e.Expression.Call(Reflection.BitConverter.ToInt32,変更元,Constant_0);
            if(typeof(long    )==変更先_Type) return e.Expression.Call(Reflection.BitConverter.ToInt64,変更元,Constant_0);
            if(typeof(float   )==変更先_Type) return e.Expression.Call(Reflection.BitConverter.ToSingle,変更元,Constant_0);
            if(typeof(double  )==変更先_Type) return e.Expression.Call(Reflection.BitConverter.ToDouble,変更元,Constant_0);
            if(typeof(decimal )==変更先_Type){
                //var lo = e.Expression.Call(Reflection.BitConverter.ToInt32,変更元);//BitConverter.ToInt32(expression);
                //var mid = e.Expression.Call(Reflection.BitConverter.ToInt32,変更元,Constant_4);//BitConverter.ToInt32(expression,4);
                //var hi = e.Expression.Call(Reflection.BitConverter.ToInt32,変更元,Constant_8);//BitConverter.ToInt32(expression,8);
                //var isNegative = e.Expression.Call(Reflection.BitConverter.ToInt32,変更元,Constant_12);// BitConverter.ToBoolean(expression,12);
                //var scale = e.Expression.ArrayIndex(変更元,Constant_13);
                return e.Expression.New(Reflection.Decimal.ctor_lo_mid_hi_isNegative_scale,変更元);
            }
            if(typeof(string        )==変更先_Type) return e.Expression.Call(Reflection.BitConverter.ToString_,変更元,Constant_0);
            if(typeof(bool          )==変更先_Type) return e.Expression.NotEqual(e.Expression.Call(Reflection.Int32.Parse_s,変更元),Constant_0);
            if(typeof(DateTime      )==変更先_Type) return e.Expression.Call(Reflection.DateTime      .Parse_input,変更元);
            if(typeof(DateTimeOffset)==変更先_Type) return e.Expression.Call(Reflection.DateTimeOffset.Parse_s,変更元);
            if(typeof(Guid          )==変更先_Type) return e.Expression.Call(Reflection.Guid.Parse_s,変更元);
            //if(typeof(byte[]) == 変更先_Type) return e.Expression.Call(Reflection.BitConverter.ToString_,変更元);
            //XDocument
        //} else if(typeof(XDocument) == 変更元_Type) {
        }
        throw new NotSupportedException($"{変更元_Type.FullName}と{変更先_Type.FullName}に変換出来ない");
    }
    /// <summary>
    /// Int32?aをDouble?にキャストするとき
    /// a.HasValue?new Nullable&lt;Double>((Double)a.Value):default(Double?)
    /// </summary>
    /// <param name="変換元"></param>
    /// <param name="変換先_Type"></param>
    /// <returns></returns>
    private e.Expression Convertデータ型を合わせるNullableは想定する(e.Expression 変換元,Type 変換先_Type){
        var 変換元_Type=変換元.Type;
        if(変換元_Type==変換先_Type)return 変換元;
        var 変換元Nullableなし=変換元;
        種類 種類_変換元,種類_変換先;
        if(変換元_Type.IsNullable()){
            変換元Nullableなし=GetValueOrDefault(変換元Nullableなし);
            種類_変換元=種類.Nullable;
        }else{
            種類_変換元=変換元_Type.IsValueType?種類.ValueType:種類.Reference;
        }
        var 変換先Nullableなし_Type=変換先_Type;
        if(変換先_Type.IsNullable()    )種類_変換先=種類.Nullable;
        else if(変換先_Type.IsValueType)種類_変換先=種類.ValueType;
        else                            種類_変換先=種類.Reference;
        if(種類_変換先==種類.Nullable)     変換先Nullableなし_Type=変換先Nullableなし_Type.GetGenericArguments()[0]; //Int32?→Int32
        変換元Nullableなし=this.Convertデータ型を合わせるNullableは想定しない(変換元Nullableなし,変換先Nullableなし_Type);
        if(種類_変換元==種類.Nullable)
            if(種類_変換先==種類.Nullable)return e.Expression.Condition(
                e.Expression.Property(変換元,"HasValue"),
                this.ConvertNullable(変換元Nullableなし),
                e.Expression.Default(変換先_Type)
            );
            else if(種類_変換先==種類.ValueType)return 変換元Nullableなし;
            else return e.Expression.Condition(
                e.Expression.Property(変換元,"HasValue"),
                変換元Nullableなし,
                e.Expression.Default(変換先_Type)
            );
        else if(種類_変換元==種類.ValueType)
            if(種類_変換先==種類.Nullable)return this.ConvertNullable(変換元Nullableなし);
            else if(種類_変換先==種類.ValueType)return 変換元Nullableなし;
            else return e.Expression.Convert(変換元Nullableなし,変換先_Type);
        else
            if(種類_変換先==種類.Nullable)return e.Expression.Condition(
                e.Expression.NotEqual(変換元,Constant_null),
                this.ConvertNullable(変換元Nullableなし),
                e.Expression.Default(変換先_Type)
            );
            else if(種類_変換先==種類.ValueType)return 変換元Nullableなし;
            else return e.Expression.Condition(
                e.Expression.NotEqual(変換元,Constant_null),
                変換元Nullableなし,
                e.Expression.Default(変換先_Type)
            );
        //throw new NotSupportedException($"{変換元_Type.FullName}と{変換先_Type.FullName}は合わせれない");
    }
    private NotSupportedException 単純NotSupportedException(object Value){
        var Method=new StackFrame(1).GetMethod()!;
        var x=Method.GetParameters()[0];
        return new NotSupportedException($"{this.GetType().FullName}.{Method.Name}({x.ParameterType}{x.Name})の{x.Name}で{Value}は処理できない。");
    }
    private e.Expression TSqlFragment(TSqlFragment x)=>x switch{
        AdHocDataSource                                       y=>this.AdHocDataSource(y),
        AddFileSpec                                           y=>this.AddFileSpec(y),
        AlterAvailabilityGroupAction                          y=>this.AlterAvailabilityGroupAction(y),
        AlterAvailabilityGroupFailoverOption                  y=>this.AlterAvailabilityGroupFailoverOption(y),
        AlterDatabaseTermination                              y=>this.AlterDatabaseTermination(y),
        AlterFullTextIndexAction                              y=>this.AlterFullTextIndexAction(y),
        AlterRoleAction                                       y=>this.AlterRoleAction(y),
        AlterServerConfigurationBufferPoolExtensionOption     y=>this.AlterServerConfigurationBufferPoolExtensionOption(y),
        AlterServerConfigurationDiagnosticsLogOption          y=>this.AlterServerConfigurationDiagnosticsLogOption(y),
        AlterServerConfigurationExternalAuthenticationOption  y=>this.AlterServerConfigurationExternalAuthenticationOption(y),
        AlterServerConfigurationFailoverClusterPropertyOption y=>this.AlterServerConfigurationFailoverClusterPropertyOption(y),
        AlterServerConfigurationHadrClusterOption             y=>this.AlterServerConfigurationHadrClusterOption(y),
        AlterServerConfigurationSoftNumaOption                y=>this.AlterServerConfigurationSoftNumaOption(y),
        AlterTableDropTableElement                            y=>this.AlterTableDropTableElement(y),
        ApplicationRoleOption                                 y=>this.ApplicationRoleOption(y),
        AssemblyName                                          y=>this.AssemblyName(y),
        AssemblyOption                                        y=>this.AssemblyOption(y),
        //AtomicBlockOption                                     y=>this.AtomicBlockOption(y),
        AuditOption                                           y=>this.AuditOption(y),
        AuditSpecificationDetail                              y=>this.AuditSpecificationDetail(y),
        AuditSpecificationPart                                y=>this.AuditSpecificationPart(y),
        AuditTarget                                           y=>this.AuditTarget(y),
        AuditTargetOption                                     y=>this.AuditTargetOption(y),
        AutomaticTuningOption                                 y=>this.AutomaticTuningOption(y),
        AvailabilityGroupOption                               y=>this.AvailabilityGroupOption(y),
        AvailabilityReplica                                   y=>this.AvailabilityReplica(y),
        AvailabilityReplicaOption                             y=>this.AvailabilityReplicaOption(y),
        BackupOption                                          y=>this.BackupOption(y),
        BackupRestoreFileInfo                                 y=>this.BackupRestoreFileInfo(y),
        BooleanExpression                                     y=>this.BooleanExpression(y),
        BoundingBoxParameter                                  y=>this.BoundingBoxParameter(y),
        BrokerPriorityParameter                               y=>this.BrokerPriorityParameter(y),
        BulkInsertOption                                      y=>this.BulkInsertOption(y),
        CallTarget                                            y=>this.CallTarget(y),
        CertificateOption                                     y=>this.CertificateOption(y),
        ChangeTrackingOptionDetail                            y=>this.ChangeTrackingOptionDetail(y),
        ColumnEncryptionDefinition                            y=>this.ColumnEncryptionDefinition(y),
        ColumnEncryptionDefinitionParameter                   y=>this.ColumnEncryptionDefinitionParameter(y),
        ColumnEncryptionKeyValue                              y=>this.ColumnEncryptionKeyValue(y),
        ColumnEncryptionKeyValueParameter                     y=>this.ColumnEncryptionKeyValueParameter(y),
        ColumnMasterKeyParameter                              y=>this.ColumnMasterKeyParameter(y),
        ColumnStorageOptions                                  y=>this.ColumnStorageOptions(y),
        ColumnWithSortOrder                                   y=>this.ColumnWithSortOrder(y),
        CommonTableExpression                                 y=>this.CommonTableExpression(y),
        CompressionPartitionRange                             y=>this.CompressionPartitionRange(y),
        ComputeClause                                         y=>this.ComputeClause(y),
        ComputeFunction                                       y=>this.ComputeFunction(y),
        //ConstraintDefinition                                  y=>this.ConstraintDefinition(y),
        ContractMessage                                       y=>this.ContractMessage(y),
        CopyOption                                            y=>this.CopyOption(y),
        CopyStatementOptionBase                               y=>this.CopyStatementOptionBase(y),
        CreateLoginSource                                     y=>this.CreateLoginSource(y),
        CryptoMechanism                                       y=>this.CryptoMechanism(y),
        CursorDefinition                                      y=>this.CursorDefinition(y),
        CursorId                                              y=>this.CursorId(y),
        CursorOption                                          y=>this.CursorOption(y),
        DataModificationSpecification                         y=>this.DataModificationSpecification(y),
        DatabaseAuditAction                                   y=>this.DatabaseAuditAction(y),
        DatabaseConfigurationClearOption                      y=>this.DatabaseConfigurationClearOption(y),
        DatabaseConfigurationSetOption                        y=>this.DatabaseConfigurationSetOption(y),
        DatabaseOption                                        y=>this.DatabaseOption(y),
        DbccOption                                            y=>this.DbccOption(y),
        DbccNamedLiteral                                      y=>this.DbccNamedLiteral(y),
        DeclareTableVariableBody                              y=>this.DeclareTableVariableBody(y),
        DeviceInfo                                            y=>this.DeviceInfo(y),
        DialogOption                                          y=>this.DialogOption(y),
        DiskStatementOption                                   y=>this.DiskStatementOption(y),
        DropClusteredConstraintOption                         y=>this.DropClusteredConstraintOption(y),
        DropIndexClauseBase                                   y=>this.DropIndexClauseBase(y),
        EncryptionSource                                      y=>this.EncryptionSource(y),
        EndpointAffinity                                      y=>this.EndpointAffinity(y),
        EndpointProtocolOption                                y=>this.EndpointProtocolOption(y),
        EventDeclaration                                      y=>this.EventDeclaration(y),
        EventDeclarationSetParameter                          y=>this.EventDeclarationSetParameter(y),
        EventNotificationObjectScope                          y=>this.EventNotificationObjectScope(y),
        EventSessionObjectName                                y=>this.EventSessionObjectName(y),
        EventTypeContainer                                    y=>this.EventTypeContainer(y),
        EventTypeGroupContainer                               y=>this.EventTypeGroupContainer(y),
        //ExecutableEntity                                      y=>this.ExecutableEntity(y),
        ExecuteAsClause                                       y=>this.ExecuteAsClause(y),
        ExecuteAsProcedureOption                              y=>this.ExecuteAsProcedureOption(y),
        ExecuteAsTriggerOption                                y=>this.ExecuteAsTriggerOption(y),
        ExecuteContext                                        y=>this.ExecuteContext(y),
        ExecuteInsertSource                                   y=>this.ExecuteInsertSource(y),
        ExecuteOption                                         y=>this.ExecuteOption(y),
        ExecuteParameter                                      y=>this.ExecuteParameter(y),
        ExecuteSpecification                                  y=>this.ExecuteSpecification(y),
        ExpressionGroupingSpecification                       y=>this.ExpressionGroupingSpecification(y),
        ExpressionWithSortOrder                               y=>this.ExpressionWithSortOrder(y),
        ExternalDataSourceLiteralOrIdentifierOption           y=>this.ExternalDataSourceLiteralOrIdentifierOption(y),
        ExternalDataSourceOption                              y=>this.ExternalDataSourceOption(y),
        ExternalFileFormatOption                              y=>this.ExternalFileFormatOption(y),
        ExternalLanguageFileOption                            y=>this.ExternalLanguageFileOption(y),
        ExternalLibraryFileOption                             y=>this.ExternalLibraryFileOption(y),
        ExternalResourcePoolAffinitySpecification             y=>this.ExternalResourcePoolAffinitySpecification(y),
        ExternalResourcePoolParameter                         y=>this.ExternalResourcePoolParameter(y),
        ExternalStreamLiteralOrIdentifierOption               y=>this.ExternalStreamLiteralOrIdentifierOption(y),
        ExternalStreamOption                                  y=>this.ExternalStreamOption(y),
        ExternalTableColumnDefinition                         y=>this.ExternalTableColumnDefinition(y),
        ExternalTableDistributionOption                       y=>this.ExternalTableDistributionOption(y),
        ExternalTableDistributionPolicy                       y=>this.ExternalTableDistributionPolicy(y),
        ExternalTableLiteralOrIdentifierOption                y=>this.ExternalTableLiteralOrIdentifierOption(y),
        ExternalTableOption                                   y=>this.ExternalTableOption(y),
        FederationScheme                                      y=>this.FederationScheme(y),
        FetchType                                             y=>this.FetchType(y),
        FileDeclaration                                       y=>this.FileDeclaration(y),
        FileDeclarationOption                                 y=>this.FileDeclarationOption(y),
        FileGroupDefinition                                   y=>this.FileGroupDefinition(y),
        FileGroupOrPartitionScheme                            y=>this.FileGroupOrPartitionScheme(y),
        FileStreamOnDropIndexOption                           y=>this.FileStreamOnDropIndexOption(y),
        FileStreamOnTableOption                               y=>this.FileStreamOnTableOption(y),
        FileStreamRestoreOption                               y=>this.FileStreamRestoreOption(y),
        FileTableCollateFileNameTableOption                   y=>this.FileTableCollateFileNameTableOption(y),
        FileTableConstraintNameTableOption                    y=>this.FileTableConstraintNameTableOption(y),
        FileTableDirectoryTableOption                         y=>this.FileTableDirectoryTableOption(y),
        ForClause                                             y=>this.ForClause(y),
        ForceSeekTableHint                                    y=>this.ForceSeekTableHint(y),
        ForeignKeyConstraintDefinition                        y=>this.ForeignKeyConstraintDefinition(y),
        //FromClause                                            y=>this.FromClause(y),
        FullTextCatalogAndFileGroup                           y=>this.FullTextCatalogAndFileGroup(y),
        FullTextCatalogOption                                 y=>this.FullTextCatalogOption(y),
        FullTextIndexColumn                                   y=>this.FullTextIndexColumn(y),
        FullTextIndexOption                                   y=>this.FullTextIndexOption(y),
        FullTextStopListAction                                y=>this.FullTextStopListAction(y),
        //FullTextTableReference                                y=>this.FullTextTableReference(y),
        FunctionCall                                          y=>this.FunctionCall(y),
        FunctionCallSetClause                                 y=>this.FunctionCallSetClause(y),
        FunctionOption                                        y=>this.FunctionOption(y),
        //FunctionReturnType                                    y=>this.FunctionReturnType(y),
        GeneralSetCommand                                     y=>this.GeneralSetCommand(y),
        GlobalFunctionTableReference                          y=>this.GlobalFunctionTableReference(y),
        GlobalVariableExpression                              y=>this.GlobalVariableExpression(y),
        GrandTotalGroupingSpecification                       y=>this.GrandTotalGroupingSpecification(y),
        GraphConnectionBetweenNodes                           y=>this.GraphConnectionBetweenNodes(y),
        GraphConnectionConstraintDefinition                   y=>this.GraphConnectionConstraintDefinition(y),
        GridParameter                                         y=>this.GridParameter(y),
        GridsSpatialIndexOption                               y=>this.GridsSpatialIndexOption(y),
        GroupByClause                                         y=>this.GroupByClause(y),
        GroupingSetsGroupingSpecification                     y=>this.GroupingSetsGroupingSpecification(y),
        GroupingSpecification                                 y=>this.GroupingSpecification(y),
        HavingClause                                          y=>this.HavingClause(y),
        IIfCall                                               y=>this.IIfCall(y),
        IPv4                                                  y=>this.IPv4(y),
        //Identifier                                            y=>this.Identifier(y),
        IdentifierOrScalarExpression                          y=>this.IdentifierOrScalarExpression(y),
        IdentifierOrValueExpression                           y=>this.IdentifierOrValueExpression(y),
        //IdentifierSnippet                                     y=>this.IdentifierSnippet(y),
        IdentityFunctionCall                                  y=>this.IdentityFunctionCall(y),
        IdentityOptions                                       y=>this.IdentityOptions(y),
        IndexDefinition                                       y=>this.IndexDefinition(y),
        IndexExpressionOption                                 y=>this.IndexExpressionOption(y),
        IndexOption                                           y=>this.IndexOption(y),
        IndexTableHint                                        y=>this.IndexTableHint(y),
        IndexType                                             y=>this.IndexType(y),
        InlineResultSetDefinition                             y=>this.InlineResultSetDefinition(y),
        InsertBulkColumnDefinition                            y=>this.InsertBulkColumnDefinition(y),
        InsertMergeAction                                     y=>this.InsertMergeAction(y),
        InsertSource                                          y=>this.InsertSource(y),
        InternalOpenRowset                                    y=>this.InternalOpenRowset(y),
        JsonKeyValue                                          y=>this.JsonKeyValue(y),
        KeyOption                                             y=>this.KeyOption(y),
        LedgerTableOption                                     y=>this.LedgerTableOption(y),
        LedgerViewOption                                      y=>this.LedgerViewOption(y),
        LeftFunctionCall                                      y=>this.LeftFunctionCall(y),
        LiteralOpenRowsetCosmosOption                         y=>this.LiteralOpenRowsetCosmosOption(y),
        LiteralOptionValue                                    y=>this.LiteralOptionValue(y),
        LiteralRange                                          y=>this.LiteralRange(y),
        LiteralSessionOption                                  y=>this.LiteralSessionOption(y),
        LiteralStatisticsOption                               y=>this.LiteralStatisticsOption(y),
        LiteralTableHint                                      y=>this.LiteralTableHint(y),
        LocationOption                                        y=>this.LocationOption(y),
        LockEscalationTableOption                             y=>this.LockEscalationTableOption(y),
        LowPriorityLockWaitAbortAfterWaitOption               y=>this.LowPriorityLockWaitAbortAfterWaitOption(y),
        LowPriorityLockWaitMaxDurationOption                  y=>this.LowPriorityLockWaitMaxDurationOption(y),
        LowPriorityLockWaitOption                             y=>this.LowPriorityLockWaitOption(y),
        LowPriorityLockWaitTableSwitchOption                  y=>this.LowPriorityLockWaitTableSwitchOption(y),
        MaxDispatchLatencySessionOption                       y=>this.MaxDispatchLatencySessionOption(y),
        MemoryOptimizedTableOption                            y=>this.MemoryOptimizedTableOption(y),
        MemoryPartitionSessionOption                          y=>this.MemoryPartitionSessionOption(y),
        MergeAction                                           y=>this.MergeAction(y),
        MergeActionClause                                     y=>this.MergeActionClause(y),
        MethodSpecifier                                       y=>this.MethodSpecifier(y),
        MirrorToClause                                        y=>this.MirrorToClause(y),
        MoveRestoreOption                                     y=>this.MoveRestoreOption(y),
        //MultiPartIdentifier                                   y=>this.MultiPartIdentifier(y),
        NextValueForExpression                                y=>this.NextValueForExpression(y),
        NullableConstraintDefinition                          y=>this.NullableConstraintDefinition(y),
        OdbcFunctionCall                                      y=>this.OdbcFunctionCall(y),
        OdbcQualifiedJoinTableReference                       y=>this.OdbcQualifiedJoinTableReference(y),
        OffsetClause                                          y=>this.OffsetClause(y),
        OnOffSessionOption                                    y=>this.OnOffSessionOption(y),
        OnOffStatisticsOption                                 y=>this.OnOffStatisticsOption(y),
        OnlineIndexLowPriorityLockWaitOption                  y=>this.OnlineIndexLowPriorityLockWaitOption(y),
        OpenJsonTableReference                                y=>this.OpenJsonTableReference(y),
        OpenQueryTableReference                               y=>this.OpenQueryTableReference(y),
        OpenRowsetColumnDefinition                            y=>this.OpenRowsetColumnDefinition(y),
        OpenRowsetCosmos                                      y=>this.OpenRowsetCosmos(y),
        OpenRowsetCosmosOption                                y=>this.OpenRowsetCosmosOption(y),
        //OpenRowsetTableReference                              y=>this.OpenRowsetTableReference(y),
        OpenXmlTableReference                                 y=>this.OpenXmlTableReference(y),
        OptimizeForOptimizerHint                              y=>this.OptimizeForOptimizerHint(y),
        //OptimizerHint                                         y=>this.OptimizerHint(y),
        OptionValue                                           y=>this.OptionValue(y),
        OrderByClause                                         y=>this.OrderByClause(y),
        OutputClause                                          y=>this.OutputClause(y),
        OutputIntoClause                                      y=>this.OutputIntoClause(y),
        OverClause                                            y=>this.OverClause(y),
        //ParameterizedDataTypeReference                        y=>this.ParameterizedDataTypeReference(y),
        ParameterlessCall                                     y=>this.ParameterlessCall(y),
        ParenthesisExpression                                 y=>this.ParenthesisExpression(y),
        ParseCall                                             y=>this.ParseCall(y),
        PartitionFunctionCall                                 y=>this.PartitionFunctionCall(y),
        PartitionParameterType                                y=>this.PartitionParameterType(y),
        PartitionSpecifications                               y=>this.PartitionSpecifications(y),
        PartitionSpecifier                                    y=>this.PartitionSpecifier(y),
        PayloadOption                                         y=>this.PayloadOption(y),
        Permission                                            y=>this.Permission(y),
        PrimaryExpression                                     y=>this.PrimaryExpression(y),
        PrincipalOption                                       y=>this.PrincipalOption(y),
        Privilege80                                           y=>this.Privilege80(y),
        PrivilegeSecurityElement80                            y=>this.PrivilegeSecurityElement80(y),
        ProcedureOption                                       y=>this.ProcedureOption(y),
        ProcedureParameter                                    y=>this.ProcedureParameter(y),
        //ProcedureReferenceName                                y=>this.ProcedureReferenceName(y),
        QueryExpression                                       y=>this.QueryExpression(y),
        QueryStoreCapturePolicyOption                         y=>this.QueryStoreCapturePolicyOption(y),
        QueryStoreDataFlushIntervalOption                     y=>this.QueryStoreDataFlushIntervalOption(y),
        QueryStoreDesiredStateOption                          y=>this.QueryStoreDesiredStateOption(y),
        QueryStoreIntervalLengthOption                        y=>this.QueryStoreIntervalLengthOption(y),
        QueryStoreOption                                      y=>this.QueryStoreOption(y),
        QueueExecuteAsOption                                  y=>this.QueueExecuteAsOption(y),
        QueueOption                                           y=>this.QueueOption(y),
        QueueStatement                                        y=>this.QueueStatement(y),
        RemoteDataArchiveAlterTableOption                     y=>this.RemoteDataArchiveAlterTableOption(y),
        RemoteDataArchiveDatabaseSetting                      y=>this.RemoteDataArchiveDatabaseSetting(y),
        RemoteDataArchiveTableOption                          y=>this.RemoteDataArchiveTableOption(y),
        RemoteServiceBindingOption                            y=>this.RemoteServiceBindingOption(y),
        ResampleStatisticsOption                              y=>this.ResampleStatisticsOption(y),
        ResourcePoolAffinitySpecification                     y=>this.ResourcePoolAffinitySpecification(y),
        ResourcePoolParameter                                 y=>this.ResourcePoolParameter(y),
        RestoreOption                                         y=>this.RestoreOption(y),
        ResultColumnDefinition                                y=>this.ResultColumnDefinition(y),
        ResultSetDefinition                                   y=>this.ResultSetDefinition(y),
        RetentionPeriodDefinition                             y=>this.RetentionPeriodDefinition(y),
        RouteOption                                           y=>this.RouteOption(y),
        RowValue                                              y=>this.RowValue(y),
        ScalarExpression                                      y=>this.ScalarExpression(y),
        ScalarExpressionSequenceOption                        y=>this.ScalarExpressionSequenceOption(y),
        SchemaDeclarationItem                                 y=>this.SchemaDeclarationItem(y),
        SchemaObjectNameOrValueExpression                     y=>this.SchemaObjectNameOrValueExpression(y),
        SearchPropertyListAction                              y=>this.SearchPropertyListAction(y),
        SearchedWhenClause                                    y=>this.SearchedWhenClause(y),
        SecurityElement80                                     y=>this.SecurityElement80(y),
        SecurityPolicyOption                                  y=>this.SecurityPolicyOption(y),
        SecurityPredicateAction                               y=>this.SecurityPredicateAction(y),
        SecurityPrincipal                                     y=>this.SecurityPrincipal(y),
        SecurityTargetObject                                  y=>this.SecurityTargetObject(y),
        SecurityTargetObjectName                              y=>this.SecurityTargetObjectName(y),
        SecurityUserClause80                                  y=>this.SecurityUserClause80(y),
        //SelectElement                                         y=>this.SelectElement(y),
        SelectFunctionReturnType                              y=>this.SelectFunctionReturnType(y),
        //SelectScalarExpression                                y=>this.SelectScalarExpression(y),
        //SelectSetVariable                                     y=>this.SelectSetVariable(y),
        //SelectStarExpression                                  y=>this.SelectStarExpression(y),
        SelectiveXmlIndexPromotedPath                         y=>this.SelectiveXmlIndexPromotedPath(y),
        SemanticTableReference                                y=>this.SemanticTableReference(y),
        //SensitivityClassification                             y=>this.SensitivityClassification(y),
        SensitivityClassificationOption                       y=>this.SensitivityClassificationOption(y),
        SequenceOption                                        y=>this.SequenceOption(y),
        ServiceContract                                       y=>this.ServiceContract(y),
        SessionOption                                         y=>this.SessionOption(y),
        //SetClause                                             y=>this.SetClause(y),
        SetCommand                                            y=>this.SetCommand(y),
        SimpleWhenClause                                      y=>this.SimpleWhenClause(y),
        SpatialIndexOption                                    y=>this.SpatialIndexOption(y),
        //SqlCommandIdentifier                                  y=>this.SqlCommandIdentifier(y),
        //SqlDataTypeReference                                  y=>this.SqlDataTypeReference(y),
        StatementList                                         y=>this.StatementList(y),
        StatementWithCtesAndXmlNamespaces                     y=>this.StatementWithCtesAndXmlNamespaces(y),
        StatisticsOption                                      y=>this.StatisticsOption(y),
        StatisticsPartitionRange                              y=>this.StatisticsPartitionRange(y),
        SystemTimePeriodDefinition                            y=>this.SystemTimePeriodDefinition(y),
        SystemVersioningTableOption                           y=>this.SystemVersioningTableOption(y),
        TSqlBatch                                             y=>this.TSqlBatch(y),
        TSqlFragmentSnippet                                   y=>this.TSqlFragmentSnippet(y),
        TSqlScript                                            y=>this.TSqlScript(y),
        TableClusteredIndexType                               y=>this.TableClusteredIndexType(y),
        TableDataCompressionOption                            y=>this.TableDataCompressionOption(y),
        //TableDefinition                                       y=>this.TableDefinition(y),
        TableDistributionOption                               y=>this.TableDistributionOption(y),
        TableDistributionPolicy                               y=>this.TableDistributionPolicy(y),
        TableHint                                             y=>this.TableHint(y),
        TableHintsOptimizerHint                               y=>this.TableHintsOptimizerHint(y),
        TableIndexOption                                      y=>this.TableIndexOption(y),
        TableIndexType                                        y=>this.TableIndexType(y),
        TableOption                                           y=>this.TableOption(y),
        //TableReference                                        y=>this.TableReference(y),
        TableSampleClause                                     y=>this.TableSampleClause(y),
        TableSwitchOption                                     y=>this.TableSwitchOption(y),
        TargetDeclaration                                     y=>this.TargetDeclaration(y),
        TemporalClause                                        y=>this.TemporalClause(y),
        TopRowFilter                                          y=>this.TopRowFilter(y),
        TriggerAction                                         y=>this.TriggerAction(y),
        TriggerObject                                         y=>this.TriggerObject(y),
        TriggerOption                                         y=>this.TriggerOption(y),
        //UniqueConstraintDefinition                            y=>this.UniqueConstraintDefinition(y),
        UnpivotedTableReference                               y=>this.UnpivotedTableReference(y),
        //UnqualifiedJoin                                       y=>this.UnqualifiedJoin(y),
        UseHintList                                           y=>this.UseHintList(y),
        UserLoginOption                                       y=>this.UserLoginOption(y),
        VariableValuePair                                     y=>this.VariableValuePair(y),
        ViewDistributionOption                                y=>this.ViewDistributionOption(y),
        ViewDistributionPolicy                                y=>this.ViewDistributionPolicy(y),
        ViewForAppendOption                                   y=>this.ViewForAppendOption(y),
        ViewOption                                            y=>this.ViewOption(y),
        WhenClause                                            y=>this.WhenClause(y),
        WhereClause                                           y=>this.WhereClause(y),
        WindowClause                                          y=>this.WindowClause(y),
        WindowDefinition                                      y=>this.WindowDefinition(y),
        WindowDelimiter                                       y=>this.WindowDelimiter(y),
        WindowFrameClause                                     y=>this.WindowFrameClause(y),
        WithCtesAndXmlNamespaces                              y=>this.WithCtesAndXmlNamespaces(y),
        WithinGroupClause                                     y=>this.WithinGroupClause(y),
        WlmTimeLiteral                                        y=>this.WlmTimeLiteral(y),
        WorkloadClassifierOption                              y=>this.WorkloadClassifierOption(y),
        WorkloadGroupImportanceParameter                      y=>this.WorkloadGroupImportanceParameter(y),
        WorkloadGroupParameter                                y=>this.WorkloadGroupParameter(y),
        //XmlNamespaces                                         y=>this.XmlNamespaces(y),
        XmlNamespacesElement                                  y=>this.XmlNamespacesElement(y),
        _=>throw this.単純NotSupportedException(x)
    };
}

