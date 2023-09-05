using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using LinqDB.Optimizers;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Serializers.MessagePack.Formatters;
using Binder = Microsoft.CSharp.RuntimeBinder;
using Expression = System.Linq.Expressions.Expression;
using LinqDB.Remote.Servers;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace Optimizers;


public class ExpressionEqualityComparer:共通{
    [Serializable]
    public struct StructCollection:ICollection<int>{
        public IEnumerator<int> GetEnumerator(){
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator(){
            return this.GetEnumerator();
        }
        public void Add(int item){
        }
        public void Clear(){
            throw new NotImplementedException();
        }
        public bool Contains(int item){
            throw new NotImplementedException();
        }
        public void CopyTo(int[] array,int arrayIndex){
            throw new NotImplementedException();
        }
        public bool Remove(int item){
            throw new NotImplementedException();
        }
        public int Count=>0;
        public bool IsReadOnly=>true;
    }
    public class class_演算子オーバーロード2{
        // ReSharper disable once CollectionNeverQueried.Global
        public StructCollection StructCollectionフィールド=new();
    }
    public struct struct_演算子オーバーロード2{
        // ReSharper disable once CollectionNeverQueried.Global
        // ReSharper disable once UnassignedField.Global
        public StructCollection StructCollectionフィールド;
    }
    [Serializable]
    public class class_演算子オーバーロード:IEquatable<class_演算子オーバーロード>{
        public class_演算子オーバーロード(){
        }
        private readonly int 内部の値;
        public class_演算子オーバーロード(int 内部の値){
            this.内部の値=内部の値;
        }
        private readonly bool 内部のBoolean;
        public class_演算子オーバーロード(int 内部の値,bool 内部のBoolean){
            this.内部の値=内部の値;
            this.内部のBoolean=内部のBoolean;
        }
        //[global::Lite.Optimizers.NoOptimize]
        public int _最適化されないメンバー=4;
        public int Int32フィールド=4;
        public int 最適化されないメソッド()=>4;
        [Pure]
        public int メソッド()=>4;
        public int Int32プロパティ{get;set;}
        // ReSharper disable once UnassignedField.Global
        public static int StaticInt32フィールド;
        public static string StaticStringフィールド="B";
        public string Stringフィールド="A";
        public string Stringプロパティ{get;set;}
        // ReSharper disable once CollectionNeverQueried.Global
        public StructCollection StructCollectionフィールド=new();
        // ReSharper disable once CollectionNeverQueried.Global
        public List<int> Listフィールド=new();

        // ReSharper disable once CollectionNeverQueried.Global
        public List<int> Listプロパティ{get;set;}=new();

        // ReSharper disable once CollectionNeverQueried.Global
        public HashSet<int> HashSetプロパティ{get;set;}=new();

        public class_演算子オーバーロード2 class演算子オーバーロード2プロパティ{get;set;}=new();
        public readonly class_演算子オーバーロード2 class_演算子オーバーロード2フィールド=new();
        public struct_演算子オーバーロード2 Struct演算子オーバーロード2=new();
        public static class_演算子オーバーロード operator~(class_演算子オーバーロード a)=>new(~a.内部の値,a.内部のBoolean);
        public static class_演算子オーバーロード operator!(class_演算子オーバーロード a)=>new(-1^a.内部の値,!a.内部のBoolean);
        public static bool operator false(class_演算子オーバーロード a)=>!a.内部のBoolean;
        public static bool operator true(class_演算子オーバーロード a)=>a is{内部のBoolean: true};
        public static class_演算子オーバーロード operator++(class_演算子オーバーロード a)=>new(a.内部の値+1);
        public static class_演算子オーバーロード operator--(class_演算子オーバーロード a)=>new(a.内部の値-1);
        public static class_演算子オーバーロード operator-(class_演算子オーバーロード a)=>new(-a.内部の値,a.内部のBoolean);
        public static class_演算子オーバーロード operator+(class_演算子オーバーロード a)=>new(a.内部の値,a.内部のBoolean);
        public static class_演算子オーバーロード operator-(class_演算子オーバーロード a,class_演算子オーバーロード b)=>new(-a.内部の値);
        public static class_演算子オーバーロード operator&(class_演算子オーバーロード a,class_演算子オーバーロード b){
            return new class_演算子オーバーロード(a.内部の値&b.内部の値);
        }
        [SuppressMessage("ReSharper","ConvertIfStatementToReturnStatement")]
        public static class_演算子オーバーロード operator|(class_演算子オーバーロード? a,class_演算子オーバーロード? b){
            if(a==null) return b!;
            if(b==null) return a;
            return new class_演算子オーバーロード(a.内部の値|b.内部の値);
        }
        public static explicit operator int(class_演算子オーバーロード a)=>1;
        public static explicit operator decimal(class_演算子オーバーロード a)=>1;
        public static explicit operator string(class_演算子オーバーロード a)=>"class_演算子オーバーロード";
        public static explicit operator class_演算子オーバーロード(decimal a)=>new();
        //public static explicit operator Object(class_演算子オーバーロード _Field)=>new Object();
        public static decimal class_演算子オーバーロードからDecimalにキャスト(class_演算子オーバーロード a)=>1m;

        public bool Equals(class_演算子オーバーロード? other){
            if(ReferenceEquals(this,other)) return true;
            if(other is null) return false;
            return this.内部の値==other.内部の値&&this.内部のBoolean==other.内部のBoolean;
        }
        public override bool Equals(object? obj)=>obj is not null&&this.Equals((class_演算子オーバーロード)obj);
        public override int GetHashCode()=>this.内部の値;
    }
    protected static readonly class_演算子オーバーロード[] _class_演算子オーバーロードArray={new()};

