using System.Numerics;
using LinqDB.Sets;
using Expressions=System.Linq.Expressions;
using Collections=System.Collections;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Build.Framework;
namespace TestLinqDB.Sets;
using Generic=Collections.Generic;
using Sets=LinqDB.Sets;

public class ExtensionSet:共通{
    private static Random r=new Random(1);
    static T 数値<T>(int number)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
        T a=default;
        for(var i=0;i<number;i++) a++;
        return a;
    }
    private static (Sets.IEnumerable<T>e,Generic.IEnumerable<T>s)数列範囲<T>(int 下限,int 上限)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
        var 下限0=数値<T>(下限);
        var 上限0=数値<T>(上限);
        var value=new Set<T>();
        for(var a=下限0;a<上限0;a++){
            value.Add(a);
        }
        return (value,value);
    }
    private static (Sets.IEnumerable<T>e,Generic.IEnumerable<T>s)数列<T>(int 上限)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
        var value=new Set<T>();
        var 上限0=数値<T>(上限);
        for(var a=default(T);a<上限0;a++){
            value.Add(a);
        }
        return (value,value);
    }
    private static (Sets.IEnumerable<T?>e,Generic.IEnumerable<T?>s)数列Nullable<T>(int 上限)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
        var 上限0=数値<T>(上限);
        var value=new Set<T?>();
        for(var a=default(T);a<上限0;a++){
            if(r.Next(2)==0)
                value.Add(a);
            else
                value.Add(null);
        }
        return (value,value);
    }
    private static void 集約関数<T>(int 下限,int 上限,Action<LinqDB.Sets.IEnumerable<T>,Generic.IEnumerable<T>> action)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
        for(var a=下限;a<上限;a++){
            var (s,e)=数列<T>(a);
            action(s,e);
        }
    }
    private static void 集約関数Nullable<T>(int 上限,Action<LinqDB.Sets.IEnumerable<T?>,Generic.IEnumerable<T?>> action) where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool> =>
        集約関数Nullable<T>(上限,action);
    private static void 集約関数Nullable<T>(int 下限,int 上限,Action<LinqDB.Sets.IEnumerable<T?>,Generic.IEnumerable<T?>> action)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
        for(var a=下限;a<上限;a++){
            var (s,e)=数列Nullable<T>(a);
            action(s,e);
        }
    }
    //[Fact]public void Aggregate(){
    //    //0,1,2,3,4,5,6,7,8,9
    //    //0+1=1,1+1=2,2+2=4,4+3=7
    //    var expected=数列.Aggregate((a,b)=>a+b);
    //    //expected[0].
    //    var actual=数列.Lookup(p=>p/10).ToArray().OrderBy(p=>p.Key).ToArray();
    //}
    [Fact]public void Aggregate_func(){
        //集約関数<sbyte  >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        //集約関数<short  >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        集約関数<int    >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        集約関数<long   >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        //集約関数<byte   >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        //集約関数<ushort >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        集約関数<uint   >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        集約関数<ulong  >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        集約関数<float  >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        集約関数<double >(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
        集約関数<decimal>(1,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate((a,b)=>a+b),()=>e.Aggregate((a,b)=>a+b)));
    }
    [Fact]public void Aggregate_seed_func(){
        集約関数<sbyte  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b),()=>e.Aggregate(1  ,(a,b)=>a+b)));
        集約関数<short  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b),()=>e.Aggregate(1  ,(a,b)=>a+b)));
        集約関数<int    >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b),()=>e.Aggregate(1  ,(a,b)=>a+b)));
        集約関数<long   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1L ,(a,b)=>a+b),()=>e.Aggregate(1L ,(a,b)=>a+b)));
        集約関数<byte   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b),()=>e.Aggregate(1  ,(a,b)=>a+b)));
        集約関数<ushort >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b),()=>e.Aggregate(1  ,(a,b)=>a+b)));
        集約関数<uint   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1U ,(a,b)=>a+b),()=>e.Aggregate(1U ,(a,b)=>a+b)));
        集約関数<ulong  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1UL,(a,b)=>a+b),()=>e.Aggregate(1UL,(a,b)=>a+b)));
        集約関数<float  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1f ,(a,b)=>a+b),()=>e.Aggregate(1f ,(a,b)=>a+b)));
        集約関数<double >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1d ,(a,b)=>a+b),()=>e.Aggregate(1d ,(a,b)=>a+b)));
        集約関数<decimal>(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1m ,(a,b)=>a+b),()=>e.Aggregate(1m ,(a,b)=>a+b)));
    }
    [Fact]public void Aggregate_seed_func_resultSelector(){
        集約関数<sbyte  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<short  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<int    >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<long   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1L ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1L ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<byte   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<ushort >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1  ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<uint   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1U ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1U ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<ulong  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1UL,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1UL,(a,b)=>a+b,ab=>ab*2)));
        集約関数<float  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1f ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1f ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<double >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1d ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1d ,(a,b)=>a+b,ab=>ab*2)));
        集約関数<decimal>(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.Aggregate(1m ,(a,b)=>a+b,ab=>ab*2),()=>e.Aggregate(1m ,(a,b)=>a+b,ab=>ab*2)));
    }
    //private void 集約関数<T>(T 下限,T 上限,Action<LinqDB.Sets.IEnumerable<T>,Generic.IEnumerable<T>> action)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
    //    for(var a=下限;a<上限;a++){
    //        var (s,e)=数列(a);
    //        action(s,e);
    //    }
    //}
    [Fact]public void All(){
        集約関数<sbyte  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<short  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<int    >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<long   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<byte   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<ushort >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<uint   >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<ulong  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<float  >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<double >(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<decimal>(0,10,(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        集約関数<char   >('a','z',(s,e)=>this.共通コンパイル実行 (()=>s.All(p=>p%2==0),()=>e.All(p=>p%2==0)));
        //void 共通<T>() where T :struct, IIncrementOperators<T>, IComparisonOperators<T,T,bool>, IAdditionOperators<T,T,T>, IMultiplyOperators<T,T,T>{
        //    T max=default;
        //    T seed=;
        //    seed++;
        //    for(var a=0;a<10;a++) max++;
        //    集約関数<T>(default,max,(s,e) => {
        //        this.共通コンパイル実行(() => s.Aggregate(seed,(a,b) => a+b,ab => ab*2),
        //            ()=>e.Aggregate(seed,(a,b) => a+b,ab => ab*2));
        //    });
        //}
    }
    [Fact]public void Any(){
        集約関数<sbyte  >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<short  >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<int    >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<long   >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<byte   >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<ushort >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<uint   >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<ulong  >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<float  >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<double >(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<decimal>(0  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
        集約関数<char   >('a','z',(s,e)=>this.共通コンパイル実行 (()=>s.Any(p=>p%2==0),()=>e.Any(p=>p%2==0)));
    }
    private void 共通コンパイル実行InvalidOperationException<T>(Expressions.Expression<Func<T>> input0,Expressions.Expression<Func<T>> input1)where T:struct,
        IIncrementOperators<T>,IComparisonOperators<T,T,bool>,IAdditionOperators<T,T,T>
    {
        var Optimizer = this.Optimizer;
        var 標準0 = input0.Compile();
        var 標準1 = input1.Compile();
        Assert.Throws<InvalidOperationException>(()=>標準0());
        Assert.Throws<InvalidOperationException>(()=>標準1());
        Optimizer.IsInline=true;
        var Del0=Optimizer.CreateDelegate(input0);
        var Del1=Optimizer.CreateDelegate(input1);
        Assert.Throws<InvalidOperationException>(()=>Del0());
        Assert.Throws<InvalidOperationException>(()=>Del1());
    }
    private static void 集約関数0<T>(Action<Sets.IEnumerable<T>,Generic.IEnumerable<T>> action)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
        集約関数(0,0,action);
    }
    [Fact]public void AverageNullable(){
        const int Count=10;
        //集約関数Nullable<sbyte  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        //集約関数Nullable<short  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        集約関数Nullable<int    >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        集約関数Nullable<long   >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        //集約関数Nullable<byte   >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        //集約関数Nullable<ushort >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        //集約関数Nullable<uint   >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        //集約関数Nullable<ulong  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        集約関数Nullable<float  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        集約関数Nullable<double >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        集約関数Nullable<decimal>(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
        //集約関数Nullable<char   >('a','z',(s,e)=>this.共通コンパイル実行 (()=>s.Average(),()=>e.Average()));
    }
    [Fact]public void Average0行(){
        //集約関数0<sbyte  >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        //集約関数0<short  >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        集約関数0<int    >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        集約関数0<long   >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        //集約関数0<byte   >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        //集約関数0<ushort >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        //集約関数0<uint   >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        //集約関数0<ulong  >((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),()=>e.Average()));
        集約関数0<float>((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),() => e.Average()));
        集約関数0<double>((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),() => e.Average()));
        集約関数0<decimal>((s,e) => this.共通コンパイル実行InvalidOperationException(() => s.Average(),() => e.Average()));
    }
    [Fact]public void Average(){
        //集約関数<sbyte  >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        //集約関数<short  >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        集約関数<int    >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        集約関数<long   >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        //集約関数<byte   >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        //集約関数<ushort >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        //集約関数<uint   >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        //集約関数<ulong  >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        集約関数<float  >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        集約関数<double >(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        集約関数<decimal>(1,10,(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
        //集約関数<char   >('a','z',(s,e) => this.共通コンパイル実行(() => s.Average(),()=>e.Average()));
    }
    [Fact]public void AverageNullable_selector(){
        const int Count=10;
        集約関数Nullable<sbyte  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<short  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<int    >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<long   >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<byte   >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<ushort >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<uint   >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        //集約関数Nullable<ulong  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<float  >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<double >(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<decimal>(Count ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数Nullable<char   >('a','z',(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
    }
    private void コンパイル実行<T>(Expressions.Expression<Func<T>> input0,Expressions.Expression<Func<T>> input1)where T:struct,
        IIncrementOperators<T>,IComparisonOperators<T,T,bool>,IAdditionOperators<T,T,T>
    {
        var Optimizer = this.Optimizer;
        var 標準0 = input0.Compile();
        var 標準1 = input1.Compile();
        var expected0=標準0();
        var expected1=標準1();
        Assert.Equal(expected0,expected1);
        Optimizer.IsInline=true;
        var Del0=Optimizer.CreateDelegate(input0);
        var Del1=Optimizer.CreateDelegate(input1);
        var actual0=Del0();
        var actual1=Del1();
        Assert.Equal(expected0,actual0);
        Assert.Equal(expected1,actual1);
    }
    [Fact]public void Average0_selector(){
        集約関数0<sbyte  >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<short  >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<int    >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<long   >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<byte   >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<ushort >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<uint   >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        //集約関数0<ulong  >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<float  >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<double >((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数0<decimal>((s,e)=>this.共通コンパイル実行InvalidOperationException (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
    }
    [Fact]public void Average_selector(){
        集約関数<sbyte  >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<short  >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<int    >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<long   >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<byte   >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<ushort >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<uint   >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        //集約関数<ulong  >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<float  >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<double >(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<decimal>(1  ,10 ,(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
        集約関数<char   >('a','z',(s,e)=>this.共通コンパイル実行 (()=>s.Average(p=>p+p),()=>e.Average(p=>p+p)));
    }
    Sets.IEnumerable<T> m<T>(LinqDB.Sets.IEnumerable<T> i)=>i;
    Generic.IEnumerable<T> m<T>(Generic.IEnumerable<T> i)=>i;
    [Fact]public void Cast(){
        集約関数<sbyte  >(1  ,10 ,(s,e)=>this.共通コンパイル実行(()=>s.Cast<object>().Cast<sbyte>(),()=>e.Cast<object>().Cast<sbyte>()));
        集約関数<sbyte  >(1  ,10 ,(s,e)=>this.共通コンパイル実行(()=>s.Cast<sbyte  >(),()=>e.Cast<sbyte  >()));
    }
    [Fact]public void Contains(){
        const int 最大値=10;
        共通<sbyte  >(最大値);
        共通<short  >(最大値);
        共通<int    >(最大値);
        共通<long   >(最大値);
        共通<byte   >(最大値);
        共通<ushort >(最大値);
        共通<uint   >(最大値);
        共通<ulong  >(最大値);
        共通<float  >(最大値);
        共通<double >(最大値);
        共通<decimal>(最大値);
        共通<char   >(最大値);
        void 共通<T>(int Count)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
            var 中央値=数値<T>(Count/2);
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.Contains(中央値),()=>e.Contains(中央値)));
            var はみ出し=数値<T>(Count);
            はみ出し++;
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.Contains(はみ出し),()=>e.Contains(はみ出し)));
        }
    }
    [Fact]public void DefaultIfEmptyNullable(){
        const int 最大値=10;
        共通<sbyte  >(最大値);
        共通<short  >(最大値);
        共通<int    >(最大値);
        共通<long   >(最大値);
        共通<byte   >(最大値);
        共通<ushort >(最大値);
        共通<uint   >(最大値);
        共通<ulong  >(最大値);
        共通<float  >(最大値);
        共通<double >(最大値);
        共通<decimal>(最大値);
        共通<char   >(最大値);
        void 共通<T>(int Count)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
            var a=数値<T>(9999);
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.DefaultIfEmpty(a),()=>e.DefaultIfEmpty(a)));
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.DefaultIfEmpty(),()=>e.DefaultIfEmpty()));
        }
    }
    [Fact]public void DefaultIfEmpty(){
        const int 最大値=10;
        共通<sbyte  >(最大値);
        共通<short  >(最大値);
        共通<int    >(最大値);
        共通<long   >(最大値);
        共通<byte   >(最大値);
        共通<ushort >(最大値);
        共通<uint   >(最大値);
        共通<ulong  >(最大値);
        共通<float  >(最大値);
        共通<double >(最大値);
        共通<decimal>(最大値);
        共通<char   >(最大値);
        void 共通<T>(int Count)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
            var a=数値<T>(9999);
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.DefaultIfEmpty(a),()=>e.DefaultIfEmpty(a)));
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.DefaultIfEmpty(),()=>e.DefaultIfEmpty()));
        }
    }
    [Fact]public void Lookup(){
        const int 最大値=10;
        共通<sbyte  >(最大値);
        共通<short  >(最大値);
        共通<int    >(最大値);
        共通<long   >(最大値);
        共通<byte   >(最大値);
        共通<ushort >(最大値);
        共通<uint   >(最大値);
        共通<ulong  >(最大値);
        共通<float  >(最大値);
        共通<double >(最大値);
        共通<decimal>(最大値);
        共通<char   >(最大値);
        void 共通<T>(int Count)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>,IMultiplyOperators<T,T,T>{
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>(Sets.IEnumerable <Sets.IGrouping<T,T>>)s.Lookup(p=>p),()=>e.Lookup(p=>p)));
            //集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>(Generic.IEnumerable <System.Linq.IGrouping<T,T>>)s.Lookup(p=>p),()=>(Generic.IEnumerable <System.Linq.IGrouping<T,T>>)e.Lookup(p=>p)));
        }
    }
    //private Sets.IEnumerable<T> データ<T>(int 下限,int 上限)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
    //    T 下限0=default,上限0=default;
    //    for(var a=0;a<下限;a++) 下限0++;
    //    for(var a=0;a<上限;a++) 上限0++;
    //    for(var a=下限0;a<上限0;a++){
    //        var (s0,e0)=数列<T>(a);
    //        action(s0,e0);
    //    }
    //}
    //private void 集約関数2<T>(int 下限,int 上限,Action<LinqDB.Sets.IEnumerable<T>,Generic.IEnumerable<T>> action)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>{
    //    T 下限0=default,上限0=default;
    //    for(var a=0;a<下限;a++) 下限0++;
    //    for(var a=0;a<上限;a++) 上限0++;
    //    for(var a=下限0;a<上限0;a++){
    //        var (s0,e0)=数列<T>(a);
    //        action(s0,e0);
    //    }
    //}
    //[Fact]public void DUnion()
    [Fact]public void Except(){
        const int 最大値=10;
        共通<sbyte  >(最大値);
        共通<short  >(最大値);
        共通<int    >(最大値);
        共通<long   >(最大値);
        共通<byte   >(最大値);
        共通<ushort >(最大値);
        共通<uint   >(最大値);
        共通<ulong  >(最大値);
        共通<float  >(最大値);
        共通<double >(最大値);
        共通<decimal>(最大値);
        共通<char   >(最大値);
        void 共通<T>(int Count)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>,IMultiplyOperators<T,T,T>{
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.Except(s),()=>e.Except(e)));
        }
    }
    [Fact]public void GroupBy_keySelector_elementSelector(){
        const int 最大値=10;
        共通<sbyte  >(最大値);
        共通<short  >(最大値);
        共通<int    >(最大値);
        共通<long   >(最大値);
        共通<byte   >(最大値);
        共通<ushort >(最大値);
        共通<uint   >(最大値);
        共通<ulong  >(最大値);
        共通<float  >(最大値);
        共通<double >(最大値);
        共通<decimal>(最大値);
        共通<char   >(最大値);
        void 共通<T>(int Count)where T:struct,IIncrementOperators<T>,IComparisonOperators<T,T,bool>,IDivisionOperators<T,T,T>{
            集約関数<T>(0,Count,(s,e)=>{
                if(s.LongCount==2){
                    var Set_IGrouping1=s.GroupBy(p=>++p);//.Select(p=>p.Key);
                    var Enumerable_IGrouping2=e.GroupBy(p=>++p);
                    var Array_IGrouping1 = Set_IGrouping1.ToArray();
                    var Array_IGrouping2 = Enumerable_IGrouping2.ToArray();
                    for(var a = 0;a<Array_IGrouping1.Length;a++) {
                        Sets.IGrouping<T,T> IGrouping1 = Array_IGrouping1[a];
                        System.Linq.IGrouping<T,T> IGrouping2 = Array_IGrouping2[a];
                        var IGrouping1_Key = IGrouping1.Key;
                        var IGrouping2_Key = IGrouping2.Key;
                        var IGrouping1_ToArray = IGrouping1.ToArray();
                        foreach(var b in IGrouping1) {

                        }
                        var IGrouping2_ToArray = IGrouping2.ToArray();
                        for(var b = 0;b<IGrouping1_ToArray.Length;b++) {
                            var Element1 = IGrouping1_ToArray[b];
                            var Element2 = IGrouping2_ToArray[b];
                            //Assert.True(s2,e2);
                        }
                    }
                    Assert.True(Set_IGrouping1.Equals(Enumerable_IGrouping2));
                    Assert.Equal(Set_IGrouping1,Enumerable_IGrouping2);
                }
            });
            集約関数<T>(0,Count,(s,e)=>this.共通コンパイル実行(()=>s.GroupBy(p=>p),()=>e.GroupBy(p=>p)));
        }
    }
}
