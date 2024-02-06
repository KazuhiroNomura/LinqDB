using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using LinqDB.Databases.Dom;
namespace VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class Relation:IRelation {
    public string Name{get;set;}
    public string ToolTip {
        get {
            var sb = new StringBuilder($"{this.Name}:{this.親Table!.Name}<-{this.子Table!.Name}(");
            foreach(var Column in this.Columns) {
                sb.Append($"{Column.Name},");
            }
            sb[^1]=')';
            return sb.ToString();
        }
    }
    public Table? 親Table { get;internal set; }
    /// <summary>
    /// テーブルからは親が分かればいいのでこれが重要
    /// </summary>
    ITable? IRelation.親ITable => this.親Table;
    //internal Point 親Point;
    public Table? 子Table { get; internal set; }
    /// <summary>
    /// ダイアグラムでの子
    /// </summary>
    ITable? IRelation.子ITable => this.子Table;
    public List<Column> Columns { get; } = new();
    IEnumerable<IColumn> IRelation.Columns => this.Columns;
    public IRelation.Information I { get; } = new();
    public Relation() {
    }
    public Relation(string Name,Table 親Table,Table 子Table):this() {
        this.Name=Name;
        this.親Table=親Table;
        this.子Table=子Table;
    }
    public void AddColumn(Column Column) {
        this.Columns.Add(Column);
    }
}
