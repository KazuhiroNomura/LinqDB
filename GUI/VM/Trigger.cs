using LinqDB.Databases.Dom;
using System.Diagnostics;

namespace GUI.VM;
[DebuggerDisplay("{this.InsertSQL??\"\"+this.UpdateSQL??\"\"+this.DeleteSQL??\"\"}")]
public class Trigger:IName {
    public string? InsertSQL;
    public string? UpdateSQL;
    public string? DeleteSQL;
    public string Name { get; set; }
}
