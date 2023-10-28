using System.Collections;
using System.Reflection;
using LinqDB.Sets;
using IEnumerable=System.Collections.IEnumerable;
namespace TestLinqDB;
file class コンパイラが生成したIEnumerable1継承クラス:System.Collections.Generic.IEnumerable<double>,System.Collections.Generic.IEnumerable<int>{
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
public abstract class 特定パターン<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected 特定パターン():base(new AssertDefinition(new TSerializer())){}
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
        this.AssertEqual(input);
    }
    [Fact]public void コンパイラが生成したIEnumerable1継承クラスシリアライズ(){
        //System.Collections.Generic.IEnumerable<int> input=new コンパイラが生成したIEnumerable1継承クラス();
        var input=new コンパイラが生成したIEnumerable1継承クラス();
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
        this.AssertEqual(input);
    }
    [Fact]public void UnionBy結果シリアライズ(){
        var a=new[]{3,5,7};
        var b=new[]{4,6,8};
        var input=a.UnionBy(b,x=>x+1);
        var g=input.GetType().GetMethod("GetEnumerator",BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
        this.AssertEqual(input);
    }
    [Fact]public void IEnumerableAnonymous(){
        var st=Helpers.ToSet(new[]{3,5,7});
        this.ExpressionAssertEqual(()=>st.Select(o=>new{o}));
    }
}