﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using LinqDB.Sets;
using LinqDB.Databases.Tables;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using LinqDB.Databases.Attributes;
using LinqDB.Helpers;
// ReSharper disable RedundantNameQualifier
namespace LinqDB.Databases;
/// <summary>
/// エンティティの基底クラス
/// </summary>
//[MemoryPack.MemoryPackable,MessagePack.MessagePackObject]
[SuppressMessage("ReSharper","VirtualMemberCallInConstructor")]
public class Container:IDisposable{
    //protected const string 拡張子="MemoryPack";
    protected const string 拡張子="json";
    [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember, NonSerialized]
    //public readonly Serializers.MemoryPack.Serializer Serializer=new();
    public readonly Serializers.Utf8Json.Serializer Serializer=new();
    /// <summary>
    /// リレーションシップ制約
    /// </summary>
    public virtual void RelationValidate() {
    }
    /// <summary>
    /// 独自の制約
    /// </summary>
    public virtual void CustomValidate() {
    }
    /// <summary>
    /// 空集合
    /// </summary>
    public static Set<AttributeEmpty> TABLE_DUM { get; } = new();
    /// <summary>
    /// 1要素集合
    /// </summary>
    public static IEnumerable<AttributeEmpty> TABLE_DEE { get; } = new Set<AttributeEmpty> { new() };
    ///// <summary>
    ///// ログ書き込みに使うXmlDictionaryWriter
    ///// </summary>
    //[field: NonSerialized]
    //public Stream? Writer { get; private set; }
    //[field: NonSerialized]
    [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember,IgnoreDataMember]
    public Schemas.information_schema information_schema { get;}
    [MemoryPack.MemoryPackIgnore,MemoryPack.MemoryPackAllowSerialize,MessagePack.IgnoreMember, NonSerialized]
    private Schemas.system _system;
    [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember]
    public Schemas.system system=>this._system;
    /// <summary>
    /// 既定コンストラクタ。
    /// </summary>
    [MemoryPack.MemoryPackConstructor]
    public Container(){
        this.Init();
        {
            var tables=new Set<tables>();
            var columns=new Set<columns>();
            var referential_constraints=new Set<referential_constraints>();
            this.information_schema=new Schemas.information_schema(tables,columns,referential_constraints);
            var Schemas=new Set<PrimaryKeys.Reflection,Tables.Schema>();
            var Tables=new Set<PrimaryKeys.Reflection,Table>();
            var Views=new Set<PrimaryKeys.Reflection,View>();
            var TableColumns=new Set<PrimaryKeys.Reflection,TableColumn>();
            var ViewColumns=new Set<PrimaryKeys.Reflection,ViewColumn>();
            this._system=new Schemas.system(Schemas,Tables,Views,TableColumns,ViewColumns);
            var Container_Type=this.GetType();
            //var FieldInfoParent = Container_Type.GetField("Parent",BindingFlags.Instance|BindingFlags.NonPublic);
            //if(FieldInfoParent is null) {
            //    return;
            //}
            var ContainerType=this.GetType();// FieldInfoParent!.FieldType;
            static string Catalog取得(string Name)=>Name[..Name.IndexOf('.')];
            var catalog=Catalog取得(ContainerType.FullName!);
            var ChildExtensions=ContainerType.Assembly.GetType(catalog+".ChildExtensions");
            if(ChildExtensions is not null){
                foreach(var Method in ChildExtensions.GetMethods(BindingFlags.Static|BindingFlags.Public)){
                    var OneTableType=Method.ReturnType;
                    var OneCatalog=Catalog取得(OneTableType.FullName!);
                    var ManyTableType=Method.GetParameters()[0].ParameterType;
                    var ManyCatalog=Catalog取得(ManyTableType.FullName!);
                    referential_constraints.Add(
                        new referential_constraints(
                            ManyCatalog,
                            "constraint_schema",
                            Method.Name,
                            OneCatalog,
                            "unique_constraint_schema",
                            "PrimaryKey",
                            rule.ristrict,
                            rule.ristrict
                        )
                    );
                }
            }
            foreach(var Schema_Property in Container_Type.GetProperties(BindingFlags.Instance|BindingFlags.Public)){
                var system_Schema=new Tables.Schema(Schema_Property);
                if(!typeof(Sets.Schema).IsAssignableFrom(Schema_Property.PropertyType)){
                    continue;
                }
                Schemas.Add(system_Schema);
                foreach(var TableView_Property in Schema_Property.PropertyType.GetProperties(BindingFlags.Instance|BindingFlags.Public)){
                    var SequenceAttribute=TableView_Property.GetCustomAttribute<SequenceAttribute>();
                    if(SequenceAttribute is not null) continue;
                    var TableView_Type=TableView_Property.PropertyType;
                    if(TableView_Type.GetGenericArguments().Length==0) continue;
                    var ElementType=TableView_Type.GetGenericArguments()[0];
                    var TableView_FullName=ElementType.FullName!;
                    var TableIndex=TableView_FullName.LastIndexOf('.');
                    var SchemaIndex=TableView_FullName.LastIndexOf('.',TableIndex-1);
                    var CategoryIndex=TableView_FullName.LastIndexOf('.',SchemaIndex-1);
                    var Category=TableView_FullName[CategoryIndex..SchemaIndex];
                    switch(Category){
                        case".Tables":{
                            var Table=new Table(TableView_Property,system_Schema);
                            Tables.Add(Table);
                            foreach(var TableColumn_Property in ElementType.GetProperties(BindingFlags.Instance|BindingFlags.Public))
                                TableColumns.Add(new TableColumn(TableColumn_Property,Table));
                            break;
                        }
                        case".Views":{
                            var View=new View(TableView_Property,system_Schema);
                            Views.IsAdded(View);
                            foreach(var ViewColumn_Property in ElementType.GetProperties(BindingFlags.Instance|BindingFlags.Public)){
                                ViewColumns.IsAdded(new ViewColumn(ViewColumn_Property,View));
                            }
                            break;
                        }
                    }
                }
            }
            Schema処理(catalog,tables,columns,typeof(Schemas.system));
            //Schema処理(catalog,tables,columns,typeof(Container).GetProperty(nameof(this.information_schema)).PropertyType);
            foreach(var Schema in Container_Type.GetProperties(BindingFlags.Instance|BindingFlags.Public)){
                Schema処理(catalog,tables,columns,Schema.PropertyType);
            }
        }
        {
            var Regex= new Regex(@$"\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\.{拡張子}",RegexOptions.Compiled);
            var Files=Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"*.??????????????????").Where(p=>Regex.IsMatch(p)).OrderBy(p=>p);
            var File=Files.LastOrDefault();
            if(File is not null){
                using var Stream=new FileStream(File,FileMode.Open,FileAccess.Read);
                this.Read(Stream);
            }
        }
        static void Schema処理(string catalog,Set<tables> tables,Set<columns> columns,Type Schema) {
            foreach(var Table in Schema.GetProperties(BindingFlags.Instance|BindingFlags.Public)) {
                tables.IsAdded(
                    new tables(
                        catalog,
                        Schema.Name,
                        Table.Name,
                        table_type.base_type
                    )
                );
                var IEnumerable1 = Table.PropertyType.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
                if(IEnumerable1 is not null) {
                    Table処理(catalog,tables,columns,IEnumerable1.GetGenericArguments()[0]);
                }
            }
        }
        static void Table処理(string catalog,Set<tables> tables,Set<columns> columns,Type Table) {
            foreach(var Column in Table.GetProperties(BindingFlags.Instance|BindingFlags.Public)) {
                columns.IsAdded(
                    new columns(
                        catalog,
                        Table.Name,
                        Table.Name,
                        Column.Name,
                        //0,
                        false,
                        Column.PropertyType
                    )
                );
            }
        }
    }
    public static void ClearLog(){
        var Regex= new Regex(@$"\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\.{拡張子}",RegexOptions.Compiled);
        foreach(var File in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"*.??????????????????").Where(p=>Regex.IsMatch(p)))
            System.IO.File.Delete(File);
    }
    /// <summary>
    /// ファイナライザ
    /// </summary>
    ~Container() => this.Dispose(false);
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose() {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// 破棄されているか
    /// </summary>
    [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember]
    public bool IsDisposed { get; private set; }
    /// <summary>
    /// 継承先のファイナライザでthis.Dispose(false)を呼び出す
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing) {
        if(!this.IsDisposed) {
            this.IsDisposed=true;
            if(disposing) {
            }
        }
    }
    /// <summary>
    /// 全メンバーの初期化。
    /// </summary>
    protected virtual void Init() {
    }
    /// <summary>
    /// 全メンバーの読み込み。
    /// </summary>
    /// <param name="reader"></param>
    protected virtual void Read(Stream reader){}

    /// <summary>
    /// 全メンバーの書き込み。
    /// </summary>
    /// <param name="writer"></param>
    protected virtual void Write(Stream writer) {
    }
    /// <summary>
    /// AssociateSet`3,EntitySet`3間のリレーションを作る。
    /// </summary>
    protected virtual void UpdateRelationship() {
    }
    /// <summary>
    /// コミット処理を実装。
    /// </summary>
    /// <param name="LogStream"></param>
    protected virtual void Commit(Stream LogStream) {
    }
    /// <summary>
    /// コミットして上位のContainerに反映させる。
    /// </summary>
    public virtual void Commit() {
    }
    /// <summary>
    /// 全Setの要素数0にする。リレーションがあっても全データ削除するため単純な実装でいい。
    /// </summary>
    public virtual void Clear(){}
}
