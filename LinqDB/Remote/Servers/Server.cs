//#define クエリは別プロセスで実行
//#define クエリは別スレッドで実行
#define クエリは同スレッドで実行
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using static LinqDB.Helpers.Configulation;
using static LinqDB.Helpers.CommonLibrary;
namespace LinqDB.Remote.Servers;

/// <summary>
/// 複数のListerSocketでAcceptする。
/// </summary>
public class Server:IDisposable{
    internal readonly object Proxy;
    private readonly ParameterizedThreadStart Delegate実行;
    /// <summary>
    /// 非同期処理が終了したらWaitを通過する。
    /// </summary>
    internal readonly CountdownEvent CountdownEvent = new(0);
    /// <summary>
    /// テスト用のログメソッド
    /// </summary>
    protected static void ログ(string s) => Trace.WriteLine(s);
    private sealed class Threadで実行するMethodデータ {
        /// <summary>
        /// 実行結果に渡すヘッダー
        /// </summary>
        private Response Sendヘッダー;
        /// <summary>
        /// Invokeの戻り値
        /// </summary>
        private object Result = null!;
        /// <summary>
        /// 実行したいデリゲート
        /// </summary>
        private readonly Delegate Delegate = null!;
        //[EnvironmentPermission(SecurityAction.PermitOnly)] //System.Security.Permissions.FileIOPermission
        //[FileDialogPermission(SecurityAction.PermitOnly)]//例えばFileStream
        //[FileIOPermission(SecurityAction.PermitOnly)]//例えばFileStream
        //[GacIdentityPermission(SecurityAction.PermitOnly)]//例えばFileStream
        //[IsolatedStorageFilePermission(SecurityAction.PermitOnly)]//例えばFileStream
        //[KeyContainerPermission(SecurityAction.PermitOnly)]//例えばFileStream
        //[PrincipalPermission(SecurityAction.PermitOnly)]   //System.Security.Permissions.FileIOPermission
        //[PublisherIdentityPermission(SecurityAction.PermitOnly)]
        //[ReflectionPermission(SecurityAction.PermitOnly)]
        //[RegistryPermission(SecurityAction.PermitOnly)]
        //[SecurityPermission(SecurityAction.PermitOnly)]
        //[SiteIdentityPermission(SecurityAction.PermitOnly)]
        //[StrongNameIdentityPermission(SecurityAction.PermitOnly)]
        //[UIPermission(SecurityAction.PermitOnly)]
        //[UrlIdentityPermission(SecurityAction.PermitOnly)] //System.Security.Permissions.FileIOPermission
        //[ZoneIdentityPermission(SecurityAction.PermitOnly)]//例えばFileStream

        /// <summary>
        /// スレッド上で実行することでタイムアウトさせる
        /// </summary>
        /// <param name="Delegate"></param>
        /// <returns>(Response Response,Object Result)</returns>
        internal (Response Response, object Result) Threadで実行(object Delegate) {
#if クエリは同スレッドで実行
            try {
                var MulticastDelegate = (MulticastDelegate)Delegate;
                var Method = MulticastDelegate.Method;
                Debug.Assert(
                    Method.GetParameters().Length==0||//static
                    Method.GetParameters().Length==1||//this
                    Method.GetParameters().Length==2  //R.Expression(r=>
                );
                var Length = Method.IsStatic&&MulticastDelegate.Target is not null
                    ? 1
                    : 0;
                var args=Method.GetParameters().Length==Length
                    ?Array.Empty<object>()
                    :this.args1;
                var result=(
                    Method.ReturnType==typeof(void)
                        ? Response.Bytes0
                        : Response.Object,
                    MulticastDelegate.DynamicInvoke(args)!
                );
                return result;
                //this.Result=Delegate.DynamicInvoke(
                //    Method.GetParameters().Length==(Method.IsStatic&&Delegate.Target is not null
                //        ? 1
                //        : 0
                //    )
                //        ?null
                //        :this.args
                //);
            } catch(TargetInvocationException ex) {
                return (Response.ThrowException, ex.InnerException!);
#pragma warning disable CA1031 // 一般的な例外の種類はキャッチしません
            } catch(Exception ex) {
#pragma warning restore CA1031 // 一般的な例外の種類はキャッチしません
                return (Response.ThrowException, ex);
            }
#else
                this.Delegate=(Delegate)Delegate;
                var Thread =スレッド実行(
                    "タイムアウトのためのスレッドCommit<TResult>",
                    true,
                    this.DelegateをDynamicInvokeDelegate
                );
                if(Thread.Join(this.処理のTimeout)) {
                    return (this.Sendヘッダー, this.Result);
                }
                Thread.Abort();
                var CurrentMethod = MethodBase.GetCurrentMethod();
                Debug.Assert(CurrentMethod.DeclaringType is not null,"CurrentMethod.DeclaringType  is not null");
                return (Response.ThrowException, new TimeoutException(CurrentMethod.DeclaringType.AssemblyQualifiedName+"."+CurrentMethod.Name));
#endif
        }
        private readonly ThreadStart DelegateをDynamicInvokeDelegate;
        /// <summary>
        /// 実行を終わらせる必要がある制限時間
        /// </summary>
        //private Int32 処理のTimeout = -1;
        //private static readonly Object[] args0 = new Object[0];
        private readonly object?[] args1 = new object?[1];
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Threadで実行するMethodデータ(object? ServerObject) {
            this.DelegateをDynamicInvokeDelegate=this.DelegateをDynamicInvoke;
            this.args1[0]=ServerObject;
        }

