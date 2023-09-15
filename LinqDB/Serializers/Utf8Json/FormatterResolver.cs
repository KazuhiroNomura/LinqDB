using System.Collections.Generic;
using LinqDB.Helpers;
using LinqDB.Serializers.Utf8Json.Formatters;
using Expressions = System.Linq.Expressions;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json;
internal sealed class FormatterResolver:IJsonFormatterResolver{
    //private readonly Type[] GenericArguments=new Type[1];
    public readonly Dictionary<System.Type,IJsonFormatter> DictionaryTypeFormatter=new();
    public IJsonFormatter<T> GetFormatter<T>(){
        if(this.DictionaryTypeFormatter.TryGetValue(typeof(T),out var IJsonFormatter))
            return(IJsonFormatter<T>)IJsonFormatter;
        if(typeof(T).IsDisplay()){
            return Return(DisplayClass<T>.Instance);
        }
        if(typeof(Expressions.LambdaExpression).IsAssignableFrom(typeof(T))){
            var FormatterType = typeof(ExpressionT<>).MakeGenericType(typeof(T));
            var Instance=FormatterType.GetField("Instance")!;
            var FormatterT=(IJsonFormatter<T>)Instance.GetValue(null)!;
            return Return(FormatterT);
        }
        return null!;
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
