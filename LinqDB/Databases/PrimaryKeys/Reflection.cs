using LinqDB.CRC;

using System;
using System.Runtime.InteropServices;
namespace LinqDB.Databases.PrimaryKeys;
[Serializable]
[StructLayout(LayoutKind.Auto)]
public readonly struct Reflection:IEquatable<Reflection> {
    public string Name { get; }
    public Reflection(string Name) {
        this.Name=Name;
    }
    public bool Equals(Reflection other) {
        if(this.Name.Equals(other.Name)) return true;
        return false;
    }
    internal void InputHashCode(ref CRC32 CRC) {
        CRC.Input(this.Name);
    }
    public override int GetHashCode() {
        var CRC = new CRC32();
        this.InputHashCode(ref CRC);
        return CRC.GetHashCode();
    }
    public override string ToString() => this.Name;
    public static bool operator ==(Reflection a,Reflection b) => a.Equals(b);
    public static bool operator !=(Reflection a,Reflection b) => !a.Equals(b);
    public override bool Equals(object? obj) => obj is Reflection other ? this.Equals(other) : false;
}
