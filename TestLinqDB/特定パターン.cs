using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TestLinqDB;
public class 特定パターン:共通{
    private class コンパイラが生成したIEnumerable1継承クラス:IEnumerable<double>,IEnumerable<int>{
        IEnumerator<double> IEnumerable<double>.GetEnumerator(){
            for(var a=0;a<10;a++) yield return a;
        }
        public IEnumerator<int> GetEnumerator(){
            for(var a=0;a<10;a++) yield return a;
        }
        IEnumerator IEnumerable.GetEnumerator(){
            return this.GetEnumerator();
        }
    }
    private class コンパイラが生成したIEnumerable1明示的実装継承クラス:IEnumerable<int> {
        IEnumerator<int> IEnumerable<int>.GetEnumerator() {
            for(var a=0;a<10;a++) yield return a;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            for(var a=0;a<10;a++) yield return a;
        }
    }
    [Fact]public void コンパイラが生成したIEnumerable1継承クラスシリアライズ(){
        IEnumerable<int> input=new コンパイラが生成したIEnumerable1継承クラス();
        var ty=input.GetType().GetInterfaces();
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
    [Fact]public void UnionBy結果シリアライズ(){
        var a=new[]{3,5,7};
        var b=new[]{4,6,8};
        var input=a.UnionBy(b,x=>x+1);
        var g=input.GetType().GetMethod("GetEnumerator",BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}