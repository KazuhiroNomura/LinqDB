using LinqDB.Enumerables;
using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters;
public class FormatterResolver : 共通
{
    //protected override テストオプション テストオプション=>テストオプション.MemoryPack_MessagePack_Utf8Json;
    private static void M(){}
    private class C<T>{
        public T M;
        public C(T M)=>this.M=M;
    }
    [Fact]
    public void GetFormatter0(){
        //if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter))return(IJsonFormatter<T>)Formatter;
        //this.ObjectシリアライズAssertEqual(new Set<Tables.Table>());
        //if(type.IsDisplay())return Return(Formatters.Others.DisplayClass<T>.Instance);
        //if(type.IsArray){
        this.ObjectシリアライズAssertEqual(new Tables.Table[10]);
        //}
        //if(type.IsAnonymous())
        this.ObjectシリアライズAssertEqual(new { a = 1 });
        //if(type.IsDisplay())//classしか想定してない。structはローカル関数のキャプチャ。ジェネリックstructはローカル関数の親関数がジェネリック関数だった場合
        //{
        //    var x=ClassDisplay取得();
        //    var C=typeof(ValueTuple<>).MakeGenericType(x.GetType());
        //    var ctor=C.GetConstructors()[0];
        //    var y=Activator.CreateInstance(
        //        C,new object[]{x}
        //    );
        //    this.ObjectシリアライズAssertEqual(y);
        //}
        //this.ObjectシリアライズAssertEqual(ClassDisplay取得());
        //if(typeof(Delegate).IsAssignableFrom(type))
        this.ObjectシリアライズAssertEqual(new{a=(Action)M,b=(Action)M});
        this.ObjectシリアライズAssertEqual((Action)M);
        //if(type.IsGenericType) {
        //    foreach(var GenericArgument in type.GetGenericArguments())GetFormatterDynamic(GenericArgument);
        //    if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))
        this.ExpressionシリアライズAssertEqual(
            Q.Expressions.Expression.Lambda<Func<int>>(
                Q.Expressions.Expression.Constant(1)
            )
        );
        //    if(type.IsInterface){
        //        if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new Set<int>().GroupBy(p => p).SingleOrDefault());
        //        if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)return Return(Formatter);
        this.ObjectシリアライズAssertEqual((Q.IGrouping<int, int>)new Set<int>().GroupBy(p => p).SingleOrDefault());
        //        if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)return Return(Formatter);
        this.ObjectシリアライズAssertEqual((S.IEnumerable<int>)new Set<int>());//0
        //        Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >));
        this.ObjectシリアライズAssertEqual((E.IEnumerable<int>)new Set<int>());
        //    }else{
        //        if((Formatter=RegisterType(type,typeof(Enumerables.GroupingList<, >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new S.Grouping<int, int>(1));
        this.ObjectシリアライズAssertEqual(new LinqDB.Enumerables.Grouping<int, int>(1));
        //        if((Formatter=RegisterType(type,typeof(Sets.GroupingSet        <, >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new S.Grouping<int, int>(1));
        //        if((Formatter=RegisterType(type,typeof(Sets.SetGroupingList    <, >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new LinqDB.Enumerables.Lookup<int, int>());
        //        if((Formatter=RegisterType(type,typeof(Sets.SetGroupingSet     <, >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new SetGroupingSet<int, int>());
        //        if((Formatter=RegisterType(type,typeof(Sets.Set                <,,>))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new Set<Keys.Key, Tables.Table, LinqDB.Databases.Container>());
        //        if((Formatter=RegisterType(type,typeof(Sets.Set                <, >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new Set<Keys.Key, Tables.Table>());
        //        if((Formatter=RegisterType(type,typeof(Sets.Set                <  >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new Set<int>());//0
        //        if((Formatter=RegisterType(type,typeof(Enumerables.List        <  >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new LinqDB.Enumerables.List<int>());
        //        if((Formatter=RegisterType(type,typeof(Sets.HashSet            <  >))) is not null) return Return(Formatter);
        this.ObjectシリアライズAssertEqual(new LinqDB.Sets.HashSet<int>());
        var a = new[] { 3, 5, 7 };
        this.ObjectシリアライズAssertEqual(a.UnionBy(a, x => x+1));
        this.ObjectシリアライズAssertEqual(a.UnionBy(a, x => x+1));//同じものを2つやればMessagePack.FormatterResolver.GetFormatterでキャッシュされる
        //    }
        //}
    }
    //[Fact]
    //public void GetFormatter1()
    //{
    //    //            if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <, >)))is not null)return Formatter_T;
    //    this.ObjectシリアライズAssertEqual(new Set<Keys.Key, Tables.Table>());
    //    //            if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <  >)))is not null)return Formatter_T;
    //    this.ObjectシリアライズAssertEqual(new Set<Tables.Table>());
    //    //            do{
    //    //                if(type0.BaseType is null) return default!;
    //    //            }while(!type0.IsGenericType);
    //    //        } while(typeof(object)!=type0);
    //    //    }            
    //    //this.MemoryMessageJson_Assert(new{a=1);
    //    //this.MemoryMessageJson_Assert(new LinqDB.Sets.Set<Tables.Table>();
    //    //this.MemoryMessageJson_Assert(new{a=(S.IEnumerable<Tables.Table>)new S.Set<Tables.Table>());
    //}
    [Fact]
    public void GetFormatter_RegisterInterface()
    {
        var GroupBy = new Set<int>().GroupBy(p => p);
        //if(type0.IsGenericType&&type0.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
        this.ObjectシリアライズAssertEqual(GroupBy.SingleOrDefault());
        //}
        this.ObjectシリアライズAssertEqual((S.IEnumerable<Tables.Table>)new Set<Tables.Table>());
        //foreach(var Interface in type.GetInterfaces()){
        //    if(!Interface.IsGenericType)continue;
        //    if(Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
        this.ObjectシリアライズAssertEqual(new Set<Tables.Table>().GroupBy(p => p).SingleOrDefault());
        //    }
        this.ObjectシリアライズAssertEqual((E.IEnumerable<Tables.Table>)new Set<Tables.Table>());
        //}
        this.ObjectシリアライズAssertEqual((Q.IGrouping<int, int>)GroupBy.SingleOrDefault());
        this.ObjectシリアライズAssertEqual(ValueTuple.Create<Q.IGrouping<int, int>>(GroupBy.SingleOrDefault()));
    }
}
