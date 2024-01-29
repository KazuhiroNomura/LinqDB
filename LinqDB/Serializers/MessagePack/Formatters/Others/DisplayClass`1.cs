using System;
using System.Reflection;
using System.Reflection.Emit;

using LinqDB.Helpers;
using Lokad.ILPack;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Others;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
public class DisplayClass<T>:IMessagePackFormatter<T>{
    public static readonly DisplayClass<T> Instance=new();
    
    private delegate void delegate_Write(ref Writer writer,T value,O Resolver);
    private delegate T delegate_Read(ref Reader reader,O Resolver);
    private readonly delegate_Write Write;
    private readonly delegate_Read Read;
    private DisplayClass(){
        var Types1=new Type[1];
        var ReadTypes=new Type[2];
        var WriteTypes=new Type[3];
        WriteTypes[0]=typeof(Writer).MakeByRefType();
        ReadTypes[0]=typeof(Reader).MakeByRefType();
        WriteTypes[1]=typeof(T);
        ReadTypes[1]=WriteTypes[2]=typeof(O);
        var ctor=typeof(T).GetConstructors()[0];
        var Fields=typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
        var Fields_Length=Fields.Length;
        Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        {
            var WriteMethod=new DynamicMethod("Write",typeof(void),WriteTypes,typeof(Utf8Json.Serializer),true){InitLocals=false};
            var ReadMethod=new DynamicMethod("Read",typeof(T),ReadTypes,typeof(Utf8Json.Serializer),true){InitLocals=false};
            共通(WriteMethod.GetILGenerator(),ReadMethod.GetILGenerator());
            this.Write=(delegate_Write)WriteMethod.CreateDelegate(typeof(delegate_Write));
            this.Read=(delegate_Read)ReadMethod.CreateDelegate(typeof(delegate_Read));
        }
        //{
        //    var AssemblyName=new AssemblyName{Name="Name"};
        //    var DynamicAssembly=AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        //    var ModuleBuilder=DynamicAssembly.DefineDynamicModule("動的");
        //    var Disp_TypeBuilder=ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
        //    var WriteMethod=Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),WriteTypes);
        //    WriteMethod.DefineParameter(1,ParameterAttributes.None,"writer");
        //    WriteMethod.DefineParameter(2,ParameterAttributes.None,"value");
        //    WriteMethod.DefineParameter(3,ParameterAttributes.None,"Resolver");
        //    var ReadMethod=Disp_TypeBuilder.DefineMethod($"Deserialize",MethodAttributes.Static|MethodAttributes.Public,typeof(T),ReadTypes);
        //    ReadMethod.DefineParameter(1,ParameterAttributes.None,"writer");
        //    ReadMethod.DefineParameter(2,ParameterAttributes.None,"Resolver");
        //    ReadMethod.InitLocals=false;
        //    共通(WriteMethod.GetILGenerator(),ReadMethod.GetILGenerator());
        //    Disp_TypeBuilder.CreateType();
        //    new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Environment.CurrentDirectory}\MessagePack.DisplayClass.WriteRead.dll");
        //}
        void 共通(ILGenerator I0,ILGenerator I1){
            I0.Ldarg_0();//writer
            I0.Ldc_I4(Fields_Length);
            I0.Call(Extension.WriteArrayHeader);
            I1.Ldarg_0();//reader
            I1.Call(Extension.ReadArrayHeader);
            I1.Pop();
            I1.Newobj(ctor);
            I1.DeclareLocal(typeof(T));
            I1.Stloc_0();//変数=new T()
            foreach(var Field in Fields){
                Types1[0]=Field.FieldType;
                I0.Ldarg_0();//writer
                I0.Ldarg_1();//value
                I0.Ldfld(Field);//value.property
                I0.Ldarg_2();//Resolver
                I0.Call(Extension.SerializeMethod.MakeGenericMethod(Types1));
                I1.Ldloc_0();//変数.
                I1.Ldarg_0();//reader
                I1.Ldarg_1();//Resolver
                I1.Call(Extension.DeserializeMethod.MakeGenericMethod(Types1));
                I1.Stfld(Field);//変数.field=Deserialize(ref reader,Resolver)
            }
            I0.Ret();
            I1.Ldloc_0();
            I1.Ret();
        }
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        this.Write(ref writer,value,Resolver);
    }
    public T Deserialize(ref Reader reader,O Resolver)=>reader.TryReadNil()?default!:this.Read(ref reader,Resolver);
}
