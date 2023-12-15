using System.Diagnostics;
using System.Linq.Expressions;
//using Microsoft.CSharp.RuntimeBinder;
using Microsoft.CSharp.RuntimeBinder;
// ReSharper disable All
namespace LinqDB.Optimizers.Comparer;
using Generic = System.Collections.Generic;
/// <summary>
/// 代入左辺値に未定義のParameterが出現したら同じデータ型ならtrue
/// 評価値に未定義のParameterが出現したらfalse
/// </summary>
public class ExpressionEqualityComparer:AExpressionEqualityComparer
    ,Generic.IEqualityComparer<Expression>
    ,Generic.IEqualityComparer<ParameterExpression>
    ,Generic.IEqualityComparer<LabelTarget>
    ,Generic.IEqualityComparer<CatchBlock>
    ,Generic.IEqualityComparer<CSharpArgumentInfo>
    ,Generic.IEqualityComparer<SwitchCase>
    ,Generic.IEqualityComparer<MemberBinding>
    ,Generic.IEqualityComparer<MemberAssignment>
    ,Generic.IEqualityComparer<MemberListBinding>
    ,Generic.IEqualityComparer<MemberMemberBinding>
    ,Generic.IEqualityComparer<ElementInit>
    ,Generic.IEqualityComparer<SymbolDocumentInfo>{
    internal Generic.List<ParameterExpression> x_ラムダ跨ぎParameters=new();
    internal Generic.List<ParameterExpression> y_ラムダ跨ぎParameters=new();
    public ExpressionEqualityComparer(){

    }
    internal override void Clear(){
        this.x_ラムダ跨ぎParameters.Clear();
        this.y_ラムダ跨ぎParameters.Clear();
        base.Clear();
    }
    protected override bool ProtectedAssign後処理(ParameterExpression x,ParameterExpression y){
        this.x_ラムダ跨ぎParameters.Add(x);
        this.y_ラムダ跨ぎParameters.Add(y);
        //Let(x=>.v),Let(y=>.w)は一致しない。
        //Let(x=>.v=x),Let(y=>.w=y)は一致しない。
        //Let(x=>.v),Let(y=>.v)は一致する。
        //Let(x=>.v=x),Let(y=>.v=y)は一致する。
        //代入先が始めて出現した宣言してないParameterだった場合始めて出現したという意味で一致する
        return x.GetType()==y.GetType();//true;
    }
    protected override bool Equals後処理(ParameterExpression x,ParameterExpression y){
        return @false;
    }
    protected override bool T(LambdaExpression x,LambdaExpression y){
        if(x.Type!=y.Type||x.TailCall!=y.TailCall) return @false;
        var x_ラムダ跨ぎParameters=this.x_ラムダ跨ぎParameters;
        var y_ラムダ跨ぎParameters=this.y_ラムダ跨ぎParameters;
        this.x_ラムダ跨ぎParameters=new();
        this.y_ラムダ跨ぎParameters=new();
        var Lambdx_x_Parameters=x.Parameters;
        var Lambdx_y_Parameters=y.Parameters;
        var Lambdx_x_Parameters_Count=Lambdx_x_Parameters.Count;
        if(Lambdx_x_Parameters_Count!=Lambdx_y_Parameters.Count) return @false;
        var x_Parameters=this.x_Parameters;
        var y_Parameters=this.y_Parameters;
        var x_Parameters_Count=x_Parameters.Count;
        Debug.Assert(x_Parameters_Count==y_Parameters.Count);
        x_Parameters.AddRange(Lambdx_x_Parameters);
        y_Parameters.AddRange(Lambdx_y_Parameters);
        Debug.Assert(this.x_LabelTargets.Count==this.y_LabelTargets.Count);
        //var count=this.count++;
        var r=this.ProtectedEquals(x.Body,y.Body);
        Debug.Assert(this.x_LabelTargets.Count==this.y_LabelTargets.Count);
        x_Parameters.RemoveRange(x_Parameters_Count,Lambdx_x_Parameters_Count);
        y_Parameters.RemoveRange(x_Parameters_Count,Lambdx_x_Parameters_Count);
        this.x_ラムダ跨ぎParameters=x_ラムダ跨ぎParameters;
        this.y_ラムダ跨ぎParameters=y_ラムダ跨ぎParameters;
        return r;
    }
    //public bool Equals(Expression? x,Expression? y) {
    //    if(x==y) return true;
    //    if(x==null&&y!=null||x!=null&&y==null) return @false;
    //    this.Clear();
    //    return this.InternalEquals(x,y);
    //}
    public bool Equals(SymbolDocumentInfo? x,SymbolDocumentInfo? y){
        this.Clear();
        return this.lnternalEquals(x,y);
    }
    public bool Equals(LabelTarget? x,LabelTarget? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    //public bool Equals(ElementInit? x,ElementInit? y) {
    //    this.Clear();
    //    return this.InternalEquals(x,y);
    //}
    public bool Equals(ElementInit? x,ElementInit? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberBinding? x,MemberBinding? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberAssignment? x,MemberAssignment? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberListBinding? x,MemberListBinding? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberMemberBinding? x,MemberMemberBinding? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(CSharpArgumentInfo? x,CSharpArgumentInfo? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(CatchBlock? x,CatchBlock? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(SwitchCase? x,SwitchCase? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
}
