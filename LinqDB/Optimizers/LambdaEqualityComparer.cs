using System.Diagnostics;
using System.Linq.Expressions;
//using Microsoft.CSharp.RuntimeBinder;
using SQLServer = Microsoft.SqlServer.TransactSql.ScriptDom;
// ReSharper disable All
namespace LinqDB.Optimizers;
using Generic=System.Collections.Generic;
public class LambdaEqualityComparer:AExpressionEqualityComparer{
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
        //Let(x=>.v),Let(y=>.w)は一致しない。
        //Let(x=>.v=x),Let(y=>.w=y)は一致しない。
        //Let(x=>.v),Let(y=>.v)は一致する。
        //Let(x=>.v=x),Let(y=>.v=y)は一致する。
        //代入先が宣言してないParameterだった場合一致しない
        return @false;
    }
    public override bool Equals(ParameterExpression? x,ParameterExpression? y){
        var x_Index0 = this.x_Parameters.IndexOf(x);
        var y_Index0 = this.y_Parameters.IndexOf(y);
        if(x_Index0!=y_Index0) return @false;
        if(x_Index0>=0) return true;
        return @false;
        //return x==y;
    }
    protected override bool T(LambdaExpression x,LambdaExpression y){
        if(x.Type!=y.Type||x.TailCall!=y.TailCall) return @false;
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
        return r;
    }
}
