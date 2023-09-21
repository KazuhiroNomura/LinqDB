using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using Writer = JsonWriter;
using Reader = JsonReader;
internal static class DisplayClass
{
    public static readonly MethodInfo WriteValueSeparator = typeof(Writer).GetMethod(nameof(Writer.WriteValueSeparator))!;
    public static readonly MethodInfo WriteString = typeof(Writer).GetMethod(nameof(Writer.WriteString))!;
    public static readonly MethodInfo WriteNameSeparator = typeof(Writer).GetMethod(nameof(Writer.WriteNameSeparator))!;
    public static readonly MethodInfo ReadIsValueSeparatorWithVerify = typeof(Reader).GetMethod(nameof(Reader.ReadIsValueSeparatorWithVerify))!;
    public static readonly MethodInfo ReadString = typeof(Reader).GetMethod(nameof(Reader.ReadString))!;
    public static readonly MethodInfo ReadIsNameSeparatorWithVerify = typeof(Reader).GetMethod(nameof(Reader.ReadIsNameSeparatorWithVerify))!;
    private static void Serialize<T>(ref Writer writer, T value, IJsonFormatterResolver Resolver) => Resolver.GetFormatter<T>().Serialize(ref writer, value, Resolver);
    public static readonly MethodInfo MethodSerialize = typeof(DisplayClass).GetMethod(nameof(Serialize), BindingFlags.Static|BindingFlags.NonPublic)!;
    private static T Deserialize<T>(ref Reader reader, IJsonFormatterResolver Resolver) => Resolver.GetFormatter<T>().Deserialize(ref reader, Resolver);
    public static readonly MethodInfo MethodDeserialize = typeof(DisplayClass).GetMethod(nameof(Deserialize), BindingFlags.Static|BindingFlags.NonPublic)!;
}
public class DisplayClass<T> : IJsonFormatter<T>
{
    public static readonly DisplayClass<T> Instance = new();


    private delegate void delegate_Serialize(ref Writer writer, T value, IJsonFormatterResolver formatterResolver);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref Reader reader, IJsonFormatterResolver formatterResolver);
    private readonly delegate_Deserialize DelegateDeserialize;
    public DisplayClass()
    {
        try
        {
            var Types1 = new Type[1];
            var Types2 = new Type[2];
            var Types3 = new Type[3];
            Types3[0]=typeof(Writer).MakeByRefType();
            Types2[0]=typeof(Reader).MakeByRefType();
            Types3[1]=typeof(T);
            Types2[1]=Types3[2]=typeof(IJsonFormatterResolver);
            var Fields = typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
            var Fields_Length = Fields.Length;
            {
                var D0 = new DynamicMethod("", typeof(void), Types3, typeof(DisplayClass), true) { InitLocals=false };
                var D1 = new DynamicMethod("", typeof(T), Types2, typeof(DisplayClass), true) { InitLocals=false };
                var I0 = D0.GetILGenerator();
                var I1 = D1.GetILGenerator();
                共通(I0, I1);
                this.DelegateSerialize=(delegate_Serialize)D0.CreateDelegate(typeof(delegate_Serialize));
                this.DelegateDeserialize=(delegate_Deserialize)D1.CreateDelegate(typeof(delegate_Deserialize));
            }
            void 共通(ILGenerator I0, ILGenerator I1)
            {
                I1.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes)!);
                I1.DeclareLocal(typeof(T));
                I1.Emit(OpCodes.Stloc_0);
                if (Fields_Length>0)
                {
                    var index = 0;
                    while (true)
                    {
                        var Field = Fields[index];
                        Types1[0]=Field.FieldType;
                        I0.Emit(OpCodes.Ldarg_0);//writer
                        I0.Emit(OpCodes.Ldstr, Field.Name);
                        I0.Emit(OpCodes.Call, DisplayClass.WriteString);
                        I0.Emit(OpCodes.Ldarg_0);//writer
                        I0.Emit(OpCodes.Call, DisplayClass.WriteNameSeparator);
                        I0.Emit(OpCodes.Ldarg_0);//writer
                        I0.Emit(OpCodes.Ldarg_1);//value
                        I0.Emit(OpCodes.Ldfld, Field);//value.field
                        I0.Emit(OpCodes.Ldarg_2);//resolver
                        I0.Emit(OpCodes.Call, DisplayClass.MethodSerialize.MakeGenericMethod(Types1));
                        I1.Emit(OpCodes.Ldarg_0);//reader
                        I1.Emit(OpCodes.Call, DisplayClass.ReadString);//Nameを読む
                        I1.Emit(OpCodes.Pop);//Nameを捨てる
                        I1.Emit(OpCodes.Ldarg_0);//reader
                        I1.Emit(OpCodes.Call, DisplayClass.ReadIsNameSeparatorWithVerify);//":"を読む
                        I1.Emit(OpCodes.Ldloc_0);//display
                        I1.Emit(OpCodes.Ldarg_0);//display reader
                        I1.Emit(OpCodes.Ldarg_1);//display reader resolver
                        I1.Emit(OpCodes.Call, DisplayClass.MethodDeserialize.MakeGenericMethod(Types1));//display Deserialize(ref reader,resolver)
                        I1.Emit(OpCodes.Stfld, Field);//display.field=Deserialize(ref reader,resolver)
                        index++;
                        if (index==Fields_Length) break;
                        I0.Emit(OpCodes.Ldarg_0);
                        I0.Emit(OpCodes.Call, DisplayClass.WriteValueSeparator);
                        I1.Emit(OpCodes.Ldarg_0);
                        I1.Emit(OpCodes.Call, DisplayClass.ReadIsValueSeparatorWithVerify);
                    }
                }
                I0.Emit(OpCodes.Ret);
                I1.Emit(OpCodes.Ldloc_0);
                I1.Emit(OpCodes.Ret);
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            throw;
        }
    }
    public void Serialize(ref Writer writer, T? value, IJsonFormatterResolver formatterResolver)
    {
        //      if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null, nameof(value)+" != null");
        writer.WriteBeginObject();
        this.DelegateSerialize(ref writer, value, formatterResolver);
        writer.WriteEndObject();
        //var Parameters = typeof(T).GetConstructors()[0].GetParameters();
        //var Parameters_Length = Parameters.Length;
        //writer.WriteBeginObject();
    }
    public T Deserialize(ref Reader reader, IJsonFormatterResolver formatterResolver)
    {
        reader.ReadIsBeginObjectWithVerify();
        var result = this.DelegateDeserialize(ref reader, formatterResolver);
        reader.ReadIsEndObjectWithVerify();
        return result;
    }
}
