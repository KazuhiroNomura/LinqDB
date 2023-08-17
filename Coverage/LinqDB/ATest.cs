using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using CoverageCS.LinqDB.Sets;
using LinqDB.Databases;
using LinqDB.Optimizers;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#pragma warning disable 659
namespace CoverageCS.LinqDB;

[Serializable]
public abstract class ATest
{
    protected static readonly short Int16 = 1;
    protected static readonly int Int32 = 1;
    protected static readonly long Int64 = 1;
    protected static readonly uint UInt32 = 1;
    protected static readonly ulong UInt64 = 1;
    protected static readonly string String = "A";
    protected static readonly uint UInt32_1 = 1;
    protected static readonly int Int32_1 = 1, Int32_2 = 2;
    protected static readonly long Int64_1 = 1;
    protected static readonly decimal Decimal_1 = 1, Decimal_2 = 2;
    protected static readonly bool Boolean1 = true, Boolean2 = false;
    protected static readonly object Object_String = "A";
    protected static readonly object Object_Int32 = 1;
    protected static readonly string[] Array = { "A", "B" };


    protected static readonly int _Int32 = 2;
    protected static readonly bool _Boolean = true;
    public static readonly string _String = "S";
    //protected  static readonly Object _ObjectString="S";
    //protected  static readonly Object _ObjectInt32=1;
    //protected  static readonly Int32[]_Array=new Int32[1];
    protected static readonly int? _NullableInt32 = 0;
    protected static readonly List<int> _List = new() { 1, 2 };
    protected static readonly Func<int, int> _Delegate = p => p;
    protected static int Function() => 1;
    protected enum EEnum { A, B, C }
    //protected static readonly Int32[] Arr変数=new Int32[1];
    protected static readonly ParameterExpression i0 = Expression.Parameter(typeof(int));
    protected static readonly ParameterExpression i1 = Expression.Parameter(typeof(int));
    protected static readonly ParameterExpression d0 = Expression.Parameter(typeof(double));
    protected static readonly ParameterExpression d1 = Expression.Parameter(typeof(double));
    //        protected static T[] Arr0<T>() => System.Array.Empty<T>();
    //      protected static T[] Arr1<T>() => new T[1];
    //    protected static T[] ArrN<T>() => ArrN<T>(10);
    protected static T[] ArrN<T>(int size)
    {
        dynamic Array = new T[size];
        for (var a = 0; a < size; a++)
        {
            Array[a] = (T)(dynamic)a;
        }
        return (T[])Array;
    }
    protected static T?[] ArrNullable<T>(int size) where T : struct
    {
        dynamic Array = new T?[size];
        for (var a = 0; a < size; a++)
        {
            if (a % 2 == 0)
            {
                Array[a] = (T?)(dynamic)a;
            }
            else
            {
                Array[a] = (T?)null;
            }
        }
        return (T?[])Array;
    }
    //protected static IEnumerable<T> Enu0<T>() => System.Array.Empty<T>();
    //  protected static IEnumerable<T> Enu1<T>() => Arr1<T>();
    //    protected static IEnumerable<T> EnuN<T>() => ArrN<T>();
    protected static IEnumerable<T> EnuN<T>(int size) => ArrN<T>(size);
    protected static IEnumerable<T?> EnuNullable<T>(int size) where T : struct => ArrNullable<T>(size);
    protected static Set<Tables.Entity, PrimaryKeys.Entity, Container> KeySet変数() => new(default!);
    //        protected static Set<T> Set0<T>()=>new Set<T>();
    //      protected static Set<T> Set1<T>()=>new Set<T> { (dynamic)1 };
    //protected static Set<T> SetN<T>()=>new Set<T>(ArrN<T>());
    protected static Set<T> SetN<T>(int size) => new(ArrN<T>(size));
    protected static Set<T?> SetNullable<T>(int size) where T : struct => new(ArrNullable<T>(size));
    protected static Func<T1, TResult> Func<T1, TResult>(Func<T1, TResult> input) => input;
    protected static bool Booleanを返すプロパティ => true;
    protected static bool Booleanを返すメソッド() => true;
    protected static int Staticプロパティ { get; set; }
    protected int InstanceInt32プロパティ { get; set; }
    protected string InstanceStringプロパティ { get; set; }
    [Serializable]
    public struct StructCollection : ICollection<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public void Add(int item)
        {
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(int[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }
        public int Count => 0;
        public bool IsReadOnly => true;
    }
    public class class_演算子オーバーロード2
    {
        // ReSharper disable once CollectionNeverQueried.Global
        public StructCollection StructCollectionフィールド = new();
    }
    public struct struct_演算子オーバーロード2
    {
        // ReSharper disable once CollectionNeverQueried.Global
        // ReSharper disable once UnassignedField.Global
        public StructCollection StructCollectionフィールド;
    }
    [DebuggerDisplay("{内部の値+\"+,\"+内部のBoolean}")]

