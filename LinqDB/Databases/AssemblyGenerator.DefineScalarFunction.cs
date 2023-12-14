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
    private void DefineScalarFunction(IScalarFunction Object,ModuleBuilder ModuleBuilder,TypeBuilder Container_TypeBuilder,TypeBuilder Schema_TypeBuilder,ILGenerator Schema_ctor_I,LocalBuilder Schema_ToString_sb,ILGenerator Schema_ToString_I) {
        const string Disp_Name = "DispScalarFunction",Impl_Name = "ImplScalarFunction";
        //1 V,TF
        //2 T,V,TF,SF
        var EscapedName           =Object.EscapedName;
        var Object_TypeBuilder    =ModuleBuilder.DefineType           ($"{Object.Schema.Container.EscapedName}.Functions.{Schema_TypeBuilder.Name}.{EscapedName}",TypeAttributes.Public,typeof(Entity));
        //var Object_TypeBuilder    =ModuleBuilder.DefineType           ($"{Object.Schema.Container.EscapedName}.Functions.{Schema_TypeBuilder.Name}.{EscapedName}",TypeAttributes.Public|TypeAttributes.Serializable,typeof(Entity));
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
        var InstanceParameters = Object.Parameters.ToList();
        var InstanceParameters_Count = InstanceParameters.Count;
        var InstanceParameterTypes = new Type[InstanceParameters_Count];
        var StaticParameterTypes = new Type[InstanceParameters_Count+1];
        StaticParameterTypes[0]=Disp_TypeBuilder;
        for(var a=0;a<InstanceParameters_Count;a++){
            var Type=this.Nullableまたは参照型(InstanceParameters[a].Type);
            InstanceParameterTypes[a]=Type;
            StaticParameterTypes[a+1]=Type;
        }
        //4 SF
        var ReturnType=this.Nullableまたは参照型(Object.Type);
        //5 V,TF
        var SchemaのMethod=Schema_TypeBuilder.DefineMethod(EscapedName,Public_HideBySig,ReturnType,InstanceParameterTypes);
        SchemaのMethod.InitLocals=false;
        var Disp_Evaluate = Disp_TypeBuilder.DefineMethod(Disp_Name,MethodAttributes.Public,ReturnType,InstanceParameterTypes);
        Disp_Evaluate.InitLocals=false;
        {
            var I = SchemaのMethod.GetILGenerator();
            I.Ldarg_0();
            I.Ldfld(Disp_FieldBuilder);
            //6 TF,SF
            for(var a = 1;a<=InstanceParameters_Count;a++) I.Ldarg((ushort)a);
            //7 V,TF,SF
            I.Call(Disp_Evaluate);
            I.Ret();
        }
        //8 V
        //9 V,TF,SF
        var Impl_Evaluate = Impl_TypeBuilder.DefineMethod(Impl_Name,Public_HideBySig_Static,ReturnType,StaticParameterTypes);
        Impl_Evaluate.InitLocals=false;
        {
            var I = Disp_Evaluate.GetILGenerator();
            I.Ldarg_0();
            //10 TF,SF
            Impl_Evaluate.DefineParameter(1,ParameterAttributes.None,"Disp");
            for(var a = 1;a<=InstanceParameters_Count;a++) {
                SchemaのMethod.DefineParameter(a,ParameterAttributes.None,InstanceParameters[a-1].Name);
                Disp_Evaluate.DefineParameter(a,ParameterAttributes.None,InstanceParameters[a-1].Name);
                Impl_Evaluate.DefineParameter(a+1,ParameterAttributes.None,InstanceParameters[a-1].Name);
                I.Ldarg((ushort)a);
            }
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
        Object_TypeBuilder.CreateType();
        this.Dictionary_ScalarFunction.Add(Object,new(
            Disp_TypeBuilder,
            Disp_ctor_I,
            Impl_TypeBuilder,
            this.ExpressionEqualityComparer,
            Impl_Evaluate
        ));
    }
}
//2022/0402 2364
//1794