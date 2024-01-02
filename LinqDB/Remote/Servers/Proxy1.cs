
namespace LinqDB.Remote.Servers {
    public class Proxy<T> {
        public readonly T Entities;
        public Proxy(T Entities) => this.Entities=Entities;
    }
}
