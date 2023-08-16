
using MessagePack;

using System.Reflection.Emit;
using System;
using LinqDB.Helpers;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers;
public readonly struct SerializerConfiguration{
    public readonly AnonymousExpressionJsonFormatterResolver AnonymousExpressionJsonFormatterResolver;
    public readonly AnonymousExpressionFormatterResolver AnonymousExpressionMessagePackFormatterResolver;
    public void Clear(){
        this.AnonymousExpressionJsonFormatterResolver.Clear();
        this.AnonymousExpressionMessagePackFormatterResolver.Clear();
    }
    public readonly IJsonFormatterResolver JsonFormatterResolver;
    //public readonly MessagePack.Resolver MessagePack_Resolver;
    public readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    public SerializerConfiguration(){
        this.AnonymousExpressionJsonFormatterResolver=new();
        //順序が大事
        this.JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
            new IJsonFormatterResolver[]{
                //this.AnonymousExpressionJsonFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                //short
                //int
                //long
                //ushort
                //uint
                //ulong
                //float
                //double
                //bool
                //byte
                //sbyte
                //DateTime
                //DateOnly
                //TimeOnly
                //char
                //short?
                //int?
                //long?
                //ushort?
                //uint?
                //ulong?
                //float?
                //double?
                //bool?
                //byte?
                //sbyte?
                //DateTime?
                //char?
                //string
                //decimal
                //decimal?
                //TimeSpan
                //TimeSpan?
                //DateTimeOffset
                //DateTimeOffset?
                //Guid
                //Guid?
                //Uri
                //Version
                //StringBuilder
                //BitArray
                //Type
                //byte[]
                //Nil
                //Nil?
                //short[]
                //int[]
                //long[]
                //ushort[]
                //uint[]
                //ulong[]
                //float[]
                //double[]
                //bool[]
                //sbyte[]
                //DateTime[]
                //char[]
                //string[]
                //List<short>
                //List<int>
                //List<long>
                //List<ushort>
                //List<uint>
                //List<ulong>
                //List<float>
                //List<double>
                //List<bool>
                //List<byte>
                //List<sbyte>
                //List<DateTime>
                //List<char>
                //List<string>
                //object[]
                //List<object>
                //Memory<byte>
                //Memory<byte>?
                //ReadOnlyMemory<byte>
                //ReadOnlyMemory<byte>?
                //ReadOnlySequence<byte>
                //ReadOnlySequence<byte>?
                //ArraySegment<byte>
                //ArraySegment<byte>?
                //BigInteger
                //BigInteger?
                //Complex
                //Complex?
                //Half
                Utf8Json.Resolvers.BuiltinResolver.Instance,
                //List<>
                //LinkedList<>
                //Queue<>
                //Stack<>
                //HashSet<>
                //ReadOnlyCollection<>
                //IList<>
                //ICollection<>
                //IEnumerable<>
                //Dictionary<, >
                //IDictionary<, >
                //SortedDictionary<, >
                //SortedList<, >
                //ILookup<, >
                //IGrouping<, >
                //ObservableCollection<>
                //ReadOnlyObservableCollection<>
                //IReadOnlyList<>
                //IReadOnlyCollection<>
                //ISet<>
                //IReadOnlySet<>
                //ConcurrentBag<>
                //ConcurrentQueue<>
                //ConcurrentStack<>
                //ReadOnlyDictionary<, >
                //IReadOnlyDictionary<, >
                //ConcurrentDictionary<, >
                //Lazy<>
                //Utf8Json.Resolvers.EnumResolver.Default,
                Utf8Json.Resolvers.DynamicGenericResolver.Instance,
                //Utf8Json.Resolvers.DynamicObjectResolver.Default,
                //Utf8Json.Resolvers.AttributeFormatterResolver.Instance,



                this.AnonymousExpressionJsonFormatterResolver,



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
        this.AnonymousExpressionMessagePackFormatterResolver=new();
        this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
            MessagePack.Resolvers.CompositeResolver.Create(
                new IFormatterResolver[]{
                    //this.AnonymousExpressionMessagePackFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                    MessagePack.Resolvers.BuiltinResolver.Instance,
                    MessagePack.Resolvers.DynamicGenericResolver.Instance,
                    this.AnonymousExpressionMessagePackFormatterResolver,
                    //MessagePack.Resolvers.DynamicObjectResolver.Instance,//
                    //MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//
                    //MessagePack.Resolvers.StandardResolver.Instance,
                    //MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
                    //MessagePack.Resolvers.StandardResolver.Instance,//
                    //this.AnonymousExpressionMessagePackFormatterResolver,
                }
            )
        );
        //this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
        //    MessagePack.Resolvers.CompositeResolver.Create(
        //        MessagePack.Resolvers.BuiltinResolver.Instance,
        //        MessagePack.Resolvers.DynamicGenericResolver.Instance,
        //        this.AnonymousExpressionMessagePackFormatterResolver,
        //        //MessagePack.Resolvers.DynamicObjectResolver.Instance,//
        //        //MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//
        //        MessagePack.Resolvers.StandardResolverAllowPrivate.Instance
        //        //MessagePack.Resolvers.StandardResolver.Instance,//
        //    )
        //);
        //this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
        //    MessagePack.Resolvers.CompositeResolver.Create(
        //        new IFormatterResolver[]{
        //            AnonymousExpressionResolver,
        //            //MessagePack.Resolvers.DynamicObjectResolver.Instance,//
        //            MessagePack.Resolvers.DynamicGenericResolver.Instance,
        //            //MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//
        //            MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
        //            //MessagePack.Resolvers.StandardResolver.Instance,//
        //        }
        //    )
        //);
        //var MessagePack_Resolver=this.MessagePack_Resolver=new MessagePack.Resolver();
        //this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
        //    MessagePack.Resolvers.CompositeResolver.Create(
        //        new IFormatterResolver[]{
        //            AnonymousExpressionResolver,
        //            //global::MessagePack.Resolvers.DynamicObjectResolver.Instance,
        //            MessagePack.Resolvers.DynamicGenericResolver.Instance,
        //            //global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,
        //            MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
        //            //global::MessagePack.Resolvers.StandardResolver.Instance,
        //        }
        //    )
        //);
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
