using System;
using System.Collections.Generic;
using System.Buffers;
using MemoryPack;
using Expressions=System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;
using MemoryPack.Formatters;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using LinqDB.Helpers;
using static LinqDB.Reflection.Common;
using static Microsoft.FSharp.Core.ByRefKinds;
using System.IO;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming
namespace LinqDB.Serializers.MemoryPack.Formatters;
public static class CustomSerializerMemoryPack {
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
    static CustomSerializerMemoryPack(){
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
    internal static readonly List<ParameterExpression> ListParameter = new();
    internal static readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int = new();
    internal static readonly List<Expressions.LabelTarget> ListLabelTarget = new();
    internal static readonly Dictionary<System.Type,int> DictionaryTypeIndex = new();
    internal static readonly List<System.Type> Types = new();
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
    public static void WriteBoolean<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,bool value)where TBufferWriter :  IBufferWriter<byte>  => writer.WriteVarInt((byte)(value?1:0));
    public static bool ReadBoolean(ref MemoryPackReader reader) => reader.ReadVarIntByte()!=0;
    private static readonly ReadOnlyCollectionFormatter<Expressions.Expression>Expressions=new();
    public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<Expressions.Expression>? value) where TBufferWriter : IBufferWriter<byte> =>
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
    private static readonly ReadOnlyCollectionFormatter<Expressions.SwitchCase>SwitchCases=new();
    public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<Expressions.SwitchCase>? value) where TBufferWriter : IBufferWriter<byte> =>
        SwitchCases.Serialize(ref writer,ref value!);
    public static readonly ReadOnlyCollectionFormatter<Expressions.MemberBinding>MemberBindings=new();
    public static readonly ReadOnlyCollectionFormatter<Expressions.CatchBlock>CatchBlocks=new();
    public static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit>ElementInits=new();
    public static void Serialize宣言Parameters<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<ParameterExpression>? value) where TBufferWriter : IBufferWriter<byte> {
        writer.WriteVarInt(value!.Count);
        foreach(var Parameter in value) {
            writer.WriteString(Parameter.Name);
            Type.Serialize(ref writer,Parameter.Type);
        }
    }
    public static ParameterExpression[] Deserialize宣言Parameters(ref MemoryPackReader reader) {
        var Count = reader.ReadVarIntInt32();
        var Parameters = new ParameterExpression[Count];
        for(var a = 0;a<Count;a++) {
            var name = reader.ReadString();
            var type = Type.DeserializeType(ref reader);
            Parameters[a]=System.Linq.Expressions.Expression.Parameter(type,name);
        }
        return Parameters;
    }
}
public class Binary2:MemoryPackFormatter<BinaryExpression>{
    //internal readonly 必要なFormatters2 必要なFormatters2;
    //public Binary2(必要なFormatters2 必要なFormatters2){
    //    this.必要なFormatters2=必要なFormatters2;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref BinaryExpression? value) {
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref BinaryExpression? value) {
    }
}
public class 必要なFormatters2 {
    static 必要なFormatters2() {
    }
#pragma warning disable CS8618
    internal readonly Binary2 Binary2;
#pragma warning restore CS8618
    public 必要なFormatters2() {
        Debug.WriteLine("15");
        MemoryPackFormatterProvider.Register(this.Binary2=new());
    }
}

