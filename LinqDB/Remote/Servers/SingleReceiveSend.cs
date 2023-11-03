using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using Serializers=LinqDB.Serializers;
//using MemoryStream = System.IO.MemoryStream;
using MemoryStream = LinqDB.Helpers.CommonLibrary.MemoryStream;
using static LinqDB.Helpers.Configulation;
using static LinqDB.Helpers.CommonLibrary;

//using NetworkStream = Lite.Collections.NetworkStream;
// ReSharper disable ConvertToLocalFunction

//CancelするときというのはRecieveCompleateで==0を受け取ってDisposeしたとき。
//CancelすればSend.Takeでの待ちが解除される。
namespace LinqDB.Remote.Servers;

/// <summary>
/// 受信してから送信する方式。サーバーによくあるパターン。
/// </summary>
internal class SingleReceiveSend:IDisposable{
    private readonly Serializers.Utf8Json.Serializer Utf8Json=new();
    private readonly Serializers.MessagePack.Serializer MessagePack=new();
    private readonly Serializers.MemoryPack.Serializer MemoryPack=new();
    private readonly SHA256 Provider=SHA256.Create();
    private readonly byte[] Buffer = new byte[MemoryStreamBufferSize];
    /// <summary>
    /// 実際の送受信するストリーム。
    /// </summary>
    [NonSerialized]
#pragma warning disable CA2213 // Disposable fields should be disposed
    private Stream Stream=null!;
#pragma warning restore CA2213 // Disposable fields should be disposed
    private readonly MultiReceiveSend MultiReceiveSend;
    //private readonly Action<Object> Delegate受信;
    //private readonly Action<Task,Object> Delegate受信終了 = (Task,Object) => Trace_WriteLine(0,"Server.Function受信終了");
    //private readonly AsyncCallback Delegate受信終了;
    //private readonly Action<Object> Delegate送信;
    //private readonly AsyncCallback Delegate送信終了;
    private readonly int Index;
    //private readonly MessagePackCustomSerializer MessagePackCustomSerializer=new();
    //private readonly CustomSerializerMemoryPack CustomSerializerMemoryPack=new();
    //private readonly SerializerConfiguration SerializerConfiguration;
    //private readonly Serializers.Utf8Json.Resolver Utf8Json_Resolver=new();
    //private readonly IJsonFormatterResolver JsonFormatterResolver;
    //private readonly Serializers.MessagePack.Resolver MessagePack_Resolver=new();
    //private readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    //private static readonly ScriptOptions options = ScriptOptions.Default.AddReferences(typeof(object).Assembly).AddImports("System");
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="MultiReceiveSend"></param>
    /// <param name="Index"></param>
    public SingleReceiveSend(MultiReceiveSend MultiReceiveSend,int Index){
        Debug.WriteLine("11");
        this.MultiReceiveSend=MultiReceiveSend;
        this.Index=Index;
        //this.MemoryStream=new System.IO.MemoryStream(this.Buffer);
        this.MemoryStream=new MemoryStream(this.Buffer);
        Debug.WriteLine("12");

        //this.JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
        //    //順序が大事
        //    this.Utf8Json_Resolver,
        //    Utf8Json.Resolvers.StandardResolver.AllowPrivate,
        //    Utf8Json.Resolvers.StandardResolver.Default
        //);
        //this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
        //    MessagePack.Resolvers.CompositeResolver.Create(
        //        this.MessagePack_Resolver,
        //        MessagePack.Resolvers.StandardResolver.Instance,
        //        MessagePack.Resolvers.DynamicContractlessObjectResolverAllowPrivate.Instance
        //    )
        //);
        //this.Delegate受信=this.Function受信;
        //this.Delegate受信終了=this.Function受信終了;
        //this.Delegate送信=this.Function送信;
        //this.Delegate送信終了=this.Function送信終了;
    }
    //private readonly IJsonFormatterResolver JsonFormatterResolver = Utf8Json.Resolvers.CompositeResolver.Create(
    //    //順序が大事
    //    new Serializers.Utf8Json.Resolver(),
    //    Utf8Json.Resolvers.StandardResolver.Default);
    //private readonly MessagePackSerializerOptions MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
    //    MessagePack.Resolvers.CompositeResolver.Create(
    //        MessagePack.Resolvers.StandardResolver.Instance,
    //        new Serializers.MessagePack.Resolver()
    //    )
    //);
    private int count;

