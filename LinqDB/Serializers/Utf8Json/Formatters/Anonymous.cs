//#define 匿名型にキーを入れる
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
public class Anonymous{
    protected static readonly MethodInfo WriteValueSeparator=typeof(Writer).GetMethod(nameof(Writer.WriteValueSeparator))!;
    protected static readonly MethodInfo WriteString=typeof(Writer).GetMethod(nameof(Writer.WriteString))!;
    protected static readonly MethodInfo WriteNameSeparator=typeof(Writer).GetMethod(nameof(Writer.WriteNameSeparator))!;
    protected static readonly MethodInfo ReadIsValueSeparatorWithVerify=typeof(Reader).GetMethod(nameof(Reader.ReadIsValueSeparatorWithVerify))!;
    protected static readonly MethodInfo ReadString=typeof(Reader).GetMethod(nameof(Reader.ReadString))!;
    protected static readonly MethodInfo ReadIsNameSeparatorWithVerify=typeof(Reader).GetMethod(nameof(Reader.ReadIsNameSeparatorWithVerify))!;
    protected static void Serialize<T>(ref Writer writer,T value,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
    protected static readonly MethodInfo MethodSerialize=typeof(Anonymous).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    protected static T Deserialize<T>(ref Reader reader,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
    protected static readonly MethodInfo MethodDeserialize=typeof(Anonymous).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
}
public class Anonymous<T>:Anonymous,IJsonFormatter<T>{
    public static readonly Anonymous<T> Instance=new();
    private delegate void delegate_Serialize(ref Writer writer,T value,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref Reader reader,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Deserialize DelegateDeserialize;
    public Anonymous() {
        var Types1 = new System.Type[1];
        var DeserializeTypes = new System.Type[2];
        var SerializeTypes = new System.Type[3];
        SerializeTypes[0]=typeof(Writer).MakeByRefType();
        DeserializeTypes[0]=typeof(Reader).MakeByRefType();
        SerializeTypes[1]=typeof(T);
        DeserializeTypes[1]=SerializeTypes[2]=typeof(IJsonFormatterResolver);
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length = Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        {
            var D0=new DynamicMethod("Serialize",typeof(void),SerializeTypes,typeof(Anonymous),true){InitLocals=false};
            var D1=new DynamicMethod("Deserialize ",typeof(T),DeserializeTypes,typeof(Anonymous),true){InitLocals=false};
            var I0=D0.GetILGenerator();
            var I1=D1.GetILGenerator();
            D0.InitLocals=false;
            D1.InitLocals=false;
            共通(I0,I1);
            this.DelegateSerialize=(delegate_Serialize)D0.CreateDelegate(typeof(delegate_Serialize));
            this.DelegateDeserialize=(delegate_Deserialize)D1.CreateDelegate(typeof(delegate_Deserialize));
        }
        //{
        //    var AssemblyName = new AssemblyName { Name="Name" };
        //    var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        //    var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
        //    var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
        //    var Serialize = Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),SerializeTypes);
        //    Serialize.DefineParameter(1,ParameterAttributes.None,"writer");
        //    Serialize.DefineParameter(2,ParameterAttributes.None,"value");
        //    Serialize.DefineParameter(3,ParameterAttributes.None,"resolver");
        //    Serialize.InitLocals=false;
        //    var Deserialize = Disp_TypeBuilder.DefineMethod($"Deserialize",MethodAttributes.Static|MethodAttributes.Public,typeof(T),DeserializeTypes);
        //    Deserialize.DefineParameter(1,ParameterAttributes.None,"writer");
        //    Deserialize.DefineParameter(2,ParameterAttributes.None,"resolver");
        //    Deserialize.InitLocals=false;
        //    共通(Serialize.GetILGenerator(),Deserialize.GetILGenerator());
        //    Disp_TypeBuilder.CreateType();
        //    var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
        //    new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\JsonSerializer.dll");
        //}
        void 共通(ILGenerator I0,ILGenerator I1){
            var index=0;
            while(true){
                var Property=Properties[index];
                Types1[0]=Property.PropertyType;
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldstr,Property.Name);
                I0.Emit(OpCodes.Call,WriteString);
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Call,WriteNameSeparator);
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldarg_1);//value
                I0.Emit(OpCodes.Call,Property.GetMethod);//value.property
                I0.Emit(OpCodes.Ldarg_2);//resolver
                I0.Emit(OpCodes.Call,MethodSerialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Call,ReadString);//Nameを読む
                I1.Emit(OpCodes.Pop);            //Nameを捨てる
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Call,ReadIsNameSeparatorWithVerify);//":"を読む
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Ldarg_1);//Resolver
                I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
                index++;
                if(index==Properties_Length) break;
                I0.Emit(OpCodes.Ldarg_0);
                I0.Emit(OpCodes.Call,WriteValueSeparator);
                I1.Emit(OpCodes.Ldarg_0);
                I1.Emit(OpCodes.Call,ReadIsValueSeparatorWithVerify);
            }
            I0.Emit(OpCodes.Ret);
            I1.Emit(OpCodes.Newobj,ctor);
            I1.Emit(OpCodes.Ret);
        }
    }
    //private readonly object[] Objects3=new object[3];
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver formatterResolver){
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginObject();
        this.DelegateSerialize(ref writer, value, formatterResolver);
        writer.WriteEndObject();
    }
    //private readonly object[] Objects2=new object[2];
    public T Deserialize(ref Reader reader,IJsonFormatterResolver formatterResolver){
        if(reader.ReadIsNull())return default!;
        reader.ReadIsBeginObjectWithVerify();
        var result=this.DelegateDeserialize(ref reader, formatterResolver);
        reader.ReadIsEndObjectWithVerify();
        //return (T)ctor.Invoke(args);
        return result;
    }
}
