using System.Diagnostics;
using System.Numerics;
[DebuggerDisplay("{"+nameof(v)+"}")]
public readonly struct Int:IEquatable<Int>,IAdditionOperators<Int,Int,Int>{
    private readonly long v;
    public Int(long v)=>this.v=v;
    public static implicit operator Int(long v)=>new(v);
    public static Int operator+(Int a,long b)=>new(a.v+b);
    public static Int operator+(long a,Int b)=>new(a+b.v);
    public static Int operator+(Int a,int b)=>new(a.v+b);
    public static Int operator+(int a,Int b)=>new(a+b.v);
    public static Int operator+(Int a,Int b)=>new(a.v+b.v);
    public static bool operator==(Int a,int b)=>a.v==b;
    public static bool operator!=(Int a,int b)=>a.v!=b;
    public static bool operator==(Int a,Int b)=>a.v==b.v;
    public static bool operator!=(Int a,Int b)=>a.v!=b.v;
    public override bool Equals(object? obj)=>obj is Int other&&this.Equals(other);
    public bool Equals(Int other)=>this.v==other.v;
    public override int GetHashCode()=>this.v.GetHashCode();
}
