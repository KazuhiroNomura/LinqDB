namespace TestLinqDB.Serializers.Formatters.Others;
public class Delegate : 共通
{
    protected override テストオプション テストオプション{get;}=テストオプション.MemoryPack_MessagePack_Utf8Json;
    [Fact]
    public void Action3()
    {
        this.ObjectシリアライズAssertEqual((int a, int b, int c) => { });
    }
    [Fact]
    public void Action2()
    {
        this.ObjectシリアライズAssertEqual((int a, int b) => { });
    }
    [Fact]
    public void Action1()
    {
        this.ObjectシリアライズAssertEqual((int a) => { });
    }
    [Fact]
    public void Action0()
    {
        this.ObjectシリアライズAssertEqual(() => { });
    }
    [Fact]
    public void Func3()
    {
        this.ObjectシリアライズAssertEqual((int a, int b) => a+b);
    }
    [Fact]
    public void Func2()
    {
        this.ObjectシリアライズAssertEqual((int a) => a);
    }
    [Fact]
    public void Func1()
    {
        this.ObjectシリアライズAssertEqual(() => 0);
    }
}
