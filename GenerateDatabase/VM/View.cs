using System;
using System.Collections.Generic;
using System.Diagnostics;
using LinqDB.Databases.Dom;
using LinqDB.Helpers;

namespace VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class View:IView {
    public List<Column> Columns { get; } = new();
    IEnumerable<IColumn> IColumns.Columns =>this.Columns;
    public ISchema Schema{get;set;}
    ISchema IView.Schema => this.Schema;
    public string SQL { get; set; } = "";
    public string Name { get; set; }="";
    public View(string Name,Schema Schema,string SQL) {
        this.Name=Name;
        this.Schema=Schema;
        this.SQL=SQL;
    }
    public Column CreateColumn(string Name,Type Type,bool IsNullable) {
        var Column = new Column(Name,IsNullable&&Type.IsNullable()? Type.GetGenericArguments()[0]:Type,IsNullable,false);
        this.Columns.Add(Column);
        return Column;
    }
}
