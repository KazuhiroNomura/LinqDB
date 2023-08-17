using System.Globalization;
using LinqDB.Sets;
using CoverageCS.LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers.変換_SelectManyをJoin;

[TestClass]
public class 取得_DictionaryHash_Equal_葉Where_根Where2 : ATest
{
    [TestMethod]
    public void Traverse()
    {
        //if(e0_NodeType==ExpressionType.OrElse){
        this.Execute引数パターン(a => ArrN<int>(a).Where(p => p == 3 || p == 2));
        this.Execute引数パターン(a => SetN<int>(a).Where(p => p == 3 || p == 2));
        //    if(Binary1_Left==null)
        this.Execute引数パターン(a => ArrN<int>(a).Where(c => c == 3 && c == 2));
        this.Execute引数パターン(a => SetN<int>(a).Where(c => c == 3 && c == 2));
        //    if(Binary1_Right==null)
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(c => a == 0 && a == c)));
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where(c => a == 0 && a == c)));
        //    if(Binary1_Left==Binary0_Left&&Binary1_Right==Binary0_Right)
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(c => a == 1 && a == 0)));
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where(c => a == 1 && a == 0)));
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(c => a == 1 && a == c && a == 1 && a == c)));
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where(c => a == 1 && a == c && a == 1 && a == c)));
        //}else if(e0_NodeType==ExpressionType.Equal){
        //    if(Binary_Left.Type.IsPrimitive||Binary0.Method!=null) {
        this.Execute引数パターン(a => ArrN<int>(a).Where(b => b == 3));
        this.Execute引数パターン(a => SetN<int>(a).Where(b => b == 3));
        //    }else{
        this.Execute引数パターン(a => ArrN<object>(a).Where(b => b == new object()));
        this.Execute引数パターン(a => SetN<object>(a).Where(b => b == new object()));
        //    }
        //}else if(e0_NodeType==ExpressionType.Call){
        //    if(Reflection.Object.Equals.MethodEquals(MethodCall_Method)) {
        this.Execute引数パターン(a => ArrN<object>(a).Where(b => b.Equals(new object())));
        this.Execute引数パターン(a => SetN<object>(a).Where(b => b.Equals(new object())));
        //    }
        //    if(IEquatableType.IsAssignableFrom(Method_DeclaringType)) {
        this.Execute引数パターン(a => ArrN<int>(a).Where(b => b.Equals(1)));
        this.Execute引数パターン(a => SetN<int>(a).Where(b => b.Equals(1)));
        //    }
        this.Execute引数パターン(a => ArrN<int>(a).Where(b => Booleanを返すメソッド()));
        this.Execute引数パターン(a => SetN<int>(a).Where(b => Booleanを返すメソッド()));
        //}
        //if(this._変数_判定_指定Parametersが1つ以上存在し他のParameterは存在しない.実行(e0,_内側Parameters)) {
        this.Execute引数パターン(a => ArrN<int>(a).Where(c => c > 3));
        this.Execute引数パターン(a => SetN<int>(a).Where(c => c > 3));
        //}
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(c => a > c)));
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where(c => a > c)));
    }
    [TestMethod]
    public void 等号が出現した時にHashDictionaryとEqualに分離()
    {
        //if(this.HashEqualを設定(Right,Left)){
        this.Execute引数パターン(a => Lambda(q => ArrN<int>(a).Where(r => a == r)));
        this.Execute引数パターン(a => Lambda(q => SetN<int>(a).Where(r => a == r)));
        //}
        //if(this.HashEqualを設定(Left,Right)){
        this.Execute引数パターン(a => Lambda(q => ArrN<int>(a).Where(r => r == a)));
        this.Execute引数パターン(a => Lambda(q => SetN<int>(a).Where(r => r == a)));
        //}
        //if(this._変数_判定_指定Parametersが1つ以上存在し他のParameterは存在しない.実行(e,this._内側Parameters)) {
        this.Execute引数パターン(a => Lambda(r => ArrN<int>(a).Where(q => q == 3)));
        this.Execute引数パターン(a => Lambda(r => SetN<int>(a).Where(q => q == 3)));
        //}
        this.Execute引数パターン(a => Lambda(r => ArrN<int>(a).Where(q => q == r)));
        this.Execute引数パターン(a => Lambda(r => SetN<int>(a).Where(q => q == r)));
    }
    [TestMethod]
    public void 実行()
    {
        //if(プローブPrimaryKeyExpression==null) {
        this.Execute2(() => KeySet変数().Where(p => p == null));
        //    if(Constructor_GetParameters.Length==ListNameHashEqualExpression_Count) {
        this.Execute2(() => KeySet変数().Where(p => p.ID1 == 0));
        //    }
        this.Execute2(() => KeySet変数().Where(p => p.ID3.ToString(CultureInfo.InvariantCulture) == ""));
        //}
        var PrimaryKey = new PrimaryKeys.Entity(1, 1);
        this.Execute2(() => KeySet変数().Where(p => p.PrimaryKey == PrimaryKey));
    }
    [TestMethod]
    public void op_EqualとEqualsの共通処理()
    {
        var PrimaryKey = new PrimaryKeys.Entity(0, 0);
        //if(L_Member!=null&&L_Member.Expression==Parameters[0]) {
        //    if(this._変数_判定_指定Parametersが存在しない.実行(R,Parameters)) {
        //        if(L_Member_Member.MetadataToken==_PrimaryKey_MetadataToken) {
        this.Execute2(() => KeySet変数().Where(p => p.PrimaryKey == PrimaryKey));
        //        } else if(this._HashSetFieldName.Contains(L_Member_Member.Name)) {
        this.Execute2(() => KeySet変数().Where(p => p.ID1 == 3));
        //        }else{
        this.Execute2(() => KeySet変数().Where(p => p.ID3.ToString(CultureInfo.InvariantCulture) == ""));
        //        }
        //    }
        this.Execute2(() => KeySet変数().Where(p => p.ID3.ToString(CultureInfo.InvariantCulture) == p.ToString()));
        //}
        //if(R_Member!=null&&R_Member.Expression==Parameters[0]) {
        //    if(this._変数_判定_指定Parametersが存在しない.実行(L,Parameters)) {
        //        if(R_Member_Member.MetadataToken==_PrimaryKey_MetadataToken) {
        this.Execute2(() => KeySet変数().Where(p => PrimaryKey == p.PrimaryKey));
        //        } else if(this._HashSetFieldName.Contains(R_Member_Member.Name)) {
        this.Execute2(() => KeySet変数().Where(p => 3 == p.ID1));
        //        }else{
        this.Execute2(() => KeySet変数().Where(p => "" == p.ID3.ToString(CultureInfo.InvariantCulture)));
        //        }
        //    }
        this.Execute2(() => KeySet変数().Where(p => p.ToString() == p.ID3.ToString(CultureInfo.InvariantCulture)));
        //}
        this.Execute2(() => KeySet変数().Where(p => null == p));
    }
    [TestMethod]
    public void SelectManyの展開() {
        var A = new Set<int>();
        var B = new Set<int>();
        this.Execute2(() =>
            A.SelectMany(
                a => B,
                (a,b) => new { a,b }
            ).Where(
                ab => ab.a==ab.b
            ).Select(
                ab => new { ab.a,ab.b}
            )
        );
        var C = new Set<int>();
        this.Execute2(() =>
            A.SelectMany(
                a => B.SelectMany(
                    b => C,
                    (int b,int c) => new { b,c }
                ),
                (a,bc) => new { a,bc }
            ).Where(
                abc => abc.a==abc.bc.b&&abc.bc.b==abc.bc.c
            ).Select(
                abc => new { abc.a,abc.bc.b,abc.bc.c }
            )
        );
        //this.Execute(() =>
        //    from a in A
        //    from b in B
        //    where a==b
        //    select new { a,b }
        //);
        //return A.SelectMany(a => B.SelectMany(b => C,(b,c) => new { b,c }),(a,b) => new { a,b }).Where(ab => ab.a==ab.b.b).Select(ab => new { ab.a,ab.b });
        //return A.Join(B,a => a,b => b,(a,b) => new { a,b }).Join(C,ab => ab.b,c => c,(ab,c) => new { ab.a,ab.b,c });
    }
}