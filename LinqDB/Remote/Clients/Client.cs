using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Xml;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Optimizers.VoidExpressionTraverser;
using LinqDB.Properties;
using static LinqDB.Helpers.CommonLibrary;
using static LinqDB.Helpers.Configulation;
using MemoryStream = LinqDB.Helpers.CommonLibrary.MemoryStream;

namespace LinqDB.Remote.Clients;

/// <summary>
/// リモートクラス。
/// </summary>
public class Client:IDisposable {
    private readonly Serializers.Utf8Json.Serializer Utf8Json=new();
    private readonly Serializers.MessagePack.Serializer MessagePack=new();
    private readonly Serializers.MemoryPack.Serializer MemoryPack=new();
    //[NonSerialized]
    //private readonly byte[]Buffer=new byte[MemoryStreamBufferSize];
    ///// <summary>
    ///// バッファストリーム。
    ///// </summary>
    //[NonSerialized]
    //private protected readonly MemoryStream MemoryStream;
    [NonSerialized]private readonly byte[] Hash1=new byte[HashLength+1];
    [NonSerialized]private readonly byte[] Hash2=new byte[HashLength+2];
    [NonSerialized]private byte[] PasswordHash=default!;
    //[NonSerialized]private byte[] WriteBuffer;
    [NonSerialized]private byte[] ReadBuffer=default!;
    private struct メルセンヌツィスター乱数 {
        /// <summary>
        /// 内部状態ベクトル総数
        /// </summary>
        private const int N=624;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const int M=397;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const uint MATRIX_A=0x9908b0dfU;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const uint UPPER_MASK=0x80000000U;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const uint LOWER_MASK=0x7fffffffU;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const uint TEMPER1=0x9d2c5680U;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const uint TEMPER2=0xefc60000U;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const int TEMPER3=11;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const int TEMPER4=7;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const int TEMPER5=15;

        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        private const int TEMPER6=18;

        /// <summary>
        /// 内部状態ベクトル。
        /// </summary>
        private readonly uint[] mt;

        /// <summary>
        /// 内部状態ベクトルのうち、次に乱数として使用するインデックス。
        /// </summary>
        private int mti;

        private readonly uint[] mag01;

        /// <summary>
        /// seedを種とした、メルセンヌツィスター擬似乱数ジェネレーターを初期化します。
        /// </summary>
        public メルセンヌツィスター乱数(int seed) {
            //内部状態配列初期化
            var mt=this.mt=new uint[N];
            mt[0]=(uint)seed;
            this.mti=N+1;
            this.mag01=new[] {
                0x0U,MATRIX_A
            };
            for(var a=1;a<N;a++)
                mt[a]=(uint)(1812433253*(mt[a-1]^(mt[a-1]>>30))+a);
        }

        public uint NextUInt32(uint Max)=>this.NextUInt32()%Max;

        /// <summary>
        /// 符号なし32bitの擬似乱数を取得します。
        /// </summary>
        private uint NextUInt32() {
            if(this.mti>=N) {
                var kk=1;
                uint p;
                var Y=this.mt[0]&UPPER_MASK;
                do {
                    p=this.mt[kk];
                    this.mt[kk-1]=this.mt[kk+(M-1)]^((Y|(p&LOWER_MASK))>>1)^this.mag01[p&1];
                    Y=p&UPPER_MASK;
                } while(++kk<N-M+1);
                do {
                    p=this.mt[kk];
                    this.mt[kk-1]=this.mt[kk+(M-N-1)]^((Y|(p&LOWER_MASK))>>1)^this.mag01[p&1];
                    Y=p&UPPER_MASK;
                } while(++kk<N);
                p=this.mt[0];
                this.mt[N-1]=this.mt[M-1]^((Y|(p&LOWER_MASK))>>1)^this.mag01[p&1];
                this.mti=0;
            }
            var y=this.mt[this.mti++];
            y^=y>>TEMPER3;
            y^=(y<<TEMPER4)&TEMPER1;
            y^=(y<<TEMPER5)&TEMPER2;
            y^=y>>TEMPER6;
            return y;
        }
    }
    //private メルセンヌツィスター乱数 乱数=new メルセンヌツィスター乱数(Environment.TickCount);
    //        internal UInt32 ID=(4<<0)+(3<<8)+(2<<16)+(1<<24);
    //internal UInt32 ID=('4'<<0)+('3'<<8)+('2'<<16)+('1'<<24);
    //      internal UInt32 ID=>this.乱数.NextUInt32(UInt32.MaxValue-1);
    ///// <summary>
    ///// どのリクエストに対するレスポンスかを判定するためランダムなID。メルセンヌツィスターがベストかな。
    ///// </summary>
    //internal UInt32 ID=>this.乱数.NextUInt32();
    //[NonSerialized] internal Socket _ConnectSocket;

    /// <summary>
    /// 受信タイムアウト。
    /// </summary>
    public int ReadTimeout{
        get;
        set;
    }

