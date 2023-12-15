using LinqDB.Sets;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
internal partial struct 嘘Equals{
    public bool Equals(嘘Equals x)=>true;
    public bool Equals2()=>true;
}
public class 取得_OuterPredicate_InnerPredicate_プローブビルド:共通{
    private readonly static int[]array={1,2,3,4,5,6,7};
    [Fact]public void 実行(){
        this.Expression実行AssertEqual(() => array.Where(p => p==0));
    }
    [Fact]public void 等号が出現した時にDictionaryHashとEqualに分離(){
        //if(this.判定_指定Parameter有_他Parameter無_Lambda内部走査.実行(e,this.内側Parameter!)){
        this.Expression実行AssertEqual(() => array.Where(p => p==0));
        //}
        this.Expression実行AssertEqual(() => array.Join(array,o => o,i => i,(o,i) => o+i ).Where(p => p==0));
        //bool HashEqualを設定(Expression プローブ,Expression ビルド){
        //    if(this._判定_指定Parameterが存在する.実行(ビルド,this.内側Parameter)){
        //        if(this.判定_指定Parameter無_他Parameter有.実行(プローブ,this.内側Parameter)){
        //            if(!this.Listプローブビルド.Contains((プローブ,ビルド),this.ブローブビルドExpressionEqualityComparer)){
        this.Expression実行AssertEqual(() => array.Join(array,o => o,i => i,(o,i) =>o+i));
        //            }
        this.Expression実行AssertEqual(() => (1).Let(q=>array.Where(p=>p==q&&q==p)));
        //        }
        this.Expression実行AssertEqual(() => array.Join(array,o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //    }
        this.Expression実行AssertEqual(() => array.Join(array,o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //}
    }
    private sealed class EqualityComparer2:EqualityComparer<int>{
        public override bool Equals(int x,int y)=>x==y;
        public override int GetHashCode(int obj)=>obj;
    }
    [Fact]public void PrivateTraverseNullable(){
        //switch(Expression0.NodeType){
        //    case ExpressionType.OrElse:
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => p==2||p==4)));
        //    case ExpressionType.AndAlso:{
        //        if(Binary1_Left is null) return Binary1_Right;
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => p==q&&q==p)));
        //        if(Binary1_Right is null) return Binary1_Left;
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => p==1&&p==1)));
        //        if(Binary1_Left==Binary0_Left&&Binary1_Right==Binary0_Right) return Expression0;
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => q==1&&p==1)));
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => q==1&&1==q)));
        //    }
        //    case ExpressionType.Equal:{
        //        if(Binary_Left.Type.IsPrimitive) return this.等号が出現した時にDictionaryHashとEqualに分離(Binary0,Binary_Left,Binary0.Right);
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => p==1)));
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => p==1m)));
        //    }
        //    case ExpressionType.Call:{
        //        if(nameof(Equals)==MethodCall.Method.Name){
        //            if(Reflection.Object.Equals_==MethodCall.Method){
        this.Expression実行AssertEqual(() => (1).Let(q => array.Where(p => ((object)1m).Equals(p))));
        //            }
        //            if(MethodCall.Object!.Type.GetGenericArguments(typeof(IEqualityComparer<>),out var _)){
        this.Expression実行AssertEqual(() => array.Join(array,o => o,i => i,(o,i) => o+i ).Where(p => p==0));
        //            }
        //            if(IEquatable is not null) return this.等号が出現した時にDictionaryHashとEqualに分離(Expression0,MethodCall.Object,MethodCall.Arguments[0]);
        this.Expression実行AssertEqual(() => (1).Let(q=>array.Where(p=>1m.Equals(p))));
        //        }
        this.Expression実行AssertEqual(() => (1).Let(q=>new 嘘Equals[2].Where(p=>p.Equals(new 嘘Equals()))));
        //    }
        this.Expression実行AssertEqual(() => (1).Let(q=>new 嘘Equals[2].Where(p=>p.Equals2())));
        //}
        //if(this.判定_指定Parameter有_他Parameter無_Lambda内部走査.実行(Expression0,this.内側Parameter!)){
        this.Expression実行AssertEqual(()=>array.Where((Func<int,bool>)(p=>p==2)).Where(p=>p==2));
        //}
        this.Expression実行AssertEqual(()=>array.Where(p=>true));
    }
}
