//#define 匿名型にキーを入れる
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
internal static class DisplayClass {
    public static readonly MethodInfo WriteValueSeparator = typeof(Writer).GetMethod(nameof(Writer.WriteValueSeparator))!;
    public static readonly MethodInfo WriteString = typeof(Writer).GetMethod(nameof(Writer.WriteString))!;
    public static readonly MethodInfo WriteNameSeparator = typeof(Writer).GetMethod(nameof(Writer.WriteNameSeparator))!;
    public static readonly MethodInfo ReadIsValueSeparatorWithVerify = typeof(Reader).GetMethod(nameof(Reader.ReadIsValueSeparatorWithVerify))!;
    public static readonly MethodInfo ReadString = typeof(Reader).GetMethod(nameof(Reader.ReadString))!;
    public static readonly MethodInfo ReadIsNameSeparatorWithVerify = typeof(Reader).GetMethod(nameof(Reader.ReadIsNameSeparatorWithVerify))!;
    private static void Serialize<T>(ref Writer writer,T value,IJsonFormatterResolver Resolver) => Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
    public static readonly MethodInfo MethodSerialize = typeof(DisplayClass).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static T Deserialize<T>(ref Reader reader,IJsonFormatterResolver Resolver) => Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
    public static readonly MethodInfo MethodDeserialize = typeof(DisplayClass).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    //public static readonly Dictionary<System.Type,Delegate> DictionarySerialize = new();
}
public class DisplayClass<T>:IJsonFormatter<T>{
    public static readonly DisplayClass<T> Instance=new();
    private delegate void delegate_Serialize(ref Writer writer,T value,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref Reader reader,IJsonFormatterResolver formatterResolver);
    private readonly delegate_Deserialize DelegateDeserialize;
    public DisplayClass() {
        var Types1 = new System.Type[1];
        var Types2 = new System.Type[2];
        var Types3 = new System.Type[3];
        Types3[0]=typeof(Writer).MakeByRefType();
        Types2[0]=typeof(Reader).MakeByRefType();
        Types3[1]=typeof(T);
        Types2[1]=Types3[2]=typeof(IJsonFormatterResolver);
        var Fields = typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
        var Fields_Length = Fields.Length;
        {
            var D0=new DynamicMethod("",typeof(void),Types3,typeof(DisplayClass),true){InitLocals=false};
            var D1=new DynamicMethod("",typeof(T),Types2,typeof(DisplayClass),true){InitLocals=false};
            var I0=D0.GetILGenerator();
            var I1=D1.GetILGenerator();
            共通(I0,I1);
            this.DelegateSerialize=(delegate_Serialize)D0.CreateDelegate(typeof(delegate_Serialize));
            this.DelegateDeserialize=(delegate_Deserialize)D1.CreateDelegate(typeof(delegate_Deserialize));
        }
        void 共通(ILGenerator I0,ILGenerator I1){
            I1.Emit(OpCodes.Newobj,typeof(T).GetConstructor(System.Type.EmptyTypes)!);
            var display=I1.DeclareLocal(typeof(T));
            I1.Emit(OpCodes.Stloc_0);
            var index=0;
            while(true){
                var Field=Fields[index];
                Types1[0]=Field.FieldType;
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldstr,Field.Name);
                I0.Emit(OpCodes.Call,DisplayClass.WriteString);
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Call,DisplayClass.WriteNameSeparator);
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldarg_1);//value
                I0.Emit(OpCodes.Ldfld,Field);//value.field
                I0.Emit(OpCodes.Ldarg_2);//resolver
                I0.Emit(OpCodes.Call,DisplayClass.MethodSerialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Call,DisplayClass.ReadString);//Nameを読む
                I1.Emit(OpCodes.Pop);            //Nameを捨てる
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Call,DisplayClass.ReadIsNameSeparatorWithVerify);//":"を読む
                I1.Emit(OpCodes.Ldloc_0);//display
                I1.Emit(OpCodes.Ldarg_0);//display reader
                I1.Emit(OpCodes.Ldarg_1);//display reader resolver
                I1.Emit(OpCodes.Call,DisplayClass.MethodDeserialize.MakeGenericMethod(Types1));//display Deserialize(ref reader,resolver)
                I1.Emit(OpCodes.Stfld,Field);//display.field=Deserialize(ref reader,resolver)
                index++;
                if(index==Fields_Length) break;
                I0.Emit(OpCodes.Ldarg_0);
                I0.Emit(OpCodes.Call,DisplayClass.WriteValueSeparator);
                I1.Emit(OpCodes.Ldarg_0);
                I1.Emit(OpCodes.Call,DisplayClass.ReadIsValueSeparatorWithVerify);
            }
            I0.Emit(OpCodes.Ret);
            I1.Emit(OpCodes.Ldloc_0);
            I1.Emit(OpCodes.Ret);
        }
    }
    //private readonly object[] Objects3=new object[3];
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver formatterResolver){
  //      if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
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
        //    //writer.SerializeReadOnlyCollection(Value,formatterResolver);
        //}
        //writer.WriteEndObject();
    }
    //private readonly object[] Objects2=new object[2];
    public T Deserialize(ref Reader reader,IJsonFormatterResolver formatterResolver){
        //if(reader.ReadIsNull())return default!;
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
