using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Others;
using Reader = MemoryPackReader;
public class Anonymous<T>:MemoryPackFormatter<T>{
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
    public static readonly Anonymous<T> Instance=new();//リフレクションで使われる
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    private delegate void delegate_Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ref T value) where TBufferWriter:IBufferWriter<byte>;
    private delegate void delegate_Deserialize(ref Reader reader,scoped ref T? value);
    private readonly delegate_Deserialize DelegateDeserialize;
    public Anonymous(){
        var Types1=new Type[1];
        var DeserializeTypes=new Type[2];
        DeserializeTypes[0]=typeof(Reader).MakeByRefType();
        DeserializeTypes[1]=typeof(T).MakeByRefType();
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties=typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length=Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        {
            //var MethodDeserialize = typeof(Anonymous).GetMethod("Deserialize2",BindingFlags.Static|BindingFlags.NonPublic)!;
            var Deserialize=new DynamicMethod("Deserialize",typeof(void),DeserializeTypes,typeof(Anonymous<T>),true){InitLocals=false};
            var I1=Deserialize.GetILGenerator();
            I1.Emit(OpCodes.Ldarg_1);//ref value
            var index=0;
            while(true){
                var Property=Properties[index];
                Types1[0]=Property.PropertyType;
                I1.Emit(OpCodes.Ldarg_0);//reader
                var L=I1.DeclareLocal(Property.PropertyType);
                I1.Emit(OpCodes.Ldloca,L);//value
                //I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Call,Extension.MethodDeserialize.MakeGenericMethod(Types1));
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
    private static readonly Dictionary<Type,Delegate> DictionarySerialize=new();
    private readonly Type[] MethodTypes=new Type[2];
    private readonly Type[] SerializeTypes=new Type[2];
    private readonly Type[] PropertyTypes=new Type[1];
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        if(!DictionarySerialize.TryGetValue(typeof(TBufferWriter),out var Delegate)){
            var MethodTypes=this.MethodTypes;
            var SerializeTypes=this.SerializeTypes;
            var PropertyTypes=this.PropertyTypes;
            SerializeTypes[0]=typeof(MemoryPackWriter<TBufferWriter>).MakeByRefType();
            SerializeTypes[1]=typeof(T).MakeByRefType();
            var ctor=typeof(T).GetConstructors()[0];
            var Parameters=ctor.GetParameters();
            var Properties=typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            var Properties_Length=Properties.Length;
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
                        var Register=Serializer.Register.MakeGenericMethod(PropertyTypes);
                        Register.Invoke(null,new[]{Activator.CreateInstance(FormatterType)});
                    }
                    MethodTypes[1]=PropertyType;
                    I0.Emit(OpCodes.Ldarg_0);//writer
                    I0.Emit(OpCodes.Ldarg_1);//value
                    I0.Emit(OpCodes.Ldobj,typeof(T));//*value
                    Debug.Assert(Property.GetMethod!=null&&!Property.GetMethod.IsVirtual);
                    I0.Emit(OpCodes.Call,Property.GetMethod);//value.property
                    var L=I0.DeclareLocal(Property.PropertyType);
                    I0.Emit(OpCodes.Stloc,L);//value=
                    I0.Emit(OpCodes.Ldloca,L);//ref value
                    I0.Emit(OpCodes.Call,Extension.MethodSerialize.MakeGenericMethod(MethodTypes));
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
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>this.DelegateDeserialize(ref reader,ref value);
}