    protected static readonly struct_演算子オーバーロード[] _struct_演算子オーバーロードArray=new struct_演算子オーバーロード[1];
    //リフレクションで書き換える
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    protected static class_演算子オーバーロード _Static_class_演算子オーバーロード1=new();
    protected readonly class_演算子オーバーロード _Instance_class_演算子オーバーロード1=new();
    protected static readonly class_演算子オーバーロード _class_演算子オーバーロード2=new();
    public struct struct_演算子オーバーロード:IEquatable<struct_演算子オーバーロード>{
        public int Int32フィールド;
        public bool Booleanフィールド;
        public string Stringフィールド;
        public int Int32プロパティ{get;set;}
        public struct_演算子オーバーロード(int Int32フィールド,bool Booleanフィールド){
            this.Int32フィールド=Int32フィールド;
            this.Booleanフィールド=Booleanフィールド;
            this.Stringフィールド="";
            this.Int32プロパティ=0;
        }
        private struct_演算子オーバーロード(int Int32フィールド,bool Booleanフィールド,string Stringフィールド,int Int32プロパティ){
            this.Int32フィールド=Int32フィールド;
            this.Booleanフィールド=Booleanフィールド;
            this.Stringフィールド=Stringフィールド;
            this.Int32プロパティ=Int32プロパティ;
        }
        public static bool operator true(struct_演算子オーバーロード a)=>a.Booleanフィールド;
        public static bool operator false(struct_演算子オーバーロード a)=>!a.Booleanフィールド;
        public static struct_演算子オーバーロード operator&(struct_演算子オーバーロード a,struct_演算子オーバーロード b)=>
            new(a.Int32フィールド&b.Int32フィールド,a.Booleanフィールド&b.Booleanフィールド,a.Stringフィールド+b.Stringフィールド,
                a.Int32プロパティ&b.Int32プロパティ);
        public static struct_演算子オーバーロード operator|(struct_演算子オーバーロード a,struct_演算子オーバーロード b)=>
            new(a.Int32フィールド|b.Int32フィールド,a.Booleanフィールド|b.Booleanフィールド,a.Stringフィールド,a.Int32プロパティ|b.Int32プロパティ);
        //          public static explicit operator Int32(struct_演算子オーバーロード a)=>1;
        public static explicit operator struct_演算子オーバーロード(int a)=>new();
        //      public static Decimal struct_演算子オーバーロードからDecimalにキャスト(struct_演算子オーバーロード a)=>1m;
        public static struct_演算子オーバーロード operator~(struct_演算子オーバーロード a)=>new(~a.Int32フィールド,a.Booleanフィールド);
        public static struct_演算子オーバーロード operator!(struct_演算子オーバーロード a)=>new(-1^a.Int32フィールド,!a.Booleanフィールド);
        public static struct_演算子オーバーロード operator++(struct_演算子オーバーロード a)=>new(a.Int32フィールド+1,a.Booleanフィールド);
        public static struct_演算子オーバーロード operator--(struct_演算子オーバーロード a)=>new(a.Int32フィールド-1,a.Booleanフィールド);
        public static struct_演算子オーバーロード operator-(struct_演算子オーバーロード a)=>new(-a.Int32フィールド,a.Booleanフィールド);
        public static struct_演算子オーバーロード operator+(struct_演算子オーバーロード a)=>new(a.Int32フィールド+2,a.Booleanフィールド);
        public static struct_演算子オーバーロード operator-(struct_演算子オーバーロード a,struct_演算子オーバーロード b)=>
            new(-a.Int32フィールド-b.Int32フィールド,a.Booleanフィールド);
        public static explicit operator int(struct_演算子オーバーロード a)=>1;
        public bool Equals(struct_演算子オーバーロード other)=>this.Int32フィールド==other.Int32フィールド;
    }
    protected static readonly short Int16=1;
    protected static readonly int Int32=1;
    protected static readonly long Int64=1;
    protected static readonly uint UInt32=1;
    protected static readonly ulong UInt64=1;
    protected static readonly string String="A";
    protected static readonly uint UInt32_1=1;
    protected static readonly int Int32_1=1,Int32_2=2;
    protected static readonly long Int64_1=1;
    protected static readonly decimal Decimal_1=1,Decimal_2=2;
    protected static readonly bool Boolean1=true,Boolean2=false;
    protected static readonly object Object_String="A";
    protected static readonly object Object_Int32=1;
    protected static readonly string[] Array={"A","B"};


    protected static readonly int _Int32=2;
    protected static readonly bool _Boolean=true;
    public static readonly string _String="S";
    protected static readonly int? _NullableInt32=0;
    protected static readonly List<int> _List=new(){1,2};
    protected static readonly Func<int,int> _Delegate=p=>p;
    protected static int Function()=>1;
    [Fact]
    public void Add()=>this.実行結果が一致するか確認(()=>_Int32+_Int32);
    [Fact]
    public void AddChecked()=>this.実行結果が一致するか確認(()=>checked(_Int32+_Int32));
    [Fact]
    public void Subtract()=>this.実行結果が一致するか確認(()=>_Int32-_Int32);
    [Fact]
    public void SubtractChecked()=>this.実行結果が一致するか確認(()=>checked(_Int32-_Int32));
    [Fact]
    public void Multiply()=>this.実行結果が一致するか確認(()=>_Int32*_Int32);
    [Fact]
    public void MultiplyChecked()=>this.実行結果が一致するか確認(()=>checked(_Int32*_Int32));
    [Fact]
    public void Divide()=>this.実行結果が一致するか確認(()=>_Int32/_Int32);
    [Fact]
    public void Modulo()=>this.実行結果が一致するか確認(()=>_Int32%_Int32);
    [Fact]
    public void And()=>this.実行結果が一致するか確認(()=>_Boolean&_Boolean);
    [Fact]
    public void Or()=>this.実行結果が一致するか確認(()=>_Boolean|_Boolean);
    [Fact]
    public void AndChecked()=>this.実行結果が一致するか確認(()=>_Boolean&_Boolean);
    [Fact]
    public void ExclusiveOr()=>this.実行結果が一致するか確認(()=>_Boolean^_Boolean);
    [Fact]
    public void AndAlso()=>this.実行結果が一致するか確認(()=>_Boolean&&_Boolean);
    [Fact]
    public void OrElse()=>this.実行結果が一致するか確認(()=>_Boolean||_Boolean);
    [Fact]
    public void Equal()=>this.実行結果が一致するか確認(()=>_Int32==3);
    [Fact]
    public void NotEqual()=>this.実行結果が一致するか確認(()=>_Int32!=3);
    [Fact]
    public void GreaterThan()=>this.実行結果が一致するか確認(()=>_Int32>3);
    [Fact]
    public void GreaterThanOrEqual()=>this.実行結果が一致するか確認(()=>_Int32>=3);
    [Fact]
    public void LessThan()=>this.実行結果が一致するか確認(()=>_Int32<3);
    [Fact]
    public void LessThanOrEqual()=>this.実行結果が一致するか確認(()=>_Int32<=3);
    [Fact]
    public void LeftShift()=>this.実行結果が一致するか確認(()=>_Int32<<3);
    [Fact]
    public void RightShift()=>this.実行結果が一致するか確認(()=>_Int32>> 3);
    [Fact]
    public void ArrayIndex()=>this.実行結果が一致するか確認(()=>Array[0]);
    [Fact]
    public void ArrayLength()=>this.実行結果が一致するか確認(()=>Array.Length);
    [Fact]
    public void Convert()=>this.実行結果が一致するか確認(()=>(double)_Int32+(double)_Int32);
    [Fact]
    public void ConvertChecked()=>this.実行結果が一致するか確認(()=>(double)_Int32+(double)_Int32);
    //[Fact] public void Increment         ()=>this._変数Cache.AssertExecute(()=>_Int32+1);
    //[Fact] public void Decrement         ()=>this._変数Cache.AssertExecute(()=>_Int32-1);
    //[Fact] public void IsFalse           ()=>this._変数Cache.AssertExecute(()=>class_演算子オーバーロード2--);
    //[Fact] public void IsTrue            ()=>this._変数Cache.AssertExecute(()=>class_演算子オーバーロード2++);
    [Fact]
    public void Negate()=>this.実行結果が一致するか確認(()=>-_Int32+-_Int32);
    [Fact]
    public void NegateChecked()=>this.実行結果が一致するか確認(()=>checked(-_Int32)+checked(-_Int32));
    [Fact]
    public void Not()=>this.実行結果が一致するか確認(()=>!_Boolean&&!_Boolean);
    [Fact]
    public void OnesComplement()=>this.実行結果が一致するか確認(()=>~_Int32+~_Int32);