        /// <summary>
        /// 実行処理
        /// </summary>
        private void DelegateをDynamicInvoke() {
            try {
                var Delegate = this.Delegate;
                var Method = Delegate.Method;
                Debug.Assert(
                    Method.GetParameters().Length==0||//static
                    Method.GetParameters().Length==1||//this
                    Method.GetParameters().Length==2  //R.Expression(r=>
                );
                //                var Count= Method.GetParameters().Length;
                this.Sendヘッダー=Method.ReturnType==typeof(void)
                    ? Response.Bytes0
                    : Response.Object;
                var Length = Method.IsStatic&&Delegate.Target is not null
                    ? 1
                    : 0;
                this.Result=Method.GetParameters().Length==Length?Delegate.DynamicInvoke(Array.Empty<object>())!:Delegate.DynamicInvoke(this.args1)!;
                //this.Result=Delegate.DynamicInvoke(
                //    Method.GetParameters().Length==(Method.IsStatic&&Delegate.Target is not null
                //        ? 1
                //        : 0
                //    )
                //        ?null
                //        :this.args
                //);
            } catch(Exception ex) {
                this.Result=ex;
                this.Sendヘッダー=Response.ThrowException;
                throw;
            }
        }
    }
    internal CancellationTokenSource CancellationTokenSourceループRequestResponse = null!;
    internal void Add(SingleReceiveSend SingleReceiveSend) => this.RequestResponseSingleReceiveSends.Add(
        SingleReceiveSend,
        this.CancellationTokenSourceループRequestResponse.Token
    );



    private const int RequestResponseCount = 4;
    private readonly SingleAcceptReceiveSend[] SingleAcceptReceiveSends;
    //internal readonly CountdownEvent CountdownEvent = new CountdownEvent(0);

