//#define 匿名型にキーを入れる
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using LinqDB.Databases;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using Utf8Json.Formatters;
using AssemblyGenerator=Lokad.ILPack.AssemblyGenerator;
namespace LinqDB.Serializers.Formatters;
public static class Common2{
    public static(DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[]Properties) 初期化<T,TWriter,TResolver>(){
        var Types2 = new Type[2];
        var Types3 = new Type[3];
        Types3[0]=Types2[0]=typeof(TWriter).MakeByRefType();
        Types3[1]=typeof(T);
        Types2[1]=Types3[2]=typeof(TResolver);
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length = Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        var D0 = new DynamicMethod("",typeof(void),Types3,typeof(Common2),true) { InitLocals=false };
        var D1 = new DynamicMethod("",typeof(T),Types2,typeof(Common2),true) { InitLocals=false };
        return(D0,D1,ctor,Properties);
    }
}
public class AnonymousJsonFormatter{
    protected static readonly MethodInfo WriteBeginArray=typeof(JsonWriter).GetMethod(nameof(JsonWriter.WriteBeginArray))!;
    protected static readonly MethodInfo WriteValueSeparator=typeof(JsonWriter).GetMethod(nameof(JsonWriter.WriteValueSeparator))!;
    protected static readonly MethodInfo WriteValWriteEndArrayueSeparator=typeof(JsonWriter).GetMethod(nameof(JsonWriter.WriteEndArray))!;
    protected static readonly MethodInfo WriteBeginObject=typeof(JsonWriter).GetMethod(nameof(JsonWriter.WriteBeginObject))!;
    protected static readonly MethodInfo WriWriteStringteBeginObject=typeof(JsonWriter).GetMethod(nameof(JsonWriter.WriteString))!;
    protected static readonly MethodInfo WriteString=typeof(JsonWriter).GetMethod(nameof(JsonWriter.WriteString))!;
    protected static readonly MethodInfo WriteNameSeparator=typeof(JsonWriter).GetMethod(nameof(JsonWriter.WriteNameSeparator))!;
    protected static readonly MethodInfo ReadIsBeginArrayWithVerify=typeof(JsonReader).GetMethod(nameof(JsonReader.ReadIsBeginArrayWithVerify))!;
    protected static readonly MethodInfo ReadIsValueSeparatorWithVerify=typeof(JsonReader).GetMethod(nameof(JsonReader.ReadIsValueSeparatorWithVerify))!;
    protected static readonly MethodInfo ReadIsEndArrayWithVerify=typeof(JsonReader).GetMethod(nameof(JsonReader.ReadIsEndArrayWithVerify))!;
    protected static readonly MethodInfo ReadIsBeginObjectWithVerify=typeof(JsonReader).GetMethod(nameof(JsonReader.ReadIsBeginObjectWithVerify))!;
    protected static readonly MethodInfo ReadString=typeof(JsonReader).GetMethod(nameof(JsonReader.ReadString))!;
    protected static readonly MethodInfo ReadIsNameSeparatorWithVerify=typeof(JsonReader).GetMethod(nameof(JsonReader.ReadIsNameSeparatorWithVerify))!;
    protected static void WriteType(ref JsonWriter writer,Type value)=>writer.WriteString(value.AssemblyQualifiedName);
    protected static Type ReadType(ref JsonReader reader)=>Type.GetType(reader.ReadString())!;
    protected static void WriteType(ref MessagePackWriter writer,Type value)=>writer.Write(value.AssemblyQualifiedName);
    protected static Type ReadType(ref MessagePackReader reader)=>Type.GetType(reader.ReadString())!;
    protected static void Serialize<T>(ref JsonWriter writer,T value,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
    protected static readonly MethodInfo MethodSerialize=typeof(AnonymousJsonFormatter).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    protected static T Deserialize<T>(ref JsonReader reader,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
    protected static readonly MethodInfo MethodDeserialize=typeof(AnonymousJsonFormatter).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
}
public class AnonymousJsonFormatter<T>:AnonymousJsonFormatter,IJsonFormatter<T>{
    private delegate void delegate_Serialize(ref JsonWriter writer,T value,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Deserialize DelegateDeserialize;
    public AnonymousJsonFormatter() {
        var Types1 = new Type[1];
        var Types2 = new Type[2];
        var Types3 = new Type[3];
        Types3[0]=typeof(JsonWriter).MakeByRefType();
        Types2[0]=typeof(JsonReader).MakeByRefType();
        Types3[1]=typeof(T);
        Types2[1]=Types3[2]=typeof(IJsonFormatterResolver);
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length = Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        {
            var D0=new DynamicMethod("Serialize",typeof(void),Types3,typeof(Common2),true){InitLocals=false};
            var D1=new DynamicMethod("Deserialize ",typeof(T),Types2,typeof(Common2),true){InitLocals=false};
            //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
            var I0=D0.GetILGenerator();
            var I1=D1.GetILGenerator();
            NewFunction(I0,I1);
            this.DelegateSerialize=(delegate_Serialize)D0.CreateDelegate(typeof(delegate_Serialize));
            this.DelegateDeserialize=(delegate_Deserialize)D1.CreateDelegate(typeof(delegate_Deserialize));
        }
        {
            var AssemblyName = new AssemblyName { Name="Name" };
            var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
            var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
            var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
            var Serialize = Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),Types3);
            Serialize.DefineParameter(1,ParameterAttributes.None,"writer");
            Serialize.DefineParameter(2,ParameterAttributes.None,"value");
            Serialize.DefineParameter(3,ParameterAttributes.None,"resolver");
            var Deserialize = Disp_TypeBuilder.DefineMethod($"Deserialize",MethodAttributes.Static|MethodAttributes.Public,typeof(T),Types2);
            Deserialize.DefineParameter(1,ParameterAttributes.None,"writer");
            Deserialize.DefineParameter(2,ParameterAttributes.None,"resolver");
            Deserialize.InitLocals=false;
            NewFunction(Serialize.GetILGenerator(),Deserialize.GetILGenerator());
            Disp_TypeBuilder.CreateType();
            var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\JsonSerializer.dll");
        }
        void NewFunction(ILGenerator I0,ILGenerator I1){
            //I0.Emit(OpCodes.Ldarg_0);
            //I0.Emit(OpCodes.Call,WriteBeginArray);
            //I1.Emit(OpCodes.Ldarg_0);
            //I1.Emit(OpCodes.Call,ReadIsBeginArrayWithVerify);
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
                I1.Emit(OpCodes.Call,ReadString);
                I1.Emit(OpCodes.Pop);
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Call,ReadIsNameSeparatorWithVerify);
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
            //I0.Emit(OpCodes.Call,WriteValWriteEndArrayueSeparator);
            I0.Emit(OpCodes.Ret);
            //I1.Emit(OpCodes.Call,ReadIsEndArrayWithVerify);
            I1.Emit(OpCodes.Newobj,ctor);
            I1.Emit(OpCodes.Ret);
        }
    }
    //private readonly object[] Objects3=new object[3];
    public void Serialize(ref JsonWriter writer,T? value,IJsonFormatterResolver formatterResolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginObject();
        this.DelegateSerialize(ref writer, value, formatterResolver);




        //var Parameters = typeof(T).GetConstructors()[0].GetParameters();
        //var Parameters_Length = Parameters.Length;
        //var Objects3=this.Objects3;
        //Objects3[2]=formatterResolver;
        //for(var a = 0;a<Parameters_Length;a++) {
        //    var Parameter = Parameters[a];
        //    var Key = Parameter.Name;
        //    var Value=typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>());
        //    writer.WriteString(Key);
        //    writer.WriteNameSeparator();
        //    var Formatter=formatterResolver.GetFormatterDynamic(Parameter.ParameterType);
        //    var Serialize = Formatter.GetType().GetMethod("Serialize");
        //    Debug.Assert(Serialize is not null);
        //    Objects3[0]=writer;
        //    Objects3[1]=Value;
        //    Serialize.Invoke(Formatter,Objects3);
        //    writer=(JsonWriter)Objects3[0];
        //    if(a<Parameters_Length-1)
        //        writer.WriteValueSeparator();
        //    //Serialize_T(ref writer,Value,formatterResolver);
        //}
        writer.WriteEndObject();
    }
    //private readonly object[] Objects2=new object[2];
    public T Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        if(reader.ReadIsNull())return default!;
        reader.ReadIsBeginObjectWithVerify();
        var result=this.DelegateDeserialize(ref reader, formatterResolver);
        reader.ReadIsEndObjectWithVerify();
        //return (T)ctor.Invoke(args);
        return result;
    }
}
public class AnonymousMessagePackFormatter{
    protected static void WriteType(ref MessagePackWriter writer,Type value)=>writer.Write(value.AssemblyQualifiedName);
    protected static Type ReadType(ref MessagePackReader reader)=>Type.GetType(reader.ReadString())!;
    private static void Serialize<T>(ref MessagePackWriter writer,T value,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Serialize(ref writer,value,options);
    protected static readonly MethodInfo MethodSerialize=typeof(AnonymousMessagePackFormatter).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static T Deserialize<T>(ref MessagePackReader reader,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Deserialize(ref reader,options);
    protected static readonly MethodInfo MethodDeserialize=typeof(AnonymousMessagePackFormatter).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    //protected static readonly MethodInfo WriteMapHeader=typeof(MessagePackWriter).GetMethod(nameof(MessagePackWriter.WriteMapHeader),new []{typeof(int)})!;
    //protected static readonly MethodInfo ReadMapHeader=typeof(MessagePackReader).GetMethod(nameof(MessagePackReader.ReadMapHeader))!;
    protected static readonly MethodInfo WriteArrayHeader=typeof(MessagePackWriter).GetMethod(nameof(MessagePackWriter.WriteArrayHeader),new []{typeof(int)})!;
    protected static readonly MethodInfo ReadArrayHeader=typeof(MessagePackReader).GetMethod(nameof(MessagePackReader.ReadArrayHeader))!;
}
public class AnonymousMessagePackFormatter<T>:AnonymousMessagePackFormatter,IMessagePackFormatter<T>{
    private delegate void delegate_Serialize(ref MessagePackWriter writer,T value,MessagePackSerializerOptions options);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options);
    private readonly delegate_Deserialize DelegateDeserialize;
    public AnonymousMessagePackFormatter() {
        var Types1 = new Type[1];
        var Types2 = new Type[2];
        var Types3 = new Type[3];
        Types3[0]=typeof(MessagePackWriter).MakeByRefType();
        Types2[0]=typeof(MessagePackReader).MakeByRefType();
        Types3[1]=typeof(T);
        Types2[1]=Types3[2]=typeof(MessagePackSerializerOptions);
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length = Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        {
            var Serialize=new DynamicMethod("Serialize",typeof(void),Types3,typeof(Common2),true){InitLocals=false};
            var Deserialize=new DynamicMethod("Deserialize",typeof(T),Types2,typeof(Common2),true){InitLocals=false};
            //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
            var I0=Serialize.GetILGenerator();
            var I1=Deserialize.GetILGenerator();
            NewFunction(I0,I1);
            this.DelegateSerialize=(delegate_Serialize)Serialize.CreateDelegate(typeof(delegate_Serialize));
            this.DelegateDeserialize=(delegate_Deserialize)Deserialize.CreateDelegate(typeof(delegate_Deserialize));
        }
        {
            var AssemblyName = new AssemblyName { Name="Name" };
            var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
            var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
            var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
            var Serialize = Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),Types3);
            Serialize.DefineParameter(1,ParameterAttributes.None,"writer");
            Serialize.DefineParameter(2,ParameterAttributes.None,"value");
            Serialize.DefineParameter(3,ParameterAttributes.None,"options");
            var Deserialize = Disp_TypeBuilder.DefineMethod($"Deserialize",MethodAttributes.Static|MethodAttributes.Public,typeof(T),Types2);
            Deserialize.DefineParameter(1,ParameterAttributes.None,"writer");
            Deserialize.DefineParameter(2,ParameterAttributes.None,"options");
            Deserialize.InitLocals=false;
            NewFunction(Serialize.GetILGenerator(),Deserialize.GetILGenerator());
            Disp_TypeBuilder.CreateType();
            var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\MessagePackSerializer.dll");
        }


