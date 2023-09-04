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

// ReSharper disable InconsistentNaming
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class 必要なFormatters {
    public readonly static MethodInfo Register=M(()=>MemoryPackFormatterProvider.Register(new Anonymous<int>()));
    //internal static readonly Dictionary<System.Type,int> DictionaryTypeIndex = new();
    //internal static readonly SortedDictionary<string,int> DictionaryTypeNameIndex = new();
    //internal static readonly Dictionary<MemberInfo,int> DictionaryMemberIndex = new();
    //internal static readonly Dictionary<FieldInfo,int> DictionaryFieldIndex = new();
    //internal static readonly Dictionary<PropertyInfo,int> DictionaryPropertyIndex = new();
    //internal static readonly Dictionary<MethodInfo,int> DictionaryMethodIndex = new();
    //internal static readonly Dictionary<EventInfo,int> DictionaryEventIndex = new();
    //internal static readonly List<Assembly> ListAssembly= new();
    //internal static readonly List<System.Type> ListType = new();
    //internal static readonly List<MemberInfo> ListMember = new();
    //internal static readonly List<FieldInfo> ListField = new();
    //internal static readonly List<PropertyInfo> ListProperty = new();
    //internal static readonly List<MethodInfo> ListMethod = new();
    //internal static readonly List<EventInfo> ListEvent = new();
    //static 必要なFormatters(){
    //    var DLLAssembly=typeof(必要なFormatters).Assembly;
    //    //var Assemblies=Assembly0.GetReferencedAssemblies().Select(Assembly.Load).ToList();
    //    //Assemblies.Add(Assembly0);

    //    var 呼び出し元Assembly=DLLAssembly;
    //    var StackTrace=new StackTrace();
    //    var Frames=StackTrace.GetFrames();
    //    for(var a=0;a<Frames.Length;a++){
    //        呼び出し元Assembly=Frames[a]!.GetMethod()!.DeclaringType!.Assembly;
    //        if(呼び出し元Assembly!=DLLAssembly) break;
    //    }
    //    var DLLAssemblies=DLLAssembly.GetReferencedAssemblies().Select(Assembly.Load);
    //    var DLLが参照しているAssemblies=DLLAssemblies.SelectMany(p=>p.GetReferencedAssemblies()).Select(Assembly.Load);
    //    var 呼び出し元Assemblies=呼び出し元Assembly.GetReferencedAssemblies().Select(Assembly.Load);
    //    var Assemblies3=DLLAssemblies.Union(DLLが参照しているAssemblies).Union(呼び出し元Assemblies).ToList();
    //    Assemblies3.Add(呼び出し元Assembly);
    //    Assemblies3.Add(typeof(int).Assembly);
    //    Assemblies3.Sort((a,b)=>string.CompareOrdinal(a.FullName,b.FullName));
    //    // AppDomain.CurrentDomain.GetAssemblies()
    //    var names=new List<string>();
    //    var b=0;
    //    foreach(var Assembly in Assemblies3) {
    //        ListAssembly.Add(Assembly);
    //        if(Assembly.FullName!.Contains("TestLinqDB")){

    //        }
    //        b++;
    //        var Types=Assembly.GetTypes();
    //        foreach(var Type in Types) {
    //            names.Add(Type.FullName);
    //            if(Type.AssemblyQualifiedName!.Contains("TestLinqDB")) {
    //            }
    //            if(Type.AssemblyQualifiedName!.Contains("<>f__AnonymousType0`5, TestLinqDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\r\n")) {
    //            }
    //            if(Type.Name=="<>f__AnonymousType0`5"){

    //            }
    //            if(DictionaryTypeIndex.TryAdd(Type,DictionaryTypeIndex.Count)){
    //                ListType.Add(Type);
    //                DictionaryTypeNameIndex.Add(Type.AssemblyQualifiedName,DictionaryTypeNameIndex.Count);
    //            } else{
    //                Debug.Assert(DictionaryTypeNameIndex.ContainsKey(Type.AssemblyQualifiedName));
    //            }
    //            foreach(var Member in Type.GetMembers()) {
    //                Add(DictionaryMemberIndex,ListMember,Member);
    //                if(Member.MemberType==MemberTypes.Field) Add(DictionaryFieldIndex,ListField,Member);
    //                else if(Member.MemberType==MemberTypes.Property) Add(DictionaryPropertyIndex,ListProperty,Member);
    //                else if(Member.MemberType==MemberTypes.Method) Add(DictionaryMethodIndex,ListMethod,Member);
    //                else if(Member.MemberType==MemberTypes.Event) Add(DictionaryEventIndex,ListEvent,Member);

    //            }
    //        }
    //    }
    //    static void Add<T>(Dictionary<T,int> Dictionary,List<T> List,MemberInfo value) where T : MemberInfo {
    //        var v = (T)value;
    //        if(Dictionary.TryAdd(v,Dictionary.Count))
    //            List.Add(v);
    //    }
    //}
#pragma warning disable CS8618
    internal readonly Binary Binary;
    internal readonly Block Block;
    internal readonly CatchBlock CatchBlock;
    internal readonly Conditional Conditional;
    internal readonly Constant Constant;
    internal readonly Constructor Constructor;
    internal readonly Default Default;
    internal readonly ElementInit ElementInit;
    internal readonly Expression Expression;
    internal readonly Field Field;
    internal readonly Goto Goto;
    internal readonly Index Index;
    internal readonly Invocation Invocation;
    internal readonly Label Label;
    internal readonly LabelTarget LabelTarget;
    internal readonly Lambda Lambda;
    internal readonly ListInit ListInit;
    internal readonly Loop Loop;
    internal readonly Member Member;
    internal readonly MemberAccess MemberAccess;
    internal readonly MemberBinding MemberBinding;
    internal readonly MemberInit MemberInit;
    internal readonly Method Method;
    internal readonly MethodCall MethodCall;
    internal readonly New New;
    internal readonly NewArray NewArray;
    internal readonly Object Object;
    internal readonly Parameter Parameter;
    internal readonly Property Property;
    internal readonly Switch Switch;
    internal readonly SwitchCase SwitchCase;
    internal readonly Try Try;
    internal readonly Type Type;
    internal readonly TypeBinary TypeBinary;
    internal readonly Unary Unary;
//#pragma warning restore CS8618
    //internal readonly Action<dynamic> SetProvider;
    public 必要なFormatters(){
        //this.SetProvider=SetProvider;
        Debug.WriteLine("15");
        MemoryPackFormatterProvider.Register(this.Binary=new(this));
        MemoryPackFormatterProvider.Register(this.Block=new(this));
        MemoryPackFormatterProvider.Register(this.CatchBlock=new(this));
        MemoryPackFormatterProvider.Register(this.Conditional=new(this));
        MemoryPackFormatterProvider.Register(this.Constant=new(this));
        MemoryPackFormatterProvider.Register(this.Constructor=new(this));
        MemoryPackFormatterProvider.Register(this.Default=new(this));
        MemoryPackFormatterProvider.Register(this.ElementInit=new(this));
        MemoryPackFormatterProvider.Register(this.Expression=new(this));
        MemoryPackFormatterProvider.Register(this.Field=new(this));
        MemoryPackFormatterProvider.Register(this.Goto=new(this));
        MemoryPackFormatterProvider.Register(this.Index=new(this));
        MemoryPackFormatterProvider.Register(this.Invocation=new(this));
        MemoryPackFormatterProvider.Register(this.Label=new(this));
        MemoryPackFormatterProvider.Register(this.LabelTarget=new(this));
        MemoryPackFormatterProvider.Register(this.Lambda=new(this));
        MemoryPackFormatterProvider.Register(this.ListInit=new(this));
        MemoryPackFormatterProvider.Register(this.Loop=new(this));
        MemoryPackFormatterProvider.Register(this.Member=new(this));
        MemoryPackFormatterProvider.Register(this.MemberAccess=new(this));
        MemoryPackFormatterProvider.Register(this.MemberBinding=new(this));
        MemoryPackFormatterProvider.Register(this.MemberInit=new(this));
        MemoryPackFormatterProvider.Register(this.Method=new(this));
        MemoryPackFormatterProvider.Register(this.MethodCall=new(this));
        MemoryPackFormatterProvider.Register(this.New=new(this));
        MemoryPackFormatterProvider.Register(this.NewArray=new(this));
        //this.Object=new(this);
        MemoryPackFormatterProvider.Register(this.Object=new(this));
        MemoryPackFormatterProvider.Register(this.Parameter=new(this));
        MemoryPackFormatterProvider.Register(this.Property=new(this));
        MemoryPackFormatterProvider.Register(this.Switch=new(this));
        MemoryPackFormatterProvider.Register(this.SwitchCase=new(this));
        MemoryPackFormatterProvider.Register(this.Try=new(this));
        MemoryPackFormatterProvider.Register(this.Type=new(this));
        MemoryPackFormatterProvider.Register(this.TypeBinary=new(this));
        MemoryPackFormatterProvider.Register(this.Unary=new(this));
        Debug.WriteLine("16");
    }
    internal readonly List<ParameterExpression> ListParameter = new();
    internal readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int = new();
    internal readonly List<Expressions.LabelTarget> ListLabelTarget = new();
    /*
    public (Expressions.Expression Left, Expressions.Expression Right) Deserialize_Binary(ref MemoryPackReader reader) {
        var Left = this.Expression.Deserialize(ref reader);
        var Right = this.Expression.Deserialize(ref reader);
        return (Left, Right);
    }
    public (Expressions.Expression Left, Expressions.Expression Right, MethodInfo? Method) Deserialize_Binary_MethodInfo(ref MemoryPackReader reader) {
        var Left = this.Expression.Deserialize(ref reader);
        var Right = this.Expression.Deserialize(ref reader);
        var Method = this.Method.DeserializeNullable(ref reader);
        return (Left, Right, Method);
    }
    public (Expressions.Expression Left, Expressions.Expression Right, bool IsLiftedToNull, MethodInfo? Method) Deserialize_Binary_bool_MethodInfo(ref MemoryPackReader reader) {
        var Left = this.Expression.Deserialize(ref reader);
        var Right = this.Expression.Deserialize(ref reader);
        var IsLiftedToNull = ReadBoolean(ref reader);
        var Method = this.Method.DeserializeNullable(ref reader);
        return (Left, Right, IsLiftedToNull, Method);
    }
    public void Serialize_Unary<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (UnaryExpression)value;
        this.Expression.Serialize(ref writer,Unary.Operand);
    }
    public void Serialize_Unary_Type<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (UnaryExpression)value;
        this.Expression.Serialize(ref writer,Unary.Operand);
        this.Type.Serialize(ref writer,Unary.Type);
    }
    public void Serialize_Unary_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (UnaryExpression)value;
        this.Expression.Serialize(ref writer,Unary.Operand);
        this.Method.SerializeNullable(ref writer,Unary.Method);
    }
    public void Serialize_Unary_Type_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (UnaryExpression)value;
        this.Expression.Serialize(ref writer,Unary.Operand);
        this.Type.Serialize(ref writer,Unary.Type);
        this.Method.SerializeNullable(ref writer,Unary.Method);
    }
    public Expressions.Expression Deserialize_Unary(ref MemoryPackReader reader) => this.Expression.Deserialize(ref reader);
    public (Expressions.Expression Operand, System.Type Type) Deserialize_Unary_Type(ref MemoryPackReader reader) {
        var Operand = this.Expression.Deserialize(ref reader);
        var Type = this.Type.DeserializeType(ref reader);
        return (Operand, Type);
    }
    public (Expressions.Expression Operand, MethodInfo? Method) Deserialize_Unary_MethodInfo(ref MemoryPackReader reader) {
        var Operand = this.Expression.Deserialize(ref reader);
        var Method = this.Method.DeserializeNullable(ref reader);
        return (Operand, Method);
    }
    public (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) Deserialize_Unary_Type_MethodInfo(ref MemoryPackReader reader) {
        var Operand = this.Expression.Deserialize(ref reader);
        var Type = this.Type.DeserializeType(ref reader);
        var Method = this.Method.DeserializeNullable(ref reader);
        return (Operand, Type, Method);
    }
    */
    //public void Provider(Action<dynamic> SetProvider)=>this.SetProvider=SetProvider;
    private readonly object[] objects2=new object[1];
    public void 変数Register(System.Type Type){
        if(Type.IsGenericType){
            if(Type.IsAnonymous()){
                var FormatterType=typeof(Anonymous<>).MakeGenericType(Type);
                var Register=必要なFormatters.Register.MakeGenericMethod(Type);
                var objects2=this.objects2;
                objects2[0]=Activator.CreateInstance(FormatterType)!;
                Register.Invoke(null,objects2);
                //Register.Invoke(null,Array.Empty<object>());
            }
            foreach(var GenericArgument in Type.GetGenericArguments())this.変数Register(GenericArgument);
        }
    }
    public void Clear(){
        this.Type.Clear();
        this.Method.Clear();
        this.Field.Clear();
        this.Property.Clear();
        //this..Clear();
        this.Method.Clear();
        this.ListParameter.Clear();
        this.Dictionary_LabelTarget_int.Clear();
        this.ListLabelTarget.Clear();
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
    public void Serialize宣言Parameters<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<ParameterExpression>? value) where TBufferWriter : IBufferWriter<byte> {
        writer.WriteVarInt(value!.Count);
        foreach(var Parameter in value) {
            writer.WriteString(Parameter.Name);
            this.Type.Serialize(ref writer,Parameter.Type);
        }
    }
    public ParameterExpression[] Deserialize宣言Parameters(ref MemoryPackReader reader) {
        var Count = reader.ReadVarIntInt32();
        var Parameters = new ParameterExpression[Count];
        for(var a = 0;a<Count;a++) {
            var name = reader.ReadString();
            var type = this.Type.DeserializeType(ref reader);
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