    //[System.Diagnostics.CodeAnalysis.SuppressMessage("コードの品質","IDE0069:破棄可能なフィールドは破棄しなければなりません",Justification = "<保留中>")]
#pragma warning disable IDE0069 // 破棄可能なフィールドは破棄しなければなりません
    internal BlockingCollection<SingleReceiveSend> RequestResponseSingleReceiveSends { get;}
#pragma warning restore IDE0069 // 破棄可能なフィールドは破棄しなければなりません
    public bool リクエストを保存するか{
        get;
        set;
    }
    //internal Logic Logic {
    //    get;
    //    set;
    //} = null!;
    /// <summary>
    /// X.509.v3証明書
    /// </summary>
    public X509Certificate? X509Certificate {
        get;
        set;
    }
    /// <summary>
    /// ユーザ名とパスワードハッシュ
    /// </summary>
    public readonly UserPasswordDictionary UserPasswordDictionary=new()
    {
        {
            "Administrator",""
        }
    };
    private int _ReadTimeout=既定のタイムアウト;
    /// <summary>
    /// 受信タイムアウト。
    /// </summary>
    public int ReadTimeout{
        get=>this._ReadTimeout;
        set{
            this._ReadTimeout=value;
            foreach(var SingleAcceptReceiveSend in this.SingleAcceptReceiveSends) {
                SingleAcceptReceiveSend.ReadTimeout=value;
            }
        }
    }
    private int _WriteTimeout= 既定のタイムアウト;
    /// <summary>
    /// 送信タイムアウト。
    /// </summary>
    public int WriteTimeout {
        get => this._WriteTimeout;
        set {
            this._WriteTimeout=value;
            foreach(var SingleAcceptReceiveSend in this.SingleAcceptReceiveSends)SingleAcceptReceiveSend.WriteTimeout=value;
        }
    }
    internal readonly object? ServerObject;
    public static Server<T> Create<T>(T ServerObject,int ReceiveSendスレッド数,params int[] ListenPorts) where T : class {
        return new Server<T>(ServerObject,ReceiveSendスレッド数,ListenPorts);
    }
    public static Server Create(int ReceiveSendスレッド数,params int[] ListenPorts){
        return new Server<object>(null!,ReceiveSendスレッド数,ListenPorts);
    }
    private static readonly object staticプロキシ = new();
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="ReceiveSendスレッド数"></param>
    /// <param name="ListenPorts"></param>
    public Server(int ReceiveSendスレッド数,params int[] ListenPorts):this(null,staticプロキシ,ReceiveSendスレッド数,ListenPorts) {
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="ServerObject"></param>
    /// <param name="プロキシ"></param>
    /// <param name="ReceiveSendスレッド数"></param>
    /// <param name="ListenPorts"></param>
    private protected Server(object? ServerObject,object プロキシ,int ReceiveSendスレッド数,params int[] ListenPorts) {
        this.ServerObject=ServerObject;
        this.Proxy=プロキシ;
        this.RequestResponseSingleReceiveSends=new BlockingCollection<SingleReceiveSend>(RequestResponseCount);
        var SingleAcceptReceiveSends = this.SingleAcceptReceiveSends=new SingleAcceptReceiveSend[ListenPorts.Length];
        for(var a=0;a<ListenPorts.Length;a++)SingleAcceptReceiveSends[a]=new SingleAcceptReceiveSend(this,ReceiveSendスレッド数,ListenPorts[a]);
        //var Threadで実行するDelegate_Target = new Threadで実行するMethodデータ(ServerObject);
        //BlockingCollection.Dispose()したあとCancellationTokenSource.Cancel()したらAggregateException
        //this.RequestResponseSingleReceiveSends=Server.RequestResponseSingleReceiveSends;
        //            this.DelegateループRequestResponse=this.ループRequestResponse;
        this.Delegate実行=this.Function実行;
    }
    private void Function実行(object? obj) {
        Trace_WriteLine(0,"Server.Function実行");
        var RequestResponseSingleReceiveSends = (BlockingCollection<SingleReceiveSend>)obj!;
        var Threadで実行するDelegate_Target = new Threadで実行するMethodデータ(this.ServerObject);
        var Token = this.CancellationTokenSourceループRequestResponse.Token;
        try {
            while(true) {
                var SingleReceiveSend = RequestResponseSingleReceiveSends.Take(Token);
                var デシリアライズした = SingleReceiveSend.デシリアライズした;
                try {
                    var Request = デシリアライズした.Request;
                    Trace_WriteLine(1,$"Server.Function実行 {Request}");
                    switch(Request) {
                        case Request.Bytes0_Bytes0: {
                            SingleReceiveSend.送信(
                                Response.Bytes0,
                                null
                            );
                            break;
                        }
                        case Request.Byte_Byte: {
                            SingleReceiveSend.送信(
                                Response.Byte,
                                (byte)デシリアライズした.Object!
                            );
                            break;
                        }
                        case Request.BytesN_BytesN: {
                            SingleReceiveSend.送信(
                                Response.BytesN,
                                デシリアライズした.Object!
                            );
                            break;
                        }
                        case Request.TimeoutException_ThrowException: {
                            SingleReceiveSend.送信(
                                Response.ThrowException,
                                new TimeoutException("テスト"),
                                デシリアライズした.XmlType
                            );
                            break;
                        }
                        case Request.Object_Object:
                        case Request.Delegate_Invoke:
                        case Request.Expression_Invoke: {
                            try {
                                switch(Request) {
                                    case Request.Object_Object: {
                                        SingleReceiveSend.送信(
                                            Response.Object,
                                            デシリアライズした.Object!,
                                            デシリアライズした.XmlType
                                        );
                                        break;
                                    }
                                    case Request.Delegate_Invoke:
                                    case Request.Expression_Invoke: {
                                        Debug.Assert(デシリアライズした.XmlType==XmlType.Utf8Json||デシリアライズした.XmlType==XmlType.MessagePack);
                                        var Lambda=(LambdaExpression)デシリアライズした.Object!;
                                        var Optimizer=new Optimizers.Optimizer();
                                        var Delegate=Optimizer.CreateServerDelegate(Lambda);
                                        var (ResponseType, Result)=Threadで実行するDelegate_Target.Threadで実行(
                                            Delegate
                                        );
                                        SingleReceiveSend.送信(
                                            ResponseType,
                                            Result,
                                            デシリアライズした.XmlType
                                        );
                                        //if(デシリアライズした.XmlType==XmlType.Utf8Json) {
                                        //    var ScriptRunner = (ScriptRunner<object>)デシリアライズした.Object!;
                                        //    var e = ScriptRunner(this.Proxy);
                                        //    e.Wait();
                                        //    SingleReceiveSend.送信(
                                        //        Response.Object,
                                        //        e.Result,
                                        //        デシリアライズした.XmlType
                                        //    );
                                        //} else {
                                        //    Debug.Assert(デシリアライズした.XmlType==XmlType.MessagePack);
                                        //    var (ResponseType, Result)=Threadで実行するDelegate_Target.Threadで実行(
                                        //        デシリアライズした.Object!
                                        //    );
                                        //    SingleReceiveSend.送信(
                                        //        ResponseType,
                                        //        Result,
                                        //        デシリアライズした.XmlType
                                        //    );
                                        //}
                                        break;
                                    }
                                }
                            } catch(SerializationException ex) {
                                SingleReceiveSend.送信(
                                    Response.ThrowException,
                                    ex,
                                    デシリアライズした.XmlType
                                );
                            }
                            break;
                        }
                        case Request.Exception_ThrowException: {
                            SingleReceiveSend.送信(
                                Response.ThrowException,
                                デシリアライズした.Object
                            );
                            break;
                        }
                        default: throw new InvalidDataException("不正な通信方式"+Request);
                    }
                } catch(OperationCanceledException ex) {
                    Trace_WriteLine(2,$"Server.Function実行 catch(OperationCanceledException){ex.StackTrace}");
                } catch(Exception ex) {
                    SingleReceiveSend.送信(
                        Response.ThrowException,
                        ex,
                        デシリアライズした.XmlType
                    );
                    Trace_WriteLine(3,$"Server.Function実行 catch({MethodBase.GetCurrentMethod()}){ex.Message} {ex.StackTrace}");
                    throw;
                }
            }
        } catch(OperationCanceledException) {
            //
        } catch(ArgumentNullException) {
            //RequestResponseSingleReceiveSends.Take(Token)で待っているときに別スレッドで
            //RequestResponseSingleReceiveSends.Dispose()すると発生。
        } catch(Exception ex) {
            Trace_WriteLine(4,$"Server.Function実行 catch({ex.GetType().FullName}){Thread.CurrentThread.Name} {ex}");
            throw;
        } finally {
            this.CountdownEvent.Signal();
        }
    }
    /// <summary>
    /// 実行中かどうか表す
    /// </summary>
    public bool IsRunning=>!this.CountdownEvent.WaitHandle.WaitOne(0);
    private Thread ループRequestResponse=null!;
    //private CancellationTokenSource? CancellationTokenSource=null;
    /// <summary>
    /// Acceptを開始。
    /// </summary>
    public void Open() {
        var ループRequestResponse = this.ループRequestResponse=new Thread(this.Delegate実行) {
            Name="ループRequestResponse"
        };
        this.CountdownEvent.Reset(1);
        var CancellationToken=(this.CancellationTokenSourceループRequestResponse=new CancellationTokenSource()).Token;
        ループRequestResponse.Start(this.RequestResponseSingleReceiveSends);//throw可能性
        Debug.Assert(this.RequestResponseSingleReceiveSends.Count==0);
        foreach(var SingleAccept in this.SingleAcceptReceiveSends)SingleAccept.Open(CancellationToken);
    }
    /// <summary>
    /// 非同期でAcceptを終了させる
    /// </summary>
    public void BeginClose() {
        foreach(var SingleAccept in this.SingleAcceptReceiveSends)SingleAccept.BeginClose();
        //this.CountdownEvent.Signal();
        this.CancellationTokenSourceループRequestResponse.Cancel();
    }
    /// <summary>
    /// BeginCloseが完了するまで待つ。
    /// </summary>
    public void EndClose(){
        foreach(var SingleAccept in this.SingleAcceptReceiveSends) {
            SingleAccept.EndClose();
        }
        this.CountdownEvent.Wait();
    }
    /// <summary>
    /// 同期でAcceptを終了させる
    /// </summary>
    public void Close(){
        if(this.CountdownEvent.IsSet)return;
        this.BeginClose();
        this.EndClose();
    }
    /// <summary>
    /// ファイナライザ
    /// </summary>
    ~Server() => this.Dispose(false);
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose() {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Disposeされているか
    /// </summary>
    public bool IsDisposed {get; private set;}
    private void Dispose(bool disposing) {
        if(!this.IsDisposed) {
            this.IsDisposed=true;
            if(disposing) {
                this.Close();
                //this.RequestResponseSingleReceiveSends.Dispose()するとToken待ちのTokenがCancelしたとき例外が発生する。
                this.CountdownEvent.Dispose();
                //Open前に終了した場合
                this.CancellationTokenSourceループRequestResponse?.Dispose();
                //Debug.Assert(this.CancellationTokenSource is not null);
                //this.CancellationTokenSource!.Dispose();
                this.RequestResponseSingleReceiveSends.Dispose();
            }
        }
    }
}