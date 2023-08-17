//using static BackendClient.ExtendAggregate;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable InvertIf
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable RedundantCast
// ReSharper disable UseCollectionCountProperty
// ReSharper disable RedundantExplicitArrayCreation
// ReSharper disable UnusedTypeParameter
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable ReplaceWithSingleCallToAny

namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_A作成IL: ATest
{
    private interface I
    {
    }
    private class A : I
    {
    }
    private class B : A
    {
    }
    public struct S0 : I
    {
        public readonly int v;
        public S0(int v) => this.v=v;
    }
    /// <summary>
    /// structはpublicにしないとTypeBuilderでLoadException access deniedしてしまう
    /// </summary>
    public struct S1:I {
        public readonly int v;
        public S1(int v) => this.v=v;
        public static explicit operator S0(S1 s1) => new(s1.v);
        public static explicit operator S1(S0 s0) => new(s0.v);
    }
    private class C0:I,IEquatable<C0>{
        private readonly int v;
        public C0(int v) => this.v=v;
        public static explicit operator C1(C0 c1) => new(c1.v);
        public static explicit operator C0(C1 c0) => new(c0.v);

        public bool Equals(C0? other){
            if(ReferenceEquals(null,other)) return false;
            if(ReferenceEquals(this,other)) return true;
            return this.v==other.v;
        }

        public override bool Equals(object? obj){
            if(ReferenceEquals(null,obj)) return false;
            if(ReferenceEquals(this,obj)) return true;
            if(obj.GetType()!=this.GetType()) return false;
            return this.Equals((C0)obj);
        }

        public override int GetHashCode(){
            return this.v;
        }
    }
    private class C1:I,IEquatable<C1>{
        public readonly int v;
        public C1(int v) => this.v=v;
        public static explicit operator C1(S1 s1) => new(s1.v);
        public static explicit operator S1(C1 s0) => new(s0.v);

        public bool Equals(C1? other){
            if(ReferenceEquals(null,other)) return false;
            if(ReferenceEquals(this,other)) return true;
            return this.v==other.v;
        }

        public override bool Equals(object? obj){
            if(ReferenceEquals(null,obj)) return false;
            if(ReferenceEquals(this,obj)) return true;
            if(obj.GetType()!=this.GetType()) return false;
            return this.Equals((C1)obj);
        }

        public override int GetHashCode(){
            return this.v;
        }

        public static bool operator==(C1? left,C1? right){
            return Equals(left,right);
        }

        public static bool operator!=(C1? left,C1? right){
            return!Equals(left,right);
        }
    }
    [TestMethod]
    public void CovertNullableMethod(){
        var D = 1d;
        var S1 = new S1(2);
        var C1 = new C1(3);
        var DConstant = Expression.Constant(D);
        var NDConstant =Expression.Constant(D,typeof(double?));
        var ODConstant = Expression.Constant(D,typeof(object));
        var S1Constant = Expression.Constant(S1);
        var C1Constant = Expression.Constant(C1);
        var OC1Constant = Expression.Constant(C1,typeof(object));
        var NS1Constant = Expression.Constant(S1,typeof(S1?));
        this.Execute2(
            Expression.Lambda<Func<double?>>(
                Expression.Convert(
                    ODConstant,
                    typeof(double?)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<S1?>>(
                Expression.Convert(
                    C1Constant,
                    typeof(S1?)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<decimal?>>(
                Expression.Convert(
                    NDConstant,
                    typeof(decimal?)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<float?>>(
                Expression.Convert(
                    NDConstant,
                    typeof(float?)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Convert(
                    NDConstant,
                    typeof(decimal)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<float>>(
                Expression.Convert(
                    NDConstant,
                    typeof(float)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<decimal?>>(
                Expression.Convert(
                    DConstant,
                    typeof(decimal?)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<float?>>(
                Expression.Convert(
                    DConstant,
                    typeof(float?)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Convert(
                    DConstant,
                    typeof(decimal)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<float>>(
                Expression.Convert(
                    DConstant,
                    typeof(float)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<double>>(
                Expression.Convert(
                    ODConstant,
                    typeof(double)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<object>>(
                Expression.Convert(
                    NDConstant,
                    typeof(object)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<C0>>(
                Expression.Convert(
                    C1Constant,
                    typeof(C0)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<object>>(
                Expression.Convert(
                    C1Constant,
                    typeof(object)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<C1>>(
                Expression.Convert(
                    OC1Constant,
                    typeof(C1)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<C1>>(
                Expression.Convert(
                    NS1Constant,
                    typeof(C1)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<C1>>(
                Expression.Convert(
                    S1Constant,
                    typeof(C1)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<object>>(
                Expression.Convert(
                    S1Constant,
                    typeof(object)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<I>>(
                Expression.Convert(
                    S1Constant,
                    typeof(I)
                )
            )
        );
    }
}