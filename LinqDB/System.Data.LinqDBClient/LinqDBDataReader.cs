using System.Data.Common;
using System.Collections;
// ReSharper disable once CheckNamespace
namespace System.Data.LinqDBClient;

/// <summary>
/// データを読み出す。
/// </summary>
public sealed class LinqDBDataReader:DbDataReader {
    private readonly IEnumerator Enumerator;
    internal LinqDBDataReader(IEnumerator Enumerator) {
        this.Enumerator=Enumerator;
    }
    public override object this[int ordinal] => this.GetValue(ordinal);
    public override object this[string name] => throw new NotImplementedException();
    public override int Depth => throw new NotImplementedException();
    public override int FieldCount => throw new NotImplementedException();
    public override bool HasRows => throw new NotImplementedException();
    public override bool IsClosed => throw new NotImplementedException();
    public override int RecordsAffected => throw new NotImplementedException();
    public override bool GetBoolean(int ordinal) => (bool)this.GetValue(ordinal);
    public override byte GetByte(int ordinal) => (byte)this.GetValue(ordinal);
    public override long GetBytes(int ordinal,long dataOffset,byte[]?buffer,int bufferOffset,int length) => throw new NotImplementedException();
    public override char GetChar(int ordinal) => (char)this.GetValue(ordinal);
    public override long GetChars(int ordinal,long dataOffset,char[]?buffer,int bufferOffset,int length) => throw new NotImplementedException();
    public override string GetDataTypeName(int ordinal) => throw new NotImplementedException();
    public override DateTime GetDateTime(int ordinal) => (DateTime)this.GetValue(ordinal);
    public override decimal GetDecimal(int ordinal) => (decimal)this.GetValue(ordinal);
    public override double GetDouble(int ordinal) => (double)this.GetValue(ordinal);
    public override IEnumerator GetEnumerator() => throw new NotImplementedException();
    public override Type GetFieldType(int ordinal) => throw new NotImplementedException();
    public override float GetFloat(int ordinal) => (float)this.GetValue(ordinal);
    public override Guid GetGuid(int ordinal) => (Guid)this.GetValue(ordinal);
    public override short GetInt16(int ordinal) => (short)this.GetValue(ordinal);
    public override int GetInt32(int ordinal) => (int)this.GetValue(ordinal);
    public override long GetInt64(int ordinal) => (long)this.GetValue(ordinal);
    public override string GetName(int ordinal) => throw new NotImplementedException();
    public override int GetOrdinal(string name) => throw new NotImplementedException();
    public override string GetString(int ordinal) => (string)this.GetValue(ordinal);
    public override object GetValue(int ordinal) => throw new NotImplementedException(); //((ITuple)this.Enumerator.Current!)[ordinal]!;
    public override int GetValues(object[] values) => throw new NotImplementedException();
    public override bool IsDBNull(int ordinal) => throw new NotImplementedException();
    public override bool NextResult() => throw new NotImplementedException();
    public override bool Read() => this.Enumerator.MoveNext();
}