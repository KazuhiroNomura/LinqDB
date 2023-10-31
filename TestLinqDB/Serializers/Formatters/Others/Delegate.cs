namespace TestLinqDB.Serializers.Formatters.Others;
public class Delegate : 共通
{
    [Fact]
    public void Action3()
    {
        this.AssertEqual((int a, int b, int c) => { });
    }
    [Fact]
    public void Action2()
    {
        this.AssertEqual((int a, int b) => { });
    }
    [Fact]
    public void Action1()
    {
        this.AssertEqual((int a) => { });
    }
    [Fact]
    public void Action0()
    {
        this.AssertEqual(() => { });
    }
    [Fact]
    public void Func3()
    {
        this.AssertEqual((int a, int b) => a+b);
    }
    [Fact]
    public void Func2()
    {
        this.AssertEqual((int a) => a);
    }
    [Fact]
    public void Func1()
    {
        this.AssertEqual(() => 0);
    }
}
