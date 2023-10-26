//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.Formatters;
using Expressions = System.Linq.Expressions;
public class Binary:共通{
    [Fact]
    public void Serialize(){
        var Constant1=Expressions.Expression.Constant(1m);
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Add(Constant1,Constant1));

    }
    private void PrivateWrite<T>(Expressions.ExpressionType NodeType){
        this.MemoryMessageJson_Expression_Assert全パターン(
            Expressions.Expression.MakeBinary(
                NodeType,
                Expressions.Expression.Parameter(typeof(T),typeof(T).Name),
                Expressions.Expression.Constant(default(T))
            )
        );

    }
    [Fact]
    public void Write(){
        this.MemoryMessageJson_Expression_Assert全パターン(
            Expressions.Expression.ArrayIndex(
                Expressions.Expression.Parameter(typeof(int[])),
                Expressions.Expression.Constant(default(int))
            )
        );
        this.PrivateWrite<int>(Expressions.ExpressionType.Assign);
        this.PrivateWrite<string>(Expressions.ExpressionType.Coalesce);
        this.PrivateWrite<int>(Expressions.ExpressionType.Add);
        this.PrivateWrite<int>(Expressions.ExpressionType.AddChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.And);
        this.PrivateWrite<bool>(Expressions.ExpressionType.AndAlso);
        this.PrivateWrite<int>(Expressions.ExpressionType.Divide);
        this.PrivateWrite<int>(Expressions.ExpressionType.ExclusiveOr);
        this.PrivateWrite<int>(Expressions.ExpressionType.LeftShift);
        this.PrivateWrite<int>(Expressions.ExpressionType.Modulo);
        this.PrivateWrite<int>(Expressions.ExpressionType.Multiply);
        this.PrivateWrite<int>(Expressions.ExpressionType.MultiplyChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.Or);
        this.PrivateWrite<bool>(Expressions.ExpressionType.OrElse);
        this.PrivateWrite<double>(Expressions.ExpressionType.Power);
        this.PrivateWrite<int>(Expressions.ExpressionType.RightShift);
        this.PrivateWrite<int>(Expressions.ExpressionType.Subtract);
        this.PrivateWrite<int>(Expressions.ExpressionType.SubtractChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.AddAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.AddAssignChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.DivideAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.AndAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.ExclusiveOrAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.LeftShiftAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.ModuloAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.MultiplyAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.MultiplyAssignChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.OrAssign);
        this.PrivateWrite<double>(Expressions.ExpressionType.PowerAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.RightShiftAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.SubtractAssign);
        this.PrivateWrite<int>(Expressions.ExpressionType.SubtractAssignChecked);
        this.PrivateWrite<int>(Expressions.ExpressionType.Equal);
        this.PrivateWrite<int>(Expressions.ExpressionType.GreaterThan);
        this.PrivateWrite<int>(Expressions.ExpressionType.GreaterThanOrEqual);
        this.PrivateWrite<int>(Expressions.ExpressionType.LessThan);
        this.PrivateWrite<int>(Expressions.ExpressionType.LessThanOrEqual);
        this.PrivateWrite<int>(Expressions.ExpressionType.NotEqual);
    }
    [Fact]
    public void WriteLeftRight(){
        this.MemoryMessageJson_Expression_Assert全パターン(
            Expressions.Expression.Assign(
                Expressions.Expression.Parameter(typeof(int),"int32"),
                Expressions.Expression.Constant(1)
            )
        );
    }
    private static string string_string_string(string? a,string b)=>a??b;
    [Fact]
    public void WriteLeftRightLambda(){
        var ParameterString = Expressions.Expression.Parameter(typeof(string), "string");
        var ConstantString = Expressions.Expression.Constant("string");
        var ConversionString = Expressions.Expression.Lambda<Func<string, string>>(Expressions.Expression.Call(null, GetMethod(() => string_string_string("", "")), ParameterString, ParameterString), ParameterString);
        this.MemoryMessageJson_Expression_Assert全パターン(
            Expressions.Expression.Coalesce(
                Expressions.Expression.Parameter(typeof(string),"string"),
                Expressions.Expression.Constant("string"),
                Expressions.Expression.Lambda<Func<string,string>>(
                    Expressions.Expression.Call(
                        null,
                        GetMethod(()=>string_string_string("","")),
                        ParameterString,
                        ParameterString
                    ),
                    ParameterString
                )
            )
        );
    }
    [Fact]
    public void WriteLeftRightMethod(){
        var Constant1=Expressions.Expression.Constant(1m);
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Add(Constant1,Constant1));
    }
    [Fact]
    public void WriteLeftRightMethodLambda(){
        var ParameterDecimmal=Expressions.Expression.Parameter(typeof(decimal));
        var Constant1=Expressions.Expression.Constant(1m);
        var ConversionDecimal=Expressions.Expression.Lambda<Func<decimal,decimal>>(Expressions.Expression.Add(ParameterDecimmal,ParameterDecimmal),ParameterDecimmal);
        var input1=Expressions.Expression.AddAssign(ParameterDecimmal,Constant1,typeof(decimal).GetMethod("op_Addition"),ConversionDecimal);
        this.MemoryMessageJson_Expression_Assert全パターン(
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

        this.MemoryMessageJson_Expression_コンパイルリモート実行(
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
