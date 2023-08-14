#pragma warning disable 1591
using System;
using System.Collections;
namespace LinqDB.Optimizers;

/// <summary>
/// 最適化したDelegate.Targetに割り当てる自動変数を表現する具象クラス。
/// </summary>
[Serializable]
public class Target<TValueTuple>:Target 
    where TValueTuple: IComparable, IComparable<TValueTuple>, IEquatable<TValueTuple>, IStructuralComparable, IStructuralEquatable {
    public TValueTuple ValueTuple;
    public override object Object {
        get => this.ValueTuple;
        internal set => this.ValueTuple=(TValueTuple)value;
    }
    public Target() {
        this.ValueTuple=default!;
    }
    public Target(TValueTuple ValueTuple) {
        this.ValueTuple=ValueTuple;
    }
}