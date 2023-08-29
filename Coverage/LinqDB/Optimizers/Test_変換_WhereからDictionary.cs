using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_WhereからDictionary : ATest
{
    [TestMethod]
    public void Call()
    {
        //if((Setか = Reflection.ExtendSet.Where == MethodCall0_GenericMethodDefinition) || Reflection.ExtendEnumerable.Where == MethodCall0_GenericMethodDefinition) {
        //    while(BaseType != null) {
        //        if(GenericTypeDifinition.IsGenericType)
        this.実行結果が一致するか確認(() => KeySet変数().Where(p => p.ID1 == 1 && p.ID3 == 2));
        this.実行結果が一致するか確認(() => KeySet変数().Where(p => p.ID1 == 1));
        this.実行結果が一致するか確認(() => KeySet変数().Where(p => p.ID3 == 2));
        //    }
        this.実行結果が一致するか確認(a => ArrN<int>(a).Where(p => p == 1));
        this.実行結果が一致するか確認(a => EnuN<int>(a).Where(p => p == 1));
        this.実行結果が一致するか確認(a => SetN<int>(a).Where(p => p == 1));
        //    if(ListHashDictionaryEqual_Count>0){
        //        if(ListHashDictionaryEqual_Count==1) {
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => c == b)));
        this.実行結果が一致するか確認(a => Lambda(b => SetN<int>(a).Where(c => c == b)));
        //        }else{
        //            this._変数Cache.Execute(a=>Lambda(b => Arr変数<Int32>().Where(c=>c==a&&c.ToString()==""))));
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => c==a)));
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => c + 0 == a + 0 && c + 1 == a + 1 && c + 2 == a + 2 && c + 3 == a + 3 && c + 4 == a + 4 && c + 5 == a + 5 && c + 6 == a + 6 && c + 7 == a + 7 && c + 8 == a + 8 && c + 9 == a + 9 && c + 10 == a + 10 && c + 11 == a + 11 && c + 12 == a + 12 && c + 13 == a + 13 && c + 14 == a + 14 && c + 15 == a + 15)));
        //          this._変数Cache.Execute(a=>Lambda(b => Set変数<Int32>().Where(c=>c==a&&c.ToString()==""))));
        this.実行結果が一致するか確認(a => Lambda(b => SetN<int>(a).Where(c => c + 0 == a + 0 && c + 1 == a + 1 && c + 2 == a + 2 && c + 3 == a + 3 && c + 4 == a + 4 && c + 5 == a + 5 && c + 6 == a + 6 && c + 7 == a + 7 && c + 8 == a + 8 && c + 9 == a + 9 && c + 10 == a + 10 && c + 11 == a + 11 && c + 12 == a + 12 && c + 13 == a + 13 && c + 14 == a + 14 && c + 15 == a + 15)));
        //        }
        //        if(葉Where!=null) {
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => c == a && c > 3)));
        this.実行結果が一致するか確認(a => Lambda(b => SetN<int>(a).Where(c => c == a && c > 3)));
        //        }else{
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => c == a)));
        this.実行結果が一致するか確認(a => Lambda(b => SetN<int>(a).Where(c => c == a)));
        //            }
        //        if(Setか) {
        //        }else{
        //        }
        //    }else (葉Where!=null){
        this.実行結果が一致するか確認(a => ArrN<int>(a).Where(b => b > 3));
        this.実行結果が一致するか確認(a => SetN<int>(a).Where(b => b > 3));
        //    }
        {
            var b = 3;
            var c = 4;
            this.実行結果が一致するか確認(a => ArrN<int>(a).Where(p => b > c));
            this.実行結果が一致するか確認(a => SetN<int>(a).Where(p => b > c));
        }
        //if(根Where!=null){
        //    if(this._変数_判定_指定Parametersが存在しない.実行(根Where,MethodCall1_predicate_Parameters)){
        {
            var c = 3;
            var d = 4;
            this.実行結果が一致するか確認(a => ArrN<int>(a).Where(p => c > d));
            this.実行結果が一致するか確認(a => SetN<int>(a).Where(p => c > d));
        }
        //    }else{
        {
            var d = 4;
            this.実行結果が一致するか確認(a => ArrN<int>(a).Where(c => c > d || c < d));
            this.実行結果が一致するか確認(a => SetN<int>(a).Where(c => c > d || c < d));
        }
        //    }
        //}
        {
            var c = 3;
            var d = 4;
            this.実行結果が一致するか確認(a => ArrN<int>(a).Where(Func((int p) => c > d)));
            this.実行結果が一致するか確認(a => SetN<int>(a).Where(Func((int p) => c > d)));
        }
    }

    [TestMethod]
    public void 取得_Dictionary_Equal_葉Where_根Where_等号が出現した時にDictionaryHashとEqualに分離()
    {
        //if(this.HashEqualを設定(Right,Left))
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => a == c)));
        //if(this.HashEqualを設定(Left,Right))
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => c == a)));
        //if(this.変数_判定_指定Parametersが1つ以上存在し他のParameterは存在しない.実行(e,this.内側Parameters)) {
        this.実行結果が一致するか確認(a => ArrN<int>(a).Where(p => p == 1));
        //}
        this.実行結果が一致するか確認((a, b) => ArrN<int>(a).SelectMany(p => ArrN<int>(b).Where(r => p == 4)));
    }
}