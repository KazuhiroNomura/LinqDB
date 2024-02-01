using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using LinqDB.Databases.Dom;
namespace GUI.VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class Relation:DependencyObject,IRelation,IDiagramObject {
    private static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name),typeof(string),typeof(Relation));
    public string Name {
        get => (string)this.GetValue(NameProperty);
        set => this.SetValue(NameProperty,value);
    }
    public string ToolTip {
        get {
            var sb = new StringBuilder($"{this.Name}:{this.親TableUi!.Name}<-{this.子TableUI!.Name}(");
            foreach(var Column in this.Columns) {
                sb.Append($"{Column.Name},");
            }
            sb[^1]=')';
            return sb.ToString();
        }
    }
    public TableUI? 親TableUi { get;internal set; }
    /// <summary>
    /// テーブルからは親が分かればいいのでこれが重要
    /// </summary>
    ITable? IRelation.親ITable => this.親TableUi;
    //internal Point 親Point;
    public TableUI? 子TableUI { get; internal set; }
    /// <summary>
    /// ダイアグラムでの子
    /// </summary>
    ITable? IRelation.子ITable => this.子TableUI;
    private static readonly DependencyProperty 親LeftProperty = DependencyProperty.Register(nameof(親Left),typeof(double),typeof(Relation));
    public double 親Left {
        get => (double)this.GetValue(親LeftProperty);
        set => this.SetValue(親LeftProperty,value);
    }
    private static readonly DependencyProperty 親TopProperty = DependencyProperty.Register(nameof(親Top),typeof(double),typeof(Relation));
    public double 親Top {
        get => (double)this.GetValue(親TopProperty);
        set => this.SetValue(親TopProperty,value);
    }
    private static readonly DependencyProperty 子LeftProperty = DependencyProperty.Register(nameof(子Left),typeof(double),typeof(Relation));
    public double 子Left {
        get => (double)this.GetValue(子LeftProperty);
        set => this.SetValue(子LeftProperty,value);
    }
    private static readonly DependencyProperty 子TopProperty = DependencyProperty.Register(nameof(子Top),typeof(double),typeof(Relation));
    public double 子Top {
        get => (double)this.GetValue(子TopProperty);
        set => this.SetValue(子TopProperty,value);
    }
    private static readonly DependencyProperty _Points = DependencyProperty.Register(nameof(Points),typeof(PointCollection),typeof(Relation));
    public PointCollection Points {
        get => (PointCollection)this.GetValue(_Points);
        set => this.SetValue(_Points,value);
    }
    private static readonly DependencyProperty _PathGeometry = DependencyProperty.Register(nameof(PathGeometry),typeof(PathGeometry),typeof(Relation));
    public PathGeometry PathGeometry {
        get => (PathGeometry)this.GetValue(_PathGeometry);
        set => this.SetValue(_PathGeometry,value);
    }
    public ObservableCollection<Column> Columns { get; } = new();
    IEnumerable<IColumn> IRelation.Columns => this.Columns;
    public IRelation.Information I { get; } = new();
    public Relation() {
        this.Points=new PointCollection();
        this.PathGeometry=new PathGeometry(new List<PathFigure>());
    }
    public Relation(string Name,TableUI 親TableUi,TableUI 子TableUI):this() {
        this.Name=Name;
        this.親TableUi=親TableUi;
        this.子TableUI=子TableUI;
    }
    public void AddColumn(Column Column) {
        this.Columns.Add(Column);
    }
    public Visibility DataGridVisibility => Visibility.Hidden;
    public Visibility LineVisibility => Visibility.Visible;
}
