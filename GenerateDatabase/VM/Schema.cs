using System.Linq;
using System.Diagnostics;
using LinqDB.Databases.Dom;
using System.Collections.Generic;
using LinqDB.Serializers.MemoryPack.Formatters.Reflection;
namespace VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class Schema:ISchema {
    public string Name{get;set;}
    IContainer ISchema.Container => this.Container!;
    public Container? Container { get; set; }
    //internal 作業配列 作業配列 => this.Database!.作業配列;
    //public Schema()=>this.SchemaNameTextBox_IsReadOnly=true;
    public Schema(string Name,Container Container) {
        this.Name=Name;
        this.Container = Container;
    }
    //public SortedDictionary<string,Table> Dictionary_SynonymTables { get; } = new();
    //public List<(string Synonym,object Table_View_ScalarFunction_TableFunction_Procedure)> Synonyms { get; } = new();
    //ICollection<(string Synonym,object Table_View_ScalarFunction_TableFunction_Procedure)> ISchema.Synonyms => this.Synonyms;
    public List<(string Name,ITable Table)> SynonymTables { get; } = new();
    ICollection<(string Name,ITable Table)> ISchema.SynonymTables=> this.SynonymTables;
    public List<(string Name,IView View)> SynonymViews { get; } = new();
    ICollection<(string Name,IView View)> ISchema.SynonymViews => this.SynonymViews;
    public List<(string Synonym, IScalarFunction)> SynonymScalarFunctions { get; } = new();
    ICollection<(string Name,IScalarFunction ScalarFunction)> ISchema.SynonymScalarFunctions => this.SynonymScalarFunctions;
    public List<(string Synonym, ITableFunction)> SynonymTableFunctions { get; } = new();
    ICollection<(string Name,ITableFunction TableFunction)> ISchema.SynonymTableFunctions => this.SynonymTableFunctions;
    public List<(string Synonym, IProcedure)> SynonymProcedures { get; } = new();
    ICollection<(string Name,IProcedure Procedure)> ISchema.SynonymProcedures => this.SynonymProcedures;
    public List<ScalarType> ScalarTypes { get; } = new();
    public List<TableType> TableTypes { get; } = new();
    public List<Table> Tables { get; } = new();
    IEnumerable<ITable> ISchema.Tables => this.Tables;
    public Table this[string Table] => this.Tables.First(p => p.Name==Table);
    public List<View> Views { get; } = new();
    IEnumerable<IView> ISchema.Views => this.Views;
    public List<ScalarFunction> ScalarFunctions { get; } = new();
    IEnumerable<IScalarFunction> ISchema.ScalarFunctions => this.ScalarFunctions;
    public List<TableFunction> TableFunctions { get; } = new();
    IEnumerable<ITableFunction> ISchema.TableFunctions => this.TableFunctions;
    public List<Schema> Schemas => this.Container!.Schemas;
    IEnumerable<ISchema> ISchema.Schemas => this.Schemas;
    public List<Procedure> Procedures { get; } = new();
    IEnumerable<IProcedure> ISchema.Procedures => this.Procedures;
    //public FieldBuilder? Container_Schema_FieldBuilder { get; set; }
    //public LocalBuilder? Container_RelationValidate_LocalBuilder { get; set; }
    //public Type? Type{ get; set; }
    public void CreateScalarType(string 新Name,string 旧Name) {
        var Table = new ScalarType(this,新Name,旧Name);
        this.Container!.Add(Table);
        this.ScalarTypes.Add(Table);
    }
    public void CreateTableType(string Name) {
        var Table = new TableType(this,Name);
        this.Container!.Add(Table);
        this.TableTypes.Add(Table);
    }
    public void CreateTable(string Name) {
        var Table = new Table(Name,this);
        this.Container!.Add(Table);
        this.Tables.Add(Table);
    }
    //public void CreateSynonymTable(string Synonym,Table Table)=>this.SynonymTables.Add((Synonym,Table));
    //public void CreateSynonymView(string Synonym,View View)=>this.SynonymViews.Add((Synonym,View));
    //public void CreateSynonymScalarFunction(string Synonym,ScalarFunction ScalarFunction)=>this.SynonymScalarFunctions.Add((Synonym,ScalarFunction));
    //public void CreateSynonymTableFunction(string Synonym,TableFunction TableFunction)=>this.SynonymTableFunctions.Add((Synonym,TableFunction));
    //public void CreateSynonymProcedure(string Synonym,Procedure Procedure)=>this.SynonymProcedures.Add((Synonym,Procedure));
    public void CreateView(string Name,string SQL) {
        var View = new View(Name,this,SQL);
        this.Views.Add(View);
    }
    public void CreateScalarFunction(string Name,string SQL) {
        var Function = new ScalarFunction(Name,this,SQL);
        this.ScalarFunctions.Add(Function);
    }
    public void CreateTableFunction(string Name,string SQL) {
        var TableFunction = new TableFunction(Name,this,SQL);
        this.TableFunctions.Add(TableFunction);
    }
    public Procedure CreateProcedure(string Name,string SQL) {
        var Procedure = new Procedure(Name,this,SQL);
        this.Procedures.Add(Procedure);
        return Procedure;
    }
}
class Schemas:List<Schema> {
}
