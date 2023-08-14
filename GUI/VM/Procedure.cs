#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using LinqDB.Databases.Dom;
namespace GUI.VM;

[DebuggerDisplay("{this."+nameof(SQL)+"}")]
public class Procedure:IProcedure {
    public string Name { get; set; }
    public string SQL { get; set; }
    public ISchema Schema {get;}
    ISchema IProcedure.Schema => this.Schema;
    public ObservableCollection<Parameter> Parameters { get; } = new();
    IEnumerable<IParameter> IParameters.Parameters => this.Parameters;
    public Procedure(string Name,Schema Schema,string SQL) {
        this.Name = Name;
        this.Schema = Schema;
        this.SQL = SQL;
    }
}