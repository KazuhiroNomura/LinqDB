using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class MemberBinding:共通 {
    [Fact]public void Serialize(){
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1))!;
        var Int32フィールド2 = Type.GetField(nameof(BindCollection.Int32フィールド2))!;
        var BindCollectionフィールド1 = Type.GetField(nameof(BindCollection.BindCollectionフィールド1))!;
        //var BindCollectionフィールド2 = Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1));
        //var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1 = Expressions.Expression.Constant(1);
        //var Constant_2 = Expressions.Expression.Constant(2);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expressions.Expression.New(
            ctor,
            Constant_1
        );
        var input=Expressions.Expression.MemberInit(
            New,
            Expressions.Expression.Bind(
                Int32フィールド1,
                Constant_1
            ),
            Expressions.Expression.ListBind(
                Listフィールド1,
                Expressions.Expression.ElementInit(
                    typeof(List<int>).GetMethod(nameof(List<int>.Add))!,
                    Expressions.Expression.Constant(1)
                )
            ),
            Expressions.Expression.MemberBind(
                BindCollectionフィールド1,
                Expressions.Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
        this.MemoryMessageJson_Assert(new{a=default(Expressions.MemberBinding)},output=>{});
        this.MemoryMessageJson_Assert(
            new{
                a=input,b=(Expressions.Expression)input
            },output=>{}
        );
    }
}
