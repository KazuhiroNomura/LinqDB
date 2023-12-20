using System;
using LinqDB.Databases;
namespace LinqDB.Sets;

[Serializable]
public abstract class Entity<TContainer>:Entity where TContainer : Container {
    /// <summary>
    /// 親Setに自身のタプルを追加させる。
    /// </summary>
    /// <param name="Container"></param>
    protected internal virtual void AddRelationship(TContainer Container){}
    ///// <summary>
    ///// 全Clearするときにこのタプルを参照している子Setがあれば例外。自分自身が参照しているものは無視。
    ///// </summary>
    //protected internal virtual void InvalidateClearRelationship(){}
    /// <summary>
    /// 親Setに自身のタプルを削除する。
    /// </summary>
    protected internal virtual void RemoveRelationship(){}
}
[MessagePack.MessagePackObject(true),Serializable]
public abstract class EntityKey<TKey>:IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    //[IgnoreDataMember]
    protected readonly TKey ProtectKey;
    //[IgnoreDataMember]
    public TKey Key=>this.ProtectKey;
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    /// <param name="Key"></param>
    protected EntityKey(TKey Key) => this.ProtectKey=Key;
    public override int GetHashCode() => this.ProtectKey.GetHashCode();
    public override bool Equals(object? obj)=>obj is EntityKey<TKey> other&&this.Key.Equals(other.Key);
}

