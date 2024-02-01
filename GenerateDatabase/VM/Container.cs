using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using LinqDB.Databases.Dom;
using System.Diagnostics;

namespace VM;
[DebuggerDisplay("{Name}")]
public class Container:IContainer {
    public string Name { get; set; }="";
    public List<Schema> Schemas { get; } = new();
    IEnumerable<ISchema> IContainer.Schemas => this.Schemas;
    //public List<ParentChild> ParentsChildren { get; } = new();
    //IEnumerable<IParentChild> IContainer.ParentsChildren => this.ParentsChildren;
    //public Schema this[String Schema] => this.Schemas.First(p => p.Name==Schema);

    //public void Load(Marshal.Database Marshal_Database) {
    //    this.Name=Marshal_Database.Name;
    //    this.Clear();
    //    foreach(var Marshal_Schema in Marshal_Database.Schemas) {
    //        var Schema=this.CreateSchema(Marshal_Schema.Name);
    //        foreach(var Marshal_Table in Marshal_Schema.Tables) {
    //            var Table=Schema.CreateTable(Marshal_Table.Name);
    //            var MetaAttribute=Marshal_Table.MetaAttribute;
    //            if(MetaAttribute!=null) {
    //                Table.Left=MetaAttribute.Left;
    //                Table.Top=MetaAttribute.Top;
    //            }
    //            foreach(var Marshal_Column in Marshal_Table.Columns) {
    //                Table.CreateColumn(Marshal_Column.Name,Marshal_Column.Type,Marshal_Column.IsNullable,Marshal_Column.IsPrimaryKey);
    //            }
    //        }
    //    }
    //    var Schemas=this.Schemas;
    //    foreach(var Marshal_Relation in Marshal_Database.Relations) {
    //        var Marshal_子Table = Marshal_Relation.子Table;
    //        var Marshal_親Table = Marshal_Relation.親Table;
    //        var Marshal_Relation_Name=Marshal_Relation.Name;
    //        var 子Table = Schemas.Single(p => p.Name==Marshal_子Table.Schema.Name).Tables.Single(p => p.Name==Marshal_子Table.Name);
    //        //var 子Table = this[Marshal_子Table.Schema.Name][Marshal_子Table.Name];
    //        var 親Schema_Name = Marshal_親Table.Schema.Name;
    //        var 親Table_Name = Marshal_親Table.Name;
    //        var Relation =this.CreateRelation(
    //            Marshal_Relation_Name,
    //            Schemas.Single(p => p.Name==親Schema_Name).Tables.Single(p => p.Name==親Table_Name),
    //            子Table
    //        );
    //        foreach(var Marshal_Column in Marshal_Relation.Columns) {
    //            Relation.AddColumn(子Table.Columns.Single(p=>p.Name==Marshal_Column.Name));
    //        }
    //    }
    //}
    public Schema CreateSchema(string Name) {
        var Schema = new Schema(Name,this);
        this.Schemas.Add(Schema);
        return Schema;
    }
    public Relation CreateRelation(string Name,Table 親Table,Table 子Table) {
        var Relation = new Relation(Name,親Table,子Table);
        this.Add(Relation);
        子Table.親Relations.Add(Relation);
        親Table.子Relations.Add(Relation);
        return Relation;
    }
    //public Table CreateTable(String Name) {
    //    var Table = new Table {
    //        Name=Name
    //    };
    //    this.Add(Table);
    //    return Table;
    //}
    public override string ToString() => this.Name;
    public Schema this[string Schema] => this.Schemas.First(p => p.Name==Schema);
    public List<Table> Tables { get; } = new();
    //public List<View> Views { get; } = new List<View>();
    //public List<ForeignKey> ForeignKeys { get; } = new List<ForeignKey>();
    public List<Relation> Relations { get; } = new();
    IEnumerable<IRelation> IContainer.Relations => this.Relations;
    public void Add(Relation Value) {
        this.Relations.Add(Value);
    }
    public void Remove(Relation Value) {
        this.Relations.Remove(Value);
    }
    public void Add(Table Value) {
        this.Tables.Add(Value);
    }
    public void Remove(Table Value) {
        this.Tables.Remove(Value);
    }
    public void Clear(){
        this.Schemas.Clear();
        this.Relations.Clear();
        this.Tables.Clear();
    }
    //public IEnumerable<DiagramObject> DiagramObjects {
    //    get {
    //        var Result = new List<DiagramObject>();
    //        foreach(var Table in this.Tables) {
    //            Result.Add(Table);
    //        }
    //        foreach(var Relation in this.Relations) {
    //            Result.Add(Relation);
    //        }
    //        return Result;
    //    }
    //}
}

