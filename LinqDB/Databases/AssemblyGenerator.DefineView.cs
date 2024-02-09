
using System;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using LinqDB.Helpers;
using LinqDB.Sets;
using LinqDB.Databases.Dom;
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Databases;
public partial class AssemblyGenerator {
    private void DefineView(IView Object,ModuleBuilder ModuleBuilder,TypeBuilder Container_TypeBuilder,TypeBuilder Schema_TypeBuilder,ILGenerator Schema_ctor_I,LocalBuilder Schema_ToString_sb,ILGenerator Schema_ToString_I) {
        const string Disp_Name = "DispView",Impl_Name = "ImplView";
        //1 V,TF
        var Entity2 = typeof(Entity);
        //2 T,V,TF,SF
        var EscapedName           =Object.EscapedName;
        var Object_TypeBuilder    =ModuleBuilder     .DefineType      ($"{Object.Schema.Container.EscapedName}.Views.{Schema_TypeBuilder.Name}.{EscapedName}",TypeAttributes.Public,Entity2);
        //var Object_TypeBuilder    =ModuleBuilder     .DefineType      ($"{Object.Schema.Container.EscapedName}.Views.{Schema_TypeBuilder.Name}.{EscapedName}",TypeAttributes.Public|TypeAttributes.Serializable,Entity2);
        var Disp_TypeBuilder      =Schema_TypeBuilder.DefineNestedType(EscapedName,TypeAttributes.NestedPrivate);
        var Disp_FieldBuilder     =Schema_TypeBuilder.DefineField     (EscapedName,Disp_TypeBuilder,FieldAttributes.Private);
        var Impl_TypeBuilder      =Disp_TypeBuilder.DefineNestedType  ("Impl",TypeAttributes.NestedPublic|TypeAttributes.Sealed|TypeAttributes.Abstract);
        var Container_FieldBuilder=Disp_TypeBuilder.DefineField       ("Container",Container_TypeBuilder,FieldAttributes.Public);
        var Types1=this.Types1;
        Types1[0]=Container_TypeBuilder;
        var Disp_ctor = Disp_TypeBuilder.DefineConstructor(MethodAttributes.Public,CallingConventions.HasThis,Types1);
        {
            Disp_ctor.InitLocals=false;
            Disp_ctor.DefineParameter(1,ParameterAttributes.None,"Container");
        }
        Schema_ctor_I.Ldarg_0();
        Schema_ctor_I.Ldarg_1();
        Schema_ctor_I.Newobj(Disp_ctor);
        Schema_ctor_I.Stfld(Disp_FieldBuilder);
        //3 TF,SF
        //4 SF
        Types1[0]=Object_TypeBuilder;
        var ReturnType = typeof(IEnumerable<>).MakeGenericType(Types1);
        //5 V,TF
        var SchemaのMethod=Schema_TypeBuilder.DefineMethod($"[get]{EscapedName}",Public_HideBySig,ReturnType,Type.EmptyTypes);
        SchemaのMethod.InitLocals=false;
        var Disp_Evaluate=Disp_TypeBuilder.DefineMethod(Disp_Name,MethodAttributes.Public,ReturnType,Type.EmptyTypes);
        Disp_Evaluate.InitLocals=false;
        {
            var I = SchemaのMethod.GetILGenerator();
            I.Ldarg_0();
            I.Ldfld(Disp_FieldBuilder);
            //6 TF,SF
            //7 V,TF,SF
            I.Call(Disp_Evaluate);
            I.Ret();
        }
        //8 V
        Types1[0]=Disp_TypeBuilder;
        //9 V,TF,SF
        var Impl_Evaluate = Impl_TypeBuilder.DefineMethod(Impl_Name,Public_HideBySig_Static,ReturnType,Types1);
        Impl_Evaluate.InitLocals=false;
        {
            var I = Disp_Evaluate.GetILGenerator();
            I.Ldarg_0();
            //10 TF,SF
            Impl_Evaluate.DefineParameter(1,ParameterAttributes.None,"Disp");
            //11 V,TF,SF
            I.Call(Impl_Evaluate);
            I.Ret();
        }
        //13 V,TF,SF
        var Disp_ctor_I=Disp_ctor.GetILGenerator();
        {
            Disp_ctor_I.Ldarg_0();
            Disp_ctor_I.Ldarg_1();
            Disp_ctor_I.Stfld(Container_FieldBuilder);
            //Disp_ctor_I.Ret();
        }
        //14 V
        Schema_TypeBuilder.DefineProperty(EscapedName,PropertyAttributes.None,CallingConventions.HasThis,ReturnType,Type.EmptyTypes).SetGetMethod(SchemaのMethod);
        //15 V,TF,SF
        Schema_ToString_I.Ldloc(Schema_ToString_sb);
        Schema_ToString_I.Ldstr(EscapedName+":");
        Schema_ToString_I.Call(StringBuilder_Append_String);
        Schema_ToString_I.Ldarg_0();
        Schema_ToString_I.Call(SchemaのMethod);
        Schema_ToString_I.Callvirt(Object_ToString);
        Schema_ToString_I.Call(StringBuilder_AppendLine_String);
        Schema_ToString_I.Pop();
        //12 V,TF
        var Columns = Object.Columns.ToList();
        var Columns_Count = Columns.Count;
        var View_ctor_parameterTypes = new Type[Columns_Count];
        for(var a = 0;a<Columns_Count;a++)View_ctor_parameterTypes[a]=Columns[a].Nullableを考慮したType;
        var View_ctor = Object_TypeBuilder.DefineConstructor(Public_HideBySig,CallingConventions.HasThis,View_ctor_parameterTypes);
        View_ctor.InitLocals=false;
        var View_ctor_I = View_ctor.GetILGenerator();
        var (_, View_ToStringBuilder_I)=メソッド開始引数名(Object_TypeBuilder,"ToStringBuilder",Family_Virtual,typeof(void),Types_StringBuilder,"sb");
        Types1[0]=Object_TypeBuilder;
        var View_IEquatable = typeof(IEquatable<>).MakeGenericType(Types1);
        Object_TypeBuilder.AddInterfaceImplementation(View_IEquatable);
        var (View_IEquatable_Equals, View_IEquatable_Equals_I)=メソッド開始引数名(Object_TypeBuilder,"Equals",Public_Final_NewSlot_HideBySig_Virtual,typeof(bool),Types1,"other");
        Object_TypeBuilder.DefineMethodOverride(View_IEquatable_Equals,TypeBuilder.GetMethod(View_IEquatable,IEquatable_Equals));
        var View_IEquatable_Equalsでfalseの時 = View_IEquatable_Equals_I.DefineLabel();
        for(var a = 0;a<Columns_Count;a++) {
            var Column = Columns[a];
            var Parameter = View_ctor.DefineParameter(a+1,ParameterAttributes.None,Columns[a].EscapedName);
            if(Column.NullableAttribute)Parameter.SetCustomAttribute(Nullable_CustomAttributeBuilder);
        }
        for(var a = 0;a<Columns_Count;)
            this.Column共通処理(Columns[a],++a,Types1,Object_TypeBuilder,View_ctor_I,View_ToStringBuilder_I,View_IEquatable_Equals_I,View_IEquatable_Equalsでfalseの時);
        View_ctor_I.Ret();
        View_ToStringBuilder_I.Ret();
        共通override_IEquatable_Equalsメソッド終了(View_IEquatable_Equals_I,View_IEquatable_Equalsでfalseの時);
        共通override_Object_Equals終了(Object_TypeBuilder,View_IEquatable_Equals,OpCodes.Isinst);
        Object_TypeBuilder.CreateType();
        this.Dictionary_View.Add(Object,new(
            //Object_TypeBuilder,
            Disp_TypeBuilder,
            Disp_ctor_I,
            Impl_TypeBuilder,
            this.ExpressionEqualityComparer,
            Impl_Evaluate,
            Object.SQL
        ));
    }
}
//2022/0402 2364
//1794