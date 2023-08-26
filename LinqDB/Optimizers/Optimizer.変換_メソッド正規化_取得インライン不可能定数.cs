﻿/*
 * a.Union(a)→a
 * a.Except(a)→Empty
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Sets;
//using Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder.Binder;
using LinqDB.Helpers;
//using System.Runtime.Remoting.Messaging;
// ReSharper disable MemberHidesStaticFromOuterClass
namespace LinqDB.Optimizers;
partial class Optimizer {
    /// <summary>
    /// インライン不可能定数とは1mとか単純にIL命令で書けないもの。
    /// a+=bなどの合体演算子をa=a+bにする。
    /// A.SelectMany(a=>B,(a,b)=>new{a,b})→A.SelectMany(a=>B.Select(b=>=>new{a,b}))にする。
    /// decimal.Parse("1111")→1111mに変換する
    /// </summary>
    private sealed class 変換_メソッド正規化_取得インライン不可能定数:ReturnExpressionTraverser_Quoteを処理しない {
        private abstract class 判定_葉に移動したいPredicate:VoidExpressionTraverser_Quoteを処理しない {
            protected ParameterExpression? 許可するParameter;
            protected bool 移動出来る;
            protected override void Parameter(ParameterExpression Parameter) {
                if(Parameter!=this.許可するParameter) {
                    this.移動出来る=false;
                }
            }
        }
        /// <summary>
        /// GroupJoin.WhereのWhereのAnd条件を葉に移動。ORも移動するがそれは正しいか不明。
        /// </summary>
        private sealed class 取得_New_OuterPredicate_InnerPredicate {
            private sealed class 判定_New_葉に移動したいPredicate:判定_葉に移動したいPredicate {
                public bool 実行(NewExpression New,Expression e,ParameterExpression? 許可するParameter) {
                    this.New0=New;
                    this.許可するParameter=許可するParameter;
                    this.移動出来る=true;
                    this.Traverse(e);
                    return this.移動出来る;
                }

                private NewExpression? New0;
                protected override void New(NewExpression New) {
                    if(New.Type.IsAnonymousValueTuple()) {
                        var 旧New = this.New0;
                        base.New(New);
                        this.New0=旧New;
                    } else {
                        base.New(New);
                    }
                }
                protected override void MemberAccess(MemberExpression Member) {
                    var Member_Expression = Member.Expression;
                    if(Member_Expression is not null&&Member_Expression.Type.IsAnonymousValueTuple()) {
                        var New = this.New0!;
                        var Parameters = New.Type.GetConstructors()[0].GetParameters();
                        var Name = Member.Member.Name;
                        for(var a = 0;a<Parameters.Length;a++) {
                            if(Parameters[a].Name==Name) {
                                this.Traverse(New.Arguments[a]);
                                return;
                            }
                        }
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
                        throw new InvalidProgramException("ここは実行されないはず");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
                    }
                    base.MemberAccess(Member);
                }
            }
            private readonly 判定_New_葉に移動したいPredicate _判定_New_葉に移動したいPredicate=new();
            private Expression? OuterPredicate, InnerPredicate;
            private ParameterExpression? Outer;
            private NewExpression? New;
            public (Expression? OuterPredicate, Expression? InnerPredicate) 実行(NewExpression New,Expression Predicate,ParameterExpression Outer) {
                this.New=New;
                this.Outer=Outer;
                this.OuterPredicate=this.InnerPredicate=null;
                this.Traverse(Predicate);
                return (this.OuterPredicate, this.InnerPredicate);
            }
            private void 共通(Expression e) {
                if(this._判定_New_葉に移動したいPredicate.実行(this.New!,e,this.Outer!)) {
                    this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,e);
                } else {
                    this.InnerPredicate=AndAlsoで繋げる(this.InnerPredicate,e);
                }
            }
            private void Traverse(Expression e) {
                var 判定_New_葉に移動したいPredicate = this._判定_New_葉に移動したいPredicate;
                if(e.NodeType==ExpressionType.AndAlso) {
                    var Binary = (BinaryExpression)e;
                    var Binary_Left = Binary.Left;
                    var Binary_Right = Binary.Right;
                    var Left葉Outerに移動する = 判定_New_葉に移動したいPredicate.実行(this.New!,Binary_Left,this.Outer!);
                    var Right葉Outerに移動する = 判定_New_葉に移動したいPredicate.実行(this.New!,Binary_Right,this.Outer!);
                    if(Left葉Outerに移動する) {
                        if(Right葉Outerに移動する) {
                            this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary);
                        } else {
                            this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary_Left);
                            this.Traverse(Binary_Right);
                        }
                    } else if(Right葉Outerに移動する) {
                        this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary_Right);
                        this.Traverse(Binary_Left);
                    } else {
                        this.Traverse(Binary_Left);
                        this.Traverse(Binary_Right);
                    }
                } else if(e.NodeType==ExpressionType.MemberAccess) {
                    var Member = (MemberExpression)e;
                    var Member_Expression = Member.Expression;
                    if(Member_Expression is not null&&Member_Expression.Type.IsAnonymousValueTuple()) {
                        var New = this.New!;
                        var Parameters = New.Type.GetConstructors()[0].GetParameters();
                        var Name = Member.Member.Name;
                        for(var a = 0;a<Parameters.Length;a++) {
                            if(Parameters[a].Name==Name) {
                                this.共通(New.Arguments[a]);
                                return;
                            }
                        }
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
                        throw new InvalidProgramException("ここは実行されないはず");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
                    }
                } else {
                    this.共通(e);
                }
            }
        }
        private readonly 取得_New_OuterPredicate_InnerPredicate _取得_New_OuterPredicate_InnerPredicate=new();
        //private sealed class 取得_New_OuterPredicate_InnerPredicate_OtherPredicate:取得_New_OuterPredicate_InnerPredicate {
        //    public 取得_New_OuterPredicate_InnerPredicate_OtherPredicate(判定_New_葉に移動したいPredicate 判定_New_葉に移動したいPredicate):base(判定_New_葉に移動したいPredicate){
        //    }
        //    private Expression? OtherPredicate;
        //    private ParameterExpression? Inner;
        //    public (Expression? OuterPredicate, Expression? InnerPredicate, Expression? OtherPredicate) 実行(NewExpression New,Expression Predicate,ParameterExpression Outer,ParameterExpression Inner) {
        //        this.New=New;
        //        this.Outer=Outer;
        //        this.Inner=Inner;
        //        this.OuterPredicate=this.InnerPredicate=this.OtherPredicate=null;
        //        this.Traverse(Predicate);
        //        return (this.OuterPredicate, this.InnerPredicate, this.OtherPredicate);
        //    }
        //    private protected override void 共通(Expression e) {
        //        if(this.判定_New_葉に移動したいPredicate.実行(this.New!,e,this.Outer!)) {
        //            this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,e);
        //        } else if(this.判定_New_葉に移動したいPredicate.実行(this.New!,e,this.Inner!)) {
        //            this.InnerPredicate=AndAlsoで繋げる(this.InnerPredicate,e);
        //        } else {
        //            this.OtherPredicate=AndAlsoで繋げる(this.OtherPredicate,e);
        //        }
        //    }
        //}
        //private readonly 取得_New_OuterPredicate_InnerPredicate_OtherPredicate _取得_New_OuterPredicate_InnerPredicate_OtherPredicate;
        private sealed class 取得_Parameter_OuterPredicate_InnerPredicate {
            private sealed class 判定_Parameter_葉に移動したいPredicate:判定_葉に移動したいPredicate {
                public bool 実行(Expression e,ParameterExpression? 許可するParameter) {
                    this.許可するParameter=許可するParameter;
                    this.移動出来る=true;
                    this.Traverse(e);
                    return this.移動出来る;
                }
            }
            private readonly 判定_Parameter_葉に移動したいPredicate _判定_Parameter_葉に移動したいPredicate=new();
            private Expression? OuterPredicate , OtherPredicate;
            private ParameterExpression? Outer;
            public (Expression? OuterPredicate, Expression? OtherPredicate) 実行(Expression e,ParameterExpression Outer) {
                this.Outer=Outer;
                this.OuterPredicate=this.OtherPredicate=null;
                this.Traverse(e);
                return (this.OuterPredicate, this.OtherPredicate);
            }
            private void Traverse(Expression e) {
                var 判定_Parameter_葉に移動したいPredicate = this._判定_Parameter_葉に移動したいPredicate;
                if(e.NodeType==ExpressionType.AndAlso) {
                    var Binary = (BinaryExpression)e;
                    var Binary_Left = Binary.Left;
                    var Binary_Right = Binary.Right;
                    var Left葉Outerに移動する = 判定_Parameter_葉に移動したいPredicate.実行(Binary_Left,this.Outer!);
                    var Right葉Outerに移動する = 判定_Parameter_葉に移動したいPredicate.実行(Binary_Right,this.Outer!);
                    if(Left葉Outerに移動する) {
                        if(Right葉Outerに移動する) {
                            this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary);
                        } else {
                            this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary_Left);
                            this.Traverse(Binary_Right);
                        }
                    } else if(Right葉Outerに移動する) {
                        this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary_Right);
                        this.Traverse(Binary_Left);
                    } else {
                        this.Traverse(Binary_Left);
                        this.Traverse(Binary_Right);
                    }
                } else if(this._判定_Parameter_葉に移動したいPredicate.実行(e,this.Outer!)) {
                    this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,e);
                } else {
                    this.OtherPredicate=AndAlsoで繋げる(this.OtherPredicate,e);
                }
            }
        }
        private readonly 取得_Parameter_OuterPredicate_InnerPredicate _取得_Parameter_OuterPredicate_InnerPredicate=new();
        private readonly 変換_旧Expressionを新Expression1 変換_旧Expressionを新Expression1;
        private readonly 変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1;
        private readonly 変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2;
        public 変換_メソッド正規化_取得インライン不可能定数(作業配列 作業配列,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1,
            変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2,
            変換_旧Expressionを新Expression1 変換_旧Expressionを新Expression1) : base(作業配列) {
            this.変換_旧Parameterを新Expression1=変換_旧Parameterを新Expression1;
            this.変換_旧Parameterを新Expression2=変換_旧Parameterを新Expression2;
            //this._取得_New_OuterPredicate_InnerPredicate_OtherPredicate=new 取得_New_OuterPredicate_InnerPredicate_OtherPredicate(判定_New_葉に移動したいPredicate);
            this.変換_旧Expressionを新Expression1=変換_旧Expressionを新Expression1;
        }
        private int 番号;
        //internal Information? Information;
        public Expression 実行(Expression e) {
            Debug.Assert(this.DictionaryConstant.Comparer is ExpressionEqualityComparer ExpressionEqualityComparer&&ExpressionEqualityComparer.スコープParameters.Count==0);
            this.番号=0;
            this.DictionaryConstant.Clear();
            return this.Traverse(e);
        }
        internal Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant=default!;
        protected override Expression Constant(ConstantExpression Constant0) {
            if(!ILで直接埋め込めるか(Constant0.Type)&&!this.DictionaryConstant.ContainsKey(Constant0))
                this.DictionaryConstant.Add(Constant0,default!);
            return Constant0;
        }
        protected override Expression Quote(UnaryExpression Unary0){
            var Constant=Expression.Constant(Unary0.Operand);
            if(!this.DictionaryConstant.ContainsKey(Constant))
                this.DictionaryConstant.Add(Constant,default!);
            return Constant;
            //this.Dictionary_Quote_Member.Add(Unary0,default!);
            //return Unary0;
        }

        private Expression 共通BinaryAssign(BinaryExpression Binary0, ExpressionType NodeType) {
            var Binary1_Left=this.Traverse(Binary0.Left);
            return Expression.Assign(
                Binary1_Left,
                Expression.MakeBinary(
                    NodeType,
                    Binary1_Left,
                    this.Traverse(Binary0.Right),
                    Binary0.IsLiftedToNull,
                    Binary0.Method,
                    Binary0.Conversion
                )
            );
        }
        protected override Expression AddAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Add);
        protected override Expression AddAssignChecked(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.AddChecked);
        protected override Expression AndAssign(BinaryExpression Binary0) {
            var Binary1_Left=this.Traverse(Binary0.Left);
            return Expression.Assign(
                Binary1_Left,
                Expression.And(
                    Binary1_Left,
                    this.Traverse(Binary0.Right),
                    Binary0.Method
                )
            );
        }
        protected override Expression DivideAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Divide);
        protected override Expression ExclusiveOrAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.ExclusiveOr);
        protected override Expression LeftShiftAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.LeftShift);
        protected override Expression ModuloAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Modulo);
        protected override Expression MultiplyAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Multiply);
        protected override Expression MultiplyAssignChecked(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.MultiplyChecked);
        protected override Expression OrAssign(BinaryExpression Binary0) {
            var Binary1_Left=this.Traverse(Binary0.Left);
            return Expression.Assign(
                Binary1_Left,
                Expression.Or(
                    Binary1_Left,
                    this.Traverse(Binary0.Right),
                    Binary0.Method
                )
            );
        }
        protected override Expression PowerAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Power);
        protected override Expression RightShiftAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.RightShift);
        protected override Expression SubtractAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Subtract);
        protected override Expression SubtractAssignChecked(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.SubtractChecked);
        protected override Expression Try(TryExpression Try0){
            Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
            var Try0_Handlers=Try0.Handlers;
            var Try0_Handlers_Count=Try0_Handlers.Count;
            var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
            var 変化したか=false;
            var Try0_Body=Try0.Body;
            var Try1_Body=this.Traverse(Try0_Body);
            if(Try0_Body!=Try1_Body)
                変化したか=true;
            var Try0_Finally=Try0.Finally;
            var Try1_Finally=this.TraverseNullable(Try0_Finally);
            if(Try0_Finally!=Try1_Finally)
                変化したか=true;
            for(var a=0;a<Try0_Handlers_Count;a++) {
                var Try0_Handler=Try0_Handlers[a];
                Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
                var Try0_Handler_Variable=Try0_Handler.Variable;
                CatchBlock Try1_Handler;
                if(Try0_Handler_Variable is not null) {
                    var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                    var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
                    //Debug.Assert(Try1_Handler_Filter!=null,nameof(Try1_Handler_Filter)+" != null");
                    if(Try0_Handler.Body!=Try1_Handler_Body||Try0_Handler.Filter!=Try1_Handler_Filter) {
                        変化したか=true;
                        Try1_Handler=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
                    } else {
                        Try1_Handler=Try0_Handler;
                    }
                } else {
                    var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                    var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
                    if(Try0_Handler.Body!=Try1_Handler_Body||Try0_Handler.Filter!=Try1_Handler_Filter) {
                        変化したか=true;
                        Try1_Handler=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
                    } else {
                        Try1_Handler=Try0_Handler;
                    }
                }
                Try1_Handlers[a]=Try1_Handler;
            }
            if(Try0.Fault is not null){
                Debug.Assert(Try0_Finally is null);
                var Try0_Fault=Try0.Fault;
                var Try1_Fault=this.Traverse(Try0_Fault);
                if(Try0_Fault!=Try1_Fault)変化したか=true;
                return 変化したか
                    ? Expression.TryFault(Try1_Body,Try1_Fault)
                    :Try0;
            } else{
                return 変化したか
                    ? Expression.TryCatchFinally(Try1_Body,Try1_Finally,Try1_Handlers)
                    :Try0;
            }
        }
        private Expression 共通Pre(UnaryExpression Unary0, ExpressionType NodeType) {
            var Unary1_Operand=this.Traverse(Unary0.Operand);
            return Expression.Assign(
                Unary1_Operand,
                Expression.MakeUnary(NodeType,Unary1_Operand,Unary0.Type,Unary0.Method)
            );
        }
        private Expression 共通Post(UnaryExpression Unary0, ExpressionType NodeType) {
            var Unary1_Operand=this.Traverse(Unary0.Operand);
            var 変数=Expression.Parameter(Unary0.Operand.Type);
            var 作業配列=this._作業配列;
            return Expression.Block(
                作業配列.Parameters設定(変数),
                作業配列.Expressions設定(
                    Expression.Assign(変数,Unary1_Operand),
                    Expression.Assign(
                        Unary1_Operand,
                        Expression.MakeUnary(NodeType,Unary1_Operand,Unary0.Type,Unary0.Method)
                    ),
                    変数
                )
            );
        }
        protected override Expression PostDecrementAssign(UnaryExpression Unary0)=> this.共通Post(Unary0, ExpressionType.Decrement);
        protected override Expression PostIncrementAssign(UnaryExpression Unary0)=> this.共通Post(Unary0, ExpressionType.Increment);
        protected override Expression PreDecrementAssign(UnaryExpression Unary0)=> this.共通Pre(Unary0, ExpressionType.Decrement);
        protected override Expression PreIncrementAssign(UnaryExpression Unary0)=> this.共通Pre(Unary0, ExpressionType.Increment);
        protected override Expression Lambda(LambdaExpression Lambda0) {
            var Lambda0_Parameters=Lambda0.Parameters;
            var Lambda1_Body=this.Traverse(Lambda0.Body);
            return Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0_Parameters);
        }
        //protected override Expression Block(BlockExpression Block0) {
        //    var Block0_Variables=Block0.Variables;
        //    var Block0_Expressions=Block0.Expressions;
        //    var Block0_Expressions_Count=Block0_Expressions.Count;
        //    var Block1_Expressions=new Expression[Block0_Expressions_Count];
        //    for(var a=0;a<Block0_Expressions_Count;a++){
        //        var Block0_Expression=Block0_Expressions[a];
        //        if(Block0_Expression.Type!=typeof(void)&&Block0_Expression is ConditionalExpression Conditional){
        //            if(Labelで終わるか(Conditional.IfTrue))
        //        }
        //        Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
        //    }
        //    return Expression.Block(Block0_Variables,Block1_Expressions);
        //    base.Block()
        //}
        /// <summary>
        /// !(!(a))→a
        /// </summary>
        /// <param name="Unary0"></param>
        /// <returns></returns>
        protected override Expression Not(UnaryExpression Unary0) {
            var Unary0_Operand = Unary0.Operand;
            var Unary1_Operand = this.Traverse(Unary0_Operand);
            if(Unary0_Operand==Unary1_Operand)return Unary0;
            if(Unary1_Operand.NodeType==ExpressionType.Not)return ((UnaryExpression)Unary1_Operand).Operand;
            return Expression.Not(Unary1_Operand);
        }
        protected override Expression MakeAssign(BinaryExpression Binary0, ExpressionType NodeType) {
            var Binary0_Left=Binary0.Left;
            var Binary0_Right=Binary0.Right;
            var Binary0_Conversion=Binary0.Conversion;
            var Binary1_Left=this.Traverse(Binary0_Left);
            var Binary1_Right=this.Traverse(Binary0_Right);
            var Binary1_Conversion= this.TraverseNullable(Binary0_Conversion);
            if(Binary0_Left==Binary1_Left && Binary0_Right==Binary1_Right && Binary0_Conversion==Binary1_Conversion) return Binary0;
            return Expression.MakeBinary(NodeType,Binary1_Left,Binary1_Right,Binary0.IsLiftedToNull,Binary0.Method,Binary1_Conversion as LambdaExpression);
        }
        private Expression 共通ConvertConvertChecked(UnaryExpression Unary0, ExpressionType NodeType) {
            Debug.Assert(NodeType==ExpressionType.Convert||NodeType==ExpressionType.ConvertChecked);
            var Unary0_Type =Unary0.Type;
            var Unary0_Operand=Unary0.Operand;
            var Unary0_Operand_Type=Unary0_Operand.Type;
            //(Int32)3はキャストを除く
            //(Func<Int32>)(()=>3)は除かない
            if(Unary0_Type==Unary0_Operand_Type&&Unary0_Operand.NodeType!=ExpressionType.Lambda)return this.Traverse(Unary0_Operand);
            if(Unary0.Method is null)return base.Convert(Unary0);
            Debug.Assert(Unary0.Method.GetParameters().Length==1);
            if(
                Unary0_Type==typeof(IntPtr) && (Unary0_Operand_Type==typeof(int) || 
                                                Unary0_Operand_Type==typeof(long)) ||
                Unary0_Type==typeof(UIntPtr) && (Unary0_Operand_Type==typeof(uint) || 
                                                 Unary0_Operand_Type==typeof(ulong)) ||
                (Unary0_Type==typeof(int) || Unary0_Type==typeof(long)) && Unary0_Operand_Type==typeof(IntPtr) ||
                (Unary0_Type==typeof(uint) || Unary0_Type==typeof(ulong)) && Unary0_Operand_Type==typeof(UIntPtr)
            )return Expression.MakeUnary(NodeType,this.Traverse(Unary0_Operand),Unary0_Type);
            var Unary1_Operand=this.Traverse(Unary0_Operand);
            if(Unary0_Operand==Unary1_Operand) return Unary0;
            return Expression.MakeUnary(NodeType,Unary1_Operand,Unary0_Type,Unary0.Method);
        }
        //protected override Expression Convert(UnaryExpression Unary0)=> this.共通ConvertConvertChecked(Unary0, ExpressionType.Convert);
        protected override Expression Convert(UnaryExpression Unary0) {
            var Unary0_Type = Unary0.Type;
            var Unary0_Operand = Unary0.Operand;
            if(Unary0_Operand is ConstantExpression Constant&&Unary0_Operand.Type==typeof(string)) {
                var Constant_Value=Constant.Value!;
                if(Unary0_Type==typeof(sbyte         ))return Expression.Constant(sbyte         .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(short         ))return Expression.Constant(short         .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(int           ))return Expression.Constant(int           .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(long          ))return Expression.Constant(long          .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(byte          ))return Expression.Constant(byte          .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(ushort        ))return Expression.Constant(ushort        .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(uint          ))return Expression.Constant(uint          .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(ulong         ))return Expression.Constant(ulong         .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(DateTime      ))return Expression.Constant(DateTime      .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(DateTimeOffset))return Expression.Constant(DateTimeOffset.Parse((string)Constant_Value));
                if(Unary0_Type==typeof(Guid          ))return Expression.Constant(Guid          .Parse((string)Constant_Value));
                if(Unary0_Type==typeof(object))return Constant;
                Debug.Fail("ありえない");
            }
            return this.共通ConvertConvertChecked(Unary0,ExpressionType.Convert);
        }
        protected override Expression ConvertChecked(UnaryExpression Unary0)=> this.共通ConvertConvertChecked(Unary0, ExpressionType.ConvertChecked);

        /// <summary>
        /// 末尾最適化できる部分多いが煩雑になるので素直にthis.Call再帰する。
        /// </summary>
        /// <param name="MethodCall0"></param>
        /// <returns></returns>
        protected override Expression Call(MethodCallExpression MethodCall0) {
            var MethodCall0_Method = MethodCall0.Method;
            var MethodCall0_Arguments = MethodCall0.Arguments;
            if(MethodCall0_Method.IsStatic) {
                var MethodCall0_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall0_Method);
                if(ループ展開可能メソッドか(MethodCall0_GenericMethodDefinition)) {
                    //内部のSelectManyのにSelectManyのsource,selectorに分離する。
                    //任意のメソッド
                    //    SelectMany<TSource,TResult>
                    //        O
                    //        o=>I
                    //    X
                    //SelectMany<TSource,TResult>
                    //    Where
                    //        O
                    //        o=>o==0
                    //    o=>Where
                    //        I
                    //        i=>o==i&&i==1
                    var 作業配列 = this._作業配列;
                    switch(MethodCall0_Method.Name) {
                        case nameof(Enumerable.Average): {
                            if(Reflection.ExtensionEnumerable.AverageDecimal==MethodCall0_GenericMethodDefinition)
                                return 集約を集約_selectorに変換TSource(MethodCall0,MethodCall0.Type,Reflection.ExtensionEnumerable.AverageDecimal_selector);
                            if(Reflection.ExtensionEnumerable.AverageDouble==MethodCall0_GenericMethodDefinition)
                                return 集約を集約_selectorに変換TSource(MethodCall0,MethodCall0.Type,Reflection.ExtensionEnumerable.AverageDouble_selector);
                            if(Reflection.ExtensionEnumerable.AverageNullableDecimal==MethodCall0_GenericMethodDefinition)
                                return 集約を集約_selectorに変換TSource(MethodCall0,MethodCall0.Type,Reflection.ExtensionEnumerable.AverageNullableDecimal_selector);
                            if(Reflection.ExtensionEnumerable.AverageNullableDouble==MethodCall0_GenericMethodDefinition)
                                return 集約を集約_selectorに変換TSource(MethodCall0,MethodCall0.Type,Reflection.ExtensionEnumerable.AverageNullableDouble_selector);
                            MethodCallExpression 集約を集約_selectorに変換TSource(MethodCallExpression MethodCall00,Type SourceType,MethodInfo 集約_selector) {
                                var 作業配列 = this._作業配列;
                                var p = Expression.Parameter(SourceType,$"Averageﾟ{this.番号++}");
                                var MethodCall01_Arguments_0 = this.Traverse(MethodCall00.Arguments[0]);
                                return Expression.Call(
                                    作業配列.MakeGenericMethod(
                                        集約_selector,
                                        SourceType
                                    ),
                                    MethodCall01_Arguments_0,
                                    Expression.Lambda(
                                        p,
                                        作業配列.Parameters設定(p)
                                    )
                                );
                            }
                            break;
                        }
                        case nameof(Enumerable.Any): {
                            var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
                            var MethodCall1_Arguments_0=this.Traverse(MethodCall0_Arguments_0);
                            while(MethodCall1_Arguments_0 is MethodCallExpression MethodCall){
                                if(ループ展開可能メソッドか(MethodCall)){
                                    if(MethodCall.Method.Name is nameof(Enumerable.GroupJoin) or nameof(Enumerable.Select)){
                                        MethodCall1_Arguments_0=MethodCall.Arguments[0];
                                    } else
                                        break;
                                } else
                                    break;

                            }
                            //while(ループ展開可能メソッドか(MethodCall1_Arguments_0)){
                            //    //MethodCall1_Arguments_0=MethodCall1_MethodCall1.Arguments[0];
                            //    var MethodCall1_MethodCall0_Method=MethodCall1_MethodCall1.Method;
                            //    while(MethodCall1_MethodCall0_Method.Name==nameof(Enumerable.GroupJoin)||
                            //          MethodCall1_MethodCall0_Method.Name==nameof(Enumerable.Select)){
                            //        MethodCall1_MethodCall1=MethodCall1_Arguments_0;
                            //        Debug.Assert(MethodCall1_MethodCall1.Arguments[0] is not null);
                            //        if(MethodCall1_MethodCall1.Arguments[0] is MethodCallExpression x){
                            //            MethodCall1_MethodCall1=x;
                            //            //O.GroupJoin().Select().Any()
                            //            //O.Select().GroupJoin().Any()
                            //            MethodCall1_Arguments_0=MethodCall1_MethodCall1;
                            //            MethodCall1_MethodCall0_Method=MethodCall1_MethodCall1.Method;
                            //        } else{
                            //            //O.GroupJoin().Any()
                            //            //O.Select().Any()
                            //            break;
                            //        }
                            //    }
                            //    break;
                            //}

                            var GenericArguments = this._作業配列.GetGenericArguments(MethodCall1_Arguments_0.Type);
                            MethodCallExpression MethodCall1;
                            if(Reflection.ExtensionEnumerable.Any_predicate==MethodCall0_GenericMethodDefinition) {
                                MethodCall1=Expression.Call(
                                    Reflection.ExtensionEnumerable.Any.MakeGenericMethod(GenericArguments),
                                    Expression.Call(
                                        Reflection.ExtensionEnumerable.Where.MakeGenericMethod(GenericArguments),
                                        MethodCall1_Arguments_0,
                                        this.Traverse(MethodCall0_Arguments[1])
                                    )
                                );
                            } else {
                                Debug.Assert(
                                    Reflection.ExtensionSet.Any==MethodCall0_GenericMethodDefinition||
                                    Reflection.ExtensionEnumerable.Any==MethodCall0_GenericMethodDefinition
                                );
                                //break先で共通処理だがCallのオーバーロードが1引数なので
                                MethodCall1=Expression.Call(
                                    MethodCall0_GenericMethodDefinition.MakeGenericMethod(GenericArguments),
                                    MethodCall1_Arguments_0
                                );
                            }
                            return MethodCall1;
                        }
                        case nameof(Enumerable.Contains): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var MethodCall1_Arguments_0_Type = MethodCall1_Arguments_0.Type;
                            Type[] GenericArguments;
                            Type GenericArgument;
                            if(MethodCall1_Arguments_0_Type.IsArray) {
                                GenericArgument=MethodCall1_Arguments_0_Type.GetElementType()!;
                                GenericArguments=作業配列.Types設定(GenericArgument);
                            } else {
                                Type? Set1 = MethodCall1_Arguments_0_Type;
                                while(true) {
                                    if(Set1 is null) {
                                        GenericArguments=IEnumerable1(MethodCall1_Arguments_0_Type).GetGenericArguments();
                                        break;
                                    }
                                    var GenericTypeDefinition = Set1;
                                    if(GenericTypeDefinition.IsGenericType) {
                                        GenericTypeDefinition=Set1.GetGenericTypeDefinition();
                                    }
                                    if(GenericTypeDefinition==typeof(ImmutableSet<>)) {
                                        GenericArguments=Set1.GetGenericArguments();
                                        break;
                                    }
                                    Set1=Set1.BaseType;
                                }
                                GenericArgument=GenericArguments[0];
                            }
                            Debug.Assert(GenericArgument is not null,"GenericArgument != null");
                            var p = Expression.Parameter(
                                GenericArgument,
                                $"Containsﾟ{this.番号++}"
                            );
                            var (Where, Any)=MethodCall0_GenericMethodDefinition.DeclaringType==typeof(ExtensionSet)
                                ? (Reflection.ExtensionSet.Where, Reflection.ExtensionSet.Any)
                                : (Reflection.ExtensionEnumerable.Where, Reflection.ExtensionEnumerable.Any);
                            Any=Any.MakeGenericMethod(GenericArguments);
                            Where=Where.MakeGenericMethod(MethodCall0_Method.GetGenericArguments());
                            var q = this.Traverse(MethodCall0_Arguments[1]);
                            Expression EqualExpression;
                            var p_Type = p.Type;
                            var q_Type = q.Type;
                            //Object a;Int32 b
                            //aは上位クラスでもいい。
                            Debug.Assert(p_Type.IsAssignableFrom(q.Type));
                            if(p_Type.IsPrimitive) {
                                EqualExpression=Expression.Equal(
                                    p,
                                    q
                                );
                            } else {
                                var IEquatableType = typeof(IEquatable<>).MakeGenericType(GenericArguments);
                                if(IEquatableType.IsAssignableFrom(p_Type)) {
                                    var InterfaceMap = p_Type.GetInterfaceMap(IEquatableType);
                                    Debug.Assert(InterfaceMap.InterfaceMethods[0]==
                                                 IEquatableType.GetMethod(nameof(IEquatable<int>.Equals)));
                                    EqualExpression=Expression.Call(
                                        p,
                                        InterfaceMap.TargetMethods[0],
                                        作業配列.Expressions設定(q)
                                    );
                                } else {
                                    if(q_Type.IsValueType) {
                                        q=Expression.Convert(
                                            q,
                                            typeof(object)
                                        );
                                    }
                                    EqualExpression=Expression.Call(
                                        p,
                                        Reflection.Object.Equals_,
                                        作業配列.Expressions設定(q)
                                    );
                                }
                            }
                            return Expression.Call(
                                Any,
                                Expression.Call(
                                    Where,
                                    MethodCall1_Arguments_0,
                                    Expression.Lambda(
                                        EqualExpression,
                                        作業配列.Parameters設定(p)
                                    )
                                )
                            );
                        }
                        case nameof(ExtensionSet.Delete): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                            var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1
                            );
                            if(SelectMany is not null) return SelectMany;
                            if(MethodCall1_Arguments_1 is LambdaExpression MethodCall1_predicate) {
                                return Expression.Call(
                                    Reflection.ExtensionSet.Where.MakeGenericMethod(MethodCall0_Method.GetGenericArguments()),
                                    MethodCall1_Arguments_0,
                                    Expression.Lambda(
                                        MethodCall1_predicate.Type,
                                        Expression.Not(MethodCall1_predicate.Body),
                                        MethodCall1_predicate.Parameters
                                    )
                                );
                            }
                            return Expression.Call(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1
                            );
                        }
                        case nameof(Enumerable.GroupBy): {
                            if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector,Reflection.ExtensionEnumerable.Select_selector);
                            } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector_comparer==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer,Reflection.ExtensionEnumerable.Select_selector);
                            } else if(Reflection.ExtensionSet.GroupBy_keySelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector_resultSelector(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector,Reflection.ExtensionSet.Select_selector);
                            }
                            Expression GroupBy_keySelector_resultSelector(MethodInfo GroupBy_keySelector,MethodInfo Select_selector) {
                                //source.GroupBy(x => x.Id).Select(g =>new{Id=g.Key,Count=g.Count()})
                                //source.GroupBy(x => x.Id,   (Key,g)=>new{Id=  Key,Count=g.Count()})
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments[2]);
                                var GenericArguments = MethodCall0_Method.GetGenericArguments();
                                var TSource = GenericArguments[0];
                                var TKey = GenericArguments[1];
                                var TResult = GenericArguments[2];
                                GroupBy_keySelector=作業配列.MakeGenericMethod(
                                    GroupBy_keySelector,
                                    TSource,
                                    TKey,
                                    TSource
                                );
                                var e = Expression.Parameter(TSource,"e");
                                var elementSelector = Expression.Lambda(
                                    e,
                                    作業配列.Parameters設定(e)
                                );
                                MethodCallExpression GroupBy;
                                if(MethodCall0_Arguments.Count==3) {
                                    GroupBy=Expression.Call(
                                        GroupBy_keySelector,
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1,
                                        elementSelector
                                    );
                                } else {
                                    GroupBy=Expression.Call(
                                        GroupBy_keySelector,
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1,
                                        elementSelector,
                                        this.Traverse(MethodCall0_Arguments[3])
                                    );
                                }
                                var TGrouping = GroupBy_keySelector.ReturnType.GetGenericArguments()[0];
                                var p = Expression.Parameter(TGrouping,"p");
                                var p_Key = Expression.Property(
                                    p,
                                    nameof(IGrouping<int,int>.Key)
                                );
                                var p_Source = p;
                                Expression selector_Body;
                                if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
                                    //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                        (TKey Key,TSource Source)=>new{Id=         Key,Count=Source  .Count()})
                                    //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>((IGrouping<TKey,TSource>Grouping=>new{Id=Grouping.Key,Count=Grouping.Count()})
                                    var resultSelector_Parameters = resultSelector.Parameters;
                                    selector_Body=this.変換_旧Parameterを新Expression2.実行(
                                        resultSelector.Body,
                                        resultSelector_Parameters[0],
                                        p_Key,
                                        resultSelector_Parameters[1],
                                        p_Source
                                    );
                                } else {
                                    //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                     resultSelector         )
                                    //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>(g =>resultSelector(g.Key,g))
                                    selector_Body=Expression.Invoke(
                                        MethodCall1_Arguments_2,
                                        作業配列.Expressions設定(
                                            p_Key,
                                            p_Source
                                        )
                                    );
                                }
                                return Expression.Call(
                                    作業配列.MakeGenericMethod(
                                        Select_selector,
                                        TGrouping,
                                        TResult
                                    ),
                                    GroupBy,
                                    Expression.Lambda(
                                        selector_Body,
                                        作業配列.Parameters設定(p)
                                    )
                                );
                            }
                            if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector_elementSelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector,Reflection.ExtensionEnumerable.Select_selector);
                            } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector_comparer==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector_elementSelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer,Reflection.ExtensionEnumerable.Select_selector);
                            } else if(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector_elementSelector_resultSelector(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector,Reflection.ExtensionSet.Select_selector);
                            }
                            Expression GroupBy_keySelector_elementSelector_resultSelector(MethodInfo GroupBy_keySelector_elementSelector,MethodInfo Select_selector) {
                                //source.GroupBy<TSource,TKey,TElemnt,TResult>(keySelector,elementSelector,resultSelector)
                                //source.GroupBy<TSource,TKey,TElemnt>(keySelector,elementSelector).Select<TGropuing,TResult>(IGrouping)=>resultSelect(IGrouping.Key,IGrouping))
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments[2]);
                                var MethodCall1_Arguments_3 = this.Traverse(MethodCall0_Arguments[3]);
                                var GenericArguments = MethodCall0_Method.GetGenericArguments();
                                var TSource = GenericArguments[0];
                                var TKey = GenericArguments[1];
                                var TElement = GenericArguments[2];
                                var TResult = GenericArguments[3];
                                var GroupBy_keySelector = 作業配列.MakeGenericMethod(
                                    GroupBy_keySelector_elementSelector,
                                    TSource,
                                    TKey,
                                    TElement
                                );
                                MethodCallExpression GroupBy;
                                if(MethodCall0_Arguments.Count==4) {
                                    GroupBy=Expression.Call(
                                        GroupBy_keySelector,
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1,
                                        MethodCall1_Arguments_2
                                    );
                                } else {
                                    GroupBy=Expression.Call(
                                        GroupBy_keySelector,
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1,
                                        MethodCall1_Arguments_2,
                                        this.Traverse(MethodCall0_Arguments[4])
                                    );
                                }
                                var TGrouping = GroupBy_keySelector.ReturnType.GetGenericArguments()[0];
                                var p = Expression.Parameter(TGrouping,"p");
                                var p_Key = Expression.Property(
                                    p,
                                    nameof(IGrouping<int,int>.Key)
                                );
                                var p_Source = p;
                                Expression selector_Body;
                                if(MethodCall1_Arguments_3 is LambdaExpression resultSelector) {
                                    //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                        (TKey Key,TSource Source)=>new{Id=         Key,Count=Source  .Count()})
                                    //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>((IGrouping<TKey,TSource>Grouping=>new{Id=Grouping.Key,Count=Grouping.Count()})
                                    var resultSelector_Parameters = resultSelector.Parameters;
                                    selector_Body=this.変換_旧Parameterを新Expression2.実行(
                                        resultSelector.Body,
                                        resultSelector_Parameters[0],
                                        p_Key,
                                        resultSelector_Parameters[1],
                                        p_Source
                                    );
                                } else {
                                    //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                     resultSelector         )
                                    //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>(g =>resultSelector(g.Key,g))
                                    selector_Body=Expression.Invoke(
                                        MethodCall1_Arguments_3,
                                        作業配列.Expressions設定(
                                            p_Key,
                                            p_Source
                                        )
                                    );
                                }
                                return Expression.Call(
                                    作業配列.MakeGenericMethod(
                                        Select_selector,
                                        TGrouping,
                                        TResult
                                    ),
                                    GroupBy,
                                    Expression.Lambda(
                                        selector_Body,
                                        作業配列.Parameters設定(p)
                                    )
                                );
                            }
                            if(Reflection.ExtensionEnumerable.GroupBy_keySelector==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector);
                            } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_comparer==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer);
                            } else if(Reflection.ExtensionSet.GroupBy_keySelector==MethodCall0_GenericMethodDefinition) {
                                return GroupBy_keySelector(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector);
                            }
                            Expression GroupBy_keySelector(MethodInfo GroupBy_keySelector_elementSelector) {
                                //source.GroupBy<TSource,TKey,TElemnt,TResult>(keySelector,elementSelector,resultSelector)
                                //source.GroupBy<TSource,TKey,TElemnt>(keySelector,elementSelector).Select<TGropuing,TResult>(IGrouping)=>resultSelect(IGrouping.Key,IGrouping))
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                var GenericArguments = MethodCall0_Method.GetGenericArguments();
                                var TSource = GenericArguments[0];
                                var TKey = GenericArguments[1];
                                GroupBy_keySelector_elementSelector=作業配列.MakeGenericMethod(
                                    GroupBy_keySelector_elementSelector,
                                    TSource,
                                    TKey,
                                    TSource
                                );
                                var e = Expression.Parameter(TSource,"e");
                                var elementSelector = Expression.Lambda(
                                    e,
                                    作業配列.Parameters設定(e)
                                );
                                if(MethodCall0_Arguments.Count==2) {
                                    return Expression.Call(
                                        GroupBy_keySelector_elementSelector,
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1,
                                        elementSelector
                                    );
                                } else {
                                    return Expression.Call(
                                        GroupBy_keySelector_elementSelector,
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1,
                                        elementSelector,
                                        this.Traverse(MethodCall0_Arguments[2])
                                    );
                                }
                            }
                            break;
                        }
                        case nameof(Enumerable.GroupJoin): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                            var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments[2]);
                            var MethodCall1_Arguments_3 = this.Traverse(MethodCall0_Arguments[3]);
                            var MethodCall1_Arguments_4 = this.Traverse(MethodCall0_Arguments[4]);
                            var GenericArguments = MethodCall0_Method.GetGenericArguments();
                            var TOuter = GenericArguments[0];
                            var TInner = GenericArguments[1];
                            var TKey = GenericArguments[2];
                            var TResult = GenericArguments[3];
                            Expression Equals_this;
                            ParameterExpression o;
                            if(MethodCall1_Arguments_2 is LambdaExpression outerKeySelector) {
                                //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,(o,i)=>o,i)
                                //O.Select   <TOuter,            TResult>(o=>I.Where(i=>o==i)   )
                                o=outerKeySelector.Parameters[0];
                                Equals_this=outerKeySelector.Body;
                            } else {
                                //O.GroupJoin<TOuter,TInner,TKey,TResult>(   I,outerKeySelector,i=>i,(o,i)=>o,i)
                                //O.Select   <TOuter,            TResult>(o=>I.Where(i=>outerKeySelector(o).Equals(i)))
                                o=Expression.Parameter(TOuter,"o");
                                Equals_this=Expression.Invoke(
                                    MethodCall1_Arguments_2,
                                    o
                                );
                            }
                            Expression Equals_Argument;
                            ParameterExpression i;
                            if(MethodCall1_Arguments_3 is LambdaExpression innerKeySelector) {
                                //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,(o,i)=>o,i)
                                //O.Select   <TOuter,            TResult>(o=>I.Where(i=>o==i)   )
                                i=innerKeySelector.Parameters[0];
                                Equals_Argument=innerKeySelector.Body;
                            } else {
                                //O.GroupJoin<TOuter,TInner,TKey,TResult>(   I,o=>      o,innerKeySelector,(o,i)=>o,i)
                                //O.Select   <TOuter,            TResult>(o=>I.Where(i=>o.Equals(innerKeySelector(i))))
                                i=Expression.Parameter(TInner,"i");
                                Equals_Argument=Expression.Invoke(
                                    MethodCall1_Arguments_3,
                                    i
                                );
                            }
                            MethodInfo Where_predicate;
                            MethodInfo Select_selector;
                            if(typeof(Enumerable)==MethodCall0_Method.DeclaringType) {
                                Where_predicate=Reflection.ExtensionEnumerable.Where;
                                Select_selector=Reflection.ExtensionEnumerable.Select_selector;
                            } else {
                                Debug.Assert(typeof(ExtensionSet)==MethodCall0_Method.DeclaringType);
                                Where_predicate=Reflection.ExtensionSet.Where;
                                Select_selector=Reflection.ExtensionSet.Select_selector;
                            }
                            var Where = Expression.Call(
                                作業配列.MakeGenericMethod(
                                    Where_predicate,
                                    TInner
                                ),
                                MethodCall1_Arguments_1,
                                Expression.Lambda(
                                    Expression.Call(
                                        作業配列.MakeGenericMethod(
                                            Reflection.Helpers.EqualityComparer_Equals,
                                            TKey
                                        ),
                                        Equals_this,
                                        Equals_Argument
                                    ),
                                    作業配列.Parameters設定(i)
                                )
                            );
                            Expression selector_Body;
                            if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,(o,i)=>o,i)
                                //O.Select   <TOuter,            TResult>(o=>o,I.Where(i=>o.Equals(i)))
                                var resultSelector_Parameters = resultSelector.Parameters;
                                selector_Body=this.変換_旧Parameterを新Expression2.実行(
                                    resultSelector.Body,
                                    resultSelector_Parameters[0],
                                    o,
                                    resultSelector_Parameters[1],
                                    Where
                                );
                            } else {
                                //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,resultSelector)
                                //O.Select   <TOuter,            TResult>(o=>resultSelector(o,I.Where(i=>o.Equals(i))))
                                selector_Body=Expression.Invoke(
                                    MethodCall1_Arguments_4,
                                    作業配列.Expressions設定(
                                        o,
                                        Where
                                    )
                                );
                            }
                            return Expression.Call(
                                作業配列.MakeGenericMethod(
                                    Select_selector,
                                    TOuter,
                                    TResult
                                ),
                                MethodCall1_Arguments_0,
                                Expression.Lambda(
                                    selector_Body,
                                    作業配列.Parameters設定(o)
                                )
                            );
                        }
                        case nameof(Enumerable.Intersect): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                            //Intersect
                            //    SelectMany
                            //        MethodCall1_MethodCall.Arguments[0]
                            //        o=>x
                            //    MethodCall1_Arguments_1
                            //SelectMany
                            //    MethodCall1_MethodCall.Arguments[0]
                            //    o=>Intersect
                            //        x
                            //        MethodCall1_Arguments_1
                            if(typeof(ExtensionSet)==MethodCall0_Method.DeclaringType) {
                                //Enumerable.Intersect
                                //    Enumerable.SelectMany
                                //Enumerable.SelectMany
                                //    Enumerable.Intersect
                                //SelectManyは重複を許しIntersectは重複除去されるので結果が異なる
                                var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                    MethodCall0_Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1
                                );
                                if(SelectMany is not null) return SelectMany;
                            }
                            if(ループ展開可能なSetのCall(MethodCall1_Arguments_1) is null) {
                                //A.Intersect(B).Where(predicate)のWhere
                                var t = MethodCall1_Arguments_0;
                                MethodCall1_Arguments_0=MethodCall1_Arguments_1;
                                MethodCall1_Arguments_1=t;
                            }
                            if(MethodCall0_Arguments.Count==3) {
                                Debug.Assert(Reflection.ExtensionEnumerable.Intersect_comparer==MethodCall0_GenericMethodDefinition);
                                return Expression.Call(
                                    MethodCall0_Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1,
                                    this.Traverse(MethodCall0_Arguments[2])
                                );
                            } else {
                                Debug.Assert(Reflection.ExtensionSet.Intersect==MethodCall0_GenericMethodDefinition||Reflection.ExtensionEnumerable.Intersect==MethodCall0_GenericMethodDefinition);
                                return Expression.Call(
                                    MethodCall0_Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1
                                );
                            }
                        }
                        case nameof(Enumerable.Join): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                            var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments[2]);
                            var MethodCall1_Arguments_3 = this.Traverse(MethodCall0_Arguments[3]);
                            var MethodCall1_Arguments_4 = this.Traverse(MethodCall0_Arguments[4]);
                            Expression? MethodCall1_Arguments_5=MethodCall0_Arguments.Count==6
                                //引数5にはComparerがあるのでそれで比較する。
                                ?this.Traverse(MethodCall0_Arguments[5])
                                :null;
                            //Join
                            //    O
                            //    I
                            //    o=>
                            //    i=>
                            //    (o,i)=>
                            //SelectMany
                            //    O
                            //    o=>
                            //        Select
                            //            Where
                            //                I
                            //                i=>o.Equals(i)
                            //            i=>o/i
                            MethodInfo SelectMany_selector, Select_selector, Where_predicate;
                            //Join,SelectManyのresultSelectorが(o,i)=>new { o,i }でなければそれに置換する
                            if(Reflection.ExtensionSet.Join==MethodCall0_GenericMethodDefinition) {
                                SelectMany_selector=Reflection.ExtensionSet.SelectMany_selector;
                                Select_selector=Reflection.ExtensionSet.Select_selector;
                                Where_predicate=Reflection.ExtensionSet.Where;
                            } else {
                                Debug.Assert(typeof(Enumerable)==MethodCall0_GenericMethodDefinition.DeclaringType);
                                SelectMany_selector=Reflection.ExtensionEnumerable.SelectMany_selector;
                                Select_selector=Reflection.ExtensionEnumerable.Select_selector;
                                Where_predicate=Reflection.ExtensionEnumerable.Where;
                            }
                            var MethodCall0_Method_GetGenericArguments = MethodCall0_Method.GetGenericArguments();
                            var TOuter = MethodCall0_Method_GetGenericArguments[0];
                            var TInner = MethodCall0_Method_GetGenericArguments[1];
                            var TKey = MethodCall0_Method_GetGenericArguments[2];
                            var TResult = MethodCall0_Method_GetGenericArguments[3];
                            LambdaExpression selector;
                            Expression Equals_this;
                            Expression Equals_Argument;
                            IEnumerable<ParameterExpression> predicate_Parameters;
                            ParameterExpression o;
                            {
                                if(MethodCall1_Arguments_2 is LambdaExpression outerKeySelector) {
                                    o=outerKeySelector.Parameters[0];
                                    if(MethodCall1_Arguments_3 is LambdaExpression innerKeySelector) {
                                        Equals_Argument=innerKeySelector.Body;
                                        var innerKeySelector_Parameters = innerKeySelector.Parameters;
                                        predicate_Parameters=innerKeySelector_Parameters;
                                        if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                            //Join
                                            //    O
                                            //    I
                                            //    o0=>o0*o0                       outerKeySelector
                                            //    i0=>i0*i0                       innerKeySelector
                                            //    (o1,i1)=>{o1,i1}
                                            //SelectMany
                                            //    O
                                            //    o0=>                            outerKeySelector.Parameters
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i0=>                innerKeySelector.Parameters
                                            //                    (o0*o0).Equals( Equals_this=outerKeySelector.Body
                                            //                        i0*i0       Equals_Argument=innerKeySelector.Body
                                            //                    )
                                            //            i1=>                    resultSelector
                                            //                {o0,i1}
                                            selector=Expression.Lambda(
                                                this.変換_旧Parameterを新Expression1.実行(
                                                    resultSelector.Body,
                                                    resultSelector.Parameters[0],
                                                    o
                                                ),
                                                作業配列.Parameters設定(resultSelector.Parameters[1])
                                            );
                                        } else {
                                            //Join
                                            //    O
                                            //    I
                                            //    o0=>o0*o0                       outerKeySelector
                                            //    i0=>i0*i0                       innerKeySelector
                                            //    resultSelector
                                            //SelectMany
                                            //    O
                                            //    o0=>                            outerKeySelector.Parameters
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i0=>                innerKeySelector.Parameters
                                            //                    (o0*o0).Equals( Equals_this=outerKeySelector.Body
                                            //                        i0*i0       Equals_Argument=innerKeySelector.Body
                                            //                    )
                                            //            i0=>                    resultSelector
                                            //                resultSelector(o0,i0)
                                            selector=Expression.Lambda(
                                                Expression.Invoke(
                                                    MethodCall1_Arguments_4,
                                                    作業配列.Expressions設定(
                                                        o,
                                                        innerKeySelector_Parameters[0]
                                                    )
                                                ),
                                                innerKeySelector_Parameters
                                            );
                                        }
                                    } else {
                                        ParameterExpression i;
                                        if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                            //Join
                                            //    O
                                            //    I
                                            //    o0=>o0*o0                                outerKeySelector
                                            //    innerKeySelector
                                            //    (o1,i1)=>{o1,i1}                         resultSelector
                                            //SelectMany
                                            //    O
                                            //    o0=>                                     outerKeySelector.Parameters
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i1=>
                                            //                    (o0*o0).Equals(          Equals_this=outerKeySelector.Body
                                            //                        innerKeySelector(i1) innerKeySelector(i2)
                                            //                    )
                                            //            i1=>{o0,i1}
                                            var resultSelector_Parameters = resultSelector.Parameters;
                                            i=resultSelector_Parameters[1];
                                            selector=Expression.Lambda(
                                                this.変換_旧Parameterを新Expression1.実行(
                                                    resultSelector.Body,
                                                    resultSelector_Parameters[0],
                                                    o
                                                ),
                                                作業配列.Parameters設定(i)
                                            );
                                        } else {
                                            //Join
                                            //    O
                                            //    I
                                            //    o0=>o0*o0
                                            //    innerKeySelector
                                            //    resultSelector
                                            //SelectMany
                                            //    O
                                            //    o0=>
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i2=>
                                            //                    (o0*o0).Equals(          Equals_this=outerKeySelector.Body
                                            //                        innerKeySelector(i2) Equals_Argument=innerKeySelector(i2)
                                            //                    )
                                            //            i2=>resultSelector(o0,i2)
                                            i=Expression.Parameter(TInner,"i2");
                                            selector=Expression.Lambda(
                                                Expression.Invoke(
                                                    MethodCall1_Arguments_4,
                                                    作業配列.Expressions設定(
                                                        o,
                                                        i
                                                    )
                                                ),
                                                作業配列.Parameters設定(i)
                                            );
                                        }
                                        Equals_Argument=Expression.Invoke(
                                            MethodCall1_Arguments_3,
                                            i
                                        );
                                        predicate_Parameters=作業配列.Parameters設定(i);
                                    }
                                    Equals_this=outerKeySelector.Body;
                                } else {
                                    if(MethodCall1_Arguments_3 is LambdaExpression innerKeySelector) {
                                        var innerKeySelector_Parameters = innerKeySelector.Parameters;
                                        predicate_Parameters=innerKeySelector.Parameters;
                                        var innerKeySelector_Body = innerKeySelector.Body;
                                        if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                            //Join
                                            //    O
                                            //    I
                                            //    outerKeySelector
                                            //    i0=>i0*i0
                                            //    (o1,i1)=>{o1,i1}
                                            //SelectMany
                                            //    O
                                            //    o0=>
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i0=>
                                            //                    outerKeySelector(o0).Equals(
                                            //                        i0*i0
                                            //                    )
                                            //            i1=>{o0,i1}
                                            o=resultSelector.Parameters[0];
                                            selector=Expression.Lambda(
                                                this.変換_旧Parameterを新Expression1.実行(
                                                    resultSelector.Body,
                                                    resultSelector.Parameters[0],
                                                    o
                                                ),
                                                作業配列.Parameters設定(resultSelector.Parameters[1])
                                            );
                                        } else {
                                            //Join
                                            //    O
                                            //    I
                                            //    outerKeySelector
                                            //    i0=>i0*i0
                                            //    resultSelector
                                            //SelectMany
                                            //    O
                                            //    o2=>
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i0=>
                                            //                    outerKeySelector(o2).Equals(
                                            //                        i0*i0
                                            //                    )
                                            //            i0=>resultSelector(o2,i0)
                                            o=Expression.Parameter(TOuter,"o2");
                                            selector=Expression.Lambda(
                                                Expression.Invoke(
                                                    MethodCall1_Arguments_4,
                                                    作業配列.Expressions設定(
                                                        o,
                                                        innerKeySelector_Parameters[0]
                                                    )
                                                ),
                                                innerKeySelector_Parameters
                                            );
                                        }
                                        Equals_Argument=innerKeySelector_Body;
                                    } else {
                                        var Parameters1 = 作業配列.Parameters1;
                                        predicate_Parameters=Parameters1;
                                        ParameterExpression i;
                                        if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                            //Join
                                            //    O
                                            //    I
                                            //    outerKeySelector
                                            //    innerKeySelector
                                            //    (o1,i1)=>{o1,i1}
                                            //SelectMany
                                            //    O
                                            //    o1=>
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i1=>
                                            //                    outerKeySelector(o2).Equals(
                                            //                        innerKeySelector(i1)
                                            //                    )
                                            //            i1=>{o1,i1}
                                            var resultSelector_Parameters = resultSelector.Parameters;
                                            o=resultSelector_Parameters[0];
                                            i=resultSelector_Parameters[1];
                                            selector=Expression.Lambda(
                                                resultSelector.Body,
                                                作業配列.Parameters設定(i)
                                            );
                                        } else {
                                            //Join
                                            //    O
                                            //    I
                                            //    outerKeySelector
                                            //    innerKeySelector
                                            //    resultSelector
                                            //SelectMany
                                            //    O
                                            //    o2=>
                                            //        Select
                                            //            Where
                                            //                I
                                            //                i2=>
                                            //                    outerKeySelector(o2).Equals(
                                            //                        innerKeySelector(i2)
                                            //                    )
                                            //            i2=>resultSelector(o2,i2)
                                            o=Expression.Parameter(TOuter,"o2");
                                            i=Expression.Parameter(TInner,"i2");
                                            selector=Expression.Lambda(
                                                Expression.Invoke(
                                                    MethodCall1_Arguments_4,
                                                    作業配列.Expressions設定(
                                                        o,
                                                        i
                                                    )
                                                ),
                                                作業配列.Parameters設定(i)
                                            );
                                        }
                                        Parameters1[0]=i;
                                        Equals_Argument=Expression.Invoke(
                                            MethodCall1_Arguments_3,
                                            i
                                        );
                                    }
                                    Equals_this=Expression.Invoke(
                                        MethodCall1_Arguments_2,
                                        o
                                    );
                                }
                            }
                            Expression predicate_Body;
                            if(MethodCall1_Arguments_5 is not null) {
                                //引数5にはComparerがあるのでそれで比較する。
                                var MethodCall1_Arguments_5_Type = MethodCall1_Arguments_5.Type;
                                var T = MethodCall1_Arguments_5_Type.GetInterface(CommonLibrary.IEqualityComparer_FullName)!.GetGenericArguments()[0];
                                predicate_Body=Expression.Call(
                                    MethodCall1_Arguments_5,
                                    作業配列.GetMethod(
                                        MethodCall1_Arguments_5_Type,
                                        nameof(IEqualityComparer<int>.Equals),
                                        T,
                                        T
                                    ),
                                    Equals_this,
                                    Equals_Argument
                                );
                            } else {
                                predicate_Body=Expression.Call(
                                    作業配列.MakeGenericMethod(
                                        Reflection.Helpers.EqualityComparer_Equals,
                                        TKey
                                    ),
                                    Equals_this,
                                    Equals_Argument
                                );
                            }
                            var Where = Expression.Call(
                                作業配列.MakeGenericMethod(
                                    Where_predicate,
                                    TInner
                                ),
                                MethodCall1_Arguments_1,
                                Expression.Lambda(
                                    predicate_Body,
                                    predicate_Parameters
                                )
                            );
                            var Select = Expression.Call(
                                作業配列.MakeGenericMethod(
                                    Select_selector,
                                    TInner,
                                    TResult
                                ),
                                Where,
                                selector
                            );
                            var SelectMany = Expression.Call(
                                作業配列.MakeGenericMethod(
                                    SelectMany_selector,
                                    TOuter,
                                    TResult
                                ),
                                MethodCall1_Arguments_0,
                                Expression.Lambda(
                                    Select,
                                    作業配列.Parameters設定(o)
                                )
                            );
                            //todo goto SelectMany;
                            Debug.Assert(Reflection.ExtensionEnumerable.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.SelectMany_indexSelector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionSet.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition());
                            return this.Call(SelectMany);
                        }
                        case nameof(Enumerable.OfType): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0
                            );
                            if(SelectMany is not null) return SelectMany;
                            //Strings.OfType<Object>()
                            //Strings
                            if(MethodCall0.Type.IsAssignableFrom(MethodCall1_Arguments_0.Type)) {
                                return MethodCall1_Arguments_0;
                            }
                            return Expression.Call(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0
                            );
                        }
                        case nameof(Enumerable.Select): {
                            if(Reflection.ExtensionEnumerable.Select_indexSelector!=MethodCall0_GenericMethodDefinition) {
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                                    var MethodCall1_MethodCall_Method = MethodCall1_MethodCall.Method;
                                    var MethodCall1_MethodCall_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall1_MethodCall_Method);
                                    Debug.Assert(nameof(Enumerable.Join)!=MethodCall1_MethodCall_Method.Name);
                                    switch(MethodCall1_MethodCall_Method.Name) {
                                        case nameof(Enumerable.SelectMany): {
                                            if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                                                var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                                    MethodCall0_Method,
                                                    MethodCall1_MethodCall,
                                                    MethodCall1_Arguments_1
                                                );
                                                //todo goto SelectMany;
                                                Debug.Assert(Reflection.ExtensionEnumerable.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.SelectMany_indexSelector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionSet.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition());
                                                return this.Call(SelectMany);
                                            }
                                            break;
                                        }
                                        case nameof(Enumerable.Select): {
                                            if(Reflection.ExtensionEnumerable.Select_indexSelector!=MethodCall0_GenericMethodDefinition) {
                                                Debug.Assert(
                                                    Reflection.ExtensionEnumerable.Select_selector==MethodCall0_GenericMethodDefinition||
                                                    Reflection.ExtensionSet.Select_selector==MethodCall0_GenericMethodDefinition
                                                );
                                                //A.Select(selector1).Select(selector0)
                                                //A.Select(selector0(selector1))
                                                var MethodCall1_MethodCall_Arguments_1 = MethodCall1_MethodCall.Arguments[1];
                                                var MethodCall0_Arguments_1 = MethodCall0_Arguments[1];
                                                LambdaExpression Lambda;
                                                if(Reflection.ExtensionEnumerable.Select_indexSelector==MethodCall1_MethodCall_GenericMethodDefinition) {
                                                    if(Reflection.ExtensionEnumerable.Select_indexSelector==MethodCall0_GenericMethodDefinition) {
                                                        if(MethodCall1_MethodCall_Arguments_1 is LambdaExpression indexSelector1) {
                                                            var indexSelector1_Parameters = indexSelector1.Parameters;
                                                            if(MethodCall0_Arguments_1 is LambdaExpression indexSelector0) {
                                                                //O.Select_indexSelector((p1,index1)=>p1+index1).Select_indexSelector((p0,index0)=>p0*index0)
                                                                //O.Select_indexSelector((p1,index1)=>(p1+index1)*index0)
                                                                //O.Select_indexSelector((p0,index0)=>(p0+index0)*index0)
                                                                var indexSelector0_Parameters = indexSelector0.Parameters;
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression2.実行(
                                                                        indexSelector0.Body,
                                                                        indexSelector0_Parameters[0],
                                                                        indexSelector1.Body,
                                                                        indexSelector0_Parameters[1],
                                                                        indexSelector1_Parameters[1]
                                                                    ),
                                                                    indexSelector1_Parameters
                                                                );
                                                            } else {
                                                                //O.Select_indexSelector((p1,index1)=>p1+index1).Select_indexSelector(MethodCall0_Arguments_1)
                                                                //O.Select_indexSelector((p1,index1)=>MethodCall0_Arguments_1(p1+index1,index1))
                                                                var index1 = indexSelector1_Parameters[1];
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(
                                                                        MethodCall0_Arguments_1,
                                                                        作業配列.Expressions設定(
                                                                            indexSelector1.Body,
                                                                            index1
                                                                        )
                                                                    ),
                                                                    indexSelector1_Parameters
                                                                );
                                                            }
                                                        } else {
                                                            var p = Expression.Parameter(MethodCall0_Method.GetGenericArguments()[0],"p");
                                                            if(MethodCall0_Arguments_1 is LambdaExpression indexSelector0) {
                                                                //O.Select_indexSelector(MethodCall1_MethodCall_Arguments_1).Select_indexSelector((p0,index0)=>p0+index0)
                                                                //O.Select_indexSelector((p,index0)=>MethodCall1_MethodCall_Arguments_1(p,index0)+index0)
                                                                var indexSelector0_Parameters = indexSelector0.Parameters;
                                                                var index0 = indexSelector0_Parameters[1];
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        indexSelector0.Body,
                                                                        indexSelector0_Parameters[0],
                                                                        Expression.Invoke(
                                                                            MethodCall1_MethodCall_Arguments_1,
                                                                            作業配列.Expressions設定(
                                                                                p,
                                                                                index0
                                                                            )
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(p,indexSelector0_Parameters[1])
                                                                );
                                                            } else {
                                                                //O.Select_indexSelector(MethodCall1_MethodCall_Arguments_1).Select_indexSelector(MethodCall0_Arguments_1)
                                                                //O.Select_indexSelector((p,index)=>MethodCall0_Arguments_1(MethodCall1_MethodCall_Arguments_1(p,index),index))
                                                                var index = Expression.Parameter(typeof(int),"index");
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(
                                                                        MethodCall0_Arguments_1,
                                                                        作業配列.Expressions設定(
                                                                            Expression.Invoke(
                                                                                MethodCall1_MethodCall_Arguments_1,
                                                                                作業配列.Expressions設定(
                                                                                    p,
                                                                                    index
                                                                                )
                                                                            ),
                                                                            index
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(p,index)
                                                                );
                                                            }
                                                        }
                                                    } else {
                                                        if(MethodCall1_MethodCall_Arguments_1 is LambdaExpression indexSelector1) {
                                                            if(MethodCall0_Arguments_1 is LambdaExpression selector0) {
                                                                //O.Select_indexSelector((p1,index1)=>p1+index1).Select_selector(p0=>p0*p0)
                                                                //O.Select_indexSelector((p1,index1)=>(p1+index1)*(p1+index1))
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        selector0.Body,
                                                                        selector0.Parameters[0],
                                                                        indexSelector1.Body
                                                                    ),
                                                                    indexSelector1.Parameters
                                                                );
                                                            } else {
                                                                //O.Select_indexSelector((p1,index1)=>p1+index1).Select_selector(MethodCall0_Arguments_1)
                                                                //O.Select_indexSelector((p1,index1)=>MethodCall0_Arguments_1(p1+index1))
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(MethodCall0_Arguments_1,indexSelector1.Body),
                                                                    indexSelector1.Parameters
                                                                );
                                                            }
                                                        } else {
                                                            var p = Expression.Parameter(MethodCall0_Method.GetGenericArguments()[0],"p");
                                                            if(MethodCall0_Arguments_1 is LambdaExpression selector0) {
                                                                //O.Select_indexSelector(MethodCall1_MethodCall_Arguments_1).Select_selector(p0=>p0+p0)
                                                                //O.Select_indexSelector((p,index)=>MethodCall1_MethodCall_Arguments_1(p,index)+MethodCall1_MethodCall_Arguments_1(p,index))
                                                                var selector0_Parameters = selector0.Parameters;
                                                                var index = Expression.Parameter(typeof(int),"index");
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        selector0.Body,
                                                                        selector0_Parameters[0],
                                                                        Expression.Invoke(
                                                                            MethodCall1_MethodCall_Arguments_1,
                                                                            作業配列.Expressions設定(p,index)
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(p,index)
                                                                );
                                                                MethodCall1_MethodCall_GenericMethodDefinition=Reflection.ExtensionEnumerable.Select_indexSelector;
                                                            } else {
                                                                //O.Select_indexSelector(MethodCall1_MethodCall_Arguments_1).Select_selector(MethodCall0_Arguments_1)
                                                                //O.Select_indexSelector((p,index)=>MethodCall0_Arguments_1(MethodCall1_MethodCall_Arguments_1(p,index))
                                                                var index = Expression.Parameter(typeof(int),"index");
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(
                                                                        MethodCall0_Arguments_1,
                                                                        作業配列.Expressions設定(
                                                                            Expression.Invoke(
                                                                                MethodCall1_MethodCall_Arguments_1,
                                                                                作業配列.Expressions設定(p,index)
                                                                            )
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(p,index)
                                                                );
                                                            }
                                                        }
                                                    }
                                                } else {
                                                    if(Reflection.ExtensionEnumerable.Select_indexSelector==MethodCall0_GenericMethodDefinition) {
                                                        if(MethodCall1_MethodCall_Arguments_1 is LambdaExpression selector1) {
                                                            if(MethodCall0_Arguments_1 is LambdaExpression indexSelector0) {
                                                                //O.Select_selector(p1=>p1+p1).Select_indexSelector((p0,index0)=>p0*index0)
                                                                //O.Select_indexSelector((p1,index0)=>(p1+p1)*index0)
                                                                var indexSelector0_Parameters = indexSelector0.Parameters;
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        indexSelector0.Body,
                                                                        indexSelector0_Parameters[0],
                                                                        selector1.Body
                                                                    ),
                                                                    作業配列.Parameters設定(
                                                                        selector1.Parameters[0],
                                                                        indexSelector0_Parameters[1]
                                                                    )
                                                                );
                                                            } else {
                                                                //O.Select_selector(p1=>p1+p1).Select_indexSelector(MethodCall0_Arguments_1)
                                                                //O.Select_indexSelector((p1,index)=>MethodCall0_Arguments_1(p1+p1,index))
                                                                var index = Expression.Parameter(typeof(int),"index");
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(
                                                                        MethodCall0_Arguments_1,
                                                                        作業配列.Expressions設定(
                                                                            selector1.Body,
                                                                            index
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(
                                                                        selector1.Parameters[0],
                                                                        index
                                                                    )
                                                                );
                                                            }
                                                        } else {
                                                            var p = Expression.Parameter(MethodCall1_MethodCall_Method.GetGenericArguments()[0],"p");
                                                            if(MethodCall0_Arguments_1 is LambdaExpression indexSelector0) {
                                                                //O.Select_selector(MethodCall1_MethodCall_Arguments_1).Select_indexSelector((p0,index0)=>p0*index0)
                                                                //O.Select_indexSelector(p,index0)=>MethodCall1_MethodCall_Arguments_1(p)*index0)
                                                                var indexSelector0_Parameters = indexSelector0.Parameters;
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        indexSelector0.Body,
                                                                        indexSelector0_Parameters[0],
                                                                        Expression.Invoke(
                                                                            MethodCall1_MethodCall_Arguments_1,
                                                                            作業配列.Expressions設定(p)
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(
                                                                        p,
                                                                        indexSelector0_Parameters[1]
                                                                    )
                                                                );
                                                            } else {
                                                                //O.Select_selector<Int32,Int32>(MethodCall1_MethodCall_Arguments_1).Select_indexSelector<Int32,Double>(MethodCall1_Arguments_1)
                                                                //O.Select_indexSelector<Int32,Double>((p,index)=>MethodCall1_Arguments_1(MethodCall1_Arguments_1(p),index))
                                                                var index = Expression.Parameter(typeof(int),"index");
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(
                                                                        MethodCall1_Arguments_1,
                                                                        作業配列.Expressions設定(
                                                                            Expression.Invoke(
                                                                                MethodCall1_MethodCall_Arguments_1,
                                                                                作業配列.Expressions設定(p)
                                                                            ),
                                                                            index
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(p,index)
                                                                );
                                                            }
                                                        }
                                                        MethodCall1_MethodCall_GenericMethodDefinition=Reflection.ExtensionEnumerable.Select_indexSelector;
                                                    } else {
                                                        if(MethodCall1_MethodCall_Arguments_1 is LambdaExpression selector1) {
                                                            if(MethodCall0_Arguments_1 is LambdaExpression selector0) {
                                                                //O.Select_selector(p1=>p1+p1).Select_selector(p0=>p0*p0)
                                                                //O.Select_selector(p1=>(p1+p1)*(p1+p1))
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        selector0.Body,
                                                                        selector0.Parameters[0],
                                                                        selector1.Body
                                                                    ),
                                                                    selector1.Parameters
                                                                );
                                                            } else {
                                                                //O.Select_selector(p1=>p1+p1).Select_selector(MethodCall0_Arguments_1)
                                                                //O.Select_selector(p1=>MethodCall0_Arguments_1(p1+p1))
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(
                                                                        MethodCall0_Arguments_1,
                                                                        selector1.Body
                                                                    ),
                                                                    selector1.Parameters
                                                                );
                                                            }
                                                        } else {
                                                            var p1 = Expression.Parameter(MethodCall1_MethodCall_Method.GetGenericArguments()[0],"p1");
                                                            if(MethodCall0_Arguments_1 is LambdaExpression selector0) {
                                                                //O.Select_selector(MethodCall1_MethodCall_Arguments_1).Select_selector(p0=>p0+p0)
                                                                //O.Select_selector(p1=>MethodCall1_MethodCall_Arguments_1(p1)+MethodCall1_MethodCall_Arguments_1(p1)))
                                                                Lambda=Expression.Lambda(
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        selector0.Body,
                                                                        selector0.Parameters[0],
                                                                        Expression.Invoke(
                                                                            MethodCall1_MethodCall_Arguments_1,
                                                                            作業配列.Expressions設定(p1)
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(p1)
                                                                );
                                                            } else {
                                                                //O.Select_selector(selector1).Select_selector(selector0)
                                                                //O.Select_selector(p1=>selector0(selector1(p1)))
                                                                Lambda=Expression.Lambda(
                                                                    Expression.Invoke(
                                                                        MethodCall0_Arguments_1,
                                                                        Expression.Invoke(
                                                                            MethodCall1_MethodCall_Arguments_1,
                                                                            作業配列.Expressions設定(p1)
                                                                        )
                                                                    ),
                                                                    作業配列.Parameters設定(p1)
                                                                );
                                                            }
                                                        }
                                                    }
                                                }
                                                return Expression.Call(
                                                    作業配列.MakeGenericMethod(
                                                        MethodCall1_MethodCall_GenericMethodDefinition,
                                                        MethodCall1_MethodCall_Method.GetGenericArguments()[0],
                                                        MethodCall0_Method.GetGenericArguments()[1]
                                                    ),
                                                    MethodCall1_MethodCall.Arguments[0],
                                                    Lambda
                                                );
                                            }
                                            break;
                                        }
                                    }
                                }
                                //A.Select(a=>a)
                                //A
                                Debug.Assert(Reflection.ExtensionSet.Select_selector==MethodCall0_GenericMethodDefinition||Reflection.ExtensionEnumerable.Select_selector==MethodCall0_GenericMethodDefinition||Reflection.ExtensionEnumerable.Select_indexSelector==MethodCall0_GenericMethodDefinition);
                                if(MethodCall1_Arguments_1 is LambdaExpression MethodCall1_selector&&MethodCall1_selector.Parameters[0]==MethodCall1_selector.Body) {
                                    return MethodCall1_Arguments_0;
                                }
                                return Expression.Call(
                                    MethodCall0_Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1
                                );
                            }
                            break;
                        }
                        case nameof(Enumerable.Single): {
                            if(Reflection.ExtensionEnumerable.Single_predicate==MethodCall0_GenericMethodDefinition) {
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                return Expression.Call(
                                    Reflection.ExtensionEnumerable.Single.MakeGenericMethod(MethodCall0_Method.GetGenericArguments()),
                                    Expression.Call(
                                        作業配列.ElementTypeからMakeGenericMethod(
                                            Reflection.ExtensionEnumerable.Where,
                                            MethodCall1_Arguments_0.Type
                                        ),
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1
                                    )
                                );
                            }
                            break;
                        }
                        case nameof(Enumerable.SingleOrDefault): {
                            if(Reflection.ExtensionEnumerable.SingleOrDefault_predicate==MethodCall0_GenericMethodDefinition) {
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                //A.SingleOrDefault(predicate)
                                //A.Where(predicate).SingleOrDefault()
                                return Expression.Call(
                                    Reflection.ExtensionEnumerable.SingleOrDefault.MakeGenericMethod(MethodCall0_Method.GetGenericArguments()),
                                    Expression.Call(
                                        作業配列.ElementTypeからMakeGenericMethod(
                                            Reflection.ExtensionEnumerable.Where,
                                            MethodCall1_Arguments_0.Type
                                        ),
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_1
                                    )
                                );
                            }
                            break;
                        }
                        case nameof(Enumerable.ToArray): {
                            Debug.Assert(Reflection.ExtensionEnumerable.ToArray==MethodCall0_GenericMethodDefinition);
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            return MethodCall1_Arguments_0.Type.IsArray
                                ? MethodCall1_Arguments_0
                                : Expression.Call(
                                    MethodCall0_Method,
                                    MethodCall1_Arguments_0
                                );
                        }
                        case nameof(Enumerable.Except):
                        case nameof(Enumerable.Union): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                            if(MethodCall0_Arguments.Count==3) {
                                Debug.Assert(
                                    Reflection.ExtensionEnumerable.Except_comparer==MethodCall0_GenericMethodDefinition||
                                    Reflection.ExtensionEnumerable.Union_comparer==MethodCall0_GenericMethodDefinition
                                );
                                var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments[2]);
                                var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                    MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2
                                );
                                if(SelectMany is not null) return SelectMany;
                                return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2);
                            } else {
                                Debug.Assert(
                                    Reflection.ExtensionEnumerable.Except==MethodCall0_GenericMethodDefinition||
                                    Reflection.ExtensionEnumerable.Union==MethodCall0_GenericMethodDefinition||
                                    Reflection.ExtensionSet.Except==MethodCall0_GenericMethodDefinition||
                                    Reflection.ExtensionSet.Union==MethodCall0_GenericMethodDefinition
                                );
                                var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                    MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1
                                );
                                if(SelectMany is not null) return SelectMany;
                                return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
                            }
                        }
                        case nameof(ExtensionSet.Update): {
                            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                            var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                            var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments[2]);
                            var MethodCall1_Arguments_0_Type = MethodCall1_Arguments_0.Type;
                            var T = MethodCall1_Arguments_0_Type.GetGenericArguments()[0];
                            var p = Expression.Parameter(T,$"Updateﾟ{this.番号++}");
                            Debug.Assert(MethodCall0_Method.GetGenericArguments()[0]==T);
                            Expression LambdaExpressionを展開1(Expression Lambda,Expression argument) => Optimizer.LambdaExpressionを展開1(
                                Lambda,
                                argument,
                                this.変換_旧Parameterを新Expression1
                            );
                            return Expression.Call(
                                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,T,T),
                                MethodCall1_Arguments_0,
                                Expression.Lambda(
                                    MethodCall1_Arguments_2.Type,
                                    Expression.Condition(
                                        LambdaExpressionを展開1(MethodCall1_Arguments_1,p),
                                        LambdaExpressionを展開1(MethodCall1_Arguments_2,p),
                                        p,T
                                    ),
                                    作業配列.Parameters設定(p)
                                )
                            );
                        }
                        case nameof(Enumerable.SelectMany): {
                            if(MethodCall0_Arguments.Count==2) {
                                //SelectManyの内部のWhereをSelectManyのsource,selectorに分離する。
                                //SelectMany<TSource,TResult>
                                //    O
                                //    o=>Where
                                //        I
                                //        i=>o==i&&o==0&&i==1
                                //SelectMany<TSource,TResult>
                                //    Where
                                //        O
                                //        o=>o==0
                                //    o=>Where
                                //        I
                                //        i=>o==i&&i==1
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                {
                                    if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                                        //O.SelectMany(o=>I.Where(i=>o==0&&i==0))
                                        //O.Where(o=>o==0).SelectMany(o=>I.Where(i=>i==0))
                                        //に出来るが
                                        //O.SelectMany(o=>I.Where(i=>o==0&&i==0).GroupJoin.Join)
                                        //は処理できない。引数0を再帰で呼び出しWhereがあるところまで戻って処理すればいい
                                        //O.Where(o=>o==0).SelectMany(o=>I.Where(i=>i==0))

                                        switch(MethodCall1_MethodCall.Method.Name) {
                                            case nameof(ExtensionSet.SelectMany): {
                                                if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                                                    var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                                        MethodCall0_Method,
                                                        MethodCall1_MethodCall,
                                                        MethodCall1_Arguments_1
                                                    );
                                                    //todo goto SelectMany;
                                                    Debug.Assert(Reflection.ExtensionEnumerable.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.SelectMany_indexSelector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionSet.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition());
                                                    return this.Call(SelectMany);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                Expression 共通(IList<ParameterExpression> selector_Parameters,ref Expression? ref_OuterPredicate,Expression InputBody) {
                                    if(ループ展開可能メソッドか(InputBody,out var MethodCall)) {
                                        var MethodCall_Method = MethodCall.Method;
                                        switch(MethodCall_Method.Name) {
                                            case nameof(Enumerable.Where): {
                                                if(MethodCall.Arguments[1] is LambdaExpression predicate) {
                                                    var o = selector_Parameters[0];
                                                    var predicate_Parameters = predicate.Parameters;
                                                    var (OuterPredicate, OtherPredicate)=this._取得_Parameter_OuterPredicate_InnerPredicate.実行(
                                                        predicate.Body,
                                                        o
                                                    );
                                                    if(OuterPredicate is not null) {
                                                        ref_OuterPredicate=AndAlsoで繋げる(ref_OuterPredicate,OuterPredicate);
                                                    }
                                                    if(OtherPredicate is not null) {
                                                        return Expression.Call(
                                                            MethodCall_Method,//Where
                                                            MethodCall.Arguments[0],
                                                            Expression.Lambda(
                                                                OtherPredicate,
                                                                predicate_Parameters
                                                            )
                                                        );
                                                    }
                                                    return MethodCall.Arguments[0];
                                                }
                                                break;
                                            }
                                            default: {
                                                var Expressions = new Expression[MethodCall.Arguments.Count];
                                                Expressions[0]=共通(selector_Parameters,ref ref_OuterPredicate,MethodCall.Arguments[0]);
                                                for(var a = 1;a<MethodCall.Arguments.Count;a++) {
                                                    Expressions[a]=MethodCall.Arguments[a];
                                                }
                                                return Expression.Call(MethodCall_Method,Expressions);
                                            }
                                        }
                                    }
                                    return InputBody;
                                }
                                {
                                    if(MethodCall1_Arguments_1 is LambdaExpression selector&&ループ展開可能メソッドか(selector.Body,out _)) {
                                        var selector_Parameters = selector.Parameters;
                                        Expression? OuterPredicate = null;
                                        var Body=共通(
                                            selector.Parameters,
                                            ref OuterPredicate,
                                            selector.Body
                                        );
                                        if(OuterPredicate is not null) {
                                            MethodCall1_Arguments_0=this.Outer又はInnerにWhereを付ける(
                                                MethodCall1_Arguments_0,
                                                MethodCall1_Arguments_0.Type.GetInterface(CommonLibrary.IOutputSet1_FullName) is not null
                                                    ? Reflection.ExtensionSet.Where
                                                    : Reflection.ExtensionEnumerable.Where,
                                                selector_Parameters[0].Type,
                                                selector_Parameters,
                                                OuterPredicate
                                            );
                                        }
                                        MethodCall1_Arguments_1=Expression.Lambda(
                                            Body,
                                            selector_Parameters
                                        );
                                    }
                                }
                                return Expression.Call(
                                    MethodCall0_Method,//SelectMany_selector
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1
                                );
                            } else {
                                Debug.Assert(MethodCall0_Arguments.Count==3);
                                //SelectMany<TSource,ICollection,TResult>
                                //    O
                                //    o=>I
                                //    o,i=>o+i
                                //SelectMany<TSource,TResult>
                                //    O
                                //    o=>Select<ICollection,TResult>
                                //        I
                                //        i=>o+i
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                var MethodCall1_Arguments_2 = this.Traverse(MethodCall0_Arguments[2]);
                                var SelectMany_GenericArguments = MethodCall0_Method.GetGenericArguments();
                                var TSource = SelectMany_GenericArguments[0];
                                var TCollection = SelectMany_GenericArguments[1];
                                var TResult = SelectMany_GenericArguments[2];
                                MethodInfo SelectMany, Select;
                                bool indexSelectorか;
                                if(Reflection.ExtensionEnumerable.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                    SelectMany=Reflection.ExtensionEnumerable.SelectMany_selector;
                                    Select=Reflection.ExtensionEnumerable.Select_selector;
                                    indexSelectorか=false;
                                } else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                    SelectMany=Reflection.ExtensionEnumerable.SelectMany_indexSelector;
                                    Select=Reflection.ExtensionEnumerable.Select_selector;
                                    indexSelectorか=true;
                                } else {
                                    Debug.Assert(Reflection.ExtensionSet.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition);
                                    SelectMany=Reflection.ExtensionSet.SelectMany_selector;
                                    Select=Reflection.ExtensionSet.Select_selector;
                                    indexSelectorか=false;
                                }
                                SelectMany=作業配列.MakeGenericMethod(SelectMany,TSource,TResult);
                                Select=作業配列.MakeGenericMethod(Select,TCollection,TResult);
                                IEnumerable<ParameterExpression> SelectMany_Parameters;
                                Expression Select_source, Select_selector;
                                if(MethodCall1_Arguments_1 is LambdaExpression collectionSelector) {
                                    var collectionSelector_Parameters = collectionSelector.Parameters;
                                    var collectionSelector_o = collectionSelector_Parameters[0];
                                    SelectMany_Parameters=collectionSelector_Parameters;
                                    if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
                                        var resultSelector_o = resultSelector.Parameters[0];
                                        var resultSelector_i = resultSelector.Parameters[1];
                                        //SelectMany<TSource,ICollection,TResult>
                                        //    O
                                        //    (o,index)=>I+index
                                        //    o,i=>o+i
                                        //SelectMany<TSource,TResult>
                                        //    O
                                        //    (o,index)=>Select<ICollection,TResult>Select_selector
                                        //        I+index
                                        //        i=>o+i
                                        Select_selector=Expression.Lambda(
                                            this.変換_旧Parameterを新Expression1.実行(
                                                resultSelector.Body,
                                                resultSelector_o,
                                                collectionSelector_o
                                            ),
                                            this._作業配列.Parameters設定(resultSelector_i)
                                        );
                                    } else {
                                        var resultSelector_i = Expression.Parameter(TCollection,"i");
                                        //SelectMany<TSource,ICollection,TResult>
                                        //    O
                                        //    o=>I
                                        //    resultSelector
                                        //SelectMany<TSource,TResult>
                                        //    O
                                        //    o=>Select<ICollection,TResult>
                                        //        I
                                        //        i=>resultSelector(o,i)
                                        Select_selector=Expression.Lambda(
                                            Expression.Invoke(
                                                MethodCall1_Arguments_2,
                                                作業配列.Expressions設定(collectionSelector_o,resultSelector_i)
                                            ),
                                            作業配列.Parameters設定(resultSelector_i)
                                        );
                                    }
                                    Select_source=collectionSelector.Body;
                                } else {
                                    ParameterExpression SelectMany_collectionSelector_o, SelectMany_selector_i;
                                    if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
                                        SelectMany_collectionSelector_o=resultSelector.Parameters[0];
                                        SelectMany_selector_i=resultSelector.Parameters[1];
                                        Select_selector=Expression.Lambda(
                                            resultSelector.Body,
                                            作業配列.Parameters設定(SelectMany_selector_i)
                                        );
                                    } else {
                                        SelectMany_collectionSelector_o=Expression.Parameter(TSource,"o");
                                        SelectMany_selector_i=Expression.Parameter(TCollection,"i");
                                        Select_selector=Expression.Lambda(
                                            Expression.Invoke(
                                                MethodCall1_Arguments_2,
                                                作業配列.Expressions設定(SelectMany_collectionSelector_o,SelectMany_selector_i)
                                            ),
                                            作業配列.Parameters設定(SelectMany_selector_i)
                                        );
                                    }
                                    if(indexSelectorか) {
                                        //SelectMany<TSource,ICollection,TResult>
                                        //    O
                                        //    MethodCall1_Arguments_1
                                        //    MethodCall1_Arguments_2
                                        //SelectMany<TSource,TResult>
                                        //    O
                                        //    o,index=>Select<TCollection,TResult> SelectMany_collectionSelector_index
                                        //        MethodCall1_Arguments_1(o,index)
                                        //        i=>MethodCall1_Arguments_2(o,i)
                                        var SelectMany_collectionSelector_index = Expression.Parameter(typeof(int),"index");
                                        Select_source=Expression.Invoke(
                                            MethodCall1_Arguments_1,
                                            作業配列.Expressions設定(SelectMany_collectionSelector_o,SelectMany_collectionSelector_index)
                                        );
                                        SelectMany_Parameters=作業配列.Parameters設定(SelectMany_collectionSelector_o,SelectMany_collectionSelector_index);
                                    } else {
                                        //SelectMany<TSource,ICollection,TResult>
                                        //    O
                                        //    collectionSelector
                                        //    resultSelector
                                        //SelectMany<TSource,TResult>
                                        //    O
                                        //    o=>Select<TCollection,TResult>Select_source
                                        //        MethodCall1_Arguments_1(o)
                                        //        i=>resultSelector(o,i)
                                        Select_source=Expression.Invoke(
                                            MethodCall1_Arguments_1,
                                            作業配列.Expressions設定(SelectMany_collectionSelector_o)
                                        );
                                        SelectMany_Parameters=作業配列.Parameters設定(SelectMany_collectionSelector_o);
                                    }
                                }
                                return Expression.Call(
                                    SelectMany,
                                    MethodCall1_Arguments_0,
                                    Expression.Lambda(
                                        Expression.Call(
                                            Select,
                                            Select_source,
                                            Select_selector
                                        ),
                                        SelectMany_Parameters
                                    )
                                );
                            }
                        }
                        case nameof(Enumerable.Where): {
                            //A.Where((a)=>a==1).xxx()
                            //A.Where((a,index)=>a==index).xxx()
                            //A.Where((a,index)=>a==index).xxx()
                            //変えない。
                            if(Reflection.ExtensionEnumerable.Where_index!=MethodCall0_GenericMethodDefinition) {
                                var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                                var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                                if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                                    //if(Reflection.ExtensionEnumerable.Where_index!=MethodCall0_GenericMethodDefinition&&ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                                    var MethodCall1_MethodCall_Method = MethodCall1_MethodCall.Method;
                                    switch(MethodCall1_MethodCall_Method.Name) {
                                        case nameof(ExtensionSet.Except):
                                        case nameof(ExtensionSet.Intersect):
                                        case nameof(ExtensionSet.Union): {
                                            //O               .Intersect(I).Where(p=>p==1)
                                            //O.Where(p=>p==1).Intersect(I.Where(p=>p==1))
                                            var MethodCall0_MethodCall0_Arguments = MethodCall1_MethodCall.Arguments;
                                            return Expression.Call(
                                                MethodCall1_MethodCall_Method,
                                                Expression.Call(MethodCall0_Method,MethodCall0_MethodCall0_Arguments[0],MethodCall1_Arguments_1),
                                                Expression.Call(MethodCall0_Method,MethodCall0_MethodCall0_Arguments[1],MethodCall1_Arguments_1)
                                            );
                                        }
                                        case nameof(ExtensionSet.GroupJoin): {
                                            if(
                                                MethodCall1_Arguments_1 is LambdaExpression predicate&&
                                                MethodCall1_MethodCall.Arguments[3] is LambdaExpression resultSelector&&
                                                resultSelector.Body is NewExpression New&&
                                                New.Type.IsAnonymousValueTuple()
                                            ) {
                                                //Where
                                                //    GroupJoin
                                                //        O
                                                //        I
                                                //        outerKeySelector
                                                //        innerKeySelector
                                                //        (o,i)=>new{o,i=i.Count()}
                                                //    oi=>oi.o==1&&oi.i==2
                                                //Where
                                                //    GroupJoin
                                                //        Where
                                                //            O
                                                //            o=>o==1
                                                //        I
                                                //        outerKeySelector
                                                //        innerKeySelector
                                                //        (o,i)=>new{o,i=i.Count()}
                                                //        oi=>oi.i==2
                                                var predicate_Parameters = predicate.Parameters;
                                                var resultSelector_Parameters = resultSelector.Parameters;
                                                var resultSelector_o = resultSelector_Parameters[0];
                                                var (OuterPredicate, OtherPredicate)=this._取得_New_OuterPredicate_InnerPredicate.実行(
                                                    New,
                                                    predicate.Body,
                                                    resultSelector_o
                                                );
                                                var MethodCall1_MethodCall_Arguments = MethodCall1_MethodCall.Arguments;
                                                var Outer = MethodCall1_MethodCall_Arguments[0];
                                                var Inner = MethodCall1_MethodCall_Arguments[1];
                                                if(OuterPredicate is not null) {
                                                    Outer=this.Outer又はInnerにWhereを付ける(
                                                        Outer,
                                                        MethodCall0_GenericMethodDefinition,
                                                        resultSelector_o.Type,
                                                        作業配列.Parameters設定(resultSelector_o),
                                                        OuterPredicate
                                                    );
                                                }
                                                var GroupJoin = Expression.Call(
                                                    MethodCall1_MethodCall.Method,
                                                    Outer,
                                                    Inner,
                                                    MethodCall1_MethodCall_Arguments[2],//o=>,i=>
                                                    resultSelector//(o,i)=>
                                                );
                                                if(OtherPredicate is not null) {
                                                    GroupJoin=Expression.Call(
                                                        MethodCall0_Method,
                                                        GroupJoin,
                                                        Expression.Lambda(OtherPredicate,predicate_Parameters)
                                                    );
                                                }
                                                return GroupJoin;
                                            }
                                            break;
                                        }
                                        case nameof(ExtensionSet.Select): {
                                            if(MethodCall1_Arguments_1 is LambdaExpression predicate) {
                                                if(MethodCall1_MethodCall.Arguments[1] is LambdaExpression selector) {
                                                    //A.Select(a=>new{a}).Where(w=>w.a==3)
                                                    //A.Where(w=>w==3).Select(a=>new{a})
                                                    var selector_Body = selector.Body;
                                                    var selector_Parameters = selector.Parameters;
                                                    var predicate_Parameters = predicate.Parameters;
                                                    var Where0 = Expression.Call(
                                                        this._作業配列.MakeGenericMethod(MethodCall0_GenericMethodDefinition,MethodCall1_MethodCall_Method.GetGenericArguments()[0]),
                                                        MethodCall1_MethodCall.Arguments[0],
                                                        Expression.Lambda(
                                                            this.Select_Where再帰で匿名型を走査(
                                                                selector_Body,
                                                                predicate_Parameters[0],
                                                                predicate.Body
                                                            ),
                                                            selector_Parameters
                                                        )
                                                    );
                                                    var Where1 = this.Call(Where0);
                                                    return Expression.Call(
                                                        MethodCall1_MethodCall_Method,
                                                        Where1,
                                                        selector
                                                    );
                                                }
                                            }
                                            break;
                                        }
                                        case nameof(ExtensionSet.SelectMany): {
                                            if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                                                //Where
                                                //    SelectMany
                                                //        O
                                                //        o=>I
                                                var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                                    MethodCall0_Method,
                                                    MethodCall1_MethodCall,
                                                    MethodCall1_Arguments_1
                                                );
                                                //todo goto SelectMany;
                                                Debug.Assert(Reflection.ExtensionEnumerable.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.SelectMany_indexSelector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionSet.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition());
                                                return this.Call(SelectMany);
                                            }
                                            break;
                                        }
                                        case nameof(ExtensionSet.Where): {
                                            if(Reflection.ExtensionEnumerable.Where_index!=MethodCall1_MethodCall_Method.GetGenericMethodDefinition()) {
                                                var MethodCall1_MethodCall0_Arguments = MethodCall1_MethodCall.Arguments;
                                                if(MethodCall1_Arguments_1 is LambdaExpression predicate外) {
                                                    var predicate外_Parameters = predicate外.Parameters;
                                                    if(MethodCall1_MethodCall0_Arguments[1]is LambdaExpression predicate内) {
                                                        //Where(p=>p==3).Where(q=>q==2)
                                                        //Where(q=>p==3&&q==2)
                                                        //Where(q=>q==3&&q==2)
                                                        return Expression.Call(
                                                            MethodCall0_Method,
                                                            MethodCall1_MethodCall0_Arguments[0],
                                                            Expression.Lambda(
                                                                predicate外.Type,
                                                                Expression.AndAlso(
                                                                    predicate外.Body,
                                                                    this.変換_旧Parameterを新Expression1.実行(
                                                                        predicate内.Body,
                                                                        predicate内.Parameters[0],
                                                                        predicate外_Parameters[0]
                                                                    )
                                                                ),
                                                                predicate外_Parameters
                                                            )
                                                        );
                                                    } else {
                                                        //Where(predicate).Where(q=>q==3)
                                                        //Where(q=>predicate(q)&&q==3)
                                                        return Expression.Call(
                                                            MethodCall0_Method,
                                                            MethodCall1_MethodCall0_Arguments[0],
                                                            Expression.Lambda(
                                                                predicate外.Type,
                                                                Expression.AndAlso(
                                                                    Expression.Invoke(
                                                                        MethodCall1_MethodCall0_Arguments[1],
                                                                        predicate外_Parameters[0]
                                                                    ),
                                                                    predicate外.Body
                                                                ),
                                                                predicate外_Parameters
                                                            )
                                                        );
                                                    }
                                                } else {
                                                    if(MethodCall1_MethodCall0_Arguments[1] is LambdaExpression predicate内) {
                                                        //Where(p=>p==3).Where(predicate)
                                                        //Where(p=>p==3&&predicate(p))
                                                        var predicate内_Parameters = predicate内.Parameters;
                                                        return Expression.Call(
                                                            MethodCall0_Method,
                                                            MethodCall1_MethodCall0_Arguments[0],
                                                            Expression.Lambda(
                                                                MethodCall1_Arguments_1.Type,
                                                                Expression.AndAlso(
                                                                    predicate内.Body,
                                                                    Expression.Invoke(
                                                                        MethodCall1_Arguments_1,
                                                                        predicate内_Parameters[0]
                                                                    )
                                                                ),
                                                                predicate内_Parameters
                                                            )
                                                        );
                                                    } else {
                                                        //Where(predicate0).Where(predicate1)
                                                        //Where(p=>predicate0(p)&&predicate1(p))
                                                        var MethodCall1_Arguments_1_Type = MethodCall1_Arguments_1.Type;
                                                        var p = Expression.Parameter(
                                                            MethodCall1_Arguments_1_Type.GetGenericArguments()[0],
                                                            $"Whereﾟ{this.番号++}"
                                                        );
                                                        var Expressions = 作業配列.Expressions設定(p);
                                                        var Left = Expression.Invoke(
                                                            MethodCall1_MethodCall0_Arguments[1],
                                                            Expressions
                                                        );
                                                        var Right = Expression.Invoke(
                                                            MethodCall1_Arguments_1,
                                                            Expressions
                                                        );
                                                        return Expression.Call(
                                                            MethodCall0_Method,
                                                            MethodCall1_MethodCall0_Arguments[0],
                                                            Expression.Lambda(
                                                                MethodCall1_Arguments_1_Type,
                                                                Expression.AndAlso(
                                                                    Left,
                                                                    Right
                                                                ),
                                                                作業配列.Parameters設定(p)
                                                            )
                                                        );
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                if(MethodCall0_Arguments.Count==1&&MethodCall0_Arguments[0]is ConstantExpression{Value: string Value}){
                    if(ILで直接埋め込めるか(MethodCall0.Type)){
                        if(Reflection.Byte   .Parse_s==MethodCall0_Method)return Expression.Constant(byte  .Parse(Value),MethodCall0.Type);
                        if(Reflection.Int16  .Parse_s==MethodCall0_Method)return Expression.Constant(short .Parse(Value),MethodCall0.Type);
                        if(Reflection.Int32  .Parse_s==MethodCall0_Method)return Expression.Constant(int   .Parse(Value),MethodCall0.Type);
                        if(Reflection.Int64  .Parse_s==MethodCall0_Method)return Expression.Constant(long  .Parse(Value),MethodCall0.Type);
                        if(Reflection.Single .Parse_s==MethodCall0_Method)return Expression.Constant(float .Parse(Value),MethodCall0.Type);
                        if(Reflection.Double .Parse_s==MethodCall0_Method)return Expression.Constant(double.Parse(Value),MethodCall0.Type);
                        if(Reflection.Boolean.Parse_s==MethodCall0_Method)return Expression.Constant(bool  .Parse(Value),MethodCall0.Type);
                    }else{
                        if(Reflection.Decimal       .Parse_s==MethodCall0_Method)return 共通(decimal       .Parse(Value));
                        //if(Reflection.DateTimeOffset.Parse_s==MethodCall0_Method)return 共通(DateTimeOffset.Parse(Value));
                        if(Reflection.DateTimeOffset.Parse_input==MethodCall0_Method)return 共通(DateTimeOffset.ParseExact(Value,CommonLibrary.日時Formats,null));
                        Expression 共通(object Value0) {
                            var Constant=Expression.Constant(Value0,MethodCall0.Type);
                            if(!this.DictionaryConstant.ContainsKey(Constant))
                                this.DictionaryConstant.Add(Constant,default!);
                            return Constant;
                        }
                    }
                }
                return Expression.Call(MethodCall0_Method,this.TraverseExpressions(MethodCall0_Arguments));
            }else{
                Debug.Assert(MethodCall0!=null);
                Debug.Assert(MethodCall0.Object!=null,"MethodCall0.Object != null");
                var MethodCall1_Object = this.Traverse(MethodCall0.Object);
                var MethodCall1_Object_Type = MethodCall1_Object.Type;
                //インスタンスメソッドで、仮想メソッドの場合よりインスタンスの変数の型一致していてsealedの場合それを選ぶ。
                foreach(var ChildMethod in MethodCall1_Object_Type.GetMethods(BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public)) {
                    if((ChildMethod.IsFinal||MethodCall1_Object_Type.IsSealed)&&ChildMethod.GetBaseDefinition()==MethodCall0_Method) {
                        MethodCall0_Method=ChildMethod;
                        break;
                    }
                }
                return Expression.Call(
                    MethodCall1_Object,
                    MethodCall0_Method,
                    this.TraverseExpressions(MethodCall0_Arguments)
                );
            }
        }
        private Expression? 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,Expression MethodCall1_Arguments_0) {
            if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                switch(MethodCall1_MethodCall.Method.Name) {
                    case nameof(ExtensionSet.SelectMany): {
                        if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                            var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_MethodCall
                            );
                            return this.Call(SelectMany);
                        }
                        break;
                    }
                }
            }
            return null;
        }
        private Expression? 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,Expression MethodCall1_Arguments_0,Expression MethodCall1_Arguments_1) {
            if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)){
                switch(MethodCall1_MethodCall.Method.Name) {
                    case nameof(ExtensionSet.SelectMany): {
                        if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                            var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_MethodCall,
                                MethodCall1_Arguments_1
                            );
                            return this.Call(SelectMany);
                        }
                        break;
                    }
                }
            }
            return null;
        }
        private Expression? 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,Expression MethodCall1_Arguments_0,Expression MethodCall1_Arguments_1,Expression MethodCall1_Arguments_2) {
            if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                switch(MethodCall1_MethodCall.Method.Name) {
                    case nameof(ExtensionSet.SelectMany): {
                        if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                            var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_MethodCall,
                                MethodCall1_Arguments_1,
                                MethodCall1_Arguments_2
                            );
                            return this.Call(SelectMany);
                        }
                        break;
                    }
                }
            }
            return null;
        }
        private MethodCallExpression 内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,MethodCallExpression MethodCall1_MethodCall) {
            var MethodCall1_MethodCall_Arguments = MethodCall1_MethodCall.Arguments;
            Debug.Assert(MethodCall1_MethodCall_Arguments.Count==2);
            LambdaExpression selector1;
            if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
                Debug.Assert(
                    Reflection.ExtensionSet.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                    Reflection.ExtensionEnumerable.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()
                );
                //MethodCall0_Method
                //    MethodCall1_MethodCall_Method           SelectMany
                //        MethodCall1_MethodCall_Arguments[0]
                //        o=>                                 selector0.Parameters
                //            I                               selector0.Body
                //MethodCall1_MethodCall_Method               SelectMany
                //    MethodCall1_MethodCall_Arguments[0]
                //    o=>                                     selector0.Parameters
                //        MethodCall0_Method
                //            I                               selector0.Body
                var selector0_Body = Expression.Call(MethodCall0_Method,selector0.Body);
                var selector1_Body = this.Call(selector0_Body);
                selector1=Expression.Lambda(selector1_Body,selector0.Parameters);
            } else {
                //MethodCall0_Method
                //    MethodCall1_MethodCall_Method           SelectMany
                //        MethodCall1_MethodCall_Arguments[0]
                //        MethodCall1_MethodCall_Arguments[1]
                //MethodCall1_MethodCall_Method               SelectMany
                //    MethodCall1_MethodCall_Arguments[0]
                //    o=>
                //        MethodCall0_Method
                //            MethodCall1_MethodCall_Arguments[1].Invoke(o)
                var o = Expression.Parameter(MethodCall1_MethodCall.Method.GetGenericArguments()[0],"o");
                var 作業配列 = this._作業配列;
                var selector0_Body = Expression.Call(
                    MethodCall0_Method,
                    Expression.Invoke(MethodCall1_MethodCall_Arguments[1],作業配列.Expressions設定(o))
                );
                var selector1_Body = this.Call(selector0_Body);
                selector1=Expression.Lambda(selector1_Body,作業配列.Parameters設定(o));
            }
            return Expression.Call(
                MethodCall1_MethodCall.Method,
                MethodCall1_MethodCall_Arguments[0],
                selector1
            );
        }
        private MethodCallExpression 内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,MethodCallExpression MethodCall1_MethodCall,Expression MethodCall1_Arguments_1) {
            var MethodCall1_MethodCall_Arguments = MethodCall1_MethodCall.Arguments;
            Debug.Assert(MethodCall1_MethodCall_Arguments.Count==2);
            LambdaExpression selector1;
            Expression selector1_Body;
            if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
                Debug.Assert(
                    Reflection.ExtensionEnumerable.SelectMany_indexSelector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                    Reflection.ExtensionEnumerable.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                    Reflection.ExtensionSet.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()
                );
                //MethodCall0_Method
                //    MethodCall1_MethodCall_Method           SelectMany
                //        MethodCall1_MethodCall_Arguments[0]
                //        o=>                                 selector0.Parameters
                //            I                               selector0.Body
                //    MethodCall1_Arguments[1]
                //MethodCall1_MethodCall_Method               SelectMany
                //    MethodCall1_MethodCall_Arguments[0]
                //    o=>                                     selector0.Parameters
                //        MethodCall0_Method
                //            I                               selector0.Body
                //            MethodCall1_Arguments[1]
                //Types2[0]=IEnumerable1のT(selector0.ReturnType);
                var selector0_Body = Expression.Call(
                    MethodCall0_Method,
                    selector0.Body,
                    MethodCall1_Arguments_1
                );
                selector1_Body = this.Call(selector0_Body);
                selector1=Expression.Lambda(
                    selector1_Body,
                    selector0.Parameters
                );
            } else {
                //MethodCall0_Method
                //    MethodCall1_MethodCall_Method           SelectMany
                //        MethodCall1_MethodCall_Arguments[0]
                //        MethodCall1_MethodCall_Arguments[1]
                //    MethodCall1_Arguments[1]
                //MethodCall1_MethodCall_Method               SelectMany
                //    MethodCall1_MethodCall_Arguments[0]
                //    o=>
                //        MethodCall0_Method
                //            MethodCall1_MethodCall_Arguments[1].Invoke(o)
                //            MethodCall1_Arguments[1]
                var 作業配列 = this._作業配列;
                var o = Expression.Parameter(MethodCall1_MethodCall.Method.GetGenericArguments()[0],"o");
                var Invoke=Expression.Invoke(
                    MethodCall1_MethodCall_Arguments[1],
                    作業配列.Expressions設定(o)
                );
                var selector0_Body = Expression.Call(
                    MethodCall0_Method,
                    Invoke,
                    MethodCall1_Arguments_1
                );
                selector1_Body = this.Call(selector0_Body);
                selector1=Expression.Lambda(
                    selector1_Body,
                    作業配列.Parameters設定(o)
                );
            }
            var MethodCall1_MethodCall_Method = MethodCall1_MethodCall.Method;
            var MethodCall1_MethodCall_GenericMethodDefinition=MethodCall1_MethodCall_Method.GetGenericMethodDefinition();
            if(typeof(ExtensionSet)==MethodCall1_MethodCall_GenericMethodDefinition.DeclaringType) {
                Type? Set1 = selector1_Body.Type;
                while(true) {
                    if(Set1 is null) {
                        //IEnumerable<>
                        if(MethodCall1_MethodCall_GenericMethodDefinition==Reflection.ExtensionSet.SelectMany_selector) {
                            MethodCall1_MethodCall_GenericMethodDefinition=Reflection.ExtensionEnumerable.SelectMany_selector;
                        }
                        break;
                    }
                    var GenericTypeDefinition = Set1;
                    if(GenericTypeDefinition.IsGenericType) {
                        GenericTypeDefinition=Set1.GetGenericTypeDefinition();
                    }
                    if(GenericTypeDefinition==typeof(ImmutableSet<>)) {
                        break;
                    }
                    Set1=Set1.BaseType;
                }
            }
            var MethodCall1_MethodCall_Arguments_0 = MethodCall1_MethodCall_Arguments[0];
            var GenericArguments=MethodCall1_MethodCall_Method.GetGenericArguments();
            GenericArguments[1]=IEnumerable1のT(selector1.ReturnType);
            return Expression.Call(
                MethodCall1_MethodCall_GenericMethodDefinition.MakeGenericMethod(GenericArguments),
                MethodCall1_MethodCall_Arguments_0,
                selector1
            );
        }
        private MethodCallExpression 内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,MethodCallExpression MethodCall1_MethodCall,Expression MethodCall1_Arguments_1,Expression MethodCall1_Arguments_2) {
            var MethodCall1_MethodCall_Arguments = MethodCall1_MethodCall.Arguments;
            Debug.Assert(MethodCall1_MethodCall_Arguments.Count==2);
            LambdaExpression selector1;
            Expression selector1_Body;
            if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
                Debug.Assert(
                    Reflection.ExtensionSet.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                    Reflection.ExtensionEnumerable.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()
                );
                //MethodCall0_Method
                //    MethodCall1_MethodCall_Method           SelectMany
                //        MethodCall1_MethodCall_Arguments[0]
                //        o=>                                 selector0.Parameters
                //            I                               selector0.Body
                //    MethodCall1_Arguments[1]
                //    MethodCall1_Arguments[2]
                //MethodCall1_MethodCall_Method               SelectMany
                //    MethodCall1_MethodCall_Arguments[0]
                //    o=>                                     selector0.Parameters
                //        MethodCall0_Method
                //            I                               selector0.Body
                //            MethodCall1_Arguments[1]
                //            MethodCall1_Arguments[2]
                var selector0_Body = Expression.Call(
                    MethodCall0_Method,
                    selector0.Body,
                    MethodCall1_Arguments_1,
                    MethodCall1_Arguments_2
                );
                selector1_Body=this.Call(selector0_Body);
                selector1=Expression.Lambda(
                    selector1_Body,
                    selector0.Parameters
                );
            } else {
                //MethodCall0_Method
                //    MethodCall1_MethodCall_Method           SelectMany
                //        MethodCall1_MethodCall_Arguments[0]
                //        MethodCall1_MethodCall_Arguments[1]
                //    MethodCall1_Arguments[1]
                //    MethodCall1_Arguments[2]
                //MethodCall1_MethodCall_Method               SelectMany
                //    MethodCall1_MethodCall_Arguments[0]
                //    o=>
                //        MethodCall0_Method
                //            MethodCall1_MethodCall_Arguments[1].Invoke(o)
                //            MethodCall1_Arguments[1]
                //            MethodCall1_Arguments[2]
                var o = Expression.Parameter(MethodCall1_MethodCall.Method.GetGenericArguments()[0],"o");
                var 作業配列 = this._作業配列;
                var selector0_Body = Expression.Call(
                    MethodCall0_Method,
                    Expression.Invoke(
                        MethodCall1_MethodCall_Arguments[1],
                        作業配列.Expressions設定(o)
                    ),
                    MethodCall1_Arguments_1,
                    MethodCall1_Arguments_1
                );
                selector1_Body=this.Call(selector0_Body);
                selector1=Expression.Lambda(
                    selector1_Body,
                    作業配列.Parameters設定(o)
                );
            }
            var MethodCall1_MethodCall_Method = MethodCall1_MethodCall.Method;
            if(typeof(ExtensionSet)==MethodCall1_MethodCall_Method.DeclaringType) {
                Type? Set1 = selector1_Body.Type;
                while(true) {
                    if(Set1 is null) {
                        //IEnumerable<>
                        if(MethodCall1_MethodCall_Method.GetGenericMethodDefinition()==Reflection.ExtensionSet.SelectMany_selector) {
                            MethodCall1_MethodCall_Method=Reflection.ExtensionEnumerable.SelectMany_selector.MakeGenericMethod(MethodCall1_MethodCall_Method.GetGenericArguments());
                        }
                        break;
                    }
                    var GenericTypeDefinition = Set1;
                    if(GenericTypeDefinition.IsGenericType) {
                        GenericTypeDefinition=Set1.GetGenericTypeDefinition();
                    }
                    if(GenericTypeDefinition==typeof(ImmutableSet<>)) {
                        break;
                    }
                    Set1=Set1.BaseType;
                }
            }
            return Expression.Call(
                MethodCall1_MethodCall_Method,
                MethodCall1_MethodCall_Arguments[0],
                selector1
            );
        }
        /// <summary>
        /// ローカル関数にするとビルドがフリーズする。
        /// </summary>
        /// <param name="outer又はinner"></param>
        /// <param name="Where"></param>
        /// <param name="Where_T"></param>
        /// <param name="Where_Parameters"></param>
        /// <param name="OuterPredicate又はInnerPredicate"></param>
        /// <returns></returns>
        private Expression Outer又はInnerにWhereを付ける(Expression outer又はinner,MethodInfo Where,Type Where_T,IEnumerable<ParameterExpression> Where_Parameters,Expression OuterPredicate又はInnerPredicate) => Expression.Call(
            this._作業配列.MakeGenericMethod(
                Where,
                Where_T
            ),
            outer又はinner,
            Expression.Lambda(
                OuterPredicate又はInnerPredicate,
                Where_Parameters
            )
        );
        /// <summary>
        /// ローカル関数にするとビルドがフリーズする。
        /// </summary>
        /// <param name="匿名"></param>
        /// <param name="メンバー参照"></param>
        /// <param name="対象"></param>
        /// <returns></returns>
        private Expression Select_Where再帰で匿名型を走査(Expression 匿名,Expression メンバー参照,Expression 対象) {
            if(匿名 is NewExpression NewExpression) {
                var NewExpression_Type = NewExpression.Type;
                var IsAnonymous = NewExpression_Type.IsAnonymous();
                var IsValueTuple = NewExpression_Type.IsValueTuple();
                if(IsAnonymous||IsValueTuple) {
                    var NewExpression_Constructor_GetParameters = NewExpression.Constructor.GetParameters();
                    if(IsAnonymous) {
                        var NewExpression_Arguments = NewExpression.Arguments;
                        var NewExpression_Arguments_Count = NewExpression_Arguments.Count;
                        Debug.Assert(NewExpression_Constructor_GetParameters.Length==NewExpression_Arguments_Count);
                        for(var a = 0;a<NewExpression_Arguments_Count;a++) {
                            var NewExpression_Argument = NewExpression_Arguments[a];
                            対象=this.Select_Where再帰で匿名型を走査(
                                NewExpression_Argument,
                                Expression.Property(
                                    メンバー参照,
                                    NewExpression_Constructor_GetParameters[a].Name
                                ),
                                対象
                            );
                        }
                    } else {
                        Debug.Assert(IsValueTuple);
                        var Instance = メンバー参照;
                        var Index = 0;
                        foreach(var NewExpression_Argument in NewExpression.Arguments) {
                            switch(Index) {
                                case 0: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item1)),対象); Index=1; break;
                                case 1: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item2)),対象); Index=2; break;
                                case 2: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item3)),対象); Index=3; break;
                                case 3: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item4)),対象); Index=4; break;
                                case 4: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item5)),対象); Index=5; break;
                                case 5: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item6)),対象); Index=6; break;
                                case 6: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item7)),対象); Index=7; break;
                                default: Instance=Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Rest)); goto case 0;
                            }
                        }
                    }
                }
            }
            return this.変換_旧Expressionを新Expression1.実行(対象,メンバー参照,匿名);
        }
    }
}
//2022/05/30 2788
//2022/04/02 3123
//2022/03/23 3192