//public class Binary2:MemoryPackFormatter<BinaryExpression>{
//    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref BinaryExpression? value){
//    }
//    public override void Deserialize(ref MemoryPackReader reader,scoped ref BinaryExpression? value){
//    }
//}
//public class 必要なFormatters{
//    internal static readonly Dictionary<System.Type,int>Dictionary番号Type=new();
//    internal static readonly Dictionary<MemberInfo,int>Dictionary番号Member=new();
//    internal static readonly Dictionary<FieldInfo,int>Dictionary番号Field=new();
//    internal static readonly Dictionary<PropertyInfo,int>Dictionary番号Property=new();
//    internal static readonly Dictionary<MethodInfo,int>Dictionary番号Method=new();
//    internal static readonly Dictionary<EventInfo,int>Dictionary番号Event=new();
//    internal static readonly List<System.Type>ListType=new();
//    internal static readonly List<MemberInfo>ListMember=new();
//    internal static readonly List<FieldInfo>ListField=new();
//    internal static readonly List<PropertyInfo>ListProperty=new();
//    internal static readonly List<MethodInfo>ListMethod=new();
//    internal static readonly List<EventInfo>ListEvent=new();
//    static 必要なFormatters(){
//        //foreach(var Assembly in AppDomain.CurrentDomain.GetAssemblies()){
//        //    foreach(var Type in Assembly.GetTypes()){
//        //        if(Dictionary番号Type.TryAdd(Type,Dictionary番号Type.Count))
//        //            ListType.Add(Type);
//        //        foreach(var Member in Type.GetMembers()){
//        //            Add(Dictionary番号Member,ListMember,Member);
//        //            if     (Member.MemberType==MemberTypes.Field)Add(Dictionary番号Field,ListField,Member);
//        //            else if(Member.MemberType==MemberTypes.Property)Add(Dictionary番号Property,ListProperty,Member);
//        //            else if(Member.MemberType==MemberTypes.Method)Add(Dictionary番号Method,ListMethod,Member);
//        //            else if(Member.MemberType==MemberTypes.Event)Add(Dictionary番号Event,ListEvent,Member);

