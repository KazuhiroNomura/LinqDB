using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;

using Expressions=System.Linq.Expressions;
public class CatchBlock:共通 {
    [Fact]public void Serialize(){
        var Variable=Expressions.Expression.Parameter(typeof(Exception));
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(Expressions.CatchBlock)});
        //if(value.Variable is null){
        //    if(value.Filter is null){
        this.MemoryMessageJson_Assert(
            new{
                a=Expressions.Expression.Catch(
                    typeof(Exception),
                    Expressions.Expression.Default(typeof(void))
                )
            }
        );
        //    } else{
        this.MemoryMessageJson_Assert(
            new{
                a=Expressions.Expression.Catch(
                    typeof(Exception),
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Constant(true)
                )
            }
        );
        //    }
        //} else{
        //    if(value.Filter is null){
        this.MemoryMessageJson_Assert(
            new{
                a=Expressions.Expression.Catch(
                    Variable,
                    Expressions.Expression.Default(typeof(void))
                )
            }
        );
        //    } else{
        this.MemoryMessageJson_Assert(
            new{
                a=Expressions.Expression.Catch(
                    Variable,
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Constant(true)
                )
            }
        );
        //    }
        //}

    }
    [Fact]public void Block0(){
        var ParameterDecimmal=Expressions.Expression.Parameter(typeof(decimal));
        共通0(
            Expressions.Expression.Block(
                new[]{ParameterDecimmal},
                Expressions.Expression.Block(
                    new[]{ParameterDecimmal},
                    ParameterDecimmal
                )
            )
        );
        void 共通0(Expressions.Expression input){
            this.MemoryMessageJson_Assert(
                input,
                output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        }
    }
}
