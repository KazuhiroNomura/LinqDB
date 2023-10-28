

namespace TestLinqDB.Serializers.Generic.Formatters.Others;
public abstract class Delegate<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Delegate():base(new AssertDefinition(new TSerializer())){}
    [Fact]public void Action3(){
        this.MemoryMessageJson_T_Assert全パターン((Action<int,int,int>)((int a,int b,int c) => { }));
    }
    [Fact]public void Action2(){
        this.MemoryMessageJson_T_Assert全パターン((Action<int,int>)((int a,int b) => { }));
    }
    [Fact]public void Action1(){
        this.MemoryMessageJson_T_Assert全パターン((Action<int>)((int a) => { }));
    }
    [Fact]public void Action0(){
        this.MemoryMessageJson_T_Assert全パターン((Action)(() => { }));
    }
    [Fact]public void Func3(){
        this.MemoryMessageJson_T_Assert全パターン((Func<int,int,int>)((int a,int b) => a+b));
    }
    [Fact]public void Func2(){
        this.MemoryMessageJson_T_Assert全パターン((Func<int,int>)((int a) => a));
    }
    [Fact]public void Func1(){
        this.MemoryMessageJson_T_Assert全パターン((Func<int>)(() => 0));
    }
}
