using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GUI;

/// <summary>
/// RelationConnecter.xaml の相互作用ロジック
/// </summary>
public partial class RelationConnecter:UserControl {
    public static readonly DependencyProperty 親Property = DependencyProperty.Register(nameof(親),typeof(TableDataControl),typeof(RelationConnecter));
    public TableDataControl 親 {
        get => (TableDataControl)this.GetValue(親Property);
        set{
            this.SetValue(親Property,value);
            value.SetBinding(
                Canvas.LeftProperty,
                new Binding {
                    Path=new PropertyPath("(Canvas.Left)"),
                    Source=this.子
                }
            );
        }
    }
    public static readonly DependencyProperty 子Property = DependencyProperty.Register(nameof(子),typeof(TableDataControl),typeof(RelationConnecter));
    public TableDataControl 子 {
        get => (TableDataControl)this.GetValue(子Property);
        set => this.SetValue(子Property,value);
    }
    public RelationConnecter() {
        this.InitializeComponent();
        //<Style x: Key="矢印" TargetType="{x:Type Path}">
 
        //     <Setter Property="Data" Value="M0,0 L-2,-1 -2,1 0,0"/>
 
        //     <Setter Property="Width" Value="16"/>
 
        //     <Setter Property="Height" Value="16"/>
 
        //     <Setter Property="Stretch" Value="Fill"/>
 
        // </Style>
    }
}