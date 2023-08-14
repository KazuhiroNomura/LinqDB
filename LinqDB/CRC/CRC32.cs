using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace LinqDB.CRC;

/// <summary>
/// CRC32をハッシュコードとして使う
/// </summary>
public unsafe struct CRC32{
    private static readonly uint[] CRC32Table2=new uint[256];
    private static readonly uint* CRC32Table=(uint*)GCHandle.Alloc(CRC32Table2,GCHandleType.Pinned).AddrOfPinnedObject();
    static CRC32() {
        for(uint i=0;i<256;i++) {
            var c=i;
            for(uint j=0;j<8;j++){
                c=(c&1)==1
                    ?0xEDB88320^(c>>1)
                    :c>>1;
            }
            CRC32Table[i]=c;
        }
    }
    private HashCode HashCode;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void PrivateInput(uint 入力HashCode) {
        var hashCode = this._HashCode;
        hashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8);入力HashCode>>=8;
        hashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8);入力HashCode>>=8;
        hashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8);入力HashCode>>=8;
        this._HashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8);
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
    private uint _HashCode;
    ///// <summary>
    ///// 同じCRC型で内部のHashCode状態が一致するか
    ///// </summary>
    ///// <param name="obj"></param>
    ///// <returns></returns>
    //public override Boolean Equals(Object obj)=>obj is CRC32 CRC32&&this.Equals(CRC32);
    ///// <summary>
    ///// 内部のHashCode状態が一致するか
    ///// </summary>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public Boolean Equals(CRC32 other)=>this._HashCode==other._HashCode;
    /// <summary>
    /// 内部のHashCodeを返す
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once NonReadonlyMemberInGetHashCode
    //public override Int32 GetHashCode() => (Int32)this._HashCode;
    public override int GetHashCode() => this.HashCode.ToHashCode();
    ///// <summary>
    ///// 内部のHashCode状態が一致するか
    ///// </summary>
    ///// <param name="left"></param>
    ///// <param name="right"></param>
    ///// <returns></returns>
    //public static Boolean operator ==(CRC32 left,CRC32 right) =>  left.Equals(right);
    ///// <summary>
    ///// 内部のHashCode状態が不一致するか
    ///// </summary>
    ///// <param name="left"></param>
    ///// <param name="right"></param>
    ///// <returns></returns>
    //public static Boolean operator !=(CRC32 left,CRC32 right) => !left.Equals(right);
}