using LinqDB.Sets;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
public class 変換_WhereからLookup:共通{
    struct OI{
        public readonly int o,i;
        public OI(int o,int i){
            this.o=o;
            this.i=i;
        }
    }
    class SetT:Set<OI>{

    }
    [Fact]public void Call(){
        var s = new Set<int>();
        var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
        //if(ループ展開可能メソッドか(MethodCall0)) {
        //    if(nameof(Sets.ExtensionSet.Where)==MethodCall0_Method.Name) {
        //        if(this.判定_指定Parameters無.実行(MethodCall1_Arguments_0,this.外側Parameters!)&&MethodCall1_Arguments_1 is LambdaExpression predicate) {
        //            if(OuterPredicate is not null) {
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //            }
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==0));
        //            if(Listプローブビルド_Count>0) {
        //                if(Listプローブビルド_Count==1) {
        //                    while(true) {
        //                        if(Set1 is null) {
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //                        }
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //                        var GenericTypeDefinition = Set1;
        //                        if(GenericTypeDefinition.IsGenericType) {
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //                        }
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==0));
        //                        if(GenericTypeDefinition==typeof(Set<,>)) {
        //                            if() {
        this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.Equals(o.Key))));
        //                            } else {
        this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.Equals(o.Key.メンバー))));
        //                            }
        //                        }
        //                    }
        //                } else {
        this.共通コンパイル実行(() => s.SelectMany(o=>s.Where(i=>o==i&&i+1==o+1)));
        //                }
        //                Expression LookupExpression(Expression プローブ,Expression ビルド) {
        //                    var Lookup = typeof(Sets.ExtensionSet)==MethodCall0_Method.DeclaringType
        //                        ? ExtensionSet.Lookup
        this.共通コンパイル実行(() => s.SelectMany(o=>s.Where(i=>o==i&&i+1==o+1)));
        //                        : ExtensionEnumerable.Where==MethodCall0_Method.GetGenericMethodDefinition()
        //                            ? ExtensionEnumerable.Lookup
        this.共通コンパイル実行(() => s.SelectMany(o=>new int[3].Where(i=>o==i&&i+1==o+1)));
        //                            : ExtensionEnumerable.Lookup_index;
        this.共通コンパイル実行(() => s.SelectMany(o=>new int[3].Where((i,index)=>o==i&&i+1==o+1&&index==3)));
        //                    var GetValue = 作業配列.GetMethod(Instance.Type,nameof(LookupList<int,int>.GetTKeyValue),プローブ.Type);
        ////                    if(GetValue is null) {
        //this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.Equals(o.Key))));
        ////                    } else {
        //this.共通コンパイル実行(() => s.SelectMany(o=>s.Where(i=>o==i&&i+1==o+1)));
        ////                    }
        this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.Equals(o.Key))));
        this.共通コンパイル実行(() => s.SelectMany(o=>s.Where(i=>o==i&&i+1==o+1)));
        //                }
        //            }
        this.共通コンパイル実行(()=>s.Where(p=>p==0));
        //            if(InnerPredicate is not null){
        //                if(this.判定_指定Parameters無.実行(InnerPredicate,predicate_Parameters)) {
        //                    if(typeof(Sets.ExtensionSet)==MethodCall0.Method.DeclaringType) {
        this.共通コンパイル実行(() => s.Where(p => true));
        //                    } else {
        this.共通コンパイル実行(()=>new int[1].Where(p => true));
        //                    }
        //                }
        //            }
        this.共通コンパイル実行(()=>s.Where(p=>p==0));
        //        }
        this.共通コンパイル実行(()=>st.SelectMany(o=>s.Let(i=>new{o,i}).i.Where(i=>i==3)));
        this.共通コンパイル実行(()=>st.SelectMany(o=>s.Where((Func<int,bool>)(i=>i==3))));
        //    }
        this.共通コンパイル実行(()=>st.Select(o=>new{o}));
        //}
        this.共通コンパイル実行(()=>st.Let(o=>new{o}));
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通コンパイル実行(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
