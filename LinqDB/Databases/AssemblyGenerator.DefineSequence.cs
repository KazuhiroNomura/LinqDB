using System;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using LinqDB.Databases.Attributes;
using LinqDB.Helpers;
using LinqDB.Sets;
using LinqDB.Databases.Dom;
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Databases;
public partial class AssemblyGenerator {
    private void DefineSequence(ISequence Object,ModuleBuilder ModuleBuilder,TypeBuilder Schema_TypeBuilder,ILGenerator Schema_ctor_I,LocalBuilder Schema_ToString_sb,ILGenerator Schema_ToString_I) {
        //const string Disp_Name = "DispSequence";
        var SequenceType=Object.start_value.GetType();
        var EscapedName           =Object.EscapedName;
        var Object_TypeBuilder    =ModuleBuilder.DefineType           ($"{Object.Schema.Container.EscapedName}.Sequences.{Schema_TypeBuilder.Name}.{EscapedName}",TypeAttributes.Public,typeof(Entity));
        //var SequenceTypeBuilder   =Schema_TypeBuilder.DefineNestedType(EscapedName,TypeAttributes.NestedPrivate);
        var Types1=this.Types1;
        Types1[0]=SequenceType;
        var SequenceT=typeof(Sequence<>).MakeGenericType(Types1);
        var Disp_FieldBuilder     =Schema_TypeBuilder.DefineField        (EscapedName,SequenceT,FieldAttributes.Private);
        //var Container_FieldBuilder=SequenceTypeBuilder.DefineField       ("Container",Container_TypeBuilder,FieldAttributes.Public);
        //var start_value           =SequenceTypeBuilder.DefineField       (nameof(ISequence.start_value  ),SequenceType,FieldAttributes.Public);
        //var increment             =SequenceTypeBuilder.DefineField       (nameof(ISequence.increment    ),SequenceType,FieldAttributes.Public);
        //var current_value         =SequenceTypeBuilder.DefineField       (nameof(ISequence.current_value),SequenceType,FieldAttributes.Public);
        //var Disp_ctor = SequenceTypeBuilder.DefineConstructor(MethodAttributes.Public,CallingConventions.HasThis,Types1);
        //{
        //    Disp_ctor.InitLocals=false;
        //    Disp_ctor.DefineParameter(1,ParameterAttributes.None,"Container");
        //}
        Schema_ctor_I.Ldarg_0();
        //Schema_ctor_I.Ldarg_1();
        Schema_ctor_I.Newobj(SequenceT.GetConstructors()[0]);
        Schema_ctor_I.Stfld(Disp_FieldBuilder);
        var SchemaProperty=Schema_TypeBuilder.DefineProperty(EscapedName,PropertyAttributes.None,CallingConventions.HasThis,SequenceT,Type.EmptyTypes);
        //var NullableContext_CustomAttributeBuilder = new CustomAttributeBuilder(SequenceAttribute.Reflection.ctor,Array.Empty<object>());
        SchemaProperty.SetCustomAttribute(new CustomAttributeBuilder(SequenceAttribute.Reflection.ctor,Array.Empty<object>()));
        var SchemaGetMethod=Schema_TypeBuilder.DefineMethod(EscapedName,Public_HideBySig,SequenceT,Type.EmptyTypes);
        SchemaGetMethod.InitLocals=false;
        SchemaProperty.SetGetMethod(SchemaGetMethod);
        //var NextValue = SequenceTypeBuilder.DefineMethod("NextValue",MethodAttributes.Public,SequenceType,Type.EmptyTypes);
        //NextValue.InitLocals=false;
        {
            var I = SchemaGetMethod.GetILGenerator();
            I.Ldarg_0();
            I.Ldflda(Disp_FieldBuilder);
            var NextValue =SequenceT.GetMethod(nameof(Sequence<int>.NextValue));
            Debug.Assert(NextValue is not null);

            //NextValue.InitLocals=false;
            I.Call(NextValue);
            I.Ret();
        }
        //{
        //    var I = NextValue.GetILGenerator();
        //    I.Ldarg_0();
        //    I.Ldfld(current_value);
        //    var L=I.M_DeclareLocal_Stloc(SequenceType);
        //    I.Ldarg_0();
        //    I.Ldarg_0();
        //    I.Ldfld(increment);
        //    I.Ldloc_S(L);
        //    var op_Addition=SequenceType.GetMethod("op_Addition");
        //    if(op_Addition is not null)
        //        I.Call(op_Addition);
        //    else
        //        I.Add();
        //    I.Stfld(start_value);
        //    I.Ldloc_S(L);
        //    I.Ret();
        //}
        //////13 V,TF,SF
        //var Disp_ctor_I=Disp_ctor.GetILGenerator();
        //{
        //    Disp_ctor_I.Ldarg_0();
        //    Disp_ctor_I.Ldarg_1();
        //    Disp_ctor_I.Stfld(Container_FieldBuilder);
        //    Disp_ctor_I.Ret();
        //}
        //14 V
        //15 V,TF,SF
        Schema_ToString_I.Ldloc(Schema_ToString_sb);
        Schema_ToString_I.Ldstr(EscapedName+":");
        Schema_ToString_I.Call(StringBuilder_Append_String);
        Schema_ToString_I.Ldarg_0();
        Schema_ToString_I.Call(SchemaGetMethod);
        Schema_ToString_I.Callvirt(Object_ToString);
        Schema_ToString_I.Call(StringBuilder_AppendLine_String);
        Schema_ToString_I.Pop();
        //SequenceTypeBuilder.CreateType();
        Object_TypeBuilder.CreateType();
        this.ListSequence.Add(Object);
    }
}
//2022/0402 2364
//1794