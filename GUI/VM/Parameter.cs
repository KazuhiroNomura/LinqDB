using System;
using System.Diagnostics;
using System.Windows;
using LinqDB.Databases.Dom;
namespace GUI.VM;
[Serializable,DebuggerDisplay("{Type.Name+\" \"+Name}")]
public class Parameter:DependencyObject, IParameter {
    private static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name),typeof(string),typeof(Parameter));
    public string Name {
        get => (string)this.GetValue(NameProperty);
        set => this.SetValue(NameProperty,value);
    }

    public Type Type{ get; set; }
    public bool IsNullable=>true;
    public bool has_default_value{ get; }
    public object default_value { get; }
    public Parameter(string Name,Type Type,bool has_default_value,object default_value) {
        this.Name=Name;
        this.Type=Type;
        this.has_default_value=has_default_value;
        this.default_value=default_value;
    }
}
