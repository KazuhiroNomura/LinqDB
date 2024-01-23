using LinqDB.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
// ReSharper disable All
namespace LinqDB.Optimizers.VoidExpressionTraverser;
using static Common;
internal class 作成_DynamicAssembly:A作成_IL{
    public 作成_DynamicAssembly(判定_InstanceMethodか 判定_InstanceMethodか) :base(判定_InstanceMethodか){
    }
    /// <summary>
    /// ILを生成する
    /// </summary>
    /// <param name="Lambda"></param>
    /// <param name="DispParameter"></param>
    /// <param name="DictionaryConstant"></param>
    /// <param name="DictionaryDynamic"></param>
    /// <param name="DictionaryLambda"></param>
    /// <param name="Dictionaryラムダ跨ぎParameter"></param>
    internal void Impl作成(LambdaExpression Lambda,ParameterExpression DispParameter,
        Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant,
        Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic,
        Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)> DictionaryLambda,
        Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter){
        //ContainerParameterはパラメーターに含まれていない
        Debug.Assert(DispParameter is not null);
        this.DispParameter=DispParameter;
        var DispType=DispParameter.Type;
        Debug.Assert(DispType is not null);
        this.DictionaryConstant=DictionaryConstant;
        this.DictionaryDynamic=DictionaryDynamic;
        this.DictionaryLambda=DictionaryLambda;
        this.Dictionaryラムダ跨ぎParameter=Dictionaryラムダ跨ぎParameter;
        Debug.Assert(DispParameter is not null);
        var 番号=0;
        foreach(var a in DictionaryConstant.AsEnumerable()){
            Debug.Assert($"Constant{番号}"==a.Value.Disp.Name);
            番号++;
            DictionaryConstant[a.Key]=(
                a.Value.Disp,
                Expression.Field(
                    DispParameter,
                    DispType.GetField(a.Value.Disp.Name,Instance_NonPublic_Public)!
                )
            );
        }
        //Dictionaryラムダ跨ぎParameter.Add(ContainerParameter,(ContainerField,Expression.Field(DispParameter,ContainerField)));
        var 跨番号=0;
        foreach(var a in Dictionaryラムダ跨ぎParameter.AsEnumerable()){
            Debug.Assert(a.Key.Name is null||a.Key.Name==a.Value.Disp.Name);
            Dictionaryラムダ跨ぎParameter[a.Key]=(DispType.GetField(a.Key.Name??$"[跨]{跨番号++}",Instance_NonPublic_Public),a.Value.Member)!;
        }
        this.Clear();
        this.インスタンスメソッドか=true;
        this.Lambda(Lambda);
    }
    protected override void Lambda(LambdaExpression Lambda) {
        var 旧インスタンスメソッドか=this.インスタンスメソッドか;
        var 旧I = this.I;
        var 旧DictionaryNameLabel = this.Dictionary_Name_Label;
        var 旧Parameters = this.Parameters;
        var 旧Dictionary_Parameter_LocalBuilder=this.Dictionary_Parameter_LocalBuilder;
        this.Dictionary_Name_Label=new();
        this.Dictionary_Parameter_LocalBuilder=new();
        var Lambda_Parameters = Lambda.Parameters;
        if(旧インスタンスメソッドか){
            this.インスタンスメソッドか=this.判定_InstanceMethodか.実行(Lambda.Body)|true;
        }
        var ReturnType = Lambda.ReturnType;
        var (_,Member,Impl)=this.DictionaryLambda[Lambda];
        this.Parameters=Lambda_Parameters;
        {
            var I=this.I=Impl.GetILGenerator();
            if(ReturnType!=typeof(void))this.Traverse(Lambda.Body);
            else                        this.VoidTraverse(Lambda.Body);
            I.Ret();
        }
        this.I=旧I;
        this.Parameters=旧Parameters;
        this.Dictionary_Name_Label=旧DictionaryNameLabel;
        this.Dictionary_Parameter_LocalBuilder=旧Dictionary_Parameter_LocalBuilder;
        this.インスタンスメソッドか=旧インスタンスメソッドか;
        if(旧I is not null)this.MemberAccess(Member);
    }
    protected override void Block(BlockExpression Block) {
        //var Dictionaryラムダ跨ぎParameter = this.Dictionaryラムダ跨ぎParameter;
        var Dictionary_Parameter_LocalBuilder = this.Dictionary_Parameter_LocalBuilder;
        Debug.Assert(Dictionary_Parameter_LocalBuilder is not null);
        var Block_Expressions = Block.Expressions;
        var I = this.I!;
        foreach(var Block_Variable in Block.Variables) {
            //Debug.Assert(!this.Dictionaryラムダ跨ぎParameter.ContainsKey(Block_Variable));
            //if(this.Dictionaryラムダ跨ぎParameter.ContainsKey(Block_Variable))
            Dictionary_Parameter_LocalBuilder.Add(Block_Variable,I.DeclareLocal(Block_Variable.Type));
        }
        var Block_Expressions_Count_1 = Block_Expressions.Count-1;
        for(var a = 0;a<Block_Expressions_Count_1;a++)
            this.VoidTraverse(Block_Expressions[a]);
        //Blockは最後の式の型を返す。それを有効にする。
        this.Traverse(Block_Expressions[Block_Expressions_Count_1]);
        foreach(var Block_Variable in Block.Variables)
            Dictionary_Parameter_LocalBuilder.Remove(Block_Variable);
            //if(this.Dictionaryラムダ跨ぎParameter.ContainsKey(Block_Variable))
            //    Dictionaryラムダ跨ぎParameter.Remove(Block_Variable);
            //else
            //    Dictionary_Parameter_LocalBuilder.Remove(Block_Variable);
    }
#if NETFRAMEWORK
    protected override void DebugInfo(DebugInfoExpression DebugInfo){
        var Document=this.ModuleBuilder!.DefineDocument(@"Expression.txt",Guid.Empty,Guid.Empty,Guid.Empty);
        this.I!.MarkSequencePoint(Document,3,4,6,1);
        base.DebugInfo(DebugInfo);
    }
#endif
    //private void PrivateFilter(CatchBlock Try_Handler){
    //    var I = this.I!;
    //    var Isinst = I.DefineLabel();
    //    I.Brtrue(Isinst);
    //    I.Ldc_I4_0();
    //    var endfilter = I.DefineLabel();
    //    I.Br(endfilter);
    //    I.MarkLabel(Isinst);
    //    this.Traverse(Try_Handler.Filter);
    //    I.MarkLabel(endfilter);
    //    //I.Endfilter();
    //    I.BeginCatchBlock(null);
    //}
    //protected override void ProtectedFilter(CatchBlock Try_Handler,LocalBuilder Variable){
    //    var I = this.I!;
    //    I.BeginExceptFilterBlock();
    //    I.Isinst(Try_Handler.Test);
    //    I.Stloc(Variable);
    //    I.Ldloc(Variable);
    //    this.PrivateFilter(Try_Handler);
    //}
    //protected override void ProtectedFilter(CatchBlock Try_Handler){
    //    var I = this.I!;
    //    I.BeginExceptFilterBlock();
    //    I.Isinst(Try_Handler.Test);
    //    this.PrivateFilter(Try_Handler);
    //}
    protected override void ProtectedFault(Expression? Fault){
        if(Fault is null) return;
        this.I!.BeginFaultBlock();
        this.Traverse(Fault);
    }
}

//137 20220521
//372 20220504