    /// <summary>
    /// 送信タイムアウト。
    /// </summary>
    public int WriteTimeout {
        get;
        set;
    }

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="WriteTimeout"></param>
    /// <param name="ReadTimeout"></param>
    public Client(string host,int port,int WriteTimeout=既定のタイムアウト,int ReadTimeout=既定のタイムアウト)
        :this(WriteTimeout,ReadTimeout,new DnsEndPoint(host,port,AddressFamily.InterNetwork)) {
    }
    /// <summary>
    /// 既定コンストラクタ。
    /// </summary>
    public Client():this(既定のタイムアウト,既定のタイムアウト,null!) {}
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="WriteTimeout">書き込みタイムアウト</param>
    /// <param name="ReadTimeout">読み込みタイムアウト</param>
    /// <param name="DnsEndPoint">接続先</param>
    private Client(int WriteTimeout,int ReadTimeout,DnsEndPoint DnsEndPoint){
        //this.WriteBuffer=new byte[65536];
        //this.MemoryStream=new MemoryStream(this.Buffer);
        //this.MemoryStream=new MemoryStream();
        this.WriteTimeout=WriteTimeout;
        this.ReadTimeout=ReadTimeout;
        this.DnsEndPoint=DnsEndPoint;
    }
    /// <summary>
    /// ファイナライザ
    /// </summary>
    ~Client()=>this.Dispose(false);
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose() {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// 破棄されているか
    /// </summary>
    public bool IsDisposed { get; private set; }
    /// <summary>
    /// 継承先のファイナライザでthis.Dispose(false)を呼び出す
    /// </summary>
    /// <param name="disposing"></param>
    protected void Dispose(bool disposing) {
        if(!this.IsDisposed) {
            this.IsDisposed=true;
            if(disposing) {
                this.Provider.Dispose();
                //this.MemoryStream.Dispose();
                this.取得_CSharp.Dispose();
            }
        }
    }
    /// <summary>
    /// 接続先
    /// </summary>
    public DnsEndPoint DnsEndPoint{get;set;}
    /*
    private String _Password;
    private SHA256 SHA256;
    /// <summary>
    /// 暗号化、復号化のパスワード
    /// </summary>
    public String Password{
        get=>this._Password;
        set {
            var SHA256 =this.SHA256=new SHA256();
            SHA256.ComputeHash(Encoding.Unicode.GetBytes(value));
            this._Password=value;
        }
    }
    */
    /// <summary>
    /// ユーザー名
    /// </summary>
    public string User{
        get;
        set;
    }="Administrator";

    /// <summary>
    /// 暗号化、復号化のパスワード
    /// </summary>
    public string Password{
        get;
        set;
    }="";
    private SslProtocols _SslProtocol;
    /// <summary>
    /// SSL暗号化の種類
    /// </summary>
    public SslProtocols SslProtocol=>this._SslProtocol;
    private X509Certificate? _X509Certificate;
    /// <summary>
    /// 証明書
    /// </summary>
    public X509Certificate? X509Certificate {
        get => this._X509Certificate;
        set {
            if(value is null) {
                this._SslProtocol=SslProtocols.None;
            } else if(this.SslProtocol==SslProtocols.None){
                this._SslProtocol=SslProtocols.Tls12;
            }
            this._X509Certificate=value;
        }
    }
    //private static readonly RemoteCertificateValidationCallback DelegateRemoteCertificateValidationCallbackDelegate = RemoteCertificateValidationCallback;
    //private static bool RemoteCertificateValidationCallback(object sender,X509Certificate? Certificate,X509Chain? chain,SslPolicyErrors SslPolicyErrors) {
    //    //if(Certificate is not null) {
    //    //    Console.WriteLine(@"===========================================");
    //    //    Console.WriteLine(@"Subject={0}",Certificate.Subject);
    //    //    Console.WriteLine(@"Subject={0}",Certificate.Issuer);
    //    //    Console.WriteLine(@"Subject={0}",Certificate.GetFormat());
    //    //    Console.WriteLine(@"Subject={0}",Certificate.GetExpirationDateString());
    //    //    Console.WriteLine(@"Subject={0}",Certificate.GetEffectiveDateString());
    //    //    Console.WriteLine(@"Subject={0}",Certificate.GetKeyAlgorithm());
    //    //    Console.WriteLine(@"Subject={0}",Certificate.GetPublicKeyString());
    //    //    Console.WriteLine(@"Subject={0}",Certificate.GetSerialNumberString());
    //    //    Console.WriteLine(@"===========================================");
    //    //}
    //    //return true;
    //    if(SslPolicyErrors == SslPolicyErrors.None) {
    //        Console.WriteLine(Resources.クライアントでサーバー証明書の検証に成功しなかった);
    //        return true;
    //    } else {
    //        //何かサーバー証明書検証エラーが発生している

    //        //SslPolicyErrors列挙体には、Flags属性があるので、
    //        //エラーの原因が複数含まれているかもしれない。
    //        //そのため、&演算子で１つ１つエラーの原因を検出する。
    //        if((SslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) ==
    //           SslPolicyErrors.RemoteCertificateChainErrors) {
    //            Console.WriteLine(Resources.クライアントでChainStatusが空でない配列を返した);
    //        }

    //        if((SslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) ==
    //           SslPolicyErrors.RemoteCertificateNameMismatch) {
    //            Console.WriteLine(Resources.クライアントで証明書名が不一致だった);
    //        }

    //        if((SslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) ==
    //           SslPolicyErrors.RemoteCertificateNotAvailable) {
    //            Console.WriteLine(Resources.クライアントで証明書が利用できなかった);
    //        }
    //        //検証失敗とする
    //        return false;
    //    }
    //}
    private readonly X509CertificateCollection X509CertificateCollection = new();
    /// <summary>
    /// NetworkStreamはusing用、SslStreamはNetworkStreamかSslStreamを取得する。
    /// </summary>
    /// <returns></returns>
    private void PasswordHashを設定(byte[] Header) {
        //var MemoryStream = this.MemoryStream;
        //MemoryStream.SetLength(4);
        //MemoryStream.Position=4;
        //using var BinaryWriter = new BinaryWriter(MemoryStream,Encoding.Unicode,true);
        //BinaryWriter.Write(this.User);
        //BinaryWriter.Write(GetPasswordHash(this.Password));
        //BinaryWriter.Flush();
        //MemoryStream.WriteByte((byte)Request);
        this.PasswordHash=Header;
        var Provider=this.Provider;
        //Provider.Initialize();
        //Provider.ComputeHash(Encoding.Unicode.GetBytes(this.User));
        //Array.Copy(Provider.Hash,0,Header,0,32);
        Provider.Initialize();
        Provider.ComputeHash(Encoding.Unicode.GetBytes(this.Password));
        Array.Copy(Provider.Hash,0,Header,0,32);
        //Array.Copy(Provider.Hash,0,Header,32,32);

    }
    //private Response WriteBufferを送信(byte[]WriteBuffer,byte[]Hash) {
    //    //Trace.WriteLine("Client");
    //    //Trace.WriteLine("Server.Function受信");
    //    //Trace.WriteLine("Server.Function受信終了");
    //    //Trace.WriteLine("Server.FunctionRequestResponse");
    //    //Trace.WriteLine("Server.Function送信");
    //    //Trace.WriteLine("Server.Function送信終了");
    //    //Trace.WriteLine("Client");
    //    //var MemoryStream = this.MemoryStream;
    //    //var Buffer = this.Buffer;
    //    //var WriteBuffer = this.WriteBuffer;
    //    //var Provider = this.Provider;
    //    ////var Length = WriteBuffer.Length;
    //    ////var Length除外全体バイト数 = Length-4;
    //    ////Debug.Assert(送信データとハッシュのバイト数<=ServerMemoryStreamBufferSize);
    //    ////送信データのバイト数
    //    //Provider.Initialize();
    //    //Provider.ComputeHash(WriteBuffer);
    //    //var Provider_Hash0 = Provider.Hash!;
    //    //for(var a = 0;a<ハッシュバイト数;a++)
    //    //    WriteBuffer[a+Length]=Provider_Hash0[a];
    //    // 4:バイト数(未確定)
    //    //64:SHA256(未確定)
    //    // 1:公開鍵暗号化アルゴリズム
    //    //  :ユーザ名
    //    //  :パスワード
    //    // 1:リクエストヘッダ
    //    //  :本体
    //    using var ConnectSocket = new Socket(SocketAddressFamily,SocketType.Stream,ProtocolType.Tcp) {
    //        //LingerState=Common.LingerState,
    //        ReceiveBufferSize=ClientReceiveBufferSize,
    //        SendBufferSize = ClientSendBufferSize,
    //        ReceiveTimeout=this.ReadTimeout,
    //        SendTimeout=this.WriteTimeout
    //    };
    //    ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.KeepAlive,1);
    //    //ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReadTimeout,this.ReadTimeout);
    //    //ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendTimeout,this.ReadTimeout);
    //    ConnectSocket.Connect(this.DnsEndPoint);
    //    Trace_WriteLine(0,"Client.送信 ConnectSocket.Connect");
    //    using Stream NetworkStream = new NetworkStream(ConnectSocket,false) {
    //        ReadTimeout= this.ReadTimeout,
    //        WriteTimeout= this.ReadTimeout
    //    };
    //    var SslProtocol = this.SslProtocol;
    //    NetworkStream.WriteByte((byte)((int)SslProtocol>>0));
    //    NetworkStream.WriteByte((byte)((int)SslProtocol>>8));
    //    NetworkStream.WriteByte((byte)((int)SslProtocol>>16));
    //    NetworkStream.WriteByte((byte)((int)SslProtocol>>24));
    //    Stream Stream;
    //    if(this.X509Certificate is not null) {
    //        var SslStream = new SslStream(
    //            NetworkStream,
    //            true,
    //            DelegateRemoteCertificateValidationCallbackDelegate,
    //            null
    //        );
    //        var X509CertificateCollection = this.X509CertificateCollection;
    //        X509CertificateCollection.Clear();
    //        X509CertificateCollection.Add(this.X509Certificate);
    //        Debug.Assert(this.DnsEndPoint is not null);
    //        SslStream.AuthenticateAsClient(this.DnsEndPoint.Host,X509CertificateCollection,this.SslProtocol,true);
    //        //                SslStream.AuthenticateAsClient(this.Endpoint.ToString(),null,SslProtocols.Tls12,true);
    //        Stream=SslStream;
    //    } else {
    //        Stream=NetworkStream;
    //    }

    //    using var BinaryWriter = new BinaryWriter(Stream,Encoding.Unicode,true);
    //    BinaryWriter.Write(this.User);
    //    BinaryWriter.Flush();
    //    //Stream.Write(Buffer,0,Length+ハッシュバイト数);
    //    Stream.Write(this.PasswordHash);
    //    var WriteBuffer_Length = WriteBuffer.Length;
    //    Stream.WriteByte((byte)(WriteBuffer_Length>>0));
    //    Stream.WriteByte((byte)(WriteBuffer_Length>>8));
    //    Stream.WriteByte((byte)(WriteBuffer_Length>>16));
    //    Stream.WriteByte((byte)(WriteBuffer_Length>>24));
    //    Stream.Write(WriteBuffer);
    //    Stream.Write(Hash);
    //    Trace_WriteLine(1,"Client.送信 Stream.Write");
    //    Stream.Flush();
    //    Trace_WriteLine(2,"Client.送信 Stream.Flush");
    //    var ReadBuffer_Length = 0;
    //    ReadByte(ref ReadBuffer_Length,Stream,0);
    //    ReadByte(ref ReadBuffer_Length,Stream,8);
    //    ReadByte(ref ReadBuffer_Length,Stream,16);
    //    ReadByte(ref ReadBuffer_Length,Stream,24);
    //    var Response=(Response)Stream.ReadByte();
    //    var ReadBuffer=this.ReadBuffer=new byte[ReadBuffer_Length+HashLength];
    //    //var ReadOffset = 0;
    //    //var 残りバイト数 = ReadBuffer_Length;
    //    //do {
    //    //    var ReadしたBytes = Stream.Read(Buffer,ReadOffset,残りバイト数);
    //    //    ReadOffset+=ReadしたBytes;
    //    //    残りバイト数-=ReadしたBytes;
    //    //} while(残りバイト数>0);
    //    Read(Stream,ReadBuffer,ReadBuffer.Length);
    //    //do {
    //    //    var ReadしたBytes = Stream.Read(ReadBuffer,ReadOffset,残りバイト数);
    //    //    ReadOffset+=ReadしたBytes;
    //    //    残りバイト数-=ReadしたBytes;
    //    //} while(残りバイト数>0);
    //    ConnectSocket.Shutdown(SocketShutdown.Both);
    //    ConnectSocket.Close();
    //    var Provider=this.Provider;
    //    Provider.Initialize();
    //    Provider.ComputeHash(ReadBuffer,0,ReadBuffer_Length);
    //    var Provider_Hash = Provider.Hash!;
    //    for(var a = 0;a<HashLength;a++)
    //        if(Provider_Hash[a]!=ReadBuffer[a+ReadBuffer_Length])
    //            throw new InvalidDataException(Resources.ハッシュ値が一致しなかった);
    //    return Response;
    //}
    private Response WriteBufferを送信(byte[] WriteBuffer) {
        //Trace.WriteLine("Client");
        //Trace.WriteLine("Server.Function受信");
        //Trace.WriteLine("Server.Function受信終了");
        //Trace.WriteLine("Server.FunctionRequestResponse");
        //Trace.WriteLine("Server.Function送信");
        //Trace.WriteLine("Server.Function送信終了");
        //Trace.WriteLine("Client");
        //var MemoryStream = this.MemoryStream;
        //var Buffer = this.Buffer;
        //var WriteBuffer = this.WriteBuffer;
        //var Provider = this.Provider;
        ////var Length = WriteBuffer.Length;
        ////var Length除外全体バイト数 = Length-4;
        ////Debug.Assert(送信データとハッシュのバイト数<=ServerMemoryStreamBufferSize);
        ////送信データのバイト数
        //Provider.Initialize();
        //Provider.ComputeHash(WriteBuffer);
        //var Provider_Hash0 = Provider.Hash!;
        //for(var a = 0;a<ハッシュバイト数;a++)
        //    WriteBuffer[a+Length]=Provider_Hash0[a];
        // 4:バイト数(未確定)
        //64:SHA256(未確定)
        // 1:公開鍵暗号化アルゴリズム
        //  :ユーザ名
        //  :パスワード
        // 1:リクエストヘッダ
        //  :本体
        using var ConnectSocket = new Socket(SocketAddressFamily,SocketType.Stream,ProtocolType.Tcp) {
            //LingerState=Common.LingerState,
            ReceiveBufferSize=ClientReceiveBufferSize,
            SendBufferSize = ClientSendBufferSize,
            ReceiveTimeout=this.ReadTimeout,
            SendTimeout=this.WriteTimeout
        };
        ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.KeepAlive,1);
        //ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReadTimeout,this.ReadTimeout);
        //ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendTimeout,this.ReadTimeout);
        ConnectSocket.Connect(this.DnsEndPoint);
        Trace_WriteLine(0,"Client.送信 ConnectSocket.Connect");
        using Stream NetworkStream = new NetworkStream(ConnectSocket,false) {
            ReadTimeout= this.ReadTimeout,
            WriteTimeout= this.ReadTimeout
        };
        var SslProtocol = this.SslProtocol;
        NetworkStream.WriteByte((byte)((int)SslProtocol>>0));
        NetworkStream.WriteByte((byte)((int)SslProtocol>>8));
        NetworkStream.WriteByte((byte)((int)SslProtocol>>16));
        NetworkStream.WriteByte((byte)((int)SslProtocol>>24));
        Stream Stream;
        if(this.X509Certificate is not null) {
            var X509CertificateCollection = this.X509CertificateCollection;
            X509CertificateCollection.Clear();
            X509CertificateCollection.Add(this.X509Certificate);
            Debug.Assert(this.DnsEndPoint is not null);
            //var SslStream = new SslStream(
            //    NetworkStream,
            //    true
            //);
            ////SslStream.AuthenticateAsClient(this.DnsEndPoint.Host,X509CertificateCollection,this.SslProtocol,false);
            //SslStream.AuthenticateAsClient(this.DnsEndPoint.Host);
            var SslStream = new SslStream(
                NetworkStream,
                true,
                (sender,Certificate,Chain,SslPolicyErrors)=>{
                    //if(Certificate is not null) {
                    //    Console.WriteLine(@"===========================================");
                    //    Console.WriteLine(@"Subject={0}",Certificate.Subject);
                    //    Console.WriteLine(@"Subject={0}",Certificate.Issuer);
                    //    Console.WriteLine(@"Subject={0}",Certificate.GetFormat());
                    //    Console.WriteLine(@"Subject={0}",Certificate.GetExpirationDateString());
                    //    Console.WriteLine(@"Subject={0}",Certificate.GetEffectiveDateString());
                    //    Console.WriteLine(@"Subject={0}",Certificate.GetKeyAlgorithm());
                    //    Console.WriteLine(@"Subject={0}",Certificate.GetPublicKeyString());
                    //    Console.WriteLine(@"Subject={0}",Certificate.GetSerialNumberString());
                    //    Console.WriteLine(@"===========================================");
                    //}
                    //return true;
                    if(SslPolicyErrors == SslPolicyErrors.None) {
                        Console.WriteLine(Resources.クライアントでサーバー証明書の検証に成功した);
                        return true;
                    }
                    //何かサーバー証明書検証エラーが発生している

                    //SslPolicyErrors列挙体には、Flags属性があるので、
                    //エラーの原因が複数含まれているかもしれない。
                    //そのため、&演算子で１つ１つエラーの原因を検出する。
                    if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateChainErrors)!=0){
                        foreach(var ChainElement in Chain!.ChainElements){
                            foreach(var a in ChainElement.ChainElementStatus){
                                Trace.WriteLine(a.StatusInformation);
                                return true;
                            }
                        }
                    }

                    if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateNameMismatch)!=0){
                        Trace_WriteLine(3,Properties.Resources.サーバーで証明書名が不一致だった);
                    }

