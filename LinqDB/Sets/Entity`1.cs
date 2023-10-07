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