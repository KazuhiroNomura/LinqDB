using System;
using System.Diagnostics;
namespace LinqDB.Sets;

[Serializable, DebuggerDisplay("{"+nameof(DebuggerDisplay)+"}")]
//public abstract class Schema<TSchema>:IEquatable<TSchema>{
public abstract class Schema{
    private string DebuggerDisplay => this.ToString()!.Replace("\r\n",",");
    //protected abstract void Read(XmlDictionaryReader Reader);
    //protected abstract void Write(XmlDictionaryWriter Writer);
    //protected abstract void Clear();
    //protected abstract void Assign(TSchema source);
    //public abstract Boolean Equals(TSchema other);
    //public override Boolean Equals(Object obj) => obj is TSchema other&&this.Equals(other);
}