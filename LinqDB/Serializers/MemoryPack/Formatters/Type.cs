using System;
using MemoryPack;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using LinqDB.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Type:MemoryPackFormatter<System.Type>{
    private readonly 必要なFormatters Formatters;
    public Type(必要なFormatters Formatters)=>this.Formatters=Formatters;
    private readonly Dictionary<System.Type,int> DictionaryTypeIndex = new();
    private readonly List<System.Type> Types = new();
    internal void Clear(){
        this.DictionaryTypeIndex.Clear();
        this.Types.Clear();
    }
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,System.Type? value) where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
        ////Debug.Assert(value.IsGenericType==value.IsGenericTypeDefinition);
        //if(this.DictionaryTypeIndex.TryGetValue(value,out var index)){
        //    必要なFormatters.WriteBoolean(ref writer,true);
        //    writer.WriteVarInt(index);
        //} else{
        //    必要なFormatters.WriteBoolean(ref writer,false);
        //    writer.WriteString(value.AssemblyQualifiedName);
        //    var DictionaryTypeIndex=this.DictionaryTypeIndex;
        //    DictionaryTypeIndex.Add(value,DictionaryTypeIndex.Count);
        //    this.ListType.Add(value);
        //    this.Regist(value);
        //}
    }
    internal System.Type DeserializeType(ref MemoryPackReader reader){
        System.Type? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref System.Type? value){
        var value0=value!;
        //Debug.Assert(value.IsGenericType==value.IsGenericTypeDefinition);
        if(this.DictionaryTypeIndex.TryGetValue(value0,out var index)){
            //必要なFormatters.WriteBoolean(ref writer,true);
            writer.WriteVarInt(index);
        } else{
            var DictionaryTypeIndex=this.DictionaryTypeIndex;
            index=DictionaryTypeIndex.Count;
            writer.WriteVarInt(index);
            DictionaryTypeIndex.Add(value0,index);
            //必要なFormatters.WriteBoolean(ref writer,false);
            writer.WriteString(value0.AssemblyQualifiedName);
            this.Types.Add(value);
            if(value0.IsAnonymous()){
                var FormatterType=typeof(Anonymous<>).MakeGenericType(value0);
                var Register=必要なFormatters.Register.MakeGenericMethod(value0);
                Register.Invoke(null,new[]{Activator.CreateInstance(FormatterType)});
                //this.Formatters.SetProvider(Activator.CreateInstance(FormatterType));
            }
        }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref System.Type? value){
        var index=reader.ReadVarIntInt32();
        var Types=this.Types;
        if(index<Types.Count){
            value=Types[index];
        } else{
            var AssemblyQualifiedName=reader.ReadString();
            var Type=System.Type.GetType(AssemblyQualifiedName);
            var DictionaryTypeIndex=this.DictionaryTypeIndex;
            Debug.Assert(index==DictionaryTypeIndex.Count);
            DictionaryTypeIndex.Add(Type,index);
            Types.Add(Type);
            value=Type;
        }
        //var i=reader.ReadVarIntInt32();
        //var value0=ListType[i];
        //if(value0!.IsGenericType){
        //    var GenericTypeDefinition=value0.GetGenericTypeDefinition();
        //    var GenericArguments=value0.GetGenericArguments();
        //    for(var a=0;a<GenericArguments.Length;a++)this.Deserialize(ref reader,ref GenericArguments[a]!);
        //    value0=GenericTypeDefinition.MakeGenericType(GenericArguments);
        //    Regist(value0);
        //} else{
        //    value=value0;
        //}
    }
    //internal readonly Dictionary<System.Type,int> DictionaryTypeIndex = new();
    //internal readonly SortedDictionary<string,int> DictionaryTypeNameIndex = new();
    //internal readonly Dictionary<MemberInfo,int> DictionaryMemberIndex = new();
    //internal readonly Dictionary<FieldInfo,int> DictionaryFieldIndex = new();
    //internal readonly Dictionary<PropertyInfo,int> DictionaryPropertyIndex = new();
    //internal readonly Dictionary<MethodInfo,int> DictionaryMethodIndex = new();
    //internal readonly Dictionary<EventInfo,int> DictionaryEventIndex = new();
    //internal readonly List<Assembly> ListAssembly= new();
    //internal readonly List<System.Type> ListType = new();
    //internal readonly List<MemberInfo> ListMember = new();
    //internal readonly List<FieldInfo> ListField = new();
    //internal readonly List<PropertyInfo> ListProperty = new();
    //internal readonly List<MethodInfo> ListMethod = new();
    //internal readonly List<EventInfo> ListEvent = new();
    //public void Clear(){
    //    this.DictionaryTypeIndex.Clear();
    //    this.DictionaryTypeNameIndex.Clear();
    //    this.DictionaryMemberIndex.Clear();
    //    this.DictionaryFieldIndex.Clear();
    //    this.DictionaryPropertyIndex.Clear();
    //    this.DictionaryMethodIndex.Clear();
    //    this.DictionaryEventIndex.Clear();
    //    this.ListAssembly.Clear();
    //    this.ListType.Clear();
    //    this.ListMember.Clear();
    //    this.ListField.Clear();
    //    this.ListProperty.Clear();
    //    this.ListMethod.Clear();
    //    this.ListEvent.Clear();
    //}
    //static Type(){
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
}
