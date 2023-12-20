using System;
using LinqDB.Databases;
namespace LinqDB.Sets;

/// <summary>
/// エンティティ型はそれを管理しているContainerを持つ
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TContainer"></typeparam>
[MessagePack.MessagePackObject(true),Serializable]
public abstract class Entity<TKey,TContainer>:Entity<TContainer>, IKey<TKey>
    where TKey : struct, IEquatable<TKey>
    where TContainer:Container{
    //[IgnoreDataMember]
    protected readonly TKey ProtectKey;
    //[IgnoreDataMember]
    public TKey Key=>this.ProtectKey;
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    /// <param name="Key"></param>
    protected Entity(TKey Key) => this.ProtectKey=Key;
    public override int GetHashCode() => this.ProtectKey.GetHashCode();
    public override bool Equals(object? obj)=>obj is Entity<TKey,TContainer> other&&this.Key.Equals(other.Key);
}
