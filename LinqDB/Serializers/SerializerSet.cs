
using MessagePack;

using Utf8Json;
namespace LinqDB.Serializers;
public struct SerializerSet{
    public readonly AnonymousExpressionResolver AnonymousExpressionResolver;
    public readonly IJsonFormatterResolver JsonFormatterResolver;
    //public readonly MessagePack.Resolver MessagePack_Resolver;
    public readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    public SerializerSet(){
        var AnonymousExpressionResolver=this.AnonymousExpressionResolver=new AnonymousExpressionResolver();
        this.JsonFormatterResolver =global::Utf8Json.Resolvers.CompositeResolver.Create(
            new[]{
                //順序が大事
                AnonymousExpressionResolver,
                //global::Utf8Json.Resolvers.DynamicObjectResolver.Default,//これが存在するとStackOverflowする
                global::Utf8Json.Resolvers.DynamicGenericResolver.Instance,
                //global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//これが存在するとTypeがシリアライズできない
                global::Utf8Json.Resolvers.StandardResolver.AllowPrivate,
                //global::Utf8Json.Resolvers.StandardResolver.Default,
            }
        );
        //var MessagePack_Resolver=this.MessagePack_Resolver=new MessagePack.Resolver();
        this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
            global::MessagePack.Resolvers.CompositeResolver.Create(
                new IFormatterResolver[]{
                    AnonymousExpressionResolver,
                    //global::MessagePack.Resolvers.DynamicObjectResolver.Instance,
                    global::MessagePack.Resolvers.DynamicGenericResolver.Instance,
                    //global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,
                    global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
                    //global::MessagePack.Resolvers.StandardResolver.Instance,
                }
            )
        );
    }
}
