using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Others;


using Reader = MemoryPackReader;
public class Anonymous<T>:MemoryPackFormatter<T>{
    public static readonly Anonymous<T> Instance=new();
    private delegate void delegate_Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ref T value) where TBufferWriter:IBufferWriter<byte>;
    //private delegate void delegate_Read(ref Reader reader,scoped ref T? value);
    private delegate T? delegate_Read(ref Reader reader);
    private readonly delegate_Read Read;
    private Anonymous(){
        var Types1=new Type[1];
        
        
        
        
        
        
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties=typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        {
            var ReadMethod=new DynamicMethod("Read",typeof(T),new[]{typeof(Reader).MakeByRefType()},typeof(Anonymous<T>),true){InitLocals=false};
            共通(ReadMethod.GetILGenerator());
            this.Read=(delegate_Read)ReadMethod.CreateDelegate(typeof(delegate_Read));
        }
        void 共通(ILGenerator RI){
            foreach(var Property in Properties){
                Types1[0]=Property.PropertyType;
                RI.Ldarg_0();//reader
                var L=RI.DeclareLocal(Property.PropertyType);
                RI.Ldloca(L);//value
                RI.Call(Extension.DeserializeMethod.MakeGenericMethod(Types1));
                RI.Ldloc(L);//value
            }
            RI.Newobj(ctor);
            RI.Ret();
        }
    }
    //GenericクラスのT別のインスタンスが欲しいのでこれでいい
    private static readonly ConcurrentDictionary<Type,Delegate> Writes=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        //var f=writer.GetFormatter<ValueTuple<int>>();
        if(writer.TryWriteNil(value)) return;
        if(!Writes.TryGetValue(typeof(TBufferWriter),out var Write)){
            var Types2=new Type[2];
            var ctor=typeof(T).GetConstructors()[0];
            var Parameters=ctor.GetParameters();
            var Properties=typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            var Properties_Length=Properties.Length;
            Debug.Assert(Parameters.Length==Properties_Length);
            Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
            var WriteMethod=new DynamicMethod("Write",typeof(void),new[]{typeof(MemoryPackWriter<TBufferWriter>).MakeByRefType(),typeof(T).MakeByRefType()},typeof(Anonymous<T>),true){InitLocals=false};
            //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
            var WI=WriteMethod.GetILGenerator();
            Types2[0]=typeof(TBufferWriter);
            foreach(var Property in Properties){
                var PropertyType=Property.PropertyType;
                Types2[1]=PropertyType;
                WI.Ldarg_0();//writer
                WI.Ldarg_1();//value
                WI.Ldobj(typeof(T));//*value
                Debug.Assert(Property.GetMethod!=null&&!Property.GetMethod.IsVirtual);
                WI.Call(Property.GetMethod);//value.property
                var L=WI.M_DeclareLocal_Stloc(Property.PropertyType);
                WI.Ldloca(L);//ref value
                WI.Call(Extension.SerializeMethod.MakeGenericMethod(Types2));
            }
            WI.Ret();
            Write=WriteMethod.CreateDelegate(typeof(delegate_Write<TBufferWriter>));
            Writes.TryAdd(typeof(TBufferWriter),Write);
        }
        ((delegate_Write<TBufferWriter>)Write)(ref writer,ref value!);
    }
    //public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
    //    if(writer.TryWriteNil(value)) return;
    //    if(!Extension.Writes.TryGetValue(typeof(TBufferWriter),out var Write)){
    //        var Types2=new Type[2];
    //        var ctor=typeof(T).GetConstructors()[0];
    //        var Parameters=ctor.GetParameters();
    //        var Properties=typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
    //        var Properties_Length=Properties.Length;
    //        Debug.Assert(Parameters.Length==Properties_Length);
    //        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
    //        {
    //            var WriteMethod=new DynamicMethod("Write",typeof(void),new[]{typeof(MemoryPackWriter<TBufferWriter>).MakeByRefType(),typeof(T).MakeByRefType()},typeof(Anonymous<T>),true){InitLocals=false};
    //            var I0=WriteMethod.GetILGenerator();
    //            Types2[0]=typeof(TBufferWriter);
    //            var index=0;
    //            while(true){
    //                var Property=Properties[index];
    //                var PropertyType=Property.PropertyType;
    //                Types2[1]=PropertyType;
    //                I0.Ldarg_0();//writer
    //                I0.Ldarg_1();//value
    //                I0.Ldobj(typeof(T));//*value
    //                Debug.Assert(Property.GetMethod!=null&&!Property.GetMethod.IsVirtual);
    //                I0.Call(Property.GetMethod);//value.property
    //                var L=I0.DeclareLocal(Property.PropertyType);
    //                I0.Stloc(L);//value=
    //                I0.Ldloca(L);//ref value
    //                I0.Call(Extension.SerializeMethod.MakeGenericMethod(Types2));
    //                index++;
    //                if(index==Properties_Length) break;
    //            }
    //            I0.Ret();
    //            Write=WriteMethod.CreateDelegate(typeof(delegate_Write<TBufferWriter>));
    //            Extension.Writes.TryAdd(typeof(TBufferWriter),Write);
    //        }
    //    }
    //    ((delegate_Write<TBufferWriter>)Write)(ref writer,ref value!);
    //}
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=reader.TryReadNil()?default:this.Read(ref reader);
}
