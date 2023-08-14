#pragma warning disable 1591
using System;
using System.Runtime.CompilerServices;
// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable FieldCanBeMadeReadOnly.Global
#pragma warning disable CS8618//デフォルトコンストラクターで初期化はしない
namespace LinqDB.Optimizers;

/// <summary>
/// 最適化したDelegate.Targetに割り当てる自動変数を表現する抽象クラス。
/// </summary>
[Serializable]
public abstract class Target {
    public abstract object Object{
        get; internal set;
    }
}
//public class ClassTuple<T1>:IEquatable<ClassTuple<T1>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1>>,ITuple{
//	public T1 Item1;
//    public ClassTuple(){
//        this.Item1=default!;
//    }
//    int ITuple.Length=>6;
//    object? ITuple.this[int index]=>index switch{
//        0=>this.Item1,
//        _=>throw new IndexOutOfRangeException()
//    };
//    public ClassTuple(T1 Item1){
//        this.Item1 = Item1;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1>)obj);
//    }
//    public bool Equals(ClassTuple<T1>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1> valueTuple))
//			return false;
//		if (comparer.Equals(this.Item1,valueTuple.Item1))
//			return comparer.Equals(this,valueTuple);
//		return false;
//	}
//	int IComparable.CompareTo(object? other){
//		if(other == null)
//			return 1;
//		if(other is ClassTuple<T1> Tuple)
//    		return this.CompareTo(Tuple);
//        throw new ArgumentException(this.GetType().FullName,nameof(other));
//	}
//    public int CompareTo(ClassTuple<T1> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        return 0;
//	}

//    public static bool operator==(ClassTuple<T1> first,ClassTuple<T1> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1> first,ClassTuple<T1> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1> first,ClassTuple<T1> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1> first,ClassTuple<T1> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1> first,ClassTuple<T1> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1> first,ClassTuple<T1> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        return 0;
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1);
//    }
//    public int GetHashCode(IEqualityComparer comparer)=>this.GetHashCode();
//}
//public class ClassTuple<T1,T2>:IEquatable<ClassTuple<T1,T2>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1,T2>>,ITuple{
//	public T1 Item1;
//    public T2 Item2;
//    public ClassTuple(){
//        this.Item1=default!;
//        this.Item2=default!;
//    }
//    int ITuple.Length=>6;
//    object? ITuple.this[int index]=>index switch{
//        0=>this.Item1,
//        1=>this.Item2,
//        _=>throw new IndexOutOfRangeException()
//    };
//    public ClassTuple(T1 Item1,T2 Item2){
//        this.Item1 = Item1;
//        this.Item2 = Item2;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1,T2>)obj);
//    }
//    public bool Equals(ClassTuple<T1,T2>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1)&&EqualityComparer<T2>.Default.Equals(this.Item2,other.Item2);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1,T2> valueTuple))
//		{
//			return false;
//		}
//		if (comparer.Equals(this.Item1,valueTuple.Item1) && comparer.Equals(this.Item2,valueTuple.Item2))
//		{
//			return comparer.Equals(this,valueTuple);
//		}
//		return false;
//	}

//	int IComparable.CompareTo(object? other){
//		if(other == null)
//			return 1;
//		if(other is ClassTuple<T1,T2> Tuple)
//    		return this.CompareTo(Tuple);
//        throw new ArgumentException(this.GetType().FullName,nameof(other));
//	}
//    public int CompareTo(ClassTuple<T1,T2> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        if((num=Comparer<T2>.Default.Compare(this.Item2,other.Item2))!= 0)return num;
//        return 0;
//	}

//    public static bool operator==(ClassTuple<T1,T2> first,ClassTuple<T1,T2> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1,T2> first,ClassTuple<T1,T2> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1,T2> first,ClassTuple<T1,T2> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1,T2> first,ClassTuple<T1,T2> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1,T2> first,ClassTuple<T1,T2> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1,T2> first,ClassTuple<T1,T2> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1,T2> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        if((num=comparer.Compare(this.Item2,valueTuple.Item2))!= 0)return num;
//        return 0;
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1,this.Item2);
//    }
//    public int GetHashCode(IEqualityComparer comparer)=>this.GetHashCode();
//}
//public class ClassTuple<T1,T2,T3>:IEquatable<ClassTuple<T1,T2,T3>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1,T2,T3>>,ITuple{
//	public T1 Item1;
//    public T2 Item2;
//    public T3 Item3;
//    public ClassTuple(){
//        this.Item1=default!;
//        this.Item2=default!;
//        this.Item3=default!;
//    }
//    int ITuple.Length=>6;
//    object? ITuple.this[int index]=>index switch{
//        0=>this.Item1,
//        1=>this.Item2,
//        2=>this.Item3,
//        _=>throw new IndexOutOfRangeException()
//    };
//    public ClassTuple(T1 Item1,T2 Item2,T3 Item3){
//        this.Item1 = Item1;
//        this.Item2 = Item2;
//        this.Item3 = Item3;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1,T2,T3>)obj);
//    }
//    public bool Equals(ClassTuple<T1,T2,T3>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1)&&EqualityComparer<T2>.Default.Equals(this.Item2,other.Item2)&&EqualityComparer<T3>.Default.Equals(this.Item3,other.Item3);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1,T2,T3> valueTuple))
//		{
//			return false;
//		}
//		if (comparer.Equals(this.Item1,valueTuple.Item1) && comparer.Equals(this.Item2,valueTuple.Item2) && comparer.Equals(this.Item3,valueTuple.Item3))
//		{
//			return comparer.Equals(this,valueTuple);
//		}
//		return false;
//	}

