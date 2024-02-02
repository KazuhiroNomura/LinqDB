using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using LinqDB.Databases.Dom;
using System.Diagnostics;

namespace GUI.VM;
[DebuggerDisplay("{Name}")]
public class Container:DependencyObject, IContainer {
    public string Name { get; set; }="";
    public ObservableCollection<Schema> Schemas { get; } = new();
    IEnumerable<ISchema> IContainer.Schemas => this.Schemas;
    public Schema CreateSchema(string Name) {
        var Schema = new Schema(Name,this);
        this.Schemas.Add(Schema);
        return Schema;
    }
    public Relation CreateRelation(string Name,TableUI 親TableUi,TableUI 子TableUI) {
        var Relation = new Relation(Name,親TableUi,子TableUI);
        this.Add(Relation);
        子TableUI.親Relations.Add(Relation);
        親TableUi.子Relations.Add(Relation);
        return Relation;
    }
    public override string ToString() => this.Name;
    public Schema this[string Schema] => this.Schemas.First(p => p.Name==Schema);
    public ObservableCollection<TableUI> Tables { get; } = new();
    //public ObservableCollection<View> Views { get; } = new ObservableCollection<View>();
    //public ObservableCollection<ForeignKey> ForeignKeys { get; } = new ObservableCollection<ForeignKey>();
    public ObservableCollection<Relation> Relations { get; } = new();
    IEnumerable<IRelation> IContainer.Relations => this.Relations;
    public ObservableCollection<IDiagramObject> DiagramObjects { get; } = new();
    public void Add(Relation Value) {
        this.Relations.Add(Value);
        this.DiagramObjects.Add(Value);
    }
    public void Remove(Relation Value) {
        this.Relations.Remove(Value);
        this.DiagramObjects.Remove(Value);
    }
    public void Add(TableUI Value) {
        this.Tables.Add(Value);
        this.DiagramObjects.Add(Value);
    }
    public void Remove(TableUI Value) {
        this.Tables.Remove(Value);
        this.DiagramObjects.Remove(Value);
    }
    public void Clear(){
        this.Schemas.Clear();
        this.Relations.Clear();
        this.Tables.Clear();
        this.DiagramObjects.Clear();
    }
    //public IEnumerable<DiagramObject> DiagramObjects {
    //    get {
    //        var Result = new ObservableCollection<DiagramObject>();
    //        foreach(var Table in this.Tables) {
    //            Result.Add(Table);
    //        }
    //        foreach(var Relation in this.Relations) {
    //            Result.Add(Relation);
    //        }
    //        return Result;
    //    }
    //}
    private static readonly DependencyProperty _SelectedIndex = DependencyProperty.Register(nameof(SelectedIndex),typeof(int),typeof(Container));
    //public event ComponentModel.PropertyChangedEventHandler? PropertyChanged;
    public int SelectedIndex {
        get => (int)this.GetValue(_SelectedIndex);
        set => this.SetValue(_SelectedIndex,value);
    }
}

