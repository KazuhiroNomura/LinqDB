using System;
using System.Diagnostics;
using System.Windows;
using LinqDB.Databases.Dom;
namespace GUI.VM;
[Serializable,DebuggerDisplay("{Type.Name+\" \"+Name}")]
public class Column:DependencyObject, IColumn {
    private static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name),typeof(string),typeof(Column));
    public string Name {
        get => (string)this.GetValue(NameProperty);
        set => this.SetValue(NameProperty,value);
    }
    private static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type),typeof(Type),typeof(Column));
    public Type Type {
        get => (Type)this.GetValue(TypeProperty);
        set => this.SetValue(TypeProperty,value);
    }
    private static readonly DependencyProperty IsNullableProperty = DependencyProperty.Register(nameof(IsNullable),typeof(bool),typeof(Column));
    public bool IsNullable {
        get => (bool)this.GetValue(IsNullableProperty);
        set => this.SetValue(IsNullableProperty,value);
    }
    private static readonly DependencyProperty IsPrimaryKeyProperty = DependencyProperty.Register(nameof(IsPrimaryKey),typeof(bool),typeof(Column));
    public bool IsPrimaryKey {
        get => (bool)this.GetValue(IsPrimaryKeyProperty);
        set => this.SetValue(IsPrimaryKeyProperty,value);
    }
    public Column() { }
    public Column(string Name,Type Type,bool IsNullable,bool IsPrimaryKey) {
        this.Name=Name;
        this.Type=Type;
        this.IsNullable=IsNullable;
        this.IsPrimaryKey=IsPrimaryKey;
    }
}