        void NewFunction(ILGenerator I0,ILGenerator I1){
            I0.Emit(OpCodes.Ldarg_0);//writer
            I0.Emit(OpCodes.Ldc_I4,Properties_Length);
            I0.Emit(OpCodes.Call,WriteArrayHeader);
            I1.Emit(OpCodes.Ldarg_0);//reader
            I1.Emit(OpCodes.Call,ReadArrayHeader);
            I1.Emit(OpCodes.Pop);
//        writer.WriteMapHeader(Parameters_Length);
            var index=0;
//        var Length=reader.ReadMapHeader();
            while(true){
                var Property=Properties[index];
                Types1[0]=Property.PropertyType;
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldarg_1);//value
                Debug.Assert(!Property.GetMethod.IsVirtual);
                I0.Emit(OpCodes.Call,Property.GetMethod);//value.property
                I0.Emit(OpCodes.Ldarg_2);//options
                I0.Emit(OpCodes.Call,MethodSerialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Ldarg_1);//options
                I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
                index++;
                if(index==Properties_Length) break;
            }
            I0.Emit(OpCodes.Ret);
            I1.Emit(OpCodes.Newobj,ctor);
            I1.Emit(OpCodes.Ret);
        }
        //150
    }
    private class Container2 : global::LinqDB.Databases.Container<Container2>
    {
    }
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions options){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.DelegateSerialize(ref writer, value,options);
//        var Parameters = typeof(T).GetConstructors()[0].GetParameters();
//        var Parameters_Length = Parameters.Length;
//#if 匿名型にキーを入れる
//        writer.WriteMapHeader(Parameters_Length);
//        for(var a = 0;a<Parameters_Length;a++) {
//            var Parameter = Parameters[a];
//            var Key = Parameter.Name;
//            writer.Write(Key);
//            SerializerConfiguration.DynamicSerialize(options.Resolver.GetFormatterDynamic(Parameter.ParameterType),ref writer,typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>()),options);
//        }
//#else
//        writer.WriteArrayHeader(Parameters_Length);
//        for(var a = 0;a<Parameters_Length;a++) {
//            var Parameter = Parameters[a];
//            var Key = Parameter.Name;
//            SerializerConfiguration.DynamicSerialize(options.Resolver.GetFormatterDynamic(Parameter.ParameterType),ref writer,typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>()),options);
//        }
//#endif
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return default!;
        return this.DelegateDeserialize(ref reader,options);
//        var ctor = typeof(T).GetConstructors()[0];
//        var Parameters = ctor.GetParameters();
//#if 匿名型にキーを入れる
//        var Length=reader.ReadMapHeader();
//        var args=new object[Length];
//        for(var a = 0;a<Length;a++) {
//            var Key = reader.ReadString();
//            Debug.Assert(Parameters[a].Name==Key);
//            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
//        }
//#else
//        var Length = reader.ReadArrayHeader();
//        var args = new object[Length];
//        for(var a = 0;a<Length;a++) {
//            //var Key = reader.ReadString();
//            //Debug.Assert(Parameters[a].Name==Key);
//            //options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType).
//            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
//        }
//#endif
//        Debug.Assert(Length==Parameters.Length);
//        return (T)ctor.Invoke(args);
    }
}
