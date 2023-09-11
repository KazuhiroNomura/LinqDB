using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using LinqDB.Serializers.MessagePack.Formatters;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
internal static class DisplayClass {
    public static readonly Dictionary<System.Type,Delegate> DictionarySerialize = new();
}
public class DisplayClass<T>:MemoryPackFormatter<T>{
    public static readonly DisplayClass<T> Instance=new();
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
    private delegate void delegate_Deserialize(ref Reader reader,scoped ref T?value);
    private readonly delegate_Deserialize DelegateDeserialize;
    public DisplayClass(){
        //DisplayClassMessagePackFormatterを参考にする
        var Types1 = new System.Type[1];
        var DeserializeTypes = new System.Type[2];
        DeserializeTypes[0]=typeof(Reader).MakeByRefType();
        DeserializeTypes[1]=typeof(T).MakeByRefType();
        var ctor = typeof(T).GetConstructors()[0];
        var Fields = typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
        var Fields_Length = Fields.Length;
        Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        {
            //var MethodDeserialize = typeof(Anonymous).GetMethod("Deserialize2",BindingFlags.Static|BindingFlags.NonPublic)!;
            var Deserialize = new DynamicMethod("Deserialize",typeof(void),DeserializeTypes,typeof(Anonymous<T>),true) { InitLocals=false };
            var I1 = Deserialize.GetILGenerator();
            I1.Emit(OpCodes.Ldarg_1);//value=new c_DisplayClass()
            I1.Emit(OpCodes.Newobj,ctor);
            I1.Emit(OpCodes.Stobj,typeof(T));
            var index = 0;
            while(true) {
                var Field = Fields[index];
                var FieldType=Field.FieldType;
                Types1[0]=FieldType;
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Ldarg_1);//value
                I1.Emit(OpCodes.Ldind_Ref);
                I1.Emit(OpCodes.Ldflda,Field);//value.field
                I1.Emit(OpCodes.Call,Anonymous.MethodDeserialize.MakeGenericMethod(Types1));//Deserialize(ref reader,ref value.field)
                index++;
                if(index==Fields_Length) break;
            }
            I1.Emit(OpCodes.Ret);
            this.DelegateDeserialize=(delegate_Deserialize)Deserialize.CreateDelegate(typeof(delegate_Deserialize));
        }
    }
    private readonly System.Type[] MethodTypes = new System.Type[2];
    private readonly System.Type[] SerializeTypes = new System.Type[2];
    private readonly System.Type[] FieldTypes = new System.Type[1];
    private readonly object[] objects1=new object[1];
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(!DisplayClass.DictionarySerialize.TryGetValue(typeof(TBufferWriter),out var Delegate)){
            var MethodTypes =this.MethodTypes;
            var SerializeTypes =this.SerializeTypes;
            var FieldTypes=this.FieldTypes;
            SerializeTypes[0]=typeof(MemoryPackWriter<TBufferWriter>).MakeByRefType();
            SerializeTypes[1]=typeof(T).MakeByRefType();
            //var ctor=typeof(T).GetConstructors()[0];
            var Fields = typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
            var Fields_Length = Fields.Length;
            Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
            {
                var objects1=this.objects1;
                var Serialize=new DynamicMethod("Serialize",typeof(void),SerializeTypes,typeof(Anonymous<T>),true){InitLocals=false};
                //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
                var I0=Serialize.GetILGenerator();
                MethodTypes[0]=typeof(TBufferWriter);
                var index=0;
                while(true){
                    var Field=Fields[index];
                    var FieldType=Field.FieldType;
                    if(FieldType.IsAnonymous()){
                        FieldTypes[0]=FieldType;
                        var FormatterType=typeof(Anonymous<>).MakeGenericType(FieldTypes);
                        var Register=Serializer.Register.MakeGenericMethod(FieldTypes);
                        objects1[0]=Activator.CreateInstance(FormatterType)!;
                        Register.Invoke(null,objects1);
                    }
                    MethodTypes[1]=FieldType;
                    I0.Emit(OpCodes.Ldarg_0);//writer
                    I0.Emit(OpCodes.Ldarg_1);//value
                    I0.Emit(OpCodes.Ldind_Ref);//*value
                    I0.Emit(OpCodes.Ldflda,Field);//ref value.field
                    I0.Emit(OpCodes.Call,Anonymous.MethodSerialize.MakeGenericMethod(MethodTypes));
                    index++;
                    if(index==Fields_Length) break;
                }
                I0.Emit(OpCodes.Ret);
                Debug.Assert(SerializeTypes[0]==typeof(delegate_Serialize<TBufferWriter>).GetMethod("Invoke")!.GetParameters()[0].ParameterType);
                Debug.Assert(SerializeTypes[1]==typeof(delegate_Serialize<TBufferWriter>).GetMethod("Invoke")!.GetParameters()[1].ParameterType);
                Delegate=Serialize.CreateDelegate(typeof(delegate_Serialize<TBufferWriter>));
                DisplayClass.DictionarySerialize.Add(typeof(TBufferWriter),Delegate);
            }
        }
        ((delegate_Serialize<TBufferWriter>)Delegate)(ref writer,ref value!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value) {
        this.DelegateDeserialize(ref reader,ref value);
    }
}
