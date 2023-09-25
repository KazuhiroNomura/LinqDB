using System.Diagnostics;
//using System.Linq.Expressions;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace Serializers.MessagePack.Formatters;
using System.Linq.Expressions;
public class Binary:共通 {
    [Fact]public void WriteLeftRight(){
        var ParameterDouble=Expression.Parameter(typeof(double));
        var ParameterInt32= Expression.Parameter(typeof(int),"int32");
        var ParameterString= Expression.Parameter(typeof(string),"string");
        var ConstantString = Expression.Constant("string");
        //var ConstantStringNull=Expression.Constant(null,typeof(string));
        var ConstantArray = Expression.Constant(new int[10]);
        var Constant0= Expression.Constant(0);
        var Constant1= Expression.Constant(1);
        var ConstantTrue = Expression.Constant(true);
        var ConstantBinry= Expression.Constant(new 演算子(true));
        var ConversionInt32=Expression.Lambda<Func<int,int>>(Expression.Add(ParameterInt32,ParameterInt32),ParameterInt32);
        var ConversionDouble=Expression.Lambda<Func<double,double>>(Expression.Add(ParameterDouble,ParameterDouble),ParameterDouble);
        this.MemoryMessageJson_Expression(Expression.Assign(ParameterInt32,Constant1));
    }
    private static string string_string_string(string? a,string b)=>a??b;
    [Fact]public void WriteLeftRightLambda(){
        var ParameterString= Expression.Parameter(typeof(string),"string");
        var ConstantString = Expression.Constant("string");
        var ConversionString=Expression.Lambda<Func<string,string>>(Expression.Call(null,GetMethod(()=>string_string_string("","")),ParameterString,ParameterString),ParameterString);
        this.MemoryMessageJson_Expression(Expression.Coalesce(ConstantString,ConstantString,ConversionString));
    }
    [Fact]public void WriteLeftRightMethod(){
        var Constant1= Expression.Constant(1m);
        this.MemoryMessageJson_Expression(Expression.Add(Constant1,Constant1));
    }
    [Fact]public void WriteLeftRightMethodLambda(){
        var ParameterDecimmal=Expression.Parameter(typeof(decimal));
        var Constant1= Expression.Constant(1m);
        var ConversionDecimal=Expression.Lambda<Func<decimal,decimal>>(Expression.Add(ParameterDecimmal,ParameterDecimmal),ParameterDecimmal);
        共通0(Expression.AddAssign(ParameterDecimmal,Constant1,typeof(decimal).GetMethod("op_Addition"),ConversionDecimal));
        void 共通0(BinaryExpression input){
            //{
            //    var Lambda=Expression.Lambda<Func<decimal,decimal>>(
            //        input,ParameterDecimmal
            //    );
            //    this.MemoryMessageJson_Assert(
            //        Lambda,
            //        output=>Assert.Equal(Lambda,output,this.ExpressionEqualityComparer));
            //}
            {
                var Lambda=Expression.Lambda<Func<object>>(
                    Expression.Block(
                        new[]{ParameterDecimmal},
                        //Expression.Assign(
                        //    ParameterDecimmal,
                        //    Expression.Constant(0m)
                        //),
                        Expression.Convert(
                            input,
                            typeof(object)
                        )
                    )
                );
                this.MemoryMessageJson_Assert(
                    Lambda,
                    output=>Assert.Equal(Lambda,output,this.ExpressionEqualityComparer));
            }
            this.MemoryMessageJson_Expression(input);
            this.MemoryMessageJson_Expression(
                Expression.Lambda<Func<object>>(
                    Expression.Block(
                        new[]{ParameterDecimmal},
                        Expression.Assign(
                            ParameterDecimmal,
                            Expression.Constant(0m)
                        ),
                        Expression.Convert(
                            input,
                            typeof(object)
                        )
                    )
                )
            );

            this.MemoryMessageJson_TExpressionObject_コンパイル実行(
                Expression.Lambda<Func<object>>(
                    Expression.Block(
                        new[]{ParameterDecimmal},
                        Expression.Assign(
                            ParameterDecimmal,
                            Expression.Constant(0m)
                        ),
                        Expression.Convert(
                            input,
                            typeof(object)
                        )
                    )
                )
            );
        }
    }
}