    private async Task Function受信Async(Socket Socket,CancellationToken CancellationToken) {
        Trace_WriteLine(0,"Server.Function受信");
        // 4:バイト数
        //64:SHA256
        // 1:公開鍵暗号化アルゴリズム
        //  :ユーザ名
        //  :パスワード
        // 1:リクエストヘッダ
        //  :本体
        try {
            var NetworkStream = this.NetworkStream=new NetworkStream(Socket) {
                ReadTimeout=this.ReadTimeout,
                WriteTimeout=this.WriteTimeout
            };
            var buffer = new byte[4];
            Stream Stream;
            await NetworkStream.ReadAsync(buffer,CancellationToken).ConfigureAwait(false);
            var SslProtocol0 = buffer[0];
            var SslProtocol1 = buffer[1];
            var SslProtocol2 = buffer[2];
            var SslProtocol3 = buffer[3];
            //var SslProtocol = (SslProtocols)NetworkStream.ReadByte();
#pragma warning disable CA5397 // SslProtocols の非推奨の値を使用しない
            var SslProtocol = (SslProtocols)((SslProtocol0<<0)|(SslProtocol1<<8)|(SslProtocol2<<16)|(SslProtocol3<<24));
#pragma warning restore CA5397 // SslProtocols の非推奨の値を使用しない
            if(SslProtocol!=SslProtocols.None) {
                var X509Certificate = this.X509Certificate;
                if(X509Certificate is null) {
                    this.Privateデシリアライズした(
                        LinqDB.Request.Exception_ThrowException,
                        ExceptionのString(new InvalidOperationException("SSL接続を要求したが証明書が無かった。")),
                        SerializeType.Utf8Json
                    );
                    return;
                }
                var SslStream = new SslStream(
                    NetworkStream,
                    true,
                    DelegateRemoteCertificateValidationCallback
                );
                await SslStream.AuthenticateAsServerAsync(X509Certificate,true,SslProtocol,true).ConfigureAwait(false);
                Trace_WriteLine(1,"Server.Function受信 AuthenticateAsServer");
                Stream=SslStream;
            } else {
                Stream=NetworkStream;
            }
            this.Stream=Stream;
            var Length0 = Stream.ReadByte();
            var Length1 = Stream.ReadByte();
            var Length2 = Stream.ReadByte();
            var Length3 = Stream.ReadByte();
            if(Length0<0||Length1<0||Length2<0||Length3<0) {
                this.Privateデシリアライズした(
                    LinqDB.Request.Exception_ThrowException,
                    ExceptionのString(new InvalidOperationException("Length0が負だった。")),
                    SerializeType.Utf8Json
                );
                return;
            }
            var 受信データとハッシュのバイト数 = (Length0<<0)|(Length1<<8)|(Length2<<16)|(Length3<<24);
            if(受信データとハッシュのバイト数>MemoryStreamBufferSize) {
                this.Privateデシリアライズした(
                    LinqDB.Request.Exception_ThrowException,
                    ExceptionのString(
                        new InvalidOperationException(
                            $"Lengthが{受信データとハッシュのバイト数}バイトで{MemoryStreamBufferSize}({nameof(MemoryStreamBufferSize)})を超えてはいけない。"
                        )
                    ),
                    SerializeType.Utf8Json
                );
                return;
            }
            var 受信データのバイト数 = 受信データとハッシュのバイト数-ハッシュバイト数;
            var Buffer = this.Buffer;
            var ReadOffset = 0;
            var 残りバイト数 = 受信データとハッシュのバイト数;
            do {
                var Memory = Buffer.AsMemory(ReadOffset,残りバイト数);
                var ReadしたBytes = await Stream.ReadAsync(Memory,CancellationToken).ConfigureAwait(false);
                ReadOffset+=ReadしたBytes;
                残りバイト数-=ReadしたBytes;
                this.count++;
            } while(残りバイト数>0);
            var Provider = this.Provider;
            Provider.Initialize();
            Provider.ComputeHash(Buffer,0,受信データのバイト数);
            var 受信Hash = Provider.Hash!;
            for(var a = 0;a<ハッシュバイト数;a++) {
                if(受信Hash[a]!=Buffer[a+受信データのバイト数]) {
                    throw new InvalidDataException("ハッシュ値が一致しなかった");
                }
            }
            var MemoryStream = this.MemoryStream;
            MemoryStream.SetLength(受信データのバイト数);
            MemoryStream.Position=0;
            //leaveOpenとはtrueだとReader.CloseされてもStream.Closeしないということ。
            using(var Reader = new BinaryReader(MemoryStream,Encoding.Unicode,true)) {
                var User = Reader.ReadString();
                var PasswordHash = Reader.ReadString();
                if(PasswordHash.Length!=64) {
                    throw new InvalidDataException("パスワードハッシュは64文字必要だったが"+PasswordHash.Length+"文字だった。");
                }
                var UserPasswordDictionary = this.MultiReceiveSend.Server.UserPasswordDictionary;
                if(!UserPasswordDictionary.Authentication(User,PasswordHash)) {
                    this.Privateデシリアライズした(
                        LinqDB.Request.Exception_ThrowException,
                        ExceptionのString(new InvalidOperationException("ユーザー名かパスワードが間違っている。")),
                        SerializeType.Utf8Json
                    );
                    return;
                }
            }
            var Request = (Request)MemoryStream.ReadByte();
            if(Request==Request.CommunicateClient_ReceiveでOutOfMemoryException) {
                throw new OutOfMemoryException(Request.ToString());
            }
            Trace_WriteLine(2,$"Server.Function受信 {Request}");
            switch(Request) {
                case Request.Bytes0_Bytes0: {
                    var result = MemoryStream.ReadByte();
                    if(result>=0) {
                        throw new InvalidOperationException();
                    }
                    this.Privateデシリアライズした(
                        Request.Bytes0_Bytes0,
                        default(object),
                        SerializeType.MemoryPack
                    );
                    break;
                }
                case Request.Byte_Byte: {
                    var result = MemoryStream.ReadByte();
                    if(result<0)
                        throw new InvalidOperationException();
                    this.Privateデシリアライズした(
                        Request.Byte_Byte,
                        (byte)result,
                        SerializeType.MemoryPack
                    );
                    break;
                }
                case Request.BytesN_BytesN: {
                    var List = new List<byte>();
                    while(true) {
                        var Int32 = MemoryStream.ReadByte();
                        if(Int32<0) {
                            break;
                        }
                        List.Add((byte)Int32);
                    }
                    this.Privateデシリアライズした(
                        Request.BytesN_BytesN,
                        List,
                        SerializeType.MemoryPack
                    );
                    break;
                }
                case Request.TimeoutException_ThrowException: {
                    var SerializeType = (SerializeType)MemoryStream.ReadByte();
                    Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
                    throw new TimeoutException("テスト");
                }
                case Request.Object_Object: {
                    var SerializeType = (SerializeType)MemoryStream.ReadByte();
                    Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
                    try {
                        var Object =SerializeType switch{
                            SerializeType.MemoryPack=>this.MemoryPack.Deserialize<string>(MemoryStream),
                            SerializeType.MessagePack=>this.MessagePack.Deserialize<string>(MemoryStream),
                            SerializeType.Utf8Json=>this.Utf8Json.Deserialize<string>(MemoryStream),
                            _=>throw new NotSupportedException(SerializeType.ToString())
                        };
                        this.Privateデシリアライズした(
                            Request,
                            Object,
                            SerializeType
                        );
                    } catch(SerializationException ex) {
                        Trace_WriteLine(3,"Server.Function受信 catch(SerializationException)");
                        this.Privateデシリアライズした(
                            Request.Exception_ThrowException,
                            ExceptionのString(ex),
                            SerializeType
                        );
                    }
                    break;
                }
                case Request.Delegate_Invoke:
                case Request.Expression_Invoke: {
                    var SerializeType = (SerializeType)MemoryStream.ReadByte();
                    Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
                    try {
                        ////パラメーター_ステートメント パラメーター_ステートメント;
                        ////String パラメーター,ステートメント;
                        ////if(SerializeType==SerializeType.Utf8Json) {
                        ////    パラメーター_ステートメント=Utf8Json.JsonResolver.Serializer().Deserialize<パラメーター_ステートメント>(MemoryStream);
                        ////    //ParameterName =Utf8Json.JsonResolver.Serializer().Deserialize<String>(MemoryStream);
                        ////    //ステートメント=Utf8Json.JsonResolver.Serializer().Deserialize<String>(MemoryStream);
                        ////    パラメーター=パラメーター_ステートメント.パラメーター;
                        ////    ステートメント=パラメーター_ステートメント.ステートメント;
                        ////} else{
                        ////    パラメーター=MessagePack.MessagePackResolver.Serializer().Deserialize<String>(MemoryStream);
                        ////    ステートメント=MessagePack.MessagePackResolver.Serializer().Deserialize<String>(MemoryStream);
                        ////}
                        ////var ParameterName= Utf8Json.JsonResolver.Serializer().Deserialize<String>(MemoryStream);
                        ////var ステートメント = Utf8Json.JsonResolver.Serializer().Deserialize<String>(MemoryStream);
                        //using var r = new BinaryReader(MemoryStream);
                        //var ParameterName = r.ReadString();
                        //var Statement = r.ReadString();
                        //Debug.Assert(Request==Request.Expression_Invoke);
                        //var Proxy = this.MultiReceiveSend.Server.Proxy!;
                        //if(ParameterName!="") {
                        //    Statement=$"var {ParameterName}=Entities;\r\n{Statement}";
                        //}
                        //var Script = CSharpScript.Create(
                        //    Statement,
                        //    options,
                        //    Proxy.GetType()
                        //);
                        ////var Compile=Script.Compile();
                        //Object=Script.CreateDelegate(CancellationToken);
                        ////var e = Script.CreateDelegate()("");
                        ////e.Wait();
                        ////var input = e.Result;
                        //break;
                        var Object= SerializeType switch{
                            SerializeType.Utf8Json   => this.Utf8Json.Deserialize<LambdaExpression>(MemoryStream),
                            SerializeType.MessagePack=> this.MessagePack.Deserialize<LambdaExpression>(MemoryStream),
                            SerializeType.MemoryPack => this.MemoryPack.Deserialize<LambdaExpression>(MemoryStream),
                            _=>throw new NotSupportedException(SerializeType.ToString())
                        };
                        this.Privateデシリアライズした(
                            Request,
                            Object,
                            SerializeType
                        );
                    } catch(SerializationException ex) {
                        Trace_WriteLine(4,"Server.Function受信 catch(SerializationException)");
                        this.Privateデシリアライズした(
                            Request.Exception_ThrowException,
                            ex,
                            SerializeType
                        );
                    }
                    break;
                }
                case Request.リモート先でOutOfMemoryException: {
                    throw new OutOfMemoryException("サーバー側で発生");
                    //this.Privateデシリアライズした(
                    //    Request.Exception_ThrowException,
                    //    new OutOfMemoryException("サーバー側で発生"),
                    //    SerializeType.Utf8Json
                    //);
                    //break;
                }
                default: throw new InvalidDataException("不正な通信方式"+Request);
            }
        } catch(ObjectDisposedException) {
            Trace_WriteLine(5,"Server.Function受信 catch(ObjectDisposedException)");
        } catch(IOException) {
            var Socket0 = this.Socket;
            //Debug.Assert(Socket0 is not null);
            Socket0!.Shutdown(SocketShutdown.Both);
            Socket0.Dispose();
            Trace_WriteLine(6,"Server.Function受信 catch(IOException)");
        } catch(OperationCanceledException) {
            Trace_WriteLine(7,"Server.Function受信 catch(OperationCanceledException)");
#pragma warning disable CA1031 // 一般的な例外の種類はキャッチしません
        } catch(Exception ex) {
#pragma warning restore CA1031 // 一般的な例外の種類はキャッチしません
            this.Privateデシリアライズした(
                Request.Exception_ThrowException,
                ex,
                SerializeType.Utf8Json
            );
            Trace_WriteLine(8,$"Server.Function受信 catch({ex.GetType().FullName})");
            //throw;
        }
    }
    //private readonly byte[] Header= new byte[MemoryStreamBufferSize];
    private void Function受信(Socket Socket,CancellationToken CancellationToken) {
        Trace_WriteLine(0,"Server.Function受信");
        // 4:バイト数
        //64:SHA256
        // 1:公開鍵暗号化アルゴリズム
        //  :ユーザ名
        //  :パスワード
        // 1:リクエストヘッダ
        //  :本体
        try {
            var NetworkStream = this.NetworkStream=new NetworkStream(Socket) {
                ReadTimeout=this.ReadTimeout,
                WriteTimeout=this.WriteTimeout
            };
            var Buffer=this.Buffer;
            Stream Stream;
            //NetworkStream.ReadByte()
            //var バイト数=NetworkStream.Read(Buffer,0,4);
            //Debug.Assert(バイト数==4);
            var SslProtocol0=NetworkStream.ReadByte();
            var SslProtocol1=NetworkStream.ReadByte();
            var SslProtocol2=NetworkStream.ReadByte();
            var SslProtocol3=NetworkStream.ReadByte();
            //var SslProtocol = (SslProtocols)NetworkStream.ReadByte();
#pragma warning disable CA5397 // SslProtocols の非推奨の値を使用しない
            var SslProtocol = (SslProtocols)((SslProtocol0<<0)|(SslProtocol1<<8)|(SslProtocol2<<16)|(SslProtocol3<<24));
#pragma warning restore CA5397 // SslProtocols の非推奨の値を使用しない
            if(SslProtocol!=SslProtocols.None) {
                var X509Certificate = this.X509Certificate;
                if(X509Certificate is null) {
                    this.Privateデシリアライズした(
                        LinqDB.Request.Exception_ThrowException,
                        ExceptionのString(new InvalidOperationException("SSL接続を要求したが証明書が無かった。")),
                        SerializeType.Utf8Json
                    );
                    return;
                }
                var SslStream = new SslStream(
                    NetworkStream,
                    true,
                    DelegateRemoteCertificateValidationCallback
                );
                SslStream.AuthenticateAsServer(X509Certificate,true,SslProtocol,true);
                Trace_WriteLine(1,"Server.Function受信 AuthenticateAsServer");
                Stream=SslStream;
            } else {
                Stream=NetworkStream;
            }
            this.Stream=Stream;
            var Length0 = Stream.ReadByte();
            var Length1 = Stream.ReadByte();
            var Length2 = Stream.ReadByte();
            var Length3 = Stream.ReadByte();
            if(Length0<0||Length1<0||Length2<0||Length3<0) {
                this.Privateデシリアライズした(
                    LinqDB.Request.Exception_ThrowException,
                    ExceptionのString(new InvalidOperationException("Length0が負だった。")),
                    SerializeType.Utf8Json
                );
                return;
            }
            var 受信データとハッシュのバイト数 = (Length0<<0)|(Length1<<8)|(Length2<<16)|(Length3<<24);
            if(受信データとハッシュのバイト数>MemoryStreamBufferSize) {
                this.Privateデシリアライズした(
                    LinqDB.Request.Exception_ThrowException,
                    ExceptionのString(
                        new InvalidOperationException(
                            $"Lengthが{受信データとハッシュのバイト数}バイトで{MemoryStreamBufferSize}({nameof(MemoryStreamBufferSize)})を超えてはいけない。"
                        )
                    ),
                    SerializeType.Utf8Json
                );
                return;
            }
            var 受信データのバイト数 = 受信データとハッシュのバイト数-ハッシュバイト数;
            //var Buffer = this.Buffer;
            var ReadOffset = 0;
            var 残りバイト数 = 受信データとハッシュのバイト数;
            do{
                //var Memory=Buffer.AsMemory(ReadOffset,残りバイト数);
                var ReadしたBytes=Stream.Read(Buffer,ReadOffset,残りバイト数);
                ReadOffset+=ReadしたBytes;
                残りバイト数-=ReadしたBytes;
                this.count++;
            } while(残りバイト数>0);
            var Provider = this.Provider;
            Provider.Initialize();
            Provider.ComputeHash(Buffer,0,受信データのバイト数);
            var 受信Hash = Provider.Hash!;
            for(var a = 0;a<ハッシュバイト数;a++) {
                if(受信Hash[a]!=Buffer[a+受信データのバイト数]) {
                    throw new InvalidDataException("ハッシュ値が一致しなかった");
                }
            }
            var MemoryStream = this.MemoryStream;
            MemoryStream.SetLength(受信データのバイト数);
            MemoryStream.Position=0;
            //leaveOpenとはtrueだとReader.CloseされてもStream.Closeしないということ。
            using(var Reader = new BinaryReader(MemoryStream,Encoding.Unicode,true)) {
                var User = Reader.ReadString();
                var PasswordHash = Reader.ReadString();
                if(PasswordHash.Length!=64) {
                    throw new InvalidDataException("パスワードハッシュは64文字必要だったが"+PasswordHash.Length+"文字だった。");
                }
                var UserPasswordDictionary = this.MultiReceiveSend.Server.UserPasswordDictionary;
                if(!UserPasswordDictionary.Authentication(User,PasswordHash)) {
                    this.Privateデシリアライズした(
                        LinqDB.Request.Exception_ThrowException,
                        ExceptionのString(new InvalidOperationException("ユーザー名かパスワードが間違っている。")),
                        SerializeType.Utf8Json
                    );
                    return;
                }
            }
            var Request = (Request)MemoryStream.ReadByte();
            if(Request==Request.CommunicateClient_ReceiveでOutOfMemoryException) {
                throw new OutOfMemoryException(Request.ToString());
            }
            Trace_WriteLine(2,$"Server.Function受信 {Request}");
            switch(Request) {
                case Request.Bytes0_Bytes0: {
                    var result = MemoryStream.ReadByte();
                    if(result>=0) {
                        throw new InvalidOperationException();
                    }
                    this.Privateデシリアライズした(
                        Request.Bytes0_Bytes0,
                        default(object),
                        SerializeType.MemoryPack
                    );
                    break;
                }
                case Request.Byte_Byte: {
                    var result = MemoryStream.ReadByte();
                    if(result<0)
                        throw new InvalidOperationException();
                    this.Privateデシリアライズした(
                        Request.Byte_Byte,
                        (byte)result,
                        SerializeType.MemoryPack
                    );
                    break;
                }
                case Request.BytesN_BytesN: {
                    var List = new List<byte>();
                    while(true) {
                        var Int32 = MemoryStream.ReadByte();
                        if(Int32<0) {
                            break;
                        }
                        List.Add((byte)Int32);
                    }
                    this.Privateデシリアライズした(
                        Request.BytesN_BytesN,
                        List,
                        SerializeType.MemoryPack
                    );
                    break;
                }
                case Request.TimeoutException_ThrowException: {
                    var SerializeType = (SerializeType)MemoryStream.ReadByte();
                    Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
                    throw new TimeoutException("テスト");
                }
                case Request.Object_Object: {
                    var SerializeType = (SerializeType)MemoryStream.ReadByte();
                    Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
                    try{
                        var Object= SerializeType switch{
                            SerializeType.MemoryPack =>this.MemoryPack.Deserialize<object>(MemoryStream),
                            SerializeType.MessagePack=>this.MessagePack.Deserialize<object>(MemoryStream),
                            _                        =>this.Utf8Json.Deserialize<object>(MemoryStream)
                        };
                        //var Object=SerializeType==SerializeType.Utf8Json
                        //    ?JsonResolver.Serializer().Deserialize<object>(MemoryStream,this.CustomSerializerUtf8Json.Resolver)
                        //    :MessagePackResolver.Serializer().Deserialize<object>(MemoryStream,this.CustomSerializerMessagePack.Options,CancellationToken);
                        this.Privateデシリアライズした(
                            Request,
                            Object,
                            SerializeType
                        );
                    } catch(SerializationException ex) {
                        Trace_WriteLine(3,"Server.Function受信 catch(SerializationException)");
                        this.Privateデシリアライズした(
                            Request.Exception_ThrowException,
                            ExceptionのString(ex),
                            SerializeType
                        );
                    }
                    break;
                }
                case Request.Delegate_Invoke:
                case Request.Expression_Invoke: {
                    var SerializeType = (SerializeType)MemoryStream.ReadByte();
                    Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
                    try {
                        //string s = Encoding.UTF8.GetString(this.Buffer,(int)this.MemoryStream.Position,
                        //    (int)(this.MemoryStream.Length-this.MemoryStream.Position));
                        //var s1 = format_json(s);
                        //File.WriteAllText("受診Json.json",s1);
                        //var o=JsonResolver.Serializer().Deserialize<LambdaExpression>(MemoryStream,
                        //    this.SerializerConfiguration.JsonFormatterResolver);
                        //var Lambda=JsonResolver.Serializer().Deserialize<LambdaExpression>(MemoryStream,this.SerializerConfiguration.JsonFormatterResolver);
                        var Object= SerializeType switch{
                            SerializeType.MemoryPack =>this.MemoryPack.Deserialize<Expression>(MemoryStream),
                            SerializeType.MessagePack=>this.MessagePack.Deserialize<Expression>(MemoryStream),
                            _                        =>this.Utf8Json.Deserialize<Expression>(MemoryStream)
                        };
                        this.Privateデシリアライズした(
                            Request,
                            Object,
                            SerializeType
                        );
                    } catch(SerializationException ex) {
                        Trace_WriteLine(4,"Server.Function受信 catch(SerializationException)");
                        this.Privateデシリアライズした(
                            Request.Exception_ThrowException,
                            ex,
                            SerializeType
                        );
                    }
                    break;
                }
                case Request.リモート先でOutOfMemoryException: {
                    this.Privateデシリアライズした(
                        Request.Exception_ThrowException,
                        new OutOfMemoryException("サーバー側で発生"),
                        SerializeType.Utf8Json
                    );
                    break;
                }
                default: throw new InvalidDataException("不正な通信方式"+Request);
            }
        } catch(ObjectDisposedException) {
            Trace_WriteLine(5,"Server.Function受信 catch(ObjectDisposedException)");
        } catch(IOException) {
            var Socket0 = this.Socket;
            //Debug.Assert(Socket0 is not null);
            Socket0!.Shutdown(SocketShutdown.Both);
            Socket0.Dispose();
            Trace_WriteLine(6,"Server.Function受信 catch(IOException)");
        } catch(OperationCanceledException) {
            Trace_WriteLine(7,"Server.Function受信 catch(OperationCanceledException)");
#pragma warning disable CA1031 // 一般的な例外の種類はキャッチしません
        } catch(Exception ex) {
#pragma warning restore CA1031 // 一般的な例外の種類はキャッチしません
            this.Privateデシリアライズした(
                Request.Exception_ThrowException,
                ex,
                SerializeType.Utf8Json
            );
            Trace_WriteLine(8,$"Server.Function受信 catch({ex.GetType().FullName})");
            //throw;
        }
    }
    private void Function受信終了(IAsyncResult IAsyncResult) {
        Trace_WriteLine(0,"Server.Function受信終了");
    }
    private void Function送信(object Object) {
        Trace_WriteLine(0,"Server.Function送信");
        var シリアライズしたい = (シリアライズしたい)Object;
        var MemoryStream = this.MemoryStream;
        MemoryStream.SetLength(4);
        MemoryStream.Position=4;
        var Response = シリアライズしたい.Response;
        Trace_WriteLine(1,$"Server.Function送信 {Response}");
        try {
            MemoryStream.WriteByte((byte)Response);
            switch(Response) {
                case Response.Bytes0: {
                    break;
                }
                case Response.Byte: {
                    MemoryStream.WriteByte((byte)シリアライズしたい.Object!);
                    break;
                }
                case Response.BytesN: {
                    var List = (List<byte>)シリアライズしたい.Object!;
                    foreach(var a in List) {
                        MemoryStream.WriteByte(a);
                    }
                    //Trace_Write("Delegate送信3");
                    break;
                }
                case Response.Object:
                case Response.ThrowException: {
                    var SerializeType = シリアライズしたい.SerializeType;
                    MemoryStream.WriteByte((byte)SerializeType);
                    switch(SerializeType) {
                        case SerializeType.MemoryPack: this.MemoryPack.Serialize(MemoryStream, シリアライズしたい.Object);break;
                        case SerializeType.MessagePack: this.MessagePack.Serialize<object>(MemoryStream, シリアライズしたい.Object);break;
                        case SerializeType.Utf8Json: this.Utf8Json.Serialize<object>(MemoryStream, シリアライズしたい.Object);break;
                        default:throw new NotSupportedException(SerializeType.ToString());
                    }
                    break;
                }
            }
            if(this.Buffer.Length>MemoryStreamBufferSize) {
                Trace_WriteLine(2,$"Server.Function送信 {this.Buffer.Length}Bytes,Buffer {MemoryStreamBufferSize}Bytes");
            } else {
                BufferにLengthとSHA256を設定してStreamにWrite();
                Trace_WriteLine(3,"Server.Function送信 サーバーが正常に送信できた");
            }
        } catch(NotSupportedException ex) {
            //メモリがいっぱい
            Trace_WriteLine(4,$"Server.Function送信 catch(NotSupportedException){ex}");
            MemoryStream.SetLength(4);
            MemoryStream.Position=4;
            MemoryStream.WriteByte((byte)Response.ThrowException);
            //using(var XmlWriter=CreateXmlWriter(MemoryStream,シリアライズしたい.Xmlの表現形式)){
            //    ExpressionSurrogateSelector.serializer.WriteObject(
            //        XmlWriter,
            //        ex
            //    );
            //}
            var SerializeType = シリアライズしたい.SerializeType;
            switch(SerializeType) {
                case SerializeType.Utf8Json: this.Utf8Json.Serialize<object>(MemoryStream, ex);break;
                case SerializeType.MessagePack: this.MessagePack.Serialize<object>(MemoryStream, ex);break;
                case SerializeType.MemoryPack: this.MemoryPack.Serialize<object>(MemoryStream, ex);break;
                default:throw new NotSupportedException(SerializeType.ToString());
            }
            BufferにLengthとSHA256を設定してStreamにWrite();
        } catch(ObjectDisposedException) {
            Trace_WriteLine(5,"Server.Function送信 catch(ObjectDisposedException)");
        } catch(IOException ex) {
            Trace_WriteLine(6,$"Server.Function送信 catch(IOException)サーバーがクライアントにWriteしたらクライアントが切断してきた。{ex.Message} {ex.StackTrace}");
        } catch(IndexOutOfRangeException ex) {
            Trace_WriteLine(7,$"Server.Function送信 catch(IndexOutOfRangeException)MemoryStream.Bufferのサイズが足りなかった。{ex.Message} {ex.StackTrace}");
        } catch(Exception ex) {
            Trace_WriteLine(8,$"Server.Function送信 catch(Exception){ex.Message} {ex.StackTrace}");
            throw;
        }
        Trace_WriteLine(0,"Server.Function送信終了");
        try {
            //BeginCloseでthis.Socket=nullされている可能性がある。
            if(this.Socket is not null) {
                this.Socket.Dispose();
            }
        } catch(ObjectDisposedException) {
            Trace_WriteLine(1,"Server.Function送信終了 catch(ObjectDisposedException)");
        } catch(Exception ex) {
            Trace_WriteLine(2,$"Server.Function送信終了 catch(ObjectDisposedException){ex.Message} {ex.StackTrace}");
            throw;
        } finally {
            this.NetworkStream=null;
            this.MultiReceiveSend.未使用のSingleReceiveSends.Add(this.Index);
            Trace_WriteLine(3,"Server.Function送信終了 finally");
        }
        void BufferにLengthとSHA256を設定してStreamにWrite() {
            var MemoryStream0 = this.MemoryStream;
            var Length = (int)MemoryStream0.Length;
            var Length除外全体バイト数 = Length-4;
            var 送信データとハッシュのバイト数 = Length除外全体バイト数+ハッシュバイト数;
            var Provider = this.Provider;
            Trace_WriteLine(0,"Server.Function送信.BufferにLengthとSHA256を設定してStreamにWrite");
            Provider.Initialize();
            var Buffer = this.Buffer;
            Provider.ComputeHash(Buffer,4,Length除外全体バイト数);
            Trace_WriteLine(1,"Server.Function送信.BufferにLengthとSHA256を設定してStreamにWrite");
            Buffer[0]=(byte)(送信データとハッシュのバイト数>>0);
            Buffer[1]=(byte)(送信データとハッシュのバイト数>>8);
            Buffer[2]=(byte)(送信データとハッシュのバイト数>>16);
            Buffer[3]=(byte)(送信データとハッシュのバイト数>>24);
            var Provider_Hash = Provider.Hash!;
            for(var a = 0;a<ハッシュバイト数;a++) {
                Buffer[a+Length]=Provider_Hash[a];
            }
            var Stream = this.Stream;
            Stream.Write(Buffer,0,Length+ハッシュバイト数);
            Stream.Flush();
            Trace_WriteLine(2,"Server.Function送信.BufferにLengthとSHA256を設定してStreamにWrite");
        }
    }
    private void Function送信終了(IAsyncResult IAsyncResult) {
        Trace_WriteLine(0,"Server.Function送信終了");
        var Socket = this.Socket;
        try {
            //BeginCloseでthis.Socket=nullされている可能性がある。
            if(Socket is not null) {
                Socket.Dispose();
            }
        } catch(ObjectDisposedException) {
            Trace_WriteLine(1,"Server.Function送信終了 catch(ObjectDisposedException)");
        } catch(Exception ex) {
            Trace_WriteLine(2,$"Server.Function送信終了 catch(ObjectDisposedException){ex.Message} {ex.StackTrace}");
            throw;
        } finally {
            this.NetworkStream=null;
            this.MultiReceiveSend.未使用のSingleReceiveSends.Add(this.Index);
            Trace_WriteLine(3,"Server.Function送信終了 finally");
        }
    }
    /// <summary>
    /// バッファストリーム。
    /// </summary>
    [NonSerialized]
    private readonly MemoryStream MemoryStream;
    //private readonly System.IO.MemoryStream MemoryStream;
    /// <summary>
    /// 受信バッファサイズ
    /// </summary>
    public int ReceiveBufferSize{
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
        get => this.Socket.ReceiveBufferSize;
        set=>this.Socket.ReceiveBufferSize=value;
    }
    /// <summary>
    /// 送信バッファサイズ
    /// </summary>
    public int SendBufferSize{
        get=>this.Socket.SendBufferSize;
        set => this.Socket.SendBufferSize=value;
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
    }

