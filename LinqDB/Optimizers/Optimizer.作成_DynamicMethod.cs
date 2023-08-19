using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;
using System.Reflection.Emit;
using LinqDB.Helpers;
using System.Diagnostics;
namespace LinqDB.Optimizers;

partial class Optimizer{
    /// <summary>
    /// Optimizerで最適化されたExpressionからDynamicMethodを作る専用
    /// </summary>
    internal class 作成_DynamicMethod:A作成_IL {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="判定_InstanceMethodか"></param>
        public 作成_DynamicMethod(判定_InstanceMethodか 判定_InstanceMethodか) :base(判定_InstanceMethodか) {
        }
        private object? ClassTuple;
        internal void Impl作成(LambdaExpression Lambda,ParameterExpression TupleParameter,
            Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant,
            Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic,
            Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)> DictionaryLambda,
            Dictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter,
            object? ClassTuple){
            this.DispParameter=TupleParameter;
            this.DictionaryConstant=DictionaryConstant;
            this.DictionaryDynamic=DictionaryDynamic;
            this.DictionaryLambda=DictionaryLambda;
            this.Dictionaryラムダ跨ぎParameter=Dictionaryラムダ跨ぎParameter;
            this.ClassTuple=ClassTuple;
            this.Clear();
            this.インスタンスメソッドか=true;
            this.Lambda(Lambda);
        }
        private Type[]LambdaからTypesとRootが含まれている(LambdaExpression Lambda){
            var インスタンスメソッドか=this.インスタンスメソッドか;
            var Lambda_Parameters=Lambda.Parameters;
            var Lambda_Parameters_Count=Lambda_Parameters.Count;
            if(インスタンスメソッドか)
                インスタンスメソッドか=this.インスタンスメソッドか=this.判定_InstanceMethodか.実行(Lambda.Body);
            //var 新Parameters=Lambda_Parameters;
            Type[] 新ParameterTypes;
            int offset;
            if(インスタンスメソッドか){
                offset=1;
                新ParameterTypes=new Type[Lambda_Parameters_Count+1];
                新ParameterTypes[0]=this.ClassTuple!.GetType();
            } else{
                offset=0;
                新ParameterTypes=new Type[Lambda_Parameters_Count];
            }
            for(var a=0;a<Lambda_Parameters_Count;a++){
                var 元Parameter=Lambda_Parameters[a];
                新ParameterTypes[offset+a]=元Parameter.IsByRef
                    ?元Parameter.Type.MakeByRefType()
                    :元Parameter.Type;
            }
            this.Parameters=Lambda_Parameters;
            return 新ParameterTypes;
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
            var 新ParameterTypes=this.LambdaからTypesとRootが含まれている(Lambda);
            var ReturnType = Lambda.ReturnType;
            this.Parameters=Lambda_Parameters;
            var(_,Member,_)=this.DictionaryLambda[Lambda];
            {
                var Impl_DynamicMethod=new DynamicMethod(Lambda.Name??this.番号++.ToString(),ReturnType,新ParameterTypes,typeof(作成_DynamicMethod),true){
                    InitLocals=false
                };
                var I=this.I=Impl_DynamicMethod.GetILGenerator();
                if(ReturnType!=typeof(void))this.Traverse(Lambda.Body);
                else                        this.VoidTraverse(Lambda.Body);
                I.Ret();
                var Delegate=this.インスタンスメソッドか?Impl_DynamicMethod.CreateDelegate(Lambda.Type,this.ClassTuple):Impl_DynamicMethod.CreateDelegate(Lambda.Type);
                //Debug.Assert(Lambda.Type==Fields[^1].FieldType);
                //var Tuple=this.ClassTuple;
                var Field= (FieldInfo)Member.Member;
                SetClassTuple(Member,this.ClassTuple);
                void SetClassTuple(MemberExpression Member0,object ClassTuple){
                    if(Member0.Expression is MemberExpression Member1) {
                        Debug.Assert(Member1.Member.Name=="Rest");
                        var Rest_Field=ClassTuple.GetType().GetField("Rest")!;
                        var Rest_ValueTuple=SetValueTuple(Member1,Rest_Field.GetValue(ClassTuple)!);
                        Rest_Field.SetValue(ClassTuple,Rest_ValueTuple);
                    } else {
                        Debug.Assert(Member0.Member==Field);
                        ((FieldInfo)Member0.Member).SetValue(ClassTuple,Delegate);
                    }
                }
                object SetValueTuple(MemberExpression Member0,object ValueTuple){
                    if (Member0.Member.Name == "Rest"){
                        if (Member0.Expression is MemberExpression Member1){
                            var Rest_Field = ValueTuple.GetType().GetField("Rest")!;
                            var Rest_ValueTuple= Rest_Field.GetValue(ValueTuple)!;
                            var ValueTuple1= SetValueTuple(Member1,Rest_ValueTuple);
                            Rest_Field.SetValue(ValueTuple,ValueTuple1);
                        } else{
                            Field.SetValue(ValueTuple,Delegate);
                        }
                    } else{
                        ((FieldInfo)Member0.Member).SetValue(ValueTuple,Delegate);
                    }
                    return ValueTuple;
                }
            }
            this.I=旧I;
            this.Parameters=旧Parameters;
            this.Dictionary_Name_Label=旧DictionaryNameLabel;
            this.Dictionary_Parameter_LocalBuilder=旧Dictionary_Parameter_LocalBuilder;
            this.インスタンスメソッドか=旧インスタンスメソッドか;
            if(旧I is not null)
                this.Traverse(Member);
        }
        protected override void Block(BlockExpression Block) {
            var Dictionary_Parameter_LocalBuilder = this.Dictionary_Parameter_LocalBuilder;
            Debug.Assert(Dictionary_Parameter_LocalBuilder is not null);
            var Block_Expressions = Block.Expressions;
            var I = this.I!;
            foreach(var Block_Variable in Block.Variables)
                //if(!this.Dictionaryラムダ跨ぎParameter.ContainsKey(Block_Variable))
                    Dictionary_Parameter_LocalBuilder.Add(Block_Variable,I.DeclareLocal(Block_Variable.Type));
            var Block_Expressions_Count_1 = Block_Expressions.Count-1;
            for(var a = 0;a<Block_Expressions_Count_1;a++)
                this.VoidTraverse(Block_Expressions[a]);
            //Blockは最後の式の型を返す。それを有効にする。
            this.Traverse(Block_Expressions[Block_Expressions_Count_1]);
            foreach(var Block_Variable in Block.Variables)
                Dictionary_Parameter_LocalBuilder.Remove(Block_Variable);
        }
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
        //protected override void ProtectedFilter(CatchBlock Try_Handler,LocalBuilder Variable) {
        //    //throw new NotSupportedException(Properties.Resources.DynamicMethodでFilterはサポートされていない);
        //    var I = this.I!;
        //    I.BeginExceptFilterBlock();
        //    //I.Isinst(Try_Handler.Test);
        //    I.Stloc(Variable);
        //    I.Ldloc(Variable);
        //    this.PrivateFilter(Try_Handler);
        //    //var Isinst = I.DefineLabel();
        //    //I.Brtrue(Isinst);
        //    //I.Ldc_I4_0();//理由不明
        //    //var endfilter = I.DefineLabel();
        //    //I.Br(endfilter);
        //    //I.MarkLabel(Isinst);
        //    //this.Traverse(Try_Handler.Filter);
        //    //I.MarkLabel(endfilter);
        //    //I.Endfilter();
        //    ////filterがある場合はcatch変数がなくpushされない
        //    //I.BeginCatchBlock(null);
        //}
        //protected override void ProtectedFilter(CatchBlock Try_Handler) {
        //    //throw new NotSupportedException(Properties.Resources.DynamicMethodでFilterはサポートされていない);
        //    var I = this.I!;
        //    I.BeginExceptFilterBlock();
        //    this.PrivateFilter(Try_Handler);
        //}
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
        //protected override void ProtectedFilter(CatchBlock Try_Handler,LocalBuilder Variable) {
        //    //throw new NotSupportedException(Properties.Resources.DynamicMethodでFilterはサポートされていない);
        //    var I = this.I!;
        //    I.BeginExceptFilterBlock();
        //    //I.Isinst(Try_Handler.Test);
        //    I.Stloc(Variable);
        //    I.Ldloc(Variable);
        //    this.PrivateFilter(Try_Handler);
        //    //var Isinst = I.DefineLabel();
        //    //I.Brtrue(Isinst);
        //    //I.Ldc_I4_0();//理由不明
        //    //var endfilter = I.DefineLabel();
        //    //I.Br(endfilter);
        //    //I.MarkLabel(Isinst);
        //    //this.Traverse(Try_Handler.Filter);
        //    //I.MarkLabel(endfilter);
        //    //I.Endfilter();
        //    ////filterがある場合はcatch変数がなくpushされない
        //    //I.BeginCatchBlock(null);
        //}
        //protected override void ProtectedFilter(CatchBlock Try_Handler) {
        //    //throw new NotSupportedException(Properties.Resources.DynamicMethodでFilterはサポートされていない);
        //    var I = this.I!;
        //    I.BeginExceptFilterBlock();
        //    this.PrivateFilter(Try_Handler);
        //}
        protected override void ProtectedFault(Expression? Fault) {
            //throw new NotSupportedException(Properties.Resources.DynamicMethodでFaultはサポートされていない);
            if(Fault is null) return;
            this.I!.BeginFaultBlock();
            this.Traverse(Fault);
        }
    }
}
//132 20220521