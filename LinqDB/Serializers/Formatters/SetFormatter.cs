//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using MessagePack;
//using MessagePack.Formatters;
//using Utf8Json;
//namespace LinqDB.Serializers.Formatters;
//public class SetFormatter:IJsonFormatter<Expression>,IMessagePackFormatter<Expression>{
//    //public static ExpressionFormatter Instance{get;private set;}
//    public static readonly ExpressionFormatter Instance=new();
//    internal readonly List<ParameterExpression> ListParameter=new();
//    private readonly Dictionary<LabelTarget,int> Dictionary_LabelTarget_int=new();
//    private readonly Dictionary<int,LabelTarget> Dictionary_int_LabelTarget=new();
//    private IJsonFormatter<Expression> JExpression=>this;
//    private IMessagePackFormatter<Expression> MSExpression=>this;
//    public static int count=0;
//    public SetFormatter(){
//        count++;
//    }
//    //internal static void Create(List<ParameterExpression> ListParameter,Dictionary<LabelTarget,int> Dictionary_LabelTarget_int,Dictionary<int,LabelTarget> Dictionary_int_LabelTarget){
//    //    Instance=new(ListParameter,Dictionary_LabelTarget_int,Dictionary_int_LabelTarget);
//    //}
//    //private ExpressionFormatter(List<ParameterExpression> ListParameter,Dictionary<LabelTarget,int> Dictionary_LabelTarget_int,Dictionary<int,LabelTarget> Dictionary_int_LabelTarget){
//    //    this.ListParameter=ListParameter;
//    //    this.Dictionary_LabelTarget_int=Dictionary_LabelTarget_int;
//    //    this.Dictionary_int_LabelTarget=Dictionary_int_LabelTarget;
//    //}
//    //private static void Serialize<T>(ref JsonWriter writer,T value,IJsonFormatterResolver Resolver){
//    //    Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
//    //}
//    public void Serialize(ref JsonWriter writer,Expression value,IJsonFormatterResolver formatterResolver){
//        throw new NotImplementedException();
//    }
//    public Expression Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
//        throw new NotImplementedException();
//    }
//    public void Serialize(ref MessagePackWriter writer,Expression value,MessagePackSerializerOptions options){
//        throw new NotImplementedException();
//    }
//    public Expression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
//        throw new NotImplementedException();
//    }
//}
