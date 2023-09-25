using System.Diagnostics;
//using System.Linq.Expressions;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace Serializers.MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
public class Unary:共通 {
    private static 演算子 Unary演算子(演算子 a)=>~a;
    private static bool IsTrue演算子(演算子 a)=>a.HasValue;
    static int UnaryDouble(double a){
        return (int)a;
    }
    [Fact]public void Serialize(){
        var ConstantArray = Expressions.Expression.Constant(new int[10]);
        var Constant1= Expressions.Expression.Constant(1);
        var Constant1_1d= Expressions.Expression.Constant(1.1);
        var ConstantTrue= Expressions.Expression.Constant(true);
        var Constant演算子=Expressions.Expression.Constant(new 演算子(true));
        var Constant演算子1=Expressions.Expression.Constant(new 演算子1(true));
        var Parameter演算子=Expressions.Expression.Parameter(typeof(演算子));
        var ParameterInt32=Expressions.Expression.Parameter(typeof(int));
        this.MemoryMessageJson_Expression(Expressions.Expression.ArrayLength(Expressions.Expression.Constant(new int[1])));
        共通1(Expressions.Expression.ArrayLength(ConstantArray));
        共通1(Expressions.Expression.Quote(Expressions.Expression.Lambda(ConstantArray)));
        共通1(Expressions.Expression.Throw(Expressions.Expression.New(typeof(InvalidOperationException).GetConstructor(Type.EmptyTypes)!)));
        共通1(Expressions.Expression.TypeAs(Expressions.Expression.New(typeof(Exception).GetConstructor(Type.EmptyTypes)!),typeof(InvalidOperationException)));
        共通1(Expressions.Expression.Unbox(Expressions.Expression.Constant(1,typeof(object)),typeof(int)));
        共通1(Expressions.Expression.Convert(Constant1_1d,typeof(int)));
        共通1(Expressions.Expression.ConvertChecked(Constant1_1d,typeof(int)));
        共通1(Expressions.Expression.Decrement(Constant1_1d));
        共通1(Expressions.Expression.Increment(Constant1_1d));
        共通1(Expressions.Expression.IsFalse(ConstantTrue));
        共通1(Expressions.Expression.IsTrue(ConstantTrue));
        共通1(Expressions.Expression.Negate(Constant1_1d));
        共通1(Expressions.Expression.NegateChecked(Constant1_1d));
        共通1(Expressions.Expression.Not(Constant1));
        共通1(Expressions.Expression.OnesComplement(Constant1));
        共通1(Expressions.Expression.Decrement(Constant1_1d));
        共通1(Expressions.Expression.Increment(Constant1_1d));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PostDecrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PostIncrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PreDecrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PreIncrementAssign(ParameterInt32));
        共通1(Expressions.Expression.UnaryPlus(Constant1_1d));

