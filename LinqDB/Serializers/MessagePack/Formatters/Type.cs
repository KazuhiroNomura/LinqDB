using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = System.Type;


public class Type:IMessagePackFormatter<T> {
    public static readonly Type Instance=new();
    //private const int ArrayHeader0=2;
    //private const int ArrayHeader1=2;
    //private void PrivateSerialize(ref Writer writer,T value){
    //    if(Resolver.Serializer().Dictionary_Type_int.TryGetValue(value,out var index)){
    //        writer.WriteArrayHeader(ArrayHeader0);
    //        writer.WriteInt32(index);
    //    } else{
    //        writer.WriteArrayHeader(ArrayHeader1);
    //        var DictionaryTypeIndex=Resolver.Serializer().Dictionary_Type_int;
    //        index=DictionaryTypeIndex.Count;
    //        writer.WriteInt32(index);
    //        DictionaryTypeIndex.Add(value,index);
    //        writer.WriteType(value);
    //        /*
    //        if(value.IsGenericType){
    //            var GenericTypeDifinition=value.GetGenericTypeDefinition();
    //            //ReadOnlySpan<char> gg="ABC";
    //            string s=GenericTypeDifinition!.AssemblyQualifiedName!;
    //            writer.WriteString(GenericTypeDifinition!.AssemblyQualifiedName!);
    //            foreach(var GenericArgument in value.GetGenericArguments()) this.PrivateSerialize(ref writer,GenericArgument);
    //            if(value.IsAnonymous()) this.Register(value);
    //        } else{
    //            writer.WriteString(value.AssemblyQualifiedName);
    //        }
    //        */
    //        Resolver.Serializer().Types.Add(value);
    //    }
    //}
    //private readonly object[] objects2=new object[1];
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions options) {
        writer.WriteType(value);
        //this.PrivateSerialize(ref writer,value);
    }
    //private T PrivateDeserialize(ref Reader reader){
    //    var count=reader.ReadArrayHeader();
    //    var index=reader.ReadInt32();
    //    var Types=Resolver.Serializer().Types;
    //    if(index<Types.Count){
    //        Debug.Assert(count==ArrayHeader0);
    //        return Types[index];
    //    } else{
    //        Debug.Assert(count==ArrayHeader1);
    //        var DictionaryTypeIndex=Resolver.Serializer().Dictionary_Type_int;
    //        Debug.Assert(index==Types.Count);
    //        var AssemblyQualifiedName=reader.ReadString();
    //        var value= T.GetType(AssemblyQualifiedName);
    //        Types.Add(value);
    //        Debug.Assert(value!=null,nameof(value)+" != null");
    //        if(value.IsGenericType){
    //            Debug.Assert(value.IsGenericTypeDefinition);
    //            var GenericArguments=value.GetGenericArguments();
    //            for(var a=0;a<GenericArguments.Length;a++)GenericArguments[a]=this.PrivateDeserialize(ref reader);
    //            value=value.MakeGenericType(GenericArguments);
    //            //f(value.IsAnonymous())this.Register(value);
    //            Debug.Assert(Types[index]==value.GetGenericTypeDefinition());
    //            Types[index]=value;
    //        }
    //        DictionaryTypeIndex.Add(value,index);
    //        return value;
    //    }
    //}
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions options) {
        return reader.ReadType();
        //return this.PrivateDeserialize(ref reader);
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
