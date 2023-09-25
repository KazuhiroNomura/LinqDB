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
    [Fact]public void Serialize(){
        var Constant1= Expression.Constant(1m);
        var input1=Expression.Add(Constant1,Constant1);
        this.MessagePack_Assert(new{a=input1},output=>{});
        this.MessagePack_Assert(new{a=default(BinaryExpression)},output=>{});

    }
    private void PrivateWrite<T>(ExpressionType NodeType){
        var Constant= Expression.Constant(default(T));
        var Parameter= Expression.Parameter(typeof(T),typeof(T).Name);
        var input1=Expression.MakeBinary(NodeType,Parameter,Constant);
        this.MessagePack_Assert(new{a=input1},output=>{});
        this.MessagePack_Assert(new{a=default(BinaryExpression)},output=>{});

    }
    [Fact]public void Write(){
        //this.PrivateWrite(ExpressionType.ArrayIndex);
        this.PrivateWrite<int>(ExpressionType.Assign            );
        this.PrivateWrite<string>(ExpressionType.Coalesce            );
        this.PrivateWrite<int>(ExpressionType.Add            );
        this.PrivateWrite<int>(ExpressionType.AddChecked     );
        this.PrivateWrite<int>(ExpressionType.And            );
        this.PrivateWrite<bool>(ExpressionType.AndAlso        );
        this.PrivateWrite<int>(ExpressionType.Divide         );
        this.PrivateWrite<int>(ExpressionType.ExclusiveOr    );
        this.PrivateWrite<int>(ExpressionType.LeftShift      );
        this.PrivateWrite<int>(ExpressionType.Modulo         );
        this.PrivateWrite<int>(ExpressionType.Multiply       );
        this.PrivateWrite<int>(ExpressionType.MultiplyChecked);
        this.PrivateWrite<int>(ExpressionType.Or             );
        this.PrivateWrite<bool>(ExpressionType.OrElse         );
        this.PrivateWrite<double>(ExpressionType.Power          );
        this.PrivateWrite<int>(ExpressionType.RightShift     );
        this.PrivateWrite<int>(ExpressionType.Subtract       );
        this.PrivateWrite<int>(ExpressionType.SubtractChecked);
        this.PrivateWrite<int>(ExpressionType.AddAssign            );
        this.PrivateWrite<int>(ExpressionType.AddAssignChecked     );
        this.PrivateWrite<int>(ExpressionType.DivideAssign         );
        this.PrivateWrite<int>(ExpressionType.AndAssign            );
        this.PrivateWrite<int>(ExpressionType.ExclusiveOrAssign    );
        this.PrivateWrite<int>(ExpressionType.LeftShiftAssign      );
        this.PrivateWrite<int>(ExpressionType.ModuloAssign         );
        this.PrivateWrite<int>(ExpressionType.MultiplyAssign       );
        this.PrivateWrite<int>(ExpressionType.MultiplyAssignChecked);
        this.PrivateWrite<int>(ExpressionType.OrAssign             );
        this.PrivateWrite<double>(ExpressionType.PowerAssign          );
        this.PrivateWrite<int>(ExpressionType.RightShiftAssign     );
        this.PrivateWrite<int>(ExpressionType.SubtractAssign       );
        this.PrivateWrite<int>(ExpressionType.SubtractAssignChecked);
        this.PrivateWrite<int>(ExpressionType.Equal             );
        this.PrivateWrite<int>(ExpressionType.GreaterThan       );
        this.PrivateWrite<int>(ExpressionType.GreaterThanOrEqual);
        this.PrivateWrite<int>(ExpressionType.LessThan          );
        this.PrivateWrite<int>(ExpressionType.LessThanOrEqual   );
        this.PrivateWrite<int>(ExpressionType.NotEqual          );
    }                                    
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
        var input1=Expression.AddAssign(ParameterDecimmal,Constant1,typeof(decimal).GetMethod("op_Addition"),ConversionDecimal);
        this.MemoryMessageJson_Expression(
            Expression.Lambda<Func<object>>(
                Expression.Block(
                    new[]{ParameterDecimmal},
                    Expression.Assign(
                        ParameterDecimmal,
                        Expression.Constant(0m)
                    ),
                    Expression.Convert(
                        input1,
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
                        input1,
                        typeof(object)
                    )
                )
            )
        );
    }
}
