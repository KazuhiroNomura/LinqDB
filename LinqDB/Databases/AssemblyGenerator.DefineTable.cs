using System;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using LinqDB.Helpers;
using LinqDB.Sets;
using LinqDB.CRC;
using System.Diagnostics;
using LinqDB.Databases.Dom;
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Databases;
public partial class AssemblyGenerator {
    private void DefineTable(ITable Object,ModuleBuilder ModuleBuilder,TypeBuilder Container_TypeBuilder,TypeBuilder Schema_TypeBuilder,ILGenerator Schema_ctor_I,LocalBuilder Schema_ToString_sb,ILGenerator Schema_ToString_I,Type Entity1_Container,Type[] Types_Container,ILGenerator Schema_Read_I,ILGenerator Schema_Write_I,ILGenerator Schema_Equals_I,Label Schema_Equalsでfalseの時,ILGenerator Schema_Assign_I,ILGenerator Schema_Clear_I) {
        //0 V,TF
        //1 T,V,TF,SF
        var EscapedName = Object.EscapedName;
        //2 TF,SF
        //3 SF
        //4 V,TF
        var ISchema = Object.Schema;
        var ISchema_EscapedName = ISchema.EscapedName;
        var IContainer_EscapedName = ISchema.Container.EscapedName;
        var Key_TypeBuilder=ModuleBuilder.DefineType(
            $"{IContainer_EscapedName}.PrimaryKeys.{ISchema_EscapedName}.{EscapedName}",
            TypeAttributes.Public|TypeAttributes.SequentialLayout,
            //TypeAttributes.Public|TypeAttributes.SequentialLayout|TypeAttributes.Serializable,
            typeof(ValueType)
        );
        //これがあるとCore系列では保存できない
        //Key_TypeBuilder.SetCustomAttribute(Common.IsReadOnlyAttribute_ctor,Array.Empty<Byte>());
        //Key_TypeBuilder.SetCustomAttribute(Common.IsReadOnlyAttribute_ctor,Array.Empty<Byte>());
        Key_TypeBuilder.SetCustomAttribute(IsReadOnlyAttribute);
        var Types1 = this.Types1;
        Types1[0]=Key_TypeBuilder;
        var Key_IEquatable = typeof(IEquatable<>).MakeGenericType(Types1);
        Key_TypeBuilder.AddInterfaceImplementation(Key_IEquatable);
        var (Key_IEquatable_Equals, Key_IEquatable_Equals_I)=メソッド開始引数名(Key_TypeBuilder,nameof(IEquatable<int>.Equals),Public_Final_NewSlot_HideBySig_Virtual,typeof(bool),Types1,"other");
        Key_TypeBuilder.DefineMethodOverride(Key_IEquatable_Equals,TypeBuilder.GetMethod(Key_IEquatable,IEquatable_Equals));
        var Types2 = this.Types2;
        Types2[0]=Key_TypeBuilder;
        Types2[1]=Container_TypeBuilder;
        var Entity2 = typeof(Entity<,>).MakeGenericType(Types2);
        var Entity2_ProtectedPrimaryKey = TypeBuilder.GetField(Entity2,AssemblyGenerator.Entity2_ProtectedPrimaryKey);
        var Table_TypeBuilder = ModuleBuilder.DefineType($"{IContainer_EscapedName}.Tables.{ISchema_EscapedName}.{EscapedName}",TypeAttributes.Public,Entity2);
        //var Table_TypeBuilder = ModuleBuilder.DefineType($"{IContainer_EscapedName}.Tables.{ISchema_EscapedName}.{EscapedName}",TypeAttributes.Public|TypeAttributes.Serializable,Entity2);
        Types1[0]=Table_TypeBuilder;
        //{
        //    var IMemoryPackable=typeof(IMemoryPackable<>);
        //    var IMemoryPackableT=IMemoryPackable.MakeGenericType(Types1);
        //    Table_TypeBuilder.AddInterfaceImplementation(IMemoryPackableT);
        //    {
        //        //Table_TypeBuilder.generic
        //        var Serialize本体 = Table_TypeBuilder.DefineMethod(
        //            nameof(IMemoryPackable<int>.Serialize),
        //            MethodAttributes.Private|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.Static,
        //            typeof(void),new Type[] { Table_TypeBuilder,Table_TypeBuilder});
        //        var GenericTypeParameterBuilders=Serialize本体.DefineGenericParameters("TBufferWriter");
        //        GenericTypeParameterBuilders[0].SetInterfaceConstraints(typeof(IBufferWriter<byte>));
        //        Type[] GenericTypes=GenericTypeParameterBuilders;
        //        Serialize本体.SetParameters(typeof(MemoryPackWriter<>).MakeGenericType(GenericTypes).MakeByRefType());
        //        Serialize本体.DefineParameter(1,ParameterAttributes.Out,"writer");
        //        Serialize本体.SetParameters(Table_TypeBuilder.MakeByRefType());
        //        Serialize本体.DefineParameter(2,ParameterAttributes.Out,"value");
        //        var Serialize宣言 = IMemoryPackable.GetMethod(nameof(IMemoryPackable<int>.Serialize));
        //        Table_TypeBuilder.DefineMethodOverride(
        //            Serialize本体,
        //            TypeBuilder.GetMethod(IMemoryPackable,Serialize宣言!)
        //        );
        //        Serialize本体.InitLocals=false;
        //        var I = Serialize本体.GetILGenerator();
        //        //I.Ldnull();
        //        I.Ret();
        //    }
        //}
        //Table_TypeBuilder.SetCustomAttribute(Serializable_CustomAttributeBuilder);
        //Table_TypeBuilder.SetCustomAttribute(Serializable_CustomAttributeBuilder);
        //Table_TypeBuilder.SetCustomAttribute(Serializable_CustomAttributeBuilder);
        //Table_TypeBuilder.SetCustomAttribute(Serializable_CustomAttributeBuilder);
        //Table_TypeBuilder.SetCustomAttribute(Serializable_CustomAttributeBuilder);
        var Objects2 = this.Objects2;
        Objects2[0]=Object.Left;
        Objects2[1]=Object.Top;
        Table_TypeBuilder.SetCustomAttribute(new CustomAttributeBuilder(Meta_ctor,Objects2));
        var Columns = Object.Columns.ToList();
        var PrimaryKeyColumns = Columns.Where(p => p.IsPrimaryKey).ToList();
        var PrimaryKeyColumns_Count = PrimaryKeyColumns.Count;
        var Key_ctor_parameterTypes = new Type[PrimaryKeyColumns_Count];
        for(var a = 0;a<PrimaryKeyColumns_Count;a++)
            Key_ctor_parameterTypes[a]=PrimaryKeyColumns[a].Type;
        var Key_ctor = Key_TypeBuilder.DefineConstructor(MethodAttributes.Public,CallingConventions.HasThis,Key_ctor_parameterTypes);
        Key_ctor.InitLocals=false;
        var Key_ctor_I = Key_ctor.GetILGenerator();
        var Columns_Count = Columns.Count;
        var Table_ctor_parameterTypes = new Type[Columns_Count];
        for(var a = 0;a<Columns_Count;a++) {
            var Column = Columns[a];
            var Column_Type = Column.Type;
            if(Column.IsNullable&&Column_Type.IsValueType) {
                Types1[0]=Column_Type;
                Column_Type=typeof(Nullable<>).MakeGenericType(Types1);
            }
            Table_ctor_parameterTypes[a]=Column_Type;
        }
        var Table_ctor = Table_TypeBuilder.DefineConstructor(Public_HideBySig,CallingConventions.HasThis,Table_ctor_parameterTypes);
        Table_ctor.InitLocals=false;
        var Table_ctor_I = Table_ctor.GetILGenerator();
        var (_, Key_InputHashCode_I)=メソッド開始引数名(Key_TypeBuilder,"InputHashCode",Assembly_HideBySig,typeof(void),Types_InputHashCode,"CRC");
        var (Key_GetHashCode, Key_GetHashCode_I)=メソッド開始(Key_TypeBuilder,nameof(GetHashCode),Public_HideBySig_Virtual,typeof(int));
        Key_TypeBuilder.DefineMethodOverride(Key_GetHashCode,Object_GetHashCode);
        var (Key_ToStringBuilder, Key_ToStringBuilder_I)=メソッド開始引数名(Key_TypeBuilder,"ToStringBuilder",Assembly_HideBySig,typeof(void),Types_StringBuilder,"sb");
        var (_, Table_ToStringBuilder_I)=メソッド開始引数名(Table_TypeBuilder,"ToStringBuilder",Family_Virtual,typeof(void),Types_StringBuilder,"sb");
        Table_ToStringBuilder_I.Ldarg_0();
        Table_ToStringBuilder_I.Ldflda(Entity2_ProtectedPrimaryKey);
        Table_ToStringBuilder_I.Ldarg_1();
        Table_ToStringBuilder_I.Call(Key_ToStringBuilder);
        var AddRelationship_I=AddRelationship開始(Table_TypeBuilder,"AddRelationship",Entity1_Container,Types_Container);
        var RemoveRelationship_I=RemoveRelationship開始(Table_TypeBuilder,"RemoveRelationship",Entity1_Container);
        Types1[0]=Table_TypeBuilder;
        var Table_IEquatable = typeof(IEquatable<>).MakeGenericType(Types1);
        Table_TypeBuilder.AddInterfaceImplementation(Table_IEquatable);
        var ImmutableSet = typeof(ImmutableSet<>).MakeGenericType(Types1);
        var (Table_IEquatable_Equals, Table_IEquatable_Equals_I)=メソッド開始引数名(Table_TypeBuilder,nameof(IEquatable<int>.Equals),Public_Final_NewSlot_HideBySig_Virtual,typeof(bool),Types1,"other");
        Table_TypeBuilder.DefineMethodOverride(Table_IEquatable_Equals,TypeBuilder.GetMethod(Table_IEquatable,IEquatable_Equals));
        //{
        //    var Table_IWriteRead = typeof(IWriteRead<>).MakeGenericType(Types1);
        //    Object_TypeBuilder.AddInterfaceImplementation(Table_IWriteRead);
        //    Types1[0]=typeof(BinaryWriter);
        //    var (IWriteRead_BinaryWrite, IWriteRead_BinaryWrite_I)=メソッド開始引数名(Object_TypeBuilder,nameof(IWriteRead<int>.BinaryWrite),Public_Final_NewSlot_HideBySig_Virtual,typeof(void),Types1,"Writer");
        //    Object_TypeBuilder.DefineMethodOverride(IWriteRead_BinaryWrite,TypeBuilder.GetMethod(Table_IWriteRead,AssemblyGenerator.IWriteRead_BinaryWrite));
        //    IWriteRead_BinaryWrite_I.Ret();
        //    Types2[0]=typeof(BinaryReader);
        //    Types1[0]=Object_TypeBuilder;
        //    Types2[1]=typeof(Func<>).MakeGenericType(Types1);
        //    var IWriteRead_BinaryRead = Object_TypeBuilder.DefineMethod(nameof(IWriteRead<int>.BinaryRead),Public_Final_NewSlot_HideBySig_Virtual,typeof(void),Types2);
        //    IWriteRead_BinaryRead.InitLocals=false;
        //    IWriteRead_BinaryRead.DefineParameter(1,ParameterAttributes.None,"Reader");
        //    IWriteRead_BinaryRead.DefineParameter(2,ParameterAttributes.None,"Create");
        //    Object_TypeBuilder.DefineMethodOverride(IWriteRead_BinaryRead,TypeBuilder.GetMethod(Table_IWriteRead,AssemblyGenerator.IWriteRead_BinaryRead));
        //    var IWriteRead_BinaryRead_I = IWriteRead_BinaryRead.GetILGenerator();
        //    IWriteRead_BinaryRead_I.Ret();
        //}
        var KeyTable_Equalsでfalseの時 = Key_IEquatable_Equals_I.DefineLabel();
        var KeyTable_GetHashCode_CRC = Key_GetHashCode_I.DeclareLocal(typeof(CRC32));
        var Table_IEquatable_Equalsでfalseの時 = Table_IEquatable_Equals_I.DefineLabel();
        if(PrimaryKeyColumns_Count==0) {
            Key_GetHashCode_I.Ldc_I4_0();
        } else if(PrimaryKeyColumns_Count>1) {
            Key_GetHashCode_I.Ldloca(KeyTable_GetHashCode_CRC);
            Key_GetHashCode_I.Initobj(typeof(CRC32));
        }
        var Dictionary_Column = this.Dictionary_Column;
        for(var a = 0;a<PrimaryKeyColumns_Count;) {
            var PrimaryKeyColumn = PrimaryKeyColumns[a];
            a++;
            var PrimaryKeyColumn_Type = PrimaryKeyColumn.Type;
            var PrimaryKeyColumn_EscapedName = PrimaryKeyColumn.EscapedName;
            var Key_FieldBuilder = Field実装Property実装GetMethod実装(Key_TypeBuilder,PrimaryKeyColumn_Type,PrimaryKeyColumn_EscapedName,PrimaryKeyColumn_Type);
            Dictionary_Column.Add(PrimaryKeyColumn,Key_FieldBuilder);
            Key_ctor.DefineParameter(a,ParameterAttributes.None,PrimaryKeyColumn_EscapedName);
            Key_ctor_I.Ldarg_0();
            Key_ctor_I.Ldarg((ushort)a);
            Key_ctor_I.Stfld(Key_FieldBuilder);
            {
                Key_IEquatable_Equals_I.Ldarg_0();
                if(PrimaryKeyColumn_Type.IsValueType)Key_IEquatable_Equals_I.Ldflda(Key_FieldBuilder);
                else                                 Key_IEquatable_Equals_I.Ldfld (Key_FieldBuilder);
                Key_IEquatable_Equals_I.Ldarg_1();
                Key_IEquatable_Equals_I.Ldfld(Key_FieldBuilder);
                Types1[0]=PrimaryKeyColumn_Type;
                var Equals = PrimaryKeyColumn_Type.GetMethod(nameof(object.Equals),Types1);
                Debug.Assert(
                    Equals is not null&&(
                        Equals.DeclaringType==typeof(object)||
                        Equals.DeclaringType is not null&&Equals.DeclaringType.IsSubclassOf(typeof(object))
                    )
                );
                if(PrimaryKeyColumn_Type.IsValueType&&Reflection.Object.Equals_==Equals.GetBaseDefinition()) {
                    Key_IEquatable_Equals_I.Box(PrimaryKeyColumn_Type);
                    Key_IEquatable_Equals_I.Constrained(PrimaryKeyColumn_Type);
                }
                Key_IEquatable_Equals_I.Callvirt(Equals);
                Key_IEquatable_Equals_I.Brfalse(KeyTable_Equalsでfalseの時);
            }
            {
                var Property = Table_TypeBuilder.DefineProperty(PrimaryKeyColumn_EscapedName,PropertyAttributes.None,CallingConventions.HasThis,PrimaryKeyColumn_Type,Type.EmptyTypes);
                var GetMethodBuilder = Table_TypeBuilder.DefineMethod(
                    PrimaryKeyColumn_EscapedName,
                    MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName,
                    PrimaryKeyColumn_Type,
                    Type.EmptyTypes
                );
                GetMethodBuilder.InitLocals=false;
                Property.SetGetMethod(GetMethodBuilder);
                var I = GetMethodBuilder.GetILGenerator();
                I.Ldarg_0();
                I.Ldflda(Entity2_ProtectedPrimaryKey);
                I.Ldfld(Key_FieldBuilder);
                I.Ret();
            }
            Key_InputHashCode_I.Ldarg_1();
            Key_InputHashCode_I.Ldarg_0();
            Key_InputHashCode_I.Ldfld(Key_FieldBuilder);
            Types1[0]=PrimaryKeyColumn_Type;
            Key_InputHashCode_I.Call(InputT.MakeGenericMethod(Types1));
            if(PrimaryKeyColumns_Count==1) {
                Key_GetHashCode_I.Ldarg_0();
                if(PrimaryKeyColumn_Type==typeof(int)) {
                    Key_GetHashCode_I.Ldfld(Key_FieldBuilder);
                } else {
                    Key_GetHashCode_I.Ldflda(Key_FieldBuilder);
                    Key_GetHashCode_I.Constrained(PrimaryKeyColumn_Type);
                    Key_GetHashCode_I.Callvirt(PrimaryKeyColumn_Type.GetMethod(nameof(GetHashCode),Type.EmptyTypes)!);
                }
            } else {
                Key_GetHashCode_I.Ldloca(KeyTable_GetHashCode_CRC);
                Key_GetHashCode_I.Ldarg_0();
                Key_GetHashCode_I.Ldfld(Key_FieldBuilder);
                Key_GetHashCode_I.Call(InputT.MakeGenericMethod(Types1));
            }
            Key_ToStringBuilder_I.Ldarg_1();
            Key_ToStringBuilder_I.Ldstr(PrimaryKeyColumn_EscapedName+"=");
            Key_ToStringBuilder_I.Call(StringBuilder_Append_String);
            Key_ToStringBuilder_I.Ldarg_0();
            Key_ToStringBuilder_I.Ldfld(Key_FieldBuilder);
            if(PrimaryKeyColumn_Type==typeof(string)) {
                Key_ToStringBuilder_I.Call(StringBuilder_AppendLine_String);
            } else {
                if(PrimaryKeyColumn_Type.IsValueType)Key_ToStringBuilder_I.Box(PrimaryKeyColumn_Type);
                Key_ToStringBuilder_I.Call(StringBuilder_Append_Object);
                Key_ToStringBuilder_I.Call(StringBuilder_AppendLine);
            }
            Key_ToStringBuilder_I.Pop();
        }
        Key_ctor_I.Ret();
        Key_InputHashCode_I.Ret();
        Key_ToStringBuilder_I.Ret();
        if(PrimaryKeyColumns_Count==0) {
            Key_IEquatable_Equals_I.MarkLabel(KeyTable_Equalsでfalseの時);
            Key_IEquatable_Equals_I.Ldc_I4_0();
            Key_IEquatable_Equals_I.Ret();
        } else {
            if(PrimaryKeyColumns_Count>1) {
                Key_GetHashCode_I.Ldloca(KeyTable_GetHashCode_CRC);
                Key_GetHashCode_I.Call(CRC32_GetHashCode);
            }
            共通override_IEquatable_Equalsメソッド終了(Key_IEquatable_Equals_I,KeyTable_Equalsでfalseの時);
        }
        Key_GetHashCode_I.Ret();
        {
            var (ToString, I)=メソッド開始(Key_TypeBuilder,nameof(object.ToString),Public_HideBySig_Virtual,typeof(string));
            Key_TypeBuilder.DefineMethodOverride(ToString,Object_ToString);
            I.Newobj(StringBuilder_ctor);
            var sb = I.M_DeclareLocal_Stloc(typeof(StringBuilder));
            I.Ldarg_0();
            I.Ldloc(sb);
            I.Call(Key_ToStringBuilder);
            I.Ldloc(sb);
            I.Callvirt(Object_ToString);
            I.Ret();
        }
        Table_ctor_I.Ldarg_0();
        for(var a = 0;a<Columns_Count;) {
            var Column = Columns[a];
            a++;
            if(PrimaryKeyColumns.Contains(Column)) Table_ctor_I.Ldarg((ushort)a);
            var Parameter = Table_ctor.DefineParameter(a,ParameterAttributes.None,Column.EscapedName);
            if(Column.NullableAttribute)Parameter.SetCustomAttribute(Nullable_CustomAttributeBuilder);
        }
        Table_ctor_I.Newobj(Key_ctor);
        Table_ctor_I.Call(TypeBuilder.GetConstructor(Entity2,Entity2_ctor));
        Table_IEquatable_Equals_I.Ldarg_0();
        Table_IEquatable_Equals_I.Ldflda(Entity2_ProtectedPrimaryKey);
        Table_IEquatable_Equals_I.Ldarg_1();
        Table_IEquatable_Equals_I.Ldfld(Entity2_ProtectedPrimaryKey);
        Table_IEquatable_Equals_I.Call(Key_IEquatable_Equals);
        Table_IEquatable_Equals_I.Brfalse(Table_IEquatable_Equalsでfalseの時);
        for(var a = 0;a<Columns_Count;) {
            var Column = Columns[a++];
            //キーフィールドからプロパティはすでに実装されている。
            if(PrimaryKeyColumns.Contains(Column)) continue;
            this.Column共通処理(Column,a,Types1,Table_TypeBuilder,Table_ctor_I,Table_ToStringBuilder_I,Table_IEquatable_Equals_I,Table_IEquatable_Equalsでfalseの時);
        }
        Table_ToStringBuilder_I.Ret();
        共通override_IEquatable_Equalsメソッド終了(Table_IEquatable_Equals_I,Table_IEquatable_Equalsでfalseの時);
        Types2[0]=Types2[1]=Key_TypeBuilder;
        共通op_Equality_Inequality(Key_TypeBuilder,Key_IEquatable_Equals,Types2);
        {
            var Key_Equals = Key_TypeBuilder.DefineMethod(nameof(Equals),Public_Virtual_HideBySig,typeof(bool),Types_Object);
            Key_Equals.InitLocals=false;
            Key_Equals.DefineParameter(1,ParameterAttributes.None,"other");
            Key_TypeBuilder.DefineMethodOverride(Key_Equals,Object_Equals);
            var Key_Equals_I = Key_Equals.GetILGenerator();
            Key_Equals_I.Ldarg_1();
            Key_Equals_I.Isinst(Key_TypeBuilder);
            var falseの時 = Key_Equals_I.DefineLabel();
            Key_Equals_I.Brfalse_S(falseの時);
            Key_Equals_I.Ldarg_1();
            Key_Equals_I.Unbox_Any(Key_TypeBuilder);
            var 変数 = Key_Equals_I.M_DeclareLocal_Stloc(Key_TypeBuilder);
            Key_Equals_I.Ldarg_0();
            Key_Equals_I.Ldloc(変数);
            Key_Equals_I.Call(Key_IEquatable_Equals);
            var 終了 = Key_Equals_I.DefineLabel();
            Key_Equals_I.Br_S(終了);
            Key_Equals_I.MarkLabel(falseの時);
            Key_Equals_I.Ldc_I4_0();
            Key_Equals_I.MarkLabel(終了);
            Key_Equals_I.Ret();
        }
        共通override_Object_Equals終了(Table_TypeBuilder,Table_IEquatable_Equals,OpCodes.Isinst);
        var Types3 = this.Types3;
        Types3[0]=Key_TypeBuilder;
        Types3[1]=Table_TypeBuilder;
        Types3[2]=Container_TypeBuilder;
        var Set3 = typeof(Set<,,>).MakeGenericType(Types3);
        var (Tables_FieldBuilder,Tables_MethodBuilder) = PrivateField実装Property実装GetMethod実装(Schema_TypeBuilder,Set3,Table_TypeBuilder.Name,Set3);
        Schema_ctor_I.Ldarg_0();
        Schema_ctor_I.Ldarg_1();
        Schema_ctor_I.Newobj(TypeBuilder.GetConstructor(Set3,Set3_ctor));
        Schema_ctor_I.Stfld(Tables_FieldBuilder);
        {
            //Viewsには存在しない
            Schema_Read_I.Ldarg_0();
            Schema_Read_I.Ldfld(Tables_FieldBuilder);
            Schema_Read_I.Ldarg_1();
            Schema_Read_I.Call(TypeBuilder.GetMethod(ImmutableSet,ImmutableSet_Read));
            Schema_Equals_I.Ldarg_0();
            Schema_Equals_I.Ldfld(Tables_FieldBuilder);
            Schema_Equals_I.Ldarg_1();
            Schema_Equals_I.Ldfld(Tables_FieldBuilder);
            Schema_Equals_I.Callvirt(Object_Equals);//本当はネイティブImmutable<>.Equalsを呼び出したい
            Schema_Equals_I.Brfalse(Schema_Equalsでfalseの時);

            Schema_Assign_I.Ldarg_0();
            Schema_Assign_I.Ldfld(Tables_FieldBuilder);
            Schema_Assign_I.Ldarg_1();
            Schema_Assign_I.Ldfld(Tables_FieldBuilder);
            Types1[0]=Table_TypeBuilder;
            var Tables_InstanceType = typeof(Set<>).MakeGenericType(Types1);
            Schema_Assign_I.Call(TypeBuilder.GetMethod(Tables_InstanceType,Set1_Assign));
            Schema_Clear_I.Ldarg_0();
            Schema_Clear_I.Ldfld(Tables_FieldBuilder);
            Schema_Clear_I.Callvirt(TypeBuilder.GetMethod(Tables_InstanceType,Set1_Clear));
            Key_TypeBuilder.CreateType();
        }
        Schema_ToString_I.Ldloc(Schema_ToString_sb);
        Schema_ToString_I.Ldstr(EscapedName+":");
        Schema_ToString_I.Call(StringBuilder_Append_String);
        Schema_ToString_I.Ldarg_0();
        Schema_ToString_I.Ldfld(Tables_FieldBuilder);
        Schema_ToString_I.Callvirt(Object_ToString);
        Schema_ToString_I.Call(StringBuilder_AppendLine_String);
        Schema_ToString_I.Pop();
        this.Dictionary_Table.Add(Object,new ITable.Information(
            Key_TypeBuilder,
            Key_IEquatable_Equals,
            Key_ctor,
            Table_TypeBuilder,
            Table_ctor_I,
            Tables_FieldBuilder,
            Tables_MethodBuilder,
            AddRelationship_I,
            RemoveRelationship_I
        )
        );
    }
}