//	int IComparable.CompareTo(object? other){
//		if(other == null)
//			return 1;
//		if(other is ClassTuple<T1,T2,T3> Tuple)
//    		return this.CompareTo(Tuple);
//        throw new ArgumentException(this.GetType().FullName,nameof(other));
//	}
//    public int CompareTo(ClassTuple<T1,T2,T3> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        if((num=Comparer<T2>.Default.Compare(this.Item2,other.Item2))!= 0)return num;
//        if((num=Comparer<T3>.Default.Compare(this.Item3,other.Item3))!= 0)return num;
//        return 0;
//	}

//    public static bool operator==(ClassTuple<T1,T2,T3> first,ClassTuple<T1,T2,T3> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1,T2,T3> first,ClassTuple<T1,T2,T3> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1,T2,T3> first,ClassTuple<T1,T2,T3> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1,T2,T3> first,ClassTuple<T1,T2,T3> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1,T2,T3> first,ClassTuple<T1,T2,T3> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1,T2,T3> first,ClassTuple<T1,T2,T3> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1,T2,T3> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        if((num=comparer.Compare(this.Item2,valueTuple.Item2))!= 0)return num;
//        if((num=comparer.Compare(this.Item3,valueTuple.Item3))!= 0)return num;
//        return 0;
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1,this.Item2,this.Item3);
//    }
//    public int GetHashCode(IEqualityComparer comparer)=>this.GetHashCode();
//}
//public class ClassTuple<T1,T2,T3,T4>:IEquatable<ClassTuple<T1,T2,T3,T4>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1,T2,T3,T4>>,ITuple{
//	public T1 Item1;
//    public T2 Item2;
//    public T3 Item3;
//    public T4 Item4;
//    public ClassTuple(){
//        this.Item1=default!;
//        this.Item2=default!;
//        this.Item3=default!;
//        this.Item4=default!;
//    }
//    int ITuple.Length=>6;
//    object? ITuple.this[int index]=>index switch{
//        0=>this.Item1,
//        1=>this.Item2,
//        2=>this.Item3,
//        3=>this.Item4,
//        _=>throw new IndexOutOfRangeException()
//    };
//    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4){
//        this.Item1 = Item1;
//        this.Item2 = Item2;
//        this.Item3 = Item3;
//        this.Item4 = Item4;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1,T2,T3,T4>)obj);
//    }
//    public bool Equals(ClassTuple<T1,T2,T3,T4>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1)&&EqualityComparer<T2>.Default.Equals(this.Item2,other.Item2)&&EqualityComparer<T3>.Default.Equals(this.Item3,other.Item3)&&EqualityComparer<T4>.Default.Equals(this.Item4,other.Item4);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1,T2,T3,T4> valueTuple))
//		{
//			return false;
//		}
//		if (comparer.Equals(this.Item1,valueTuple.Item1) && comparer.Equals(this.Item2,valueTuple.Item2) && comparer.Equals(this.Item3,valueTuple.Item3) && comparer.Equals(this.Item4,valueTuple.Item4))
//		{
//			return comparer.Equals(this,valueTuple);
//		}
//		return false;
//	}

//	int IComparable.CompareTo(object? other){
//		if(other == null)
//			return 1;
//		if(other is ClassTuple<T1,T2,T3,T4> Tuple)
//    		return this.CompareTo(Tuple);
//        throw new ArgumentException(this.GetType().FullName,nameof(other));
//	}
//    public int CompareTo(ClassTuple<T1,T2,T3,T4> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        if((num=Comparer<T2>.Default.Compare(this.Item2,other.Item2))!= 0)return num;
//        if((num=Comparer<T3>.Default.Compare(this.Item3,other.Item3))!= 0)return num;
//        if((num=Comparer<T4>.Default.Compare(this.Item4,other.Item4))!= 0)return num;
//        return 0;
//	}

