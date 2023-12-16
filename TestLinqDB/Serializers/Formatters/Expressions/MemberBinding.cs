using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class MemberBinding : 共通
{
    [Fact]
    public void Serialize()
    {
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1))!;
        var Int32フィールド2 = Type.GetField(nameof(BindCollection.Int32フィールド2))!;
        var BindCollectionフィールド1 = Type.GetField(nameof(BindCollection.BindCollectionフィールド1))!;
        //var BindCollectionフィールド2 = Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1))!;
        //var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1 = Expression.Constant(1);
        //var Constant_2 = Expression.Constant(2);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        })!;
        var New = Expression.New(
            ctor,
            Constant_1
        );
        var MemberAsignment = Expression.Bind(
            Int32フィールド1,
            Constant_1
        );
        var MemberListBinding = Expression.ListBind(
            Listフィールド1,
            Expression.ElementInit(
                typeof(List<int>).GetMethod(nameof(List<int>.Add))!,
                Expression.Constant(1)
            )
        );
        var MemberMemberBinding = Expression.MemberBind(
            BindCollectionフィールド1,
            Expression.Bind(
                Int32フィールド2,
                Constant_1
            )
        );
        var input = Expression.MemberInit(
            New,
            MemberAsignment,
            MemberListBinding,
            MemberMemberBinding
        );
        this.ObjectシリアライズAssertEqual(MemberAsignment);
        this.ObjectシリアライズAssertEqual(MemberListBinding);
        this.ObjectシリアライズAssertEqual(MemberMemberBinding);
        this.ObjectシリアライズAssertEqual(input);
    }
}
