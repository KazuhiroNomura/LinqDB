using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Generic.Formatters;
public abstract class FormatterResolver:共通{
    protected FormatterResolver(テストオプション テストオプション):base(テストオプション){}
    [Fact]public void GetFormatter0(){
        //if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter))return(IJsonFormatter<T>)Formatter;
        this.AssertEqual(new Set<Tables.Table>());
        //if(type.IsDisplay())return Return(Formatters.Others.DisplayClass<T>.Instance);
        this.AssertEqual(ClassDisplay取得());
        //if(type.IsArray) {
        this.AssertEqual(new Tables.Table[10]);
        //}else if(type.IsGenericType) {
        //    if(type.IsAnonymous()) {
        this.AssertEqual(new{a=1});
        //    } else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)) {
        this.AssertEqual(
            System.Linq.Expressions.Expression.Lambda<Func<int>>(
                System.Linq.Expressions.Expression.Constant(1)
            )
        );
        //    }else if(type.IsInterface){
        //        IJsonFormatter<T>?Formatter_T;
        //        if((Formatter_T=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)return Formatter_T;
        this.AssertEqual(new Set<int>().GroupBy(p=>p).SingleOrDefault());
        //        if((Formatter_T=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)return Formatter_T;
        this.AssertEqual((System.Linq.IGrouping<int,int>)new Set<int>().GroupBy(p=>p).SingleOrDefault());
        //        if((Formatter_T=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)return Formatter_T;
        this.AssertEqual((LinqDB.Sets.IEnumerable<int>)new Set<int>());
        //        if((Formatter_T=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null)return Formatter_T;
        this.AssertEqual((System.Collections.Generic.IEnumerable<int>)new Set<int>());
        //        do{
        //            if((Formatter_T=RegisterType(type0,typeof(Enumerables.GroupingList<, >)))is not null)return Formatter_T;
        this.AssertEqual(new GroupingSet<int,int>(1));
        this.AssertEqual(new LinqDB.Enumerables.GroupingList<int,int>(1));
        //            if((Formatter_T=RegisterType(type0,typeof(Sets.GroupingSet        <, >)))is not null)return Formatter_T;
        this.AssertEqual(new GroupingSet<int,int>(1));
        //            if((Formatter_T=RegisterType(type0,typeof(Sets.SetGroupingList    <, >)))is not null)return Formatter_T;
        this.AssertEqual(new SetGroupingList<int,int>());
        //            if((Formatter_T=RegisterType(type0,typeof(Sets.SetGroupingSet     <, >)))is not null)return Formatter_T;
        this.AssertEqual(new SetGroupingSet<int,int>());
        //            if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <,,>)))is not null)return Formatter_T;
        this.AssertEqual(new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>());
    }
    [Fact]public void GetFormatter1(){
        //            if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <, >)))is not null)return Formatter_T;
        this.AssertEqual(new Set<Keys.Key,Tables.Table>());
        //            if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <  >)))is not null)return Formatter_T;
        this.AssertEqual(new Set<Tables.Table>());
        //            do{
        //                if(type0.BaseType is null) return default!;
        //            }while(!type0.IsGenericType);
        //        } while(typeof(object)!=type0);
        //    }            
        //this.MemoryMessageJson_Assert(new{a=1);
        //this.MemoryMessageJson_Assert(new LinqDB.Sets.Set<Tables.Table>();
        //this.MemoryMessageJson_Assert(new{a=(S.IEnumerable<Tables.Table>)new S.Set<Tables.Table>());
    }
    [Fact]
    public void GetFormatter_RegisterInterface(){
        var GroupBy=new Set<int>().GroupBy(p=>p);
        //if(type0.IsGenericType&&type0.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
        this.AssertEqual(GroupBy.SingleOrDefault());
        //}
        this.AssertEqual((S.IEnumerable<Tables.Table>)new Set<Tables.Table>());
        //foreach(var Interface in type.GetInterfaces()){
        //    if(!Interface.IsGenericType)continue;
        //    if(Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
        this.AssertEqual(new Set<Tables.Table>().GroupBy(p => p).SingleOrDefault());
        //    }
        this.AssertEqual((E.IEnumerable<Tables.Table>)new Set<Tables.Table>());
        //}
        this.AssertEqual((Q.IGrouping<int,int>)GroupBy.SingleOrDefault());
        this.AssertEqual(ValueTuple.Create<Q.IGrouping<int,int>>(GroupBy.SingleOrDefault()));
    }
}
