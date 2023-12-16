using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Databases.PrimaryKeys;
using LinqDB.Helpers;
using LinqDB.Sets;
using TestLinqDB.Serializers;

using Expression = System.Linq.Expressions.Expression;
using ExtensionSet = LinqDB.Sets.ExtensionSet;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_跨ぎParameterの先行評価 : 共通{
    struct OI
    {
        public readonly int o, i;
        public OI(int o, int i)
        {
            this.o=o;
            this.i=i;
        }
    }
    class SetT : Set<OI>
    {

    }
    public class 取得_先行評価式:共通{
        private void TraceWrite(Expression Expression){
            this.Optimizer.Lambda最適化(Expression);
        }
        class A{
            private int a;
        }
        class B:A{
        }
        class C:B{
        }
        [Fact]
        public void 変形確認(){
            var s=new Set<int>();
            var x0=typeof(A).GetField("a",BindingFlags.Instance|BindingFlags.NonPublic);
            var x1=typeof(B).GetField("a",BindingFlags.Instance|BindingFlags.NonPublic);
            var x2=typeof(C).GetField("a",BindingFlags.Instance|BindingFlags.NonPublic);
            var x3=typeof(B).GetField("a",BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.FlattenHierarchy);
            var x4=typeof(C).GetField("a",BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.FlattenHierarchy);
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Let(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Let(
                            c=>new{a=a+a,b=b+b,c=c+c}
                        ).ToString()+new{a=a+a,b=b+b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Let(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )+"".Let(
                        c=>new{a,c}
                    ).ToString()+new{a}
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Inline(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Inline(
                            c=>new{a=a+a,b=b+b,c=c+c}
                        ).ToString()+new{a=a+a,b=b+b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Inline(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )+"".Inline(
                        c=>new{a,c}
                    ).ToString()+new{a}
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Inline(
                        b=>"".Let(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Inline(
                        b=>"".Let(
                            c=>new{a=a+a,b=b+b,c=c+c}
                        ).ToString()+new{a=a+a,b=b+b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Inline(
                        b=>"".Let(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )+"".Let(
                        c=>new{a,c}
                    ).ToString()+new{a}
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Inline(
                        b=>"".Inline(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Inline(
                        b=>"".Inline(
                            c=>new{a=a+a,b=b+b,c=c+c}
                        ).ToString()+new{a=a+a,b=b+b}
                    )
                )
            );
            this.TraceWrite(
                ()=>"".Let(
                    a=>"".Inline(
                        b=>"".Inline(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )+"".Inline(
                        c=>new{a,c}
                    ).ToString()+new{a}
                )
            );
        }
        [Fact]public void Traverse(){
            var s=new Set<int>();
            this.Optimizer.Lambda最適化(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Let(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )
                )
            );
            this.Optimizer.Lambda最適化(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Let(
                            c=>new{a=a+a,b=b+b,c=c+c}
                        ).ToString()+new{a=a+a,b=b+b}
                    )
                )
            );
            this.Optimizer.Lambda最適化(
                ()=>"".Let(
                    a=>"".Let(
                        b=>"".Let(
                            c=>new{a,b,c}
                        ).ToString()+new{a,b}
                    )+"".Let(
                        c=>new{a,c}
                    ).ToString()+new{a}
                )
            );
            this.Optimizer.Lambda最適化(()=>"".Let(a=>"".Let(b=>"".Let(c=>new{a=a+a,b=b+b,c=c+c}).ToString()+new{a=a+a,b=b+b})));

            this.Optimizer.Lambda最適化(()=>"".Let(a=>"".Let(b=>a+a)));
            this.Optimizer.Lambda最適化(()=>"".Let(a=>"".Inline(b=>a)));
            this.Optimizer.Lambda最適化(()=>"".Let(a=>"".Inline(b=>a+a)));
            this.Optimizer.Lambda最適化(()=>"".Let(a=>a));
            this.Expression実行AssertEqual(()=>s.Select(p=>s.Select(q=>new{a=s.Intersect(s),b=s.Intersect(s)})));
            this.Expression実行AssertEqual(()=>s.Select(p=>s.Select(q=>q+1)));
            //if(this.結果Expression is not null)return;
            this.Expression実行AssertEqual(()=>s.Join(s,o=>o,i=>i,(o,i)=>new{o,i}).Where(p=>p.i==0));//0
            //switch(e.NodeType) {
            //    case ExpressionType.Constant: {
            //        if(ILで直接埋め込めるか((ConstantExpression)e))return;
            //0
            //0
            //    }
            //    case ExpressionType.Default:return;
            //    case ExpressionType.Parameter: {
            //        if(this.ラムダ跨ぎParameters.Contains(e))return;
            //0
            //        if(this.ループ跨ぎParameters.Contains(e))return;
            //
            //0
            //    }
            //}
            //if(e.Type!=typeof(void)) {
            //    if(this.結果の場所==場所.ループ跨ぎ) {
            //        if((e.NodeType!=ExpressionType.Lambda)&&e.NodeType!=ExpressionType.Parameter) {
            //            var Result = this._判定_移動できるか.実行(e);
            //            if(Result==判定_移動できるか.EResult.移動できる) {
            //0
            //            }
            //0
            //        }
            //    } else if(this.結果の場所==場所.ラムダ跨ぎ) {
            //        if(e.NodeType!=ExpressionType.Lambda) {
            //            var Result = this._判定_移動できるか.実行(e);
            //            if(Result==判定_移動できるか.EResult.移動できる) {
            //0
            //            }
            //0
            //        }
            //    }
            //0
            //}
            //0
        }
        [Fact]
        public void Traverse1(){
            var s=new Set<int>();
            //if(this.結果Expression is not null)return;
            this.Expression実行AssertEqual(()=>s.Join(s,o=>new{key=o},i=>new{key=i},(o,i)=>o+i));
            //switch(e.NodeType) {
            //    case ExpressionType.Constant: {
            //        if(ILで直接埋め込めるか((ConstantExpression)e))return;
            this.Expression実行AssertEqual(()=>0);
            this.Expression実行AssertEqual(()=>s);
            //    }
            //    case ExpressionType.Default:return;
            this.ExpressionAssertEqual(Expression.Lambda<Action>(Expression.Default(typeof(int))));
            //    case ExpressionType.Parameter: {
            //        if(this.ラムダ跨ぎParameters.Contains(e)||this.ループ跨ぎParameters.Contains(e))return;
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Let(b=>a)));
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Inline(b=>a)));
            this.Expression実行AssertEqual(()=>"".Let(a=>a));
            //    }
            //}
            //if(e.Type!=typeof(void)) {
            //    if(this.結果の場所==場所.ループ跨ぎ) {
            //        if((this.ラムダ式は取り出す||e.NodeType!=ExpressionType.Lambda)&&e.NodeType!=ExpressionType.Parameter) {
            //            var Result = this._判定_移動できるか.実行(e);
            //            if(Result==判定_移動できるか.EResult.移動できる) {
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Inline(b=>a)));
            //            }
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Inline(b=>b)));
            //            if(Result==判定_移動できるか.EResult.NoLoopUnrollingがあったので移動できない)return;
            //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(() => "".Let(a=>"".Inline(b=>a).NoLoopUnrolling()));
            //        }
            //    } else if(this.結果の場所==場所.ラムダ跨ぎ) {
            //        if(this.ラムダ式は取り出す||e.NodeType!=ExpressionType.Lambda) {
            //            var Result = this._判定_移動できるか.実行(e);
            //            if(Result==判定_移動できるか.EResult.移動できる) {
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Let(b=>a)));
            //            }
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Let(b=>b)));
            //            if(Result==判定_移動できるか.EResult.NoLoopUnrollingがあったので移動できない)return;
            //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(() => "".Let(a=>"".Let(b=>a).NoLoopUnrolling()));
            //        }
            //    }
            //}
            this.Expression実行AssertEqual(()=>s.ToString());
        }
        [Fact]
        public void Call(){
            var s=new Set<int>();
            //if(Reflection.Helpers.NoLoopUnrolling==MethodCall_GenericMethodDefinition)
            this.Expression実行AssertEqual(()=>ExtensionSet.Inline(()=>"").NoLoopUnrolling());//0
            //if(this.IsInline&&ループ展開可能メソッドか(MethodCall_GenericMethodDefinition)) {
            //    switch(MethodCall_GenericMethodDefinition.Name) {
            //        case nameof(ExtensionSet.Inline): {
            //            if(MethodCall0_Arguments.Count==1) {
            //                if(MethodCall0_Arguments_0 is LambdaExpression Lambda0) {
            this.Expression実行AssertEqual(()=>ExtensionSet.Inline(()=>""));
            //                }else{
            this.Expression実行AssertEqual(()=>ExtensionSet.Inline(()=>1m));//1
            //                }
            //            }else{
            //                if(MethodCall0_Arguments_1 is LambdaExpression Lambda0) {
            this.Expression実行AssertEqual(()=>"".Inline(s=>s+"x"));
            //                } else {
            this.Expression実行AssertEqual(()=>"".Inline(s=>s+"x"));
            //                }
            //            }
            //        }
            //        case nameof(ExtensionSet.Intersect):
            //        case nameof(ExtensionSet.Union):
            //        case nameof(ExtensionSet.DUnion):
            //        case nameof(ExtensionSet.Except):
            //            if(this.結果Expression is not null)
            this.Expression実行AssertEqual(()=>"".Let(b=>s.Select(a=>a+a)).Except(s.Select(a=>a+a)));
            //            if(this.結果Expression is not null)
            this.Expression実行AssertEqual(()=>s.Select(a=>a+a).Except(s.Select(a=>a+a)));
            //            if(MethodCall.Arguments.Count==3)
            this.Expression実行AssertEqual(()=>s.Except(s,EqualityComparer<int>.Default));
            //            break;
            //        }
            //        default: {
            //            if(this.結果Expression is not null)
            this.Expression実行AssertEqual(()=>"".Let(b=>s.Select(a=>a+a)).Select(a=>a+a));
            //            for(var a = 1;a<MethodCall0_Arguments_Count;a++)
            //                if(巻き上げ処理(MethodCall0_Arguments[a])) 
            this.Expression実行AssertEqual(()=>s.Select(a=>1m));
            this.Expression実行AssertEqual(()=>s.Select(a=>a+1));
            var xx=new{a=4};
            //        }
            //    }
            //} else {
            this.Expression実行AssertEqual(()=>"".Let(s=>""));
            //}
            //bool 巻き上げ処理(Expression Expression0) {
            //    if(Expression0 is LambdaExpression Lambda0) {
            //        if(this.結果Expression is not null)
            //1
            //0
            //    } else if(Expression0.NodeType!=ExpressionType.Parameter) {
            this.Expression実行AssertEqual(()=>s.GroupBy(a=>a/2d,(a,b)=>new{a,b},EqualityComparer<double>.Default));
            //    }
            var GroupBy=(MethodCallExpression)GetLambda(()=>s.GroupBy(a=>a/2d,(a,b)=>new{a,b},EqualityComparer<double>.Default));
            var Default=GroupBy.Arguments[3];
            var p=Expression.Parameter(Default.Type);
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<object>>(
                    Expression.Block(
                        new[]{p},
                        Expression.Assign(p,Default),
                        Expression.Call(
                            GroupBy.Method,
                            GroupBy.Arguments[0],
                            GroupBy.Arguments[1],
                            GroupBy.Arguments[2],
                            p
                        )
                    )
                )
            );
            //}
        }
        [Fact]
        public void Block(){
            this.ExpressionAssertEqual(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Constant(0),
                        Expression.Constant(0)
                    )
                )
            );
        }
        public class 判定_移動できるか:共通{
            [Fact]public void MakeAssign(){
                var a=Expression.Parameter(typeof(int),"a");
                this.ExpressionAssertEqual(
                    Expression.Lambda<Action>(
                        Expression.Lambda<Func<int,int>>(
                            Expression.Block(
                                Expression.Lambda<Func<int>>(
                                    Expression.Assign(a,Expression.Constant(0))
                                ),
                                a
                            ),
                            a
                        )
                    )
                );
            }
            [Fact]
            public void Lambda(){
                var s=new Set<int>();
                this.Expression実行AssertEqual(()=>s.Join(s,o=>o,i=>i,(o,i)=>new{o,i}).Where(p=>p.i==0));
            }
            [Fact]
            public void Parameter(){
                var s=new Set<int>();
                var p=Expression.Parameter(typeof(int));
                //if(this.ContainerParameter==Parameter||this.ラムダ跨ぎParameters.Contains(Parameter)||this.ループ跨ぎParameters.Contains(Parameter))
                this.Expression実行AssertEqual(()=>s.Join(s,o=>o,i=>i,(o,i)=>new{o,i}).Where(p=>p.i==0));
                //foreach(var 内部Parameters in this.List内部Parameters)
                //    if(内部Parameters.Contains(Parameter)) return;
                this.Expression実行AssertEqual(()=>s.Join(s,o=>o,i=>i,(o,i)=>new{o,i}).Where(p=>p.i==0));
                //foreach(var a in this.List束縛Parameter情報){
                //    if(a.Parameters.Contains(Parameter)) return;
                this.Expression実行AssertEqual(()=>s.Join(s,o=>o,i=>i,(o,i)=>new{o,i}).Where(p=>p.i==0));
                //    foreach(var Variables in a.ListVariables)
                //        if(Variables.Contains(Parameter)){
                this.ExpressionAssertEqual(
                    Expression.Lambda<Action>(
                        Expression.Lambda<Func<Func<int>>>(
                            Expression.Block(
                                new[]{p},
                                Expression.Lambda<Func<int>>(
                                    p
                                )
                            )
                        )
                    )
                );
                //        }
                this.ExpressionAssertEqual(
                    Expression.Lambda<Action>(
                        Expression.Lambda<Func<Func<int>>>(
                            Expression.Block(
                                new[]{p},
                                Expression.Lambda<Func<int>>(
                                    Expression.Constant(0)
                                )
                            )
                        )
                    )
                );
                //}
            }
        }
    }
    public class 変換_先行評価式:共通{
        [Fact]
        public void Block(){
            this.ExpressionAssertEqual(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Constant(0),
                        Expression.Constant(0)
                    )
                )
            );
        }
        [Fact]
        public void Lambda(){
            var a=Expression.Parameter(typeof(int),"a");
            this.ExpressionAssertEqual(
                Expression.Lambda<Action>(
                    Expression.Lambda<Func<int,int>>(
                        Expression.Block(
                            Expression.Lambda<Func<int>>(
                                a
                            ),
                            a
                        ),
                        a
                    )
                )
            );
        }
        [Fact]
        public void Traverse(){
            var a=Expression.Parameter(typeof(int),"a");
            var s=new Set<int>();
            //    if(this.現在探索場所==this.希望探索場所&&this.ExpressionEqualityComparer.Equals(Expression0,this.旧Expression!)) {
            //        if(this.書き込み項か)this.書き戻しがあるか=true;
            this.ExpressionAssertEqual(
                Expression.Lambda<Action>(
                    Expression.Lambda<Func<int,int>>(
                        Expression.Block(
                            Expression.Lambda<Func<int>>(
                                Expression.Assign(a,Expression.Constant(0))
                            ),
                            a
                        ),
                        a
                    )
                )
            );
            //        else                 this.読み込みがあるか=true;
            this.ExpressionAssertEqual(
                Expression.Lambda<Action>(
                    Expression.Lambda<Func<int,int>>(
                        Expression.Block(
                            Expression.Lambda<Func<int>>(
                                a
                            ),
                            a
                        ),
                        a
                    )
                )
            );
        }
        [Fact]
        public void エラー(){
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Inline(b=>1m)));
        }
        [Fact]
        public void Call(){
            var s=new Set<int>();
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Inline(b=>1m)));
            //if(!(this.ループ跨ぎを使うか&&ループ展開可能メソッドか(MethodCall0)))
            this.Expression実行AssertEqual(()=>s.Select(a=>a+a));
            //switch(MethodCall_GenericMethodDefinition.Name) {
            //    case nameof(ExtensionSet.Inline): {
            //        if(MethodCall0_Arguments.Count==1) {
            this.Expression実行AssertEqual(()=>"".Let(a=>ExtensionSet.Inline(()=>1m)));
            //        }else{
            this.Expression実行AssertEqual(()=>"".Let(a=>"".Inline(b=>1m)));
            //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>ExtensionSet.Inline(()=>"".Inline(s=>s+"x")));
            //        }
            //    }
            //    default: {
            //        for(var a=1;a<MethodCall0_Arguments_Count;a++){
            this.Expression実行AssertEqual(()=>s.Select(a=>s));
            //        }
            //    }
            //}
        }
        [Fact]
        public void MakeAssign(){
            //if(Binary0_Right==Binary1_Right&&Binary0_Left==Binary1_Left&&Binary0_Conversion==Binary1_Conversion)
            var a=Expression.Parameter(typeof(int),"a");
            this.ExpressionAssertEqual(
                Expression.Lambda<Action>(
                    Expression.Block(
                        new[]{a},
                        Expression.AddAssign(a,Expression.Constant(0)),
                        Expression.Lambda<Func<decimal>>(
                            Expression.Add(
                                Expression.Constant(0m),
                                Expression.Constant(0m)
                            )
                        )
                    )
                )
            );
            this.Expression実行AssertEqual(()=>"");
        }
    }
    [Fact]
    public void Call()
    {
        var s = new Set<int>();
        //if(!ループ展開可能メソッドか(MethodCall0))
        this.Expression実行AssertEqual(() => s.Join(s, o => o, i => i, (o, i) => o+i));
        //if(Reflection.ExtensionSet.Inline1==GetGenericMethodDefinition(MethodCall0.Method)){
        //    if(MethodCall0_Arguments_0 is LambdaExpression Lambda0)
        this.Expression実行AssertEqual(() => ExtensionSet.Inline(() => ""));
        //    else
        this.Expression実行AssertEqual(() => ExtensionSet.Inline(Anonymous(() => "")));
        //}else if(Reflection.ExtensionSet.Inline2==GetGenericMethodDefinition(MethodCall0.Method)) {
        //    if(MethodCall0_Arguments_1 is LambdaExpression Lambda0)
        this.Expression実行AssertEqual(() => "".Inline(s => ""));
        //    else
        this.Expression実行AssertEqual(() => "".Inline(Anonymous((string s) => "")));
        //}
        //for(var a = 1;a<MethodCall0_Arguments_Count;a++)
        //    if(MethodCall0_Argument is LambdaExpression Lambda0)
        this.Expression実行AssertEqual(() => s.Select(p => p+1));
        //    else
        this.Expression実行AssertEqual(() => s.Select(Anonymous((int p) => p+1)));
    }
    [Fact]
    public void Block()
    {
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Constant(0),
                    Expression.Constant(0)
                )
            )
        );
        //if(Block0_Variables.Count==0&&LinkedList.Count==1&&Block0.Type==LinkedList.Last.Value.Type) return LinkedList.Last.Value;
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Constant(0)
                )
            )
        );
    }
    [Fact]
    public void 外だし()
    {
        var a = Expression.Parameter(typeof(int), "Pワーク変数a");
        var b = Expression.Parameter(typeof(int), "Pワーク変数b");
        var c = Expression.Parameter(typeof(int), "Pワーク変数c");
        var s = new Set<int>();
        //do {
        //    Debug.Assert(LinkedListNode!=null);
        //    var LinkedListNode_Value = LinkedListNode.Value;
        //    if(LinkedListNode_Value.NodeType==ExpressionType.Assign) {
        this.Expression実行AssertEqual(() => s.Join(s, o => o, i => i, (o, i) => o+i));
        //    }
        this.Expression実行AssertEqual(() => "");
        //    if(旧 is null) {
        this.Expression実行AssertEqual(() => "");
        //    } else {
        //        if(分離Expressionの場所==場所.ラムダ跨ぎ){
        this.Expression実行AssertEqual(() => "a".Let(o => o.Let(i => o)));
        //        } else{
        this.Expression実行AssertEqual(() => "a".Inline(a => a.Inline(b => a.Inline(c => a))));
        //        }
        //        do {
        this.Expression実行AssertEqual(() => s.Join(s, o => o, i => i, (o, i) => new { o, i }));
        //        } while(LinkedListNode0 is not null);
        this.Expression実行AssertEqual(() => "a".Let(o => o.Let(i => o)));
        //        if(読み込みがあるか)LinkedListNode=LinkedList.AddBefore(LinkedListNode,Expression.Assign(新,旧));
        this.Expression実行AssertEqual(() => "a".Let(o => o.Let(i => o)));
        //        if(書き込みがあるか)LinkedList.AddLast(Expression.Assign(旧,新));
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Lambda<Func<int, int>>(
                    Expression.Block(
                        Expression.Lambda<Func<int>>(
                            Expression.Assign(a, Expression.Constant(0))
                        ),
                        a
                    ),
                    a
                )
            )
        );
        this.Expression実行AssertEqual(() => "a".Let(o => o.Let(i => o)));
        //    }
        //} while(LinkedListNode is not null);
        //for(var Node = LinkedList.First;Node is not null;Node=Node.Next) Node.Value=this.Traverse(Node.Value);
        this.Expression実行AssertEqual(() => s.SelectMany(o => s.Let(i => new { o, i }).i.Where(i => i==3)));
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
