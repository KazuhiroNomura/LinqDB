using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
namespace LinqDB.Optimizers;
using static Common;
/// <summary>
/// ガベージヒープを節約するための作業用メモリプール。
/// </summary>
public class 作業配列 {
    public readonly Type[] Types1=new Type[1];
    public readonly Type[] Types2=new Type[2];
    public readonly Type[] Types3=new Type[3];
    public readonly Type[] Types4=new Type[4];
    public readonly Type[] Types5=new Type[5];
    public readonly Type[] Types6=new Type[6];
    public readonly Type[] Types7=new Type[7];
    public readonly Type[] Types8=new Type[8];
    /// <summary>
    /// あるTypeに存在するGenericMethod
    /// </summary>
    /// <param name="DeclaringType"></param>
    /// <param name="Name"></param>
    /// <param name="Type0"></param>
    /// <returns></returns>
    public MethodInfo GetMethod(Type DeclaringType, string Name, Type Type0)=> DeclaringType.GetMethod(Name, Instance_NonPublic_Public, null, this.Types設定(Type0), null)!;
    /// <summary>
    /// あるTypeに存在するGenericMethod
    /// </summary>
    /// <param name="DeclaringType"></param>
    /// <param name="Name"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <returns></returns>
    public MethodInfo GetMethod(Type DeclaringType, string Name, Type Type0, Type Type1)=> DeclaringType.GetMethod(Name, Instance_NonPublic_Public, null, this.Types設定(Type0, Type1), null)!;
    /// <summary>
    /// TypeのGenericParameter。配列型はその要素。
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public Type[] GetGenericArguments(Type Type)=> this.Types設定(
        Type.IsArray
            ? Type.GetElementType()!
            : Type.GetGenericArguments()[0]
    );
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition, Type Type0) {
        var Types=this.Types1;
        Types[0]=Type0;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition, Type Type0, Type Type1) {
        var Types=this.Types2;
        Types[0]=Type0;
        Types[1]=Type1;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition, Type Type0, Type Type1, Type Type2) {
        var Types=this.Types3;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3) {
        var Types = this.Types4;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4) {
        var Types = this.Types5;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5) {
        var Types = this.Types6;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5,Type Type6) {
        var Types = this.Types7;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// GenericTypeを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <param name="Type7"></param>
    /// <returns></returns>
    public Type MakeGenericType(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5,Type Type6,Type Type7) {
        var Types = this.Types8;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        Types[7]=Type7;
        return GenericTypeDefinition.MakeGenericType(Types);
    }
    /// <summary>
    /// ElementTypeからGenericMethod
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="EnumerableType"></param>
    /// <returns></returns>
    public MethodInfo ElementTypeからMakeGenericMethod(MethodInfo GenericMethodDefinition, Type EnumerableType) {
        var Types=this.Types1;
        Types[0]=IEnumerable1のT(EnumerableType);
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0) {
        var Types = this.Types1;
        Types[0]=Type0;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0,Type Type1) {
        var Types = this.Types2;
        Types[0]=Type0;
        Types[1]=Type1;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2) {
        var Types = this.Types3;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3) {
        var Types = this.Types4;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4) {
        var Types = this.Types5;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5) {
        var Types = this.Types6;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5,Type Type6) {
        var Types = this.Types7;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericConstructorを作る。
    /// </summary>
    /// <param name="GenericTypeDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <param name="Type7"></param>
    /// <returns></returns>
    public ConstructorInfo MakeValueTuple_ctor(Type GenericTypeDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5,Type Type6,Type Type7) {
        var Types = this.Types8;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        Types[7]=Type7;
        return GenericTypeDefinition.MakeGenericType(Types).GetConstructor(Types)!;
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition, Type Type0) {
        var Types=this.Types1;
        Types[0]=Type0;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition, Type Type0, Type Type1) {
        var Types=this.Types2;
        Types[0]=Type0;
        Types[1]=Type1;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition,Type Type0,Type Type1,Type Type2) {
        var Types = this.Types3;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition, Type Type0, Type Type1, Type Type2, Type Type3) {
        var Types=this.Types4;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4) {
        var Types = this.Types5;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5) {
        var Types = this.Types6;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5,Type Type6) {
        var Types = this.Types7;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// GenericMethodを作る。
    /// </summary>
    /// <param name="GenericMethodDefinition"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <param name="Type7"></param>
    /// <returns></returns>
    public MethodInfo MakeGenericMethod(MethodInfo GenericMethodDefinition,Type Type0,Type Type1,Type Type2,Type Type3,Type Type4,Type Type5,Type Type6,Type Type7) {
        var Types = this.Types8;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        Types[7]=Type7;
        return GenericMethodDefinition.MakeGenericMethod(Types);
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0) {
        var Types=this.Types1;
        Types[0]=Type0;
        return Types;
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0, Type Type1) {
        var Types=this.Types2;
        Types[0]=Type0;
        Types[1]=Type1;
        return Types;
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0, Type Type1, Type Type2) {
        var Types=this.Types3;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        return Types;
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0, Type Type1, Type Type2, Type Type3) {
        var Types=this.Types4;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        return Types;
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0, Type Type1, Type Type2, Type Type3, Type Type4) {
        var Types=this.Types5;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        return Types;
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0, Type Type1, Type Type2, Type Type3, Type Type4, Type Type5) {
        var Types=this.Types6;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        return Types;
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0, Type Type1, Type Type2, Type Type3, Type Type4, Type Type5, Type Type6) {
        var Types=this.Types7;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        return Types;
    }
    /// <summary>
    /// TypeからType配列を作る。
    /// </summary>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <param name="Type2"></param>
    /// <param name="Type3"></param>
    /// <param name="Type4"></param>
    /// <param name="Type5"></param>
    /// <param name="Type6"></param>
    /// <param name="Type7"></param>
    /// <returns></returns>
    public Type[] Types設定(Type Type0, Type Type1, Type Type2, Type Type3, Type Type4, Type Type5, Type Type6, Type Type7) {
        var Types=this.Types8;
        Types[0]=Type0;
        Types[1]=Type1;
        Types[2]=Type2;
        Types[3]=Type3;
        Types[4]=Type4;
        Types[5]=Type5;
        Types[6]=Type6;
        Types[7]=Type7;
        return Types;
    }
    /// <summary>
    /// Typeに存在するCunstructor
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Type0"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstructorInfo GetConstructor(Type Type, Type Type0) {
        var Types=this.Types1;
        Types[0]=Type0;
        return Type.GetConstructor(Types)!;
    }
    /// <summary>
    /// Typeに存在するCunstructor
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Type0"></param>
    /// <param name="Type1"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ConstructorInfo GetConstructor(Type Type, Type Type0, Type Type1) {
        var Types=this.Types2;
        Types[0]=Type0;
        Types[1]=Type1;
        return Type.GetConstructor(Types)!;
    }
    internal readonly ParameterExpression[] Parameters1=new ParameterExpression[1];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0) {
        var Parameters=this.Parameters1;
        Parameters[0]=Parameter0;
        return Parameters;
    }

    internal readonly ParameterExpression[] Parameters2=new ParameterExpression[2];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1) {
        var Parameters=this.Parameters2;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        return Parameters;
    }
    internal readonly ParameterExpression[] Parameters3=new ParameterExpression[3];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <param name="Parameter2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1, ParameterExpression Parameter2) {
        var Parameters=this.Parameters3;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        Parameters[2]=Parameter2;
        return Parameters;
    }
    internal readonly ParameterExpression[] Parameters4=new ParameterExpression[4];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <param name="Parameter2"></param>
    /// <param name="Parameter3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1, ParameterExpression Parameter2, ParameterExpression Parameter3) {
        var Parameters=this.Parameters4;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        Parameters[2]=Parameter2;
        Parameters[3]=Parameter3;
        return Parameters;
    }
    private readonly ParameterExpression[] Parameters5=new ParameterExpression[5];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <param name="Parameter2"></param>
    /// <param name="Parameter3"></param>
    /// <param name="Parameter4"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1, ParameterExpression Parameter2, ParameterExpression Parameter3, ParameterExpression Parameter4) {
        var Parameters=this.Parameters5;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        Parameters[2]=Parameter2;
        Parameters[3]=Parameter3;
        Parameters[4]=Parameter4;
        return Parameters;
    }
    private readonly ParameterExpression[] Parameters6=new ParameterExpression[6];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <param name="Parameter2"></param>
    /// <param name="Parameter3"></param>
    /// <param name="Parameter4"></param>
    /// <param name="Parameter5"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1, ParameterExpression Parameter2, ParameterExpression Parameter3, ParameterExpression Parameter4, ParameterExpression Parameter5) {
        var Parameters=this.Parameters6;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        Parameters[2]=Parameter2;
        Parameters[3]=Parameter3;
        Parameters[4]=Parameter4;
        Parameters[5]=Parameter5;
        return Parameters;
    }
    private readonly ParameterExpression[] Parameters7=new ParameterExpression[7];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <param name="Parameter2"></param>
    /// <param name="Parameter3"></param>
    /// <param name="Parameter4"></param>
    /// <param name="Parameter5"></param>
    /// <param name="Parameter6"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1, ParameterExpression Parameter2, ParameterExpression Parameter3, ParameterExpression Parameter4, ParameterExpression Parameter5, ParameterExpression Parameter6) {
        var Parameters=this.Parameters7;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        Parameters[2]=Parameter2;
        Parameters[3]=Parameter3;
        Parameters[4]=Parameter4;
        Parameters[5]=Parameter5;
        Parameters[6]=Parameter6;
        return Parameters;
    }
    private readonly ParameterExpression[] Parameters8=new ParameterExpression[8];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <param name="Parameter2"></param>
    /// <param name="Parameter3"></param>
    /// <param name="Parameter4"></param>
    /// <param name="Parameter5"></param>
    /// <param name="Parameter6"></param>
    /// <param name="Parameter7"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1, ParameterExpression Parameter2, ParameterExpression Parameter3, ParameterExpression Parameter4, ParameterExpression Parameter5, ParameterExpression Parameter6, ParameterExpression Parameter7) {
        var Parameters=this.Parameters8;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        Parameters[2]=Parameter2;
        Parameters[3]=Parameter3;
        Parameters[4]=Parameter4;
        Parameters[5]=Parameter5;
        Parameters[6]=Parameter6;
        Parameters[7]=Parameter7;
        return Parameters;
    }
    private readonly ParameterExpression[] Parameters9=new ParameterExpression[9];
    /// <summary>
    /// Parameter配列を作る。
    /// </summary>
    /// <param name="Parameter0"></param>
    /// <param name="Parameter1"></param>
    /// <param name="Parameter2"></param>
    /// <param name="Parameter3"></param>
    /// <param name="Parameter4"></param>
    /// <param name="Parameter5"></param>
    /// <param name="Parameter6"></param>
    /// <param name="Parameter7"></param>
    /// <param name="Parameter8"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterExpression[] Parameters設定(ParameterExpression Parameter0, ParameterExpression Parameter1, ParameterExpression Parameter2, ParameterExpression Parameter3, ParameterExpression Parameter4, ParameterExpression Parameter5, ParameterExpression Parameter6, ParameterExpression Parameter7, ParameterExpression Parameter8) {
        var Parameters=this.Parameters9;
        Parameters[0]=Parameter0;
        Parameters[1]=Parameter1;
        Parameters[2]=Parameter2;
        Parameters[3]=Parameter3;
        Parameters[4]=Parameter4;
        Parameters[5]=Parameter5;
        Parameters[6]=Parameter6;
        Parameters[7]=Parameter7;
        Parameters[8]=Parameter8;
        return Parameters;
    }
    internal readonly Expression[] Expressions1=new Expression[1];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0) {
        var Expressions=this.Expressions1;
        Expressions[0]=Expression0;
        return Expressions;
    }
    internal readonly Expression[] Expressions2=new Expression[2];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1) {
        var Expressions=this.Expressions2;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        return Expressions;
    }
    private readonly Expression[] Expressions3=new Expression[3];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2) {
        var Expressions=this.Expressions3;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        return Expressions;
    }
    internal readonly Expression[] Expressions4=new Expression[4];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3) {
        var Expressions=this.Expressions4;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        return Expressions;
    }
    internal readonly Expression[] Expressions5=new Expression[5];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <param name="Expression4"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3, Expression Expression4) {
        var Expressions=this.Expressions5;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        Expressions[4]=Expression4;
        return Expressions;
    }
    private readonly Expression[] Expressions6=new Expression[6];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <param name="Expression4"></param>
    /// <param name="Expression5"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3, Expression Expression4, Expression Expression5) {
        var Expressions=this.Expressions6;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        Expressions[4]=Expression4;
        Expressions[5]=Expression5;
        return Expressions;
    }
    private readonly Expression[] Expressions7=new Expression[7];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <param name="Expression4"></param>
    /// <param name="Expression5"></param>
    /// <param name="Expression6"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3, Expression Expression4, Expression Expression5, Expression Expression6) {
        var Expressions=this.Expressions7;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        Expressions[4]=Expression4;
        Expressions[5]=Expression5;
        Expressions[6]=Expression6;
        return Expressions;
    }
    public readonly Expression[] Expressions8=new Expression[8];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <param name="Expression4"></param>
    /// <param name="Expression5"></param>
    /// <param name="Expression6"></param>
    /// <param name="Expression7"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3, Expression Expression4, Expression Expression5, Expression Expression6, Expression Expression7) {
        var Expressions=this.Expressions8;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        Expressions[4]=Expression4;
        Expressions[5]=Expression5;
        Expressions[6]=Expression6;
        Expressions[7]=Expression7;
        return Expressions;
    }
    internal readonly Expression[] Expressions9=new Expression[9];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <param name="Expression4"></param>
    /// <param name="Expression5"></param>
    /// <param name="Expression6"></param>
    /// <param name="Expression7"></param>
    /// <param name="Expression8"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3, Expression Expression4, Expression Expression5, Expression Expression6, Expression Expression7, Expression Expression8) {
        var Expressions=this.Expressions9;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        Expressions[4]=Expression4;
        Expressions[5]=Expression5;
        Expressions[6]=Expression6;
        Expressions[7]=Expression7;
        Expressions[8]=Expression8;
        return Expressions;
    }
    internal readonly Expression[] Expressions10=new Expression[10];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <param name="Expression4"></param>
    /// <param name="Expression5"></param>
    /// <param name="Expression6"></param>
    /// <param name="Expression7"></param>
    /// <param name="Expression8"></param>
    /// <param name="Expression9"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3, Expression Expression4, Expression Expression5, Expression Expression6, Expression Expression7, Expression Expression8, Expression Expression9) {
        var Expressions=this.Expressions10;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        Expressions[4]=Expression4;
        Expressions[5]=Expression5;
        Expressions[6]=Expression6;
        Expressions[7]=Expression7;
        Expressions[8]=Expression8;
        Expressions[9]=Expression9;
        return Expressions;
    }
    private readonly Expression[] Expressions11=new Expression[11];
    /// <summary>
    /// Expression配列を作る。
    /// </summary>
    /// <param name="Expression0"></param>
    /// <param name="Expression1"></param>
    /// <param name="Expression2"></param>
    /// <param name="Expression3"></param>
    /// <param name="Expression4"></param>
    /// <param name="Expression5"></param>
    /// <param name="Expression6"></param>
    /// <param name="Expression7"></param>
    /// <param name="Expression8"></param>
    /// <param name="Expression9"></param>
    /// <param name="Expression10"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expression[] Expressions設定(Expression Expression0, Expression Expression1, Expression Expression2, Expression Expression3, Expression Expression4, Expression Expression5, Expression Expression6, Expression Expression7, Expression Expression8, Expression Expression9, Expression Expression10) {
        var Expressions=this.Expressions11;
        Expressions[0]=Expression0;
        Expressions[1]=Expression1;
        Expressions[2]=Expression2;
        Expressions[3]=Expression3;
        Expressions[4]=Expression4;
        Expressions[5]=Expression5;
        Expressions[6]=Expression6;
        Expressions[7]=Expression7;
        Expressions[8]=Expression8;
        Expressions[9]=Expression9;
        Expressions[10]=Expression10;
        return Expressions;
    }
    private readonly object[] Objects2 = new object[2];
    /// <summary>
    /// ObjectからObject配列を作る。
    /// </summary>
    /// <param name="Object0"></param>
    /// <param name="Object1"></param>
    /// <returns></returns>
    public object[] Objects設定(object Object0,object Object1) {
        var Objects2 = this.Objects2;
        Objects2[0]=Object0;
        Objects2[1]=Object1;
        return Objects2;
    }
}