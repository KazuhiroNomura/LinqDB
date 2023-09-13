﻿using System.Collections.Generic;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using LinqDB.Helpers;
using LinqDB.Serializers.Formatters;
using LinqDB.Serializers.MemoryPack.Formatters;
using LinqDB.Serializers.MessagePack.Formatters;

using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack;
internal sealed class FormatterResolver:IFormatterResolver {
    public static readonly FormatterResolver Instance = new();
    public readonly Dictionary<System.Type,IMessagePackFormatter> DictionaryTypeFormatter = new();
    public readonly object[] objects1=new object[1];
    public IMessagePackFormatter<T> GetFormatter<T>() {
        //if(this.DictionaryTypeFormatter.TryGetValue(typeof(T),out var Formatter)) return (IMessagePackFormatter<T>)Formatter;
        //var FormatterType = typeof(DisplayClass<>).MakeGenericType(typeof(T));
        //var Instance = FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
        //this.DictionaryTypeFormatter.Add(typeof(T),(IMessagePackFormatter)Instance.GetValue(null)!);
        var type=typeof(T);
        if(type.IsDisplay()){
            if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter)) return(IMessagePackFormatter<T>)Formatter;
            var FormatterType = typeof(Formatters.DisplayClass<>).MakeGenericType(type);
            var Instance=FormatterType.GetField(nameof(Formatters.DisplayClass<int>.Instance))!;
            var FormatterT=(IMessagePackFormatter<T>)Instance.GetValue(null)!;
            this.DictionaryTypeFormatter.Add(type,FormatterT);
            return FormatterT;
        }
        if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)){
            if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter)) return(IMessagePackFormatter<T>)Formatter;
            var FormatterType = typeof(Formatters.ExpressionT<>).MakeGenericType(type);
            var Instance=FormatterType.GetField("Instance")!;
            var FormatterT=(IMessagePackFormatter<T>)Instance.GetValue(null)!;
            this.DictionaryTypeFormatter.Add(type,FormatterT);
            return FormatterT;

        }
        return default!;
    }
    public void Clear() {
        this.DictionaryTypeFormatter.Clear();
    }
}
