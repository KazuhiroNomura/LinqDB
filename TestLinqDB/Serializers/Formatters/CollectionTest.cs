namespace TestLinqDB.Serializers.Formatters;
public abstract class CollectionTest<T>:共通{
    protected readonly T? Default=default;
    protected readonly T Data;
    //protected CollectionTest(){}
    protected CollectionTest(T Data)=>this.Data=Data;
    [Fact]public void @default()=>this.MemoryMessageJson_Assert(this.Default);
    [Fact]public void Anonymous_default()=>this.MemoryMessageJson_Assert(new{a=this.Default});
    [Fact]public void Test()=>this.MemoryMessageJson_Assert(this.Data);
    [Fact]public void Anonymous_Test()=>this.MemoryMessageJson_Assert(new{a=this.Data});
}