//    public static bool operator==(ClassTuple<T1,T2,T3,T4> first,ClassTuple<T1,T2,T3,T4> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1,T2,T3,T4> first,ClassTuple<T1,T2,T3,T4> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1,T2,T3,T4> first,ClassTuple<T1,T2,T3,T4> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1,T2,T3,T4> first,ClassTuple<T1,T2,T3,T4> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1,T2,T3,T4> first,ClassTuple<T1,T2,T3,T4> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1,T2,T3,T4> first,ClassTuple<T1,T2,T3,T4> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1,T2,T3,T4> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        if((num=comparer.Compare(this.Item2,valueTuple.Item2))!= 0)return num;
//        if((num=comparer.Compare(this.Item3,valueTuple.Item3))!= 0)return num;
//        if((num=comparer.Compare(this.Item4,valueTuple.Item4))!= 0)return num;
//        return 0;
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4);
//    }
//    public int GetHashCode(IEqualityComparer comparer)=>this.GetHashCode();
//}

//public class ClassTuple<T1,T2,T3,T4,T5>:IEquatable<ClassTuple<T1,T2,T3,T4,T5>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1,T2,T3,T4,T5>>,ITuple{
//	public T1 Item1;
//    public T2 Item2;
//    public T3 Item3;
//    public T4 Item4;
//    public T5 Item5;
//    public ClassTuple(){
//        this.Item1=default!;
//        this.Item2=default!;
//        this.Item3=default!;
//        this.Item4=default!;
//        this.Item5=default!;
//    }
//    int ITuple.Length=>6;
//    object? ITuple.this[int index]=>index switch{
//        0=>this.Item1,
//        1=>this.Item2,
//        2=>this.Item3,
//        3=>this.Item4,
//        4=>this.Item5,
//        _=>throw new IndexOutOfRangeException()
//    };
//    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5){
//        this.Item1 = Item1;
//        this.Item2 = Item2;
//        this.Item3 = Item3;
//        this.Item4 = Item4;
//        this.Item5 = Item5;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1,T2,T3,T4,T5>)obj);
//    }
//    public bool Equals(ClassTuple<T1,T2,T3,T4,T5>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1)&&EqualityComparer<T2>.Default.Equals(this.Item2,other.Item2)&&EqualityComparer<T3>.Default.Equals(this.Item3,other.Item3)&&EqualityComparer<T4>.Default.Equals(this.Item4,other.Item4)&&EqualityComparer<T5>.Default.Equals(this.Item5,other.Item5);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1,T2,T3,T4,T5> valueTuple))
//		{
//			return false;
//		}
//		if (comparer.Equals(this.Item1,valueTuple.Item1) && comparer.Equals(this.Item2,valueTuple.Item2) && comparer.Equals(this.Item3,valueTuple.Item3) && comparer.Equals(this.Item4,valueTuple.Item4) && comparer.Equals(this.Item5,valueTuple.Item5))
//		{
//			return comparer.Equals(this,valueTuple);
//		}
//		return false;
//	}

//	int IComparable.CompareTo(object? other){
//		if(other == null)
//			return 1;
//		if(other is ClassTuple<T1,T2,T3,T4,T5> Tuple)
//    		return this.CompareTo(Tuple);
//        throw new ArgumentException(this.GetType().FullName,nameof(other));
//	}
//    public int CompareTo(ClassTuple<T1,T2,T3,T4,T5> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        if((num=Comparer<T2>.Default.Compare(this.Item2,other.Item2))!= 0)return num;
//        if((num=Comparer<T3>.Default.Compare(this.Item3,other.Item3))!= 0)return num;
//        if((num=Comparer<T4>.Default.Compare(this.Item4,other.Item4))!= 0)return num;
//        if((num=Comparer<T5>.Default.Compare(this.Item5,other.Item5))!= 0)return num;
//        return 0;
//	}

