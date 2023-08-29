using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
[SuppressMessage("ReSharper", "OperatorIsCanBeUsed")]
public class Test_変換_ラムダ跨ぎ変数をTargetメンバー参照 : ATest
{
    [TestMethod]
    public void MakeAssign()
    {
        //if(Binary0_Left==Binary1_Left&&Binary0_Right==Binary1_Right&&Binary0_Conversion==Binary1_Conversion) return Binary0;
        this.実行結果が一致するか確認(() => Lambda(p => p * 2) + Lambda(p => p * 2));
        this.実行結果が一致するか確認(() => _StaticString.GetType() == typeof(int) && _StaticInt32.GetType() == typeof(int));
    }
    [TestMethod]
    public void Traverse0(){
        //if(e==null) return null;
        this.実行結果が一致するか確認(() => Lambda(p => p * 3) + Lambda(p => p * 3));
    }
    [TestMethod]
    public void Traverse1(){
        //if(this.Dictionary大域Parameter大域Field.TryGetValue(e,out Right))return Right;
        this.実行結果が一致するか確認(() => _StaticInt32.GetType() == typeof(int));
    }
    [TestMethod]
    public void Traverse2(){
        //if(this.Dictionary大域Parameter大域Field.TryGetValue(e,out Right))return Right;
        this.実行結果が一致するか確認(() => _StaticInt32.GetType() == typeof(int) && _StaticString.GetType() == typeof(int));
    }
    public bool Get(int a,int b){
        return a.GetType() == typeof(int) && b.GetType() == typeof(int);
    }
    public bool Get3(int a,Type b){
        return a.GetType() == typeof(int) && b== typeof(int);
    }
    public bool GetEqual(int a){
        return a.GetType() == typeof(int);
    }
    public bool GetEqual(FieldInfo a,FieldInfo b){
        return a == b;
    }
    public bool GetNotEqual(int a){
        return a.GetType() != typeof(int);
    }
    public Type GetGetType(object a){
        return a.GetType();
    }
    public Type GetType_(Type a){
        return a;
    }
    public bool GetIs(object a){
        return a is int;
    }
}