    public class class_演算子オーバーロード : IEquatable<class_演算子オーバーロード>
    {
        public class_演算子オーバーロード() { }
        private readonly int 内部の値;
        public class_演算子オーバーロード(int 内部の値)
        {
            this.内部の値 = 内部の値;
        }
        private readonly bool 内部のBoolean;
        public class_演算子オーバーロード(int 内部の値, bool 内部のBoolean)
        {
            this.内部の値 = 内部の値;
            this.内部のBoolean = 内部のBoolean;
        }
        //[global::Lite.Optimizers.NoOptimize]
        public int _最適化されないメンバー = 4;
        public int Int32フィールド = 4;
        public int 最適化されないメソッド() => 4;
        [Pure]
        public int メソッド() => 4;
        public int Int32プロパティ {
            get;
            set;
        }
        // ReSharper disable once UnassignedField.Global
        public static int StaticInt32フィールド;
        public static string StaticStringフィールド = "B";
        public string Stringフィールド = "A";
        public string Stringプロパティ { get; set; }
        // ReSharper disable once CollectionNeverQueried.Global
        public StructCollection StructCollectionフィールド = new();
        // ReSharper disable once CollectionNeverQueried.Global
        public List<int> Listフィールド = new();

        // ReSharper disable once CollectionNeverQueried.Global
        public List<int> Listプロパティ{get;set;}=new();

        // ReSharper disable once CollectionNeverQueried.Global
        public HashSet<int> HashSetプロパティ{get;set;}=new();

        public class_演算子オーバーロード2 class演算子オーバーロード2プロパティ{get;set;}=new();
        public readonly class_演算子オーバーロード2 class_演算子オーバーロード2フィールド = new();
        public struct_演算子オーバーロード2 Struct演算子オーバーロード2 = new();
        public static class_演算子オーバーロード operator ~(class_演算子オーバーロード a) => new(~a.内部の値, a.内部のBoolean);
        public static class_演算子オーバーロード operator !(class_演算子オーバーロード a) => new(-1 ^ a.内部の値, !a.内部のBoolean);
        public static bool operator false(class_演算子オーバーロード a) => !a.内部のBoolean;
        public static bool operator true(class_演算子オーバーロード a) => a is{内部のBoolean: true};
        public static class_演算子オーバーロード operator ++(class_演算子オーバーロード a) => new(a.内部の値 + 1);
        public static class_演算子オーバーロード operator --(class_演算子オーバーロード a) => new(a.内部の値 - 1);
        public static class_演算子オーバーロード operator -(class_演算子オーバーロード a) => new(-a.内部の値, a.内部のBoolean);
        public static class_演算子オーバーロード operator +(class_演算子オーバーロード a) => new(a.内部の値, a.内部のBoolean);
        public static class_演算子オーバーロード operator -(class_演算子オーバーロード a, class_演算子オーバーロード b) => new(-a.内部の値);
        public static class_演算子オーバーロード operator &(class_演算子オーバーロード a, class_演算子オーバーロード b)
        {
            return new class_演算子オーバーロード(a.内部の値 & b.内部の値);
        }
        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public static class_演算子オーバーロード operator |(class_演算子オーバーロード? a, class_演算子オーバーロード? b)
        {
            if (a == null) return b!;
            if (b == null) return a;
            return new class_演算子オーバーロード(a.内部の値 | b.内部の値);
        }
        public static explicit operator int(class_演算子オーバーロード a) => 1;
        public static explicit operator decimal(class_演算子オーバーロード a) => 1;
        public static explicit operator string(class_演算子オーバーロード a) => "class_演算子オーバーロード";
        public static explicit operator class_演算子オーバーロード(decimal a) => new();
        //public static explicit operator Object(class_演算子オーバーロード _Field)=>new Object();
        public static decimal class_演算子オーバーロードからDecimalにキャスト(class_演算子オーバーロード a) => 1m;

        public bool Equals(class_演算子オーバーロード? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return this.内部の値 == other.内部の値 && this.内部のBoolean == other.内部のBoolean;
        }
        public override bool Equals(object? obj) => obj is not null&&this.Equals((class_演算子オーバーロード)obj);
        public override int GetHashCode() => this.内部の値;
    }

