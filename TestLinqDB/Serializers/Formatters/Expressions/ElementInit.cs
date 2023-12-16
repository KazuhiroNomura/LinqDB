using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class ElementInit : 共通
{
    [Fact]
    public void Serialize()
    {
        var Type = typeof(BindCollection);
        var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2))!;
        var Constant_1 = Expression.Constant(1);
        var ctor = Type.GetConstructor(new[] { typeof(int) })!;
        var New = Expression.New(
            ctor,
            Constant_1
        );
        var Add = typeof(List<int>).GetMethod("Add")!;
        this.ExpressionシリアライズAssertEqual(
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
