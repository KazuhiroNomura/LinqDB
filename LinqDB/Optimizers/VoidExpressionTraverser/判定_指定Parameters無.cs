using System.Collections.Generic;
using System.Linq.Expressions;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.VoidExpressionTraverser;
using static Common;

internal sealed class 判定_指定Parameters無:VoidExpressionTraverser {
    //そのラムダパラメータ以外のみ式は移動することが出来る。
    //例えば、
    //p3=>(p1+p2)+p3から"p1+p2"は移動できる。
    //p3=>p1+p2+3から"p1+p2+3"は移動できる。
    //p3=>p1+p2から"p1+p2"は移動できる。
    //p3=>p1+3から"p1+3"は移動できる。
    //p3=>p2+3から"p2+3"は移動できる。
    //p3=>p1から"p1"は移動できる。
    //p3=>p2から"p2"は移動できる。
    //p3=>3から"3"は移動できる。
    private IList<ParameterExpression>? 指定Parameters;
    private bool 指定Parameters無;
    //パラメータが全て使われていて、別のパラメータがなかったらtrue
    //パラメータが全て使われていて、別のパラメータがあったらfalse
    //パラメータが一部使われていて、別のパラメータがなかったらtrue
    //パラメータが一部使われていて、別のパラメータがあったらfalse
    public bool 実行(Expression e,IList<ParameterExpression> 指定Parameters) {
        this.指定Parameters=指定Parameters;
        this.指定Parameters無=true;
        this.Traverse(e);
        return this.指定Parameters無;
    }
    protected override void Parameter(ParameterExpression Parameter) {
        if(this.指定Parameters!.Contains(Parameter))
            this.指定Parameters無=false;
    }
    protected override void Call(MethodCallExpression MethodCall) {
        var MethodCall0_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall.Method);
        if(
            Reflection.Helpers.NoEarlyEvaluation==MethodCall0_GenericMethodDefinition||
            Reflection.Helpers.NoLoopUnrolling==MethodCall0_GenericMethodDefinition
        ) {
            this.指定Parameters無=false;
            return;
        }
        base.Call(MethodCall);
    }
}
