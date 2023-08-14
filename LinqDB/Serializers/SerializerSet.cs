﻿
using MessagePack;

using Utf8Json;
namespace LinqDB.Serializers;
public struct SerializerSet{
    public readonly AnonymousExpressionJsonFormatterResolver AnonymousExpressionJsonFormatterResolver;
    public readonly AnonymousExpressionFormatterResolver AnonymousExpressionFormatterResolver;
    public void Clear(){
        this.AnonymousExpressionJsonFormatterResolver.Clear();
        this.AnonymousExpressionFormatterResolver.Clear();
    }
    public readonly IJsonFormatterResolver JsonFormatterResolver;
    //public readonly MessagePack.Resolver MessagePack_Resolver;
    public readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    public SerializerSet(){
        var AnonymousExpressionResolver=this.AnonymousExpressionJsonFormatterResolver=new();
        this.JsonFormatterResolver =Utf8Json.Resolvers.CompositeResolver.Create(
            //順序が大事
            AnonymousExpressionResolver,
            //global::Utf8Json.Resolvers.DynamicObjectResolver.Default,//これが存在するとStackOverflowする
            Utf8Json.Resolvers.DynamicGenericResolver.Instance,
            //global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//これが存在するとTypeがシリアライズできない
            Utf8Json.Resolvers.StandardResolver.AllowPrivate
            //global::Utf8Json.Resolvers.StandardResolver.Default,
        );
        var AnonymousExpressionFormatterResolver=this.AnonymousExpressionFormatterResolver=new();
        this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
            MessagePack.Resolvers.CompositeResolver.Create(
                AnonymousExpressionFormatterResolver,
                //MessagePack.Resolvers.DynamicObjectResolver.Instance,//
                MessagePack.Resolvers.DynamicGenericResolver.Instance,
                //MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//
                MessagePack.Resolvers.StandardResolverAllowPrivate.Instance
                //MessagePack.Resolvers.StandardResolver.Instance,//
            )
        );
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
}
