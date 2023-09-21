using Expressions = System.Linq.Expressions;
using LinqDB.Helpers;

using MessagePack;
using MessagePack.Formatters;
using LinqDB.Serializers.MessagePack.Formatters.Others;

namespace LinqDB.Serializers.MessagePack;
internal sealed class FormatterResolver:IFormatterResolver {
    public static readonly FormatterResolver Instance = new();
    //public readonly Dictionary<System.Type,IMessagePackFormatter> DictionaryTypeFormatter = new();
    public IMessagePackFormatter<T> GetFormatter<T>() {
        var type=typeof(T);
        if(type.IsDisplay()){
            //if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter)) return(IMessagePackFormatter<T>)Formatter;
            var FormatterType = typeof(DisplayClass<>).MakeGenericType(type);
            var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
            var FormatterT=(IMessagePackFormatter<T>)Instance.GetValue(null)!;
            //this.DictionaryTypeFormatter.Add(type,FormatterT);
            return FormatterT;
        }
        if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)){
            //if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter)) return(IMessagePackFormatter<T>)Formatter;
            var FormatterType = typeof(Formatters.ExpressionT<>).MakeGenericType(type);
            var Instance=FormatterType.GetField("Instance")!;
            var FormatterT=(IMessagePackFormatter<T>)Instance.GetValue(null)!;
            //this.DictionaryTypeFormatter.Add(type,FormatterT);
            return FormatterT;

        }
        return default!;
    }
    //public void Clear() {
    //    this.DictionaryTypeFormatter.Clear();
    //}
}