    protected static readonly class_演算子オーバーロード[] _class_演算子オーバーロードArray ={
        new()
    };

    protected static readonly struct_演算子オーバーロード[] _struct_演算子オーバーロードArray = new struct_演算子オーバーロード[1];
    //リフレクションで書き換える
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    protected static class_演算子オーバーロード _Static_class_演算子オーバーロード1 = new();
    protected readonly class_演算子オーバーロード _Instance_class_演算子オーバーロード1 = new();
    protected static readonly class_演算子オーバーロード _class_演算子オーバーロード2 = new();
    public struct struct_演算子オーバーロード : IEquatable<struct_演算子オーバーロード>
    {
        public int Int32フィールド;
        public bool Booleanフィールド;
        public string Stringフィールド;
        public int Int32プロパティ {
            get;
            set;
        }
        public struct_演算子オーバーロード(int Int32フィールド, bool Booleanフィールド)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = Booleanフィールド;
            this.Stringフィールド = "";
            this.Int32プロパティ = 0;
        }
        private struct_演算子オーバーロード(int Int32フィールド, bool Booleanフィールド, string Stringフィールド, int Int32プロパティ)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = Booleanフィールド;
            this.Stringフィールド = Stringフィールド;
            this.Int32プロパティ = Int32プロパティ;
        }
        public static bool operator true(struct_演算子オーバーロード a) => a.Booleanフィールド;
        public static bool operator false(struct_演算子オーバーロード a) => !a.Booleanフィールド;
        public static struct_演算子オーバーロード operator &(struct_演算子オーバーロード a, struct_演算子オーバーロード b) => new(a.Int32フィールド & b.Int32フィールド, a.Booleanフィールド & b.Booleanフィールド, a.Stringフィールド + b.Stringフィールド, a.Int32プロパティ & b.Int32プロパティ);
        public static struct_演算子オーバーロード operator |(struct_演算子オーバーロード a, struct_演算子オーバーロード b) => new(a.Int32フィールド | b.Int32フィールド, a.Booleanフィールド | b.Booleanフィールド, a.Stringフィールド, a.Int32プロパティ | b.Int32プロパティ);
        //          public static explicit operator Int32(struct_演算子オーバーロード a)=>1;
        public static explicit operator struct_演算子オーバーロード(int a) => new();
        //      public static Decimal struct_演算子オーバーロードからDecimalにキャスト(struct_演算子オーバーロード a)=>1m;
        public static struct_演算子オーバーロード operator ~(struct_演算子オーバーロード a) => new(~a.Int32フィールド, a.Booleanフィールド);
        public static struct_演算子オーバーロード operator !(struct_演算子オーバーロード a) => new(-1 ^ a.Int32フィールド, !a.Booleanフィールド);
        public static struct_演算子オーバーロード operator ++(struct_演算子オーバーロード a) => new(a.Int32フィールド + 1, a.Booleanフィールド);
        public static struct_演算子オーバーロード operator --(struct_演算子オーバーロード a) => new(a.Int32フィールド - 1, a.Booleanフィールド);
        public static struct_演算子オーバーロード operator -(struct_演算子オーバーロード a) => new(-a.Int32フィールド, a.Booleanフィールド);
        public static struct_演算子オーバーロード operator +(struct_演算子オーバーロード a) => new(a.Int32フィールド + 2, a.Booleanフィールド);
        public static struct_演算子オーバーロード operator -(struct_演算子オーバーロード a, struct_演算子オーバーロード b) => new(-a.Int32フィールド - b.Int32フィールド, a.Booleanフィールド);
        public static explicit operator int(struct_演算子オーバーロード a) => 1;
        public bool Equals(struct_演算子オーバーロード other) => this.Int32フィールド == other.Int32フィールド;
    }
#pragma warning disable IDE0009 // Member access should be qualified.
    [DebuggerDisplay("{this." + nameof(結果) + "}")]