//    public static bool operator==(ClassTuple<T1,T2,T3,T4,T5> first,ClassTuple<T1,T2,T3,T4,T5> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1,T2,T3,T4,T5> first,ClassTuple<T1,T2,T3,T4,T5> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1,T2,T3,T4,T5> first,ClassTuple<T1,T2,T3,T4,T5> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1,T2,T3,T4,T5> first,ClassTuple<T1,T2,T3,T4,T5> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1,T2,T3,T4,T5> first,ClassTuple<T1,T2,T3,T4,T5> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1,T2,T3,T4,T5> first,ClassTuple<T1,T2,T3,T4,T5> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1,T2,T3,T4,T5> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        if((num=comparer.Compare(this.Item2,valueTuple.Item2))!= 0)return num;
//        if((num=comparer.Compare(this.Item3,valueTuple.Item3))!= 0)return num;
//        if((num=comparer.Compare(this.Item4,valueTuple.Item4))!= 0)return num;
//        if((num=comparer.Compare(this.Item5,valueTuple.Item5))!= 0)return num;
//        return 0;
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5);
//    }
//    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer){
//        var h=new HashCode();
//        h.Add(comparer.GetHashCode(this.Item1));
//        h.Add(comparer.GetHashCode(this.Item2));
//        h.Add(comparer.GetHashCode(this.Item3));
//        h.Add(comparer.GetHashCode(this.Item4));
//        h.Add(comparer.GetHashCode(this.Item5));
//        return h.ToHashCode();
//	}
//}
//public class ClassTuple<T1,T2,T3,T4,T5,T6>:IEquatable<ClassTuple<T1,T2,T3,T4,T5,T6>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1,T2,T3,T4,T5,T6>>,ITuple{
//	public T1 Item1;
//    public T2 Item2;
//    public T3 Item3;
//    public T4 Item4;
//    public T5 Item5;
//    public T6 Item6;
//    public ClassTuple(){
//        this.Item1=default!;
//        this.Item2=default!;
//        this.Item3=default!;
//        this.Item4=default!;
//        this.Item5=default!;
//        this.Item6=default!;
//    }
//    int ITuple.Length=>6;
//    object? ITuple.this[int index]=>index switch{
//        0=>this.Item1,
//        1=>this.Item2,
//        2=>this.Item3,
//        3=>this.Item4,
//        4=>this.Item5,
//        5=>this.Item6,
//        _=>throw new IndexOutOfRangeException()
//    };
//    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5,T6 Item6){
//        this.Item1 = Item1;
//        this.Item2 = Item2;
//        this.Item3 = Item3;
//        this.Item4 = Item4;
//        this.Item5 = Item5;
//        this.Item6 = Item6;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1,T2,T3,T4,T5,T6>)obj);
//    }
//    public bool Equals(ClassTuple<T1,T2,T3,T4,T5,T6>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1)&&EqualityComparer<T2>.Default.Equals(this.Item2,other.Item2)&&EqualityComparer<T3>.Default.Equals(this.Item3,other.Item3)&&EqualityComparer<T4>.Default.Equals(this.Item4,other.Item4)&&EqualityComparer<T5>.Default.Equals(this.Item5,other.Item5)&&EqualityComparer<T6>.Default.Equals(this.Item6,other.Item6)&&this.Equals(other);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1,T2,T3,T4,T5,T6> valueTuple))
//		{
//			return false;
//		}
//		if (comparer.Equals(this.Item1,valueTuple.Item1) && comparer.Equals(this.Item2,valueTuple.Item2) && comparer.Equals(this.Item3,valueTuple.Item3) && comparer.Equals(this.Item4,valueTuple.Item4) && comparer.Equals(this.Item5,valueTuple.Item5) && comparer.Equals(this.Item6,valueTuple.Item6))
//		{
//			return comparer.Equals(this,valueTuple);
//		}
//		return false;
//	}

//	int IComparable.CompareTo(object? other){
//		if(other == null)
//			return 1;
//		if(other is ClassTuple<T1,T2,T3,T4,T5,T6> Tuple)
//    		return this.CompareTo(Tuple);
//        throw new ArgumentException(this.GetType().FullName,nameof(other));
//	}
//    public int CompareTo(ClassTuple<T1,T2,T3,T4,T5,T6> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        if((num=Comparer<T2>.Default.Compare(this.Item2,other.Item2))!= 0)return num;
//        if((num=Comparer<T3>.Default.Compare(this.Item3,other.Item3))!= 0)return num;
//        if((num=Comparer<T4>.Default.Compare(this.Item4,other.Item4))!= 0)return num;
//        if((num=Comparer<T5>.Default.Compare(this.Item5,other.Item5))!= 0)return num;
//        if((num=Comparer<T6>.Default.Compare(this.Item6,other.Item6))!= 0)return num;
//        return 0;
//	}

