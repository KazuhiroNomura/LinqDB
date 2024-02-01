using System;
using System.Diagnostics;
using System.Windows;
using LinqDB.Databases.Dom;
namespace VM;
[Serializable,DebuggerDisplay("{Type.Name+\" \"+Name}")]
public class Parameter(string Name,Type Type,bool has_default_value,object default_value)
    :IParameter {
    public string Name{get;set;}=Name;
    public Type Type{ get; set; }=Type;
    public bool IsNullable=>true;
    public bool has_default_value{ get; }=has_default_value;
    public object default_value { get; }=default_value;
}
