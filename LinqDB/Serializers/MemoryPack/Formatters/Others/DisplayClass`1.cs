using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using LinqDB.Helpers;

using MemoryPack;
using System.Buffers;
using Lokad.ILPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Others;

using Reader = MemoryPackReader;
public class DisplayClass<T>:MemoryPackFormatter<T>{
    public static readonly DisplayClass<T> Instance=new();
    private readonly Dictionary<Type,Delegate> DictionarySerialize=new();
    private delegate void delegate_Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ref T value) where TBufferWriter:IBufferWriter<byte>;
    private delegate T? delegate_Read(ref Reader reader);

    private readonly delegate_Read Read;
    private DisplayClass(){
        var Types1=new Type[1];
        
        


        var ctor=typeof(T).GetConstructors()[0];
        var Fields=typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
        Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        var ReadTypes=new[]{typeof(Reader).MakeByRefType()};
        {

            var ReadMethod=new DynamicMethod("Read",typeof(T),ReadTypes,typeof(DisplayClass<T>),true){InitLocals=false};
            共通(ReadMethod.GetILGenerator());

            this.Read=(delegate_Read)ReadMethod.CreateDelegate(typeof(delegate_Read));
        }
        //{
        //    var AssemblyName=new AssemblyName{Name="Name"};
        //    var DynamicAssembly=AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        //    var ModuleBuilder=DynamicAssembly.DefineDynamicModule("動的");
        //    var Disp_TypeBuilder=ModuleBuilder.DefineType("Disp",TypeAttributes.Public);




        //    var ReadMethod=Disp_TypeBuilder.DefineMethod("Read",MethodAttributes.Static|MethodAttributes.Public,typeof(T),ReadTypes);
        //    ReadMethod.DefineParameter(1,ParameterAttributes.None,"writer");

        //    ReadMethod.InitLocals=false;
        //    共通(ReadMethod.GetILGenerator());
        //    Disp_TypeBuilder.CreateType();
        //    new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Environment.CurrentDirectory}\MemoryPack.DisplayClass.Read.dll");
        //}
        void 共通(ILGenerator RI){
            RI.Emit(OpCodes.Newobj,ctor);
            var value=RI.M_DeclareLocal_Stloc(typeof(T));
            foreach(var Field in Fields){
                Types1[0]=Field.FieldType;
                RI.Ldarg_0();//reader
                RI.Ldloc_S(value);
                RI.Ldflda(Field);//value.field
                RI.Call(Extension.DeserializeMethod.MakeGenericMethod(Types1));//Deserialize(ref reader,ref value.field)
            }
            RI.Ldloc_S(value);
            RI.Ret();
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    //private readonly Type[] MethodTypes=new Type[2];
    //private readonly Type[] SerializeTypes=new Type[2];
    //private readonly Type[] FieldTypes=new Type[1];
    //private readonly object[] objects1=new object[1];
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        if(!this.DictionarySerialize.TryGetValue(typeof(TBufferWriter),out var Write)){
            var MethodTypes=new Type[2];
            var Fields=typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
            Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
            var WriteMethod=new DynamicMethod("Write",typeof(void),new[]{typeof(MemoryPackWriter<TBufferWriter>).MakeByRefType(),typeof(T).MakeByRefType()},typeof(DisplayClass<T>),true){InitLocals=false};
            var WI=WriteMethod.GetILGenerator();
            MethodTypes[0]=typeof(TBufferWriter);
            foreach(var Field in Fields){
                //var FieldType=Field.FieldType;
                //FormatterResolver.GetRegisteredFormatter(FieldType);
                MethodTypes[1]=Field.FieldType;
                WI.Ldarg_0();//writer
                WI.Ldarg_1();//value
                WI.Ldind_Ref();//*value
                WI.Ldflda(Field);//ref value.field
                WI.Call(Extension.SerializeMethod.MakeGenericMethod(MethodTypes));
            }
            WI.Ret();
            Write=WriteMethod.CreateDelegate(typeof(delegate_Write<TBufferWriter>));
            this.DictionarySerialize.Add(typeof(TBufferWriter),Write);
        }
        ((delegate_Write<TBufferWriter>)Write)(ref writer,ref value!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=reader.TryReadNil()?default:this.Read(ref reader);
}