//    public static bool operator==(ClassTuple<T1,T2,T3,T4,T5,T6> first,ClassTuple<T1,T2,T3,T4,T5,T6> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1,T2,T3,T4,T5,T6> first,ClassTuple<T1,T2,T3,T4,T5,T6> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1,T2,T3,T4,T5,T6> first,ClassTuple<T1,T2,T3,T4,T5,T6> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1,T2,T3,T4,T5,T6> first,ClassTuple<T1,T2,T3,T4,T5,T6> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1,T2,T3,T4,T5,T6> first,ClassTuple<T1,T2,T3,T4,T5,T6> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1,T2,T3,T4,T5,T6> first,ClassTuple<T1,T2,T3,T4,T5,T6> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1,T2,T3,T4,T5,T6> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        if((num=comparer.Compare(this.Item2,valueTuple.Item2))!= 0)return num;
//        if((num=comparer.Compare(this.Item3,valueTuple.Item3))!= 0)return num;
//        if((num=comparer.Compare(this.Item4,valueTuple.Item4))!= 0)return num;
//        if((num=comparer.Compare(this.Item5,valueTuple.Item5))!= 0)return num;
//        if((num=comparer.Compare(this.Item6,valueTuple.Item6))!= 0)return num;
//        return 0;
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5,this.Item6);
//    }
//    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer){
//        var h=new HashCode();
//        h.Add(comparer.GetHashCode(this.Item1));
//        h.Add(comparer.GetHashCode(this.Item2));
//        h.Add(comparer.GetHashCode(this.Item3));
//        h.Add(comparer.GetHashCode(this.Item4));
//        h.Add(comparer.GetHashCode(this.Item5));
//        h.Add(comparer.GetHashCode(this.Item6));
//        return h.ToHashCode();
//	}
//}
//public class ClassTuple<T1,T2,T3,T4,T5,T6,T7>:IEquatable<ClassTuple<T1,T2,T3,T4,T5,T6,T7>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1,T2,T3,T4,T5,T6,T7>>,ITuple{
//	public T1 Item1;
//    public T2 Item2;
//    public T3 Item3;
//    public T4 Item4;
//    public T5 Item5;
//    public T6 Item6;
//    public T7 Item7;
//    public ClassTuple(){
//        this.Item1=default!;
//        this.Item2=default!;
//        this.Item3=default!;
//        this.Item4=default!;
//        this.Item5=default!;
//        this.Item6=default!;
//        this.Item7=default!;
//    }
//    int ITuple.Length=>7;
//    object? ITuple.this[int index]=>index switch{
//        0=>this.Item1,
//        1=>this.Item2,
//        2=>this.Item3,
//        3=>this.Item4,
//        4=>this.Item5,
//        5=>this.Item6,
//        6=>this.Item7,
//        _=>throw new IndexOutOfRangeException()
//    };
//    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5,T6 Item6,T7 Item7){
//        this.Item1 = Item1;
//        this.Item2 = Item2;
//        this.Item3 = Item3;
//        this.Item4 = Item4;
//        this.Item5 = Item5;
//        this.Item6 = Item6;
//        this.Item7 = Item7;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1,T2,T3,T4,T5,T6,T7>)obj);
//    }
//    public bool Equals(ClassTuple<T1,T2,T3,T4,T5,T6,T7>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1)&&EqualityComparer<T2>.Default.Equals(this.Item2,other.Item2)&&EqualityComparer<T3>.Default.Equals(this.Item3,other.Item3)&&EqualityComparer<T4>.Default.Equals(this.Item4,other.Item4)&&EqualityComparer<T5>.Default.Equals(this.Item5,other.Item5)&&EqualityComparer<T6>.Default.Equals(this.Item6,other.Item6)&&EqualityComparer<T7>.Default.Equals(this.Item7,other.Item7)&&this.Equals(other);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1,T2,T3,T4,T5,T6,T7> valueTuple))
//		{
//			return false;
//		}
//		if (comparer.Equals(this.Item1,valueTuple.Item1) && comparer.Equals(this.Item2,valueTuple.Item2) && comparer.Equals(this.Item3,valueTuple.Item3) && comparer.Equals(this.Item4,valueTuple.Item4) && comparer.Equals(this.Item5,valueTuple.Item5) && comparer.Equals(this.Item6,valueTuple.Item6) && comparer.Equals(this.Item7,valueTuple.Item7))
//		{
//			return comparer.Equals(this,valueTuple);
//		}
//		return false;
//	}

//	int IComparable.CompareTo(object? other){
//		if(other == null)
//			return 1;
//		if(other is ClassTuple<T1,T2,T3,T4,T5,T6,T7> Tuple)
//    		return this.CompareTo(Tuple);
//        throw new ArgumentException(this.GetType().FullName,nameof(other));
//	}
//    public int CompareTo(ClassTuple<T1,T2,T3,T4,T5,T6,T7> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        if((num=Comparer<T2>.Default.Compare(this.Item2,other.Item2))!= 0)return num;
//        if((num=Comparer<T3>.Default.Compare(this.Item3,other.Item3))!= 0)return num;
//        if((num=Comparer<T4>.Default.Compare(this.Item4,other.Item4))!= 0)return num;
//        if((num=Comparer<T5>.Default.Compare(this.Item5,other.Item5))!= 0)return num;
//        if((num=Comparer<T6>.Default.Compare(this.Item6,other.Item6))!= 0)return num;
//        if((num=Comparer<T7>.Default.Compare(this.Item7,other.Item7))!= 0)return num;
//        return 0;
//	}