    [Fact]
    public void TypeAs()=>this.実行結果が一致するか確認(()=>(Object_String as string)+(Object_String as string));

    [Fact]
    public void UnaryPlus()=>this.実行結果が一致するか確認(()=>+_Static_class_演算子オーバーロード1-+_Static_class_演算子オーバーロード1);

    [Fact]
    public void Unbox()=>this.実行結果が一致するか確認(()=>(int)Object_Int32+(int)Object_Int32);

    [Fact]
    public void Block(){
        //if(a_Variables_Count!=b_Variables_Count) return false;
        var p=Expression.Parameter(typeof(int));
        var q=Expression.Parameter(typeof(double));
        this.AssertNotEqual(
            Expression.Block(
                new[]{p,q},
                Expression.Constant(0)
            ),
            Expression.Block(
                new[]{p},
                Expression.Constant(0)
            )
        );
        //for(var i=0;i<a_Variables_Count;i++) {
        //    if(a_Variable.Type!=b_Variable.Type)return false;
        this.AssertNotEqual(
            Expression.Block(
                new[]{q},
                Expression.Constant(0)
            ),
            Expression.Block(
                new[]{p},
                Expression.Constant(0)
            )
        );
        //}
        this.AssertEqual(
            Expression.Block(
                new[]{p},
                Expression.Constant(0)
            ),
            Expression.Block(
                new[]{p},
                Expression.Constant(0)
            )
        );
    }

    [Fact]
    public void Conditional()=>this.実行結果が一致するか確認(()=>(_Boolean?_Int32:10)+(_Boolean?_Int32:10));

    [Fact]
    public void Constant()=>this.実行結果が一致するか確認(()=>_List[1]+_List[1]);

    [Fact]
    public void DebugInfo(){
        var SymbolDocument0=Expression.SymbolDocument("ソースファイル名0.cs");
        var SymbolDocument1=Expression.SymbolDocument("ソースファイル名1.cs");
        //a.Document==b.Document&&a.StartLine==b.StartLine&&a.StartColumn==b.StartColumn&&a.EndLine==b.EndLine&&a.EndColumn==b.EndColumn;
        this.AssertNotEqual(
            Expression.DebugInfo(SymbolDocument0,1,2,3,4),
            Expression.DebugInfo(SymbolDocument1,11,22,33,44)
        );
        this.AssertNotEqual(
            Expression.DebugInfo(SymbolDocument0,1,2,3,4),
            Expression.DebugInfo(SymbolDocument0,11,22,33,44)
        );
        this.AssertNotEqual(
            Expression.DebugInfo(SymbolDocument0,1,2,3,4),
            Expression.DebugInfo(SymbolDocument0,1,22,33,44)
        );
        this.AssertNotEqual(
            Expression.DebugInfo(SymbolDocument0,1,2,3,4),
            Expression.DebugInfo(SymbolDocument0,1,2,33,44)
        );
        this.AssertNotEqual(
            Expression.DebugInfo(SymbolDocument0,1,2,3,4),
            Expression.DebugInfo(SymbolDocument0,1,2,3,44)
        );
        this.AssertEqual(
            Expression.DebugInfo(SymbolDocument0,1,2,3,4),
            Expression.DebugInfo(SymbolDocument0,1,2,3,4)
        );
    }

    //Default
    [SuppressMessage("ReSharper","MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper","MemberCanBePrivate.Local")]
    [SuppressMessage("ReSharper","FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper","NotAccessedField.Local")]
    public struct TestDynamic<T>{
        public readonly T メンバー1;
        public readonly T メンバー2;

        public TestDynamic(T メンバー){
            this.メンバー1=メンバー;
            this.メンバー2=メンバー;
        }
    }

    public static dynamic Dynamicメンバーアクセス(dynamic a){
        return a.メンバー;
    }
    private void AssertEqual(Expression a,Expression b)=>this.共通object1(a,b=>Assert.Equal(a,b,this.Comparer));

    private void AssertNotEqual(Expression a,Expression b)=>this.共通object1(a,b=>Assert.NotEqual(a,b,this.Comparer));

