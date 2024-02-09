#pragma warning disable CA1822 // Mark members as static
//#define タイム出力
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    //private Expressions.Expression 集約関数(ScalarExpression x,string FunctionName){
    //    Expressions.Expression Result;
    //    ref var RefPeek = ref this._StackSubquery単位の情報.RefPeek;
    //    var 集約関数のParameter = RefPeek.集約関数のParameter!;
    //    var 集約関数のSource = RefPeek.集約関数のSource!;
    //    RefPeek.集約関数のParameter=null;
    //    //Debug.Assert(集約関数のParameter is not null&&集約関数のSource is not null);
    //    switch(FunctionName) {
    //        case "avg": {
    //            RefPeek.集約関数の内部か=true;
    //            var Body = this.ScalarExpression(x)!;
    //            RefPeek.集約関数の内部か=false;
    //            //Body=this.ConvertNullable(Body);
    //            var Body_Type =Int32に変換して(ref Body);
    //            if     (typeof(int?    )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt32_selector  );
    //            else if(typeof(long?   )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt64_selector  );
    //            else if(typeof(float?  )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableSingle_selector );
    //            else if(typeof(double? )==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDouble_selector );
    //            else if(typeof(decimal?)==Body_Type)Result=共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDecimal_selector);
    //            else throw new NotSupportedException(Body_Type.FullName);
    //            break;
    //            Expressions.Expression 共通Nullable(Expressions.Expression Body,MethodInfo Method) {
    //                var 作業配列 = this.作業配列;
    //                Debug.Assert(共通Nullable is not null);
    //                Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
    //                var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
    //                return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
    //            }
    //        }
    //        case "sum": {
    //            RefPeek.集約関数の内部か=true;
    //            var Body = this.ScalarExpression(x)!;
    //            RefPeek.集約関数の内部か=false;
    //            var 作業配列 = this.作業配列;
    //            MethodInfo Method;
    //            var Body_Type =Int32に変換して(ref Body);
    //            if     (Body_Type==typeof(int     ))Method=Reflection.ExtensionSet.SumInt32_selector;
    //            else if(Body_Type==typeof(long    ))Method=Reflection.ExtensionSet.SumInt64_selector;
    //            else if(Body_Type==typeof(float   ))Method=Reflection.ExtensionSet.SumSingle_selector;
    //            else if(Body_Type==typeof(double  ))Method=Reflection.ExtensionSet.SumDouble_selector;
    //            else if(Body_Type==typeof(decimal ))Method=Reflection.ExtensionSet.SumDecimal_selector;
    //            else if(Body_Type==typeof(int?    ))Method=Reflection.ExtensionSet.SumNullableInt32_selector;
    //            else if(Body_Type==typeof(long?   ))Method=Reflection.ExtensionSet.SumNullableInt64_selector;
    //            else if(Body_Type==typeof(float?  ))Method=Reflection.ExtensionSet.SumNullableSingle_selector;
    //            else if(Body_Type==typeof(double? ))Method=Reflection.ExtensionSet.SumNullableDouble_selector;
    //            else if(Body_Type==typeof(decimal?))Method=Reflection.ExtensionSet.SumNullableDecimal_selector;
    //            else throw new NotSupportedException(Body_Type.FullName);
    //            Result=Expressions.Expression.Call(
    //                作業配列.MakeGenericMethod(Method,集約関数のParameter.Type),
    //                集約関数のSource,
    //                Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter))
    //            );
    //            break;
    //        }
    //        case "count": {
    //            Result=Expressions.Expression.Convert(
    //                Expressions.Expression.Call(
    //                    this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Count,IEnumerable1のT(集約関数のSource.Type)),
    //                    集約関数のSource
    //                ),
    //                typeof(int?)
    //            );
    //            break;
    //        }
    //        case "max":
    //        case "min":{
    //            RefPeek.集約関数の内部か=true;
    //            var Body = this.ScalarExpression(x);
    //            RefPeek.集約関数の内部か=false;
    //            //Body=this.Convert値型をNullable参照型は想定しない(Body);
    //            var Body_Type =Int32に変換して(ref Body);
    //            if     (Body_Type==typeof(int     ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt32_selector          :Reflection.ExtensionSet.MaxInt32_selector          );
    //            else if(Body_Type==typeof(long    ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt64_selector          :Reflection.ExtensionSet.MaxInt64_selector          );
    //            else if(Body_Type==typeof(float   ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinSingle_selector         :Reflection.ExtensionSet.MaxSingle_selector         );
    //            else if(Body_Type==typeof(double  ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDouble_selector         :Reflection.ExtensionSet.MaxDouble_selector         );
    //            else if(Body_Type==typeof(decimal ))Result=共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDecimal_selector        :Reflection.ExtensionSet.MaxDecimal_selector        );
    //            else if(Body_Type==typeof(int?    ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt32_selector  :Reflection.ExtensionSet.MaxNullableInt32_selector  );
    //            else if(Body_Type==typeof(long?   ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt64_selector  :Reflection.ExtensionSet.MaxNullableInt64_selector  );
    //            else if(Body_Type==typeof(float?  ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableSingle_selector :Reflection.ExtensionSet.MaxNullableSingle_selector );
    //            else if(Body_Type==typeof(double? ))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDouble_selector :Reflection.ExtensionSet.MaxNullableDouble_selector );
    //            else if(Body_Type==typeof(decimal?))Result=共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDecimal_selector:Reflection.ExtensionSet.MaxNullableDecimal_selector);
    //            else if(Body_Type==typeof(object  )){
    //                var Method=FunctionName=="min"?Reflection.ExtensionSet.MinTSource_selector        :Reflection.ExtensionSet.MaxTSource_selector;
    //                var 作業配列 = this.作業配列;
    //                Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type,typeof(object));
    //                var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
    //                Result=Expressions.Expression.Call(Method,集約関数のSource,Lambda);
    //            }
    //            else throw new NotSupportedException(Body_Type.FullName);
    //            break;
    //            Expressions.Expression 共通非Nullable用(Expressions.Expression Body,MethodInfo Method)=>this.ConvertNullable(共通Nullable用(Body,Method));
    //            Expressions.Expression 共通Nullable用(Expressions.Expression Body,MethodInfo Method) {
    //                var 作業配列 = this.作業配列;
    //                Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
    //                var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
    //                return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
    //            }
    //        }
    //        default:
    //            //Expressions.Expression.Dynamic(
    //            //    Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(
    //            //        CSharpBinderFlags.None,FunctionName,)
    //            //    Binder.Binder.
    //            //    変換_メソッド正規化_取得インライン不可能定数.DynamicReflection.)
    //            throw new NotSupportedException(FunctionName);
    //        static Type Int32に変換して(ref Expressions.Expression Body) {
    //            var Body_Type=Body.Type;
    //            if     (Body_Type==typeof(byte  ))Body=Expressions.Expression.Convert(Body,typeof(int ));
    //            else if(Body_Type==typeof(short ))Body=Expressions.Expression.Convert(Body,typeof(int ));
    //            else if(Body_Type==typeof(byte? ))Body=Expressions.Expression.Convert(Body,typeof(int?));
    //            else if(Body_Type==typeof(short?))Body=Expressions.Expression.Convert(Body,typeof(int?));
    //            return Body.Type;
    //        }
    //    }
    //    RefPeek.集約関数のParameter=集約関数のParameter;
    //    return Result;
    //}
//}
}
//615