//    public static bool operator==(ClassTuple<T1,T2,T3,T4,T5,T6,T7> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1,T2,T3,T4,T5,T6,T7> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1,T2,T3,T4,T5,T6,T7> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1,T2,T3,T4,T5,T6,T7> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1,T2,T3,T4,T5,T6,T7> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1,T2,T3,T4,T5,T6,T7> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1,T2,T3,T4,T5,T6,T7> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        if((num=comparer.Compare(this.Item2,valueTuple.Item2))!= 0)return num;
//        if((num=comparer.Compare(this.Item3,valueTuple.Item3))!= 0)return num;
//        if((num=comparer.Compare(this.Item4,valueTuple.Item4))!= 0)return num;
//        if((num=comparer.Compare(this.Item5,valueTuple.Item5))!= 0)return num;
//        if((num=comparer.Compare(this.Item6,valueTuple.Item6))!= 0)return num;
//        if((num=comparer.Compare(this.Item7,valueTuple.Item7))!= 0)return num;
//        return 0;
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5,this.Item6,this.Item7,this);
//    }
//    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer){
//        var h=new HashCode();
//        h.Add(comparer.GetHashCode(this.Item1));
//        h.Add(comparer.GetHashCode(this.Item2));
//        h.Add(comparer.GetHashCode(this.Item3));
//        h.Add(comparer.GetHashCode(this.Item4));
//        h.Add(comparer.GetHashCode(this.Item5));
//        h.Add(comparer.GetHashCode(this.Item6));
//        h.Add(comparer.GetHashCode(this.Item7));
//        return h.ToHashCode();
//	}
//}
//public class ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>:IEquatable<ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>>,IStructuralEquatable,IStructuralComparable,IComparable,IComparable<ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>>,ITuple where TRest : struct{
//	public T1 Item1;
//    public T2 Item2;
//    public T3 Item3;
//    public T4 Item4;
//    public T5 Item5;
//    public T6 Item6;
//    public T7 Item7;
//    public TRest Rest;

//    public ClassTuple(){
//        this.Item1=default!;
//        this.Item2=default!;
//        this.Item3=default!;
//        this.Item4=default!;
//        this.Item5=default!;
//        this.Item6=default!;
//        this.Item7=default!;
//        this.Rest=default!;
//    }
//    int ITuple.Length{
//		get{
//			if(this.Rest is ITuple valueTupleInternal)return 7 + valueTupleInternal.Length;
//			return 8;
//		}
//	}

//	object? ITuple.this[int index]{
//		get{
//			switch (index){
//			case 0:return this.Item1;
//			case 1:return this.Item2;
//			case 2:return this.Item3;
//			case 3:return this.Item4;
//			case 4:return this.Item5;
//			case 5:return this.Item6;
//			case 6:return this.Item7;
//			default:
//				if (!(this.Rest is ITuple valueTupleInternal)){
//					if (index == 7)return this.Rest;
//					throw new IndexOutOfRangeException();
//				}
//				return valueTupleInternal[index - 7];
//			}
//		}
//	}
//    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5,T6 Item6,T7 Item7,TRest Rest){
//		if(Rest is not ITuple)
//			throw new ArgumentException("RestがITupleであるべき");
//        this.Item1 = Item1;
//        this.Item2 = Item2;
//        this.Item3 = Item3;
//        this.Item4 = Item4;
//        this.Item5 = Item5;
//        this.Item6 = Item6;
//        this.Item7 = Item7;
//        this.Rest = Rest;
//	}
//    public override bool Equals(object? obj){
//        if(ReferenceEquals(null,obj)) return false;
//        if(ReferenceEquals(this,obj)) return true;
//        if(obj.GetType()!=this.GetType()) return false;
//        return this.Equals((ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>)obj);
//    }
//    public bool Equals(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>? other){
//        if(ReferenceEquals(null,other)) return false;
//        if(ReferenceEquals(this,other)) return true;
//        return EqualityComparer<T1>.Default.Equals(this.Item1,other.Item1)&&EqualityComparer<T2>.Default.Equals(this.Item2,other.Item2)&&EqualityComparer<T3>.Default.Equals(this.Item3,other.Item3)&&EqualityComparer<T4>.Default.Equals(this.Item4,other.Item4)&&EqualityComparer<T5>.Default.Equals(this.Item5,other.Item5)&&EqualityComparer<T6>.Default.Equals(this.Item6,other.Item6)&&EqualityComparer<T7>.Default.Equals(this.Item7,other.Item7)&&this.Rest.Equals(other.Rest);
//    }
//    bool IStructuralEquatable.Equals(object? other,IEqualityComparer comparer)
//	{
//		if (other == null || !(other is ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> valueTuple))
//		{
//			return false;
//		}
//		if (comparer.Equals(this.Item1,valueTuple.Item1) && comparer.Equals(this.Item2,valueTuple.Item2) && comparer.Equals(this.Item3,valueTuple.Item3) && comparer.Equals(this.Item4,valueTuple.Item4) && comparer.Equals(this.Item5,valueTuple.Item5) && comparer.Equals(this.Item6,valueTuple.Item6) && comparer.Equals(this.Item7,valueTuple.Item7))
//		{
//			return comparer.Equals(this.Rest,valueTuple.Rest);
//		}
//		return false;
//	}

