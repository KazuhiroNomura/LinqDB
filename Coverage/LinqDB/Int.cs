using System;
using System.Diagnostics;
namespace CoverageCS.LinqDB;
#pragma warning disable IDE0009 // Member access should be qualified.
[DebuggerDisplay("{" + nameof(v) + "}")]
#pragma warning restore IDE0009 // Member access should be qualified.
public struct Int : IEquatable<Int>
{
    private readonly long v;
    public Int(long v) => this.v = v;
    public static implicit operator Int(long v) => new(v);
    public static Int operator +(Int a, long b) => new(a.v + b);
    public static Int operator +(long a, Int b) => new(a + b.v);
    public static Int operator +(Int a, int b) => new(a.v + b);
    public static Int operator +(int a, Int b) => new(a + b.v);
    public static Int operator +(Int a, Int b) => new(a.v + b.v);
    public static bool operator ==(Int a, int b) => a.v == b;
    public static bool operator !=(Int a, int b) => a.v != b;
    public static bool operator ==(Int a, Int b) => a.v == b.v;
    public static bool operator !=(Int a, Int b) => a.v != b.v;
    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        return obj is Int i &&this.Equals(i);
    }
    public bool Equals(Int other) => this.v == other.v;
    public override int GetHashCode() => this.v.GetHashCode();
}