using System;
using System.Collections;
using System.Runtime.CompilerServices;
namespace LinqDB.Helpers;
[Serializable]
public struct ValueTuple:
    IEquatable<ValueTuple>,
    IStructuralEquatable,
    IStructuralComparable,
    IComparable,
    IComparable<ValueTuple>,
    ITuple{
    public override bool Equals(object? obj)=>obj is ValueTuple;
    public bool Equals(ValueTuple other)=>true;
#nullable disable
    bool IStructuralEquatable.Equals(object other,IEqualityComparer comparer)=>
        other is ValueTuple;
    public int GetHashCode(IEqualityComparer comparer){
        throw new NotImplementedException();
    }
    int IComparable.CompareTo(object other)=>other is ValueTuple?1:0;
#nullable enable
    public int CompareTo(ValueTuple other)=>0;
#nullable disable
    int IStructuralComparable.CompareTo(object other,IComparer comparer)=>other==null?1:0;
    public override int GetHashCode()=>0;
#nullable enable
    public override string ToString()=>"{}";
#nullable disable

    int ITuple.Length=>1;


#nullable enable
    object? ITuple.this[int index]=>throw new IndexOutOfRangeException();

    public static bool operator ==(ValueTuple left,ValueTuple right)=>true;
    public static bool operator !=(ValueTuple left,ValueTuple right)=>false;
    public static bool operator <(ValueTuple left,ValueTuple right)=>false;
    public static bool operator <=(ValueTuple left,ValueTuple right)=>true;
    public static bool operator >(ValueTuple left,ValueTuple right)=>false;
    public static bool operator >=(ValueTuple left,ValueTuple right)=>true;
}