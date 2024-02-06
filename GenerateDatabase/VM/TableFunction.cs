using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinqDB.Databases.Dom;
using LinqDB.Helpers;

namespace VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class TableFunction(string Name,Schema Schema,string SQL):ITableFunction {
    public List<Parameter> Parameters { get; } = new();
    IEnumerable<IParameter> IParameters.Parameters => this.Parameters;
    public List<Column> Columns { get; } = new();
    IEnumerable<IColumn> IColumns.Columns =>this.Columns;
    public Type? Type{ get; set; }
    public bool IsNullable{ get; set; }
    public ISchema Schema{get;set;}=Schema;
    ISchema ITableFunction.Schema => this.Schema;
    public string Name { get; set; }=Name;
    public string SQL { get; set; }=SQL;
    public Column CreateColumn(string Name,Type Type,bool IsNullable) {
        var Column = new Column(Name,IsNullable&&Type.IsNullable()? Type.GetGenericArguments()[0]:Type,IsNullable,false);
        Debug.Assert(this.Columns.All(p=>p.Name!=Name));
        this.Columns.Add(Column);
        return Column;
    }
}
