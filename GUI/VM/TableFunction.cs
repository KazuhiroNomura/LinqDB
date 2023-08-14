using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using LinqDB.Databases.Dom;
using LinqDB.Helpers;

namespace GUI.VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class TableFunction:DependencyObject, ITableFunction {
    public ObservableCollection<Parameter> Parameters { get; } = new();
    IEnumerable<IParameter> IParameters.Parameters => this.Parameters;
    public ObservableCollection<Column> Columns { get; } = new();
    IEnumerable<IColumn> IColumns.Columns =>this.Columns;
    public Type? Type{ get; set; }
    public bool IsNullable{ get; set; }

    private static readonly DependencyProperty ISchemaProperty = DependencyProperty.Register(
        nameof(Schema),
        typeof(Schema),
        typeof(TableFunction)/*,
    new FrameworkPropertyMetadata(
        (d,e) => {
            if(d is Schema Schema) {
                Schema.Views.Remove(this);
            }
        }
    )*/
    );
    public ISchema Schema {
        get => (Schema)this.GetValue(ISchemaProperty);
        set => this.SetValue(ISchemaProperty,value);
    }
    ISchema ITableFunction.Schema => this.Schema;
    public string Name { get; set; }
    public string SQL { get; set; }
    public Column CreateColumn(string Name,Type Type,bool IsNullable) {
        var Column = new Column(Name,IsNullable&&Type.IsNullable()? Type.GetGenericArguments()[0]:Type,IsNullable,false);
        Debug.Assert(this.Columns.All(p=>p.Name!=Name));
        this.Columns.Add(Column);
        return Column;
    }
    public TableFunction(string Name,Schema Schema,string SQL) {
        this.Name=Name;
        this.Schema=Schema;
        this.SQL=SQL;
    }
}
