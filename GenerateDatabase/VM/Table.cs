using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using LinqDB.Helpers;
using System.Linq;
//using static LinqDB.Helpers.CommonLibrary;
using LinqDB.Databases.Dom;
namespace VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class Table:ITable{
    public List<Column> Columns { get; } = new();
    IEnumerable<IColumn> IColumns.Columns =>this.Columns;
    IEnumerable<string> SchemaNames => ((ITableUI)this).Schema.Schemas.Select(p => p.Name!);
    public IColumn this[string Column] => this.Columns.First(p => p.Name==Column);
    public readonly List<Trigger> BeforeTriggers = new();
    public readonly List<Trigger> AfterTriggers = new();
    public Table(string Name,Schema Schema) {
        this.Name = Name;
        this.Schema = Schema;
    }
    public List<Relation> 親Relations { get; } = new();
    public List<Relation> 子Relations { get; } = new();
    public ISchema Schema{get;set;}
    public string Name { get; set; }="";
    public Column CreateColumn(string Name,Type Type,bool IsNullable,bool IsPrimaryKey) {
        var Column = new Column(Name,IsNullable&&Type.IsNullable()? Type.GetGenericArguments()[0]:Type,IsNullable,IsPrimaryKey);
        Debug.Assert(this.Columns.All(p=>p.Name!=Name));
        this.Columns.Add(Column);
        return Column;
    }
}
