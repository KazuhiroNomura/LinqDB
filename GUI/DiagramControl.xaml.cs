using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GUI;

/// <summary>
/// DiagramControl.xaml の相互作用ロジック
/// </summary>
public partial class DiagramControl:UserControl {
    public DiagramControl() {
        this.InitializeComponent();
    }
    private static T? FindVisualChild<T>(DependencyObject obj) where T : DependencyObject {
        for(var i = 0;i<VisualTreeHelper.GetChildrenCount(obj);i++) {
            var child = VisualTreeHelper.GetChild(obj,i);
            if(child is T Result) {
                return Result;
            } else {
                var childOfChild = FindVisualChild<T>(child);
                if(childOfChild!=null) {
                    return childOfChild;
                }
            }
        }
        return null;
    }
    private static T? FindVisualChild<T>(DependencyObject obj,string Name) where T : FrameworkElement {
        for(var i = 0;i<VisualTreeHelper.GetChildrenCount(obj);i++) {
            var child = VisualTreeHelper.GetChild(obj,i);
            if(child is T Result&&Result.Name==Name) {
                return Result;
            } else {
                var childOfChild = FindVisualChild<T>(child,Name);
                if(childOfChild!=null) {
                    return childOfChild;
                }
            }
        }
        return null;
    }
    private void DragDelta(object sender,DragDeltaEventArgs e) {
        var Thumb = e.Source as Thumb;
        if(Thumb==null)
            return;
        if(Thumb.DataContext is VM.Table Table) {
            var ListBoxItem = (ListBoxItem)ItemsControl.ContainerFromElement(this.DiagramListBox,Thumb);
            var Top = Canvas.GetTop(ListBoxItem)+e.VerticalChange;
            var Left = Canvas.GetLeft(ListBoxItem)+e.HorizontalChange;
            Table.Top=Top;
            Table.Left=Left;
            var DataGridRowHeader = FindVisualChild<DataGridRowHeader>(ListBoxItem)!;
            var Point = DataGridRowHeader.PointToScreen(new Point(Left-VM.Common.ColumnHeaderWidth/2,Top));
            Table.Point=FindVisualChild<Canvas>(ListBoxItem)!.PointFromScreen(Point);
        }
    }
    private void つまみMouseLeftButtonDown(object sender,System.Windows.Input.MouseButtonEventArgs e) {
        if(((FrameworkElement)sender).DataContext is VM.Table Table) {
            var DiagramObjects = ((VM.Container)Table.Schema.Container!).DiagramObjects;
            var TableCount = DiagramObjects.OfType<VM.Table>().Count();
            var oldIndex = DiagramObjects.IndexOf(Table);
            DiagramObjects.Move(oldIndex,TableCount-1);
        }
    }
    private void RemoveTable_Click(object sender,RoutedEventArgs e) {
        var Database = (VM.Container)this.DataContext;
        var VMTable = (VM.Table)((Button)sender).DataContext;
        var 親Relations = VMTable.親Relations;
        var DiagramObjects = Database.DiagramObjects;
        DiagramObjects.Remove(VMTable);
        var Relations = Database.Relations;
        foreach(var 親Relation in 親Relations) {
            DiagramObjects.Remove(親Relation);
            Relations.Remove(親Relation);
        }
        var 子Relations = VMTable.子Relations;
        foreach(var 子Relation in 子Relations) {
            DiagramObjects.Remove(子Relation);
            Relations.Remove(子Relation);
        }
        親Relations.Clear();
        子Relations.Clear();
        Database.Remove(VMTable);
        ((VM.Schema)VMTable.Schema).Tables.Remove(VMTable);
    }
}