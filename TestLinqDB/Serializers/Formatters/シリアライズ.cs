namespace TestLinqDB.Serializers.Formatters;
public abstract class シリアライズ<T>:共通 {
    private readonly T 対象;
    public シリアライズ(T 対象) {
        this.対象=対象;
    }
    [Fact]
    public void Test0() {
        this.ObjectシリアライズAssertEqual(this.対象);
    }
}
