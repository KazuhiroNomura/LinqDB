using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Properties;
//using MemoryPack=LinqDB.Serializers.MemoryPack;
//using MessagePack=LinqDB.Serializers.MessagePack;
//using Utf8Json=LinqDB.Serializers.Utf8Json;
//using LinqDB.Serializers.Utf8Json.Formatters;
using static LinqDB.Helpers.CommonLibrary;
using static LinqDB.Helpers.Configulation;
//using System.Linq.Expressions;
//using Expression = System.Linq.Expressions.Expression;
//using MemoryStream = System.IO.MemoryStream;
using MemoryStream = LinqDB.Helpers.CommonLibrary.MemoryStream;

namespace LinqDB.Remote.Clients;

/// <summary>
/// リモートクラス。
/// </summary>
public class Client:IDisposable {
    private readonly Serializers.Utf8Json.Serializer Utf8Json=new();
    private readonly Serializers.MessagePack.Serializer MessagePack=new();
    private readonly Serializers.MemoryPack.Serializer MemoryPack=new();
    [NonSerialized]
    private readonly byte[]Buffer=new byte[ClientMemoryStreamBufferSize];
    /// <summary>
    /// バッファストリーム。
    /// </summary>
    [NonSerialized]
    private protected readonly MemoryStream MemoryStream;
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
        :this(WriteTimeout,ReadTimeout,new DnsEndPoint(host,port)) {
    }
    /// <summary>
    /// 既定コンストラクタ。
    /// </summary>
    public Client():this(既定のタイムアウト,既定のタイムアウト,null!) {}
    //private readonly Utf8JsonCustomSerializer Utf8JsonCustomSerializer;
    //private readonly MessagePackCustomSerializer MessagePackCustomSerializer;
    //private readonly CustomSerializerMemoryPack CustomSerializerMemoryPack;
    //private readonly Serializers.Utf8Json.Resolver Utf8Json_Resolver=new Serializers.Utf8Json.Resolver();
    //private readonly IJsonFormatterResolver JsonFormatterResolver;
    //private readonly Serializers.MessagePack.Resolver MessagePack_Resolver=new Serializers.MessagePack.Resolver();
    //private readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="WriteTimeout">書き込みタイムアウト</param>
    /// <param name="ReadTimeout">読み込みタイムアウト</param>
    /// <param name="DnsEndPoint">接続先</param>
    private Client(int WriteTimeout,int ReadTimeout,DnsEndPoint DnsEndPoint) {
        this.MemoryStream=new MemoryStream(this.Buffer);
        //this.MemoryStream=new MemoryStream();
        this.WriteTimeout=WriteTimeout;
        this.ReadTimeout=ReadTimeout;
        this.DnsEndPoint=DnsEndPoint;
        //this.Utf8JsonCustomSerializer=new();
        //this.JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
        //    //順序が大事
        //    this.Utf8Json_Resolver,
        //    Utf8Json.Resolvers.StandardResolver.AllowPrivate,
        //    Utf8Json.Resolvers.StandardResolver.Default
        //);
        //this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
        //    MessagePack.Resolvers.CompositeResolver.Create(
        //        //順序が大事
        //        this.MessagePack_Resolver,
        //        MessagePack.Resolvers.StandardResolver.Instance,
        //        MessagePack.Resolvers.DynamicContractlessObjectResolverAllowPrivate.Instance
        //    )
        //);
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
                this.MemoryStream.Dispose();
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
    private static readonly RemoteCertificateValidationCallback DelegateRemoteCertificateValidationCallbackDelegate = RemoteCertificateValidationCallback;
    private static bool RemoteCertificateValidationCallback(object sender,X509Certificate? Certificate,X509Chain? chain,SslPolicyErrors SslPolicyErrors) {
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
            Console.WriteLine(Resources.クライアントでサーバー証明書の検証に成功しなかった);
            return true;
        } else {
            //何かサーバー証明書検証エラーが発生している

            //SslPolicyErrors列挙体には、Flags属性があるので、
            //エラーの原因が複数含まれているかもしれない。
            //そのため、&演算子で１つ１つエラーの原因を検出する。
            if((SslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) ==
               SslPolicyErrors.RemoteCertificateChainErrors) {
                Console.WriteLine(Resources.クライアントでChainStatusが空でない配列を返した);
            }

            if((SslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) ==
               SslPolicyErrors.RemoteCertificateNameMismatch) {
                Console.WriteLine(Resources.クライアントで証明書名が不一致だった);
            }

            if((SslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) ==
               SslPolicyErrors.RemoteCertificateNotAvailable) {
                Console.WriteLine(Resources.クライアントで証明書が利用できなかった);
            }
            //検証失敗とする
            return false;
        }
    }
    private readonly X509CertificateCollection X509CertificateCollection = new();
    /// <summary>
    /// NetworkStreamはusing用、SslStreamはNetworkStreamかSslStreamを取得する。
    /// </summary>
    /// <returns></returns>
    private void BufferにUserとPasswordHashを設定(Request Request) {
        var MemoryStream = this.MemoryStream;
        MemoryStream.SetLength(4);
        MemoryStream.Position=4;
        using var BinaryWriter = new BinaryWriter(MemoryStream,Encoding.Unicode,true);
        BinaryWriter.Write(this.User);
        BinaryWriter.Write(GetPasswordHash(this.Password));
        BinaryWriter.Flush();
        MemoryStream.WriteByte((byte)Request);
    }
    private void Bufferをサーバーに送信してBufferに受信() {
        //Trace.WriteLine("Client");
        //Trace.WriteLine("Server.Function受信");
        //Trace.WriteLine("Server.Function受信終了");
        //Trace.WriteLine("Server.FunctionRequestResponse");
        //Trace.WriteLine("Server.Function送信");
        //Trace.WriteLine("Server.Function送信終了");
        //Trace.WriteLine("Client");
        var MemoryStream = this.MemoryStream;
        var Buffer = this.Buffer;
        var Provider = this.Provider;
        Provider.Initialize();
        var Length = (int)MemoryStream.Length;
        var Length除外全体バイト数 = Length-4;
        var 送信データとハッシュのバイト数 = Length除外全体バイト数+ハッシュバイト数;
        //Debug.Assert(送信データとハッシュのバイト数<=ServerMemoryStreamBufferSize);
        //送信データのバイト数
        Provider.ComputeHash(Buffer,4,Length除外全体バイト数);
        Buffer[0]=(byte)(送信データとハッシュのバイト数>>0);
        Buffer[1]=(byte)(送信データとハッシュのバイト数>>8);
        Buffer[2]=(byte)(送信データとハッシュのバイト数>>16);
        Buffer[3]=(byte)(送信データとハッシュのバイト数>>24);
        var Provider_Hash = Provider.Hash!;
        for(var a = 0;a<ハッシュバイト数;a++)
            Buffer[a+Length]=Provider_Hash[a];
        // 4:バイト数(未確定)
        //64:SHA256(未確定)
        // 1:公開鍵暗号化アルゴリズム
        //  :ユーザ名
        //  :パスワード
        // 1:リクエストヘッダ
        //  :本体
        using var ConnectSocket = new Socket(CommonLibrary.AddressFamily,SocketType.Stream,ProtocolType.Tcp) {
            //LingerState=Common.LingerState,
            ReceiveBufferSize=ClientReceiveBufferSize,
            SendBufferSize=ClientSendBufferSize,
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
            var SslStream = new SslStream(
                NetworkStream,
                true,
                DelegateRemoteCertificateValidationCallbackDelegate,
                null
            );
            var X509CertificateCollection = this.X509CertificateCollection;
            X509CertificateCollection.Clear();
            X509CertificateCollection.Add(this.X509Certificate);
            Debug.Assert(this.DnsEndPoint is not null);
            SslStream.AuthenticateAsClient(this.DnsEndPoint.Host,X509CertificateCollection,this.SslProtocol,true);
            //                SslStream.AuthenticateAsClient(this.Endpoint.ToString(),null,SslProtocols.Tls12,true);
            Stream=SslStream;
        } else {
            Stream=NetworkStream;
        }
        Stream.Write(Buffer,0,Length+ハッシュバイト数);
        Trace_WriteLine(1,"Client.送信 Stream.Write");
        Stream.Flush();
        Trace_WriteLine(2,"Client.送信 Stream.Flush");
        var 受信データとハッシュのバイト数 = 0;
        ReadByte(ref 受信データとハッシュのバイト数,Stream,0);
        ReadByte(ref 受信データとハッシュのバイト数,Stream,8);
        ReadByte(ref 受信データとハッシュのバイト数,Stream,16);
        ReadByte(ref 受信データとハッシュのバイト数,Stream,24);
        var 受信データバイト数 = 受信データとハッシュのバイト数-ハッシュバイト数;
        MemoryStream.SetLength(受信データバイト数);
        MemoryStream.Position=0;
        var ReadOffset = 0;
        var 残りバイト数 = 受信データとハッシュのバイト数;
        do {
            var ReadしたBytes = Stream.Read(Buffer,ReadOffset,残りバイト数);
            ReadOffset+=ReadしたBytes;
            残りバイト数-=ReadしたBytes;
        } while(残りバイト数>0);
        ConnectSocket.Shutdown(SocketShutdown.Both);
        ConnectSocket.Close();
        Provider.Initialize();
        Provider.ComputeHash(Buffer,0,受信データバイト数);
        var 受信Hash = Provider.Hash!;
        for(var a = 0;a<ハッシュバイト数;a++)
            if(受信Hash[a]!=Buffer[a+受信データバイト数])
                throw new InvalidDataException(Resources.ハッシュ値が一致しなかった);
    }
    private static void ReadByte(ref int バイト数,Stream Stream,int シフト数) {
        var Byte = Stream.ReadByte();
        if(Byte<0)throw new InvalidDataException(Resources.データ長が読み込めなかった);
        バイト数+=Byte<<シフト数;
    }
    private void Bufferをサーバーに送信してBufferに受信_例外処理(Response response) {
        this.Bufferをサーバーに送信してBufferに受信();
        var actual = (Response)this.MemoryStream.ReadByte();
        if(response==actual)return;
        if(actual==Response.ThrowException){
            var Message=this.ReadObject<string>(this.MemoryStream);
            throw new InvalidDataException(Resources.リモート先で例外が発生した,new Exception(Message));
        }
        throw リモート先から_を受信することを期待したが_だった(response,actual);
    }
    private readonly SHA256 Provider = SHA256.Create();
    /// <summary>
    /// 空を送信し空を受信する。
    /// </summary>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void EmptySendReceive() {
        this.BufferにUserとPasswordHashを設定(Request.Bytes0_Bytes0);
        this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.Bytes0);
    }
    /// <summary>
    /// this.NetworkStreamからObjectをデシリアライズする
    /// </summary>
    /// <param name="ReadStream"></param>
    /// <returns></returns>
    internal T ReadObject<T>(MemoryStream ReadStream) {
        var SerializeType = (SerializeType)ReadStream.ReadByte();
        return SerializeType switch{
            SerializeType.MemoryPack=>(T)this.MemoryPack.Deserialize<object>(ReadStream),
            SerializeType.MessagePack=>(T)this.MessagePack.Deserialize<object>(ReadStream),
            SerializeType.Utf8Json=>(T)this.Utf8Json.Deserialize<object>(ReadStream),
            _=>throw new NotSupportedException(SerializeType.ToString())
        };
        //object o;
        //T Result;
        //switch(SerializeType) {
        //    case SerializeType.Utf8Json: 
        //        o=JsonResolver.Serializer().Deserialize<object>(ReadStream,this.SerializerConfiguration.JsonFormatterResolver); 
        //        Result=(T)o;
        //        break;
        //    case SerializeType.MessagePack: 
        //        o=MessagePackResolver.Serializer().Deserialize<object>(ReadStream); 
        //        Result=(T)o;
        //        break;
        //    default: {
        //        throw new NotSupportedException(SerializeType.ToString());
        //    }
        //}
        //return Result;
    }
    /// <summary>
    /// 空を送信し例外をthrowする。
    /// </summary>
    public void BackendOutOfMemoryException() {
        this.BufferにUserとPasswordHashを設定(Request.リモート先でOutOfMemoryException);
        this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.ThrowException);
        throw new OutOfMemoryException(this.ReadObject<string>(this.MemoryStream));
    }
    /// <summary>
    /// Byteを送信し同じByteを受信する。
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void ByteEofSendReceive(byte value) {
        this.BufferにUserとPasswordHashを設定(Request.Byte_Byte);
        var MemoryStream = this.MemoryStream;
        MemoryStream.WriteByte(value);
        this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.Byte);
        var ReadByte0= MemoryStream.ReadByte();
        if(ReadByte0!=value)throw リモート先から_を受信することを期待したが_だった(value,ReadByte0);
        var ReadByte1= MemoryStream.ReadByte();
        Debug.Assert(ReadByte1<0);
    }

    /// <summary>
    /// サーバーでTimeoutExceptionを発生させる
    /// </summary>
    /// <param name="WriteXmlの表現形式"></param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    /// <returns>TimeoutException</returns>
    public TimeoutException SendTimeoutException(SerializeType WriteXmlの表現形式){
        this.BufferにUserとPasswordHashを設定(Request.TimeoutException_ThrowException);
        var MemoryStream=this.MemoryStream;
        MemoryStream.WriteByte((byte)WriteXmlの表現形式);
        this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.ThrowException);
        throw new InvalidDataException(Resources.リモート先で例外が発生した,new TimeoutException(this.ReadObject<string>(MemoryStream)));
    }
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

    /// <summary>
    /// 指定されたバイト数送受信する。
    /// </summary>
    /// <param name="要素数"></param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void BytesSendReceive(int 要素数) {
        this.BufferにUserとPasswordHashを設定(Request.BytesN_BytesN);
        var MemoryStream=this.MemoryStream;
        for(var a=0;a<要素数;a++)MemoryStream.WriteByte((byte)a);
        this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.BytesN);
        for(var a=0;a<要素数;a++){
            var Byte= MemoryStream.ReadByte();
            if(Byte==(byte)a)continue;
            throw リモート先から_を受信することを期待したが_だった(a,Byte);
        }
        var ReadObject1= MemoryStream.ReadByte();
        Debug.Assert(ReadObject1<0);
    }
    /// <summary>
    /// 指定されたバイト数1バイトずつ送受信する。
    /// </summary>
    /// <param name="要素数"></param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    public void Bytes1SendReceive(int 要素数) {
        this.BufferにUserとPasswordHashを設定(Request.BytesN_BytesN);
        var MemoryStream = this.MemoryStream;
        for(var a = 0;a<要素数;a++)MemoryStream.WriteByte((byte)a);
        this.Bufferをサーバーに送信してBufferに受信();
        var actual = (Response)this.MemoryStream.ReadByte();
        if(Response.BytesN==actual)return;
        throw リモート先から_を受信することを期待したが_だった(Response.BytesN,actual);
    }
    /// <summary>
    /// 指定された配列を送受信する。
    /// </summary>
    /// <param name="配列"></param>
    /// <exception cref="InvalidDataException">送信したデータと同じデータが受信できなかった場合</exception>
    public void BytesSendReceive(byte[] 配列) {
        this.BufferにUserとPasswordHashを設定(Request.BytesN_BytesN);
        var MemoryStream = this.MemoryStream;
        foreach(var a in 配列)MemoryStream.WriteByte(a);
        this.Bufferをサーバーに送信してBufferに受信_例外処理(Response.BytesN);
        foreach(var a in 配列){
            var Byte= MemoryStream.ReadByte();
            if(Byte==a)continue;
            throw リモート先から_を受信することを期待したが_だった(a,Byte);
        }
        var ReadObject= MemoryStream.ReadByte();
        Debug.Assert(ReadObject<0);
    }
    /// <summary>
    /// オブジェクトをXmlBinaryで送信し、受信する。
    /// </summary>
    /// <param name="送信">送信したオブジェクト。</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>受信したオブジェクト。</returns>
    public T XmlSendReceive<T>(T 送信)=>this.XmlSendReceive(送信,SerializeType.MessagePack);
    private void サーバーに送信(Request Request,SerializeType SerializeType,object Object) {
        this.BufferにUserとPasswordHashを設定(Request);
        this.MemoryStream.WriteByte((byte)SerializeType);
        switch(SerializeType) {
            case SerializeType.Utf8Json   :this.Utf8Json.Serialize(this.MemoryStream,Object);break;
            case SerializeType.MessagePack:this.MessagePack.Serialize(this.MemoryStream,Object);break;
            case SerializeType.MemoryPack :this.MemoryPack.Serialize(this.MemoryStream,Object);break;
            default:throw new NotSupportedException(SerializeType.ToString());
        }
        //switch(SerializeType) {
        //    case SerializeType.Utf8Json:
        //        //this.SerializerConfiguration.Clear();
        //        JsonSerializer.Serialize(this.MemoryStream,Object,this.SerializerConfiguration.JsonFormatterResolver);
        //        break;
        //    case SerializeType.MessagePack:
        //        this.SerializerConfiguration.Clear();
        //        MessagePackSerializer.Serialize(this.MemoryStream,Object,this.SerializerConfiguration.MessagePackSerializerOptions);
        //        break;
        //    default:throw new NotSupportedException(SerializeType.ToString());
        //}
        this.Bufferをサーバーに送信してBufferに受信();
    }
    private readonly Optimizer.取得_CSharp 取得_CSharp = new();
    internal void サーバーに送信(Request Request,SerializeType SerializeType,Expression Expression) {
        this.BufferにUserとPasswordHashを設定(Request);
        //var Lambda = (LambdaExpression)Expression;
        this.MemoryStream.WriteByte((byte)SerializeType);
        switch(SerializeType) {
            case SerializeType.MemoryPack:this.MemoryPack.Serialize(this.MemoryStream,Expression);break;
            case SerializeType.MessagePack:this.MessagePack.Serialize(this.MemoryStream,Expression);break;
            case SerializeType.Utf8Json:this.Utf8Json.Serialize(this.MemoryStream,Expression);break;
            default:throw new NotSupportedException(SerializeType.ToString());
        }
        this.Bufferをサーバーに送信してBufferに受信();
    }
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
    /// <summary>
    /// オブジェクトをNetContractSerializerで送信し、受信する。
    /// </summary>
    /// <param name="送信">送信したオブジェクト。</param>
    /// <param name="SerializeType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>受信したオブジェクト。</returns>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="Exception"></exception>
    /// <exception cref="XmlException"></exception>
    public T XmlSendReceive<T>(T 送信,SerializeType SerializeType) {
        this.サーバーに送信(Request.Object_Object,SerializeType,送信!);
        var MemoryStream = this.MemoryStream;
        var Response =(Response)MemoryStream.ReadByte();
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return Response switch{
            Response.ThrowException => throw new Exception(this.ReadObject<string>(MemoryStream)),
            Response.Object => this.ReadObject<T>(MemoryStream),
            _ => throw 受信ヘッダー_は不正だった(Response)
        };
    }
    /// <summary>
    /// Remoteの接続先を文字列で表現。
    /// </summary>
    /// <returns></returns>
    public override string ToString()=>this.DnsEndPoint is null?"": this.DnsEndPoint.ToString();
    /// <summary>
    /// Streamにテキストシリアライズする
    /// </summary>
    /// <param name="Stream"></param>
    /// <param name="リモート先で実行させるデリゲート"></param>
    /// <returns>戻り値</returns>
    public static void SaveRequest(Stream Stream,object リモート先で実行させるデリゲート) {
        using var Writer = XmlDictionaryWriter.CreateTextWriter(Stream,Encoding.Unicode,false);
        //ExpressionSurrogateSelector.serializer.WriteObject(Writer,リモート先で実行させるデリゲート);
        Writer.Flush();
    }
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
        this.サーバーに送信(Request.Object_Object,SerializeType,送信!);
        var MemoryStream = this.MemoryStream;
        var Response = (Response)MemoryStream.ReadByte();
        if(Response!=Response.Object)throw 受信ヘッダー_は不正だった(Response);
        return this.ReadObject<T>(MemoryStream);
    }
    ///// <summary>
    ///// 式木のデリゲート
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <returns></returns>
    //public delegate T サーバーで実行する式木<out T>();
    /// <summary>
    /// Optimizerオブジェクト
    /// </summary>
    [field:NonSerialized]
    protected Optimizer Optimizer { get; } = new();
    ///// <summary>
    ///// 最適化レベル
    ///// </summary>
    //public OptimizeLevels OptimizeLevel { get; set; } = OptimizeLevels.デバッグ;
    /// <summary>
    /// 戻り値のあるリモート処理を行う。
    /// </summary>
    /// <param name="Lambda">戻り値のあるリモート処理を行うデリゲート。</param>
    /// <param name="SerializeType"></param>
    public T Expression<T>(Expression<Func<T>> Lambda,SerializeType SerializeType){
        return (T)this.Expression((LambdaExpression)Lambda,SerializeType);
        //var DeclaringType = new StackFrame(1).GetMethod()!.DeclaringType!;
        //var Optimizer = this.Optimizer;
        //Optimizer.Context=DeclaringType;
        //var 最適化Lambda=Optimizer.Lambda最適化(Lambda);
        ////var s=JsonSerializer.Serialize(最適化Lambda,this.SerializerConfiguration.JsonFormatterResolver);
        ////var o=JsonResolver.Serializer().Deserialize<LambdaExpression>(s,this.SerializerConfiguration.JsonFormatterResolver);


        //this.サーバーに送信(Request.Expression_Invoke,SerializeType,最適化Lambda);
        //var MemoryStream = this.MemoryStream;
        //var Response = (Response)MemoryStream.ReadByte();
        //return Response switch{
        //    Response.Object=>this.ReadObject<T>(MemoryStream),
        //    Response.ThrowException=>throw new InvalidOperationException(this.ReadObject<string>(MemoryStream)),
        //    _=>throw 受信ヘッダー_は不正だった(Response)
        //};
    }
    public object Expression(LambdaExpression Lambda,SerializeType SerializeType) {
        var DeclaringType = new StackFrame(1).GetMethod()!.DeclaringType!;
        var Optimizer = this.Optimizer;
        Optimizer.Context=DeclaringType;
        var 最適化Lambda=Optimizer.Lambda最適化(Lambda);
        this.サーバーに送信(Request.Expression_Invoke,SerializeType,最適化Lambda);
        var MemoryStream = this.MemoryStream;
        var Response = (Response)MemoryStream.ReadByte();
        return Response switch{
            Response.Object=>this.ReadObject<object>(MemoryStream),
            Response.ThrowException=>throw new InvalidOperationException(this.ReadObject<string>(MemoryStream)),
            _=>throw 受信ヘッダー_は不正だった(Response)
        };
    }
}

//898