    private int _ReadTimeout= 既定のタイムアウト;
    /// <summary>
    /// SocketのReceiveタイムアウト
    /// </summary>
    public int ReadTimeout{
        get=>this._ReadTimeout;
        set{
            this._ReadTimeout=value;
            if(this.NetworkStream is not null){
                this.NetworkStream.ReadTimeout=value;
            }
        }
    }

    private int _WriteTimeout= 既定のタイムアウト;
    /// <summary>
    /// SocketのSendタイムアウト
    /// </summary>
    public int WriteTimeout{
        get=>this._WriteTimeout;
        set{
            this._WriteTimeout=value;
            if(this.NetworkStream is not null){
                this.NetworkStream.WriteTimeout=value;
            }
        }
    }
    private Socket? Socket;
    private NetworkStream? NetworkStream;
    internal デシリアライズした デシリアライズした;
    //internal Logic Logic=>this.MultiReceiveSend.Logic;
    internal Server Server => this.MultiReceiveSend.Server;
    private void Privateデシリアライズした(Request Request,object? Object,SerializeType SerializeType) {
        this.デシリアライズした=new デシリアライズした(
            this,
            Request,
            Object,
            SerializeType
        );
        this.Server.Add(this);
    }
    private void Privateデシリアライズした(Request Request,Exception Exception,SerializeType SerializeType)=>
        this.Privateデシリアライズした(
            Request,
            ExceptionのString(Exception),
            SerializeType
        );
    //private readonly Optimizer.作成_DynamicMethodによるDelegate 作成DynamicMethodによるDelegate=new Optimizer.作成_DynamicMethodによるDelegate(
    //    new Optimizer.作業配列(),
    //    new Optimizer.ExpressionEqualityComparer(new List<ParameterExpression>())
    //);
    //private readonly Optimizer.作成_DynamicAssemblyによるDelegate 作成_DynamicAssemblyによるDelegate = new Optimizer.作成_DynamicAssemblyによるDelegate(
    //    new Optimizer.作業配列(),
    //    new Optimizer.ExpressionEqualityComparer(new List<ParameterExpression>())
    //);
    private static readonly RemoteCertificateValidationCallback DelegateRemoteCertificateValidationCallback=RemoteCertificateValidationCallback;
    //証明書の内容を表示するメソッド
    private static bool RemoteCertificateValidationCallback(
        object sender,
        X509Certificate? Certificate,
        X509Chain? Chain,
        SslPolicyErrors SslPolicyErrors){
        //const String Subject=@"Subject={0}";
        //if(Certificate is not null){
        //    Trace_WriteLine(@"===========================================");
        //    Trace_WriteLine(Subject,Certificate.Subject);
        //    Trace_WriteLine(Subject,Certificate.Issuer);
        //    Trace_WriteLine(Subject,Certificate.GetFormat());
        //    Trace_WriteLine(Subject,Certificate.GetExpirationDateString());
        //    Trace_WriteLine(Subject,Certificate.GetEffectiveDateString());
        //    Trace_WriteLine(Subject,Certificate.GetKeyAlgorithm());
        //    Trace_WriteLine(Subject,Certificate.GetPublicKeyString());
        //    Trace_WriteLine(Subject,Certificate.GetSerialNumberString());
        //    Trace_WriteLine(@"===========================================");
        //}
        //return true;
        if(SslPolicyErrors==SslPolicyErrors.None){
            Trace_WriteLine(1,Properties.Resources.サーバーでサーバー証明書の検証に成功した);
            return true;
        } else{
            //何かサーバー証明書検証エラーが発生している

            //SslPolicyErrors列挙体には、Flags属性があるので、
            //エラーの原因が複数含まれているかもしれない。
            //そのため、&演算子で１つ１つエラーの原因を検出する。
            if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateChainErrors)==
               SslPolicyErrors.RemoteCertificateChainErrors){
                Trace_WriteLine(2,Properties.Resources.サーバーでChainStatusが空でない配列を返した);
            }

