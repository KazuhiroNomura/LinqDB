#define MEMORY
#define MESSAGE
#define JSON
using System.Diagnostics;
using Reflection = System.Reflection;
using LinqDB.Optimizers;
using LinqDB.Remote.Servers;
using static LinqDB.Helpers.Configulation;
using Expressions = System.Linq.Expressions;
using LinqDB.Remote.Clients;
using System.Net;
using System.Reflection;
using LinqDB;
using LinqDB.Optimizers.Comparison;
using TestLinqDB.Serializers;
namespace TestLinqDB;
[Flags]
public enum テストオプション{
    MemoryPack                                        =0b100000000,
    MessagePack                                       =0b010000000,
    Utf8Json                                          =0b001000000,
    ローカル実行                                      =0b000100000,
    リモート                                          =0b000010000,
    ファイルが無ければシリアライズ有ればデシリアライズ=0b000001000,
    ラムダ式最適化                                    =0b000000100,
    ループ式最適化                                    =0b000000010,
    アセンブリ保存                                    =0b000000001,
    全て                                              =0b111111111,
    MemoryPack_MessagePack_Utf8Json=MemoryPack|MessagePack|Utf8Json,
}
public abstract class 共通{
    private protected static int ポート番号;
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
        //this.汎用Comparer=new(this.ExpressionEqualityComparer);
    }
    private static void ServerClient(Action<Client>Action){
        const int receiveTimeout = 1000;
        if((テストオプション.リモート&C.O)!=0){
            var port=Interlocked.Increment(ref ポート番号);
            using var Server=new Server(1,port);
            Server.ReadTimeout=receiveTimeout;
            Server.Open();
            using var Client=new Client(Dns.GetHostName(),port);
            Action(Client);
            Server.Close();
        }
    }
    //protected readonly 汎用Comparer 汎用Comparer;
    protected readonly LinqDB.Serializers.Utf8Json.Serializer Utf8Json=new();
    protected readonly LinqDB.Serializers.MessagePack.Serializer MessagePack=new();
    protected readonly LinqDB.Serializers.MemoryPack.Serializer MemoryPack=new();
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly=(C.O&テストオプション.アセンブリ保存)!=0,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
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
    private protected static void SerializeDeserialize<T>(LinqDB.Serializers.Serializer Serializer,T expected){
        var bytes = Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<T>(bytes);
        Assert.Equal(expected,actual,new 汎用Comparer());
    }
    private void SaveSerialize又はLoadDeserializeCoverage<T>(T input){
        if((テストオプション.MemoryPack&C.O)!=0)
            共通(this.MemoryPack,nameof(this.MemoryPack));
        if((テストオプション.MessagePack&C.O)!=0)
            共通(this.MessagePack,nameof(this.MessagePack));
        if((テストオプション.Utf8Json&C.O)!=0)
            共通(this.Utf8Json,nameof(this.Utf8Json));
        void 共通(LinqDB.Serializers.Serializer Serializer,string プリフィックス) {
            if((テストオプション.ファイルが無ければシリアライズ有ればデシリアライズ&C.O)!=0) {
                var ファイル名 = this.ファイル名(プリフィックス);
                if(File.Exists(ファイル名)) {
                    try {
                        using var f = new FileStream(ファイル名,FileMode.Open);
                        var _= Serializer.Deserialize<T>(f);
                    } finally {
                        File.Delete(ファイル名);
                    }
                } else {
                    using var f = new FileStream(ファイル名,FileMode.CreateNew);
                    Serializer.Serialize(f,input);
                }
            } else {
                var bytes = Serializer.Serialize(input);
                var _= Serializer.Deserialize<T>(bytes);
            }
        }
    }
    private void SaveSerialize又はLoadDeserializeAssertEqual<T>(T input){
        if((テストオプション.MemoryPack&C.O)!=0)
            共通(this.MemoryPack,nameof(this.MemoryPack));
        if((テストオプション.MessagePack&C.O)!=0)
            共通(this.MessagePack,nameof(this.MessagePack));
        if((テストオプション.Utf8Json&C.O)!=0)
            共通(this.Utf8Json,nameof(this.Utf8Json));
        void 共通(LinqDB.Serializers.Serializer Serializer,string プリフィックス) {
            if((テストオプション.ファイルが無ければシリアライズ有ればデシリアライズ&C.O)!=0) {
                var ファイル名 = this.ファイル名(プリフィックス);
                if(File.Exists(ファイル名)) {
                    try {
                        using var f = new FileStream(ファイル名,FileMode.Open);
                        var output = Serializer.Deserialize<T>(f);
                        Assert.Equal(input,output!,new 汎用Comparer());
                    } finally {
                        File.Delete(ファイル名);
                    }
                } else {
                    using var f = new FileStream(ファイル名,FileMode.CreateNew);
                    Serializer.Serialize(f,input);
                }
            } else {
                SerializeDeserialize(Serializer,input);
            }
        }
    }
    protected void AssertEqual<T>(T input){
        共通0(input,(object?)input);
        共通0(default(T),(object?)default(T));
        共通0(new   []{input  ,input  },new object?[]{input  ,input  });
        共通0(new T?[]{default,default},new object?[]{default,default});
        ServerClient(Client=>{
            if((テストオプション.MemoryPack&C.O)!=0){
                var actual=Client.SerializeSendReceive(input,SerializeType.MemoryPack);
                Assert.Equal(input,actual,new 汎用Comparer());
            }
            if((テストオプション.MessagePack&C.O)!=0){
                var actual=Client.SerializeSendReceive(input,SerializeType.MessagePack);
                Assert.Equal(input,actual,new 汎用Comparer());
            }
            if((テストオプション.Utf8Json&C.O)!=0){
                var actual=Client.SerializeSendReceive(input,SerializeType.Utf8Json);
                Assert.Equal(input,actual,new 汎用Comparer());
            }
        });
        //const int receiveTimeout = 1000;
        //var port = Interlocked.Increment(ref ポート番号);
        //using var Server = new Server(1,port);
        //Server.ReadTimeout=receiveTimeout;
        //Server.Open();
        //using var Client = new Client(Dns.GetHostName(),port);
        //if((テストオプション.MemoryPack&C.O)!=0){
        //    var actual=Client.SerializeSendReceive(input,SerializeType.MemoryPack);
        //    Assert.Equal(input,actual,new 汎用Comparer());
        //}
        //if((テストオプション.MessagePack&C.O)!=0){
        //    var actual=Client.SerializeSendReceive(input,SerializeType.MessagePack);
        //    Assert.Equal(input,actual,new 汎用Comparer());
        //}
        //if((テストオプション.Utf8Json&C.O)!=0){
        //    var actual=Client.SerializeSendReceive(input,SerializeType.Utf8Json);
        //    Assert.Equal(input,actual,new 汎用Comparer());
        //}
        //Server.Close();
        if((テストオプション.MemoryPack&C.O)!=0)
            SerializeDeserialize(this.MemoryPack,input);
        if((テストオプション.MessagePack&C.O)!=0)
            SerializeDeserialize(this.MessagePack,input);
        if((テストオプション.Utf8Json&C.O)!=0)
            SerializeDeserialize(this.Utf8Json,input);
        void 共通0<T0,T1>(T0 t0,T1 t1) {
            this.SaveSerialize又はLoadDeserializeAssertEqual(t0);
            this.SaveSerialize又はLoadDeserializeAssertEqual(t1);
            this.SaveSerialize又はLoadDeserializeAssertEqual(new {t0 });
            this.SaveSerialize又はLoadDeserializeAssertEqual(new {t1 });
            this.SaveSerialize又はLoadDeserializeAssertEqual(new {t0,t1});
        }
    }
    protected void AssertEqual<T>(T input,Action<T> AssertAction){
        if((テストオプション.MemoryPack&C.O)!=0)
            共通(this.MemoryPack);
        if((テストオプション.MessagePack&C.O)!=0)
            共通(this.MessagePack);
        if((テストオプション.Utf8Json&C.O)!=0)
            共通(this.Utf8Json);
        void 共通(LinqDB.Serializers.Serializer Serializer){
            var bytes=Serializer.Serialize(input);
            var output=Serializer.Deserialize<T>(bytes);
            AssertAction(output);
        }
    }
    protected void ExpressionシリアライズCoverage<T>(T input)where T:Expressions.Expression{
        共通(
            input     ,(Expressions.Expression?)input ,(object?)input     ,
            default(T),default(Expressions.Expression),(object?)default(T)
        );
        共通(
            new   []{input  ,input  },new Expressions.Expression?[]{input  ,input  },new object?[]{input  ,input  },
            new T?[]{default,default},new Expressions.Expression?[]{default,default},new object?[]{default,default}
        );
        void 共通<T0,T1,T2,T3,T4,T5>(T0 t0,T1 t1,T2 t2,T3 t3,T4 t4,T5 t5) {
            this.SaveSerialize又はLoadDeserializeCoverage(t0);
            this.SaveSerialize又はLoadDeserializeCoverage(t1);
            this.SaveSerialize又はLoadDeserializeCoverage(t2);
            this.SaveSerialize又はLoadDeserializeCoverage(t3);
            this.SaveSerialize又はLoadDeserializeCoverage(t4);
            this.SaveSerialize又はLoadDeserializeCoverage(t5);
            this.SaveSerialize又はLoadDeserializeCoverage(new {t0 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t1 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t2 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t3 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t4 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t5 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t0,t1,t2,t3,t4,t5 });
        }
    }
    protected void Coverage<T>(T input){
        共通(input,(object?)input);
        共通(default(T),(object?)default(T));
        共通(new[] { input,input },new object?[] { input,input });
        共通(new T?[] { default,default },new object?[] { default,default });
        void 共通<T0,T1>(T0 t0,T1 t1) {
            this.SaveSerialize又はLoadDeserializeCoverage(t0);
            this.SaveSerialize又はLoadDeserializeCoverage(t1);
            this.SaveSerialize又はLoadDeserializeCoverage(new {t0 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t1 });
            this.SaveSerialize又はLoadDeserializeCoverage(new {t0,t1});
        }
    }
    protected void ExpressionシリアライズAssertEqual<T>(T input) where T:Expressions.Expression{
        共通0(input                    ,(Expressions.Expression?)input                ,(object?)input                );
        共通0(default(T)               ,default(Expressions.Expression)               ,(object?)default(T)           );
        共通0(new   []{input  ,input  },new Expressions.Expression?[]{input  ,input  },new object?[]{input  ,input  });
        共通0(new T?[]{default,default},new Expressions.Expression?[]{default,default},new object?[]{default,default});
        void 共通0<T0,T1,T2>(T0 t0,T1 t1,T2 t2) {
            this.SaveSerialize又はLoadDeserializeAssertEqual(t0);
            this.SaveSerialize又はLoadDeserializeAssertEqual(t1);
            this.SaveSerialize又はLoadDeserializeAssertEqual(t2);
            this.SaveSerialize又はLoadDeserializeAssertEqual(new {t0 });
            this.SaveSerialize又はLoadDeserializeAssertEqual(new {t1 });
            this.SaveSerialize又はLoadDeserializeAssertEqual(new {t2 });
            this.SaveSerialize又はLoadDeserializeAssertEqual(new {t0,t1,t2});
        }
        ServerClient(Client=>{
            if((テストオプション.MemoryPack&C.O)!=0){
                var actual=Client.SerializeSendReceive(input,SerializeType.MemoryPack);
                Assert.Equal(input,actual,new 汎用Comparer());
            }
            if((テストオプション.MessagePack&C.O)!=0){
                var actual=Client.SerializeSendReceive(input,SerializeType.MessagePack);
                Assert.Equal(input,actual,new 汎用Comparer());
            }
            if((テストオプション.Utf8Json&C.O)!=0){
                var actual=Client.SerializeSendReceive(input,SerializeType.Utf8Json);
                Assert.Equal(input,actual,new 汎用Comparer());
            }
        });
        //const int receiveTimeout = 1000;
        //var port = Interlocked.Increment(ref ポート番号);
        //using var Server = new Server(1,port);
        //Server.ReadTimeout=receiveTimeout;
        //Server.Open();
        //using var Client = new Client(Dns.GetHostName(),port);
        //if((テストオプション.MemoryPack&C.O)!=0){
        //    var actual=Client.SerializeSendReceive(input,SerializeType.MemoryPack);
        //    Assert.Equal(input,actual,new 汎用Comparer());
        //}
        //if((テストオプション.MessagePack&C.O)!=0){
        //    var actual=Client.SerializeSendReceive(input,SerializeType.MessagePack);
        //    Assert.Equal(input,actual,new 汎用Comparer());
        //}
        //if((テストオプション.Utf8Json&C.O)!=0){
        //    var actual=Client.SerializeSendReceive(input,SerializeType.Utf8Json);
        //    Assert.Equal(input,actual,new 汎用Comparer());
        //}
        //Server.Close();
    }
    //protected void ExpressionAssertEqual<T>(Expressions.Expression<Func<T>> input)where T: Expressions.LambdaExpression=>this.ExpressionAssertEqual(input,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    //protected void ExpressionAssertEqual<T,TResult>(Expressions.Expression<Func<T,TResult>> input,T ServerObject)where T: Expressions.LambdaExpression=>this.ExpressionAssertEqual(input,ServerObject,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    protected void ExpressionAssertEqual(Expressions.Expression<Action> input){
        this.ExpressionシリアライズAssertEqual(input);
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        標準();
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(input)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(input)();
    }
    protected T Expression実行AssertEqual<T>(Expressions.Expression<Func<T>> input){
        this.ExpressionシリアライズAssertEqual(input);
        var 標準 = input.Compile();
        var expected = 標準();
        ServerClient(Client=>{
            if((テストオプション.MemoryPack&C.O)!=0){
                var actual=Client.Expression(input,SerializeType.MemoryPack);
                Assert.Equal(expected,actual,new 汎用Comparer());
            }
            if((テストオプション.MessagePack&C.O)!=0){
                var actual=Client.Expression(input,SerializeType.MessagePack);
                Assert.Equal(expected,actual,new 汎用Comparer());
            }
            if((テストオプション.Utf8Json&C.O)!=0){
                var actual=Client.Expression(input,SerializeType.Utf8Json);
                Assert.Equal(expected,actual,new 汎用Comparer());
            }
        });
        //const int receiveTimeout = 1000;
        //var port = Interlocked.Increment(ref ポート番号);
        //using var Server = new Server(1,port);
        //Server.ReadTimeout=receiveTimeout;
        //Server.Open();
        //using var R = new Client(Dns.GetHostName(),port);
        //if((テストオプション.MemoryPack&C.O)!=0){
        //    var actual=R.Expression(input,SerializeType.MemoryPack);
        //    Assert.Equal(expected,actual,new 汎用Comparer());
        //}
        //if((テストオプション.MessagePack&C.O)!=0){
        //    var actual=R.Expression(input,SerializeType.MessagePack);
        //    Assert.Equal(expected,actual,new 汎用Comparer());
        //}
        //if((テストオプション.Utf8Json&C.O)!=0){
        //    var actual=R.Expression(input,SerializeType.Utf8Json);
        //    Assert.Equal(expected,actual,new 汎用Comparer());
        //}
        //Server.Close();
        if((テストオプション.ローカル実行&C.O)!=0){
            if((テストオプション.MemoryPack&C.O)!=0)
                共通(this.MemoryPack,nameof(this.MemoryPack));
            if((テストオプション.MessagePack&C.O)!=0)
                共通(this.MessagePack,nameof(this.MessagePack));
            if((テストオプション.Utf8Json&C.O)!=0)
                共通(this.Utf8Json,nameof(this.Utf8Json));
            void 共通(LinqDB.Serializers.Serializer Serializer,string プリフィックス) {
                var 汎用Comparer=new 汎用Comparer();
                var Optimizer=this.Optimizer;
                Optimizer.IsInline=false;
                var actual0=Optimizer.CreateDelegate(input)();
                Optimizer.IsInline=true;
                var actual1=Optimizer.CreateDelegate(input)();
                Assert.Equal(expected,actual0,汎用Comparer);
                Assert.Equal(expected,actual1,汎用Comparer);
            }
        }
        return expected;
    }
    protected void Expression実行AssertEqual<T>(Expressions.Expression<Func<int,T>> input){
        const int 引数=0;
        this.ExpressionシリアライズAssertEqual(input);
        var 標準 = input.Compile();
        var expected = 標準(引数);
        ServerClient(Client=>{
            if((テストオプション.MemoryPack&C.O)!=0){
                var actual=Client.Expression(input,SerializeType.MemoryPack);
                Assert.Equal(expected,actual,new 汎用Comparer());
            }
            if((テストオプション.MessagePack&C.O)!=0){
                var actual=Client.Expression(input,SerializeType.MessagePack);
                Assert.Equal(expected,actual,new 汎用Comparer());
            }
            if((テストオプション.Utf8Json&C.O)!=0){
                var actual=Client.Expression(input,SerializeType.Utf8Json);
                Assert.Equal(expected,actual,new 汎用Comparer());
            }
        });
        //const int receiveTimeout = 1000;
        //var port = Interlocked.Increment(ref ポート番号);
        //using var Server = new Server<int>(引数,1,port);
        //Server.ReadTimeout=receiveTimeout;
        //Server.Open();
        //using var R = new Client(Dns.GetHostName(),port);
        //if((テストオプション.MemoryPack&C.O)!=0){
        //    var actual=R.Expression(input,SerializeType.MemoryPack);
        //    Assert.Equal(expected,actual,new 汎用Comparer());
        //}
        //if((テストオプション.MessagePack&C.O)!=0){
        //    var actual=R.Expression(input,SerializeType.MessagePack);
        //    Assert.Equal(expected,actual,new 汎用Comparer());
        //}
        //if((テストオプション.Utf8Json&C.O)!=0){
        //    var actual=R.Expression(input,SerializeType.Utf8Json);
        //    Assert.Equal(expected,actual,new 汎用Comparer());
        //}
        //Server.Close();
        {
            if((テストオプション.MemoryPack&C.O)!=0)
                共通(this.MemoryPack,nameof(this.MemoryPack));
            if((テストオプション.MessagePack&C.O)!=0)
                共通(this.MessagePack,nameof(this.MessagePack));
            if((テストオプション.Utf8Json&C.O)!=0)
                共通(this.Utf8Json,nameof(this.Utf8Json));
            void 共通(LinqDB.Serializers.Serializer Serializer,string プリフィックス) {
                var 汎用Comparer=new 汎用Comparer();
                var Optimizer=this.Optimizer;
                Optimizer.IsInline=false;
                var actual0=Optimizer.CreateDelegate(input)(引数);
                Optimizer.IsInline=true;
                var actual1=Optimizer.CreateDelegate(input)(引数);
                Assert.Equal(expected,actual0,汎用Comparer);
                Assert.Equal(expected,actual1,汎用Comparer);
            }
        }
    }
    protected void Expression比較実行AssertEqual<T0, T1>(Expressions.Expression<Func<T0>> input0,Expressions.Expression<Func<T1>> input1) {
        var actual0 = this.Expression実行AssertEqual(input0);
        var actual1 = this.Expression実行AssertEqual(input1);
        Assert.Equal(actual0,actual1,new 汎用Comparer());
    }
    //protected void ExpressionAssertEqual<T,TResult>(Expressions.Expression<Func<T,TResult>> input,T ServerObject,Action<T> Action)where T: Expressions.LambdaExpression{
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
        var body=GetLambda(()=>a).Body;
        var member=(Expressions.MemberExpression)body;
        var constant=(Expressions.ConstantExpression)member.Expression!;
        return constant.Value!;
        static Expressions.LambdaExpression GetLambda<T>(Expressions.Expression<Func<T>> e)=>e;
    }
    protected static Reflection.MethodInfo GetMethod<T>(Expressions.Expression<Func<T>> e)=>((Expressions.MethodCallExpression)e.Body).Method;
    protected static Reflection.MethodInfo GetMethod(string Name)=>typeof(Serializer).GetMethod(Name,Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!;
    protected static Reflection.MethodInfo M(Expressions.Expression<Action> f)=>((Expressions.MethodCallExpression)f.Body).Method;
}
