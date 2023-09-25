using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
using System.Security.Cryptography;

public class Expression:共通 {
    private void Binary(){
        {
            var Parameter= Expressions.Expression.Parameter(typeof(int[]));
            var Constant= Expressions.Expression.Constant(default(int));
            var input=Expressions.Expression.ArrayIndex(Parameter,Constant);
            this.MessagePack_Assert(new{a=input},output=>{});
            this.MessagePack_Assert(new{a=default(Expressions.BinaryExpression) },output=>{});

        }
        共通<int>(Expressions.ExpressionType.Assign            );
        共通<string>(Expressions.ExpressionType.Coalesce            );
        共通<int>(Expressions.ExpressionType.Add            );
        共通<int>(Expressions.ExpressionType.AddChecked     );
        共通<int>(Expressions.ExpressionType.And            );
        共通<bool>(Expressions.ExpressionType.AndAlso        );
        共通<int>(Expressions.ExpressionType.Divide         );
        共通<int>(Expressions.ExpressionType.ExclusiveOr    );
        共通<int>(Expressions.ExpressionType.LeftShift      );
        共通<int>(Expressions.ExpressionType.Modulo         );
        共通<int>(Expressions.ExpressionType.Multiply       );
        共通<int>(Expressions.ExpressionType.MultiplyChecked);
        共通<int>(Expressions.ExpressionType.Or             );
        共通<bool>(Expressions.ExpressionType.OrElse         );
        共通<double>(Expressions.ExpressionType.Power          );
        共通<int>(Expressions.ExpressionType.RightShift     );
        共通<int>(Expressions.ExpressionType.Subtract       );
        共通<int>(Expressions.ExpressionType.SubtractChecked);
        共通<int>(Expressions.ExpressionType.AddAssign            );
        共通<int>(Expressions.ExpressionType.AddAssignChecked     );
        共通<int>(Expressions.ExpressionType.DivideAssign         );
        共通<int>(Expressions.ExpressionType.AndAssign            );
        共通<int>(Expressions.ExpressionType.ExclusiveOrAssign    );
        共通<int>(Expressions.ExpressionType.LeftShiftAssign      );
        共通<int>(Expressions.ExpressionType.ModuloAssign         );
        共通<int>(Expressions.ExpressionType.MultiplyAssign       );
        共通<int>(Expressions.ExpressionType.MultiplyAssignChecked);
        共通<int>(Expressions.ExpressionType.OrAssign             );
        共通<double>(Expressions.ExpressionType.PowerAssign          );
        共通<int>(Expressions.ExpressionType.RightShiftAssign     );
        共通<int>(Expressions.ExpressionType.SubtractAssign       );
        共通<int>(Expressions.ExpressionType.SubtractAssignChecked);
        共通<int>(Expressions.ExpressionType.Equal             );
        共通<int>(Expressions.ExpressionType.GreaterThan       );
        共通<int>(Expressions.ExpressionType.GreaterThanOrEqual);
        共通<int>(Expressions.ExpressionType.LessThan          );
        共通<int>(Expressions.ExpressionType.LessThanOrEqual   );
        共通<int>(Expressions.ExpressionType.NotEqual          );
        void 共通<T>(Expressions.ExpressionType NodeType){
            var Constant= Expressions.Expression.Constant(default(T));
            var Parameter= Expressions.Expression.Parameter(typeof(T),typeof(T).Name);
            Expressions.Expression input=Expressions.Expression.MakeBinary(NodeType,Parameter,Constant);
            this.MessagePack_Assert(new{a=input},output=>{});
        }
    }
    [Fact]
    public void Serialize(){
        this.Binary();
        this.Unary();
    }
    private static 演算子 Unary演算子(演算子 a)=>~a;
    private static bool IsTrue演算子(演算子 a)=>a.HasValue;
    static int UnaryDouble(double a){
        return (int)a;
    }
    private void Unary(){
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

        //共通1(Expressions.Expression.Convert(Constant演算子,typeof(演算子1)));
        //共通1(Expressions.Expression.ConvertChecked(Constant演算子,typeof(演算子1)));
        //共通1(Expressions.Expression.Convert(Constant演算子1,typeof(演算子)));
        //共通1(Expressions.Expression.ConvertChecked(Constant演算子1,typeof(演算子)));
        //共通1(Expressions.Expression.Decrement(Constant演算子));
        //共通1(Expressions.Expression.Increment(Constant演算子));
        //共通1(Expressions.Expression.IsFalse(Constant演算子));
        //共通1(Expressions.Expression.IsTrue(Constant演算子));
        //共通1(Expressions.Expression.Negate(Constant演算子));
        //共通1(Expressions.Expression.NegateChecked(Constant演算子));
        //共通1(Expressions.Expression.OnesComplement(Constant演算子,GetMethod(nameof(Unary演算子))));
        //共通1(Expressions.Expression.Decrement(Constant演算子));
        //共通1(Expressions.Expression.Increment(Constant演算子));
        //共通0(Parameter演算子,Constant演算子,Expressions.Expression.PostDecrementAssign(Parameter演算子));
        //共通0(Parameter演算子,Constant演算子,Expressions.Expression.PostIncrementAssign(Parameter演算子));
        //共通0(Parameter演算子,Constant演算子,Expressions.Expression.PreDecrementAssign(Parameter演算子));
        //共通0(Parameter演算子,Constant演算子,Expressions.Expression.PreIncrementAssign(Parameter演算子));
        //共通1(Expressions.Expression.UnaryPlus(Constant演算子));


        //共通1(Expressions.Expression.Convert(Constant1_1d,typeof(int),GetMethod(()=>UnaryDouble(0))));
        //共通1(Expressions.Expression.ConvertChecked(Constant1_1d,typeof(int),GetMethod(()=>UnaryDouble(0))));
        //共通1(Expressions.Expression.Decrement(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        //共通1(Expressions.Expression.Increment(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        //共通1(Expressions.Expression.IsFalse(Constant演算子,GetMethod(nameof(IsTrue演算子))));
        //共通1(Expressions.Expression.IsTrue(Constant演算子,GetMethod(nameof(IsTrue演算子))));
        //共通1(Expressions.Expression.Negate(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        //共通1(Expressions.Expression.NegateChecked(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        //共通1(Expressions.Expression.OnesComplement(Constant演算子,GetMethod(nameof(Unary演算子))));
        //共通1(Expressions.Expression.Decrement(Constant演算子,GetMethod(nameof(Unary演算子))));
        //共通1(Expressions.Expression.Increment(Constant演算子,GetMethod(nameof(Unary演算子))));
        //共通1(Expressions.Expression.UnaryPlus(Constant演算子,GetMethod(nameof(Unary演算子))));
        void 共通0(Expressions.ParameterExpression 代入先,Expressions.ConstantExpression 代入元,Expressions.UnaryExpression a)=>this.MessagePack_Assert(new{a},output=>{});
        void 共通1(Expressions.UnaryExpression Unary)=>this.MessagePack_Assert(new{Unary},output=>{});
    }
}
