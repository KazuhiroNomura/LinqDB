//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Reflection;
//using LinqDB.Helpers;
//using LinqDB.Serializers.Formatters;
//using MessagePack;
//using MessagePack.Formatters;
//namespace LinqDB.Serializers.MessagePack;
//public class Resolver:IFormatterResolver{
//    //public static readonly IFormatterResolver Instance=new Resolver();
//    private readonly ExpressionFormatter ExpressionFormatter=new();
//    private readonly AbstractFormatter AbstractFormatter=new();
//    private readonly Dictionary<Type,IMessagePackFormatter> Dictionary_Type_IMessagePackFormatter=new();
//    public IMessagePackFormatter<T> GetFormatter<T>(){
//        if(this.Dictionary_Type_IMessagePackFormatter.TryGetValue(typeof(T),out var IMessagePackFormatter))
//            return (IMessagePackFormatter<T>)IMessagePackFormatter;
//        IMessagePackFormatter Formatter;
//        if(typeof(Expression).IsAssignableFrom(typeof(T)))
//            //if(typeof(LambdaExpression).IsAssignableFrom(typeof(T)))
//            //    Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
//            if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))
//                Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),this.ExpressionFormatter)!;
//            else
//                Formatter=(IMessagePackFormatter<T>)this.ExpressionFormatter;
//        else if(
//            typeof(MemberBinding).IsAssignableFrom(typeof(T))||
//            typeof(T)==typeof(MethodInfo)||
//            typeof(T)==typeof(MemberInfo)||
//            typeof(T)==typeof(CatchBlock)||
//            typeof(T)==typeof(SwitchCase)||
//            typeof(T)==typeof(ElementInit))
//            Formatter=(IMessagePackFormatter<T>)this.ExpressionFormatter;
//        //else if(typeof(T)==typeof(ConstantExpression))Formatter=(IMessagePackFormatter<T>)new ConstantFormatter();
//        //else if(typeof(T)==typeof(DefaultExpression ))Formatter=(IMessagePackFormatter<T>)new DefaultFormatter();
//        //else if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
//        //else if(typeof(T)==typeof(DefaultExpression))
//        //    Formatter=(IMessagePackFormatter<T>)new DefaultFormatter();
//        //else if(typeof(T)==typeof(NewArrayExpression))Formatter=(IMessagePackFormatter<T>)new NewArrayFormatter();
//        //else if(!typeof(T).IsValueType&&!typeof(T).IsSealed)
//        else if(typeof(T).IsAnonymous())
//            Formatter=this.AbstractFormatter;
//        else
//            return null!;//MessagePack.Resolver.Instance.GetFormatter<T>();
//        this.Dictionary_Type_IMessagePackFormatter.Add(typeof(T),Formatter);
//        return (IMessagePackFormatter<T>)Formatter;
//    }
//    public void Clear(){
//        this.ExpressionFormatter.Clear();
//    }
//}












