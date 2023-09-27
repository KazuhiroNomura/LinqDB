using LinqDB.Helpers;
using LinqDB.Sets;

using System.Collections;
using System.Text;
using LinqDB.Databases;
using Newtonsoft.Json.Linq;
using System.Linq;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Serializers.Formatters;
namespace Sets;


public class ExtensionEnumerable:共通{
    private const int Count=100;
    private static Random r=new Random(1);
    private static Set<int>IntSet(int Count){
        var value=new Set<int>();
        for(var a=0;a<Count;a++) value.Add(r.Next(Count));
        return value;
    }
    private static Set<double?>DoubleNullableSet(int Count){
        var value=new Set<double?>();
        for(var a=0;a<Count;a++)
            if(r.Next(10)==0)value.Add(null);
            else value.Add(r.NextDouble());
        return value;
    }
    private static Set<double>DoubleSet(int Count){
        var value=new Set<double>();
        for(var a=0;a<Count;a++) value.Add(r.NextDouble());
        return value;
    }
    private static Set<decimal?>DecimalNullableSet(int Count){
        var value=new Set<decimal?>();
        for(var a=0;a<Count;a++)
            if(r.Next(10)==0)value.Add(null);
            else value.Add((decimal)r.NextDouble());
        return value;
    }
    private static Set<decimal>DecimalSet(int Count){
        var value=new Set<decimal>();
        for(var a=0;a<Count;a++) value.Add((decimal)r.NextDouble());
        return value;
    }
    [Fact]public void All(){
        const double 割合=.9;
        var true数=0;
        for(var a=1;a<Count;a++){
            var s=IntSet(a);
            var e=s.AsEnumerable();
            var 中央値=(int)(a*割合);
            var expected=e.All(p=>p<中央値);
            var actual=s.All(p=>p<中央値);
            Assert.Equal(expected,actual);
            if(actual) true数++;
        }
    }
    [Fact]public void Any(){
        var true数=0;
        for(var a=1;a<Count;a++){
            var s=IntSet(a);
            var e=s.AsEnumerable();
            var 中央値=a/2;
            var expected=e.Any(p=>p<中央値);
            var actual=s.Any(p=>p<中央値);
            Assert.Equal(expected,actual);
            if(actual) true数++;
        }
    }
    [Fact]public void AverageDecimal(){
        for(var a=1;a<Count;a++){
            var s=DecimalSet(a);
            var e=s.AsEnumerable();
            var expected=e.Average();
            var actual=s.Average();
            Assert.Equal(expected,actual);
        }
    }
    [Fact]public void AverageDouble(){
        for(var a=1;a<Count;a++){
            var s=DoubleSet(a);
            var e=s.AsEnumerable();
            var expected=e.Average();
            var actual=s.Average();
            Assert.Equal(expected,actual);
        }
    }
    [Fact]public void AverageSingle(){
        for(var a=1;a<Count;a++){
            var s=DoubleSet(a).Select(p=>(float)p);
            var e=s.AsEnumerable();
            var expected=e.Average();
            var actual=s.Average();
            Assert.Equal(expected,actual);
        }
    }
    [Fact]public void AverageInt32(){
        for(var a=1;a<Count;a++){
            var s=IntSet(a);
            var e=s.AsEnumerable();
            var expected=e.Average();
            var actual=s.Average();
            Assert.Equal(expected,actual);
        }
    }
    [Fact]public void AverageDoubleNullable(){
        for(var a=0;a<Count;a++){
            var s=DoubleNullableSet(a);
            var e=s.AsEnumerable();
            var expected=e.Average();
            var actual=s.Average();
            Assert.Equal(expected,actual);
        }
    }
    [Fact]public void AverageDecimalNullable(){
        for(var a=0;a<Count;a++){
            var s=DecimalNullableSet(a);
            var e=s.AsEnumerable();
            var expected=e.Average();
            var actual=s.Average();
            Assert.Equal(expected,actual);
        }
    }
    //[Fact]public void AverageDoubleNullable(){
    //    for(var a=0;a<Count;a++){
    //        var s=DoubleNullableSet(a);
    //        var e=s.AsEnumerable();
    //        var expected=e.Average(p=>p*2);
    //        var actual=s.Average(p=>p*2);
    //        Assert.Equal(expected,actual);
    //    }
    //}
    //[Fact]public void AverageDouble(){
    //    for(var a=1;a<Count;a++){
    //        var s=DoubleSet(a);
    //        var e=s.AsEnumerable();
    //        var expected=e.Average(p=>p*2);
    //        var actual=s.Average(p=>p*2);
    //        Assert.Equal(expected,actual);
    //    }
    //}
    //[Fact]public void AverageDecimalNullable(){
    //    for(var a=0;a<Count;a++){
    //        var s=DecimalNullableSet(a);
    //        var e=s.AsEnumerable();
    //        var expected=e.Average(p=>p*2);
    //        var actual=s.Average(p=>p*2);
    //        Assert.Equal(expected,actual);
    //    }
    //}
    //[Fact]public void AverageDecimal(){
    //    for(var a=1;a<Count;a++){
    //        var s=DecimalSet(a);
    //        var e=s.AsEnumerable();
    //        var expected=e.Average(p=>p*2);
    //        var actual=s.Average(p=>p*2);
    //        Assert.Equal(expected,actual);
    //    }
    //}
}