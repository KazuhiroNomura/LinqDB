namespace LinqDB.Sets;

public interface IKey<out TKey>where TKey:struct {
    TKey Key { get; }
}