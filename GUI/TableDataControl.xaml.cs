using System.Windows;
using System.Windows.Controls;

namespace GUI;

/// <summary>
/// TableControl.xaml の相互作用ロジック
/// </summary>
public partial class TableDataControl:UserControl {
    public TableDataControl() {
        this.InitializeComponent();
    }
    private static readonly DependencyProperty _DataContext2 = DependencyProperty.Register(nameof(DataContext2),typeof(bool),typeof(TableDataControl));
    public bool DataContext2 {
        get => (bool)this.GetValue(_DataContext2);
        set => this.SetValue(_DataContext2,value);
    }
}