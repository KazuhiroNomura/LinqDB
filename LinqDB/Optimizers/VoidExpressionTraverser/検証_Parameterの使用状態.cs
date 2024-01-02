using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace LinqDB.Optimizers.VoidExpressionTraverser;

internal sealed class 検証_Parameterの使用状態:VoidExpressionTraverser_Quoteを処理しない {
    private readonly Dictionary<ParameterExpression,int> DictionaryCパラメーター読込回数=new();
    private readonly Dictionary<ParameterExpression,int> DictionaryCパラメーター書込回数=new();
    private readonly Dictionary<ParameterExpression,int> DictionaryCローカル書込回数=new();
    private readonly Dictionary<ParameterExpression,int> DictionaryCローカル読込回数=new();
    private readonly Dictionary<ParameterExpression,int> DictionaryCラムダ跨書込回数=new();
    private readonly Dictionary<ParameterExpression,int> DictionaryCラムダ跨読込回数=new();
    private readonly Dictionary<ParameterExpression,int> DictionaryCループ跨書込回数=new();
    private readonly Dictionary<ParameterExpression,int> DictionaryCループ跨読込回数=new();
    private readonly List<ParameterExpression> ListスコープParameter = new();
    private readonly IEnumerable<ParameterExpression> ループ跨ぎParameters;
    public 検証_Parameterの使用状態(IEnumerable<ParameterExpression> ループ跨ぎParameters) {
        this.ループ跨ぎParameters=ループ跨ぎParameters;
    }
    internal IEnumerable<ParameterExpression> ラムダ跨ぎParameters=default!;
    public void 実行(Expression Lambda){
        var DictionaryCパラメーター読込回数=this.DictionaryCパラメーター読込回数;
        var DictionaryCパラメーター書込回数=this.DictionaryCパラメーター書込回数;
        var DictionaryCローカル書込回数 = this.DictionaryCローカル書込回数;
        var DictionaryCローカル読込回数 = this.DictionaryCローカル読込回数;
        var DictionaryCラムダ跨書込回数 = this.DictionaryCラムダ跨書込回数;
        var DictionaryCラムダ跨読込回数 = this.DictionaryCラムダ跨読込回数;
        var DictionaryCループ跨書込回数 = this.DictionaryCループ跨書込回数;
        var DictionaryCループ跨読込回数 = this.DictionaryCループ跨読込回数;
        DictionaryCパラメーター読込回数.Clear();
        DictionaryCパラメーター書込回数.Clear();
        DictionaryCローカル書込回数.Clear();
        DictionaryCローカル読込回数.Clear();
        DictionaryCラムダ跨書込回数.Clear();
        DictionaryCラムダ跨読込回数.Clear();
        DictionaryCループ跨書込回数.Clear();
        DictionaryCループ跨読込回数.Clear();
        this.ListスコープParameter.Clear();
        this.Traverse(Lambda);
        //foreach(var p in DictionaryCパラメーター読込回数)
        //    Debug.Assert(p.Value>=0,"パラメーター変数は1回以上読み込むのが正しい。");
        //foreach(var p in DictionaryCローカル書込回数){
        //    if(p.Key.Name is not null&&p.Key.Name.Length>=2&&p.Key.Name[..2]=="局所")
        //        Debug.Assert(p.Value==1,"ローカル変数は1回だけ書き込むのが正しい。");
        //    //Parameterは参照一致なので２回書き込まれと判定されることも
        //}

        //foreach(var p in DictionaryCローカル読込回数)
            //Debug.Assert(p.Value>=2,"ローカル変数は2回以上読み込むされるのが正しい。");
        //Debug.Assert(p.Value>=2,"ローカル変数は2回以上読み込むされるのが正しい。");手動で式木作っていた場合は==1,==0もありえる
        //foreach(var p in DictionaryCラムダ跨書込回数)
        //    Debug.Assert(p.Value==1,"ラムダ跨変数は1回だけ書き込むのが正しい。");
        foreach(var p in DictionaryCラムダ跨読込回数)
            Debug.Assert(p.Value>=1,"ラムダ跨変数は1回以上読み込むされるのが正しい。");
        //foreach(var p in DictionaryCループ跨書込回数)
        //    Debug.Assert(p.Value==1,"ループ跨変数は1回だけ書き込むのが正しい。");
        foreach(var p in DictionaryCループ跨読込回数)
            Debug.Assert(p.Value>=1,"ループ跨変数は1回以上読み込むされるのが正しい。");
        //                Debug.Assert(p.Value>=0);//(Cラムダ局所0=a*b)!=3という使い方をするから。
        //v =>v.Let(a =>v.Inline(b =>a))
        //v =>{
        //    跨ぎ=v;
        //    v.Let(a =>{
        //        跨ぎ.Inline(b =>a))
        //    })
        //}
        //v =>{
        //    跨ぎ=v;
        //    v.Let(a =>{
        //        a
        //    })
        //}
        //そうなるので跨ぎは使われない。
        //foreach(var p in DictionaryCラムダ跨ぎ読込回数)
        //    Debug.Assert(p.Value>=1);
        foreach(var p in DictionaryCループ跨書込回数)
            Debug.Assert(p.Value==1||p.Value==2);
        //↑と同じくインライン跨ぎで使われない可能性がある。
        //foreach(var p in DictionaryCループ跨ぎ読込回数)
        //    Debug.Assert(p.Value>=1);
    }
    protected override void Assign(BinaryExpression Binary) {
        if(Binary.Left is ParameterExpression Parameter){
            if(this.ラムダ跨ぎParameters.Contains(Parameter)){
                var DictionaryCラムダ跨書込回数=this.DictionaryCラムダ跨書込回数;
                if( DictionaryCラムダ跨書込回数.ContainsKey(Parameter))
                    DictionaryCラムダ跨書込回数[Parameter]++;
                else {
                    DictionaryCラムダ跨書込回数.Add(Parameter,1);
                    this.DictionaryCラムダ跨読込回数.Add(Parameter,0);
                }
            }else if(this.ループ跨ぎParameters.Contains(Parameter)) {
                this.DictionaryCループ跨書込回数[Parameter]++;
            }else if(this.DictionaryCローカル書込回数.ContainsKey(Parameter)) {
                this.DictionaryCローカル書込回数[Parameter]++;
                this.DictionaryCローカル読込回数[Parameter]++;
            }else if(this.DictionaryCパラメーター読込回数.ContainsKey(Parameter)){
                this.DictionaryCパラメーター書込回数[Parameter]++;
                this.DictionaryCパラメーター読込回数[Parameter]++;
            }
        } else {
            this.Traverse(Binary.Left);
        }
        this.Traverse(Binary.Right);
    }
    protected override void Block(BlockExpression Block) {
        var Block_Variables=Block.Variables;
        foreach(var Variable in Block_Variables){
            if(this.ラムダ跨ぎParameters.Contains(Variable)) {
                this.DictionaryCラムダ跨書込回数.Add(Variable,0);
                this.DictionaryCラムダ跨読込回数.Add(Variable,0);
            }else if(this.ループ跨ぎParameters.Contains(Variable)) {
                this.DictionaryCループ跨書込回数.Add(Variable,0);
                this.DictionaryCループ跨読込回数.Add(Variable,0);
            } else if(!this.DictionaryCローカル書込回数.ContainsKey(Variable)) { 
                this.DictionaryCローカル書込回数.Add(Variable,0);
                this.DictionaryCローカル読込回数.Add(Variable,0);
            }
        }
        base.Block(Block);
    }
    protected override void Try(TryExpression Try) {
        this.Traverse(Try.Body);
        var ListスコープParameter = this.ListスコープParameter;
        foreach(var Try_Handler in Try.Handlers){
            if(Try_Handler.Variable is null) continue;
            var Try_Handler_Variable=Try_Handler.Variable;
            ListスコープParameter.Add(Try_Handler_Variable);
            //if(!this.DictionaryCローカル書込回数.ContainsKey(Try_Handler_Variable)){
            //    this.DictionaryCローカル書込回数.Add(Try_Handler_Variable,1);
            //    this.DictionaryCローカル読込回数.Add(Try_Handler_Variable,0);
            //}
            this.Traverse(Try_Handler.Body);
            this.PrivateTryFilterCatch(Try_Handler);
            ListスコープParameter.RemoveAt(ListスコープParameter.Count-1);
        }
        this.TraverseNulllable(Try.Finally);
    }
    private void PrivateTryFilterCatch(CatchBlock Try_Handler){
        if(Try_Handler.Variable is not null){
            var ListスコープParameter = this.ListスコープParameter;
            var Try_Handler_Variable=Try_Handler.Variable;
            ListスコープParameter.Add(Try_Handler_Variable);
            this.Traverse(Try_Handler.Body);
            ListスコープParameter.RemoveAt(ListスコープParameter.Count-1);
        } else{
            this.TraverseNulllable(Try_Handler.Filter);
            this.Traverse(Try_Handler.Body);
        }
    }
    protected override void Lambda(LambdaExpression Lambda) {
        var Lambda_Parameters = Lambda.Parameters;
        var ListスコープParameter = this.ListスコープParameter;
        var ListスコープParameter_Count = ListスコープParameter.Count;
        ListスコープParameter.AddRange(Lambda_Parameters);
        foreach(var Parameter in Lambda_Parameters){
            var DictionaryCパラメーター読込回数=this.DictionaryCパラメーター読込回数;
            var DictionaryCパラメーター書込回数=this.DictionaryCパラメーター書込回数;
            if(!DictionaryCパラメーター読込回数.ContainsKey(Parameter)){
                DictionaryCパラメーター読込回数.Add(Parameter,0);
                DictionaryCパラメーター書込回数.Add(Parameter,0);
            }
            //var DictionaryCローカル書込回数 = this.DictionaryCローカル書込回数;
            //if(!DictionaryCローカル書込回数.ContainsKey(Parameter)) {
            //    DictionaryCローカル書込回数.Add(Parameter,1);
            //    this.DictionaryCローカル読込回数.Add(Parameter,0);
            //} else {
            //    DictionaryCローカル書込回数[Parameter]++;
            //}
        }
        this.Traverse(Lambda.Body);
        ListスコープParameter.RemoveRange(ListスコープParameter_Count,Lambda_Parameters.Count);
    }
    protected override void Parameter(ParameterExpression Parameter){
        if(     this.ラムダ跨ぎParameters.Contains(Parameter  ))this.DictionaryCラムダ跨読込回数[Parameter]++;
        else if(this.ループ跨ぎParameters.Contains(Parameter  ))this.DictionaryCループ跨読込回数[Parameter]++;
        else if(this.DictionaryCローカル読込回数.ContainsKey(Parameter))this.DictionaryCローカル読込回数[Parameter]++;
        else if(this.DictionaryCパラメーター読込回数.ContainsKey(Parameter))this.DictionaryCパラメーター読込回数[Parameter]++;
        //else if(!this.ListスコープParameter.Contains(Parameter))this.DictionaryCパラメーター読込回数[Parameter]++;
    }
}
