using System;
using System.Collections.Generic;
using System.Buffers;
using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;
using MemoryPack.Formatters;
using static LinqDB.Reflection.Common;
using System.IO;
using LinqDB.Helpers;
using LinqDB.Serializers.MemoryPack.Formatters;
using Index=LinqDB.Serializers.MemoryPack.Formatters.Index;
using Object=LinqDB.Serializers.MemoryPack.Formatters.Object;
using Type=LinqDB.Serializers.MemoryPack.Formatters.Type;

// ReSharper disable InconsistentNaming
namespace LinqDB.Serializers.MemoryPack;
public class MemoryPackCustomSerializer{
    public static readonly MemoryPackCustomSerializer Instance=new();
    public static readonly MethodInfo Register=M(()=>MemoryPackFormatterProvider.Register(new Anonymous<int>()));
#pragma warning disable CS8618
    //internal readonly Binary Binary=new();
    //internal readonly Block Block=new();
    //internal readonly CatchBlock CatchBlock=new();
    //internal readonly Conditional Conditional=new();
    //internal readonly Constant Constant=new();
    //internal readonly Constructor Constructor=new();
    //internal readonly Default Default=new();
    //internal readonly ElementInit ElementInit=new();
    //internal readonly Expression Expression=new();
    //internal readonly Field Field=new();
    //internal readonly Goto Goto=new();
    //internal readonly Index Index=new();
    //internal readonly Invocation Invocation=new();
    //internal readonly Label Label=new();
    //internal readonly LabelTarget LabelTarget=new();
    //internal readonly Lambda Lambda=new();
    //internal readonly ListInit ListInit=new();
    //internal readonly Loop Loop=new();
    //internal readonly Member Member=new();
    //internal readonly MemberAccess MemberAccess=new();
    //internal readonly MemberBinding MemberBinding=new();
    //internal readonly MemberInit MemberInit=new();
    //internal readonly Method Method=new();
    //internal readonly MethodCall MethodCall=new();
    //internal readonly New New=new();
    //internal readonly NewArray NewArray=new();
    //internal readonly Object Object=new();
    //internal readonly Parameter Parameter=new();
    //internal readonly Property Property=new();
    //internal readonly Switch Switch=new();
    //internal readonly SwitchCase SwitchCase=new();
    //internal readonly Try Try=new();
    //internal readonly Type Type=new();
    //internal readonly TypeBinary TypeBinary=new();
    //internal readonly Unary Unary=new();
    //#pragma warning restore CS8618
    //internal readonly Action<dynamic> SetProvider;
    private MemoryPackCustomSerializer(){
        MemoryPackFormatterProvider.Register(Binary.Instance);
        MemoryPackFormatterProvider.Register(Block.Instance);
        MemoryPackFormatterProvider.Register(CatchBlock.Instance);
        MemoryPackFormatterProvider.Register(Conditional.Instance);
        MemoryPackFormatterProvider.Register(Constant.Instance);
        MemoryPackFormatterProvider.Register(Constructor.Instance);
        MemoryPackFormatterProvider.Register(Default.Instance);
        MemoryPackFormatterProvider.Register(ElementInit.Instance);
        MemoryPackFormatterProvider.Register(Event.Instance);
        MemoryPackFormatterProvider.Register(Expression.Instance);
        MemoryPackFormatterProvider.Register(Field.Instance);
        MemoryPackFormatterProvider.Register(Goto.Instance);
        MemoryPackFormatterProvider.Register(Index.Instance);
        MemoryPackFormatterProvider.Register(Invocation.Instance);
        MemoryPackFormatterProvider.Register(Label.Instance);
        MemoryPackFormatterProvider.Register(LabelTarget.Instance);
        MemoryPackFormatterProvider.Register(Lambda.Instance);
        MemoryPackFormatterProvider.Register(ListInit.Instance);
        MemoryPackFormatterProvider.Register(Loop.Instance);
        MemoryPackFormatterProvider.Register(Member.Instance);
        MemoryPackFormatterProvider.Register(MemberAccess.Instance);
        MemoryPackFormatterProvider.Register(MemberBinding.Instance);
        MemoryPackFormatterProvider.Register(MemberInit.Instance);
        MemoryPackFormatterProvider.Register(Method.Instance);
        MemoryPackFormatterProvider.Register(MethodCall.Instance);
        MemoryPackFormatterProvider.Register(New.Instance);
        MemoryPackFormatterProvider.Register(NewArray.Instance);
        MemoryPackFormatterProvider.Register(Object.Instance);
        MemoryPackFormatterProvider.Register(Parameter.Instance);
        MemoryPackFormatterProvider.Register(Property.Instance);
        MemoryPackFormatterProvider.Register(Switch.Instance);
        MemoryPackFormatterProvider.Register(SwitchCase.Instance);
        MemoryPackFormatterProvider.Register(Try.Instance);
        MemoryPackFormatterProvider.Register(Type.Instance);
        MemoryPackFormatterProvider.Register(TypeBinary.Instance);
        MemoryPackFormatterProvider.Register(Unary.Instance);
    }
    internal readonly List<Expressions.ParameterExpression> ListParameter=new();
    internal readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
    internal readonly List<Expressions.LabelTarget> ListLabelTarget=new();
    internal readonly Dictionary<System.Type,int> DictionaryTypeIndex=new();
    internal readonly List<System.Type> Types=new();
    internal readonly Dictionary<System.Type,MemberInfo[]> TypeMembers=new();
    internal readonly Dictionary<System.Type,ConstructorInfo[]> TypeConstructors=new();
    internal readonly Dictionary<System.Type,MethodInfo[]> TypeMethods=new();
    internal readonly Dictionary<System.Type,FieldInfo[]> TypeFields=new();
    internal readonly Dictionary<System.Type,PropertyInfo[]> TypeProperties=new();
    internal readonly Dictionary<System.Type,EventInfo[]> TypeEvents=new();
     private void Clear(){
        this.ListParameter.Clear();
        this.Dictionary_LabelTarget_int.Clear();
        this.ListLabelTarget.Clear();
        this.DictionaryTypeIndex.Clear();
        this.Types.Clear();
        this.TypeMembers.Clear();
        this.TypeConstructors.Clear();
        this.TypeMethods.Clear();
        this.TypeFields.Clear();
        this.TypeProperties.Clear();
        this.TypeEvents.Clear();
    }
    private static readonly object[] objects1 = new object[1];
    internal static void 変数Register(System.Type Type) {
        if(Type.IsGenericType) {
            if(Type.IsAnonymous()) {
                var FormatterType = typeof(Anonymous<>).MakeGenericType(Type);
                var Register = MemoryPackCustomSerializer.Register.MakeGenericMethod(Type);
                objects1[0]=Activator.CreateInstance(FormatterType)!;
                Register.Invoke(null,objects1);
                //Register.Invoke(null,Array.Empty<object>());
            }
            foreach(var GenericArgument in Type.GetGenericArguments()) 変数Register(GenericArgument);
        }
    }
    public byte[] Serialize<T>(T value){
        this.Clear();
        変数Register(value!.GetType());
        return MemoryPackSerializer.Serialize(value);
    }
    public void Serialize<T>(Stream stream,T? value){
        this.Clear();
        var Task=MemoryPackSerializer.SerializeAsync(stream,value).AsTask();
        Task.Wait();
    }
    public T Deserialize<T>(byte[] bytes){
        this.Clear();
        return MemoryPackSerializer.Deserialize<T>(bytes)!;
    }
    public T Deserialize<T>(Stream stream){
        this.Clear();
        //var e=MemoryPackSerializer.Instance.DeserializeAsync<T>(stream);
        var Task=MemoryPackSerializer.DeserializeAsync<T>(stream).AsTask();
        Task.Wait();
        return Task.Result!;
    }
    ////public static void WriteBoolean<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,bool value)
    ////    where TBufferWriter:IBufferWriter<byte> =>
    ////    writer.WriteVarInt((byte)(value?1:0));
    ////public static bool ReadBoolean(ref MemoryPackReader reader)=>reader.ReadVarIntByte()!=0;
    //private static class StaticReadOnlyCollectionFormatter<T>{
    //    public static readonly ReadOnlyCollectionFormatter<T> Formatter=new();
    //}
    ////internal static ReadOnlyCollectionFormatter<T> GetReadOnlyCollectionFormatter<T>()=>GenericFormatter<T>.Formatter;
    ////public static readonly ReadOnlyCollectionFormatter<Expressions.Expression> Expressions=new();
    //internal static void SerializeReadOnlyCollection<TBufferWriter,T>(ref MemoryPackWriter<TBufferWriter> writer,
    //    ReadOnlyCollection<T>? value) where TBufferWriter:IBufferWriter<byte> =>
    //    StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,ref value!);
    ////public Expressions.Expression[] DeserializeExpressions(ref MemoryPackReader reader){
    ////    Expressions.Expression[] value=default!;
    ////    reader.ReadArray(ref value!);
    ////    return value;
    ////}
    ////private static readonly ReadOnlyCollectionFormatter<ParameterExpression>Parameters=new();
    ////public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<ParameterExpression>? value) where TBufferWriter : IBufferWriter<byte> =>
    ////    Parameters.Serialize(ref writer,ref value!);
    ////public Expressions.ParameterExpression[] DeserializeParameters(ref MemoryPackReader reader){
    ////    Expressions.ParameterExpression[] value=default!;
    ////    reader.ReadArray(ref value!);
    ////    return value;
    ////}
    ////private static readonly ReadOnlyCollectionFormatter<Expressions.SwitchCase> SwitchCases=new();
    ////public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,
    ////    ReadOnlyCollection<Expressions.SwitchCase>? value) where TBufferWriter:IBufferWriter<byte> =>
    ////    SwitchCases.Serialize(ref writer,ref value!);
    ////internal static readonly ReadOnlyCollectionFormatter<Expressions.MemberBinding> MemberBindings=new();
    ////internal static readonly ReadOnlyCollectionFormatter<Expressions.CatchBlock> CatchBlocks=new();
    ////internal static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit> ElementInits=new();
    //public void Serialize宣言Parameters<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,
    //    ReadOnlyCollection<Expressions.ParameterExpression>? value) where TBufferWriter:IBufferWriter<byte>{
    //    writer.WriteVarInt(value!.Count);
    //    var Instance=Type.Instance;
    //    foreach(var Parameter in value){
    //        writer.WriteString(Parameter.Name);
    //        Instance.Serialize(ref writer,Parameter.Type);
    //    }
    //}
    //public Expressions.ParameterExpression[] Deserialize宣言Parameters(ref MemoryPackReader reader){
    //    var Count=reader.ReadVarIntInt32();
    //    var Parameters=new Expressions.ParameterExpression[Count];
    //    var Instance=Type.Instance;
    //    for(var a=0;a<Count;a++){
    //        var name=reader.ReadString();
    //        var type=Instance.Deserialize(ref reader);
    //        Parameters[a]=Expressions.Expression.Parameter(type,name);
    //    }
    //    return Parameters;
    //}
}