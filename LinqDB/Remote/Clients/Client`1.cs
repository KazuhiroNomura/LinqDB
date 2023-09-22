using System;
using System.Linq.Expressions;
namespace LinqDB.Remote.Clients;
/// <summary>
/// エンティティを指定したリモートクラス。
/// </summary>
/// <typeparam name="TContainer"></typeparam>
public sealed class Client<TContainer>:Client, IClient {
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="host">ホスト名</param>
    /// <param name="port">ポート番号</param>
    public Client(string host,int port):base(host,port){}
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="host">ホスト名</param>
    /// <param name="port">ポート番号</param>
    /// <param name="WriteTimeout">書き込みタイムアウト(ms)</param>
    /// <param name="ReadTimeout">読み込みタイムアウト(ms)</param>
    public Client(string host,int port,int WriteTimeout,int ReadTimeout):base(host,port,WriteTimeout, ReadTimeout){}
    /// <summary>
    /// 既定コンストラクタ。Connectで接続する。
    /// </summary>
    public Client(){}
    /// <summary>オブジェクトが、ガベージ コレクションによって収集される前に、リソースの解放とその他のクリーンアップ操作の実行を試みることができるようにします。</summary>
    ~Client() => this.Dispose(false);
    /// <summary>
    /// サーバーで実行する式木
    /// </summary>
    /// <param name="サーバーから提供されるEntities"></param>
    public delegate void サーバーで実行する式木(TContainer サーバーから提供されるEntities);
    /// <summary>
    /// 戻り値のないリモート処理を行う。
    /// </summary>
    /// <param name="リモート先で実行させるExpression"></param>
    /// <param name="SerializeType"></param>
    public void Expression(Expression<サーバーで実行する式木> リモート先で実行させるExpression, SerializeType SerializeType=SerializeType.Utf8Json) {
        this.サーバーに送信(Request.Expression_Invoke,SerializeType,this.Optimizer.Lambda最適化(リモート先で実行させるExpression));
        var MemoryStream = this.MemoryStream;
        var Response =(Response)MemoryStream.ReadByte();
        switch (Response) {
            case Response.Object:throw 受信ヘッダー_は不正だった(Response);
            case Response.ThrowException:throw new InvalidOperationException(this.ReadObject<string>(MemoryStream));
        }
    }
    /// <summary>
    /// 戻り値のあるサーバーで実行する式木
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="サーバーから提供されるEntities"></param>
    /// <returns></returns>
    public delegate TResult サーバーで実行するEntities式木<out TResult>(TContainer サーバーから提供されるEntities);
    /// <summary>
    /// TResultを戻り値にするリモート処理を行う。
    /// </summary>
    /// <param name="リモート先で実行させるLambda"></param>
    /// <param name="SerializeType"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>戻り値</returns>
    public TResult Expression<TResult>(Expression<サーバーで実行するEntities式木<TResult>> リモート先で実行させるLambda,SerializeType SerializeType=SerializeType.Utf8Json){
        var Lambda=this.Optimizer.Lambda最適化(リモート先で実行させるLambda);
        this.サーバーに送信(Request.Expression_Invoke,SerializeType,Lambda);
        var MemoryStream = this.MemoryStream;
        var Response = (Response)MemoryStream.ReadByte();
        return Response switch{
            Response.Object=>this.ReadObject<TResult>(MemoryStream),
            Response.ThrowException=>throw new InvalidOperationException(this.ReadObject<string>(MemoryStream)),
            _=>throw 受信ヘッダー_は不正だった(Response)
        };
    }
    /// <summary>
    /// SQLリモート処理する。
    /// </summary>
    /// <param name="SQL">SQL文</param>
    /// <param name="SerializeType"></param>
    public object Expression(string SQL,SerializeType SerializeType) {
        var Optimizer = this.Optimizer;
        Optimizer.Context=typeof(Client<TContainer>);
        var Container_Parameter=System.Linq.Expressions.Expression.Parameter(typeof(TContainer),"this");
        var リモート先で実行させるLambda_Body= Optimizer.SQLToExpression(Container_Parameter,SQL);
        var リモート先で実行させるLambda = System.Linq.Expressions.Expression.Lambda<サーバーで実行するEntities式木<object>>(リモート先で実行させるLambda_Body,Container_Parameter);
        this.サーバーに送信(Request.Expression_Invoke,SerializeType,Optimizer.Lambda最適化(リモート先で実行させるLambda));
        var MemoryStream = this.MemoryStream;
        var Response = (Response)MemoryStream.ReadByte();
        return Response switch{
            Response.Object=>this.ReadObject<object>(MemoryStream),
            Response.ThrowException=>throw new InvalidOperationException(this.ReadObject<string>(MemoryStream)),
            _=>throw 受信ヘッダー_は不正だった(Response)
        };
    }
}
// 93 2022/06/12
//118
