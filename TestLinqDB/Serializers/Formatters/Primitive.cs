namespace TestLinqDB.Serializers.Formatters;
public class Primitive : 共通{
    //protected override テストオプション テストオプション=>テストオプション.MemoryPack_MessagePack_Utf8Json;
    [Fact] public void @sbyte() => this.ObjectシリアライズAssertEqual((sbyte)1);
    [Fact] public void @byte() => this.ObjectシリアライズAssertEqual((byte)1);
    [Fact] public void @short() => this.ObjectシリアライズAssertEqual((short)1);
    [Fact] public void @ushort() => this.ObjectシリアライズAssertEqual((ushort)1);
    [Fact] public void @int() => this.ObjectシリアライズAssertEqual(1);
    [Fact] public void @uint() => this.ObjectシリアライズAssertEqual((uint)1);
    [Fact] public void @long() => this.ObjectシリアライズAssertEqual((long)1);
    [Fact] public void @ulong() => this.ObjectシリアライズAssertEqual((ulong)1);
    [Fact] public void @float() => this.ObjectシリアライズAssertEqual((float)1);
    [Fact] public void @double() => this.ObjectシリアライズAssertEqual((double)1);
    [Fact] public void @bool() => this.ObjectシリアライズAssertEqual(true);
    [Fact] public void @char() => this.ObjectシリアライズAssertEqual('a');
    [Fact] public void @decimal() => this.ObjectシリアライズAssertEqual((decimal)1);
    [Fact] public void String() => this.ObjectシリアライズAssertEqual("string");
}
