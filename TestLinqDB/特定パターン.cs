using System.Collections;
using System.Reflection;
using LinqDB.Sets;
using IEnumerable=System.Collections.IEnumerable;
namespace TestLinqDB;
public class 特定パターン:共通{
    private class コンパイラが生成したIEnumerable1継承クラス:System.Collections.Generic.IEnumerable<double>,System.Collections.Generic.IEnumerable<int>{
        IEnumerator<double> System.Collections.Generic.IEnumerable<double>.GetEnumerator(){
            for(var a=0;a<10;a++) yield return a;
        }
        public IEnumerator<int> GetEnumerator(){
            for(var a=0;a<10;a++) yield return a;
        }
        IEnumerator IEnumerable.GetEnumerator(){
            return this.GetEnumerator();
        }
    }
    private class コンパイラが生成したIEnumerable1明示的実装継承クラス:System.Collections.Generic.IEnumerable<int> {
        IEnumerator<int> System.Collections.Generic.IEnumerable<int>.GetEnumerator() {
            for(var a=0;a<10;a++) yield return a;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            for(var a=0;a<10;a++) yield return a;
        }
    }
    [Fact]public void IEnumerableInt32継承シリアライズ(){
        //System.Collections.Generic.IEnumerable<int> input=new コンパイラが生成したIEnumerable1継承クラス();
        System.Collections.Generic.IEnumerable<int>input=new []{1,2,3};
        //現状
        //ListをIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //今後
        //ListをIEnumerableでシリアライズするとIEnumerableの内部でListでデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        var g=typeof(コンパイラが生成したIEnumerable1明示的実装継承クラス).GetMethod("GetEnumerator",BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
    [Fact]public void コンパイラが生成したIEnumerable1継承クラスシリアライズ(){
        //System.Collections.Generic.IEnumerable<int> input=new コンパイラが生成したIEnumerable1継承クラス();
        var input=new コンパイラが生成したIEnumerable1継承クラス();
        System.Collections.Generic.IEnumerable<double> x=input;
        System.Collections.Generic.IEnumerable<int> y=input;
        //現状
        //ListをIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //今後
        //ListをIEnumerableでシリアライズするとIEnumerableの内部でListでデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
    [Fact]public void UnionBy結果シリアライズ(){
        var a=new[]{3,5,7};
        var b=new[]{4,6,8};
        var input=a.UnionBy(b,x=>x+1);
        var g=input.GetType().GetMethod("GetEnumerator",BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
    [Fact]public void IEnumerableAnonymous(){
        var st=Helpers.ToSet(new[]{3,5,7});
        this.MemoryMessageJson_Expression_Assert全パターン(()=>st.Select(o=>new{o}));
    }
    [Fact]
    public void インターフェースにキャスト(){
        dynamic a=new List<int>();
        System.Collections.Generic.IEnumerable<int> b=a;
        //System.Collections.GenericIEnumerable<double> c=a;
        static System.Collections.Generic.IEnumerable<T> 共通<T>(System.Collections.Generic.IEnumerable<T> i){
            return i;
        }
    }
}