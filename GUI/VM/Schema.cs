using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Diagnostics;
using LinqDB.Databases.Dom;
using System.Collections.Generic;
namespace GUI.VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class Schema:DependencyObject,ISchema {
    private static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name),typeof(string),typeof(Schema));
    public string Name {
        get => (string)this.GetValue(NameProperty);
        set => this.SetValue(NameProperty,value);
    }
    private static readonly DependencyProperty SchemaNameTextBox_IsReadOnlyProperty = DependencyProperty.Register(nameof(SchemaNameTextBox_IsReadOnly),typeof(bool),typeof(Schema));
    public bool SchemaNameTextBox_IsReadOnly {
        get => (bool)this.GetValue(SchemaNameTextBox_IsReadOnlyProperty);
        set => this.SetValue(SchemaNameTextBox_IsReadOnlyProperty,value);
    }
    IContainer ISchema.Container => this.Container!;
    public Container? Container { get; set; }
    //internal 作業配列 作業配列 => this.Database!.作業配列;
    public Schema()=>this.SchemaNameTextBox_IsReadOnly=true;
    public Schema(string Name,Container Container) {
        this.SchemaNameTextBox_IsReadOnly=true;
        this.Name=Name;
        this.Container = Container;
    }
    //public SortedDictionary<string,Table> Dictionary_SynonymTables { get; } = new();
    //public ObservableCollection<(string Synonym,object Table_View_ScalarFunction_TableFunction_Procedure)> Synonyms { get; } = new();
    //ICollection<(string Synonym,object Table_View_ScalarFunction_TableFunction_Procedure)> ISchema.Synonyms => this.Synonyms;
    public ObservableCollection<(string Name,ITable Table)> SynonymTables { get; } = new();
    ICollection<(string Name,ITable Table)> ISchema.SynonymTables=> this.SynonymTables;
    public ObservableCollection<(string Name,IView View)> SynonymViews { get; } = new();
    ICollection<(string Name,IView View)> ISchema.SynonymViews => this.SynonymViews;
    public ObservableCollection<(string Synonym, IScalarFunction)> SynonymScalarFunctions { get; } = new();
    ICollection<(string Name,IScalarFunction ScalarFunction)> ISchema.SynonymScalarFunctions => this.SynonymScalarFunctions;
    public ObservableCollection<(string Synonym, ITableFunction)> SynonymTableFunctions { get; } = new();
    ICollection<(string Name,ITableFunction TableFunction)> ISchema.SynonymTableFunctions => this.SynonymTableFunctions;
    public ObservableCollection<(string Synonym, IProcedure)> SynonymProcedures { get; } = new();
    ICollection<(string Name,IProcedure Procedure)> ISchema.SynonymProcedures => this.SynonymProcedures;
    public ObservableCollection<TableUI> Tables { get; } = new();
    IEnumerable<ITable> ISchema.Tables => this.Tables;
    public TableUI this[string Table] => this.Tables.First(p => p.Name==Table);
    public ObservableCollection<View> Views { get; } = new();
    IEnumerable<IView> ISchema.Views => this.Views;
    public ObservableCollection<ScalarFunction> ScalarFunctions { get; } = new();
    IEnumerable<IScalarFunction> ISchema.ScalarFunctions => this.ScalarFunctions;
    public ObservableCollection<TableFunction> TableFunctions { get; } = new();
    IEnumerable<ITableFunction> ISchema.TableFunctions => this.TableFunctions;
    public ObservableCollection<Schema> Schemas => this.Container!.Schemas;
    IEnumerable<ISchema> ISchema.Schemas => this.Schemas;
    public ObservableCollection<Procedure> Procedures { get; } = new();
    IEnumerable<IProcedure> ISchema.Procedures => this.Procedures;
    //public FieldBuilder? Container_Schema_FieldBuilder { get; set; }
    //public LocalBuilder? Container_RelationValidate_LocalBuilder { get; set; }
    //public Type? Type{ get; set; }
    public TableUI CreateTable(string Name) {
        var Table = new TableUI(Name,this);
        this.Container!.Add(Table);
        this.Tables.Add(Table);
        return Table;
    }
    //public void CreateSynonymTable(string Synonym,Table Table)=>this.SynonymTables.Add((Synonym,Table));
    //public void CreateSynonymView(string Synonym,View View)=>this.SynonymViews.Add((Synonym,View));
    //public void CreateSynonymScalarFunction(string Synonym,ScalarFunction ScalarFunction)=>this.SynonymScalarFunctions.Add((Synonym,ScalarFunction));
    //public void CreateSynonymTableFunction(string Synonym,TableFunction TableFunction)=>this.SynonymTableFunctions.Add((Synonym,TableFunction));
    //public void CreateSynonymProcedure(string Synonym,Procedure Procedure)=>this.SynonymProcedures.Add((Synonym,Procedure));
    public TableUI CreateTable(string Name,double Left,double Top) {
        var Table = new TableUI(Name,this,Left,Top);
        this.Container!.Add(Table);
        this.Tables.Add(Table);
        return Table;
    }
    public void CreateView(string Name,string SQL) {
        var View = new View(Name,this,SQL);
        this.Views.Add(View);
    }
    public ScalarFunction CreateScalarFunction(string Name,string SQL) {
        var Function = new ScalarFunction(Name,this,SQL);
        this.ScalarFunctions.Add(Function);
        return Function;
    }
    public TableFunction CreateTableFunction(string Name,string SQL) {
        var TableFunction = new TableFunction(Name,this,SQL);
        this.TableFunctions.Add(TableFunction);
        return TableFunction;
    }
    public Procedure CreateProcedure(string Name,string SQL) {
        var Procedure = new Procedure(Name,this,SQL);
        this.Procedures.Add(Procedure);
        return Procedure;
    }
    private static readonly DependencyProperty _SelectedIndex = DependencyProperty.Register(nameof(SelectedIndex),typeof(int),typeof(Schema));
    public int SelectedIndex {
        get => (int)this.GetValue(_SelectedIndex);
        set => this.SetValue(_SelectedIndex,value);
    }
}
class Schemas:ObservableCollection<Schema> {
}
