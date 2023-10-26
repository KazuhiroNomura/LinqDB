using System;
using System.Reflection;
using System.Reflection.Emit;

using Lokad.ILPack;
using LinqDB.Helpers;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
public class DisplayClass<T>:IJsonFormatter<T>{
    public static readonly DisplayClass<T> Instance=new();

    private delegate void delegate_Serialize(ref Writer writer,T value,O Resolver);
    private delegate T delegate_Deserialize(ref Reader reader,O Resolver);
    private readonly delegate_Serialize Write;
    private readonly delegate_Deserialize Read;
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
            var WriteMethod=new DynamicMethod("Write",typeof(void),WriteTypes,typeof(Extension),true){InitLocals=false};
            var ReadMethod=new DynamicMethod("Read",typeof(T),ReadTypes,typeof(Extension),true){InitLocals=false};
            共通(WriteMethod.GetILGenerator(),ReadMethod.GetILGenerator());
            this.Write=(delegate_Serialize)WriteMethod.CreateDelegate(typeof(delegate_Serialize));
            this.Read=(delegate_Deserialize)ReadMethod.CreateDelegate(typeof(delegate_Deserialize));
        }
        {
            var AssemblyName=new AssemblyName{Name="Name"};
            var DynamicAssembly=AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
            var ModuleBuilder=DynamicAssembly.DefineDynamicModule("動的");
            var Disp_TypeBuilder=ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
            var WriteMethod=Disp_TypeBuilder.DefineMethod("Write",MethodAttributes.Static|MethodAttributes.Public,typeof(void),WriteTypes);
            WriteMethod.DefineParameter(1,ParameterAttributes.None,"writer");
            WriteMethod.DefineParameter(2,ParameterAttributes.None,"value");
            WriteMethod.DefineParameter(3,ParameterAttributes.None,"Resolver");
            var ReadMethod=Disp_TypeBuilder.DefineMethod("Read",MethodAttributes.Static|MethodAttributes.Public,typeof(T),ReadTypes);
            ReadMethod.DefineParameter(1,ParameterAttributes.None,"writer");
            ReadMethod.DefineParameter(2,ParameterAttributes.None,"Resolver");
            ReadMethod.InitLocals=false;
            共通(WriteMethod.GetILGenerator(),ReadMethod.GetILGenerator());
            Disp_TypeBuilder.CreateType();
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Environment.CurrentDirectory}\Utf8Json.DisplayClass,WriteRead.dll");
        }
        void 共通(ILGenerator WI,ILGenerator RI){
            RI.Newobj(typeof(T).GetConstructor(Type.EmptyTypes)!);
            RI.DeclareLocal(typeof(T));
            RI.Stloc_0();
            if(Fields_Length>0){
                var index=0;
                while(true){
                    var Field=Fields[index];
                    Types1[0]=Field.FieldType;
                    WI.Ldarg_0();//writer
                    WI.Ldstr(Field.Name);
                    WI.Call(Extension.WriteString);
                    WI.Ldarg_0();//writer
                    WI.Call(Extension.WriteNameSeparator);
                    WI.Ldarg_0();//writer
                    WI.Ldarg_1();//value
                    WI.Ldfld(Field);//value.field
                    WI.Ldarg_2();//resolver
                    WI.Call(Extension.SerializeMethod.MakeGenericMethod(Types1));
                    RI.Ldarg_0();//reader
                    RI.Call(Extension.ReadString);//Nameを読む
                    RI.Pop();//Nameを捨てる
                    RI.Ldarg_0();//reader
                    RI.Call(Extension.ReadIsNameSeparatorWithVerify);//":"を読む
                    RI.Ldloc_0();//display
                    RI.Ldarg_0();//display reader
                    RI.Ldarg_1();//display reader resolver
                    RI.Call(Extension.DeserializeMethod.MakeGenericMethod(Types1));//display Deserialize(ref reader,resolver)
                    RI.Stfld(Field);//display.field=Deserialize(ref reader,resolver)
                    index++;
                    if(index==Fields_Length) break;
                    WI.Ldarg_0();
                    WI.Call(Extension.WriteValueSeparator);
                    RI.Ldarg_0();
                    RI.Call(Extension.ReadIsValueSeparatorWithVerify);
                }
            }
            WI.Ret();
            RI.Ldloc_0();
            RI.Ret();
        }
    }
    
    
    
    
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        this.Write(ref writer,value,Resolver);
    }
    public T Deserialize(ref Reader reader,O Resolver)=>reader.TryReadNil()?default!:this.Read(ref reader,Resolver);
}