    [Fact]
    public void Dynamic(){
        var CSharpArgumentInfo1=Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None,null);
        var CSharpArgumentInfoArray1=new[]{CSharpArgumentInfo1};
        var CSharpArgumentInfoArray2=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1};
        var CSharpArgumentInfoArray3=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
        //if(!this.SequenceEqual(a.Arguments,b.Arguments)) return false;
        {
            this.AssertNotEqual(
                Expression.Dynamic(
                    Binder.Binder.UnaryOperation(
                        Binder.CSharpBinderFlags.None,
                        ExpressionType.Increment,
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(1,typeof(object))
                ),
                Expression.Dynamic(
                    Binder.Binder.UnaryOperation(
                        Binder.CSharpBinderFlags.None,
                        ExpressionType.Increment,
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(2,typeof(object))
                )
            );
        }
        //if(a_ConvertBinder!=null) {
        //    if(a_ConvertBinder.ReturnType!=b_ConvertBinder.ReturnType)return false;
        {
            var Constant_1L=Expression.Constant(1L);
            this.AssertNotEqual(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(int),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
            this.AssertEqual(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
        }
        //    return a_ConvertBinder.Explicit==b_ConvertBinder.Explicit;
        {
            var Constant_1L=Expression.Constant(1L);
            this.AssertEqual(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
            this.AssertNotEqual(
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                ),
                Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.None,
                        typeof(double),
                        typeof(ExpressionEqualityComparer)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
        }
        //if(a_GetMemberBinder!=null) {
        {
            this.AssertEqual(
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.None,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                ),
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.None,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                )
            );
        }
        //    return a_GetMemberBinder.Name.Equals(b_GetMemberBinder.Name,StringComparison.Ordinal);
        {
            this.AssertNotEqual(
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                ),
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー2),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                )
            );
            this.AssertEqual(
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                ),
                Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1))
                )
            );
        }
        //}
        //if(a_SetMemberBinder!=null) {
        //    return a_SetMemberBinder.Name.Equals(b_SetMemberBinder.Name,StringComparison.Ordinal);
        {
            this.AssertNotEqual(
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                ),
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー2),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                )
            );
            this.AssertEqual(
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                ),
                Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(new TestDynamic<int>(1)),
                    Expression.Constant(2)
                )
            );
        }
        //}
        //if(a_GetIndexBinder!=null) {
        {
            const int expected=2;
            var Array=new[]{1,expected,3};
            this.AssertEqual(
                Expression.Dynamic(
                    Binder.Binder.GetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1)
                ),
                Expression.Dynamic(
                    Binder.Binder.GetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1)
                )
            );
        }
        //}
        //if(a_SetIndexBinder!=null) {
        //    return a_SetIndexBinder.CallInfo.Equals(b_SetIndexBinder.CallInfo);
        {
            var Array=new[]{1,2,3};
            this.AssertEqual(
                Expression.Dynamic(
                    Binder.Binder.SetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray3
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1),
                    Expression.Constant(2)
                ),
                Expression.Dynamic(
                    Binder.Binder.SetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(ExpressionEqualityComparer),
                        CSharpArgumentInfoArray3
                    ),
                    typeof(object),
                    Expression.Constant(Array),
                    Expression.Constant(1),
                    Expression.Constant(2)
                )
            );
        }
        //}
        //return true;
        {
            var オペランド=Expression.Dynamic(
                Binder.Binder.BinaryOperation(
                    Binder.CSharpBinderFlags.None,
                    ExpressionType.Add,
                    typeof(ExpressionEqualityComparer),
                    new[]{
                        Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None,null),
                        Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None,null)
                    }
                ),
                typeof(object),
                Expression.Constant(1),
                Expression.Constant(1)
            );
            this.AssertEqual(
                オペランド,
                オペランド
            );
        }
    }
    [Fact]
    public void Goto00(){
        var Label1=Expression.Label(typeof(int),"Label1");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Goto(Label1,Expression.Constant(1)),
                    Expression.Label(Label1,Expression.Constant(11))
                )
            )
        );
    }
    [Fact]
    public void Goto01(){
        //a.Target==b.Target&&this.Equals(a.Value,b.Value);
        var Label1=Expression.Label(typeof(int),"Label1");
        var Label2=Expression.Label(typeof(int),"Label2");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Block(
                        Expression.Goto(Label1,Expression.Constant(1)),
                        Expression.Label(Label1,Expression.Constant(11))
                    ),
                    Expression.Block(
                        Expression.Goto(Label2,Expression.Constant(2)),
                        Expression.Label(Label2,Expression.Constant(22))
                    )
                )
            )
        );
    }
    [Fact]
    public void Goto1(){
        var Label1=Expression.Label(typeof(int),"Label1");
        var Label2=Expression.Label(typeof(int),"Label2");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Block(
                        Expression.Goto(Label1,Expression.Constant(1)),
                        Expression.Label(Label1,Expression.Constant(11))
                    ),
                    Expression.Block(
                        Expression.Goto(Label2,Expression.Constant(1)),
                        Expression.Label(Label2,Expression.Constant(11))
                    )
                )
            )
        );
    }
    [Fact]
    public void Goto2(){
        var Label1=Expression.Label(typeof(int),"Label1");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Goto(Label1,Expression.Constant(1)),
                    Expression.Goto(Label1,Expression.Constant(2)),
                    Expression.Label(Label1,Expression.Constant(22))
                )
            )
        );
    }

    //private sealed class Target<
    //    T00,T01
    //>:Target {
    //    public T00 v00;
    //    public T01 v01;

    //    public override Object this[Int32 index]{
    //        get{
    //            switch(index) {
    //                case 0:
    //                    return this.v00;
    //                case 1:
    //                    return this.v01;
    //                default:
    //                    throw new IndexOutOfRangeException("Target2["+index+"]は範囲外");
    //            }
    //        }
    //        set {
    //            switch(index) {
    //                case 0:
    //                this.v00 =(T00)value;
    //                    break;
    //                case 1:
    //                this.v01 =(T01)value;
    //                    break;
    //                default:
    //                    throw new IndexOutOfRangeException("Target2["+index+"]は範囲外");
    //            }
    //        }
    //    }
    //}

    //[Fact] public void Index() {
    //    {
    //        var Target=new Target<Int32,Int32> {
    //            v00=11,
    //            v01=22
    //        };
    //        var D=Expression.Lambda<Func<Int32>>(
    //            Expression.Add(
    //                Expression.Field(
    //                    Expression.Constant(Target),
    //                    "v00"
    //                ),
    //                Expression.Field(
    //                    Expression.Constant(Target),
    //                    "v01"
    //                )
    //            )
    //        ).Compile();
    //        Assert.AreEqual(33,D());
    //    }
    //    //a.Indexer==b.Indexer&&this.Equals(a.Object,b.Object)&&this.SequentialEquals(a.Arguments,b.Arguments);
    //    {
    //        var 配列1=new[] {
    //            1,2,3
    //        };
    //        var 配列2=new[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Int32>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列2),
    //                        Expression.Constant(2)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //    {
    //        var 配列1=new[] {
    //            1,2,3
    //        };
    //        var 配列2=new[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Int32>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列2),
    //                        Expression.Constant(1)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //    {
    //        var 配列1=new[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Int32>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(2)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //    {
    //        var 配列1=new Decimal[] {
    //            1,2,3
    //        };
    //        this.Execute(
    //            Expression.Lambda<Func<Decimal>>(
    //                Expression.Add(
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    ),
    //                    Expression.ArrayAccess(
    //                        Expression.Constant(配列1),
    //                        Expression.Constant(1)
    //                    )
    //                )
    //            )
    //        );
    //    }
    //}

    [Fact]
    public void Invoke()=>this.実行結果が一致するか確認(()=>_Delegate(4)+_Delegate(4));
    [Fact]
    public void Lambda0(){
        var p=Expression.Parameter(typeof(int));
        this.AssertEqual(
            Expression.Lambda(
                p,
                p
            ),
            Expression.Lambda(
                p,
                p
            )
        );
        var q=Expression.Parameter(typeof(int));
        this.AssertNotEqual(
            Expression.Lambda(
                p,
                p
            ),
            Expression.Lambda(
                q,
                p
            )
        );
    }
    [Fact]
    public void Lambda1(){
        var v=Expression.Parameter(typeof(int),"v");
        var a0=Expression.Parameter(typeof(int),"a0");
        var a1=Expression.Parameter(typeof(int),"a1");
        this.AssertEqual(
            Expression.Lambda<Func<int,int>>(
                a0,
                a0
            ),
            Expression.Lambda<Func<int,int>>(
                a1,
                a1
            )
        );
    }
    [Fact]
    public void Lambda2(){
        var v0=Expression.Parameter(typeof(int),"v0");
        var v1=Expression.Parameter(typeof(int),"v1");
        var a0=Expression.Parameter(typeof(int),"a0");
        var a1=Expression.Parameter(typeof(int),"a1");
        this.AssertEqual(
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a0,
                    a0
                ),
                v0
            ),
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a1,
                    a1
                ),
                v0
            )
        );
        this.AssertNotEqual(
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a0,
                    a0
                ),
                v0
            ),
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a1,
                    a1
                ),
                v1
            )
        );
        this.AssertNotEqual(
            Expression.Block(
                typeof(void),
                Expression.Lambda<Func<int,int>>(
                    a0,
                    a0
                ),
                v0
            ),
            Expression.Block(
                Expression.Lambda<Func<int,int>>(
                    a1,
                    a1
                ),
                v0
            )
        );
    }
    [Fact]
    public void ListInit()=>this.実行結果が一致するか確認(()=>new{a=new List<int>{1,2},b=new List<int>{1,2}});

    [Fact]
    public void Loop(){
        var VoidBreak0=Expression.Label();
        var Continue0=Expression.Label();
        var カウンタ=Expression.Parameter(typeof(int),"カウンタ");
        //if(a_BreakLabel==null) {
        //    if(b_BreakLabel!=null) return false;
        this.AssertNotEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                ),
                Expression.Label(VoidBreak0)
            )
        );
        this.AssertEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            )
        );
        this.AssertEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            )
        );
        //}
        //if(b_BreakLabel==null) return false;
        this.AssertNotEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                ),
                Expression.Label(VoidBreak0)
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Default(typeof(void)),
                        Expression.Goto(VoidBreak0)
                    )
                ),
                Expression.Label(VoidBreak0)
            )
        );
        //if(a_ContinueLabel!=null) {
        //    if(b_ContinueLabel==null) return false;
        this.AssertNotEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            )
        );
        this.AssertEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            )
        );
        //} else {
        //    if(b_ContinueLabel!=null) return false;
        this.AssertNotEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0
                )
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            )
        );
        this.AssertEqual(
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            ),
            Expression.Block(
                new[]{カウンタ},
                Expression.Assign(
                    カウンタ,
                    Expression.Constant(0)
                ),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(
                            Expression.PostIncrementAssign(カウンタ),
                            Expression.Constant(10)
                        ),
                        Expression.Goto(Continue0),
                        Expression.Goto(VoidBreak0)
                    ),
                    VoidBreak0,
                    Continue0
                )
            )
        );
        //}
    }

    [Fact]
    public void MemberAccess()=>
        this.実行結果が一致するか確認(
            ()=>_Static_class_演算子オーバーロード1.Int32フィールド+_Static_class_演算子オーバーロード1.Int32フィールド);

    [Fact]
    public void MemberInit()=>
        this.実行結果が一致するか確認(()=>new{a=new class_演算子オーバーロード{Int32フィールド=3},b=new class_演算子オーバーロード{Int32フィールド=3}});

    [Fact]
    public void Call()=>this.実行結果が一致するか確認(()=>Function()+Function());

    [Fact]
    public void NewArrayBounds()=>this.実行結果が一致するか確認(()=>new{a=new int[10],b=new int[10]});

    [Fact]
    public void NewArrayInit()=>this.実行結果が一致するか確認(()=>new{a=new[]{1,2,3},b=new[]{1,2,3}});

    [Fact]
    public void New()=>this.実行結果が一致するか確認(()=>new{a=new class_演算子オーバーロード(),b=new class_演算子オーバーロード()});

    [Fact]
    public void Calesce()=>this.実行結果が一致するか確認(()=>(_NullableInt32??4)+(_NullableInt32??4));
    //共通部分式でParameterは最速なので先行評価しないため、カバレッジが出来方法が思いつかない。
    [Fact]
    public void Parameter(){
        //if(a_Index!=b_Index) return false;
        var p=Expression.Parameter(typeof(int));
        var q=Expression.Parameter(typeof(int));
        this.AssertNotEqual(
            Expression.Lambda<Func<int,int,int>>(
                p,p,q),
            Expression.Lambda<Func<int,int,int>>(
                q,p,q)
        );
        //if(a_Index>=0) return true;
        this.AssertEqual(
            Expression.Lambda<Func<int,int,int>>(
                p,p,q),
            Expression.Lambda<Func<int,int,int>>(
                p,p,q)
        );
        //this.a_Parameters.Add(a);
        //this.b_Parameters.Add(b);
        //return a==b;
        this.AssertEqual(
            p,
            p
        );
        this.AssertNotEqual(
            p,
            q
        );
    }

    //Switch
    //Try
    //実際どんな式か分からない。
    [Fact]
    public void TypeEqual(){
        this.実行結果が一致するか確認(()=>
            // ReSharper disable once OperatorIsCanBeUsed
            Object_Int32.GetType()==typeof(int)&&
            // ReSharper disable once OperatorIsCanBeUsed
            Object_Int32.GetType()==typeof(int)
        );
    }

    [Fact]
    public void TypeIs()=>this.実行結果が一致するか確認(()=>Object_Int32 is int||Object_Int32 is int);

    private static double L(Func<double> f)=>f();

    [Fact]
    public void Default(){
        var Method=typeof(ExpressionEqualityComparer).GetMethod(nameof(L),BindingFlags.Static|BindingFlags.NonPublic)!;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<double>>(
                Expression.Add(
                    Expression.Call(
                        Method,
                        Expression.Lambda<Func<double>>(
                            Expression.Default(typeof(double))
                        )
                    ),
                    Expression.Call(
                        Method,
                        Expression.Lambda<Func<double>>(
                            Expression.Default(typeof(double))
                        )
                    )
                )
            )
        );
    }
    [Fact]
    public void Switch(){
        //if(a.Comparison!=b.Comparison) return false;
        this.AssertNotEqual(
            Expression.Switch(
                Expression.Constant(123),
                Expression.Constant(0m),
                Expression.SwitchCase(
                    Expression.Constant(64m),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant("A"),
                Expression.Constant(1m),
                Expression.SwitchCase(
                    Expression.Constant(64m),
                    Expression.Constant("A")
                )
            )
        );
        //if(!this.PrivateEquals(a.DefaultBody,b.DefaultBody)) return false;
        this.AssertNotEqual(
            Expression.Switch(
                Expression.Constant(123),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(1),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            )
        );
        //if(!this.PrivateEquals(a.SwitchValue,b.SwitchValue)) return false;
        this.AssertNotEqual(
            Expression.Switch(
                Expression.Constant(123),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(63),
                    Expression.Constant(124)
                )
            )
        );
        //if(a_Cases.Count!=b_Cases.Count) return false;
        this.AssertNotEqual(
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(66),
                    Expression.Constant(124)
                ),
                Expression.SwitchCase(
                    Expression.Constant(63),
                    Expression.Constant(123)
                )
            )
        );
        //for(var c=0;c<a_Cases_Count;c++) {
        //    if(!this.PrivateEquals(a_Case.Body,b_Case.Body)) return false;
        this.AssertNotEqual(
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(124)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-63),
                    Expression.Constant(124)
                )
            )
        );
        //    if(!this.SequenceEqual(a_Case.TestValues,b_Case.TestValues)) return false;
        this.AssertNotEqual(
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(123)
                )
            ),
            Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(124)
                )
            )
        );
        //}
        {
            var Switch=Expression.Switch(
                Expression.Constant(124),
                Expression.Constant(0),
                Expression.SwitchCase(
                    Expression.Constant(-64),
                    Expression.Constant(124)
                )
            );
            this.AssertEqual(
                Switch,
                Switch
            );
        }
    }

    [Fact]
    public void Try(){
        //if(!this.PrivateEquals(a.Body,b.Body)) return false;
        this.AssertNotEqual(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(1),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
        this.AssertEqual(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
    }

    [Fact]
    public void TryFault(){
        //if(!this.PrivateEquals(a.Fault,b.Fault)) return false;
        this.AssertNotEqual(
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(0)
            ),
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(1)
            )
        );
        this.AssertEqual(
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(0)
            ),
            Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(0)
            )
        );
    }

    [Fact]
    public void TryFinally(){
        //if(!this.PrivateEquals(a.Finally,b.Finally)) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.TryFinally(
                    Expression.Constant(0),
                    Expression.Default(typeof(int))
                )
            )
        );
        this.AssertNotEqual(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            ),
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(int))
            )
        );
        this.AssertEqual(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            ),
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            )
        );
    }

    [Fact]
    public void TryCatch_Handlers(){
        //if(a_Handlers_Count!=b_Handlers.Count) return false;
        this.AssertNotEqual(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(AmbiguousMatchException),
                    Expression.Constant(0)
                ),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
        this.AssertEqual(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
    }

    [Fact]
    public void TryCatch_Handler_Body(){
        //for(var c=0;c<a_Handlers_Count;c++) {
        //    if(!this.PrivateEquals(a_Handler.Body  ,b_Handler.Body  )) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(1)
                        )
                    )
                )
            )
        );
    }
    [Fact]
    public void TryCatch_Filter0(){
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(0),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0),
                        Expression.Constant(true)

                    )
                )
            )
        );
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
    }
    [Fact]
    public void TryCatch_Filter1(){
        //    if(!this.PrivateEquals(a_Handler.Filter,b_Handler.Filter)) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0),
                            Expression.Constant(true)

                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0),
                            Expression.Constant(false)
                        )
                    )
                )
            )
        );
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
    }
    [Fact]
    public void TryCatch_Filter2(){
        var Variable=Expression.Parameter(typeof(Exception));
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            Variable,
                            Expression.Constant(0),
                            Expression.Equal(Variable,Expression.Default(Variable.Type))

                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            Variable,
                            Expression.Constant(1),
                            Expression.Equal(
                                Expression.Property(
                                    Variable,
                                    "Message"
                                ),
                                Expression.Default(typeof(string))
                            )
                        )
                    )
                )
            )
        );
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
    }

    [Fact]
    public void TryCatch_Test(){
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)

                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(NotSupportedException),
                            Expression.Constant(0)
                        )
                    )
                )
            )
        );
    }

    [Fact]
    public void TryCatch(){
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    )
                )
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    )
                )
            )
        );
    }
    [Fact]
    public void TryCatchFinally(){
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int>>(
                Expression.TryCatchFinally(
                    Expression.Constant(0),
                    Expression.Default(typeof(int)),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(1)
                    )
                )
            )
        );
        //if(!this.PrivateEquals(a.Finally,b.Finally)) return false;
        this.AssertNotEqual(
            Expression.TryCatchFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void)),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            ),
            Expression.TryCatchFinally(
                Expression.Constant(0),
                Expression.Default(typeof(int)),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(1)
                )
            )
        );
        this.AssertEqual(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            ),
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Default(typeof(void))
            )
        );
    }

    [Fact]
    public void IsTrue(){
        //todo ダメだった
        //            this._変数Cache.AssertExecute(()=>(class_演算子オーバーロード2&&class_演算子オーバーロード2)-(class_演算子オーバーロード2&&class_演算子オーバーロード2));
        //_Field ? _Field : (_Field | b);
        //Test_ExpressionEqualityComparer.class_演算子オーバーロード2.op_False(_Field) ? _Field : (_Field & b);
        var Constant=Expression.Constant(_Static_class_演算子オーバーロード1);
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<class_演算子オーバーロード>>(
                Expression.Condition(
                    Expression.IsTrue(Constant),
                    Constant,
                    Expression.Or(
                        Constant,Constant
                    )
                )
            )
        );
    }

    [Fact]
    public void IsFalse(){
        //todo ダメだった
        //            this._変数Cache.AssertExecute(()=>(class_演算子オーバーロード2&&class_演算子オーバーロード2)-(class_演算子オーバーロード2&&class_演算子オーバーロード2));
        //_Field ? _Field : (_Field | b);
        //Test_ExpressionEqualityComparer.class_演算子オーバーロード2.op_False(_Field) ? _Field : (_Field & b);
        var Constant=Expression.Constant(_Static_class_演算子オーバーロード1);
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<class_演算子オーバーロード>>(
                Expression.Condition(
                    Expression.IsFalse(Constant),
                    Constant,
                    Expression.And(
                        Constant,Constant
                    )
                )
            )
        );
    }

    [Fact]
    public void SequencialEqual(){
        //if(a_Count!=b.Count) return false;
        this.AssertNotEqual(
            Expression.Block(
                Expression.Constant(0),
                Expression.Constant(0)
            ),
            Expression.Block(
                Expression.Constant(0)
            )
        );
        //for(var i=0;i<a_Count;i++) {
        //    if(!this.PrivateEquals(a[i],b[i])) return false;
        this.AssertNotEqual(
            Expression.Block(
                Expression.Constant(0)
            ),
            Expression.Block(
                Expression.Constant(1)
            )
        );
        //}
        this.AssertEqual(
            Expression.Block(
                Expression.Constant(0)
            ),
            Expression.Block(
                Expression.Constant(0)
            )
        );
    }

    [Fact]
    public void PrivateEquals(){
        //if(a==null)return b==null;
        this.AssertNotEqual(
            null,
            Expression.Constant(0)
        );
        this.AssertEqual(
            null,
            null
        );
        //if(b==null)return false;
        this.AssertNotEqual(
            Expression.Constant(0),
            null
        );
        //if(a.NodeType!=b.NodeType||a.Type!=b.Type) return false;
        this.AssertNotEqual(
            Expression.Constant(0),
            Expression.Default(typeof(int))
        );
        //Assign
        //if(a_Left.NodeType!=b_Left.NodeType) return false;
        {
            var p=Expression.Parameter(typeof(int));
            var q=Expression.Parameter(typeof(int[]));
            this.AssertNotEqual(
                Expression.Assign(
                    p,
                    Expression.Constant(0)
                ),
                Expression.Assign(
                    Expression.ArrayAccess(
                        q,
                        Expression.Constant(0)
                    ),
                    Expression.Constant(0)
                )
            );
        }
        //if(a_Left.NodeType!=ExpressionType.Parameter) return this.T(a_Assign,b_Assign);
        {
            var p=Expression.Parameter(typeof(int[]));
            this.AssertEqual(
                Expression.Assign(
                    Expression.ArrayAccess(
                        p,
                        Expression.Constant(0)
                    ),
                    Expression.Constant(0)
                ),
                Expression.Assign(
                    Expression.ArrayAccess(
                        p,
                        Expression.Constant(0)
                    ),
                    Expression.Constant(0)
                )
            );
        }
        //if(!this.PrivateEquals(a_Assign.Right,b_Assign.Right)) return false;
        {
            var p=Expression.Parameter(typeof(int));
            var q=Expression.Parameter(typeof(int));
            this.AssertNotEqual(
                Expression.Assign(
                    p,
                    Expression.Constant(0)
                ),
                Expression.Assign(
                    q,
                    Expression.Constant(1)
                )
            );
        }
        //if(a_局所Index!=b_局所Index) return false;
        {
            var p=Expression.Parameter(typeof(int));
            var q=Expression.Parameter(typeof(int));
            this.AssertNotEqual(
                Expression.Block(
                    new[]{p,q},
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                ),
                Expression.Block(
                    new[]{p,q},
                    Expression.Assign(
                        q,
                        Expression.Constant(0)
                    )
                )
            );
        }
        //if(a_局所Index>=0) return true;
        {
            var p=Expression.Parameter(typeof(int));
            var q=Expression.Parameter(typeof(int));
            this.AssertEqual(
                Expression.Block(
                    new[]{p,q},
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                ),
                Expression.Block(
                    new[]{p,q},
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                )
            );
        }
        {
            var p=Expression.Parameter(typeof(int));
            var q=Expression.Parameter(typeof(int));
            this.AssertEqual(
                Expression.Block(
                    new[]{q},
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    )
                ),
                Expression.Block(
                    new[]{p},
                    Expression.Assign(
                        q,
                        Expression.Constant(0)
                    )
                )
            );
        }
    }

    private class List1:IEnumerable<int>{
        public void Add(int value){
        }

        public IEnumerator<int> GetEnumerator(){
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            throw new NotImplementedException();
        }
    }

    private sealed class List2:List1{
        public new void Add(int value){
        }
    }

    [Fact]
    public void InitializersEquals(){
        var List1=typeof(List1);
        var List2=typeof(List2);
        var AddMethod1=typeof(List1).GetMethod("Add");
        var AddMethod2=typeof(List2).GetMethod("Add");
        var ctor1=List1.GetConstructor(Type.EmptyTypes);
        var ctor2=List2.GetConstructor(Type.EmptyTypes);
        //if(a_Initializers.Count!=b_Initializers.Count) return false;
        Debug.Assert(ctor1!=null,"ctor1 != null");
        this.AssertNotEqual(
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0),
                Expression.Constant(0)
            )
        );
        //for(var c=0;c<a_Initializers_Count;c++){
        //    if(a.AddMethod!=b.AddMethod) return false;
        Debug.Assert(ctor2!=null,"ctor2 != null");
        this.AssertNotEqual(
            Expression.ListInit(
                Expression.New(ctor2),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor2),
                AddMethod2,
                Expression.Constant(0)
            )
        );
        //    if(!this.SequenceEqual(a.Arguments,b.Arguments)) return false;
        this.AssertNotEqual(
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(1)
            )
        );
        //}
        this.AssertEqual(
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            ),
            Expression.ListInit(
                Expression.New(ctor1),
                AddMethod1,
                Expression.Constant(0)
            )
        );
    }

    [SuppressMessage("ReSharper","FieldCanBeMadeReadOnly.Local")]
    private class BindCollection{
        public int Int32フィールド1;
        public int Int32フィールド2;
        public BindCollection BindCollectionフィールド1;
        public BindCollection BindCollectionフィールド2;
        public readonly List<int> Listフィールド1=new();
        public readonly List<int> Listフィールド2=new();

        public BindCollection(int v){
            this.Int32フィールド1=0;
            this.Int32フィールド2=0;
            this.BindCollectionフィールド1=null;
            this.BindCollectionフィールド2=null;
        }
    }

    [Fact]
    [SuppressMessage("ReSharper","AssignNullToNotNullAttribute")]
    public void MemberBindingsEquals(){
        var Type=typeof(BindCollection);
        var Int32フィールド1=Type.GetField(nameof(BindCollection.Int32フィールド1));
        var Int32フィールド2=Type.GetField(nameof(BindCollection.Int32フィールド2));
        var BindCollectionフィールド1=Type.GetField(nameof(BindCollection.BindCollectionフィールド1));
        var BindCollectionフィールド2=Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1=Type.GetField(nameof(BindCollection.Listフィールド1));
        var Listフィールド2=Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1=Expression.Constant(1);
        var Constant_2=Expression.Constant(2);
        var ctor=Type.GetConstructor(new[]{typeof(int)});
        var New=Expression.New(
            ctor,
            Constant_1
        );
        //if(a_Bindings.Count!=b_Bindings.Count) return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                ),
                Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
        //for(var c=0;c<a_Bindings_Count;c++){
        //    if(a.BindingType!=b.BindingType) return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        typeof(List<int>).GetMethod("Add"),
                        Constant_1
                    )
                )
            )
        );
        //    switch(a.BindingType){
        //        case MemberBindingType.Assignment:{
        //            if(a1.Member!=b1.Member)return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
        //            if(!this.Equals(a1.Expression,b1.Expression))return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_2
                )
            )
        );
        //            break;
        this.AssertEqual(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            ),
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            )
        );
        //}
        //        case MemberBindingType.MemberBinding:{
        //            if(a1.Member!=b1.Member)return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド2,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            )
        );
        //            if(!this.MemberBindingsEquals(a1.Bindings,b1.Bindings)) return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    ),
                    Expression.Bind(
                        Int32フィールド2,
                        Constant_1
                    )
                )
            )
        );
        //            break;
        this.AssertEqual(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド1,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            )
        );
        //        }
        //        default:{
        var Add=typeof(List<int>).GetMethod("Add");
        //            if(a1.Member!=b1.Member)return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド2,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            )
        );
        //            if(!InitializersEquals(a1.Initializers,b1.Initializers)) return false;
        this.AssertNotEqual(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_2
                    )
                )
            )
        );
        //            break;
        this.AssertEqual(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            ),
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド1,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            )
        );
        //        }
        //    }
        //}
        //return true;
    }
    private readonly struct A:IEquatable<A>{
        public static A operator+(A a,A b)=>new(a.v+b.v);
        public static A Add1(A a,A b)=>new(a.v+b.v);
        public static A Add2(A a,A b)=>new(a.v+b.v);
        public bool Equals(A other)=>this.v==other.v;
        public override bool Equals(object obj)=>this.Equals((A)obj);
        public override int GetHashCode()=>this.v;
        private readonly int v;
        public A(int v){
            this.v=v;
        }
    }
    [Fact]
    public void T_Binary(){
        //a.Method == b.Method && 
        //                        this.PrivateEquals(a.Left, b.Left) && 
        //                                                              this.PrivateEquals(a.Right, b.Right);
        //            var Methods = new[] { null, typeof(A).GetMethod(nameof(A.Add1)), typeof(A).GetMethod(nameof(A.Add2)) };
        var Methods=new[]{null,typeof(A).GetMethod("op_Addition")};
        var 値配列=new[]{new A(1),new A(2)};
        foreach(var Method0 in Methods){
            foreach(var Method1 in Methods){
                foreach(var Left0 in 値配列){
                    foreach(var Left1 in 値配列){
                        foreach(var Right0 in 値配列){
                            foreach(var Right1 in 値配列){
                                var _=this.Comparer.Equals(
                                    Expression.Add(
                                        Expression.Constant(Left0),
                                        Expression.Constant(Right0),
                                        Method0
                                    ),
                                    Expression.Add(
                                        Expression.Constant(Left1),
                                        Expression.Constant(Right1),
                                        Method1
                                    )
                                );
                            }
                        }
                    }
                }
            }
        }
    }
}
