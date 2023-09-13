using System;
using MemoryPack;
using System.Buffers;
using System.Diagnostics;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=System.Type;
using C=Serializer;


public class Type:MemoryPackFormatter<T> {
    public static readonly Type Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        Instance.Serialize(ref writer,ref value);
    }
    internal static T Deserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    //private void Register(T AnonymousType){
    //    var FormatterType=typeof(Anonymous<>).MakeGenericType(AnonymousType);
    //    var Register= C.Instance.Register.MakeGenericMethod(AnonymousType);
    //    var objects2=this.objects2;
    //    objects2[0]=Activator.CreateInstance(FormatterType)!;
    //    Register.Invoke(null,objects2);
    //}
    //private void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
    //    if(DictionaryTypeIndex.TryGetValue(value,out var index)){
    //        writer.WriteVarInt(index);
    //    } else{
    //        var DictionaryTypeIndex=DictionaryTypeIndex;
    //        index=DictionaryTypeIndex.Count;
    //        writer.WriteVarInt(index);
    //        DictionaryTypeIndex.Add(value,index);
    //        if(value.IsGenericType){
    //            var GenericTypeDifinition=value.GetGenericTypeDefinition();
    //            writer.WriteString(GenericTypeDifinition.AssemblyQualifiedName);
    //            foreach(var GenericArgument in value.GetGenericArguments()) this.PrivateSerialize(ref writer,GenericArgument);
    //            if(value.IsAnonymous()) this.Register(value);
    //        } else{
    //            writer.WriteString(value.AssemblyQualifiedName);
    //        }
    //        Types.Add(value);
    //    }
    //}
    //private readonly object[] objects2=new object[1];
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteType(value);
        ////this.PrivateSerialize(ref writer,value);
        //if(C.Instance.Dictionary_Type_int.TryGetValue(value,out var index)){
        //    writer.WriteVarInt(index);
        //} else{
        //    var Dictionary_Type_int= C.Instance.Dictionary_Type_int;
        //    C.Instance.Types.Add(value);
        //    index=Dictionary_Type_int.Count;
        //    Dictionary_Type_int.Add(value,index);
        //    writer.WriteVarInt(index);
        //    writer.WriteString(value.AssemblyQualifiedName);
        //}
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=reader.ReadType();
        //var index=reader.ReadVarIntInt32();
        //var Types= C.Instance.Types;
        //if(index<Types.Count){
        //    value=Types[index];
        //} else{
        //    value=System.Type.GetType(reader.ReadString())!;
        //    var Dictionary_Type_int= C.Instance.Dictionary_Type_int;
        //    index=Dictionary_Type_int.Count;
        //    Types.Add(value);
        //    Dictionary_Type_int.Add(value,index);
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
