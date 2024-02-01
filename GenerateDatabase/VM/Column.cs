using System;
using System.Diagnostics;
using System.Windows;
using LinqDB.Databases.Dom;
namespace VM;
[Serializable,DebuggerDisplay("{Type.Name+\" \"+Name}")]
public class Column: IColumn {
    public string Name{
        get;
        set;
    }
    public Type Type{
        get;
        set;
    }
    public bool IsNullable {
        get;
        set;
    }
    public bool IsPrimaryKey {
        get;
        set;
    }
    public Column() { }
    public Column(string Name,Type Type,bool IsNullable,bool IsPrimaryKey) {
        this.Name=Name;
        this.Type=Type;
        this.IsNullable=IsNullable;
        this.IsPrimaryKey=IsPrimaryKey;
    }
}
