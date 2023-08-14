using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using LinqDB.Databases.Dom;
using LinqDB.Helpers;

namespace GUI.VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class View:DependencyObject, IView {
    private static readonly DependencyProperty _ViewNameTextBox_IsReadOnly = DependencyProperty.Register(nameof(ViewNameTextBox_IsReadOnly),typeof(bool),typeof(View));
    public bool ViewNameTextBox_IsReadOnly {
        get => (bool)this.GetValue(_ViewNameTextBox_IsReadOnly);
        set => this.SetValue(_ViewNameTextBox_IsReadOnly,value);
    } 
    public ObservableCollection<Column> Columns { get; } = new();
    IEnumerable<IColumn> IColumns.Columns =>this.Columns;
    private static readonly DependencyProperty SchemaProperty = DependencyProperty.Register(
        nameof(Schema),
        typeof(Schema),
        typeof(View)/*,
    new FrameworkPropertyMetadata(
        (d,e) => {
            if(d is Schema Schema) {
                Schema.Views.Remove(this);
            }
        }
    )*/
    );
    public ISchema Schema {
        get => (Schema)this.GetValue(SchemaProperty);
        set => this.SetValue(SchemaProperty,value);
    }
    ISchema IView.Schema => this.Schema;
    //public IEnumerable<ISchema> Schemas => this.Schema.Schemas;
    //public IEnumerable<String> SchemaNames => this.Schema.Schemas.Select(p => p.Name!);
    public Visibility DataGridVisibility => Visibility.Visible;
    public Visibility LineVisibility => Visibility.Hidden;
    public string SQL { get; set; } = "";
    public string Name { get; set; }
    //public TypeBuilder? View_TypeBuilder { get; set; }
    //public Type? Type { get; private set; }
    //public MethodBuilder? Impl_MethodBuilder { get; set; }
    public View() {
        this.ViewNameTextBox_IsReadOnly=true;
        //this.VMColumns=new ObservableCollection<VMColumn>();
    }
    public View(string Name,Schema Schema,string SQL) {
        this.ViewNameTextBox_IsReadOnly=true;
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
