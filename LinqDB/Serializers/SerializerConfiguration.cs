
using MessagePack;

using System.Reflection.Emit;
using System;
using System.Diagnostics;
using LinqDB.Helpers;
using MemoryPack_Formatters=LinqDB.Serializers.MemoryPack.Formatters;
using MemoryPack.Formatters;
using Utf8Json;

namespace LinqDB.Serializers;
public readonly struct SerializerConfiguration{
    private readonly DispatchJsonFormatterResolver DispatchJsonFormatterResolver=new();
    private readonly DispatchMessagePackFormatterResolver DispatchMessagePackFormatterResolver=new();
    public void ClearJson()=>this.DispatchJsonFormatterResolver.Clear();
    public void ClearMessagePack()=>this.DispatchMessagePackFormatterResolver.Clear();
    //public void Clear(){
    //    this.AnonymousExpressionJsonFormatterResolver.Clear();
    //    this.AnonymousExpressionMessagePackFormatterResolver.Clear();
    //}
    public readonly IJsonFormatterResolver JsonFormatterResolver;
    //public readonly MessagePack.Resolver MessagePack_Resolver;
    public readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    public SerializerConfiguration(){
        //this.AnonymousExpressionJsonFormatterResolver=new();
        //順序が大事
        Debug.WriteLine("0");
        this.JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
            //new IJsonFormatter[]{
            //    this.AnonymousExpressionJsonFormatterResolver.ExpressionFormatter, 
            //},
            new IJsonFormatterResolver[]{
                //    this.AnonymousExpressionJsonFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                //    short
                //    int
                //    long
                //    ushort
                //    uint
                //    ulong
                //    float
                //    double
                //    bool
                //    byte
                //    sbyte
                //    DateTime
                //    DateOnly
                //    TimeOnly
                //    char
                //    short?
                //    int?
                //    long?
                //    ushort?
                //    uint?
                //    ulong?
                //    float?
                //    double?
                //    bool?
                //    byte?
                //    sbyte?
                //    DateTime?
                //    char?
                //    string
                //    decimal
                //    decimal?
                //    TimeSpan
                //    TimeSpan?
                //    DateTimeOffset
                //    DateTimeOffset?
                //    Guid
                //    Guid?
                //    Uri
                //    Version
                //    StringBuilder
                //    BitArray
                //    Type
                //    byte[]
                //    Nil
                //    Nil?
                //    short[]
                //    int[]
                //    long[]
                //    ushort[]
                //    uint[]
                //    ulong[]
                //    float[]
                //    double[]
                //    bool[]
                //    sbyte[]
                //    DateTime[]
                //    char[]
                //    string[]
                //    List<short>
                //    List<int>
                //    List<long>
                //    List<ushort>
                //    List<uint>
                //    List<ulong>
                //    List<float>
                //    List<double>
                //    List<bool>
                //    List<byte>
                //    List<sbyte>
                //    List<DateTime>
                //    List<char>
                //    List<string>
                //    object[]
                //    List<object>
                //    Memory<byte>
                //    Memory<byte>?
                //    ReadOnlyMemory<byte>
                //    ReadOnlyMemory<byte>?
                //    ReadOnlySequence<byte>
                //    ReadOnlySequence<byte>?
                //    ArraySegment<byte>
                //    ArraySegment<byte>?
                //    BigInteger
                //    BigInteger?
                //    Complex
                //    Complex?
                //    Half
                Utf8Json.Resolvers.BuiltinResolver.Instance,//よく使う型
                //    List<>
                //    LinkedList<>
                //    Queue<>
                //    Stack<>
                //    HashSet<>
                //    ReadOnlyCollection<>
                //    IList<>
                //    ICollection<>
                //    IEnumerable<>
                //    Dictionary<, >
                //    IDictionary<, >
                //    SortedDictionary<, >
                //    SortedList<, >
                //    ILookup<, >
                //    IGrouping<, >
                //    ObservableCollection<>
                //    ReadOnlyObservableCollection<>
                //    IReadOnlyList<>
                //    IReadOnlyCollection<>
                //    ISet<>
                //    IReadOnlySet<>
                //    ConcurrentBag<>
                //    ConcurrentQueue<>
                //    ConcurrentStack<>
                //    ReadOnlyDictionary<, >
                //    IReadOnlyDictionary<, >
                //    ConcurrentDictionary<, >
                //    Lazy<>
                Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
                Utf8Json.Resolvers.EnumResolver.Default,
                //Utf8Json.Resolvers.AttributeFormatterResolver.Default,
                //Utf8Json.Resolvers.StandardResolver.Default,

                //Utf8Json.Resolvers.DynamicObjectResolver.Default,
                //Utf8Json.Resolvers.CompositeResolver.Instance,



                this.DispatchJsonFormatterResolver,



                //Utf8Json.Resolvers.DynamicObjectResolver.Default,//これが存在するとStackOverflowする
                //Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//これが存在するとTypeがシリアライズできない

                //BuiltinResolver.Instance,
                //EnumResolver.Default,
                //DynamicGenericResolver.Instance,
                //AttributeFormatterResolver.Instance
                //Utf8Json.Resolvers.EnumResolver.Default,
                //Utf8Json.Resolvers.DynamicGenericResolver.Instance,
                //Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
                //Utf8Json.Resolvers.StandardResolver.AllowPrivate,//いくつかのリゾルバをまとめてある
                //Utf8Json.Resolvers.StandardResolver.Default,
                //this.AnonymousExpressionJsonFormatterResolver,
            }
        );
        Debug.WriteLine("1");
        //this.JsonFormatterResolver =Utf8Json.Resolvers.CompositeResolver.Create(
        //    //順序が大事
        //    Utf8Json.Resolvers.BuiltinResolver.Instance,
        //    Utf8Json.Resolvers.DynamicGenericResolver.Instance,
        //    this.AnonymousExpressionJsonFormatterResolver,
        //    //global::Utf8Json.Resolvers.DynamicObjectResolver.Default,//これが存在するとStackOverflowする
        //    //global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//これが存在するとTypeがシリアライズできない
        //    Utf8Json.Resolvers.StandardResolver.AllowPrivate
        //    //global::Utf8Json.Resolvers.StandardResolver.Default,
        //);
        //this.AnonymousExpressionMessagePackFormatterResolver=new();
        this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
            MessagePack.Resolvers.CompositeResolver.Create(
                new IFormatterResolver[]{
                    //this.AnonymousExpressionMessagePackFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                    MessagePack.Resolvers.BuiltinResolver.Instance,
                    MessagePack.Resolvers.DynamicGenericResolver.Instance,//GenericEnumerableFormatter
                    //MessagePack.Resolvers.DynamicEnumAsStringResolver.Instance,
                    //MessagePack.Resolvers.DynamicEnumResolver.Instance,
                    this.DispatchMessagePackFormatterResolver,
                    //MessagePack.Resolvers.DynamicObjectResolver.Instance,//
                    //MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//
                    //MessagePack.Resolvers.StandardResolver.Instance,
                    //MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
                    //MessagePack.Resolvers.StandardResolver.Instance,//
                    //this.AnonymousExpressionMessagePackFormatterResolver,
                }
            )
        );
    }
    private delegate void SerializeDelegate(object Formatter,ref MessagePackWriter writer,object value,MessagePackSerializerOptions options);
    private static readonly Type[] SerializeTypes={typeof(object),typeof(MessagePackWriter).MakeByRefType(),typeof(object),typeof(MessagePackSerializerOptions)};
    public static void DynamicSerialize(object Formatter,ref MessagePackWriter writer,object value,MessagePackSerializerOptions options){
        var Formatter_Serialize = Formatter.GetType().GetMethod("Serialize")!;
        var D = new DynamicMethod("",typeof(void),SerializeTypes) {
            InitLocals=false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        I.Ldarg_2();
        I.Unbox_Any(Formatter_Serialize.GetParameters()[1].ParameterType);
        I.Ldarg_3();
        I.Callvirt(Formatter_Serialize);
        I.Ret();
        ((SerializeDelegate)D.CreateDelegate(typeof(SerializeDelegate)))(Formatter,ref writer,value,options);
    }
    private delegate object DeserializeDelegate            (object Formatter,ref MessagePackReader reader          ,       MessagePackSerializerOptions options);
    private static readonly Type[] DeserializeTypes={typeof(object),      typeof(MessagePackReader).MakeByRefType(),typeof(MessagePackSerializerOptions)};
    public static object DynamicDeserialize(object Formatter,ref MessagePackReader reader,MessagePackSerializerOptions options){
        var Method=Formatter.GetType().GetMethod("Deserialize")!;
        var D=new DynamicMethod("",typeof(object),DeserializeTypes){
            InitLocals=false
        };
        var I=D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        I.Ldarg_2();
        I.Callvirt(Method);
        I.Box(Method.ReturnType);
        I.Ret();
        var Del=(DeserializeDelegate)D.CreateDelegate(typeof(DeserializeDelegate));
        var Result=Del(Formatter,ref reader,options);
        return Result;
    }
}
