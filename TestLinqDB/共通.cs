#define MEMORY
#define MESSAGE
#define JSON
using System.Diagnostics;
using Reflection = System.Reflection;
using LinqDB.Optimizers;
using LinqDB.Remote.Servers;
using static LinqDB.Helpers.Configulation;
using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
using LinqDB.Remote.Clients;
using System.Net;
using System.Reflection;
using System.Text;
using LinqDB;
using LinqDB.Helpers;
using LinqDB.Optimizers.Comparer;
using LinqDB.Sets;
using TestLinqDB.Serializers;
using Utf8Json;
namespace TestLinqDB;
[Flags]
public enum テストオプション{
    None=0,
    MemoryPack                                        =0b100000000,
    MessagePack                                       =0b010000000,
    Utf8Json                                          =0b001000000,
    ローカル実行                                      =0b000100000,
    リモート実行                                      =0b000010000,
    ファイルが無ければシリアライズ有ればデシリアライズ=0b000001000,
    最適化                                            =0b000000100,
    インライン                                        =0b000000010,
    アセンブリ保存                                    =0b000000001,
    全て                                              =0b111111111,
    MemoryPack_MessagePack_Utf8Json=MemoryPack|MessagePack|Utf8Json,
}
public abstract class 共通{
    private protected static int ポート番号;
    protected abstract テストオプション テストオプション{get;}
    const string フォルダ="シリアライズテスト";
    static 共通(){
        const string Serialize=nameof(Serialize);
        const string Deserialize=nameof(Deserialize);
        ポート番号=ListenerSocketポート番号;
        const string ファイル=$@"{フォルダ}\今回の操作.txt";
        if(!Directory.Exists(フォルダ))
            Directory.CreateDirectory(フォルダ);
        try{
            var 今回の操作=File.ReadAllText(ファイル);
            今回の操作=今回の操作==Serialize?Deserialize:Serialize;
            File.WriteAllText(ファイル,今回の操作);
        } catch(FileNotFoundException){
            File.WriteAllText(ファイル,Serialize);
        } catch{
            File.WriteAllText(ファイル,Serialize);
        }
    }
    protected 共通(){
        // ReSharper disable once VirtualMemberCallInConstructor
        this.Optimizer=new(){IsGenerateAssembly=(テストオプション.アセンブリ保存&this.テストオプション)!=0,Context=typeof(共通),AssemblyFileName="デバッグ.dll",IsInline = true};

    }
    protected readonly dynamic 変換_局所Parameterの先行評価 = new NonPublicAccessor(
        typeof(LinqDB.Optimizers.ReturnExpressionTraverser.変換_局所Parameterの先行評価).GetConstructor(Type.EmptyTypes)!.Invoke(Array.Empty<object>()));
    /// <summary>
    /// 変換_局所Parameterの先行評価.実行
    /// </summary>
    /// <param name="Body"></param>
    protected void 変換_局所Parameterの先行評価_実行(Expression Body){
        var Lambda=Expression.Lambda(Body);
        this.変換_局所Parameterの先行評価.実行(Lambda);
        //var s0=CommonLibrary.インラインラムダテキスト(Lambda);
        //var s1=CommonLibrary.インラインラムダテキスト((Expression)this.変換_局所Parameterの先行評価.実行(Lambda));
        //Trace.WriteLine(s0);
        //Trace.WriteLine(s1);
        //var Lambda = Expression.Lambda(Expression0);
        //this.Lambda最適化(Lambda );
    }
    private void ServerClient(Action<Client>Action){
        const int receiveTimeout = 1000;
        var port=Interlocked.Increment(ref ポート番号);
        using var Server=new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var Client=new Client(Dns.GetHostName(),port);
        Action(Client);
        Server.Close();
    }
    //protected readonly 汎用Comparer 汎用Comparer;
    protected readonly LinqDB.Serializers.Utf8Json.Serializer Utf8Json=new();
    protected readonly LinqDB.Serializers.MessagePack.Serializer MessagePack=new();
    protected readonly LinqDB.Serializers.MemoryPack.Serializer MemoryPack=new();
    protected readonly Optimizer Optimizer;
    protected static Set<int>CreateSet()=>new();
    protected static Expression GetLambda(LambdaExpression Lambda)=>Lambda.Body;
    protected static Func<TResult> Anonymous<TResult>(Func<TResult> i)=>i;
    protected static Func<TO,TResult> Anonymous<TO,TResult>(Func<TO,TResult> i)=>i;
    protected static Func<TO,T1,TResult> Anonymous<TO,T1,TResult>(Func<TO,T1,TResult> i)=>i;
    /// <summary>
    /// Optimizer.Lambda最適化(Expression);
    /// </summary>
    /// <param name="Expression"></param>
    protected void Optimizer_Lambda最適化(Expression Expression){
        var Optimizer=this.Optimizer;
        Optimizer.IsInline=false;
        Optimizer.Lambda最適化(Expression);
        Optimizer.IsInline=true;
        Optimizer.Lambda最適化(Expression);
    }
    private string ファイル名(string プリフィックス){
        var Type=this.GetType();
        var Frames=new StackTrace().GetFrames();
        for(var a=Frames.Length-1;a>=0;a--){
            var Method=Frames[a].GetMethod()!;
            if(Method.GetCustomAttribute(typeof(FactAttribute))!=null){
                //return $@"{フォルダ}\{プリフィックス}.{Type.FullName}.{Method.Name}.txt";
                return $@"{フォルダ}\{プリフィックス}.{Type.FullName}.{Method.Name}.txt";
            }
        }
        throw new NotImplementedException("ファイル名が取得できなかった");
    }
    private protected static void SerializeDeserialize<T>(LinqDB.Serializers.Serializer Serializer,T input){
        var bytes = Serializer.Serialize(input);
        var actual=Serializer.Deserialize<T>(bytes);
    }
    private protected static void SerializeDeserializeAreEqual<T>(LinqDB.Serializers.Serializer Serializer,T input){
        var bytes = Serializer.Serialize(input);
//        var json = Encoding.UTF8.GetString(bytes);
        var actual=Serializer.Deserialize<T>(bytes);
        Assert.Equal(input,actual,new 汎用Comparer());
    }
    private void SerializeDeserializeAreEqual<T>(T input){
        if((テストオプション.MemoryPack&テストオプション.MemoryPack)!=0)
            SerializeDeserializeAreEqual(this.MemoryPack,input);
        if((テストオプション.MessagePack&テストオプション.MemoryPack)!=0)
            SerializeDeserializeAreEqual(this.MemoryPack,input);
        if((テストオプション.Utf8Json&テストオプション.MemoryPack)!=0)
            SerializeDeserializeAreEqual(this.MemoryPack,input);
    }
    //private void SaveSerialize又はLoadDeserializeAssertEqual<T>(T input){
    //    if((テストオプション.MemoryPack&C.O)!=0)
    //        共通(this.MemoryPack,nameof(this.MemoryPack));
    //    if((テストオプション.MessagePack&C.O)!=0)
    //        共通(this.MessagePack,nameof(this.MessagePack));
    //    if((テストオプション.Utf8Json&C.O)!=0)
    //        共通(this.Utf8Json,nameof(this.Utf8Json));
    //    void 共通(LinqDB.Serializers.Serializer Serializer,string プリフィックス) {
    //        if((テストオプション.ファイルが無ければシリアライズ有ればデシリアライズ&C.O)!=0) {
    //            var ファイル名 = this.ファイル名(プリフィックス);
    //            if(File.Exists(ファイル名)) {
    //                try {
    //                    using var f = new FileStream(ファイル名,FileMode.Open);
    //                    var output = Serializer.Deserialize<T>(f);
    //                    Assert.Equal(input,output!,new 汎用Comparer());
    //                } finally {
    //                    File.Delete(ファイル名);
    //                }
    //            } else {
    //                using var f = new FileStream(ファイル名,FileMode.CreateNew);
    //                Serializer.Serialize(f,input);
    //            }
    //        } else {
    //            SerializeDeserialize(Serializer,input);
    //        }
    //    }
    //}
    protected void Objectシリアライズ<T>(T input) {
        if((テストオプション.MemoryPack&this.テストオプション)!=0)
            共通2(this.MemoryPack);
        if((テストオプション.MessagePack&this.テストオプション)!=0)
            共通2(this.MessagePack);
        if((テストオプション.Utf8Json&this.テストオプション)!=0)
            共通2(this.Utf8Json);
        void 共通2(LinqDB.Serializers.Serializer Serializer){
            共通(Serializer,input,(object?)input);
            共通(Serializer,default(T),(object?)default(T));
            共通(Serializer,new[]{input,input},new object?[]{input,input});
            共通(Serializer,new T?[]{default,default},new object?[]{default,default});
        }
        void 共通<T0, T1>(LinqDB.Serializers.Serializer Serializer,T0 t0,T1 t1){
            SerializeDeserialize(Serializer,t0);
            SerializeDeserialize(Serializer,t1);
            SerializeDeserialize(Serializer,new { t0 });
            SerializeDeserialize(Serializer,new { t1 });
            SerializeDeserialize(Serializer,new { t0,t1 });
        }
    }
    protected void ObjectシリアライズAssertEqual<T>(T input) {
        if((テストオプション.MemoryPack&this.テストオプション)!=0)
            共通2(this.MemoryPack);
        if((テストオプション.MessagePack&this.テストオプション)!=0)
            共通2(this.MessagePack);
        if((テストオプション.Utf8Json&this.テストオプション)!=0)
            共通2(this.Utf8Json);
        void 共通2(LinqDB.Serializers.Serializer Serializer){
            共通(Serializer,input,(object?)input);
            共通(Serializer,default(T),(object?)default(T));
            共通(Serializer,new[]{input,input},new object?[]{input,input});
            共通(Serializer,new T?[]{default,default},new object?[]{default,default});
        }
        void 共通<T0, T1>(LinqDB.Serializers.Serializer Serializer,T0 t0,T1 t1){
            SerializeDeserializeAreEqual(Serializer,t0);
            SerializeDeserializeAreEqual(Serializer,t1);
            SerializeDeserializeAreEqual(Serializer,new { t0 });
            SerializeDeserializeAreEqual(Serializer,new { t1 });
            SerializeDeserializeAreEqual(Serializer,new { t0,t1 });
        }
    }
    protected void ExpressionシリアライズAssertEqual<T>(T input,Action<T> AssertAction){
        if((テストオプション.MemoryPack&this.テストオプション)!=0)
            共通(this.MemoryPack);
        if((テストオプション.MessagePack&this.テストオプション)!=0)
            共通(this.MessagePack);
        if((テストオプション.Utf8Json&this.テストオプション)!=0)
            共通(this.Utf8Json);
        void 共通(LinqDB.Serializers.Serializer Serializer){
            var bytes=Serializer.Serialize(input);
            var output=Serializer.Deserialize<T>(bytes);
            AssertAction(output);
        }
    }
    protected void ExpressionシリアライズCoverage<T>(T input)where T:Expression{
        共通(
            input     ,(Expression?)input ,(object?)input     ,
            default(T),default(Expression),(object?)default(T)
        );
        共通(
            new   []{input  ,input  },new Expression?[]{input  ,input  },new object?[]{input  ,input  },
            new T?[]{default,default},new Expression?[]{default,default},new object?[]{default,default}
        );
        void 共通<T0,T1,T2,T3,T4,T5>(T0 t0,T1 t1,T2 t2,T3 t3,T4 t4,T5 t5) {
            this.SerializeDeserializeAreEqual(t0);
            this.SerializeDeserializeAreEqual(t1);
            this.SerializeDeserializeAreEqual(t2);
            this.SerializeDeserializeAreEqual(t3);
            this.SerializeDeserializeAreEqual(t4);
            this.SerializeDeserializeAreEqual(t5);
            this.SerializeDeserializeAreEqual(new {t0 });
            this.SerializeDeserializeAreEqual(new {t1 });
            this.SerializeDeserializeAreEqual(new {t2 });
            this.SerializeDeserializeAreEqual(new {t3 });
            this.SerializeDeserializeAreEqual(new {t4 });
            this.SerializeDeserializeAreEqual(new {t5 });
            this.SerializeDeserializeAreEqual(new {t0,t1,t2,t3,t4,t5 });
        }
    }
    //protected void Coverage<T>(T input){
    //    共通(input,(object?)input);
    //    共通(default(T),(object?)default(T));
    //    共通(new[] { input,input },new object?[] { input,input });
    //    共通(new T?[] { default,default },new object?[] { default,default });
    //    void 共通<T0,T1>(T0 t0,T1 t1) {
    //        this.SerializeDeserializeAreEqual(t0);
    //        this.SerializeDeserializeAreEqual(t1);
    //        this.SerializeDeserializeAreEqual(new {t0 });
    //        this.SerializeDeserializeAreEqual(new {t1 });
    //        this.SerializeDeserializeAreEqual(new {t0,t1});
    //    }
    //}
    protected void Expressionシリアライズ<T>(T? input) where T:Expression{
        if((テストオプション.MemoryPack&this.テストオプション)!=0)
            共通(this.MemoryPack);
        if((テストオプション.MessagePack&this.テストオプション)!=0)
            共通(this.MessagePack);
        if((テストオプション.Utf8Json&this.テストオプション)!=0)
            共通(this.Utf8Json);
        void 共通(LinqDB.Serializers.Serializer Serializer){
            共通0(Serializer,input,(Expression?)input,(object?)input);
            共通0(Serializer,default(T),default(Expression),(object?)default(T));
            共通0(Serializer,new[]{input,input},new Expression?[]{input,input},new object?[]{input,input});
            共通0(Serializer,new T?[]{default,default},new Expression?[]{default,default},new object?[]{default,default});
            void 共通0<T0,T1,T2>(LinqDB.Serializers.Serializer Serializer,T0 t0,T1 t1,T2 t2){
                SerializeDeserialize(Serializer,t0);
                SerializeDeserialize(Serializer,t1);
                SerializeDeserialize(Serializer,t2);
                SerializeDeserialize(Serializer,new{t0});
                SerializeDeserialize(Serializer,new{t1});
                SerializeDeserialize(Serializer,new{t2});
                SerializeDeserialize(Serializer,new{t0,t1,t2});
            }
        }
    }
    protected void ExpressionシリアライズAssertEqual<T>(T input) where T:Expression{
        if((テストオプション.MemoryPack&this.テストオプション)!=0)
            共通(this.MemoryPack);
        if((テストオプション.MessagePack&this.テストオプション)!=0)
            共通(this.MessagePack);
        if((テストオプション.Utf8Json&this.テストオプション)!=0)
            共通(this.Utf8Json);
        void 共通(LinqDB.Serializers.Serializer Serializer){
            共通0(Serializer,input,(Expression?)input,(object?)input);
            共通0(Serializer,default(T),default(Expression),(object?)default(T));
            共通0(Serializer,new[]{input,input},new Expression?[]{input,input},new object?[]{input,input});
            共通0(Serializer,new T?[]{default,default},new Expression?[]{default,default},new object?[]{default,default});
            void 共通0<T0,T1,T2>(LinqDB.Serializers.Serializer Serializer,T0 t0,T1 t1,T2 t2){
                SerializeDeserializeAreEqual(Serializer,t0);
                SerializeDeserializeAreEqual(Serializer,t1);
                SerializeDeserializeAreEqual(Serializer,t2);
                SerializeDeserializeAreEqual(Serializer,new{t0});
                SerializeDeserializeAreEqual(Serializer,new{t1});
                SerializeDeserializeAreEqual(Serializer,new{t2});
                SerializeDeserializeAreEqual(Serializer,new{t0,t1,t2});
            }
        }
    }
    //protected void ExpressionAssertEqual<T>(Expression<Func<T>> input)where T: LambdaExpression=>this.ExpressionAssertEqual(input,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    //protected void ExpressionAssertEqual<T,TResult>(Expression<Func<T,TResult>> input,T ServerObject)where T: LambdaExpression=>this.ExpressionAssertEqual(input,ServerObject,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    protected void Expression実行AssertEqual(Expression<Action> input) {
        this.ExpressionシリアライズAssertEqual(input);
        var Optimizer = this.Optimizer;
        var 標準 = input.Compile();
        標準();
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(input)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(input)();
    }
    protected void Expression実行AssertEqual<T>(Expression<Func<T>> input){
        if((テストオプション.最適化&this.テストオプション)!=0){
            var Optimizer=this.Optimizer;
            if((テストオプション.インライン&this.テストオプション)!=0){
                Optimizer.IsInline=true;
                Trace.WriteLine(CommonLibrary.インラインラムダテキスト(Optimizer.Lambda最適化(input)));
            } else{
                Optimizer.IsInline=false;
                Trace.WriteLine(CommonLibrary.インラインラムダテキスト(Optimizer.Lambda最適化(input)));
            }
        }
        if((テストオプション.ローカル実行&this.テストオプション)!=0){
            var 標準 = input.Compile();
            var expected = 標準();
            var 汎用Comparer=new 汎用Comparer();
            var Optimizer=this.Optimizer;
            if((テストオプション.インライン&this.テストオプション)!=0){
                Optimizer.IsInline=true;
                var actual=Optimizer.CreateDelegate(input)();
                Assert.Equal(expected,actual,汎用Comparer);
            } else{
                Optimizer.IsInline=false;
                var actual=Optimizer.CreateDelegate(input)();
                Assert.Equal(expected,actual,汎用Comparer);
            }
        }
        if((テストオプション.リモート実行&this.テストオプション)!=0){
            var 標準 = input.Compile();
            this.ServerClient(Client=>{
                var expected = 標準();
                if((テストオプション.MemoryPack&this.テストオプション)!=0){
                    var actual=Client.Expression(input,SerializeType.MemoryPack);
                    Assert.Equal(expected,actual,new 汎用Comparer());
                }
                if((テストオプション.MessagePack&this.テストオプション)!=0){
                    var actual=Client.Expression(input,SerializeType.MessagePack);
                    Assert.Equal(expected,actual,new 汎用Comparer());
                }
                if((テストオプション.Utf8Json&this.テストオプション)!=0){
                    var actual=Client.Expression(input,SerializeType.Utf8Json);
                    Assert.Equal(expected,actual,new 汎用Comparer());
                }
            });
        } else{
            if((テストオプション.MemoryPack&this.テストオプション)!=0)
                共通(this.MemoryPack);
            if((テストオプション.MessagePack&this.テストオプション)!=0)
                共通(this.MessagePack);
            if((テストオプション.Utf8Json&this.テストオプション)!=0)
                共通(this.Utf8Json);
        }
        void 共通(LinqDB.Serializers.Serializer Serializer){
            共通0(Serializer,input,(Expression?)input,(object?)input);
            共通0(Serializer,default(T),default(Expression),(object?)default(T));
            共通0(Serializer,new[]{input,input},new Expression?[]{input,input},new object?[]{input,input});
            共通0(Serializer,new T?[]{default,default},new Expression?[]{default,default},new object?[]{default,default});
            void 共通0<T0,T1,T2>(LinqDB.Serializers.Serializer Serializer,T0 t0,T1 t1,T2 t2){
                SerializeDeserializeAreEqual(Serializer,t0);
                SerializeDeserializeAreEqual(Serializer,t1);
                SerializeDeserializeAreEqual(Serializer,t2);
                SerializeDeserializeAreEqual(Serializer,new{t0});
                SerializeDeserializeAreEqual(Serializer,new{t1});
                SerializeDeserializeAreEqual(Serializer,new{t2});
                SerializeDeserializeAreEqual(Serializer,new{t0,t1,t2});
            }
        }
    }
    protected T Expression実行<T>(Expression<Func<T>> input){
        if((テストオプション.最適化&this.テストオプション)!=0){
            var Optimizer=this.Optimizer;
            if((テストオプション.インライン&this.テストオプション)!=0){
                Optimizer.IsInline=true;
                return Optimizer.CreateDelegate(input)();
            } else{
                Optimizer.IsInline=false;
                return Optimizer.CreateDelegate(input)();
            }
        }else{
            return input.Compile()();
        }
    }
    /// <summary>
    /// 3種シリアライズ、サーバー実行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="input"></param>
    protected void Expression実行AssertEqual<T,TResult>(Expression<Func<T,TResult>> input){
        var 引数=default(T)!;
        if((テストオプション.最適化&this.テストオプション)!=0){
            var Optimizer=this.Optimizer;
            if((テストオプション.インライン&this.テストオプション)!=0){
                Optimizer.IsInline=true;
                Trace.WriteLine(CommonLibrary.インラインラムダテキスト(Optimizer.Lambda最適化(input)));
            } else{
                Optimizer.IsInline=false;
                Trace.WriteLine(CommonLibrary.インラインラムダテキスト(Optimizer.Lambda最適化(input)));
            }
        }
        if((テストオプション.ローカル実行&this.テストオプション)!=0){
            var 標準 = input.Compile();
            var expected = 標準(引数);
            var 汎用Comparer=new 汎用Comparer();
            var Optimizer=this.Optimizer;
            if((テストオプション.インライン&this.テストオプション)!=0){
                Optimizer.IsInline=true;
                var Delegate=Optimizer.CreateDelegate(input);
                var actual=Delegate(引数);
                Assert.Equal(expected,actual,汎用Comparer);
            } else{
                Optimizer.IsInline=false;
                var Delegate=Optimizer.CreateDelegate(input);
                var actual=Delegate(引数);
                Assert.Equal(expected,actual,汎用Comparer);
            }
        }
        if((テストオプション.リモート実行&this.テストオプション)!=0){
            var 標準 = input.Compile();
            this.ServerClient(Client=>{
                var expected = 標準(引数);
                if((テストオプション.MemoryPack&this.テストオプション)!=0){
                    var actual=Client.Expression(input,SerializeType.MemoryPack);
                    Assert.Equal(expected,actual,new 汎用Comparer());
                }
                if((テストオプション.MessagePack&this.テストオプション)!=0){
                    var actual=Client.Expression(input,SerializeType.MessagePack);
                    Assert.Equal(expected,actual,new 汎用Comparer());
                }
                if((テストオプション.Utf8Json&this.テストオプション)!=0){
                    var actual=Client.Expression(input,SerializeType.Utf8Json);
                    Assert.Equal(expected,actual,new 汎用Comparer());
                }
            });
        } else{
            if((テストオプション.MemoryPack&this.テストオプション)!=0)
                共通(this.MemoryPack);
            if((テストオプション.MessagePack&this.テストオプション)!=0)
                共通(this.MessagePack);
            if((テストオプション.Utf8Json&this.テストオプション)!=0)
                共通(this.Utf8Json);
        }
        void 共通(LinqDB.Serializers.Serializer Serializer){
            共通0(Serializer,input,(Expression?)input,(object?)input);
            共通0(Serializer,default(T),default(Expression),(object?)default(T));
            共通0(Serializer,new[]{input,input},new Expression?[]{input,input},new object?[]{input,input});
            共通0(Serializer,new T?[]{default,default},new Expression?[]{default,default},new object?[]{default,default});
            void 共通0<T0,T1,T2>(LinqDB.Serializers.Serializer Serializer,T0 t0,T1 t1,T2 t2){
                SerializeDeserializeAreEqual(Serializer,t0);
                SerializeDeserializeAreEqual(Serializer,t1);
                SerializeDeserializeAreEqual(Serializer,t2);
                SerializeDeserializeAreEqual(Serializer,new{t0});
                SerializeDeserializeAreEqual(Serializer,new{t1});
                SerializeDeserializeAreEqual(Serializer,new{t2});
                SerializeDeserializeAreEqual(Serializer,new{t0,t1,t2});
            }
        }
    }
    protected void Expression比較実行AssertEqual<T0, T1>(Expression<Func<T0>> input0,Expression<Func<T1>> input1) {
        var actual0 = this.Expression実行(input0);
        var actual1 = this.Expression実行(input1);
        Assert.Equal(actual0,actual1,new 汎用Comparer());
    }
    //protected void ExpressionAssertEqual<T,TResult>(Expression<Func<T,TResult>> input,T ServerObject,Action<T> Action)where T: LambdaExpression{
    //    if((テストオプション.MemoryPack&C.O)!=0)
    //        Serialize(this.MemoryPack,nameof(this.MemoryPack));
    //    if((テストオプション.MessagePack&C.O)!=0)
    //        Serialize(this.MessagePack,nameof(this.MessagePack));
    //    if((テストオプション.Utf8Json&C.O)!=0)
    //        Serialize(this.Utf8Json,nameof(this.Utf8Json));
    //    LinqDB.Serializers.Serializer Serializer;
    //    if((テストオプション.MemoryPack_MessagePack_Utf8Json&C.O)!=0){
    //        if((テストオプション.MemoryPack&C.O)!=0){
    //            Serializer=this.MemoryPack;
    //        } else if((テストオプション.MessagePack&C.O)!=0){
    //            Serializer=this.MessagePack;
    //        } else{
    //            Assert.True((テストオプション.Utf8Json&C.O)!=0);
    //            Serializer=this.Utf8Json;
    //        }
    //        var bytes = Serializer.Serialize(input);
    //        var output = Serializer.Deserialize<T>(bytes);
    //        this.PrivateAssert(input,Action);
    //    }
    //    if((テストオプション.リモート実行&C.O)!=0){
    //        const int receiveTimeout = 1000;
    //        var port = Interlocked.Increment(ref ポート番号);
    //        var 標準 = input.Compile();
    //        var expected = 標準(ServerObject);
    //        using var Server = new Server(ServerObject,1,port);
    //        Server.ReadTimeout=receiveTimeout;
    //        Server.Open();
    //        using var R = new Client(Dns.GetHostName(),port);
    //        if((テストオプション.MemoryPack&C.O)!=0){
    //            var actual=R.Expression(input,SerializeType.MemoryPack);
    //            Assert.Equal(expected,actual,new 汎用Comparer());
    //        }
    //        if((テストオプション.MessagePack&C.O)!=0){
    //            var actual=R.Expression(input,SerializeType.MessagePack);
    //            Assert.Equal(expected,actual,new 汎用Comparer());
    //        }
    //        if((テストオプション.Utf8Json&C.O)!=0){
    //            var actual=R.Expression(input,SerializeType.Utf8Json);
    //            Assert.Equal(expected,actual,new 汎用Comparer());
    //        }
    //        Server.Close();
    //    }
    //    void Serialize(LinqDB.Serializers.Serializer Serializer,string プリフィックス){
    //        var ファイル名=this.ファイル名(プリフィックス);
    //        if((テストオプション.ファイルが無ければシリアライズ有ればデシリアライズ&C.O)!=0){
    //            if(File.Exists(ファイル名)){
    //                try{
    //                    using var f=new FileStream(ファイル名,FileMode.Open);
    //                    var output=Serializer.Deserialize<T>(f);
    //                    Action(output!);
    //                } finally{
    //                    File.Delete(ファイル名);
    //                }
    //            } else{
    //                using var f=new FileStream(ファイル名,FileMode.CreateNew);
    //                Serializer.Serialize(f,input);
    //            }
    //        } else{
    //            var bytes=Serializer.Serialize(input);
    //            var output=Serializer.Deserialize<T>(bytes);
    //            Action(output!);
    //        }
    //    }
    //}
    //protected void AssertEqual全パターン<T>(T input){
    //    this.AssertDefinition.AssertEqual全パターン(input);
    //}
    protected static object ClassDisplay取得(){
        var a=1;
        var b=2;
        var _=GetLambda(()=>b);
        var body=GetLambda(()=>a);
        var member=(MemberExpression)body;
        var constant=(ConstantExpression)member.Expression!;
        return constant.Value!;
    }
    protected static Reflection.MethodInfo GetMethod<T>(Expression<Func<T>> e)=>((MethodCallExpression)e.Body).Method;
    protected static Reflection.MethodInfo GetMethod(string Name)=>typeof(Serializer).GetMethod(Name,Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!;
    protected static Reflection.MethodInfo M(Expression<Action> f)=>((MethodCallExpression)f.Body).Method;
}
