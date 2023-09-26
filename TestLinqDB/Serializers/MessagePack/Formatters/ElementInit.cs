using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
using System.Security.Cryptography;

public class ElementInit:共通 {
    [Fact]
    public void Serialize(){
        var Type=typeof(BindCollection);
        var Listフィールド2=Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1=Expressions.Expression.Constant(1);
        var ctor=Type.GetConstructor(new[]{typeof(int)});
        var New=Expressions.Expression.New(
            ctor,
            Constant_1
        );
        var Add=typeof(List<int>).GetMethod("Add");
        {
            this.MemoryMessageJson_Assert(new{a=default(Expressions.ElementInit)});
            this.MemoryMessageJson_Assert(
                new{
                    a=Expressions.Expression.MemberInit(
                        New,
                        Expressions.Expression.ListBind(
                            Listフィールド2,
                            Expressions.Expression.ElementInit(
                                Add,
                                Constant_1
                            )
                        )
                    )
                }
            );
        }
    }
}
