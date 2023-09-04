//#define 匿名型にキーを入れる
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Lokad.ILPack;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
//public class DisplayClassJsonFormatter{
//    protected static readonly MethodInfo WriteBeginArray=typeof(JsonWriter).GetMethod("WriteBeginArray")!;
//    protected static readonly MethodInfo WriteValueSeparator=typeof(JsonWriter).GetMethod("WriteValueSeparator")!;
//    protected static readonly MethodInfo WriteValWriteEndArrayueSeparator=typeof(JsonWriter).GetMethod("WriteEndArray")!;
//    protected static readonly MethodInfo ReadIsBeginArrayWithVerify=typeof(JsonReader).GetMethod("ReadIsBeginArrayWithVerify")!;
//    protected static readonly MethodInfo ReadIsValueSeparatorWithVerify=typeof(JsonReader).GetMethod("ReadIsValueSeparatorWithVerify")!;
//    protected static readonly MethodInfo ReadIsEndArrayWithVerify=typeof(JsonReader).GetMethod("ReadIsEndArrayWithVerify")!;
//    protected static void WriteType(ref JsonWriter writer,Type value)=>writer.WriteString(value.AssemblyQualifiedName);
//    protected static Type ReadType(ref JsonReader reader)=>Type.GetType(reader.ReadString())!;
//    protected static void WriteType(ref MessagePackWriter writer,Type value)=>writer.Write(value.AssemblyQualifiedName);
//    protected static Type ReadType(ref MessagePackReader reader)=>Type.GetType(reader.ReadString())!;
//    protected static void Serialize<T>(ref JsonWriter writer,T value,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
//    protected static readonly MethodInfo MethodSerialize=typeof(CommonJsonFormatter).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
//    protected static T Deserialize<T>(ref JsonReader reader,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
//    protected static readonly MethodInfo MethodDeserialize=typeof(CommonJsonFormatter).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
//}
public class DisplayClassJsonFormatter<T>:CommonJsonFormatter,IJsonFormatter<T>{
    private delegate void delegate_Serialize(ref JsonWriter writer,T value,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Deserialize DelegateDeserialize;
    public DisplayClassJsonFormatter() {
        var Types1 = new Type[1];
        var Types2 = new Type[2];
        var Types3 = new Type[3];
        Types3[0]=typeof(JsonWriter).MakeByRefType();
        Types2[0]=typeof(JsonReader).MakeByRefType();
        Types3[1]=typeof(T);
        Types2[1]=Types3[2]=typeof(IJsonFormatterResolver);
        var Fields = typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
        var Fields_Length = Fields.Length;
        {
            var D0=new DynamicMethod("",typeof(void),Types3,typeof(CommonJsonFormatter),true){InitLocals=false};
            var D1=new DynamicMethod("",typeof(T),Types2,typeof(CommonJsonFormatter),true){InitLocals=false};
            var I0=D0.GetILGenerator();
            var I1=D1.GetILGenerator();
            共通(I0,I1);
            this.DelegateSerialize=(delegate_Serialize)D0.CreateDelegate(typeof(delegate_Serialize));
            this.DelegateDeserialize=(delegate_Deserialize)D1.CreateDelegate(typeof(delegate_Deserialize));
        }
        void 共通(ILGenerator I0,ILGenerator I1){
            I1.Emit(OpCodes.Newobj,typeof(T).GetConstructor(Type.EmptyTypes)!);
            var display=I1.DeclareLocal(typeof(T));
            I1.Emit(OpCodes.Stloc_0);
            var index=0;
            while(true){
                var Field=Fields[index];
                Types1[0]=Field.FieldType;
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldstr,Field.Name);
                I0.Emit(OpCodes.Call,WriteString);
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Call,WriteNameSeparator);
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldarg_1);//value
                I0.Emit(OpCodes.Ldfld,Field);//value.field
                I0.Emit(OpCodes.Ldarg_2);//resolver
                I0.Emit(OpCodes.Call,MethodSerialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Call,ReadString);//Nameを読む
                I1.Emit(OpCodes.Pop);            //Nameを捨てる
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Call,ReadIsNameSeparatorWithVerify);//":"を読む
                I1.Emit(OpCodes.Ldloc_0);//display
                I1.Emit(OpCodes.Ldarg_0);//display reader
                I1.Emit(OpCodes.Ldarg_1);//display reader resolver
                I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));//display Deserialize(ref reader,resolver)
                I1.Emit(OpCodes.Stfld,Field);//display.field=Deserialize(ref reader,resolver)
                index++;
                if(index==Fields_Length) break;
                I0.Emit(OpCodes.Ldarg_0);
                I0.Emit(OpCodes.Call,WriteValueSeparator);
                I1.Emit(OpCodes.Ldarg_0);
                I1.Emit(OpCodes.Call,ReadIsValueSeparatorWithVerify);
            }
            I0.Emit(OpCodes.Ret);
            I1.Emit(OpCodes.Ldloc_0);
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
        writer.WriteEndObject();
        //var Parameters = typeof(T).GetConstructors()[0].GetParameters();
        //var Parameters_Length = Parameters.Length;
        //writer.WriteBeginObject();
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
        //writer.WriteEndObject();
    }
    //private readonly object[] Objects2=new object[2];
    public T Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        if(reader.ReadIsNull())return default!;
        reader.ReadIsBeginObjectWithVerify();
        var result=this.DelegateDeserialize(ref reader, formatterResolver);
        //var ctor = typeof(T).GetConstructors()[0];
        //var Parameters = ctor.GetParameters();
        //var Parameters_Length = Parameters.Length;
        //var args=new object[Parameters_Length];
        //for(var a = 0;a<Parameters_Length;a++) {
        //    var Key = reader.ReadString();
        //    reader.ReadIsNameSeparatorWithVerify();
        //    Debug.Assert(Parameters[a].Name==Key);
        //    var Formatter=formatterResolver.GetFormatterDynamic(Parameters[a].ParameterType);
        //    var Deserialize = Formatter.GetType().GetMethod("Deserialize");
        //    Debug.Assert(Deserialize is not null);
        //    var Objects2 = this.Objects2;
        //    Objects2[0]=reader;
        //    Objects2[1]=formatterResolver;
        //    args[a]=Deserialize.Invoke(Formatter,Objects2);
        //    reader=(JsonReader)Objects2[0];
        //    if(a<Parameters_Length-1)
        //        reader.ReadIsValueSeparatorWithVerify();
        //}
        reader.ReadIsEndObjectWithVerify();
        return result;
        //return (T)ctor.Invoke(args);
    }
}
public class DisplayClassMessagePackFormatter<T>:AnonymousMessagePackFormatter,IMessagePackFormatter<T>{
    private delegate void delegate_Serialize(ref MessagePackWriter writer,T value,MessagePackSerializerOptions options);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options);
    private readonly delegate_Deserialize DelegateDeserialize;
    public DisplayClassMessagePackFormatter() {
        var Types1 = new Type[1];
        var Types2 = new Type[2];
        var Types3 = new Type[3];
        Types3[0]=typeof(MessagePackWriter).MakeByRefType();
        Types2[0]=typeof(MessagePackReader).MakeByRefType();
        Types3[1]=typeof(T);
        Types2[1]=Types3[2]=typeof(MessagePackSerializerOptions);
        var ctor=typeof(T).GetConstructors()[0];
        var Fields = typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
        var Fields_Length = Fields.Length;
        Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        {
            var Serialize=new DynamicMethod("Serialize",typeof(void),Types3,typeof(CommonJsonFormatter),true){InitLocals=false};
            var Deserialize=new DynamicMethod("Deserialize",typeof(T),Types2,typeof(CommonJsonFormatter),true){InitLocals=false};
            //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
            var I0=Serialize.GetILGenerator();
            var I1=Deserialize.GetILGenerator();
            共通(I0,I1);
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
            共通(Serialize.GetILGenerator(),Deserialize.GetILGenerator());
            Disp_TypeBuilder.CreateType();
            var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\MessagePackSerializer.dll");
        }
        void 共通(ILGenerator I0,ILGenerator I1){
            I0.Emit(OpCodes.Ldarg_0);//writer
            I0.Emit(OpCodes.Ldc_I4,Fields_Length);
            I0.Emit(OpCodes.Call,WriteArrayHeader);
            I1.Emit(OpCodes.Ldarg_0);//reader
            I1.Emit(OpCodes.Call,ReadArrayHeader);
            I1.Emit(OpCodes.Pop);
            I1.Emit(OpCodes.Newobj,ctor);
            I1.DeclareLocal(typeof(T));
            I1.Emit(OpCodes.Stloc_0);//変数=new T()
            var index=0;
            while(true){
                var Field=Fields[index];
                Types1[0]=Field.FieldType;
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldarg_1);//value
                I0.Emit(OpCodes.Ldfld,Field);//value.property
                I0.Emit(OpCodes.Ldarg_2);//options
                I0.Emit(OpCodes.Call,MethodSerialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldloc_0);//変数.
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Ldarg_1);//options
                I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Stfld,Field);//変数.field=Deserialize(ref reader,options)
                index++;
                if(index==Fields_Length) break;
            }
            I0.Emit(OpCodes.Ret);
            I1.Emit(OpCodes.Ldloc_0);
            I1.Emit(OpCodes.Ret);
        }
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
//        var Length=reader.ReadArrayHeader();
//        var args=new object[Length];
//        for(var a = 0;a<Length;a++) {
//            //var Key = reader.ReadString();
//            //Debug.Assert(Parameters[a].Name==Key);
//            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
//        }
//#endif
//        Debug.Assert(Length==Parameters.Length);
//        return (T)ctor.Invoke(args);
    }
}
