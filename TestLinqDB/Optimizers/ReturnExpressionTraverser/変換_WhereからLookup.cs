using LinqDB.Sets;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_WhereからLookup : 共通{
    //protected override テストオプション テストオプション{get;}=テストオプション.式木の最適化を試行|テストオプション.インライン|テストオプション.プロファイラ|テストオプション.ローカル実行;
    //protected override テストオプション テストオプション{get;}=テストオプション.式木の最適化を試行;
    struct OI
    {
        public readonly int o, i;
        public OI(int o, int i)
        {
            this.o=o;
            this.i=i;
        }
    }
    class SetT : Set<OI>
    {

    }
    private static readonly Set<int>s = new(){ 1, 2, 3 };
    private static readonly Set<Sets.Key, Sets.Value>st = new(){ new(new(0)), new(new(1)) };
    [Fact]
    public void Call(){
        //if(ループ展開可能メソッドか(MethodCall0)) {
        //    if(nameof(Sets.ExtensionSet.Where)==MethodCall0_Method.Name) {
        //        if(this.判定_指定Parameters無.実行(MethodCall1_Arguments_0,this.外側Parameters!){
        //            if(MethodCall1_Arguments_1 is LambdaExpression predicate){
        //                if(OuterPredicate is not null) 
        this.Expression実行AssertEqual(()=> s.Where(p=>p==0));//0
        //                if(Listプローブビルド_Count>0) {
        //                    if(Listプローブビルド_Count==1) {
        //                        while(true) {
        //                            if(Set1 is null) {
        this.Expression実行AssertEqual(()=> s.SelectMany(o=> s.Where(i=>i.Equals(o))));
        //                            }
        //                            if(GenericTypeDefinition.IsGenericType)
        this.Expression実行AssertEqual(()=> st.SelectMany(o=> st.Where(i=>i.Key.Equals(o.Key))));
        //                            if(GenericTypeDefinition==typeof(Set<,>)) {
        //                                if(
        //                                    ビルド is MemberExpression Member&&
        //                                    Member.Member.DeclaringType!.IsGenericType&&
        //                                    Member.Member.DeclaringType.GetGenericTypeDefinition()==typeof(Entity<,>)&&
        //                                    Member.Member.Name==nameof(IKey<int>.Key)
        //                                    //Member.Member==Member.Expression!.Type.GetProperty(nameof(IKey<int>.Key))&&
        //                                    //Set1.GetGenericArguments()[0].GetProperty(nameof(IKey<int>.Key))==Member.Member
        //                                ) {
        //0
    }
    [Fact]public void Call01(){
        //                                } else {
        this.Expression実行AssertEqual(()=> st.SelectMany(o=> st.Where(i=>i.Key.メンバー11.Equals(o.Key.メンバー11))));//1
        //                                }
        //                                break;
        //                            }
        //                            Set1=Set1.BaseType;
        //                        }
    }
    [Fact]public void Call02(){
        //                    } else {
        this.Expression実行AssertEqual(()=> s.SelectMany(o=> s.Where(i=>o==i&&i+1==o+1)));
        //                    }
        //                    Expression LookupExpression(Expression プローブ,Expression ビルド,Expression?Comparer0){
        //                        MethodInfo Lookup;
        //                        if(typeof(Sets.ExtensionSet)==MethodCall0_Method.DeclaringType){
        //1
        //                        } else if(ExtensionEnumerable.Where==MethodCall0_Method.GetGenericMethodDefinition()){
        this.Expression実行AssertEqual(()=> s.SelectMany(o=>new int[3].Where(i=>o==i&&i+1==o+1)));//2
        //                        } else{
        this.Expression実行AssertEqual(()=> s.SelectMany(o=>new int[3].Where((i,index)=>o==i&&i+1==o+1&&index==0)));//3
    }
    [Fact]public void Call1(){
        //                        }
        //                    }
        //                }
        //                if(InnerPredicate is not null){
        //                    if(this.判定_指定Parameters無.実行(InnerPredicate,predicate_Parameters)) {
        //                        if(typeof(Sets.ExtensionSet)==MethodCall0.Method.DeclaringType) {
        this.Expression実行AssertEqual(()=>CreateSet().Except(CreateSet()).Where(p=>true));
        //                        } else {
        this.Expression実行AssertEqual(() => new int[1].Where(p => true));
        //                        }
        //                    }
    }
    [Fact]public void Call2(){
        this.Expression実行AssertEqual(() => s.SelectMany(o => new int[]{0,1,2,3}.Where((i, index) => index==o)));
        //                }
        this.Expression実行AssertEqual(() => s.SelectMany(o => new int[3].Where((i, index) => o==0)));
        //            }
        this.Expression実行AssertEqual(() => new int[1].Where(Anonymous((int p) => true)));
        //        }
        this.Expression実行AssertEqual(() => s.Let(o => o.Where((i) => true)));
        //    }
        this.Expression実行AssertEqual(() => new int[1].Select(p => true));
        //}
        this.Expression実行AssertEqual(() => "".ToString());
    }
    //public static System.Collections.Generic.IEnumerable<int> Impl_Method1()
    //{
    //    SetGroupingList<int, int> setGroupingList = new SetGroupingList<int, int>();
    //    LinqDB.Optimizers.ReturnExpressionTraverser.変換_インラインループ.SZArrayEnumerator<int> sZArrayEnumerator = new LinqDB.Optimizers.ReturnExpressionTraverser.変換_インラインループ.SZArrayEnumerator<int>(new int[4] { 0, 1, 2, 3 });
    //    while (sZArrayEnumerator.MoveNext())
    //    {
    //        int current = sZArrayEnumerator.Current;
    //        setGroupingList.AddKeyValue(key, current);
    //    }
    //    System.Linq.ILookup<int, int> val = setGroupingList;
    //    LinqDB.Enumerables.List<int> list = new LinqDB.Enumerables.List<int>();
    //    ImmutableSet<int>.Enumerator enumerator = s.GetEnumerator();
    //    while (enumerator.MoveNext())
    //    {
    //        IEnumerator<int> enumerator2 = val[enumerator.Current].GetEnumerator();
    //        while (enumerator2.MoveNext())
    //        {
    //            list.Add(enumerator2.Current);
    //        }
    //    }
    //    return list;
    //}
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通MemoryMessageJson_Expression_コンパイル実行(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通MemoryMessageJson_Expression_コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通MemoryMessageJson_Expression_コンパイル実行(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
