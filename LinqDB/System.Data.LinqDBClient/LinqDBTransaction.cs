using System.Data.Common;
// ReSharper disable once CheckNamespace
namespace System.Data.LinqDBClient;

/// <summary>
/// 利用予定なし。
/// </summary>
public class LinqDbTransaction:DbTransaction {
    private readonly LinqDBConnetion LinqDBConnetion;
    internal LinqDbTransaction(LinqDBConnetion LinqDBConnetion) {
        this.LinqDBConnetion=LinqDBConnetion;
    }
    public override IsolationLevel IsolationLevel { get; }

    protected override DbConnection DbConnection => this.LinqDBConnetion;

    public override void Commit() => throw new NotImplementedException();
    public override void Rollback() => throw new NotImplementedException();
}