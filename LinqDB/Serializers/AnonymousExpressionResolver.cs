using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using LinqDB.Helpers;
using LinqDB.Serializers.Formatters;

using MessagePack;
using MessagePack.Formatters;

using Utf8Json;
namespace LinqDB.Serializers;
public sealed class AnonymousExpressionResolver:IJsonFormatterResolver, IFormatterResolver {
    //public static readonly IJsonFormatterResolver Instance=new Resolver();
    private readonly ExpressionFormatter ExpressionFormatter = new();
    //private readonly AbstractFormatter AbstractFormatter = new();
    private readonly Dictionary<Type,IJsonFormatter> Dictionary_Type_IJsonFormatter = new();
    IJsonFormatter<T> IJsonFormatterResolver.GetFormatter<T>() {
        if(this.Dictionary_Type_IJsonFormatter.TryGetValue(typeof(T),out var IJsonFormatter))
            return (IJsonFormatter<T>)IJsonFormatter;
        IJsonFormatter Formatter;
        if(typeof(Expression).IsAssignableFrom(typeof(T)))
            //if(typeof(LambdaExpression).IsAssignableFrom(typeof(T)))
            //    Formatter=(IJsonFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
            if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))
                Formatter=(IJsonFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),this.ExpressionFormatter)!;
            else
                Formatter=(IJsonFormatter<T>)this.ExpressionFormatter;
        else if(
            typeof(MemberBinding).IsAssignableFrom(typeof(T))||
            typeof(T)==typeof(MethodInfo)||
            typeof(T)==typeof(MemberInfo)||
            typeof(T)==typeof(CatchBlock)||
            typeof(T)==typeof(SwitchCase)||
            typeof(T)==typeof(ElementInit))
            Formatter=(IJsonFormatter<T>)this.ExpressionFormatter;
        //else if(typeof(T)==typeof(ConstantExpression))Formatter=(IJsonFormatter<T>)new ConstantFormatter();
        //else if(typeof(T)==typeof(DefaultExpression ))Formatter=(IJsonFormatter<T>)new DefaultFormatter();
        //else if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))Formatter=(IJsonFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
        //else if(typeof(T)==typeof(DefaultExpression))
        //    Formatter=(IJsonFormatter<T>)new DefaultFormatter();
        //else if(typeof(T)==typeof(NewArrayExpression))Formatter=(IJsonFormatter<T>)new NewArrayFormatter();
        else if(typeof(T).IsAnonymous())
            Formatter=new AbstractFormatter<T>();
        else if(!typeof(T).IsValueType&&!typeof(T).IsSealed)
            Formatter=new AbstractFormatter<T>();
        else if(global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatterDynamic(typeof(T))is not null)
            return null!;
        else
            throw new NotSupportedException(typeof(T).FullName);
        //else if(!typeof(T).IsValueType&&!typeof(T).IsSealed)
        //    Formatter=new AbstractFormatter<T>();
        //else if(typeof(T).IsAnonymous()) 
        //    Formatter=global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<T>();
        //    //Formatter=new AbstractFormatter<T>();
        //else
        //    return null!;//MessagePack.Resolver.Instance.GetFormatter<T>();
        this.Dictionary_Type_IJsonFormatter.Add(typeof(T),Formatter);
        return (IJsonFormatter<T>)Formatter;
    }
    private readonly Dictionary<Type,IMessagePackFormatter> Dictionary_Type_IMessagePackFormatter = new();
    IMessagePackFormatter<T> IFormatterResolver.GetFormatter<T>() {
        if(this.Dictionary_Type_IMessagePackFormatter.TryGetValue(typeof(T),out var IMessagePackFormatter))
            return (IMessagePackFormatter<T>)IMessagePackFormatter;
        IMessagePackFormatter Formatter;
        if(typeof(Expression).IsAssignableFrom(typeof(T)))
            //if(typeof(LambdaExpression).IsAssignableFrom(typeof(T)))
            //    Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
            if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))
                Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),this.ExpressionFormatter)!;
            else
                Formatter=(IMessagePackFormatter<T>)this.ExpressionFormatter;
        else if(
            typeof(MemberBinding).IsAssignableFrom(typeof(T))||
            typeof(T)==typeof(MethodInfo)||
            typeof(T)==typeof(MemberInfo)||
            typeof(T)==typeof(CatchBlock)||
            typeof(T)==typeof(SwitchCase)||
            typeof(T)==typeof(ElementInit))
            Formatter=(IMessagePackFormatter<T>)this.ExpressionFormatter;
        //else if(typeof(T)==typeof(ConstantExpression))Formatter=(IMessagePackFormatter<T>)new ConstantFormatter();
        //else if(typeof(T)==typeof(DefaultExpression ))Formatter=(IMessagePackFormatter<T>)new DefaultFormatter();
        //else if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
        //else if(typeof(T)==typeof(DefaultExpression))
        //    Formatter=(IMessagePackFormatter<T>)new DefaultFormatter();
        //else if(typeof(T)==typeof(NewArrayExpression))Formatter=(IMessagePackFormatter<T>)new NewArrayFormatter();
        else if(typeof(T).IsAnonymous())
            Formatter=new AbstractFormatter<T>();
        else if(!typeof(T).IsValueType&&!typeof(T).IsSealed)
            Formatter=new AbstractFormatter<T>();
        else if(global::MessagePack.Resolvers.StandardResolver.Instance.GetFormatterDynamic(typeof(T))is not null)
            return null!;
        //else if(!typeof(T).IsValueType&&!typeof(T).IsSealed)
          //  Formatter=new AbstractFormatter<T>();
        //Formatter=global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<T>();// new AbstractFormatter<T>();
        else
            throw new NotSupportedException(typeof(T).FullName);
            //return null!;//MessagePack.Resolver.Instance.GetFormatter<T>();
        this.Dictionary_Type_IMessagePackFormatter.Add(typeof(T),Formatter);
        return (IMessagePackFormatter<T>)Formatter;
    }
    public void Clear() {
        this.ExpressionFormatter.Clear();
        this.Dictionary_Type_IJsonFormatter.Clear();
        this.Dictionary_Type_IMessagePackFormatter.Clear();
    }
    //public IJsonFormatter<T> GetFormatter<T>(){
    //    //if(typeof(LambdaExpression).IsSubclassOf(typeof(T))){
    //    //    return (IJsonFormatter<T>)new ExpressionFormatter();
    //    //}
    //    if(typeof(T).IsSubclassOf(typeof(LambdaExpression))){
    //        return (IJsonFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
    //    }
    //    return null!;
    //}
}
