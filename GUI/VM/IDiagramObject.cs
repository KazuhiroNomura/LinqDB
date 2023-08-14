using LinqDB.Databases.Dom;
using System.Windows;
namespace GUI.VM;
public interface IDiagramObject:IName {
    Visibility DataGridVisibility { get; }
    Visibility LineVisibility { get; }
}
