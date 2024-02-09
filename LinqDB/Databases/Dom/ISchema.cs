using System;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinqDB.Databases.Dom;
public interface ISchema:IName {
    IContainer Container{ get; }
    //ICollection<(string Synonym,object Table_View_ScalarFunction_TableFunction_Procedure)> Synonyms { get; }
    ICollection<(string Name,ITable Table)> SynonymTables { get; }
    ICollection<(string Name,IView View)> SynonymViews { get; }
    ICollection<(string Name,IScalarFunction ScalarFunction)> SynonymScalarFunctions { get; }
    ICollection<(string Name,ITableFunction TableFunction)> SynonymTableFunctions { get; }
    ICollection<(string Name,IProcedure Procedure)> SynonymProcedures{ get; }
    IEnumerable<ITable> Tables { get; }
    IEnumerable<IView> Views { get; }
    IEnumerable<IScalarFunction> ScalarFunctions { get; }
    IEnumerable<ITableFunction> TableFunctions { get; }
    IEnumerable<IProcedure> Procedures { get; }
    IEnumerable<ISequence> Sequences{ get; }
    /// <summary>
    /// 兄弟Schemaのコレクション
    /// </summary>
    IEnumerable<ISchema> Schemas { get; }
    public class Information(TypeBuilder TypeBuilder,FieldBuilder Containerに定義されるSchema_FieldBuilder,LocalBuilder ContainerのRelationValidateで使うLocalBuilder){
        internal readonly FieldBuilder Containerに定義されるSchema_FieldBuilder=Containerに定義されるSchema_FieldBuilder;
        internal readonly LocalBuilder ContainerのRelationValidateで使うLocalBuilder=ContainerのRelationValidateで使うLocalBuilder;
        private TypeBuilder TypeBuilder { get;set;}=TypeBuilder;
        //internal TypeBuilder Lazy_TypeBuilder { get;private set;}
        //internal Type? Type { get; private set; }
        internal FieldInfo? Containerに定義されるSchema_Field;
        public void CreateType(Type Container_Type) {
            this.TypeBuilder.CreateType();
            this.TypeBuilder=default!;
            this.Containerに定義されるSchema_Field=Container_Type.GetField(this.Containerに定義されるSchema_FieldBuilder.Name,BindingFlags.Instance|BindingFlags.NonPublic);
        }
    }
    void CreateSynonym(string SynonymName,string base_object_name){
        var split=base_object_name.Split("].[");
        var BaseSchema=split[1];
        var BaseName=split[2];
        BaseName=BaseName[..^1];
        var Schema=this.Schemas.Single(p=>p.Name==BaseSchema);
        var Table = Schema.Tables.SingleOrDefault(p => p.Name==BaseName);
        if(Table is not null) {
            this.SynonymTables.Add((SynonymName,Table));
            return;
        }
        var View = Schema.Views.SingleOrDefault(p => p.Name==BaseName);
        if(View is not null) {
            this.SynonymViews.Add((SynonymName,View));
            return;
        }
        var ScalarFunction = Schema.ScalarFunctions.SingleOrDefault(p => p.Name==BaseName);
        if(ScalarFunction is not null) {
            this.SynonymScalarFunctions.Add((SynonymName,ScalarFunction));
            return;
        }
        var TableFunction = Schema.TableFunctions.SingleOrDefault(p => p.Name==BaseName);
        if(TableFunction is not null) {
            Schema.SynonymTableFunctions.Add((SynonymName,TableFunction));
            return;
        }
        var Procedure = Schema.Procedures.SingleOrDefault(p => p.Name==BaseName);
        if(Procedure is not null) {
            Schema.SynonymProcedures.Add((SynonymName,Procedure));
            return;
        }
        throw new NotSupportedException(base_object_name);
    }
    //public void CreateSynonymView(string Synonym,IView View)=>this.SynonymViews.Add((Synonym,View));
    //public void CreateSynonymScalarFunction(string Synonym,IScalarFunction ScalarFunction)=>this.SynonymScalarFunctions.Add((Synonym,ScalarFunction));
    //public void CreateSynonymTableFunction(string Synonym,ITableFunction TableFunction)=>this.SynonymTableFunctions.Add((Synonym,TableFunction));
    //public void CreateSynonymProcedure(string Synonym,IProcedure Procedure)=>this.SynonymProcedures.Add((Synonym,Procedure));
}
