using System.Diagnostics;
using System.Linq.Expressions;
//using Microsoft.CSharp.RuntimeBinder;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.CSharp.RuntimeBinder;
// ReSharper disable All
namespace LinqDB.Optimizers;
using Generic=System.Collections.Generic;
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
    internal Generic.List<ParameterExpression> x_ラムダ跨ぎParameters= new();
    internal Generic.List<ParameterExpression> y_ラムダ跨ぎParameters= new();
    protected override void Clear(){
        this.x_ラムダ跨ぎParameters.Clear();
        this.y_ラムダ跨ぎParameters.Clear();
        base.Clear();
    }
    protected override bool ProtectedAssign(BinaryExpression x_Assign,BinaryExpression y_Assign){
        var x_Left = x_Assign.Left;
        var y_Left = y_Assign.Left;
        if(x_Left.NodeType!=y_Left.NodeType) return @false;
        if(x_Left.NodeType!=ExpressionType.Parameter) return this.T(x_Assign,y_Assign);
        if(!this.ProtectedEquals(x_Assign.Right,y_Assign.Right))
            return @false;
        if(!this.InternalEquals(x_Assign.Conversion,y_Assign.Conversion))
            return @false;
        var x_Parameter= (ParameterExpression)x_Left;
        var y_Parameter= (ParameterExpression)y_Left;
        var x_Index0 = this.x_Parameters.IndexOf(x_Parameter);
        var y_Index0 = this.y_Parameters.IndexOf(y_Parameter);
        if(x_Index0!=y_Index0) return @false;
        if(x_Index0>=0) return true;
        //a=1,b=1はtrue
        //var a;a=1,var b;b=1はtrue
        var x_ラムダ跨ぎParameters = this.x_ラムダ跨ぎParameters;
        var y_ラムダ跨ぎParameters = this.y_ラムダ跨ぎParameters;
        var x_Index1 = x_ラムダ跨ぎParameters.IndexOf(x_Parameter);
        var y_Index1 = y_ラムダ跨ぎParameters.IndexOf(y_Parameter);
        if(x_Index1!=y_Index1) return @false;
        if(x_Index1>=0) return true;
        x_ラムダ跨ぎParameters.Add(x_Parameter);
        y_ラムダ跨ぎParameters.Add(y_Parameter);
        //Let(x=>.v),Let(y=>.w)は一致しない。
        //Let(x=>.v=x),Let(y=>.w=y)は一致しない。
        //Let(x=>.v),Let(y=>.v)は一致する。
        //Let(x=>.v=x),Let(y=>.v=y)は一致する。
        //代入先が始めて出現した宣言してないParameterだった場合始めて出現したという意味で一致する
        return x_Left.GetType()==y_Left.GetType(); //true;
    }
    public override bool Equals(ParameterExpression? x,ParameterExpression? y){
        var x_Index0 = this.x_Parameters.IndexOf(x);
        var y_Index0 = this.y_Parameters.IndexOf(y);
        if(x_Index0!=y_Index0) return @false;
        if(x_Index0>=0) return true;
        //Let(x=>.v),Let(y=>.w)は一致しない。
        //Let(x=>.v=x),Let(y=>.w=y)は一致しない。
        //Let(x=>.v),Let(y=>.v)は一致する。
        //Let(x=>.v=x),Let(y=>.v=y)は一致する。
        var x_ラムダ跨ぎParameters = this.x_ラムダ跨ぎParameters;
        var y_ラムダ跨ぎParameters = this.y_ラムダ跨ぎParameters;
        var x_Index1 = x_ラムダ跨ぎParameters.IndexOf(x);
        var y_Index1 = y_ラムダ跨ぎParameters.IndexOf(y);
        if(x_Index1!=y_Index1) return @false;
        if(x_Index1>=0){
            return x==y;
        }
        x_ラムダ跨ぎParameters.Add(x);
        y_ラムダ跨ぎParameters.Add(y);
        //return true;
        return @false;
    }
    protected override bool T(LambdaExpression x,LambdaExpression y){
        if(x.Type!=y.Type||x.TailCall!=y.TailCall) return @false;
        var x_ラムダ跨ぎParameters=this.x_ラムダ跨ぎParameters;
        var y_ラムダ跨ぎParameters=this.y_ラムダ跨ぎParameters;
        this.x_ラムダ跨ぎParameters=new();
        this.y_ラムダ跨ぎParameters=new();
        var Lambdx_x_Parameters = x.Parameters;
        var Lambdx_y_Parameters = y.Parameters;
        var Lambdx_x_Parameters_Count = Lambdx_x_Parameters.Count;
        if(Lambdx_x_Parameters_Count!=Lambdx_y_Parameters.Count) return @false;
        var x_Parameters = this.x_Parameters;
        var y_Parameters = this.y_Parameters;
        var x_Parameters_Count = x_Parameters.Count;
        Debug.Assert(x_Parameters_Count==y_Parameters.Count);
        x_Parameters.AddRange(Lambdx_x_Parameters);
        y_Parameters.AddRange(Lambdx_y_Parameters);
        Debug.Assert(this.x_LabelTargets.Count==this.y_LabelTargets.Count);
        //var count=this.count++;
        var r = this.ProtectedEquals(x.Body,y.Body);
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
    public bool Equals(ElementInit? x,ElementInit? y) {
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberBinding? x,MemberBinding? y) {
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberAssignment? x,MemberAssignment? y) {
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberListBinding? x,MemberListBinding? y) {
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(MemberMemberBinding? x,MemberMemberBinding? y) {
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(CSharpArgumentInfo? x,CSharpArgumentInfo? y) {
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(CatchBlock? x,CatchBlock? y) {
        this.Clear();
        return this.InternalEquals(x,y);
    }
    public bool Equals(SwitchCase? x,SwitchCase? y){
        this.Clear();
        return this.InternalEquals(x,y);
    }
}