//        //        }
//        //    }
//        //}
//        //static void Add<T>(Dictionary<T,int> Dictionary,List<T>List,MemberInfo value)where T:MemberInfo{
//        //    var v=(T)value;
//        //    if(Dictionary.TryAdd(v,Dictionary.Count))
//        //        List.Add(v);
//        //}
//    }
//#pragma warning disable CS8618
//    internal readonly Binary2 Binary2=default!;
//    internal readonly Binary Binary=default!;
//    internal readonly Block  Block=default!;
//    internal readonly CatchBlock CatchBlock=default!;
//    internal readonly Conditional Conditional=default!;
//    internal readonly Constant Constant=default!;
//    internal readonly Constructor Constructor=default!;
//    internal readonly Default Default=default!;
//    internal readonly ElementInit ElementInit=default!;
//    public readonly Expression Expression=default!;
//    internal readonly Field Field=default!;
//    internal readonly Goto Goto=default!;
//    internal readonly Index Index=default!;
//    internal readonly Invocation Invocation=default!;
//    internal readonly Label Label=default!;
//    internal readonly LabelTarget LabelTarget=default!;
//    internal readonly Lambda Lambda=default!;
//    internal readonly ListInit ListInit=default!;
//    internal readonly Loop Loop=default!;
//    internal readonly Member Member=default!;
//    internal readonly MemberAccess MemberAccess=default!;
//    internal readonly MemberInit MemberInit=default!;
//    internal readonly Method Method=default!;
//    internal readonly MethodCall MethodCall=default!;
//    internal readonly New New=default!;
//    internal readonly NewArray NewArray=default!;
//    internal readonly Object Object=default!;
//    internal readonly Parameter Parameter=default!;
//    internal readonly Property Property=default!;
//    internal readonly Switch Switch=default!;
//    internal readonly SwitchCase SwitchCase=default!;
//    internal readonly Try Try=default!;
//    internal readonly Type Type=default!;
//    internal readonly TypeBinary TypeBinary=default!;
//    internal readonly Unary Unary=default!;
//#pragma warning restore CS8618
//    public 必要なFormatters(){
//        Debug.WriteLine("15");
//        //this.Binary2=new(this);
//        //MemoryPackFormatterProvider.Register(this.Expression=new(this));
//        Debug.WriteLine("16");
//    }
//    internal readonly List<ParameterExpression> ListParameter=new();
//    internal readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
//    internal readonly Dictionary<int,Expressions.LabelTarget> Dictionary_int_LabelTarget=new();
//    public(Expressions.Expression Left,Expressions.Expression Right)Deserialize_Binary(ref MemoryPackReader reader){
//        var Left= this.Expression.DeserializeExpression(ref reader);
//        var Right= this.Expression.DeserializeExpression(ref reader);
//        return(Left,Right);
//    }
//    public(Expressions.Expression Left,Expressions.Expression Right,MethodInfo? Method)Deserialize_Binary_MethodInfo(ref MemoryPackReader reader){
//        var Left= this.Expression.DeserializeExpression(ref reader);
//        var Right= this.Expression.DeserializeExpression(ref reader);
//        var Method=this.Method.DeserializeNullable(ref reader);
//        return(Left,Right,Method);
//    }
//    public(Expressions.Expression Left,Expressions.Expression Right,bool IsLiftedToNull,MethodInfo? Method)Deserialize_Binary_bool_MethodInfo(ref MemoryPackReader reader){
//        var Left= this.Expression.DeserializeExpression(ref reader);
//        var Right= this.Expression.DeserializeExpression(ref reader);
//        var IsLiftedToNull=ReadBoolean(ref reader);
//        var Method=this.Method.DeserializeNullable(ref reader);
//        return(Left,Right,IsLiftedToNull,Method);
//    }
//    public void Serialize_Unary<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
//        var Unary = (UnaryExpression)value;
//        this.Expression.Serialize(ref writer,Unary.Operand);
//    }
//    public void Serialize_Unary_Type<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
//        var Unary = (UnaryExpression)value;
//        this.Expression.Serialize(ref writer,Unary.Operand);
//        this.Type.Serialize(ref writer,Unary.Type);
//    }
//    public void Serialize_Unary_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
//        var Unary = (UnaryExpression)value;
//        this.Expression.Serialize(ref writer,Unary.Operand);
//        this.Method.SerializeNullable(ref writer,Unary.Method);
//    }
//    public void Serialize_Unary_Type_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
//        var Unary = (UnaryExpression)value;
//        this.Expression.Serialize(ref writer,Unary.Operand);
//        this.Type.Serialize(ref writer,Unary.Type);
//        this.Method.SerializeNullable(ref writer,Unary.Method);
//    }
//    public Expressions.Expression Deserialize_Unary(ref MemoryPackReader reader) => this.Expression.DeserializeExpression(ref reader);
//    public(Expressions.Expression Operand, System.Type Type) Deserialize_Unary_Type(ref MemoryPackReader reader) {
//        var Operand = this.Expression.DeserializeExpression(ref reader);
//        var Type = this.Type.DeserializeType(ref reader);
//        return (Operand, Type);
//    }
//    public(Expressions.Expression Operand, MethodInfo? Method) Deserialize_Unary_MethodInfo(ref MemoryPackReader reader) {
//        var Operand = this.Expression.DeserializeExpression(ref reader);
//        var Method = this.Method.DeserializeNullable(ref reader);
//        return (Operand, Method);
//    }
//    public(Expressions.Expression Operand, System.Type Type, MethodInfo? Method) Deserialize_Unary_Type_MethodInfo(ref MemoryPackReader reader) {
//        var Operand = this.Expression.DeserializeExpression(ref reader);
//        var Type = this.Type.DeserializeType(ref reader);
//        var Method = this.Method.DeserializeNullable(ref reader);
//        return (Operand, Type, Method);
//    }
//    public void Clear(){
//        this.ListParameter.Clear();
//        this.Dictionary_LabelTarget_int.Clear();
//        this.Dictionary_int_LabelTarget.Clear();
//    }
//    public static bool ReadBoolean(ref MemoryPackReader reader)=>reader.ReadVarIntByte()!=0;
//    public static void WriteBoolean<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,bool value)where TBufferWriter:IBufferWriter<byte> =>writer.WriteVarInt((byte)(value?1:0));
//    public void Serialize宣言Parameters<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<ParameterExpression>? value)where TBufferWriter:IBufferWriter<byte>{
//        writer.WriteVarInt(value!.Count);
//        foreach(var Parameter in value){
//            writer.WriteString(Parameter.Name);
//            this.Type.Serialize(ref writer,Parameter.Type);
//        }
//    }
//    public ParameterExpression[]Deserialize宣言Parameters(ref MemoryPackReader reader){
//        var Count=reader.ReadVarIntInt32();
//        var Parameters=new ParameterExpression[Count];
//        for(var a=0;a<Count;a++){
//            var name=reader.ReadString();
//            var type= this.Type.DeserializeType(ref reader);
//            Parameters[a]=Expressions.Expression.Parameter(type,name);
//        }
//        return Parameters;
//    }
//}
