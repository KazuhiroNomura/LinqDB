using MemoryPack;

namespace TestLinqDB.Serializers.Formatters;
public abstract class CollectionTest<T>:共通{
    protected readonly T? Default=default;
    protected readonly T Data;
    //protected CollectionTest(){}
    protected CollectionTest(T Data)=>this.Data=Data;
    [Fact]public void Test()=>this.MemoryMessageJson_T_Assert全パターン(this.Data);
    [Fact]public void Array(){
        共通<string>();
        共通<byte>();
        共通<char>();
        共通<int>();
        void 共通<T>(){
            this.MemoryMessageJson_T_Assert(new T[2]);
        }
    }
}
