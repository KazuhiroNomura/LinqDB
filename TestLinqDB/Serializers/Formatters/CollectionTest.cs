namespace TestLinqDB.Serializers.Formatters;
public abstract class CollectionTest<T>:共通{
    public readonly T Data;
    public CollectionTest(テストオプション テストオプション,T Data)=>this.Data=Data;
    [Fact]
    public void Test()=>this.AssertEqual(this.Data);
    [Fact]
    public void Array(){
        共通<char>();
        共通<string>();
        共通<byte>();
        共通<int>();
        void 共通<T>(){
            this.AssertEqual(new T[2]);
        }
    }
}
