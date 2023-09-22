using System.Net;

namespace LinqDB.Remote.Clients;

public interface IClient {
    /// <summary>
    /// SQLリモート処理する。
    /// </summary>
    /// <param name="SQL">SQL文</param>
    /// <param name="SerializeType"></param>
    object Expression(string SQL,SerializeType SerializeType);
    /// <summary>
    /// 接続先
    /// </summary>
    DnsEndPoint DnsEndPoint { get; set; }
    /// <summary>
    /// ユーザー名
    /// </summary>
    string User {
        get;
        set;
    }
}
