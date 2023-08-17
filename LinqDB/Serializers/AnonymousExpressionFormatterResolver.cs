using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

using LinqDB.Helpers;
using LinqDB.Serializers.Formatters;

using MessagePack;
using MessagePack.Formatters;
//using MessagePack.Formatters;

using Utf8Json;
//using Utf8Json.Formatters;
namespace LinqDB.Serializers;
public sealed class AnonymousExpressionJsonFormatterResolver:IJsonFormatterResolver{
    public static readonly AnonymousExpressionJsonFormatterResolver Instance=new();
    private readonly ExpressionJsonFormatter ExpressionFormatter = new();
    private readonly Dictionary<Type,IJsonFormatter> Dictionary_Type_Formatter = new();
    private readonly Type[] GenericArguments=new Type[1];
    public IJsonFormatter<T> GetFormatter<T>(){
        if(this.Dictionary_Type_Formatter.TryGetValue(typeof(T),out var IJsonFormatter))return (IJsonFormatter<T>)IJsonFormatter;
        //if(typeof(Type).IsAssignableFrom(typeof(T))) return (IJsonFormatter<T>)(IJsonFormatter)Utf8Json.Formatters.TypeFormatter.Default;
        //if(typeof(Type).IsAssignableFrom(typeof(T))) return null!;
        if(typeof(Expression).IsAssignableFrom(typeof(T))){
            if(!typeof(T).IsSubclassOf(typeof(LambdaExpression))) return Return(this.ExpressionFormatter);
            var GenericArguments=this.GenericArguments;
            GenericArguments[0]=typeof(T);
            return Return(Activator.CreateInstance(typeof(ExpressionJsonFormatter<>).MakeGenericType(GenericArguments),this.ExpressionFormatter)!);
            //if(typeof(T).IsSubclassOf(typeof(LambdaExpression))){
            //    return Return(Activator.CreateInstance(typeof(ExpressionJsonFormatter<>).MakeGenericType(typeof(T)),this.ExpressionFormatter)!);
            //} else{
            //    return Return(this.ExpressionFormatter);
            //}
            //return Return(typeof(T).IsSubclassOf(typeof(LambdaExpression))
            //    ?Activator.CreateInstance(typeof(ExpressionJsonFormatter<>).MakeGenericType(typeof(T)),
            //        this.ExpressionFormatter)!
            //    :this.ExpressionFormatter);
        }
        if(
            typeof(MemberBinding).IsAssignableFrom(typeof(T))||
            typeof(T)==typeof(MethodInfo)||
            typeof(T)==typeof(MemberInfo)||
            typeof(T)==typeof(CatchBlock)||
            typeof(T)==typeof(SwitchCase)||
            typeof(T)==typeof(ElementInit))
            return Return(this.ExpressionFormatter);
        //else if(typeof(T)==typeof(ConstantExpression))Formatter=(IJsonFormatter<T>)new ConstantFormatter();
        //else if(typeof(T)==typeof(DefaultExpression ))Formatter=(IJsonFormatter<T>)new DefaultFormatter();
        //else if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))Formatter=(IJsonFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
        //else if(typeof(T)==typeof(DefaultExpression))
        //    Formatter=(IJsonFormatter<T>)new DefaultFormatter();
        //else if(typeof(T)==typeof(NewArrayExpression))Formatter=(IJsonFormatter<T>)new NewArrayFormatter();
        //else if(typeof(T).IsAnonymous())
        //    Formatter=new AbstractFormatter<T>();
        if(typeof(T).IsAnonymous())return Return(new AnonymousJsonFormatter<T>());
        if(!typeof(T).IsValueType&&!typeof(T).IsSealed)return Return(new AbstractJsonFormatter<T>());
        return null!;
        IJsonFormatter<T> Return(object Formatter){
            var result=(IJsonFormatter<T>)Formatter;
            this.Dictionary_Type_Formatter.Add(typeof(T),result);
            return result;
        }
        //else if(typeof(T).IsValueType||typeof(T).IsSealed||typeof(ITuple).IsAssignableFrom(typeof(T)))
        //    return null!;
        //else
        //    return this.Return<T>(this.AbstractFormatter);
    }
    public void Clear() {
        this.ExpressionFormatter.Clear();
        this.Dictionary_Type_Formatter.Clear();
    }
}
public sealed class AnonymousExpressionFormatterResolver:IFormatterResolver {
    public static readonly AnonymousExpressionFormatterResolver Instance=new();
    //public static readonly IJsonFormatterResolver Instance=new Resolver();
    //private readonly AbstractMessagePackFormatter AbstractFormatter= new();
    private readonly ExpressionMessagePackFormatter ExpressionFormatter=new();
    private readonly Dictionary<Type,IMessagePackFormatter> Dictionary_Type_Formatter = new();
    private readonly Type[] GenericArguments=new Type[1];
    public IMessagePackFormatter<T> GetFormatter<T>() {
        //if(typeof(T).IsSerializable) return null!;
        if(this.Dictionary_Type_Formatter.TryGetValue(typeof(T),out var IMessagePackFormatter))return (IMessagePackFormatter<T>)IMessagePackFormatter;
        //if(typeof(Type).IsAssignableFrom(typeof(T))){
        //    var GenericArguments=this.GenericArguments;
        //    GenericArguments[0]=typeof(T);
        //    return (IMessagePackFormatter<T>)typeof(TypeFormatter<>).MakeGenericType(GenericArguments).GetField("Instance").GetValue(null);
        //}
        if(typeof(Expression).IsAssignableFrom(typeof(T))){
            //if(typeof(LambdaExpression).IsAssignableFrom(typeof(T)))
            //    Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
            if(!typeof(T).IsSubclassOf(typeof(LambdaExpression))) return Return(this.ExpressionFormatter);
            var GenericArguments=this.GenericArguments;
            GenericArguments[0]=typeof(T);
            return Return(Activator.CreateInstance(typeof(ExpressionMessagePackFormatter<>).MakeGenericType(GenericArguments),this.ExpressionFormatter)!);
        }
        if(typeof(Expression).IsAssignableFrom(typeof(T))){
            return Return(typeof(T).IsSubclassOf(typeof(LambdaExpression))?Activator.CreateInstance(typeof(ExpressionMessagePackFormatter<>).MakeGenericType(typeof(T)),this.ExpressionFormatter)!:this.ExpressionFormatter);
        }
        if(
            typeof(MemberBinding).IsAssignableFrom(typeof(T))||
            typeof(T)==typeof(MethodInfo)||
            typeof(T)==typeof(MemberInfo)||
            typeof(T)==typeof(CatchBlock)||
            typeof(T)==typeof(SwitchCase)||
            typeof(T)==typeof(ElementInit))
            return Return(this.ExpressionFormatter);
        if(typeof(T).IsAnonymous())return Return(new AnonymousMessagePackFormatter<T>());
        if(!typeof(T).IsValueType&&!typeof(T).IsSealed)return Return(new AbstractMessagePackFormatter<T>());
        return null!;
        //else if(typeof(T).IsValueType||typeof(T).IsSealed||typeof(ITuple).IsAssignableFrom(typeof(T)))
        //    return null!;
        //else
        //    Formatter=this.AbstractFormatter;
        //else if(global::MessagePack.Resolvers.StandardResolver.Instance.GetFormatterDynamic(typeof(T))is not null)
        //    return null!;
        //else if(!typeof(T).IsValueType&&!typeof(T).IsSealed)
          //  Formatter=new AbstractFormatter<T>();
        //Formatter=global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<T>();// new AbstractFormatter<T>();
        //else//  throw new NotSupportedException(typeof(T).FullName);
            //return null!;//MessagePack.Resolver.Instance.GetFormatter<T>();
        //this.Dictionary_Type_IMessagePackFormatter.Add(typeof(T),Formatter);
        //return (IMessagePackFormatter<T>)Formatter;
        IMessagePackFormatter<T> Return(object Formatter){
            var result=(IMessagePackFormatter<T>)Formatter;
            this.Dictionary_Type_Formatter.Add(typeof(T),result);
            return result;
        }
    }
    public void Clear() {
        this.ExpressionFormatter.Clear();
        this.Dictionary_Type_Formatter.Clear();
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
