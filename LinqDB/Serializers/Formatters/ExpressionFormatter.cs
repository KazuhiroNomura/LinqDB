using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
public partial class ExpressionFormatter:IJsonFormatter<Expression>,IMessagePackFormatter<Expression>{
    //public static ExpressionFormatter Instance{get;private set;}
    //public static readonly ExpressionFormatter Instance=new();
    internal readonly List<ParameterExpression> ListParameter=new();
    private readonly Dictionary<LabelTarget,int> Dictionary_LabelTarget_int=new();
    private readonly Dictionary<int,LabelTarget> Dictionary_int_LabelTarget=new();
    public void Clear(){
        this.ListParameter.Clear();
        this.Dictionary_LabelTarget_int.Clear();
        this.Dictionary_int_LabelTarget.Clear();
    }
    private IJsonFormatter<Expression> JExpression=>this;
    private IMessagePackFormatter<Expression> MSExpression=>this;
}
