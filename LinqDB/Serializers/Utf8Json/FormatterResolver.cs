using System.Collections.Generic;
using LinqDB.Helpers;
using LinqDB.Serializers.Utf8Json.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json;
internal sealed class FormatterResolver:IJsonFormatterResolver{
    //private readonly Type[] GenericArguments=new Type[1];
    public readonly Dictionary<System.Type,IJsonFormatter> DictionaryTypeFormatter=new();
    public IJsonFormatter<T> GetFormatter<T>(){
        if(this.DictionaryTypeFormatter.TryGetValue(typeof(T),out var IJsonFormatter))
            return(IJsonFormatter<T>)IJsonFormatter;
        if(typeof(System.Linq.Expressions.Expression).IsAssignableFrom(typeof(T))){
            //return Return(Instance.ExpressionFormatter);
            return null!;
        }
        //if(
        //    typeof(T)==typeof(System.Type)||
        //    typeof(T)==typeof(MemberInfo)||
        //    typeof(T)==typeof(MethodInfo)||
        //    typeof(T)==typeof(FieldInfo)||
        //    typeof(T)==typeof(PropertyInfo)||
        //    typeof(T)==typeof(EventInfo)||
        //    typeof(Expressions.MemberBinding).IsAssignableFrom(typeof(T))||
        //    typeof(T)==typeof(MethodInfo)||
        //    typeof(T)==typeof(MemberInfo)||
        //    typeof(T)==typeof(Expressions.CatchBlock)||
        //    typeof(T)==typeof(Expressions.SwitchCase)||
        //    typeof(T)==typeof(Expressions.ElementInit))
        //    return null!;
        //return Return(Instance.ExpressionFormatter);
        if(typeof(T).IsDisplay()){
            var Formatter=new DisplayClass<T>();
            Serializer.Instance.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
                new IJsonFormatter[]{Formatter},
                new []{Serializer.Instance.IResolver}
            );
            return Formatter;
        }
        //if(typeof(T).IsAnonymous()){
        //    var Formatter=new Anonymous<T>();
        //    Instance.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
        //        new IJsonFormatter[]{Formatter},
        //        new []{Instance.IResolver}
        //    );
        //    return Formatter;
        //}
        //var Formatter=GetFormatter(typeof(T));
        //if(Formatter!=null) Return(Formatter);
        return Return(new Abstract<T>());
        IJsonFormatter<T> Return(object Formatter){
            var result=(IJsonFormatter<T>)Formatter;
            this.DictionaryTypeFormatter.Add(typeof(T),result);
            return result;
        }
    }
    public void Clear(){
        //this.ExpressionFormatter.Clear();
        this.DictionaryTypeFormatter.Clear();
    }
}
