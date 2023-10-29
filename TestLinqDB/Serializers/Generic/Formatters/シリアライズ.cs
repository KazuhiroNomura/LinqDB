namespace TestLinqDB.Serializers.Generic.Formatters;
public abstract class シリアライズ<T> : 共通{
    private readonly T 対象;
    protected シリアライズ(テストオプション テストオプション,T 対象) : base(テストオプション) {
        this.対象=対象;
    }
    [Fact]public void Test0(){
        this.AssertEqual(this.対象);
    }
}
