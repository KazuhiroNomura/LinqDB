namespace TestLinqDB.Serializers.Formatters.Others;
public class DisplayClass : 共通
{
    protected override テストオプション テストオプション{get;}=テストオプション.MemoryPack_MessagePack_Utf8Json;
    [Fact]
    public void Test()
    {
        this.ObjectシリアライズAssertEqual(ClassDisplay取得());
    }
}