                    if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateNotAvailable)!=0){
                        Trace_WriteLine(4,Properties.Resources.サーバーで証明書が利用できなかった);
                    }
                    //検証失敗とする
                    return false;
                },
                null
            );
            SslStream.AuthenticateAsClient(this.DnsEndPoint.Host,X509CertificateCollection,this.SslProtocol,true);
            //                SslStream.AuthenticateAsClient(this.Endpoint.ToString(),null,SslProtocols.Tls12,true);
            Stream=SslStream;
        } else {
            Stream=NetworkStream;
        }

        using var BinaryWriter = new BinaryWriter(Stream,Encoding.Unicode,true);
        BinaryWriter.Write(this.User);
        BinaryWriter.Flush();
        //Stream.Write(Buffer,0,Length+ハッシュバイト数);
        Stream.Write(this.PasswordHash);
        var WriteBuffer_Length = WriteBuffer.Length;
        Stream.WriteByte((byte)(WriteBuffer_Length>> 0));
        Stream.WriteByte((byte)(WriteBuffer_Length>> 8));
        Stream.WriteByte((byte)(WriteBuffer_Length>> 16));
        Stream.WriteByte((byte)(WriteBuffer_Length>> 24));
        Stream.Write(WriteBuffer);
        var Provider=this.Provider;
        Provider.Initialize();
        Provider.ComputeHash(WriteBuffer);
        Stream.Write(Provider.Hash);
        //Trace_WriteLine(1,"Client.送信 Stream.Write");
        //Stream.Flush();
        //Trace_WriteLine(2,"Client.送信 Stream.Flush");

        Stream.Flush();
        var ReadBuffer_Length = 0;
        ReadByte(ref ReadBuffer_Length,Stream,0);
        ReadByte(ref ReadBuffer_Length,Stream,8);
        ReadByte(ref ReadBuffer_Length,Stream,16);
        ReadByte(ref ReadBuffer_Length,Stream,24);
        var Response=(Response)Stream.ReadByte();
        var ReadBuffer=this.ReadBuffer=new byte[ReadBuffer_Length+HashLength];
        //var ReadOffset = 0;
        //var 残りバイト数 = ReadBuffer_Length;
        //do {
        //    var ReadしたBytes = Stream.Read(Buffer,ReadOffset,残りバイト数);
        //    ReadOffset+=ReadしたBytes;
        //    残りバイト数-=ReadしたBytes;
        //} while(残りバイト数>0);
        Read(Stream,ReadBuffer,ReadBuffer.Length);
        //do {
        //    var ReadしたBytes = Stream.Read(ReadBuffer,ReadOffset,残りバイト数);
        //    ReadOffset+=ReadしたBytes;
        //    残りバイト数-=ReadしたBytes;
        //} while(残りバイト数>0);
        ConnectSocket.Shutdown(SocketShutdown.Both);
        ConnectSocket.Close();
        Provider.Initialize();
        Provider.ComputeHash(ReadBuffer,0,ReadBuffer_Length);
        var Provider_Hash = Provider.Hash!;
        for(var a = 0;a<HashLength;a++)
            if(Provider_Hash[a]!=ReadBuffer[a+ReadBuffer_Length])
                throw new InvalidDataException(Resources.ハッシュ値が一致しなかった);
        return Response;
    }
    //private Response ReadBufferを受信(Stream Stream,Socket ConnectSocket) {
    //    Stream.Flush();
    //    var ReadBuffer_Length = 0;
    //    ReadByte(ref ReadBuffer_Length,Stream,0);
    //    ReadByte(ref ReadBuffer_Length,Stream,8);
    //    ReadByte(ref ReadBuffer_Length,Stream,16);
    //    ReadByte(ref ReadBuffer_Length,Stream,24);
    //    var Response=(Response)Stream.ReadByte();
    //    var ReadBuffer=this.ReadBuffer=new byte[ReadBuffer_Length+HashLength];
    //    //var ReadOffset = 0;
    //    //var 残りバイト数 = ReadBuffer_Length;
    //    //do {
    //    //    var ReadしたBytes = Stream.Read(Buffer,ReadOffset,残りバイト数);
    //    //    ReadOffset+=ReadしたBytes;
    //    //    残りバイト数-=ReadしたBytes;
    //    //} while(残りバイト数>0);
    //    Read(Stream,ReadBuffer,ReadBuffer.Length);
    //    //do {
    //    //    var ReadしたBytes = Stream.Read(ReadBuffer,ReadOffset,残りバイト数);
    //    //    ReadOffset+=ReadしたBytes;
    //    //    残りバイト数-=ReadしたBytes;
    //    //} while(残りバイト数>0);
    //    ConnectSocket.Shutdown(SocketShutdown.Both);
    //    ConnectSocket.Close();
    //    var Provider=this.Provider;
    //    Provider.Initialize();
    //    Provider.ComputeHash(ReadBuffer,0,ReadBuffer_Length);
    //    var Provider_Hash = Provider.Hash!;
    //    for(var a = 0;a<HashLength;a++)
    //        if(Provider_Hash[a]!=ReadBuffer[a+ReadBuffer_Length])
    //            throw new InvalidDataException(Resources.ハッシュ値が一致しなかった);
    //    return Response;
    //}
    private static void ReadByte(ref int バイト数,Stream Stream,int シフト数) {
        var Byte = Stream.ReadByte();
        if(Byte<0)throw new InvalidDataException(Resources.データ長が読み込めなかった);
        バイト数+=Byte<<シフト数;
    }
    private readonly SHA256 Provider = SHA256.Create();
    /// <summary>
    /// 空を送信し空を受信する。
    /// </summary>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void EmptySendReceive() {
        var Hash=this.Hash1;
        this.PasswordHashを設定(Hash);
        Hash[^1]=(byte)Request.Bytes0_Bytes0;
        //this.WriteBuffer=Array.Empty<byte>();
        var actual = this.WriteBufferを送信(Array.Empty<byte>());
        //var actual = this.ReadBufferを受信(Stream,ConnectSocket);
        if(Response.Bytes0==actual)return;
        if(actual==Response.ThrowException){
            var Message=this.ReadObject<string>(SerializeType.Utf8Json);
            throw new InvalidDataException(Resources.リモート先で例外が発生した,new Exception(Message));
        }
        throw リモート先から_を受信することを期待したが_だった(Response.Bytes0,actual);
    }
    /// <summary>
    /// this.NetworkStreamからObjectをデシリアライズする
    /// </summary>
    /// <returns></returns>
    internal T ReadObject<T>(SerializeType SerializeType){
        var ReadBuffer=this.ReadBuffer;
        Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
        var value=SerializeType switch{
            SerializeType.MemoryPack =>this.MemoryPack.Deserialize<object>(ReadBuffer),
            SerializeType.MessagePack=>this.MessagePack.Deserialize<object>(ReadBuffer),
            _                        =>this.Utf8Json.Deserialize<object>(ReadBuffer)
        };
        return(T)value;
    }
    ///// <summary>
    ///// サーバーでTimeoutExceptionを発生させる
    ///// </summary>
    ///// <param name="WriteXmlの表現形式"></param>
    ///// <exception cref="InvalidDataException"></exception>
    ///// <exception cref="Exception"></exception>
    ///// <returns>TimeoutException</returns>
    //public TimeoutException SendTimeoutException(SerializeType WriteXmlの表現形式){
    //    this.BufferにUserとPasswordHashを設定(Request.TimeoutException_ThrowException,this.Header65);
    //    var MemoryStream=this.MemoryStream;
    //    MemoryStream.WriteByte((byte)WriteXmlの表現形式);
    //    this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.ThrowException);
    //    throw new InvalidDataException(Resources.リモート先で例外が発生した,new TimeoutException(this.ReadObject<string>(MemoryStream)));
    //}
    /// <summary>
    /// 指定されたバイト数送信中にタイムアウトする。
    /// </summary>
    /// <param name="要素数"></param>
    /// <param name="WriteTimeout"></param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void BytesSendTimeoutReceive(int 要素数,int WriteTimeout) {
        this.WriteTimeout=WriteTimeout;
        this.ReadTimeout=-1;
        this.BytesSendReceive(要素数);
    }
    /// <summary>
    /// 指定されたバイト数受信中にタイムアウトする。
    /// </summary>
    /// <param name="要素数"></param>
    /// <param name="ReadTimeout"></param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void BytesSendReceiveTimeout(int 要素数,int ReadTimeout){
        this.WriteTimeout=-1;
        this.ReadTimeout=ReadTimeout;
        this.BytesSendReceive(要素数);
    }
    public void HttpRequest(string Request){
        using var ConnectSocket=new Socket(SocketAddressFamily,SocketType.Stream,ProtocolType.Tcp) {
            //LingerState=Common.LingerState,
            ReceiveBufferSize=ClientReceiveBufferSize,
            SendBufferSize = ClientSendBufferSize,
            ReceiveTimeout=this.ReadTimeout,
            SendTimeout=this.WriteTimeout
        };
        ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.KeepAlive,1);
        //ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReadTimeout,this.ReadTimeout);
        //ConnectSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendTimeout,this.ReadTimeout);
        ConnectSocket.Connect(this.DnsEndPoint);
        Trace_WriteLine(0,"Client.送信 ConnectSocket.Connect");
        using Stream NetworkStream = new NetworkStream(ConnectSocket,false) {
            ReadTimeout= this.ReadTimeout,
            WriteTimeout= this.ReadTimeout
        };
        var sslProtocol = this.SslProtocol;
        Stream Stream;
        if(this.X509Certificate is not null) {
            var x509CertificateCollection = this.X509CertificateCollection;
            x509CertificateCollection.Clear();
            x509CertificateCollection.Add(this.X509Certificate);
            Debug.Assert(this.DnsEndPoint is not null);
            //var SslStream = new SslStream(
            //    NetworkStream,
            //    true
            //);
            ////SslStream.AuthenticateAsClient(this.DnsEndPoint.Host,X509CertificateCollection,this.SslProtocol,false);
            //SslStream.AuthenticateAsClient(this.DnsEndPoint.Host);
            var SslStream = new SslStream(
                NetworkStream,
                true,
                (sender,Certificate,Chain,SslPolicyErrors)=>{
                    if(SslPolicyErrors == SslPolicyErrors.None) {
                        Console.WriteLine(Resources.クライアントでサーバー証明書の検証に成功した);
                        return true;
                    }
                    //何かサーバー証明書検証エラーが発生している

                    //SslPolicyErrors列挙体には、Flags属性があるので、
                    //エラーの原因が複数含まれているかもしれない。
                    //そのため、&演算子で１つ１つエラーの原因を検出する。
                    if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateChainErrors)!=0){
                        foreach(var ChainElement in Chain!.ChainElements){
                            foreach(var a1 in ChainElement.ChainElementStatus){
                                Trace.WriteLine(a1.StatusInformation);
                            }
                        }
                    }

                    if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateNameMismatch)!=0){
                        Trace_WriteLine(3,Properties.Resources.サーバーで証明書名が不一致だった);
                    }

                    if((SslPolicyErrors&SslPolicyErrors.RemoteCertificateNotAvailable)!=0){
                        Trace_WriteLine(4,Properties.Resources.サーバーで証明書が利用できなかった);
                    }
                    //検証失敗とする
                    return false;
                },
                null
            );
            SslStream.AuthenticateAsClient(this.DnsEndPoint.Host,x509CertificateCollection,this.SslProtocol,true);
            //                SslStream.AuthenticateAsClient(this.Endpoint.ToString(),null,SslProtocols.Tls12,true);
            Stream=SslStream;
        } else {
            Stream=NetworkStream;
        }
        using var BinaryWriter = new BinaryWriter(Stream,Encoding.UTF8,true);
        BinaryWriter.Write(Request);
        BinaryWriter.Flush();
        Stream.Flush();
        var ReadBuffer_Length = 10000;
        var ReadBuffer=new byte[ReadBuffer_Length];
        var ReadOffset=0;
        try{
            do{
                var ReadしたBytes=Stream.Read(ReadBuffer,ReadOffset,ReadBuffer_Length-ReadOffset);
                if(ReadしたBytes==0) break;
                ReadOffset+=ReadしたBytes;
            } while(true);
        } catch(IOException){

        }
        var s=Encoding.UTF8.GetString(ReadBuffer,0,ReadOffset);
        ConnectSocket.Shutdown(SocketShutdown.Both);
        ConnectSocket.Close();
    }
    /// <summary>
    /// 指定されたバイト数送受信する。
    /// </summary>
    /// <param name="要素数"></param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void BytesSendReceive(int 要素数){
        var Hash=this.Hash1;
        this.PasswordHashを設定(Hash);
        Hash[^1]=(byte)Request.BytesN_BytesN;
        var WriteBuffer=new byte[要素数];
        for(var a=0;a<要素数;a++)WriteBuffer[a]=(byte)a;
        var actual = this.WriteBufferを送信(WriteBuffer);
        //var actual = this.ReadBufferを受信(Stream,ConnectSocket);
        if(Response.ThrowException==actual){
            var Message=this.ReadObject<string>(SerializeType.Utf8Json);
            throw new InvalidDataException(Resources.リモート先で例外が発生した,new Exception(Message));
        }
        if(Response.BytesN!=actual){
            throw リモート先から_を受信することを期待したが_だった(Response.BytesN,actual);
        }
        var ReadBuffer=this.ReadBuffer;
        for(var a=0;a<要素数;a++){
            var Byte=ReadBuffer[a];
            if(Byte==(byte)a)continue;
            throw リモート先から_を受信することを期待したが_だった(a,Byte);
        }
        var ReadObject1=ReadBuffer[要素数];
    }
    ///// <summary>
    ///// 指定されたバイト数1バイトずつ送受信する。
    ///// </summary>
    ///// <param name="要素数"></param>
    ///// <exception cref="InvalidDataException"></exception>
    ///// <exception cref="Exception"></exception>
    //public void Bytes1SendReceive(int 要素数) {
    //    this.BufferにUserとPasswordHashを設定(Request.BytesN_BytesN);
    //    var MemoryStream = this.MemoryStream;
    //    for(var a = 0;a<要素数;a++)MemoryStream.WriteByte((byte)a);
    //    this.Bufferをサーバーに送信してBufferに受信();
    //    var actual = (Response)this.MemoryStream.ReadByte();
    //    if(Response.BytesN==actual)return;
    //    throw リモート先から_を受信することを期待したが_だった(Response.BytesN,actual);
    //}
    ///// <summary>
    ///// 指定された配列を送受信する。
    ///// </summary>
    ///// <param name="配列"></param>
    ///// <exception cref="InvalidDataException">送信したデータと同じデータが受信できなかった場合</exception>
    //public void BytesSendReceive(byte[] 配列) {
    //    this.BufferにUserとPasswordHashを設定(Request.BytesN_BytesN);
    //    var MemoryStream = this.MemoryStream;
    //    foreach(var a in 配列)MemoryStream.WriteByte(a);
    //    this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.BytesN);
    //    foreach(var a in 配列){
    //        var Byte= MemoryStream.ReadByte();
    //        if(Byte==a)continue;
    //        throw リモート先から_を受信することを期待したが_だった(a,Byte);
    //    }
    //    var ReadObject= MemoryStream.ReadByte();
    //    Debug.Assert(ReadObject<0);
    //}
    internal Response サーバーに送信(Request Request,SerializeType SerializeType,object Object){
        var Hash_Request_SerializeType=this.Hash2;
        this.PasswordHashを設定(Hash_Request_SerializeType);
        Hash_Request_SerializeType[^2]=(byte)Request;
        Hash_Request_SerializeType[^1]=(byte)SerializeType;
        Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
        //switch(SerializeType) {
        //    case SerializeType.MemoryPack :this.MemoryPack.Serialize(this.MemoryStream,Object);break;
        //    case SerializeType.MessagePack:this.MessagePack.Serialize(this.MemoryStream,Object);break;
        //    default                       :this.Utf8Json.Serialize(this.MemoryStream,Object);break;
        //}
        var WriteBuffer=SerializeType switch{
            SerializeType.MemoryPack=>this.MemoryPack.Serialize(Object),
            SerializeType.MessagePack=>this.MessagePack.Serialize(Object),
            _=>this.Utf8Json.Serialize(Object)
        };
        var Response=this.WriteBufferを送信(WriteBuffer);
        return Response;
    }
    private readonly 取得_CSharp 取得_CSharp = new();
    //internal Response サーバーに送信(Request Request,SerializeType SerializeType,Expression Expression)=>

    //    var Header66=this.Header66;
    //    this.BufferにUserとPasswordHashを設定(Request,Header66);
    //    Header66[65]=(byte)SerializeType;
    //    Debug.Assert(SerializeType is SerializeType.MemoryPack or SerializeType.MessagePack or SerializeType.Utf8Json);
    //    this.WriteBuffer=SerializeType switch{
    //        SerializeType.MemoryPack=>this.MemoryPack.Serialize(Object),
    //        SerializeType.MessagePack=>this.MessagePack.Serialize(Object),
    //        _=>this.Utf8Json.Serialize(Object)
    //    };
    //    return this.Bufferをサーバーに送信してBufferに受信();
    //    this.WriteBuffer=SerializeType switch{
    //        SerializeType.MemoryPack: this.MemoryPack.Serialize(Expression); break;
    //        SerializeType.MessagePack: this.MessagePack.Serialize(Expression); break;
    //        _                     : this.Utf8Json.Serialize(Expression); break;
    //        default: throw new NotSupportedException(SerializeType.ToString());
    //    }
    //    return this.Bufferをサーバーに送信してBufferに受信();
    //}
    private protected static InvalidDataException 受信ヘッダー_は不正だった(Response Response) =>new(
        string.Format(
            CultureInfo.CurrentCulture,
            Resources.受信ヘッダー_は不正だった,
            Response
        )
    );
    private static InvalidDataException リモート先から_を受信することを期待したが_だった(object arg0,object arg1) => new(
        string.Format(
            CultureInfo.CurrentCulture,
            Resources.リモート先から_を受信することを期待したが_だった,
            arg0,
            arg1
        )
    );
    ///// <summary>
    ///// オブジェクトをNetContractSerializerで送信し、受信する。
    ///// </summary>
    ///// <param name="送信">送信したオブジェクト。</param>
    ///// <param name="SerializeType"></param>
    ///// <typeparam name="T"></typeparam>
    ///// <returns>受信したオブジェクト。</returns>
    ///// <exception cref="InvalidDataException"></exception>
    ///// <exception cref="Exception"></exception>
    ///// <exception cref="XmlException"></exception>
    //public T XmlSendReceive<T>(T 送信,SerializeType SerializeType) {
    //    this.サーバーに送信(Request.Object_Object,SerializeType,送信!);
    //    var MemoryStream = this.MemoryStream;
    //    var Response =(Response)MemoryStream.ReadByte();
    //    // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
    //    return Response switch{
    //        Response.ThrowException => throw new Exception(this.ReadObject<string>(MemoryStream)),
    //        Response.Object => this.ReadObject<T>(MemoryStream),
    //        _ => throw 受信ヘッダー_は不正だった(Response)
    //    };
    //}
    /// <summary>
    /// シリアライズを送信して同じデータを受信する。
    /// </summary>
    /// <param name="送信">これをシリアライズする。</param>
    /// <param name="SerializeType">送信プロトコルを指定</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>送信した値が返ってくる。</returns>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public T SerializeSendReceive<T>(T 送信,SerializeType SerializeType) {
        var Response = this.サーバーに送信(Request.Object_Object,SerializeType,送信!);
        if(Response!=Response.Object)throw 受信ヘッダー_は不正だった(Response);
        return this.ReadObject<T>(SerializeType);
    }
    /// <summary>
    /// Optimizerオブジェクト
    /// </summary>
    [field:NonSerialized]
    protected Optimizer Optimizer { get; } = new();
    /// <summary>
    /// 戻り値のあるリモート処理を行う。
    /// </summary>
    /// <param name="Lambda">戻り値のあるリモート処理を行うデリゲート。</param>
    /// <param name="SerializeType"></param>
    public T Expression<T>(Expression<Func<T>> Lambda,SerializeType SerializeType){
        return (T)this.Expression((LambdaExpression)Lambda,SerializeType);
    }
    public object Expression(LambdaExpression Lambda,SerializeType SerializeType) {
        var DeclaringType = new StackFrame(1).GetMethod()!.DeclaringType!;
        var Optimizer = this.Optimizer;
        Optimizer.Context=DeclaringType;
        var 最適化Lambda=Optimizer.Lambda最適化(Lambda);
        var Response = this.サーバーに送信(Request.Expression_Invoke,SerializeType,最適化Lambda);
        return Response switch{
            Response.Object=>this.ReadObject<object>(SerializeType),
            Response.ThrowException=>throw this.ReadObject<Exception>(SerializeType),
            _=>throw 受信ヘッダー_は不正だった(Response)
        };
    }
}

//898