using LinqDB.Databases.Dom;

using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace VM;
[DebuggerDisplay("{"+nameof(Name)+"}")]
public class ScalarFunction(string Name,Type Type,Schema Schema,string SQL):IScalarFunction {
    public List<Parameter> Parameters { get; } = new();
    IEnumerable<IParameter> IParameters.Parameters => this.Parameters;
    public bool IsNullable{ get; set; }
    public ISchema Schema {
        get;
        set;
    }=Schema;
    ISchema IScalarFunction.Schema => this.Schema;
    public Type Type { get; }=Type;
    public string Name { get; set; }=Name;
    public string SQL { get; set; }=SQL;
}
