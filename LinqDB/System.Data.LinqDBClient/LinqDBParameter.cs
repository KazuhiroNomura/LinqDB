using System.Data.Common;
#pragma warning disable CS8765 // パラメーターの型の NULL 値の許容が、オーバーライドされたメンバーと一致しません。おそらく、NULL 値の許容の属性が原因です。
// ReSharper disable once CheckNamespace
namespace System.Data.LinqDBClient;

/// <summary>
/// 利用予定なし。
/// </summary>
public class LinqDBParameter:DbParameter {
    public override DbType DbType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override ParameterDirection Direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override bool IsNullable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override string ParameterName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override int Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override string SourceColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override bool SourceColumnNullMapping { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override void ResetDbType() => throw new NotImplementedException();
}