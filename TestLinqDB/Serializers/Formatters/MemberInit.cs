using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class MemberInit:共通{
    [Fact]
    public void Serialize(){
        var Type=typeof(BindCollection);
        var Int32フィールド1=Type.GetField(nameof(BindCollection.Int32フィールド1));
        Assert.NotNull(Int32フィールド1);
        var Listフィールド1=Type.GetField(nameof(BindCollection.Listフィールド1));
        Assert.NotNull(Listフィールド1);
        var Listフィールド2=Type.GetField(nameof(BindCollection.Listフィールド2))!;
        Assert.NotNull(Listフィールド2);
        var Constant_1=Expressions.Expression.Constant(1);
        var ctor=Type.GetConstructor(new[]{typeof(int)});
        var New=Expressions.Expression.New(
            ctor,
            Constant_1
        );
        var input=Expressions.Expression.MemberInit(
            New,
            Expressions.Expression.Bind(
                Int32フィールド1,
                Constant_1
            )
        );
        this.MemoryMessageJson_T_Assert全パターン(
            new{a=input,b=(Expressions.Expression)input}
        );
    }
}
