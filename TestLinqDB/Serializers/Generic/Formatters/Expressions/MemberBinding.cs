using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class MemberBinding<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected MemberBinding():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize()
    {
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1))!;
        var Int32フィールド2 = Type.GetField(nameof(BindCollection.Int32フィールド2))!;
        var BindCollectionフィールド1 = Type.GetField(nameof(BindCollection.BindCollectionフィールド1))!;
        //var BindCollectionフィールド2 = Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1));
        //var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1 = Expression.Constant(1);
        //var Constant_2 = Expression.Constant(2);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expression.New(
            ctor,
            Constant_1
        );
        var input = Expression.MemberInit(
            New,
            Expression.Bind(
                Int32フィールド1,
                Constant_1
            ),
            Expression.ListBind(
                Listフィールド1,
                Expression.ElementInit(
                    typeof(List<int>).GetMethod(nameof(List<int>.Add))!,
                    Expression.Constant(1)
                )
            ),
            Expression.MemberBind(
                BindCollectionフィールド1,
                Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
        this.AssertEqual(new { a = default(MemberBinding) });
        this.AssertEqual(
            new
            {
                a = input,
                b = (Expression)input
            }
        );
    }
}
