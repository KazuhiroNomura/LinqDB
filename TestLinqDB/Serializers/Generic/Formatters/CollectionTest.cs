namespace TestLinqDB.Serializers.Generic.Formatters;
public abstract class CollectionTest<T,TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected readonly T? Default=default;
    protected readonly T Data;
    //protected CollectionTest(){}
    protected CollectionTest(T Data):base(new AssertDefinition(new TSerializer()))=>this.Data=Data;
    [Fact]public void Test()=>this.AssertEqual(this.Data);
    [Fact]public void Array(){
        共通<string>();
        共通<byte>();
        共通<char>();
        共通<int>();
        void 共通<T>(){
            this.AssertEqual(new T[2]);
        }
    }
}
