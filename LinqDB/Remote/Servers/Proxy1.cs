//#define クエリは別プロセスで実行
//#define クエリは別スレッドで実行
#define クエリは同スレッドで実行
namespace LinqDB.Remote.Servers {
    public class Proxy<T> {
        public readonly T Entities;
        public Proxy(T Entities) => this.Entities=Entities;
    }
}