//	int IComparable.CompareTo(object? other){
//		if (other == null)
//			return 1;
//		if (other is not ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>)
//			throw new ArgumentException(this.GetType().FullName,nameof(other));
//		return this.CompareTo((ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>)other);
//	}
//    public int CompareTo(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> other){
//		int num;
//		if((num=Comparer<T1>.Default.Compare(this.Item1,other.Item1))!= 0)return num;
//        if((num=Comparer<T2>.Default.Compare(this.Item2,other.Item2))!= 0)return num;
//        if((num=Comparer<T3>.Default.Compare(this.Item3,other.Item3))!= 0)return num;
//        if((num=Comparer<T4>.Default.Compare(this.Item4,other.Item4))!= 0)return num;
//        if((num=Comparer<T5>.Default.Compare(this.Item5,other.Item5))!= 0)return num;
//        if((num=Comparer<T6>.Default.Compare(this.Item6,other.Item6))!= 0)return num;
//        if((num=Comparer<T7>.Default.Compare(this.Item7,other.Item7))!= 0)return num;
//		return Comparer<TRest>.Default.Compare(this.Rest,other.Rest);
//	}

//    public static bool operator==(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> second)=>
//        first.Equals(second);
//    public static bool operator!=(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> second)=>
//        !first.Equals(second);
//    public static bool operator<(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> second)=>
//        first.CompareTo(second)<0;
//    public static bool operator<=(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> second)=>
//        first.CompareTo(second)<=0;
//    public static bool operator>(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> second)=>
//        first.CompareTo(second)>0;
//    public static bool operator>=(ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> first,ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> second)=>
//        first.CompareTo(second)>=0;

//    int IStructuralComparable.CompareTo(object? other,IComparer comparer){
//		if(other == null)
//			return 1;
//		if(other is not ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest> valueTuple)
//            throw new ArgumentException(this.GetType().FullName,nameof(other));
//		int num;
//		if((num=comparer.Compare(this.Item1,valueTuple.Item1))!= 0)return num;
//        if((num=comparer.Compare(this.Item2,valueTuple.Item2))!= 0)return num;
//        if((num=comparer.Compare(this.Item3,valueTuple.Item3))!= 0)return num;
//        if((num=comparer.Compare(this.Item4,valueTuple.Item4))!= 0)return num;
//        if((num=comparer.Compare(this.Item5,valueTuple.Item5))!= 0)return num;
//        if((num=comparer.Compare(this.Item6,valueTuple.Item6))!= 0)return num;
//        if((num=comparer.Compare(this.Item7,valueTuple.Item7))!= 0)return num;
//		return comparer.Compare(this.Rest,valueTuple.Rest);
//	}
//    public override int GetHashCode(){
//        return HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5,this.Item6,this.Item7,this.Rest);
//    }
//    public int GetHashCode(IEqualityComparer comparer)=>this.GetHashCode();
//}
public class ClassTuple<T1>:ITuple{
    public ClassTuple(){}
	public T1 Item1;
    int ITuple.Length=>1;
    object? ITuple.this[int index]=>index switch{
        0=>this.Item1,
        _=>throw new IndexOutOfRangeException()
    };
    public ClassTuple(T1 Item1){
        this.Item1 = Item1;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1);
    public override string ToString()=>"("+this.Item1+")";
}
public class ClassTuple<T1,T2>:ITuple{
    public ClassTuple(){}
	public T1 Item1;
    public T2 Item2;
    int ITuple.Length=>2;
    object? ITuple.this[int index]=>index switch{
        0=>this.Item1,
        1=>this.Item2,
        _=>throw new IndexOutOfRangeException()
    };
    public ClassTuple(T1 Item1,T2 Item2){
        this.Item1 = Item1;
        this.Item2 = Item2;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1,this.Item2);
    public override string ToString()=>"("+this.Item1+", "+this.Item2+")";
}
public class ClassTuple<T1,T2,T3>:ITuple{
    public ClassTuple(){}
	public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    int ITuple.Length=>3;
    object? ITuple.this[int index]=>index switch{
        0=>this.Item1,
        1=>this.Item2,
        2=>this.Item3,
        _=>throw new IndexOutOfRangeException()
    };
    public ClassTuple(T1 Item1,T2 Item2,T3 Item3){
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1,this.Item2,this.Item3);
    public override string ToString()=>"(" +this.Item1+", " +this.Item2+", " +this.Item3+")";
}
public class ClassTuple<T1,T2,T3,T4>:ITuple{
    public ClassTuple(){}
	public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    int ITuple.Length=>4;
    object? ITuple.this[int index]=>index switch{
        0=>this.Item1,
        1=>this.Item2,
        2=>this.Item3,
        3=>this.Item4,
        _=>throw new IndexOutOfRangeException()
    };
    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4){
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
        this.Item4 = Item4;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4);
    public override string ToString()=>"(" +this.Item1+", " +this.Item2+", " +this.Item3+", " +this.Item4+", " +")";
}

