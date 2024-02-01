using LinqDB.Databases.Dom;
using LinqDB.Helpers;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using Array = System.Array;
//using Microsoft.CSharp.RuntimeBinder;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;
using AssemblyGenerator = Lokad.ILPack.AssemblyGenerator;
using Container = LinqDB.Databases.Container;
using Delegate = System.Delegate;
using Regex = System.Text.RegularExpressions.Regex;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
using static LinqDB.Optimizers.Common;
using LinqDB.Optimizers.VoidExpressionTraverser;
using LinqDB.Optimizers.VoidTSqlFragmentTraverser;
using Expression = System.Linq.Expressions.Expression;
using System.Collections.Generic;
using LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
// ReSharper disable All
namespace LinqDB.Optimizers;
using Generic = System.Collections.Generic;
/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed class Optimizer:IDisposable {
    private readonly 作業配列 作業配列 = new();
    /// <summary>
    /// Where((ValueTule&lt;,>p=>p.Item1==p.Item2)は移動できない
    /// Where((ValueTule&lt;,>p=>p.Item1.Item1==p.Item.Item2)は移動できる
    /// pは要素数2の匿名型かValueTule&lt;,>
    /// </summary>
    //private static Expression AndAlsoで繋げる(Expression? predicate,Expression e) => predicate is null ? e : Expression.AndAlso(predicate,e);
    //private readonly ExpressionEqualityComparer _ExpressionEqualityComparer;
    private readonly Generic.List<ParameterExpression> ループ跨ぎParameters = new();
    private readonly SQLServer.TSql160Parser Parser = new(true);
    private readonly 変換_TSqlFragment正規化 _変換_TSqlFragment正規化;
    private readonly 変換_TSqlFragmentからExpression _変換_TSqlFragmentからExpression;
    private readonly 変換_メソッド正規化_取得インライン不可能定数 変換_メソッド正規化_取得インライン不可能定数;
    private readonly 変換_Tryの先行評価 変換_Tryの先行評価;
    private readonly 変換_WhereからLookup _変換_WhereからLookup;
    private readonly 変換_跨ぎParameterの先行評価 変換_跨ぎParameterの先行評価;
    private readonly 変換_局所Parameterの先行評価 変換_局所Parameterの先行評価;
    private readonly 変換_Stopwatchに埋め込む 変換_Stopwatchに埋め込む;
    private readonly 変換_インラインループ独立 変換_インラインループ独立;
    private readonly 取得_ラムダを跨ぐParameter 取得ラムダを跨ぐParameter;
    private readonly 検証_Parameterの使用状態 _検証_Parameterの使用状態;
    private readonly 取得_CSharp _取得_CSharp = new();
    private readonly 判定_InstanceMethodか 判定InstanceMethodか;

    private readonly 検証_変形状態 _検証_変形状態;
    private readonly 作成_DynamicMethod _作成_DynamicMethod;
    private readonly 作成_DynamicAssembly _作成_DynamicAssembly;
    private readonly 取得_命令ツリー _取得_命令ツリー = new();
    private readonly 計測Maneger 計測Maneger = new();
    //private readonly A計測 Top辺;
    //private readonly List計測 List辺= new();
    private readonly Dictionary<LabelTarget,計測> Dictionary_LabelTarget_辺=new();
    //private readonly ConstantExpression ConstantList計測;
    private readonly StringBuilder sb=new();
    private Generic.Dictionary<ConstantExpression,(FieldInfo Disp, MemberExpression Member)> DictionaryConstant {
        get => this.変換_メソッド正規化_取得インライン不可能定数.DictionaryConstant;
        set {
            this.取得ラムダを跨ぐParameter.DictionaryConstant=value;
            this.変換_メソッド正規化_取得インライン不可能定数.DictionaryConstant=value;
            this.判定InstanceMethodか.DictionaryConstant=value;
            Debug.Assert(this._作成_DynamicMethod.DictionaryConstant==value);
            Debug.Assert(this._作成_DynamicAssembly.DictionaryConstant==value);
            //this._作成_DynamicMethod.DictionaryConstant=value;
            //this._作成_DynamicAssembly.DictionaryConstant=value;
        }
    }
    private Generic.Dictionary<DynamicExpression,(FieldInfo Disp, MemberExpression Member)> DictionaryDynamic {
        get => this.取得ラムダを跨ぐParameter.DictionaryDynamic;
        set {
            this.取得ラムダを跨ぐParameter.DictionaryDynamic=value;
            this._作成_DynamicMethod.DictionaryDynamic=value;
            this._作成_DynamicAssembly.DictionaryDynamic=value;
        }
    }
    private Generic.Dictionary<LambdaExpression,(FieldInfo Disp, MemberExpression Member, MethodBuilder Impl)> DictionaryLambda {
        get => this.取得ラムダを跨ぐParameter.DictionaryLambda;
        set {
            this.取得ラムダを跨ぐParameter.DictionaryLambda=value;
            this._作成_DynamicMethod.DictionaryLambda=value;
            this._作成_DynamicAssembly.DictionaryLambda=value;
        }
    }
    private Generic.Dictionary<ParameterExpression,(FieldInfo Disp, MemberExpression Member)> Dictionaryラムダ跨ぎParameter {
        get => this.取得ラムダを跨ぐParameter.Dictionaryラムダ跨ぎParameter;
        set {
            this.取得ラムダを跨ぐParameter.Dictionaryラムダ跨ぎParameter=value;
            this.変換_跨ぎParameterの先行評価.Dictionaryラムダ跨ぎParameter=value;
            this.変換_局所Parameterの先行評価.ラムダ跨ぎParameters=value.Keys;
            this._検証_Parameterの使用状態.ラムダ跨ぎParameters=value.Keys;
            this._作成_DynamicMethod.Dictionaryラムダ跨ぎParameter=value;
            this._作成_DynamicAssembly.Dictionaryラムダ跨ぎParameter=value;
        }
    }
    private ParameterExpression DispParameter {
        get => this._作成_DynamicMethod.DispParameter;
        set {
            this._作成_DynamicMethod.DispParameter=value;
            this._作成_DynamicAssembly.DispParameter=value;
        }
    }
    public string Analize=>this.変換_Stopwatchに埋め込む.Analize;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Optimizer() : this(typeof(object)) {
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="BaseType"></param>
    private Optimizer(Type BaseType) {
        var 作業配列 = this.作業配列;
        var ScriptGenerator = new SQLServer.Sql160ScriptGenerator(
            new SQLServer.SqlScriptGeneratorOptions {
                KeywordCasing=SQLServer.KeywordCasing.Lowercase,
                IncludeSemicolons=true,
                NewLineBeforeFromClause=true,
                NewLineBeforeJoinClause=true,
                NewLineBeforeWhereClause=true,
                NewLineBeforeGroupByClause=true,
                NewLineBeforeOrderByClause=true,
                NewLineBeforeHavingClause=true,
            }
        );
        var 変換_旧Parameterを新Expression1 = new 変換_旧Parameterを新Expression1(作業配列);
        var ExpressionEqualityComparer = new ExpressionEqualityComparer();
        //TSQLでは1度だけnewすればいいが
        var 判定_InstanceMethodか = this.判定InstanceMethodか=new(ExpressionEqualityComparer);
        this._変換_TSqlFragment正規化=new(ScriptGenerator);
        var ブローブビルドExpressionEqualityComparer = new ブローブビルドExpressionEqualityComparer(ExpressionEqualityComparer);
        var 判定_指定Parameter無_他Parameter有 = new 判定_指定Parameter無_他Parameter有();
        var 判定_指定Parameters無 = new 判定_指定Parameters無();
        var 判定_指定Parameter有_他Parameter無_Lambda内部走査 = new 判定_指定Parameter有_他Parameter無_Lambda内部走査();
        var 取得_OuterPredicate_InnerPredicate_プローブビルド = new 取得_OuterPredicate_InnerPredicate_プローブビルド(作業配列,判定_指定Parameter無_他Parameter有,判定_指定Parameter有_他Parameter無_Lambda内部走査,ブローブビルドExpressionEqualityComparer);
        var 変換_旧Expressionを新Expression1 = new 変換_旧Expressionを新Expression1(作業配列,ExpressionEqualityComparer);
        this._変換_TSqlFragmentからExpression=new(作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド,ExpressionEqualityComparer,変換_旧Parameterを新Expression1,変換_旧Expressionを新Expression1,判定_指定Parameters無,ScriptGenerator);
        var 変換_旧Parameterを新Expression2 = new 変換_旧Parameterを新Expression2(作業配列);
        this.変換_メソッド正規化_取得インライン不可能定数=new(作業配列,変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2,変換_旧Expressionを新Expression1);
        this.変換_Tryの先行評価=new(作業配列);
        this._変換_WhereからLookup=new(作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド,判定_指定Parameters無);
        var ループ跨ぎParameters = this.ループ跨ぎParameters;
        this.変換_跨ぎParameterの先行評価=new(作業配列,ExpressionEqualityComparer);
        //var ExpressionEqualityComparer_Assign_Leftで比較 = new ExpressionEqualityComparer_Assign_Leftで比較();
        this.変換_局所Parameterの先行評価=new(作業配列);
        this.取得ラムダを跨ぐParameter=new();
        this._検証_変形状態=new();
        this._検証_Parameterの使用状態=new(ループ跨ぎParameters);
        this.変換_インラインループ独立=new(作業配列,変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2);
        var List計測=this.計測Maneger;
        //this.Top辺=new 計測する{子コメント="開始"};
        this.変換_Stopwatchに埋め込む=new(作業配列,this.計測Maneger,this.Dictionary_LabelTarget_辺);
        this._作成_DynamicMethod=new(判定_InstanceMethodか);
        this._作成_DynamicAssembly=new(判定_InstanceMethodか);
        this.DictionaryConstant=new(ExpressionEqualityComparer);
        this.DictionaryDynamic=new();
        //this.DictionaryLambda=new(ExpressionEqualityComparer);
        this.DictionaryLambda=new(new LambdaEqualityComparer());
        this.Dictionaryラムダ跨ぎParameter=new();
    }
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    /// <summary>オブジェクトが、ガベージ コレクションによって収集される前に、リソースの解放とその他のクリーンアップ操作の実行を試みることができるようにします。</summary>
    ~Optimizer() => this.Dispose(false);
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
                this._取得_命令ツリー.Dispose();
                this._取得_CSharp.Dispose();
            }
        }
    }
    private SQLServer.TSqlFragment SQLからTSqlFragment(string SQL) {
        var Parser = this.Parser;
        var Parsed = Parser.Parse(new StringReader(SQL),out var errors);
        if(errors!.Count!=0) {
            var sb = new StringBuilder();
            foreach(var error in errors) {
                sb.AppendLine(error.Message);
            }
            throw new System.Data.SyntaxErrorException(sb.ToString());
        }
        return Parsed;
    }
    private string? SQL;
    /// <summary>
    /// クライアントで使う
    /// </summary>
    /// <param name="Parameter"></param>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public Expression SQLToExpression(ParameterExpression Parameter,string SQL) {
        var TSqlFragment = this.SQLからTSqlFragment(SQL);
        this.SQL=SQL;
        this._変換_TSqlFragment正規化.実行(TSqlFragment);
        return this._変換_TSqlFragmentからExpression.実行(Parameter,TSqlFragment);
    }
    private static object Get_ValueTuple(MemberExpression Member,object Tuple) {
        var Field = (FieldInfo)Member.Member;
        if(Member.Expression is MemberExpression Member1) {
            Tuple=Get_ValueTuple(Member1,Tuple);
        }
        var Value = Field.GetValue(Tuple);
        Debug.Assert(Value!=null);
        return Value;
    }
    internal void Disp作成(ParameterExpression ContainerParameter,Information Information,string SQL) {
        var Disp_TypeBuilder = Information.Disp_TypeBuilder;
        var Impl_TypeBuilder = Information.Impl_TypeBuilder;
        Debug.Assert(Disp_TypeBuilder is not null);
        var Expression0 = this.SQLToExpression(ContainerParameter,SQL);
        var Lambda0 = (LambdaExpression)Expression0;
        var DictionaryConstant = this.DictionaryConstant=Information.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic=Information.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda=Information.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter=Information.Dictionaryラムダ跨ぎParameter;
        var Lambda1 = Information.Lambda=this.Lambda最適化(Lambda0);
        this.取得ラムダを跨ぐParameter.実行(Lambda1);
        var Disp_ctor_I = Information.Disp_ctor_I;
        {
            var Field番号 = 0;
            foreach(var a in DictionaryConstant.AsEnumerable())
                DictionaryConstant[a.Key]=(Disp_TypeBuilder.DefineField($"Constant{Field番号++}",a.Key.Type,FieldAttributes.Public)!, default!);
            foreach(var a in DictionaryDynamic.AsEnumerable())
                DictionaryDynamic[a.Key]=(Disp_TypeBuilder.DefineField($"CallSite{Field番号++}",a.Key.Type,FieldAttributes.Public)!, default!);
            var 判定InstanceMethodか = this.判定InstanceMethodか;
            var 作業配列 = this.作業配列;
            var Types2 = 作業配列.Types2;
            Types2[0]=typeof(object);
            Types2[1]=typeof(IntPtr);
            foreach(var a in DictionaryLambda.AsEnumerable()) {
                var Lambda = a.Key;
                var Lambda_Parameters = Lambda.Parameters;
                var Lambda_Parameters_Count = Lambda_Parameters.Count;
                var インスタンスメソッドか = 判定InstanceMethodか.実行(Lambda.Body)|true;
                Type[] DispTypes, ImplTypes;
                if(インスタンスメソッドか) {
                    DispTypes=new Type[Lambda_Parameters_Count];
                    ImplTypes=new Type[Lambda_Parameters_Count+1];
                    ImplTypes[0]=Disp_TypeBuilder;
                    for(var b = 0;b<Lambda_Parameters_Count;b++) {
                        var 元Parameter = Lambda_Parameters[b];
                        var Type = 元Parameter.IsByRef
                            ? 元Parameter.Type.MakeByRefType()
                            : 元Parameter.Type;
                        DispTypes[b+0]=Type;
                        ImplTypes[b+1]=Type;
                    }
                } else {
                    DispTypes=ImplTypes=new Type[Lambda_Parameters_Count];
                    for(var b = 0;b<Lambda_Parameters_Count;b++) {
                        var 元Parameter = Lambda_Parameters[b];
                        ImplTypes[b]=元Parameter.IsByRef ? 元Parameter.Type.MakeByRefType() : 元Parameter.Type;
                    }
                }
                var Disp_MethodBuilder = Disp_TypeBuilder.DefineMethod($"DispMethod{Field番号}",MethodAttributes.Public,Lambda.ReturnType,DispTypes);
                Disp_MethodBuilder.InitLocals=false;
                var Impl_MethodBuilder = Impl_TypeBuilder.DefineMethod($"ImplMethod{Field番号}",MethodAttributes.Public|MethodAttributes.Static,Lambda.ReturnType,ImplTypes);
                Impl_MethodBuilder.InitLocals=false;
                var Disp_MethodBuilder_I = Disp_MethodBuilder.GetILGenerator();
                int Disp_index = 1, Impl_index;
                if(インスタンスメソッドか) {
                    Disp_MethodBuilder_I.Ldarg_0();
                    Impl_MethodBuilder.DefineParameter(1,ParameterAttributes.None,"Disp");
                    Impl_index=2;
                } else {
                    Impl_index=1;
                }
                ushort index = 1;
                for(var b = 0;b<Lambda_Parameters_Count;b++) {
                    Disp_MethodBuilder_I.Ldarg(index++);
                    var ParameterName = Lambda_Parameters[b].Name;
                    Disp_MethodBuilder.DefineParameter(Disp_index++,ParameterAttributes.None,ParameterName);
                    Impl_MethodBuilder.DefineParameter(Impl_index++,ParameterAttributes.None,ParameterName);
                }
                Disp_MethodBuilder_I.Call(Impl_MethodBuilder);
                Disp_MethodBuilder_I.Ret();
                var Delegate = Disp_TypeBuilder.DefineField($"Delegate{Field番号}",Lambda.Type,FieldAttributes.Public);
                Disp_ctor_I.Ldarg_0();
                Disp_ctor_I.Ldarg_0();
                Disp_ctor_I.Ldftn(Disp_MethodBuilder);
                Disp_ctor_I.Newobj(Lambda.Type.GetConstructor(Types2)!);
                Disp_ctor_I.Stfld(Delegate);
                DictionaryLambda[a.Key]=(Delegate, a.Value.Member, Impl_MethodBuilder);
                Field番号++;
            }
            Disp_ctor_I.Ret();
            {
                var (_, _, Impl_MethodBuilder)=DictionaryLambda[Lambda1];
                var I = Information.SchemaのMethod.GetILGenerator();
                I.Ldarg_0();
                //10 TF,SF
                var Count = Lambda0.Parameters.Count;
                for(var a = 1;a<=Count;a++) I.Ldarg((ushort)a);
                //11 V,TF,SF
                I.Call(Impl_MethodBuilder);
                I.Ret();
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.Where(p => p.Key!=ContainerParameter))
                Dictionaryラムダ跨ぎParameter[a.Key]=(Disp_TypeBuilder.DefineField(a.Key.Name,a.Key.Type,FieldAttributes.Public), a.Value.Member);
        }

        var DispType = Information.CreateDispType();
        var DispParameter = this.DispParameter=Expression.Parameter(DispType,"Disp");
        Debug.Assert(Information.DispParameter is null);
        Information.DispParameter=DispParameter;
        {
            var Field番号 = 0;
            foreach(var a in DictionaryConstant.AsEnumerable()) {
                Debug.Assert($"Constant{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryConstant[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryDynamic.AsEnumerable()) {
                Debug.Assert($"CallSite{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryDynamic[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryLambda.AsEnumerable()) {
                Debug.Assert($"Delegate{Field番号}"==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryLambda[a.Key]=(
                    Field,
                    Expression.Field(DispParameter,Field),
                    a.Value.Impl
                );
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable()) {
                Debug.Assert(a.Key.Name==a.Value.Disp.Name);
                Field番号++;
                var Field = DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                Dictionaryラムダ跨ぎParameter[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
        }
    }
    internal void Impl作成(Information Information,ParameterExpression ContainerParameter) {
        Debug.Assert(Information.DispParameter is not null);
        var DispParameter = Information.DispParameter;
        this.DispParameter=DispParameter;
        var DictionaryConstant = this.DictionaryConstant=Information.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic=Information.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda=Information.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter=Information.Dictionaryラムダ跨ぎParameter;
        Debug.Assert(Information.Disp_Type is not null);
        var ContainerField = Information.Disp_Type.GetField("Container");
        Debug.Assert(ContainerField is not null);
        Dictionaryラムダ跨ぎParameter.Add(ContainerParameter,(ContainerField, Expression.Field(DispParameter,ContainerField)));
        Debug.Assert(Information.Lambda is not null);
        this._作成_DynamicAssembly.Impl作成(Information.Lambda,DispParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter);
        Information.CreateImplType();
    }
    /// <summary>
    /// 動的な型しかないときにTSQLを最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <param name="Container"></param>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public Func<object> CreateDelegate(Container Container,string SQL) {
        var TSqlFragment = this.SQLからTSqlFragment(SQL);
        this._変換_TSqlFragment正規化.実行(TSqlFragment);
        var Body = this._変換_TSqlFragmentからExpression.実行(Container,TSqlFragment);
        var Lambda = this.Lambda最適化(Expression.Lambda(Body,Array.Empty<ParameterExpression>()));
        return (Func<object>)this.DynamicMethod(Container.GetType(),Lambda);
    }
    /// <summary>
    /// 最適化した式木を返す
    /// </summary>
    /// <param name="Expression"></param>
    /// <returns></returns>
    public Expression CreateExpression(Expression Expression) =>
        this.Lambda最適化(Expression);
    /// <summary>
    /// 非最適化したDelegateを返す
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Delegate Create非最適化Delegate(LambdaExpression Lambda){
        if(this.IsGenerateAssembly)
            return this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda);
        else
            return this.DynamicMethod(typeof(object),Lambda);
    }

    /// <summary>
    /// 最適化したたDelegateを返す
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Delegate CreateDelegate(LambdaExpression Lambda) =>
        this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Action<T> CreateDelegate<T>(Expression<Action<T>> Lambda) =>
        (Action<T>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Action CreateDelegate(Expression<Action> Lambda) =>
        (Action)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<TResult> CreateDelegate<TResult>(Expression<Func<TResult>> Lambda) =>
        (Func<TResult>)this.PrivateDelegate(Lambda);

    public Delegate CreateServerDelegate(LambdaExpression Lambda) {
        this.取得ラムダを跨ぐParameter.実行(Lambda);
        if(this.IsGenerateAssembly)
            return this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda);
        else
            return this.DynamicMethod(typeof(object),Lambda);
    }
    private Delegate PrivateDelegate(LambdaExpression Lambda) {
        var Lambda0 = this.Lambda最適化(Lambda);
        this.取得ラムダを跨ぐParameter.実行(Lambda0);
        //dynamic NonPublicAccessor = new NonPublicAccessor(Lambda0);
        //Trace.WriteLine((string)NonPublicAccessor.DebugView);
        if(this.IsGenerateAssembly)
            return this.DynamicAssemblyとDynamicMethod(typeof(object),Lambda0);
        else
            return this.DynamicMethod(typeof(object),Lambda0);
    }

    public bool IsInline {
        get => this.変換_局所Parameterの先行評価.IsInline;
        set {
            this.変換_跨ぎParameterの先行評価.IsInline=value;
            this.変換_局所Parameterの先行評価.IsInline=value;
        }
    }
    public bool IsProfiling{ get; set; }
    public bool IsGenerateAssembly { get; set; }
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T,TResult> CreateDelegate<T, TResult>(Expression<Func<T,TResult>> Lambda) =>
        (Func<T,TResult>)this.PrivateDelegate(Lambda);
    public Func<TContainer,TResult> CreateContainerDelegate<TContainer, TResult>(Expression<Func<TContainer,TResult>> Lambda) where TContainer : Container =>
        (Func<TContainer,TResult>)this.PrivateDelegate(Lambda);
    //public Func<TContainer,T1,TResult> CreateDelegate<TContainer,T1,TResult>(Expression<Func<TContainer,T1,TResult>> Lambda)where TContainer:Container=>
    //    (Func<TContainer,T1,TResult>)this.PrivateDelegate(typeof(object),Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T1,T2,TResult> CreateDelegate<T1, T2, TResult>(Expression<Func<T1,T2,TResult>> Lambda) =>
        (Func<T1,T2,TResult>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T1,T2,T3,TResult> CreateDelegate<T1, T2, T3, TResult>(Expression<Func<T1,T2,T3,TResult>> Lambda) =>
        (Func<T1,T2,T3,TResult>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// 式木を最適化してコンパイルしてデリゲートを作る。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Lambda"></param>
    /// <returns></returns>
    public Func<T1,T2,T3,T4,TResult> CreateDelegate<T1, T2, T3, T4, TResult>(Expression<Func<T1,T2,T3,T4,TResult>> Lambda) =>
        (Func<T1,T2,T3,T4,TResult>)this.PrivateDelegate(Lambda);
    /// <summary>
    /// コンパイルした時のアセンブリファイル名
    /// </summary>
    public string? AssemblyFileName { get; set; }
    private Type Dynamicに対応するFunc(DynamicExpression Dynamic) {
        var Dynamic_Arguments = Dynamic.Arguments;
        var Dynamic_Arguments_Count = Dynamic_Arguments.Count;
        var Types_Length = Dynamic_Arguments_Count+2;
        var Types = new Type[Types_Length];
        Types[0]=typeof(CallSite);
        for(var a = 0;a<Dynamic_Arguments_Count;a++) Types[a+1]=Dynamic_Arguments[a].Type;
        Types[Dynamic_Arguments_Count+1]=Dynamic.Type;
        return Reflection.Func.Get(Dynamic_Arguments_Count+1).MakeGenericType(Types);
    }
    private Type Dynamicに対応するCallSite(DynamicExpression Dynamic) => this.作業配列.MakeGenericType(typeof(CallSite<>),this.Dynamicに対応するFunc(Dynamic));
    private Delegate DynamicAssemblyとDynamicMethod(Type ContainerType,LambdaExpression Lambda1) {
        //Lambda1=this.Lambda最適化(Lambda1);
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var Name = Lambda1.Name??"Disp";
        var AssemblyName = new AssemblyName { Name=Name };
        var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
        var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
        var Impl_TypeBuilder = Disp_TypeBuilder.DefineNestedType("Impl",TypeAttributes.NestedPublic|TypeAttributes.Sealed|TypeAttributes.Abstract);
        var Container_FieldBuilder = Disp_TypeBuilder.DefineField("Container",ContainerType,FieldAttributes.Public);
        var Disp_ctor = Disp_TypeBuilder.DefineConstructor(MethodAttributes.Public,CallingConventions.HasThis,this.作業配列.Types設定(ContainerType));
        Disp_ctor.InitLocals=false;
        Disp_ctor.DefineParameter(1,ParameterAttributes.None,"Container");
        var Disp_ctor_I = Disp_ctor.GetILGenerator();
        Disp_ctor_I.Ldarg_0();
        Disp_ctor_I.Ldarg_1();
        Disp_ctor_I.Stfld(Container_FieldBuilder);
        Debug.Assert(Disp_TypeBuilder is not null);
        var 作業配列 = this.作業配列;
        var Field番号 = 0;
        foreach(var a in DictionaryConstant.AsEnumerable())
            DictionaryConstant[a.Key]=(Disp_TypeBuilder.DefineField($"Constant{Field番号++}",a.Key.Type,FieldAttributes.Public)!, default!);
        foreach(var a in DictionaryDynamic.AsEnumerable())
            DictionaryDynamic[a.Key]=(Disp_TypeBuilder.DefineField($"Dynamic{Field番号++}",this.Dynamicに対応するCallSite(a.Key),FieldAttributes.Public)!, default!);
        var 判定InstanceMethodか = this.判定InstanceMethodか;
        var Types2 = 作業配列.Types2;
        Types2[0]=typeof(object);
        Types2[1]=typeof(IntPtr);
        foreach(var a in DictionaryLambda.AsEnumerable()) {
            var Lambda = a.Key;
            var LambdaParameters = Lambda.Parameters;
            var LambdaParametersCount = LambdaParameters.Count;
            var インスタンスメソッドか = 判定InstanceMethodか.実行(Lambda.Body)|true;
            Type[] DispTypes, ImplTypes;
            if(インスタンスメソッドか) {
                DispTypes=new Type[LambdaParametersCount];
                ImplTypes=new Type[LambdaParametersCount+1];
                ImplTypes[0]=Disp_TypeBuilder;
                for(var b = 0;b<LambdaParametersCount;b++) {
                    var 元Parameter = LambdaParameters[b];
                    var Type = 元Parameter.IsByRef ? 元Parameter.Type.MakeByRefType() : 元Parameter.Type;
                    DispTypes[b+0]=Type;
                    ImplTypes[b+1]=Type;
                }
            } else {
                DispTypes=ImplTypes=new Type[LambdaParametersCount];
                for(var b = 0;b<LambdaParametersCount;b++) {
                    var 元Parameter = LambdaParameters[b];
                    ImplTypes[b]=元Parameter.IsByRef ? 元Parameter.Type.MakeByRefType() : 元Parameter.Type;
                }
            }
            var Disp_MethodBuilder = Disp_TypeBuilder.DefineMethod($"Disp_Method{Field番号}",MethodAttributes.Public,Lambda.ReturnType,DispTypes);
            Disp_MethodBuilder.InitLocals=false;
            var Impl_MethodBuilder = Impl_TypeBuilder.DefineMethod($"Impl_Method{Field番号}",MethodAttributes.Public|MethodAttributes.Static,Lambda.ReturnType,ImplTypes);
            Impl_MethodBuilder.InitLocals=false;
            var Disp_MethodBuilder_I = Disp_MethodBuilder.GetILGenerator();
            int Disp_index = 1, Impl_index;
            if(インスタンスメソッドか) {
                Disp_MethodBuilder_I.Ldarg_0();
                Impl_MethodBuilder.DefineParameter(1,ParameterAttributes.None,"Disp");
                Impl_index=2;
            } else {
                Impl_index=1;
            }
            ushort Index = 1;
            for(var B2 = 0;B2<LambdaParametersCount;B2++) {
                Disp_MethodBuilder_I.Ldarg(Index++);
                var ParameterName = LambdaParameters[B2].Name;
                Disp_MethodBuilder.DefineParameter(Disp_index++,ParameterAttributes.None,ParameterName);
                Impl_MethodBuilder.DefineParameter(Impl_index++,ParameterAttributes.None,ParameterName);
            }
            Disp_MethodBuilder_I.Call(Impl_MethodBuilder);
            Disp_MethodBuilder_I.Ret();
            var Delegate = Disp_TypeBuilder.DefineField($"Delegate{Field番号}",Lambda.Type,FieldAttributes.Public);
            Disp_ctor_I.Ldarg_0();
            Disp_ctor_I.Ldarg_0();
            Disp_ctor_I.Ldftn(Disp_MethodBuilder);
            Disp_ctor_I.Newobj(Lambda.Type.GetConstructor(Types2)!);
            Disp_ctor_I.Stfld(Delegate);
            DictionaryLambda[a.Key]=(Delegate, default!, Impl_MethodBuilder);
            Field番号++;
        }
        Disp_ctor_I.Ret();
        var 跨番号 = 0;
        foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable())
            Dictionaryラムダ跨ぎParameter[a.Key]=(Disp_TypeBuilder.DefineField(a.Key.Name??$"[跨]{跨番号++}",a.Key.Type,FieldAttributes.Public), default!);
        //Disp作成
        var Disp_Type = Disp_TypeBuilder.CreateType();
        var DispParameter = Expression.Parameter(Disp_Type,"Disp");
        {
            var 番号 = 0;
            foreach(var a in DictionaryConstant.AsEnumerable()) {
                Debug.Assert($"Constant{番号}"==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryConstant[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
            foreach(var a in DictionaryDynamic.AsEnumerable()) {
                Debug.Assert($"Dynamic{番号}"==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryDynamic[a.Key]=(Field, Expression.Field(DispParameter,Field));
                Debug.Assert(this.Dynamicに対応するCallSite(a.Key)==Field.FieldType);
                Debug.Assert(this.Dynamicに対応するCallSite(a.Key)==Expression.Field(DispParameter,Field).Type);
            }
            foreach(var a in DictionaryLambda.AsEnumerable()) {
                Debug.Assert($"Delegate{番号}"==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                DictionaryLambda[a.Key]=(
                    Field,
                    Expression.Field(DispParameter,Field),
                    a.Value.Impl
                );
            }
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable()) {
                Debug.Assert(a.Key.Name is null||a.Key.Name==a.Value.Disp.Name);
                番号++;
                var Field = Disp_Type.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!;
                Dictionaryラムダ跨ぎParameter[a.Key]=(Field, Expression.Field(DispParameter,Field));
            }
        }
        this._作成_DynamicAssembly.Impl作成(Lambda1,DispParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter);
        Debug.Assert(Disp_Type.GetField("Container",Instance_NonPublic_Public) is not null);
        var (Tuple, TupleParameter)=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(ContainerType);
        {
            var _ = Impl_TypeBuilder.CreateType();
            //todo AssemblyGenerater.GenerateAssembly()の後GC.Collect()とGC.WaitForPendingFinalizers()することでファイルハンドルをファイナライザで解放させることを期待したがだダメだった
            //var t=Stopwatch.StartNew();
            //Console.Write("GenerateAssembly,");
            //new AssemblyGenerator()をフィールドに保存すると２度目以降前回のアセンブリ情報が残る
            var Save=typeof(AssemblyBuilder).GetMethod("Save",BindingFlags.NonPublic|BindingFlags.Instance);
            var 試行回数=0;
            for(var a=0;a<10000;a++){
                try{
                    new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Environment.CurrentDirectory}\{Name}.dll");
                    break;
                } catch(IOException){
                    試行回数++;
                }
            }
            if(最大の試行回数<試行回数) 最大の試行回数=試行回数;
            //this.AssemblyGenerator.GenerateAssembly(DynamicAssembly,@$"{Folder}\{Name}.dll");
            //Console.WriteLine($"GenerateAssembly {t.ElapsedMilliseconds}ms");
        }
        this._作成_DynamicMethod.Impl作成(Lambda1,TupleParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter,Tuple);
        var Value = Get_ValueTuple(DictionaryLambda[Lambda1].Member,Tuple);
        var Delegate1 = (Delegate)Value;
        return Delegate1;
    }
    private static int 最大の試行回数=0;
    //private readonly AssemblyGenerator AssemblyGenerator=new();
    /// <summary>
    /// 動的ラムダ。
    /// </summary>
    /// <param name="ContainerType"></param>
    /// <param name="Lambda1"></param>
    /// <returns></returns>
    private Delegate DynamicMethod(Type ContainerType,LambdaExpression Lambda1) {
        //var Lambda1=this.Lambda最適化(Lambda);
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var (Tuple, TupleParameter)=this.DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(ContainerType);

        this._作成_DynamicMethod.Impl作成(Lambda1,TupleParameter,DictionaryConstant,DictionaryDynamic,DictionaryLambda,Dictionaryラムダ跨ぎParameter,Tuple);
        var Value = Get_ValueTuple(DictionaryLambda[Lambda1].Member,Tuple);
        var Delegate1 = (Delegate)Value;
        return Delegate1;
    }

    private (object Tuple, ParameterExpression TupleParameter) DynamicAssemblyとDynamicMethod_DynamicMethodの共通処理1(Type ContainerType) {
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var TargetFieldType数 = 1+DictionaryConstant.Count+DictionaryDynamic.Count+DictionaryLambda.Count+Dictionaryラムダ跨ぎParameter.Count;
        var FieldTypes = new Type[TargetFieldType数];
        {
            FieldTypes[0]=ContainerType;
            var index = 1;
            foreach(var a in DictionaryConstant.Keys)
                FieldTypes[index++]=a.Type;
            foreach(var a in DictionaryDynamic.AsEnumerable())
                FieldTypes[index++]=this.Dynamicに対応するCallSite(a.Key);
            foreach(var a in DictionaryLambda.Keys)
                FieldTypes[index++]=a.Type;
            foreach(var a in Dictionaryラムダ跨ぎParameter.Keys)
                FieldTypes[index++]=a.Type;
        }
        //末尾再帰をループで処理
        var 作業配列 = this.作業配列;
        var Switch = TargetFieldType数%7;
        var Offset = TargetFieldType数-Switch;
        Type DispType;
        if(TargetFieldType数<8) {
            DispType=Switch switch {
                1 => 作業配列.MakeGenericType(typeof(ClassTuple<>),FieldTypes[0]),
                2 => 作業配列.MakeGenericType(typeof(ClassTuple<,>),FieldTypes[0],FieldTypes[1]),
                3 => 作業配列.MakeGenericType(typeof(ClassTuple<,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2]),
                4 => 作業配列.MakeGenericType(typeof(ClassTuple<,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3]),
                5 => 作業配列.MakeGenericType(typeof(ClassTuple<,,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4]),
                6 => 作業配列.MakeGenericType(typeof(ClassTuple<,,,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4],FieldTypes[5]),
                _ => 作業配列.MakeGenericType(typeof(ClassTuple<,,,,,,>),FieldTypes[0],FieldTypes[1],FieldTypes[2],FieldTypes[3],FieldTypes[4],FieldTypes[5],FieldTypes[6])
            };
        } else {
            //Switch 16%7=2
            //Offset 16-2=14
            //1,2,3,4,5,6,7,(8,9,10)
            //1,2,3,4,5,6,7,(8,9,10,11,12,13,14,15,(16))
            //0,1,2,3,4,5,6,(7,8,9,10,11,12,13,(14,15))16個
            //0,1,2,3,4,5,6,(7,8,9,10,11,12,13,(14,15,16,17,18,19,20))21個
            DispType=Switch switch {
                1 => 作業配列.MakeGenericType(typeof(ValueTuple<>),FieldTypes[Offset+0]),
                2 => 作業配列.MakeGenericType(typeof(ValueTuple<,>),FieldTypes[Offset+0],FieldTypes[Offset+1]),
                3 => 作業配列.MakeGenericType(typeof(ValueTuple<,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2]),
                4 => 作業配列.MakeGenericType(typeof(ValueTuple<,,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3]),
                5 => 作業配列.MakeGenericType(typeof(ValueTuple<,,,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4]),
                6 => 作業配列.MakeGenericType(typeof(ValueTuple<,,,,,>),FieldTypes[Offset+0],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4],FieldTypes[Offset+5]),
                _ => 作業配列.MakeGenericType(typeof(ValueTuple<,,,,,,>),FieldTypes[Offset-=7],FieldTypes[Offset+1],FieldTypes[Offset+2],FieldTypes[Offset+3],FieldTypes[Offset+4],FieldTypes[Offset+5],FieldTypes[Offset+6])
            };
            var Types8 = 作業配列.Types8;
            while((Offset-=7)>=0) {
                Debug.Assert(Offset%7==0);
                Types8[0]=FieldTypes[Offset+0];
                Types8[1]=FieldTypes[Offset+1];
                Types8[2]=FieldTypes[Offset+2];
                Types8[3]=FieldTypes[Offset+3];
                Types8[4]=FieldTypes[Offset+4];
                Types8[5]=FieldTypes[Offset+5];
                Types8[6]=FieldTypes[Offset+6];
                Types8[7]=DispType;
                DispType=(Offset==0 ? typeof(ClassTuple<,,,,,,,>) : typeof(ValueTuple<,,,,,,,>)).MakeGenericType(Types8);
            }
        }
        Debug.Assert(DispType.IsClass);
        var Disp = Activator.CreateInstance(DispType)!;
        var DispParameter = Expression.Parameter(DispType,"Tuple");
        this.DispParameter=DispParameter;
        {
            Expression TupleExpression = DispParameter;
            var DispType0 = DispType;
            var Disp0 = Disp;
            var Item番号 = 2;//1はContainerが入る
            foreach(var a in DictionaryConstant.AsEnumerable())
                DictionaryConstant[a.Key]=(default!, ValueTuple_Item0(ref Disp0,Item番号++,TupleExpression,a.Key.Value));
            foreach(var a in DictionaryDynamic.AsEnumerable()) {
                var Dynamic = a.Key;
                var CallSite0 = CallSite.Create(this.Dynamicに対応するFunc(Dynamic),Dynamic.Binder);
                DictionaryDynamic[Dynamic]=(default!, ValueTuple_Item0(ref Disp0,Item番号++,TupleExpression,CallSite0));
            }
            foreach(var a in DictionaryLambda.AsEnumerable())
                DictionaryLambda[a.Key]=(default!, ValueTuple_Item1(ref Disp0,Item番号++,TupleExpression), default!);
            foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable())
                Dictionaryラムダ跨ぎParameter[a.Key]=(default!, ValueTuple_Item1(ref Disp0,Item番号++,TupleExpression));
        }
        return (Disp, DispParameter);
        static MemberExpression ValueTuple_Item0(ref object TupleValue,int Item番号,Expression TupleExpression,object Value) {
            if(Item番号<8){
                (var Member,TupleValue)=共通(Item番号,TupleValue,Value);
                return Member;
            } else {
                //return 共通(Switch);
                var TupleType=TupleValue.GetType();
                var Rest = TupleType.GetField("Rest");
                Debug.Assert(Rest!=null);
                TupleType=Rest.FieldType;
                var Rest_Item = Rest.GetValue(TupleValue);
                Debug.Assert(Rest_Item!=null);
                //TupleValue=Rest_Item;
                TupleExpression=Expression.Field(TupleExpression,Rest);
                var Rest0_Item=Rest_Item;
                var Member=ValueTuple_Item0(ref Rest0_Item,Item番号-7,TupleExpression,Value);
                Rest.SetValue(TupleValue,Rest0_Item);
                return Member;
            }
            (MemberExpression Member,object TupleValue)共通(int Item番号0,object TupleValue0,object Value0){
                var TupleField=TupleValue0.GetType().GetField($"Item{Item番号0}");
                Debug.Assert(TupleField!=null);
                TupleField.SetValue(TupleValue0,Value0);
                return (Expression.Field(TupleExpression,TupleField),TupleValue0);
            }
        }
        static MemberExpression ValueTuple_Item1(ref object TupleValue,int Item番号,Expression TupleExpression) {
            if(Item番号<8){
                var TupleField=TupleValue.GetType().GetField($"Item{Item番号}");
                return Expression.Field(TupleExpression,TupleField);
            }else{
                //var Rest = TupleValue.GetType().GetField("Rest");
                //Debug.Assert(Rest!=null);
                ////TupleType=Rest.FieldType;
                //var Value0 = Rest.GetValue(TupleValue);
                //Debug.Assert(Value0!=null);
                //TupleValue=Value0;
                //TupleExpression=Expression.Field(TupleExpression,Rest);
                //Item番号=1;
                var TupleType=TupleValue.GetType();
                var Rest = TupleType.GetField("Rest");
                Debug.Assert(Rest!=null);
                TupleType=Rest.FieldType;
                var Rest_Item = Rest.GetValue(TupleValue);
                Debug.Assert(Rest_Item!=null);
                //TupleValue=Rest_Item;
                TupleExpression=Expression.Field(TupleExpression,Rest);
                var Rest0_Item=Rest_Item;
                var Member=ValueTuple_Item1(ref Rest0_Item,Item番号-7,TupleExpression);
                return Member;
            }
        }
        //static MemberExpression ValueTuple_Item0(ref Type TupleType,ref object TupleValue,ref int Item番号,ref Expression TupleExpression,object Value) {
        //    if(Item番号==8) {
        //        var Rest = TupleType.GetField("Rest");
        //        Debug.Assert(Rest!=null);
        //        TupleType=Rest.FieldType;
        //        var Value0 = Rest.GetValue(TupleValue);
        //        Debug.Assert(Value0!=null);
        //        TupleValue=Value0;
        //        TupleExpression=Expression.Field(TupleExpression,Rest);
        //        Item番号=1;
        //    }
        //    var TupleField = TupleType.GetField($"Item{Item番号++}");
        //    Debug.Assert(TupleField!=null);
        //    TupleField.SetValue(TupleValue,Value);
        //    return Expression.Field(TupleExpression,TupleField);
        //}
        //static MemberExpression ValueTuple_Item1(ref Type TupleType,ref object TupleValue,ref int Item番号,ref Expression TupleExpression) {
        //    if(Item番号==8) {
        //        var Rest = TupleType.GetField("Rest");
        //        Debug.Assert(Rest!=null);
        //        TupleType=Rest.FieldType;
        //        var Value0 = Rest.GetValue(TupleValue);
        //        Debug.Assert(Value0!=null);
        //        TupleValue=Value0;
        //        TupleExpression=Expression.Field(TupleExpression,Rest);
        //        Item番号=1;
        //    }
        //    var TupleField = TupleType.GetField($"Item{Item番号++}");
        //    return Expression.Field(TupleExpression,TupleField);
        //}
    }
    internal static class DynamicReflection {
        public static readonly RuntimeBinder.CSharpArgumentInfo CSharpArgumentInfo = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null);
        public static RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray(int Count) {
            var Array = new RuntimeBinder.CSharpArgumentInfo[Count];
            for(var a = 0;a<Count;a++)
                Array[a]=CSharpArgumentInfo;
            return Array;
        }
        public static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray1 = { CSharpArgumentInfo };
        public static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray2 = { CSharpArgumentInfo,CSharpArgumentInfo };
        public static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray3 = { CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo };
        public static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray4 = { CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo,CSharpArgumentInfo };
        public static class CallSites {
            public static readonly FieldInfo ObjectObjectObjectObjectTarget = typeof(CallSite<Func<CallSite,object,object,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object,object,object>>.Target))!;
            public static readonly FieldInfo ObjectObjectObjectTarget = typeof(CallSite<Func<CallSite,object,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object,object>>.Target))!;
            public static readonly FieldInfo ObjectObjectTarget = typeof(CallSite<Func<CallSite,object,object>>).GetField(nameof(CallSite<Func<CallSite,object,object>>.Target))!;
            public static readonly FieldInfo ObjectBooleanTarget = typeof(CallSite<Func<CallSite,object,bool>>).GetField(nameof(CallSite<Func<CallSite,object,bool>>.Target))!;

            private static CallSite<Func<CallSite,object,object,object>> CallSite_Binary(ExpressionType NodeType) => CallSite<Func<CallSite,object,object,object>>.Create(RuntimeBinder.Binder.BinaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray2));
            public static readonly CallSite<Func<CallSite,object,object,object>> Add = CallSite_Binary(ExpressionType.Add);
            public static readonly CallSite<Func<CallSite,object,object,object>> AddAssign = CallSite_Binary(ExpressionType.AddAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> And = CallSite_Binary(ExpressionType.And);
            public static readonly CallSite<Func<CallSite,object,object,object>> AndAssign = CallSite_Binary(ExpressionType.AndAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Divide = CallSite_Binary(ExpressionType.Divide);
            public static readonly CallSite<Func<CallSite,object,object,object>> DivideAssign = CallSite_Binary(ExpressionType.DivideAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Equal = CallSite_Binary(ExpressionType.Equal);
            public static readonly CallSite<Func<CallSite,object,object,object>> ExclusiveOr = CallSite_Binary(ExpressionType.ExclusiveOr);
            public static readonly CallSite<Func<CallSite,object,object,object>> ExclusiveOrAssign = CallSite_Binary(ExpressionType.ExclusiveOrAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> GreaterThan = CallSite_Binary(ExpressionType.GreaterThan);
            public static readonly CallSite<Func<CallSite,object,object,object>> GreaterThanOrEqual = CallSite_Binary(ExpressionType.GreaterThanOrEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> LeftShift = CallSite_Binary(ExpressionType.LeftShift);
            public static readonly CallSite<Func<CallSite,object,object,object>> LeftShiftAssign = CallSite_Binary(ExpressionType.LeftShiftAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> LessThan = CallSite_Binary(ExpressionType.LessThan);
            public static readonly CallSite<Func<CallSite,object,object,object>> LessThanOrEqual = CallSite_Binary(ExpressionType.LessThanOrEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> Modulo = CallSite_Binary(ExpressionType.Modulo);
            public static readonly CallSite<Func<CallSite,object,object,object>> ModuloAssign = CallSite_Binary(ExpressionType.ModuloAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Multiply = CallSite_Binary(ExpressionType.Multiply);
            public static readonly CallSite<Func<CallSite,object,object,object>> MultiplyAssign = CallSite_Binary(ExpressionType.MultiplyAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> NotEqual = CallSite_Binary(ExpressionType.NotEqual);
            public static readonly CallSite<Func<CallSite,object,object,object>> Or = CallSite_Binary(ExpressionType.Or);
            public static readonly CallSite<Func<CallSite,object,object,object>> OrAssign = CallSite_Binary(ExpressionType.OrAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> RightShift = CallSite_Binary(ExpressionType.RightShift);
            public static readonly CallSite<Func<CallSite,object,object,object>> RightShiftAssign = CallSite_Binary(ExpressionType.RightShiftAssign);
            public static readonly CallSite<Func<CallSite,object,object,object>> Subtract = CallSite_Binary(ExpressionType.Subtract);
            public static readonly CallSite<Func<CallSite,object,object,object>> SubtractAssign = CallSite_Binary(ExpressionType.SubtractAssign);
            private static CallSite<T> CallSite_Unary<T>(ExpressionType NodeType) where T : class => CallSite<T>.Create(RuntimeBinder.Binder.UnaryOperation(RuntimeBinder.CSharpBinderFlags.None,NodeType,typeof(DynamicReflection),CSharpArgumentInfoArray1));
            private static CallSite<Func<CallSite,object,object>> CallSite_Unary(ExpressionType NodeType) => CallSite_Unary<Func<CallSite,object,object>>(NodeType);
            public static readonly CallSite<Func<CallSite,object,object>> Decrement = CallSite_Unary(ExpressionType.Decrement);
            public static readonly CallSite<Func<CallSite,object,object>> Increment = CallSite_Unary(ExpressionType.Increment);
            public static readonly CallSite<Func<CallSite,object,object>> Negate = CallSite_Unary(ExpressionType.Negate);
            public static readonly CallSite<Func<CallSite,object,object>> Not = CallSite_Unary(ExpressionType.Not);
            public static readonly CallSite<Func<CallSite,object,object>> OnesComplement = CallSite_Unary(ExpressionType.OnesComplement);
            public static readonly CallSite<Func<CallSite,object,object>> UnaryPlus = CallSite_Unary(ExpressionType.UnaryPlus);
            private static CallSite<Func<CallSite,object,bool>> CallSite_IsFalse_IsTrue(ExpressionType NodeType) => CallSite_Unary<Func<CallSite,object,bool>>(NodeType);
            public static readonly CallSite<Func<CallSite,object,bool>> IsFalse = CallSite_IsFalse_IsTrue(ExpressionType.IsFalse);
            public static readonly CallSite<Func<CallSite,object,bool>> IsTrue = CallSite_IsFalse_IsTrue(ExpressionType.IsTrue);
            public static readonly CallSite<Func<CallSite,object,object,object>> GetIndex = CallSite<Func<CallSite,object,object,object>>.Create(
                RuntimeBinder.Binder.GetIndex(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(DynamicReflection),
                    CSharpArgumentInfoArray2
                )
            );
            public static readonly CallSite<Func<CallSite,object,object,object,object>> SetIndex = CallSite<Func<CallSite,object,object,object,object>>.Create(
                RuntimeBinder.Binder.SetIndex(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(DynamicReflection),
                    CSharpArgumentInfoArray3
                )
            );
        }

        private static (FieldInfo Target, MethodInfo Invoke) F(Type CallSite) {
            var Target = CallSite.GetField("Target");
            Debug.Assert(Target is not null);
            var Invoke = Target.FieldType.GetMethod("Invoke");
            Debug.Assert(Invoke is not null);
            return (Target, Invoke);
        }
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectObjectObjectObject = F(typeof(CallSite<Func<CallSite,object,object,object,object>>));
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectObjectObject = F(typeof(CallSite<Func<CallSite,object,object,object>>));
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectObject = F(typeof(CallSite<Func<CallSite,object,object>>));
        public static readonly (FieldInfo Target, MethodInfo Invoke) ObjectBoolea = F(typeof(CallSite<Func<CallSite,object,bool>>));
        private static FieldInfo F(string フィールド名) => typeof(CallSites).GetField(フィールド名,Static_NonPublic_Public)!;
        public static readonly FieldInfo Add = F(nameof(Add));
        public static readonly FieldInfo AddAssign = F(nameof(AddAssign));
        public static readonly FieldInfo And = F(nameof(And));
        public static readonly FieldInfo AndAssign = F(nameof(AndAssign));
        public static readonly FieldInfo Divide = F(nameof(Divide));
        public static readonly FieldInfo DivideAssign = F(nameof(DivideAssign));
        public static readonly FieldInfo Equal = F(nameof(Equal));
        public static readonly FieldInfo ExclusiveOr = F(nameof(ExclusiveOr));
        public static readonly FieldInfo ExclusiveOrAssign = F(nameof(ExclusiveOrAssign));
        public static readonly FieldInfo GreaterThan = F(nameof(GreaterThan));
        public static readonly FieldInfo GreaterThanOrEqual = F(nameof(GreaterThanOrEqual));
        public static readonly FieldInfo LeftShift = F(nameof(LeftShift));
        public static readonly FieldInfo LeftShiftAssign = F(nameof(LeftShiftAssign));
        public static readonly FieldInfo LessThan = F(nameof(LessThan));
        public static readonly FieldInfo LessThanOrEqual = F(nameof(LessThanOrEqual));
        public static readonly FieldInfo Modulo = F(nameof(Modulo));
        public static readonly FieldInfo ModuloAssign = F(nameof(ModuloAssign));
        public static readonly FieldInfo Multiply = F(nameof(Multiply));
        public static readonly FieldInfo MultiplyAssign = F(nameof(MultiplyAssign));
        public static readonly FieldInfo NotEqual = F(nameof(NotEqual));
        public static readonly FieldInfo Or = F(nameof(Or));
        public static readonly FieldInfo OrAssign = F(nameof(OrAssign));
        public static readonly FieldInfo RightShift = F(nameof(RightShift));
        public static readonly FieldInfo RightShiftAssign = F(nameof(RightShiftAssign));
        public static readonly FieldInfo Subtract = F(nameof(Subtract));
        public static readonly FieldInfo SubtractAssign = F(nameof(SubtractAssign));

        public static readonly FieldInfo Decrement = F(nameof(Decrement));
        public static readonly FieldInfo Increment = F(nameof(Increment));
        public static readonly FieldInfo Negate = F(nameof(Negate));
        public static readonly FieldInfo Not = F(nameof(Not));
        public static readonly FieldInfo OnesComplement = F(nameof(OnesComplement));
        public static readonly FieldInfo UnaryPlus = F(nameof(UnaryPlus));
        public static readonly FieldInfo IsFalse = F(nameof(IsFalse));
        public static readonly FieldInfo IsTrue = F(nameof(IsTrue));

        public static readonly FieldInfo GetIndex = F(nameof(GetIndex));
        public static readonly FieldInfo SetIndex = F(nameof(SetIndex));
    }

    //private static string 整形したDebugView(Expression Lambda) {
    //    var 旧String = CommonLibrary.インラインラムダテキスト(Lambda);
    //    var 新StringBuilder = new StringBuilder();
    //    //var キー定義 = new Regex(@"^\..*>\( \{$",RegexOptions.Singleline);
    //    var キー本体 = new Regex(@"^\..*\}$",RegexOptions.Singleline);
    //    {
    //        var キー参照 = new Regex(@"\..*>\)",RegexOptions.Multiline);
    //        for(var キー定義_Match = キー本体.Match(旧String);キー定義_Match.Success;キー定義_Match=キー定義_Match.NextMatch()) {
    //            var 前index = 0;
    //            for(var キー参照_Match = キー参照.Match(旧String);キー参照_Match.Success;キー参照_Match=キー参照_Match.NextMatch()) {
    //                var 後index = キー参照_Match.Index;
    //                新StringBuilder.Append(旧String[前index..後index]);
    //                前index=後index;
    //                新StringBuilder.Append(キー定義_Match.Value);
    //                新StringBuilder.Append(')');
    //            }
    //            旧String=新StringBuilder.ToString();
    //            新StringBuilder.Clear();
    //        }
    //    }
    //    {
    //        var キー参照 = new Regex(@"\..*>,",RegexOptions.Multiline);
    //        for(var キー定義_Match = キー本体.Match(旧String);キー定義_Match.Success;キー定義_Match=キー定義_Match.NextMatch()) {
    //            var 前index = 0;
    //            for(var キー参照_Match = キー参照.Match(旧String);キー参照_Match.Success;キー参照_Match=キー参照_Match.NextMatch()) {
    //                var 後index = キー参照_Match.Index;
    //                新StringBuilder.Append(旧String[前index..後index]);
    //                前index=後index;
    //                新StringBuilder.Append(キー定義_Match.Value);
    //                新StringBuilder.Append(',');
    //            }
    //            旧String=新StringBuilder.ToString();
    //            新StringBuilder.Clear();
    //        }
    //    }
    //    return 旧String;
    //}
    //private static readonly Stopwatch 標準 = new Stopwatch(), 独自 = new Stopwatch();
    private static void 実行計画文字列のAssert(string[] expecteds,string テキスト化された実行計画) {
        if(expecteds.Length>0) {
            var sb = new StringBuilder();
            foreach(var expected in expecteds)
                sb.AppendLine(expected);
            Debug.Assert(テキスト化された実行計画.Length==sb.Length);
            for(var a = 0;a<テキスト化された実行計画.Length;a++)
                Debug.Assert(テキスト化された実行計画[a]==sb[a]);
        }
    }
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<TResult>(Expression<Func<TResult>> Lambda) {
        var Delegate = this.CreateDelegate(Lambda);
        return Delegate();
    }

    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値</param>
    /// <typeparam name="T">入力値のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T, TResult>(Expression<Func<T,TResult>> Lambda,T Input1) =>
        this.CreateDelegate(Lambda)(Input1);
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値1</param>
    /// <param name="Input2">入力値2</param>
    /// <typeparam name="T1">入力値1のType</typeparam>
    /// <typeparam name="T2">入力値2のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T1, T2, TResult>(Expression<Func<T1,T2,TResult>> Lambda,T1 Input1,T2 Input2) =>
        this.CreateDelegate(Lambda)(Input1,Input2);
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値1</param>
    /// <param name="Input2">入力値2</param>
    /// <param name="Input3">入力値3</param>
    /// <typeparam name="T1">入力値1のType</typeparam>
    /// <typeparam name="T2">入力値2のType</typeparam>
    /// <typeparam name="T3">入力値2のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T1, T2, T3, TResult>(Expression<Func<T1,T2,T3,TResult>> Lambda,T1 Input1,T2 Input2,T3 Input3) =>
        this.CreateDelegate(Lambda)(Input1,Input2,Input3);
    /// <summary>
    /// Lambdaからデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda">デリゲートを表すExpression</param>
    /// <param name="Input1">入力値1</param>
    /// <param name="Input2">入力値2</param>
    /// <param name="Input3">入力値3</param>
    /// <param name="Input4">入力値4</param>
    /// <typeparam name="T1">入力値1のType</typeparam>
    /// <typeparam name="T2">入力値2のType</typeparam>
    /// <typeparam name="T3">入力値3のType</typeparam>
    /// <typeparam name="T4">入力値4のType</typeparam>
    /// <typeparam name="TResult">戻り値のType</typeparam>
    /// <returns>実行結果</returns>
    public TResult Execute<T1, T2, T3, T4, TResult>(Expression<Func<T1,T2,T3,T4,TResult>> Lambda,T1 Input1,T2 Input2,T3 Input3,T4 Input4) =>
        this.CreateDelegate(Lambda)(Input1,Input2,Input3,Input4);
    ///// <summary>
    ///// F#のFSharpExpr(Expr)からデリゲートを作って呼び出す
    ///// </summary>
    ///// <param name="Expr">ラムダを表すexpr</param>
    ///// 
    ///// 
    ///// 
    ///// <typeparam name="TResult">戻り値のType</typeparam>
    ///// <returns>実行結果</returns>
    //public TResult Execute<TResult>(FSharpExpr<TResult> Expr) =>
    //    this.CreateDelegate(
    //        Expression.Lambda<Func<TResult>>(
    //            LeafExpressionConverter.QuotationToExpression(Expr),
    //            Array.Empty<ParameterExpression>()
    //        )
    //    )();
    /// <summary>
    /// Lambdaを最適化してデリゲートを作って呼び出す
    /// </summary>
    /// <param name="Lambda"></param>
    public void Execute(Expression<Action> Lambda) =>
        this.CreateDelegate(Lambda)();
    //private static readonly Regex RegexLambda=new("[#].*{",RegexOptions.Compiled);
    //private static readonly Regex Regex単純な識別子 = new("[^ ,^$,^#,^<,^[,^>,^(]*[.]",RegexOptions.Compiled);
    ///// <summary>
    ///// 式木をわかりやすくテキストにする
    ///// </summary>
    ///// <param name="e"></param>
    ///// <returns></returns>
    //public static string インラインラムダテキスト(Expression e) {
    //    //[^ ]*[.]
    //    dynamic NonPublicAccessor = new NonPublicAccessor(typeof(Expression),e);
    //    var 変換前 = NonPublicAccessor.DebugView;
    //    //変換前=変換前.Replace("LinqDB.Sets.","");
    //    //変換前=変換前.Replace("System.Collections.Generic.","");
    //    //変換前=変換前.Replace("System.Collections.","");
    //    //変換前=変換前.Replace("System.","");
    //    変換前=変換前.Replace("\r\n{","{");
    //    var KeyValues = new Generic.List<(string Key, Generic.List<string> Values)>();
    //    {
    //        var r = new StringReader(変換前);
    //        var ラムダ式定義 = new Generic.List<string>();
    //        var ラムダ定義の1行目 = new Regex(@"^\.Lambda .*\(.*$",RegexOptions.Compiled);
    //        while(true) {
    //            var Line = r.ReadLine();
    //            if(string.IsNullOrEmpty(Line)) {
    //                KeyValues.Add(("", ラムダ式定義));
    //                break;
    //            }
    //            ラムダ式定義.Add(Line);
    //        }
    //        while(true) {
    //            var Line = r.ReadLine();
    //            if(Line is null)
    //                break;
    //            if(ラムダ定義の1行目.IsMatch(Line)) {
    //                var Key = Line[..Line.IndexOf("(",StringComparison.Ordinal)];
    //                ラムダ式定義=new Generic.List<string>();
    //                KeyValues.Add((Key, ラムダ式定義));
    //                ラムダ式定義.Add(Line);
    //            } else if(string.Equals(Line,"}",StringComparison.Ordinal)) {
    //                ラムダ式定義.Add(Line);
    //            } else if(!string.Equals(Line,string.Empty,StringComparison.Ordinal)) {
    //                if(Line[^2]=='>'&&Line[^1]==')') Line=Line.Substring(0,Line.Length-1);
    //                ラムダ式定義.Add(Line);
    //            }
    //        }
    //    }
    //    {
    //        //Debug.Assert(ルートラムダ is not null,"ルートラムダ != null");
    //        //var ルートラムダ2=ルートラムダ;
    //        //再試行:
    //        //var r = new StringReader(ルートラムダ);
    //        //var sb = new StringBuilder();
    //        while(true) {
    //            for(var a = KeyValues.Count-1;a>=0;a--) {
    //                var a_KeyValue = KeyValues[a];
    //                var a_Values = a_KeyValue.Values;
    //                for(var b = a_Values.Count-1;b>=0;b--) {
    //                    var 変換元Line = a_Values[b];
    //                    for(var c = KeyValues.Count-1;c>=0;c--) {
    //                        var 変換先Value = KeyValues[c];
    //                        var 変換先キー = 変換先Value.Key;
    //                        var Lambda位置Index = 変換元Line.IndexOf(変換先キー,StringComparison.Ordinal);
    //                        if(Lambda位置Index>0) {
    //                            var 変換先Values = 変換先Value.Values;
    //                            a_Values[b]=変換元Line[..Lambda位置Index]+変換先Values[0];
    //                            var Index = 変換元Line.TakeWhile(文字 => 文字==' ').Count();
    //                            for(var d = 1;d<変換先Values.Count-1;d++)
    //                                a_Values.Insert(b+d,new string(' ',Index)+変換先Values[d]);
    //                            a_Values.Insert(b+変換先Values.Count-1,new string(' ',Index)+変換先Values[^1]+変換元Line[(Lambda位置Index+変換先キー.Length)..]);
    //                            break;
    //                        }
    //                    }
    //                }
    //            }
    //            var sb = new StringBuilder();
    //            foreach(var Value in KeyValues[0].Values) sb.AppendLine(Value);
    //            //var RegxLambda = new Regex(@"\.Lambda #Lambda.*<",RegexOptions.Compiled);
    //            //foreach(var Value in KeyValues[0].Values) {
    //            //    var Value2 = RegxLambda.Replace(Value,"");
    //            //    if(Value2!=Value) {
    //            //        Value2=Value2.Replace(">(","(");
    //            //    }
    //            //    sb.AppendLine(Value2);
    //            //}
    //            var Result1 = sb.ToString();
    //            var Constant = new Regex(@"\.Constant<.*?>\(.*?\)",RegexOptions.Compiled);
    //            var Result2 = Constant.Replace(Result1,"");
    //            //var Result3 = Result2.Replace("ExtensionSet","");
    //            //var Result4 = Result3.Replace("ExtensionEnumerable","");
    //            //var NodeType= new Regex(@"#Lambda.*\(",RegexOptions.Compiled);
    //            //var Result5 = NodeType.Replace(Result4,"(");
    //            var NodeTypeを除く = new Regex(@" \..*? ",RegexOptions.Compiled);
    //            //var Regx5= new Regex(@"Lambda #Lambda.*<",RegexOptions.Compiled);
    //            var Result5 = NodeTypeを除く.Replace(Result2," ");
    //            //var Result6 = Result5.Replace(" ."," ");
    //            if(Result5[0]=='.') Result5=Result5[1..];
    //            var Result6 = Regex単純な識別子.Replace(Result5,"");
    //            return Result6;
    //        }
    //    }
    //}
    public string 命令ツリー(Expression Expression) => this._取得_命令ツリー.実行(Expression);
    //private static object Set_ValueTuple(object ValueTuple,int Index,object Value) {
    //    switch(Index) {
    //        case 0: ValueTuple.GetType().GetField("Item1")!.SetValue(ValueTuple,Value); break;
    //        case 1: ValueTuple.GetType().GetField("Item2")!.SetValue(ValueTuple,Value); break;
    //        case 2: ValueTuple.GetType().GetField("Item3")!.SetValue(ValueTuple,Value); break;
    //        case 3: ValueTuple.GetType().GetField("Item4")!.SetValue(ValueTuple,Value); break;
    //        case 4: ValueTuple.GetType().GetField("Item5")!.SetValue(ValueTuple,Value); break;
    //        case 5: ValueTuple.GetType().GetField("Item6")!.SetValue(ValueTuple,Value); break;
    //        case 6: ValueTuple.GetType().GetField("Item7")!.SetValue(ValueTuple,Value); break;
    //        default:
    //            var Rest = ValueTuple.GetType().GetField("Rest")!;
    //            Rest.SetValue(
    //                ValueTuple,
    //                Set_ValueTuple(
    //                    Rest.GetValue(ValueTuple)!,
    //                    Index-7,
    //                    Value
    //                )
    //            );
    //            break;
    //    }
    //    return ValueTuple;
    //}
    ///// <summary>
    ///// 最適化レベル
    ///// </summary>
    //public OptimizeLevels OptimizeLevel { get; set; }
    private Type _Context = typeof(object);
    /// <summary>
    /// どのクラス内で実行するか指定
    /// </summary>
    public Type Context {
        get => this._Context;
        set => this._Context=value;
    }
    internal LambdaExpression Lambda非最適化(Expression Lambda00) {
        this.DictionaryConstant.Clear();
        var Lambda02 = this.変換_メソッド正規化_取得インライン不可能定数.実行(Lambda00);
        var Lambda03=this.変換_Tryの先行評価.実行(Lambda02);
        //プロファイル=false;
        //var List計測 = new List<A計測>();
        //var ConstantList計測 = Expression.Constant(List計測);
        //プロファイル=false;
        //if(プロファイル)HashSetConstant.Add(ConstantList計測);
        this._検証_変形状態.実行(Lambda03);
        return (LambdaExpression)Lambda03;
    }
    public LambdaExpression Lambda最適化(Expression Lambda00) {
        var DictionaryConstant = this.DictionaryConstant;
        var DictionaryDynamic = this.DictionaryDynamic;
        var DictionaryLambda = this.DictionaryLambda;
        var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        DictionaryConstant.Clear();
        DictionaryDynamic.Clear();
        DictionaryLambda.Clear();
        Dictionaryラムダ跨ぎParameter.Clear();
        //var List計測 =this.List計測;
        //DictionaryConstant.Add(this.ConstantList計測,default!);
        //分離するアイデア。ここで以下を取得する
        //DictionaryConstant add

        //var Lambda01 = this._変換_KeySelectorの匿名型をValueTuple.実行(Lambda00);
        //以下で更新されるコレクション
        //DictionaryConstant read
        var Lambda02 = this.変換_メソッド正規化_取得インライン不可能定数.実行(Lambda00);
        var Lambda03=this.変換_Tryの先行評価.実行(Lambda02);
        //プロファイル=false;
        //プロファイル=false;
        //if(プロファイル)
        var Lambda04 = this._変換_WhereからLookup.実行(Lambda03);
        //Dictionaryラムダ跨ぎParameter add
        var Lambda05 = this.変換_跨ぎParameterの先行評価.実行(Lambda04);
        //var Lambda06 = this._変換_跨ぎParameterの不要置換復元.実行(Lambda05);
        var Lambda06 = this.変換_局所Parameterの先行評価.実行(Lambda05);
        //List計測.Clear();
        //this.List辺.Clear();
        var Lambda07=Lambda06;
        if(this.IsProfiling)
            Lambda07=this.変換_Stopwatchに埋め込む.実行(Lambda07);
        //Lambda07=Lambda06;
        //Lambda07=Lambda06;
        //Trace.WriteLine(this._変換_Stopwatchに埋め込む.データフローチャート);
        //Trace.WriteLine(this.Analize);
        //Trace.WriteLine(this.命令ツリー(Lambda07));
        //Trace.WriteLine(CommonLibrary.インラインラムダテキスト(Lambda07));
        //Trace.WriteLine(this._変換_Stopwatchに埋め込む.フロー1);

        this._検証_変形状態.実行(Lambda07);
        var Lambda08 = this.IsInline ? this.変換_インラインループ独立.実行(Lambda07) : Lambda07;

        
        
        //var ListスコープParameter=new Generic.List<ParameterExpression>();
        //var ExpressionEqualityComparer=new ExpressionEqualityComparer_Assign_Leftで比較(ListスコープParameter);
        ////var 判定_左辺Parametersが含まれる=new 判定_左辺Expressionsが含まれる(ExpressionEqualityComparer);
        //var Dictionary_LabelTarget_辺に関する情報=new Generic.Dictionary<LabelTarget,辺>();
        //var Top辺=new 辺(ExpressionEqualityComparer){辺番号=0,子コメント = "開始"};
        //var List辺=this.List辺;
        //var 作成_辺=new 作成_辺(Top辺,List辺,Dictionary_LabelTarget_辺に関する情報,ExpressionEqualityComparer);
        //作成_辺.実行(Lambda06);
        //Trace.WriteLine(List辺.フロー);

        //var s=this.命令ツリー(Lambda07);
        //Trace.WriteLine(インラインラムダテキスト(Lambda07));
        //DictionaryDynamic add
        //DictionaryLambda  add
        //Dictionaryラムダ跨ぎParameter read
        //var Lambda09=this._変換_跨ぎParameterをBlock_Variablesに.実行(Lambda08);
        //分離するアイデア。ここで以下を取得する
        //DictionaryLambda,DictionaryDynamic add
        return (LambdaExpression)Lambda08;
    }
}
//2122 20220516
//2708 20220514
//3186 20220513
//2773 20220504
