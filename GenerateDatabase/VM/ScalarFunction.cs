using LinqDB.Databases.Dom;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class ScalarFunction(string Name,Schema Schema,string SQL):IScalarFunction {
    public List<Parameter> Parameters { get; } = new();
    IEnumerable<IParameter> IParameters.Parameters => this.Parameters;
    public Type? Type{ get; set; }
    public bool IsNullable{ get; set; }
    public ISchema Schema {
        get;
        set;
    }=Schema;
    ISchema IScalarFunction.Schema => this.Schema;
    public string Name { get; set; }=Name;
    public string SQL { get; set; }=SQL;
}
