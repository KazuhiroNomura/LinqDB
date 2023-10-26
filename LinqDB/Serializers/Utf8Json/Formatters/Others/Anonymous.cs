using LinqDB.Helpers;

using Lokad.ILPack;

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;


using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
public class Anonymous<T>:IJsonFormatter<T>{
    public static readonly Anonymous<T> Instance=new();
    private delegate void delegate_Write(ref Writer writer,T value,O Resolver);
    private delegate T delegate_Read(ref Reader reader,O Resolver);
    private readonly delegate_Write Write;
    private readonly delegate_Read Read;
    private Anonymous(){
        var Types1=new Type[1];
        var ReadTypes=new Type[2];
        var WriteTypes=new Type[3];
        WriteTypes[0]=typeof(Writer).MakeByRefType();
        ReadTypes[0]=typeof(Reader).MakeByRefType();
        WriteTypes[1]=typeof(T);
        ReadTypes[1]=WriteTypes[2]=typeof(O);
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties=typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length=Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        {
            var WriteMethod=new DynamicMethod("Write",typeof(void),WriteTypes,typeof(Extension),true){InitLocals=false};
            var ReadMethod=new DynamicMethod("Read",typeof(T),ReadTypes,typeof(Extension),true){InitLocals=false};
            共通(WriteMethod.GetILGenerator(),ReadMethod.GetILGenerator());
            this.Write=(delegate_Write)WriteMethod.CreateDelegate(typeof(delegate_Write));
            this.Read=(delegate_Read)ReadMethod.CreateDelegate(typeof(delegate_Read));
        }
        {
            var AssemblyName=new AssemblyName{Name="Name"};
            var DynamicAssembly=AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
            var ModuleBuilder=DynamicAssembly.DefineDynamicModule("動的");
            var Disp_TypeBuilder=ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
            var Write=Disp_TypeBuilder.DefineMethod("Write",MethodAttributes.Static|MethodAttributes.Public,typeof(void),WriteTypes);
            Write.DefineParameter(1,ParameterAttributes.None,"writer");
            Write.DefineParameter(2,ParameterAttributes.None,"value");
            Write.DefineParameter(3,ParameterAttributes.None,"Resolver");
            var Read=Disp_TypeBuilder.DefineMethod("Read",MethodAttributes.Static|MethodAttributes.Public,typeof(T),ReadTypes);
            Read.DefineParameter(1,ParameterAttributes.None,"writer");
            Read.DefineParameter(2,ParameterAttributes.None,"Resolver");
            Read.InitLocals=false;
            共通(Write.GetILGenerator(),Read.GetILGenerator());
            Disp_TypeBuilder.CreateType();
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Environment.CurrentDirectory}\Utf8Json.DisplayClass,WriteRead.dll");
        }
        void 共通(ILGenerator WI,ILGenerator RI){
            if(Properties_Length>0){
                var index=0;
                while(true){
                    var Property=Properties[index];
                    Types1[0]=Property.PropertyType;
                    WI.Ldarg_0();//writer
                    WI.Ldstr(Property.Name);
                    WI.Call(Extension.WriteString);
                    WI.Ldarg_0();//writer
                    WI.Call(Extension.WriteNameSeparator);
                    WI.Ldarg_0();//writer
                    WI.Ldarg_1();//value
                    WI.Call(Property.GetMethod);//value.field
                    WI.Ldarg_2();//resolver
                    WI.Call(Extension.SerializeMethod.MakeGenericMethod(Types1));
                    RI.Ldarg_0();//reader
                    RI.Call(Extension.ReadString);//Nameを読む
                    RI.Pop();//Nameを捨てる
                    RI.Ldarg_0();//reader
                    RI.Call(Extension.ReadIsNameSeparatorWithVerify);//":"を読む
                    RI.Ldarg_0();//display reader
                    RI.Ldarg_1();//display reader resolver
                    RI.Call(Extension.DeserializeMethod.MakeGenericMethod(Types1));//display Deserialize(ref reader,resolver)
                    index++;
                    if(index==Properties_Length) break;
                    WI.Ldarg_0();
                    WI.Call(Extension.WriteValueSeparator);
                    RI.Ldarg_0();
                    RI.Call(Extension.ReadIsValueSeparatorWithVerify);
                }
            }
            WI.Ret();
            RI.Newobj(ctor);
            RI.Ret();
        }
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginObject();
        this.Write(ref writer,value,Resolver);
        writer.WriteEndObject();
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return default!;
        reader.ReadIsBeginObjectWithVerify();
        var result=this.Read(ref reader,Resolver);
        reader.ReadIsEndObjectWithVerify();
        return result;
    }
}
