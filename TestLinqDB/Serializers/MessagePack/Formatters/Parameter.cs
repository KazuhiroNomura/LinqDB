using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Newtonsoft.Json.Linq;

using Sets;
public class Parameter:共通 {
    [Fact]public void Write(){
        var p=Expressions.Expression.Parameter(typeof(int),"p");
        ////if(index0<0){
        ////    if(index1<0){
        //this.MessagePack_Assert(new{a=Expressions.Expression.Block(p)},output=>{});
        ////    }else{
        //this.MessagePack_Assert(new{a=Expressions.Expression.Block(p,p)},output=>{});
        ////    }
        ////}else{
        this.MessagePack_Assert(
            new{
                a=Expressions.Expression.Lambda<Func<int,object>>(
                    Expressions.Expression.Constant(
                        new{a=p}
                    ),
                    p
                )

            },output=>{}
        );
        //}
    }
    [Fact]public void Serialize(){
        this.MessagePack_Assert(new{a=default(Expressions.ParameterExpression)},output=>{});
        var input = Expressions.Expression.Parameter(typeof(int));
        //if(index0<0){
        //    if(index1<0){
        this.MessagePack_Assert(new { a = input},output => { });
        //    }else{
        this.MessagePack_Assert(new { a = input,b = input},output => { });
        //    }
        //}else{
        this.MessagePack_Assert(
            Expressions.Expression.Lambda<Func<int,object>>(
                Expressions.Expression.Constant(
                    new{a=input}
                ),
                input
            ),
            output => { }
        );
        //}
        //匿名型に具体的な型のフィールドがありSerializeが呼ばれる
        this.MessagePack_Assert(new { a = input,b = input},output => { });
    }
}
