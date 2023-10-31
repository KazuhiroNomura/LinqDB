#pragma warning disable CS8604 // Null 参照引数の可能性があります。
#pragma warning disable CS8601 // Null 参照代入の可能性があります。
#define FUNCTION
#define VIEW
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;
using LinqDB.Helpers;
using LinqDB.Sets;
using LinqDB.CRC;
using System.IO;
using System.Linq.Expressions;
using System.Numerics;
using LinqDB.Databases.Dom;
using LinqDB.Optimizers;
using AssemblyName=System.Reflection.AssemblyName;
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
namespace LinqDB.Databases;
public partial class AssemblyGenerator {
    //private const bool すべて実行=true;
    private static readonly Type[] static_Types1 = new Type[1];
    private static Type[] Types(Type Type0) {
        static_Types1[0]=Type0;
        return static_Types1;
    }
    private static readonly ConstructorInfo BaseContainer_ctor0 = typeof(Container<>).GetConstructor(BindingFlags.Instance|BindingFlags.NonPublic,null,Type.EmptyTypes,null)!;
    private static readonly ConstructorInfo BaseContainer_ctor1 = typeof(Container<>).GetConstructor(BindingFlags.Instance|BindingFlags.NonPublic,null,Types(typeof(Container<>).GetGenericArguments()[0]),null)!;
    //private static readonly ConstructorInfo DifinitionContainer_ctor2 = typeof(Container<>).GetConstructor(BindingFlags.Instance|BindingFlags.NonPublic,null,new[] { typeof(XmlDictionaryReader),typeof(XmlDictionaryWriter) },null)!;
    private static readonly MethodInfo Container_Transaction = typeof(Container<>).GetMethod("Transaction",BindingFlags.Instance|BindingFlags.Public)!;
    private static readonly MethodInfo Container_Copy = typeof(Container<>).GetMethod("Copy",BindingFlags.Instance|BindingFlags.NonPublic)!;
    private static readonly MethodInfo ImmutableSet_Read = typeof(ImmutableSet<>).GetMethod(nameof(ImmutableSet<int>.Read))!;
    //private static readonly ConstructorInfo IsReadOnlyAttribute_ctor = typeof(IsReadOnlyAttribute).GetConstructor(Array.Empty<Type>())!;
    //private static readonly MethodInfo DifinitionImmutableSet_Write = typeof(ImmutableSet<>).GetMethod(nameof(Set<DEntity,DKey,LinqDB.Databases.Container>.BinaryWrite));
    private static readonly MethodInfo Set1_Assign = typeof(Set<>).GetMethod(nameof(Set<int>.Assign));
    private static readonly MethodInfo Set1_Clear = typeof(Set<>).GetMethod(nameof(Set<int>.Clear));
    //private static readonly MethodInfo Container_Init = typeof(LinqDB.Databases.Container).GetMethod("Init",BindingFlags.Instance|BindingFlags.NonPublic);
    private static readonly MethodInfo Container_Read = typeof(Container).GetMethod("Read",BindingFlags.Instance|BindingFlags.NonPublic);
    private static readonly MethodInfo Container_Write = typeof(Container).GetMethod("Write",BindingFlags.Instance|BindingFlags.NonPublic);
    private static readonly MethodInfo Container_UpdateRelationship = typeof(Container).GetMethod("UpdateRelationship",BindingFlags.Instance|BindingFlags.NonPublic);
    private static readonly MethodInfo Container_RelationValidate = typeof(Container).GetMethod(nameof(Container.RelationValidate));
    private static readonly MethodInfo Container_Clear = typeof(Container).GetMethod(nameof(Container.Clear));
    private static readonly FieldInfo Entity2_ProtectedPrimaryKey = typeof(Entity<,>).GetField("ProtectKey",BindingFlags.Instance|BindingFlags.NonPublic);
    private static readonly MethodInfo Set2_ContainsKey = typeof(Set<,>).GetMethod("ContainsKey");
    private static readonly MethodInfo Set2_TryGetValue = typeof(Set<,>).GetMethod("TryGetValue");
    private static readonly MethodInfo Set1_Add = typeof(Set<>).GetMethod(nameof(Set<int>.Add));
    private static readonly MethodInfo Set1_Remove = typeof(Set<>).GetMethod(nameof(Set<int>.Remove));
    private static readonly MethodInfo CRC32_GetHashCode = typeof(CRC32).GetMethod(nameof(CRC32.GetHashCode));
    private static readonly MethodInfo IEquatable_Equals = typeof(IEquatable<>).GetMethod(nameof(IEquatable<int>.Equals));
    //private static readonly MethodInfo IWriteRead_BinaryWrite = typeof(IWriteRead<>).GetMethod(nameof(IWriteRead<int>.BinaryWrite));
    //private static readonly MethodInfo IWriteRead_BinaryRead = typeof(IWriteRead<>).GetMethod(nameof(IWriteRead<int>.BinaryRead));
    private static readonly ConstructorInfo Set1_ctor = typeof(Set<>).GetConstructor(Type.EmptyTypes);
    private static readonly ConstructorInfo Set3_ctor = typeof(Set<,,>).GetConstructor(Types(typeof(Set<,,>).GetGenericArguments()[2]));
    private static readonly ConstructorInfo Entity2_ctor = typeof(Entity<,>).GetConstructors(BindingFlags.NonPublic|BindingFlags.Instance)[0];
    private static readonly CustomAttributeBuilder Extension_CustomAttributeBuilder = new(typeof(ExtensionAttribute).GetConstructor(Type.EmptyTypes),Array.Empty<object>());
    private static readonly Type[] Types_Strings = { typeof(string[]) };
    private static readonly ConstructorInfo ParentAttribute_ctor = typeof(ParentAttribute).GetConstructor(Types_Strings);
    private static readonly ConstructorInfo ChildAttribute_ctor = typeof(ChildAttribute).GetConstructor(Types_Strings);
    private static readonly ConstructorInfo StringBuilder_ctor = typeof(StringBuilder).GetConstructor(Type.EmptyTypes);
    private static readonly Type[] Types_InputHashCode = { typeof(CRC32).MakeByRefType() };
    private static readonly object[] Objects_Byte2 = { (byte)2 };
    private static readonly CustomAttributeBuilder Nullable_CustomAttributeBuilder = new(Type.GetType("System.Runtime.CompilerServices.NullableAttribute")!.GetConstructor(Types(typeof(byte))),Objects_Byte2);
    private static readonly Type NullableContextAttribute = Type.GetType("System.Runtime.CompilerServices.NullableContextAttribute");
    private static readonly CustomAttributeBuilder NullableContext_CustomAttributeBuilder = new(NullableContextAttribute!.GetConstructor(Types(typeof(byte))),Objects_Byte2);
    private static readonly CustomAttributeBuilder NonSerialized_CustomAttributeBuilder = new(typeof(NonSerializedAttribute).GetConstructor(Type.EmptyTypes),Array.Empty<object>());
    private static readonly MethodInfo InputT = typeof(CRC32).GetMethods().Single(p => p.Name=="Input"&&p.IsGenericMethod);
    private static readonly Type[] Types_Object = { typeof(object) };
    private static readonly Type[] Types_Stream = { typeof(Stream) };
    //private static readonly Type[] Types_XmlDictionaryWriter = { typeof(XmlDictionaryWriter) };
    //private static readonly Type[] Types_XmlDictionaryReader_XmlDictionaryWriter = { typeof(XmlDictionaryReader),typeof(XmlDictionaryWriter) };
    private static readonly Type[] Types_StringBuilder = { typeof(StringBuilder) };
    private static readonly MethodInfo Object_Equals = typeof(object).GetMethod(nameof(Equals),Types_Object);
    private static readonly MethodInfo Object_GetHashCode = typeof(object).GetMethod(nameof(GetHashCode));
    private static readonly MethodInfo Object_ToString = typeof(object).GetMethod(nameof(ToString));
    private static readonly Type[] Types_String = { typeof(string) };
    private static readonly MethodInfo StringBuilder_Append_String = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append),Types_String);
    private static readonly MethodInfo StringBuilder_AppendLine_String = typeof(StringBuilder).GetMethod(nameof(StringBuilder.AppendLine),Types_String);
    private static readonly MethodInfo StringBuilder_Append_Object = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append),Types_Object);
    private static readonly MethodInfo StringBuilder_AppendLine = typeof(StringBuilder).GetMethod(nameof(StringBuilder.AppendLine),Type.EmptyTypes);
    //public static readonly MethodInfo Entity_InvalidateClearRelationship = typeof(Entity<>).GetMethod("InvalidateClearRelationship", BindingFlags.NonPublic | BindingFlags.Instance);
    private const MethodAttributes Assembly_HideBySig=MethodAttributes.Assembly|MethodAttributes.HideBySig;
    private const MethodAttributes Family_HideBySig_Virtual=MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual;
    private const MethodAttributes Family_Virtual=MethodAttributes.Family|MethodAttributes.Virtual;
    private const MethodAttributes FamORAssem_HideBySig_Virtual=MethodAttributes.FamORAssem|MethodAttributes.HideBySig|MethodAttributes.Virtual;
    private const MethodAttributes Public_Final_NewSlot_HideBySig_Virtual=MethodAttributes.Public|MethodAttributes.Final|MethodAttributes.NewSlot|MethodAttributes.HideBySig|MethodAttributes.Virtual;
    private const MethodAttributes Public_HideBySig=MethodAttributes.Public;
    //private const MethodAttributes Public_HideBySig_SpecialName=MethodAttributes.Public;
    //private const MethodAttributes Public_HideBySig_SpecialName_RTSpecialName=MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.RTSpecialName;
    //private const MethodAttributes Public_HideBySig_SpecialName_Static=MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.Static;
    private const MethodAttributes Public_HideBySig_Static=MethodAttributes.Public|MethodAttributes.Static;
    private const MethodAttributes Public_HideBySig_Virtual=MethodAttributes.Public|MethodAttributes.Virtual;
    //private const MethodAttributes Public_Static_SpecialName_HideBySig=MethodAttributes.Public|MethodAttributes.Static|MethodAttributes.SpecialName|MethodAttributes.HideBySig;
    //private const MethodAttributes Public_Virtual=MethodAttributes.Public|MethodAttributes.Virtual;
    private const MethodAttributes Public_Virtual_HideBySig=MethodAttributes.Public|MethodAttributes.Virtual|MethodAttributes.HideBySig;

    private readonly ExpressionEqualityComparer ExpressionEqualityComparer=new();
    private static (FieldBuilder FieldBuilder, MethodBuilder GetMethodBuilder) PrivateField実装Property実装GetMethod実装(TypeBuilder TypeBuilder,Type InstanceType,string Name,Type PropertyType) {
        var FieldBuilder = TypeBuilder.DefineField($"[{Name}]",InstanceType,FieldAttributes.Assembly|FieldAttributes.InitOnly);
        var Property = TypeBuilder.DefineProperty(Name,PropertyAttributes.None,CallingConventions.HasThis,PropertyType,Type.EmptyTypes);
        var GetMethodBuilder = TypeBuilder.DefineMethod(Name,MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName,PropertyType,Type.EmptyTypes);
        GetMethodBuilder.InitLocals=false;
        Property.SetGetMethod(GetMethodBuilder);
        var I = GetMethodBuilder.GetILGenerator();
        I.Ldarg_0();
        I.Ldfld(FieldBuilder);
        I.Ret();
        return (FieldBuilder, GetMethodBuilder);
    }

    private static FieldBuilder Field実装Property実装GetMethod実装(TypeBuilder TypeBuilder,Type InstanceType,string Name,Type PropertyType) =>
        PrivateField実装Property実装GetMethod実装(TypeBuilder,InstanceType,Name,PropertyType).FieldBuilder;

    private static (MethodBuilder MethodBuilder, ILGenerator I) メソッド開始引数名(TypeBuilder TypeBuilder,string メソッド名,MethodAttributes MethodAttributes,Type ReturnType,Type[] Types,string 引数名) {
        var MethodBuilder = TypeBuilder.DefineMethod(メソッド名,MethodAttributes,ReturnType,Types);
        MethodBuilder.InitLocals=false;
        MethodBuilder.DefineParameter(1,ParameterAttributes.None,引数名);
        return (MethodBuilder, MethodBuilder.GetILGenerator());
    }
    private static (ConstructorBuilder ConstructorBuilder, ILGenerator I) コンストラクタ開始(TypeBuilder TypeBuilder,MethodAttributes MethodAttributes,Type[] Types) {
        var ctor = TypeBuilder.DefineConstructor(MethodAttributes,CallingConventions.HasThis,Types);
        ctor.InitLocals=false;
        var I = ctor.GetILGenerator();
        return (ctor, I);
    }
    private static (ConstructorBuilder ConstructorBuilder, ILGenerator I) コンストラクタ開始引数名(TypeBuilder TypeBuilder,MethodAttributes MethodAttributes,Type[] Types,string 引数名) {
        var (ctor, I)=コンストラクタ開始(TypeBuilder,MethodAttributes,Types);
        ctor.DefineParameter(1,ParameterAttributes.None,引数名);
        return (ctor, I);
    }
    private static (MethodBuilder MethodBuilder, ILGenerator I) メソッド開始(TypeBuilder TypeBuilder,string メソッド名,MethodAttributes MethodAttributes,Type ReturnType) {
        var MethodBuilder = TypeBuilder.DefineMethod(メソッド名,MethodAttributes,ReturnType,Type.EmptyTypes);
        MethodBuilder.InitLocals=false;
        return (MethodBuilder, MethodBuilder.GetILGenerator());
    }
    private static ILGenerator AddRelationship開始(TypeBuilder Type,string メソッド名,Type Entity1_Container,Type[] Types_Container) {
        var AddRelationship = Type.DefineMethod(メソッド名,FamORAssem_HideBySig_Virtual,typeof(void),Types_Container);
        AddRelationship.InitLocals=false;
        AddRelationship.DefineParameter(1,ParameterAttributes.None,"Container");
        Type.DefineMethodOverride(
            AddRelationship,
            TypeBuilder.GetMethod(
                Entity1_Container,
                typeof(Entity<>).GetMethod(メソッド名,BindingFlags.NonPublic|BindingFlags.Instance)
            )
        );
        return AddRelationship.GetILGenerator();
    }
    private static ILGenerator RemoveRelationship開始(TypeBuilder TypeBulder,string メソッド名,Type Entity1_Container) {
        var RemoveRelation = TypeBulder.DefineMethod(メソッド名,FamORAssem_HideBySig_Virtual,typeof(void),Type.EmptyTypes);
        RemoveRelation.InitLocals=false;
        TypeBulder.DefineMethodOverride(
            RemoveRelation,
            TypeBuilder.GetMethod(
                Entity1_Container,
                typeof(Entity<>).GetMethod(メソッド名,BindingFlags.NonPublic|BindingFlags.Instance)
            )
        );
        return RemoveRelation.GetILGenerator();
    }
    private static void 共通override_IEquatable_Equalsメソッド終了(ILGenerator I,Label IEquatable_Equalsでfalseの時) {
        I.Ldc_I4_1();
        I.Ret();
        I.MarkLabel(IEquatable_Equalsでfalseの時);
        I.Ldc_I4_0();
        I.Ret();
    }
    private static ILGenerator 共通struct_op_Equality_Inequality(TypeBuilder TypeBulder,MethodBuilder Equals,string メソッド名,Type[] Types) {
        var opEquality = TypeBulder.DefineMethod(メソッド名,MethodAttributes.Public|MethodAttributes.Static|MethodAttributes.SpecialName|MethodAttributes.HideBySig,typeof(bool),Types);
        opEquality.InitLocals=false;
        opEquality.DefineParameter(1,ParameterAttributes.None,"a");
        opEquality.DefineParameter(2,ParameterAttributes.None,"b");
        var I = opEquality.GetILGenerator();
        I.Ldarga_S(0);
        I.Ldarg_1();
        I.Call(Equals);
        return I;
    }
    private static void 共通override_Object_Equals終了(TypeBuilder TypeBuilder,MethodBuilder IEquatable_Equals,OpCode キャストOpCode) {
        var Type_Equals = TypeBuilder.DefineMethod(nameof(Equals),Public_Virtual_HideBySig,typeof(bool),Types_Object);
        Type_Equals.InitLocals=false;
        Type_Equals.DefineParameter(1,ParameterAttributes.None,"other");
        TypeBuilder.DefineMethodOverride(Type_Equals,Object_Equals);
        var I = Type_Equals.GetILGenerator();
        I.Ldarg_1();
        I.Emit(キャストOpCode,TypeBuilder);
        var 変数 = I.M_DeclareLocal_Stloc_Ldloc(TypeBuilder);
        var nullの時 = I.DefineLabel();
        I.Brfalse_S(nullの時);
        I.Ldarg_0();
        I.Ldloc(変数);
        I.Callvirt(IEquatable_Equals);
        var 終了 = I.DefineLabel();
        I.Br_S(終了);
        I.MarkLabel(nullの時);
        I.Ldc_I4_0();
        I.MarkLabel(終了);
        I.Ret();
    }
    private static void 共通Equals(Type[] Types1,bool IsNullableClass,FieldBuilder Field,ILGenerator I,Label Equalsでfalseの時) {
        //Left==Right→計算
        //Left is null→false
        //null==Right→false
        //null is null→true
        I.Ldarg_0();
        var FieldType = Field.FieldType;
        //Debug.Assert(Reflection.Object.Equals_==Equals.GetBaseDefinition()&&Equals.DeclaringType.IsSubclassOf(typeof(Object)));
        if(FieldType.IsValueType) {
            //LinqDB.Helpers.CommonLibrary.IsNullable()
            if(FieldType.IsNullable()) {
                var GetValueOrDefault = FieldType.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!;
                var get_HasValue = FieldType.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                //Stringにはoverride bool Equals(string)が存在してしまっている
                I.Ldfld(Field);
                var Left = I.M_DeclareLocal_Stloc(FieldType);
                I.Ldloca(Left);
                I.Call(GetValueOrDefault);
                var GetValueOrDefault_ReturnType = GetValueOrDefault.ReturnType;
                var 変数 = I.DeclareLocal(GetValueOrDefault_ReturnType);
                I.Stloc(変数);
                I.Ldloca(変数);
                I.Ldarg_1();
                I.Ldfld(Field);
                var Right = I.M_DeclareLocal_Stloc(FieldType);
                I.Ldloca(Right);
                I.Call(GetValueOrDefault);
                Types1[0]=GetValueOrDefault_ReturnType;
                I.Call(GetValueOrDefault_ReturnType.GetMethod(nameof(Equals),Types1));
                var 一致した = I.DefineLabel();
                I.Brtrue_S(一致した);
                I.Br(Equalsでfalseの時);
                I.MarkLabel(一致した);
                I.Ldloca(Left);
                I.Call(get_HasValue);
                I.Ldloca(Right);
                I.Call(get_HasValue);
                I.And();
                I.Brfalse(Equalsでfalseの時);
            } else {
                Types1[0]=FieldType;
                //Stringにはoverride bool Equals(string)が存在してしまっている
                var Equals = FieldType.GetMethod(nameof(object.Equals),Types1);
                I.Ldflda(Field);
                I.Ldarg_1();
                I.Ldfld(Field);
                if(Reflection.Object.Equals_==Equals) {
                    I.Box(FieldType);
                    I.Constrained(FieldType);
                    I.Callvirt(Equals);
                } else
                    I.Call(Equals);
                I.Brfalse(Equalsでfalseの時);
            }
        } else {
            Types1[0]=FieldType;
            //Stringにはoverride bool Equals(string)が存在してしまっている
            var Equals = FieldType.GetMethod(nameof(object.Equals),Types1);
            I.Ldfld(Field);
            if(IsNullableClass) {
                var L = I.M_DeclareLocal_Stloc_Ldloc(FieldType);
                I.Ldarg_1();
                I.Ldfld(Field);
                var R = I.M_DeclareLocal_Stloc_Ldloc(FieldType);
                //if(L==R)goto 次の処理
                var 次の処理 = I.DefineLabel();
                I.Beq(次の処理);
                I.Ldloc(L);
                I.Ldloc(R);
                //if(L.Equals(R))goto 次の処理
                I.Callvirt(Equals);
                I.Brfalse(Equalsでfalseの時);
                I.MarkLabel(次の処理);
            } else {
                I.Ldarg_1();
                I.Ldfld(Field);
                I.Callvirt(Equals);
                I.Brfalse(Equalsでfalseの時);
            }
        }
    }
    private const string op_Equality = nameof(op_Equality);
    private const string op_Inequality = nameof(op_Inequality);
    private static void 共通op_Equality_Inequality(TypeBuilder TypeBuilder,MethodBuilder Equals,Type[] Types2) {
        共通struct_op_Equality_Inequality(TypeBuilder,Equals,op_Equality,Types2).Ret();
        var I = 共通struct_op_Equality_Inequality(TypeBuilder,Equals,op_Inequality,Types2);
        I.Ldc_I4_0();
        I.Ceq();
        I.Ret();
    }
    private void Column共通処理(IColumn Column,int a,Type[] Types1,TypeBuilder TypeBuilder,ILGenerator ctor_I,ILGenerator ToStringBuilder_I,ILGenerator Equals_I,Label Equalsでfalseの時) {
        var Type = Column.Nullableを考慮したType;
        var Column_EscapedName = Column.EscapedName;
        var (FieldBuilder, GetMethodBuilder)=PrivateField実装Property実装GetMethod実装(TypeBuilder,Type,Column_EscapedName,Type);
        this.Dictionary_Column.Add(Column,FieldBuilder);
        if(Column.NullableAttribute) {
            FieldBuilder.SetCustomAttribute(Nullable_CustomAttributeBuilder);
            GetMethodBuilder.SetCustomAttribute(NullableContext_CustomAttributeBuilder);
        }
        ctor_I.Ldarg_0();
        ctor_I.Ldarg((ushort)a);
        ctor_I.Stfld(FieldBuilder);
        ToStringBuilder_I.Ldarg_1();
        ToStringBuilder_I.Ldstr(Column_EscapedName+"=");
        ToStringBuilder_I.Call(StringBuilder_Append_String);
        ToStringBuilder_I.Ldarg_0();
        ToStringBuilder_I.Ldfld(FieldBuilder);
        if(Type==typeof(string)) {
            ToStringBuilder_I.Call(StringBuilder_AppendLine_String);
        } else {
            if(Type.IsValueType)ToStringBuilder_I.Box(Type);
            ToStringBuilder_I.Call(StringBuilder_Append_Object);
            ToStringBuilder_I.Call(StringBuilder_AppendLine);
        }
        ToStringBuilder_I.Pop();
        共通Equals(Types1,Column.NullableAttribute,FieldBuilder,Equals_I,Equalsでfalseの時);
    }
    //private static readonly CustomAttributeBuilder CompilerGenerated_CustomAttributeBuilder = new(typeof(CompilerGeneratedAttribute).GetConstructor(Type.EmptyTypes)!,Array.Empty<Object>());
    private readonly Optimizer Optimizer = new();
    private readonly Type[] Types_Container = new Type[1];
    private readonly Type[] Types1 = new Type[1];
    private readonly Type[] Types2 = new Type[2];
    private readonly Type[] Types3 = new Type[3];
    private readonly Dictionary<ISchema,ISchema.Information> Dictionary_Schema = new();
    private readonly Dictionary<ITable,ITable.Information> Dictionary_Table= new();
    private readonly Dictionary<IView,Information> Dictionary_View = new();
    private readonly Dictionary<ITableFunction,Information> Dictionary_TableFunction = new();
    private readonly Dictionary<IScalarFunction,Information> Dictionary_ScalarFunction = new();
    private readonly Dictionary<IColumn,FieldBuilder> Dictionary_Column = new();
    public void Save(IContainer Container,string Folder) {
        var Dictionary_Schema=this.Dictionary_Schema;Dictionary_Schema.Clear();
        var Dictionary_Table = this.Dictionary_Table;Dictionary_Table.Clear();
        var Dictionary_View = this.Dictionary_View;Dictionary_View.Clear();
        var Dictionary_TableFunction = this.Dictionary_TableFunction;Dictionary_TableFunction.Clear();
        var Dictionary_ScalarFunction = this.Dictionary_ScalarFunction;Dictionary_ScalarFunction.Clear();
        this.Dictionary_Column.Clear();
        var Types1 = this.Types1;
        var Container_Name = Container.EscapedName;
        var AssemblyName = new AssemblyName { Name=Container_Name };
        var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        var ModuleBuilder = DynamicAssembly.DefineDynamicModule(Container.Name);
        var ParentExtensions = ModuleBuilder.DefineType(Container_Name+".ParentExtensions",TypeAttributes.Public|TypeAttributes.Sealed|TypeAttributes.Abstract);
        ParentExtensions.SetCustomAttribute(Extension_CustomAttributeBuilder);
        var ChildExtensions = ModuleBuilder.DefineType(Container_Name+".ChildExtensions",TypeAttributes.Public|TypeAttributes.Sealed|TypeAttributes.Abstract);
        ChildExtensions.SetCustomAttribute(Extension_CustomAttributeBuilder);
        var Container_TypeBuilder = ModuleBuilder.DefineType(Container_Name+".Container",TypeAttributes.Public|TypeAttributes.Serializable);

        静的インターフェース付き型(ModuleBuilder);





        Types1[0]=Container_TypeBuilder;
        var ContainerBaseType = typeof(Container<>).MakeGenericType(Types1);
        Container_TypeBuilder.SetParent(ContainerBaseType);
        var (_, Container_ctor0_I)=コンストラクタ開始(Container_TypeBuilder,Public_HideBySig,Type.EmptyTypes);
        Container_ctor0_I.Ldarg_0();
        Container_ctor0_I.Call(TypeBuilder.GetConstructor(ContainerBaseType,BaseContainer_ctor0));
        var (Container_ctor1, Container_ctor1_I)=コンストラクタ開始(Container_TypeBuilder,Public_HideBySig,Types1);
        Container_ctor1.DefineParameter(1,ParameterAttributes.None,"Parent");
        Container_ctor1_I.Ldarg_0();
        Container_ctor1_I.Ldarg_1();
        Container_ctor1_I.Call(TypeBuilder.GetConstructor(ContainerBaseType,BaseContainer_ctor1));
        var (Container_Init, Container_Init_I)=メソッド開始(Container_TypeBuilder,"Init",MethodAttributes.Private,typeof(void));
        Container_ctor0_I.Ldarg_0();
        Container_ctor0_I.Call(Container_Init);
        Container_ctor1_I.Ldarg_0();
        Container_ctor1_I.Call(Container_Init);
        var (Container_Read, Container_Read_I)=メソッド開始引数名(Container_TypeBuilder,"Read",Family_HideBySig_Virtual,typeof(void),Types_Stream,"Reader");
        Container_TypeBuilder.DefineMethodOverride(Container_Read,AssemblyGenerator.Container_Read);
        var (Container_Write, Container_Write_I)=メソッド開始引数名(Container_TypeBuilder,"Write",Family_HideBySig_Virtual,typeof(void),Types_Stream,"Writer");
        Container_TypeBuilder.DefineMethodOverride(Container_Write,AssemblyGenerator.Container_Write);
        var (Container_UpdateRelationship, Container_UpdateRelationship_I)=メソッド開始(Container_TypeBuilder,"UpdateRelationship",Family_HideBySig_Virtual,typeof(void));
        Container_TypeBuilder.DefineMethodOverride(Container_UpdateRelationship,AssemblyGenerator.Container_UpdateRelationship);
        var (Container_RelationValidate, Container_RelationValidate_I)=メソッド開始(Container_TypeBuilder,nameof(Databases.Container.RelationValidate),Public_HideBySig_Virtual,typeof(void));
        Container_TypeBuilder.DefineMethodOverride(Container_RelationValidate,AssemblyGenerator.Container_RelationValidate);
        var (Container_Transaction, Container_Transaction_I)=メソッド開始(Container_TypeBuilder,"Transaction",Public_HideBySig_Virtual,Container_TypeBuilder);
        Container_TypeBuilder.DefineMethodOverride(Container_Transaction,TypeBuilder.GetMethod(ContainerBaseType,AssemblyGenerator.Container_Transaction));
        var (Container_Copy, Container_Copy_I)=メソッド開始引数名(Container_TypeBuilder,"Copy",MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,typeof(void),Types1,"Container");
        Container_TypeBuilder.DefineMethodOverride(Container_Copy,TypeBuilder.GetMethod(typeof(Container<>).MakeGenericType(Types1),AssemblyGenerator.Container_Copy));
        var (Container_Clear, Container_Clear_I)=メソッド開始(Container_TypeBuilder,nameof(Databases.Container.Clear),Public_HideBySig_Virtual,typeof(void));
        Container_TypeBuilder.DefineMethodOverride(Container_Clear,AssemblyGenerator.Container_Clear);
        var IEquatable_Container = typeof(IEquatable<>).MakeGenericType(Types1);
        Container_TypeBuilder.AddInterfaceImplementation(IEquatable_Container);
        var (Container_Equals, Container_Equals_I)=メソッド開始引数名(Container_TypeBuilder,nameof(IEquatable<int>.Equals),Public_Final_NewSlot_HideBySig_Virtual,typeof(bool),Types1,"other");
        Container_TypeBuilder.DefineMethodOverride(Container_Equals,TypeBuilder.GetMethod(IEquatable_Container,IEquatable_Equals));
        var Container_Equalsでfalseの時 = Container_Equals_I.DefineLabel();
        var Schemas = Container.Schemas;
        var Types_Container = this.Types_Container;
        Types_Container[0]=Container_TypeBuilder;
        var Entity1_Container = typeof(Entity<>).MakeGenericType(Types_Container);
        宣言数+=Schemas.Select(p=>p.Procedures.Count()+p.Views.Count()+p.TableFunctions.Count()+p.ScalarFunctions.Count()).Sum();
        foreach(var Schema in Schemas)
            this.DefineSchema(
                Schema,ModuleBuilder,Entity1_Container,Types_Container,
                Container_TypeBuilder,
                Container_Equals_I,Container_Equalsでfalseの時,
                Container_RelationValidate_I,
                Container_Init_I,Container_Read_I,Container_Write_I,
                Container_Copy_I,
                Container_Clear_I
            );
        共通override_IEquatable_Equalsメソッド終了(Container_Equals_I,Container_Equalsでfalseの時);
        共通override_Object_Equals終了(Container_TypeBuilder,Container_Equals,OpCodes.Isinst);
        Container_Init_I.Ret();
        Container_Read_I.Ret();
        Container_Write_I.Ret();
        Container_UpdateRelationship_I.Ret();
        Container_Clear_I.Ret();
        ////親子処理
        Container_Transaction_I.Ldarg_0();
        Container_Transaction_I.Newobj(Container_ctor1);
        var Result = Container_Transaction_I.M_DeclareLocal_Stloc(Container_TypeBuilder);
        Container_Transaction_I.Ldarg_0();
        Container_Transaction_I.Ldloc(Result);
        Container_Transaction_I.Call(Container_Copy);
        Container_Transaction_I.Ldloc(Result);
        Container_Transaction_I.Ret();
        Container_Copy_I.Ret();
        Container_ctor0_I.Ret();
        Container_ctor1_I.Ret();
        foreach(var Relation in Container.Relations)this.DefineRelation(Relation);
        foreach(var Relation in Container.Relations)this.DefineRelation(Relation,Container_TypeBuilder,ParentExtensions,ChildExtensions,Container_RelationValidate_I);
        Container_RelationValidate_I.Ret();
        ParentExtensions.CreateType();
        ChildExtensions.CreateType();
        var Container_Type = Container_TypeBuilder.CreateType();
        var ContainerParameter = Expression.Parameter(Container_Type,"Container");
        /*
         * Container1
         *     dbo dbo
         * dbo
         *     Table1 Table1
         *     View1  View1
         * Table1
         *     int Field1
         *     int Field2
         * Impl
         *     static void InitView1(Container1 Container1)=>
         *         from Table1 in Container.Table select Table1
         */
        //Schemaを作る。それが呼び出しているDisp.Evaluateはまだ作られていない
        foreach(var a in Dictionary_Schema.Values)a.CreateType(Container_Type);
        foreach(var a in Dictionary_Table.Values)a.Create();
        //foreach(var a in Dictionary_View.Values)a.CreateType();
        //foreach(var a in Dictionary_TableFunction.Values)a.CreateType();
        foreach(var a in Schemas)this.Disp作成(a,ContainerParameter);
        foreach(var a in Schemas)this.Impl作成(a,ContainerParameter);
        //var Regex=new Regex("^.*$");
        //var m=Regex.Match(@"場所 LinqDB.Optimizers.Optimizer.変換_TSqlFragmentからExpression.Convertデータ型を合わせるNullableは想定しない(Expression 変更元, Type 変更先_Type) (D:\Team\kazuhiro.visualstudio.com\LinqDB\LinqDB\Optimizers\Optimizer.変換_TSqlFragmentからExpression.cs):行 784");
        //using var f=new FileStream(@"無視すべきオブジェクト.txt",FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);
        //using var w=new StreamWriter(f,Encoding.UTF8);
        //w.Close();
        //f.Close();
        ////DisplayClass_cctor_I.Ret();
        var o=Activator.CreateInstance(Container_Type,null);
        new Lokad.ILPack.AssemblyGenerator().GenerateAssembly(DynamicAssembly,$"{Folder}\\{Container_Name}.dll");
        //Process.Start(@"D:\Team\kazuhiro.visualstudio.com\LinqDB\GUI\bin\Debug\net60-windows",);
    }
    #if false
    public interface IAdditionOperators<in TSelf,in TOther,out TResult> where TSelf : IAdditionOperators<TSelf,TOther,TResult>? {
        static abstract TResult operator +(TSelf left,TOther right);
        static virtual TResult operator checked +(TSelf left,TOther right) {
            return left+right;
        }

    }
    #endif
    private static void 静的インターフェース付き型(ModuleBuilder ModuleBuilder) {
        //const string MethodName = "op_Addition";
        var GenericInterfaceDefinition = typeof(IAdditionOperators<,,>);
        var TypeBuilder = ModuleBuilder.DefineType("型",TypeAttributes.Public|TypeAttributes.Sealed);
        var GenericInterface = GenericInterfaceDefinition.MakeGenericType(TypeBuilder,TypeBuilder,TypeBuilder);
        TypeBuilder.AddInterfaceImplementation(GenericInterface);
        Type[] types = { TypeBuilder,TypeBuilder };//Public_Final_NewSlot_HideBySig_Virtual
        //var f = MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.Static;
        //var f=MethodAttributes.Public|MethodAttributes.Final|MethodAttributes.NewSlot|MethodAttributes.HideBySig|MethodAttributes.Static;
        //var メソッド本体 = TypeBuilder.DefineMethod(MethodName,MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.Static,TypeBuilder,types);
        //var メソッド宣言 = GenericInterfaceDefinition.GetMethod(MethodName);
        //TypeBuilder.DefineMethodOverride(メソッド本体,TypeBuilder.GetMethod(GenericInterface,メソッド宣言));
        //メソッド本体.InitLocals=false;
        //メソッド本体.DefineParameter(1,ParameterAttributes.None,"a");
        //メソッド本体.DefineParameter(2,ParameterAttributes.None,"b");
        //var I = メソッド本体.GetILGenerator();
        //I.Ldarg_0();
        //I.Ret();
        共通("op_Addition");
        共通("op_CheckedAddition");
        var type=TypeBuilder.CreateType();
        var i=Activator.CreateInstance(type);
        void 共通(string MethodName){
            var メソッド本体 = TypeBuilder.DefineMethod(MethodName,MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.Static,TypeBuilder,types);
            var メソッド宣言 = GenericInterfaceDefinition.GetMethod(MethodName);
            TypeBuilder.DefineMethodOverride(メソッド本体,TypeBuilder.GetMethod(GenericInterface,メソッド宣言));
            メソッド本体.InitLocals=false;
            メソッド本体.DefineParameter(1,ParameterAttributes.None,"a");
            メソッド本体.DefineParameter(2,ParameterAttributes.None,"b");
            var I = メソッド本体.GetILGenerator();
            I.Ldarg_0();
            I.Ret();
        }
    }
    private static void 静的インターフェース付き型1(ModuleBuilder ModuleBuilder) {
        const string MethodName = "Equals";
        var GenericInterfaceDefinition = typeof(IEquatable<>);
        var TypeBuilder = ModuleBuilder.DefineType("型",TypeAttributes.Public|TypeAttributes.Sealed|TypeAttributes.Abstract);
        var GenericInterface = GenericInterfaceDefinition.MakeGenericType(TypeBuilder);
        TypeBuilder.AddInterfaceImplementation(GenericInterface);
        var returnType = typeof(bool);
        Type[] types = new[] { TypeBuilder };
        var f = Public_Final_NewSlot_HideBySig_Virtual;
        var メソッド本体 = TypeBuilder.DefineMethod(MethodName,f,returnType,types);
        var メソッド宣言 = GenericInterfaceDefinition.GetMethod(MethodName);
        TypeBuilder.DefineMethodOverride(メソッド本体,TypeBuilder.GetMethod(GenericInterface,メソッド宣言));
        メソッド本体.InitLocals=false;
        メソッド本体.DefineParameter(1,ParameterAttributes.None,"a");
        var I = メソッド本体.GetILGenerator();
        I.Ldc_I4_0();
        I.Ret();
        TypeBuilder.CreateType();
    }
    private static int Impl数,Display数,Define数,宣言数;
    private static double Define率=>(double)Define数/宣言数;
    private static double Display率=>(double)Display数/宣言数;
    private static double Impl率=>(double)Impl数/宣言数;


    private void DefineSchema(ISchema Schema,ModuleBuilder ModuleBuilder,Type Entity1_Container,Type[] Types_Container,TypeBuilder Container_TypeBuilder,ILGenerator Container_Equals_I,Label Container_Equalsでfalseの時,ILGenerator Container_RelationValidate_I,ILGenerator Container_Init_I,ILGenerator Container_Read_I,ILGenerator Container_Write_I,ILGenerator Container_Copy_I,ILGenerator Container_Clear_I) {
        var Database_EscapedName = Schema.Container.EscapedName;
        var EscapedName = Schema.EscapedName;
        var Schema_TypeBuilder = ModuleBuilder.DefineType($"{Database_EscapedName}.Schemas.{EscapedName}",TypeAttributes.Public|TypeAttributes.Serializable,typeof(Schema));
        var Schema_Container_FieldBuilder = Schema_TypeBuilder.DefineField("Container3",Container_TypeBuilder,FieldAttributes.Private|FieldAttributes.InitOnly);
        var Types1 = this.Types1;
        Types1[0]=Schema_TypeBuilder;
        var Schema_IEquatable = typeof(IEquatable<>).MakeGenericType(Types1);
        Schema_TypeBuilder.AddInterfaceImplementation(Schema_IEquatable);
        var (Schema_Equals, Schema_Equals_I)=メソッド開始引数名(Schema_TypeBuilder,nameof(IEquatable<int>.Equals),Public_Final_NewSlot_HideBySig_Virtual,typeof(bool),Types1,"other");
        Schema_TypeBuilder.DefineMethodOverride(Schema_Equals,TypeBuilder.GetMethod(Schema_IEquatable,IEquatable_Equals));
        var Schema_Equalsでfalseの時 = Schema_Equals_I.DefineLabel();
        var (Schema_ctor, Schema_ctor_I)=コンストラクタ開始引数名(Schema_TypeBuilder,MethodAttributes.Assembly,Types_Container,"Container");
        Schema_ctor_I.Ldarg_0();
        Schema_ctor_I.Ldarg_1();
        Schema_ctor_I.Stfld(Schema_Container_FieldBuilder);
        var Containerに定義されるSchema_FieldBuilder = Field実装Property実装GetMethod実装(Container_TypeBuilder,Schema_TypeBuilder,Schema.Name,Schema_TypeBuilder);
        Container_Equals_I.Ldarg_0();
        Container_Equals_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        Container_Equals_I.Ldarg_1();
        Container_Equals_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        Container_Equals_I.Call(Schema_Equals);
        Container_Equals_I.Brfalse(Container_Equalsでfalseの時);
        Container_RelationValidate_I.Ldarg_0();
        Container_RelationValidate_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        var Container_RelationValidate_LocalBuilder=Container_RelationValidate_I.M_DeclareLocal_Stloc(Schema_TypeBuilder);
        var (Schema_Read, Schema_Read_I)=メソッド開始引数名(Schema_TypeBuilder,"Read",MethodAttributes.Assembly,typeof(void),Types_Stream,"Reader");
        var (Schema_Write, Schema_Write_I)=メソッド開始引数名(Schema_TypeBuilder,"Write",MethodAttributes.Assembly,typeof(void),Types_Stream,"Writer");
        Types1[0]=Schema_TypeBuilder;
        var (Schema_Assign, Schema_Assign_I)=メソッド開始引数名(Schema_TypeBuilder,"Assign",MethodAttributes.Assembly,typeof(void),Types1,"source");
        var (Schema_Clear, Schema_Clear_I)=メソッド開始(Schema_TypeBuilder,"Clear",MethodAttributes.Assembly,typeof(void));
        var Schema_ToString = Schema_TypeBuilder.DefineMethod(
            nameof(object.ToString),
            MethodAttributes.Public|MethodAttributes.Virtual,
            typeof(string),
            Type.EmptyTypes
        );
        Schema_ToString.InitLocals=false;
        Schema_TypeBuilder.DefineMethodOverride(Schema_ToString,Object_ToString);
        var Schema_ToString_I = Schema_ToString.GetILGenerator();
        Schema_ToString_I.Newobj(StringBuilder_ctor);
        var Schema_Information=new ISchema.Information(Schema_TypeBuilder,Containerに定義されるSchema_FieldBuilder,Container_RelationValidate_LocalBuilder);
        var Dictionary_Schema = this.Dictionary_Schema;
        Dictionary_Schema.Add(Schema,Schema_Information);
        var Schema_ToString_sb = Schema_ToString_I.M_DeclareLocal_Stloc(typeof(StringBuilder));
        foreach(var a in Schema.Tables         )this.DefineTable         (a,ModuleBuilder,Container_TypeBuilder,Schema_TypeBuilder,Schema_ctor_I,Schema_ToString_sb,Schema_ToString_I,Entity1_Container,Types_Container,Schema_Read_I,Schema_Write_I,Schema_Equals_I,Schema_Equalsでfalseの時,Schema_Assign_I,Schema_Clear_I);
        foreach(var a in Schema.Views          )this.DefineView          (a,ModuleBuilder,Container_TypeBuilder,Schema_TypeBuilder,Schema_ctor_I,Schema_ToString_sb,Schema_ToString_I);
        foreach(var a in Schema.TableFunctions )this.DefineTableFunction (a,ModuleBuilder,Container_TypeBuilder,Schema_TypeBuilder,Schema_ctor_I,Schema_ToString_sb,Schema_ToString_I);
        foreach(var a in Schema.ScalarFunctions)this.DefineScalarFunction(a,ModuleBuilder,Container_TypeBuilder,Schema_TypeBuilder,Schema_ctor_I,Schema_ToString_sb,Schema_ToString_I);
        foreach(var a in Schema.SynonymTables  )this.DefineSynonym       (a.Name,a.Table,Schema_TypeBuilder);
        foreach(var a in Schema.SynonymViews   )this.DefineSynonym       (a.Name,a.View,Schema_TypeBuilder);
        //foreach(var TableFunction in Schema.TableFunctions)
        //    if(すべて実行||実行すべきオブジェクト.Contains(TableFunction.Name))
        //        this.DefineTableFunction(TableFunction,ModuleBuilder,Container_TypeBuilder,Container_Schema_FieldBuilder,Schema_TypeBuilder,Schema_Container_FieldBuilder,Schema_ToString_sb,Schema_ToString_I);
        共通override_IEquatable_Equalsメソッド終了(Schema_Equals_I,Schema_Equalsでfalseの時);
        共通override_Object_Equals終了(Schema_TypeBuilder,Schema_Equals,OpCodes.Isinst);

        Schema_ToString_I.Ldloc(Schema_ToString_sb);
        Schema_ToString_I.Callvirt(Object_ToString);
        Schema_ToString_I.Ret();
        Schema_ctor_I.Ret();
        Schema_Read_I.Ret();
        Schema_Write_I.Ret();
        Schema_Assign_I.Ret();

        Container_Init_I.Ldarg_0();
        Container_Init_I.Dup();
        Container_Init_I.Newobj(Schema_ctor);
        Container_Init_I.Stfld(Containerに定義されるSchema_FieldBuilder);

        Container_Read_I.Ldarg_0();
        Container_Read_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        Container_Read_I.Ldarg_1();
        Container_Read_I.Call(Schema_Read);

        Container_Write_I.Ldarg_0();
        Container_Write_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        Container_Write_I.Ldarg_1();
        Container_Write_I.Call(Schema_Write);

        Container_Copy_I.Ldarg_1();
        Container_Copy_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        Container_Copy_I.Ldarg_0();
        Container_Copy_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        Container_Copy_I.Call(Schema_Assign);

        Schema_Clear_I.Ret();

        Container_Clear_I.Ldarg_0();
        Container_Clear_I.Ldfld(Containerに定義されるSchema_FieldBuilder);
        Container_Clear_I.Call(Schema_Clear);

        //var Schema_Type=Schema_TypeBuilder.CreateType();
    }
    private static readonly CustomAttributeBuilder IsReadOnlyAttribute = new(typeof(IsReadOnlyAttribute).GetConstructor(Array.Empty<Type>())!,Array.Empty<object>());
    private static readonly ConstructorInfo Meta_ctor = typeof(MetaAttribute).GetConstructor(
        new[] { typeof(double),typeof(double) }
    )!;
    private void DefineSynonym(string Synonym,ITable Table,TypeBuilder Schema_TypeBuilder){
        var GetMethod = this.Dictionary_Table[Table].GetMethod;
        Schema_TypeBuilder.DefineProperty(Synonym,PropertyAttributes.None,CallingConventions.HasThis,GetMethod.ReturnType,Type.EmptyTypes).SetGetMethod(GetMethod);
    }

    private void DefineSynonym(string Synonym,IView View,TypeBuilder Schema_TypeBuilder) {
        //var GetMethod = this.Dictionary_View[View].Disp_Evaluate;
        var GetMethod = this.Dictionary_View[View].SchemaのMethod;
        Schema_TypeBuilder.DefineProperty(Synonym,PropertyAttributes.None,CallingConventions.HasThis,GetMethod.ReturnType,Type.EmptyTypes).SetGetMethod(GetMethod);
    }
    private Type Nullableまたは参照型(Type Type)=>Type.Nullableまたは参照型(this.Types1);
    private void DefineRelation(IRelation Relation) {
        var Types1 = this.Types1;
        var Name = Relation.Name;
        var 親Table = Relation.親ITable!;
        var Dictionary_Table=this.Dictionary_Table;
        var 親Table_Information = Dictionary_Table[親Table];
        var 親Table_TypeBuilder = 親Table_Information.TypeBuilder;
        var 子Table = Relation.子ITable!;
        var 子Table_Information = Dictionary_Table[子Table];
        var 子Table_TypeBuilder = 子Table_Information.TypeBuilder;
        var 子Table_親One_FieldBuilder = Relation.I.子Table_親One_FieldBuilder=子Table_TypeBuilder.DefineField("親"+Name,親Table_TypeBuilder,FieldAttributes.Assembly);
        子Table_親One_FieldBuilder.SetCustomAttribute(NonSerialized_CustomAttributeBuilder);
        Types1[0]=子Table_TypeBuilder;
        var SetType = typeof(Set<>).MakeGenericType(Types1);
        var 親Table_子Many_FieldBuilder = Relation.I.親Table_子Many_FieldBuilder=親Table_TypeBuilder.DefineField("子"+Name,SetType,FieldAttributes.Assembly);
        親Table_子Many_FieldBuilder.SetCustomAttribute(NonSerialized_CustomAttributeBuilder);
        var I = 親Table_Information.ctor_I;
        I.Ldarg_0();
        I.Newobj(TypeBuilder.GetConstructor(SetType,Set1_ctor));
        I.Stfld(親Table_子Many_FieldBuilder);
    }
    private readonly object[] Objects1 = new object[1];
    private readonly object[] Objects2 = new object[2];

    private void DefineRelation(
        IRelation Relation,
        TypeBuilder Container_TypeBuilder,
        TypeBuilder ParentExtensions,
        TypeBuilder ChildExtensions,
        ILGenerator Container_RelationValidate_I
    ) {
        var Types1 = this.Types1;
        var Name = Relation.Name;
        var 親Table = Relation.親ITable!;
        var Dictionary_Table = this.Dictionary_Table;
        var 親Table_Information = Dictionary_Table[親Table];
        var 親Table_TypeBuilder = 親Table_Information.TypeBuilder;
        var 子Table = Relation.子ITable!;
        var 子Table_Information = Dictionary_Table[子Table];
        var 子Table_TypeBuilder = 子Table_Information.TypeBuilder;
        Types1[0]=子Table_TypeBuilder;
        var ImmutableSet1 = typeof(Sets.IEnumerable<>).MakeGenericType(Types1);
        Types1[0]=親Table_TypeBuilder;
        var ChildExtensions_Method = ChildExtensions.DefineMethod(Name,Public_HideBySig_Static,ImmutableSet1,Types1);
        ChildExtensions_Method.InitLocals=false;
        var Objects1 = this.Objects1;
        Objects1[0]=Relation.Columns.Select(p => p.Name).ToArray();
        var Child_CustomAttributeBuilder = new CustomAttributeBuilder(ChildAttribute_ctor,Objects1);
        var Parent_CustomAttributeBuilder = new CustomAttributeBuilder(ParentAttribute_ctor,Objects1);
        ChildExtensions_Method.SetCustomAttribute(Child_CustomAttributeBuilder);
        ChildExtensions_Method.SetCustomAttribute(Extension_CustomAttributeBuilder);
        ChildExtensions_Method.DefineParameter(1,ParameterAttributes.None,親Table.Name);
        var ChildExtensions_Method_I = ChildExtensions_Method.GetILGenerator();
        ChildExtensions_Method_I.Ldarg_0();
        ChildExtensions_Method_I.Ldfld(Relation.I.親Table_子Many_FieldBuilder!);
        ChildExtensions_Method_I.Ret();
        子Setに要素があれば例外を発生させる(親Table_Information.RemoveRelationship_I);
        //if(Relation.親Table==Relation.子Table) {
        //    //自信と同じSetに属するタプルなら無視する。他のタプルの参照があったら例外を発生させる
        //    子Setに要素があれば例外を発生させる(親Table.InvalidateClearRelationship_I!);
        //}
        void 子Setに要素があれば例外を発生させる(ILGenerator I) {
            I.Ldarg_0();
            var 親Table_子Many_FieldBuilder = Relation.I.親Table_子Many_FieldBuilder!;
            I.Ldfld(親Table_子Many_FieldBuilder);
            I.Call(Reflection.ImmutableSet.get_Count);
            var Brfalse_S = I.DefineLabel();
            I.Brfalse_S(Brfalse_S);
            I.Ldstr('\"'+親Table_子Many_FieldBuilder.Name+"\"の要素が存在したので削除できなかった。");
            I.Newobj(Reflection.Exception.RelationshipException_ctor);
            I.Throw();
            I.MarkLabel(Brfalse_S);
        }
        var Types2 = this.Types2;
        //if(SortedDictionary_自親.TryGetValue((Key_Schema.Key, Key_Table.Key),out SortedDictionary親ForeignKey)) {
        var 子Table_Key_TypeBuilder = 子Table_Information.Key_TypeBuilder;
        Types2[0]=子Table_Key_TypeBuilder;
        Types2[1]=Container_TypeBuilder;
        var Entity2_ProtectedPrimaryKey = TypeBuilder.GetField(
            typeof(Entity<,>).MakeGenericType(Types2),
            AssemblyGenerator.Entity2_ProtectedPrimaryKey
        );
        var 子Table_FieldBuilder = 子Table_Information.Tables_FieldBuilder;
        Types2[0]=子Table_Key_TypeBuilder;
        Types2[1]=子Table_TypeBuilder;
        //var Set2 = typeof(Set<,>).MakeGenericType(Types2);
        //var Set3 = 子Table_FieldBuilder.FieldType;
        var Dictionary_Schema = this.Dictionary_Schema;
        var 子Table_ISchema_Information=Dictionary_Schema[子Table.Schema];
        Container_RelationValidate_I.Ldloc(子Table_ISchema_Information.ContainerのRelationValidateで使うLocalBuilder);
        Container_RelationValidate_I.Ldfld(子Table_FieldBuilder);
        Types1[0]=子Table_TypeBuilder;
        Container_RelationValidate_I.Call(
            TypeBuilder.GetMethod(
                typeof(ImmutableSet<>).MakeGenericType(Types1),
                Reflection.ImmutableSet.GetEnumerator
            )
        );
        var Enumerator_LocalType = typeof(ImmutableSet<>.Enumerator).MakeGenericType(Types1);
        var Enumerator = Container_RelationValidate_I.M_DeclareLocal_Stloc(Enumerator_LocalType);
        Container_RelationValidate_I.BeginExceptionBlock();
        var ループ開始 = Container_RelationValidate_I.DefineLabel();
        Container_RelationValidate_I.Br(ループ開始);
        var ループ先頭 = Container_RelationValidate_I.M_DefineLabel_MarkLabel();
        Container_RelationValidate_I.Ldloca(Enumerator);
        Container_RelationValidate_I.Ldfld(TypeBuilder.GetField(Enumerator_LocalType,Reflection.ImmutableSet.Enumerator.InternalCurrent));
        var Enumerator_Current = Container_RelationValidate_I.M_DeclareLocal_Stloc(子Table_TypeBuilder);
        var 親Table_ISchema_Information = Dictionary_Schema[親Table.Schema];
        Container_RelationValidate_I.Ldloc(親Table_ISchema_Information.ContainerのRelationValidateで使うLocalBuilder);
        Container_RelationValidate_I.Ldfld(親Table_Information.Tables_FieldBuilder);
        Container_RelationValidate_I.Ldloc(Enumerator_Current);
        Types1[0]=子Table_TypeBuilder;
        var ParentExtensions_Method = ParentExtensions.DefineMethod(Name,Public_HideBySig_Static,親Table_TypeBuilder,Types1);
        ParentExtensions_Method.InitLocals=false;
        ParentExtensions_Method.SetCustomAttribute(Parent_CustomAttributeBuilder);
        ParentExtensions_Method.SetCustomAttribute(Extension_CustomAttributeBuilder);
        ParentExtensions_Method.DefineParameter(1,ParameterAttributes.None,Name);
        var ParentExtensions_Method_I = ParentExtensions_Method.GetILGenerator();
        ParentExtensions_Method_I.Ldarg_0();
        var 子Table_親One_FieldBuilder = Relation.I.子Table_親One_FieldBuilder!;
        ParentExtensions_Method_I.Ldfld(子Table_親One_FieldBuilder);
        ParentExtensions_Method_I.Ret();
        Container_RelationValidate_I.Ldfld(子Table_親One_FieldBuilder);
        Types2[0]=親Table_Information.Key_TypeBuilder;
        Types2[1]=Container_TypeBuilder;
        Container_RelationValidate_I.Ldfld(
            TypeBuilder.GetField(
                typeof(Entity<,>).MakeGenericType(Types2),
                AssemblyGenerator.Entity2_ProtectedPrimaryKey
            )
        );
        Types2[0]=親Table_Information.Key_TypeBuilder;
        Types2[1]=親Table_TypeBuilder;
        Container_RelationValidate_I.Call(
            TypeBuilder.GetMethod(
                typeof(Set<,>).MakeGenericType(Types2),
                Set2_ContainsKey
            )
        );
        Container_RelationValidate_I.Brfalse(ループ開始);
        Container_RelationValidate_I.Ldstr($"{子Table.Schema.Name}.{子Table.Name}に対応する{親Table.Name}がなかった。");
        Container_RelationValidate_I.Newobj(Reflection.Exception.RelationshipException_ctor);
        Container_RelationValidate_I.Throw();
        var 親Schema_FieldBuilder =親Table_ISchema_Information.Containerに定義されるSchema_FieldBuilder;
        Types2[0]=親Table_Information.Key_TypeBuilder;
        Types2[1]=親Table_TypeBuilder;
        var Set2_TryGetValue = TypeBuilder.GetMethod(typeof(Set<,>).MakeGenericType(Types2),AssemblyGenerator.Set2_TryGetValue);
        Types1[0]=子Table_TypeBuilder;
        var Set1 = typeof(Set<>).MakeGenericType(Types1);
        var Set1_VoidRemove = TypeBuilder.GetMethod(Set1,Set1_Remove);
        var AddRelationship_I = 子Table_Information.AddRelationship_I;
        var RemoveRelationship_I = 子Table_Information.RemoveRelationship_I;
        var AddRelationship_親タプル = this.共通AddRelationship0(
            Relation,
            Entity2_ProtectedPrimaryKey,
            AddRelationship_I,
            RemoveRelationship_I,
            子Table,
            親Schema_FieldBuilder,
            Set2_TryGetValue,
            Set1_VoidRemove
        );
        Types1[0]=子Table_Information.TypeBuilder;
        var Set = typeof(Set<>).MakeGenericType(Types1);
        var VoidAdd = TypeBuilder.GetMethod(
            Set,
            Set1_Add
        );
        if(Relation.IsNullable) {
            AddRelationship_I.Ldloc(AddRelationship_親タプル);
            var スキップ = AddRelationship_I.DefineLabel();
            AddRelationship_I.Brfalse_S(スキップ);
            AddRelationship_I.Ldloc(AddRelationship_親タプル);
            AddRelationship_I.Ldfld(Relation.I.親Table_子Many_FieldBuilder!);
            AddRelationship_I.Ldarg_0();
            AddRelationship_I.Call(VoidAdd);
            AddRelationship_I.MarkLabel(スキップ);
        } else {
            AddRelationship_I.Ldloc(AddRelationship_親タプル);
            AddRelationship_I.Ldfld(Relation.I.親Table_子Many_FieldBuilder!);
            AddRelationship_I.Ldarg_0();
            AddRelationship_I.Call(VoidAdd);
        }
        Container_RelationValidate_I.MarkLabel(ループ開始);
        Container_RelationValidate_I.Ldloca(Enumerator);
        Container_RelationValidate_I.Call(TypeBuilder.GetMethod(Enumerator_LocalType,Reflection.ImmutableSet.Enumerator.MoveNext));
        Container_RelationValidate_I.Brtrue(ループ先頭);
        Container_RelationValidate_I.BeginFinallyBlock();
        Container_RelationValidate_I.Ldloca(Enumerator);
        Container_RelationValidate_I.Constrained(Enumerator_LocalType);
        Container_RelationValidate_I.Callvirt(TypeBuilder.GetMethod(Enumerator_LocalType,typeof(ImmutableSet<>.Enumerator).GetMethod("Dispose")!));
        Container_RelationValidate_I.EndExceptionBlock();
    }
    private readonly StringBuilder sb = new StringBuilder();
    private LocalBuilder 共通AddRelationship0(
        IRelation Relation,
        FieldInfo Entity2_InternalPrimaryKey,
        ILGenerator AddRelationship_I,
        ILGenerator RemoveRelationship_I,
        ITable 自Table,
        FieldBuilder 親Schema_FieldBuilder,
        MethodInfo Set2_TryGetValue,
        MethodInfo Set1_VoidRemove
    ) {
        var Columns = Relation.Columns;
        var Dictionary_Table=this.Dictionary_Table;
        var 自Table_Information = Dictionary_Table[自Table];
        var 自Table_Key_TypeBuilder = 自Table_Information.Key_TypeBuilder;
        //var Types2 = Relation.Types2;
        var 親タプルにNULLがあるか = false;
        var 親タプルにNULLを代入して正常進行 = default(Label);
        var 正常進行 = AddRelationship_I.DefineLabel();
        var 親Table = Relation.親ITable!;
        var 親Table_Information = Dictionary_Table[親Table];
        //var 親Table_Key_TypeBuilder = 親Table_Information.Key_TypeBuilder!;
        var 親タプル = AddRelationship_I.DeclareLocal(親Table_Information.TypeBuilder);
        var 自己参照か = 親Table==Relation.子ITable;
        var Dictionary_Column = this.Dictionary_Column;
        if(Relation.IsNullable) {
            //外部キーを構成する属性がNULLだった場合親は存在しなくてもよいので飛ばす。
            foreach(var Column in Columns) {
                var Column_FieldBuilder = Dictionary_Column[Column];
                var Column_FieldBuilder_FieldType = Column_FieldBuilder.FieldType;
                if(Column.NullableAttribute) {
                    AddRelationship_I.Ldarg_0();
                    AddRelationship_I.Ldfld(Column_FieldBuilder);
                    if(!親タプルにNULLがあるか) {
                        親タプルにNULLがあるか=true;
                        親タプルにNULLを代入して正常進行=AddRelationship_I.DefineLabel();
                    }
                    AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                } else if(Column_FieldBuilder_FieldType.IsNullable()) {
                    AddRelationship_I.Ldarg_0();
                    AddRelationship_I.Ldflda(Column_FieldBuilder);
                    AddRelationship_I.Call(Column_FieldBuilder_FieldType.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
                    if(!親タプルにNULLがあるか) {
                        親タプルにNULLがあるか=true;
                        親タプルにNULLを代入して正常進行=AddRelationship_I.DefineLabel();
                    }
                    AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                }
            }
        }
        if(自己参照か) {
            AddRelationship_I.Ldarg_0();
            AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
            foreach(var Column in Columns) {
                var Column_FieldBuilder = Dictionary_Column[Column];
                var Column_FieldBuilder_FieldType = Column_FieldBuilder.FieldType;
                AddRelationship_I.Ldarg_0();
                if(Column_FieldBuilder_FieldType.IsNullable()) {
                    AddRelationship_I.Ldflda(Column_FieldBuilder);
                    AddRelationship_I.Call(Column_FieldBuilder_FieldType.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!);
                } else {
                    AddRelationship_I.Ldfld(Column_FieldBuilder);
                }
            }
            AddRelationship_I.Newobj(Dictionary_Table[Relation.親ITable].Key_ctor);
            AddRelationship_I.Call(自Table_Information.Key_IEquatable_Equals);
            var Equalsでfalseの時 = AddRelationship_I.DefineLabel();
            AddRelationship_I.Brfalse(Equalsでfalseの時);
            AddRelationship_I.Ldarg_0();
            AddRelationship_I.Stloc(親タプル);
            AddRelationship_I.Br(正常進行);
            AddRelationship_I.MarkLabel(Equalsでfalseの時);
        }
        AddRelationship_I.Ldarg_1();//Container_TypeBuilder
        AddRelationship_I.Ldfld(親Schema_FieldBuilder);//dbo
        AddRelationship_I.Ldfld(親Table_Information.Tables_FieldBuilder);//customer
        foreach(var Column in Columns) {
            AddRelationship_I.Ldarg_0();
            var IColumn_FieldBuilder = Dictionary_Column[Column];
            if(IColumn_FieldBuilder.DeclaringType==自Table_Key_TypeBuilder) {
                AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
            }
            var IColumn_FieldBuilder_FieldType = IColumn_FieldBuilder.FieldType;
            if(IColumn_FieldBuilder_FieldType.IsNullable()) {
                AddRelationship_I.Ldflda(IColumn_FieldBuilder);
                AddRelationship_I.Call(IColumn_FieldBuilder_FieldType.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes));
            } else {
                AddRelationship_I.Ldfld(IColumn_FieldBuilder);
            }
        }
        AddRelationship_I.Newobj(親Table_Information.Key_ctor);
        var sb = this.sb;
        sb.Clear();
        sb.Append($"[{自Table.Name}].[");
        foreach(var ForeignKeyColumn in Columns)
            sb.Append($"{ForeignKeyColumn.Name},");
        sb.Length--;
        sb.Append($"]に対応するタプルが[{親Table.Name}]に存在しなかった。");
        AddRelationship_I.Ldloca(親タプル);
        AddRelationship_I.Call(Set2_TryGetValue);
        AddRelationship_I.Brtrue_S(正常進行);
        AddRelationship_I.Ldstr(sb.ToString());
        AddRelationship_I.Newobj(Reflection.Exception.RelationshipException_ctor);
        AddRelationship_I.Throw();
        if(親タプルにNULLがあるか) {
            AddRelationship_I.MarkLabel(親タプルにNULLを代入して正常進行);
            AddRelationship_I.Ldnull();
            AddRelationship_I.Stloc(親タプル);
        }
        AddRelationship_I.MarkLabel(正常進行);
        AddRelationship_I.Ldarg_0();
        AddRelationship_I.Ldloc(親タプル);
        var 子Table_親One_FieldBuilder = Relation.I.子Table_親One_FieldBuilder!;
        AddRelationship_I.Stfld(子Table_親One_FieldBuilder);
        //子.Count>0だと例外を発生させる
        //this→子→thisを削除
        if(Relation.IsNullable) {
            RemoveRelationship_I.Ldarg_0();
            RemoveRelationship_I.Ldfld(子Table_親One_FieldBuilder);
            var スキップ = RemoveRelationship_I.DefineLabel();
            RemoveRelationship_I.Brfalse(スキップ);
            親タプルの子Setから自身を削除();
            RemoveRelationship_I.MarkLabel(スキップ);
        } else {
            親タプルの子Setから自身を削除();
        }
        return 親タプル;
        void 親タプルの子Setから自身を削除() {
            RemoveRelationship_I.Ldarg_0();
            RemoveRelationship_I.Ldfld(子Table_親One_FieldBuilder);
            RemoveRelationship_I.Ldfld(Relation.I.親Table_子Many_FieldBuilder!);
            RemoveRelationship_I.Ldarg_0();
            RemoveRelationship_I.Call(Set1_VoidRemove);
        }
    }

    private void Disp作成(ISchema Schema,ParameterExpression ContainerParameter) {
        var o=this.Optimizer;
        var Dictionary_View = this.Dictionary_View;
        foreach(var a in Schema.Views)o.Disp作成(ContainerParameter,Dictionary_View[a],a.SQL);
        var Dictionary_TableFunction = this.Dictionary_TableFunction;
        foreach(var a in Schema.TableFunctions)o.Disp作成(ContainerParameter,Dictionary_TableFunction[a],a.SQL);
        var Dictionary_ScalarFunction = this.Dictionary_ScalarFunction;
        foreach(var a in Schema.ScalarFunctions)o.Disp作成(ContainerParameter,Dictionary_ScalarFunction[a],a.SQL);
    }
    private void Impl作成(ISchema Schema,ParameterExpression ContainerParameter) {
        var Optimizer=this.Optimizer;
        var Dictionary_View = this.Dictionary_View;
        foreach(var a in Schema.Views)Optimizer.Impl作成(Dictionary_View[a],ContainerParameter);
        var Dictionary_TableFunction = this.Dictionary_TableFunction;
        foreach(var a in Schema.TableFunctions)Optimizer.Impl作成(Dictionary_TableFunction[a],ContainerParameter);
        var Dictionary_ScalarFunction = this.Dictionary_ScalarFunction;
        foreach(var a in Schema.ScalarFunctions)Optimizer.Impl作成(Dictionary_ScalarFunction[a],ContainerParameter);
    }
    public void ClearDebug() {
        this.Dictionary_Schema.Clear();
        this.Dictionary_Table.Clear();
        this.Dictionary_View.Clear();
        this.Dictionary_ScalarFunction.Clear();
        this.Dictionary_Column.Clear();
    }
}
//2022/0402 2364
//1794