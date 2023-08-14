using System.Data.Common;
using System.Collections;
using LinqDB.Remote.Clients;

#pragma warning disable CS8765 // パラメーターの型の NULL 値の許容が、オーバーライドされたメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
// ReSharper disable once CheckNamespace
namespace System.Data.LinqDBClient;

public class LinqDBCommand:DbCommand {
    private readonly IClient Client;
    internal LinqDBCommand(IClient Client)=>this.Client=Client;
    public override string CommandText { get; set; } = "";
    public override int CommandTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override CommandType CommandType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override bool DesignTimeVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override UpdateRowSource UpdatedRowSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    protected override DbConnection DbConnection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    protected override DbParameterCollection DbParameterCollection => throw new NotImplementedException();

    protected override DbTransaction DbTransaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Cancel() => throw new NotImplementedException();
    public override int ExecuteNonQuery() => throw new NotImplementedException();
    public override object ExecuteScalar() => throw new NotImplementedException();
    public override void Prepare() => throw new NotImplementedException();
    protected override DbParameter CreateDbParameter() => throw new NotImplementedException();
    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) {
        var Result = this.Client.Expression(this.CommandText,LinqDB.XmlType.Utf8Json);
        return new LinqDBDataReader(((IEnumerable)Result).GetEnumerator());
    }
}