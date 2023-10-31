using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Optimizers;
namespace LinqDB.Databases.Dom;
/// <summary>
/// ScalarFunction
/// </summary>
internal sealed class Information {
    //Impl作成固定,DispImplDelegate作成
    internal TypeBuilder Disp_TypeBuilder { get;private set;}
    internal TypeBuilder Impl_TypeBuilder { get;private set;}
    internal readonly Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant;
    internal readonly Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic;
    //DispのFieldを参照させる
    internal readonly Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=new();
    //ラムダをDispのFieldを参照させる
    internal readonly Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)> DictionaryLambda;

    //internal readonly Dictionary<LambdaExpression,MethodBuilder> DictionaryLambda_MethodBuilder;
    //ラムダをDispのFieldの初期化。ラムダ、定数など。
    internal readonly ILGenerator Disp_ctor_I;
    //ContainerParameterを置換させるField。DynamicMethodではItem1,AssemblyではContainer
    //ImmutableSet<T>View,Set<T>Tableのgetメソッド
    internal readonly MethodBuilder SchemaのMethod;
    public Information(
        TypeBuilder Disp_TypeBuilder,
        ILGenerator Disp_ctor_I,
        TypeBuilder Impl_TypeBuilder,
        ExpressionEqualityComparer ExpressionEqualityComparer,
        MethodBuilder SchemaのMethod
    ){
        this.Disp_TypeBuilder=Disp_TypeBuilder;
        this.Impl_TypeBuilder=Impl_TypeBuilder;
        this.DictionaryConstant=new(ExpressionEqualityComparer);
        this.DictionaryDynamic=new();
        this.DictionaryLambda=new(ExpressionEqualityComparer);
        //this.DictionaryLambda_MethodBuilder=new(ExpressionEqualityComparer);
        this.Disp_ctor_I=Disp_ctor_I;
        this.SchemaのMethod=SchemaのMethod;
    }

    internal ParameterExpression? DispParameter;
    internal FieldInfo? ContainerField;
    internal ParameterExpression? ContainerParameter;
    internal LambdaExpression? Lambda;
    internal Type? Disp_Type{ get;private set;}
    /// <summary>
    /// 作業Fieldが定義してあるView.Disp作成
    /// </summary>
    /// <returns></returns>
    public Type CreateDispType() {
        var Disp_Type=this.Disp_Type=this.Disp_TypeBuilder.CreateType();
        this.Disp_TypeBuilder=default!;
        //this.DispParameter=Expression.Parameter(Disp_Type,"Disp");
        return Disp_Type;
    }
    /// <summary>
    /// View.Dispを呼び出すView.Disp.Impl作成
    /// </summary>
    /// <returns></returns>
    public Type CreateImplType() {
        var Impl_Type=this.Impl_TypeBuilder.CreateType();
        this.Impl_TypeBuilder=default!;
        return Impl_Type;
    }
}
