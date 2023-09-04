#pragma warning disable CA1822 // Mark members as static
//#define タイム出力
using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using LinqDB.Helpers;
using e=System.Linq.Expressions;
using AssemblyName=Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
using System.Globalization;
using System.Text;
//using ColumnReferenceExpression=Microsoft.SqlServer.TransactSql.ScriptDom.ColumnReferenceExpression;
namespace LinqDB.Optimizers;
/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer{
    /// <summary>
    /// TSQLからLINQに変換する。
    /// </summary>
    private partial class 変換_TSqlFragmentからExpression{
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
        private IEnumerable<e.ParameterExpression>Variables=>this.List_ScalarVariable.Concat(
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
            e.ParameterExpression? result=PrivateFindVariable(this.List_定義型TableVariable,Name);
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
                Left = GetValueOrDefault(Left);
                if(Right.Type.IsNullable()) {
                    test = e.Expression.AndAlso(test,e.Expression.Property(Right,"HasValue"));
                    Right = GetValueOrDefault(Right);
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
        private static e.Expression GetValueOrDefault(e.Expression 入力)=>e.Expression.Call(入力,入力.Type.GetMethod("GetValueOrDefault",Type.EmptyTypes));
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
        private class 判定_集約関数があるか:VoidTSqlFragmentTraverser{
            public 判定_集約関数があるか():base(new Sql160ScriptGenerator()) { }
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
                    default:base.FunctionCall(x); break;
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
            this.変換_旧Expressionを新Expression1=変換_旧Expressionを新Expression1;
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
                if(typeof(string )==Right_Type)return(Left,e.Expression.Call(Reflection.DateTimeOffset.Parse_input,Right));
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
                if(typeof(DateTimeOffset)==Right_Type)return(e.Expression.Call(Reflection.DateTimeOffset.Parse_input,Left),Right);
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
                if(typeof(DateTimeOffset)==変更先_Type)return e.Expression.Call(Reflection.DateTimeOffset.Parse_input,変更元);
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
                if(typeof(DateTimeOffset)==変更先_Type) return e.Expression.Call(Reflection.DateTimeOffset.Parse_input,変更元);
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
            TSqlScript y=>this.TSqlScript(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression MultiPartIdentifier(MultiPartIdentifier x)=>x switch{
            SchemaObjectName y=>this.SchemaObjectName(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// MultiPartIdentifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression SchemaObjectName(SchemaObjectName x)=>x switch{
            ChildObjectName y=>this.ChildObjectName(y),
            SchemaObjectNameSnippet y=>this.SchemaObjectNameSnippet(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// SchemaObjectName:MultiPartIdentifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ChildObjectName(ChildObjectName x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// SchemaObjectName:MultiPartIdentifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression SchemaObjectNameSnippet(SchemaObjectNameSnippet x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private string Identifier(Identifier x)=>x switch{
            SqlCommandIdentifier y=>this.SqlCommandIdentifier(y),
            IdentifierSnippet y=>this.IdentifierSnippet(y),
            _=>x.Value
        };
        /// <summary>
        /// Identifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private string SqlCommandIdentifier(SqlCommandIdentifier x)=>x.Value;
        /// <summary>
        /// Identifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private string IdentifierSnippet(IdentifierSnippet x)=>x.Value;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ScalarExpression(ScalarExpression x)=>x switch{
            PrimaryExpression        y=>this.PrimaryExpression       (y),
            ExtractFromExpression    y=>this.ExtractFromExpression   (y),
            OdbcConvertSpecification y=>this.OdbcConvertSpecification(y),
            BinaryExpression         y=>this.BinaryExpression        (y),
            IdentityFunctionCall     y=>this.IdentityFunctionCall    (y),
            UnaryExpression          y=>this.UnaryExpression         (y),
            ScalarExpressionSnippet  y=>this.ScalarExpressionSnippet (y),
            SourceDeclaration        y=>this.SourceDeclaration       (y),
            _=>throw this.単純NotSupportedException(x)
        };

        /// <summary>
        /// ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression PrimaryExpression(PrimaryExpression x)=>x switch{
            ValueExpression               y=>this.ValueExpression(y),
            UserDefinedTypePropertyAccess y=>this.UserDefinedTypePropertyAccess(y),
            CaseExpression                y=>this.CaseExpression(y),
            NullIfExpression              y=>this.NullIfExpression(y),
            CoalesceExpression            y=>this.CoalesceExpression(y),
            IIfCall                       y=>this.IIfCall(y),
            ConvertCall                   y=>this.ConvertCall(y),
            TryConvertCall                y=>this.TryConvertCall(y),
            ParseCall                     y=>this.ParseCall(y),
            TryParseCall                  y=>this.TryParseCall(y),
            CastCall                      y=>this.CastCall(y),
            TryCastCall                   y=>this.TryCastCall(y),
            AtTimeZoneCall                y=>this.AtTimeZoneCall(y),
            FunctionCall                  y=>this.FunctionCall(y),
            LeftFunctionCall              y=>this.LeftFunctionCall(y),
            RightFunctionCall             y=>this.RightFunctionCall(y),
            PartitionFunctionCall         y=>this.PartitionFunctionCall(y),
            ParameterlessCall             y=>this.ParameterlessCall(y),
            ScalarSubquery                y=>this.ScalarSubquery(y),
            OdbcFunctionCall              y=>this.OdbcFunctionCall(y),
            ParenthesisExpression         y=>this.ParenthesisExpression(y),
            ColumnReferenceExpression     y=>this.ColumnReferenceExpression(y),
            NextValueForExpression        y=>this.NextValueForExpression(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// ImmutableSet&lt;bool>→bool
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ScalarSubquery(ScalarSubquery x){
            var Subquery=this.Subquery(x);
            var Subquery_Type=Subquery.Type;
            var GenericArguments=Subquery_Type.GetGenericArguments();
            var T=GenericArguments[0];
            if(T.IsValueType&&!T.IsNullable()) {
                GenericArguments[0]=typeof(Nullable<>).MakeGenericType(GenericArguments);
                Subquery=e.Expression.Call(
                    Reflection.ExtensionSet.Cast.MakeGenericMethod(GenericArguments),
                    Subquery
                );
            }
            return e.Expression.Call(
                Reflection.ExtensionSet.SingleOrDefault.MakeGenericMethod(GenericArguments),
                Subquery
            );
        }
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ValueExpression(ValueExpression x)=>x switch{
            Literal y=>this.Literal(y),
            VariableReference y=>this.VariableReference(y),
            GlobalVariableExpression y=>this.GlobalVariableExpression(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression Literal(Literal x)=>x switch{
            IdentifierLiteral y=>this.IdentifierLiteral(y),
            IntegerLiteral    y=>this.IntegerLiteral(y),
            NumericLiteral    y=>this.NumericLiteral(y),
            RealLiteral       y=>this.RealLiteral(y),
            MoneyLiteral      y=>this.MoneyLiteral(y),
            BinaryLiteral     y=>this.BinaryLiteral(y),
            StringLiteral     y=>this.StringLiteral(y),
            NullLiteral       y=>this.NullLiteral(y),
            DefaultLiteral    y=>this.DefaultLiteral(y),
            MaxLiteral        y=>this.MaxLiteral(y),
            OdbcLiteral       y=>this.OdbcLiteral(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression IdentifierLiteral(IdentifierLiteral x){
            throw this.単純NotSupportedException(x);
        }
        //private e.Expression IntegerLiteral(IntegerLiteral x)=>e.Expression.Constant(int.Parse(x.Value));
        //private static e.Expression 共通Literal(Literal x,e.ConstantExpression nullable)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(int.Parse(x.Value))
        private static readonly e.ConstantExpression nullable_int=e.Expression.Constant(null,typeof(int?));
        private e.Expression IntegerLiteral(IntegerLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(int.Parse(x.Value));
        private e.Expression NumericLiteral(NumericLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(decimal.Parse(x.Value));
        private e.Expression RealLiteral(RealLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(double.Parse(x.Value));
        private e.Expression MoneyLiteral(MoneyLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(decimal.Parse(x.Value));
        private e.Expression BinaryLiteral(BinaryLiteral x){
            Debug.Assert("0x"==x.Value[..2]);
            var x_Value=x.Value[2..];
            if(x_Value.Length%2==1)x_Value='0'+x_Value;
            var Bytes = new byte[x_Value.Length/2];
            var index =0;
            for(var a=0;a<x_Value.Length;a+=2){
                Debug.Assert(x_Value.Substring(a,2)==x_Value[a..(a+2)]);
                var Byte=Convert.ToByte(x_Value[a..(a+2)],16);
                Bytes[index++]=Byte;
            }
            return e.Expression.Constant(Bytes);
        }
        private e.Expression StringLiteral(StringLiteral x)=>e.Expression.Constant(x.Value);
        private e.Expression NullLiteral(NullLiteral x)=>Constant_null;
        private e.Expression DefaultLiteral(DefaultLiteral x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MaxLiteral(MaxLiteral x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OdbcLiteral(OdbcLiteral x){
            throw this.単純NotSupportedException(x);
            //return e.Expression.Constant(DateTimeOffset.Parse(x.Value));
        }
        private e.Expression VariableReference(VariableReference x)=>
            this.FindParameterVariable(x.Name);
        //this.Block_Variables.Concat(this.Lambda_Parameters).Single(p=>p.Name==x.Name);
        /// <summary>
        /// @@servername,@@rowcountとか
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression GlobalVariableExpression(GlobalVariableExpression x){
            Debug.Assert("@@"==x.Name[..2]);
            var Name=x.Name[2..];
            var Property=typeof(Product.SQLServer.Methods).GetProperties(BindingFlags.Static|BindingFlags.Public|BindingFlags.DeclaredOnly).Where(p => string.Equals(p.Name,Name,StringComparison.OrdinalIgnoreCase)).Single();
            return e.Expression.Property(null,Property);
        }
        private e.Expression UserDefinedTypePropertyAccess(UserDefinedTypePropertyAccess x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression CaseExpression(CaseExpression x)=>x switch{
            SimpleCaseExpression y=>this.SimpleCaseExpression(y),
            SearchedCaseExpression y=>this.SearchedCaseExpression(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// 単純検索CASE
        /// CASE N_NAME 
        ///     WHEN 'BRAZIL' THEN 'brazil' 
        ///     WHEN 'EUROPE' THEN 'europe' 
        ///     ELSE 'Z' 
        /// END
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression SimpleCaseExpression(SimpleCaseExpression x){
            var WhenClauses=x.WhenClauses;
            var WhenClauses_Count=WhenClauses.Count;
            var Switch_SwitchValue=this.ScalarExpression(x.InputExpression);
            var Switch_DefaultBody=x.ElseExpression is not null?this.ScalarExpression(x.ElseExpression):null;
            var Case_Bodies=new(e.Expression TestValue,e.Expression Body)[WhenClauses_Count];
            //Switch_SwitchValueがnullならSwitch_DefaultBody
            //TestValueがnullならSwitch_DefaultBody
            //Body,Switch_DefaultBodyのいずれかがNullableなら戻り値はNullable
            e.Expression? Condition;
            if(Switch_SwitchValue.Type.IsNullable()){
                Condition=e.Expression.Property(Switch_SwitchValue,"HasValue");
                Switch_SwitchValue=GetValueOrDefault(Switch_SwitchValue);
            }else
                Condition=null;
            Type? ResultType=null;
            //var Result_Type=Switch_DefaultBody.Type;
            //this.Convertデータ型を合わせるNullableは想定する()
            for(var a=0;a<WhenClauses_Count;a++){
                var WhenClause=WhenClauses[a];
                var TestValue=this.ScalarExpression(WhenClause.WhenExpression);
                var Body=this.ScalarExpression(WhenClause.ThenExpression);
                if(TestValue.Type.IsNullable()){
                    var HasValue=e.Expression.Property(TestValue,"HasValue");
                    Condition=Condition is null ? HasValue:e.Expression.AndAlso(Condition,HasValue);
                    Case_Bodies[a]=(GetValueOrDefault(TestValue),Body);
                }else
                    Case_Bodies[a]=(TestValue,Body);
                if(Body.Type.IsNullable())ResultType=Body.Type;
                else if(ResultType is null)ResultType=Body.Type;
            }
            //var Call=Expressions.Expression.Call(
            //    Method,
            //    入力Expressions
            //);
            //var Call=非NullのExpression(Condition,Case_Bodies);
            //if(Condition is null)return Call;
            //return Expressions.Expression.Condition(
            //    Condition,
            //    Call,
            //    Expressions.Expression.Constant(null,Call.Type)
            //);
            var SwitchCases=new e.SwitchCase[WhenClauses_Count];
            var SwitchCase_TestValues=this.作業配列.Expressions1;
            var Switch_SwitchValue_Type=Switch_SwitchValue.Type;
            for(var a=0;a<WhenClauses_Count;a++){
                ref var Case_Body=ref Case_Bodies[a];
                var Case_Body_TestValue=this.Convertデータ型を合わせるNullableは想定する(Case_Body.TestValue,Switch_SwitchValue_Type);
                SwitchCase_TestValues[0]=Case_Body_TestValue;
                SwitchCases[a]=e.Expression.SwitchCase(
                    this.Convertデータ型を合わせるNullableは想定する(Case_Body.Body,ResultType!),
                    SwitchCase_TestValues
                );
            }
            Switch_DefaultBody??=e.Expression.Default(ResultType);
            var Switch=e.Expression.Switch(Switch_SwitchValue,Switch_DefaultBody,SwitchCases);
            if(Condition is null) return Switch;
            return e.Expression.Condition(Condition, Switch, e.Expression.Default(ResultType));
        }
        /// <summary>
        /// 検索CASE
        /// CASE
        ///     WHEN N_NAME='BRAZIL' THEN 'brazil' 
        ///     WHEN N_NAME='EUROPE' THEN 'europe' 
        ///     ELSE 'Z' 
        /// END
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression SearchedCaseExpression(SearchedCaseExpression x){
            var WhenClauses=x.WhenClauses;
            var WhenClauses_Count=WhenClauses.Count;
            var Result=x.ElseExpression is null?Constant_null:this.ScalarExpression(x.ElseExpression);
            for(var a = WhenClauses_Count-1;a>=0;a--) {
                var WhenClause = WhenClauses[a];
                var IfTrue = this.ScalarExpression(WhenClause.ThenExpression);
                (IfTrue,Result)=this.Convertデータ型を合わせるNullableは想定する(IfTrue,Result);
                Result=e.Expression.Condition(
                    this.BooleanExpression(WhenClause.WhenExpression),
                    IfTrue,
                    Result
                );
            }
            return Result;
        }
        /// <summary>
        /// nullif nullだった場合どの値を返すか
        /// nullif(aatm.task_agent_data.value('(/*/retentionPeriod)[1]','int'),0)
        /// a.HasValue?a.GetValueOrDefault():default
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <example>nullif(NULL,1)</example>
        private e.Expression NullIfExpression(NullIfExpression x){
            var FirstExpression=this.ScalarExpression(x.FirstExpression);
            e.Expression test;
            if(FirstExpression.Type.IsNullable()){
                test=e.Expression.Property(FirstExpression,nameof(Nullable<int>.HasValue));
                FirstExpression=e.Expression.Call(FirstExpression,FirstExpression.Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes));
            }else{
                test=e.Expression.Equal(FirstExpression,Constant_null);
            }
            return e.Expression.Condition(
                test,
                FirstExpression,
                this.ScalarExpression(x.SecondExpression)
            );
        }
        private e.Expression CoalesceExpression(CoalesceExpression x){
            //coalesce(a,b,c,d)
            //引数を順番に評価し、NULL と評価されない最初の式の現在の値
            //a??b??c??d
            //var Result=this.ScalarExpression(x.Collation);
            var x_Expressions=x.Expressions;
            var Result=this.ScalarExpression(x_Expressions[0]);
            for(var a=1;a<x_Expressions.Count;a++)
                Result=e.Expression.Coalesce(Result,this.ScalarExpression(x_Expressions[a]));
            return Result;
        }
        private e.Expression IIfCall(IIfCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ConvertCall(ConvertCall x){
            //convert(datetime,'2020-3-4 1:2:3'),101)
            //var Convert=Constantを最適化(this.ScalarExpression(x.Parameter),DBTypeからTypeに変換(x.DataType));
            var 変換元=this.ScalarExpression(x.Parameter);
            var 変換先_Type=DataTypeReferenceからTypeに変換(x.DataType);
            return this.Convertデータ型を合わせるNullableは想定する(変換元,変換先_Type);
        }
        private e.Expression TryConvertCall(TryConvertCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ParseCall(ParseCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TryParseCall(TryParseCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CastCall(CastCall x){
            var Parameter=this.ScalarExpression(x.Parameter);
            var 変換先Type=DataTypeReferenceからTypeに変換(x.DataType);
            return this.Convertデータ型を合わせるNullableは想定する(Parameter,変換先Type);
        }
        private e.Expression TryCastCall(TryCastCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AtTimeZoneCall(AtTimeZoneCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FunctionCall(FunctionCall x){
            var FunctionName = x.FunctionName.Value.ToUpperInvariant();
            var x_Parameters = x.Parameters;
            switch(FunctionName) {
                //構成関数	現在の構成についての情報を返します。
                //変換関数	データ型のキャストと変換をサポートします。
                //カーソル関数	カーソルについての情報を返します。
                //日付と時刻のデータ型および関数	日付時刻型の入力値に対して操作を実行し、文字列値、数値、または日付時刻値を返します。
                case "DATEADD":{//(datepart,number,date)
                    var datepart = (ColumnReferenceExpression)x_Parameters[0];
                    Debug.Assert(datepart.ColumnType == ColumnType.Regular);
                    Debug.Assert(datepart.MultiPartIdentifier.Identifiers.Count == 1);
                    var number = this.ScalarExpression(x_Parameters[1]);
                    //dateadd(,,ここは日付型であってoffsetはいらない)
                    var date = this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[2]),typeof(DateTime));
                    return datepart.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                        "YYYY"or"YY"or"YEAR"     =>dateadd_int(Reflection.DateTime.AddYear),
                        "QQ"  or"Q" or"QUARTER"  =>dateadd_int(Reflection.DateTime.AddQuater),
                        "MM"  or"M" or"MONTH"    =>dateadd_int(Reflection.DateTime.AddMonths),
                        "DY"  or"Y" or"DAYOFYEAR"=>throw new NotSupportedException(datepart.MultiPartIdentifier.Identifiers[0].Value),
                        "DD"  or"D" or"DAY"      =>throw new NotSupportedException(datepart.MultiPartIdentifier.Identifiers[0].Value),
                        "DW"  or"W" or"WEEKDAY"  =>dateadd_double(Reflection.DateTime.AddDays),
                        "WK"  or"WW"or"WEEK"     =>dateadd_double(Reflection.DateTime.AddWeek),
                        "HH"  or"HOUR"           =>dateadd_double(Reflection.DateTime.AddHours),
                        "MI"  or"N" or"MINUTE"   =>dateadd_double(Reflection.DateTime.AddMinutes),
                        "SS"  or"S" or"SECOND"   =>dateadd_double(Reflection.DateTime.AddSeconds),
                        "MS"  or"MILLISECOND"    =>dateadd_double(Reflection.DateTime.AddMilliseconds),
                        "MCS" or"MICROSECOND"    =>dateadd_long(Reflection.DateTime.AddTicks,Constant_100000),
                        "NS"  or"NANOSECOND"     =>dateadd_long(Reflection.DateTime.AddTicks,Constant_100),
                        _=>throw new NotSupportedException(datepart.MultiPartIdentifier.Identifiers[0].Value),
                    };
                    e.MethodCallExpression dateadd_int(MethodInfo Method) => dateadd(Method,typeof(int));
                    e.MethodCallExpression dateadd_double(MethodInfo Method) => dateadd(Method,typeof(double));
                    e.MethodCallExpression dateadd(MethodInfo Method,Type Type) {
                        Debug.Assert(Method.GetParameters()[0].ParameterType == Type);
                        return e.Expression.Call(date,Method,this.Convertデータ型を合わせるNullableは想定する(number,Type));
                    }
                    e.BinaryExpression dateadd_long(MethodInfo Method,e.Expression Constant) => e.Expression.Divide(dateadd(Method,typeof(int)),Constant);
                }
                case "DATEDIFF":{
                    //datediff(mm,startdate,enddate)。enddate-startdate。小さな数値に切り捨てられる
                    Debug.Assert(x_Parameters.Count == 3);
                    var y = (ColumnReferenceExpression)x_Parameters[0];
                    var Method=y.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                        "YEAR" or"YY"   or"YYYY"     =>Product.SQLServer.Reflection.DATEPART.year,
                        "QQ"   or"Q"    or"QUARTER"  =>Product.SQLServer.Reflection.DATEPART.quarter,
                        "MM"   or"M"    or"MONTH"    =>Product.SQLServer.Reflection.DATEPART.month,
                        "DY"   or"Y"    or"DAYOFYEAR"=>Product.SQLServer.Reflection.DATEPART.dayofyear,
                        "DD"   or"D"    or"DAY"      =>Product.SQLServer.Reflection.DATEPART.day,
                        "WK"   or"WW"   or"WEEK"     =>Product.SQLServer.Reflection.DATEPART.week,//その年の何週目か
                        "DW"   or"W"    or"WEEKDAY"  =>Product.SQLServer.Reflection.DATEPART.weekday,
                        "HH"   or"HOUR"              =>Product.SQLServer.Reflection.DATEPART.hour,
                        "MI"   or"N"    or"MINUTE"   =>Product.SQLServer.Reflection.DATEPART.minute,
                        "SS"   or"S"    or"SECOND"   =>Product.SQLServer.Reflection.DATEPART.second,
                        "MS"   or"MILLISECOND"       =>Product.SQLServer.Reflection.DATEPART.millisecond,
                        "MCS"  or"MICROSECOND"       =>Product.SQLServer.Reflection.DATEPART.microsecond,
                        "NS"   or"NANOSECOND"        =>Product.SQLServer.Reflection.DATEPART.nanosecond,
                        _=>throw new NotSupportedException(y.MultiPartIdentifier.Identifiers[0].Value)
                    };
                    // 2021/11/11-2021/10/11→11-10
                    return this.NULLを返す2(
                        this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[2]),typeof(DateTime?)),
                        this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTime?)),
                       (arg0,arg1) => e.Expression.Subtract(
                            e.Expression.Call(Method,arg0),
                            e.Expression.Call(Method,arg1)
                        )
                    );
                }
                case "DATEPART":{
                    Debug.Assert(x_Parameters.Count == 2);
                    return this.NULLを返す1(
                        this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTime?)),
                        arg0=>{
                            var y = (ColumnReferenceExpression)x_Parameters[0];
                            var Method=y.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                                "YEAR" or"YY"   or"YYYY"     =>Product.SQLServer.Reflection.DATEPART.year,
                                "QQ"   or"Q"    or"QUARTER"  =>Product.SQLServer.Reflection.DATEPART.quarter,
                                "MM"   or"M"    or"MONTH"    =>Product.SQLServer.Reflection.DATEPART.month,
                                "DY"   or"Y"    or"DAYOFYEAR"=>Product.SQLServer.Reflection.DATEPART.dayofyear,
                                "DD"   or"D"    or"DAY"      =>Product.SQLServer.Reflection.DATEPART.day,
                                "WK"   or"WW"   or"WEEK"     =>Product.SQLServer.Reflection.DATEPART.week,//その年の何週目か
                                "DW"   or"W"    or"WEEKDAY"  =>Product.SQLServer.Reflection.DATEPART.weekday,
                                "HH"   or"HOUR"              =>Product.SQLServer.Reflection.DATEPART.hour,
                                "MI"   or"N"    or"MINUTE"   =>Product.SQLServer.Reflection.DATEPART.minute,
                                "SS"   or"S"    or"SECOND"   =>Product.SQLServer.Reflection.DATEPART.second,
                                "MS"   or"MILLISECOND"       =>Product.SQLServer.Reflection.DATEPART.millisecond,
                                "MCS"  or"MICROSECOND"       =>Product.SQLServer.Reflection.DATEPART.microsecond,
                                "NS"   or"NANOSECOND"        =>Product.SQLServer.Reflection.DATEPART.nanosecond,
                                "ISOWK"or"ISOWW"or"ISO_WEEK" =>Product.SQLServer.Reflection.DATEPART.iso_week,
                                "TZ"   or"TZOFFSET"          =>Product.SQLServer.Reflection.DATEPART.tzoffset,
                                _=>throw new NotSupportedException(y.MultiPartIdentifier.Identifiers[0].Value)
                            };
                            return e.Expression.Call(Method,arg0);
                        });
                    //return e.Expression.Call(interval(x_Parameters[0]),this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTimeOffset?)));
                }
                case "DATENAME":{
                    Debug.Assert(x_Parameters.Count == 2);
                    var y = (ColumnReferenceExpression)x_Parameters[0];
                    var Method=y.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                        "YEAR"or"YY"or"YYYY"     =>Product.SQLServer.Reflection.DATENAME.year,
                        "QQ"  or"Q" or"QUARTER"  =>Product.SQLServer.Reflection.DATENAME.quarter,
                        "MM"  or"M" or"MONTH"    =>Product.SQLServer.Reflection.DATENAME.month,
                        "DY"  or"Y" or"DAYOFYEAR"=>Product.SQLServer.Reflection.DATENAME.dayofyear,
                        "DD"  or"D" or"DAY"      =>Product.SQLServer.Reflection.DATENAME.day,
                        "WK"  or"WW"or"WEEK"     =>Product.SQLServer.Reflection.DATENAME.week,//その年の何週目か
                        "DW"  or"W" or"WEEKDAY"  =>Product.SQLServer.Reflection.DATENAME.weekday,
                        "HH"  or"HOUR"           =>Product.SQLServer.Reflection.DATENAME.hour,
                        "MI"  or"N" or"MINUTE"   =>Product.SQLServer.Reflection.DATENAME.minute,
                        "SS"  or"S" or"SECOND"   =>Product.SQLServer.Reflection.DATENAME.second,
                        "MS"  or"MILLISECOND"    =>Product.SQLServer.Reflection.DATENAME.millisecond,
                        "MCS" or"MICROSECOND"    =>Product.SQLServer.Reflection.DATENAME.microsecond,
                        "NS"  or"NANOSECOND"     =>Product.SQLServer.Reflection.DATENAME.nanosecond,
                        _=>throw new NotSupportedException(y.MultiPartIdentifier.Identifiers[0].Value)
                    };
                    //return e.Expression.Call(Method,this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTimeOffset?)));
                    return this.NULLを返す1(
                        this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTime?)),
                        arg0=>e.Expression.Call(Method,arg0)
                    );
                }
                //CAST および CONVERT(Transact - SQL)
                //case "cast":{
                //    return e.Expression.Call(Product.SQLServer.Reflection.cast,
                //        this.ScalarExpression(x_Parameters[0]),
                //        this.ScalarExpression(x_Parameters[1])
                //    );
                //}
                //case "convert":{
                //    return e.Expression.Call(Product.SQLServer.Reflection.cast,
                //        e.Expression.Constant(CommonLibrary.SQLのTypeからTypeに変換(this.SQL取得(x_Parameters[0]))),
                //        this.ScalarExpression(x_Parameters[1])
                //    );
                //}
                //システム関数
                case "ISNULL":{
                    var 変換元 = this.ScalarExpression(x_Parameters[0]);
                    var 変換先 = this.ScalarExpression(x_Parameters[1]);
                    this.判定指定Table.実行(変換元,this.RefPeek.List_TableExpression);
                    e.Expression? Predicate0 = null;
                    var 出力TableExpressions = this.出力TableExpressions;
                    if(出力TableExpressions.Count > 0) {
                        var Predicate = e.Expression.NotEqual(出力TableExpressions[0],Constant_null);
                        var 出力TableExpressions_Count = 出力TableExpressions.Count;
                        for(var a = 1;a < 出力TableExpressions_Count;a++)
                            Predicate = e.Expression.AndAlso(Predicate,e.Expression.NotEqual(出力TableExpressions[a],Constant_null));
                        //変換元=e.Expression.Condition(Predicate,変換元,変換先);
                    }

                    //var (L1,R1)=this.Convertデータ型を合わせるNullableは想定する(L0,R0);
                    //foreach(var a in 出力TableExpressions)
                    //    変換元=e.Expression.Condition(e.Expression.NotEqual(a,Constant_null),変換元,Constant_null);
                    //byte?a;int?b
                    //if(a.HasValue)return a else b
                    //int?a;byte?b
                    //if(a.HasValue)return a else new Nullable<int>(b.GetValueOrDefault())
                    //new Nullable<int>()
                    var 変換元_Type = 変換元.Type;
                    var 変換先_Type = 変換先.Type;
                    if(変換元_Type == 変換先_Type) return 変換元;
                    var 変換元_Type_IsNullable = 変換元_Type.IsNullable();
                    var 変換先_Type_IsNullable = 変換先_Type.IsNullable();
                    if(!変換元_Type.IsValueType) {
                        var Predicate1 = e.Expression.NotEqual(変換元,Constant_null);
                        Predicate0=Predicate0 is not null?e.Expression.AndAlso(Predicate0,Predicate1):Predicate1;
                        if(!変換先_Type.IsValueType) {
                            //object=(object)??(string)
                            (変換元, 変換先) = this.Convertデータ型を合わせるNullableは想定しない(変換元,変換先);
                            return e.Expression.Condition(Predicate0,変換元,変換先);
                        } else if(変換先_Type_IsNullable) {
                            //object=(object)??(int?)
                            var Element変換先 = GetValueOrDefault(変換先);
                            (変換元, Element変換先) = this.Convertデータ型を合わせるNullableは想定しない(変換元,Element変換先);
                            //変換元0=this.Convertデータ型を合わせるNullableは想定しない(Element変換元,Element変換先.Type);
                            return e.Expression.Condition(Predicate0,変換元,e.Expression.Condition(e.Expression.Property(変換先,"HasValue"),Element変換先,e.Expression.Default(変換先.Type)));
                        } else {
                            //object=(object)??(int)
                            return e.Expression.Condition(Predicate0,変換元,e.Expression.Convert(変換先,変換元_Type));
                        }
                    } else if(変換元_Type_IsNullable) {
                        var Predicate1 = e.Expression.Property(変換元,"HasValue");
                        if(Predicate0 is not null) Predicate0 = e.Expression.AndAlso(Predicate0,Predicate1);
                        else Predicate0 = Predicate1;
                        if(!変換先_Type.IsValueType) {
                            //object=(int?)??(object)
                            var Element変換元 = GetValueOrDefault(変換元);
                            (Element変換元, 変換先) = this.Convertデータ型を合わせるNullableは想定しない(Element変換元,変換先);
                            return e.Expression.Condition(Predicate0,Element変換元,変換先);
                        } else if(変換先_Type_IsNullable) {
                            //double?=(int?)??(double?)
                            var Element変換元 = GetValueOrDefault(変換元);
                            var Element変換先 = GetValueOrDefault(変換先);
                            (Element変換元, Element変換先) = this.Convertデータ型を合わせるNullableは想定しない(Element変換元,Element変換先);
                            return e.Expression.Condition(Predicate0,Element変換元,Element変換先);
                        } else {
                            //double?=(int?)??(double)
                            var Element変換元 = GetValueOrDefault(変換元);
                            (Element変換元, 変換先) = this.Convertデータ型を合わせるNullableは想定しない(Element変換元,変換先);
                            //     var Element変換元0=this.Convertデータ型を合わせるNullableは想定しない(Element変換元,変換先0.Type);
                            return e.Expression.Condition(Predicate0,Element変換元,変換先);
                        }
                    } else{
                        //decimal=(decimal)??(int)
                        return 変換元;
                    }
                    //throw new NotSupportedException($"ISNULL({変換元_Type.FullName},{変換先_Type.FullName})は出来ない");
                    //return e.Expression.Coalesce(変換元,変換先);
                }
                //case "rank":{
                //    var x_Parameters=x_Parameters;
                //    Debug.Assert(x_Parameters.Count==0);
                //    return Expressions.Expression.Call(
                //        this.ScalarExpression(x_Parameters[0]),
                //        Product.SQLServer.Reflection.Rank
                //    );
                //}
                //rank
                //e.Expression InstanceMethod1(MethodInfo Method){
                //    var Parameters=Method.GetParameters();
                //    return this.NULLUnary(
                //        this.ScalarExpression(x_Parameters[0]),
                //       (arg1)=>e.Expression.Call(arg1,Method)
                //    );
                //}
                //OPEN XML
                case "VALUE":{
                    Debug.Assert(x_Parameters.Count == 2);
                    //'nvarchar'のようにリテラルなのでシングルクォートなので削除するべき
                    var DBType = this.SQL取得(x_Parameters[1])[1..^1];
                    //Typeに合うようにExpressionを変形する()
                    var value=DBType switch{
                        "BIT"             =>Product.SQLServer.Reflection.value_Boolean,
                        "TINYINT"         =>Product.SQLServer.Reflection.value_SByte,
                        "SMALLINT"        =>Product.SQLServer.Reflection.value_Int16,
                        "INTEGER"or"INT"  =>Product.SQLServer.Reflection.value_Int32,
                        "BIGINT"          =>Product.SQLServer.Reflection.value_Int64,
                        "REAL"            =>Product.SQLServer.Reflection.value_Single,
                        "FLOAT"           =>Product.SQLServer.Reflection.value_Double,
                        "MONEY"or"DECIMAL"=>Product.SQLServer.Reflection.value_Decimal,
                        "DATETIME"        =>Product.SQLServer.Reflection.value_DateTime,
                        "XML"             =>Product.SQLServer.Reflection.value_XElement,
                        "UNIQUEIDENTIFIER"=>Product.SQLServer.Reflection.value_Guid,
                        _=>DBType.Length>=4
                            ?DBType[..4]is"CHAR"or"TEXT"
                                ?Product.SQLServer.Reflection.value_String
                                :DBType.Length>=5
                                    ?DBType[..5]is"NCHAR"or"NTEXT"
                                        ?Product.SQLServer.Reflection.value_String
                                        :DBType.Length>=7
                                            ?DBType[..7]is"DECIMAL"
                                                ?Product.SQLServer.Reflection.value_Decimal
                                                :DBType[..7]is"VARCHAR"or"SYSNAME"
                                                    ?Product.SQLServer.Reflection.value_String
                                                    :DBType.Length>=8
                                                        ?DBType[..8]is"NVARCHAR"
                                                            ?Product.SQLServer.Reflection.value_String
                                                            :throw new NotSupportedException(DBType)
                                                        :throw new NotSupportedException(DBType)
                                            :throw new NotSupportedException(DBType)
                                    :throw new NotSupportedException(DBType)
                            :throw new NotSupportedException(DBType)
                    };
                    //switch(DBType) {
                    //    case "bit":value = Product.SQLServer.Reflection.value_Boolean; break;
                    //    case "tinyint":value = Product.SQLServer.Reflection.value_SByte; break;
                    //    case "smallint":value = Product.SQLServer.Reflection.value_Int16; break;
                    //    case "integer":
                    //    case "int":value = Product.SQLServer.Reflection.value_Int32; break;
                    //    case "bigint":value = Product.SQLServer.Reflection.value_Int64; break;
                    //    //case //"hierarchyid":
                    //    case "real":value = Product.SQLServer.Reflection.value_Single; break;
                    //    case "float":value = Product.SQLServer.Reflection.value_Double; break;
                    //    case "money":
                    //    case "decimal":value = Product.SQLServer.Reflection.value_Decimal; break;
                    //    //"numeric" :value=SQLServer.Reflection.value_Decimal;break;
                    //    //"smallmoney" :value=SQLServer.Reflection.value_smallmoney;break;
                    //    //"money" :value=SQLServer.Reflection.value_money;break;
                    //    //"char":
                    //    //"varchar":
                    //    //"nchar":
                    //    //"nvarchar":
                    //    //"text":
                    //    //"ntext":
                    //    //"sysname":
                    //    //    typeof(String);break;
                    //    //"date" :value=SQLServer.Reflection.value_DateTime
                    //    case "datetime":value = Product.SQLServer.Reflection.value_DateTime; break;
                    //    //"datetime2" :value=SQLServer.Reflection.value_DateTime;break;
                    //    //"smalldatetime" :value=SQLServer.Reflection.value_DateTime;break;
                    //    //"datetimeoffset" :value=SQLServer.Reflection.value_DateTime;break;
                    //    //"binary" :value=SQLServer.Reflection.value_Bytes;break;
                    //    //"varbinary" :value=SQLServer.Reflection.value_money;break;
                    //    //"geography" :value=SQLServer.Reflection.value_money;break;
                    //    //"geometry" :value=SQLServer.Reflection.value_money;break;
                    //    //"image" :value=SQLServer.Reflection.value_money;break;
                    //    //"sql_variant" :value=SQLServer.Reflection.value_money;break;
                    //    case "xml":value = Product.SQLServer.Reflection.value_XElement; break;
                    //    case "uniqueidentifier":value = Product.SQLServer.Reflection.value_Guid; break;
                    //    default:
                    //    if(DBType.Length >= 4)
                    //        if(DBType[..4] == "char" || DBType[..4] == "text")
                    //            value = Product.SQLServer.Reflection.value_String;
                    //        else if(DBType.Length >= 5)
                    //            if(DBType[..5] == "nchar" || DBType[..5] == "ntext")
                    //                value = Product.SQLServer.Reflection.value_String;
                    //            else if(DBType.Length >= 7)
                    //                if(DBType[..7] == "decimal")
                    //                    value = Product.SQLServer.Reflection.value_Decimal;
                    //                else if(DBType[..7] == "varchar" || DBType[..7] == "sysname")
                    //                    value = Product.SQLServer.Reflection.value_String;
                    //                else if(DBType.Length >= 8)
                    //                    if(DBType[..8] == "nvarchar")
                    //                        value = Product.SQLServer.Reflection.value_String;
                    //                    else throw new NotSupportedException(DBType);
                    //                else throw new NotSupportedException(DBType);
                    //            else throw new NotSupportedException(DBType);
                    //        else throw new NotSupportedException(DBType);
                    //    else throw new NotSupportedException(DBType);
                    //    break;
                    //    //"time" :value=SQLServer.Reflection.value_money;break;
                    //    //"hierarchyid" :value=SQLServer.Reflection.value_money;break;
                    //}
                    //    _=>
                    //        if(DBType[..8]=="nvarchar"||dbtype[..7]=="varchar"||dbtype[..5]=="nchar"||dbtype[..4]=="char"||dbtype[..4]=="text"||dbtype[..5]=="ntext"||dbtype[..7]=="sysname"){
                    //            typeof(String),
                    //        }
                    //        throw new NotSupportedException(DBType),
                    //}
                    var f0 = this.CallTarget(x.CallTarget);
                    var f1 = this.ScalarExpression(x_Parameters[0]);
                    return e.Expression.Call(
                        value,
                        this.CallTarget(x.CallTarget),
                        this.ScalarExpression(x_Parameters[0])
                    );
                }
                //引数をメソッド情報から適切にキャスト
                default:{
                    var 名前一致Methods = typeof(Product.SQLServer.Methods).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(p => string.Equals(p.Name,FunctionName,StringComparison.OrdinalIgnoreCase)).ToList();
                    if(名前一致Methods.Count==0) break;
                    //1,名前が一致して1つだけならそれ
                    //特殊化すべき関数
                    //convert(intなど特殊な予約語
                    var x_Parameters_Count = x_Parameters.Count;
                    var ScalarExpressions = new e.Expression[x_Parameters_Count];
                    for(var a = 0;a < x_Parameters_Count;a++) {
                        if("convert" == FunctionName) {
                            if(a == 0) {
                                ScalarExpressions[a] = e.Expression.Constant(CommonLibrary.SQLのTypeからTypeに変換(this.SQL取得(x_Parameters[0])));
                                continue;
                            }
                        }
                        ScalarExpressions[a] = this.ScalarExpression(x_Parameters[a]);
                    }
                    //メソッドが1つだけの場合
                    e.MethodCallExpression MethodCall=null!;
                    if(共通(名前一致Methods,ref MethodCall))return MethodCall;
                    //省略なくぴったり引数の数が一致しているもの
                    if(共通(名前一致Methods.Where(Method =>ScalarExpressions.Length==Method.GetParameters().Length),ref MethodCall))return MethodCall;
                    //f(int a,int b=3,params int[]c)
                    //1,引数の数が一致         f(1 , 2,3 )
                    //2,引数の型が一致         f(1d,2d,3d)
                    //ぴったり一致する型(int,double),(int,int,double=1.0)に対して(int,int)は右で解決
                    if(
                        共通(
                            名前一致Methods.Where(
                                Method => {
                                    var Method_Parameters = Method.GetParameters();
                                    var Method_Parameters_Length = Method_Parameters.Length;
                                    var ScalarExpressions_Length = ScalarExpressions.Length;
                                    for(var a="convert" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                        var Method_Parameter = Method_Parameters[a];
                                        var ScalarExpression_Type = ScalarExpressions[a].Type;
                                        if(Method_Parameter.ParameterType!=ScalarExpression_Type) return false;
                                    }
                                    return true;
                                }
                            ),ref MethodCall
                        )
                    )return MethodCall;
                    //3,ジェネリックな型。推論は難しいので今は思いつかない
                    //4,親クラス多段に派生している場合、近い型ほど優先 インターフェースも同様だがインターフェースと親クラスの両方があった場合解決できない
                    //近い型は難しいので祖先であるかどうかだけ見る方法にしている
                    if(
                        共通(
                            名前一致Methods.Where(
                                Method =>{
                                    var Method_Parameters = Method.GetParameters();
                                    var Method_Parameters_Length = Method_Parameters.Length;
                                    var ScalarExpressions_Length = ScalarExpressions.Length;
                                    for(var a="convert" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                        var Method_Parameter = Method_Parameters[a];
                                        var ScalarExpression_Type = ScalarExpressions[a].Type;
                                        if(!Method_Parameter.ParameterType.IsAssignableFrom(ScalarExpression_Type)) return false;
                                    }
                                    return true;
                                }
                            ),ref MethodCall
                        )
                    )return MethodCall;
                    //暗黙的に変換できる型
                    if(
                        共通(
                            名前一致Methods.Where(
                                Method =>{
                                    var Method_Parameters = Method.GetParameters();
                                    var Method_Parameters_Length = Method_Parameters.Length;
                                    var ScalarExpressions_Length = ScalarExpressions.Length;
                                    for(var a="convert" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                        var Method_Parameter = Method_Parameters[a];
                                        var ScalarExpression_Type = ScalarExpressions[a].Type;
                                        if(!Method_Parameter.ParameterType.IsAssignableFrom(ScalarExpression_Type)){
                                            if(this.作業配列.GetMethod(Method_Parameter.ParameterType,"op_Implicit",ScalarExpression_Type)is not null){
                                            }else if(this.作業配列.GetMethod(ScalarExpression_Type,"op_Implicit",Method_Parameter.ParameterType)is not null){
                                            }else return false;
                                        }
                                    }
                                    return true;
                                }
                            ),ref MethodCall
                        )
                    )return MethodCall;
                    //その型が実装しているインターフェイス
                    //ユーザー定義の型変換がある場合
                    //object 
                    //4,引数の代入可能型が一致
                    //var 引数の数が同じMethods = NameMethods;
                    //2,引数の数が完全一致、オプション引数、可変長引数を考慮してもあり得るメソッド群
                    var 名前一致_既定値_可変長引数BaseMethods = 名前一致Methods.Where(
                        Method => {
                            if(Method.Name=="concat"){

                            }
                            var Method_Parameters = Method.GetParameters();
                            var Method_Parameters_Length = Method_Parameters.Length;
                            var ScalarExpressions_Length = ScalarExpressions.Length;
                            //パラメータ数が一致したら一次審査は通る
                            //(int,int=3)に(int)
                            //if(Method_Parameters_Length==0&&ScalarExpressions_Length>0) return false;
                            if(ScalarExpressions_Length<Method_Parameters_Length){
                                //(int=0,params[]int)または
                                //(int=0,int=0)メソッドを
                                //()呼び出し
                                for(var a = ScalarExpressions_Length;a<Method_Parameters_Length-1;a++) {
                                    var Method_Parameter = Method_Parameters[a];
                                    if(!Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute))) return false;
                                }
                                {
                                    var Method_Parameter = Method_Parameters[Method_Parameters_Length-1];
                                    if(!(Method_Parameter.IsDefined(typeof(ParamArrayAttribute))&&Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute)))) return false;
                                }
                            } else{
                                //(params[]int)メソッドを
                                //(1,2)呼び出し
                                var Method_Parameter = Method_Parameters[Method_Parameters_Length-1];
                                if(!Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) return false;
                            }
                            return true;
                            //if(ScalarExpressions_Length==Method_Parameters_Length-1) {
                            //    //(params int[])Method_Parameters_Length==3
                            //    //()は一致する
                            //    var Method_Parameter = Method_Parameters[ScalarExpressions_Length];
                            //    Debug.Assert(Attribute.IsDefined(Method_Parameter,typeof(ParamArrayAttribute))==Method_Parameter.IsDefined(typeof(ParamArrayAttribute)));
                            //    Debug.Assert(Attribute.IsDefined(Method_Parameter,typeof(System.Runtime.InteropServices.OptionalAttribute))==Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute)));
                            //    return Attribute.IsDefined(Method_Parameter,typeof(ParamArrayAttribute))||Attribute.IsDefined(Method_Parameter,typeof(System.Runtime.InteropServices.OptionalAttribute));
                            //} else {
                            //    //(params int[])Method_Parameters_Length==3
                            //    //(1,2,3)は一致する
                            //    for(var a = Method_Parameters_Length;a<ScalarExpressions_Length;a++) {
                            //        var Method_Parameter = Method_Parameters[a];
                            //        if(!Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) return false;
                            //        if(!Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute))) return false;
                            //    }
                            //    return true;
                            //}
                        }
                    ).ToList();
                    if(FunctionName=="concat"){

                    }
                    if(
                        共通(
                            名前一致_既定値_可変長引数BaseMethods.Where(
                                Method => {
                                    var Method_Parameters = Method.GetParameters();
                                    var Method_Parameters_Length = Method_Parameters.Length;
                                    var ScalarExpressions_Length = ScalarExpressions.Length;
                                    for(var a="CONVERT" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                        var Method_Parameter = Method_Parameters[a];
                                        var ScalarExpression_Type = ScalarExpressions[a].Type;
                                        if(Method_Parameter.ParameterType!=ScalarExpression_Type) return false;
                                    }
                                    return true;
                                }
                            ),ref MethodCall
                        )
                    )return MethodCall;

                    if(
                        共通(
                            名前一致Methods.Where(
                                Method => {
                                    var Method_Parameters = Method.GetParameters();
                                    var Method_Parameters_Length = Method_Parameters.Length;
                                    var ScalarExpressions_Length = ScalarExpressions.Length;
                                    //パラメータ数が一致したら一次審査は通る
                                    //(int,int=3)に(int)
                                    if(Method_Parameters_Length==0&&ScalarExpressions_Length>0) return false;
                                    for(var a="CONVERT" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                        var Method_Parameter = Method_Parameters[a];
                                        var ScalarExpression_Type = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[a],Method_Parameter.ParameterType).Type;
                                        if(Method_Parameter.ParameterType!=ScalarExpression_Type) return false;
                                    }
                                    return true;
                                }
                            ),ref MethodCall
                        )
                    ) return MethodCall;
                    break;
                    bool 共通(IEnumerable<MethodInfo> Methods,ref e.MethodCallExpression MethodCall) {
                        var List_Method=Methods.ToList();
                        if(List_Method.Count!=1) return false;
                        var Method= List_Method[0];
                        var Method_Parameters = Method.GetParameters();
                        var Arguments_Length = Method_Parameters.Length;
                        switch(Arguments_Length) {
                            case 0:MethodCall = e.Expression.Call(Method); return true;
                            case 1:MethodCall = e.Expression.Call(Method,@params(0)); return true;
                            case 2:MethodCall = e.Expression.Call(Method,@params(0),@params(1)); return true;
                            case 3:MethodCall = e.Expression.Call(Method,@params(0),@params(1),@params(2)); return true;
                            case 4:MethodCall = e.Expression.Call(Method,@params(0),@params(1),@params(2),@params(3)); return true;
                            default:{
                                var Arguments = new e.Expression[Arguments_Length];
                                for(var a = 0;a < Arguments_Length;a++) Arguments[a] = @params(a);
                                MethodCall = e.Expression.Call(Method,Arguments);
                                return true;
                            }
                        }
                        e.Expression @params(int index) {
                            if(index < ScalarExpressions.Length) {
                                var Method_Parameter = Method_Parameters[index];
                                //メソッド定義の引数より実際の引数が多かった場合、可変長引数だったらそのように処理する
                                if(Method_Parameter.IsDefined(typeof(Product.SQLServer.TypeAttribute))) {
                                    return ScalarExpressions[index];
                                } else if(Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) {
                                    //func(params int[]c)
                                    var params_Length = x_Parameters!.Count - 1;
                                    var param = new e.Expression[params_Length];
                                    var ElementType = Method_Parameter.ParameterType.GetElementType();
                                    for(var a = 0;a < params_Length;a++)
                                        param[a] = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index + a],ElementType);
                                    return e.Expression.NewArrayInit(ElementType,param);
                                } else return this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index],Method_Parameter.ParameterType);
                            } else if(index < Method_Parameters.Length) {
                                //メソッド定義の引数より実際の引数が少なかった場合、オプション引数だったらそのように処理する
                                //func(int a,int b=3,params int[]c)
                                var Method_Parameter = Method_Parameters[index];
                                if(Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute)))
                                    return e.Expression.Constant(Method_Parameter.DefaultValue,Method_Parameters![index].ParameterType);
                                if(Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) {
                                    var params_Length = x_Parameters!.Count - 1;
                                    var param = new e.Expression[params_Length];
                                    var ElementType = Method_Parameter.ParameterType.GetElementType();
                                    for(var a = 0;a < params_Length;a++)
                                        param[a] = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index + a],ElementType);
                                    return e.Expression.NewArrayInit(ElementType,param);
                                }
                                return this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index],Method_Parameter.ParameterType);
                            } else {
                                //func(int a,int b=3,params int[]c)
                                var Method_Parameter = Method_Parameters[Method_Parameters.Length-1];
                                if(Attribute.IsDefined(Method_Parameter,typeof(ParamArrayAttribute))) {
                                    var params_Length = x_Parameters!.Count - 1;
                                    var param = new e.Expression[params_Length];
                                    var ElementType = Method_Parameter.ParameterType.GetElementType();
                                    for(var a = 0;a < params_Length;a++)
                                        param[a] = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index + a],ElementType);
                                    return e.Expression.NewArrayInit(ElementType,param);
                                } else throw new NotSupportedException("引数が少なかった。仮引数にはデフォルトパラメーターがなかった。parms可変長引数0を指定したわけでもない。");
                            }
                        }
                    }
                }
            }
            if(x.CallTarget is not null) {
                var Schema = this.CallTarget(x.CallTarget);
                var Method = Schema.Type.GetMethod(FunctionName,BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
                Debug.Assert(Method!=null);
                var Parameters = Method.GetParameters();
                var arguments_Length = x_Parameters.Count;
                var arguments = new e.Expression[arguments_Length];
                for(var a = 0;a < arguments_Length;a++)
                    arguments[a] = this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[a]),Parameters[a].ParameterType);
                return e.Expression.Call(Schema,Method,arguments);
            }
            var AggregateFunction=this.AggregateFunction(x_Parameters[0],FunctionName);
            if(AggregateFunction is not null)return AggregateFunction;
            //Barから派生したクラスのインスタンスで引数なしで動的メソッドを呼び出します。DynamicObject
            //dynamic dynamicObject = new DerivedFromDynamicObject();
            //var callSiteBinder = Binder.InvokeMember(CSharpBinderFlags.None,"Bar",Enumerable.Empty<Type>(),typeof(Program),
            //    new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null) });
            //var callSite = CallSite<Action<CallSite,object>>.Create(callSiteBinder);
            //callSite.Target(callSite,dynamicObject);
            throw new NotSupportedException($"{FunctionName}関数が定義されていなかった");
            //{ 
            //    var Functions=this.Functions;
            //    var x_Parameters_Count= x_Parameters.Count;
            //    //var Arguments_Length = x_Parameters.Count;
            //    var Types=new Type[x_Parameters_Count];
            //    var Arguments = new e.Expression[x_Parameters_Count+2];
            //    Arguments[0]=Argument;
            //    var CSharpArgumentInfos = new RuntimeBinder.CSharpArgumentInfo[x_Parameters_Count+1];
            //    for(var a=0;a<x_Parameters_Count;a++){
            //        var Argument=this.ScalarExpression(x_Parameters[a]);
            //        Arguments[a+1]=Argument;
            //        Types[a]=Argument.Type;
            //        CSharpArgumentInfos[a]=CSharpArgumentInfo;
            //    }
            //    CSharpArgumentInfos[x_Parameters_Count]=CSharpArgumentInfo;
            //    var sb=new StringBuilder();
            //    foreach(var Type in Types) {
            //        sb.Append(Type.Name+',');
            //    }
            //    if(!Functions.ContainsKey(FunctionName))Functions.Add(FunctionName,sb.ToString());
            //    var InvokeMember = RuntimeBinder.Binder.InvokeMember(RuntimeBinder.CSharpBinderFlags.None,FunctionName,null,typeof(変換_TSqlFragmentからExpression),CSharpArgumentInfos);
            //    return e.Expression.Dynamic(InvokeMember,typeof(object),Arguments);
            //}
        }
        //private static readonly RuntimeBinder.CSharpArgumentInfo CSharpArgumentInfo = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null);
        private struct 一致度:IComparable<一致度> {
            private readonly int 引数数差;
            private readonly int 親クラスとの近さ;
            private readonly int インターフェース;
            private readonly int 暗黙型変換;
            public 一致度(int 引数数差,int 親クラスとの近さ,int インターフェース,int 暗黙型変換) {
                this.引数数差=引数数差;
                this.親クラスとの近さ=親クラスとの近さ;
                this.インターフェース=インターフェース;
                this.暗黙型変換=暗黙型変換;
            }
            public int CompareTo(一致度 other){
                int x;
                x=this.引数数差-other.引数数差; if(x!=0) return x;
                x=this.親クラスとの近さ-other.親クラスとの近さ; if(x!=0) return x;
                x=this.インターフェース-other.インターフェース; if(x!=0) return x;
                x=this.暗黙型変換-other.暗黙型変換; if(x!=0) return x;
                return 0;
            }
        }
        public readonly SortedDictionary<string,string>Functions=new();
        private e.Expression LeftFunctionCall(LeftFunctionCall x)=>this.LeftRightFunctionCall(x.Parameters,Product.SQLServer.Reflection.left);
        private e.Expression RightFunctionCall(RightFunctionCall x)=>this.LeftRightFunctionCall(x.Parameters,Product.SQLServer.Reflection.right);
        private e.Expression LeftRightFunctionCall(IList<ScalarExpression>Parameters,MethodInfo Method)=>e.Expression.Call(
            Method,
            this.ConvertNullable(this.ScalarExpression(Parameters[0])),
            this.ConvertNullable(this.ScalarExpression(Parameters[1]))
        );
        private e.Expression PartitionFunctionCall(PartitionFunctionCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ParameterlessCall(ParameterlessCall x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// ScalarSubquery orPrimaryExpression:ScalarExpression:TSqlFragment
        /// (1列を返すSELECT...)呼び出した側が1行だけ使うか複数(IN)使うか判断。EXISTS(SELECT...)はScalarSubqueryではない
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression Subquery(ScalarSubquery x){
            var StackSubquery単位の情報=this._StackSubquery単位の情報;
            StackSubquery単位の情報.Push();
            var MethodCall=(e.MethodCallExpression)this.QueryExpression(x.QueryExpression);
            StackSubquery単位の情報.Pop();
            var MethodCall_Arguments = MethodCall.Arguments;
            var Source=MethodCall_Arguments[0];
            var Lambda= (e.LambdaExpression)MethodCall_Arguments[1];
            var Parameters = Lambda.Parameters;
            var Lambda_Body=nameof(ExtensionSet.Select)==MethodCall.Method.Name?((e.NewExpression)Lambda.Body).Arguments[0]:e.Expression.Field(Parameters[0],nameof(ValueTuple<int>.Item1));
            var 作業配列=this.作業配列;
            var Lambda_Body_Type=Lambda_Body.Type;
            return e.Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,IEnumerable1のT(Source.Type),Lambda_Body_Type),
                Source,e.Expression.Lambda(Lambda_Body,Parameters)
            );
        }
        private e.Expression OdbcFunctionCall(OdbcFunctionCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ParenthesisExpression(ParenthesisExpression x){
            var y=this.ScalarExpression(x.Expression);
            return y;
        }
        private e.Expression ColumnReferenceExpression(ColumnReferenceExpression x){
            switch(x.ColumnType){
                case ColumnType.Regular:{
                    var 識別子=this.SQL取得(x);
                    var Result=this._StackSubquery単位の情報[識別子];
                    Debug.Assert(Result is not null);
                    return Result;
                }
                default:{
                    throw this.単純NotSupportedException(x);
                }
            }
        }
        private e.Expression NextValueForExpression(NextValueForExpression x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExtractFromExpression(ExtractFromExpression x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OdbcConvertSpecification(OdbcConvertSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private static e.Expression Stringをキャスト(e.Expression Left,string Right){
            var Left_Type=Left.Type;
            if(Left_Type==typeof(string))return Left;
            var Constant=e.Expression.Constant(Right);
            if(typeof(byte)==Left_Type||typeof(ushort)==Left_Type||typeof(uint)==Left_Type||typeof(char)==Left_Type||typeof(sbyte)==Left_Type||typeof(short)==Left_Type||typeof(int)==Left_Type){
                return e.Expression.Call(Reflection.Int32.Parse_s,Constant);
            }else if(typeof(byte?)==Left_Type||typeof(ushort?)==Left_Type||typeof(uint?)==Left_Type||typeof(char?)==Left_Type||typeof(sbyte?)==Left_Type||typeof(short?)==Left_Type||typeof(int?)==Left_Type){
                return e.Expression.Convert(
                    e.Expression.Call(Reflection.Int32.Parse_s,Constant),
                    typeof(int?)
                );
            }else if(typeof(ulong)==Left_Type||typeof(long)==Left_Type){
                return e.Expression.Call(Reflection.Int64.Parse_s,Constant);
            }else if(typeof(ulong?)==Left_Type||typeof(long?)==Left_Type){
                return e.Expression.Convert(
                    e.Expression.Call(Reflection.Int64.Parse_s,Constant),
                    typeof(long?)
                );
            }else if(typeof(float)==Left_Type){
                return e.Expression.Call(Reflection.Single.Parse_s,Constant);
            }else if(typeof(double)==Left_Type){
                return e.Expression.Call(Reflection.Double.Parse_s,Constant);
            }else if(typeof(float?)==Left_Type){
                return e.Expression.Convert(
                    e.Expression.Call(Reflection.Single.Parse_s,Constant),
                    Left_Type
                );
            }else if(typeof(double?)==Left_Type){
                return e.Expression.Convert(
                    e.Expression.Call(Reflection.Double.Parse_s,Constant),
                    Left_Type
                );
            }
            throw new NotSupportedException($"\"{Right}\"を{Left_Type.FullName}にキャストできない。");
        }
        //,Dictionary<(Type Left,Type Right),Type> Dictionary

        private e.Expression BinaryExpression(BinaryExpression x){
            //const Int64 DayTicks=937840050000;
            var Left=this.ScalarExpression(x.FirstExpression);
            var Right=this.ScalarExpression(x.SecondExpression);
            if(Right.Type.IsArray) {

            }
            return x.BinaryExpressionType switch{
                BinaryExpressionType.Add       =>共通(e.ExpressionType.Add),
                BinaryExpressionType.Subtract  =>共通(e.ExpressionType.Subtract),
                BinaryExpressionType.Multiply  =>共通(e.ExpressionType.Multiply),
                BinaryExpressionType.Divide    =>共通(e.ExpressionType.Divide),
                BinaryExpressionType.Modulo    =>共通(e.ExpressionType.Modulo),
                BinaryExpressionType.BitwiseAnd=>共通(e.ExpressionType.And),
                BinaryExpressionType.BitwiseOr =>共通(e.ExpressionType.Or),
                BinaryExpressionType.BitwiseXor=>共通(e.ExpressionType.ExclusiveOr),
                _=>throw this.単純NotSupportedException(x)
            };
            //    BinaryExpressionType.Add=>Expressions.Expression.Add(Left,Right),
            //    BinaryExpressionType.Subtract=>Expressions.Expression.Subtract(Left,Right),
            //    BinaryExpressionType.Multiply=>Expressions.Expression.Multiply(Left,Right),
            //    BinaryExpressionType.Divide=>Expressions.Expression.Divide(Left,Right),
            //    BinaryExpressionType.Modulo=>Expressions.Expression.Modulo(Left,Right),
            //    BinaryExpressionType.BitwiseAnd=>Expressions.Expression.Add(Left,Right),
            //    BinaryExpressionType.BitwiseOr=>Expressions.Expression.Or(Left,Right),
            //    BinaryExpressionType.BitwiseXor=>Expressions.Expression.ExclusiveOr(Left,Right),
            //    _=>throw this.単純NotSupportedException(x)
            //};
            e.Expression 共通(e.ExpressionType NodeType)=>this.NULLを返す2(
                Left,Right,
               (Left0,Right0)=>{
                   var(Left1,Right1)=this.Convertデータ型を合わせるNullableは想定しない(Left0,Right0);
                   if(NodeType==e.ExpressionType.Add&&Left1.Type==typeof(string))
                       return e.Expression.Add(Left1,Right1,Reflection.String.Concat_str0_str1);
                   if(Left1.Type==typeof(byte[])) return e.Expression.MakeBinary(NodeType,Left1,Right1,false,Product.SQLServer.Reflection.Internal.Add);
                   return e.Expression.MakeBinary(NodeType,Left1,Right1);
               }
            );
        }
        private e.Expression IdentityFunctionCall(IdentityFunctionCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UnaryExpression(UnaryExpression x){
            var Expression=this.ScalarExpression(x.Expression);
            return x.UnaryExpressionType switch{
                UnaryExpressionType.BitwiseNot=>e.Expression.Not(Expression),
                UnaryExpressionType.Negative=>e.Expression.Negate(Expression),
                UnaryExpressionType.Positive=>e.Expression.UnaryPlus(Expression),
                _=>throw new NotSupportedException(nameof(UnaryExpressionType)+x.UnaryExpressionType+"はサポートされていない。")
            };
        }
        private e.Expression ScalarExpressionSnippet(ScalarExpressionSnippet x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SourceDeclaration(SourceDeclaration x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression IdentifierOrValueExpression(IdentifierOrValueExpression x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression StatementList(StatementList x)=>x switch{
            StatementListSnippet y=>this.StatementListSnippet(y),
            _=>this.Statements(x.Statements)
        };
        /// <summary>
        /// StatementListSnippet:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression StatementListSnippet(StatementListSnippet x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression TSqlStatement(TSqlStatement x){
            this.RefPeek.Clear();
            return x switch{
                ExecuteStatement                                            y=>this.ExecuteStatement(y),
                ViewStatementBody                                           y=>this.ViewStatementBody(y),
                TriggerStatementBody                                        y=>this.TriggerStatementBody(y),
                ProcedureStatementBodyBase                                  y=>this.ProcedureStatementBodyBase(y),
                DeclareTableVariableStatement                               y=>this.DeclareTableVariableStatement(y),
                StatementWithCtesAndXmlNamespaces                           y=>this.StatementWithCtesAndXmlNamespaces(y),
                BeginEndBlockStatement                                      y=>this.BeginEndBlockStatement(y),
                TransactionStatement                                        y=>this.TransactionStatement(y),
                BreakStatement                                              y=>this.BreakStatement(y),
                ContinueStatement                                           y=>this.ContinueStatement(y),
                CreateDefaultStatement                                      y=>this.CreateDefaultStatement(y),
                CreateRuleStatement                                         y=>this.CreateRuleStatement(y),
                DeclareVariableStatement                                    y=>this.DeclareVariableStatement(y),
                GoToStatement                                               y=>this.GoToStatement(y),
                IfStatement                                                 y=>this.IfStatement(y),
                LabelStatement                                              y=>this.LabelStatement(y),
                WhileStatement                                              y=>this.WhileStatement(y),
                CreateSchemaStatement                                       y=>this.CreateSchemaStatement(y),
                WaitForStatement                                            y=>this.WaitForStatement(y),
                ReadTextStatement                                           y=>this.ReadTextStatement(y),
                TextModificationStatement                                   y=>this.TextModificationStatement(y),
                LineNoStatement                                             y=>this.LineNoStatement(y),
                SecurityStatement                                           y=>this.SecurityStatement(y),
                AlterAuthorizationStatement                                 y=>this.AlterAuthorizationStatement(y),
                SecurityStatementBody80                                     y=>this.SecurityStatementBody80(y),
                PrintStatement                                              y=>this.PrintStatement(y),
                SequenceStatement                                           y=>this.SequenceStatement(y),
                DropObjectsStatement                                        y=>this.DropObjectsStatement(y),
                SecurityPolicyStatement                                     y=>this.SecurityPolicyStatement(y),
                CreateColumnMasterKeyStatement                              y=>this.CreateColumnMasterKeyStatement(y),
                DropUnownedObjectStatement                                  y=>this.DropUnownedObjectStatement(y),
                ColumnEncryptionKeyStatement                                y=>this.ColumnEncryptionKeyStatement(y),
                ExternalTableStatement                                      y=>this.ExternalTableStatement(y),
                ExternalDataSourceStatement                                 y=>this.ExternalDataSourceStatement(y),
                ExternalFileFormatStatement                                 y=>this.ExternalFileFormatStatement(y),
                AssemblyStatement                                           y=>this.AssemblyStatement(y),
                CreateXmlSchemaCollectionStatement                          y=>this.CreateXmlSchemaCollectionStatement(y),
                AlterXmlSchemaCollectionStatement                           y=>this.AlterXmlSchemaCollectionStatement(y),
                DropXmlSchemaCollectionStatement                            y=>this.DropXmlSchemaCollectionStatement(y),
                AlterTableStatement                                         y=>this.AlterTableStatement(y),
                EnableDisableTriggerStatement                               y=>this.EnableDisableTriggerStatement(y),
                TryCatchStatement                                           y=>this.TryCatchStatement(y),
                CreateTypeStatement                                         y=>this.CreateTypeStatement(y),
                CreateSynonymStatement                                      y=>this.CreateSynonymStatement(y),
                RouteStatement                                              y=>this.RouteStatement(y),
                QueueStatement                                              y=>this.QueueStatement(y),
                IndexDefinition                                             y=>this.IndexDefinition(y),
                IndexStatement                                              y=>this.IndexStatement(y),
                CreateFullTextIndexStatement                                y=>this.CreateFullTextIndexStatement(y),
                CreateEventNotificationStatement                            y=>this.CreateEventNotificationStatement(y),
                MasterKeyStatement                                          y=>this.MasterKeyStatement(y),
                ApplicationRoleStatement                                    y=>this.ApplicationRoleStatement(y),
                RoleStatement                                               y=>this.RoleStatement(y),
                UserStatement                                               y=>this.UserStatement(y),
                CreateStatisticsStatement                                   y=>this.CreateStatisticsStatement(y),
                UpdateStatisticsStatement                                   y=>this.UpdateStatisticsStatement(y),
                ReturnStatement                                             y=>this.ReturnStatement(y),
                DeclareCursorStatement                                      y=>this.DeclareCursorStatement(y),
                SetVariableStatement                                        y=>this.SetVariableStatement(y),
                CursorStatement                                             y=>this.CursorStatement(y),
                OpenSymmetricKeyStatement                                   y=>this.OpenSymmetricKeyStatement(y),
                CloseSymmetricKeyStatement                                  y=>this.CloseSymmetricKeyStatement(y),
                OpenMasterKeyStatement                                      y=>this.OpenMasterKeyStatement(y),
                CloseMasterKeyStatement                                     y=>this.CloseMasterKeyStatement(y),
                DropDatabaseStatement                                       y=>this.DropDatabaseStatement(y),
                DropChildObjectsStatement                                   y=>this.DropChildObjectsStatement(y),
                DropIndexStatement                                          y=>this.DropIndexStatement(y),
                DropSchemaStatement                                         y=>this.DropSchemaStatement(y),
                RaiseErrorLegacyStatement                                   y=>this.RaiseErrorLegacyStatement(y),
                RaiseErrorStatement                                         y=>this.RaiseErrorStatement(y),
                ThrowStatement                                              y=>this.ThrowStatement(y),
                UseStatement                                                y=>this.UseStatement(y),
                KillStatement                                               y=>this.KillStatement(y),
                KillQueryNotificationSubscriptionStatement                  y=>this.KillQueryNotificationSubscriptionStatement(y),
                KillStatsJobStatement                                       y=>this.KillStatsJobStatement(y),
                CheckpointStatement                                         y=>this.CheckpointStatement(y),
                ReconfigureStatement                                        y=>this.ReconfigureStatement(y),
                ShutdownStatement                                           y=>this.ShutdownStatement(y),
                SetUserStatement                                            y=>this.SetUserStatement(y),
                TruncateTableStatement                                      y=>this.TruncateTableStatement(y),
                SetOnOffStatement                                           y=>this.SetOnOffStatement(y),
                SetRowCountStatement                                        y=>this.SetRowCountStatement(y),
                SetCommandStatement                                         y=>this.SetCommandStatement(y),
                SetTransactionIsolationLevelStatement                       y=>this.SetTransactionIsolationLevelStatement(y),
                SetTextSizeStatement                                        y=>this.SetTextSizeStatement(y),
                SetErrorLevelStatement                                      y=>this.SetErrorLevelStatement(y),
                CreateDatabaseStatement                                     y=>this.CreateDatabaseStatement(y),
                AlterDatabaseStatement                                      y=>this.AlterDatabaseStatement(y),
                AlterDatabaseScopedConfigurationStatement                   y=>this.AlterDatabaseScopedConfigurationStatement(y),
                CreateTableStatement                                        y=>this.CreateTableStatement(y),
                BackupStatement                                             y=>this.BackupStatement(y),
                RestoreStatement                                            y=>this.RestoreStatement(y),
                BulkInsertBase                                              y=>this.BulkInsertBase(y),
                DbccStatement                                               y=>this.DbccStatement(y),
                CreateAsymmetricKeyStatement                                y=>this.CreateAsymmetricKeyStatement(y),
                CreatePartitionFunctionStatement                            y=>this.CreatePartitionFunctionStatement(y),
                CreatePartitionSchemeStatement                              y=>this.CreatePartitionSchemeStatement(y),
                RemoteServiceBindingStatementBase                           y=>this.RemoteServiceBindingStatementBase(y),
                CertificateStatementBase                                    y=>this.CertificateStatementBase(y),
                CreateContractStatement                                     y=>this.CreateContractStatement(y),
                CredentialStatement                                         y=>this.CredentialStatement(y),
                MessageTypeStatementBase                                    y=>this.MessageTypeStatementBase(y),
                CreateAggregateStatement                                    y=>this.CreateAggregateStatement(y),
                AlterCreateEndpointStatementBase                            y=>this.AlterCreateEndpointStatementBase(y),
                SymmetricKeyStatement                                       y=>this.SymmetricKeyStatement(y),
                FullTextCatalogStatement                                    y=>this.FullTextCatalogStatement(y),
                AlterCreateServiceStatementBase                             y=>this.AlterCreateServiceStatementBase(y),
                DropFullTextIndexStatement                                  y=>this.DropFullTextIndexStatement(y),
                DropTypeStatement                                           y=>this.DropTypeStatement(y),
                DropMasterKeyStatement                                      y=>this.DropMasterKeyStatement(y),
                AlterPartitionFunctionStatement                             y=>this.AlterPartitionFunctionStatement(y),
                AlterPartitionSchemeStatement                               y=>this.AlterPartitionSchemeStatement(y),
                AlterFullTextIndexStatement                                 y=>this.AlterFullTextIndexStatement(y),
                CreateSearchPropertyListStatement                           y=>this.CreateSearchPropertyListStatement(y),
                AlterSearchPropertyListStatement                            y=>this.AlterSearchPropertyListStatement(y),
                CreateLoginStatement                                        y=>this.CreateLoginStatement(y),
                AlterLoginStatement                                         y=>this.AlterLoginStatement(y),
                RevertStatement                                             y=>this.RevertStatement(y),
                DropQueueStatement                                          y=>this.DropQueueStatement(y),
                SignatureStatementBase                                      y=>this.SignatureStatementBase(y),
                DropEventNotificationStatement                              y=>this.DropEventNotificationStatement(y),
                ExecuteAsStatement                                          y=>this.ExecuteAsStatement(y),
                EndConversationStatement                                    y=>this.EndConversationStatement(y),
                MoveConversationStatement                                   y=>this.MoveConversationStatement(y),
                WaitForSupportedStatement                                   y=>this.WaitForSupportedStatement(y),
                SendStatement                                               y=>this.SendStatement(y),
                AlterSchemaStatement                                        y=>this.AlterSchemaStatement(y),
                AlterAsymmetricKeyStatement                                 y=>this.AlterAsymmetricKeyStatement(y),
                AlterServiceMasterKeyStatement                              y=>this.AlterServiceMasterKeyStatement(y),
                BeginConversationTimerStatement                             y=>this.BeginConversationTimerStatement(y),
                BeginDialogStatement                                        y=>this.BeginDialogStatement(y),
                BackupRestoreMasterKeyStatementBase                         y=>this.BackupRestoreMasterKeyStatementBase(y),
                TSqlStatementSnippet                                        y=>this.TSqlStatementSnippet(y),
                AuditSpecificationStatement                                 y=>this.AuditSpecificationStatement(y),
                ServerAuditStatement                                        y=>this.ServerAuditStatement(y),
                DatabaseEncryptionKeyStatement                              y=>this.DatabaseEncryptionKeyStatement(y),
                DropDatabaseEncryptionKeyStatement                          y=>this.DropDatabaseEncryptionKeyStatement(y),
                ResourcePoolStatement                                       y=>this.ResourcePoolStatement(y),
                ExternalResourcePoolStatement                               y=>this.ExternalResourcePoolStatement(y),
                WorkloadGroupStatement                                      y=>this.WorkloadGroupStatement(y),
                BrokerPriorityStatement                                     y=>this.BrokerPriorityStatement(y),
                CreateFullTextStopListStatement                             y=>this.CreateFullTextStopListStatement(y),
                AlterFullTextStopListStatement                              y=>this.AlterFullTextStopListStatement(y),
                CreateCryptographicProviderStatement                        y=>this.CreateCryptographicProviderStatement(y),
                AlterCryptographicProviderStatement                         y=>this.AlterCryptographicProviderStatement(y),
                EventSessionStatement                                       y=>this.EventSessionStatement(y),
                AlterResourceGovernorStatement                              y=>this.AlterResourceGovernorStatement(y),
                CreateSpatialIndexStatement                                 y=>this.CreateSpatialIndexStatement(y),
                AlterServerConfigurationStatement                           y=>this.AlterServerConfigurationStatement(y),
                AlterServerConfigurationSetBufferPoolExtensionStatement     y=>this.AlterServerConfigurationSetBufferPoolExtensionStatement(y),
                AlterServerConfigurationSetDiagnosticsLogStatement          y=>this.AlterServerConfigurationSetDiagnosticsLogStatement(y),
                AlterServerConfigurationSetFailoverClusterPropertyStatement y=>this.AlterServerConfigurationSetFailoverClusterPropertyStatement(y),
                AlterServerConfigurationSetHadrClusterStatement             y=>this.AlterServerConfigurationSetHadrClusterStatement(y),
                AlterServerConfigurationSetSoftNumaStatement                y=>this.AlterServerConfigurationSetSoftNumaStatement(y),
                AvailabilityGroupStatement                                  y=>this.AvailabilityGroupStatement(y),
                CreateFederationStatement                                   y=>this.CreateFederationStatement(y),
                AlterFederationStatement                                    y=>this.AlterFederationStatement(y),
                UseFederationStatement                                      y=>this.UseFederationStatement(y),
                DiskStatement                                               y=>this.DiskStatement(y),
                CreateColumnStoreIndexStatement                             y=>this.CreateColumnStoreIndexStatement(y),
                _=>throw this.単純NotSupportedException(x)
            };
        }
        private e.Expression ExecuteStatement(ExecuteStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ViewStatementBody(ViewStatementBody x)=>x switch{
            AlterViewStatement y=>this.AlterViewStatement(y),
            CreateViewStatement y=> this.CreateViewStatement(y),
            CreateOrAlterViewStatement y=>this.CreateOrAlterViewStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterViewStatement(AlterViewStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateViewStatement(CreateViewStatement x){
            this.Statementの共通初期化();
            var ContainerType=this.ContainerType;
            //var x_Name=x.Name;
            var x_SchemaObjectName=x.SchemaObjectName;
            var Schema=x_SchemaObjectName.SchemaIdentifier is null ? "dbo":x_SchemaObjectName.SchemaIdentifier.Value;
            //var Schema=x_Name.SchemaIdentifier is null ? "dbo" orx_Name.SchemaIdentifier.Value;
            var Schema_FulllName=this.ContainerType.Namespace+".Schemas."+Schema;
            var Schema_Type=ContainerType.Assembly.GetType(Schema_FulllName.Replace("*",@"\*"),true,true);
            Debug.Assert(Schema_Type!=null);
            var x_SchemaObjectName_BaseIdentifier_Value=x_SchemaObjectName.BaseIdentifier.Value;
            //var View = Schema_Type.GetProperty(x_SchemaObjectName_BaseIdentifier_Value,BindingFlags.Public|BindingFlags.Instance);
            var View=Schema_Type.GetProperties(BindingFlags.Public|BindingFlags.Instance).Where(p=>string.Equals(p.Name,x_SchemaObjectName_BaseIdentifier_Value,StringComparison.OrdinalIgnoreCase)).Single();
            //Schema_PropertyInfo=ContainerType.GetProperties(BindingFlags).Where(p => string.Equals(p.Name,Schema,StringComparison.OrdinalIgnoreCase)).Single();

            var SelectStatement =this.SelectStatement(x.SelectStatement);
            //このブロックはSelectによりValueTupleを専用型にラップする
            {

                var 内ElementType=SelectStatement.Type.GetGenericArguments()[0];
                var TypeName=$"{this.ContainerType.Namespace}.Views.{Schema}.{x_SchemaObjectName.BaseIdentifier.Value}";
                var 外ElementType=ContainerType.Assembly.GetType(TypeName.Replace("*",@"\*"),true,true);
                Debug.Assert(外ElementType!=null);
                var p=e.Expression.Parameter(内ElementType,"p");
                var Constructor=外ElementType.GetConstructors()[0];
                var Parameters=Constructor.GetParameters();
                var Parameters_Length=Constructor.GetParameters().Length;
                var NewArguments=new e.Expression[Parameters_Length];
                var 作業配列=this.作業配列;
                e.Expression ValueTuple=p;
                var Item番号=1;
                for(var a=0;a<Parameters_Length;a++){
                    var Item=ValueTuple_Item(ref ValueTuple,ref Item番号);
                    NewArguments[a]=this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[a].ParameterType);
                }
                var Call=e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,内ElementType,外ElementType),
                    SelectStatement,
                    e.Expression.Lambda(
                        e.Expression.New(Constructor,NewArguments),
                        作業配列.Parameters設定(p)
                    )
                );
                var Block=e.Expression.Block(this.List_ScalarVariable,Call);
                return e.Expression.Lambda(
                    作業配列.MakeGenericType(typeof(Func<>),View.PropertyType),
                    Block,
                    x_SchemaObjectName_BaseIdentifier_Value,
                    this.List_Parameter
                );
            }
        }
        private e.Expression CreateOrAlterViewStatement(CreateOrAlterViewStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TriggerStatementBody(TriggerStatementBody x)=>x switch{
            AlterTriggerStatement y=>this.AlterTriggerStatement(y),
            CreateTriggerStatement y=>this.CreateTriggerStatement(y),
            CreateOrAlterTriggerStatement y=>this.CreateOrAlterTriggerStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterTriggerStatement(AlterTriggerStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateTriggerStatement(CreateTriggerStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateOrAlterTriggerStatement(CreateOrAlterTriggerStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ProcedureStatementBodyBase(ProcedureStatementBodyBase x)=>x switch{
            ProcedureStatementBody y=>this.ProcedureStatementBody(y),
            FunctionStatementBody y=>this.FunctionStatementBody(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ProcedureStatementBody(ProcedureStatementBody x)=>x switch{
            AlterProcedureStatement y=>this.AlterProcedureStatement(y),
            CreateProcedureStatement y=>this.CreateProcedureStatement(y),
            CreateOrAlterProcedureStatement y=>this.CreateOrAlterProcedureStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterProcedureStatement(AlterProcedureStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.LabelTarget? ReturnLabel;
        private e.Expression CreateProcedureStatement(CreateProcedureStatement x){
            var Name=x.ProcedureReference.Name.BaseIdentifier.Value;
            foreach(var Parameter in x.Parameters)this.ProcedureParameter(Parameter);
            var ReturnLabel=this.ReturnLabel=e.Expression.Label("return procedure");
            var Result=e.Expression.Lambda(
                e.Expression.Block(
                    this.StatementList(x.StatementList),
                    e.Expression.Label(ReturnLabel)
                ),
                Name,
                Array.Empty<e.ParameterExpression>()
            );
            return Result;
        }
        private e.Expression CreateOrAlterProcedureStatement(CreateOrAlterProcedureStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FunctionStatementBody(FunctionStatementBody x)=>x switch{
            AlterFunctionStatement y=>this.AlterFunctionStatement(y),
            CreateFunctionStatement y=>this.CreateFunctionStatement(y),
            CreateOrAlterFunctionStatement y=>this.CreateOrAlterFunctionStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterFunctionStatement(AlterFunctionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private void Statementの共通初期化() {
            this._StackSubquery単位の情報.Clear();
            this.Dictionary_With名_Set_ColumnAliases.Clear();
            this.List_ScalarVariable.Clear();
            this.List_匿名型TableVariable.Clear();
            this.List_定義型TableVariable.Clear();
            this.List_Parameter.Clear();
        }
        private struct 情報CreateFunctionStatement {
            public string? 変数名;
            public e.ParameterExpression? ReturnVariable;
            //public e.LabelTarget? ReturnLabel;
        }
        private 情報CreateFunctionStatement 変数CreateFunctionStatement;
        private e.Expression CreateFunctionStatement(CreateFunctionStatement x){
            this.Statementの共通初期化();
            var ContainerType=this.ContainerType;
            var Name=x.Name;
            var Schema=Name.SchemaIdentifier is null ? "dbo":Name.SchemaIdentifier.Value;
            var Schema_FulllName=this.ContainerType.Namespace+".Schemas."+Schema;
            var Schema_Type=ContainerType.Assembly.GetType(Schema_FulllName.Replace("*",@"\*"),true,true);
            var Name_BaseIdentifier_Value=Name.BaseIdentifier.Value;

            var Method = FindFunction(Schema_Type,Name_BaseIdentifier_Value)!;
            //var Method =Schema_Type.GetMethod(Name_BaseIdentifier_Value,BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
            var ReturnType=Method.ReturnType;
            var x_Parameters=x.Parameters;
            var x_Parameters_Count=x_Parameters.Count;
            var Types=new Type[x_Parameters_Count+1];
            for(var a = 0;a<x_Parameters_Count;a++) {
                //IsVarying 条件構文。不要かも
                var x_Parameter = x_Parameters[a];
                var Type = this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x_Parameter.DataType));
                Types[a]=Type;
                this.List_Parameter.Add(e.Expression.Parameter(Type,x_Parameter.VariableName.Value));
            }
            Types[x_Parameters_Count]=ReturnType;
            e.Expression Body;
            if(x.StatementList is not null) {
                //create function dbo.ufnGetContactInformation
                //(@ContactID int)
                //returns 
                //    @retContactInformation table (
                //        ContactID   int           primary key not null,
                //        FirstName   nvarchar (50) null,
                //        LastName    nvarchar (50) null,
                //        JobTitle    nvarchar (50) null,
                //        ContactType nvarchar (50) null)
                //as
                //begin
                //    return;
                //end
                if(x.ReturnType is TableValuedFunctionReturnType y){
                    var Variable_Name=this.Identifier(y.DeclareTableVariableBody.VariableName);
                    //                        ScalarFunctionReturnType:
                    //                      (TableValuedFunctionReturnType)x.ReturnType).Name;
                    //.Name:this.TableValuedFunctionReturnType((TableValuedFunctionReturnType)x.ReturnType);
                    //var 変数CreateFunctionStatement = this.変数CreateFunctionStatement;
                    var 作業配列 = this.作業配列;
                    var Variable_Type =作業配列.MakeGenericType(typeof(Set<>),ReturnType.GetGenericArguments()[0]);
                    var Variable=e.Expression.Parameter(Variable_Type,Variable_Name);
                    var ReturnLabel = this.ReturnLabel=e.Expression.Label("return function");
                    if(x.MethodSpecifier is not null) this.MethodSpecifier(x.MethodSpecifier);
                    //var Types = new Type[x_Parameters_Count+1];
                    this.AddTableVariable(Variable);//todo 名前がないからこれでいいのか？
                    Body= this.StatementList(x.StatementList);
                    Body=e.Expression.Block(
                        this.Variables,
                        作業配列.Expressions設定(
                            e.Expression.Assign(
                                Variable,
                                e.Expression.New(Variable_Type.GetConstructor(Type.EmptyTypes))
                            ),
                            Body,
                            e.Expression.Label(ReturnLabel),Variable
                        )
                    );
                    //if(ReturnType.IsGenericType&&ReturnType.GetGenericTypeDefinition()==typeof(Set<>)) { 
                    //    Body=e.Expression.Block(
                    //        this.Variables,
                    //        作業配列.Expressions設定(
                    //            e.Expression.Assign(
                    //                Variable,
                    //                e.Expression.New(Variable_Type.GetConstructor(Type.EmptyTypes))
                    //            ),
                    //            Body,
                    //            e.Expression.Label(ReturnLabel),Variable
                    //        )
                    //    );
                    //} else {
                    //    Body=e.Expression.Block(
                    //        this.Variables,
                    //        作業配列.Expressions設定(Body,e.Expression.Label(ReturnLabel),Variable)
                    //    );
                    //}
                    //return e.Expression.Lambda(Reflection.Func.Get(x_Parameters_Count).MakeGenericType(Types),StatementList,Name_BaseIdentifier_Value,List_Parameter);
                } else{
                    Debug.Assert(
                        ReturnType.IsValueType&&ReturnType.GetGenericArguments()[0]==this.DataTypeReference(((ScalarFunctionReturnType)x.ReturnType).DataType)||
                        ReturnType==this.DataTypeReference(((ScalarFunctionReturnType)x.ReturnType).DataType)
                    );
                    this.ReturnLabel=null;
                    if(x.MethodSpecifier is not null) this.MethodSpecifier(x.MethodSpecifier);
                    Body=this.Convertデータ型を合わせるNullableは想定する(this.StatementList(x.StatementList),ReturnType);
                    if(this.Variables.Any()) Body=e.Expression.Block(this.Variables,Body);
                    //return e.Expression.Lambda(Reflection.Func.Get(x_Parameters_Count).MakeGenericType(Types),StatementList,Name_BaseIdentifier_Value,List_Parameter);
                }
            } else {
                //create function Application.DetermineCustomerAccess
                //(@CityID int)
                //returns table 
                //with schemabinding
                //as
                //return 
                //    (select 1 as AccessResult
                //     where  IS_ROLEMEMBER(N'db_owner') <> 0
                //            or IS_ROLEMEMBER((select sp.SalesTerritory
                //                              from   Application.Cities as c
                //                                     inner join
                //                                     Application.StateProvinces as sp
                //                                     on c.StateProvinceID = sp.StateProvinceID
                //                              where  c.CityID = @CityID) + N' Sales') <> 0
                //            or (ORIGINAL_LOGIN() = N'Website'
                //                and exists (select 1
                //                            from   Application.Cities as c
                //                                   inner join
                //                                   Application.StateProvinces as sp
                //                                   on c.StateProvinceID = sp.StateProvinceID
                //                            where  c.CityID = @CityID
                //                                   and sp.SalesTerritory = SESSION_CONTEXT(N'SalesTerritory'))))
                Body=this.SelectFunctionReturnType((SelectFunctionReturnType)x.ReturnType);
                Body=this.ConvertNullable(Body);
                if(this.Variables.Any())Body=e.Expression.Block(this.Variables,Body);
                var ValueTuple_Type=IEnumerable1のT(Body.Type);
                var ValueTuple_p=e.Expression.Parameter(ValueTuple_Type,"ValueTuple_p");
                var Element_Type=IEnumerable1のT(ReturnType);
                var Constructor=Element_Type.GetConstructors()[0];
                var Parameters=Constructor.GetParameters();
                var Parameters_Length=Constructor.GetParameters().Length;
                var NewArguments=new e.Expression[Parameters_Length];
                var 作業配列=this.作業配列;
                e.Expression ValueTuple=ValueTuple_p;
                var Item番号=1;
                for(var a=0;a<Parameters_Length;a++)
                    NewArguments[a]=this.Convertデータ型を合わせるNullableは想定する(ValueTuple_Item(ref ValueTuple,ref Item番号),Parameters[a].ParameterType);
                Body=e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,ValueTuple_Type,Element_Type),
                    Body,
                    e.Expression.Lambda(e.Expression.New(Constructor,NewArguments),作業配列.Parameters設定(ValueTuple_p))
                );
            }
            return e.Expression.Lambda(Reflection.Func.Get(x_Parameters_Count).MakeGenericType(Types),Body,Name_BaseIdentifier_Value,this.List_Parameter);
        }
        private e.Expression CreateOrAlterFunctionStatement(CreateOrAlterFunctionStatement x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// テーブル変数宣言
        /// declare @ids table(maj int primary key,nam sysname)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression DeclareTableVariableStatement(DeclareTableVariableStatement x){
            return this.DeclareTableVariableBody(x.Body);
        }
        private e.Expression StatementWithCtesAndXmlNamespaces(StatementWithCtesAndXmlNamespaces x)=>x switch{
            SelectStatement y=>this.SelectStatement(y),
            DataModificationStatement y=>this.DataModificationStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression SelectStatement(SelectStatement x){
            switch(x){
                case SelectStatementSnippet y:return this.SelectStatementSnippet(y);
                default:{
                    //this.Statementの共通初期化();
                    var List_Expression=new List<e.Expression>();
                    if(x.WithCtesAndXmlNamespaces is not null) { 
                        var WithCtesAndXmlNamespaces=this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
                        List_Expression.Add(WithCtesAndXmlNamespaces);
                    }
                    var Variables=this.Variables.ToArray();//サブクエリで書き換えられる可能性
                    //compute by句
                    foreach(var ComputeClause in x.ComputeClauses){
                        this.ComputeClause(ComputeClause);
                    }
                    if(x.Into is not null)this.SchemaObjectName(x.Into);
                    if(x.On is not null)this.Identifier(x.On);
                    foreach(var OptimizerHint in x.OptimizerHints){
                        this.OptimizerHint(OptimizerHint);
                    }
                    var QueryExpression=this.QueryExpression(x.QueryExpression);
                    if(List_Expression.Count==0)return QueryExpression;
                    List_Expression.Add(QueryExpression);
                    return e.Expression.Block(Variables,List_Expression);
                }
            }
        }
        private e.Expression SelectStatementSnippet(SelectStatementSnippet x){
            //x.Script
            return this.QueryExpression(x.QueryExpression);
        }
        private e.Expression DataModificationStatement(DataModificationStatement x)=>x switch{
            DeleteStatement y=>this.DeleteStatement(y),
            InsertStatement y=>this.InsertStatement(y),
            UpdateStatement y=>this.UpdateStatement(y),
            MergeStatement y=>this.MergeStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression DeleteStatement(DeleteStatement x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// InsertStatement:DataModificationStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// insert into @Student select @gtinyint + 4200000000 as tinyint
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression InsertStatement(InsertStatement x){
            var InsertSpecification=this.InsertSpecification(x.InsertSpecification);
            if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
            return InsertSpecification;
        }
        private e.Expression UpdateStatement(UpdateStatement x){
            //s.UpdateWith(p=>p.diagram_id=@DiagId,p=>p.definition=@definition)
            var UpdateSpecification=this.UpdateSpecification(x.UpdateSpecification);
            return UpdateSpecification;
            //return Expressions.Expression.Call(
            //    UpdateSpecification,
            //    作業配列.MakeGenericMethod(
            //        Reflection.ExtendSet.UpdateWith,
            //        UpdateSpecification.Type
            //    )
            //);
        }
        private e.Expression MergeStatement(MergeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BeginEndBlockStatement(BeginEndBlockStatement x)=>x switch{
            BeginEndAtomicBlockStatement y=>this.BeginEndAtomicBlockStatement(y),
            _=>this.StatementList(x.StatementList)
        };
        /// <summary>
        /// begin atmic～end アトミックブロック
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression BeginEndAtomicBlockStatement(BeginEndAtomicBlockStatement x){
            foreach(var Option in x.Options){
                this.AtomicBlockOption(Option);
            }
            return this.StatementList(x.StatementList);
        }
        private e.Expression TransactionStatement(TransactionStatement x)=>x switch{
            BeginTransactionStatement y=>this.BeginTransactionStatement(y),
            CommitTransactionStatement y=>this.CommitTransactionStatement(y),
            RollbackTransactionStatement y=>this.RollbackTransactionStatement(y),
            SaveTransactionStatement y=>this.SaveTransactionStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression BeginTransactionStatement(BeginTransactionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CommitTransactionStatement(CommitTransactionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RollbackTransactionStatement(RollbackTransactionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SaveTransactionStatement(SaveTransactionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BreakStatement(BreakStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ContinueStatement(ContinueStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateDefaultStatement(CreateDefaultStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateRuleStatement(CreateRuleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DeclareVariableStatement(DeclareVariableStatement x){
            var Declarations=x.Declarations;
            var Declarations_Count=Declarations.Count;
            if(Declarations_Count==1){
                var Result=this.DeclareVariableElement(Declarations[0]);
                return Result;
            }else{
                var Block_Expressions=new List<e.Expression>();
                for(var a=0;a<Declarations_Count;a++){
                    var DeclareVariableElement=this.DeclareVariableElement(Declarations[a]);
                    if(DeclareVariableElement!=Default_void){
                        Block_Expressions.Add(this.DeclareVariableElement(Declarations[a]));
                    }
                }
                if(Block_Expressions.Count==0)return Default_void;
                return e.Expression.Block(Block_Expressions);
            }
        }
        private e.Expression GoToStatement(GoToStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IfStatement(IfStatement x)=>e.Expression.IfThenElse(
            this.BooleanExpression(x.Predicate),
            this.TSqlStatement(x.ThenStatement),
            x.ElseStatement is null ? Default_void:this.TSqlStatement(x.ElseStatement)
        );
        private e.Expression LabelStatement(LabelStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WhileStatement(WhileStatement x){
            var Break=e.Expression.Label();
            var Stack_WHILEのBreak先=this.Stack_WHILEのBreak先;
            Stack_WHILEのBreak先.Push(Break);
            var Body=this.TSqlStatement(x.Statement);
            Stack_WHILEのBreak先.Pop();
            return e.Expression.Loop(Body,Break);
        }
        private e.Expression CreateSchemaStatement(CreateSchemaStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WaitForStatement(WaitForStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ReadTextStatement(ReadTextStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TextModificationStatement(TextModificationStatement x)=>x switch{
            UpdateTextStatement y=>this.UpdateTextStatement(y),
            WriteTextStatement y=>this.WriteTextStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression UpdateTextStatement(UpdateTextStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WriteTextStatement(WriteTextStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LineNoStatement(LineNoStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityStatement(SecurityStatement x)=>x switch{
            GrantStatement y=>this.GrantStatement(y),
            DenyStatement y=>this.DenyStatement(y),
            RevokeStatement y=>this.RevokeStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression GrantStatement(GrantStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DenyStatement(DenyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RevokeStatement(RevokeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterAuthorizationStatement(AlterAuthorizationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityStatementBody80(SecurityStatementBody80 x)=>x switch{
            GrantStatement80 y=>this.GrantStatement80(y),
            DenyStatement80 y=>this.DenyStatement80(y),
            RevokeStatement80 y=>this.RevokeStatement80(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression GrantStatement80(GrantStatement80 x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DenyStatement80(DenyStatement80 x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RevokeStatement80(RevokeStatement80 x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PrintStatement(PrintStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SequenceStatement(SequenceStatement x)=>x switch{
            CreateSequenceStatement y=>this.CreateSequenceStatement(y),
            AlterSequenceStatement y=>this.AlterSequenceStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateSequenceStatement(CreateSequenceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterSequenceStatement(AlterSequenceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropObjectsStatement(DropObjectsStatement x)=>x switch{
            DropSequenceStatement y=>this.DropSequenceStatement(y),
            DropSecurityPolicyStatement y=>this.DropSecurityPolicyStatement(y),
            DropExternalTableStatement y=>this.DropExternalTableStatement(y),
            DropTableStatement y=>this.DropTableStatement(y),
            DropProcedureStatement y=>this.DropProcedureStatement(y),
            DropFunctionStatement y=>this.DropFunctionStatement(y),
            DropViewStatement y=>this.DropViewStatement(y),
            DropDefaultStatement y=>this.DropDefaultStatement(y),
            DropRuleStatement y=>this.DropRuleStatement(y),
            DropTriggerStatement y=>this.DropTriggerStatement(y),
            DropSynonymStatement y=>this.DropSynonymStatement(y),
            DropAggregateStatement y=>this.DropAggregateStatement(y),
            DropAssemblyStatement y=>this.DropAssemblyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression DropSequenceStatement(DropSequenceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropSecurityPolicyStatement(DropSecurityPolicyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropExternalTableStatement(DropExternalTableStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropTableStatement(DropTableStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropProcedureStatement(DropProcedureStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropFunctionStatement(DropFunctionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropViewStatement(DropViewStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropDefaultStatement(DropDefaultStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropRuleStatement(DropRuleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropTriggerStatement(DropTriggerStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropSynonymStatement(DropSynonymStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropAggregateStatement(DropAggregateStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropAssemblyStatement(DropAssemblyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityPolicyStatement(SecurityPolicyStatement x)=>x switch{
            CreateSecurityPolicyStatement y=>this.CreateSecurityPolicyStatement(y),
            AlterSecurityPolicyStatement y=>this.AlterSecurityPolicyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateSecurityPolicyStatement(CreateSecurityPolicyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterSecurityPolicyStatement(AlterSecurityPolicyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateColumnMasterKeyStatement(CreateColumnMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropUnownedObjectStatement(DropUnownedObjectStatement x)=>x switch{
            DropColumnMasterKeyStatement y=>this.DropColumnMasterKeyStatement(y),
            DropColumnEncryptionKeyStatement y=>this.DropColumnEncryptionKeyStatement(y),
            DropExternalDataSourceStatement y=>this.DropExternalDataSourceStatement(y),
            DropExternalFileFormatStatement y=>this.DropExternalFileFormatStatement(y),
            DropServerRoleStatement y=>this.DropServerRoleStatement(y),
            DropPartitionFunctionStatement y=>this.DropPartitionFunctionStatement(y),
            DropPartitionSchemeStatement y=>this.DropPartitionSchemeStatement(y),
            DropApplicationRoleStatement y=>this.DropApplicationRoleStatement(y),
            DropFullTextCatalogStatement y=>this.DropFullTextCatalogStatement(y),
            DropLoginStatement y=>this.DropLoginStatement(y),
            DropRoleStatement y=>this.DropRoleStatement(y),
            DropUserStatement y=>this.DropUserStatement(y),
            DropSymmetricKeyStatement y=>this.DropSymmetricKeyStatement(y),
            DropAsymmetricKeyStatement y=>this.DropAsymmetricKeyStatement(y),
            DropCertificateStatement y=>this.DropCertificateStatement(y),
            DropCredentialStatement y=>this.DropCredentialStatement(y),
            DropSearchPropertyListStatement y=>this.DropSearchPropertyListStatement(y),
            DropContractStatement y=>this.DropContractStatement(y),
            DropEndpointStatement y=>this.DropEndpointStatement(y),
            DropMessageTypeStatement y=>this.DropMessageTypeStatement(y),
            DropRemoteServiceBindingStatement y=>this.DropRemoteServiceBindingStatement(y),
            DropRouteStatement y=>this.DropRouteStatement(y),
            DropServiceStatement y=>this.DropServiceStatement(y),
            DropDatabaseAuditSpecificationStatement y=>this.DropDatabaseAuditSpecificationStatement(y),
            DropServerAuditSpecificationStatement y=>this.DropServerAuditSpecificationStatement(y),
            DropServerAuditStatement y=>this.DropServerAuditStatement(y),
            DropResourcePoolStatement y=>this.DropResourcePoolStatement(y),
            DropExternalResourcePoolStatement y=>this.DropExternalResourcePoolStatement(y),
            DropWorkloadGroupStatement y=>this.DropWorkloadGroupStatement(y),
            DropBrokerPriorityStatement y=>this.DropBrokerPriorityStatement(y),
            DropFullTextStopListStatement y=>this.DropFullTextStopListStatement(y),
            DropCryptographicProviderStatement y=>this.DropCryptographicProviderStatement(y),
            DropEventSessionStatement y=>this.DropEventSessionStatement(y),
            DropAvailabilityGroupStatement y=>this.DropAvailabilityGroupStatement(y),
            DropFederationStatement y=>this.DropFederationStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression DropColumnMasterKeyStatement(DropColumnMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropColumnEncryptionKeyStatement(DropColumnEncryptionKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropExternalDataSourceStatement(DropExternalDataSourceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropExternalFileFormatStatement(DropExternalFileFormatStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropServerRoleStatement(DropServerRoleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropPartitionFunctionStatement(DropPartitionFunctionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropPartitionSchemeStatement(DropPartitionSchemeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropApplicationRoleStatement(DropApplicationRoleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropFullTextCatalogStatement(DropFullTextCatalogStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropLoginStatement(DropLoginStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropRoleStatement(DropRoleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropUserStatement(DropUserStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropSymmetricKeyStatement(DropSymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropAsymmetricKeyStatement(DropAsymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropCertificateStatement(DropCertificateStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropCredentialStatement(DropCredentialStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropSearchPropertyListStatement(DropSearchPropertyListStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropContractStatement(DropContractStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropEndpointStatement(DropEndpointStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropMessageTypeStatement(DropMessageTypeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropRemoteServiceBindingStatement(DropRemoteServiceBindingStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropRouteStatement(DropRouteStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropServiceStatement(DropServiceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropDatabaseAuditSpecificationStatement(DropDatabaseAuditSpecificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropServerAuditSpecificationStatement(DropServerAuditSpecificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropServerAuditStatement(DropServerAuditStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropResourcePoolStatement(DropResourcePoolStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropExternalResourcePoolStatement(DropExternalResourcePoolStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropWorkloadGroupStatement(DropWorkloadGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropBrokerPriorityStatement(DropBrokerPriorityStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropFullTextStopListStatement(DropFullTextStopListStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropCryptographicProviderStatement(DropCryptographicProviderStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropEventSessionStatement(DropEventSessionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropAvailabilityGroupStatement(DropAvailabilityGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropFederationStatement(DropFederationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnEncryptionKeyStatement(ColumnEncryptionKeyStatement x)=>x switch{
            CreateColumnEncryptionKeyStatement y=>this.CreateColumnEncryptionKeyStatement(y),
            AlterColumnEncryptionKeyStatement y=>this.AlterColumnEncryptionKeyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateColumnEncryptionKeyStatement(CreateColumnEncryptionKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterColumnEncryptionKeyStatement(AlterColumnEncryptionKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableStatement(ExternalTableStatement x)=>x switch{
            CreateExternalTableStatement y=>this.CreateExternalTableStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateExternalTableStatement(CreateExternalTableStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalDataSourceStatement(ExternalDataSourceStatement x)=>x switch{
            CreateExternalDataSourceStatement y=>this.CreateExternalDataSourceStatement(y),
            AlterExternalDataSourceStatement y=>this.AlterExternalDataSourceStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateExternalDataSourceStatement(CreateExternalDataSourceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterExternalDataSourceStatement(AlterExternalDataSourceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalFileFormatStatement(ExternalFileFormatStatement x)=>x switch{
            CreateExternalFileFormatStatement y=>this.CreateExternalFileFormatStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateExternalFileFormatStatement(CreateExternalFileFormatStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AssemblyStatement(AssemblyStatement x)=>x switch{
            CreateAssemblyStatement y=>this.CreateAssemblyStatement(y),
            AlterAssemblyStatement y=>this.AlterAssemblyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateAssemblyStatement(CreateAssemblyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterAssemblyStatement(AlterAssemblyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateXmlSchemaCollectionStatement(CreateXmlSchemaCollectionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterXmlSchemaCollectionStatement(AlterXmlSchemaCollectionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropXmlSchemaCollectionStatement(DropXmlSchemaCollectionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableStatement(AlterTableStatement x)=>x switch{
            AlterTableRebuildStatement y=>this.AlterTableRebuildStatement(y),
            AlterTableChangeTrackingModificationStatement y=>this.AlterTableChangeTrackingModificationStatement(y),
            AlterTableFileTableNamespaceStatement y=>this.AlterTableFileTableNamespaceStatement(y),
            AlterTableSetStatement y=>this.AlterTableSetStatement(y),
            AlterTableAddTableElementStatement y=>this.AlterTableAddTableElementStatement(y),
            AlterTableConstraintModificationStatement y=>this.AlterTableConstraintModificationStatement(y),
            AlterTableSwitchStatement y=>this.AlterTableSwitchStatement(y),
            AlterTableDropTableElementStatement y=>this.AlterTableDropTableElementStatement(y),
            AlterTableTriggerModificationStatement y=>this.AlterTableTriggerModificationStatement(y),
            AlterTableAlterIndexStatement y=>this.AlterTableAlterIndexStatement(y),
            AlterTableAlterColumnStatement y=>this.AlterTableAlterColumnStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterTableRebuildStatement(AlterTableRebuildStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableChangeTrackingModificationStatement(AlterTableChangeTrackingModificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableFileTableNamespaceStatement(AlterTableFileTableNamespaceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableSetStatement(AlterTableSetStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableAddTableElementStatement(AlterTableAddTableElementStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableConstraintModificationStatement(AlterTableConstraintModificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableSwitchStatement(AlterTableSwitchStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableDropTableElementStatement(AlterTableDropTableElementStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableTriggerModificationStatement(AlterTableTriggerModificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableAlterIndexStatement(AlterTableAlterIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableAlterColumnStatement(AlterTableAlterColumnStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EnableDisableTriggerStatement(EnableDisableTriggerStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TryCatchStatement(TryCatchStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateTypeStatement(CreateTypeStatement x)=>x switch{
            CreateTypeUdtStatement y=>this.CreateTypeUdtStatement(y),
            CreateTypeUddtStatement y=>this.CreateTypeUddtStatement(y),
            CreateTypeTableStatement y=>this.CreateTypeTableStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateTypeUdtStatement(CreateTypeUdtStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateTypeUddtStatement(CreateTypeUddtStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateTypeTableStatement(CreateTypeTableStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateSynonymStatement(CreateSynonymStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RouteStatement(RouteStatement x)=>x switch{
            CreateRouteStatement y=>this.CreateRouteStatement(y),
            AlterRouteStatement y=>this.AlterRouteStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateRouteStatement(CreateRouteStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterRouteStatement(AlterRouteStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueueStatement(QueueStatement x)=>x switch{
            CreateQueueStatement y=>this.CreateQueueStatement(y),
            AlterQueueStatement y=>this.AlterQueueStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateQueueStatement(CreateQueueStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterQueueStatement(AlterQueueStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IndexDefinition(IndexDefinition x){
            foreach(var Column in x.Columns)
                this.ColumnWithSortOrder(Column);
            foreach(var IncludeColumn in x.IncludeColumns)
                this.ColumnReferenceExpression(IncludeColumn);
            this.IdentifierOrValueExpression(x.FileStreamOn);
            this.BooleanExpression(x.FilterPredicate);
            foreach(var IndexOption in x.IndexOptions)
                this.IndexOption(IndexOption);
            this.IndexType(x.IndexType);
            this.Identifier(x.Name);
            this.FileGroupOrPartitionScheme(x.OnFileGroupOrPartitionScheme);
            var Unique=x.Unique;
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IndexStatement(IndexStatement x)=>x switch{
            AlterIndexStatement y=>this.AlterIndexStatement(y),
            CreateXmlIndexStatement y=>this.CreateXmlIndexStatement(y),
            CreateSelectiveXmlIndexStatement y=>this.CreateSelectiveXmlIndexStatement(y),
            CreateIndexStatement y=>this.CreateIndexStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterIndexStatement(AlterIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateXmlIndexStatement(CreateXmlIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateSelectiveXmlIndexStatement(CreateSelectiveXmlIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateIndexStatement(CreateIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateFullTextIndexStatement(CreateFullTextIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateEventNotificationStatement(CreateEventNotificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MasterKeyStatement(MasterKeyStatement x)=>x switch{
            CreateMasterKeyStatement y=>this.CreateMasterKeyStatement(y),
            AlterMasterKeyStatement y=>this.AlterMasterKeyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateMasterKeyStatement(CreateMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterMasterKeyStatement(AlterMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ApplicationRoleStatement(ApplicationRoleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RoleStatement(RoleStatement x)=>x switch{
            CreateRoleStatement y=>this.CreateRoleStatement(y),
            AlterRoleStatement y=>this.AlterRoleStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateRoleStatement(CreateRoleStatement x)=>x switch{
            CreateServerRoleStatement y=>this.CreateServerRoleStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateServerRoleStatement(CreateServerRoleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterRoleStatement(AlterRoleStatement x)=>x switch{
            AlterServerRoleStatement y=>this.AlterServerRoleStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterServerRoleStatement(AlterServerRoleStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UserStatement(UserStatement x)=>x switch{
            CreateUserStatement y=>this.CreateUserStatement(y),
            AlterUserStatement y=>this.AlterUserStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateUserStatement(CreateUserStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterUserStatement(AlterUserStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateStatisticsStatement(CreateStatisticsStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UpdateStatisticsStatement(UpdateStatisticsStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ReturnStatement(ReturnStatement x){
            if(this.ReturnLabel is null)return this.ScalarExpression(x.Expression);
            var Goto=e.Expression.Goto(this.ReturnLabel);
            if(x.Expression is null)return Goto;
            var ScalarExpression=this.ScalarExpression(x.Expression);
            ScalarExpression=e.Expression.Assign(this.変数CreateFunctionStatement.ReturnVariable,ScalarExpression);
            if(this.変数CreateFunctionStatement.ReturnVariable is not null)
                ScalarExpression=e.Expression.Assign(this.変数CreateFunctionStatement.ReturnVariable,ScalarExpression);
            return e.Expression.Block(ScalarExpression,Goto);
        }
        private e.Expression DeclareCursorStatement(DeclareCursorStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetVariableStatement(SetVariableStatement x){
            Debug.Assert(!x.FunctionCallExists);
            Debug.Assert(x.CursorDefinition is null);
            Debug.Assert(x.Identifier is null);
            var NodeType=x.AssignmentKind switch{
                AssignmentKind.AddEquals=>e.ExpressionType.AddAssign,
                AssignmentKind.BitwiseAndEquals=>e.ExpressionType.AndAssign,
                AssignmentKind.BitwiseOrEquals=>e.ExpressionType.OrAssign,
                AssignmentKind.BitwiseXorEquals=>e.ExpressionType.ExclusiveOrAssign,
                AssignmentKind.DivideEquals=>e.ExpressionType.DivideAssign,
                AssignmentKind.Equals=>e.ExpressionType.Assign,
                AssignmentKind.ModEquals=>e.ExpressionType.ModuloAssign,
                AssignmentKind.MultiplyEquals=>e.ExpressionType.MultiplyAssign,
                AssignmentKind.SubtractEquals=>e.ExpressionType.SubtractAssign,
                _=>throw new NotSupportedException(x.AssignmentKind.ToString())
            };
            var Left=this.VariableReference(x.Variable);
            var Right=this.ScalarExpression(x.Expression);
            if(Right.Type.IsArray) {
            }else if(Right.Type==typeof(string)) {
            } else { 
                var IEnumerable1 = Right.Type.GetInterface(CommonLibrary.IEnumerable1_FullName);
                if(IEnumerable1 is not null) {
                    //var SingleOrDefault=e.Expression.Call(Reflection.ExtensionSet.SingleOrDefault.MakeGenericMethod(IEnumerable1.GetGenericArguments()),Right);
                    //return e.Expression.Call(作業配列.MakeGenericMethod(Reflection.ExtensionSet.SingleOrDefault,Lambda_Body_Type),MethodCall);

                    //Debug.Assert(typeof(ITuple).IsAssignableFrom(SingleOrDefault.Type));
                    //Right=e.Expression.Field(SingleOrDefault,"Item1");
                    Right=e.Expression.Call(Reflection.ExtensionSet.SingleOrDefault.MakeGenericMethod(IEnumerable1.GetGenericArguments()),Right);
                }
            }
            Right=this.Convertデータ型を合わせるNullableは想定する(Right,Left.Type);
            return e.Expression.MakeBinary(NodeType,Left,Right);
        }
        private e.Expression CursorStatement(CursorStatement x)=>x switch{
            OpenCursorStatement y=>this.OpenCursorStatement(y),
            CloseCursorStatement y=>this.CloseCursorStatement(y),
            DeallocateCursorStatement y=>this.DeallocateCursorStatement(y),
            FetchCursorStatement y=>this.FetchCursorStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OpenCursorStatement(OpenCursorStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CloseCursorStatement(CloseCursorStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DeallocateCursorStatement(DeallocateCursorStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FetchCursorStatement(FetchCursorStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OpenSymmetricKeyStatement(OpenSymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CloseSymmetricKeyStatement(CloseSymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OpenMasterKeyStatement(OpenMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CloseMasterKeyStatement(CloseMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropDatabaseStatement(DropDatabaseStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropChildObjectsStatement(DropChildObjectsStatement x)=>x switch{
            DropStatisticsStatement y=>this.DropStatisticsStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression DropStatisticsStatement(DropStatisticsStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropIndexStatement(DropIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropSchemaStatement(DropSchemaStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RaiseErrorLegacyStatement(RaiseErrorLegacyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RaiseErrorStatement(RaiseErrorStatement x){
            var x_OptionalParameters=x.OptionalParameters;
            var x_OptionalParameters_Count=x_OptionalParameters.Count;
            var Arguments=new e.Expression[x_OptionalParameters_Count];
            for(var a=0;a<x_OptionalParameters_Count;a++){
                Arguments[a]=this.ScalarExpression(x_OptionalParameters[a]);
            }
            this.ScalarExpression(x.FirstParameter);
            this.ScalarExpression(x.SecondParameter);
            this.ScalarExpression(x.ThirdParameter);
            return e.Expression.Throw(
                e.Expression.New(
                    Reflection.Exception.RaiseErrorException_ctor,
                    this.ScalarExpression(x.FirstParameter),
                    this.ScalarExpression(x.SecondParameter),
                    this.ScalarExpression(x.ThirdParameter),
                    e.Expression.NewArrayInit(
                        typeof(object),
                        Arguments
                    )
                )
            );
        }
        private e.Expression ThrowStatement(ThrowStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UseStatement(UseStatement x){
            return Default_void;
            //throw this.CreateNotSupportedException(x);
        }
        private e.Expression KillStatement(KillStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression KillQueryNotificationSubscriptionStatement(KillQueryNotificationSubscriptionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression KillStatsJobStatement(KillStatsJobStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CheckpointStatement(CheckpointStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ReconfigureStatement(ReconfigureStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ShutdownStatement(ShutdownStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetUserStatement(SetUserStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TruncateTableStatement(TruncateTableStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetOnOffStatement(SetOnOffStatement x)=>x switch{
            PredicateSetStatement y=>this.PredicateSetStatement(y),
            SetStatisticsStatement y=>this.SetStatisticsStatement(y),
            SetOffsetsStatement y=>this.SetOffsetsStatement(y),
            SetIdentityInsertStatement y=>this.SetIdentityInsertStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// SET NOCOUNTなどのオプション設定
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression PredicateSetStatement(PredicateSetStatement x)=>Default_void;
        private e.Expression SetStatisticsStatement(SetStatisticsStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetOffsetsStatement(SetOffsetsStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetIdentityInsertStatement(SetIdentityInsertStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetRowCountStatement(SetRowCountStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetCommandStatement(SetCommandStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetTransactionIsolationLevelStatement(SetTransactionIsolationLevelStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetTextSizeStatement(SetTextSizeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetErrorLevelStatement(SetErrorLevelStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateDatabaseStatement(CreateDatabaseStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseStatement(AlterDatabaseStatement x)=>x switch{
            AlterDatabaseCollateStatement y=>this.AlterDatabaseCollateStatement(y),
            AlterDatabaseRebuildLogStatement y=>this.AlterDatabaseRebuildLogStatement(y),
            AlterDatabaseAddFileStatement y=>this.AlterDatabaseAddFileStatement(y),
            AlterDatabaseAddFileGroupStatement y=>this.AlterDatabaseAddFileGroupStatement(y),
            AlterDatabaseRemoveFileGroupStatement y=>this.AlterDatabaseRemoveFileGroupStatement(y),
            AlterDatabaseRemoveFileStatement y=>this.AlterDatabaseRemoveFileStatement(y),
            AlterDatabaseModifyNameStatement y=>this.AlterDatabaseModifyNameStatement(y),
            AlterDatabaseModifyFileStatement y=>this.AlterDatabaseModifyFileStatement(y),
            AlterDatabaseModifyFileGroupStatement y=>this.AlterDatabaseModifyFileGroupStatement(y),
            AlterDatabaseSetStatement y=>this.AlterDatabaseSetStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterDatabaseCollateStatement(AlterDatabaseCollateStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseRebuildLogStatement(AlterDatabaseRebuildLogStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseAddFileStatement(AlterDatabaseAddFileStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseAddFileGroupStatement(AlterDatabaseAddFileGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseRemoveFileGroupStatement(AlterDatabaseRemoveFileGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseRemoveFileStatement(AlterDatabaseRemoveFileStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseModifyNameStatement(AlterDatabaseModifyNameStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseModifyFileStatement(AlterDatabaseModifyFileStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseModifyFileGroupStatement(AlterDatabaseModifyFileGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseSetStatement(AlterDatabaseSetStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseScopedConfigurationStatement(AlterDatabaseScopedConfigurationStatement x)=>x switch{
            AlterDatabaseScopedConfigurationSetStatement y=>this.AlterDatabaseScopedConfigurationSetStatement(y),
            AlterDatabaseScopedConfigurationClearStatement y=>this.AlterDatabaseScopedConfigurationClearStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterDatabaseScopedConfigurationSetStatement(AlterDatabaseScopedConfigurationSetStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseScopedConfigurationClearStatement(AlterDatabaseScopedConfigurationClearStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateTableStatement(CreateTableStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BackupStatement(BackupStatement x)=>x switch{
            BackupDatabaseStatement y=>this.BackupDatabaseStatement(y),
            BackupTransactionLogStatement y=>this.BackupTransactionLogStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression BackupDatabaseStatement(BackupDatabaseStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BackupTransactionLogStatement(BackupTransactionLogStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RestoreStatement(RestoreStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BulkInsertBase(BulkInsertBase x)=>x switch{
            BulkInsertStatement y=>this.BulkInsertStatement(y),
            InsertBulkStatement y=>this.InsertBulkStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression BulkInsertStatement(BulkInsertStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression InsertBulkStatement(InsertBulkStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DbccStatement(DbccStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateAsymmetricKeyStatement(CreateAsymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreatePartitionFunctionStatement(CreatePartitionFunctionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreatePartitionSchemeStatement(CreatePartitionSchemeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RemoteServiceBindingStatementBase(RemoteServiceBindingStatementBase x)=>x switch{
            CreateRemoteServiceBindingStatement y=>this.CreateRemoteServiceBindingStatement(y),
            AlterRemoteServiceBindingStatement y=>this.AlterRemoteServiceBindingStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateRemoteServiceBindingStatement(CreateRemoteServiceBindingStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterRemoteServiceBindingStatement(AlterRemoteServiceBindingStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CertificateStatementBase(CertificateStatementBase x)=>x switch{
            CreateCertificateStatement y=>this.CreateCertificateStatement(y),
            AlterCertificateStatement y=>this.AlterCertificateStatement(y),
            BackupCertificateStatement y=>this.BackupCertificateStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateCertificateStatement(CreateCertificateStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterCertificateStatement(AlterCertificateStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BackupCertificateStatement(BackupCertificateStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateContractStatement(CreateContractStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CredentialStatement(CredentialStatement x)=>x switch{
            CreateCredentialStatement y=>this.CreateCredentialStatement(y),
            AlterCredentialStatement y=>this.AlterCredentialStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateCredentialStatement(CreateCredentialStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterCredentialStatement(AlterCredentialStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MessageTypeStatementBase(MessageTypeStatementBase x)=>x switch{
            CreateMessageTypeStatement y=>this.CreateMessageTypeStatement(y),
            AlterMessageTypeStatement y=>this.AlterMessageTypeStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateMessageTypeStatement(CreateMessageTypeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterMessageTypeStatement(AlterMessageTypeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateAggregateStatement(CreateAggregateStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterCreateEndpointStatementBase(AlterCreateEndpointStatementBase x)=>x switch{
            CreateEndpointStatement y=>this.CreateEndpointStatement(y),
            AlterEndpointStatement y=>this.AlterEndpointStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateEndpointStatement(CreateEndpointStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterEndpointStatement(AlterEndpointStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SymmetricKeyStatement(SymmetricKeyStatement x)=>x switch{
            CreateSymmetricKeyStatement y=>this.CreateSymmetricKeyStatement(y),
            AlterSymmetricKeyStatement y=>this.AlterSymmetricKeyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateSymmetricKeyStatement(CreateSymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterSymmetricKeyStatement(AlterSymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FullTextCatalogStatement(FullTextCatalogStatement x)=>x switch{
            CreateFullTextCatalogStatement y=>this.CreateFullTextCatalogStatement(y),
            AlterFullTextCatalogStatement y=>this.AlterFullTextCatalogStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateFullTextCatalogStatement(CreateFullTextCatalogStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterFullTextCatalogStatement(AlterFullTextCatalogStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterCreateServiceStatementBase(AlterCreateServiceStatementBase x)=>x switch{
            CreateServiceStatement y=>this.CreateServiceStatement(y),
            AlterServiceStatement y=>this.AlterServiceStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateServiceStatement(CreateServiceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServiceStatement(AlterServiceStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropFullTextIndexStatement(DropFullTextIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropTypeStatement(DropTypeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropMasterKeyStatement(DropMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterPartitionFunctionStatement(AlterPartitionFunctionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterPartitionSchemeStatement(AlterPartitionSchemeStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterFullTextIndexStatement(AlterFullTextIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateSearchPropertyListStatement(CreateSearchPropertyListStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterSearchPropertyListStatement(AlterSearchPropertyListStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateLoginStatement(CreateLoginStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterLoginStatement(AlterLoginStatement x)=>x switch{
            AlterLoginOptionsStatement y=>this.AlterLoginOptionsStatement(y),
            AlterLoginEnableDisableStatement y=>this.AlterLoginEnableDisableStatement(y),
            AlterLoginAddDropCredentialStatement y=>this.AlterLoginAddDropCredentialStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AlterLoginOptionsStatement(AlterLoginOptionsStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterLoginEnableDisableStatement(AlterLoginEnableDisableStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterLoginAddDropCredentialStatement(AlterLoginAddDropCredentialStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RevertStatement(RevertStatement x){
            //execute as callerの権限を戻す
            return Default_void;
        }
        private e.Expression DropQueueStatement(DropQueueStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SignatureStatementBase(SignatureStatementBase x)=>x switch{
            AddSignatureStatement y=>this.AddSignatureStatement(y),
            DropSignatureStatement y=>this.DropSignatureStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AddSignatureStatement(AddSignatureStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropSignatureStatement(DropSignatureStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropEventNotificationStatement(DropEventNotificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecuteAsStatement(ExecuteAsStatement x){
            //-- SQL Server Syntax  
            //Functions(except inline table-valued functions),Stored Procedures,and DML Triggers  
            //{ EXEC | EXECUTE }AS{ CALLER | SELF | OWNER | 'user_name' }  

            //DDL Triggers with Database Scope  
            //{ EXEC | EXECUTE }AS{ CALLER | SELF | 'user_name' }  

            //DDL Triggers with Server Scope and logon triggers  
            //{ EXEC | EXECUTE }AS{ CALLER | SELF | 'login_name' }  

            //Queues  
            //{ EXEC | EXECUTE }AS{ SELF | OWNER | 'user_name' }
            return Default_void;
        }
        private e.Expression EndConversationStatement(EndConversationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MoveConversationStatement(MoveConversationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WaitForSupportedStatement(WaitForSupportedStatement x)=>x switch{
            GetConversationGroupStatement y=>this.GetConversationGroupStatement(y),
            ReceiveStatement y=>this.ReceiveStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression GetConversationGroupStatement(GetConversationGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ReceiveStatement(ReceiveStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SendStatement(SendStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterSchemaStatement(AlterSchemaStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterAsymmetricKeyStatement(AlterAsymmetricKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServiceMasterKeyStatement(AlterServiceMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BeginConversationTimerStatement(BeginConversationTimerStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BeginDialogStatement(BeginDialogStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BackupRestoreMasterKeyStatementBase(BackupRestoreMasterKeyStatementBase x)=>x switch{
            BackupServiceMasterKeyStatement y=>this.BackupServiceMasterKeyStatement(y),
            RestoreServiceMasterKeyStatement y=>this.RestoreServiceMasterKeyStatement(y),
            BackupMasterKeyStatement y=>this.BackupMasterKeyStatement(y),
            RestoreMasterKeyStatement y=>this.RestoreMasterKeyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression BackupServiceMasterKeyStatement(BackupServiceMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RestoreServiceMasterKeyStatement(RestoreServiceMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BackupMasterKeyStatement(BackupMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RestoreMasterKeyStatement(RestoreMasterKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TSqlStatementSnippet(TSqlStatementSnippet x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditSpecificationStatement(AuditSpecificationStatement x)=>x switch{
            CreateDatabaseAuditSpecificationStatement y=>this.CreateDatabaseAuditSpecificationStatement(y),
            AlterDatabaseAuditSpecificationStatement y=>this.AlterDatabaseAuditSpecificationStatement(y),
            CreateServerAuditSpecificationStatement y=>this.CreateServerAuditSpecificationStatement(y),
            AlterServerAuditSpecificationStatement y=>this.AlterServerAuditSpecificationStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateDatabaseAuditSpecificationStatement(CreateDatabaseAuditSpecificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseAuditSpecificationStatement(AlterDatabaseAuditSpecificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateServerAuditSpecificationStatement(CreateServerAuditSpecificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerAuditSpecificationStatement(AlterServerAuditSpecificationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ServerAuditStatement(ServerAuditStatement x)=>x switch{
            CreateServerAuditStatement y=>this.CreateServerAuditStatement(y),
            AlterServerAuditStatement y=>this.AlterServerAuditStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateServerAuditStatement(CreateServerAuditStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerAuditStatement(AlterServerAuditStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DatabaseEncryptionKeyStatement(DatabaseEncryptionKeyStatement x)=>x switch{
            CreateDatabaseEncryptionKeyStatement y=>this.CreateDatabaseEncryptionKeyStatement(y),
            AlterDatabaseEncryptionKeyStatement y=>this.AlterDatabaseEncryptionKeyStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateDatabaseEncryptionKeyStatement(CreateDatabaseEncryptionKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseEncryptionKeyStatement(AlterDatabaseEncryptionKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropDatabaseEncryptionKeyStatement(DropDatabaseEncryptionKeyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ResourcePoolStatement(ResourcePoolStatement x)=>x switch{
            CreateResourcePoolStatement y=>this.CreateResourcePoolStatement(y),
            AlterResourcePoolStatement y=>this.AlterResourcePoolStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateResourcePoolStatement(CreateResourcePoolStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterResourcePoolStatement(AlterResourcePoolStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalResourcePoolStatement(ExternalResourcePoolStatement x)=>x switch{
            CreateExternalResourcePoolStatement y=>this.CreateExternalResourcePoolStatement(y),
            AlterExternalResourcePoolStatement y=>this.AlterExternalResourcePoolStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateExternalResourcePoolStatement(CreateExternalResourcePoolStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterExternalResourcePoolStatement(AlterExternalResourcePoolStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WorkloadGroupStatement(WorkloadGroupStatement x)=>x switch{
            CreateWorkloadGroupStatement y=>this.CreateWorkloadGroupStatement(y),
            AlterWorkloadGroupStatement y=>this.AlterWorkloadGroupStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateWorkloadGroupStatement(CreateWorkloadGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterWorkloadGroupStatement(AlterWorkloadGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BrokerPriorityStatement(BrokerPriorityStatement x)=>x switch{
            CreateBrokerPriorityStatement y=>this.CreateBrokerPriorityStatement(y),
            AlterBrokerPriorityStatement y=>this.AlterBrokerPriorityStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateBrokerPriorityStatement(CreateBrokerPriorityStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterBrokerPriorityStatement(AlterBrokerPriorityStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateFullTextStopListStatement(CreateFullTextStopListStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterFullTextStopListStatement(AlterFullTextStopListStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateCryptographicProviderStatement(CreateCryptographicProviderStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterCryptographicProviderStatement(AlterCryptographicProviderStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventSessionStatement(EventSessionStatement x)=>x switch{
            CreateEventSessionStatement y=>this.CreateEventSessionStatement(y),
            AlterEventSessionStatement y=>this.AlterEventSessionStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateEventSessionStatement(CreateEventSessionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterEventSessionStatement(AlterEventSessionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterResourceGovernorStatement(AlterResourceGovernorStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateSpatialIndexStatement(CreateSpatialIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationStatement(AlterServerConfigurationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationSetBufferPoolExtensionStatement(AlterServerConfigurationSetBufferPoolExtensionStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationSetDiagnosticsLogStatement(AlterServerConfigurationSetDiagnosticsLogStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationSetFailoverClusterPropertyStatement(AlterServerConfigurationSetFailoverClusterPropertyStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationSetHadrClusterStatement(AlterServerConfigurationSetHadrClusterStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationSetSoftNumaStatement(AlterServerConfigurationSetSoftNumaStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AvailabilityGroupStatement(AvailabilityGroupStatement x)=>x switch{
            CreateAvailabilityGroupStatement y=>this.CreateAvailabilityGroupStatement(y),
            AlterAvailabilityGroupStatement y=>this.AlterAvailabilityGroupStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CreateAvailabilityGroupStatement(CreateAvailabilityGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterAvailabilityGroupStatement(AlterAvailabilityGroupStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateFederationStatement(CreateFederationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterFederationStatement(AlterFederationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UseFederationStatement(UseFederationStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DiskStatement(DiskStatement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateColumnStoreIndexStatement(CreateColumnStoreIndexStatement x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ExecuteOption(ExecuteOption x)=>x switch{
            ResultSetsExecuteOption y=>this.ResultSetsExecuteOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// ExecuteOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ResultSetsExecuteOption(ResultSetsExecuteOption x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ResultSetDefinition(ResultSetDefinition x)=>x switch{
            InlineResultSetDefinition y=>this.InlineResultSetDefinition(y),
            SchemaObjectResultSetDefinition y=>this.SchemaObjectResultSetDefinition(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// ResultSetDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression InlineResultSetDefinition(InlineResultSetDefinition x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// ResultSetDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression SchemaObjectResultSetDefinition(SchemaObjectResultSetDefinition x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ResultColumnDefinition(ResultColumnDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecuteSpecification(ExecuteSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecuteContext(ExecuteContext x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecuteParameter(ExecuteParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecutableEntity(ExecutableEntity x)=>x switch{
            ExecutableProcedureReference y=>this.ExecutableProcedureReference(y),
            ExecutableStringList y=>this.ExecutableStringList(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExecutableProcedureReference(ExecutableProcedureReference x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecutableStringList(ExecutableStringList x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ProcedureReferenceName(ProcedureReferenceName x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AdHocDataSource(AdHocDataSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ViewOption(ViewOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TriggerObject(TriggerObject x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TriggerOption(TriggerOption x)=>x switch{
            ExecuteAsTriggerOption y=>this.ExecuteAsTriggerOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExecuteAsTriggerOption(ExecuteAsTriggerOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TriggerAction(TriggerAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ProcedureReference(ProcedureReference x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MethodSpecifier(MethodSpecifier x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ProcedureOption(ProcedureOption x)=>x switch{
            ExecuteAsProcedureOption y=>this.ExecuteAsProcedureOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExecuteAsProcedureOption(ExecuteAsProcedureOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FunctionOption(FunctionOption x)=>x switch{
            InlineFunctionOption y=>this.InlineFunctionOption(y),
            ExecuteAsFunctionOption y=>this.ExecuteAsFunctionOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression InlineFunctionOption(InlineFunctionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecuteAsFunctionOption(ExecuteAsFunctionOption x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// create view autoadmin_backup_configurations as
        /// [with xmlnamespaces(N'http://schemas.datacontract.org/2004/07/Microsoft.SqlServer.SmartAdmin.SmartBackupAgent' as sb)]
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private void XmlNamespaces(XmlNamespaces x){
        }
        private e.Expression XmlNamespacesElement(XmlNamespacesElement x)=>x switch{
            XmlNamespacesDefaultElement y=>this.XmlNamespacesDefaultElement(y),
            XmlNamespacesAliasElement y=>this.XmlNamespacesAliasElement(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression XmlNamespacesDefaultElement(XmlNamespacesDefaultElement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression XmlNamespacesAliasElement(XmlNamespacesAliasElement x){
            throw this.単純NotSupportedException(x);
        }
        private e.BinaryExpression CommonTableExpression(CommonTableExpression x){
            //WITH ExpressionName AS(
            //    SELECT *
            //    FROM v x
            //    WHERE x.log_id=@log_id
            //    UNION ALL
            //    SELECT node.* FROM v node
            //    INNER JOIN ExpressionName AS leaf ON(node.id=leaf.parent)
            //)
            //var ExpressionName=from x in v select * where
            //while(true){
            //    var t=from node in v join leaf in graph on node.id=leaf.parent select *
            //    ExpressionName.AddRange(t)
            //    if(t.Count==0)break;
            //}
            var WITH名=this.Identifier(x.ExpressionName);
            var QueryExpression=this.QueryExpression(x.QueryExpression);
            var Parameter=e.Expression.Parameter(QueryExpression.Type,WITH名+"Set");
            this.Dictionary_With名_Set_ColumnAliases.Add(WITH名,(Parameter,this.RefPeek.List_ColumnAlias.ToArray()));
            this.AddTableVariable(Parameter);
            //this.List_ScalarVariable.Add(Parameter);
            return e.Expression.Assign(Parameter,QueryExpression);
        }
        private e.Expression WithCtesAndXmlNamespaces(WithCtesAndXmlNamespaces x){
            var StackSubquery単位の情報=this._StackSubquery単位の情報;
            StackSubquery単位の情報.Push();
            //WITH graph AS(
            //    SELECT *
            //    FROM v x
            //    WHERE x.log_id=@log_id
            //    UNION ALL
            //    SELECT node.* FROM v node
            //    INNER JOIN graph AS leaf ON(node.id=leaf.parent)
            //)
            //var graph=from x in v select * where
            //while(true){
            //    var t=from node in v join leaf in graph on node.id=leaf.parent select *
            //    graph.Add(t)
            //    if(t.Count==0)break;
            //}
            //
            //    
            if(x.ChangeTrackingContext is not null)this.ValueExpression(x.ChangeTrackingContext);
            //ref var ref__Subquery単位の情報=ref this._StackSubquery単位の情報.RefPeek;
            //var Dictionary_DatabaseSchemaTable_ColumnExpression=ref__Subquery単位の情報.Dictionary_DatabaseSchemaTable_ColumnExpression;
            //var List_ColumnAlias=ref__Subquery単位の情報.List_ColumnAlias;
            //var List_ColumnExpression=ref__Subquery単位の情報.List_ColumnExpression;
            //foreach(var CommonTableExpression in x.CommonTableExpressions){
            //    var(共通部分式名,_)=this.CommonTableExpression(CommonTableExpression);
            //    var List_Select_Count=List_ColumnExpression.Count;
            //    for(var a=0;a<List_Select_Count;a++){
            //        Dictionary_DatabaseSchemaTable_ColumnExpression.Add(
            //            共通部分式名+List_ColumnAlias[a],
            //            List_ColumnExpression[a]
            //        );
            //    }
            //}
            if(x.XmlNamespaces is not null){
                this.XmlNamespaces(x.XmlNamespaces);
            }
            var x_CommonTableExpressions=x.CommonTableExpressions;
            var x_CommonTableExpressions_Count=x_CommonTableExpressions.Count;
            if(x_CommonTableExpressions_Count==0)return Default_void;
            var List_Assign=new List<e.BinaryExpression>(x_CommonTableExpressions_Count);
            ref var RefPeek=ref this.RefPeek;
            for(var a=0;a<x_CommonTableExpressions_Count;a++)
                List_Assign.Add(this.CommonTableExpression(x_CommonTableExpressions[a]));
            StackSubquery単位の情報.Pop();
            if(List_Assign.Count==1)return List_Assign[0];
            return e.Expression.Block(List_Assign);
        }
        //private e.Expression Type FunctionReturnType(FunctionReturnType x)=>x switch{
        //    TableValuedFunctionReturnType y=>this.TableValuedFunctionReturnType(y),
        //    ScalarFunctionReturnType y=>this.ScalarFunctionReturnType(y),
        //    SelectFunctionReturnType y=>this.SelectFunctionReturnType(y),
        //    _=>throw this.単純NotSupportedException(x)
        //};
        /// <summary>
        /// @Student table(TestID int not null)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private void TableValuedFunctionReturnType(TableValuedFunctionReturnType x){
            this.DeclareTableVariableBody(x.DeclareTableVariableBody);
        }
        private Type ScalarFunctionReturnType(ScalarFunctionReturnType x){
            var DataType=DataTypeReferenceからTypeに変換(x.DataType);
            return DataType;
        }
        /// <summary>
        /// create function r(@id int)return table as return(....)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression SelectFunctionReturnType(SelectFunctionReturnType x){
            var SelectStatement=this.SelectStatement(x.SelectStatement);
            return SelectStatement;
        }
        private Type DataTypeReference(DataTypeReference x)=>x switch{
            ParameterizedDataTypeReference y=>this.ParameterizedDataTypeReference(y),
            XmlDataTypeReference y=>this.XmlDataTypeReference(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private Type ParameterizedDataTypeReference(ParameterizedDataTypeReference x)=>x switch{
            SqlDataTypeReference y=>this.SqlDataTypeReference(y),
            UserDataTypeReference y=>this.UserDataTypeReference(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private Type SqlDataTypeReference(SqlDataTypeReference x){
            var DBType=x.Name.BaseIdentifier.Value;
            return CommonLibrary.SQLのTypeからTypeに変換(DBType);
        }
        private Type UserDataTypeReference(UserDataTypeReference x){
            var DBType = x.Name.BaseIdentifier.Value;
            return CommonLibrary.SQLのTypeからTypeに変換(DBType);
        }
        private Type XmlDataTypeReference(XmlDataTypeReference x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// ContactID int primary key not null
        /// FirstName nvarchar (50) null
        /// LastName nvarchar (50) null
        /// JobTitle nvarchar (50) null
        /// ContactType nvarchar (50) null
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private (Type,string[]Names)TableDefinition(TableDefinition x){
            foreach(var TableConstraint in x.TableConstraints) {
                this.ConstraintDefinition(TableConstraint);
            }
            foreach(var Index in x.Indexes)
                this.IndexDefinition(Index);
            if(x.SystemTimePeriod is not null)this.SystemTimePeriodDefinition(x.SystemTimePeriod);
            var x_ColumnDefinitions=x.ColumnDefinitions;
            var x_ColumnDefinitions_Count= x_ColumnDefinitions.Count;
            var Names=new string[x_ColumnDefinitions_Count];
            for(var a = 0;a<x_ColumnDefinitions_Count;a++)
                Names[a]=x_ColumnDefinitions[a].ColumnIdentifier.Value;
            var ElementType=ValueTupleでNewする(this.作業配列,x_ColumnDefinitions,0);
            return (this.作業配列.MakeGenericType(typeof(Set<>),ElementType),Names);
//            foreach(var ColumnDefinition in x.ColumnDefinitions)
  //.              this.ColumnDefinition(ColumnDefinition);
     //       throw this.単純NotSupportedException(x);
            Type ValueTupleでNewする(Optimizer.作業配列 作業配列,IList<ColumnDefinition> Arguments,int Offset) {
                var 残りType数 = Arguments.Count-Offset;
                switch(残りType数) {
                    case 1:return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple1,
                        this.ColumnDefinition(Arguments[Offset+0])
                    );
                    case 2:return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple2,
                        this.ColumnDefinition(Arguments[Offset+0]),
                        this.ColumnDefinition(Arguments[Offset+1])
                    );
                    case 3:return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple3,
                        this.ColumnDefinition(Arguments[Offset+0]),
                        this.ColumnDefinition(Arguments[Offset+1]),
                        this.ColumnDefinition(Arguments[Offset+2])
                    );
                    case 4:return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple4,
                        this.ColumnDefinition(Arguments[Offset+0]),
                        this.ColumnDefinition(Arguments[Offset+1]),
                        this.ColumnDefinition(Arguments[Offset+2]),
                        this.ColumnDefinition(Arguments[Offset+3])
                    );
                    case 5:return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple5,
                        this.ColumnDefinition(Arguments[Offset+0]),
                        this.ColumnDefinition(Arguments[Offset+1]),
                        this.ColumnDefinition(Arguments[Offset+2]),
                        this.ColumnDefinition(Arguments[Offset+3]),
                        this.ColumnDefinition(Arguments[Offset+4])
                    );
                    case 6:return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple6,
                        this.ColumnDefinition(Arguments[Offset+0]),
                        this.ColumnDefinition(Arguments[Offset+1]),
                        this.ColumnDefinition(Arguments[Offset+2]),
                        this.ColumnDefinition(Arguments[Offset+3]),
                        this.ColumnDefinition(Arguments[Offset+4]),
                        this.ColumnDefinition(Arguments[Offset+5])
                    );
                    case 7:return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple7,
                        this.ColumnDefinition(Arguments[Offset+0]),
                        this.ColumnDefinition(Arguments[Offset+1]),
                        this.ColumnDefinition(Arguments[Offset+2]),
                        this.ColumnDefinition(Arguments[Offset+3]),
                        this.ColumnDefinition(Arguments[Offset+4]),
                        this.ColumnDefinition(Arguments[Offset+5]),
                        this.ColumnDefinition(Arguments[Offset+6])
                    );
                    default:{
                        var Type7 = ValueTupleでNewする(作業配列,Arguments,Offset+7);
                        return 作業配列.MakeGenericType(
                            Reflection.ValueTuple.ValueTuple6,
                            this.ColumnDefinition(Arguments[Offset+0]),
                            this.ColumnDefinition(Arguments[Offset+1]),
                            this.ColumnDefinition(Arguments[Offset+2]),
                            this.ColumnDefinition(Arguments[Offset+3]),
                            this.ColumnDefinition(Arguments[Offset+4]),
                            this.ColumnDefinition(Arguments[Offset+6]),
                            Type7
                        );
                    }
                }
            }
        }
        /// <summary>
        /// DeclareTableVariableBody orTSqlFragment
        /// returns @Student table(TestID int not null)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression DeclareTableVariableBody(DeclareTableVariableBody x){
            var AsDefined=x.AsDefined;
            var (Type,Names)=this.TableDefinition(x.Definition);
            var Name= this.Identifier(x.VariableName);
            var Variable=e.Expression.Parameter(Type,Name);
            this.AddTableVariable(Variable,Names);
            return Variable;
            //var TableDefinition=this.TableDefinition(x.Definition);
            //this.変数CreateFunctionStatement.変数名=this.Identifier(x.VariableName);
            //return this.Identifier(x.VariableName);
            //return typeof(void);
        }
        private(e.Expression Set,e.ParameterExpression Element)TableReference(TableReference x)=>x switch{
            TableReferenceWithAlias y=>this.TableReferenceWithAlias(y),
            JoinTableReference y=>this.JoinTableReference(y),
            JoinParenthesisTableReference y=>this.JoinParenthesisTableReference(y),
            OdbcQualifiedJoinTableReference y=>this.OdbcQualifiedJoinTableReference(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private(e.Expression Set,e.ParameterExpression Element)TableReferenceWithAlias(TableReferenceWithAlias x)=>x switch{
            NamedTableReference y=>this.NamedTableReference(y),
            TableReferenceWithAliasAndColumns y=>this.TableReferenceWithAliasAndColumns(y),
            FullTextTableReference y=>this.FullTextTableReference(y),
            SemanticTableReference y=>this.SemanticTableReference(y),
            OpenXmlTableReference y=>this.OpenXmlTableReference(y),
            OpenJsonTableReference y=>this.OpenJsonTableReference(y),
            InternalOpenRowset y=>this.InternalOpenRowset(y),
            OpenQueryTableReference y=>this.OpenQueryTableReference(y),
            AdHocTableReference y=>this.AdHocTableReference(y),
            BuiltInFunctionTableReference y=>this.BuiltInFunctionTableReference(y),
            GlobalFunctionTableReference y=>this.GlobalFunctionTableReference(y),
            PivotedTableReference y=>this.PivotedTableReference(y),
            UnpivotedTableReference y=>this.UnpivotedTableReference(y),
            VariableTableReference y=>this.VariableTableReference(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// ごく普通のfromに書くテーブル名
        /// </summary>
        /// <param name="x"></param>
        /// <returns>(Expressions.Expression Expression(Table=>Table.Field),Expressions.ParameterExpression Parameter(Table=>))</returns>
        private(e.Expression Set,e.ParameterExpression ss)NamedTableReference(NamedTableReference x){
            ref var RefPeek=ref this.RefPeek;
            var Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            var Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
            var List_アスタリスクColumnAlias=RefPeek.List_アスタリスクColumnAlias;
            var List_アスタリスクColumnExpression=RefPeek.List_アスタリスクColumnExpression;
            //var Dictionary_With名_Set_ColumnAliases=this.Dictionary_With名_Set_ColumnAliases;
            var SchemaObject=x.SchemaObject;
            Debug.Assert(SchemaObject is not null);
            var Table=SchemaObject.BaseIdentifier.Value;
            if(this.Dictionary_With名_Set_ColumnAliases.TryGetValue(Table,out var Set_ColumnAliases)) {
                //var Set_Type=Set.Type;
                var Element=e.Expression.Parameter(IEnumerable1のT(Set_ColumnAliases.Set.Type),Table);
                var x_Alias=x.Alias;
                if(x_Alias is not null) Table=x_Alias.Value;
                var TableDot=Table+'.';
                e.Expression ValueTuple=Element;
                var Item番号=1;
                foreach(var ColumnAlias in Set_ColumnAliases.ColumnAliases){
                    var Item=ValueTuple_Item(ref ValueTuple,ref Item番号);
                    List_アスタリスクColumnAlias.Add(ColumnAlias);
                    List_アスタリスクColumnExpression.Add(Item);
                    ////Table.Column
                    //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot+ColumnAlias,Item);
                    ////Column
                    //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,         ColumnAlias,Item);
                    DictionaryにKey0とKey1があればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot+ColumnAlias,ColumnAlias,Item);
                }
                return (Set_ColumnAliases.Set,Element);
            } else {
                var ContainerType=this.ContainerType;
                Debug.Assert(ContainerType!=null);
                const BindingFlags BindingFlags=BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.IgnoreCase|BindingFlags.GetProperty;
                PropertyInfo Schema_PropertyInfo;
                PropertyInfo Table_Property;
                Type Table_Type;
                if(SchemaObject.SchemaIdentifier is null){
                    foreach(var Schema in ContainerType.GetProperties()){
                        foreach(var TableViewFunction in Schema.PropertyType.GetProperties(BindingFlags)){
                            if(string.Equals(TableViewFunction.Name,Table,StringComparison.OrdinalIgnoreCase)){
                                Table_Property=TableViewFunction;
                                Schema_PropertyInfo=Schema;
                                Table_Type=Table_Property.PropertyType;
                                goto 発見;
                            }
                        }
                    }
                    throw new NotSupportedException($"{Table}が発見できなかった");
                }else{
                    //Schema_PropertyInfo=ContainerType.GetProperty(SchemaObject.SchemaIdentifier.Value,BindingFlags)!;
                    var Schema= SchemaObject.SchemaIdentifier.Value;
                    var Schema_PropertyInfo0=ContainerType.GetProperties(BindingFlags).Where(p => string.Equals(p.Name,Schema,StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                    if(Schema_PropertyInfo0 is null){
                        Trace.WriteLine(Schema);
                        throw new NotImplementedException($"{Schema}スキーマは定義されていなかった");
                    }
                    //Table_Property=Schema_PropertyInfo.PropertyType.GetProperty(Table,BindingFlags)!;
                    Schema_PropertyInfo=Schema_PropertyInfo0;
                    var Table_Property0=Schema_PropertyInfo.PropertyType.GetProperties(BindingFlags).Where(p=>string.Equals(p.Name,Table,StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                    if(Table_Property0 is null){
                        Trace.WriteLine(Table);
                        throw new NotImplementedException($"{Table}テーブルプロパティが定義されていなかった");
                    }
                    Table_Property=Table_Property0;
                    //if (x.Value=="objects$")
                    //{
                    //   // x.Value="(SELECT x.*,s.null_on_null_input from sys.all_objects x JOIN sys.system_sql_modules s on x.object_id=s.object_id)";
                    //}
                    //Debug.Assert(Table_Property is not null,$"{Table}テーブルプロパティが定義されていなかった");
                    Table_Type=Table_Property.PropertyType;
                }
発見:
                var T=IEnumerable1のT(Table_Type);
                var ctor_Parameters=T.GetConstructors()[0].GetParameters();
                var ctor_Parameters_Length=ctor_Parameters.Length;
                var ColumnAliases=new string[ctor_Parameters_Length];
                var x_Alias=x.Alias;
                var Database=T.FullName!.Split('.')[0];
                string Database_Schema_Table_Dot,Schema_Table_Dot;
                if(x_Alias is null){
                    Table=Table_Property.Name;
                    var Schema_Table=Schema_PropertyInfo.Name+'.'+Table;
                    Schema_Table_Dot=Schema_Table+'.';
                    var Database_Schema_Table=Database+'.'+Schema_Table;
                    Database_Schema_Table_Dot=Database_Schema_Table+'.';
                    Dictionary_TableAlias_ColumnAliases.Add(         Schema_Table,ColumnAliases);
                    Dictionary_TableAlias_ColumnAliases.Add(Database_Schema_Table,ColumnAliases);
                }else{
                    Table=x_Alias.Value;
                    Debug.Assert(x_Alias.Value==Table);
                    Schema_Table_Dot="";
                    Database_Schema_Table_Dot="";
                }
                Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
                Debug.Assert(ContainerType is not null);
                var Table_Dot=Table+'.';
                var Set=e.Expression.MakeMemberAccess(
                    e.Expression.Property(this.Container,Schema_PropertyInfo),
                    Table_Property
                );
                var Element=e.Expression.Parameter(T,Table);
                if(x_Alias is null){
                    Table=Table_Property.Name;
                    var Schema_Table=Schema_PropertyInfo.Name+'.'+Table;
                    var Database_Schema_Table=Database+'.'+Schema_Table;
                    ////Database.Schema.Table
                    //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table,Element);
                    ////Schema.Table
                    //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,         Schema_Table,Element);
                    DictionaryにKey0とKey1があればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table,Schema_Table,Element);
                }
                //Table
                DictionaryにKeyがあればValueにnullを代入        (Dictionary_DatabaseSchemaTable_ColumnExpression,                Table,Element);
                for(var a=0;a<ctor_Parameters_Length;a++){
                    var ColumnAlias=ctor_Parameters[a].Name;
                    var Item=e.Expression.PropertyOrField(Element,ColumnAlias);
                    ColumnAliases[a]=ColumnAlias;
                    List_アスタリスクColumnAlias.Add(ColumnAlias);
                    List_アスタリスクColumnExpression.Add(Item);
                    if(x_Alias is null){
                        ////Database.Schema.Table.Column
                        //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table_Dot+ColumnAlias,Item);
                        ////Schema.Table.Column
                        //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,         Schema_Table_Dot+ColumnAlias,Item);
                        DictionaryにKey0とKey1があればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table_Dot+ColumnAlias,Schema_Table_Dot+ColumnAlias,Item);
                    }
                    ////Table.Column
                    //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,                Table_Dot+ColumnAlias,Item);
                    ////Column
                    //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,                          ColumnAlias,Item);
                    DictionaryにKey0とKey1があればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot+ColumnAlias,ColumnAlias,Item);
                }
                DictionaryのValueがnullのKeyをRemove            (Dictionary_DatabaseSchemaTable_ColumnExpression);
                return(Set,Element);
            }
        }
        private static void DictionaryにKeyがあればValueにnullを代入(SortedDictionary<string,e.Expression?> Dictionary,string Key,e.Expression Value){
            if(Dictionary.ContainsKey(Key))Dictionary[Key]=null;
            else Dictionary.Add(Key,Value);
        }
        private static void DictionaryにDotKeyとKeyがあればValueにnullを代入(SortedDictionary<string,e.Expression?> Dictionary,string? Dot,string Key,e.Expression Value) {
            if(Dot is not null) DictionaryにKeyがあればValueにnullを代入(Dictionary,Dot+Key,Value);
            DictionaryにKeyがあればValueにnullを代入(Dictionary,Key,Value);
        }
        private static void DictionaryにKey0とKey1があればValueにnullを代入(SortedDictionary<string,e.Expression?> Dictionary,string Key0,string Key1,e.Expression Value) {
            DictionaryにKeyがあればValueにnullを代入(Dictionary,Key0,Value);
            DictionaryにKeyがあればValueにnullを代入(Dictionary,Key1,Value);
        }
        private static void DictionaryのValueがnullのKeyをRemove(IDictionary<string,e.Expression?> RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression){
            foreach(var KV in RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.ToList())
                if(KV.Value is null)
                    RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Remove(KV.Key);
        }
        private(e.Expression Set,e.ParameterExpression Element)TableReferenceWithAliasAndColumns(TableReferenceWithAliasAndColumns x)=>x switch{
            SchemaObjectFunctionTableReference y=>this.SchemaObjectFunctionTableReference(y),
            QueryDerivedTable y=>this.QueryDerivedTable(y),
            InlineDerivedTable y=>this.InlineDerivedTable(y),
            BulkOpenRowset y=>this.BulkOpenRowset(y),
            DataModificationTableReference y=>this.DataModificationTableReference(y),
            ChangeTableChangesTableReference y=>this.ChangeTableChangesTableReference(y),
            ChangeTableVersionTableReference y=>this.ChangeTableVersionTableReference(y),
            VariableMethodCallTableReference y=>this.VariableMethodCallTableReference(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private(e.Expression Set,e.ParameterExpression Element)SchemaObjectFunctionTableReference(SchemaObjectFunctionTableReference x){
            //SELECT 
            //     jc.[JobCandidateID] 
            //    ,jc.[BusinessEntityID] 
            //    ,[Resume].ref.value(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume"; 
            //       (/Resume/Name/Name.Prefix)[1]','nvarchar(30)')AS [Name.Prefix] 
            //    ,[Resume].ref.value(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume";
            //       (/Resume/Name/Name.First)[1]','nvarchar(30)')AS [Name.First] 
            //FROM [HumanResources].[JobCandidate] jc 
            //CROSS APPLY 
            //    jc.[Resume].nodes(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume";/Resume')
            //AS[Resume](ref)
            //SchemaObject.BaseIdentifier.Value(x.Parameters[0])as Alias(x.Columns[0])
            //sys.fn_helpdatatypemapとか
            var SchemaObject=x.SchemaObject;
            string Key;
            if(SchemaObject.DatabaseIdentifier is null){
                if(SchemaObject.SchemaIdentifier is null){
                    Debug.Assert(this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression is not null);
                    throw new NotSupportedException(Properties.Resources.スキーマとテーブルが発見できなかった);
                }else{
                    Key=SchemaObject.SchemaIdentifier.Value;
                }
            }else{
                if(SchemaObject.SchemaIdentifier is null){
                    throw new NotSupportedException(Properties.Resources.スキーマとテーブルが発見できなかった);
                }else{
                    Key=SchemaObject.DatabaseIdentifier.Value+'.'+SchemaObject.SchemaIdentifier.Value;
                }
            }
            //var Schema = this.List_Schema.SingleOrDefault(p => p.Member.Name==Key);
            var Schema = this.FindSchema(Key);
            //var Schema=this.ContainerType.GetProperty(Key);
            if(Schema is not null) {
                Debug.Assert(Schema.Expression!=null&&(e.ParameterExpression)Schema.Expression==this.Container);
                var Method =FindFunction(Schema.Type,SchemaObject.BaseIdentifier.Value);
                //var Method=Schema.Type.GetMethod(SchemaObject.BaseIdentifier.Value,BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
                if(Method is null) {
                    Trace.WriteLine("function2:"+SchemaObject.BaseIdentifier.Value);
                    //動的な型を使ってごまかす
                    return (TABLE_DEE,e.Expression.Parameter(typeof(LinqDB.Databases.AttributeEmpty),x.Alias?.Value));
                }
                Debug.Assert(x.Columns.Count==0);
                var Parameters=x.Parameters;
                var Arguments_Length=Parameters.Count;
                var Method_GetParameters=Method!.GetParameters();
                Debug.Assert(Method_GetParameters.Length==Arguments_Length);
                //var x_Alias_Value=x.Alias.Value;
                var Arguments=new e.Expression[Arguments_Length];
                for(var a = 0;a<Arguments_Length;a++) { 
                    var Parameter0=Parameters[a];
                    var Method_GetParameter=Method_GetParameters[a];
                    if(Parameter0 is DefaultLiteral) {
                        if(Method_GetParameter.HasDefaultValue) {
                            Arguments[a]=e.Expression.Constant(Method_GetParameter.DefaultValue,Method_GetParameter.ParameterType);
                        } else { 
                            Arguments[a]=e.Expression.Default(Method_GetParameter.ParameterType);
                        }
                    } else { 
                        Arguments[a]=this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(Parameter0),Method_GetParameter.ParameterType);
                    }
                }
                var Select_Expression=e.Expression.Call(Schema,Method,Arguments);
                var Parameter=e.Expression.Parameter(IEnumerable1のT(Select_Expression.Type),x.Alias?.Value);
                {
                    var RefPeek=this.RefPeek;
                    var Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
                    var List_アスタリスクColumnAlias=RefPeek.List_アスタリスクColumnAlias;
                    var List_アスタリスクColumnExpression=RefPeek.List_アスタリスクColumnExpression;
                    var Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
                    //string Table;
                    var ContainerType=this.ContainerType;
                    //const BindingFlags BindingFlags=BindingFlags.Instance|BindingFlags.Public|BindingFlags.FlattenHierarchy|BindingFlags.IgnoreCase|BindingFlags.GetProperty|BindingFlags.GetField;
                    //PropertyInfo Schema_PropertyInfo;
                    //PropertyInfo Table_Property;
                    var Table_Type=Method.ReturnType;
                    var T=IEnumerable1のT(Table_Type);
                    var ctor_Parameters=T.GetConstructors()[0].GetParameters();
                    var ctor_Parameters_Length=ctor_Parameters.Length;
                    var ColumnAliases=new string[ctor_Parameters_Length];
                    var x_Alias = x.Alias;
                    string? Table = null, TableDot = null;
                    if(x_Alias is not null) {
                        Table=x_Alias.Value;
                        TableDot = Table+'.';
                    }
                    //Table
                    if(Table is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table,Parameter);
                    for(var a=0;a<ctor_Parameters_Length;a++){
                        var ColumnAlias=ctor_Parameters[a].Name;
                        var Item=e.Expression.PropertyOrField(Parameter,ColumnAlias);
                        ColumnAliases[a]=ColumnAlias;
                        List_アスタリスクColumnAlias.Add(ColumnAlias);
                        List_アスタリスクColumnExpression.Add(Item);
                        ////Table.Column
                        //if(TableDot is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot+ColumnAlias,Item);
                        ////Column
                        //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,ColumnAlias,Item);
                        DictionaryにDotKeyとKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot,ColumnAlias,Item);
                    }
                    DictionaryのValueがnullのKeyをRemove(Dictionary_DatabaseSchemaTable_ColumnExpression);
                }
                return(Select_Expression,Parameter);
            }else if(SchemaObject.BaseIdentifier.Value.Equals("nodes",StringComparison.OrdinalIgnoreCase)){ 
                ref var RefPeek=ref this.RefPeek;
                var RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
                var RefPeek_Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
                Debug.Assert(x.Parameters.Count==1);
                var x_Columns=x.Columns;
                Debug.Assert(x_Columns.Count==1);
                var Select_Expression=RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[Key];
                Debug.Assert(Select_Expression is not null);
                var x_Columns_Count=x_Columns.Count;
                var x_Alias_Value=x.Alias.Value;
                var ColumnAliases=new string[x_Columns_Count];
                RefPeek_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value,ColumnAliases);
                var ListアスタリスクColumnAliase=RefPeek.List_アスタリスクColumnAlias;
                var x_Alias_Value_Dot=x_Alias_Value+'.';
                for(var a=0;a<x_Columns_Count;a++){
                    var ColumnAlias=x_Columns[a].Value;
                    RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(x_Alias_Value_Dot+ColumnAlias,Select_Expression);
                    //複数テーブルで同一列名があったときに無効にする
                    if(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.ContainsKey(ColumnAlias)){
                        RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[ColumnAlias]=null!;
                    }else{
                        RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(ColumnAlias,Select_Expression);
                    }
                    ColumnAliases[a]=ColumnAlias;
                    ListアスタリスクColumnAliase.Add(ColumnAlias);
                }
                DictionaryのValueがnullのKeyをRemove(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression);
                Debug.Assert(Select_Expression.Type==typeof(XDocument));
                Select_Expression=e.Expression.Call(
                    Product.SQLServer.Reflection.nodes,
                    Select_Expression,
                    this.ScalarExpression(x.Parameters[0])
                );
                var Parameter=e.Expression.Parameter(typeof(XElement),x.Alias.Value);
                return(Select_Expression,Parameter);
            }else{
                ref var RefPeek=ref this.RefPeek;
                var RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
                var RefPeek_Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
                Debug.Assert(x.Parameters.Count==1);
                var x_Columns=x.Columns;
                Debug.Assert(x_Columns.Count==1);
                var Select_Expression=RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[Key];
                Debug.Assert(Select_Expression is not null);
                var x_Columns_Count=x_Columns.Count;
                var x_Alias_Value=x.Alias.Value;
                var ColumnAliases=new string[x_Columns_Count];
                RefPeek_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value,ColumnAliases);

                //RefPeek_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value+".*",ColumnAliases);
                //AddStartColumnAliases(RefPeek_Dictionary_TableAlias_ColumnAliases,ColumnAliases);
                var ListアスタリスクColumnAliase=RefPeek.List_アスタリスクColumnAlias;
                var x_Alias_Value_Dot=x_Alias_Value+'.';
                for(var a=0;a<x_Columns_Count;a++){
                    //foreach(var Column in x_Columns){
                    var ColumnAlias=x_Alias_Value_Dot+x_Columns[a].Value;
                    RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(x_Alias_Value_Dot+ColumnAlias,Select_Expression);
                    //複数テーブルで同一列名があったときに無効にする
                    if(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.ContainsKey(ColumnAlias)){
                        RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[ColumnAlias]=null!;
                    }else{
                        RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(ColumnAlias,Select_Expression);
                    }
                    ColumnAliases[a]=ColumnAlias;
                    ListアスタリスクColumnAliase.Add(ColumnAlias);
                }
                DictionaryのValueがnullのKeyをRemove(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression);
                Type T;
                if(SchemaObject.BaseIdentifier.Value.Equals("nodes",StringComparison.OrdinalIgnoreCase)){
                    Debug.Assert(Select_Expression.Type==typeof(XDocument));
                    Select_Expression=e.Expression.Call(
                        Product.SQLServer.Reflection.nodes,
                        Select_Expression,
                        this.ScalarExpression(x.Parameters[0])
                    );
                    T=typeof(XElement);
                }else{
                    T=IEnumerable1のT(Select_Expression.Type);
                }
                var Parameter=e.Expression.Parameter(T,x.Alias.Value);
                return(Select_Expression,Parameter);
            }
        }
        //private void Debug0(){
        //    foreach(var b in this.RefPeek.Dictionary_TableAlias_ColumnAliases){
        //        foreach(var c in b.Value){
        //            Debug.Assert(c is not null);
        //        }
        //    }
        //}
        /// <summary>
        /// これを呼び出したら呼び出し元はRefPeekを参照して利用可能な列を取得できる
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private(e.Expression Set,e.ParameterExpression Element)QueryDerivedTable(QueryDerivedTable x){
            var StackSubquery単位の情報=this._StackSubquery単位の情報;
            StackSubquery単位の情報.Push();
            ref var RefPeek1=ref StackSubquery単位の情報.RefPeek;
            var RefPeek1_List_ColumnAlias=RefPeek1.List_ColumnAlias;
            var Result=this.QueryExpression(x.QueryExpression);
            var RefPeek1_List_ColumnAlias_Count=RefPeek1_List_ColumnAlias.Count;
            var x_Alias_Value=x.Alias.Value;
            Debug.Assert(x.Columns.Count==0,"0以外未発見");
            var ColumnAliases=new string[RefPeek1_List_ColumnAlias_Count];
            var TableAlias_Dot=x_Alias_Value+'.';
            StackSubquery単位の情報.Pop();
            ref var RefPeek0=ref StackSubquery単位の情報.RefPeek;
            var RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek0.Dictionary_DatabaseSchemaTable_ColumnExpression;
            var RefPeek0_Dictionary_TableAlias_ColumnAliases=RefPeek0.Dictionary_TableAlias_ColumnAliases;
            var RefPeek0_List_アスタリスクColumnAlias=RefPeek0.List_アスタリスクColumnAlias;
            var RefPeek0_List_アスタリスクExpression=RefPeek0.List_アスタリスクColumnExpression;
            RefPeek0_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value!,ColumnAliases);
            var Result_Parameter=e.Expression.Parameter(Result.Type.GetGenericArguments()[0],x_Alias_Value);
            e.Expression ValueTuple=Result_Parameter;
            var Item番号=1;
            for(var a=0;a<RefPeek1_List_ColumnAlias_Count;a++){
                var ColumnAlias=RefPeek1_List_ColumnAlias[a];
                var Item=ValueTuple_Item(ref ValueTuple,ref Item番号);
                DictionaryにKeyがあればValueにnullを代入(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression,ColumnAlias,Item);
                RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(TableAlias_Dot+ColumnAlias,Item);
                ColumnAliases[a]=ColumnAlias;
                RefPeek0_List_アスタリスクColumnAlias.Add(ColumnAlias);
                RefPeek0_List_アスタリスクExpression.Add(Item);
            }
            DictionaryのValueがnullのKeyをRemove(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression);
            return(Result,Result_Parameter);
        }
        private(e.Expression Set,e.ParameterExpression Element)InlineDerivedTable(InlineDerivedTable x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)BulkOpenRowset(BulkOpenRowset x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)DataModificationTableReference(DataModificationTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)ChangeTableChangesTableReference(ChangeTableChangesTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)ChangeTableVersionTableReference(ChangeTableVersionTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)VariableMethodCallTableReference(VariableMethodCallTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)FullTextTableReference(FullTextTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)SemanticTableReference(SemanticTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)OpenXmlTableReference(OpenXmlTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)OpenJsonTableReference(OpenJsonTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)OpenRowsetTableReference(OpenRowsetTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)InternalOpenRowset(InternalOpenRowset x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)OpenQueryTableReference(OpenQueryTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)AdHocTableReference(AdHocTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)BuiltInFunctionTableReference(BuiltInFunctionTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)GlobalFunctionTableReference(GlobalFunctionTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private partial(e.Expression Set,e.ParameterExpression Element)PivotedTableReference(PivotedTableReference x);
        private(e.Expression Set,e.ParameterExpression Element)UnpivotedTableReference(UnpivotedTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private(e.Expression Set,e.ParameterExpression Element)VariableTableReference(VariableTableReference x){
            ref var RefPeek = ref this.RefPeek;
            var Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            var Dictionary_TableAlias_ColumnAliases = RefPeek.Dictionary_TableAlias_ColumnAliases;
            var List_アスタリスクColumnAlias = RefPeek.List_アスタリスクColumnAlias;
            var List_アスタリスクColumnExpression = RefPeek.List_アスタリスクColumnExpression;
            //var x_Variable_Name=x.Variable.Name;
            //var ctor_Parameters = T.GetConstructors()[0].GetParameters();
            //var ctor_Parameters_Length = ctor_Parameters.Length;
            string[]ColumnAliases;
            var Set = PrivateFindVariable(this.List_定義型TableVariable,x.Variable.Name);
            //var T = IEnumerable1のT(Set!.Type);
            //var Element = e.Expression.Parameter(T,x.Variable.Name);
            //return (Set, Element);
            var x_Alias = x.Alias;
            string? Table = null, Table_Dot = null;
            if(x_Alias is not null) {
                Table=x_Alias.Value;
                Table_Dot = Table+'.';
            }
            var x_Variable_Name = x.Variable.Name;
            Debug.Assert(x_Variable_Name[0]=='@');
            if(Set is not null) {
                var T = IEnumerable1のT(Set.Type);
                var Element = e.Expression.Parameter(T,x.Variable.Name);
                var ctor_Parameters = T.GetConstructors()[0].GetParameters();
                var ctor_Parameters_Length = ctor_Parameters.Length;
                ColumnAliases = new string[ctor_Parameters_Length];
                if(Table is not null)Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
                for(var a = 0;a<ctor_Parameters_Length;a++) {
                    var ColumnAlias = ctor_Parameters[a].Name;
                    var Item = e.Expression.PropertyOrField(Element,ColumnAlias);
                    ColumnAliases[a]=ColumnAlias;
                    List_アスタリスクColumnAlias.Add(ColumnAlias);
                    List_アスタリスクColumnExpression.Add(Item);
                    ////Table.Column
                    //if(Table_Dot is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot+ColumnAlias,Item);
                    ////Column
                    //DictionaryにKeyがあればValueにnullを代入                         (Dictionary_DatabaseSchemaTable_ColumnExpression,          ColumnAlias,Item);
                    DictionaryにDotKeyとKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot,ColumnAlias,Item);
                }
                DictionaryのValueがnullのKeyをRemove(Dictionary_DatabaseSchemaTable_ColumnExpression);
                return (Set, Element);
            } else {
                foreach(var 匿名型TableVariable in this.List_匿名型TableVariable) {
                    if(匿名型TableVariable.Variable.Name!=x_Variable_Name)continue;
                    ColumnAliases=匿名型TableVariable.Names;
                    //var Element=e.Expression.Parameter(IEnumerable1のT(Set.Type),Name);
                    var Table_Type = 匿名型TableVariable.Variable.Type;
                    var T = IEnumerable1のT(Table_Type);
                    var Element = e.Expression.Parameter(T,x_Variable_Name);
                    if(Table is not null)Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
                    Debug.Assert(x_Variable_Name[0]=='@');
                    e.Expression ValueTuple = Element;
                    var Item番号 = 1;
                    for(var a = 0;a<ColumnAliases.Length;a++) {
                        var ColumnAlias = ColumnAliases[a];
                        var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                        List_アスタリスクColumnAlias.Add(ColumnAlias);
                        List_アスタリスクColumnExpression.Add(Item);
                        ////Table.Column
                        //if(Table_Dot is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot+ColumnAlias,Item);
                        ////Column
                        //DictionaryにKeyがあればValueにnullを代入                         (Dictionary_DatabaseSchemaTable_ColumnExpression,          ColumnAlias,Item);
                        DictionaryにDotKeyとKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot,ColumnAlias,Item);
                    }
                    DictionaryのValueがnullのKeyをRemove(Dictionary_DatabaseSchemaTable_ColumnExpression);
                    return(匿名型TableVariable.Variable,Element);
                }
                throw new KeyNotFoundException($"{x_Variable_Name}が見つからなかった。");
            }
        }
        private (e.Expression Set,e.ParameterExpression Element)JoinTableReference(JoinTableReference x)=>x switch{
            UnqualifiedJoin y=>this.UnqualifiedJoin(y),
            QualifiedJoin y=>this.QualifiedJoin(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// FROM後にカンマ区切りでテーブルを並べる
        /// SELECT ... FROM A,B
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private(e.Expression Set,e.ParameterExpression Element)UnqualifiedJoin(UnqualifiedJoin x){
            var Dictionary_DatabaseSchemaTable_ColumnExpression=this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            var (OuterSet,o)=this.TableReference(x.FirstTableReference);
            var TOuter=IEnumerable1のT(OuterSet.Type);
            var(InnerSet,i)=this.TableReference(x.SecondTableReference);
            var InnerSet_Type=InnerSet.Type;
            var TInner=IEnumerable1のT(InnerSet_Type);
            var 作業配列=this.作業配列;
            var ValueTuple2=作業配列.MakeGenericType(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
            var New2=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
            var selector_Body=e.Expression.New(New2,作業配列.Expressions設定(o,i));
            var selector=e.Expression.Lambda(selector_Body,作業配列.Parameters設定(i));
            var SelectMany=作業配列.MakeGenericMethod(Reflection.ExtensionSet.SelectMany_selector,TOuter,ValueTuple2);
            var Select=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,TInner,selector_Body.Type);
            var oi=e.Expression.Parameter(selector_Body.Type,$"<UnqualifiedJoin>{this.番号++}");
            var 変換_旧Parameterを新Expression=this.変換_旧Parameterを新Expression1;
            共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,o,nameof(ValueTuple<int,int>.Item1));
            共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,i,nameof(ValueTuple<int,int>.Item2));
            if(x.UnqualifiedJoinType==UnqualifiedJoinType.OuterApply) {
                InnerSet = e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.DefaultIfEmpty,i.Type),
                    InnerSet
                );
            }
            return (
                e.Expression.Call(
                    SelectMany,
                    OuterSet,
                    e.Expression.Lambda(
                        e.Expression.Call(Select,InnerSet,selector),
                        作業配列.Parameters設定(o)
                    )
                ),
                oi
            );
            static void 共通(IDictionary<string,e.Expression?> Dictionary_DatabaseSchemaTable_ColumnExpression0,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression0,e.ParameterExpression oi0,e.ParameterExpression Parameter0,string Item){
                var 物理Expression=e.Expression.Field(oi0,Item);
                foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression0.ToList())
                    Dictionary_DatabaseSchemaTable_ColumnExpression0[KV.Key]=変換_旧Parameterを新Expression0.実行(KV.Value!,Parameter0,物理Expression);
            }
        }
        /// <summary>
        /// FROM後にJOIN区切りでテーブルを並べる
        /// SELECT *
        /// FROM A JOIN B ON A.F=B.F
        /// ↓
        /// A.Join(B,A=>A.F,B=>B.F.(A,B)=>...)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private(e.Expression Set,e.ParameterExpression Element)QualifiedJoin(QualifiedJoin x){
            var Dictionary_DatabaseSchemaTable_ColumnExpression=this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            //leftはleftが1行以上あるはず。だからSelectManyの外側に位置する
            //O.SelectMany(o=>I.Where(i=>o==i).Select(i=>new{o,i?.F1}))
            //O.SelectMany(o=>I.Where(i=>o==i).SingleOrDefault().Select(i=>new{o,i?.F1}))
            //I.SelectMany(i=>O.Where(o=>i==o).SingleOrDefault().Select(o=>new{i,o?.F1}))
            //O.Join(I,o=>o,i=>i,(o,i)o=>I.Where(i=>o==i).SingleOrDefault().Select(i=>new{o,i?.F1}))
            TableReference FirstTableReference,SecondTableReference;
            var QualifiedJoinType=x.QualifiedJoinType;
            switch(QualifiedJoinType) {
                case QualifiedJoinType.Inner:
                    FirstTableReference=x.FirstTableReference;
                    SecondTableReference=x.SecondTableReference;
                    break;
                case QualifiedJoinType.LeftOuter:
                    FirstTableReference=x.FirstTableReference;
                    SecondTableReference=x.SecondTableReference;
                    break;
                case QualifiedJoinType.RightOuter:
                    FirstTableReference=x.SecondTableReference;
                    SecondTableReference=x.FirstTableReference;
                    break;
                case QualifiedJoinType.FullOuter:
                    FirstTableReference = x.FirstTableReference;
                    SecondTableReference = x.SecondTableReference;
                    break;
                default:
                    throw new NotSupportedException(QualifiedJoinType.ToString());
            }
            var(OuterSet,o)=this.TableReference(FirstTableReference);
            var TOuter=IEnumerable1のT(OuterSet.Type);
            var(InnerSet,i)=this.TableReference(SecondTableReference);
            var TInner=IEnumerable1のT(InnerSet.Type);
            var 作業配列=this.作業配列;
            var ValueTuple2=作業配列.MakeGenericType(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
            var SearchCondition=this.BooleanExpression(x.SearchCondition);
            var(OuterPredicate,InnerPredicate,Listプローブビルド)=this.取得_OuterPredicate_InnerPredicate_プローブビルド.実行(
                SearchCondition,作業配列.Parameters設定(o),i
            );
            e.Expression プローブ,ビルド;
            var 変換_旧Parameterを新Expression=this.変換_旧Parameterを新Expression1;
            if(Listプローブビルド.Count==0){
                if(OuterPredicate is not null){
                    OuterSet=e.Expression.Call(
                        作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,o.Type),
                        OuterSet,
                        e.Expression.Lambda(OuterPredicate,作業配列.Parameters設定(o))
                    );
                }
                if(InnerPredicate is not null){
                    InnerSet=e.Expression.Call(
                        作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,i.Type),
                        InnerSet,
                        e.Expression.Lambda(InnerPredicate,作業配列.Parameters設定(i))
                    );
                }
                if(QualifiedJoinType==QualifiedJoinType.LeftOuter||QualifiedJoinType==QualifiedJoinType.RightOuter) {
                    InnerSet=e.Expression.Call(
                        作業配列.MakeGenericMethod(Reflection.ExtensionSet.DefaultIfEmpty,i.Type),
                        InnerSet
                    );
                }
                var New2=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
                var selector_Body=e.Expression.New(New2,作業配列.Expressions設定(o,i));
                var selector=e.Expression.Lambda(selector_Body,作業配列.Parameters設定(i));
                var SelectMany=作業配列.MakeGenericMethod(Reflection.ExtensionSet.SelectMany_selector,TOuter,ValueTuple2);
                var Select=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,TInner,selector_Body.Type);
                var oi=e.Expression.Parameter(selector_Body.Type,$"j1{this.番号++}");
                共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,o,nameof(ValueTuple<int,int>.Item1));
                共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,i,nameof(ValueTuple<int,int>.Item2));
                return(
                    e.Expression.Call(
                        SelectMany,
                        OuterSet,
                        e.Expression.Lambda(
                            e.Expression.Call(Select,InnerSet,selector),
                            作業配列.Parameters設定(o)
                        )
                    ),
                    oi
                );
            }else{
                if(QualifiedJoinType==QualifiedJoinType.LeftOuter||QualifiedJoinType==QualifiedJoinType.RightOuter) {
                    //nullになりえる要素変数はこれの上位ででselectしたときに要素?.列とすべきint 列→int?列になる
                    InnerSet=e.Expression.Call(
                        作業配列.MakeGenericMethod(Reflection.ExtensionSet.DefaultIfEmpty,i.Type),
                        InnerSet
                    );
                    //iが含まれている式はnullだったらスカラ値をnullにする式に置き換える

                }
                //Joinだが変換_メソッド正規化_取得インラインでSelectManyに変換される
                (プローブ,ビルド)=Listプローブビルド.Count==1?Listプローブビルド[0]:ValueTupleでNewする(作業配列,Listプローブビルド,0);
                var Join=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Join,TOuter,TInner,ビルド.Type,ValueTuple2);
                var outerKeySelector=e.Expression.Lambda(プローブ,作業配列.Parameters設定(o));
                var innerKeySelector=e.Expression.Lambda(ビルド,作業配列.Parameters設定(i));
                var New2=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
                var resultSelector_Body=e.Expression.New(New2,作業配列.Expressions設定(o,i));
                var resultSelector=e.Expression.Lambda(resultSelector_Body,作業配列.Parameters設定(o,i));
                //var oi=e.Expression.Parameter(resultSelector_Body.Type,$"<QualifiedJoin2>{this.番号++}");
                var oi=e.Expression.Parameter(resultSelector_Body.Type,$"j2{this.番号++}");
                共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,o,nameof(ValueTuple<int,int>.Item1));
                共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,i,nameof(ValueTuple<int,int>.Item2));
                if(QualifiedJoinType==QualifiedJoinType.LeftOuter||QualifiedJoinType==QualifiedJoinType.RightOuter) {
                    this.RefPeek.List_TableExpression.Add(e.Expression.Field(oi,"Item2"));
                }

                return(
                    e.Expression.Call(Join,OuterSet,InnerSet,outerKeySelector,innerKeySelector,resultSelector),
                    oi
                );
            }
            static void 共通(IDictionary<string,e.Expression?> Dictionary_DatabaseSchemaTable_ColumnExpression0,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression0,e.ParameterExpression oi0,e.ParameterExpression Parameter0,string Item){
                var 物理Expression=e.Expression.Field(oi0,Item);
                foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression0.ToList())
                    Dictionary_DatabaseSchemaTable_ColumnExpression0[KV.Key]=変換_旧Parameterを新Expression0.実行(KV.Value,Parameter0,物理Expression);
            }
        }
        /// <summary>
        /// SELECT * FROM(CUSTOMER JOIN ORDERS ON CUSTOMER.C_CUSTKEY=ORDERS.O_CUSTKEY)
        /// CUSTOMER.Join(ORDERS,CUSTOMER=>CUSTOMER.C_CUSTKEY,ORDERS=>ORDERS.O_CUSTKEY,(CUSTOMER,ORDERS)=>(CUSTOMER,ORDERS))
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private(e.Expression Set,e.ParameterExpression Element)JoinParenthesisTableReference(JoinParenthesisTableReference x)=>this.TableReference(x.Join);
        private(e.Expression Set,e.ParameterExpression Element)OdbcQualifiedJoinTableReference(OdbcQualifiedJoinTableReference x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableHint(TableHint x)=>x switch{
            IndexTableHint y=>this.IndexTableHint(y),
            LiteralTableHint y=>this.LiteralTableHint(y),
            ForceSeekTableHint y=>this.ForceSeekTableHint(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression IndexTableHint(IndexTableHint x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralTableHint(LiteralTableHint x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ForceSeekTableHint(ForceSeekTableHint x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BooleanExpression(BooleanExpression x)=>x switch{
            SubqueryComparisonPredicate y=>this.SubqueryComparisonPredicate(y),
            ExistsPredicate y=>this.ExistsPredicate(y),
            LikePredicate y=>this.LikePredicate(y),
            InPredicate y=>this.InPredicate(y),
            FullTextPredicate y=>this.FullTextPredicate(y),
            UpdateCall y=>this.UpdateCall(y),
            TSEqualCall y=>this.TSEqualCall(y),
            BooleanNotExpression y=>this.BooleanNotExpression(y),
            BooleanParenthesisExpression y=>this.BooleanParenthesisExpression(y),
            BooleanComparisonExpression y=>this.BooleanComparisonExpression(y),
            BooleanBinaryExpression y=>this.BooleanBinaryExpression(y),
            BooleanIsNullExpression y=>this.BooleanIsNullExpression(y),
            GraphMatchPredicate y=>this.GraphMatchPredicate(y),
            GraphMatchExpression y=>this.GraphMatchExpression(y),
            BooleanTernaryExpression y=>this.BooleanTernaryExpression(y),
            BooleanExpressionSnippet y=>this.BooleanExpressionSnippet(y),
            EventDeclarationCompareFunctionParameter y=>this.EventDeclarationCompareFunctionParameter(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression SubqueryComparisonPredicate(SubqueryComparisonPredicate x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// WHERE EXISTS(SELECT * FROM)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression ExistsPredicate(ExistsPredicate x){
            var StackSubquery単位の情報=this._StackSubquery単位の情報;
            StackSubquery単位の情報.Push();
            var QueryExpression=this.QueryExpression(x.Subquery.QueryExpression);//Subqueryしない理由はSingleOrDefaultしたくないから。
            StackSubquery単位の情報.Pop();
            return e.Expression.Call(
                this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Any,IEnumerable1のT(QueryExpression.Type)),
                QueryExpression
            );
        }
        private e.Expression LikePredicate(LikePredicate x){
            var FirstExpression=this.ScalarExpression(x.FirstExpression);
            var SecondExpression=this.ScalarExpression(x.SecondExpression);
            e.Expression Result=e.Expression.Call(
                e.Expression.Call(
                    Reflection.Regex.LikeからRegex,
                    SecondExpression
                ),
                Reflection.Regex.IsMatch,
                this.作業配列.Expressions設定(FirstExpression)
            );
            if(x.NotDefined){
                Result=e.Expression.Not(Result);
            }
            return Result;
        }
        /// <summary>
        /// L_SHIPMODE(select f from ...),L_SHIPMODE IN('MAIL','SHIP')
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression InPredicate(InPredicate x){
            //where x.Expresssion in x.Qubquery
            e.Expression Result;
            var ScalarExpression=this.ScalarExpression(x.Expression);
            if(x.Subquery is not null){
                //WHERE O_ORDERKEY IN(SELECT * FROM)
                Debug.Assert(x.Values is not null&&x.Values.Count==0);
                var ScalarSubquery=this.Subquery(x.Subquery);
                Result=e.Expression.Call(
                    this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Contains_value,ScalarSubquery.Type.GetGenericArguments()[0]),
                    ScalarSubquery,
                    ScalarExpression
                );
            }else{
                //WHERE O_ORDERKEY IN(1,2,3)
                var Values=x.Values;
                var Values_Count=Values.Count;
                var ScalarExpression_Type=ScalarExpression.Type;
                Result= 共通(0);
                for(var a=1;a<Values_Count;a++)Result=this.NULL_OrElse(Result,共通(a));
                e.Expression 共通(int a)=>this.Booleanを返すComparer(
                    ScalarExpression,
                    this.Convertデータ型を合わせるNullableは想定する(
                        this.ScalarExpression(Values[a]),
                        ScalarExpression_Type
                    ),
                    (Left,Right)=>e.Expression.Equal(Left,Right)
                );
            }
            if(x.NotDefined){
                return e.Expression.Not(Result);
            }
            return Result;
        }
        private e.Expression FullTextPredicate(FullTextPredicate x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UpdateCall(UpdateCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TSEqualCall(TSEqualCall x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BooleanNotExpression(BooleanNotExpression x)=>e.Expression.Not(this.BooleanExpression(x.Expression));
        private static bool Nullable比較(int? a,int? b){
            return a.Equals(b);
        }
        private e.Expression BooleanParenthesisExpression(BooleanParenthesisExpression x)=>this.BooleanExpression(x.Expression);
        private e.Expression BooleanComparisonExpression(BooleanComparisonExpression x){
            var FirstExpression0=this.ScalarExpression(x.FirstExpression);
            var SecondExpression0=this.ScalarExpression(x.SecondExpression);
            var Result=this.Booleanを返すComparer(FirstExpression0,SecondExpression0,(Left,Right)=>{
                (Left,Right)=this.Convertデータ型を合わせるNullableは想定しない(Left,Right);
                Debug.Assert(Left.Type==Right.Type);
                if(typeof(bool)==Left.Type) {
                    Left=e.Expression.Condition(Left,Constant_1,Constant_0);
                    Right=e.Expression.Condition(Right,Constant_1,Constant_0);
                }
                return x.ComparisonType switch {
                    BooleanComparisonType.Equals               =>e.Expression.Equal(Left,Right),
                    BooleanComparisonType.GreaterThan          =>e.Expression.GreaterThan(Left,Right),
                    BooleanComparisonType.LessThan             =>e.Expression.LessThan(Left,Right),
                    BooleanComparisonType.GreaterThanOrEqualTo =>e.Expression.GreaterThanOrEqual(Left,Right),
                    BooleanComparisonType.LessThanOrEqualTo    =>e.Expression.LessThanOrEqual(Left,Right),
                    BooleanComparisonType.NotEqualToBrackets   =>e.Expression.NotEqual(Left,Right),                     //Left<>Right
                    BooleanComparisonType.NotEqualToExclamation=>e.Expression.NotEqual(Left,Right),                     //Left!=Right
                    BooleanComparisonType.NotLessThan          =>e.Expression.Not(e.Expression.LessThan(Left,Right)),   //Left!<Right
                    BooleanComparisonType.NotGreaterThan       =>e.Expression.Not(e.Expression.GreaterThan(Left,Right)),//Left!>Right
                    _ => throw this.単純NotSupportedException(x.ComparisonType)
                };
            });
            return Result;
        }
        private e.Expression BooleanBinaryExpression(BooleanBinaryExpression x){
            var FirstExpression=this.BooleanExpression(x.FirstExpression);
            var SecondExpression=this.BooleanExpression(x.SecondExpression);
            return x.BinaryExpressionType switch{
                BooleanBinaryExpressionType.And=>e.Expression.AndAlso(FirstExpression,SecondExpression),
                BooleanBinaryExpressionType.Or=>e.Expression.OrElse(FirstExpression,SecondExpression),
                _=>throw this.単純NotSupportedException(x)
            };
           
        }
        private e.Expression BooleanIsNullExpression(BooleanIsNullExpression x){
            var Expression=this.ScalarExpression(x.Expression);
            return e.Expression.Equal(
                Expression,
                e.Expression.Default(Expression.Type)
            );
        }
        private e.Expression GraphMatchPredicate(GraphMatchPredicate x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GraphMatchExpression(GraphMatchExpression x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// @byte1 between 97 and 102
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression BooleanTernaryExpression(BooleanTernaryExpression x){
            var FirstExpression=this.ScalarExpression(x.FirstExpression);
            var SecondExpression=this.ScalarExpression(x.SecondExpression);
            var ThirdExpression=this.ScalarExpression(x.ThirdExpression);
            e.Expression? Predicate=null;
            if(FirstExpression.Type.IsNullable()) {
                Predicate=e.Expression.Property(FirstExpression,"HasValue");
                FirstExpression = GetValueOrDefault(FirstExpression);
            }
            if(SecondExpression.Type.IsNullable()) {
                var HasValue=e.Expression.Property(SecondExpression,"HasValue");
                Predicate=Predicate is null?HasValue:e.Expression.AndAlso(Predicate,HasValue);
                SecondExpression = GetValueOrDefault(SecondExpression);
            }
            if(ThirdExpression.Type.IsNullable()) {
                var HasValue = e.Expression.Property(ThirdExpression,"HasValue");
                Predicate=Predicate is null ? HasValue:e.Expression.AndAlso(Predicate,HasValue);
                ThirdExpression = GetValueOrDefault(ThirdExpression);
            }
            //e.Expression Between;
            //if(FirstExpression.Type.IsNullable()) {
            //    e.Expression test = e.Expression.Property(FirstExpression,"HasValue");
            //    FirstExpression = GetValueOrDefault(FirstExpression);
            //    if(SecondExpression.Type.IsNullable()) {
            //        test = e.Expression.AndAlso(test,e.Expression.Property(SecondExpression,"HasValue"));
            //        SecondExpression = GetValueOrDefault(SecondExpression);
            //    }
            //    if(ThirdExpression.Type.IsNullable()) {
            //        test = e.Expression.AndAlso(test,e.Expression.Property(ThirdExpression,"HasValue"));
            //        ThirdExpression = GetValueOrDefault(ThirdExpression);
            //    }
            //    Between = e.Expression.AndAlso(
            //        e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
            //        e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
            //    );
            //    return e.Expression.Condition(test,Between,Constant_false);
            //} else {
            //    if(SecondExpression.Type.IsNullable()) {
            //        e.Expression test = e.Expression.Property(SecondExpression,"HasValue");
            //        SecondExpression = GetValueOrDefault(SecondExpression);
            //        if(ThirdExpression.Type.IsNullable()) {
            //            test = e.Expression.AndAlso(test,e.Expression.Property(ThirdExpression,"HasValue"));
            //            ThirdExpression = GetValueOrDefault(ThirdExpression);
            //        }
            //        Between = e.Expression.AndAlso(
            //            e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
            //            e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
            //        );
            //        return e.Expression.Condition(test,Between,Constant_false);
            //    } else {
            //        if(ThirdExpression.Type.IsNullable()) {
            //            var test = e.Expression.Property(ThirdExpression,"HasValue");
            //            ThirdExpression = GetValueOrDefault(ThirdExpression);
            //            Between = e.Expression.AndAlso(
            //                e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
            //                e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
            //            );
            //            return e.Expression.Condition(test,Between,Constant_false);
            //        } else {
            //            return e.Expression.AndAlso(
            //                e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
            //                e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
            //            );
            //        }
            //    }
            //}
            var (SecondExpression0,ThirdExpression0 )=this.Convertデータ型を合わせるNullableは想定しない(SecondExpression,ThirdExpression);
            var (FirstExpression0 ,SecondExpression1)=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,SecondExpression0);
            var (FirstExpression1 ,ThirdExpression1 )=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,ThirdExpression0);
            Debug.Assert(this.ExpressionEqualityComparer.Equals(FirstExpression0, FirstExpression1));
            Debug.Assert(SecondExpression1.Type==ThirdExpression1.Type);
            Debug.Assert(FirstExpression1.Type==SecondExpression1.Type);
            FirstExpression=FirstExpression1;
            SecondExpression=SecondExpression1;
            ThirdExpression=ThirdExpression1;
            var FirstExpression_Type =FirstExpression.Type;
            //var SecondExpression_Type=SecondExpression.Type;
            //var ThirdExpression_Type=ThirdExpression.Type;
            //if(FirstExpression_Type==typeof(DateTimeOffset)){
            //    SecondExpression=this.Convertデータ型を合わせるNullableは想定しない(SecondExpression,typeof(DateTimeOffset));
            //    ThirdExpression=this.Convertデータ型を合わせるNullableは想定しない(ThirdExpression,typeof(DateTimeOffset));
            //}else if(SecondExpression_Type==typeof(DateTimeOffset)){
            //    FirstExpression=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,typeof(DateTimeOffset));
            //    ThirdExpression=this.Convertデータ型を合わせるNullableは想定しない(ThirdExpression,typeof(DateTimeOffset));
            //}else if(ThirdExpression_Type==typeof(DateTimeOffset)){
            //    FirstExpression=this.Convertデータ型を合わせるNullableは想定しない(FirstExpression,typeof(DateTimeOffset));
            //    SecondExpression=this.Convertデータ型を合わせるNullableは想定しない(SecondExpression,typeof(DateTimeOffset));
            //}
            e.Expression Between;
            if(FirstExpression_Type==typeof(string)){
                Between=e.Expression.AndAlso(
                    e.Expression.LessThanOrEqual(e.Expression.Call(Reflection.String.CompareOrdinal,SecondExpression,FirstExpression),Constant_0),
                    e.Expression.LessThanOrEqual(e.Expression.Call(Reflection.String.CompareOrdinal,FirstExpression,ThirdExpression),Constant_0)
                );
            }else if(FirstExpression_Type==typeof(DateTime)){
                Between=e.Expression.AndAlso(
                    e.Expression.Call(Reflection.DateTime.op_LessThanOrEqual,SecondExpression,FirstExpression),
                    e.Expression.Call(Reflection.DateTime.op_LessThanOrEqual,FirstExpression,ThirdExpression)
                );
            }else if(FirstExpression_Type==typeof(DateTimeOffset)){
                Between=e.Expression.AndAlso(
                    e.Expression.Call(Reflection.DateTimeOffset.op_LessThanOrEqual,SecondExpression,FirstExpression),
                    e.Expression.Call(Reflection.DateTimeOffset.op_LessThanOrEqual,FirstExpression,ThirdExpression)
                );
            }else{
                Between=e.Expression.AndAlso(
                    e.Expression.LessThanOrEqual(SecondExpression,FirstExpression),
                    e.Expression.LessThanOrEqual(FirstExpression,ThirdExpression)
                );
            }
            if(Predicate is not null)Between=e.Expression.AndAlso(Predicate,Between);
            return x.TernaryExpressionType switch{
                BooleanTernaryExpressionType.Between=>Between,
                BooleanTernaryExpressionType.NotBetween=>e.Expression.Not(Between),
                _=>throw new NotSupportedException(x.TernaryExpressionType.ToString())
            };
            //switch(x.TernaryExpressionType){
            //    case BooleanTernaryExpressionType.Between:return Between;
            //    case BooleanTernaryExpressionType.NotBetween:return e.Expression.Not(Between);
            //    default:throw new NotSupportedException(x.TernaryExpressionType.ToString());
            //}
        }
        private e.Expression BooleanExpressionSnippet(BooleanExpressionSnippet x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventDeclarationCompareFunctionParameter(EventDeclarationCompareFunctionParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ForClause(ForClause x)=>x switch{
            BrowseForClause y=>this.BrowseForClause(y),
            ReadOnlyForClause y=>this.ReadOnlyForClause(y),
            XmlForClause y=>this.XmlForClause(y),
            XmlForClauseOption y=>this.XmlForClauseOption(y),
            JsonForClause y=>this.JsonForClause(y),
            JsonForClauseOption y=>this.JsonForClauseOption(y),
            UpdateForClause y=>this.UpdateForClause(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression BrowseForClause(BrowseForClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ReadOnlyForClause(ReadOnlyForClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression XmlForClause(XmlForClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression XmlForClauseOption(XmlForClauseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression JsonForClause(JsonForClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression JsonForClauseOption(JsonForClauseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UpdateForClause(UpdateForClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OptimizerHint(OptimizerHint x)=>x switch{
            LiteralOptimizerHint y=>this.LiteralOptimizerHint(y),
            TableHintsOptimizerHint y=>this.TableHintsOptimizerHint(y),
            OptimizeForOptimizerHint y=>this.OptimizeForOptimizerHint(y),
            UseHintList y=>this.UseHintList(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression LiteralOptimizerHint(LiteralOptimizerHint x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableHintsOptimizerHint(TableHintsOptimizerHint x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OptimizeForOptimizerHint(OptimizeForOptimizerHint x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UseHintList(UseHintList x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression VariableValuePair(VariableValuePair x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WhenClause(WhenClause x)=>x switch{
            SimpleWhenClause y=>this.SimpleWhenClause(y),
            SearchedWhenClause y=>this.SearchedWhenClause(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression SimpleWhenClause(SimpleWhenClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SearchedWhenClause(SearchedWhenClause x){
            var WhenExpression=this.BooleanExpression(x.WhenExpression);
            var ThenExpression=this.ScalarExpression(x.ThenExpression);
            return e.Expression.IfThenElse(WhenExpression,ThenExpression,Default_void);
        }
        private e.Expression SchemaDeclarationItem(SchemaDeclarationItem x)=>x switch{
            SchemaDeclarationItemOpenjson y=>this.SchemaDeclarationItemOpenjson(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression SchemaDeclarationItemOpenjson(SchemaDeclarationItemOpenjson x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CallTarget(CallTarget x)=>x switch{
            ExpressionCallTarget y=>this.ExpressionCallTarget(y),
            MultiPartIdentifierCallTarget y=>this.MultiPartIdentifierCallTarget(y),
            UserDefinedTypeCallTarget y=>this.UserDefinedTypeCallTarget(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExpressionCallTarget(ExpressionCallTarget x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MultiPartIdentifierCallTarget(MultiPartIdentifierCallTarget x){
            var Identifiers=x.MultiPartIdentifier.Identifiers;
            //string Schema_Object;
            ////e.MemberExpression Schema;
            //if(Identifiers.Count==1){
            //    Schema_Object=Identifiers[0].Value;
            //    //Schema=this.List_Schema.Find(p=>p.Member.Name==Key);
            //} else {
            //    Debug.Assert(Identifiers.Count==2);
            //    //データベース.スキーマ.オブジェクトだが現状データベースを検索する方法はない
            //    //var Key0=Identifiers[0].Value;
            //    //Schema_Object=Identifiers[0].Value+'.'+Identifiers[1].Value;
            //    Schema_Object=Identifiers[1].Value;
            //    //Schema=this.List_Schema.Find(p=>p.Member.DeclaringType.Name==Key0&&p.Member.Name==Key);
            //}
            //var Schema0=this.ContainerType.GetProperty(Identifiers[Identifiers.Count-2].Value,BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly);
            var Schema_Name= Identifiers.Count switch{
                1=>Identifiers[0].Value,
                2=> Identifiers[0].Value+'.'+Identifiers[1].Value,
                _=>throw new NotSupportedException()
            };
            var Schema = this.FindSchema(Schema_Name);
            //var Schema = this.List_Schema.Find(p => string.Equals(p.Member.Name,Schema_Name,StringComparison.OrdinalIgnoreCase));
            if(Schema is not null)return Schema;
            Debug.Assert(this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression.ContainsKey(Schema_Name));
            return this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression[Schema_Name]!;
        }
        private e.Expression UserDefinedTypeCallTarget(UserDefinedTypeCallTarget x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OverClause(OverClause x){
            throw this.単純NotSupportedException(x);
        }
        private string AtomicBlockOption(AtomicBlockOption x)=>x switch{
            LiteralAtomicBlockOption y=>this.LiteralAtomicBlockOption(y),
            IdentifierAtomicBlockOption y=>this.IdentifierAtomicBlockOption(y),
            OnOffAtomicBlockOption y=>this.OnOffAtomicBlockOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        /// <summary>
        /// LANGUAGE = N'us_english'
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private string LiteralAtomicBlockOption(LiteralAtomicBlockOption x){
            return x.OptionKind switch{
                AtomicBlockOptionKind.IsolationLevel=>"IsolationLevel",
                AtomicBlockOptionKind.Language=>"Language",
                AtomicBlockOptionKind.DateFirst=>"DateFirst",
                AtomicBlockOptionKind.DateFormat=>"DateFormat",
                AtomicBlockOptionKind.DelayedDurability=>"DelayedDurability",
                _=>throw new ArgumentOutOfRangeException(x.OptionKind.ToString())
            };
        }
        /// <summary>
        /// (TRANSACTION ISOLATION LEVEL = snapshot
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private string IdentifierAtomicBlockOption(IdentifierAtomicBlockOption x){
            //AtomicBlockOptionKind.Language;
            //var Value=x.Value;
            //x.OptionKind=AtomicBlockOptionKind.IsolationLevel;
            //throw this.単純NotSupportedException(x);
            return x.OptionKind switch{
                AtomicBlockOptionKind.IsolationLevel=>"IsolationLevel",
                AtomicBlockOptionKind.Language=>"Language",
                AtomicBlockOptionKind.DateFirst=>"DateFirst",
                AtomicBlockOptionKind.DateFormat=>"DateFormat",
                AtomicBlockOptionKind.DelayedDurability=>"DelayedDurability",
                _=>throw new ArgumentOutOfRangeException(x.OptionKind.ToString())
            };
        }
        private string OnOffAtomicBlockOption(OnOffAtomicBlockOption x){
            //x.OptionState=OptionState.Off;
            return x.OptionKind switch{
                AtomicBlockOptionKind.IsolationLevel=>"IsolationLevel",
                AtomicBlockOptionKind.Language=>"Language",
                AtomicBlockOptionKind.DateFirst=>"DateFirst",
                AtomicBlockOptionKind.DateFormat=>"DateFormat",
                AtomicBlockOptionKind.DelayedDurability=>"DelayedDurability",
                _=>throw new ArgumentOutOfRangeException(x.OptionKind.ToString())
            };
        }
        private e.Expression ColumnWithSortOrder(ColumnWithSortOrder x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DeclareVariableElement(DeclareVariableElement x){
            switch(x){
                case ProcedureParameter y:return this.ProcedureParameter(y);
                default:{
                    var Type=this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x.DataType));
                    var Parameter=e.Expression.Variable(Type,x.VariableName.Value);
                    this.AddScalarVariable(Parameter);
                    //this.Block_Variables.Add(Parameter);
                    if(x.Value is null)return Default_void;
                    return e.Expression.Assign(
                        Parameter,
                        this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x.Value),Parameter.Type)
                    );
                }
            }
        }
        private e.Expression ProcedureParameter(ProcedureParameter x){
            var Type=this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x.DataType));
            //var Name=x.VariableName.Value;
            var Parameter=e.Expression.Parameter(Type,x.VariableName.Value);
            //this.Lambda_Parameters.Add(Parameter);
            Debug.Assert(x.Value is not null);
            //if(x.Value is null)return Default_void;
            //var Parameter=Expressions.Expression.Parameter(Type,Name);
            //this.Dictionary_Name_Parameter.Add(Name,Parameter);
            this.AddScalarVariable(Parameter);
            var Right=this.ScalarExpression(x.Value);
            //if(Right==Constant_null)Right=Expressions.Expression.Parameter(Type,Name);
            return e.Expression.Assign(Parameter,Right);
        }
        private e.Expression DataModificationSpecification(DataModificationSpecification x)=>x switch{
            UpdateDeleteSpecificationBase y=>this.UpdateDeleteSpecificationBase(y),
            InsertSpecification y=>this.InsertSpecification(y),
            MergeSpecification y=>this.MergeSpecification(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression UpdateDeleteSpecificationBase(UpdateDeleteSpecificationBase x)=>x switch{
            DeleteSpecification y=>this.DeleteSpecification(y),
            UpdateSpecification y=>this.UpdateSpecification(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression DeleteSpecification(DeleteSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UpdateSpecification(UpdateSpecification x){
            var(Set,Element)=this.TableReference(x.Target);
            var ctor=Element.Type.GetConstructors()[0];
            var ctor_Parameters=ctor.GetParameters();
            var Arguments_Length=ctor_Parameters.Length;
            var Arguments=new e.Expression[Arguments_Length];
            for(var a=0;a<Arguments_Length;a++)Arguments[a]=e.Expression.PropertyOrField(Element,ctor_Parameters[a].Name);
            foreach(var SetClause in x.SetClauses){
                var(ParameterName,NewValue)=this.SetClause(SetClause);
                var index=Array.FindIndex(ctor_Parameters,p=>p.Name==ParameterName);
                ref var Argument=ref Arguments[index];
                Argument=Convert必要なら(NewValue,Argument.Type);
            }
            var predicate_Body=this.WhereClause(x.WhereClause);
            var Parameters=this.作業配列.Parameters設定(Element);
            var predicate=e.Expression.Lambda(predicate_Body,Parameters);
            var set=e.Expression.Lambda(e.Expression.New(ctor,Arguments),Parameters);
            return e.Expression.Call(Set,Set.Type.GetMethod(nameof(Set<int>.UpdateWith)),predicate,set);
        }
        //private static int count;
        //private Type?InsertType;
        private (e.Expression Set, e.ParameterExpression Element) TargetTableReference(TableReference x) => x switch {
            TableReferenceWithAlias y => this.TableReferenceWithAlias(y),
            JoinTableReference y => this.JoinTableReference(y),
            JoinParenthesisTableReference y => this.JoinParenthesisTableReference(y),
            OdbcQualifiedJoinTableReference y => this.OdbcQualifiedJoinTableReference(y),
            _ => throw this.単純NotSupportedException(x)
        };
        private (e.ParameterExpression Variable, string[] Names) Insert_VariableTableReference(VariableTableReference x) {
            //ref var RefPeek = ref this.RefPeek;
            //var Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            //var Dictionary_TableAlias_ColumnAliases = RefPeek.Dictionary_TableAlias_ColumnAliases;
            //var List_アスタリスクColumnAlias = RefPeek.List_アスタリスクColumnAlias;
            //var List_アスタリスクColumnExpression = RefPeek.List_アスタリスクColumnExpression;
            var x_Variable_Name = x.Variable.Name;
            var Set = this.FindTableVariable(x.Variable.Name);
            //var Element=e.Expression.Parameter(IEnumerable1のT(Set.Type),Name);
            //var Table_Type = Set.Type;
            //var T = IEnumerable1のT(Table_Type);
            //var ctor_Parameters = T.GetConstructors()[0].GetParameters();
            //var ctor_Parameters_Length = ctor_Parameters.Length;
            //var ColumnAliases = new string[ctor_Parameters_Length];
            //var x_Alias = x.Alias;
            //string? Table = null, TableDot = null;
            //if(x_Alias is not null) {
            //    Table=x_Alias.Value;
            //    Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
            //    TableDot = Table+'.';
            //}
            var result = PrivateFindVariable(this.List_定義型TableVariable,x_Variable_Name);
            if(result is not null){
                var T = IEnumerable1のT(result.Type);
                return (result,T.GetProperties().Select(p=>p.Name).ToArray());
            }
            foreach(var 匿名型TableVariable in this.List_匿名型TableVariable)
                if(匿名型TableVariable.Variable.Name==x_Variable_Name)
                    return 匿名型TableVariable;
            throw new KeyNotFoundException($"{x_Variable_Name}が見つからなかった。");
        }
        /// <summary>
        /// InsertSpecification:DataModificationSpecification:DataModificationSpecification:TSqlFragment
        /// insert into @Student select @gtinyint + 4200000000 as tinyint
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression InsertSpecification(InsertSpecification x){
            if(x.TopRowFilter is not null)
                this.TopRowFilter(x.TopRowFilter);
            var Columns=x.Columns;
            var Columns_Count= Columns.Count;
            foreach(var Column in x.Columns) {}
            //var(Set,Element)=this.TableReference(x.Target);
            e.ParameterExpression Set;
            if(x.Target is VariableTableReference VariableTableReference) { 
                (Set, var Names1)=this.Insert_VariableTableReference(VariableTableReference);
                string[] Names;
                if(Columns_Count==0)Names=Names1;
                else{
                    Names=new string[Columns_Count];
                    for(var a = 0;a<Columns_Count;a++) {
                        var Identifiers = Columns[a].MultiPartIdentifier.Identifiers;
                        Names[a]=Identifiers[Identifiers.Count-1].Value;
                    }
                }
                //this.List_匿名型TableVariable[0].
                //ここでthis.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpressionが追加される
                if(x.InsertSource is not null) { 
                    var InsertSource=this.InsertSource(x.InsertSource);
                    //insert into Set InsertSource(select ... from ...)
                    //Set=InsertSource
                    Debug.Assert(Set.NodeType==e.ExpressionType.Parameter);
                    if(x.OutputClause is not null)this.OutputClause(x.OutputClause);
                    if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
                    var ValueTuple_Type = IEnumerable1のT(InsertSource.Type);
                    var ValueTuple_p = e.Expression.Parameter(ValueTuple_Type,"ValueTuple_p");
                    var Element_Type =IEnumerable1のT(Set.Type);
                    var Constructor = Element_Type.GetConstructors()[0];
                    var Parameters = Constructor.GetParameters();
                    var NewArguments_Length = Parameters.Length;
                    var NewArguments = new e.Expression[NewArguments_Length];
                    var 作業配列 = this.作業配列;
                    e.Expression ValueTuple = ValueTuple_p;
                    var Item番号 = 1;
                    if(Columns_Count==0) {
                        for(var a = 0;a < NewArguments_Length;a++) {
                            var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                            NewArguments[a] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[a].ParameterType);
                        }
                    } else {
                        for(var a = 0;a < Columns_Count;a++) {
                            var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                            var index = Array.FindIndex(Parameters,p => p.Name==Names[a]);
                            NewArguments[index] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[index].ParameterType);
                        }
                    }
                    return e.Expression.Call(
                        Set,
                        Set.Type.GetMethod("AddRange"),
                        e.Expression.Call(
                            作業配列.MakeGenericMethod(InsertSource.Type.IsArray ? Reflection.ExtensionEnumerable.Select_selector:Reflection.ExtensionSet.Select_selector,ValueTuple_Type,Element_Type),
                            InsertSource,
                            e.Expression.Lambda(
                                e.Expression.New(Constructor,NewArguments),
                                作業配列.Parameters設定(ValueTuple_p)

                            )
                        )
                    );
                } else {
                    throw this.単純NotSupportedException(x);
                }
            }
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MergeSpecification(MergeSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression Permission(Permission x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityTargetObject(SecurityTargetObject x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityTargetObjectName(SecurityTargetObjectName x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityPrincipal(SecurityPrincipal x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityElement80(SecurityElement80 x)=>x switch{
            CommandSecurityElement80 y=>this.CommandSecurityElement80(y),
            PrivilegeSecurityElement80 y=>this.PrivilegeSecurityElement80(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression CommandSecurityElement80(CommandSecurityElement80 x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PrivilegeSecurityElement80(PrivilegeSecurityElement80 x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression Privilege80(Privilege80 x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityUserClause80(SecurityUserClause80 x){
            throw this.単純NotSupportedException(x);
        }
        private(string ParameterName,e.Expression NewValue)SetClause(SetClause x)=>x switch{
            AssignmentSetClause y=>this.AssignmentSetClause(y),
            FunctionCallSetClause y=>this.FunctionCallSetClause(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private(string ParameterName,e.Expression NewValue)AssignmentSetClause(AssignmentSetClause x){
            //x.AssignmentKind=AssignmentKind.
            var Identifiers=x.Column.MultiPartIdentifier.Identifiers;
            var ParameterName=this.Identifier(Identifiers[Identifiers.Count-1]);
            //var Column=this.ColumnReferenceExpression(x.Column.MultiPartIdentifier.Identifiers);
            Debug.Assert(x.Variable is null);
            //var Variable=this.VariableReference(x.Variable);
            var NewValue=this.ScalarExpression(x.NewValue);
            return(ParameterName,NewValue);
        }
        private(string ParameterName,e.Expression NewValue)FunctionCallSetClause(FunctionCallSetClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression InsertSource(InsertSource x)=>x switch{
            ValuesInsertSource y=>this.ValuesInsertSource(y),
            SelectInsertSource y=>this.SelectInsertSource(y),
            ExecuteInsertSource y=>this.ExecuteInsertSource(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ValuesInsertSource(ValuesInsertSource x){
            var 作業配列= this.作業配列;
            var RowValues=x.RowValues;
            var RowValues_Count = RowValues.Count;
            var ColumnValues_Count= RowValues[0].ColumnValues.Count;
            var Arguments0=new e.Expression[ColumnValues_Count];
            var ValueTuples=new e.NewExpression[RowValues_Count];
            var ColumnValues0 = RowValues[0].ColumnValues;
            Debug.Assert(ColumnValues_Count == ColumnValues0.Count);
            for(var b = 0;b < ColumnValues_Count;b++)
                Arguments0[b] = this.ScalarExpression(ColumnValues0[b]);
            var ValueTuple0= CommonLibrary.ValueTupleでNewする(作業配列,Arguments0);
            ValueTuples[0] = ValueTuple0;
            for(var a=1;a < RowValues_Count;a++) { 
                var ColumnValues=RowValues[a].ColumnValues;
                Debug.Assert(ColumnValues_Count== ColumnValues.Count);
                for(var b=0;b<ColumnValues_Count;b++)
                    Arguments0[b]=this.ScalarExpression(ColumnValues[b]);
                ValueTuples[a]= CommonLibrary.ValueTupleでNewする(作業配列,Arguments0);
            }
            return e.Expression.NewArrayInit(ValueTuple0.Type,ValueTuples);
        }
        private e.Expression SelectInsertSource(SelectInsertSource x){
            return this.QueryExpression(x.Select);
        }
        private e.Expression ExecuteInsertSource(ExecuteInsertSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RowValue(RowValue x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralRange(LiteralRange x)=>x switch{
            ProcessAffinityRange y=>this.ProcessAffinityRange(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ProcessAffinityRange(ProcessAffinityRange x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OptionValue(OptionValue x)=>x switch{
            OnOffOptionValue y=>this.OnOffOptionValue(y),
            LiteralOptionValue y=>this.LiteralOptionValue(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OnOffOptionValue(OnOffOptionValue x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralOptionValue(LiteralOptionValue x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IdentifierOrScalarExpression(IdentifierOrScalarExpression x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SchemaObjectNameOrValueExpression(SchemaObjectNameOrValueExpression x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SequenceOption(SequenceOption x)=>x switch{
            DataTypeSequenceOption y=>this.DataTypeSequenceOption(y),
            ScalarExpressionSequenceOption y=>this.ScalarExpressionSequenceOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression DataTypeSequenceOption(DataTypeSequenceOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ScalarExpressionSequenceOption(ScalarExpressionSequenceOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityPredicateAction(SecurityPredicateAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecurityPolicyOption(SecurityPolicyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnMasterKeyParameter(ColumnMasterKeyParameter x)=>x switch{
            ColumnMasterKeyStoreProviderNameParameter y=>this.ColumnMasterKeyStoreProviderNameParameter(y),
            ColumnMasterKeyPathParameter y=>this.ColumnMasterKeyPathParameter(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ColumnMasterKeyStoreProviderNameParameter(ColumnMasterKeyStoreProviderNameParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnMasterKeyPathParameter(ColumnMasterKeyPathParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnEncryptionKeyValue(ColumnEncryptionKeyValue x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnEncryptionKeyValueParameter(ColumnEncryptionKeyValueParameter x)=>x switch{
            ColumnMasterKeyNameParameter y=>this.ColumnMasterKeyNameParameter(y),
            ColumnEncryptionAlgorithmNameParameter y=>this.ColumnEncryptionAlgorithmNameParameter(y),
            EncryptedValueParameter y=>this.EncryptedValueParameter(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ColumnMasterKeyNameParameter(ColumnMasterKeyNameParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnEncryptionAlgorithmNameParameter(ColumnEncryptionAlgorithmNameParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EncryptedValueParameter(EncryptedValueParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableOption(ExternalTableOption x)=>x switch{
            ExternalTableLiteralOrIdentifierOption y=>this.ExternalTableLiteralOrIdentifierOption(y),
            ExternalTableDistributionOption y=>this.ExternalTableDistributionOption(y),
            ExternalTableRejectTypeOption y=>this.ExternalTableRejectTypeOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExternalTableLiteralOrIdentifierOption(ExternalTableLiteralOrIdentifierOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableDistributionOption(ExternalTableDistributionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableRejectTypeOption(ExternalTableRejectTypeOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableDistributionPolicy(ExternalTableDistributionPolicy x)=>x switch{
            ExternalTableReplicatedDistributionPolicy y=>this.ExternalTableReplicatedDistributionPolicy(y),
            ExternalTableRoundRobinDistributionPolicy y=>this.ExternalTableRoundRobinDistributionPolicy(y),
            ExternalTableShardedDistributionPolicy y=>this.ExternalTableShardedDistributionPolicy(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExternalTableReplicatedDistributionPolicy(ExternalTableReplicatedDistributionPolicy x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableRoundRobinDistributionPolicy(ExternalTableRoundRobinDistributionPolicy x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableShardedDistributionPolicy(ExternalTableShardedDistributionPolicy x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalDataSourceOption(ExternalDataSourceOption x)=>x switch{
            ExternalDataSourceLiteralOrIdentifierOption y=>this.ExternalDataSourceLiteralOrIdentifierOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExternalDataSourceLiteralOrIdentifierOption(ExternalDataSourceLiteralOrIdentifierOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalFileFormatOption(ExternalFileFormatOption x)=>x switch{
            ExternalFileFormatLiteralOption y=>this.ExternalFileFormatLiteralOption(y),
            ExternalFileFormatUseDefaultTypeOption y=>this.ExternalFileFormatUseDefaultTypeOption(y),
            ExternalFileFormatContainerOption y=>this.ExternalFileFormatContainerOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExternalFileFormatLiteralOption(ExternalFileFormatLiteralOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalFileFormatUseDefaultTypeOption(ExternalFileFormatUseDefaultTypeOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalFileFormatContainerOption(ExternalFileFormatContainerOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AssemblyOption(AssemblyOption x)=>x switch{
            OnOffAssemblyOption y=>this.OnOffAssemblyOption(y),
            PermissionSetAssemblyOption y=>this.PermissionSetAssemblyOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OnOffAssemblyOption(OnOffAssemblyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PermissionSetAssemblyOption(PermissionSetAssemblyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AddFileSpec(AddFileSpec x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AssemblyName(AssemblyName x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableOption(TableOption x)=>x switch{
            LockEscalationTableOption y=>this.LockEscalationTableOption(y),
            FileStreamOnTableOption y=>this.FileStreamOnTableOption(y),
            FileTableDirectoryTableOption y=>this.FileTableDirectoryTableOption(y),
            FileTableCollateFileNameTableOption y=>this.FileTableCollateFileNameTableOption(y),
            FileTableConstraintNameTableOption y=>this.FileTableConstraintNameTableOption(y),
            MemoryOptimizedTableOption y=>this.MemoryOptimizedTableOption(y),
            DurabilityTableOption y=>this.DurabilityTableOption(y),
            RemoteDataArchiveTableOption y=>this.RemoteDataArchiveTableOption(y),
            RemoteDataArchiveAlterTableOption y=>this.RemoteDataArchiveAlterTableOption(y),
            SystemVersioningTableOption y=>this.SystemVersioningTableOption(y),
            TableDataCompressionOption y=>this.TableDataCompressionOption(y),
            TableDistributionOption y=>this.TableDistributionOption(y),
            TableIndexOption y=>this.TableIndexOption(y),
            TablePartitionOption y=>this.TablePartitionOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression LockEscalationTableOption(LockEscalationTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileStreamOnTableOption(FileStreamOnTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileTableDirectoryTableOption(FileTableDirectoryTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileTableCollateFileNameTableOption(FileTableCollateFileNameTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileTableConstraintNameTableOption(FileTableConstraintNameTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MemoryOptimizedTableOption(MemoryOptimizedTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DurabilityTableOption(DurabilityTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RemoteDataArchiveTableOption(RemoteDataArchiveTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RemoteDataArchiveAlterTableOption(RemoteDataArchiveAlterTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SystemVersioningTableOption(SystemVersioningTableOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableDataCompressionOption(TableDataCompressionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableDistributionOption(TableDistributionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableIndexOption(TableIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TablePartitionOption(TablePartitionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DatabaseOption(DatabaseOption x)=>x switch{
            RemoteDataArchiveDatabaseOption y=>this.RemoteDataArchiveDatabaseOption(y),
            OnOffDatabaseOption y=>this.OnOffDatabaseOption(y),
            ContainmentDatabaseOption y=>this.ContainmentDatabaseOption(y),
            HadrDatabaseOption y=>this.HadrDatabaseOption(y),
            DelayedDurabilityDatabaseOption y=>this.DelayedDurabilityDatabaseOption(y),
            CursorDefaultDatabaseOption y=>this.CursorDefaultDatabaseOption(y),
            RecoveryDatabaseOption y=>this.RecoveryDatabaseOption(y),
            TargetRecoveryTimeDatabaseOption y=>this.TargetRecoveryTimeDatabaseOption(y),
            PageVerifyDatabaseOption y=>this.PageVerifyDatabaseOption(y),
            PartnerDatabaseOption y=>this.PartnerDatabaseOption(y),
            WitnessDatabaseOption y=>this.WitnessDatabaseOption(y),
            ParameterizationDatabaseOption y=>this.ParameterizationDatabaseOption(y),
            LiteralDatabaseOption y=>this.LiteralDatabaseOption(y),
            IdentifierDatabaseOption y=>this.IdentifierDatabaseOption(y),
            ChangeTrackingDatabaseOption y=>this.ChangeTrackingDatabaseOption(y),
            QueryStoreDatabaseOption y=>this.QueryStoreDatabaseOption(y),
            AutomaticTuningDatabaseOption y=>this.AutomaticTuningDatabaseOption(y),
            FileStreamDatabaseOption y=>this.FileStreamDatabaseOption(y),
            CatalogCollationOption y=>this.CatalogCollationOption(y),
            MaxSizeDatabaseOption y=>this.MaxSizeDatabaseOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression RemoteDataArchiveDatabaseOption(RemoteDataArchiveDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OnOffDatabaseOption(OnOffDatabaseOption x)=>x switch{
            AutoCreateStatisticsDatabaseOption y=>this.AutoCreateStatisticsDatabaseOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AutoCreateStatisticsDatabaseOption(AutoCreateStatisticsDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ContainmentDatabaseOption(ContainmentDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression HadrDatabaseOption(HadrDatabaseOption x)=>x switch{
            HadrAvailabilityGroupDatabaseOption y=>this.HadrAvailabilityGroupDatabaseOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression HadrAvailabilityGroupDatabaseOption(HadrAvailabilityGroupDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DelayedDurabilityDatabaseOption(DelayedDurabilityDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CursorDefaultDatabaseOption(CursorDefaultDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RecoveryDatabaseOption(RecoveryDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TargetRecoveryTimeDatabaseOption(TargetRecoveryTimeDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PageVerifyDatabaseOption(PageVerifyDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PartnerDatabaseOption(PartnerDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WitnessDatabaseOption(WitnessDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ParameterizationDatabaseOption(ParameterizationDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralDatabaseOption(LiteralDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IdentifierDatabaseOption(IdentifierDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ChangeTrackingDatabaseOption(ChangeTrackingDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreDatabaseOption(QueryStoreDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AutomaticTuningDatabaseOption(AutomaticTuningDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileStreamDatabaseOption(FileStreamDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CatalogCollationOption(CatalogCollationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MaxSizeDatabaseOption(MaxSizeDatabaseOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RemoteDataArchiveDatabaseSetting(RemoteDataArchiveDatabaseSetting x)=>x switch{
            RemoteDataArchiveDbServerSetting y=>this.RemoteDataArchiveDbServerSetting(y),
            RemoteDataArchiveDbCredentialSetting y=>this.RemoteDataArchiveDbCredentialSetting(y),
            RemoteDataArchiveDbFederatedServiceAccountSetting y=>this.RemoteDataArchiveDbFederatedServiceAccountSetting(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression RemoteDataArchiveDbServerSetting(RemoteDataArchiveDbServerSetting x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RemoteDataArchiveDbCredentialSetting(RemoteDataArchiveDbCredentialSetting x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RemoteDataArchiveDbFederatedServiceAccountSetting(RemoteDataArchiveDbFederatedServiceAccountSetting x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RetentionPeriodDefinition(RetentionPeriodDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableSwitchOption(TableSwitchOption x)=>x switch{
            LowPriorityLockWaitTableSwitchOption y=>this.LowPriorityLockWaitTableSwitchOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression LowPriorityLockWaitTableSwitchOption(LowPriorityLockWaitTableSwitchOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropClusteredConstraintOption(DropClusteredConstraintOption x)=>x switch{
            DropClusteredConstraintStateOption y=>this.DropClusteredConstraintStateOption(y),
            DropClusteredConstraintValueOption y=>this.DropClusteredConstraintValueOption(y),
            DropClusteredConstraintMoveOption y=>this.DropClusteredConstraintMoveOption(y),
            DropClusteredConstraintWaitAtLowPriorityLockOption y=>this.DropClusteredConstraintWaitAtLowPriorityLockOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression DropClusteredConstraintStateOption(DropClusteredConstraintStateOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropClusteredConstraintValueOption(DropClusteredConstraintValueOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropClusteredConstraintMoveOption(DropClusteredConstraintMoveOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropClusteredConstraintWaitAtLowPriorityLockOption(DropClusteredConstraintWaitAtLowPriorityLockOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterTableDropTableElement(AlterTableDropTableElement x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExecuteAsClause(ExecuteAsClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueueOption(QueueOption x)=>x switch{
            QueueStateOption y=>this.QueueStateOption(y),
            QueueProcedureOption y=>this.QueueProcedureOption(y),
            QueueValueOption y=>this.QueueValueOption(y),
            QueueExecuteAsOption y=>this.QueueExecuteAsOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression QueueStateOption(QueueStateOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueueProcedureOption(QueueProcedureOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueueValueOption(QueueValueOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueueExecuteAsOption(QueueExecuteAsOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RouteOption(RouteOption x){
            throw this.単純NotSupportedException(x);
        }
        private void SystemTimePeriodDefinition(SystemTimePeriodDefinition x){
            if(x.EndTimeColumn is not null)this.Identifier(x.EndTimeColumn);
            if(x.StartTimeColumn is not null)this.Identifier(x.StartTimeColumn);
        }
        private e.Expression IndexType(IndexType x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PartitionSpecifier(PartitionSpecifier x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileGroupOrPartitionScheme(FileGroupOrPartitionScheme x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IndexOption(IndexOption x)=>x switch{
            IndexStateOption y=>this.IndexStateOption(y),
            IndexExpressionOption y=>this.IndexExpressionOption(y),
            MaxDurationOption y=>this.MaxDurationOption(y),
            WaitAtLowPriorityOption y=>this.WaitAtLowPriorityOption(y),
            OrderIndexOption y=>this.OrderIndexOption(y),
            MoveToDropIndexOption y=>this.MoveToDropIndexOption(y),
            FileStreamOnDropIndexOption y=>this.FileStreamOnDropIndexOption(y),
            DataCompressionOption y=>this.DataCompressionOption(y),
            CompressionDelayIndexOption y=>this.CompressionDelayIndexOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression IndexStateOption(IndexStateOption x)=>x switch{
            OnlineIndexOption y=>this.OnlineIndexOption(y),
            IgnoreDupKeyIndexOption y=>this.IgnoreDupKeyIndexOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OnlineIndexOption(OnlineIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IgnoreDupKeyIndexOption(IgnoreDupKeyIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IndexExpressionOption(IndexExpressionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MaxDurationOption(MaxDurationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WaitAtLowPriorityOption(WaitAtLowPriorityOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OrderIndexOption(OrderIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MoveToDropIndexOption(MoveToDropIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileStreamOnDropIndexOption(FileStreamOnDropIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DataCompressionOption(DataCompressionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CompressionDelayIndexOption(CompressionDelayIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OnlineIndexLowPriorityLockWaitOption(OnlineIndexLowPriorityLockWaitOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LowPriorityLockWaitOption(LowPriorityLockWaitOption x)=>x switch{
            LowPriorityLockWaitMaxDurationOption y=>this.LowPriorityLockWaitMaxDurationOption(y),
            LowPriorityLockWaitAbortAfterWaitOption y=>this.LowPriorityLockWaitAbortAfterWaitOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression LowPriorityLockWaitMaxDurationOption(LowPriorityLockWaitMaxDurationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LowPriorityLockWaitAbortAfterWaitOption(LowPriorityLockWaitAbortAfterWaitOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FullTextIndexColumn(FullTextIndexColumn x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FullTextIndexOption(FullTextIndexOption x)=>x switch{
            ChangeTrackingFullTextIndexOption y=>this.ChangeTrackingFullTextIndexOption(y),
            StopListFullTextIndexOption y=>this.StopListFullTextIndexOption(y),
            SearchPropertyListFullTextIndexOption y=>this.SearchPropertyListFullTextIndexOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ChangeTrackingFullTextIndexOption(ChangeTrackingFullTextIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression StopListFullTextIndexOption(StopListFullTextIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SearchPropertyListFullTextIndexOption(SearchPropertyListFullTextIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FullTextCatalogAndFileGroup(FullTextCatalogAndFileGroup x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventTypeGroupContainer(EventTypeGroupContainer x)=>x switch{
            EventTypeContainer y=>this.EventTypeContainer(y),
            EventGroupContainer y=>this.EventGroupContainer(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression EventTypeContainer(EventTypeContainer x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventGroupContainer(EventGroupContainer x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventNotificationObjectScope(EventNotificationObjectScope x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ApplicationRoleOption(ApplicationRoleOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterRoleAction(AlterRoleAction x)=>x switch{
            RenameAlterRoleAction y=>this.RenameAlterRoleAction(y),
            AddMemberAlterRoleAction y=>this.AddMemberAlterRoleAction(y),
            DropMemberAlterRoleAction y=>this.DropMemberAlterRoleAction(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression RenameAlterRoleAction(RenameAlterRoleAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AddMemberAlterRoleAction(AddMemberAlterRoleAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropMemberAlterRoleAction(DropMemberAlterRoleAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UserLoginOption(UserLoginOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression StatisticsOption(StatisticsOption x)=>x switch{
            ResampleStatisticsOption y=>this.ResampleStatisticsOption(y),
            OnOffStatisticsOption y=>this.OnOffStatisticsOption(y),
            LiteralStatisticsOption y=>this.LiteralStatisticsOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ResampleStatisticsOption(ResampleStatisticsOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OnOffStatisticsOption(OnOffStatisticsOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralStatisticsOption(LiteralStatisticsOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression StatisticsPartitionRange(StatisticsPartitionRange x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CursorDefinition(CursorDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CursorOption(CursorOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CursorId(CursorId x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CryptoMechanism(CryptoMechanism x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FetchType(FetchType x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WhereClause(WhereClause x)=>this.BooleanExpression(x.SearchCondition);
        private e.Expression DropIndexClauseBase(DropIndexClauseBase x)=>x switch{
            BackwardsCompatibleDropIndexClause y=>this.BackwardsCompatibleDropIndexClause(y),
            DropIndexClause y=>this.DropIndexClause(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression BackwardsCompatibleDropIndexClause(BackwardsCompatibleDropIndexClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropIndexClause(DropIndexClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetCommand(SetCommand x)=>x switch{
            GeneralSetCommand y=>this.GeneralSetCommand(y),
            SetFipsFlaggerCommand y=>this.SetFipsFlaggerCommand(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression GeneralSetCommand(GeneralSetCommand x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetFipsFlaggerCommand(SetFipsFlaggerCommand x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileDeclaration(FileDeclaration x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileDeclarationOption(FileDeclarationOption x)=>x switch{
            NameFileDeclarationOption y=>this.NameFileDeclarationOption(y),
            FileNameFileDeclarationOption y=>this.FileNameFileDeclarationOption(y),
            SizeFileDeclarationOption y=>this.SizeFileDeclarationOption(y),
            MaxSizeFileDeclarationOption y=>this.MaxSizeFileDeclarationOption(y),
            FileGrowthFileDeclarationOption y=>this.FileGrowthFileDeclarationOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression NameFileDeclarationOption(NameFileDeclarationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileNameFileDeclarationOption(FileNameFileDeclarationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SizeFileDeclarationOption(SizeFileDeclarationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MaxSizeFileDeclarationOption(MaxSizeFileDeclarationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileGrowthFileDeclarationOption(FileGrowthFileDeclarationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileGroupDefinition(FileGroupDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DatabaseConfigurationClearOption(DatabaseConfigurationClearOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DatabaseConfigurationSetOption(DatabaseConfigurationSetOption x)=>x switch{
            OnOffPrimaryConfigurationOption y=>this.OnOffPrimaryConfigurationOption(y),
            MaxDopConfigurationOption y=>this.MaxDopConfigurationOption(y),
            GenericConfigurationOption y=>this.GenericConfigurationOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OnOffPrimaryConfigurationOption(OnOffPrimaryConfigurationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MaxDopConfigurationOption(MaxDopConfigurationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GenericConfigurationOption(GenericConfigurationOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterDatabaseTermination(AlterDatabaseTermination x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ChangeTrackingOptionDetail(ChangeTrackingOptionDetail x)=>x switch{
            AutoCleanupChangeTrackingOptionDetail y=>this.AutoCleanupChangeTrackingOptionDetail(y),
            ChangeRetentionChangeTrackingOptionDetail y=>this.ChangeRetentionChangeTrackingOptionDetail(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AutoCleanupChangeTrackingOptionDetail(AutoCleanupChangeTrackingOptionDetail x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ChangeRetentionChangeTrackingOptionDetail(ChangeRetentionChangeTrackingOptionDetail x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreOption(QueryStoreOption x)=>x switch{
            QueryStoreDesiredStateOption y=>this.QueryStoreDesiredStateOption(y),
            QueryStoreCapturePolicyOption y=>this.QueryStoreCapturePolicyOption(y),
            QueryStoreSizeCleanupPolicyOption y=>this.QueryStoreSizeCleanupPolicyOption(y),
            QueryStoreDataFlushIntervalOption y=>this.QueryStoreDataFlushIntervalOption(y),
            QueryStoreIntervalLengthOption y=>this.QueryStoreIntervalLengthOption(y),
            QueryStoreMaxStorageSizeOption y=>this.QueryStoreMaxStorageSizeOption(y),
            QueryStoreMaxPlansPerQueryOption y=>this.QueryStoreMaxPlansPerQueryOption(y),
            QueryStoreTimeCleanupPolicyOption y=>this.QueryStoreTimeCleanupPolicyOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression QueryStoreDesiredStateOption(QueryStoreDesiredStateOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreCapturePolicyOption(QueryStoreCapturePolicyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreSizeCleanupPolicyOption(QueryStoreSizeCleanupPolicyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreDataFlushIntervalOption(QueryStoreDataFlushIntervalOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreIntervalLengthOption(QueryStoreIntervalLengthOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreMaxStorageSizeOption(QueryStoreMaxStorageSizeOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreMaxPlansPerQueryOption(QueryStoreMaxPlansPerQueryOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryStoreTimeCleanupPolicyOption(QueryStoreTimeCleanupPolicyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AutomaticTuningOption(AutomaticTuningOption x)=>x switch{
            AutomaticTuningForceLastGoodPlanOption y=>this.AutomaticTuningForceLastGoodPlanOption(y),
            AutomaticTuningCreateIndexOption y=>this.AutomaticTuningCreateIndexOption(y),
            AutomaticTuningDropIndexOption y=>this.AutomaticTuningDropIndexOption(y),
            AutomaticTuningMaintainIndexOption y=>this.AutomaticTuningMaintainIndexOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AutomaticTuningForceLastGoodPlanOption(AutomaticTuningForceLastGoodPlanOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AutomaticTuningCreateIndexOption(AutomaticTuningCreateIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AutomaticTuningDropIndexOption(AutomaticTuningDropIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AutomaticTuningMaintainIndexOption(AutomaticTuningMaintainIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        //private (Type Type, string Name) ColumnDefinitionBase(ColumnDefinitionBase x){
        //    switch(x){
        //        case ColumnDefinition y:return this.ColumnDefinition(y);
        //        default:throw this.単純NotSupportedException(x);
        //    }
        //}
        /// <summary>
        /// TABLE型の列をValueTupleで表現する
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Type ColumnDefinition(ColumnDefinition x){
            return this.DataTypeReference(x.DataType);
            //if(x.Encryption is not null)this.ColumnEncryptionDefinition(x.Encryption);
            //if(x.Collation is not null)this.Identifier(x.Collation);
            //var ColumnIdentifier=this.Identifier(x.ColumnIdentifier);
            //if(x.ComputedColumnExpression is not null)this.ScalarExpression(x.ComputedColumnExpression);
            //foreach(var Constraint in x.Constraints)
            //    this.ConstraintDefinition(Constraint);
            //if(x.DefaultConstraint is not null)this.DefaultConstraintDefinition(x.DefaultConstraint);
            //var GeneratedAlways=x.GeneratedAlways;
            //if(x.IdentityOptions is not null)this.IdentityOptions(x.IdentityOptions);
            //if(x.Index is not null)this.IndexDefinition(x.Index);
            //var IsMasked=x.IsMasked;
            //var IsHidden=x.IsHidden;
            //var IsPersisted=x.IsPersisted;
            //var IsRowGuidCol=x.IsRowGuidCol;
            //if(x.MaskingFunction is not null)this.StringLiteral(x.MaskingFunction);
            //if(x.StorageOptions is not null)this.ColumnStorageOptions(x.StorageOptions);
            //return typeof(int);
        }
        private e.Expression ColumnEncryptionDefinition(ColumnEncryptionDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnEncryptionDefinitionParameter(ColumnEncryptionDefinitionParameter x)=>x switch{
            ColumnEncryptionKeyNameParameter y=>this.ColumnEncryptionKeyNameParameter(y),
            ColumnEncryptionTypeParameter y=>this.ColumnEncryptionTypeParameter(y),
            ColumnEncryptionAlgorithmParameter y=>this.ColumnEncryptionAlgorithmParameter(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ColumnEncryptionKeyNameParameter(ColumnEncryptionKeyNameParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnEncryptionTypeParameter(ColumnEncryptionTypeParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnEncryptionAlgorithmParameter(ColumnEncryptionAlgorithmParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IdentityOptions(IdentityOptions x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ColumnStorageOptions(ColumnStorageOptions x){
            throw this.単純NotSupportedException(x);
        }
        private void ConstraintDefinition(ConstraintDefinition x){
            switch(x){
                case CheckConstraintDefinition y:this.CheckConstraintDefinition(y);break;
                case DefaultConstraintDefinition y:this.DefaultConstraintDefinition(y);break;
                case ForeignKeyConstraintDefinition y:this.ForeignKeyConstraintDefinition(y);break;
                case NullableConstraintDefinition y:this.NullableConstraintDefinition(y);break;
                case GraphConnectionConstraintDefinition y:this.GraphConnectionConstraintDefinition(y);break;
                case UniqueConstraintDefinition y:this.UniqueConstraintDefinition(y);break;
                default:throw this.単純NotSupportedException(x);
            }
        }
        private e.Expression CheckConstraintDefinition(CheckConstraintDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DefaultConstraintDefinition(DefaultConstraintDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ForeignKeyConstraintDefinition(ForeignKeyConstraintDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression NullableConstraintDefinition(NullableConstraintDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GraphConnectionConstraintDefinition(GraphConnectionConstraintDefinition x){
            throw this.単純NotSupportedException(x);
        }
        /// <summary>
        /// primary keyとか
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private void UniqueConstraintDefinition(UniqueConstraintDefinition x){
            foreach(var IndexOption in x.IndexOptions)
                this.IndexOption(IndexOption);
            var IndexType=x.IndexType;
            var IsEnforced = x.IsEnforced;
            var Clustered = x.Clustered;
            var IsPrimaryKey = x.IsPrimaryKey;
            foreach(var Column in x.Columns)
                this.ColumnWithSortOrder(Column);
        }
        private e.Expression FederationScheme(FederationScheme x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableDistributionPolicy(TableDistributionPolicy x)=>x switch{
            TableReplicateDistributionPolicy y=>this.TableReplicateDistributionPolicy(y),
            TableRoundRobinDistributionPolicy y=>this.TableRoundRobinDistributionPolicy(y),
            TableHashDistributionPolicy y=>this.TableHashDistributionPolicy(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression TableReplicateDistributionPolicy(TableReplicateDistributionPolicy x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableRoundRobinDistributionPolicy(TableRoundRobinDistributionPolicy x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableHashDistributionPolicy(TableHashDistributionPolicy x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableIndexType(TableIndexType x)=>x switch{
            TableClusteredIndexType y=>this.TableClusteredIndexType(y),
            TableNonClusteredIndexType y=>this.TableNonClusteredIndexType(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression TableClusteredIndexType(TableClusteredIndexType x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableNonClusteredIndexType(TableNonClusteredIndexType x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PartitionSpecifications(PartitionSpecifications x)=>x switch{
            TablePartitionOptionSpecifications y=>this.TablePartitionOptionSpecifications(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression TablePartitionOptionSpecifications(TablePartitionOptionSpecifications x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CompressionPartitionRange(CompressionPartitionRange x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GraphConnectionBetweenNodes(GraphConnectionBetweenNodes x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RestoreOption(RestoreOption x)=>x switch{
            ScalarExpressionRestoreOption y=>this.ScalarExpressionRestoreOption(y),
            MoveRestoreOption y=>this.MoveRestoreOption(y),
            StopRestoreOption y=>this.StopRestoreOption(y),
            FileStreamRestoreOption y=>this.FileStreamRestoreOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ScalarExpressionRestoreOption(ScalarExpressionRestoreOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MoveRestoreOption(MoveRestoreOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression StopRestoreOption(StopRestoreOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileStreamRestoreOption(FileStreamRestoreOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BackupOption(BackupOption x)=>x switch{
            BackupEncryptionOption y=>this.BackupEncryptionOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression BackupEncryptionOption(BackupEncryptionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DeviceInfo(DeviceInfo x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MirrorToClause(MirrorToClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BackupRestoreFileInfo(BackupRestoreFileInfo x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BulkInsertOption(BulkInsertOption x)=>x switch{
            LiteralBulkInsertOption y=>this.LiteralBulkInsertOption(y),
            OrderBulkInsertOption y=>this.OrderBulkInsertOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression LiteralBulkInsertOption(LiteralBulkInsertOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OrderBulkInsertOption(OrderBulkInsertOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalTableColumnDefinition(ExternalTableColumnDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression InsertBulkColumnDefinition(InsertBulkColumnDefinition x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DbccOption(DbccOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DbccNamedLiteral(DbccNamedLiteral x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PartitionParameterType(PartitionParameterType x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RemoteServiceBindingOption(RemoteServiceBindingOption x)=>x switch{
            OnOffRemoteServiceBindingOption y=>this.OnOffRemoteServiceBindingOption(y),
            UserRemoteServiceBindingOption y=>this.UserRemoteServiceBindingOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OnOffRemoteServiceBindingOption(OnOffRemoteServiceBindingOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression UserRemoteServiceBindingOption(UserRemoteServiceBindingOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EncryptionSource(EncryptionSource x)=>x switch{
            AssemblyEncryptionSource y=>this.AssemblyEncryptionSource(y),
            FileEncryptionSource y=>this.FileEncryptionSource(y),
            ProviderEncryptionSource y=>this.ProviderEncryptionSource(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AssemblyEncryptionSource(AssemblyEncryptionSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FileEncryptionSource(FileEncryptionSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ProviderEncryptionSource(ProviderEncryptionSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CertificateOption(CertificateOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ContractMessage(ContractMessage x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EndpointAffinity(EndpointAffinity x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EndpointProtocolOption(EndpointProtocolOption x)=>x switch{
            LiteralEndpointProtocolOption y=>this.LiteralEndpointProtocolOption(y),
            AuthenticationEndpointProtocolOption y=>this.AuthenticationEndpointProtocolOption(y),
            PortsEndpointProtocolOption y=>this.PortsEndpointProtocolOption(y),
            CompressionEndpointProtocolOption y=>this.CompressionEndpointProtocolOption(y),
            ListenerIPEndpointProtocolOption y=>this.ListenerIPEndpointProtocolOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression LiteralEndpointProtocolOption(LiteralEndpointProtocolOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuthenticationEndpointProtocolOption(AuthenticationEndpointProtocolOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PortsEndpointProtocolOption(PortsEndpointProtocolOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CompressionEndpointProtocolOption(CompressionEndpointProtocolOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ListenerIPEndpointProtocolOption(ListenerIPEndpointProtocolOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IPv4(IPv4 x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PayloadOption(PayloadOption x)=>x switch{
            SoapMethod y=>this.SoapMethod(y),
            EnabledDisabledPayloadOption y=>this.EnabledDisabledPayloadOption(y),
            WsdlPayloadOption y=>this.WsdlPayloadOption(y),
            LoginTypePayloadOption y=>this.LoginTypePayloadOption(y),
            LiteralPayloadOption y=>this.LiteralPayloadOption(y),
            SessionTimeoutPayloadOption y=>this.SessionTimeoutPayloadOption(y),
            SchemaPayloadOption y=>this.SchemaPayloadOption(y),
            CharacterSetPayloadOption y=>this.CharacterSetPayloadOption(y),
            RolePayloadOption y=>this.RolePayloadOption(y),
            AuthenticationPayloadOption y=>this.AuthenticationPayloadOption(y),
            EncryptionPayloadOption y=>this.EncryptionPayloadOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression SoapMethod(SoapMethod x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EnabledDisabledPayloadOption(EnabledDisabledPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WsdlPayloadOption(WsdlPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LoginTypePayloadOption(LoginTypePayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralPayloadOption(LiteralPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SessionTimeoutPayloadOption(SessionTimeoutPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SchemaPayloadOption(SchemaPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CharacterSetPayloadOption(CharacterSetPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RolePayloadOption(RolePayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuthenticationPayloadOption(AuthenticationPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EncryptionPayloadOption(EncryptionPayloadOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression KeyOption(KeyOption x)=>x switch{
            KeySourceKeyOption y=>this.KeySourceKeyOption(y),
            AlgorithmKeyOption y=>this.AlgorithmKeyOption(y),
            IdentityValueKeyOption y=>this.IdentityValueKeyOption(y),
            ProviderKeyNameKeyOption y=>this.ProviderKeyNameKeyOption(y),
            CreationDispositionKeyOption y=>this.CreationDispositionKeyOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression KeySourceKeyOption(KeySourceKeyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlgorithmKeyOption(AlgorithmKeyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IdentityValueKeyOption(IdentityValueKeyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ProviderKeyNameKeyOption(ProviderKeyNameKeyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreationDispositionKeyOption(CreationDispositionKeyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FullTextCatalogOption(FullTextCatalogOption x)=>x switch{
            OnOffFullTextCatalogOption y=>this.OnOffFullTextCatalogOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OnOffFullTextCatalogOption(OnOffFullTextCatalogOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ServiceContract(ServiceContract x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ComputeClause(ComputeClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ComputeFunction(ComputeFunction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TableSampleClause(TableSampleClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExpressionWithSortOrder(ExpressionWithSortOrder x){
            throw this.単純NotSupportedException(x);
        }
        //private List<(Expressions.Expression Column,Int32 index)> List_ColumnIndex=new List<(Expressions.Expression Column,Int32 index)>();
        private e.Expression GroupByClause(GroupByClause x){
            var GroupingSpecifications=x.GroupingSpecifications;
            var GroupingSpecifications_Count=GroupingSpecifications.Count;
            var GroupingExpressions=new e.Expression[GroupingSpecifications_Count];
            var List_GroupByExpression=this.RefPeek.List_GroupByExpression;
            for(var a=0;a<GroupingSpecifications_Count;a++){
                var GroupingSpecification=this.GroupingSpecification(GroupingSpecifications[a]);
                List_GroupByExpression.Add(GroupingSpecification);
                GroupingExpressions[a]=GroupingSpecification;
            }
            return ValueTupleでNewする(this.作業配列,GroupingExpressions);
        }
        private e.Expression GroupingSpecification(GroupingSpecification x)=>x switch{
            ExpressionGroupingSpecification y=>this.ExpressionGroupingSpecification(y),
            CompositeGroupingSpecification y=>this.CompositeGroupingSpecification(y),
            CubeGroupingSpecification y=>this.CubeGroupingSpecification(y),
            RollupGroupingSpecification y=>this.RollupGroupingSpecification(y),
            GrandTotalGroupingSpecification y=>this.GrandTotalGroupingSpecification(y),
            GroupingSetsGroupingSpecification y=>this.GroupingSetsGroupingSpecification(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ExpressionGroupingSpecification(ExpressionGroupingSpecification x){
            var x_Expression=x.Expression;
            var ScalarExpression=this.ScalarExpression(x_Expression);
            return ScalarExpression;
        }
        private e.Expression CompositeGroupingSpecification(CompositeGroupingSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CubeGroupingSpecification(CubeGroupingSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression RollupGroupingSpecification(RollupGroupingSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GrandTotalGroupingSpecification(GrandTotalGroupingSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GroupingSetsGroupingSpecification(GroupingSetsGroupingSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OutputClause(OutputClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OutputIntoClause(OutputIntoClause x){
            //if(x is null)return Default_void;
            throw this.単純NotSupportedException(x);
        }
        private e.Expression HavingClause(HavingClause x)=>this.BooleanExpression(x.SearchCondition);
        private e.Expression OrderByClause(OrderByClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression QueryExpression(QueryExpression x)=>x switch{
            QueryParenthesisExpression y=>this.QueryParenthesisExpression(y),
            QuerySpecification y=>this.QuerySpecification(y),
            BinaryQueryExpression y=>this.BinaryQueryExpression(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression QueryParenthesisExpression(QueryParenthesisExpression x){
            var QueryExpression=this.QueryExpression(x.QueryExpression);
            if(x.ForClause is not null) { 
                var ForClause=this.ForClause(x.ForClause);
            }
            if(x.OffsetClause is not null) { 
                var OffsetClause=this.OffsetClause(x.OffsetClause);
            }
            if(x.OrderByClause is not null) { 
                var OrderByClause=this.OrderByClause(x.OrderByClause);
            }
            return QueryExpression;
        }
        //private partial Expressions.Expression QuerySpecification(QuerySpecification x);
        private e.Expression BinaryQueryExpression(BinaryQueryExpression x){
            var QueryExpression1=this.QueryExpression(x.FirstQueryExpression);
            this.RefPeek.Clear();
            var QueryExpression2=this.QueryExpression(x.SecondQueryExpression);
            var GenericMethodDifinition=x.BinaryQueryExpressionType switch{
                BinaryQueryExpressionType.Except=>Reflection.ExtensionSet.Except,
                BinaryQueryExpressionType.Intersect=>Reflection.ExtensionSet.Intersect,
                BinaryQueryExpressionType.Union=>Reflection.ExtensionSet.Union,
                _=>throw new NotImplementedException()
            };
            //NULLは型が確定できない、計算式によってはSingle,Doubleになるときがある
            //FirstQueryExpression  Object,Int16,String
            //SecondQueryExpression 
            //Expressions.Expression keySelector_Body1= keySelector_Body;
            List<e.Expression> List_Expression1=new(),List_Expression2=new();
            Type 内ElementType1 = IEnumerable1のT(QueryExpression1.Type),内ElementType2=IEnumerable1のT(QueryExpression2.Type);
            e.ParameterExpression 内Element1 = e.Expression.Parameter(内ElementType1,"1"),内Element2=e.Expression.Parameter(内ElementType2,"2");
            e.Expression 内ValueTuple1 =内Element1,内ValueTuple2=内Element2;
            var Item番号 = 1;
            while(true) {
                var (内Item1,内Item2)=ValueTuple_Item(ref 内ValueTuple1,ref 内ValueTuple2,ref Item番号);
                if(内Item1 is null)break;
                Debug.Assert(内Item2 is not null);
                var (外Item1,外Item2)=this.Convertデータ型を合わせるNullableは想定する(内Item1,内Item2);
                Debug.Assert(外Item1.Type==外Item2.Type);
                List_Expression1.Add(外Item1);List_Expression2.Add(外Item2);
            }
            var 作業配列=this.作業配列;
            e.NewExpression New1=ValueTupleでNewする(作業配列,List_Expression1),New2=ValueTupleでNewする(作業配列,List_Expression2);
            var 外ElementType=New1.Type;
            Debug.Assert(外ElementType==New2.Type);
            var Call1=e.Expression.Call(作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,内ElementType1,外ElementType),QueryExpression1,e.Expression.Lambda(New1,作業配列.Parameters設定(内Element1)));
            var Call2=e.Expression.Call(作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,内ElementType2,外ElementType),QueryExpression2,e.Expression.Lambda(New2,作業配列.Parameters設定(内Element2)));
            return e.Expression.Call(作業配列.MakeGenericMethod(GenericMethodDifinition,外ElementType),Call1,Call2);
            static (e.Expression?Item1, e.Expression?Item2)ValueTuple_Item(ref e.Expression ValueTuple1,ref e.Expression ValueTuple2,ref int Item番号) {
                if(Item番号==8) {
                    FieldInfo ValueTuple1_Rest= ValueTuple1.Type.GetField("Rest")!, ValueTuple2_Rest = ValueTuple2.Type.GetField("Rest")!;
                    if(ValueTuple1_Rest is null)return (null,null);
                    (ValueTuple1, ValueTuple2) = (e.Expression.Field(ValueTuple1,ValueTuple1_Rest),e.Expression.Field(ValueTuple2,ValueTuple2_Rest));
                    Item番号 = 2;
                    FieldInfo ValueTuple1_Item1 = ValueTuple1.Type.GetField("Item1")!, ValueTuple2_Item1 = ValueTuple2.Type.GetField("Item1")!;
                    return (e.Expression.Field(ValueTuple1,ValueTuple1_Item1), e.Expression.Field(ValueTuple2,ValueTuple2_Item1));
                }
                var Item= $"Item{Item番号}";
                FieldInfo ValueTuple1_Item = ValueTuple1.Type.GetField(Item)!,ValueTuple2_Item=ValueTuple2.Type.GetField(Item)!;
                Item番号++;
                return ValueTuple1_Item  is null?(null,null):(e.Expression.Field(ValueTuple1,ValueTuple1_Item), e.Expression.Field(ValueTuple2,ValueTuple2_Item));
            }
        }
        /// <summary>
        /// SELECT *
        /// FROM A,B
        /// ↓
        /// A.SelectMany(a=>B.Select(...))
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private (e.Expression Source,e.ParameterExpression Element)FromClause(FromClause x){
            var TableReferences0=x.TableReferences;
            var TableReferences0_Count=TableReferences0.Count;
            var(Source,Element0)=this.TableReference(TableReferences0[0]);
            Debug.Assert(IEnumerable1のT(Source.Type)==Element0.Type);
            var Element1=Element0;
            var TSource=Element0.Type;
            if(TableReferences0_Count>1){
                var 作業配列=this.作業配列;
                var Array論理Parameter物理Expressions=new(e.ParameterExpression 論理,e.Expression 物理)[TableReferences0_Count];
                Array論理Parameter物理Expressions[0].論理=Element0;
                //Select(ss=>)
                //Source.SelectMany(Element0=>CD,(Element0,cd)=>AB);
                for(var a=1;a<TableReferences0_Count;a++){
                    var(Collection,ce)=this.TableReference(TableReferences0[a]);
                    Array論理Parameter物理Expressions[a].論理=ce;
                    var TCollection=ce.Type;
                    //var b=作業配列.MakeGenericType(typeof(ImmutableSet<>),TCollection);
                    //Debug.Assert(b==Collection.Type);
                    var collectionSelectorMethod=作業配列.MakeGenericType(typeof(Func<,>),TSource,作業配列.MakeGenericType(typeof(ImmutableSet<>),TCollection));
                    var collectionSelector=e.Expression.Lambda(
                        collectionSelectorMethod,
                        Collection,
                        作業配列.Parameters設定(Element1)
                    );
                    var ValueTuple2_ctr=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,Element1.Type,TCollection);//todo 匿名型
                    var resultSelector=e.Expression.Lambda(
                        e.Expression.New(ValueTuple2_ctr,Element1,ce),
                        作業配列.Parameters設定(Element1,ce)
                    );
                    var TResult=ValueTuple2_ctr.DeclaringType!;
                    Source=e.Expression.Call(
                        作業配列.MakeGenericMethod(
                            Reflection.ExtensionSet.SelectMany_collectionSelector_resultSelector,
                            TSource,TCollection,TResult
                        ),
                        Source,
                        collectionSelector,
                        resultSelector
                    );
                    if(TableReferences0_Count==3) {

                    }
                    Debug.Assert(Source.Type.GetGenericArguments()[0]==TResult);
                    TSource=TResult;
                    Element1=e.Expression.Parameter(TSource,$"<,>{this.番号++}");
                }
                e.Expression Expression0=Element1;
                for(var index=TableReferences0_Count-1;index>=0;index--){
                    if(Expression0.Type.IsGenericType&&Expression0.Type.GetGenericTypeDefinition()==typeof(ValueTuple<,>)){
                        Array論理Parameter物理Expressions[index].物理=e.Expression.Field(Expression0,nameof(ValueTuple<int,int>.Item2));
                        Debug.Assert(Expression0.Type.GetGenericArguments().Length==2);
                        Expression0=e.Expression.Field(Expression0,nameof(ValueTuple<int,int>.Item1));
                    }else{
                        Debug.Assert(index==0);
                        Array論理Parameter物理Expressions[index].物理=Expression0;
                    }
                }
                ref var RefPeek=ref this.RefPeek;
                var Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
                var 変換_旧Parameterを新Expression=this.変換_旧Parameterを新Expression1;
                for(var a=0;a<TableReferences0_Count;a++){
                    ref var 論理Parameter物理Expressions=ref Array論理Parameter物理Expressions[a];
                    var 物理Expression=論理Parameter物理Expressions.物理;
                    var 論理Parameter=論理Parameter物理Expressions.論理;
                    foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression.ToList())
                        Dictionary_DatabaseSchemaTable_ColumnExpression[KV.Key]=変換_旧Parameterを新Expression.実行(KV.Value,論理Parameter,物理Expression);
                }
            }
            return(Source,Element1);
        }
        private void SelectElement(SelectElement x){
            switch(x){
                case SelectScalarExpression y:this.SelectScalarExpression(y);break;
                case SelectStarExpression y:this.SelectStarExpression(y); break;
                case SelectSetVariable y:this.SelectSetVariable(y); break;
                default:throw this.単純NotSupportedException(x);
            }
        }
        private void SelectScalarExpression(SelectScalarExpression x){
            ref var RefPeek=ref this.RefPeek;
            var Result=this.ScalarExpression(x.Expression);
            this.判定指定Table.実行(Result,RefPeek.List_TableExpression);
            var 出力TableExpressions=this.出力TableExpressions;
            if(出力TableExpressions.Count>0) { 
                var Predicate=e.Expression.NotEqual(出力TableExpressions[0],Constant_null);
                var 出力TableExpressions_Count=出力TableExpressions.Count;
                for(var a=1;a<出力TableExpressions_Count;a++)
                    Predicate=e.Expression.AndAlso(Predicate,e.Expression.NotEqual(出力TableExpressions[a],Constant_null));
                Result=e.Expression.Condition(Predicate,Result,e.Expression.Default(Result.Type));
            }
            //RefPeek.List_TableExpression
            //ResultにTableExpressionがnullだとnullを返すべき式があるか。あればCoalesce
            //    e.Expression.Coalesce()
            RefPeek.List_ColumnExpression.Add(Result);
            if(x.ColumnName is not null){
                RefPeek.List_ColumnAlias.Add(x.ColumnName.Value);
            }else if(x.Expression is ColumnReferenceExpression ColumnReferenceExpression){
                var Identifiers=ColumnReferenceExpression.MultiPartIdentifier.Identifiers;
                RefPeek.List_ColumnAlias.Add(Identifiers[Identifiers.Count-1].Value);
            }else{
                //SELECT 列名1,列名2 UNION SELECT 345←列名無し
                RefPeek.List_ColumnAlias.Add(this.番号++.ToString(CultureInfo.CurrentCulture));
            }
        }
        private void SelectStarExpression(SelectStarExpression x){
            ref var RefPeek0=ref this.RefPeek;
            var RefPeek0_List_ColumnAlias=RefPeek0.List_ColumnAlias;
            var RefPeek0_ColumnExpression=RefPeek0.List_ColumnExpression;
            var RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek0.Dictionary_DatabaseSchemaTable_ColumnExpression;
            if(x.Qualifier is not null){
                //Database.Schema.Table.*
                //Schema.Table.*
                //Table.*
                var Key=this.SQL取得(x.Qualifier);
                var ColumnAliases=RefPeek0.Dictionary_TableAlias_ColumnAliases[Key];
                RefPeek0_List_ColumnAlias.AddRange(ColumnAliases);
                foreach(var ColumnAlias in ColumnAliases)
                    RefPeek0_ColumnExpression.Add(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression[Key+'.'+ColumnAlias]);
            }else{
                //*
                Debug.Assert("*"==this.SQL取得(x));
                RefPeek0_List_ColumnAlias.AddRange(RefPeek0.List_アスタリスクColumnAlias);
                RefPeek0_ColumnExpression.AddRange(RefPeek0.List_アスタリスクColumnExpression);
            }
        }
        /// <summary>
        /// SELECT @ABC=3*4,x.AssignmentKindは処理していない。
        /// </summary>
        /// <param name="x"></param>
        private void SelectSetVariable(SelectSetVariable x){
            //select @theId=DATABASE_PRINCIPAL_ID();    
            //x.AssignmentKind=AssignmentKind.
            Debug.Assert(x.AssignmentKind==AssignmentKind.Equals);
            var Variable=this.VariableReference(x.Variable);
            var Expression=this.ScalarExpression(x.Expression);
            Expression=this.Convertデータ型を合わせるNullableは想定する(Expression,Variable.Type);
            //メッセージ 141、レベル 15、状態 1、行 2
            //変数に値を代入する SELECT ステートメントを、データ取得操作と組み合わせることはできません。
            this.RefPeek.List_ColumnExpression.Add(e.Expression.Assign(Variable,Expression));
            //this._StackSubquery単位の情報.RefPeek.List_ColumnExpression.Add(
            //    Expressions.Expression.Assign(
            //        this.VariableReference(x.Variable),
            //        this.Nullableにする(
            //            this.ScalarExpression(x.Expression)
            //        )
            //    )
            //);
        }
        private e.Expression TopRowFilter(TopRowFilter x){
            return Default_void;
        }
        private e.Expression OffsetClause(OffsetClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterFullTextIndexAction(AlterFullTextIndexAction x)=>x switch{
            SimpleAlterFullTextIndexAction y=>this.SimpleAlterFullTextIndexAction(y),
            SetStopListAlterFullTextIndexAction y=>this.SetStopListAlterFullTextIndexAction(y),
            SetSearchPropertyListAlterFullTextIndexAction y=>this.SetSearchPropertyListAlterFullTextIndexAction(y),
            DropAlterFullTextIndexAction y=>this.DropAlterFullTextIndexAction(y),
            AddAlterFullTextIndexAction y=>this.AddAlterFullTextIndexAction(y),
            AlterColumnAlterFullTextIndexAction y=>this.AlterColumnAlterFullTextIndexAction(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression SimpleAlterFullTextIndexAction(SimpleAlterFullTextIndexAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetStopListAlterFullTextIndexAction(SetStopListAlterFullTextIndexAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SetSearchPropertyListAlterFullTextIndexAction(SetSearchPropertyListAlterFullTextIndexAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropAlterFullTextIndexAction(DropAlterFullTextIndexAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AddAlterFullTextIndexAction(AddAlterFullTextIndexAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterColumnAlterFullTextIndexAction(AlterColumnAlterFullTextIndexAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SearchPropertyListAction(SearchPropertyListAction x)=>x switch{
            AddSearchPropertyListAction y=>this.AddSearchPropertyListAction(y),
            DropSearchPropertyListAction y=>this.DropSearchPropertyListAction(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AddSearchPropertyListAction(AddSearchPropertyListAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DropSearchPropertyListAction(DropSearchPropertyListAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CreateLoginSource(CreateLoginSource x)=>x switch{
            PasswordCreateLoginSource y=>this.PasswordCreateLoginSource(y),
            WindowsCreateLoginSource y=>this.WindowsCreateLoginSource(y),
            CertificateCreateLoginSource y=>this.CertificateCreateLoginSource(y),
            AsymmetricKeyCreateLoginSource y=>this.AsymmetricKeyCreateLoginSource(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression PasswordCreateLoginSource(PasswordCreateLoginSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WindowsCreateLoginSource(WindowsCreateLoginSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CertificateCreateLoginSource(CertificateCreateLoginSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AsymmetricKeyCreateLoginSource(AsymmetricKeyCreateLoginSource x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PrincipalOption(PrincipalOption x)=>x switch{
            OnOffPrincipalOption y=>this.OnOffPrincipalOption(y),
            LiteralPrincipalOption y=>this.LiteralPrincipalOption(y),
            IdentifierPrincipalOption y=>this.IdentifierPrincipalOption(y),
            PasswordAlterPrincipalOption y=>this.PasswordAlterPrincipalOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression OnOffPrincipalOption(OnOffPrincipalOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralPrincipalOption(LiteralPrincipalOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression IdentifierPrincipalOption(IdentifierPrincipalOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PasswordAlterPrincipalOption(PasswordAlterPrincipalOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DialogOption(DialogOption x)=>x switch{
            ScalarExpressionDialogOption y=>this.ScalarExpressionDialogOption(y),
            OnOffDialogOption y=>this.OnOffDialogOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression ScalarExpressionDialogOption(ScalarExpressionDialogOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OnOffDialogOption(OnOffDialogOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TSqlFragmentSnippet(TSqlFragmentSnippet x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TSqlScript(TSqlScript x){
            var Batches=x.Batches;
            var Batches_Count=Batches.Count;
            if(Batches_Count==1)return this.TSqlBatch(Batches[0]);
            else{
                var ExpressionArray=new e.Expression[Batches_Count];
                for(var a=0;a<Batches_Count;a++)
                    ExpressionArray[a]=this.TSqlBatch(Batches[a]);
                return e.Expression.Block(ExpressionArray);
            }
        }
        /// <summary>
        /// SELECT xxxx;UPDATE yyyy;SELECT zzzz;
        /// ";"が1ステートメントに対応する。
        /// </summary>
        /// <param name="Statements"></param>
        /// <returns></returns>
        private e.Expression Statements(IList<TSqlStatement> Statements){
            var Statements_Count=Statements.Count;
            if(Statements_Count==1)return this.TSqlStatement(Statements[0]);
            var ListExpression=new List<e.Expression>();
            for(var a=0;a<Statements_Count;a++){
                var Expression=this.TSqlStatement(Statements[a]);
                if(Expression!=Default_void)ListExpression.Add(Expression);
                //SELECT @package_name = name,@folderid = folderid FROM dbo.sysssispackages
                //ここでクリアすべき
                //SELECT @foldername = foldername,@folderid = parentfolderid FROM dbo.sysssispackagefolders WHERE folderid = @prevfolderid
            }
            return e.Expression.Block(ListExpression);
        }
        /// <summary>
        /// SELECT xxxx;UPDATE yyyy;SELECT zzzz;
        /// GO
        /// DELETE wwww;
        /// GO
        /// "GO"が1バッチ処理。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private e.Expression TSqlBatch(TSqlBatch x){
            var Statements=this.Statements(x.Statements);
            return Statements;
        }
        private e.Expression MergeActionClause(MergeActionClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MergeAction(MergeAction x)=>x switch{
            UpdateMergeAction y=>this.UpdateMergeAction(y),
            DeleteMergeAction y=>this.DeleteMergeAction(y),
            InsertMergeAction y=>this.InsertMergeAction(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression UpdateMergeAction(UpdateMergeAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DeleteMergeAction(DeleteMergeAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression InsertMergeAction(InsertMergeAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditSpecificationPart(AuditSpecificationPart x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditSpecificationDetail(AuditSpecificationDetail x)=>x switch{
            AuditActionSpecification y=>this.AuditActionSpecification(y),
            AuditActionGroupReference y=>this.AuditActionGroupReference(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression AuditActionSpecification(AuditActionSpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditActionGroupReference(AuditActionGroupReference x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DatabaseAuditAction(DatabaseAuditAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditTarget(AuditTarget x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditOption(AuditOption x)=>x switch{
            QueueDelayAuditOption y=>this.QueueDelayAuditOption(y),
            AuditGuidAuditOption y=>this.AuditGuidAuditOption(y),
            OnFailureAuditOption y=>this.OnFailureAuditOption(y),
            StateAuditOption y=>this.StateAuditOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression QueueDelayAuditOption(QueueDelayAuditOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditGuidAuditOption(AuditGuidAuditOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OnFailureAuditOption(OnFailureAuditOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression StateAuditOption(StateAuditOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AuditTargetOption(AuditTargetOption x)=>x switch{
            MaxSizeAuditTargetOption y=>this.MaxSizeAuditTargetOption(y),
            MaxRolloverFilesAuditTargetOption y=>this.MaxRolloverFilesAuditTargetOption(y),
            LiteralAuditTargetOption y=>this.LiteralAuditTargetOption(y),
            OnOffAuditTargetOption y=>this.OnOffAuditTargetOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression MaxSizeAuditTargetOption(MaxSizeAuditTargetOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MaxRolloverFilesAuditTargetOption(MaxRolloverFilesAuditTargetOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralAuditTargetOption(LiteralAuditTargetOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OnOffAuditTargetOption(OnOffAuditTargetOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ResourcePoolParameter(ResourcePoolParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ResourcePoolAffinitySpecification(ResourcePoolAffinitySpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalResourcePoolParameter(ExternalResourcePoolParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression ExternalResourcePoolAffinitySpecification(ExternalResourcePoolAffinitySpecification x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WorkloadGroupParameter(WorkloadGroupParameter x)=>x switch{
            WorkloadGroupResourceParameter y=>this.WorkloadGroupResourceParameter(y),
            WorkloadGroupImportanceParameter y=>this.WorkloadGroupImportanceParameter(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression WorkloadGroupResourceParameter(WorkloadGroupResourceParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WorkloadGroupImportanceParameter(WorkloadGroupImportanceParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BrokerPriorityParameter(BrokerPriorityParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FullTextStopListAction(FullTextStopListAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventSessionObjectName(EventSessionObjectName x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventDeclaration(EventDeclaration x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression EventDeclarationSetParameter(EventDeclarationSetParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TargetDeclaration(TargetDeclaration x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SessionOption(SessionOption x)=>x switch{
            EventRetentionSessionOption y=>this.EventRetentionSessionOption(y),
            MemoryPartitionSessionOption y=>this.MemoryPartitionSessionOption(y),
            LiteralSessionOption y=>this.LiteralSessionOption(y),
            MaxDispatchLatencySessionOption y=>this.MaxDispatchLatencySessionOption(y),
            OnOffSessionOption y=>this.OnOffSessionOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression EventRetentionSessionOption(EventRetentionSessionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MemoryPartitionSessionOption(MemoryPartitionSessionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression LiteralSessionOption(LiteralSessionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression MaxDispatchLatencySessionOption(MaxDispatchLatencySessionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression OnOffSessionOption(OnOffSessionOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SpatialIndexOption(SpatialIndexOption x)=>x switch{
            SpatialIndexRegularOption y=>this.SpatialIndexRegularOption(y),
            BoundingBoxSpatialIndexOption y=>this.BoundingBoxSpatialIndexOption(y),
            GridsSpatialIndexOption y=>this.GridsSpatialIndexOption(y),
            CellsPerObjectSpatialIndexOption y=>this.CellsPerObjectSpatialIndexOption(y),
            _=>throw this.単純NotSupportedException(x.GetType())
        };
        private e.Expression SpatialIndexRegularOption(SpatialIndexRegularOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BoundingBoxSpatialIndexOption(BoundingBoxSpatialIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GridsSpatialIndexOption(GridsSpatialIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression CellsPerObjectSpatialIndexOption(CellsPerObjectSpatialIndexOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression BoundingBoxParameter(BoundingBoxParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression GridParameter(GridParameter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationBufferPoolExtensionOption(AlterServerConfigurationBufferPoolExtensionOption x)=>x switch{
            AlterServerConfigurationBufferPoolExtensionContainerOption y=>this.AlterServerConfigurationBufferPoolExtensionContainerOption(y),
            AlterServerConfigurationBufferPoolExtensionSizeOption y=>this.AlterServerConfigurationBufferPoolExtensionSizeOption(y),
            _=>throw this.単純NotSupportedException(x.GetType())
        };
        private e.Expression AlterServerConfigurationBufferPoolExtensionContainerOption(AlterServerConfigurationBufferPoolExtensionContainerOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationBufferPoolExtensionSizeOption(AlterServerConfigurationBufferPoolExtensionSizeOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationDiagnosticsLogOption(AlterServerConfigurationDiagnosticsLogOption x)=>x switch{
            AlterServerConfigurationDiagnosticsLogMaxSizeOption y=>this.AlterServerConfigurationDiagnosticsLogMaxSizeOption(y),
            _=>throw this.単純NotSupportedException(x.GetType())
        };
        private e.Expression AlterServerConfigurationDiagnosticsLogMaxSizeOption(AlterServerConfigurationDiagnosticsLogMaxSizeOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationFailoverClusterPropertyOption(AlterServerConfigurationFailoverClusterPropertyOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationHadrClusterOption(AlterServerConfigurationHadrClusterOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterServerConfigurationSoftNumaOption(AlterServerConfigurationSoftNumaOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AvailabilityReplica(AvailabilityReplica x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AvailabilityReplicaOption(AvailabilityReplicaOption x)=>x switch{
            LiteralReplicaOption y=>this.LiteralReplicaOption(y),
            AvailabilityModeReplicaOption y=>this.AvailabilityModeReplicaOption(y),
            FailoverModeReplicaOption y=>this.FailoverModeReplicaOption(y),
            PrimaryRoleReplicaOption y=>this.PrimaryRoleReplicaOption(y),
            SecondaryRoleReplicaOption y=>this.SecondaryRoleReplicaOption(y),
            _=>throw this.単純NotSupportedException(x)
        };
        private e.Expression LiteralReplicaOption(LiteralReplicaOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AvailabilityModeReplicaOption(AvailabilityModeReplicaOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression FailoverModeReplicaOption(FailoverModeReplicaOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression PrimaryRoleReplicaOption(PrimaryRoleReplicaOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SecondaryRoleReplicaOption(SecondaryRoleReplicaOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AvailabilityGroupOption(AvailabilityGroupOption x)=>x switch{
            LiteralAvailabilityGroupOption y=>this.LiteralAvailabilityGroupOption(y),
            _=>throw this.単純NotSupportedException(x.GetType())
        };
        private e.Expression LiteralAvailabilityGroupOption(LiteralAvailabilityGroupOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterAvailabilityGroupAction(AlterAvailabilityGroupAction x)=>x switch{
            AlterAvailabilityGroupFailoverAction y=>this.AlterAvailabilityGroupFailoverAction(y),
            _=>throw this.単純NotSupportedException(x.GetType())
        };
        private e.Expression AlterAvailabilityGroupFailoverAction(AlterAvailabilityGroupFailoverAction x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression AlterAvailabilityGroupFailoverOption(AlterAvailabilityGroupFailoverOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression DiskStatementOption(DiskStatementOption x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WindowFrameClause(WindowFrameClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WindowDelimiter(WindowDelimiter x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression WithinGroupClause(WithinGroupClause x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression SelectiveXmlIndexPromotedPath(SelectiveXmlIndexPromotedPath x){
            throw this.単純NotSupportedException(x);
        }
        private e.Expression TemporalClause(TemporalClause x){
            throw this.単純NotSupportedException(x);
        }
    }
}
//2022/04/16 7595
//2022/04/02 7216
//2022/03/23 7861
//7701
//9940