public class ClassTuple<T1,T2,T3,T4,T5>:ITuple{
    public ClassTuple(){}
	public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;
    int ITuple.Length=>5;
    object? ITuple.this[int index]=>index switch{
        0=>this.Item1,
        1=>this.Item2,
        2=>this.Item3,
        3=>this.Item4,
        4=>this.Item5,
        _=>throw new IndexOutOfRangeException()
    };
    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5){
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
        this.Item4 = Item4;
        this.Item5 = Item5;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5);
    public override string ToString()=>"(" +this.Item1+", " +this.Item2+", " +this.Item3+", " +this.Item4+", " +this.Item5+")";
}
public class ClassTuple<T1,T2,T3,T4,T5,T6>:ITuple{
    public ClassTuple(){}
	public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;
    public T6 Item6;
    int ITuple.Length=>6;
    object? ITuple.this[int index]=>index switch{
        0=>this.Item1,
        1=>this.Item2,
        2=>this.Item3,
        3=>this.Item4,
        4=>this.Item5,
        5=>this.Item6,
        _=>throw new IndexOutOfRangeException()
    };
    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5,T6 Item6){
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
        this.Item4 = Item4;
        this.Item5 = Item5;
        this.Item6 = Item6;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5,this.Item6);
    public override string ToString()=>"(" +this.Item1+", " +this.Item2+", " +this.Item3+", " +this.Item4+", " +this.Item5+", " +this.Item6+")";
}
public class ClassTuple<T1,T2,T3,T4,T5,T6,T7>:ITuple{
    public ClassTuple(){}
	public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;
    public T6 Item6;
    public T7 Item7;
    int ITuple.Length=>7;
    object? ITuple.this[int index]=>index switch{
        0=>this.Item1,
        1=>this.Item2,
        2=>this.Item3,
        3=>this.Item4,
        4=>this.Item5,
        5=>this.Item6,
        6=>this.Item7,
        _=>throw new IndexOutOfRangeException()
    };
    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5,T6 Item6,T7 Item7){
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
        this.Item4 = Item4;
        this.Item5 = Item5;
        this.Item6 = Item6;
        this.Item7 = Item7;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5,this.Item6,this.Item7,this);
    public override string ToString()=>"(" +this.Item1+", " +this.Item2+", " +this.Item3+", " +this.Item4+", " +this.Item5+", " +this.Item6+", " +this.Item7+")";
}
public class ClassTuple<T1,T2,T3,T4,T5,T6,T7,TRest>:ITuple where TRest : struct{
    public ClassTuple(){}
	public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;
    public T6 Item6;
    public T7 Item7;
    public TRest Rest;
    int ITuple.Length{
		get{
			if(this.Rest is ITuple ValueTuple)return 7+ValueTuple.Length;
			return 8;
		}
	}
    object? ITuple.this[int index]{
		get{
			switch (index){
			case 0:return this.Item1;
			case 1:return this.Item2;
			case 2:return this.Item3;
			case 3:return this.Item4;
			case 4:return this.Item5;
			case 5:return this.Item6;
			case 6:return this.Item7;
			default:
                if (this.Rest is ITuple ValueTuple) return ValueTuple[index - 7];
                if (index == 7)return this.Rest;
                throw new IndexOutOfRangeException();
            }
		}
	}
    public ClassTuple(T1 Item1,T2 Item2,T3 Item3,T4 Item4,T5 Item5,T6 Item6,T7 Item7,TRest Rest){
		if(Rest is not ITuple)
			throw new ArgumentException("RestがITupleであるべき");
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
        this.Item4 = Item4;
        this.Item5 = Item5;
        this.Item6 = Item6;
        this.Item7 = Item7;
        this.Rest = Rest;
	}
    public override int GetHashCode()=>HashCode.Combine(this.Item1,this.Item2,this.Item3,this.Item4,this.Item5,this.Item6,this.Item7,this.Rest);

    public override string ToString(){
        var s0="(" +this.Item1+", " +this.Item2+", " +this.Item3+", " +this.Item4+", " +this.Item5+", " +this.Item6+", " +this.Item7+")";
        if(this.Rest is not ITuple ValueTuple) return s0;
        var s1 = ValueTuple.ToString()!;
        return s0[..^1] +','+ s1[1..];
    }
}
