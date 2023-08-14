using System;
using System.Diagnostics;
using System.Text;
namespace LinqDB.Sets;

//[Serializable, DebuggerDisplay("{ToString().Replace(\"\r\n\",\",\")}")]
[Serializable,DebuggerDisplay("{"+nameof(DebuggerDisplay)+"}")]
public abstract class Entity{
    private string DebuggerDisplay=>this.ToString().Replace("\r\n",",");
    protected static void ProtectedToStringBuilder(StringBuilder sb,string Name,object Value)=>
        sb.Append(Name+':').Append(Value.ToString()).Append(',');
    protected virtual void ToStringBuilder(StringBuilder sb) { }
    public override string ToString() {
        var sb = new StringBuilder();
        this.ToStringBuilder(sb);
        return sb.ToString();
    }
}