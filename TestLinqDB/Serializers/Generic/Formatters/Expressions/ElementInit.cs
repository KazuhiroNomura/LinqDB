﻿using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class ElementInit:共通{
    protected ElementInit(テストオプション テストオプション):base(テストオプション){}
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
        this.AssertEqual(
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
