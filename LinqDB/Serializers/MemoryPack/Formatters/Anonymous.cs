using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
internal static class Anonymous{
    private static void Serialize2<TBufferWriter, TValue>(ref MemoryPackWriter<TBufferWriter> writer,
        scoped ref TValue? value) where TBufferWriter : IBufferWriter<byte> {
        writer.WriteValue(value);
        //writer.GetFormatter<TValue>()!.Serialize(ref writer,ref value);
    }
    public static readonly MethodInfo MethodSerialize = typeof(Anonymous).GetMethod(nameof(Serialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Deserialize2<TValue>(ref MemoryPackReader reader,scoped ref TValue? value) {
        reader.ReadValue(ref value);
        //reader.GetFormatter<TValue>()!.Deserialize(ref reader,ref value);
    }
    public static readonly MethodInfo MethodDeserialize = typeof(Anonymous).GetMethod(nameof(Deserialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
}
public class Anonymous<T>:MemoryPackFormatter<T>{
    public static readonly Anonymous<T> Instance=new();
    //private static void Serialize2<TBufferWriter,TValue>(ref MemoryPackWriter<TBufferWriter> writer,
    //    scoped ref TValue? value) where TBufferWriter:IBufferWriter<byte>{
    //    writer.WriteValue(value);
    //    //writer.GetFormatter<TValue>()!.Serialize(ref writer,ref value);
    //}
    //private static readonly MethodInfo MethodSerialize = typeof(Anonymous<T>).GetMethod(nameof(Serialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
    //private static void Deserialize2<TValue>(ref MemoryPackReader reader,scoped ref TValue? value){
    //    reader.ReadValue(ref value);
    //    //reader.GetFormatter<TValue>()!.Deserialize(ref reader,ref value);
    //}
    ////private static readonly MethodInfo MethodDeserialize = typeof(Anonymous<T>).GetMethod(nameof(Deserialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
    private delegate void delegate_Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ref T value)where TBufferWriter:IBufferWriter<byte>;
    private delegate void delegate_Deserialize(ref MemoryPackReader reader,scoped ref T?value);
    private readonly delegate_Deserialize DelegateDeserialize;
    public Anonymous(){
        var Types1 = new System.Type[1];
        var DeserializeTypes = new System.Type[2];
        DeserializeTypes[0]=typeof(MemoryPackReader).MakeByRefType();
        DeserializeTypes[1]=typeof(T).MakeByRefType();
        var ctor = typeof(T).GetConstructors()[0];
        var Parameters = ctor.GetParameters();
        var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length = Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter => Properties.Single(Property => Property.Name==Parameter.Name)).ToArray();
        {
            //var MethodDeserialize = typeof(Anonymous).GetMethod("Deserialize2",BindingFlags.Static|BindingFlags.NonPublic)!;
            var Deserialize = new DynamicMethod("Deserialize",typeof(void),DeserializeTypes,typeof(Anonymous<T>),true) { InitLocals=false };
            var I1 = Deserialize.GetILGenerator();
            I1.Emit(OpCodes.Ldarg_1);
            var index = 0;
            while(true) {
                var Property = Properties[index];
                Types1[0]=Property.PropertyType;
                I1.Emit(OpCodes.Ldarg_0);//reader
                var L = I1.DeclareLocal(Property.PropertyType);
                I1.Emit(OpCodes.Ldloca,L);//value
                //I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Call,Anonymous.MethodDeserialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldloc,L);//value
                index++;
                if(index==Properties_Length) break;
            }
            I1.Emit(OpCodes.Newobj,ctor);
            I1.Emit(OpCodes.Stobj,typeof(T));
            I1.Emit(OpCodes.Ret);
            this.DelegateDeserialize=(delegate_Deserialize)Deserialize.CreateDelegate(typeof(delegate_Deserialize));
        }
    }
    private static readonly Dictionary<System.Type,Delegate> DictionarySerialize=new();
    private readonly System.Type[] MethodTypes = new System.Type[2];
    private readonly System.Type[] SerializeTypes = new System.Type[2];
    private readonly System.Type[] PropertyTypes = new System.Type[1];
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(!DictionarySerialize.TryGetValue(typeof(TBufferWriter),out var Delegate)){
            var MethodTypes =this.MethodTypes;
            var SerializeTypes =this.SerializeTypes;
            var PropertyTypes=this.PropertyTypes;
            SerializeTypes[0]=typeof(MemoryPackWriter<TBufferWriter>).MakeByRefType();
            SerializeTypes[1]=typeof(T).MakeByRefType();
            var ctor=typeof(T).GetConstructors()[0];
            var Parameters=ctor.GetParameters();
            var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            var Properties_Length = Properties.Length;
            Debug.Assert(Parameters.Length==Properties_Length);
            Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
            {
                var Serialize=new DynamicMethod("Serialize",typeof(void),SerializeTypes,typeof(Anonymous<T>),true){InitLocals=false};
                //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
                var I0=Serialize.GetILGenerator();
                MethodTypes[0]=typeof(TBufferWriter);
                var index=0;
                while(true){
                    var Property=Properties[index];
                    var PropertyType=Property.PropertyType;
                    if(PropertyType.IsAnonymous()){
                        PropertyTypes[0]=PropertyType;
                        var FormatterType=typeof(Anonymous<>).MakeGenericType(PropertyTypes);
                        var Register=MemoryPackCustomSerializer.Register.MakeGenericMethod(PropertyTypes);
                        Register.Invoke(null,new[]{Activator.CreateInstance(FormatterType)});
                    }
                    MethodTypes[1]=PropertyType;
                    I0.Emit(OpCodes.Ldarg_0);//writer
                    I0.Emit(OpCodes.Ldarg_1);//value
                    I0.Emit(OpCodes.Ldobj,typeof(T));//*value
                    Debug.Assert(Property.GetMethod!=null&&!Property.GetMethod.IsVirtual);
                    I0.Emit(OpCodes.Call,Property.GetMethod);//value.property
                    var L = I0.DeclareLocal(Property.PropertyType);
                    I0.Emit(OpCodes.Stloc,L);//value=
                    I0.Emit(OpCodes.Ldloca,L);//ref value
                    I0.Emit(OpCodes.Call,Anonymous.MethodSerialize.MakeGenericMethod(MethodTypes));
                    index++;
                    if(index==Properties_Length) break;
                }
                I0.Emit(OpCodes.Ret);
                Debug.Assert(SerializeTypes[0]==typeof(delegate_Serialize<TBufferWriter>).GetMethod("Invoke")!.GetParameters()[0].ParameterType);
                Debug.Assert(SerializeTypes[1]==typeof(delegate_Serialize<TBufferWriter>).GetMethod("Invoke")!.GetParameters()[1].ParameterType);
                Delegate=Serialize.CreateDelegate(typeof(delegate_Serialize<TBufferWriter>));
                DictionarySerialize.Add(typeof(TBufferWriter),Delegate);
            }
        }
        ((delegate_Serialize<TBufferWriter>)Delegate)(ref writer,ref value!);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value) {
        this.DelegateDeserialize(ref reader,ref value);
    }
}
//public class AnonymousMemoryPackFormatter<T>:MemoryPackFormatter<T>{
//    private static void Serialize2<TBufferWriter,TValue>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref TValue? value)where TBufferWriter:IBufferWriter<byte> => writer.GetFormatter<TValue>()!.Serialize(ref writer,ref value);
//    private static readonly MethodInfo MethodSerialize = typeof(AnonymousMemoryPackFormatter<T>).GetMethod(nameof(Serialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
//    private static void Deserialize2<TValue>(ref MemoryPackReader reader,scoped ref TValue?value) => reader.GetFormatter<TValue>()!.Deserialize(ref reader,ref value);
//    private static readonly MethodInfo MethodDeserialize = typeof(AnonymousMemoryPackFormatter<T>).GetMethod(nameof(Deserialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
//    private static readonly MethodInfo WriteString = typeof(MemoryPackWriter<IBufferWriter<byte>>).GetMethod(nameof(MemoryPackWriter<IBufferWriter<byte>>.WriteString))!;
//    //private static readonly MethodInfo WriteArrayHeader = typeof(MemoryPackWriter<T>).GetMethod(nameof(MemoryPackWriter.WriteArrayHeader),new[] { typeof(int) })!;
//    //private static readonly MethodInfo ReadArrayHeader = typeof(MemoryPackReader).GetMethod(nameof(MemoryPackReader.ReadArrayHeader))!;
//    private delegate void delegate_Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ref T value)where TBufferWriter:IBufferWriter<byte>;
//    private delegate void delegate_Deserialize(ref MemoryPackReader reader,scoped ref T?value);
//    private readonly delegate_Deserialize DelegateDeserialize;
//    void store<TValue>(ref MemoryPackReader reader,scoped ref TValue?value)where TValue:new(){
//        int x=1;
//        reader.GetFormatter<int>().Deserialize(ref reader,ref x);
//        value=new();
//    }
//    public AnonymousMemoryPackFormatter(){
//        var Types1 = new Type[1];
//        //var SerializeTypes = new Type[2];
//        var DeserializeTypes = new Type[2];
//        //SerializeTypes[0]=typeof(MemoryPackWriter<IBufferWriter<byte>>).MakeByRefType();
//        DeserializeTypes[0]=typeof(MemoryPackReader).MakeByRefType();
//        DeserializeTypes[1]=typeof(T).MakeByRefType();
//        var ctor = typeof(T).GetConstructors()[0];
//        var Parameters = ctor.GetParameters();
//        var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
//        var Properties_Length = Properties.Length;
//        Debug.Assert(Parameters.Length==Properties_Length);
//        Properties=Parameters.Select(Parameter => Properties.Single(Property => Property.Name==Parameter.Name)).ToArray();
//        {
//            //var Serialize=new DynamicMethod("Serialize",typeof(void),SerializeTypes,typeof(AnonymousMemoryPackFormatter<T>),true){InitLocals=false};
//            var Deserialize = new DynamicMethod("Deserialize",typeof(void),DeserializeTypes,typeof(AnonymousMemoryPackFormatter<T>),true) { InitLocals=false };
//            //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
//            var I1 = Deserialize.GetILGenerator();
//            共通(I1);
//            //this.DelegateSerialize=(delegate_Serialize)Serialize.CreateDelegate(typeof(delegate_Serialize));
//            Debug.Assert(DeserializeTypes[0]==typeof(delegate_Deserialize).GetMethod("Invoke").GetParameters()[0].ParameterType);
//            Debug.Assert(DeserializeTypes[1]==typeof(delegate_Deserialize).GetMethod("Invoke").GetParameters()[1].ParameterType);
//            this.DelegateDeserialize=(delegate_Deserialize)Deserialize.CreateDelegate(typeof(delegate_Deserialize));
//        }
//        {
//            var AssemblyName = new AssemblyName { Name="Name" };
//            var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
//            var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
//            var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
//            //var Serialize = Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),SerializeTypes);
//            //Serialize.DefineParameter(1,ParameterAttributes.None,"writer");
//            //Serialize.DefineParameter(2,ParameterAttributes.None,"value");
//            //Serialize.DefineParameter(3,ParameterAttributes.None,"options");
//            var Deserialize = Disp_TypeBuilder.DefineMethod($"Deserialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),DeserializeTypes);
//            Deserialize.DefineParameter(1,ParameterAttributes.None,"writer");
//            Deserialize.DefineParameter(2,ParameterAttributes.None,"value");
//            Deserialize.InitLocals=false;
//            共通(Deserialize.GetILGenerator());
//            Disp_TypeBuilder.CreateType();
//            var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
//            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\MemoryPackSerializer.dll");
//        }
//        void 共通(ILGenerator I1) {
//            //I0.Emit(OpCodes.Ldarg_0);//writer
//            //I0.Emit(OpCodes.Ldc_I4,Properties_Length);
//            //I0.Emit(OpCodes.Call,WriteArrayHeader);
//            //I1.Emit(OpCodes.Ldarg_0);//reader
//            //I1.Emit(OpCodes.Call,ReadArrayHeader);
//            //I1.Emit(OpCodes.Pop);
//            I1.Emit(OpCodes.Ldarg_1);
//            var index = 0;
//            while(true) {
//                var Property = Properties[index];
//                Types1[0]=Property.PropertyType;
//                I1.Emit(OpCodes.Ldarg_0);//reader
//                var L = I1.DeclareLocal(Property.PropertyType);
//                I1.Emit(OpCodes.Ldloca,L);//value
//                I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
//                I1.Emit(OpCodes.Ldloc,L);//value
//                index++;
//                if(index==Properties_Length) break;
//            }
//            I1.Emit(OpCodes.Newobj,ctor);
//            I1.Emit(OpCodes.Stobj,typeof(T));
//            //I1.Emit(OpCodes.Stind_Ref);
//            I1.Emit(OpCodes.Ret);
//        }
//    }
//    //public void Serialize(ref MemoryPackWriter<T> writer,scoped ref T? value){
//    //    if(value is null){
//    //        writer.WriteNil();
//    //        return;
//    //    }
//    //    this.DelegateSerialize(ref writer, value,options);
//    //}
//    //public T Deserialize(ref MemoryPackReader reader,MemoryPackSerializerOptions options){
//    //    if(reader.TryReadNil()) return default!;
//    //    return this.DelegateDeserialize(ref reader,options);
//    //}
//    private static readonly Dictionary<Type,Delegate> DictionarySerialize=new();
//    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
//        if(!DictionarySerialize.TryGetValue(typeof(TBufferWriter),out var Delegate)){
//            var MethodTypes = new Type[2];
//            var SerializeTypes = new Type[2];
//            SerializeTypes[0]=typeof(MemoryPackWriter<TBufferWriter>).MakeByRefType();
//            SerializeTypes[1]=typeof(T).MakeByRefType();
//            var ctor=typeof(T).GetConstructors()[0];
//            var Parameters=ctor.GetParameters();
//            var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
//            var Properties_Length = Properties.Length;
//            Debug.Assert(Parameters.Length==Properties_Length);
//            Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
//            {
//                var Serialize=new DynamicMethod("Serialize",typeof(void),SerializeTypes,typeof(AnonymousMemoryPackFormatter<T>),true){InitLocals=false};
//                //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
//                var I0=Serialize.GetILGenerator();
//                共通(I0);
//                Debug.Assert(SerializeTypes[0]==typeof(delegate_Serialize<TBufferWriter>).GetMethod("Invoke").GetParameters()[0].ParameterType);
//                Debug.Assert(SerializeTypes[1]==typeof(delegate_Serialize<TBufferWriter>).GetMethod("Invoke").GetParameters()[1].ParameterType);
//                Delegate=Serialize.CreateDelegate(typeof(delegate_Serialize<TBufferWriter>));
//                DictionarySerialize.Add(typeof(TBufferWriter),Delegate);
//            }
//            {
//                var AssemblyName = new AssemblyName { Name="Name" };
//                var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
//                var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
//                var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
//                var Serialize = Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),SerializeTypes);
//                Serialize.DefineParameter(1,ParameterAttributes.None,"writer");
//                Serialize.DefineParameter(2,ParameterAttributes.None,"value");
//                共通(Serialize.GetILGenerator());
//                Disp_TypeBuilder.CreateType();
//                var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
//                new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\MemoryPackSerializer.dll");
//            }
//            void 共通(ILGenerator I0){
//                //I0.Emit(OpCodes.Ldarg_0);//writer
//                //I0.Emit(OpCodes.Ldc_I4,Properties_Length);
//                //I0.Emit(OpCodes.Call,WriteArrayHeader);
//                //I1.Emit(OpCodes.Ldarg_0);//reader
//                //I1.Emit(OpCodes.Call,ReadArrayHeader);
//                //I1.Emit(OpCodes.Pop);
//                MethodTypes[0]=typeof(TBufferWriter);
//                var index=0;
//                while(true){
//                    var Property=Properties[index];
//                    MethodTypes[1]=Property.PropertyType;
//                    I0.Emit(OpCodes.Ldarg_0);//writer
//                    I0.Emit(OpCodes.Ldarg_1);//value
//                    I0.Emit(OpCodes.Ldobj,typeof(T));//*value
//                    Debug.Assert(!Property.GetMethod.IsVirtual);
//                    I0.Emit(OpCodes.Call,Property.GetMethod);//value.property
//                    var L = I0.DeclareLocal(Property.PropertyType);
//                    I0.Emit(OpCodes.Stloc,L);//value=
//                    I0.Emit(OpCodes.Ldloca,L);//ref value
//                    I0.Emit(OpCodes.Call,MethodSerialize.MakeGenericMethod(MethodTypes));
//                    index++;
//                    if(index==Properties_Length) break;
//                }
//                I0.Emit(OpCodes.Ret);
//            }
//        }
//        var Serialize2=(delegate_Serialize<TBufferWriter>)Delegate;
//        Serialize2(ref writer,ref value);
//        //if(value is null) {
//        //    writer.WriteNil();
//        //    return;
//        //}
//        //this.DelegateSerialize(ref writer,value,options);
//    }
//    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value) {
//        //if (value == null)
//        //{
//        //    writer.WriteNullObjectHeader();
//        //    goto END;
//        //}

//        //if (!reader.TryReadObjectHeader(out var count))
//        //{
//        //    value = default!;
//        //    goto END;
//        //}
//        //if (!reader.TryReadUnionHeader(out var tag)){
//        //    value = default;
//        //    return;
//        //}
//        this.DelegateDeserialize(ref reader,ref value);
//        //return this.DelegateDeserialize(ref reader,options);
//    }
//}
