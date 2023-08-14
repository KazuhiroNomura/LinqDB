using System;
using LinqDB.Databases;
using System.Text.Json.Serialization;
namespace LinqDB.Sets;

/// <summary>
/// エンティティ型はそれを管理しているContainerを持つ
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TContainer"></typeparam>
[Serializable]
public abstract class Entity<TKey,TContainer>:Entity<TContainer>, IPrimaryKey<TKey>
    where TKey : struct, IEquatable<TKey>
    where TContainer:Container{
    protected readonly TKey ProtectedPrimaryKey;
    [JsonIgnore]
    public TKey PrimaryKey=>this.ProtectedPrimaryKey;
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    /// <param name="PrimaryKey"></param>
    protected Entity(TKey PrimaryKey) => this.ProtectedPrimaryKey=PrimaryKey;
    public sealed override int GetHashCode() => this.ProtectedPrimaryKey.GetHashCode();
}