#pragma warning restore IDE0009 // Member access should be qualified.
    public struct struct_ショートカット検証 : IEquatable<struct_ショートカット検証>
    {
        private readonly int Int32フィールド;
        private readonly bool Booleanフィールド;
        private readonly string 結果;
        public struct_ショートカット検証(int Int32フィールド)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = false;
            this.結果 = Int32フィールド.ToString();
        }
        public struct_ショートカット検証(bool Booleanフィールド)
        {
            this.Int32フィールド = 0;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = Booleanフィールド.ToString();
        }
        public struct_ショートカット検証(int Int32フィールド, bool Booleanフィールド)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = Booleanフィールド + ":" + Int32フィールド;
        }
        private struct_ショートカット検証(bool Booleanフィールド, string 結果)
        {
            this.Int32フィールド = 0;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = 結果;
        }
        private struct_ショートカット検証(int Int32フィールド, string 結果)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = false;
            this.結果 = 結果;
        }
        private struct_ショートカット検証(int Int32フィールド, bool Booleanフィールド, string 結果)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = 結果;
        }
        public static bool operator true(struct_ショートカット検証 a) => a.Booleanフィールド;
        public static bool operator false(struct_ショートカット検証 a) => !a.Booleanフィールド;

        public static struct_ショートカット検証 operator &(struct_ショートカット検証 a, struct_ショートカット検証 b)
        {
            var Int32フィールド = a.Int32フィールド & b.Int32フィールド;
            return new struct_ショートカット検証(Int32フィールド, a.Booleanフィールド & b.Booleanフィールド, "(" + a.結果 + "&" + b.結果 + ":" + Int32フィールド + ")");
        }

        public static struct_ショートカット検証 operator |(struct_ショートカット検証 a, struct_ショートカット検証 b)
        {
            var Int32フィールド = a.Int32フィールド | b.Int32フィールド;
            return new struct_ショートカット検証(Int32フィールド, a.Booleanフィールド | b.Booleanフィールド, "(" + a.結果 + "|" + b.結果 + ":" + Int32フィールド + ")");
        }

        public static struct_ショートカット検証 operator ^(struct_ショートカット検証 a, struct_ショートカット検証 b) => new(a.Booleanフィールド ^ b.Booleanフィールド, "(" + a.結果 + "^" + b.結果 + ")");
        public static struct_ショートカット検証 operator -(struct_ショートカット検証 a) => new(-a.Int32フィールド, "-(" + a.結果 + ")");
        public static struct_ショートカット検証 operator +(struct_ショートカット検証 a) => new(a.Int32フィールド, "+(" + a.結果 + ")");
        public static struct_ショートカット検証 operator +(struct_ショートカット検証 a, struct_ショートカット検証 b) => new(a.Int32フィールド + b.Int32フィールド, "(" + a.結果 + "+" + b.結果 + ")");
        public static struct_ショートカット検証 operator ~(struct_ショートカット検証 a) => new(~a.Int32フィールド, "~(" + a.結果 + ")");
        public static struct_ショートカット検証 operator !(struct_ショートカット検証 a) => new(!a.Booleanフィールド, "!(" + a.結果 + ")");
        public bool Equals(struct_ショートカット検証 other) => this.Int32フィールド == other.Int32フィールド;
        public static implicit operator struct_ショートカット検証(bool a) => new(1, a);
        public override bool Equals(object? obj)=>
            obj is struct_ショートカット検証 other&&
            (this.Booleanフィールド==other.Booleanフィールド&&this.Int32フィールド==other.Int32フィールド);
        public override string ToString() => this.結果;
    }
    [DebuggerDisplay("{Int32フィールド.ToString()+\":\"+Booleanフィールド.ToString()}")]
    public class class_ショートカット検証 : IEquatable<class_ショートカット検証>
    {
        private readonly int Int32フィールド;
        private readonly bool Booleanフィールド;
        private readonly string 結果;
        public class_ショートカット検証(int Int32フィールド)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = false;
            this.結果 = Int32フィールド.ToString();
        }
        public class_ショートカット検証(bool Booleanフィールド)
        {
            this.Int32フィールド = 0;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = Booleanフィールド.ToString();
        }
        public class_ショートカット検証(int Int32フィールド, bool Booleanフィールド)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = Booleanフィールド + ":" + Int32フィールド;
        }
        private class_ショートカット検証(bool Booleanフィールド, string 結果)
        {
            this.Int32フィールド = 0;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = 結果;
        }
        private class_ショートカット検証(int Int32フィールド, string 結果)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = false;
            this.結果 = 結果;
        }
        private class_ショートカット検証(int Int32フィールド, bool Booleanフィールド, string 結果)
        {
            this.Int32フィールド = Int32フィールド;
            this.Booleanフィールド = Booleanフィールド;
            this.結果 = 結果;
        }
        public static bool operator true(class_ショートカット検証 a) => a.Booleanフィールド;
        public static bool operator false(class_ショートカット検証 a) => !a.Booleanフィールド;

        public static class_ショートカット検証 operator &(class_ショートカット検証 a, class_ショートカット検証 b)
        {
            var Int32フィールド = a.Int32フィールド & b.Int32フィールド;
            return new class_ショートカット検証(Int32フィールド, a.Booleanフィールド & b.Booleanフィールド, "(" + a.結果 + "&" + b.結果 + ":" + Int32フィールド + ")");
        }

        public static class_ショートカット検証 operator |(class_ショートカット検証 a, class_ショートカット検証 b)
        {
            var Int32フィールド = a.Int32フィールド | b.Int32フィールド;
            return new class_ショートカット検証(Int32フィールド, a.Booleanフィールド | b.Booleanフィールド, "(" + a.結果 + "|" + b.結果 + ":" + Int32フィールド + ")");
        }

        public static class_ショートカット検証 operator ^(class_ショートカット検証 a, class_ショートカット検証 b) => new(a.Booleanフィールド ^ b.Booleanフィールド, "(" + a.結果 + "^" + b.結果 + ")");
        public static class_ショートカット検証 operator -(class_ショートカット検証 a) => new(-a.Int32フィールド, "-(" + a.結果 + ")");
        public static class_ショートカット検証 operator +(class_ショートカット検証 a) => new(a.Int32フィールド, "+(" + a.結果 + ")");
        public static class_ショートカット検証 operator +(class_ショートカット検証 a, class_ショートカット検証 b) => new(a.Int32フィールド + b.Int32フィールド, "(" + a.結果 + "+" + b.結果 + ")");
        public static class_ショートカット検証 operator ~(class_ショートカット検証 a) => new(~a.Int32フィールド, "~(" + a.結果 + ")");
        public static class_ショートカット検証 operator !(class_ショートカット検証 a) => new(!a.Booleanフィールド, "!(" + a.結果 + ")");
        public static implicit operator class_ショートカット検証(bool a) => new(1, a);
        public override bool Equals(object? obj)=>
            obj is class_ショートカット検証 other&&
            (this.Booleanフィールド==other.Booleanフィールド&&this.Int32フィールド==other.Int32フィールド);
        public bool Equals(class_ショートカット検証? other)=>
            other!=null&&(ReferenceEquals(this,other)||
                          this.Int32フィールド==other.Int32フィールド&&this.Booleanフィールド==other.Booleanフィールド);
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Int32フィールド * 397) ^ this.Booleanフィールド.GetHashCode();
            }
        }
    }
    public static readonly struct_演算子オーバーロード _Static_struct_演算子オーバーロード1 = new();
    protected readonly struct_演算子オーバーロード _Instance_struct_演算子オーバーロード1 = new();
    protected static struct_演算子オーバーロード Static_struct_演算子オーバーロード1 => new();
    protected struct_演算子オーバーロード Instance_struct_演算子オーバーロード => new();
    protected int _InstanceInt32;
    protected string _InstanceString = "A";
    protected static int _StaticInt32 = 0;
    protected static string _StaticString = "A";
    //protected const OptimizeLevels ループ融合 = OptimizeLevels.デバッグ ^ OptimizeLevels.プロファイル;
    //protected const OptimizeLevels ループ独立 = OptimizeLevels.デバッグ ^ OptimizeLevels.ループ融合 ^ OptimizeLevels.プロファイル;
    [NonSerialized]
    protected readonly Optimizer Optimizer = new() { Context=typeof(ATest),AssemblyFileName="デバッグ.dll"};
    //protected const OptimizeLevel OptimizeLevel.None = OptimizeLevel.None;
    //protected const OptimizeLevel ループ融合 = OptimizeLevel.ループ融合;
    //protected const OptimizeLevel ローカルスコープ内の先行評価 = OptimizeLevel.ローカルスコープ内の先行評価;
    //protected const OptimizeLevel グローバルスコープ内の先行評価= OptimizeLevel.グローバルスコープ内の先行評価;
    //protected const OptimizeLevel グローバルスコープ内の先行評価しない = OptimizeLevel.Anonymousをnewしてメンバーを参照している式の省略;
    protected static readonly EnumerableSetEqualityComparer Comparer = new();
    //private static readonly EnumerableSetEqualityComparer Comparer=new();
    private const int N = 8;
    protected void Execute標準ラムダループ<TResult>(Expression<Func<TResult>> Lambda) {
        var 標準 = Lambda.Compile();
        var Optimizer = this.Optimizer;
        Optimizer.IsInline=false;
        var ラムダ = Optimizer.CreateDelegate(Lambda);
        Optimizer.IsInline=true;
        var ループ = Optimizer.CreateDelegate(Lambda);
        {
            var expected = 標準()!;
            var actualラムダ = ラムダ()!;
            var actualループ = ループ()!;
            Assert.IsTrue(Comparer.Equals(expected,actualラムダ));
            Assert.IsTrue(Comparer.Equals(expected,actualループ));
        }
    }
    protected void Execute引数パターン<TResult>(Expression<Func<int,TResult>> Lambda) {
        var 標準 = Lambda.Compile();
        var Optimizer = this.Optimizer;
        Optimizer.IsInline=false;
        var ラムダ = Optimizer.CreateDelegate(Lambda);
        Optimizer.IsInline=true;
        var ループ = Optimizer.CreateDelegate(Lambda);
        for(var a = 0;a<N;a++) {
            var expected = 標準(a)!;
            var actualラムダ = ラムダ(a)!;
            var actualループ = ループ(a)!;
            Assert.IsTrue(Comparer.Equals(expected,actualラムダ));
            Assert.IsTrue(Comparer.Equals(expected,actualループ));
        }
    }
    protected void Execute引数パターン標準ラムダループ<TResult>(Expression<Func<int,int,TResult>> Lambda) {
        var 標準 = Lambda.Compile();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=true;
        Optimizer.IsInline=false;
        var ラムダ = Optimizer.CreateDelegate(Lambda);
        Optimizer.IsInline=true;
        var ループ = Optimizer.CreateDelegate(Lambda);
        for(var a = 0;a<N;a++) {
            for(var b = 0;b<N;b++) {
                var expected = 標準(a,b);
                { 
                    var actual = ラムダ(a, b);
                    Assert.IsTrue(Comparer.Equals(expected!, actual!));
                }
                { 
                    var actual = ループ(a, b);
                    Assert.IsTrue(Comparer.Equals(expected!, actual!));
                }
            }
        }
    }
    protected void Execute引数パターン標準ラムダループアセンブリ出力しない<TResult>(Expression<Func<int,int,TResult>> Lambda) {
        var 標準 = Lambda.Compile();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=false;
        Optimizer.IsInline=false;
        var ラムダ = Optimizer.CreateDelegate(Lambda);
        Optimizer.IsInline=true;
        var ループ = Optimizer.CreateDelegate(Lambda);
        for(var a = 0;a<N;a++) {
            for(var b = 0;b<N;b++) {
                var expected = 標準(a,b);
                { 
                    var actual = ラムダ(a, b);
                    Assert.IsTrue(Comparer.Equals(expected!, actual!));
                }
                { 
                    var actual = ループ(a, b);
                    Assert.IsTrue(Comparer.Equals(expected!, actual!));
                }
            }
        }
    }
    protected void Execute引数パターン標準ラムダループ<TResult>(Expression<Func<int, int, int, TResult>> Lambda) {
        var 標準 = Lambda.Compile();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=true;
        Optimizer.IsInline=false;
        var ラムダ = Optimizer.CreateDelegate(Lambda);
        Optimizer.IsInline=true;
        var ループ = Optimizer.CreateDelegate(Lambda);
        for (var a = 0; a < N; a++) {
            for (var b = 0; b < N; b++) {
                for (var c = 0; c < N; c++) {
                    var expected = 標準(a, b, c);
                    { 
                        var actual = ラムダ(a, b, c);
                        Assert.IsTrue(Comparer.Equals(expected!, actual!));
                    }
                    { 
                        var actual = ループ(a, b, c);
                        Assert.IsTrue(Comparer.Equals(expected!, actual!));
                    }
                }
            }
        }
    }
    protected void Execute引数パターン標準ラムダループ<TResult>(Expression<Func<int,int,int,int,TResult>> Lambda) {
        const int N = 3;
        var 標準 = Lambda.Compile();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=true;
        Optimizer.IsInline=false;
        var ラムダ = Optimizer.CreateDelegate(Lambda);
        Optimizer.IsInline=true;
        var ループ = Optimizer.CreateDelegate(Lambda);
        for(var a = 0;a<N;a++) {
            for(var b = 0;b<N;b++) {
                for(var c = 0;c<N;c++) {
                    for(var d = 0;d<N;d++) {
                        var expected = 標準(a,b,c,d);
                        { 
                            var actual = ラムダ(a,b,c,d);
                            Assert.IsTrue(Comparer.Equals(expected!,actual!));
                        }
                        { 
                            var actual = ループ(a,b,c,d);
                            Assert.IsTrue(Comparer.Equals(expected!,actual!));
                        }
                    }
                }
            }
        }
    }
    protected void 共通Assert(int expected,Expression<Func<評価検証,評価検証>> Lambda) {
        var Optimizer = this.Optimizer;
        var actual = Optimizer.Execute(Lambda,new 評価検証()).評価回数;
        Assert.IsTrue(Comparer.Equals(expected,actual));
    }
    protected TResult Execute標準ラムダループ<T, TResult>(Expression<Func<T,TResult>> Lambda,T input) {
        var Optimizer = this.Optimizer;
        var M2=Lambda.Compile();
        var R2=M2(input);
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(input);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(input);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(input);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(input);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        return R2;
    }
    protected void Execute標準ラムダループ(Expression<Action> Lambda){
        var M2=Lambda.Compile();
        M2();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                M1();
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                M1();
            }
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                M1();
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                M1();
            }
        }
    }
    protected void Execute標準ラムダループ(Expression<Action> Lambda,Action 初期化,Action Assert){
        //var M2=Lambda.Compile();
        //初期化();
        //M2();
        //Assert();
        var Optimizer=this.Optimizer;
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                初期化();
                M1();
                Assert();
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                初期化();
                M1();
                Assert();
            }
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                初期化();
                M1();
                Assert();
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                初期化();
                M1();
                Assert();
            }
        }
    }
    protected void Execute標準アセンブリ出力しない<TResult>(Expression<Func<TResult>> Lambda) {
        var M2=Lambda.Compile();
        var R2=M2();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            var M1=Optimizer.CreateDelegate(Lambda);
            var R1=M1();
            Assert.IsTrue(Comparer.Equals(R1,R2));
        }
        {
            Optimizer.IsInline=true;
            var M1=Optimizer.CreateDelegate(Lambda);
            var R1=M1();
            Assert.IsTrue(Comparer.Equals(R1,R2));
        }
    }
    protected void Execute2(Expression<Action> Lambda) {
        Lambda.Compile()();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            Optimizer.CreateDelegate(Lambda)();
            Optimizer.IsInline=true;
            Optimizer.CreateDelegate(Lambda)();
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            Optimizer.CreateDelegate(Lambda)();
            Optimizer.IsInline=true;
            Optimizer.CreateDelegate(Lambda)();
        }
    }
    protected TResult Execute2<TResult>(Expression<Func<TResult>> Lambda) {
        var M2=Lambda.Compile();
        var R2=M2();
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=true;
        共通1();
        Optimizer.IsGenerateAssembly=false;
        共通1();
        return R2;
        void 共通1(){
            Optimizer.IsInline=false;
            共通0();
            Optimizer.IsInline=true;
            共通0();
        }
        void 共通0(){
            var M1=Optimizer.CreateDelegate(Lambda);
            var R1=M1();
            Assert.IsTrue(Comparer.Equals(R1,R2));
        }
    }
    protected TResult Execute2<T,TResult>(Expression<Func<T,TResult>> Lambda,T a) {
        var Optimizer = this.Optimizer;
        var M2=Lambda.Compile();
        var R2=M2(a)!;
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        return R2;
    }
    protected TResult Execute2<T0,T1,TResult>(Expression<Func<T0,T1,TResult>> Lambd,T0 a,T1 b){
        dynamic Lambda=Lambd;
        var M2=Lambda.Compile();
        var R2=M2(a,b)!;
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a,b);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a,b);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a,b);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateDelegate(Lambda);
                var R1=M1(a,b);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        return R2;
    }
    protected TResult Execute2<TContainer,TResult>(TContainer Container,Expression<Func<TContainer,TResult>> Lambda)where TContainer:Container {
        var M2=Lambda.Compile();
        var R2=M2(Container)!;
        var Optimizer = this.Optimizer;
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateContainerDelegate(Lambda);
                var R1=M1(Container);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateContainerDelegate(Lambda);
                var R1=M1(Container);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            {
                var M1=Optimizer.CreateContainerDelegate(Lambda);
                var R1=M1(Container);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
            Optimizer.IsInline=true;
            {
                var M1=Optimizer.CreateContainerDelegate(Lambda);
                var R1=M1(Container);
                Assert.IsTrue(Comparer.Equals(R1,R2));
            }
        }
        return R2;
    }
    protected Delegate CreateDelegate(LambdaExpression Lambda) =>
        this.Optimizer.CreateDelegate(Lambda);
    protected Func<TResult> CreateDelegate<TResult>(Expression<Func<TResult>> Lambda) =>
        this.Optimizer.CreateDelegate(Lambda);
    protected Func<T0, TResult> CreateDelegate<T0, TResult>(Expression<Func<T0, TResult>> Lambda) =>
        this.Optimizer.CreateDelegate(Lambda);
    protected Func<T0, T1, TResult> CreateDelegate<T0, T1, TResult>(Expression<Func<T0, T1, TResult>> Lambda) =>
        this.Optimizer.CreateDelegate(Lambda);
    [TestInitialize]
    public virtual void MyTestInitialize()
    {
        // this.変数Cache.Clear();
    }
    //各テストを実行した後にコードを実行するには、TestCleanup を使用
    [TestCleanup]
    public virtual void MyTestCleanup()
    {
        //GC.Collect();
    }
    protected void Equal<TInput, TResult>(TInput input, Expression<Func<TInput, TResult>> Lambda){
        var s=new StackFrame(1);
        var Name=s.GetMethod()!.Name;
        var actualStopwatch = Stopwatch.StartNew();
        var expectedDelegate=Lambda.Compile();
        using var Optimizer = new Optimizer();
        Optimizer.IsGenerateAssembly=false;
        {
            Optimizer.IsInline=false;
            {
                var Delegate=Optimizer.CreateDelegate(Lambda);
                var actual=Delegate(input);
                var actual秒=actualStopwatch.ElapsedMilliseconds;
                var expectedStopwatch=Stopwatch.StartNew();
                var expected=expectedDelegate(input);
                var expected秒=expectedStopwatch.ElapsedMilliseconds;
                Assert.AreEqual(expected!,actual,$"ラムダ{Name},{actual秒}ミリ秒,{expected秒}ミリ秒,{(double)expected秒/actual秒}倍");
            }
            Optimizer.IsInline=true;
            {
                var Delegate=Optimizer.CreateDelegate(Lambda);
                var actual=Delegate(input);
                var actual秒=actualStopwatch.ElapsedMilliseconds;
                var expectedStopwatch=Stopwatch.StartNew();
                var expected=expectedDelegate(input);
                var expected秒=expectedStopwatch.ElapsedMilliseconds;
                Assert.AreEqual(expected!,actual,$"ループ{Name},{actual秒}ミリ秒,{expected秒}ミリ秒,{(double)expected秒/actual秒}倍");
            }
        }
        Optimizer.IsGenerateAssembly=true;
        {
            Optimizer.IsInline=false;
            {
                var Delegate=Optimizer.CreateDelegate(Lambda);
                var actual=Delegate(input);
                var actual秒=actualStopwatch.ElapsedMilliseconds;
                var expectedStopwatch=Stopwatch.StartNew();
                var expected=expectedDelegate(input);
                var expected秒=expectedStopwatch.ElapsedMilliseconds;
                Assert.AreEqual(expected!,actual,$"ラムダ{Name},{actual秒}ミリ秒,{expected秒}ミリ秒,{(double)expected秒/actual秒}倍");
            }
            Optimizer.IsInline=true;
            {
                var Delegate=Optimizer.CreateDelegate(Lambda);
                var actual=Delegate(input);
                var actual秒=actualStopwatch.ElapsedMilliseconds;
                var expectedStopwatch=Stopwatch.StartNew();
                var expected=expectedDelegate(input);
                var expected秒=expectedStopwatch.ElapsedMilliseconds;
                Assert.AreEqual(expected!,actual,$"ループ{Name},{actual秒}ミリ秒,{expected秒}ミリ秒,{(double)expected秒/actual秒}倍");
            }
        }
    }
    protected static TResult Lambda<TInput, TResult>(Func<TInput, TResult> e){
        return e(default!);
    }
    public static TResult Lambda<TResult>(Func<int, TResult> e)
    {
        return e(0);
    }
    protected static TResult Lambda<TResult>(Func<TResult> e)
    {
        return e();
    }
    protected class 普通ではサポートされないな演算子 : IEquatable<普通ではサポートされないな演算子>
    {
        public int メンバー = 4;
        public static bool operator true(普通ではサポートされないな演算子 a) => true;
        public static bool operator false(普通ではサポートされないな演算子 a) => false;
        public static 普通ではサポートされないな演算子 operator ++(普通ではサポートされないな演算子 a) => a;
        public static 普通ではサポートされないな演算子 operator --(普通ではサポートされないな演算子 a) => a;
        public static 普通ではサポートされないな演算子 operator +(普通ではサポートされないな演算子 a) => a;
        public static 普通ではサポートされないな演算子 operator -(普通ではサポートされないな演算子 a, 普通ではサポートされないな演算子 b) => a;
        public bool Equals(普通ではサポートされないな演算子? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return this.メンバー == other.メンバー;
        }
        public override bool Equals(object? obj)
        {
            var other = obj as 普通ではサポートされないな演算子;
            return obj != null && this.Equals(other);
        }
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        public override int GetHashCode() => this.メンバー;
    }
    protected static readonly 普通ではサポートされないな演算子 変数_普通ではサポートされないな演算子 = new();
}