            if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateNameMismatch)==
               SslPolicyErrors.RemoteCertificateNameMismatch){
                Trace_WriteLine(3,Properties.Resources.サーバーで証明書名が不一致だった);
            }

            if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateNotAvailable)==
               SslPolicyErrors.RemoteCertificateNotAvailable){
                Trace_WriteLine(4,Properties.Resources.サーバーで証明書が利用できなかった);
            }

            //検証失敗とする
            return false;
        }//サーバー証明書を検証せずに無条件に許可する
    }
    internal X509Certificate? X509Certificate=>this.MultiReceiveSend.X509Certificate;
    //private IAsyncResult? AsyncResult受信;
    //private Task? Task受信;
    /// <summary>
    /// Receiveを非同期で実行する。
    /// </summary>
    /// <param name="Socket"></param>
    /// <param name="CancellationToken"></param>
    /// <returns></returns>
    public void 受信(Socket Socket,CancellationToken CancellationToken){
        this.Socket=Socket;
        Socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.KeepAlive,1);
        Socket.ReceiveTimeout=int.MaxValue;
        Socket.SendTimeout=int.MaxValue;
        this.Stream=null!;
        //var _=this.Function受信Async(Socket,CancellationToken);
        this.Function受信(Socket,CancellationToken);
        //this.AsyncResult受信=this.Delegate受信.BeginInvoke(
        //    Socket,
        //    this.Delegate受信終了,
        //    null
        //);
    }
    //private IAsyncResult? AsyncResult送信;
    private Task? Task送信;
    /// <summary>
    /// Sendを非同期で実行する。
    /// </summary>
    /// <param name="Response"></param>
    /// <param name="Object"></param>
    /// <param name="SerializeType"></param>
    //internal void 送信(Response Response,Object? Object,SerializeType SerializeType = SerializeType.Utf8Json) => this.AsyncResult送信=this.Delegate送信.BeginInvoke(
    //    new シリアライズしたい(
    //        this,
    //        Response,
    //        SerializeType,
    //        Object
    //    ),
    //    this.Delegate送信終了,
    //    null
    //);
    internal void 送信(Response Response,object? Object,SerializeType SerializeType = SerializeType.Utf8Json) => this.Task送信=Task.Run(
        ()=>this.Function送信(
            new シリアライズしたい(
                this,
                Response,
                SerializeType,
                Object
            )
        )
    );
    /// <summary>
    /// スレッド終了を開始させる
    /// </summary>
    public void BeginClose(){
        if(this.NetworkStream is not null){
            this.NetworkStream.Dispose();
            this.NetworkStream=null;
        }
        this.Socket=null!;
    }
    //[NonSerialized]
    //private readonly WaitHandle[] WaitHandles = new WaitHandle[2];
    /// <summary>
    /// BeginCloseによるスレッド終了が完了するまで待つ
    /// </summary>
    public void EndClose(){
        //this.AsyncResult送信?.AsyncWaitHandle.WaitOne();
        //this.AsyncResult受信?.AsyncWaitHandle.WaitOne();
        this.Task送信?.Wait();
        //await this.Task送信.ConfigureAwait(false);
        //this.Task受信?.Wait();
    }
    /// <summary>
    /// 同期でスレッドを終了させる
    /// </summary>
    public void Close() {
        this.BeginClose();
        this.EndClose();
    }
    /// <summary>
    /// ファイナライザ
    /// </summary>
    ~SingleReceiveSend()=>this.Dispose(false);
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose(){
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// 破棄されているか
    /// </summary>
    public bool IsDisposed{
        get;
        private set;
    }
    private void Dispose(bool disposing){
        if(!this.IsDisposed) {
            this.IsDisposed=true;
            if(disposing) {
                this.Provider.Dispose();
                //this.Socket.Dispose()するのは別スレッドで動いているので難しい。
                this.MemoryStream.Dispose();
            }
        }
    }
}