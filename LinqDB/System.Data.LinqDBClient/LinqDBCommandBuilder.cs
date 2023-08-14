using System.Data.Common;
// ReSharper disable once CheckNamespace
namespace System.Data.LinqDBClient;

/// <summary>
/// 利用予定なし。
/// </summary>
public class LinqDBCommandBuilder:DbCommandBuilder {
    protected override void ApplyParameterInfo(DbParameter parameter,DataRow row,StatementType statementType,bool whereClause) => throw new NotImplementedException();
    protected override string GetParameterName(int parameterOrdinal) => throw new NotImplementedException();
    protected override string GetParameterName(string parameterName) => throw new NotImplementedException();
    protected override string GetParameterPlaceholder(int parameterOrdinal) => throw new NotImplementedException();
    protected override void SetRowUpdatingHandler(DbDataAdapter adapter) => throw new NotImplementedException();
}