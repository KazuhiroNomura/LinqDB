using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Optimizers.Comparer;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.VoidExpressionTraverser;
internal sealed class 判定_InstanceMethodか:VoidExpressionTraverser {
    private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="ExpressionEqualityComparer"></param>
    public 判定_InstanceMethodか(ExpressionEqualityComparer ExpressionEqualityComparer){
        this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    }
    internal IReadOnlyDictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant=default!;
    internal IReadOnlyDictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter=default!;
    internal IEnumerable<LambdaExpression> Lambdas=default!;
    private bool InstanceMethodか;
    public bool 実行(Expression e) {
        this.InstanceMethodか=false;
        this.Traverse(e);
        return this.InstanceMethodか;
    }
    protected override void Constant(ConstantExpression Constant)=>this.InstanceMethodか|=this.DictionaryConstant.ContainsKey(Constant);
    protected override void Dynamic(DynamicExpression Dynamic)=>this.InstanceMethodか=true;
    protected override void Parameter(ParameterExpression Parameter)=>this.InstanceMethodか|=this.Dictionaryラムダ跨ぎParameter.ContainsKey(Parameter);
    protected override void Lambda(LambdaExpression Lambda) {
        if(this.Lambdas.Contains(Lambda,this.ExpressionEqualityComparer))
            this.InstanceMethodか=true;
        else
            Debug.Fail("ありえない");
    }
}

