using System.Collections.Generic;
using System.Buffers;
using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;
using MemoryPack.Formatters;
using static LinqDB.Reflection.Common;
using System.IO;
using LinqDB.Serializers.MemoryPack.Formatters;

// ReSharper disable InconsistentNaming
namespace LinqDB.Serializers;
public static class MemoryPackCustomSerializer{
    public static readonly MethodInfo Register=M(()=>MemoryPackFormatterProvider.Register(new Anonymous<int>()));
#pragma warning disable CS8618
    internal static readonly Binary Binary=new();
    internal static readonly Block Block=new();
    internal static readonly CatchBlock CatchBlock=new();
    internal static readonly Conditional Conditional=new();
    internal static readonly Constant Constant=new();
    internal static readonly Constructor Constructor=new();
    internal static readonly Default Default=new();
    internal static readonly ElementInit ElementInit=new();
    internal static readonly Expression Expression=new();
    internal static readonly Field Field=new();
    internal static readonly Goto Goto=new();
    internal static readonly Index Index=new();
    internal static readonly Invocation Invocation=new();
    internal static readonly Label Label=new();
    internal static readonly LabelTarget LabelTarget=new();
    internal static readonly Lambda Lambda=new();
    internal static readonly ListInit ListInit=new();
    internal static readonly Loop Loop=new();
    internal static readonly Member Member=new();
    internal static readonly MemberAccess MemberAccess=new();
    internal static readonly MemberBinding MemberBinding=new();
    internal static readonly MemberInit MemberInit=new();
    internal static readonly Method Method=new();
    internal static readonly MethodCall MethodCall=new();
    internal static readonly New New=new();
    internal static readonly NewArray NewArray=new();
    internal static readonly Object Object=new();
    internal static readonly Parameter Parameter=new();
    internal static readonly Property Property=new();
    internal static readonly Switch Switch=new();
    internal static readonly SwitchCase SwitchCase=new();
    internal static readonly Try Try=new();
    internal static readonly Type Type=new();
    internal static readonly TypeBinary TypeBinary=new();
    internal static readonly Unary Unary=new();
    //#pragma warning restore CS8618
    //internal readonly Action<dynamic> SetProvider;
    static MemoryPackCustomSerializer(){
        MemoryPackFormatterProvider.Register(Binary);
        MemoryPackFormatterProvider.Register(Block);
        MemoryPackFormatterProvider.Register(CatchBlock);
        MemoryPackFormatterProvider.Register(Conditional);
        MemoryPackFormatterProvider.Register(Constant);
        MemoryPackFormatterProvider.Register(Constructor);
        MemoryPackFormatterProvider.Register(Default);
        MemoryPackFormatterProvider.Register(ElementInit);
        MemoryPackFormatterProvider.Register(Expression);
        MemoryPackFormatterProvider.Register(Field);
        MemoryPackFormatterProvider.Register(Goto);
        MemoryPackFormatterProvider.Register(Index);
        MemoryPackFormatterProvider.Register(Invocation);
        MemoryPackFormatterProvider.Register(Label);
        MemoryPackFormatterProvider.Register(LabelTarget);
        MemoryPackFormatterProvider.Register(Lambda);
        MemoryPackFormatterProvider.Register(ListInit);
        MemoryPackFormatterProvider.Register(Loop);
        MemoryPackFormatterProvider.Register(Member);
        MemoryPackFormatterProvider.Register(MemberAccess);
        MemoryPackFormatterProvider.Register(MemberBinding);
        MemoryPackFormatterProvider.Register(MemberInit);
        MemoryPackFormatterProvider.Register(Method);
        MemoryPackFormatterProvider.Register(MethodCall);
        MemoryPackFormatterProvider.Register(New);
        MemoryPackFormatterProvider.Register(NewArray);
        MemoryPackFormatterProvider.Register(Object);
        MemoryPackFormatterProvider.Register(Parameter);
        MemoryPackFormatterProvider.Register(Property);
        MemoryPackFormatterProvider.Register(Switch);
        MemoryPackFormatterProvider.Register(SwitchCase);
        MemoryPackFormatterProvider.Register(Try);
        MemoryPackFormatterProvider.Register(Type);
        MemoryPackFormatterProvider.Register(TypeBinary);
        MemoryPackFormatterProvider.Register(Unary);
    }
    internal static readonly List<Expressions.ParameterExpression> ListParameter=new();
    internal static readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
    internal static readonly List<Expressions.LabelTarget> ListLabelTarget=new();
    internal static readonly Dictionary<System.Type,int> DictionaryTypeIndex=new();
    internal static readonly List<System.Type> Types=new();
    internal static readonly Dictionary<System.Type,MethodInfo[]> TypeMethods=new();
    internal static readonly Dictionary<System.Type,FieldInfo[]> TypeFields=new();
    internal static readonly Dictionary<System.Type,PropertyInfo[]> TypeProperties=new();
    //private readonly object[] objects2=new object[1];
    //public void 変数Register(System.Type Type){
    //    if(Type.IsGenericType){
    //        if(Type.IsAnonymous()){
    //            var FormatterType=typeof(Anonymous<>).MakeGenericType(Type);
    //            var Register=CustomSerializerMemoryPack.Register.MakeGenericMethod(Type);
    //            var objects2=this.objects2;
    //            objects2[0]=Activator.CreateInstance(FormatterType)!;
    //            Register.Invoke(null,objects2);
    //            //Register.Invoke(null,Array.Empty<object>());
    //        }
    //        foreach(var GenericArgument in Type.GetGenericArguments())this.変数Register(GenericArgument);
    //    }
    //}
    public static byte[] Serialize<T>(T value){
        Clear();
        return MemoryPackSerializer.Serialize(value);
    }
    public static void Serialize<T>(Stream stream,T? value){
        Clear();
        var Task=MemoryPackSerializer.SerializeAsync(stream,value).AsTask();
        Task.Wait();
    }
    public static T Deserialize<T>(byte[] bytes){
        Clear();
        return MemoryPackSerializer.Deserialize<T>(bytes)!;
    }
    public static T Deserialize<T>(Stream stream){
        Clear();
        //var e=MemoryPackSerializer.DeserializeAsync<T>(stream);
        var Task=MemoryPackSerializer.DeserializeAsync<T>(stream).AsTask();
        Task.Wait();
        return Task.Result!;
    }
    private static void Clear(){
        ListParameter.Clear();
        Dictionary_LabelTarget_int.Clear();
        ListLabelTarget.Clear();
        DictionaryTypeIndex.Clear();
        Types.Clear();
        TypeMethods.Clear();
        TypeFields.Clear();
        TypeProperties.Clear();
    }
    public static void WriteBoolean<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,bool value)
        where TBufferWriter:IBufferWriter<byte> =>
        writer.WriteVarInt((byte)(value?1:0));
    public static bool ReadBoolean(ref MemoryPackReader reader)=>reader.ReadVarIntByte()!=0;
    private static readonly ReadOnlyCollectionFormatter<Expressions.Expression> Expressions=new();
    public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,
        ReadOnlyCollection<Expressions.Expression>? value) where TBufferWriter:IBufferWriter<byte> =>
        Expressions.Serialize(ref writer,ref value!);
    //public Expressions.Expression[] DeserializeExpressions(ref MemoryPackReader reader){
    //    Expressions.Expression[] value=default!;
    //    reader.ReadArray(ref value!);
    //    return value;
    //}
    //private static readonly ReadOnlyCollectionFormatter<ParameterExpression>Parameters=new();
    //public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<ParameterExpression>? value) where TBufferWriter : IBufferWriter<byte> =>
    //    Parameters.Serialize(ref writer,ref value!);
    //public Expressions.ParameterExpression[] DeserializeParameters(ref MemoryPackReader reader){
    //    Expressions.ParameterExpression[] value=default!;
    //    reader.ReadArray(ref value!);
    //    return value;
    //}
    private static readonly ReadOnlyCollectionFormatter<Expressions.SwitchCase> SwitchCases=new();
    public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,
        ReadOnlyCollection<Expressions.SwitchCase>? value) where TBufferWriter:IBufferWriter<byte> =>
        SwitchCases.Serialize(ref writer,ref value!);
    public static readonly ReadOnlyCollectionFormatter<Expressions.MemberBinding> MemberBindings=new();
    public static readonly ReadOnlyCollectionFormatter<Expressions.CatchBlock> CatchBlocks=new();
    public static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit> ElementInits=new();
    public static void Serialize宣言Parameters<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,
        ReadOnlyCollection<Expressions.ParameterExpression>? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteVarInt(value!.Count);
        foreach(var Parameter in value){
            writer.WriteString(Parameter.Name);
            Type.Serialize(ref writer,Parameter.Type);
        }
    }
    public static Expressions.ParameterExpression[] Deserialize宣言Parameters(ref MemoryPackReader reader){
        var Count=reader.ReadVarIntInt32();
        var Parameters=new Expressions.ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var name=reader.ReadString();
            var type=Type.DeserializeType(ref reader);
            Parameters[a]=System.Linq.Expressions.Expression.Parameter(type,name);
        }
        return Parameters;
    }
}