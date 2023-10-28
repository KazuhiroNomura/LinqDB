using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class ElementInit<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected ElementInit():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var Type = typeof(BindCollection);
        var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2))!;
        var Constant_1 = Expression.Constant(1);
        var ctor = Type.GetConstructor(new[] { typeof(int) })!;
        var New = Expression.New(
            ctor,
            Constant_1
        );
        var Add = typeof(List<int>).GetMethod("Add")!;
        this.MemoryMessageJson_T_Assert全パターン(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド2,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            )
        );
    }
}
