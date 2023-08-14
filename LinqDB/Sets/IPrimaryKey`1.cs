namespace LinqDB.Sets;

public interface IPrimaryKey<out TKey>where TKey:struct {
    TKey PrimaryKey { get; }
}