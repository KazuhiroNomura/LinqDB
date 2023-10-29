

namespace TestLinqDB.Serializers.Generic.Formatters.Others;
public abstract class Delegate:共通{
    protected Delegate(テストオプション テストオプション):base(テストオプション){}
    [Fact]public void Action3(){
        this.AssertEqual((Action<int,int,int>)((int a,int b,int c) => { }));
    }
    [Fact]public void Action2(){
        this.AssertEqual((Action<int,int>)((int a,int b) => { }));
    }
    [Fact]public void Action1(){
        this.AssertEqual((Action<int>)((int a) => { }));
    }
    [Fact]public void Action0(){
        this.AssertEqual((Action)(() => { }));
    }
    [Fact]public void Func3(){
        this.AssertEqual((Func<int,int,int>)((int a,int b) => a+b));
    }
    [Fact]public void Func2(){
        this.AssertEqual((Func<int,int>)((int a) => a));
    }
    [Fact]public void Func1(){
        this.AssertEqual((Func<int>)(() => 0));
    }
}
