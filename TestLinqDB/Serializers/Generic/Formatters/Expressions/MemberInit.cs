using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class MemberInit<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected MemberInit():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var Type=typeof(BindCollection);
        var Int32フィールド1=Type.GetField(nameof(BindCollection.Int32フィールド1));
        Assert.NotNull(Int32フィールド1);
        var Listフィールド1=Type.GetField(nameof(BindCollection.Listフィールド1));
        Assert.NotNull(Listフィールド1);
        var Listフィールド2=Type.GetField(nameof(BindCollection.Listフィールド2))!;
        Assert.NotNull(Listフィールド2);
        var Constant_1=Expression.Constant(1);
        var ctor=Type.GetConstructor(new[]{typeof(int)});
        var New=Expression.New(
            ctor,
            Constant_1
        );
        var input=Expression.MemberInit(
            New,
            Expression.Bind(
                Int32フィールド1,
                Constant_1
            )
        );
        this.MemoryMessageJson_T_Assert全パターン(
            new{a=input,b=(Expression)input}
        );
    }
}
