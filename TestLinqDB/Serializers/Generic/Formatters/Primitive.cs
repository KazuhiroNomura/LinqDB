namespace TestLinqDB.Serializers.Generic.Formatters;
public abstract class Primitive:共通{
    protected Primitive(テストオプション テストオプション):base(テストオプション){}
    [Fact]public void @sbyte()=>this.AssertEqual((sbyte)1);
    [Fact]public void @byte()=>this.AssertEqual((byte)1);
    [Fact]public void @short()=>this.AssertEqual((short)1);
    [Fact]public void @ushort()=>this.AssertEqual((ushort)1);
    [Fact]public void @int()=>this.AssertEqual((int)1);
    [Fact]public void @uint()=>this.AssertEqual((uint)1);
    [Fact]public void @long()=>this.AssertEqual((long)1);
    [Fact]public void @ulong()=>this.AssertEqual((ulong)1);
    [Fact]public void @float()=>this.AssertEqual((float)1);
    [Fact]public void @double()=>this.AssertEqual((double)1);
    [Fact]public void @bool()=>this.AssertEqual(true);
    [Fact]public void @char()=>this.AssertEqual('a');
    [Fact]public void @decimal()=>this.AssertEqual((decimal)1);
    [Fact]public void String()=>this.AssertEqual("string");
}
