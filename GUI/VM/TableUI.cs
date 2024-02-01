using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using LinqDB.Helpers;
using System.Linq;
//using static LinqDB.Helpers.CommonLibrary;
using LinqDB.Databases.Dom;
namespace GUI.VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class TableUI:DependencyObject, ITableUI,IDiagramObject {
    public double RowHeight { get; } = Common.RowHeight;
    public double ColumnHeaderHeight { get; } = Common.ColumnHeaderHeight;
    public double ColumnHeaderWidth { get; } = Common.ColumnHeaderWidth;
    private static readonly DependencyProperty TableNameTextBox_IsReadOnlyProperty = DependencyProperty.Register(nameof(TableNameTextBox_IsReadOnly),typeof(bool),typeof(TableUI));
    public bool TableNameTextBox_IsReadOnly {
        get => (bool)this.GetValue(TableNameTextBox_IsReadOnlyProperty);
        set => this.SetValue(TableNameTextBox_IsReadOnlyProperty,value);
    }
    public ObservableCollection<Column> Columns { get; } = new();
    IEnumerable<IColumn> IColumns.Columns =>this.Columns;
    IEnumerable<string> SchemaNames => ((ITable)this).Schema.Schemas.Select(p => p.Name!);
    public IColumn this[string Column] => this.Columns.First(p => p.Name==Column);
    public readonly ObservableCollection<Trigger> BeforeTriggers = new();
    public readonly ObservableCollection<Trigger> AfterTriggers = new();
    public TableUI()=>this.TableNameTextBox_IsReadOnly=true;
    public TableUI(string Name,Schema Schema) {
        this.Name = Name;
        this.Schema = Schema;
    }
    public TableUI(string Name,Schema Schema,double Left,double Top):this(Name,Schema) {
        this.Left=Left;
        this.Top=Top;
    }
    public ObservableCollection<Relation> 親Relations { get; } = new();
    public ObservableCollection<Relation> 子Relations { get; } = new();
    private static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left),typeof(double),typeof(TableUI));
    public double Left {
        get => (double)this.GetValue(LeftProperty);
        set {
            this.SetValue(LeftProperty,value);
            foreach(var 親Relation in this.親Relations) {
                親Relation.子Left=value;
            }
            foreach(var 子Relation in this.子Relations) {
                子Relation.親Left=value;
            }
        }
    }
    private static readonly DependencyProperty TopProperty = DependencyProperty.Register(nameof(Top),typeof(double),typeof(TableUI));
    public double Top {
        get => (double)this.GetValue(TopProperty);
        set {
            this.SetValue(TopProperty,value);
            foreach(var 親Relation in this.親Relations) {
                親Relation.子Top=value;
            }
            foreach(var 子Relation in this.子Relations) {
                子Relation.親Top=value;
            }
        }
    }
    private static readonly DependencyProperty _Point = DependencyProperty.Register(nameof(Point),typeof(Point),typeof(TableUI));
    public Point Point {
        get => (Point)this.GetValue(_Point);
        set {
            this.SetValue(_Point,value);
            //const Int32 RowHeight = 44, 行高さ半分 = RowHeight/2;
            //const Int32 ColumnWidth = 44;
            //ドラッグしたのは線の親部分。矢印先。
            foreach(var Relation in this.親Relations) {
                共通(Relation,Relation.親TableUi!.Point,value);
            }
            foreach(var Relation in this.子Relations) {
                共通(Relation,value,Relation.子TableUI!.Point);
            }
            static void 共通(Relation Relation,Point 親_Point,Point 子_Point) {
                var Points = Relation.Points;
                Points.Clear();
                //　　┌
                //　┌┤
                //　│└
                //　└┐
                //　　│┌
                //　　└┤
                //　　　└
                //子Point0,子Point1,中Point0,中Point1,親Point0,親Point1
                var 子_Y開始 = 0.0;
                var 子_Y終了 = 0.0;
                var 子_X = 子_Point.X;
                var 子_Y = 子_Point.Y;
                var 子_X左 = 子_X-Common.接続線の二股の長さ;
                var 子_Y作業 = 子_Y+Common.RowHeight/2;
                var 子_Columns = Relation.子TableUI!.Columns;
                var 子_KeyColumns = Relation.Columns;
                foreach(var Column in 子_Columns){
                    var Index = 子_KeyColumns.IndexOf(Column);
                    if(Index>=0) {
                        if(Index==0) {
                            子_Y開始=子_Y作業;
                        }
                        Points.Add(new Point(子_X左,子_Y作業));
                        Points.Add(new Point(子_X,子_Y作業));
                        Points.Add(new Point(子_X左,子_Y作業));
                        if(Index==子_KeyColumns.Count-1) {
                            Points.Add(new Point(子_X左,子_Y作業));
                            子_Y終了=子_Y作業;
                        }
                    }
                    子_Y作業+=Common.RowHeight;
                }
                var 子高さ = (子_Y終了+子_Y開始)/2;
                var 子_Point0 = new Point(子_X左,子高さ);
                Points.Add(子_Point0);
                var 子_X左左 = 子_X左-Common.最低限必要な長さ;
                var 親_X = 親_Point.X;
                //var 中_X = (親_X+子_X)/2;
                var 親_Y開始 = 0.0;
                var 親_Y終了 = 0.0;
                var 親_Y = 親_Point.Y;
                var 親_Y作業 = 親_Y+Common.RowHeight/2;
                var 親_Columns = Relation.親TableUi!.Columns;
                var 親_KeyColumns = 親_Columns.Where(p => p.IsPrimaryKey).ToList();
                foreach(var Column in 親_Columns) {
                    var Index = 親_KeyColumns.IndexOf(Column);
                    if(Index>=0) {
                        if(Index==0) {
                            親_Y開始=親_Y作業;
                        }
                        if(Index==親_KeyColumns.Count-1) {
                            親_Y終了=親_Y作業;
                        }
                    }
                    親_Y作業+=Common.RowHeight;
                }
                var 親高さ = (親_Y終了+親_Y開始)/2;
                var 中高さ = (子高さ+親高さ)/2;
                //var 中_Point0 = new Point(子_X左左,中高さ);
                //Points.Add(中_Point0);
                var 親_X左 = 親_X-Common.接続線の二股の長さ;
                var 親_X左左 = 親_X左-Common.最低限必要な長さ;
                //var 中_Point1 = new Point(親_X左左,中高さ);
                //Points.Add(中_Point1);
                var 中_X = 親_X左左<子_X左左 ? 親_X左左 : 子_X左左;
                var 子_Point1 = new Point(中_X,子高さ);
                Points.Add(子_Point1);
                var 中_Point1 = new Point(中_X,親高さ);
                Points.Add(中_Point1);
                var 親_Point0 = new Point(中_X,親高さ);
                Points.Add(親_Point0);
                var 親_Point1 = new Point(親_X左,親高さ);
                Points.Add(親_Point1);
                親_Y作業=親_Point.Y+Common.RowHeight/2;
                foreach(var Column in 親_Columns) {
                    var Index = 親_KeyColumns.IndexOf(Column);
                    if(Index>=0) {
                        Points.Add(new Point(親_X左,親_Y作業));
                        Points.Add(new Point(親_X,親_Y作業));
                        Points.Add(new Point(親_X左,親_Y作業));
                        if(Index==親_KeyColumns.Count-1) {
                            Points.Add(new Point(親_X左,親_Y作業));
                        }
                    }
                    親_Y作業+=Common.RowHeight;
                }
                Relation.親Left=親_X左;
                Relation.親Top=親高さ;
                Relation.子Left=子_X左;
                Relation.子Top=子高さ;
            }
        }
    }
    public Visibility DataGridVisibility => Visibility.Visible;
    public Visibility LineVisibility => Visibility.Hidden;
    private static readonly DependencyProperty SchemaProperty = DependencyProperty.Register(
        nameof(Schema),
        typeof(ISchema),
        typeof(TableUI)/*,
    new FrameworkPropertyMetadata(
        (d,e) => {
            if(d is Schema Schema) {
                Schema.Tables.Remove(this);
            }
        }
    )*/
    );
    public ISchema Schema {
        get => (ISchema)this.GetValue(SchemaProperty);
        set => this.SetValue(SchemaProperty,value);
    }
    public string Name { get; set; }="";
    public Column CreateColumn(string Name,Type Type,bool IsNullable,bool IsPrimaryKey) {
        var Column = new Column(Name,IsNullable&&Type.IsNullable()? Type.GetGenericArguments()[0]:Type,IsNullable,IsPrimaryKey);
        Debug.Assert(this.Columns.All(p=>p.Name!=Name));
        this.Columns.Add(Column);
        return Column;
    }
}
