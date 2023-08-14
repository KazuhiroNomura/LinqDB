using LinqDB.Databases.Dom;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace GUI.VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class ScalarFunction:DependencyObject, IScalarFunction {
    public ObservableCollection<Parameter> Parameters { get; } = new();
    IEnumerable<IParameter> IParameters.Parameters => this.Parameters;
    public Type? Type{ get; set; }
    public bool IsNullable{ get; set; }

    private static readonly DependencyProperty SchemaProperty = DependencyProperty.Register(
        nameof(Schema),
        typeof(Schema),
        typeof(ScalarFunction)/*,
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
    ISchema IScalarFunction.Schema => this.Schema;
    public string Name { get; set; }
    public string SQL { get; set; }
    public ScalarFunction(string Name,Schema Schema,string SQL) {
        this.Name=Name;
        this.Schema=Schema;
        this.SQL=SQL;
    }
}
