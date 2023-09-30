using System.Data.Common;
using System.Net;
using LinqDB.Remote.Clients;

#pragma warning disable CS8765 // パラメーターの型の NULL 値の許容が、オーバーライドされたメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
// ReSharper disable once CheckNamespace
namespace System.Data.LinqDBClient;

public class LinqDBConnetion:DbConnection {
    private readonly IClient Client;
    public LinqDBConnetion(IClient Client) {
        this.Client=Client;
    }
    private string _ConnectionString="";
    public override string ConnectionString {
        get => this._ConnectionString;
        set {
            var ConnectionStringBuilder = new LinqDBConnectionStringBuilder(value);
            var DnsEndPoint = new DnsEndPoint(ConnectionStringBuilder.Host,ConnectionStringBuilder.Port);
            var Client = this.Client;
            Client.DnsEndPoint=DnsEndPoint;
            if(ConnectionStringBuilder.User is not null) {
                Client.User=ConnectionStringBuilder.User;
            }
        }
    }
    public override string Database => throw new NotImplementedException();
    public override string DataSource => this.Client.DnsEndPoint.Host;
    public override string ServerVersion => throw new NotImplementedException();
    public override ConnectionState State => throw new NotImplementedException();
    public override void ChangeDatabase(string databaseName) => throw new NotImplementedException();
    public override void Close() => throw new NotImplementedException();
    public override void Open() => throw new NotImplementedException();
    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => new LinqDbTransaction(this);
    protected override DbCommand CreateDbCommand()=>new LinqDBCommand(this.Client);
}