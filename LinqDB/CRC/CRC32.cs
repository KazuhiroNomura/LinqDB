using System;
using System.Runtime.CompilerServices;
namespace LinqDB.CRC;

/// <summary>
/// CRC32をハッシュコードとして使う
/// </summary>
public unsafe struct CRC32{
    private HashCode HashCode;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void PrivateInput(uint 入力HashCode) {
        this.HashCode.Add(入力HashCode);
    }
    /// <summary>
    /// HashCodeを求めたい値
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Input<T>(T value) => this.PrivateInput(value is not null ? (uint)value.GetHashCode() : 0);
    /// <summary>
    /// HashCodeを求めたい値UInt32専用
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Input(uint value) => this.PrivateInput(value);
    /// <summary>
    /// HashCodeを求めたい値Int32専用
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(int value)=>this.Input((uint)value);
    /// <summary>
    /// HashCodeを求めたい値UInt16専用
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(ushort value)=> this.Input((uint)value);
    /// <summary>
    /// HashCodeを求めたい値Int16専用
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(short value)=> this.Input((uint)value);
    /// <summary>
    /// HashCodeを求めたい値Byte専用
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(byte value)=> this.Input((uint)value);
    /// <summary>
    /// HashCodeを求めたい値SByte専用
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(sbyte value)=> this.Input((uint)value);
    public override int GetHashCode() => this.HashCode.ToHashCode();
}