        共通1(Expressions.Expression.Convert(Constant演算子,typeof(演算子1)));
        共通1(Expressions.Expression.ConvertChecked(Constant演算子,typeof(演算子1)));
        共通1(Expressions.Expression.Convert(Constant演算子1,typeof(演算子)));
        共通1(Expressions.Expression.ConvertChecked(Constant演算子1,typeof(演算子)));
        共通1(Expressions.Expression.Decrement(Constant演算子));
        共通1(Expressions.Expression.Increment(Constant演算子));
        共通1(Expressions.Expression.IsFalse(Constant演算子));
        共通1(Expressions.Expression.IsTrue(Constant演算子));
        共通1(Expressions.Expression.Negate(Constant演算子));
        共通1(Expressions.Expression.NegateChecked(Constant演算子));
        共通1(Expressions.Expression.OnesComplement(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.Decrement(Constant演算子));
        共通1(Expressions.Expression.Increment(Constant演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PostDecrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PostIncrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PreDecrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PreIncrementAssign(Parameter演算子));
        共通1(Expressions.Expression.UnaryPlus(Constant演算子));


        共通1(Expressions.Expression.Convert(Constant1_1d,typeof(int),GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.ConvertChecked(Constant1_1d,typeof(int),GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.Decrement(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.Increment(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.IsFalse(Constant演算子,GetMethod(nameof(IsTrue演算子))));
        共通1(Expressions.Expression.IsTrue(Constant演算子,GetMethod(nameof(IsTrue演算子))));
        共通1(Expressions.Expression.Negate(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.NegateChecked(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.OnesComplement(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.Decrement(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.Increment(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.UnaryPlus(Constant演算子,GetMethod(nameof(Unary演算子))));
        void 共通0(Expressions.ParameterExpression 代入先,Expressions.ConstantExpression 代入元,Expressions.UnaryExpression a)=>this.MessagePack_Assert(new{a},output=>{});
        void 共通1(Expressions.UnaryExpression Unary)=>this.MessagePack_Assert(new{Unary},output=>{});
    }
    private void PrivateWrite<T>(Expressions.ExpressionType NodeType){
        var Constant= Expressions.Expression.Constant(default(T));
        var Parameter= Expressions.Expression.Parameter(typeof(T),typeof(T).Name);
        var input1=Expressions.Expression.MakeBinary(NodeType,Parameter,Constant);
        this.MessagePack_Assert(new{a=input1},output=>{});

    }
    [Fact]public void Write(){

        {
            var Parameter= Expressions.Expression.Parameter(typeof(int[]));
            var Constant= Expressions.Expression.Constant(default(int));
            var input=Expressions.Expression.ArrayIndex(Parameter,Constant);
            this.MessagePack_Assert(new{a=input},output=>{});
            this.MessagePack_Assert(new{a=default(Expressions.UnaryExpression) },output=>{});

        }
        this.PrivateWrite<int>(Expressions.ExpressionType.Assign            );
        this.PrivateWrite<string>(Expressions.ExpressionType.Coalesce            );
        this.PrivateWrite<int>(Expressions.ExpressionType.Add            );
        this.PrivateWrite<int>(Expressions.ExpressionType.AddChecked     );
        this.PrivateWrite<int>(Expressions.ExpressionType.And            );
        this.PrivateWrite<bool>(Expressions.ExpressionType.AndAlso        );
        this.PrivateWrite<int>(Expressions.ExpressionType.Divide         );
        this.PrivateWrite<int>(Expressions.ExpressionType.ExclusiveOr    );
        this.PrivateWrite<int>(Expressions.ExpressionType.LeftShift      );
        this.PrivateWrite<int>(Expressions.ExpressionType.Modulo         );
        this.PrivateWrite<int>(Expressions.ExpressionType.Multiply       );
        this.PrivateWrite<int>(Expressions.ExpressionType.MultiplyChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.Or             );
        this.PrivateWrite<bool>(Expressions.ExpressionType.OrElse         );
        this.PrivateWrite<double>(Expressions.ExpressionType.Power          );
        this.PrivateWrite<int>(Expressions.ExpressionType.RightShift     );
        this.PrivateWrite<int>(Expressions.ExpressionType.Subtract       );
        this.PrivateWrite<int>(Expressions.ExpressionType.SubtractChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.AddAssign            );
        this.PrivateWrite<int>(Expressions.ExpressionType.AddAssignChecked     );
        this.PrivateWrite<int>(Expressions.ExpressionType.DivideAssign         );
        this.PrivateWrite<int>(Expressions.ExpressionType.AndAssign            );
        this.PrivateWrite<int>(Expressions.ExpressionType.ExclusiveOrAssign    );
        this.PrivateWrite<int>(Expressions.ExpressionType.LeftShiftAssign      );
        this.PrivateWrite<int>(Expressions.ExpressionType.ModuloAssign         );
        this.PrivateWrite<int>(Expressions.ExpressionType.MultiplyAssign       );
        this.PrivateWrite<int>(Expressions.ExpressionType.MultiplyAssignChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.OrAssign             );
        this.PrivateWrite<double>(Expressions.ExpressionType.PowerAssign          );
        this.PrivateWrite<int>(Expressions.ExpressionType.RightShiftAssign     );
        this.PrivateWrite<int>(Expressions.ExpressionType.SubtractAssign       );
        this.PrivateWrite<int>(Expressions.ExpressionType.SubtractAssignChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.Equal             );
        this.PrivateWrite<int>(Expressions.ExpressionType.GreaterThan       );
        this.PrivateWrite<int>(Expressions.ExpressionType.GreaterThanOrEqual);
        this.PrivateWrite<int>(Expressions.ExpressionType.LessThan          );
        this.PrivateWrite<int>(Expressions.ExpressionType.LessThanOrEqual   );
        this.PrivateWrite<int>(Expressions.ExpressionType.NotEqual          );
    }                                    
    [Fact]public void WriteLeftRight(){
        var ParameterDouble=Expressions.Expression.Parameter(typeof(double));
        var ParameterInt32= Expressions.Expression.Parameter(typeof(int),"int32");
        var ParameterString= Expressions.Expression.Parameter(typeof(string),"string");
        var ConstantString = Expressions.Expression.Constant("string");
        //var ConstantStringNull=Expressions.Expression.Constant(null,typeof(string));
        var ConstantArray = Expressions.Expression.Constant(new int[10]);
        var Constant0= Expressions.Expression.Constant(0);
        var Constant1= Expressions.Expression.Constant(1);
        var ConstantTrue = Expressions.Expression.Constant(true);
        var ConstantBinry= Expressions.Expression.Constant(new 演算子(true));
        var ConversionInt32=Expressions.Expression.Lambda<Func<int,int>>(Expressions.Expression.Add(ParameterInt32,ParameterInt32),ParameterInt32);
        var ConversionDouble=Expressions.Expression.Lambda<Func<double,double>>(Expressions.Expression.Add(ParameterDouble,ParameterDouble),ParameterDouble);
        this.MemoryMessageJson_Expression(Expressions.Expression.Assign(ParameterInt32,Constant1));
    }
    private static string string_string_string(string? a,string b)=>a??b;
    [Fact]public void WriteLeftRightLambda(){
        var ParameterString= Expressions.Expression.Parameter(typeof(string),"string");
        var ConstantString = Expressions.Expression.Constant("string");
        var ConversionString=Expressions.Expression.Lambda<Func<string,string>>(Expressions.Expression.Call(null,GetMethod(()=>string_string_string("","")),ParameterString,ParameterString),ParameterString);
        this.MemoryMessageJson_Expression(Expressions.Expression.Coalesce(ConstantString,ConstantString,ConversionString));
    }
    [Fact]public void WriteLeftRightMethod(){
        var Constant1= Expressions.Expression.Constant(1m);
        this.MemoryMessageJson_Expression(Expressions.Expression.Add(Constant1,Constant1));
    }
    [Fact]public void WriteLeftRightMethodLambda(){
        var ParameterDecimmal=Expressions.Expression.Parameter(typeof(decimal));
        var Constant1= Expressions.Expression.Constant(1m);
        var ConversionDecimal=Expressions.Expression.Lambda<Func<decimal,decimal>>(Expressions.Expression.Add(ParameterDecimmal,ParameterDecimmal),ParameterDecimmal);
        var input1=Expressions.Expression.AddAssign(ParameterDecimmal,Constant1,typeof(decimal).GetMethod("op_Addition"),ConversionDecimal);
        this.MemoryMessageJson_Expression(
            Expressions.Expression.Lambda<Func<object>>(
                Expressions.Expression.Block(
                    new[]{ParameterDecimmal},
                    Expressions.Expression.Assign(
                        ParameterDecimmal,
                        Expressions.Expression.Constant(0m)
                    ),
                    Expressions.Expression.Convert(
                        input1,
                        typeof(object)
                    )
                )
            )
        );

        this.MemoryMessageJson_TExpressionObject_コンパイル実行(
            Expressions.Expression.Lambda<Func<object>>(
                Expressions.Expression.Block(
                    new[]{ParameterDecimmal},
                    Expressions.Expression.Assign(
                        ParameterDecimmal,
                        Expressions.Expression.Constant(0m)
                    ),
                    Expressions.Expression.Convert(
                        input1,
                        typeof(object)
                    )
                )
            )
        );
    }
}
