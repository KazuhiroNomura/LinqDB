#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
using System;
using System.Collections.Generic;
using System.Diagnostics;

using LinqDB.Databases.Dom;
namespace VM;

[DebuggerDisplay("{this."+nameof(SQL)+"}")]
public class Procedure(string Name,Type Type,Schema Schema,string SQL):IProcedure {
    public string Name { get; set; }=Name;
    public Type Type{ get; }=Type;
    public string SQL { get; }=SQL;
    public ISchema Schema {get;}=Schema;
    ISchema IProcedure.Schema => this.Schema;
    public List<Parameter> Parameters { get; } = new();
    IEnumerable<IParameter> IParameters.Parameters => this.Parameters;
}